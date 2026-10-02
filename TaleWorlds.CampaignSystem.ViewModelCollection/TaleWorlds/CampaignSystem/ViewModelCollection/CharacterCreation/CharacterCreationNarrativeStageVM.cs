using System;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation
{
	// Token: 0x02000153 RID: 339
	public class CharacterCreationNarrativeStageVM : CharacterCreationStageBaseVM
	{
		// Token: 0x06002006 RID: 8198 RVA: 0x00075760 File Offset: 0x00073960
		public CharacterCreationNarrativeStageVM(CharacterCreationManager characterCreationManagerMenu, Action affirmativeAction, TextObject affirmativeActionText, Action negativeAction, TextObject negativeActionText, Action onMenuChanged)
			: base(characterCreationManagerMenu, affirmativeAction, affirmativeActionText, negativeAction, negativeActionText)
		{
			this._onMenuChanged = onMenuChanged;
			this.SelectionList = new MBBindingList<CharacterCreationOptionVM>();
			StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, null, false);
			this.GainedPropertiesController = new CharacterCreationGainedPropertiesVM(this.CharacterCreationManager);
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x000757B0 File Offset: 0x000739B0
		public void RefreshMenu()
		{
			this.SelectionList.Clear();
			foreach (NarrativeMenuOption narrativeMenuOption in this.CharacterCreationManager.GetSuitableNarrativeMenuOptions())
			{
				CharacterCreationOptionVM characterCreationOptionVM = new CharacterCreationOptionVM(new Action<CharacterCreationOptionVM>(this.OnOptionSelected), narrativeMenuOption);
				this.SelectionList.Add(characterCreationOptionVM);
			}
			NarrativeMenuOption narrativeMenuOption2;
			if (this.CharacterCreationManager.SelectedOptions.TryGetValue(this.CharacterCreationManager.CurrentMenu, out narrativeMenuOption2))
			{
				for (int i = 0; i < this.SelectionList.Count; i++)
				{
					if (this.SelectionList[i].Option == narrativeMenuOption2)
					{
						this.SelectionList[i].ExecuteSelect();
					}
				}
			}
			base.Title = this.CharacterCreationManager.CurrentMenu.Title.ToString();
			base.Description = this.CharacterCreationManager.CurrentMenu.Description.ToString();
			GameTexts.SetVariable("SELECTION", base.Title);
			base.SelectionText = GameTexts.FindText("str_char_creation_generic_selection", null).ToString();
			base.CanAdvance = this.CanAdvanceToNextStage();
			Action onMenuChanged = this._onMenuChanged;
			if (onMenuChanged == null)
			{
				return;
			}
			onMenuChanged();
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x00075900 File Offset: 0x00073B00
		public void OnOptionSelected(CharacterCreationOptionVM option)
		{
			if (this.SelectedOption != null)
			{
				this.SelectedOption.IsSelected = false;
			}
			this.SelectedOption = option;
			if (this.SelectedOption != null)
			{
				this.SelectedOption.IsSelected = true;
				this.CharacterCreationManager.OnNarrativeMenuOptionSelected(this._selectedOption.Option);
				this.SelectedOption.RefreshValues();
			}
			Action onOptionSelection = this.OnOptionSelection;
			if (onOptionSelection != null)
			{
				onOptionSelection();
			}
			base.CanAdvance = this.CanAdvanceToNextStage();
			this.GainedPropertiesController.UpdateValues();
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x00075985 File Offset: 0x00073B85
		public override void OnNextStage()
		{
			if (this.CharacterCreationManager.TrySwitchToNextMenu())
			{
				this.RefreshMenu();
				return;
			}
			this._affirmativeAction();
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x000759A6 File Offset: 0x00073BA6
		public override void OnPreviousStage()
		{
			if (this.CharacterCreationManager.TrySwitchToPreviousMenu())
			{
				this.RefreshMenu();
				return;
			}
			this._negativeAction();
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x000759C7 File Offset: 0x00073BC7
		public override bool CanAdvanceToNextStage()
		{
			if (this.SelectionList.Count != 0)
			{
				return this.SelectionList.Any<CharacterCreationOptionVM>((CharacterCreationOptionVM s) => s.IsSelected);
			}
			return true;
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x00075A02 File Offset: 0x00073C02
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM cancelInputKey = this.CancelInputKey;
			if (cancelInputKey != null)
			{
				cancelInputKey.OnFinalize();
			}
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey == null)
			{
				return;
			}
			doneInputKey.OnFinalize();
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x00075A2B File Offset: 0x00073C2B
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x00075A3A File Offset: 0x00073C3A
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x0600200F RID: 8207 RVA: 0x00075A49 File Offset: 0x00073C49
		// (set) Token: 0x06002010 RID: 8208 RVA: 0x00075A51 File Offset: 0x00073C51
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

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002011 RID: 8209 RVA: 0x00075A6F File Offset: 0x00073C6F
		// (set) Token: 0x06002012 RID: 8210 RVA: 0x00075A77 File Offset: 0x00073C77
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

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002013 RID: 8211 RVA: 0x00075A95 File Offset: 0x00073C95
		// (set) Token: 0x06002014 RID: 8212 RVA: 0x00075A9D File Offset: 0x00073C9D
		[DataSourceProperty]
		public CharacterCreationGainedPropertiesVM GainedPropertiesController
		{
			get
			{
				return this._gainedPropertiesController;
			}
			set
			{
				if (value != this._gainedPropertiesController)
				{
					this._gainedPropertiesController = value;
					base.OnPropertyChangedWithValue<CharacterCreationGainedPropertiesVM>(value, "GainedPropertiesController");
				}
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x00075ABB File Offset: 0x00073CBB
		// (set) Token: 0x06002016 RID: 8214 RVA: 0x00075AC3 File Offset: 0x00073CC3
		[DataSourceProperty]
		public CharacterCreationOptionVM SelectedOption
		{
			get
			{
				return this._selectedOption;
			}
			set
			{
				if (value != this._selectedOption)
				{
					this._selectedOption = value;
					base.OnPropertyChangedWithValue<CharacterCreationOptionVM>(value, "SelectedOption");
					base.AnyItemSelected = this.SelectedOption != null;
				}
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x00075AF0 File Offset: 0x00073CF0
		// (set) Token: 0x06002018 RID: 8216 RVA: 0x00075AF8 File Offset: 0x00073CF8
		[DataSourceProperty]
		public MBBindingList<CharacterCreationOptionVM> SelectionList
		{
			get
			{
				return this._selectionList;
			}
			set
			{
				if (value != this._selectionList)
				{
					this._selectionList = value;
					base.OnPropertyChangedWithValue<MBBindingList<CharacterCreationOptionVM>>(value, "SelectionList");
				}
			}
		}

		// Token: 0x04000EEA RID: 3818
		public Action OnOptionSelection;

		// Token: 0x04000EEB RID: 3819
		private readonly Action _onMenuChanged;

		// Token: 0x04000EEC RID: 3820
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000EED RID: 3821
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000EEE RID: 3822
		private CharacterCreationGainedPropertiesVM _gainedPropertiesController;

		// Token: 0x04000EEF RID: 3823
		private CharacterCreationOptionVM _selectedOption;

		// Token: 0x04000EF0 RID: 3824
		private MBBindingList<CharacterCreationOptionVM> _selectionList;
	}
}
