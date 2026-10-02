using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.Extensions
{
	// Token: 0x0200016A RID: 362
	public static class Items
	{
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0008D470 File Offset: 0x0008B670
		public static MBReadOnlyList<ItemObject> All
		{
			get
			{
				return Campaign.Current.AllItems;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x0008D47C File Offset: 0x0008B67C
		public static IEnumerable<ItemObject> AllTradeGoods
		{
			get
			{
				MBReadOnlyList<ItemObject> all = Items.All;
				foreach (ItemObject itemObject in all)
				{
					if (itemObject.IsTradeGood)
					{
						yield return itemObject;
					}
				}
				List<ItemObject>.Enumerator enumerator = default(List<ItemObject>.Enumerator);
				yield break;
				yield break;
			}
		}
	}
}
