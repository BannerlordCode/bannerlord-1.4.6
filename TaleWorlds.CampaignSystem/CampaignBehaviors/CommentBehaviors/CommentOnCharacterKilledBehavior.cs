using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.MapNotificationTypes;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x0200045A RID: 1114
	public class CommentOnCharacterKilledBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600481D RID: 18461 RVA: 0x0016B909 File Offset: 0x00169B09
		public override void RegisterEvents()
		{
			CampaignEvents.BeforeHeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnBeforeHeroKilled));
		}

		// Token: 0x0600481E RID: 18462 RVA: 0x0016B922 File Offset: 0x00169B22
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x0016B924 File Offset: 0x00169B24
		private void OnBeforeHeroKilled(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification)
		{
			if (victim.Clan != null && !Clan.BanditFactions.Contains(victim.Clan))
			{
				CharacterKilledLogEntry characterKilledLogEntry = new CharacterKilledLogEntry(victim, killer, detail);
				LogEntry.AddLogEntry(characterKilledLogEntry);
				if (this.IsRelatedToPlayer(victim) && ((detail != KillCharacterAction.KillCharacterActionDetail.Executed && detail != KillCharacterAction.KillCharacterActionDetail.ExecutionAfterMapEvent) || killer != Hero.MainHero))
				{
					Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new DeathMapNotification(victim, killer, characterKilledLogEntry.GetEncyclopediaText(), detail, CampaignTime.Now));
				}
			}
		}

		// Token: 0x06004820 RID: 18464 RVA: 0x0016B998 File Offset: 0x00169B98
		private bool IsRelatedToPlayer(Hero victim)
		{
			bool flag = victim == Hero.MainHero.Mother || victim == Hero.MainHero.Father || victim == Hero.MainHero.Spouse || victim == Hero.MainHero;
			if (!flag)
			{
				foreach (Hero hero in Hero.MainHero.Children)
				{
					if (victim == hero)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				foreach (Hero hero2 in Hero.MainHero.Siblings)
				{
					if (victim == hero2)
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}
	}
}
