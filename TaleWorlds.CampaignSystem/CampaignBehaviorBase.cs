using System;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000033 RID: 51
	public abstract class CampaignBehaviorBase : ICampaignBehavior
	{
		// Token: 0x06000362 RID: 866 RVA: 0x0001788C File Offset: 0x00015A8C
		public CampaignBehaviorBase(string stringId)
		{
			this.StringId = stringId;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001789B File Offset: 0x00015A9B
		public CampaignBehaviorBase()
		{
			this.StringId = base.GetType().Name;
		}

		// Token: 0x06000364 RID: 868
		public abstract void RegisterEvents();

		// Token: 0x06000365 RID: 869 RVA: 0x000178B4 File Offset: 0x00015AB4
		public static T GetCampaignBehavior<T>()
		{
			return Campaign.Current.GetCampaignBehavior<T>();
		}

		// Token: 0x06000366 RID: 870
		public abstract void SyncData(IDataStore dataStore);

		// Token: 0x040000CF RID: 207
		public readonly string StringId;
	}
}
