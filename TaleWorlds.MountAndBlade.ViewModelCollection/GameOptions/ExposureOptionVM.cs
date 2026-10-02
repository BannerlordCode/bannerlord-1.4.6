using System;
using TaleWorlds.Engine.Options;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x0200006C RID: 108
	public class ExposureOptionVM : ViewModel
	{
		// Token: 0x06000864 RID: 2148 RVA: 0x0001CD99 File Offset: 0x0001AF99
		public ExposureOptionVM(Action<bool> onClose = null)
		{
			this._onClose = onClose;
			this.InitialValue = 0f;
			this.Value = this.InitialValue;
			this.RefreshValues();
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0001CDC8 File Offset: 0x0001AFC8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TitleText = Module.CurrentModule.GlobalTextManager.FindText("str_exposure_option_title", null).ToString();
			TextObject textObject = Module.CurrentModule.GlobalTextManager.FindText("str_exposure_option_explainer", null);
			textObject.SetTextVariable("newline", "\n");
			this.ExplanationText = textObject.ToString();
			this.CancelText = new TextObject("{=3CpNUnVl}Cancel", null).ToString();
			this.AcceptText = new TextObject("{=Y94H6XnK}Accept", null).ToString();
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001CE5A File Offset: 0x0001B05A
		public void ExecuteConfirm()
		{
			this.InitialValue = this.Value;
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.ExposureCompensation, this.Value);
			Action<bool> onClose = this._onClose;
			if (onClose != null)
			{
				onClose(true);
			}
			this.Visible = false;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001CE8E File Offset: 0x0001B08E
		public void ExecuteCancel()
		{
			this.Value = this.InitialValue;
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.ExposureCompensation, this.InitialValue);
			this.Visible = false;
			Action<bool> onClose = this._onClose;
			if (onClose == null)
			{
				return;
			}
			onClose(false);
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0001CEC1 File Offset: 0x0001B0C1
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x0001CEC9 File Offset: 0x0001B0C9
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

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0001CEEC File Offset: 0x0001B0EC
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
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

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x0001CF17 File Offset: 0x0001B117
		// (set) Token: 0x0600086D RID: 2157 RVA: 0x0001CF1F File Offset: 0x0001B11F
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

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x0001CF42 File Offset: 0x0001B142
		// (set) Token: 0x0600086F RID: 2159 RVA: 0x0001CF4A File Offset: 0x0001B14A
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

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x0001CF6D File Offset: 0x0001B16D
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x0001CF75 File Offset: 0x0001B175
		public float Value
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
					NativeOptions.SetConfig(NativeOptions.NativeOptionsType.ExposureCompensation, this.Value);
				}
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0001CFA0 File Offset: 0x0001B1A0
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x0001CFA8 File Offset: 0x0001B1A8
		public float InitialValue
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

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0001CFC6 File Offset: 0x0001B1C6
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x0001CFCE File Offset: 0x0001B1CE
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
						this.Value = NativeOptions.GetConfig(NativeOptions.NativeOptionsType.ExposureCompensation);
					}
				}
			}
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001CFFD File Offset: 0x0001B1FD
		public void SetCancelInputKey(HotKey hotkey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001D00C File Offset: 0x0001B20C
		public void SetConfirmInputKey(HotKey hotkey)
		{
			this.ConfirmInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0001D01B File Offset: 0x0001B21B
		// (set) Token: 0x06000879 RID: 2169 RVA: 0x0001D023 File Offset: 0x0001B223
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

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0001D041 File Offset: 0x0001B241
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x0001D049 File Offset: 0x0001B249
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

		// Token: 0x040003BC RID: 956
		private readonly Action<bool> _onClose;

		// Token: 0x040003BD RID: 957
		private string _titleText;

		// Token: 0x040003BE RID: 958
		private string _explanationText;

		// Token: 0x040003BF RID: 959
		private string _cancelText;

		// Token: 0x040003C0 RID: 960
		private string _acceptText;

		// Token: 0x040003C1 RID: 961
		private float _initialValue;

		// Token: 0x040003C2 RID: 962
		private float _value;

		// Token: 0x040003C3 RID: 963
		private bool _visible;

		// Token: 0x040003C4 RID: 964
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x040003C5 RID: 965
		private InputKeyItemVM _confirmInputKey;
	}
}
