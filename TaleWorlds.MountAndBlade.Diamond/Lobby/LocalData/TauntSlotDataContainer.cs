using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData
{
	// Token: 0x02000176 RID: 374
	public class TauntSlotDataContainer : MultiplayerLocalDataContainer<TauntSlotData>
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x000111E6 File Offset: 0x0000F3E6
		protected override string GetSaveDirectoryName()
		{
			return "Data";
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000111ED File Offset: 0x0000F3ED
		protected override string GetSaveFileName()
		{
			return "TauntSlots.json";
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x000111F4 File Offset: 0x0000F3F4
		protected override PlatformFilePath GetCompatibilityFilePath()
		{
			return new PlatformFilePath(new PlatformDirectoryPath(PlatformFileType.User, "Data"), "Taunts.json");
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001120C File Offset: 0x0000F40C
		protected override List<TauntSlotData> DeserializeInCompatibilityMode(string serializedJson)
		{
			List<TauntSlotData> list = new List<TauntSlotData>();
			try
			{
				Dictionary<string, List<ValueTuple<string, int>>> dictionary = JsonConvert.DeserializeObject<Dictionary<string, List<ValueTuple<string, int>>>>(serializedJson);
				if (dictionary != null)
				{
					foreach (KeyValuePair<string, List<ValueTuple<string, int>>> keyValuePair in dictionary)
					{
						string key = keyValuePair.Key;
						List<TauntIndexData> list2 = new List<TauntIndexData>();
						if (keyValuePair.Value != null)
						{
							foreach (ValueTuple<string, int> valueTuple in keyValuePair.Value)
							{
								if (string.IsNullOrEmpty(valueTuple.Item1))
								{
									Debug.FailedAssert("Taunt id is null when trying to load in compatibility mode", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\LocalData\\TauntSlotDataContainer.cs", "DeserializeInCompatibilityMode", 120);
								}
								else
								{
									for (int i = 0; i < list2.Count; i++)
									{
										if (list2[i].TauntIndex == valueTuple.Item2)
										{
											Debug.FailedAssert("Taunt index used for multiple taunts", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\LocalData\\TauntSlotDataContainer.cs", "DeserializeInCompatibilityMode", 128);
										}
									}
									list2.Add(new TauntIndexData(valueTuple.Item1, valueTuple.Item2));
								}
							}
						}
						list.Add(new TauntSlotData(key)
						{
							TauntIndices = list2
						});
					}
				}
			}
			catch
			{
				Debug.FailedAssert("Failed to resolve taunt slot data in compatibility mode. Resetting local data.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\LocalData\\TauntSlotDataContainer.cs", "DeserializeInCompatibilityMode", 145);
			}
			return list;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000113C0 File Offset: 0x0000F5C0
		public MBReadOnlyList<TauntIndexData> GetTauntIndicesForPlayer(string playerId)
		{
			MBReadOnlyList<TauntSlotData> entries = base.GetEntries();
			for (int i = 0; i < entries.Count; i++)
			{
				if (entries[i].PlayerId == playerId)
				{
					return new MBReadOnlyList<TauntIndexData>(entries[i].TauntIndices);
				}
			}
			return null;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001140C File Offset: 0x0000F60C
		public void SetTauntIndicesForPlayer(string playerId, List<TauntIndexData> tauntIndices)
		{
			MBReadOnlyList<TauntSlotData> entries = base.GetEntries();
			TauntSlotData tauntSlotData = null;
			int num = -1;
			for (int i = 0; i < entries.Count; i++)
			{
				TauntSlotData tauntSlotData2 = entries[i];
				if (tauntSlotData2.PlayerId == playerId)
				{
					tauntSlotData = tauntSlotData2;
					num = i;
					break;
				}
			}
			TauntSlotData tauntSlotData3 = new TauntSlotData(playerId);
			tauntSlotData3.TauntIndices = tauntIndices.ToList<TauntIndexData>();
			if (tauntSlotData != null)
			{
				base.RemoveEntry(tauntSlotData);
				base.InsertEntry(tauntSlotData3, num);
				return;
			}
			base.AddEntry(tauntSlotData3);
		}
	}
}
