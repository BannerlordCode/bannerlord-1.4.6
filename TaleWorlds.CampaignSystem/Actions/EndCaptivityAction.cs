using System;
using Helpers;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004AF RID: 1199
	public static class EndCaptivityAction
	{
		// Token: 0x06004A81 RID: 19073 RVA: 0x00178E7C File Offset: 0x0017707C
		private static void ApplyInternal(Hero prisoner, EndCaptivityDetail detail, Hero facilitatior = null, bool showNotification = true)
		{
			PartyBase partyBelongedToAsPrisoner = prisoner.PartyBelongedToAsPrisoner;
			IFaction faction = ((partyBelongedToAsPrisoner != null) ? partyBelongedToAsPrisoner.MapFaction : null);
			if (prisoner == Hero.MainHero)
			{
				PlayerCaptivity.EndCaptivity();
				if (partyBelongedToAsPrisoner != null && partyBelongedToAsPrisoner.IsSettlement)
				{
					MobileParty.MainParty.DisembarkToPosition(partyBelongedToAsPrisoner.Settlement.GatePosition);
				}
				else if (partyBelongedToAsPrisoner != null && partyBelongedToAsPrisoner.IsMobile)
				{
					MobileParty.MainParty.IsCurrentlyAtSea = partyBelongedToAsPrisoner.MobileParty.IsCurrentlyAtSea;
				}
				if (facilitatior != null && detail != EndCaptivityDetail.Death)
				{
					StringHelpers.SetCharacterProperties("FACILITATOR", facilitatior.CharacterObject, null, false);
					MBInformationManager.AddQuickInformation(new TextObject("{=xPuSASof}{FACILITATOR.NAME} paid a ransom and freed you from captivity.", null), 0, null, null, "");
				}
				CampaignEventDispatcher.Instance.OnHeroPrisonerReleased(prisoner, partyBelongedToAsPrisoner, faction, detail, true);
				return;
			}
			if (detail == EndCaptivityDetail.Death)
			{
				prisoner.StayingInSettlement = null;
			}
			if (partyBelongedToAsPrisoner != null && partyBelongedToAsPrisoner.PrisonRoster.Contains(prisoner.CharacterObject))
			{
				partyBelongedToAsPrisoner.PrisonRoster.RemoveTroop(prisoner.CharacterObject, 1, default(UniqueTroopDescriptor), 0);
			}
			if (detail != EndCaptivityDetail.Death)
			{
				if (detail <= EndCaptivityDetail.ReleasedByChoice || detail == EndCaptivityDetail.ReleasedByCompensation)
				{
					prisoner.ChangeState(Hero.CharacterStates.Released);
					if (prisoner.IsPlayerCompanion && detail != EndCaptivityDetail.Ransom)
					{
						MakeHeroFugitiveAction.Apply(prisoner, false);
					}
				}
				else
				{
					MakeHeroFugitiveAction.Apply(prisoner, false);
				}
				Settlement currentSettlement = prisoner.CurrentSettlement;
				if (currentSettlement != null)
				{
					currentSettlement.AddHeroWithoutParty(prisoner);
				}
				CampaignEventDispatcher.Instance.OnHeroPrisonerReleased(prisoner, partyBelongedToAsPrisoner, faction, detail, showNotification);
			}
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x00178FBD File Offset: 0x001771BD
		public static void ApplyByReleasedAfterBattle(Hero character)
		{
			EndCaptivityAction.ApplyInternal(character, EndCaptivityDetail.ReleasedAfterBattle, null, true);
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x00178FC8 File Offset: 0x001771C8
		public static void ApplyByRansom(Hero character, Hero facilitator)
		{
			EndCaptivityAction.ApplyInternal(character, EndCaptivityDetail.Ransom, facilitator, true);
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x00178FD3 File Offset: 0x001771D3
		public static void ApplyByPeace(Hero character, Hero facilitator = null)
		{
			EndCaptivityAction.ApplyInternal(character, EndCaptivityDetail.ReleasedAfterPeace, facilitator, true);
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x00178FDE File Offset: 0x001771DE
		public static void ApplyByEscape(Hero character, Hero facilitator = null, bool showNotification = true)
		{
			EndCaptivityAction.ApplyInternal(character, EndCaptivityDetail.ReleasedAfterEscape, facilitator, showNotification);
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x00178FE9 File Offset: 0x001771E9
		public static void ApplyByDeath(Hero character)
		{
			EndCaptivityAction.ApplyInternal(character, EndCaptivityDetail.Death, null, true);
		}

		// Token: 0x06004A87 RID: 19079 RVA: 0x00178FF4 File Offset: 0x001771F4
		public static void ApplyByReleasedByChoice(FlattenedTroopRoster troopRoster)
		{
			foreach (FlattenedTroopRosterElement flattenedTroopRosterElement in troopRoster)
			{
				if (flattenedTroopRosterElement.Troop.IsHero)
				{
					EndCaptivityAction.ApplyInternal(flattenedTroopRosterElement.Troop.HeroObject, EndCaptivityDetail.ReleasedByChoice, null, true);
				}
			}
			CampaignEventDispatcher.Instance.OnPrisonerReleased(troopRoster);
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x00179064 File Offset: 0x00177264
		public static void ApplyByReleasedByChoice(Hero character, Hero facilitator = null)
		{
			EndCaptivityAction.ApplyInternal(character, EndCaptivityDetail.ReleasedByChoice, facilitator, true);
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x0017906F File Offset: 0x0017726F
		public static void ApplyByReleasedByCompensation(Hero character)
		{
			EndCaptivityAction.ApplyInternal(character, EndCaptivityDetail.ReleasedByCompensation, null, true);
		}
	}
}
