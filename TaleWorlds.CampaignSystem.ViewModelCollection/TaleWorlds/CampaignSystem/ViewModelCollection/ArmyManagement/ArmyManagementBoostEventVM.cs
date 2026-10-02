using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ArmyManagement
{
	// Token: 0x0200015B RID: 347
	public class ArmyManagementBoostEventVM : ViewModel
	{
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x00078863 File Offset: 0x00076A63
		public ArmyManagementBoostEventVM.BoostCurrency CurrencyToPayForCohesion { get; }

		// Token: 0x06002115 RID: 8469 RVA: 0x0007886B File Offset: 0x00076A6B
		public ArmyManagementBoostEventVM(ArmyManagementBoostEventVM.BoostCurrency currencyToPayForCohesion, int amountToPay, int amountOfCohesionToGain, Action<ArmyManagementBoostEventVM> onExecuteEvent)
		{
			this.IsEnabled = true;
			this._onExecuteEvent = onExecuteEvent;
			this.AmountToPay = amountToPay;
			this.AmountOfCohesionToGain = amountOfCohesionToGain;
			this.CurrencyToPayForCohesion = currencyToPayForCohesion;
			this.CurrencyType = (int)currencyToPayForCohesion;
			this.RefreshValues();
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x000788A4 File Offset: 0x00076AA4
		public override void RefreshValues()
		{
			base.RefreshValues();
			GameTexts.SetVariable("AMOUNT", this.AmountToPay);
			this.SpendText = GameTexts.FindText("str_cohesion_boost_spend", null).ToString();
			GameTexts.SetVariable("GAIN_AMOUNT", this.AmountOfCohesionToGain);
			this.GainText = GameTexts.FindText("str_cohesion_boost_gain", null).ToString();
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x00078903 File Offset: 0x00076B03
		private void ExecuteEvent()
		{
			this._onExecuteEvent(this);
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x00078911 File Offset: 0x00076B11
		// (set) Token: 0x06002119 RID: 8473 RVA: 0x00078919 File Offset: 0x00076B19
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x00078937 File Offset: 0x00076B37
		// (set) Token: 0x0600211B RID: 8475 RVA: 0x0007893F File Offset: 0x00076B3F
		[DataSourceProperty]
		public int AmountToPay
		{
			get
			{
				return this._amountToPay;
			}
			set
			{
				if (value != this._amountToPay)
				{
					this._amountToPay = value;
					base.OnPropertyChangedWithValue(value, "AmountToPay");
				}
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x0007895D File Offset: 0x00076B5D
		// (set) Token: 0x0600211D RID: 8477 RVA: 0x00078965 File Offset: 0x00076B65
		[DataSourceProperty]
		public int CurrencyType
		{
			get
			{
				return this._currencyType;
			}
			set
			{
				if (value != this._currencyType)
				{
					this._currencyType = value;
					base.OnPropertyChangedWithValue(value, "CurrencyType");
				}
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x00078983 File Offset: 0x00076B83
		// (set) Token: 0x0600211F RID: 8479 RVA: 0x0007898B File Offset: 0x00076B8B
		[DataSourceProperty]
		public int AmountOfCohesionToGain
		{
			get
			{
				return this._amountOfCohesionToGain;
			}
			set
			{
				if (value != this._amountOfCohesionToGain)
				{
					this._amountOfCohesionToGain = value;
					base.OnPropertyChangedWithValue(value, "AmountOfCohesionToGain");
				}
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x000789A9 File Offset: 0x00076BA9
		// (set) Token: 0x06002121 RID: 8481 RVA: 0x000789B1 File Offset: 0x00076BB1
		[DataSourceProperty]
		public string SpendText
		{
			get
			{
				return this._spendText;
			}
			set
			{
				if (value != this._spendText)
				{
					this._spendText = value;
					base.OnPropertyChangedWithValue<string>(value, "SpendText");
				}
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06002122 RID: 8482 RVA: 0x000789D4 File Offset: 0x00076BD4
		// (set) Token: 0x06002123 RID: 8483 RVA: 0x000789DC File Offset: 0x00076BDC
		[DataSourceProperty]
		public string GainText
		{
			get
			{
				return this._gainText;
			}
			set
			{
				if (value != this._gainText)
				{
					this._gainText = value;
					base.OnPropertyChangedWithValue<string>(value, "GainText");
				}
			}
		}

		// Token: 0x04000F5E RID: 3934
		private readonly Action<ArmyManagementBoostEventVM> _onExecuteEvent;

		// Token: 0x04000F5F RID: 3935
		private int _amountToPay;

		// Token: 0x04000F60 RID: 3936
		private int _amountOfCohesionToGain;

		// Token: 0x04000F61 RID: 3937
		private int _currencyType;

		// Token: 0x04000F62 RID: 3938
		private string _spendText;

		// Token: 0x04000F63 RID: 3939
		private string _gainText;

		// Token: 0x04000F64 RID: 3940
		private bool _isEnabled;

		// Token: 0x020002EB RID: 747
		public enum BoostCurrency
		{
			// Token: 0x04001402 RID: 5122
			Gold,
			// Token: 0x04001403 RID: 5123
			Influence
		}
	}
}
