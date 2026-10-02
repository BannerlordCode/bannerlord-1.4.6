using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200010F RID: 271
	[Serializable]
	public struct CustomBattleId
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x000073DE File Offset: 0x000055DE
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x000073E6 File Offset: 0x000055E6
		[JsonProperty]
		public Guid Guid { get; private set; }

		// Token: 0x060005D5 RID: 1493 RVA: 0x000073EF File Offset: 0x000055EF
		public CustomBattleId(Guid guid)
		{
			this.Guid = guid;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000073F8 File Offset: 0x000055F8
		public static CustomBattleId NewGuid()
		{
			return new CustomBattleId(Guid.NewGuid());
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00007404 File Offset: 0x00005604
		public override string ToString()
		{
			return this.Guid.ToString();
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00007428 File Offset: 0x00005628
		public byte[] ToByteArray()
		{
			return this.Guid.ToByteArray();
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00007443 File Offset: 0x00005643
		public static bool operator ==(CustomBattleId a, CustomBattleId b)
		{
			return a.Guid == b.Guid;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00007458 File Offset: 0x00005658
		public static bool operator !=(CustomBattleId a, CustomBattleId b)
		{
			return a.Guid != b.Guid;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00007470 File Offset: 0x00005670
		public override bool Equals(object o)
		{
			if (o != null && o is CustomBattleId)
			{
				CustomBattleId customBattleId = (CustomBattleId)o;
				return this.Guid.Equals(customBattleId.Guid);
			}
			return false;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000074A8 File Offset: 0x000056A8
		public override int GetHashCode()
		{
			return this.Guid.GetHashCode();
		}

		// Token: 0x04000235 RID: 565
		[JsonIgnore]
		public static CustomBattleId Empty = new CustomBattleId(Guid.Empty);
	}
}
