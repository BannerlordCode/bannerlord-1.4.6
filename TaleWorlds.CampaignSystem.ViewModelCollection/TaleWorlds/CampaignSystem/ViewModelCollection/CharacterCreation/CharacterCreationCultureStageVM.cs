using System;
using System.Linq;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation
{
	// Token: 0x0200014D RID: 333
	public class CharacterCreationCultureStageVM : CharacterCreationStageBaseVM
	{
		// Token: 0x06001FB5 RID: 8117 RVA: 0x00074294 File Offset: 0x00072494
		public CharacterCreationCultureStageVM(CharacterCreationManager characterCreationManager, Action affirmativeAction, TextObject affirmativeActionText, Action negativeAction, TextObject negativeActionText, Action<CultureObject> onCultureSelected)
			: base(characterCreationManager, affirmativeAction, affirmativeActionText, negativeAction, negativeActionText)
		{
			this._onCultureSelected = onCultureSelected;
			CharacterCreationContent currentContent = (GameStateManager.Current.ActiveState as CharacterCreationState).CharacterCreationManager.CharacterCreationContent;
			this.Cultures = new MBBindingList<CharacterCreationCultureVM>();
			base.Title = GameTexts.FindText("str_culture", null).ToString();
			base.Description = new TextObject("{=fz2kQjFS}Choose your character's culture:", null).ToString();
			base.SelectionText = new TextObject("{=MaHMOzL2}Character Culture", null).ToString();
			foreach (CultureObject cultureObject in currentContent.GetCultures())
			{
				CharacterCreationCultureVM characterCreationCultureVM = new CharacterCreationCultureVM(cultureObject, new Action<CharacterCreationCultureVM>(this.OnCultureSelection));
				this.Cultures.Add(characterCreationCultureVM);
			}
			this.SortCultureList(this.Cultures);
			if (currentContent.SelectedCulture != null)
			{
				CharacterCreationCultureVM characterCreationCultureVM2 = this.Cultures.FirstOrDefault<CharacterCreationCultureVM>((CharacterCreationCultureVM c) => c.Culture == currentContent.SelectedCulture);
				if (characterCreationCultureVM2 != null)
				{
					this.OnCultureSelection(characterCreationCultureVM2);
				}
			}
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x000743C0 File Offset: 0x000725C0
		private void SortCultureList(MBBindingList<CharacterCreationCultureVM> listToWorkOn)
		{
			int num = listToWorkOn.IndexOf(listToWorkOn.Single<CharacterCreationCultureVM>((CharacterCreationCultureVM i) => i.CultureID.Contains("vlan")));
			this.Swap(listToWorkOn, num, 0);
			int num2 = listToWorkOn.IndexOf(listToWorkOn.Single<CharacterCreationCultureVM>((CharacterCreationCultureVM i) => i.CultureID.Contains("stur")));
			this.Swap(listToWorkOn, num2, 1);
			int num3 = listToWorkOn.IndexOf(listToWorkOn.Single<CharacterCreationCultureVM>((CharacterCreationCultureVM i) => i.CultureID.Contains("empi")));
			this.Swap(listToWorkOn, num3, 2);
			int num4 = listToWorkOn.IndexOf(listToWorkOn.Single<CharacterCreationCultureVM>((CharacterCreationCultureVM i) => i.CultureID.Contains("aser")));
			this.Swap(listToWorkOn, num4, 3);
			int num5 = listToWorkOn.IndexOf(listToWorkOn.Single<CharacterCreationCultureVM>((CharacterCreationCultureVM i) => i.CultureID.Contains("khuz")));
			this.Swap(listToWorkOn, num5, 4);
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x000744D8 File Offset: 0x000726D8
		public void OnCultureSelection(CharacterCreationCultureVM selectedCulture)
		{
			this.InitializePlayersFaceKeyAccordingToCultureSelection(selectedCulture);
			foreach (CharacterCreationCultureVM characterCreationCultureVM in this.Cultures.Where<CharacterCreationCultureVM>((CharacterCreationCultureVM c) => c.IsSelected))
			{
				characterCreationCultureVM.IsSelected = false;
			}
			selectedCulture.IsSelected = true;
			this.CurrentSelectedCulture = selectedCulture;
			base.AnyItemSelected = true;
			base.CanAdvance = this.CanAdvanceToNextStage();
			Action<CultureObject> onCultureSelected = this._onCultureSelected;
			if (onCultureSelected == null)
			{
				return;
			}
			onCultureSelected(selectedCulture.Culture);
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x00074588 File Offset: 0x00072788
		private void InitializePlayersFaceKeyAccordingToCultureSelection(CharacterCreationCultureVM selectedCulture)
		{
			if (selectedCulture.Culture.DefaultCharacterCreationBodyProperty != null)
			{
				CharacterObject.PlayerCharacter.UpdatePlayerCharacterBodyProperties(selectedCulture.Culture.DefaultCharacterCreationBodyProperty.BodyPropertyMax, CharacterObject.PlayerCharacter.Race, CharacterObject.PlayerCharacter.IsFemale);
				Hero.MainHero.Culture = selectedCulture.Culture;
			}
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x000745E0 File Offset: 0x000727E0
		private void Swap(MBBindingList<CharacterCreationCultureVM> listToWorkOn, int swapFromIndex, int swapToIndex)
		{
			if (swapFromIndex != swapToIndex)
			{
				CharacterCreationCultureVM characterCreationCultureVM = listToWorkOn[swapToIndex];
				listToWorkOn[swapToIndex] = listToWorkOn[swapFromIndex];
				listToWorkOn[swapFromIndex] = characterCreationCultureVM;
			}
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x00074610 File Offset: 0x00072810
		public override void OnNextStage()
		{
			if (this.CurrentSelectedCulture == null)
			{
				Debug.FailedAssert("Selected culture can't be null at this stage", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\CharacterCreation\\CharacterCreationCultureStageVM.cs", "OnNextStage", 111);
				return;
			}
			(GameStateManager.Current.ActiveState as CharacterCreationState).CharacterCreationManager.CharacterCreationContent.SetSelectedCulture(this.CurrentSelectedCulture.Culture, this.CharacterCreationManager);
			this._affirmativeAction();
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x00074676 File Offset: 0x00072876
		public override void OnPreviousStage()
		{
			this._negativeAction();
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x00074683 File Offset: 0x00072883
		public override bool CanAdvanceToNextStage()
		{
			return this.Cultures.Any<CharacterCreationCultureVM>((CharacterCreationCultureVM s) => s.IsSelected);
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x000746AF File Offset: 0x000728AF
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

		// Token: 0x06001FBE RID: 8126 RVA: 0x000746D8 File Offset: 0x000728D8
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x000746E7 File Offset: 0x000728E7
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x000746F6 File Offset: 0x000728F6
		// (set) Token: 0x06001FC1 RID: 8129 RVA: 0x000746FE File Offset: 0x000728FE
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

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06001FC2 RID: 8130 RVA: 0x0007471C File Offset: 0x0007291C
		// (set) Token: 0x06001FC3 RID: 8131 RVA: 0x00074724 File Offset: 0x00072924
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

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x00074742 File Offset: 0x00072942
		// (set) Token: 0x06001FC5 RID: 8133 RVA: 0x0007474A File Offset: 0x0007294A
		[DataSourceProperty]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChangedWithValue(value, "IsActive");
				}
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x00074768 File Offset: 0x00072968
		// (set) Token: 0x06001FC7 RID: 8135 RVA: 0x00074770 File Offset: 0x00072970
		[DataSourceProperty]
		public MBBindingList<CharacterCreationCultureVM> Cultures
		{
			get
			{
				return this._cultures;
			}
			set
			{
				if (value != this._cultures)
				{
					this._cultures = value;
					base.OnPropertyChangedWithValue<MBBindingList<CharacterCreationCultureVM>>(value, "Cultures");
				}
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x0007478E File Offset: 0x0007298E
		// (set) Token: 0x06001FC9 RID: 8137 RVA: 0x00074796 File Offset: 0x00072996
		[DataSourceProperty]
		public CharacterCreationCultureVM CurrentSelectedCulture
		{
			get
			{
				return this._currentSelectedCulture;
			}
			set
			{
				if (value != this._currentSelectedCulture)
				{
					this._currentSelectedCulture = value;
					base.OnPropertyChangedWithValue<CharacterCreationCultureVM>(value, "CurrentSelectedCulture");
				}
			}
		}

		// Token: 0x04000EC9 RID: 3785
		private Action<CultureObject> _onCultureSelected;

		// Token: 0x04000ECA RID: 3786
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000ECB RID: 3787
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000ECC RID: 3788
		private bool _isActive;

		// Token: 0x04000ECD RID: 3789
		private MBBindingList<CharacterCreationCultureVM> _cultures;

		// Token: 0x04000ECE RID: 3790
		private CharacterCreationCultureVM _currentSelectedCulture;
	}
}
