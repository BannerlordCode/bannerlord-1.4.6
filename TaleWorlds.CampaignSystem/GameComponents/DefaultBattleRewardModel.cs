using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Naval;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x020000F6 RID: 246
	public class DefaultBattleRewardModel : BattleRewardModel
	{
		// Token: 0x0600166C RID: 5740 RVA: 0x0006768C File Offset: 0x0006588C
		public override int GetPlayerGainedRelationAmount(MapEvent mapEvent, Hero hero)
		{
			float playerBattleContributionRate = mapEvent.GetPlayerBattleContributionRate();
			float num = (mapEvent.StrengthOfSide[(int)PartyBase.MainParty.Side] - PlayerEncounter.Current.PlayerPartyInitialStrength) / (mapEvent.StrengthOfSide[(int)PartyBase.MainParty.OpponentSide] + 1f);
			float num2 = ((num < 1f) ? (1f + (1f - num)) : ((num < 3f) ? (0.5f * (3f - num)) : 0f));
			float renownValue = (mapEvent.AttackerSide.IsMainPartyAmongParties() ? mapEvent.AttackerSide : mapEvent.DefenderSide).RenownValue;
			ExplainedNumber explainedNumber = new ExplainedNumber(0.75f + MathF.Pow(playerBattleContributionRate * 1.3f * (num2 + renownValue), 0.67f), false, null);
			if (Hero.MainHero.GetPerkValue(DefaultPerks.Charm.Camaraderie))
			{
				explainedNumber.AddFactor(DefaultPerks.Charm.Camaraderie.PrimaryBonus, DefaultPerks.Charm.Camaraderie.Name);
			}
			return (int)explainedNumber.ResultNumber;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00067784 File Offset: 0x00065984
		public override ExplainedNumber CalculateRenownGain(PartyBase winnerParty, float renownValueOfBattleForWinnerSide, float contributionShareOfWinnerParty, float renownMultiplierForWinnerSide, bool includeDescriptions)
		{
			ExplainedNumber explainedNumber = new ExplainedNumber(contributionShareOfWinnerParty * renownValueOfBattleForWinnerSide * renownMultiplierForWinnerSide, includeDescriptions, null);
			if (winnerParty.IsMobile)
			{
				if (winnerParty.MobileParty.HasPerk(DefaultPerks.Throwing.LongReach, true))
				{
					PerkHelper.AddPerkBonusForParty(DefaultPerks.Throwing.LongReach, winnerParty.MobileParty, false, ref explainedNumber, false);
				}
				if (winnerParty.MobileParty.HasPerk(DefaultPerks.Charm.PublicSpeaker, false))
				{
					explainedNumber.AddFactor(DefaultPerks.Charm.PublicSpeaker.PrimaryBonus, DefaultPerks.Charm.PublicSpeaker.Name);
				}
				if (winnerParty.LeaderHero != null)
				{
					PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Leadership.FamousCommander, winnerParty.LeaderHero.CharacterObject, true, ref explainedNumber, winnerParty.MobileParty.IsCurrentlyAtSea);
				}
				if (PartyBaseHelper.HasFeat(winnerParty, DefaultCulturalFeats.VlandianRenownMercenaryFeat))
				{
					explainedNumber.AddFactor(DefaultCulturalFeats.VlandianRenownMercenaryFeat.EffectBonus, GameTexts.FindText("str_culture", null));
				}
			}
			return explainedNumber;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00067858 File Offset: 0x00065A58
		public override ExplainedNumber CalculateInfluenceGain(PartyBase winnerParty, float influenceValueOfBattleForWinnerSide, float contributionShareOfWinnerParty, float influenceMultiplierForWinnerSide, bool includeDescriptions)
		{
			ExplainedNumber explainedNumber = new ExplainedNumber(0f, false, null);
			if (winnerParty.MapFaction.IsKingdomFaction)
			{
				explainedNumber = new ExplainedNumber(influenceValueOfBattleForWinnerSide * contributionShareOfWinnerParty * influenceMultiplierForWinnerSide, includeDescriptions, null);
				if (winnerParty.LeaderHero != null)
				{
					PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Charm.Warlord, winnerParty.LeaderHero.CharacterObject, true, ref explainedNumber, winnerParty.MobileParty.IsCurrentlyAtSea);
				}
			}
			return explainedNumber;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x000678BC File Offset: 0x00065ABC
		public override ExplainedNumber CalculateMoraleGainVictory(PartyBase winnerParty, float renownValueOfBattleForWinnerSide, float contributionShareOfWinnerParty, bool includeDescriptions)
		{
			ExplainedNumber explainedNumber = new ExplainedNumber(0.5f + renownValueOfBattleForWinnerSide * contributionShareOfWinnerParty * 0.5f, includeDescriptions, null);
			if (winnerParty.IsMobile)
			{
				if (winnerParty.MobileParty.HasPerk(DefaultPerks.Throwing.LongReach, true))
				{
					PerkHelper.AddPerkBonusForParty(DefaultPerks.Throwing.LongReach, winnerParty.MobileParty, false, ref explainedNumber, false);
				}
				if (winnerParty.MobileParty.HasPerk(DefaultPerks.Leadership.CitizenMilitia, true))
				{
					PerkHelper.AddPerkBonusForParty(DefaultPerks.Leadership.CitizenMilitia, winnerParty.MobileParty, false, ref explainedNumber, winnerParty.MobileParty.IsCurrentlyAtSea);
				}
			}
			return explainedNumber;
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00067943 File Offset: 0x00065B43
		public override int CalculateGoldLossAfterDefeat(Hero partyLeaderHero)
		{
			return (int)Math.Min((float)partyLeaderHero.Gold * 0.05f, 10000f);
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x00067960 File Offset: 0x00065B60
		public override EquipmentElement GetLootedItemFromTroop(CharacterObject character, float targetValue)
		{
			bool flag = MobileParty.MainParty.HasPerk(DefaultPerks.Engineering.Metallurgy, false);
			EquipmentElement randomItem = DefaultBattleRewardModel.GetRandomItem(character.BattleEquipments.GetRandomElementInefficiently<Equipment>(), targetValue);
			if (flag && randomItem.ItemModifier != null && randomItem.ItemModifier.PriceMultiplier < 1f && MBRandom.RandomFloat < DefaultPerks.Engineering.Metallurgy.PrimaryBonus)
			{
				randomItem = new EquipmentElement(randomItem.Item, null, null, false);
			}
			return randomItem;
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x000679D0 File Offset: 0x00065BD0
		private static EquipmentElement GetRandomItem(Equipment equipment, float targetValue = 0f)
		{
			int num = 0;
			for (int i = 0; i < 12; i++)
			{
				if (equipment[i].Item != null && !equipment[i].Item.NotMerchandise)
				{
					DefaultBattleRewardModel._indices[num] = i;
					num++;
				}
			}
			for (int j = 0; j < num - 1; j++)
			{
				int num2 = j;
				int num3 = equipment[DefaultBattleRewardModel._indices[j]].Item.Value;
				for (int k = j + 1; k < num; k++)
				{
					if (equipment[DefaultBattleRewardModel._indices[k]].Item.Value > num3)
					{
						num2 = k;
						num3 = equipment[DefaultBattleRewardModel._indices[k]].Item.Value;
					}
				}
				int num4 = DefaultBattleRewardModel._indices[j];
				DefaultBattleRewardModel._indices[j] = DefaultBattleRewardModel._indices[num2];
				DefaultBattleRewardModel._indices[num2] = num4;
			}
			if (num > 0)
			{
				for (int l = 0; l < num; l++)
				{
					int num5 = DefaultBattleRewardModel._indices[l];
					EquipmentElement equipmentElement = equipment[num5];
					if (equipmentElement.Item != null && !equipment[num5].Item.NotMerchandise)
					{
						float num6 = (float)equipmentElement.Item.Value + 0.1f;
						float num7 = 0.325f * (targetValue / (MathF.Max(targetValue, num6) * (float)(num - l)));
						if (MBRandom.RandomFloat < num7)
						{
							ItemComponent itemComponent = equipmentElement.Item.ItemComponent;
							ItemModifier itemModifier;
							if (itemComponent == null)
							{
								itemModifier = null;
							}
							else
							{
								ItemModifierGroup itemModifierGroup = itemComponent.ItemModifierGroup;
								itemModifier = ((itemModifierGroup != null) ? itemModifierGroup.GetRandomItemModifierLootScoreBased() : null);
							}
							ItemModifier itemModifier2 = itemModifier;
							if (itemModifier2 != null)
							{
								equipmentElement = new EquipmentElement(equipmentElement.Item, itemModifier2, null, false);
							}
							return equipmentElement;
						}
					}
				}
			}
			return default(EquipmentElement);
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00067B98 File Offset: 0x00065D98
		public override float GetExpectedLootedItemValueFromCasualty(Hero winnerPartyLeaderHero, CharacterObject casualtyCharacter)
		{
			float num = 7.25f * (float)(casualtyCharacter.Level * casualtyCharacter.Level);
			if (winnerPartyLeaderHero != Hero.MainHero)
			{
				return num;
			}
			if (PlayerEncounter.Current != null && PlayerEncounter.Current.ForceHideoutSendTroops)
			{
				return 0f;
			}
			return num * MBRandom.RandomFloatRanged(0.85f, 1.15f);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x00067BEE File Offset: 0x00065DEE
		public override float GetAITradePenalty()
		{
			return 0.018181818f;
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x00067BF5 File Offset: 0x00065DF5
		public override float GetMainPartyMemberScatterChance()
		{
			return 0.1f;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x00067BFC File Offset: 0x00065DFC
		public override int CalculatePlunderedGoldAmountFromDefeatedParty(PartyBase defeatedParty)
		{
			int num = 0;
			if (defeatedParty.LeaderHero != null)
			{
				num = Campaign.Current.Models.BattleRewardModel.CalculateGoldLossAfterDefeat(defeatedParty.LeaderHero);
			}
			else if (defeatedParty.IsMobile && defeatedParty.MobileParty.IsPartyTradeActive)
			{
				MobileParty mobileParty = defeatedParty.MobileParty;
				num = (int)((float)mobileParty.PartyTradeGold * (mobileParty.IsBandit ? 0.5f : 0.1f));
			}
			return num;
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x00067C6C File Offset: 0x00065E6C
		public override MBReadOnlyList<KeyValuePair<MapEventParty, float>> GetLootGoldChances(MBReadOnlyList<MapEventParty> winnerParties)
		{
			MBList<KeyValuePair<MapEventParty, float>> mblist = new MBList<KeyValuePair<MapEventParty, float>>();
			float num = 0f;
			foreach (MapEventParty mapEventParty in winnerParties)
			{
				if (mapEventParty.ContributionToBattle > 0 && (!mapEventParty.Party.IsMobile || !mapEventParty.Party.MobileParty.IsPatrolParty))
				{
					mblist.Add(new KeyValuePair<MapEventParty, float>(mapEventParty, (float)mapEventParty.ContributionToBattle));
					num += (float)mapEventParty.ContributionToBattle;
				}
			}
			for (int i = 0; i < mblist.Count; i++)
			{
				mblist[i] = new KeyValuePair<MapEventParty, float>(mblist[i].Key, mblist[i].Value / num);
			}
			return mblist;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x00067D4C File Offset: 0x00065F4C
		public override void GetCaptureMemberChancesForWinnerParties(MapEvent endedMapEvent, MBReadOnlyList<MapEventParty> winnerParties, out MBList<KeyValuePair<MapEventParty, float>> woundedMemberChances, out MBList<KeyValuePair<MapEventParty, float>> healthyMemberChances)
		{
			woundedMemberChances = new MBList<KeyValuePair<MapEventParty, float>>();
			healthyMemberChances = new MBList<KeyValuePair<MapEventParty, float>>();
			float num = 0f;
			float num2 = 0.25f;
			if (endedMapEvent.GetMapEventSide(endedMapEvent.DefeatedSide).IsSurrendered)
			{
				num2 = 1f;
			}
			foreach (MapEventParty mapEventParty in winnerParties)
			{
				MobileParty mobileParty = mapEventParty.Party.MobileParty;
				if (mapEventParty.ContributionToBattle > 0 && mapEventParty.Party.MemberRoster.Count > 0 && (mobileParty == null || (!mobileParty.IsVillager && !mobileParty.IsCaravan && !mobileParty.IsPatrolParty && ((!mobileParty.IsGarrison && !mobileParty.IsMilitia) || !mobileParty.CurrentSettlement.IsVillage))))
				{
					healthyMemberChances.Add(new KeyValuePair<MapEventParty, float>(mapEventParty, (float)mapEventParty.ContributionToBattle));
					num += (float)mapEventParty.ContributionToBattle;
				}
			}
			for (int i = 0; i < healthyMemberChances.Count; i++)
			{
				woundedMemberChances.Add(new KeyValuePair<MapEventParty, float>(healthyMemberChances[i].Key, healthyMemberChances[i].Value / num * 1f));
				healthyMemberChances[i] = new KeyValuePair<MapEventParty, float>(healthyMemberChances[i].Key, healthyMemberChances[i].Value / num * num2);
			}
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x00067EE4 File Offset: 0x000660E4
		public override MBReadOnlyList<KeyValuePair<MapEventParty, float>> GetLootPrisonerChances(MBReadOnlyList<MapEventParty> winnerParties, TroopRosterElement prisonerElement)
		{
			MBList<KeyValuePair<MapEventParty, float>> mblist = new MBList<KeyValuePair<MapEventParty, float>>();
			CharacterObject character = prisonerElement.Character;
			if (character.HeroObject == null || !character.HeroObject.IsReleased)
			{
				float num = 0f;
				Occupation occupation = character.Occupation;
				foreach (MapEventParty mapEventParty in winnerParties)
				{
					MobileParty mobileParty = mapEventParty.Party.MobileParty;
					if (mapEventParty.ContributionToBattle > 0 && mapEventParty.Party.MemberRoster.Count > 0 && ((mobileParty == null && occupation != Occupation.Bandit) || (mobileParty != null && !mobileParty.IsVillager && !mobileParty.IsCaravan && !mobileParty.IsMilitia && !mobileParty.IsPatrolParty && (!mobileParty.IsBandit || occupation == Occupation.Bandit) && (!mobileParty.IsGarrison || occupation != Occupation.Bandit))))
					{
						mblist.Add(new KeyValuePair<MapEventParty, float>(mapEventParty, (float)mapEventParty.ContributionToBattle));
						num += (float)mapEventParty.ContributionToBattle;
					}
				}
				for (int i = 0; i < mblist.Count; i++)
				{
					mblist[i] = new KeyValuePair<MapEventParty, float>(mblist[i].Key, mblist[i].Value / num * 1f);
				}
			}
			return mblist;
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x00068050 File Offset: 0x00066250
		public override MBList<KeyValuePair<MapEventParty, float>> GetLootItemChancesForWinnerParties(MBReadOnlyList<MapEventParty> winnerParties, PartyBase defeatedParty)
		{
			MBList<KeyValuePair<MapEventParty, float>> mblist = new MBList<KeyValuePair<MapEventParty, float>>();
			if (PlayerEncounter.Current != null && PlayerEncounter.Current.ForceHideoutSendTroops)
			{
				if (winnerParties.Any<MapEventParty>((MapEventParty x) => x.Party == PartyBase.MainParty))
				{
					return mblist;
				}
			}
			if (!defeatedParty.IsSettlement)
			{
				MBList<KeyValuePair<MapEventParty, float>> mblist2 = new MBList<KeyValuePair<MapEventParty, float>>();
				float num = 0f;
				foreach (MapEventParty mapEventParty in winnerParties)
				{
					MobileParty mobileParty = mapEventParty.Party.MobileParty;
					PartyBase party = mapEventParty.Party;
					if (mapEventParty.ContributionToBattle > 0 && mapEventParty.Party.MemberRoster.Count > 0 && (mobileParty == null || (!mobileParty.IsGarrison && !mobileParty.IsMilitia)))
					{
						mblist2.Add(new KeyValuePair<MapEventParty, float>(mapEventParty, (float)mapEventParty.ContributionToBattle));
						num += (float)mapEventParty.ContributionToBattle;
						ExplainedNumber explainedNumber = new ExplainedNumber(1f, false, null);
						SkillHelper.AddSkillBonusForParty(DefaultSkillEffects.RogueryLootBonus, party.MobileParty, ref explainedNumber);
						if (party.LeaderHero != null && party.LeaderHero.GetPerkValue(DefaultPerks.Roguery.RogueExtraordinaire))
						{
							PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.Roguery.RogueExtraordinaire, party.LeaderHero.CharacterObject, DefaultSkills.Roguery, true, ref explainedNumber, Campaign.Current.Models.CharacterDevelopmentModel.MinSkillRequiredForEpicPerkBonus, false);
						}
						float num2 = explainedNumber.ResultNumber;
						if (party.MobileParty.HasPerk(DefaultPerks.Roguery.KnowHow, false) && (defeatedParty.MobileParty.IsCaravan || defeatedParty.MobileParty.IsVillager))
						{
							num2 *= 1f + DefaultPerks.Roguery.KnowHow.PrimaryBonus;
						}
						mblist.Add(new KeyValuePair<MapEventParty, float>(mapEventParty, num2));
					}
				}
				for (int i = 0; i < mblist2.Count; i++)
				{
					mblist[i] = new KeyValuePair<MapEventParty, float>(mblist2[i].Key, mblist2[i].Value / num * mblist[i].Value * 0.5f);
				}
			}
			return mblist;
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x000682A4 File Offset: 0x000664A4
		public override MBReadOnlyList<KeyValuePair<MapEventParty, float>> GetLootCasualtyChances(MBReadOnlyList<MapEventParty> winnerParties, PartyBase defeatedParty)
		{
			MBList<KeyValuePair<MapEventParty, float>> mblist = new MBList<KeyValuePair<MapEventParty, float>>();
			if (!defeatedParty.IsSettlement || !defeatedParty.Settlement.IsTown)
			{
				float num = 0f;
				foreach (MapEventParty mapEventParty in winnerParties)
				{
					MobileParty mobileParty = mapEventParty.Party.MobileParty;
					if (mapEventParty.ContributionToBattle > 0 && mapEventParty.Party.MemberRoster.Count > 0 && (mobileParty == null || (!mobileParty.IsGarrison && !mobileParty.IsMilitia)))
					{
						mblist.Add(new KeyValuePair<MapEventParty, float>(mapEventParty, (float)mapEventParty.ContributionToBattle));
						num += (float)mapEventParty.ContributionToBattle;
					}
				}
				for (int i = 0; i < mblist.Count; i++)
				{
					mblist[i] = new KeyValuePair<MapEventParty, float>(mblist[i].Key, mblist[i].Value / num * 1f);
				}
			}
			return mblist;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x000683B8 File Offset: 0x000665B8
		public override float CalculateShipDamageAfterDefeat(Ship ship)
		{
			return 0f;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x000683BF File Offset: 0x000665BF
		public override MBReadOnlyList<KeyValuePair<Ship, MapEventParty>> DistributeDefeatedPartyShipsAmongWinners(MapEvent mapEvent, MBReadOnlyList<Ship> shipsToLoot, MBReadOnlyList<MapEventParty> winnerParties)
		{
			return new MBReadOnlyList<KeyValuePair<Ship, MapEventParty>>();
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x000683C8 File Offset: 0x000665C8
		public override float GetBannerLootChanceFromDefeatedHero(Hero defeatedHero)
		{
			Clan clan = defeatedHero.Clan;
			Hero hero;
			if (clan == null)
			{
				hero = null;
			}
			else
			{
				Kingdom kingdom = clan.Kingdom;
				hero = ((kingdom != null) ? kingdom.RulingClan.Leader : null);
			}
			if (hero == defeatedHero)
			{
				return 0.1f;
			}
			Clan clan2 = defeatedHero.Clan;
			if (((clan2 != null) ? clan2.Leader : null) == defeatedHero)
			{
				return 0.25f;
			}
			return 0.5f;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00068424 File Offset: 0x00066624
		public override ItemObject GetBannerRewardForWinningMapEvent(MapEvent mapEvent)
		{
			if (mapEvent.IsHideoutBattle || (mapEvent.AttackerSide.MissionSide == mapEvent.PlayerSide && mapEvent.IsSiegeAssault))
			{
				bool isHideoutBattle = mapEvent.IsHideoutBattle;
				Settlement mapEventSettlement = mapEvent.MapEventSettlement;
				float num = (isHideoutBattle ? 0.1f : 0.5f);
				if (MBRandom.RandomFloat <= num)
				{
					MBList<ItemObject> mblist = Campaign.Current.Models.BannerItemModel.GetPossibleRewardBannerItems().ToMBList<ItemObject>();
					if (mblist.Count > 0)
					{
						mblist.Shuffle<ItemObject>();
						int num2 = (isHideoutBattle ? 1 : mapEventSettlement.Town.GetWallLevel());
						foreach (ItemObject itemObject in mblist)
						{
							if (((BannerComponent)itemObject.ItemComponent).BannerLevel == num2 && (itemObject.Culture == null || itemObject.Culture.StringId == "neutral_culture" || (!isHideoutBattle && itemObject.Culture == mapEventSettlement.Culture)))
							{
								return itemObject;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00068550 File Offset: 0x00066750
		public override float GetSunkenShipMoraleEffect(PartyBase shipOwner, Ship ship)
		{
			return 0f;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00068558 File Offset: 0x00066758
		public override float CalculateMoraleChangeOnRoundVictory(PartyBase party, MapEventSide partySide, BattleSideEnum roundWinner)
		{
			float num = 0f;
			if (partySide.MissionSide != roundWinner && roundWinner != BattleSideEnum.None)
			{
				if (partySide.MapEvent.RetreatingSide != BattleSideEnum.None)
				{
					num = -1f;
				}
				else
				{
					num = -3f;
				}
			}
			return num;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00068595 File Offset: 0x00066795
		public override float GetShipSiegeEngineHitMoraleEffect(Ship ship, SiegeEngineType siegeEngineType)
		{
			return 0f;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x0006859C File Offset: 0x0006679C
		public override Figurehead GetFigureheadLoot(MBReadOnlyList<MapEventParty> defeatedParties, PartyBase defeatedSideLeaderParty)
		{
			return null;
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x0006859F File Offset: 0x0006679F
		public override MBReadOnlyList<MapEventParty> GetWinnerPartiesThatCanPlunderGoldFromShips(MBReadOnlyList<MapEventParty> winnerParties)
		{
			return new MBReadOnlyList<MapEventParty>();
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x000685A6 File Offset: 0x000667A6
		public override bool CanTroopBeTakenPrisoner(CharacterObject troop)
		{
			return true;
		}

		// Token: 0x04000784 RID: 1924
		private static readonly int[] _indices = new int[12];

		// Token: 0x04000785 RID: 1925
		private const float DestroyHideoutBannerLootChance = 0.1f;

		// Token: 0x04000786 RID: 1926
		private const float CaptureSettlementBannerLootChance = 0.5f;

		// Token: 0x04000787 RID: 1927
		private const float DefeatRegularHeroBannerLootChance = 0.5f;

		// Token: 0x04000788 RID: 1928
		private const float DefeatClanLeaderBannerLootChance = 0.25f;

		// Token: 0x04000789 RID: 1929
		private const float DefeatKingdomRulerBannerLootChance = 0.1f;

		// Token: 0x0400078A RID: 1930
		private const float MainPartyMemberScatterChance = 0.1f;
	}
}
