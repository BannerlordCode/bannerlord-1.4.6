using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.MapNotificationTypes;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x02000463 RID: 1123
	public class CommentOnMakePeaceBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004842 RID: 18498 RVA: 0x0016BCB5 File Offset: 0x00169EB5
		public override void RegisterEvents()
		{
			CampaignEvents.MakePeace.AddNonSerializedListener(this, new Action<IFaction, IFaction, MakePeaceAction.MakePeaceDetail>(this.OnMakePeace));
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x0016BCCE File Offset: 0x00169ECE
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x0016BCD0 File Offset: 0x00169ED0
		private void OnMakePeace(IFaction faction1, IFaction faction2, MakePeaceAction.MakePeaceDetail detail)
		{
			MakePeaceLogEntry makePeaceLogEntry = new MakePeaceLogEntry(faction1, faction2);
			LogEntry.AddLogEntry(makePeaceLogEntry);
			if (faction2 == Hero.MainHero.MapFaction || (faction1 == Hero.MainHero.MapFaction && detail != MakePeaceAction.MakePeaceDetail.ByKingdomDecision))
			{
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new PeaceMapNotification(faction1, faction2, makePeaceLogEntry.GetEncyclopediaText()));
			}
		}
	}
}
