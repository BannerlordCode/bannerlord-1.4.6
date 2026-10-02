using System;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004B2 RID: 1202
	public static class GainKingdomInfluenceAction
	{
		// Token: 0x06004A93 RID: 19091 RVA: 0x0017937C File Offset: 0x0017757C
		private static void ApplyInternal(Hero hero, MobileParty party, float gainedInfluence, GainKingdomInfluenceAction.InfluenceGainingReason detail)
		{
			Clan clan = null;
			if (hero != null)
			{
				if (hero.CompanionOf != null)
				{
					clan = hero.CompanionOf;
				}
				else if (hero.Clan != null)
				{
					clan = hero.Clan;
				}
			}
			else if (party.ActualClan != null)
			{
				clan = party.ActualClan;
			}
			else if (party.Owner != null)
			{
				clan = party.Owner.Clan;
			}
			if (clan == null || clan.Kingdom == null)
			{
				return;
			}
			MobileParty mobileParty = party ?? hero.PartyBelongedTo;
			if (detail != GainKingdomInfluenceAction.InfluenceGainingReason.BeingAtArmy && detail == GainKingdomInfluenceAction.InfluenceGainingReason.ClanSupport)
			{
				gainedInfluence = 0.5f;
			}
			if (detail != GainKingdomInfluenceAction.InfluenceGainingReason.Default && detail != GainKingdomInfluenceAction.InfluenceGainingReason.GivingFood && detail != GainKingdomInfluenceAction.InfluenceGainingReason.JoinFaction && detail != GainKingdomInfluenceAction.InfluenceGainingReason.ClanSupport && ((Kingdom)clan.MapFaction).ActivePolicies.Contains(DefaultPolicies.MilitaryCoronae))
			{
				gainedInfluence *= 1.2f;
			}
			ExplainedNumber explainedNumber = new ExplainedNumber(gainedInfluence, false, null);
			if (detail == GainKingdomInfluenceAction.InfluenceGainingReason.Battle && gainedInfluence > 0f)
			{
				PerkHelper.AddPerkBonusForParty(DefaultPerks.Tactics.PreBattleManeuvers, mobileParty, true, ref explainedNumber, false);
			}
			if (detail == GainKingdomInfluenceAction.InfluenceGainingReason.CaptureSettlement && (hero != null || mobileParty.LeaderHero != null))
			{
				Hero hero2 = hero ?? mobileParty.LeaderHero;
				PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Tactics.Besieged, hero2.CharacterObject, false, ref explainedNumber, false);
			}
			gainedInfluence = explainedNumber.ResultNumber;
			ChangeClanInfluenceAction.Apply(clan, gainedInfluence);
			int num = (int)gainedInfluence;
			if (MathF.Abs(num) > 0)
			{
				if ((detail == GainKingdomInfluenceAction.InfluenceGainingReason.DonatePrisoners && party == MobileParty.MainParty) || (detail == GainKingdomInfluenceAction.InfluenceGainingReason.Battle && hero == Hero.MainHero))
				{
					TextObject textObject = GameTexts.FindText("str_influence_gain_message", null);
					textObject.SetTextVariable("INFLUENCE", num);
					textObject.SetTextVariable("NEW_INFLUENCE", (int)clan.Influence);
					InformationManager.DisplayMessage(new InformationMessage(textObject.ToString()));
				}
				if (detail == GainKingdomInfluenceAction.InfluenceGainingReason.SiegeSafePassage && hero == Hero.MainHero)
				{
					TextObject textObject2 = GameTexts.FindText("str_leave_siege_lose_influence_message", null);
					textObject2.SetTextVariable("INFLUENCE", -num);
					InformationManager.DisplayMessage(new InformationMessage(textObject2.ToString()));
				}
			}
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x0017952F File Offset: 0x0017772F
		public static void ApplyForBattle(Hero hero, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(hero, null, value, GainKingdomInfluenceAction.InfluenceGainingReason.Battle);
		}

		// Token: 0x06004A95 RID: 19093 RVA: 0x0017953A File Offset: 0x0017773A
		public static void ApplyForGivingFood(Hero hero1, Hero hero2, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(hero1, null, value, GainKingdomInfluenceAction.InfluenceGainingReason.GivingFood);
			GainKingdomInfluenceAction.ApplyInternal(hero2, null, -value, GainKingdomInfluenceAction.InfluenceGainingReason.GivingFood);
		}

		// Token: 0x06004A96 RID: 19094 RVA: 0x0017954F File Offset: 0x0017774F
		public static void ApplyForDefault(Hero hero, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(hero, null, value, GainKingdomInfluenceAction.InfluenceGainingReason.Default);
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x0017955A File Offset: 0x0017775A
		public static void ApplyForJoiningFaction(Hero hero, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(hero, null, value, GainKingdomInfluenceAction.InfluenceGainingReason.JoinFaction);
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x00179565 File Offset: 0x00177765
		public static void ApplyForDonatePrisoners(MobileParty donatingParty, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(null, donatingParty, value, GainKingdomInfluenceAction.InfluenceGainingReason.DonatePrisoners);
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x00179571 File Offset: 0x00177771
		public static void ApplyForRaidingEnemyVillage(MobileParty side1Party, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(null, side1Party, value, GainKingdomInfluenceAction.InfluenceGainingReason.Raiding);
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x0017957C File Offset: 0x0017777C
		public static void ApplyForBesiegingEnemySettlement(MobileParty side1Party, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(null, side1Party, value, GainKingdomInfluenceAction.InfluenceGainingReason.Besieging);
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x00179587 File Offset: 0x00177787
		public static void ApplyForSiegeSafePassageBarter(MobileParty side1Party, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(null, side1Party, value, GainKingdomInfluenceAction.InfluenceGainingReason.SiegeSafePassage);
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x00179593 File Offset: 0x00177793
		public static void ApplyForCapturingEnemySettlement(MobileParty side1Party, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(null, side1Party, value, GainKingdomInfluenceAction.InfluenceGainingReason.CaptureSettlement);
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x0017959E File Offset: 0x0017779E
		public static void ApplyForLeavingTroopToGarrison(Hero hero, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(hero, null, value, GainKingdomInfluenceAction.InfluenceGainingReason.LeaveGarrison);
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x001795A9 File Offset: 0x001777A9
		public static void ApplyForBoardGameWon(Hero hero, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(hero, null, value, GainKingdomInfluenceAction.InfluenceGainingReason.BoardGameWon);
		}

		// Token: 0x02000898 RID: 2200
		private enum InfluenceGainingReason
		{
			// Token: 0x040024B9 RID: 9401
			Default,
			// Token: 0x040024BA RID: 9402
			BeingAtArmy,
			// Token: 0x040024BB RID: 9403
			Battle,
			// Token: 0x040024BC RID: 9404
			Raiding,
			// Token: 0x040024BD RID: 9405
			Besieging,
			// Token: 0x040024BE RID: 9406
			CaptureSettlement,
			// Token: 0x040024BF RID: 9407
			JoinFaction,
			// Token: 0x040024C0 RID: 9408
			GivingFood,
			// Token: 0x040024C1 RID: 9409
			LeaveGarrison,
			// Token: 0x040024C2 RID: 9410
			BoardGameWon,
			// Token: 0x040024C3 RID: 9411
			ClanSupport,
			// Token: 0x040024C4 RID: 9412
			DonatePrisoners,
			// Token: 0x040024C5 RID: 9413
			SiegeSafePassage
		}
	}
}
