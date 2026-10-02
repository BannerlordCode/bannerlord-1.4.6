using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby
{
	// Token: 0x0200016D RID: 365
	public abstract class MultiplayerLocalDataContainer<T> where T : MultiplayerLocalData
	{
		// Token: 0x06000A1E RID: 2590 RVA: 0x00010263 File Offset: 0x0000E463
		public MultiplayerLocalDataContainer()
		{
			this._operationQueue = new List<MultiplayerLocalDataContainer<T>.ContainerOperation>();
			this._dataList = new List<T>();
			this._saveDirectoryName = this.GetSaveDirectoryName();
			this._saveFileName = this.GetSaveFileName();
			this._isCacheDirty = true;
		}

		// Token: 0x06000A1F RID: 2591
		protected abstract string GetSaveDirectoryName();

		// Token: 0x06000A20 RID: 2592
		protected abstract string GetSaveFileName();

		// Token: 0x06000A21 RID: 2593 RVA: 0x000102A0 File Offset: 0x0000E4A0
		public void AddEntry(T item)
		{
			List<MultiplayerLocalDataContainer<T>.ContainerOperation> operationQueue = this._operationQueue;
			lock (operationQueue)
			{
				MultiplayerLocalDataContainer<T>.ContainerOperation containerOperation = MultiplayerLocalDataContainer<T>.ContainerOperation.CreateAsAdd(item);
				this._operationQueue.Add(containerOperation);
			}
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x000102F0 File Offset: 0x0000E4F0
		public void InsertEntry(T item, int index)
		{
			List<MultiplayerLocalDataContainer<T>.ContainerOperation> operationQueue = this._operationQueue;
			lock (operationQueue)
			{
				MultiplayerLocalDataContainer<T>.ContainerOperation containerOperation = MultiplayerLocalDataContainer<T>.ContainerOperation.CreateAsInsert(item, index);
				this._operationQueue.Add(containerOperation);
			}
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00010340 File Offset: 0x0000E540
		public void RemoveEntry(T item)
		{
			List<MultiplayerLocalDataContainer<T>.ContainerOperation> operationQueue = this._operationQueue;
			lock (operationQueue)
			{
				MultiplayerLocalDataContainer<T>.ContainerOperation containerOperation = MultiplayerLocalDataContainer<T>.ContainerOperation.CreateAsRemove(item);
				this._operationQueue.Add(containerOperation);
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00010390 File Offset: 0x0000E590
		public MBReadOnlyList<T> GetEntries()
		{
			return new MBReadOnlyList<T>(this._dataList);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x000103A0 File Offset: 0x0000E5A0
		internal async Task Tick(float dt)
		{
			if (this._isCacheDirty)
			{
				await this.LoadFileAux();
				this._isCacheDirty = false;
			}
			while (this._operationQueue.Count > 0)
			{
				this.HandleOperation(this._operationQueue[0]);
				this._operationQueue.RemoveAt(0);
			}
			if (this._isFileDirty)
			{
				await this.SaveFileAux();
				this._isFileDirty = false;
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x000103E8 File Offset: 0x0000E5E8
		private void HandleOperation(MultiplayerLocalDataContainer<T>.ContainerOperation operation)
		{
			switch (operation.OperationType)
			{
			case MultiplayerLocalDataContainer<T>.OperationType.Add:
				this.AddEntryAux(operation.Item);
				return;
			case MultiplayerLocalDataContainer<T>.OperationType.Insert:
				this.InsertEntryAux(operation.Item, operation.Index);
				return;
			case MultiplayerLocalDataContainer<T>.OperationType.Remove:
				this.RemoveEntryAux(operation.Item);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001043C File Offset: 0x0000E63C
		private void AddEntryAux(T item)
		{
			bool flag;
			this.OnBeforeAddEntry(item, out flag);
			if (!flag)
			{
				return;
			}
			bool flag2 = false;
			using (List<T>.Enumerator enumerator = this._dataList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasSameContentWith(item))
					{
						flag2 = true;
						break;
					}
				}
			}
			if (!flag2)
			{
				this._dataList.Add(item);
			}
			else
			{
				Debug.FailedAssert("Item is already in container: " + item, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\MultiplayerLocalDataManager.cs", "AddEntryAux", 234);
			}
			this._isFileDirty = true;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x000104E8 File Offset: 0x0000E6E8
		private void InsertEntryAux(T item, int index)
		{
			bool flag;
			this.OnBeforeAddEntry(item, out flag);
			if (!flag)
			{
				return;
			}
			bool flag2 = false;
			using (List<T>.Enumerator enumerator = this._dataList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasSameContentWith(item))
					{
						flag2 = true;
						break;
					}
				}
			}
			if (!flag2)
			{
				if (index >= 0 && index < this._dataList.Count)
				{
					this._dataList.Insert(index, item);
				}
				else
				{
					this._dataList.Add(item);
				}
			}
			else
			{
				Debug.FailedAssert("Item is already in container: " + item, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\MultiplayerLocalDataManager.cs", "InsertEntryAux", 272);
			}
			this._isFileDirty = true;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000105B8 File Offset: 0x0000E7B8
		protected virtual void OnBeforeAddEntry(T item, out bool canAddEntry)
		{
			canAddEntry = true;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x000105C0 File Offset: 0x0000E7C0
		private void RemoveEntryAux(T item)
		{
			bool flag;
			this.OnBeforeRemoveEntry(item, out flag);
			if (!flag)
			{
				return;
			}
			int count = this._dataList.Count;
			for (int i = this._dataList.Count - 1; i >= 0; i--)
			{
				if (this._dataList[i].HasSameContentWith(item))
				{
					this._dataList.Remove(item);
				}
			}
			if (count == this._dataList.Count)
			{
				Debug.FailedAssert("Item is not in container: " + item, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\MultiplayerLocalDataManager.cs", "RemoveEntryAux", 304);
			}
			this._isFileDirty = true;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00010662 File Offset: 0x0000E862
		protected virtual void OnBeforeRemoveEntry(T item, out bool canRemoveEntry)
		{
			canRemoveEntry = true;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00010667 File Offset: 0x0000E867
		private PlatformFilePath GetDataFilePath()
		{
			return new PlatformFilePath(new PlatformDirectoryPath(PlatformFileType.User, this._saveDirectoryName), this._saveFileName);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00010680 File Offset: 0x0000E880
		private async Task SaveFileAux()
		{
			try
			{
				await FileHelper.SaveFileAsync(this.GetDataFilePath(), Common.SerializeObjectAsJson(this._dataList));
			}
			catch (Exception ex)
			{
				Debug.FailedAssert("An exception occured while trying to save " + base.GetType().Name + " data: " + ex.Message, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\MultiplayerLocalDataManager.cs", "SaveFileAux", 331);
			}
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x000106C8 File Offset: 0x0000E8C8
		private async Task LoadFileAux()
		{
			PlatformFilePath oldFilePath = this.GetCompatibilityFilePath();
			if (FileHelper.FileExists(oldFilePath))
			{
				string text = await FileHelper.GetFileContentStringAsync(oldFilePath);
				FileHelper.DeleteFile(oldFilePath);
				if (!string.IsNullOrEmpty(text))
				{
					this._dataList.Clear();
					List<T> list = null;
					try
					{
						list = JsonConvert.DeserializeObject<List<T>>(text);
					}
					catch
					{
						try
						{
							list = this.DeserializeInCompatibilityMode(text);
						}
						catch
						{
							Debug.FailedAssert("Failed to load old data in compatibility mode", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\MultiplayerLocalDataManager.cs", "LoadFileAux", 362);
						}
					}
					if (list != null)
					{
						foreach (T t in list)
						{
							this._dataList.Add(t);
						}
					}
					this._isFileDirty = true;
					return;
				}
			}
			PlatformFilePath dataFilePath = this.GetDataFilePath();
			if (FileHelper.FileExists(dataFilePath))
			{
				string text2 = await FileHelper.GetFileContentStringAsync(dataFilePath);
				if (!string.IsNullOrEmpty(text2))
				{
					this._dataList.Clear();
					List<T> list2 = null;
					try
					{
						list2 = JsonConvert.DeserializeObject<List<T>>(text2);
					}
					catch
					{
						try
						{
							list2 = this.DeserializeInCompatibilityMode(text2);
							this._isFileDirty = true;
						}
						catch
						{
							Debug.FailedAssert("Failed to load file in compatibility mode", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\MultiplayerLocalDataManager.cs", "LoadFileAux", 403);
						}
					}
					if (list2 != null)
					{
						foreach (T t2 in list2)
						{
							this._dataList.Add(t2);
						}
					}
				}
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001070D File Offset: 0x0000E90D
		protected virtual PlatformFilePath GetCompatibilityFilePath()
		{
			return new PlatformFilePath(new PlatformDirectoryPath(PlatformFileType.User, "DataOld"), "TmpData");
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00010724 File Offset: 0x0000E924
		protected virtual List<T> DeserializeInCompatibilityMode(string serializedJson)
		{
			return null;
		}

		// Token: 0x040004F6 RID: 1270
		private readonly string _saveDirectoryName;

		// Token: 0x040004F7 RID: 1271
		private readonly string _saveFileName;

		// Token: 0x040004F8 RID: 1272
		private readonly List<MultiplayerLocalDataContainer<T>.ContainerOperation> _operationQueue;

		// Token: 0x040004F9 RID: 1273
		private readonly List<T> _dataList;

		// Token: 0x040004FA RID: 1274
		private bool _isFileDirty;

		// Token: 0x040004FB RID: 1275
		private bool _isCacheDirty;

		// Token: 0x020001D1 RID: 465
		private enum OperationType
		{
			// Token: 0x040006C0 RID: 1728
			Add,
			// Token: 0x040006C1 RID: 1729
			Insert,
			// Token: 0x040006C2 RID: 1730
			Remove
		}

		// Token: 0x020001D2 RID: 466
		private struct ContainerOperation
		{
			// Token: 0x06000B53 RID: 2899 RVA: 0x000167DA File Offset: 0x000149DA
			private ContainerOperation(MultiplayerLocalDataContainer<T>.OperationType type, T item, int index)
			{
				this.OperationType = type;
				this.Item = item;
				this.Index = index;
			}

			// Token: 0x06000B54 RID: 2900 RVA: 0x000167F1 File Offset: 0x000149F1
			public static MultiplayerLocalDataContainer<T>.ContainerOperation CreateAsAdd(T item)
			{
				return new MultiplayerLocalDataContainer<T>.ContainerOperation(MultiplayerLocalDataContainer<T>.OperationType.Add, item, -1);
			}

			// Token: 0x06000B55 RID: 2901 RVA: 0x000167FB File Offset: 0x000149FB
			public static MultiplayerLocalDataContainer<T>.ContainerOperation CreateAsRemove(T item)
			{
				return new MultiplayerLocalDataContainer<T>.ContainerOperation(MultiplayerLocalDataContainer<T>.OperationType.Remove, item, -1);
			}

			// Token: 0x06000B56 RID: 2902 RVA: 0x00016805 File Offset: 0x00014A05
			public static MultiplayerLocalDataContainer<T>.ContainerOperation CreateAsInsert(T item, int index)
			{
				return new MultiplayerLocalDataContainer<T>.ContainerOperation(MultiplayerLocalDataContainer<T>.OperationType.Insert, item, index);
			}

			// Token: 0x040006C3 RID: 1731
			public readonly MultiplayerLocalDataContainer<T>.OperationType OperationType;

			// Token: 0x040006C4 RID: 1732
			public readonly T Item;

			// Token: 0x040006C5 RID: 1733
			public readonly int Index;
		}

		// Token: 0x020001D3 RID: 467
		private class ContainerOperationComparer : IComparer<MultiplayerLocalDataContainer<T>.ContainerOperation>
		{
			// Token: 0x06000B57 RID: 2903 RVA: 0x0001680F File Offset: 0x00014A0F
			public int Compare(MultiplayerLocalDataContainer<T>.ContainerOperation x, MultiplayerLocalDataContainer<T>.ContainerOperation y)
			{
				return x.OperationType.CompareTo(y.OperationType);
			}
		}
	}
}
