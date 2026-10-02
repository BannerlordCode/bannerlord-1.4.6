using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200042D RID: 1069
	public class PerkActivationHandlerCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004424 RID: 17444 RVA: 0x0014C12E File Offset: 0x0014A32E
		public override void RegisterEvents()
		{
			CampaignEvents.PerkOpenedEvent.AddNonSerializedListener(this, new Action<Hero, PerkObject>(this.OnPerkOpened));
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x0014C148 File Offset: 0x0014A348
		private void OnPerkOpened(Hero hero, PerkObject perk)
		{
			if (hero != null)
			{
				if (perk == DefaultPerks.OneHanded.Trainer || perk == DefaultPerks.OneHanded.UnwaveringDefense || perk == DefaultPerks.TwoHanded.ThickHides || perk == DefaultPerks.Athletics.WellBuilt || perk == DefaultPerks.Medicine.PreventiveMedicine)
				{
					hero.HitPoints += (int)perk.PrimaryBonus;
				}
				else if (perk == DefaultPerks.Crafting.VigorousSmith)
				{
					hero.HeroDeveloper.AddAttribute(DefaultCharacterAttributes.Vigor, 1, false);
				}
				else if (perk == DefaultPerks.Crafting.StrongSmith)
				{
					hero.HeroDeveloper.AddAttribute(DefaultCharacterAttributes.Control, 1, false);
				}
				else if (perk == DefaultPerks.Crafting.EnduringSmith)
				{
					hero.HeroDeveloper.AddAttribute(DefaultCharacterAttributes.Endurance, 1, false);
				}
				else if (perk == DefaultPerks.Crafting.WeaponMasterSmith)
				{
					hero.HeroDeveloper.AddFocus(DefaultSkills.OneHanded, 1, false);
					hero.HeroDeveloper.AddFocus(DefaultSkills.TwoHanded, 1, false);
				}
				else if (perk == DefaultPerks.Athletics.Durable)
				{
					hero.HeroDeveloper.AddAttribute(DefaultCharacterAttributes.Endurance, 1, false);
				}
				else if (perk == DefaultPerks.Athletics.Steady)
				{
					hero.HeroDeveloper.AddAttribute(DefaultCharacterAttributes.Control, 1, false);
				}
				else if (perk == DefaultPerks.Athletics.Strong)
				{
					hero.HeroDeveloper.AddAttribute(DefaultCharacterAttributes.Vigor, 1, false);
				}
				if (hero == Hero.MainHero && (perk == DefaultPerks.OneHanded.Prestige || perk == DefaultPerks.TwoHanded.Hope || perk == DefaultPerks.Athletics.ImposingStature || perk == DefaultPerks.Bow.MerryMen || perk == DefaultPerks.Tactics.HordeLeader || perk == DefaultPerks.Scouting.MountedScouts || perk == DefaultPerks.Leadership.Authority || perk == DefaultPerks.Leadership.LeaderOfMasses || perk == DefaultPerks.Leadership.UltimateLeader))
				{
					PartyBase.MainParty.MemberRoster.UpdateVersion();
				}
				if (perk.PrimaryRole == PartyRole.Captain)
				{
					hero.UpdatePowerModifier();
				}
			}
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x0014C2E8 File Offset: 0x0014A4E8
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
