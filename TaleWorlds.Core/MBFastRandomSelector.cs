using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core
{
	// Token: 0x020000AC RID: 172
	public class MBFastRandomSelector<T>
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0001DE1A File Offset: 0x0001C01A
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x0001DE22 File Offset: 0x0001C022
		public ushort RemainingCount { get; private set; }

		// Token: 0x0600091D RID: 2333 RVA: 0x0001DE2B File Offset: 0x0001C02B
		public MBFastRandomSelector(ushort capacity = 32)
		{
			this.ReallocateIndexArray(capacity);
			this._list = null;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001DE41 File Offset: 0x0001C041
		public MBFastRandomSelector(MBReadOnlyList<T> list, ushort capacity = 32)
		{
			this.ReallocateIndexArray(capacity);
			this.Initialize(list);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001DE58 File Offset: 0x0001C058
		public void Initialize(MBReadOnlyList<T> list)
		{
			if (list != null && list.Count <= 65535)
			{
				this._list = list;
				this.TryExpand();
			}
			else
			{
				Debug.FailedAssert("Cannot initialize random selector as passed list is null or it exceeds " + ushort.MaxValue + " elements).", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.Core\\MBFastRandomSelector.cs", "Initialize", 63);
				this._list = null;
			}
			this.Reset();
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001DEBC File Offset: 0x0001C0BC
		public void Reset()
		{
			if (this._list != null)
			{
				if (this._currentVersion < 65535)
				{
					this._currentVersion += 1;
				}
				else
				{
					for (int i = 0; i < this._indexArray.Length; i++)
					{
						this._indexArray[i] = default(MBFastRandomSelector<T>.IndexEntry);
					}
					this._currentVersion = 1;
				}
				this.RemainingCount = (ushort)this._list.Count;
				return;
			}
			this._currentVersion = 1;
			this.RemainingCount = 0;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001DF3C File Offset: 0x0001C13C
		public void Pack()
		{
			if (this._list != null)
			{
				ushort num = (ushort)MathF.Max(32, this._list.Count);
				if (this._indexArray.Length != (int)num)
				{
					this.ReallocateIndexArray(num);
					return;
				}
			}
			else if (this._indexArray.Length != 32)
			{
				this.ReallocateIndexArray(32);
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001DF8C File Offset: 0x0001C18C
		public bool SelectRandom(out T selection, Predicate<T> conditions = null)
		{
			selection = default(T);
			if (this._list == null)
			{
				return false;
			}
			bool flag = false;
			while (this.RemainingCount > 0 && !flag)
			{
				ushort num = (ushort)MBRandom.RandomInt((int)this.RemainingCount);
				ushort num2 = this.RemainingCount - 1;
				MBFastRandomSelector<T>.IndexEntry indexEntry = this._indexArray[(int)num];
				T t = ((indexEntry.Version == this._currentVersion) ? this._list[(int)indexEntry.Index] : this._list[(int)num]);
				if (conditions == null || conditions(t))
				{
					flag = true;
					selection = t;
				}
				MBFastRandomSelector<T>.IndexEntry indexEntry2 = this._indexArray[(int)num2];
				this._indexArray[(int)num] = ((indexEntry2.Version == this._currentVersion) ? new MBFastRandomSelector<T>.IndexEntry(indexEntry2.Index, this._currentVersion) : new MBFastRandomSelector<T>.IndexEntry(num2, this._currentVersion));
				ushort remainingCount = this.RemainingCount;
				this.RemainingCount = remainingCount - 1;
			}
			return flag;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001E088 File Offset: 0x0001C288
		private void TryExpand()
		{
			if (this._indexArray.Length >= this._list.Count)
			{
				return;
			}
			ushort num = (ushort)(this._list.Count * 2);
			this.ReallocateIndexArray(num);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001E0C1 File Offset: 0x0001C2C1
		private void ReallocateIndexArray(ushort capacity)
		{
			capacity = (ushort)MBMath.ClampInt((int)capacity, 32, 65535);
			this._indexArray = new MBFastRandomSelector<T>.IndexEntry[(int)capacity];
			this._currentVersion = 1;
		}

		// Token: 0x0400050F RID: 1295
		public const ushort MinimumCapacity = 32;

		// Token: 0x04000510 RID: 1296
		public const ushort MaximumCapacity = 65535;

		// Token: 0x04000511 RID: 1297
		private const ushort InitialVersion = 1;

		// Token: 0x04000512 RID: 1298
		private const ushort MaximumVersion = 65535;

		// Token: 0x04000514 RID: 1300
		private MBReadOnlyList<T> _list;

		// Token: 0x04000515 RID: 1301
		private MBFastRandomSelector<T>.IndexEntry[] _indexArray;

		// Token: 0x04000516 RID: 1302
		private ushort _currentVersion;

		// Token: 0x0200011F RID: 287
		public struct IndexEntry
		{
			// Token: 0x06000C10 RID: 3088 RVA: 0x000269A7 File Offset: 0x00024BA7
			public IndexEntry(ushort index, ushort version)
			{
				this.Index = index;
				this.Version = version;
			}

			// Token: 0x040007B0 RID: 1968
			public ushort Index;

			// Token: 0x040007B1 RID: 1969
			public ushort Version;
		}
	}
}
