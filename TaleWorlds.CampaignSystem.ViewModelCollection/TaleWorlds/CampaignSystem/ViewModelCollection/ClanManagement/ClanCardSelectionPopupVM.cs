using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x02000121 RID: 289
	public class ClanCardSelectionPopupVM : ViewModel
	{
		// Token: 0x06001A5B RID: 6747 RVA: 0x00063878 File Offset: 0x00061A78
		public ClanCardSelectionPopupVM()
		{
			this._titleText = TextObject.GetEmpty();
			this.Items = new MBBindingList<ClanCardSelectionPopupItemVM>();
			this.DisabledHint = new HintViewModel();
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x000638A4 File Offset: 0x00061AA4
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (!this._isMultiSelection)
			{
				ClanCardSelectionPopupItemVM lastSelectedItem = this._lastSelectedItem;
				string text;
				if (lastSelectedItem == null)
				{
					text = null;
				}
				else
				{
					TextObject actionResultText = lastSelectedItem.ActionResultText;
					text = ((actionResultText != null) ? actionResultText.ToString() : null);
				}
				this.ActionResult = text ?? string.Empty;
			}
			this.DoneLbl = GameTexts.FindText("str_done", null).ToString();
			TextObject titleText = this._titleText;
			this.Title = ((titleText != null) ? titleText.ToString() : null) ?? string.Empty;
			this.Items.ApplyActionOnAllItems(delegate(ClanCardSelectionPopupItemVM x)
			{
				x.RefreshValues();
			});
			this.RefreshHintText();
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00063954 File Offset: 0x00061B54
		private void RefreshHintText()
		{
			TextObject textObject = TextObject.GetEmpty();
			if (this._isMultiSelection)
			{
				if (this._maximumSelection > 0 && this._selectedItemCount > this._maximumSelection)
				{
					textObject = new TextObject("{=lIGdkJGm}You must choose less than {NUMBER} {?NUMBER>1}items{?}item{\\?}", null);
					textObject.SetTextVariable("NUMBER", this._maximumSelection);
				}
				else if (this._selectedItemCount < this._minimumSelection)
				{
					textObject = new TextObject("{=woD234nb}You must choose more than {NUMBER} {?NUMBER>1}items{?}item{\\?}", null);
					textObject.SetTextVariable("NUMBER", this._minimumSelection);
				}
			}
			else if (this._selectedItemCount != 1)
			{
				textObject = new TextObject("{=aYm5Ehv1}You must choose an item", null);
			}
			this.DisabledHint.HintText = textObject;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x000639F5 File Offset: 0x00061BF5
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey != null)
			{
				doneInputKey.OnFinalize();
			}
			InputKeyItemVM cancelInputKey = this.CancelInputKey;
			if (cancelInputKey == null)
			{
				return;
			}
			cancelInputKey.OnFinalize();
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00063A1E File Offset: 0x00061C1E
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00063A2D File Offset: 0x00061C2D
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00063A3C File Offset: 0x00061C3C
		public void Open(ClanCardSelectionInfo info)
		{
			this._isMultiSelection = info.IsMultiSelection;
			this._minimumSelection = info.MinimumSelection;
			this._maximumSelection = info.MaximumSelection;
			this._titleText = info.Title;
			this._onClosed = info.OnClosedAction;
			foreach (ClanCardSelectionItemInfo clanCardSelectionItemInfo in info.Items)
			{
				this.Items.Add(new ClanCardSelectionPopupItemVM(in clanCardSelectionItemInfo, new Action<ClanCardSelectionPopupItemVM>(this.OnItemSelected)));
			}
			this._selectedItemCount = 0;
			this.RefreshValues();
			this.IsVisible = true;
			this.UpdateIsDoneEnabled();
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00063AF8 File Offset: 0x00061CF8
		public void ExecuteCancel()
		{
			Action<List<object>, Action> onClosed = this._onClosed;
			if (onClosed != null)
			{
				onClosed(new List<object>(), null);
			}
			this.Close();
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00063B18 File Offset: 0x00061D18
		public void ExecuteDone()
		{
			List<object> selectedItems = new List<object>();
			this.Items.ApplyActionOnAllItems(delegate(ClanCardSelectionPopupItemVM x)
			{
				if (x.IsSelected)
				{
					selectedItems.Add(x.Identifier);
				}
			});
			Action<List<object>, Action> onClosed = this._onClosed;
			if (onClosed == null)
			{
				return;
			}
			onClosed(selectedItems, new Action(this.Close));
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00063B70 File Offset: 0x00061D70
		private void Close()
		{
			this.IsVisible = false;
			this._lastSelectedItem = null;
			this._titleText = TextObject.GetEmpty();
			this.ActionResult = string.Empty;
			this.Title = string.Empty;
			this._onClosed = null;
			this.Items.Clear();
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00063BC0 File Offset: 0x00061DC0
		private void OnItemSelected(ClanCardSelectionPopupItemVM item)
		{
			if (this._isMultiSelection)
			{
				item.IsSelected = !item.IsSelected;
				if (item.IsSelected)
				{
					this._selectedItemCount++;
				}
				else
				{
					this._selectedItemCount--;
				}
			}
			else if (item != this._lastSelectedItem)
			{
				if (this._lastSelectedItem != null)
				{
					this._lastSelectedItem.IsSelected = false;
				}
				item.IsSelected = true;
				TextObject actionResultText = item.ActionResultText;
				this.ActionResult = ((actionResultText != null) ? actionResultText.ToString() : null) ?? string.Empty;
				this._selectedItemCount = 1;
			}
			this._lastSelectedItem = item;
			this.UpdateIsDoneEnabled();
			this.RefreshHintText();
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00063C6C File Offset: 0x00061E6C
		private void UpdateIsDoneEnabled()
		{
			if (this._isMultiSelection)
			{
				this.IsDoneEnabled = this._selectedItemCount >= this._minimumSelection && (this._maximumSelection <= 0 || this._selectedItemCount <= this._maximumSelection);
				return;
			}
			this.IsDoneEnabled = this._selectedItemCount == 1;
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x00063CC5 File Offset: 0x00061EC5
		// (set) Token: 0x06001A68 RID: 6760 RVA: 0x00063CCD File Offset: 0x00061ECD
		[DataSourceProperty]
		public MBBindingList<ClanCardSelectionPopupItemVM> Items
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
					base.OnPropertyChangedWithValue<MBBindingList<ClanCardSelectionPopupItemVM>>(value, "Items");
				}
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00063CEB File Offset: 0x00061EEB
		// (set) Token: 0x06001A6A RID: 6762 RVA: 0x00063CF3 File Offset: 0x00061EF3
		[DataSourceProperty]
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x00063D11 File Offset: 0x00061F11
		// (set) Token: 0x06001A6C RID: 6764 RVA: 0x00063D19 File Offset: 0x00061F19
		[DataSourceProperty]
		public InputKeyItemVM CancelInputKey
		{
			get
			{
				return this._cancelInputKey;
			}
			set
			{
				if (value != this._cancelInputKey)
				{
					this._cancelInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelInputKey");
				}
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x00063D37 File Offset: 0x00061F37
		// (set) Token: 0x06001A6E RID: 6766 RVA: 0x00063D3F File Offset: 0x00061F3F
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

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x00063D62 File Offset: 0x00061F62
		// (set) Token: 0x06001A70 RID: 6768 RVA: 0x00063D6A File Offset: 0x00061F6A
		[DataSourceProperty]
		public string ActionResult
		{
			get
			{
				return this._actionResult;
			}
			set
			{
				if (value != this._actionResult)
				{
					this._actionResult = value;
					base.OnPropertyChangedWithValue<string>(value, "ActionResult");
				}
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x00063D8D File Offset: 0x00061F8D
		// (set) Token: 0x06001A72 RID: 6770 RVA: 0x00063D95 File Offset: 0x00061F95
		[DataSourceProperty]
		public string DoneLbl
		{
			get
			{
				return this._doneLbl;
			}
			set
			{
				if (value != this._doneLbl)
				{
					this._doneLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneLbl");
				}
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x00063DB8 File Offset: 0x00061FB8
		// (set) Token: 0x06001A74 RID: 6772 RVA: 0x00063DC0 File Offset: 0x00061FC0
		[DataSourceProperty]
		public bool IsVisible
		{
			get
			{
				return this._isVisible;
			}
			set
			{
				if (value != this._isVisible)
				{
					this._isVisible = value;
					base.OnPropertyChangedWithValue(value, "IsVisible");
				}
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x00063DDE File Offset: 0x00061FDE
		// (set) Token: 0x06001A76 RID: 6774 RVA: 0x00063DE6 File Offset: 0x00061FE6
		[DataSourceProperty]
		public bool IsDoneEnabled
		{
			get
			{
				return this._isDoneEnabled;
			}
			set
			{
				if (value != this._isDoneEnabled)
				{
					this._isDoneEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsDoneEnabled");
				}
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x00063E04 File Offset: 0x00062004
		// (set) Token: 0x06001A78 RID: 6776 RVA: 0x00063E0C File Offset: 0x0006200C
		[DataSourceProperty]
		public HintViewModel DisabledHint
		{
			get
			{
				return this._disabledHint;
			}
			set
			{
				if (value != this._disabledHint)
				{
					this._disabledHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DisabledHint");
				}
			}
		}

		// Token: 0x04000C39 RID: 3129
		private TextObject _titleText;

		// Token: 0x04000C3A RID: 3130
		private bool _isMultiSelection;

		// Token: 0x04000C3B RID: 3131
		private int _minimumSelection;

		// Token: 0x04000C3C RID: 3132
		private int _maximumSelection;

		// Token: 0x04000C3D RID: 3133
		private ClanCardSelectionPopupItemVM _lastSelectedItem;

		// Token: 0x04000C3E RID: 3134
		private int _selectedItemCount;

		// Token: 0x04000C3F RID: 3135
		private Action<List<object>, Action> _onClosed;

		// Token: 0x04000C40 RID: 3136
		private MBBindingList<ClanCardSelectionPopupItemVM> _items;

		// Token: 0x04000C41 RID: 3137
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000C42 RID: 3138
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000C43 RID: 3139
		private string _title;

		// Token: 0x04000C44 RID: 3140
		private string _actionResult;

		// Token: 0x04000C45 RID: 3141
		private string _doneLbl;

		// Token: 0x04000C46 RID: 3142
		private bool _isVisible;

		// Token: 0x04000C47 RID: 3143
		private bool _isDoneEnabled;

		// Token: 0x04000C48 RID: 3144
		private HintViewModel _disabledHint;
	}
}
