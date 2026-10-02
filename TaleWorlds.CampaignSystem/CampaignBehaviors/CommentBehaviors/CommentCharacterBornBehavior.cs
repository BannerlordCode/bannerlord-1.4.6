using System;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x02000455 RID: 1109
	public class CommentCharacterBornBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004809 RID: 18441 RVA: 0x0016B683 File Offset: 0x00169883
		public override void RegisterEvents()
		{
			CampaignEvents.HeroCreated.AddNonSerializedListener(this, new Action<Hero, bool>(this.HeroCreated));
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x0016B69C File Offset: 0x0016989C
		private void HeroCreated(Hero hero, bool isBornNaturally)
		{
			if (isBornNaturally)
			{
				LogEntry.AddLogEntry(new CharacterBornLogEntry(hero));
			}
		}

		// Token: 0x0600480B RID: 18443 RVA: 0x0016B6AC File Offset: 0x001698AC
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
