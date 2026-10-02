using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x0200011C RID: 284
	public class ClanCardSelectionPopupItemPropertyVM : ViewModel
	{
		// Token: 0x06001A33 RID: 6707 RVA: 0x0006326B File Offset: 0x0006146B
		public ClanCardSelectionPopupItemPropertyVM(in ClanCardSelectionItemPropertyInfo info)
		{
			this._titleText = info.Title;
			this._valueText = info.Value;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0006328C File Offset: 0x0006148C
		public override void RefreshValues()
		{
			base.RefreshValues();
			TextObject titleText = this._titleText;
			this.Title = ((titleText != null) ? titleText.ToString() : null) ?? string.Empty;
			TextObject valueText = this._valueText;
			this.Value = ((valueText != null) ? valueText.ToString() : null) ?? string.Empty;
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x000632E1 File Offset: 0x000614E1
		// (set) Token: 0x06001A36 RID: 6710 RVA: 0x000632E9 File Offset: 0x000614E9
		[DataSourceProperty]
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				if (value != this._title)
				{
					this._title = value;
					base.OnPropertyChangedWithValue<string>(value, "Title");
				}
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x0006330C File Offset: 0x0006150C
		// (set) Token: 0x06001A38 RID: 6712 RVA: 0x00063314 File Offset: 0x00061514
		[DataSourceProperty]
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (value != this._value)
				{
					this._value = value;
					base.OnPropertyChangedWithValue<string>(value, "Value");
				}
			}
		}

		// Token: 0x04000C0C RID: 3084
		private readonly TextObject _titleText;

		// Token: 0x04000C0D RID: 3085
		private readonly TextObject _valueText;

		// Token: 0x04000C0E RID: 3086
		private string _title;

		// Token: 0x04000C0F RID: 3087
		private string _value;
	}
}
