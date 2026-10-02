using System;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000E6 RID: 230
	public class EncyclopediaFamilyMemberVM : HeroVM
	{
		// Token: 0x06001599 RID: 5529 RVA: 0x000553E5 File Offset: 0x000535E5
		public EncyclopediaFamilyMemberVM(Hero hero, Hero baseHero)
			: base(hero, false)
		{
			this._baseHero = baseHero;
			this.RefreshValues();
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x000553FC File Offset: 0x000535FC
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this._baseHero != null)
			{
				this.Role = ConversationHelper.GetHeroRelationToHeroTextShort(base.Hero, this._baseHero, true);
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x00055424 File Offset: 0x00053624
		// (set) Token: 0x0600159C RID: 5532 RVA: 0x0005542C File Offset: 0x0005362C
		[DataSourceProperty]
		public string Role
		{
			get
			{
				return this._role;
			}
			set
			{
				if (value != this._role)
				{
					this._role = value;
					base.OnPropertyChangedWithValue<string>(value, "Role");
				}
			}
		}

		// Token: 0x040009D4 RID: 2516
		private readonly Hero _baseHero;

		// Token: 0x040009D5 RID: 2517
		private string _role;
	}
}
