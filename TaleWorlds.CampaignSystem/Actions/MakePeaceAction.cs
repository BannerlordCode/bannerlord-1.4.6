using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004BD RID: 1213
	public static class MakePeaceAction
	{
		// Token: 0x06004AC7 RID: 19143 RVA: 0x0017A720 File Offset: 0x00178920
		private static void ApplyInternal(IFaction faction1, IFaction faction2, int dailyTributeFrom1To2, int dailyTributeDuration, MakePeaceAction.MakePeaceDetail detail = MakePeaceAction.MakePeaceDetail.Default)
		{
			StanceLink stanceWith = faction1.GetStanceWith(faction2);
			FactionManager.SetNeutral(faction1, faction2);
			stanceWith.SetDailyTributePaid(faction1, dailyTributeFrom1To2, dailyTributeDuration);
			if (faction1 == Hero.MainHero.MapFaction || faction2 == Hero.MainHero.MapFaction)
			{
				IFaction dirtySide = ((faction1 == Hero.MainHero.MapFaction) ? faction2 : faction1);
				IEnumerable<Settlement> all = Settlement.All;
				Func<Settlement, bool> func;
				Func<Settlement, bool> <>9__0;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (Settlement party) => party.IsVisible && party.MapFaction == dirtySide);
				}
				foreach (Settlement settlement in all.Where<Settlement>(func))
				{
					settlement.Party.SetVisualAsDirty();
				}
				IEnumerable<MobileParty> all2 = MobileParty.All;
				Func<MobileParty, bool> func2;
				Func<MobileParty, bool> <>9__1;
				if ((func2 = <>9__1) == null)
				{
					func2 = (<>9__1 = (MobileParty party) => party.IsVisible && party.MapFaction == dirtySide);
				}
				foreach (MobileParty mobileParty in all2.Where<MobileParty>(func2))
				{
					mobileParty.Party.SetVisualAsDirty();
				}
			}
			CampaignEventDispatcher.Instance.OnMakePeace(faction1, faction2, detail);
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x0017A860 File Offset: 0x00178A60
		public static void Apply(IFaction faction1, IFaction faction2)
		{
			MakePeaceAction.ApplyInternal(faction1, faction2, 0, 0, MakePeaceAction.MakePeaceDetail.Default);
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x0017A86C File Offset: 0x00178A6C
		public static void ApplyByKingdomDecision(IFaction faction1, IFaction faction2, int dailyTributeFrom1To2, int dailyTributeDuration)
		{
			MakePeaceAction.ApplyInternal(faction1, faction2, dailyTributeFrom1To2, dailyTributeDuration, MakePeaceAction.MakePeaceDetail.ByKingdomDecision);
		}

		// Token: 0x04001482 RID: 5250
		private const float DefaultValueForBeingLimitedAfterPeace = 100000f;

		// Token: 0x0200089E RID: 2206
		public enum MakePeaceDetail
		{
			// Token: 0x040024D7 RID: 9431
			Default,
			// Token: 0x040024D8 RID: 9432
			ByKingdomDecision
		}
	}
}
