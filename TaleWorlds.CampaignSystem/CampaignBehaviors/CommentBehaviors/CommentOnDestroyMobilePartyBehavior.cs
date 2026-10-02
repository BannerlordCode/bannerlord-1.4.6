using System;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.Party;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x0200045F RID: 1119
	public class CommentOnDestroyMobilePartyBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004832 RID: 18482 RVA: 0x0016BB82 File Offset: 0x00169D82
		public override void RegisterEvents()
		{
			CampaignEvents.MobilePartyDestroyed.AddNonSerializedListener(this, new Action<MobileParty, PartyBase>(this.OnMobilePartyDestroyed));
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x0016BB9B File Offset: 0x00169D9B
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06004834 RID: 18484 RVA: 0x0016BBA0 File Offset: 0x00169DA0
		private void OnMobilePartyDestroyed(MobileParty mobileParty, PartyBase destroyerParty)
		{
			Hero hero = ((destroyerParty != null) ? destroyerParty.LeaderHero : null);
			IFaction faction = ((destroyerParty != null) ? destroyerParty.MapFaction : null);
			if (hero == Hero.MainHero || mobileParty.LeaderHero == Hero.MainHero || (faction != null && mobileParty.MapFaction != null && faction.IsKingdomFaction && mobileParty.MapFaction.IsKingdomFaction))
			{
				LogEntry.AddLogEntry(new DestroyMobilePartyLogEntry(mobileParty, destroyerParty));
			}
		}
	}
}
