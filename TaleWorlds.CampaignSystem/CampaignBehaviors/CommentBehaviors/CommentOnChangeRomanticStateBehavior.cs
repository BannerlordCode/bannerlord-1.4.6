using System;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x02000457 RID: 1111
	public class CommentOnChangeRomanticStateBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004811 RID: 18449 RVA: 0x0016B7AC File Offset: 0x001699AC
		public override void RegisterEvents()
		{
			CampaignEvents.RomanticStateChanged.AddNonSerializedListener(this, new Action<Hero, Hero, Romance.RomanceLevelEnum>(this.OnRomanticStateChanged));
		}

		// Token: 0x06004812 RID: 18450 RVA: 0x0016B7C5 File Offset: 0x001699C5
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x0016B7C7 File Offset: 0x001699C7
		private void OnRomanticStateChanged(Hero hero1, Hero hero2, Romance.RomanceLevelEnum level)
		{
			if (hero1 == Hero.MainHero || hero2 == Hero.MainHero || hero1.Clan.Leader == hero1 || hero2.Clan.Leader == hero2)
			{
				LogEntry.AddLogEntry(new ChangeRomanticStateLogEntry(hero1, hero2, level));
			}
		}
	}
}
