using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x02000145 RID: 325
	public class DefaultRaidModel : RaidModel
	{
		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x0008170C File Offset: 0x0007F90C
		private MBReadOnlyList<ValueTuple<ItemObject, float>> CommonLootItemSpawnChances
		{
			get
			{
				if (this._commonLootItems == null)
				{
					List<ValueTuple<ItemObject, float>> list = new List<ValueTuple<ItemObject, float>>
					{
						new ValueTuple<ItemObject, float>(DefaultItems.Hides, 1f),
						new ValueTuple<ItemObject, float>(DefaultItems.HardWood, 1f),
						new ValueTuple<ItemObject, float>(DefaultItems.Tools, 1f),
						new ValueTuple<ItemObject, float>(DefaultItems.Grain, 1f),
						new ValueTuple<ItemObject, float>(Campaign.Current.ObjectManager.GetObject<ItemObject>("linen"), 1f),
						new ValueTuple<ItemObject, float>(Campaign.Current.ObjectManager.GetObject<ItemObject>("sheep"), 1f),
						new ValueTuple<ItemObject, float>(Campaign.Current.ObjectManager.GetObject<ItemObject>("mule"), 1f),
						new ValueTuple<ItemObject, float>(Campaign.Current.ObjectManager.GetObject<ItemObject>("pottery"), 1f)
					};
					for (int i = list.Count - 1; i >= 0; i--)
					{
						ItemObject item = list[i].Item1;
						float num = 100f / ((float)item.Value + 1f);
						list[i] = new ValueTuple<ItemObject, float>(item, num);
					}
					this._commonLootItems = new MBReadOnlyList<ValueTuple<ItemObject, float>>(list);
				}
				return this._commonLootItems;
			}
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00081864 File Offset: 0x0007FA64
		public override ExplainedNumber CalculateHitDamage(MapEventSide attackerSide, float settlementHitPoints)
		{
			float num = (MathF.Sqrt((float)attackerSide.TroopCount) + 5f) / 900f;
			ExplainedNumber explainedNumber = new ExplainedNumber(num * (float)CampaignTime.DeltaTime.ToHours, false, null);
			foreach (MapEventParty mapEventParty in attackerSide.Parties)
			{
				MobileParty mobileParty = mapEventParty.Party.MobileParty;
				if (((mobileParty != null) ? mobileParty.LeaderHero : null) != null && mapEventParty.Party.MobileParty.LeaderHero.GetPerkValue(DefaultPerks.Roguery.NoRestForTheWicked))
				{
					explainedNumber.AddFactor(DefaultPerks.Roguery.NoRestForTheWicked.SecondaryBonus, null);
				}
			}
			return explainedNumber;
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00081930 File Offset: 0x0007FB30
		public override ExplainedNumber GetRaidLootMultiplier(PartyBase receivingParty)
		{
			return new ExplainedNumber(1f, false, null);
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0008193E File Offset: 0x0007FB3E
		public override MBReadOnlyList<ValueTuple<ItemObject, float>> GetCommonLootItemScores()
		{
			return this.CommonLootItemSpawnChances;
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x00081946 File Offset: 0x0007FB46
		public override int GoldRewardForEachLostHearth
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x0400088C RID: 2188
		private MBReadOnlyList<ValueTuple<ItemObject, float>> _commonLootItems;
	}
}
