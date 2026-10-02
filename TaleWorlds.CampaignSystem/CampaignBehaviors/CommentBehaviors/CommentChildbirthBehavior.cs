using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.MapNotificationTypes;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x02000456 RID: 1110
	public class CommentChildbirthBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600480D RID: 18445 RVA: 0x0016B6B6 File Offset: 0x001698B6
		public override void RegisterEvents()
		{
			CampaignEvents.OnGivenBirthEvent.AddNonSerializedListener(this, new Action<Hero, List<Hero>, int>(this.OnGivenBirthEvent));
		}

		// Token: 0x0600480E RID: 18446 RVA: 0x0016B6CF File Offset: 0x001698CF
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0600480F RID: 18447 RVA: 0x0016B6D4 File Offset: 0x001698D4
		private void OnGivenBirthEvent(Hero mother, List<Hero> aliveChildren, int stillbornCount)
		{
			if (mother.IsHumanPlayerCharacter || mother.Clan == Hero.MainHero.Clan)
			{
				for (int i = 0; i < stillbornCount; i++)
				{
					ChildbirthLogEntry childbirthLogEntry = new ChildbirthLogEntry(mother, null);
					LogEntry.AddLogEntry(childbirthLogEntry);
					Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new ChildBornMapNotification(null, childbirthLogEntry.GetEncyclopediaText(), CampaignTime.Now));
				}
				foreach (Hero hero in aliveChildren)
				{
					ChildbirthLogEntry childbirthLogEntry2 = new ChildbirthLogEntry(mother, hero);
					LogEntry.AddLogEntry(childbirthLogEntry2);
					Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new ChildBornMapNotification(hero, childbirthLogEntry2.GetEncyclopediaText(), CampaignTime.Now));
				}
			}
		}
	}
}
