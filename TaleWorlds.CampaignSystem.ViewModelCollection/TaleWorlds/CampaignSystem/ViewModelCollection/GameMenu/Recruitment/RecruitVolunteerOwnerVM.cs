using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Recruitment
{
	// Token: 0x020000B1 RID: 177
	public class RecruitVolunteerOwnerVM : HeroVM
	{
		// Token: 0x0600116F RID: 4463 RVA: 0x00045DF7 File Offset: 0x00043FF7
		public RecruitVolunteerOwnerVM(Hero hero, int relation)
			: base(hero, hero != null && hero.IsNotable)
		{
			this._hero = hero;
			this.RelationToPlayer = relation;
			this.RefreshValues();
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00045E20 File Offset: 0x00044020
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this._hero != null)
			{
				if (this._hero.IsPreacher)
				{
					this.TitleText = GameTexts.FindText("str_preacher", null).ToString();
					return;
				}
				if (this._hero.IsGangLeader)
				{
					this.TitleText = GameTexts.FindText("str_gang_leader", null).ToString();
					return;
				}
				if (this._hero.IsMerchant)
				{
					this.TitleText = GameTexts.FindText("str_merchant", null).ToString();
					return;
				}
				if (this._hero.IsRuralNotable)
				{
					this.TitleText = GameTexts.FindText("str_rural_notable", null).ToString();
				}
			}
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00045ECD File Offset: 0x000440CD
		public void ExecuteOpenEncyclopedia()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this._hero.EncyclopediaLink);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00045EE9 File Offset: 0x000440E9
		public void ExecuteFocus()
		{
			Action<RecruitVolunteerOwnerVM> onFocused = RecruitVolunteerOwnerVM.OnFocused;
			if (onFocused == null)
			{
				return;
			}
			onFocused(this);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00045EFB File Offset: 0x000440FB
		public void ExecuteUnfocus()
		{
			Action<RecruitVolunteerOwnerVM> onFocused = RecruitVolunteerOwnerVM.OnFocused;
			if (onFocused == null)
			{
				return;
			}
			onFocused(null);
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x00045F0D File Offset: 0x0004410D
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x00045F15 File Offset: 0x00044115
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x00045F38 File Offset: 0x00044138
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x00045F40 File Offset: 0x00044140
		[DataSourceProperty]
		public int RelationToPlayer
		{
			get
			{
				return this._relationToPlayer;
			}
			set
			{
				if (value != this._relationToPlayer)
				{
					this._relationToPlayer = value;
					base.OnPropertyChangedWithValue(value, "RelationToPlayer");
				}
			}
		}

		// Token: 0x040007F2 RID: 2034
		public static Action<RecruitVolunteerOwnerVM> OnFocused;

		// Token: 0x040007F3 RID: 2035
		private Hero _hero;

		// Token: 0x040007F4 RID: 2036
		private string _titleText;

		// Token: 0x040007F5 RID: 2037
		private int _relationToPlayer;
	}
}
