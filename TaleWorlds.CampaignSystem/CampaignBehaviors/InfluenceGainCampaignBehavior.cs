using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000405 RID: 1029
	public class InfluenceGainCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x060040E6 RID: 16614 RVA: 0x00130713 File Offset: 0x0012E913
		public override void RegisterEvents()
		{
			CampaignEvents.OnPrisonerDonatedToSettlementEvent.AddNonSerializedListener(this, new Action<MobileParty, FlattenedTroopRoster, Settlement>(this.OnPrisonerDonatedToSettlement));
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x0013072C File Offset: 0x0012E92C
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x00130730 File Offset: 0x0012E930
		private void OnPrisonerDonatedToSettlement(MobileParty donatingParty, FlattenedTroopRoster donatedPrisoners, Settlement donatedSettlement)
		{
			if (donatedSettlement.OwnerClan != Clan.PlayerClan || donatingParty.ActualClan != Clan.PlayerClan)
			{
				float num = 0f;
				foreach (FlattenedTroopRosterElement flattenedTroopRosterElement in donatedPrisoners)
				{
					num += Campaign.Current.Models.PrisonerDonationModel.CalculateInfluenceGainAfterPrisonerDonation(donatingParty.Party, flattenedTroopRosterElement.Troop, donatedSettlement);
				}
				GainKingdomInfluenceAction.ApplyForDonatePrisoners(donatingParty, num);
			}
		}
	}
}
