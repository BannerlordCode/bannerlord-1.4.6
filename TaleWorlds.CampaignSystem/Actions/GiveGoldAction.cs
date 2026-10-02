using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004B5 RID: 1205
	public static class GiveGoldAction
	{
		// Token: 0x06004AA3 RID: 19107 RVA: 0x00179624 File Offset: 0x00177824
		private static void ApplyInternal(Hero giverHero, PartyBase giverParty, Hero recipientHero, PartyBase recipientParty, int goldAmount, bool showQuickInformation, string transactionStringId = "")
		{
			if (giverHero != null)
			{
				goldAmount = MathF.Min(giverHero.Gold, goldAmount);
				giverHero.ChangeHeroGold(-goldAmount);
			}
			else if (giverParty != null && giverParty.IsMobile)
			{
				goldAmount = MathF.Min(giverParty.MobileParty.PartyTradeGold, goldAmount);
				giverParty.MobileParty.PartyTradeGold -= goldAmount;
			}
			else if (giverParty != null && giverParty.IsSettlement)
			{
				SettlementComponent settlementComponent = giverParty.Settlement.SettlementComponent;
				goldAmount = MathF.Min(settlementComponent.Gold, goldAmount);
				settlementComponent.ChangeGold(-goldAmount);
			}
			if (recipientHero != null)
			{
				recipientHero.ChangeHeroGold(goldAmount);
			}
			else if (recipientParty != null && recipientParty.IsMobile)
			{
				recipientParty.MobileParty.PartyTradeGold += goldAmount;
			}
			else if (recipientParty != null && recipientParty.IsSettlement)
			{
				recipientParty.Settlement.SettlementComponent.ChangeGold(goldAmount);
			}
			CampaignEventDispatcher.Instance.OnHeroOrPartyTradedGold(new ValueTuple<Hero, PartyBase>(giverHero, giverParty), new ValueTuple<Hero, PartyBase>(recipientHero, recipientParty), new ValueTuple<int, string>(goldAmount, transactionStringId), showQuickInformation);
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x0017971E File Offset: 0x0017791E
		public static void ApplyBetweenCharacters(Hero giverHero, Hero recipientHero, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(giverHero, null, recipientHero, null, amount, !disableNotification && (giverHero == Hero.MainHero || recipientHero == Hero.MainHero), "");
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x00179748 File Offset: 0x00177948
		public static void ApplyForCharacterToSettlement(Hero giverHero, Settlement settlement, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(giverHero, null, null, settlement.Party, amount, !disableNotification && giverHero == Hero.MainHero, "");
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x0017976C File Offset: 0x0017796C
		public static void ApplyForSettlementToCharacter(Settlement giverSettlement, Hero recipientHero, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(recipientHero, null, null, giverSettlement.Party, -amount, !disableNotification && recipientHero == Hero.MainHero, "");
		}

		// Token: 0x06004AA7 RID: 19111 RVA: 0x00179791 File Offset: 0x00177991
		public static void ApplyForSettlementToParty(Settlement giverSettlement, PartyBase recipientParty, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(null, giverSettlement.Party, null, recipientParty, amount, !disableNotification && recipientParty.LeaderHero == Hero.MainHero, "");
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x001797BA File Offset: 0x001779BA
		public static void ApplyForPartyToSettlement(PartyBase giverParty, Settlement settlement, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(null, giverParty, null, settlement.Party, amount, !disableNotification && ((giverParty != null) ? giverParty.LeaderHero : null) == Hero.MainHero, "");
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x001797E9 File Offset: 0x001779E9
		public static void ApplyForPartyToCharacter(PartyBase giverParty, Hero recipientHero, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(null, giverParty, recipientHero, null, amount, !disableNotification && giverParty != null && (giverParty.LeaderHero == Hero.MainHero || recipientHero == Hero.MainHero), "");
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x0017981B File Offset: 0x00177A1B
		public static void ApplyForCharacterToParty(Hero giverHero, PartyBase receipentParty, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(giverHero, null, null, receipentParty, amount, !disableNotification && (giverHero == Hero.MainHero || receipentParty.LeaderHero == Hero.MainHero), "");
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x0017984A File Offset: 0x00177A4A
		public static void ApplyForPartyToParty(PartyBase giverParty, PartyBase receipentParty, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(null, giverParty, null, receipentParty, amount, !disableNotification && (giverParty.LeaderHero == Hero.MainHero || receipentParty.LeaderHero == Hero.MainHero), "");
		}
	}
}
