using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper.PerkSelection
{
	// Token: 0x02000147 RID: 327
	public class PerkSelectionItemVM : ViewModel
	{
		// Token: 0x06001F70 RID: 8048 RVA: 0x000738BE File Offset: 0x00071ABE
		public PerkSelectionItemVM(PerkObject perk, Action<PerkSelectionItemVM> onSelection)
		{
			this.Perk = perk;
			this._onSelection = onSelection;
			this.RefreshValues();
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x000738DC File Offset: 0x00071ADC
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PickText = new TextObject("{=1CXlqb2U}Pick:", null).ToString();
			this.PerkName = this.Perk.Name.ToString();
			this.PerkDescription = this.Perk.Description.ToString();
			TextObject combinedPerkRoleText = CampaignUIHelper.GetCombinedPerkRoleText(this.Perk);
			this.PerkRole = ((combinedPerkRoleText != null) ? combinedPerkRoleText.ToString() : null) ?? "";
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x00073957 File Offset: 0x00071B57
		public void ExecuteSelection()
		{
			this._onSelection(this);
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x00073965 File Offset: 0x00071B65
		// (set) Token: 0x06001F74 RID: 8052 RVA: 0x0007396D File Offset: 0x00071B6D
		[DataSourceProperty]
		public string PickText
		{
			get
			{
				return this._pickText;
			}
			set
			{
				if (value != this._pickText)
				{
					this._pickText = value;
					base.OnPropertyChangedWithValue<string>(value, "PickText");
				}
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x00073990 File Offset: 0x00071B90
		// (set) Token: 0x06001F76 RID: 8054 RVA: 0x00073998 File Offset: 0x00071B98
		[DataSourceProperty]
		public string PerkName
		{
			get
			{
				return this._perkName;
			}
			set
			{
				if (value != this._perkName)
				{
					this._perkName = value;
					base.OnPropertyChangedWithValue<string>(value, "PerkName");
				}
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x000739BB File Offset: 0x00071BBB
		// (set) Token: 0x06001F78 RID: 8056 RVA: 0x000739C3 File Offset: 0x00071BC3
		[DataSourceProperty]
		public string PerkDescription
		{
			get
			{
				return this._perkDescription;
			}
			set
			{
				if (value != this._perkDescription)
				{
					this._perkDescription = value;
					base.OnPropertyChangedWithValue<string>(value, "PerkDescription");
				}
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x000739E6 File Offset: 0x00071BE6
		// (set) Token: 0x06001F7A RID: 8058 RVA: 0x000739EE File Offset: 0x00071BEE
		[DataSourceProperty]
		public string PerkRole
		{
			get
			{
				return this._perkRole;
			}
			set
			{
				if (value != this._perkRole)
				{
					this._perkRole = value;
					base.OnPropertyChangedWithValue<string>(value, "PerkRole");
				}
			}
		}

		// Token: 0x04000EAD RID: 3757
		private readonly Action<PerkSelectionItemVM> _onSelection;

		// Token: 0x04000EAE RID: 3758
		public readonly PerkObject Perk;

		// Token: 0x04000EAF RID: 3759
		private string _pickText;

		// Token: 0x04000EB0 RID: 3760
		private string _perkName;

		// Token: 0x04000EB1 RID: 3761
		private string _perkDescription;

		// Token: 0x04000EB2 RID: 3762
		private string _perkRole;
	}
}
