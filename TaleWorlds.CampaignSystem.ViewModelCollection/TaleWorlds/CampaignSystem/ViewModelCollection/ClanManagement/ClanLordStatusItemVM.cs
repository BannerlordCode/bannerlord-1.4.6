using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x02000127 RID: 295
	public class ClanLordStatusItemVM : ViewModel
	{
		// Token: 0x06001AF4 RID: 6900 RVA: 0x00064F98 File Offset: 0x00063198
		public ClanLordStatusItemVM(ClanLordStatusItemVM.LordStatus status, TextObject hintText)
		{
			this.Type = (int)status;
			this.Hint = new HintViewModel(hintText, null);
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x00064FBB File Offset: 0x000631BB
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x00064FC3 File Offset: 0x000631C3
		[DataSourceProperty]
		public int Type
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
					base.OnPropertyChangedWithValue(value, "Type");
				}
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x00064FE1 File Offset: 0x000631E1
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x00064FE9 File Offset: 0x000631E9
		[DataSourceProperty]
		public HintViewModel Hint
		{
			get
			{
				return this._hint;
			}
			set
			{
				if (value != this._hint)
				{
					this._hint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x04000C92 RID: 3218
		private int _type = -1;

		// Token: 0x04000C93 RID: 3219
		private HintViewModel _hint;

		// Token: 0x02000287 RID: 647
		public enum LordStatus
		{
			// Token: 0x040012E8 RID: 4840
			Dead,
			// Token: 0x040012E9 RID: 4841
			Married,
			// Token: 0x040012EA RID: 4842
			Pregnant,
			// Token: 0x040012EB RID: 4843
			InBattle,
			// Token: 0x040012EC RID: 4844
			InSiege,
			// Token: 0x040012ED RID: 4845
			Child,
			// Token: 0x040012EE RID: 4846
			Prisoner,
			// Token: 0x040012EF RID: 4847
			Sick
		}
	}
}
