using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x02000070 RID: 112
	public class MPOptionsVM : OptionsVM
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x0001D9E9 File Offset: 0x0001BBE9
		public MPOptionsVM(bool autoHandleClose, Action onChangeBrightnessRequest, Action onChangeExposureRequest, Action<KeyOptionVM> onKeybindRequest)
			: base(autoHandleClose, OptionsVM.OptionsMode.Multiplayer, onKeybindRequest, onChangeBrightnessRequest, onChangeExposureRequest)
		{
			this.RefreshValues();
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001DA1F File Offset: 0x0001BC1F
		public MPOptionsVM(Action onClose, Action<KeyOptionVM> onKeybindRequest)
			: base(OptionsVM.OptionsMode.Multiplayer, onClose, onKeybindRequest, null, null)
		{
			this.RefreshValues();
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001DA54 File Offset: 0x0001BC54
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ApplyText = new TextObject("{=BAaS5Dkc}Apply", null).ToString();
			this.RevertText = new TextObject("{=Npqlj5Ln}Revert Changes", null).ToString();
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001DA88 File Offset: 0x0001BC88
		public new void ExecuteCancel()
		{
			base.ExecuteCancel();
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001DA90 File Offset: 0x0001BC90
		public void ExecuteApply()
		{
			bool flag = base.IsOptionsChanged();
			base.OnDone();
			InformationManager.DisplayMessage(new InformationMessage(flag ? this._changesAppliedTextObject.ToString() : this._noChangesMadeTextObject.ToString()));
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001DAC2 File Offset: 0x0001BCC2
		public void ForceCancel()
		{
			base.HandleCancel(false);
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0001DACB File Offset: 0x0001BCCB
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x0001DAD3 File Offset: 0x0001BCD3
		[DataSourceProperty]
		public bool AreHotkeysEnabled
		{
			get
			{
				return this._areHotkeysEnabled;
			}
			set
			{
				if (value != this._areHotkeysEnabled)
				{
					this._areHotkeysEnabled = value;
					base.OnPropertyChangedWithValue(value, "AreHotkeysEnabled");
				}
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0001DAF1 File Offset: 0x0001BCF1
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x0001DAF9 File Offset: 0x0001BCF9
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

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0001DB17 File Offset: 0x0001BD17
		// (set) Token: 0x060008D1 RID: 2257 RVA: 0x0001DB1F File Offset: 0x0001BD1F
		[DataSourceProperty]
		public string ApplyText
		{
			get
			{
				return this._applyText;
			}
			set
			{
				if (value != this._applyText)
				{
					this._applyText = value;
					base.OnPropertyChangedWithValue<string>(value, "ApplyText");
				}
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0001DB42 File Offset: 0x0001BD42
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x0001DB4A File Offset: 0x0001BD4A
		[DataSourceProperty]
		public string RevertText
		{
			get
			{
				return this._revertText;
			}
			set
			{
				if (value != this._revertText)
				{
					this._revertText = value;
					base.OnPropertyChangedWithValue<string>(value, "RevertText");
				}
			}
		}

		// Token: 0x040003E4 RID: 996
		private TextObject _changesAppliedTextObject = new TextObject("{=SfsnlbyK}Changes applied.", null);

		// Token: 0x040003E5 RID: 997
		private TextObject _noChangesMadeTextObject = new TextObject("{=jS5rrX8M}There are no changes to apply.", null);

		// Token: 0x040003E6 RID: 998
		private bool _areHotkeysEnabled;

		// Token: 0x040003E7 RID: 999
		private bool _isEnabled;

		// Token: 0x040003E8 RID: 1000
		private string _applyText;

		// Token: 0x040003E9 RID: 1001
		private string _revertText;
	}
}
