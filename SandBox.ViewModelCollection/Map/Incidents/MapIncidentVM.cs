using System;
using System.Collections.Generic;
using SandBox.ViewModelCollection.Input;
using TaleWorlds.CampaignSystem.Incidents;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.Map.Incidents
{
	// Token: 0x0200004D RID: 77
	public class MapIncidentVM : ViewModel
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x000126C0 File Offset: 0x000108C0
		public MapIncidentVM(Incident incident, Action onClose)
		{
			this._incident = incident;
			this._onClose = onClose;
			this.IncidentType = incident.Type.ToString();
			this.ConfirmHint = new HintViewModel();
			this.Options = new MBBindingList<MapIncidentOptionVM>();
			this.PopulateOptions();
			this.RefreshValues();
			this.UpdateCanConfirm();
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00012724 File Offset: 0x00010924
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Title = this._incident.Title.ToString();
			this.Description = this._incident.Description.ToString();
			this.ConfirmText = new TextObject("{=WiNRdfsm}Done", null).ToString();
			this.Options.ApplyActionOnAllItems(delegate(MapIncidentOptionVM o)
			{
				o.RefreshValues();
			});
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000127A3 File Offset: 0x000109A3
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.Options.ApplyActionOnAllItems(delegate(MapIncidentOptionVM o)
			{
				o.OnFinalize();
			});
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000127D8 File Offset: 0x000109D8
		public void ExecuteConfirm()
		{
			if (this.SelectedOption != null)
			{
				int index = this.SelectedOption.Index;
				if (index >= 0 && index < this._incident.NumOfOptions)
				{
					using (List<TextObject>.Enumerator enumerator = this._incident.InvokeOption(index).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							TextObject textObject = enumerator.Current;
							MBInformationManager.AddQuickInformation(textObject, 0, null, null, "");
						}
						goto IL_0095;
					}
				}
				Debug.FailedAssert("Selected incident option is out of bounds. Action won't be invoked", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\SandBox.ViewModelCollection\\Map\\Incidents\\MapIncidentVM.cs", "ExecuteConfirm", 69);
			}
			else
			{
				Debug.FailedAssert("An incident option must be selected before confirm", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\SandBox.ViewModelCollection\\Map\\Incidents\\MapIncidentVM.cs", "ExecuteConfirm", 74);
			}
			IL_0095:
			this._onClose();
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00012898 File Offset: 0x00010A98
		private void PopulateOptions()
		{
			this.Options.Clear();
			for (int i = 0; i < this._incident.NumOfOptions; i++)
			{
				TextObject optionText = this._incident.GetOptionText(i);
				List<TextObject> optionHint = this._incident.GetOptionHint(i);
				MapIncidentOptionVM mapIncidentOptionVM = new MapIncidentOptionVM(optionText, optionHint, i, new Action<MapIncidentOptionVM>(this.OnOptionSelected), new Action<MapIncidentOptionVM>(this.OnOptionFocused));
				this.Options.Add(mapIncidentOptionVM);
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001290B File Offset: 0x00010B0B
		private void OnOptionSelected(MapIncidentOptionVM option)
		{
			this.SelectedOption = option;
			this.UpdateActiveHint();
			this.UpdateCanConfirm();
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00012920 File Offset: 0x00010B20
		private void OnOptionFocused(MapIncidentOptionVM option)
		{
			this.FocusedOption = option;
			this.UpdateActiveHint();
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00012930 File Offset: 0x00010B30
		private void UpdateActiveHint()
		{
			if (this.FocusedOption != null)
			{
				this.ActiveHint = this.FocusedOption.Hint;
				return;
			}
			if (this.SelectedOption != null)
			{
				this.ActiveHint = this.SelectedOption.Hint;
				return;
			}
			this.ActiveHint = string.Empty;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001297C File Offset: 0x00010B7C
		private void UpdateCanConfirm()
		{
			if (this.SelectedOption == null)
			{
				this.CanConfirm = false;
				this.ConfirmHint.HintText = new TextObject("{=R3Zn7x07}You must select an option", null);
				return;
			}
			this.CanConfirm = true;
			this.ConfirmHint.HintText = TextObject.GetEmpty();
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x000129BB File Offset: 0x00010BBB
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x000129C3 File Offset: 0x00010BC3
		[DataSourceProperty]
		public bool CanConfirm
		{
			get
			{
				return this._canConfirm;
			}
			set
			{
				if (value != this._canConfirm)
				{
					this._canConfirm = value;
					base.OnPropertyChangedWithValue(value, "CanConfirm");
				}
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x000129E1 File Offset: 0x00010BE1
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x000129E9 File Offset: 0x00010BE9
		[DataSourceProperty]
		public bool HasFocusedOption
		{
			get
			{
				return this._hasFocusedOption;
			}
			set
			{
				if (value != this._hasFocusedOption)
				{
					this._hasFocusedOption = value;
					base.OnPropertyChangedWithValue(value, "HasFocusedOption");
				}
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00012A07 File Offset: 0x00010C07
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x00012A0F File Offset: 0x00010C0F
		[DataSourceProperty]
		public bool HasSelectedOption
		{
			get
			{
				return this._hasSelectedOption;
			}
			set
			{
				if (value != this._hasSelectedOption)
				{
					this._hasSelectedOption = value;
					base.OnPropertyChangedWithValue(value, "HasSelectedOption");
				}
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00012A2D File Offset: 0x00010C2D
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x00012A35 File Offset: 0x00010C35
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

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00012A58 File Offset: 0x00010C58
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00012A60 File Offset: 0x00010C60
		[DataSourceProperty]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (value != this._description)
				{
					this._description = value;
					base.OnPropertyChangedWithValue<string>(value, "Description");
				}
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00012A83 File Offset: 0x00010C83
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x00012A8B File Offset: 0x00010C8B
		[DataSourceProperty]
		public string ConfirmText
		{
			get
			{
				return this._confirmText;
			}
			set
			{
				if (value != this._confirmText)
				{
					this._confirmText = value;
					base.OnPropertyChangedWithValue<string>(value, "ConfirmText");
				}
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00012AAE File Offset: 0x00010CAE
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x00012AB6 File Offset: 0x00010CB6
		[DataSourceProperty]
		public string IncidentType
		{
			get
			{
				return this._incidentType;
			}
			set
			{
				if (value != this._incidentType)
				{
					this._incidentType = value;
					base.OnPropertyChangedWithValue<string>(value, "IncidentType");
				}
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00012AD9 File Offset: 0x00010CD9
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x00012AE1 File Offset: 0x00010CE1
		[DataSourceProperty]
		public string ActiveHint
		{
			get
			{
				return this._activeHint;
			}
			set
			{
				if (value != this._activeHint)
				{
					this._activeHint = value;
					base.OnPropertyChangedWithValue<string>(value, "ActiveHint");
				}
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00012B04 File Offset: 0x00010D04
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x00012B0C File Offset: 0x00010D0C
		[DataSourceProperty]
		public HintViewModel ConfirmHint
		{
			get
			{
				return this._confirmHint;
			}
			set
			{
				if (value != this._confirmHint)
				{
					this._confirmHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ConfirmHint");
				}
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00012B2A File Offset: 0x00010D2A
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00012B34 File Offset: 0x00010D34
		[DataSourceProperty]
		public MapIncidentOptionVM FocusedOption
		{
			get
			{
				return this._focusedOption;
			}
			set
			{
				if (value != this._focusedOption)
				{
					if (this._focusedOption != null)
					{
						this._focusedOption.IsFocused = false;
					}
					this._focusedOption = value;
					base.OnPropertyChangedWithValue<MapIncidentOptionVM>(value, "FocusedOption");
					if (this._focusedOption != null)
					{
						this._focusedOption.IsFocused = true;
					}
					this.HasFocusedOption = value != null;
				}
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00012B8F File Offset: 0x00010D8F
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x00012B98 File Offset: 0x00010D98
		[DataSourceProperty]
		public MapIncidentOptionVM SelectedOption
		{
			get
			{
				return this._selectedOption;
			}
			set
			{
				if (value != this._selectedOption)
				{
					if (this._selectedOption != null)
					{
						this._selectedOption.IsSelected = false;
					}
					this._selectedOption = value;
					base.OnPropertyChangedWithValue<MapIncidentOptionVM>(value, "SelectedOption");
					if (this._selectedOption != null)
					{
						this._selectedOption.IsSelected = true;
					}
					this.HasSelectedOption = value != null;
				}
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00012BF3 File Offset: 0x00010DF3
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x00012BFB File Offset: 0x00010DFB
		[DataSourceProperty]
		public MBBindingList<MapIncidentOptionVM> Options
		{
			get
			{
				return this._options;
			}
			set
			{
				if (value != this._options)
				{
					this._options = value;
					base.OnPropertyChangedWithValue<MBBindingList<MapIncidentOptionVM>>(value, "Options");
				}
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00012C19 File Offset: 0x00010E19
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00012C28 File Offset: 0x00010E28
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x00012C30 File Offset: 0x00010E30
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

		// Token: 0x0400025A RID: 602
		private readonly Incident _incident;

		// Token: 0x0400025B RID: 603
		private readonly Action _onClose;

		// Token: 0x0400025C RID: 604
		private bool _canConfirm;

		// Token: 0x0400025D RID: 605
		private bool _hasFocusedOption;

		// Token: 0x0400025E RID: 606
		private bool _hasSelectedOption;

		// Token: 0x0400025F RID: 607
		private string _title;

		// Token: 0x04000260 RID: 608
		private string _description;

		// Token: 0x04000261 RID: 609
		private string _confirmText;

		// Token: 0x04000262 RID: 610
		private string _incidentType;

		// Token: 0x04000263 RID: 611
		private string _activeHint;

		// Token: 0x04000264 RID: 612
		private HintViewModel _confirmHint;

		// Token: 0x04000265 RID: 613
		private MapIncidentOptionVM _focusedOption;

		// Token: 0x04000266 RID: 614
		private MapIncidentOptionVM _selectedOption;

		// Token: 0x04000267 RID: 615
		private MBBindingList<MapIncidentOptionVM> _options;

		// Token: 0x04000268 RID: 616
		private InputKeyItemVM _doneInputKey;
	}
}
