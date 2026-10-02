using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200041B RID: 1051
	public class MobilePartyTrainingBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004340 RID: 17216 RVA: 0x00145BF0 File Offset: 0x00143DF0
		public override void RegisterEvents()
		{
			CampaignEvents.HourlyTickPartyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.HourlyTickParty));
			CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.OnDailyTickParty));
			CampaignEvents.PlayerUpgradedTroopsEvent.AddNonSerializedListener(this, new Action<CharacterObject, CharacterObject, int>(this.OnPlayerUpgradedTroops));
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x00145C42 File Offset: 0x00143E42
		private void OnPlayerUpgradedTroops(CharacterObject troop, CharacterObject upgrade, int number)
		{
			SkillLevelingManager.OnUpgradeTroops(PartyBase.MainParty, troop, upgrade, number);
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x00145C54 File Offset: 0x00143E54
		private void HourlyTickParty(MobileParty mobileParty)
		{
			if (mobileParty.LeaderHero != null)
			{
				if (mobileParty.BesiegerCamp != null)
				{
					SkillLevelingManager.OnSieging(mobileParty);
				}
				if (mobileParty.Army != null && mobileParty.Army.LeaderParty == mobileParty && mobileParty.AttachedParties.Count > 0)
				{
					SkillLevelingManager.OnLeadingArmy(mobileParty);
				}
				if (mobileParty.IsActive)
				{
					this.WorkSkills(mobileParty);
				}
			}
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x00145CB0 File Offset: 0x00143EB0
		private void OnDailyTickParty(MobileParty mobileParty)
		{
			foreach (TroopRosterElement troopRosterElement in mobileParty.MemberRoster.GetTroopRoster())
			{
				ExplainedNumber effectiveDailyExperience = Campaign.Current.Models.PartyTrainingModel.GetEffectiveDailyExperience(mobileParty, troopRosterElement);
				if (!troopRosterElement.Character.IsHero)
				{
					mobileParty.Party.MemberRoster.AddXpToTroop(troopRosterElement.Character, MathF.Round(effectiveDailyExperience.ResultNumber * (float)troopRosterElement.Number));
				}
			}
			if (!mobileParty.IsDisbanding && mobileParty.HasPerk(DefaultPerks.Bow.Trainer, false))
			{
				Hero hero = null;
				int num = int.MaxValue;
				foreach (TroopRosterElement troopRosterElement2 in mobileParty.MemberRoster.GetTroopRoster())
				{
					if (troopRosterElement2.Character.IsHero)
					{
						int skillValue = troopRosterElement2.Character.HeroObject.GetSkillValue(DefaultSkills.Bow);
						if (skillValue < num)
						{
							num = skillValue;
							hero = troopRosterElement2.Character.HeroObject;
						}
					}
				}
				if (hero != null)
				{
					hero.AddSkillXp(DefaultSkills.Bow, DefaultPerks.Bow.Trainer.PrimaryBonus);
				}
			}
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x00145E0C File Offset: 0x0014400C
		private void CheckScouting(MobileParty mobileParty)
		{
			if (mobileParty.EffectiveScout != null && !mobileParty.IsCurrentlyAtSea)
			{
				TerrainType faceTerrainType = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(mobileParty.CurrentNavigationFace);
				if (mobileParty != MobileParty.MainParty)
				{
					SkillLevelingManager.OnAIPartiesTravel(mobileParty.EffectiveScout, mobileParty.IsCaravan, faceTerrainType);
				}
				SkillLevelingManager.OnTraverseTerrain(mobileParty, faceTerrainType);
			}
		}

		// Token: 0x06004345 RID: 17221 RVA: 0x00145E60 File Offset: 0x00144060
		private void WorkSkills(MobileParty mobileParty)
		{
			if (mobileParty.IsMoving)
			{
				this.CheckScouting(mobileParty);
				if (CampaignTime.Now.GetHourOfDay % 4 == 1)
				{
					this.CheckMovementSkills(mobileParty);
				}
			}
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x00145E98 File Offset: 0x00144098
		private void CheckMovementSkills(MobileParty mobileParty)
		{
			if (mobileParty == MobileParty.MainParty)
			{
				if (mobileParty.IsCurrentlyAtSea)
				{
					SkillLevelingManager.OnTravelOnWater(mobileParty, mobileParty._lastCalculatedSpeed);
					return;
				}
				using (List<TroopRosterElement>.Enumerator enumerator = mobileParty.MemberRoster.GetTroopRoster().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TroopRosterElement troopRosterElement = enumerator.Current;
						if (troopRosterElement.Character.IsHero)
						{
							if (troopRosterElement.Character.Equipment.Horse.IsEmpty)
							{
								SkillLevelingManager.OnTravelOnFoot(troopRosterElement.Character.HeroObject, mobileParty._lastCalculatedSpeed);
							}
							else
							{
								SkillLevelingManager.OnTravelOnHorse(troopRosterElement.Character.HeroObject, mobileParty._lastCalculatedSpeed);
							}
						}
					}
					return;
				}
			}
			if (mobileParty.LeaderHero != null)
			{
				if (mobileParty.IsCurrentlyAtSea)
				{
					SkillLevelingManager.OnTravelOnWater(mobileParty, mobileParty._lastCalculatedSpeed);
					return;
				}
				if (mobileParty.LeaderHero.CharacterObject.Equipment.Horse.IsEmpty)
				{
					SkillLevelingManager.OnTravelOnFoot(mobileParty.LeaderHero, mobileParty._lastCalculatedSpeed);
					return;
				}
				SkillLevelingManager.OnTravelOnHorse(mobileParty.LeaderHero, mobileParty._lastCalculatedSpeed);
			}
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x00145FC0 File Offset: 0x001441C0
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
