using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004CC RID: 1228
	public static class TeleportHeroAction
	{
		// Token: 0x06004AFF RID: 19199 RVA: 0x0017C134 File Offset: 0x0017A334
		private static void ApplyInternal(Hero hero, Settlement targetSettlement, MobileParty targetParty, TeleportHeroAction.TeleportationDetail detail)
		{
			CampaignEventDispatcher.Instance.OnHeroTeleportationRequested(hero, targetSettlement, targetParty, detail);
			switch (detail)
			{
			case TeleportHeroAction.TeleportationDetail.ImmediateTeleportToSettlement:
				if (targetSettlement != null)
				{
					if (!hero.IsActive)
					{
						hero.ChangeState(Hero.CharacterStates.Active);
					}
					if (hero.CurrentSettlement != null)
					{
						LeaveSettlementAction.ApplyForCharacterOnly(hero);
					}
					if (hero.PartyBelongedTo != null)
					{
						if (!hero.PartyBelongedTo.IsActive || hero.PartyBelongedTo.IsCurrentlyEngagingParty || hero.PartyBelongedTo.MapEvent != null)
						{
							return;
						}
						hero.PartyBelongedTo.MemberRoster.RemoveTroop(hero.CharacterObject, 1, default(UniqueTroopDescriptor), 0);
					}
					EnterSettlementAction.ApplyForCharacterOnly(hero, targetSettlement);
					return;
				}
				break;
			case TeleportHeroAction.TeleportationDetail.ImmediateTeleportToParty:
				if (hero.IsTraveling)
				{
					hero.ChangeState(Hero.CharacterStates.Active);
				}
				AddHeroToPartyAction.Apply(hero, targetParty, true);
				return;
			case TeleportHeroAction.TeleportationDetail.ImmediateTeleportToPartyAsPartyLeader:
				if (hero.IsTraveling)
				{
					hero.ChangeState(Hero.CharacterStates.Active);
				}
				AddHeroToPartyAction.Apply(hero, targetParty, true);
				targetParty.ChangePartyLeader(hero);
				targetParty.PartyComponent.ClearCachedName();
				targetParty.Party.SetCustomName(null);
				targetParty.Party.SetVisualAsDirty();
				if (targetParty.IsDisbanding)
				{
					DisbandPartyAction.CancelDisband(targetParty);
				}
				if (targetParty.Ai.DoNotMakeNewDecisions)
				{
					targetParty.Ai.SetDoNotMakeNewDecisions(false);
					return;
				}
				break;
			case TeleportHeroAction.TeleportationDetail.DelayedTeleportToSettlement:
			case TeleportHeroAction.TeleportationDetail.DelayedTeleportToParty:
			case TeleportHeroAction.TeleportationDetail.DelayedTeleportToSettlementAsGovernor:
			case TeleportHeroAction.TeleportationDetail.DelayedTeleportToPartyAsPartyLeader:
				if (detail == TeleportHeroAction.TeleportationDetail.DelayedTeleportToSettlement && hero.CurrentSettlement == targetSettlement)
				{
					TeleportHeroAction.ApplyImmediateTeleportToSettlement(hero, targetSettlement);
					return;
				}
				if (hero.GovernorOf != null)
				{
					ChangeGovernorAction.RemoveGovernorOf(hero);
				}
				if (hero.CurrentSettlement != null && hero.CurrentSettlement != targetSettlement)
				{
					LeaveSettlementAction.ApplyForCharacterOnly(hero);
				}
				if (hero.PartyBelongedTo != null)
				{
					if (!hero.PartyBelongedTo.IsActive || (hero.PartyBelongedTo.IsCurrentlyEngagingParty && hero.PartyBelongedTo.MapEvent != null))
					{
						return;
					}
					hero.PartyBelongedTo.MemberRoster.RemoveTroop(hero.CharacterObject, 1, default(UniqueTroopDescriptor), 0);
				}
				if (detail == TeleportHeroAction.TeleportationDetail.DelayedTeleportToPartyAsPartyLeader)
				{
					TextObject textObject = new TextObject("{=ithcVNfA}{CLAN_NAME}{.o} Party", null);
					textObject.SetTextVariable("CLAN_NAME", (targetParty.ActualClan != null) ? targetParty.ActualClan.Name : CampaignData.NeutralFactionName);
					targetParty.Party.SetCustomName(textObject);
				}
				hero.ChangeState(Hero.CharacterStates.Traveling);
				break;
			default:
				return;
			}
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x0017C349 File Offset: 0x0017A549
		public static void ApplyImmediateTeleportToSettlement(Hero heroToBeMoved, Settlement targetSettlement)
		{
			TeleportHeroAction.ApplyInternal(heroToBeMoved, targetSettlement, null, TeleportHeroAction.TeleportationDetail.ImmediateTeleportToSettlement);
		}

		// Token: 0x06004B01 RID: 19201 RVA: 0x0017C354 File Offset: 0x0017A554
		public static void ApplyImmediateTeleportToParty(Hero heroToBeMoved, MobileParty party)
		{
			TeleportHeroAction.ApplyInternal(heroToBeMoved, null, party, TeleportHeroAction.TeleportationDetail.ImmediateTeleportToParty);
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x0017C35F File Offset: 0x0017A55F
		public static void ApplyImmediateTeleportToPartyAsPartyLeader(Hero heroToBeMoved, MobileParty party)
		{
			TeleportHeroAction.ApplyInternal(heroToBeMoved, null, party, TeleportHeroAction.TeleportationDetail.ImmediateTeleportToPartyAsPartyLeader);
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x0017C36A File Offset: 0x0017A56A
		public static void ApplyDelayedTeleportToSettlement(Hero heroToBeMoved, Settlement targetSettlement)
		{
			TeleportHeroAction.ApplyInternal(heroToBeMoved, targetSettlement, null, TeleportHeroAction.TeleportationDetail.DelayedTeleportToSettlement);
		}

		// Token: 0x06004B04 RID: 19204 RVA: 0x0017C375 File Offset: 0x0017A575
		public static void ApplyDelayedTeleportToParty(Hero heroToBeMoved, MobileParty party)
		{
			TeleportHeroAction.ApplyInternal(heroToBeMoved, null, party, TeleportHeroAction.TeleportationDetail.DelayedTeleportToParty);
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x0017C380 File Offset: 0x0017A580
		public static void ApplyDelayedTeleportToSettlementAsGovernor(Hero heroToBeMoved, Settlement targetSettlement)
		{
			TeleportHeroAction.ApplyInternal(heroToBeMoved, targetSettlement, null, TeleportHeroAction.TeleportationDetail.DelayedTeleportToSettlementAsGovernor);
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x0017C38B File Offset: 0x0017A58B
		public static void ApplyDelayedTeleportToPartyAsPartyLeader(Hero heroToBeMoved, MobileParty party)
		{
			TeleportHeroAction.ApplyInternal(heroToBeMoved, null, party, TeleportHeroAction.TeleportationDetail.DelayedTeleportToPartyAsPartyLeader);
		}

		// Token: 0x020008A6 RID: 2214
		public enum TeleportationDetail
		{
			// Token: 0x040024F8 RID: 9464
			ImmediateTeleportToSettlement,
			// Token: 0x040024F9 RID: 9465
			ImmediateTeleportToParty,
			// Token: 0x040024FA RID: 9466
			ImmediateTeleportToPartyAsPartyLeader,
			// Token: 0x040024FB RID: 9467
			DelayedTeleportToSettlement,
			// Token: 0x040024FC RID: 9468
			DelayedTeleportToParty,
			// Token: 0x040024FD RID: 9469
			DelayedTeleportToSettlementAsGovernor,
			// Token: 0x040024FE RID: 9470
			DelayedTeleportToPartyAsPartyLeader
		}
	}
}
