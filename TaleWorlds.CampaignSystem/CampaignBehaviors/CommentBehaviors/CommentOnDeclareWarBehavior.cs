using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.MapNotificationTypes;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x0200045D RID: 1117
	public class CommentOnDeclareWarBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600482A RID: 18474 RVA: 0x0016BAD9 File Offset: 0x00169CD9
		public override void RegisterEvents()
		{
			CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
		}

		// Token: 0x0600482B RID: 18475 RVA: 0x0016BAF2 File Offset: 0x00169CF2
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0600482C RID: 18476 RVA: 0x0016BAF4 File Offset: 0x00169CF4
		private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail detail)
		{
			DeclareWarLogEntry declareWarLogEntry = new DeclareWarLogEntry(faction1, faction2);
			LogEntry.AddLogEntry(declareWarLogEntry);
			if (faction2 == Hero.MainHero.MapFaction || (faction1 == Hero.MainHero.MapFaction && detail != DeclareWarAction.DeclareWarDetail.CausedByKingdomDecision))
			{
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new WarMapNotification(faction1, faction2, declareWarLogEntry.GetEncyclopediaText()));
			}
		}
	}
}
