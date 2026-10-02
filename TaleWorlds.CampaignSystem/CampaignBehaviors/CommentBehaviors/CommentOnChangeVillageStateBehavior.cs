using System;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x02000459 RID: 1113
	public class CommentOnChangeVillageStateBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004819 RID: 18457 RVA: 0x0016B877 File Offset: 0x00169A77
		public override void RegisterEvents()
		{
			CampaignEvents.VillageStateChanged.AddNonSerializedListener(this, new Action<Village, Village.VillageStates, Village.VillageStates, MobileParty>(this.OnVillageStateChanged));
		}

		// Token: 0x0600481A RID: 18458 RVA: 0x0016B890 File Offset: 0x00169A90
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0600481B RID: 18459 RVA: 0x0016B894 File Offset: 0x00169A94
		private void OnVillageStateChanged(Village village, Village.VillageStates oldState, Village.VillageStates newState, MobileParty raiderParty)
		{
			if (newState != Village.VillageStates.Normal && raiderParty != null && (raiderParty.LeaderHero == Hero.MainHero || village.Owner.Settlement.OwnerClan.Leader == Hero.MainHero || village.Settlement.MapFaction.IsKingdomFaction || raiderParty.MapFaction.IsKingdomFaction))
			{
				LogEntry.AddLogEntry(new VillageStateChangedLogEntry(village, oldState, newState, raiderParty));
			}
		}
	}
}
