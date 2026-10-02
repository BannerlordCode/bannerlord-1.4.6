using System;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x02000464 RID: 1124
	public class CommentOnPlayerMeetLordBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004846 RID: 18502 RVA: 0x0016BD2D File Offset: 0x00169F2D
		public override void RegisterEvents()
		{
			CampaignEvents.OnPlayerMetHeroEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnPlayerMetCharacter));
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x0016BD46 File Offset: 0x00169F46
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x0016BD48 File Offset: 0x00169F48
		private void OnPlayerMetCharacter(Hero hero)
		{
			if (hero.Mother != Hero.MainHero && hero.Father != Hero.MainHero)
			{
				LogEntry.AddLogEntry(new PlayerMeetLordLogEntry(hero));
			}
		}
	}
}
