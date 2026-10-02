using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004CB RID: 1227
	public static class TakePrisonerAction
	{
		// Token: 0x06004AFC RID: 19196 RVA: 0x0017BFC4 File Offset: 0x0017A1C4
		private static void ApplyInternal(PartyBase capturerParty, Hero prisonerCharacter, bool isEventCalled = true)
		{
			if (prisonerCharacter.PartyBelongedTo != null)
			{
				if (prisonerCharacter.PartyBelongedTo.LeaderHero == prisonerCharacter)
				{
					prisonerCharacter.PartyBelongedTo.RemovePartyLeader();
				}
				prisonerCharacter.PartyBelongedTo.MemberRoster.RemoveTroop(prisonerCharacter.CharacterObject, 1, default(UniqueTroopDescriptor), 0);
			}
			prisonerCharacter.CaptivityStartTime = CampaignTime.Now;
			prisonerCharacter.ChangeState(Hero.CharacterStates.Prisoner);
			capturerParty.AddPrisoner(prisonerCharacter.CharacterObject, 1);
			if (prisonerCharacter == Hero.MainHero)
			{
				if (MobileParty.MainParty.IsDisorganized)
				{
					MobileParty.MainParty.SetDisorganized(false);
				}
				PlayerCaptivity.StartCaptivity(capturerParty);
				if (MobileParty.MainParty.IsCurrentlyAtSea)
				{
					for (int i = MobileParty.MainParty.Ships.Count - 1; i >= 0; i--)
					{
						DestroyShipAction.Apply(MobileParty.MainParty.Ships[i]);
					}
				}
			}
			if (prisonerCharacter.StayingInSettlement != null)
			{
				prisonerCharacter.StayingInSettlement = null;
			}
			if (isEventCalled)
			{
				CampaignEventDispatcher.Instance.OnHeroPrisonerTaken(capturerParty, prisonerCharacter);
			}
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x0017C0B3 File Offset: 0x0017A2B3
		public static void Apply(PartyBase capturerParty, Hero prisonerCharacter)
		{
			TakePrisonerAction.ApplyInternal(capturerParty, prisonerCharacter, true);
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x0017C0C0 File Offset: 0x0017A2C0
		public static void ApplyByTakenFromPartyScreen(FlattenedTroopRoster roster)
		{
			foreach (FlattenedTroopRosterElement flattenedTroopRosterElement in roster)
			{
				if (flattenedTroopRosterElement.Troop.IsHero)
				{
					TakePrisonerAction.ApplyInternal(PartyBase.MainParty, flattenedTroopRosterElement.Troop.HeroObject, true);
				}
			}
			CampaignEventDispatcher.Instance.OnPrisonerTaken(roster);
		}
	}
}
