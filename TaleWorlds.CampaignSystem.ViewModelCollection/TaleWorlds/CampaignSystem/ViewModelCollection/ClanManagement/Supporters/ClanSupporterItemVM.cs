using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Supporters
{
	// Token: 0x02000131 RID: 305
	public class ClanSupporterItemVM : ViewModel
	{
		// Token: 0x06001CA1 RID: 7329 RVA: 0x0006A037 File Offset: 0x00068237
		public ClanSupporterItemVM(Hero hero)
		{
			this.Hero = new HeroVM(hero, false);
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0006A04C File Offset: 0x0006824C
		public void ExecuteOpenTooltip()
		{
			InformationManager.ShowTooltip(typeof(Hero), new object[]
			{
				this.Hero.Hero,
				false
			});
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0006A07A File Offset: 0x0006827A
		public void ExecuteCloseTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x0006A081 File Offset: 0x00068281
		// (set) Token: 0x06001CA5 RID: 7333 RVA: 0x0006A089 File Offset: 0x00068289
		[DataSourceProperty]
		public HeroVM Hero
		{
			get
			{
				return this._hero;
			}
			set
			{
				if (value != this._hero)
				{
					this._hero = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "Hero");
				}
			}
		}

		// Token: 0x04000D5C RID: 3420
		private HeroVM _hero;
	}
}
