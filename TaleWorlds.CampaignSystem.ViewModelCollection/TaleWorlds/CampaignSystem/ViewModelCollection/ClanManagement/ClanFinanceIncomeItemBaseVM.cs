using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x02000125 RID: 293
	public class ClanFinanceIncomeItemBaseVM : ViewModel
	{
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x00064277 File Offset: 0x00062477
		// (set) Token: 0x06001A9B RID: 6811 RVA: 0x0006427F File Offset: 0x0006247F
		public IncomeTypes IncomeTypeAsEnum
		{
			get
			{
				return this._incomeTypeAsEnum;
			}
			protected set
			{
				if (value != this._incomeTypeAsEnum)
				{
					this._incomeTypeAsEnum = value;
					this.IncomeType = (int)value;
				}
			}
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00064298 File Offset: 0x00062498
		protected ClanFinanceIncomeItemBaseVM(Action<ClanFinanceIncomeItemBaseVM> onSelection, Action onRefresh)
		{
			this._onSelection = onSelection;
			this._onRefresh = onRefresh;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000642B9 File Offset: 0x000624B9
		protected virtual void PopulateStatsList()
		{
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000642BB File Offset: 0x000624BB
		protected virtual void PopulateActionList()
		{
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x000642BD File Offset: 0x000624BD
		public void OnIncomeSelection()
		{
			this._onSelection(this);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x000642CC File Offset: 0x000624CC
		protected string DetermineIncomeText(int incomeAmount)
		{
			if (incomeAmount == 0)
			{
				return GameTexts.FindText("str_clan_finance_value_zero", null).ToString();
			}
			GameTexts.SetVariable("IS_POSITIVE", (this.Income > 0) ? 1 : 0);
			GameTexts.SetVariable("NUMBER", MathF.Abs(this.Income));
			return GameTexts.FindText("str_clan_finance_value", null).ToString();
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x00064329 File Offset: 0x00062529
		// (set) Token: 0x06001AA2 RID: 6818 RVA: 0x00064331 File Offset: 0x00062531
		[DataSourceProperty]
		public MBBindingList<SelectableItemPropertyVM> ItemProperties
		{
			get
			{
				return this._itemProperties;
			}
			set
			{
				if (value != this._itemProperties)
				{
					this._itemProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<SelectableItemPropertyVM>>(value, "ItemProperties");
				}
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x0006434F File Offset: 0x0006254F
		// (set) Token: 0x06001AA4 RID: 6820 RVA: 0x00064357 File Offset: 0x00062557
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x0006437A File Offset: 0x0006257A
		// (set) Token: 0x06001AA6 RID: 6822 RVA: 0x00064382 File Offset: 0x00062582
		[DataSourceProperty]
		public string Location
		{
			get
			{
				return this._location;
			}
			set
			{
				if (value != this._location)
				{
					this._location = value;
					base.OnPropertyChangedWithValue<string>(value, "Location");
				}
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x000643A5 File Offset: 0x000625A5
		// (set) Token: 0x06001AA8 RID: 6824 RVA: 0x000643AD File Offset: 0x000625AD
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x000643CB File Offset: 0x000625CB
		// (set) Token: 0x06001AAA RID: 6826 RVA: 0x000643D3 File Offset: 0x000625D3
		[DataSourceProperty]
		public string IncomeValueText
		{
			get
			{
				return this._incomeValueText;
			}
			set
			{
				if (value != this._incomeValueText)
				{
					this._incomeValueText = value;
					base.OnPropertyChangedWithValue<string>(value, "IncomeValueText");
				}
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x000643F6 File Offset: 0x000625F6
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x000643FE File Offset: 0x000625FE
		[DataSourceProperty]
		public string ImageName
		{
			get
			{
				return this._imageName;
			}
			set
			{
				if (value != this._imageName)
				{
					this._imageName = value;
					base.OnPropertyChangedWithValue<string>(value, "ImageName");
				}
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x00064421 File Offset: 0x00062621
		// (set) Token: 0x06001AAE RID: 6830 RVA: 0x00064429 File Offset: 0x00062629
		[DataSourceProperty]
		public int Income
		{
			get
			{
				return this._income;
			}
			set
			{
				if (value != this._income)
				{
					this._income = value;
					base.OnPropertyChangedWithValue(value, "Income");
				}
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x00064447 File Offset: 0x00062647
		// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x0006444F File Offset: 0x0006264F
		[DataSourceProperty]
		public ImageIdentifierVM Visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				if (value != this._visual)
				{
					this._visual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Visual");
				}
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x0006446D File Offset: 0x0006266D
		// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x00064475 File Offset: 0x00062675
		[DataSourceProperty]
		public int IncomeType
		{
			get
			{
				return this._incomeType;
			}
			set
			{
				if (value != this._incomeType)
				{
					this._incomeType = value;
					base.OnPropertyChangedWithValue(value, "IncomeType");
				}
			}
		}

		// Token: 0x04000C63 RID: 3171
		protected Action _onRefresh;

		// Token: 0x04000C64 RID: 3172
		protected Action<ClanFinanceIncomeItemBaseVM> _onSelection;

		// Token: 0x04000C65 RID: 3173
		protected IncomeTypes _incomeTypeAsEnum;

		// Token: 0x04000C66 RID: 3174
		private int _incomeType;

		// Token: 0x04000C67 RID: 3175
		private string _name;

		// Token: 0x04000C68 RID: 3176
		private string _location;

		// Token: 0x04000C69 RID: 3177
		private string _incomeValueText;

		// Token: 0x04000C6A RID: 3178
		private string _imageName;

		// Token: 0x04000C6B RID: 3179
		private int _income;

		// Token: 0x04000C6C RID: 3180
		private bool _isSelected;

		// Token: 0x04000C6D RID: 3181
		private ImageIdentifierVM _visual;

		// Token: 0x04000C6E RID: 3182
		private MBBindingList<SelectableItemPropertyVM> _itemProperties = new MBBindingList<SelectableItemPropertyVM>();
	}
}
