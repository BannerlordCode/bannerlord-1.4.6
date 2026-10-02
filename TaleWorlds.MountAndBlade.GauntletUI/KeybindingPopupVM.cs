using System;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x0200001C RID: 28
	public class KeybindingPopupVM : ViewModel
	{
		// Token: 0x0600010B RID: 267 RVA: 0x0000853D File Offset: 0x0000673D
		public KeybindingPopupVM(Action onCancel)
		{
			this._onCancel = onCancel;
			this.RefreshValues();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00008554 File Offset: 0x00006754
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PressKeyText = new TextObject("{=hvaDkG4w}Press any key.", null).ToString();
			TextObject textObject = new TextObject("{=5U8vXv4E}Press {KEY} to cancel", null);
			textObject.SetTextVariable("KEY", HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit").ToString());
			this.CancelText = textObject.ToString();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000085BA File Offset: 0x000067BA
		public void ExecuteCancel()
		{
			Action onCancel = this._onCancel;
			if (onCancel == null)
			{
				return;
			}
			onCancel();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000085CC File Offset: 0x000067CC
		// (set) Token: 0x0600010F RID: 271 RVA: 0x000085D4 File Offset: 0x000067D4
		[DataSourceProperty]
		public string PressKeyText
		{
			get
			{
				return this._pressKeyText;
			}
			set
			{
				if (this._pressKeyText != value)
				{
					this._pressKeyText = value;
					base.OnPropertyChangedWithValue<string>(value, "PressKeyText");
				}
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000085F7 File Offset: 0x000067F7
		// (set) Token: 0x06000111 RID: 273 RVA: 0x000085FF File Offset: 0x000067FF
		[DataSourceProperty]
		public string CancelText
		{
			get
			{
				return this._cancelText;
			}
			set
			{
				if (this._cancelText != value)
				{
					this._cancelText = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelText");
				}
			}
		}

		// Token: 0x040000AB RID: 171
		private readonly Action _onCancel;

		// Token: 0x040000AC RID: 172
		private string _pressKeyText;

		// Token: 0x040000AD RID: 173
		private string _cancelText;
	}
}
