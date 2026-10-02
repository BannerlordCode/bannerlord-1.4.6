using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000429 RID: 1065
	public class PartyRolesCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x060043D1 RID: 17361 RVA: 0x00149F98 File Offset: 0x00148198
		public override void RegisterEvents()
		{
			CampaignEvents.HeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnHeroKilled));
			CampaignEvents.OnGovernorChangedEvent.AddNonSerializedListener(this, new Action<Town, Hero, Hero>(this.OnGovernorChanged));
			CampaignEvents.MobilePartyCreated.AddNonSerializedListener(this, new Action<MobileParty>(this.OnPartySpawned));
			CampaignEvents.CompanionRemoved.AddNonSerializedListener(this, new Action<Hero, RemoveCompanionAction.RemoveCompanionDetail>(this.OnCompanionRemoved));
			CampaignEvents.OnHeroGetsBusyEvent.AddNonSerializedListener(this, new Action<Hero, HeroGetsBusyReasons>(this.OnHeroGetsBusy));
			CampaignEvents.HeroPrisonerTaken.AddNonSerializedListener(this, new Action<PartyBase, Hero>(this.OnHeroPrisonerTaken));
			CampaignEvents.OnHeroChangedClanEvent.AddNonSerializedListener(this, new Action<Hero, Clan>(this.OnHeroChangedClan));
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x0014A046 File Offset: 0x00148246
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x0014A048 File Offset: 0x00148248
		private void OnHeroKilled(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
		{
			if (victim.Clan == Clan.PlayerClan)
			{
				this.RemoveAllPartyRolesOfHeroIfExist(victim);
			}
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x0014A05E File Offset: 0x0014825E
		private void OnHeroPrisonerTaken(PartyBase party, Hero prisoner)
		{
			if (prisoner.Clan == Clan.PlayerClan)
			{
				this.RemoveAllPartyRolesOfHeroIfExist(prisoner);
			}
		}

		// Token: 0x060043D5 RID: 17365 RVA: 0x0014A074 File Offset: 0x00148274
		private void OnGovernorChanged(Town fortification, Hero oldGovernor, Hero newGovernor)
		{
			if (((newGovernor != null) ? newGovernor.Clan : null) == Clan.PlayerClan)
			{
				this.RemoveAllPartyRolesOfHeroIfExist(newGovernor);
			}
		}

		// Token: 0x060043D6 RID: 17366 RVA: 0x0014A090 File Offset: 0x00148290
		private void OnPartySpawned(MobileParty spawnedParty)
		{
			if (spawnedParty.IsLordParty && spawnedParty.ActualClan == Clan.PlayerClan)
			{
				foreach (TroopRosterElement troopRosterElement in spawnedParty.MemberRoster.GetTroopRoster())
				{
					if (troopRosterElement.Character.IsHero)
					{
						this.RemoveAllPartyRolesOfHeroIfExist(troopRosterElement.Character.HeroObject);
					}
				}
			}
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x0014A114 File Offset: 0x00148314
		private void OnCompanionRemoved(Hero companion, RemoveCompanionAction.RemoveCompanionDetail detail)
		{
			this.RemoveAllPartyRolesOfHeroIfExist(companion);
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x0014A11D File Offset: 0x0014831D
		private void OnHeroGetsBusy(Hero hero, HeroGetsBusyReasons heroGetsBusyReason)
		{
			if (hero.Clan == Clan.PlayerClan)
			{
				this.RemoveAllPartyRolesOfHeroIfExist(hero);
			}
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x0014A133 File Offset: 0x00148333
		private void OnHeroChangedClan(Hero hero, Clan oldClan)
		{
			if (oldClan == Clan.PlayerClan)
			{
				this.RemoveAllPartyRolesOfHeroIfExist(hero);
			}
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x0014A144 File Offset: 0x00148344
		private void RemoveAllPartyRolesOfHeroIfExist(Hero hero)
		{
			foreach (WarPartyComponent warPartyComponent in Clan.PlayerClan.WarPartyComponents)
			{
				warPartyComponent.MobileParty.RemoveAllPartyRolesOfHero(hero);
			}
		}
	}
}
