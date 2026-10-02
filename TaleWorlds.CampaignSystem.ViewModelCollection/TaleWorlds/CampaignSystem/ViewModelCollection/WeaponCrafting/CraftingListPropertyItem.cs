using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting
{
	// Token: 0x020000FA RID: 250
	public class CraftingListPropertyItem : ViewModel
	{
		// Token: 0x06001693 RID: 5779 RVA: 0x00057F30 File Offset: 0x00056130
		public CraftingListPropertyItem(TextObject description, float maxValue, float value, float targetValue, CraftingTemplate.CraftingStatTypes propertyType, bool isAlternativeUsageProperty = false)
		{
			this.Description = description;
			this.PropertyMaxValue = maxValue;
			this.PropertyValue = value;
			this.TargetValue = targetValue;
			this.IsAlternativeUsageProperty = isAlternativeUsageProperty;
			this.Type = propertyType;
			this.RefreshValues();
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00057F84 File Offset: 0x00056184
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.HasValidTarget = this.TargetValue > float.Epsilon;
			this.HasValidValue = this.PropertyValue > float.Epsilon;
			TextObject description = this.Description;
			this.PropertyLbl = ((description != null) ? description.ToString() : null);
			this.IsExceedingBeneficial = this.CheckIfExceedingIsBeneficial();
			this.SeparatorText = new TextObject("{=dB6cFDmz}/", null).ToString();
			this.PropertyValueText = CampaignUIHelper.GetFormattedItemPropertyText(this.PropertyValue, this.GetIsTypeRequireInteger(this.Type));
			if (this.HasValidTarget)
			{
				this.TargetValueText = CampaignUIHelper.GetFormattedItemPropertyText(this.TargetValue, this.GetIsTypeRequireInteger(this.Type));
			}
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00058039 File Offset: 0x00056239
		private bool CheckIfExceedingIsBeneficial()
		{
			return this.Type > CraftingTemplate.CraftingStatTypes.Weight;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00058044 File Offset: 0x00056244
		private bool GetIsTypeRequireInteger(CraftingTemplate.CraftingStatTypes type)
		{
			return type == CraftingTemplate.CraftingStatTypes.StackAmount;
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0005804B File Offset: 0x0005624B
		// (set) Token: 0x06001698 RID: 5784 RVA: 0x00058053 File Offset: 0x00056253
		[DataSourceProperty]
		public bool IsValidForUsage
		{
			get
			{
				return this._showStats;
			}
			set
			{
				if (value != this._showStats)
				{
					this._showStats = value;
					base.OnPropertyChangedWithValue(value, "IsValidForUsage");
				}
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x00058071 File Offset: 0x00056271
		// (set) Token: 0x0600169A RID: 5786 RVA: 0x00058079 File Offset: 0x00056279
		[DataSourceProperty]
		public bool IsExceedingBeneficial
		{
			get
			{
				return this._isExceedingBeneficial;
			}
			set
			{
				if (value != this._isExceedingBeneficial)
				{
					this._isExceedingBeneficial = value;
					base.OnPropertyChangedWithValue(value, "IsExceedingBeneficial");
				}
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x00058097 File Offset: 0x00056297
		// (set) Token: 0x0600169C RID: 5788 RVA: 0x0005809F File Offset: 0x0005629F
		[DataSourceProperty]
		public bool HasValidTarget
		{
			get
			{
				return this._hasValidTarget;
			}
			set
			{
				if (value != this._hasValidTarget)
				{
					this._hasValidTarget = value;
					base.OnPropertyChangedWithValue(value, "HasValidTarget");
				}
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x000580BD File Offset: 0x000562BD
		// (set) Token: 0x0600169E RID: 5790 RVA: 0x000580C5 File Offset: 0x000562C5
		[DataSourceProperty]
		public bool HasValidValue
		{
			get
			{
				return this._hasValidValue;
			}
			set
			{
				if (value != this._hasValidValue)
				{
					this._hasValidValue = value;
					base.OnPropertyChangedWithValue(value, "HasValidValue");
				}
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x000580E3 File Offset: 0x000562E3
		// (set) Token: 0x060016A0 RID: 5792 RVA: 0x000580EB File Offset: 0x000562EB
		[DataSourceProperty]
		public float TargetValue
		{
			get
			{
				return this._targetValue;
			}
			set
			{
				if (value != this._targetValue)
				{
					this._targetValue = value;
					base.OnPropertyChangedWithValue(value, "TargetValue");
				}
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00058109 File Offset: 0x00056309
		// (set) Token: 0x060016A2 RID: 5794 RVA: 0x00058111 File Offset: 0x00056311
		[DataSourceProperty]
		public string TargetValueText
		{
			get
			{
				return this._targetValueText;
			}
			set
			{
				if (value != this._targetValueText)
				{
					this._targetValueText = value;
					base.OnPropertyChangedWithValue<string>(value, "TargetValueText");
				}
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x00058134 File Offset: 0x00056334
		// (set) Token: 0x060016A4 RID: 5796 RVA: 0x0005813C File Offset: 0x0005633C
		[DataSourceProperty]
		public bool IsAlternativeUsageProperty
		{
			get
			{
				return this._isAlternativeUsageProperty;
			}
			set
			{
				if (this._isAlternativeUsageProperty != value)
				{
					this._isAlternativeUsageProperty = value;
					base.OnPropertyChangedWithValue(value, "IsAlternativeUsageProperty");
				}
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x0005815A File Offset: 0x0005635A
		// (set) Token: 0x060016A6 RID: 5798 RVA: 0x00058162 File Offset: 0x00056362
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

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x00058185 File Offset: 0x00056385
		// (set) Token: 0x060016A8 RID: 5800 RVA: 0x0005818D File Offset: 0x0005638D
		[DataSourceProperty]
		public float PropertyValue
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
					base.OnPropertyChangedWithValue(value, "PropertyValue");
				}
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x000581B3 File Offset: 0x000563B3
		// (set) Token: 0x060016AA RID: 5802 RVA: 0x000581BB File Offset: 0x000563BB
		[DataSourceProperty]
		public float PropertyMaxValue
		{
			get
			{
				return this._propertyMaxValue;
			}
			set
			{
				if (value != this._propertyMaxValue)
				{
					this._propertyMaxValue = value;
					base.OnPropertyChangedWithValue(value, "PropertyMaxValue");
				}
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x000581D9 File Offset: 0x000563D9
		// (set) Token: 0x060016AC RID: 5804 RVA: 0x000581E1 File Offset: 0x000563E1
		[DataSourceProperty]
		public string PropertyValueText
		{
			get
			{
				return this._propertyValueText;
			}
			set
			{
				if (this._propertyValueText != value)
				{
					this._propertyValueText = value;
					base.OnPropertyChangedWithValue<string>(value, "PropertyValueText");
				}
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x00058204 File Offset: 0x00056404
		// (set) Token: 0x060016AE RID: 5806 RVA: 0x0005820C File Offset: 0x0005640C
		[DataSourceProperty]
		public string SeparatorText
		{
			get
			{
				return this._separatorText;
			}
			set
			{
				if (value != this._separatorText)
				{
					this._separatorText = value;
					base.OnPropertyChangedWithValue<string>(value, "SeparatorText");
				}
			}
		}

		// Token: 0x04000A52 RID: 2642
		public readonly TextObject Description;

		// Token: 0x04000A53 RID: 2643
		public readonly CraftingTemplate.CraftingStatTypes Type;

		// Token: 0x04000A54 RID: 2644
		private bool _showStats;

		// Token: 0x04000A55 RID: 2645
		private bool _isExceedingBeneficial;

		// Token: 0x04000A56 RID: 2646
		private bool _hasValidTarget;

		// Token: 0x04000A57 RID: 2647
		private bool _hasValidValue;

		// Token: 0x04000A58 RID: 2648
		private float _targetValue;

		// Token: 0x04000A59 RID: 2649
		private string _targetValueText;

		// Token: 0x04000A5A RID: 2650
		private string _propertyLbl;

		// Token: 0x04000A5B RID: 2651
		private float _propertyValue;

		// Token: 0x04000A5C RID: 2652
		private float _propertyMaxValue = -1f;

		// Token: 0x04000A5D RID: 2653
		private string _propertyValueText;

		// Token: 0x04000A5E RID: 2654
		public bool _isAlternativeUsageProperty;

		// Token: 0x04000A5F RID: 2655
		private string _separatorText;
	}
}
