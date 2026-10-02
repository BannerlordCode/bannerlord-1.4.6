using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CharacterDevelopment
{
	// Token: 0x020003AE RID: 942
	public static class TraitLevelingHelper
	{
		// Token: 0x060036E5 RID: 14053 RVA: 0x000E4990 File Offset: 0x000E2B90
		public static void UpdateTraitXPAccordingToTraitLevels()
		{
			foreach (TraitObject traitObject in TraitObject.All)
			{
				int traitLevel = Hero.MainHero.GetTraitLevel(traitObject);
				if (traitLevel != 0)
				{
					int traitXpRequiredForTraitLevel = Campaign.Current.Models.CharacterDevelopmentModel.GetTraitXpRequiredForTraitLevel(traitObject, traitLevel);
					Campaign.Current.PlayerTraitDeveloper.SetPropertyValue(traitObject, traitXpRequiredForTraitLevel);
				}
			}
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000E4A14 File Offset: 0x000E2C14
		public static void OnBattleWon(MapEvent mapEvent, float contribution)
		{
			float strengthRatio = mapEvent.GetMapEventSide(PlayerEncounter.Current.PlayerSide).StrengthRatio;
			if (strengthRatio > 9f)
			{
				int num = (int)(MBMath.Map(strengthRatio, 9f, 10f, 5f, 20f) * contribution);
				if (num > 0)
				{
					TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Valor, num, ActionNotes.BattleValor, null);
				}
			}
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000E4A6F File Offset: 0x000E2C6F
		public static void OnTroopsSacrificed()
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Valor, -30, ActionNotes.SacrificedTroops, null);
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000E4A80 File Offset: 0x000E2C80
		public static void OnLordExecuted()
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Honor, -1000, ActionNotes.SacrificedTroops, null);
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x000E4A94 File Offset: 0x000E2C94
		public static void OnTradeAgreementBroken()
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Honor, -1000, ActionNotes.DishonestBusinessQuarrel, null);
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x000E4AA7 File Offset: 0x000E2CA7
		public static void OnVillageRaided()
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Mercy, -30, ActionNotes.VillageRaid, null);
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000E4AB8 File Offset: 0x000E2CB8
		public static void OnHostileAction(int amount)
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Honor, amount, ActionNotes.HostileAction, null);
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Mercy, amount, ActionNotes.HostileAction, null);
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000E4AD6 File Offset: 0x000E2CD6
		public static void OnPartyTreatedWell()
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Generosity, 20, ActionNotes.PartyTakenCareOf, null);
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x000E4AE7 File Offset: 0x000E2CE7
		public static void OnPartyStarved()
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Generosity, -20, ActionNotes.PartyHungry, null);
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x000E4AF8 File Offset: 0x000E2CF8
		public static void OnIssueFailed(Hero targetHero, Tuple<TraitObject, int>[] effectedTraits)
		{
			foreach (Tuple<TraitObject, int> tuple in effectedTraits)
			{
				TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(tuple.Item1, tuple.Item2, ActionNotes.QuestFailed, targetHero);
			}
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x000E4B30 File Offset: 0x000E2D30
		public static void OnIssueSolvedThroughQuest(Hero targetHero, Tuple<TraitObject, int>[] effectedTraits)
		{
			foreach (Tuple<TraitObject, int> tuple in effectedTraits)
			{
				TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(tuple.Item1, tuple.Item2, ActionNotes.QuestSuccess, targetHero);
			}
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x000E4B65 File Offset: 0x000E2D65
		public static void OnIssueSolvedThroughQuest(Hero targetHero, TraitObject trait, int xp)
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(trait, xp, ActionNotes.QuestSuccess, targetHero);
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x000E4B74 File Offset: 0x000E2D74
		public static void OnIssueSolvedThroughAlternativeSolution(Hero targetHero, Tuple<TraitObject, int>[] effectedTraits)
		{
			foreach (Tuple<TraitObject, int> tuple in effectedTraits)
			{
				TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(tuple.Item1, tuple.Item2, ActionNotes.QuestSuccess, targetHero);
			}
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x000E4BAC File Offset: 0x000E2DAC
		public static void OnIssueSolvedThroughBetrayal(Hero targetHero, Tuple<TraitObject, int>[] effectedTraits)
		{
			foreach (Tuple<TraitObject, int> tuple in effectedTraits)
			{
				TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(tuple.Item1, tuple.Item2, ActionNotes.QuestBetrayal, targetHero);
			}
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000E4BE1 File Offset: 0x000E2DE1
		public static void OnLordFreed(Hero targetHero)
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Calculating, 20, ActionNotes.NPCFreed, targetHero);
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x000E4BF2 File Offset: 0x000E2DF2
		public static void OnPersuasionDefection(Hero targetHero)
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Calculating, 20, ActionNotes.PersuadedToDefect, targetHero);
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x000E4C04 File Offset: 0x000E2E04
		public static void OnSiegeAftermathApplied(Settlement settlement, SiegeAftermathAction.SiegeAftermath aftermathType, TraitObject[] effectedTraits)
		{
			foreach (TraitObject traitObject in effectedTraits)
			{
				TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(traitObject, Campaign.Current.Models.SiegeAftermathModel.GetSiegeAftermathTraitXpChangeForPlayer(traitObject, settlement, aftermathType), ActionNotes.SiegeAftermath, null);
			}
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x000E4C45 File Offset: 0x000E2E45
		public static void OnIncidentResolved(TraitObject trait, int xpValue)
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(trait, xpValue, ActionNotes.DefaultNote, Hero.MainHero);
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x000E4C54 File Offset: 0x000E2E54
		public static void OnAllianceBrokenThroughHostility()
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Honor, -1000, ActionNotes.DishonestBusinessQuarrel, null);
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x000E4C68 File Offset: 0x000E2E68
		private static void AddPlayerTraitXPAndLogEntry(TraitObject trait, int xpValue, ActionNotes context, Hero referenceHero)
		{
			int traitLevel = Hero.MainHero.GetTraitLevel(trait);
			TraitLevelingHelper.AddTraitXp(trait, xpValue);
			if (traitLevel != Hero.MainHero.GetTraitLevel(trait))
			{
				CampaignEventDispatcher.Instance.OnPlayerTraitChanged(trait, traitLevel);
			}
			if (MathF.Abs(xpValue) >= 10)
			{
				LogEntry.AddLogEntry(new PlayerReputationChangesLogEntry(trait, referenceHero, context));
			}
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000E4CBC File Offset: 0x000E2EBC
		private static void AddTraitXp(TraitObject trait, int xpAmount)
		{
			xpAmount += Campaign.Current.PlayerTraitDeveloper.GetPropertyValue(trait);
			int num;
			int num2;
			Campaign.Current.Models.CharacterDevelopmentModel.GetTraitLevelForTraitXp(Hero.MainHero, trait, xpAmount, out num, out num2);
			Campaign.Current.PlayerTraitDeveloper.SetPropertyValue(trait, num2);
			if (num != Hero.MainHero.GetTraitLevel(trait))
			{
				Hero.MainHero.SetTraitLevel(trait, num);
			}
		}

		// Token: 0x04001113 RID: 4371
		private const int LordExecutedHonorPenalty = -1000;

		// Token: 0x04001114 RID: 4372
		private const int TradeAgreementBrokenPenalty = -1000;

		// Token: 0x04001115 RID: 4373
		private const int AllianceBrokenHonorPenalty = -1000;

		// Token: 0x04001116 RID: 4374
		private const int TroopsSacrificedValorPenalty = -30;

		// Token: 0x04001117 RID: 4375
		private const int VillageRaidedMercyPenalty = -30;

		// Token: 0x04001118 RID: 4376
		private const int PartyStarvingGenerosityPenalty = -20;

		// Token: 0x04001119 RID: 4377
		private const int PartyTreatedWellGenerosityBonus = 20;

		// Token: 0x0400111A RID: 4378
		private const int LordFreedCalculatingBonus = 20;

		// Token: 0x0400111B RID: 4379
		private const int PersuasionDefectionCalculatingBonus = 20;
	}
}
