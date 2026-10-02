using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Education
{
	// Token: 0x020000F4 RID: 244
	public class EducationReviewVM : ViewModel
	{
		// Token: 0x06001620 RID: 5664 RVA: 0x00056D0C File Offset: 0x00054F0C
		public EducationReviewVM(int pageCount)
		{
			this._pageCount = pageCount;
			this.ReviewList = new MBBindingList<EducationReviewItemVM>();
			for (int i = 0; i < this._pageCount - 1; i++)
			{
				this.ReviewList.Add(new EducationReviewItemVM());
			}
			this.RefreshValues();
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00056D7C File Offset: 0x00054F7C
		public override void RefreshValues()
		{
			for (int i = 0; i < this.ReviewList.Count; i++)
			{
				this._educationPageTitle.SetTextVariable("NUMBER", i + 1);
				this.ReviewList[i].Title = this._educationPageTitle.ToString();
			}
			this.StageCompleteText = this._stageCompleteTextObject.ToString();
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00056DE0 File Offset: 0x00054FE0
		public void SetGainForStage(int pageIndex, string gainText)
		{
			if (pageIndex >= 0 && pageIndex < this._pageCount)
			{
				this.ReviewList[pageIndex].UpdateWith(gainText);
			}
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x00056E01 File Offset: 0x00055001
		public void SetCurrentPage(int currentPageIndex)
		{
			this.IsEnabled = currentPageIndex == this._pageCount - 1;
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x00056E14 File Offset: 0x00055014
		// (set) Token: 0x06001625 RID: 5669 RVA: 0x00056E1C File Offset: 0x0005501C
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

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x00056E3A File Offset: 0x0005503A
		// (set) Token: 0x06001627 RID: 5671 RVA: 0x00056E42 File Offset: 0x00055042
		[DataSourceProperty]
		public string StageCompleteText
		{
			get
			{
				return this._stageCompleteText;
			}
			set
			{
				if (value != this._stageCompleteText)
				{
					this._stageCompleteText = value;
					base.OnPropertyChangedWithValue<string>(value, "StageCompleteText");
				}
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x00056E65 File Offset: 0x00055065
		// (set) Token: 0x06001629 RID: 5673 RVA: 0x00056E6D File Offset: 0x0005506D
		[DataSourceProperty]
		public MBBindingList<EducationReviewItemVM> ReviewList
		{
			get
			{
				return this._reviewList;
			}
			set
			{
				if (value != this._reviewList)
				{
					this._reviewList = value;
					base.OnPropertyChangedWithValue<MBBindingList<EducationReviewItemVM>>(value, "ReviewList");
				}
			}
		}

		// Token: 0x04000A10 RID: 2576
		private readonly int _pageCount;

		// Token: 0x04000A11 RID: 2577
		private readonly TextObject _educationPageTitle = new TextObject("{=m1Yynagz}Page {NUMBER}", null);

		// Token: 0x04000A12 RID: 2578
		private readonly TextObject _stageCompleteTextObject = new TextObject("{=flxDkoMh}Stage Complete", null);

		// Token: 0x04000A13 RID: 2579
		private MBBindingList<EducationReviewItemVM> _reviewList;

		// Token: 0x04000A14 RID: 2580
		private bool _isEnabled;

		// Token: 0x04000A15 RID: 2581
		private string _stageCompleteText;
	}
}
