using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData
{
	// Token: 0x02000173 RID: 371
	public class MatchHistoryDataContainer : MultiplayerLocalDataContainer<MatchHistoryData>
	{
		// Token: 0x06000A6E RID: 2670 RVA: 0x00010D07 File Offset: 0x0000EF07
		public MatchHistoryDataContainer()
		{
			this._matchesToRemove = new List<MatchHistoryData>();
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00010D1A File Offset: 0x0000EF1A
		protected override string GetSaveDirectoryName()
		{
			return "Data";
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00010D21 File Offset: 0x0000EF21
		protected override string GetSaveFileName()
		{
			return "History.json";
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00010D28 File Offset: 0x0000EF28
		protected override void OnBeforeRemoveEntry(MatchHistoryData item, out bool canRemoveEntry)
		{
			base.OnBeforeRemoveEntry(item, out canRemoveEntry);
			this._matchesToRemove.Remove(item);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00010D40 File Offset: 0x0000EF40
		protected override void OnBeforeAddEntry(MatchHistoryData item, out bool canAddEntry)
		{
			bool flag;
			base.OnBeforeAddEntry(item, out flag);
			MBReadOnlyList<MatchHistoryData> entries = base.GetEntries();
			bool flag2 = false;
			for (int i = 0; i < entries.Count; i++)
			{
				if (entries[i].MatchId == item.MatchId)
				{
					MatchHistoryDataContainer.PrintDebugLog("Found existing match with id trying to replace: " + entries[i].MatchId);
					base.RemoveEntry(entries[i]);
					this._matchesToRemove.Add(entries[i]);
					base.InsertEntry(item, i);
					MatchHistoryDataContainer.PrintDebugLog("Replaced existing match: (" + entries[i].MatchId + ") with: " + item.MatchId);
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				int num = this.GetEntryCountOfMatchType(item.MatchType) + 1 - 10;
				if (num > 0)
				{
					MatchHistoryDataContainer.PrintDebugLog(string.Format("Max match count is reached, removing ({0}) matches with type: {1}", num, item.MatchType));
					List<MatchHistoryData> oldestMatches = this.GetOldestMatches(item.MatchType, num);
					for (int j = 0; j < oldestMatches.Count; j++)
					{
						if (!this._matchesToRemove.Contains(oldestMatches[j]))
						{
							base.RemoveEntry(oldestMatches[j]);
							this._matchesToRemove.Add(oldestMatches[j]);
						}
					}
				}
			}
			canAddEntry = !flag2;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00010E9C File Offset: 0x0000F09C
		protected override List<MatchHistoryData> DeserializeInCompatibilityMode(string serializedJson)
		{
			List<MatchHistoryData> list = new List<MatchHistoryData>();
			try
			{
				MBList<MatchHistoryData> mblist = JsonConvert.DeserializeObject<MBList<MatchHistoryData>>(serializedJson);
				for (int i = 0; i < mblist.Count; i++)
				{
					list.Add(mblist[i]);
				}
			}
			catch
			{
				Debug.FailedAssert("Failed to resolve match history in compatibility mode. Resetting the file.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\LocalData\\MatchHistoryDataContainer.cs", "DeserializeInCompatibilityMode", 228);
			}
			return list;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00010F04 File Offset: 0x0000F104
		public bool TryGetHistoryData(string matchId, out MatchHistoryData historyData)
		{
			historyData = null;
			MBReadOnlyList<MatchHistoryData> entries = base.GetEntries();
			for (int i = 0; i < entries.Count; i++)
			{
				if (entries[i].MatchId == matchId)
				{
					historyData = entries[i];
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00010F4C File Offset: 0x0000F14C
		private List<MatchHistoryData> GetOldestMatches(string matchType, int count = 1)
		{
			DateTime maxValue = DateTime.MaxValue;
			List<MatchHistoryData> list = new List<MatchHistoryData>();
			MBReadOnlyList<MatchHistoryData> entries = base.GetEntries();
			entries.OrderBy<MatchHistoryData, DateTime>((MatchHistoryData e) => e.MatchDate);
			int num = 0;
			foreach (MatchHistoryData matchHistoryData in entries)
			{
				if (matchHistoryData == null)
				{
					Debug.FailedAssert("Trying to remove null match history data", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\LocalData\\MatchHistoryDataContainer.cs", "GetOldestMatches", 267);
				}
				else
				{
					if (matchHistoryData.MatchType == matchType)
					{
						list.Add(matchHistoryData);
						num++;
					}
					if (num == count)
					{
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0001100C File Offset: 0x0000F20C
		private int GetEntryCountOfMatchType(string matchType)
		{
			int num = 0;
			using (List<MatchHistoryData>.Enumerator enumerator = base.GetEntries().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.MatchType == matchType)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0001106C File Offset: 0x0000F26C
		private static void PrintDebugLog(string text)
		{
			Debug.Print("[MATCH_HISTORY]: " + text, 0, Debug.DebugColor.Yellow, 17592186044416UL);
		}

		// Token: 0x04000512 RID: 1298
		private const int MaxMatchCountPerMatchType = 10;

		// Token: 0x04000513 RID: 1299
		private List<MatchHistoryData> _matchesToRemove;
	}
}
