using System;
using TaleWorlds.Engine.Options;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x0200006B RID: 107
	public class BrightnessOptionVM : ViewModel
	{
		// Token: 0x06000843 RID: 2115 RVA: 0x0001C90D File Offset: 0x0001AB0D
		public BrightnessOptionVM(Action<bool> onClose = null)
		{
			this._onClose = onClose;
			this.RefreshOptionValues();
			this.RefreshValues();
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001C928 File Offset: 0x0001AB28
		private void RefreshOptionValues()
		{
			this.InitialValue = 50;
			this.InitialValue1 = NativeOptions.GetConfig(NativeOptions.NativeOptionsType.BrightnessMax);
			this.InitialValue2 = NativeOptions.GetConfig(NativeOptions.NativeOptionsType.BrightnessMin);
			if (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.BrightnessCalibrated) < 2f)
			{
				this.Value1 = 0;
				this.Value2 = 0;
				return;
			}
			this.Value1 = MathF.Round((this.InitialValue1 - 1f) / 0.003f) - 2;
			this.Value2 = MathF.Round(this.InitialValue2 / 0.003f) + 2;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001C9AC File Offset: 0x0001ABAC
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TitleText = Module.CurrentModule.GlobalTextManager.FindText("str_brightness_option_title", null).ToString();
			TextObject textObject = Module.CurrentModule.GlobalTextManager.FindText("str_brightness_option_explainer", null);
			textObject.SetTextVariable("newline", "\n");
			this.ExplanationText = textObject.ToString();
			this.CancelText = new TextObject("{=3CpNUnVl}Cancel", null).ToString();
			this.AcceptText = new TextObject("{=Y94H6XnK}Accept", null).ToString();
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001CA40 File Offset: 0x0001AC40
		public void ExecuteConfirm()
		{
			this.InitialValue = this.Value;
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.Brightness, (float)this.Value);
			float num = (float)(this.Value1 + 2) * 0.003f + 1f;
			float num2 = (float)(this.Value2 - 2) * 0.003f;
			this.InitialValue1 = num;
			this.InitialValue2 = num2;
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.BrightnessMax, num);
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.BrightnessMin, num2);
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.BrightnessCalibrated, 4f);
			Action<bool> onClose = this._onClose;
			if (onClose != null)
			{
				onClose(true);
			}
			this.Visible = false;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001CAD0 File Offset: 0x0001ACD0
		public void ExecuteCancel()
		{
			this.Value = this.InitialValue;
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.Brightness, (float)this.InitialValue);
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.BrightnessMax, this.InitialValue1);
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.BrightnessMin, this.InitialValue2);
			this.Visible = false;
			Action<bool> onClose = this._onClose;
			if (onClose == null)
			{
				return;
			}
			onClose(false);
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x0001CB29 File Offset: 0x0001AD29
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x0001CB31 File Offset: 0x0001AD31
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

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0001CB54 File Offset: 0x0001AD54
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x0001CB5C File Offset: 0x0001AD5C
		[DataSourceProperty]
		public string ExplanationText
		{
			get
			{
				return this._explanationText;
			}
			set
			{
				if (value != this._explanationText)
				{
					this._explanationText = value;
					base.OnPropertyChangedWithValue<string>(value, "ExplanationText");
				}
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0001CB7F File Offset: 0x0001AD7F
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x0001CB87 File Offset: 0x0001AD87
		[DataSourceProperty]
		public string CancelText
		{
			get
			{
				return this._cancelText;
			}
			set
			{
				if (value != this._cancelText)
				{
					this._cancelText = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelText");
				}
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0001CBAA File Offset: 0x0001ADAA
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x0001CBB2 File Offset: 0x0001ADB2
		[DataSourceProperty]
		public string AcceptText
		{
			get
			{
				return this._acceptText;
			}
			set
			{
				if (value != this._acceptText)
				{
					this._acceptText = value;
					base.OnPropertyChangedWithValue<string>(value, "AcceptText");
				}
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0001CBD5 File Offset: 0x0001ADD5
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x0001CBDD File Offset: 0x0001ADDD
		public int Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (this._value != value)
				{
					this._value = value;
					base.OnPropertyChangedWithValue(value, "Value");
				}
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x0001CBFB File Offset: 0x0001ADFB
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x0001CC03 File Offset: 0x0001AE03
		public int InitialValue
		{
			get
			{
				return this._initialValue;
			}
			set
			{
				if (this._initialValue != value)
				{
					this._initialValue = value;
					base.OnPropertyChangedWithValue(value, "InitialValue");
				}
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0001CC21 File Offset: 0x0001AE21
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x0001CC29 File Offset: 0x0001AE29
		public float InitialValue1
		{
			get
			{
				return this._initialValue1;
			}
			set
			{
				if (this._initialValue1 != value)
				{
					this._initialValue1 = value;
					base.OnPropertyChangedWithValue(value, "InitialValue1");
				}
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0001CC47 File Offset: 0x0001AE47
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x0001CC4F File Offset: 0x0001AE4F
		public float InitialValue2
		{
			get
			{
				return this._initialValue2;
			}
			set
			{
				if (this._initialValue2 != value)
				{
					this._initialValue2 = value;
					base.OnPropertyChangedWithValue(value, "InitialValue2");
				}
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0001CC6D File Offset: 0x0001AE6D
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x0001CC78 File Offset: 0x0001AE78
		public int Value1
		{
			get
			{
				return this._value1;
			}
			set
			{
				if (this._value1 != value)
				{
					float num = (float)(value + 2) * 0.003f + 1f;
					NativeOptions.SetConfig(NativeOptions.NativeOptionsType.BrightnessMax, num);
					this._value1 = value;
					base.OnPropertyChangedWithValue(value, "Value1");
				}
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0001CCBA File Offset: 0x0001AEBA
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x0001CCC4 File Offset: 0x0001AEC4
		public int Value2
		{
			get
			{
				return this._value2;
			}
			set
			{
				if (this._value2 != value)
				{
					float num = (float)(value - 2) * 0.003f;
					NativeOptions.SetConfig(NativeOptions.NativeOptionsType.BrightnessMin, num);
					this._value2 = value;
					base.OnPropertyChangedWithValue(value, "Value2");
				}
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x0001CD00 File Offset: 0x0001AF00
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x0001CD08 File Offset: 0x0001AF08
		public bool Visible
		{
			get
			{
				return this._visible;
			}
			set
			{
				if (this._visible != value)
				{
					this._visible = value;
					base.OnPropertyChangedWithValue(value, "Visible");
					if (value)
					{
						this.RefreshOptionValues();
					}
				}
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001CD2F File Offset: 0x0001AF2F
		public void SetCancelInputKey(HotKey hotkey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001CD3E File Offset: 0x0001AF3E
		public void SetConfirmInputKey(HotKey hotkey)
		{
			this.ConfirmInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0001CD4D File Offset: 0x0001AF4D
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x0001CD55 File Offset: 0x0001AF55
		[DataSourceProperty]
		public InputKeyItemVM CancelInputKey
		{
			get
			{
				return this._cancelInputKey;
			}
			set
			{
				if (value != this._cancelInputKey)
				{
					this._cancelInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelInputKey");
				}
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0001CD73 File Offset: 0x0001AF73
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x0001CD7B File Offset: 0x0001AF7B
		[DataSourceProperty]
		public InputKeyItemVM ConfirmInputKey
		{
			get
			{
				return this._confirmInputKey;
			}
			set
			{
				if (value != this._confirmInputKey)
				{
					this._confirmInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ConfirmInputKey");
				}
			}
		}

		// Token: 0x040003AE RID: 942
		private readonly Action<bool> _onClose;

		// Token: 0x040003AF RID: 943
		private string _titleText;

		// Token: 0x040003B0 RID: 944
		private string _explanationText;

		// Token: 0x040003B1 RID: 945
		private string _cancelText;

		// Token: 0x040003B2 RID: 946
		private string _acceptText;

		// Token: 0x040003B3 RID: 947
		private int _initialValue;

		// Token: 0x040003B4 RID: 948
		private float _initialValue1;

		// Token: 0x040003B5 RID: 949
		private float _initialValue2;

		// Token: 0x040003B6 RID: 950
		private int _value;

		// Token: 0x040003B7 RID: 951
		private int _value1;

		// Token: 0x040003B8 RID: 952
		private int _value2;

		// Token: 0x040003B9 RID: 953
		private bool _visible;

		// Token: 0x040003BA RID: 954
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x040003BB RID: 955
		private InputKeyItemVM _confirmInputKey;
	}
}
