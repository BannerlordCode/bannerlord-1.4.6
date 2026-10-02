using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x0200000D RID: 13
	public class LauncherInformationVM : ViewModel
	{
		// Token: 0x0600006B RID: 107 RVA: 0x0000357B File Offset: 0x0000177B
		public LauncherInformationVM()
		{
			LauncherUI.OnAddHintInformation += this.ExecuteEnableHint;
			LauncherUI.OnHideHintInformation += this.ExecuteDisableHint;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000035A5 File Offset: 0x000017A5
		private void ExecuteEnableHint(string text)
		{
			this.IsEnabled = true;
			this.Text = text;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000035B5 File Offset: 0x000017B5
		private void ExecuteDisableHint()
		{
			this.IsEnabled = false;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000035BE File Offset: 0x000017BE
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000035C6 File Offset: 0x000017C6
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000035E4 File Offset: 0x000017E4
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000035EC File Offset: 0x000017EC
		[DataSourceProperty]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (value != this._text)
				{
					this._text = value;
					base.OnPropertyChangedWithValue<string>(value, "Text");
				}
			}
		}

		// Token: 0x0400003E RID: 62
		private bool _isEnabled;

		// Token: 0x0400003F RID: 63
		private string _text;
	}
}
