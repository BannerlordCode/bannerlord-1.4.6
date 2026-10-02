using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Inquiries
{
	// Token: 0x02000046 RID: 70
	public class MultiSelectionQueryPopUpVM : PopUpBaseVM
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x000165E4 File Offset: 0x000147E4
		public MultiSelectionQueryPopUpVM(Action closeQuery)
			: base(closeQuery)
		{
			this.InquiryElements = new MBBindingList<InquiryElementVM>();
			this.MaxSelectableOptionCount = 0;
			this.MinSelectableOptionCount = 0;
			this._selectedOptionCount = 0;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00016610 File Offset: 0x00014810
		public void SetData(MultiSelectionInquiryData data)
		{
			this._data = data;
			this.InquiryElements.Clear();
			foreach (InquiryElement inquiryElement in this._data.InquiryElements)
			{
				TextObject textObject = (string.IsNullOrEmpty(inquiryElement.Hint) ? TextObject.GetEmpty() : new TextObject("{=!}" + inquiryElement.Hint, null));
				InquiryElementVM inquiryElementVM = new InquiryElementVM(inquiryElement, textObject, new Action<InquiryElementVM, bool>(this.OnInquiryElementSelected));
				this.InquiryElements.Add(inquiryElementVM);
			}
			base.TitleText = this._data.TitleText;
			base.PopUpLabel = this._data.DescriptionText;
			this.MaxSelectableOptionCount = this._data.MaxSelectableOptionCount;
			this.MinSelectableOptionCount = this._data.MinSelectableOptionCount;
			base.ButtonOkLabel = this._data.AffirmativeText;
			base.ButtonCancelLabel = this._data.NegativeText;
			base.IsButtonOkShown = true;
			base.IsButtonCancelShown = this._data.IsExitShown;
			this.IsSearchAvailable = this._data.IsSeachAvailable;
			this.SearchPlaceholderText = new TextObject("{=tQOPRBFg}Search...", null).ToString();
			this.RefreshIsButtonOkEnabled();
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001676C File Offset: 0x0001496C
		private void OnInquiryElementSelected(InquiryElementVM elementVM, bool isSelected)
		{
			if (isSelected)
			{
				this._selectedOptionCount++;
				if (this.MaxSelectableOptionCount != 1)
				{
					goto IL_005C;
				}
				using (IEnumerator<InquiryElementVM> enumerator = this.InquiryElements.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						InquiryElementVM inquiryElementVM = enumerator.Current;
						if (inquiryElementVM != elementVM)
						{
							inquiryElementVM.IsSelected = false;
						}
					}
					goto IL_005C;
				}
			}
			this._selectedOptionCount--;
			IL_005C:
			this.RefreshIsButtonOkEnabled();
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000167EC File Offset: 0x000149EC
		public override void ExecuteAffirmativeAction()
		{
			if (this._data.AffirmativeAction != null)
			{
				List<InquiryElement> list = new List<InquiryElement>();
				foreach (InquiryElementVM inquiryElementVM in this.InquiryElements)
				{
					if (inquiryElementVM.IsSelected)
					{
						list.Add(inquiryElementVM.InquiryElement);
					}
				}
				this._data.AffirmativeAction(list);
			}
			base.CloseQuery();
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00016870 File Offset: 0x00014A70
		public override void ExecuteNegativeAction()
		{
			Action<List<InquiryElement>> negativeAction = this._data.NegativeAction;
			if (negativeAction != null)
			{
				negativeAction(new List<InquiryElement>());
			}
			base.CloseQuery();
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00016893 File Offset: 0x00014A93
		public override void OnClearData()
		{
			base.OnClearData();
			this._data = null;
			this.MaxSelectableOptionCount = 0;
			this.MinSelectableOptionCount = 0;
			this._selectedOptionCount = 0;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000168B7 File Offset: 0x00014AB7
		private void RefreshIsButtonOkEnabled()
		{
			base.IsButtonOkEnabled = (this.MaxSelectableOptionCount <= 0 || this._selectedOptionCount <= this.MaxSelectableOptionCount) && this._selectedOptionCount >= this.MinSelectableOptionCount;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000168EC File Offset: 0x00014AEC
		private void UpdateInquiryFilter(string searchText, bool isAppending)
		{
			string text = searchText.ToLower();
			for (int i = 0; i < this.InquiryElements.Count; i++)
			{
				InquiryElementVM inquiryElementVM = this.InquiryElements[i];
				if (!isAppending || !inquiryElementVM.IsFilteredOut)
				{
					inquiryElementVM.IsFilteredOut = !inquiryElementVM.Text.ToLower().Contains(text);
				}
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00016948 File Offset: 0x00014B48
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x00016950 File Offset: 0x00014B50
		[DataSourceProperty]
		public MBBindingList<InquiryElementVM> InquiryElements
		{
			get
			{
				return this._inquiryElements;
			}
			set
			{
				if (value != this._inquiryElements)
				{
					this._inquiryElements = value;
					base.OnPropertyChangedWithValue<MBBindingList<InquiryElementVM>>(value, "InquiryElements");
				}
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0001696E File Offset: 0x00014B6E
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x00016976 File Offset: 0x00014B76
		[DataSourceProperty]
		public int MaxSelectableOptionCount
		{
			get
			{
				return this._maxSelectableOptionCount;
			}
			set
			{
				if (value != this._maxSelectableOptionCount)
				{
					this._maxSelectableOptionCount = value;
					base.OnPropertyChangedWithValue(value, "MaxSelectableOptionCount");
				}
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00016994 File Offset: 0x00014B94
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x0001699C File Offset: 0x00014B9C
		[DataSourceProperty]
		public int MinSelectableOptionCount
		{
			get
			{
				return this._minSelectableOptionCount;
			}
			set
			{
				if (value != this._minSelectableOptionCount)
				{
					this._minSelectableOptionCount = value;
					base.OnPropertyChangedWithValue(value, "MinSelectableOptionCount");
				}
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x000169BA File Offset: 0x00014BBA
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x000169C2 File Offset: 0x00014BC2
		[DataSourceProperty]
		public bool IsSearchAvailable
		{
			get
			{
				return this._isSearchAvailable;
			}
			set
			{
				if (value != this._isSearchAvailable)
				{
					this._isSearchAvailable = value;
					base.OnPropertyChangedWithValue(value, "IsSearchAvailable");
				}
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x000169E0 File Offset: 0x00014BE0
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x000169E8 File Offset: 0x00014BE8
		[DataSourceProperty]
		public string SearchText
		{
			get
			{
				return this._searchText;
			}
			set
			{
				if (value != this._searchText)
				{
					bool flag = value.IndexOf(this._searchText ?? "") >= 0;
					this._searchText = value;
					base.OnPropertyChangedWithValue<string>(value, "SearchText");
					this.UpdateInquiryFilter(this._searchText, flag);
				}
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00016A3F File Offset: 0x00014C3F
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x00016A47 File Offset: 0x00014C47
		[DataSourceProperty]
		public string SearchPlaceholderText
		{
			get
			{
				return this._searchPlaceholderText;
			}
			set
			{
				if (value != this._searchPlaceholderText)
				{
					this._searchPlaceholderText = value;
					base.OnPropertyChangedWithValue<string>(value, "SearchPlaceholderText");
				}
			}
		}

		// Token: 0x040002AF RID: 687
		private MultiSelectionInquiryData _data;

		// Token: 0x040002B0 RID: 688
		private int _selectedOptionCount;

		// Token: 0x040002B1 RID: 689
		private MBBindingList<InquiryElementVM> _inquiryElements;

		// Token: 0x040002B2 RID: 690
		private int _maxSelectableOptionCount;

		// Token: 0x040002B3 RID: 691
		private int _minSelectableOptionCount;

		// Token: 0x040002B4 RID: 692
		private bool _isSearchAvailable;

		// Token: 0x040002B5 RID: 693
		private string _searchText;

		// Token: 0x040002B6 RID: 694
		private string _searchPlaceholderText;
	}
}
