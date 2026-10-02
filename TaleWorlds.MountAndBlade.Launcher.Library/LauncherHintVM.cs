using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x0200000E RID: 14
	public class LauncherHintVM : ViewModel
	{
		// Token: 0x06000072 RID: 114 RVA: 0x0000360F File Offset: 0x0000180F
		public LauncherHintVM(string text)
		{
			this.Text = text;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000361E File Offset: 0x0000181E
		public void ExecuteBeginHint()
		{
			if (!string.IsNullOrEmpty(this.Text))
			{
				LauncherUI.AddHintInformation(this.Text);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003638 File Offset: 0x00001838
		public void ExecuteEndHint()
		{
			LauncherUI.HideHintInformation();
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000363F File Offset: 0x0000183F
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003647 File Offset: 0x00001847
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

		// Token: 0x04000040 RID: 64
		private string _text;
	}
}
