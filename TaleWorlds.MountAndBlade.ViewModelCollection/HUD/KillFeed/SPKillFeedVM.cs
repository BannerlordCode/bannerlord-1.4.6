using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD.KillFeed.General;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD.KillFeed.Personal;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.KillFeed
{
	// Token: 0x0200005C RID: 92
	public class SPKillFeedVM : ViewModel
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x0001ACED File Offset: 0x00018EED
		public SPKillFeedVM()
		{
			this.GeneralCasualty = new SPGeneralKillNotificationVM();
			this.PersonalFeed = new SPPersonalKillNotificationVM();
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001AD0B File Offset: 0x00018F0B
		public void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, bool isHeadshot, bool isSuicide, bool isDrowning)
		{
			this.GeneralCasualty.OnAgentRemoved(affectedAgent, affectorAgent, isHeadshot, isSuicide, isDrowning);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001AD1F File Offset: 0x00018F1F
		public void OnPersonalKill(int damageAmount, bool isMountDamage, bool isFriendlyFire, bool isHeadshot, string killedAgentName, bool isUnconscious)
		{
			this.PersonalFeed.OnPersonalKill(damageAmount, isMountDamage, isFriendlyFire, isHeadshot, killedAgentName, isUnconscious);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001AD35 File Offset: 0x00018F35
		public void OnPersonalDamage(int totalDamage, bool isVictimAgentMount, bool isFriendlyFire, string victimAgentName)
		{
			this.PersonalFeed.OnPersonalHit(totalDamage, isVictimAgentMount, isFriendlyFire, victimAgentName);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001AD47 File Offset: 0x00018F47
		public void OnPersonalMessage(string message)
		{
			this.PersonalFeed.OnPersonalMessage(message);
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0001AD55 File Offset: 0x00018F55
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x0001AD5D File Offset: 0x00018F5D
		[DataSourceProperty]
		public SPGeneralKillNotificationVM GeneralCasualty
		{
			get
			{
				return this._generalCasualty;
			}
			set
			{
				if (value != this._generalCasualty)
				{
					this._generalCasualty = value;
					base.OnPropertyChangedWithValue<SPGeneralKillNotificationVM>(value, "GeneralCasualty");
				}
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0001AD7B File Offset: 0x00018F7B
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x0001AD83 File Offset: 0x00018F83
		[DataSourceProperty]
		public SPPersonalKillNotificationVM PersonalFeed
		{
			get
			{
				return this._personalFeed;
			}
			set
			{
				if (value != this._personalFeed)
				{
					this._personalFeed = value;
					base.OnPropertyChangedWithValue<SPPersonalKillNotificationVM>(value, "PersonalFeed");
				}
			}
		}

		// Token: 0x04000351 RID: 849
		private SPGeneralKillNotificationVM _generalCasualty;

		// Token: 0x04000352 RID: 850
		private SPPersonalKillNotificationVM _personalFeed;
	}
}
