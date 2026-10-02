using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000EB RID: 235
	[Serializable]
	public class BadgeDataEntry
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00005208 File Offset: 0x00003408
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00005210 File Offset: 0x00003410
		[JsonProperty]
		public PlayerId PlayerId { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00005219 File Offset: 0x00003419
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x00005221 File Offset: 0x00003421
		[JsonProperty]
		public string BadgeId { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000522A File Offset: 0x0000342A
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x00005232 File Offset: 0x00003432
		[JsonProperty]
		public string ConditionId { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000523B File Offset: 0x0000343B
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x00005243 File Offset: 0x00003443
		[JsonProperty]
		public int Count { get; set; }

		// Token: 0x06000485 RID: 1157 RVA: 0x00005254 File Offset: 0x00003454
		public static Dictionary<ValueTuple<PlayerId, string, string>, int> ToDictionary(List<BadgeDataEntry> entries)
		{
			Dictionary<ValueTuple<PlayerId, string, string>, int> dictionary = new Dictionary<ValueTuple<PlayerId, string, string>, int>();
			if (entries != null)
			{
				foreach (BadgeDataEntry badgeDataEntry in entries)
				{
					dictionary.Add(new ValueTuple<PlayerId, string, string>(badgeDataEntry.PlayerId, badgeDataEntry.BadgeId, badgeDataEntry.ConditionId), badgeDataEntry.Count);
				}
			}
			return dictionary;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000052C8 File Offset: 0x000034C8
		public static List<BadgeDataEntry> ToList(Dictionary<ValueTuple<PlayerId, string, string>, int> dictionary)
		{
			List<BadgeDataEntry> list = new List<BadgeDataEntry>();
			if (dictionary != null)
			{
				foreach (KeyValuePair<ValueTuple<PlayerId, string, string>, int> keyValuePair in dictionary)
				{
					list.Add(new BadgeDataEntry
					{
						PlayerId = keyValuePair.Key.Item1,
						BadgeId = keyValuePair.Key.Item2,
						ConditionId = keyValuePair.Key.Item3,
						Count = keyValuePair.Value
					});
				}
			}
			return list;
		}
	}
}
