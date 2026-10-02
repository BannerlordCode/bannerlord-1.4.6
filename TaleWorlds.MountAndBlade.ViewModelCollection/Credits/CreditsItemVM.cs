using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Credits
{
	// Token: 0x02000082 RID: 130
	public class CreditsItemVM : ViewModel
	{
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00026C60 File Offset: 0x00024E60
		// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x00026C68 File Offset: 0x00024E68
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

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00026C8B File Offset: 0x00024E8B
		// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x00026C93 File Offset: 0x00024E93
		[DataSourceProperty]
		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (value != this._type)
				{
					this._type = value;
					base.OnPropertyChangedWithValue<string>(value, "Type");
				}
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00026CB6 File Offset: 0x00024EB6
		// (set) Token: 0x06000AC9 RID: 2761 RVA: 0x00026CBE File Offset: 0x00024EBE
		[DataSourceProperty]
		public MBBindingList<CreditsItemVM> Items
		{
			get
			{
				return this._items;
			}
			set
			{
				if (value != this._items)
				{
					this._items = value;
					base.OnPropertyChangedWithValue<MBBindingList<CreditsItemVM>>(value, "Items");
				}
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00026CDC File Offset: 0x00024EDC
		public CreditsItemVM()
		{
			this._items = new MBBindingList<CreditsItemVM>();
			this.Type = "Entry";
			this.Text = "";
		}

		// Token: 0x040004EA RID: 1258
		private string _text;

		// Token: 0x040004EB RID: 1259
		private string _type;

		// Token: 0x040004EC RID: 1260
		private MBBindingList<CreditsItemVM> _items;
	}
}
