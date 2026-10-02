using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x0200010C RID: 268
	public class WeaponDesignResultPropertyItemVM : ViewModel
	{
		// Token: 0x060017EF RID: 6127 RVA: 0x0005B748 File Offset: 0x00059948
		public WeaponDesignResultPropertyItemVM(TextObject description, float value, float changeAmount, bool showFloatingPoint)
		{
			this._description = description;
			this.InitialValue = value;
			this.ChangeAmount = changeAmount;
			this.ShowFloatingPoint = showFloatingPoint;
			this.IsOrderResult = false;
			this.OrderRequirementTooltip = new HintViewModel();
			this.CraftedValueTooltip = new HintViewModel();
			this.BonusPenaltyTooltip = new HintViewModel();
			this.RefreshValues();
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0005B7A8 File Offset: 0x000599A8
		public WeaponDesignResultPropertyItemVM(TextObject description, float craftedValue, float requiredValue, float changeAmount, bool showFloatingPoint, bool isExceedingBeneficial, bool showTooltip = true)
		{
			this._showTooltip = showTooltip;
			this._description = description;
			this.TargetValue = requiredValue;
			this.InitialValue = craftedValue;
			this.ChangeAmount = changeAmount;
			this._isExceedingBeneficial = isExceedingBeneficial;
			this.IsOrderResult = true;
			this.ShowFloatingPoint = showFloatingPoint;
			this.OrderRequirementTooltip = new HintViewModel();
			this.CraftedValueTooltip = new HintViewModel();
			this.BonusPenaltyTooltip = new HintViewModel();
			this.RefreshValues();
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x0005B820 File Offset: 0x00059A20
		public override void RefreshValues()
		{
			base.RefreshValues();
			TextObject description = this._description;
			this.PropertyLbl = ((description != null) ? description.ToString() : null);
			TextObject textObject = GameTexts.FindText("str_STR_in_parentheses", null);
			textObject.SetTextVariable("STR", CampaignUIHelper.GetFormattedItemPropertyText(this.TargetValue, this.ShowFloatingPoint));
			this.RequiredValueText = ((this.TargetValue == 0f) ? string.Empty : textObject.ToString());
			this.HasBenefit = (this._isExceedingBeneficial ? (this.InitialValue + this.ChangeAmount >= this.TargetValue) : (this.InitialValue + this.ChangeAmount <= this.TargetValue));
			this.OrderRequirementTooltip.HintText = (this._showTooltip ? GameTexts.FindText("str_crafting_order_requirement_tooltip", null) : TextObject.GetEmpty());
			this.CraftedValueTooltip.HintText = (this._showTooltip ? GameTexts.FindText("str_crafting_crafted_value_tooltip", null) : TextObject.GetEmpty());
			this.BonusPenaltyTooltip.HintText = (this._showTooltip ? GameTexts.FindText("str_crafting_bonus_penalty_tooltip", null) : TextObject.GetEmpty());
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0005B943 File Offset: 0x00059B43
		// (set) Token: 0x060017F3 RID: 6131 RVA: 0x0005B94B File Offset: 0x00059B4B
		[DataSourceProperty]
		public string PropertyLbl
		{
			get
			{
				return this._propertyLbl;
			}
			set
			{
				if (value != this._propertyLbl)
				{
					this._propertyLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "PropertyLbl");
				}
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x0005B96E File Offset: 0x00059B6E
		// (set) Token: 0x060017F5 RID: 6133 RVA: 0x0005B976 File Offset: 0x00059B76
		[DataSourceProperty]
		public float InitialValue
		{
			get
			{
				return this._propertyValue;
			}
			set
			{
				if (value == 0f || value != this._propertyValue)
				{
					this._propertyValue = value;
					base.OnPropertyChangedWithValue(value, "InitialValue");
				}
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x0005B99C File Offset: 0x00059B9C
		// (set) Token: 0x060017F7 RID: 6135 RVA: 0x0005B9A4 File Offset: 0x00059BA4
		[DataSourceProperty]
		public float TargetValue
		{
			get
			{
				return this._requiredValue;
			}
			set
			{
				if (value != this._requiredValue)
				{
					this._requiredValue = value;
					base.OnPropertyChangedWithValue(value, "TargetValue");
				}
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x0005B9C2 File Offset: 0x00059BC2
		// (set) Token: 0x060017F9 RID: 6137 RVA: 0x0005B9CA File Offset: 0x00059BCA
		[DataSourceProperty]
		public string RequiredValueText
		{
			get
			{
				return this._requiredValueText;
			}
			set
			{
				if (value != this._requiredValueText)
				{
					this._requiredValueText = value;
					base.OnPropertyChangedWithValue<string>(value, "RequiredValueText");
				}
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x0005B9ED File Offset: 0x00059BED
		// (set) Token: 0x060017FB RID: 6139 RVA: 0x0005B9F5 File Offset: 0x00059BF5
		[DataSourceProperty]
		public float ChangeAmount
		{
			get
			{
				return this._changeAmount;
			}
			set
			{
				if (this._changeAmount != value)
				{
					this._changeAmount = value;
					base.OnPropertyChangedWithValue(value, "ChangeAmount");
				}
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x0005BA13 File Offset: 0x00059C13
		// (set) Token: 0x060017FD RID: 6141 RVA: 0x0005BA1B File Offset: 0x00059C1B
		[DataSourceProperty]
		public bool ShowFloatingPoint
		{
			get
			{
				return this._showFloatingPoint;
			}
			set
			{
				if (this._showFloatingPoint != value)
				{
					this._showFloatingPoint = value;
					base.OnPropertyChangedWithValue(value, "ShowFloatingPoint");
				}
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x0005BA39 File Offset: 0x00059C39
		// (set) Token: 0x060017FF RID: 6143 RVA: 0x0005BA41 File Offset: 0x00059C41
		[DataSourceProperty]
		public bool IsOrderResult
		{
			get
			{
				return this._isOrderResult;
			}
			set
			{
				if (value != this._isOrderResult)
				{
					this._isOrderResult = value;
					base.OnPropertyChangedWithValue(value, "IsOrderResult");
				}
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x0005BA5F File Offset: 0x00059C5F
		// (set) Token: 0x06001801 RID: 6145 RVA: 0x0005BA67 File Offset: 0x00059C67
		[DataSourceProperty]
		public bool HasBenefit
		{
			get
			{
				return this._hasBenefit;
			}
			set
			{
				if (value != this._hasBenefit)
				{
					this._hasBenefit = value;
					base.OnPropertyChangedWithValue(value, "HasBenefit");
				}
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x0005BA85 File Offset: 0x00059C85
		// (set) Token: 0x06001803 RID: 6147 RVA: 0x0005BA8D File Offset: 0x00059C8D
		[DataSourceProperty]
		public HintViewModel OrderRequirementTooltip
		{
			get
			{
				return this._orderRequirementTooltip;
			}
			set
			{
				if (value != this._orderRequirementTooltip)
				{
					this._orderRequirementTooltip = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "OrderRequirementTooltip");
				}
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x0005BAAB File Offset: 0x00059CAB
		// (set) Token: 0x06001805 RID: 6149 RVA: 0x0005BAB3 File Offset: 0x00059CB3
		[DataSourceProperty]
		public HintViewModel CraftedValueTooltip
		{
			get
			{
				return this._craftedValueTooltip;
			}
			set
			{
				if (value != this._craftedValueTooltip)
				{
					this._craftedValueTooltip = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "CraftedValueTooltip");
				}
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x0005BAD1 File Offset: 0x00059CD1
		// (set) Token: 0x06001807 RID: 6151 RVA: 0x0005BAD9 File Offset: 0x00059CD9
		[DataSourceProperty]
		public HintViewModel BonusPenaltyTooltip
		{
			get
			{
				return this._bonusPenaltyTooltip;
			}
			set
			{
				if (value != this._bonusPenaltyTooltip)
				{
					this._bonusPenaltyTooltip = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "BonusPenaltyTooltip");
				}
			}
		}

		// Token: 0x04000AFA RID: 2810
		private readonly TextObject _description;

		// Token: 0x04000AFB RID: 2811
		private bool _isExceedingBeneficial;

		// Token: 0x04000AFC RID: 2812
		private bool _showTooltip;

		// Token: 0x04000AFD RID: 2813
		private string _propertyLbl;

		// Token: 0x04000AFE RID: 2814
		private float _propertyValue;

		// Token: 0x04000AFF RID: 2815
		private float _requiredValue;

		// Token: 0x04000B00 RID: 2816
		private string _requiredValueText;

		// Token: 0x04000B01 RID: 2817
		private float _changeAmount;

		// Token: 0x04000B02 RID: 2818
		private bool _showFloatingPoint;

		// Token: 0x04000B03 RID: 2819
		private bool _isOrderResult;

		// Token: 0x04000B04 RID: 2820
		private bool _hasBenefit;

		// Token: 0x04000B05 RID: 2821
		private HintViewModel _orderRequirementTooltip;

		// Token: 0x04000B06 RID: 2822
		private HintViewModel _craftedValueTooltip;

		// Token: 0x04000B07 RID: 2823
		private HintViewModel _bonusPenaltyTooltip;
	}
}
