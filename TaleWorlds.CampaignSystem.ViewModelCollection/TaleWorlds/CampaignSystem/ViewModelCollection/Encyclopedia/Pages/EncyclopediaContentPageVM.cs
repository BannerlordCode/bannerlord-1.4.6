using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000D1 RID: 209
	public class EncyclopediaContentPageVM : EncyclopediaPageVM
	{
		// Token: 0x060013D0 RID: 5072 RVA: 0x0004FA8A File Offset: 0x0004DC8A
		public EncyclopediaContentPageVM(EncyclopediaPageArgs args)
			: base(args)
		{
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0004FAB5 File Offset: 0x0004DCB5
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PreviousButtonLabel = this._previousButtonLabelText.ToString();
			this.NextButtonLabel = this._nextButtonLabelText.ToString();
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0004FAE0 File Offset: 0x0004DCE0
		public void InitializeQuickNavigation(EncyclopediaListVM list)
		{
			if (list != null && list.Items != null)
			{
				List<EncyclopediaListItemVM> list2 = list.Items.Where<EncyclopediaListItemVM>((EncyclopediaListItemVM x) => !x.IsFiltered).ToList<EncyclopediaListItemVM>();
				int count = list2.Count;
				int num = list2.FindIndex((EncyclopediaListItemVM x) => x.Object == base.Obj);
				if (count > 1 && num > -1)
				{
					if (num > 0)
					{
						this._previousItem = list2[num - 1];
						this.PreviousButtonHint = new HintViewModel(new TextObject(this._previousItem.Name, null), null);
						this.IsPreviousButtonEnabled = true;
					}
					if (num < count - 1)
					{
						this._nextItem = list2[num + 1];
						this.NextButtonHint = new HintViewModel(new TextObject(this._nextItem.Name, null), null);
						this.IsNextButtonEnabled = true;
					}
				}
			}
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0004FBC0 File Offset: 0x0004DDC0
		public void ExecuteGoToNextItem()
		{
			if (this._nextItem != null)
			{
				this._nextItem.Execute();
				return;
			}
			Debug.FailedAssert("If the next button is enabled then next item should not be null.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Encyclopedia\\Pages\\EncyclopediaContentPageVM.cs", "ExecuteGoToNextItem", 66);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0004FBEC File Offset: 0x0004DDEC
		public void ExecuteGoToPreviousItem()
		{
			if (this._previousItem != null)
			{
				this._previousItem.Execute();
				return;
			}
			Debug.FailedAssert("If the previous button is enabled then previous item should not be null.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Encyclopedia\\Pages\\EncyclopediaContentPageVM.cs", "ExecuteGoToPreviousItem", 78);
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0004FC18 File Offset: 0x0004DE18
		// (set) Token: 0x060013D6 RID: 5078 RVA: 0x0004FC20 File Offset: 0x0004DE20
		[DataSourceProperty]
		public bool IsPreviousButtonEnabled
		{
			get
			{
				return this._isPreviousButtonEnabled;
			}
			set
			{
				if (value != this._isPreviousButtonEnabled)
				{
					this._isPreviousButtonEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsPreviousButtonEnabled");
				}
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0004FC3E File Offset: 0x0004DE3E
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x0004FC46 File Offset: 0x0004DE46
		[DataSourceProperty]
		public bool IsNextButtonEnabled
		{
			get
			{
				return this._isNextButtonEnabled;
			}
			set
			{
				if (value != this._isNextButtonEnabled)
				{
					this._isNextButtonEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsNextButtonEnabled");
				}
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0004FC64 File Offset: 0x0004DE64
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x0004FC6C File Offset: 0x0004DE6C
		[DataSourceProperty]
		public string PreviousButtonLabel
		{
			get
			{
				return this._previousButtonLabel;
			}
			set
			{
				if (value != this._previousButtonLabel)
				{
					this._previousButtonLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "PreviousButtonLabel");
				}
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x0004FC8F File Offset: 0x0004DE8F
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x0004FC97 File Offset: 0x0004DE97
		[DataSourceProperty]
		public string NextButtonLabel
		{
			get
			{
				return this._nextButtonLabel;
			}
			set
			{
				if (value != this._nextButtonLabel)
				{
					this._nextButtonLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "NextButtonLabel");
				}
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0004FCBA File Offset: 0x0004DEBA
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x0004FCC2 File Offset: 0x0004DEC2
		[DataSourceProperty]
		public HintViewModel PreviousButtonHint
		{
			get
			{
				return this._previousButtonHint;
			}
			set
			{
				if (value != this._previousButtonHint)
				{
					this._previousButtonHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "PreviousButtonHint");
				}
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0004FCE0 File Offset: 0x0004DEE0
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x0004FCE8 File Offset: 0x0004DEE8
		[DataSourceProperty]
		public HintViewModel NextButtonHint
		{
			get
			{
				return this._nextButtonHint;
			}
			set
			{
				if (value != this._nextButtonHint)
				{
					this._nextButtonHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "NextButtonHint");
				}
			}
		}

		// Token: 0x04000911 RID: 2321
		private EncyclopediaListItemVM _previousItem;

		// Token: 0x04000912 RID: 2322
		private EncyclopediaListItemVM _nextItem;

		// Token: 0x04000913 RID: 2323
		private TextObject _previousButtonLabelText = new TextObject("{=zlcMGAbn}Previous Page", null);

		// Token: 0x04000914 RID: 2324
		private TextObject _nextButtonLabelText = new TextObject("{=QFfMd5q3}Next Page", null);

		// Token: 0x04000915 RID: 2325
		private bool _isPreviousButtonEnabled;

		// Token: 0x04000916 RID: 2326
		private bool _isNextButtonEnabled;

		// Token: 0x04000917 RID: 2327
		private string _previousButtonLabel;

		// Token: 0x04000918 RID: 2328
		private string _nextButtonLabel;

		// Token: 0x04000919 RID: 2329
		private HintViewModel _previousButtonHint;

		// Token: 0x0400091A RID: 2330
		private HintViewModel _nextButtonHint;
	}
}
