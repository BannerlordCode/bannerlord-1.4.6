using System;
using System.Runtime.CompilerServices;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200040C RID: 1036
	public interface IStatisticsCampaignBehavior : ICampaignBehavior
	{
		// Token: 0x06004138 RID: 16696
		void OnDefectionPersuasionSucess();

		// Token: 0x06004139 RID: 16697
		void OnPlayerAcceptedRansomOffer(int ransomPrice);

		// Token: 0x0600413A RID: 16698
		int GetHighestTournamentRank();

		// Token: 0x0600413B RID: 16699
		int GetNumberOfTournamentWins();

		// Token: 0x0600413C RID: 16700
		int GetNumberOfChildrenBorn();

		// Token: 0x0600413D RID: 16701
		int GetNumberOfPrisonersRecruited();

		// Token: 0x0600413E RID: 16702
		int GetNumberOfTroopsRecruited();

		// Token: 0x0600413F RID: 16703
		int GetNumberOfClansDefected();

		// Token: 0x06004140 RID: 16704
		int GetNumberOfIssuesSolved();

		// Token: 0x06004141 RID: 16705
		int GetTotalInfluenceEarned();

		// Token: 0x06004142 RID: 16706
		int GetTotalCrimeRatingGained();

		// Token: 0x06004143 RID: 16707
		int GetNumberOfBattlesWon();

		// Token: 0x06004144 RID: 16708
		int GetNumberOfBattlesLost();

		// Token: 0x06004145 RID: 16709
		int GetLargestBattleWonAsLeader();

		// Token: 0x06004146 RID: 16710
		int GetLargestArmyFormedByPlayer();

		// Token: 0x06004147 RID: 16711
		int GetNumberOfEnemyClansDestroyed();

		// Token: 0x06004148 RID: 16712
		int GetNumberOfHeroesKilledInBattle();

		// Token: 0x06004149 RID: 16713
		int GetNumberOfTroopsKnockedOrKilledAsParty();

		// Token: 0x0600414A RID: 16714
		int GetNumberOfTroopsKnockedOrKilledByPlayer();

		// Token: 0x0600414B RID: 16715
		int GetNumberOfHeroPrisonersTaken();

		// Token: 0x0600414C RID: 16716
		int GetNumberOfTroopPrisonersTaken();

		// Token: 0x0600414D RID: 16717
		int GetNumberOfTownsCaptured();

		// Token: 0x0600414E RID: 16718
		int GetNumberOfHideoutsCleared();

		// Token: 0x0600414F RID: 16719
		int GetNumberOfCastlesCaptured();

		// Token: 0x06004150 RID: 16720
		int GetNumberOfVillagesRaided();

		// Token: 0x06004151 RID: 16721
		int GetNumberOfCraftingPartsUnlocked();

		// Token: 0x06004152 RID: 16722
		int GetNumberOfWeaponsCrafted();

		// Token: 0x06004153 RID: 16723
		int GetNumberOfCraftingOrdersCompleted();

		// Token: 0x06004154 RID: 16724
		int GetNumberOfCompanionsHired();

		// Token: 0x06004155 RID: 16725
		ulong GetTotalTimePlayedInSeconds();

		// Token: 0x06004156 RID: 16726
		ulong GetTotalDenarsEarned();

		// Token: 0x06004157 RID: 16727
		ulong GetDenarsEarnedFromCaravans();

		// Token: 0x06004158 RID: 16728
		ulong GetDenarsEarnedFromWorkshops();

		// Token: 0x06004159 RID: 16729
		ulong GetDenarsEarnedFromRansoms();

		// Token: 0x0600415A RID: 16730
		ulong GetDenarsEarnedFromTaxes();

		// Token: 0x0600415B RID: 16731
		ulong GetDenarsEarnedFromTributes();

		// Token: 0x0600415C RID: 16732
		ulong GetDenarsPaidAsTributes();

		// Token: 0x0600415D RID: 16733
		CampaignTime GetTotalTimePlayed();

		// Token: 0x0600415E RID: 16734
		CampaignTime GetTimeSpentAsPrisoner();

		// Token: 0x0600415F RID: 16735
		ValueTuple<string, int> GetMostExpensiveItemCrafted();

		// Token: 0x06004160 RID: 16736
		[return: TupleElementNames(new string[] { "name", "value" })]
		ValueTuple<string, int> GetCompanionWithMostKills();

		// Token: 0x06004161 RID: 16737
		[return: TupleElementNames(new string[] { "name", "value" })]
		ValueTuple<string, int> GetCompanionWithMostIssuesSolved();
	}
}
