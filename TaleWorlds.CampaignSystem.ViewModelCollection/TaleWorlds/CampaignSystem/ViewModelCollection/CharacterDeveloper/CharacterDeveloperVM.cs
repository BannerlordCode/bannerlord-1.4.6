using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper
{
	// Token: 0x02000143 RID: 323
	public class CharacterDeveloperVM : ViewModel
	{
		// Token: 0x06001EA6 RID: 7846 RVA: 0x00071468 File Offset: 0x0006F668
		public CharacterDeveloperVM(Action closeCharacterDeveloper)
		{
			this._closeCharacterDeveloper = closeCharacterDeveloper;
			this.TutorialNotification = new ElementNotificationVM();
			this._viewDataTracker = Campaign.Current.GetCampaignBehavior<IViewDataTracker>();
			this._heroList = new List<CharacterDeveloperHeroItemVM>();
			this.HeroList = new ReadOnlyCollection<CharacterDeveloperHeroItemVM>(this._heroList);
			foreach (Hero hero in this.GetApplicableHeroes())
			{
				if (hero == null)
				{
					Debug.FailedAssert("Trying to use null hero for character developer", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\CharacterDeveloper\\CharacterDeveloperVM.cs", ".ctor", 40);
				}
				else if (hero.HeroDeveloper == null)
				{
					Debug.FailedAssert("Hero does not have hero developer", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\CharacterDeveloper\\CharacterDeveloperVM.cs", ".ctor", 46);
				}
				else if (hero == Hero.MainHero)
				{
					this._heroList.Insert(0, new CharacterDeveloperHeroItemVM(hero, new Action(this.OnPerkSelection)));
				}
				else
				{
					this._heroList.Add(new CharacterDeveloperHeroItemVM(hero, new Action(this.OnPerkSelection)));
				}
			}
			this._heroIndex = 0;
			this.CharacterList = new SelectorVM<SelectorItemVM>(new List<string>(), this._heroIndex, new Action<SelectorVM<SelectorItemVM>>(this.OnCharacterSelection));
			this.RefreshCharacterSelector();
			this.IsPlayerAccompanied = this._heroList.Count > 1;
			this.SetCurrentHero(this._heroList[this._heroIndex]);
			this._viewDataTracker.ClearCharacterNotification();
			this.UnopenedPerksNumForCurrentCharacter = this.CurrentCharacter.GetNumberOfUnselectedPerks();
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			this.RefreshValues();
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x00071620 File Offset: 0x0006F820
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.DoneLbl = GameTexts.FindText("str_done", null).ToString();
			this.ResetLbl = GameTexts.FindText("str_reset", null).ToString();
			this.CancelLbl = GameTexts.FindText("str_cancel", null).ToString();
			this.SkillsText = GameTexts.FindText("str_skills", null).ToString();
			this.AddFocusText = GameTexts.FindText("str_add_focus", null).ToString();
			this.UnspentCharacterPointsText = GameTexts.FindText("str_character_unspent_character_points", null).ToString();
			this.TraitsText = new TextObject("{=FYJC7cDD}Trait(s)", null).ToString();
			this.PartyRoleText = new TextObject("{=9FJi2SaE}Party Role", null).ToString();
			this.ResetHint = new HintViewModel(GameTexts.FindText("str_reset", null), null);
			this.SkillFocusText = GameTexts.FindText("str_character_skill_focus", null).ToString();
			this.FocusVisualHint = new HintViewModel(new TextObject("{=GwA9oUBC}Your skill focus determines the rate your skill increases with practice", null), null);
			GameTexts.SetVariable("FOCUS_PER_LEVEL", Campaign.Current.Models.CharacterDevelopmentModel.FocusPointsPerLevel);
			GameTexts.SetVariable("ATTRIBUTE_EVERY_LEVEL", Campaign.Current.Models.CharacterDevelopmentModel.LevelsPerAttributePoint);
			this.UnspentCharacterPointsHint = new HintViewModel(GameTexts.FindText("str_character_points_how_to_get", null), null);
			this.UnspentAttributePointsHint = new HintViewModel(GameTexts.FindText("str_attribute_points_how_to_get", null), null);
			this.LevelHint = new HintViewModel(GameTexts.FindText("str_level_tag", null), null);
			this.UnopenedPerksHint = new HintViewModel(new TextObject("{=jmLg6HQh}Number of available perk unlocks.", null), null);
			this.SetPreviousCharacterHint();
			this.SetNextCharacterHint();
			this.CharacterList.RefreshValues();
			this.CurrentCharacter.RefreshValues();
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000717E1 File Offset: 0x0006F9E1
		private void SetPreviousCharacterHint()
		{
			this.PreviousCharacterHint = new BasicTooltipViewModel(delegate
			{
				GameTexts.SetVariable("HOTKEY", this.GetPreviousCharacterKeyText());
				GameTexts.SetVariable("TEXT", GameTexts.FindText("str_inventory_prev_char", null));
				return GameTexts.FindText("str_hotkey_with_hint", null).ToString();
			});
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000717FA File Offset: 0x0006F9FA
		private void SetNextCharacterHint()
		{
			this.NextCharacterHint = new BasicTooltipViewModel(delegate
			{
				GameTexts.SetVariable("HOTKEY", this.GetNextCharacterKeyText());
				GameTexts.SetVariable("TEXT", GameTexts.FindText("str_inventory_next_char", null));
				return GameTexts.FindText("str_hotkey_with_hint", null).ToString();
			});
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x00071814 File Offset: 0x0006FA14
		public void SelectHero(Hero hero)
		{
			for (int i = 0; i < this._heroList.Count; i++)
			{
				if (this._heroList[i].Hero == hero)
				{
					this._heroIndex = i;
					this.RefreshCharacterSelector();
					return;
				}
			}
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x0007185C File Offset: 0x0006FA5C
		private void OnCharacterSelection(SelectorVM<SelectorItemVM> newIndex)
		{
			if (newIndex.SelectedIndex >= 0 && newIndex.SelectedIndex < this._heroList.Count)
			{
				this._heroIndex = newIndex.SelectedIndex;
				this.SetCurrentHero(this._heroList[this._heroIndex]);
				this.UnopenedPerksNumForCurrentCharacter = this.CurrentCharacter.GetNumberOfUnselectedPerks();
				this.HasUnopenedPerksForCurrentCharacter = this._heroList[this._heroIndex].GetNumberOfUnselectedPerks() > 0;
			}
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x000718D8 File Offset: 0x0006FAD8
		private void OnPerkSelection()
		{
			this.RefreshCharacterSelector();
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x000718E0 File Offset: 0x0006FAE0
		private void RefreshCharacterSelector()
		{
			List<string> list = new List<string>();
			for (int i = 0; i < this._heroList.Count; i++)
			{
				list.Add(this._heroList[i].HeroNameText);
			}
			this.CharacterList.Refresh(list, this._heroIndex, new Action<SelectorVM<SelectorItemVM>>(this.OnCharacterSelection));
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x00071940 File Offset: 0x0006FB40
		public void ExecuteReset()
		{
			foreach (CharacterDeveloperHeroItemVM characterDeveloperHeroItemVM in this._heroList)
			{
				characterDeveloperHeroItemVM.ResetChanges(false);
			}
			this.RefreshCharacterSelector();
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x00071998 File Offset: 0x0006FB98
		public void ExecuteDone()
		{
			this.ApplyAllChanges();
			this._closeCharacterDeveloper();
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x000719AC File Offset: 0x0006FBAC
		public void ExecuteCancel()
		{
			foreach (CharacterDeveloperHeroItemVM characterDeveloperHeroItemVM in this._heroList)
			{
				characterDeveloperHeroItemVM.ResetChanges(true);
			}
			this._closeCharacterDeveloper();
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x00071A08 File Offset: 0x0006FC08
		private void SetCurrentHero(CharacterDeveloperHeroItemVM currentHero)
		{
			CharacterDeveloperVM.<>c__DisplayClass18_0 CS$<>8__locals1 = new CharacterDeveloperVM.<>c__DisplayClass18_0();
			CharacterDeveloperVM.<>c__DisplayClass18_0 CS$<>8__locals2 = CS$<>8__locals1;
			CharacterDeveloperHeroItemVM currentCharacter = this.CurrentCharacter;
			SkillObject skillObject;
			if (currentCharacter == null)
			{
				skillObject = null;
			}
			else
			{
				SkillVM skillVM = currentCharacter.Skills.FirstOrDefault<SkillVM>((SkillVM s) => s.IsInspected);
				skillObject = ((skillVM != null) ? skillVM.Skill : null);
			}
			CS$<>8__locals2.prevSkill = skillObject;
			this.CurrentCharacter = currentHero;
			if (CS$<>8__locals1.prevSkill != null)
			{
				CharacterDeveloperHeroItemVM currentCharacter2 = this.CurrentCharacter;
				if (currentCharacter2 == null)
				{
					return;
				}
				currentCharacter2.SetCurrentSkill(this.CurrentCharacter.Skills.FirstOrDefault<SkillVM>((SkillVM s) => s.Skill == CS$<>8__locals1.prevSkill));
			}
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x00071AA0 File Offset: 0x0006FCA0
		public void ApplyAllChanges()
		{
			foreach (CharacterDeveloperHeroItemVM characterDeveloperHeroItemVM in this._heroList)
			{
				characterDeveloperHeroItemVM.ApplyChanges();
			}
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00071AF0 File Offset: 0x0006FCF0
		public bool IsThereAnyChanges()
		{
			return this._heroList.Any<CharacterDeveloperHeroItemVM>((CharacterDeveloperHeroItemVM c) => c.IsThereAnyChanges());
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00071B1C File Offset: 0x0006FD1C
		private List<Hero> GetApplicableHeroes()
		{
			List<Hero> list = new List<Hero>();
			Func<Hero, bool> func = (Hero x) => x != null && x.HeroState != Hero.CharacterStates.Disabled && x.IsAlive && !x.IsChild;
			Clan playerClan = Clan.PlayerClan;
			IEnumerable<Hero> enumerable = ((playerClan != null) ? playerClan.Heroes : null);
			foreach (Hero hero in (enumerable ?? Enumerable.Empty<Hero>()))
			{
				if (func(hero))
				{
					list.Add(hero);
				}
			}
			Clan playerClan2 = Clan.PlayerClan;
			enumerable = ((playerClan2 != null) ? playerClan2.Companions : null);
			foreach (Hero hero2 in (enumerable ?? Enumerable.Empty<Hero>()))
			{
				if (func(hero2) && !list.Contains(hero2))
				{
					list.Add(hero2);
				}
			}
			return list;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00071C1C File Offset: 0x0006FE1C
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent obj)
		{
			if (obj.NewNotificationElementID != this._latestTutorialElementID)
			{
				if (this._latestTutorialElementID != null)
				{
					this.TutorialNotification.ElementID = string.Empty;
					if (this._isActivePerkHighlightsApplied)
					{
						this.SetAvailablePerksHighlightState(false);
						this._isActivePerkHighlightsApplied = false;
					}
				}
				this._latestTutorialElementID = obj.NewNotificationElementID;
				if (this._latestTutorialElementID != null)
				{
					this.TutorialNotification.ElementID = this._latestTutorialElementID;
					if (!this._isActivePerkHighlightsApplied && this._latestTutorialElementID == this._availablePerksHighlighId)
					{
						this.SetAvailablePerksHighlightState(true);
						this._isActivePerkHighlightsApplied = true;
						SkillVM skillVM = this.CurrentCharacter.Skills.FirstOrDefault<SkillVM>((SkillVM s) => s.NumOfUnopenedPerks > 0);
						if (skillVM == null)
						{
							return;
						}
						skillVM.ExecuteInspect();
					}
				}
			}
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00071CF4 File Offset: 0x0006FEF4
		private void SetAvailablePerksHighlightState(bool state)
		{
			foreach (SkillVM skillVM in this.CurrentCharacter.Skills)
			{
				foreach (PerkVM perkVM in skillVM.Perks)
				{
					if (state && perkVM.CurrentState == PerkVM.PerkStates.EarnedButNotSelected)
					{
						perkVM.IsTutorialHighlightEnabled = true;
					}
					else if (!state)
					{
						perkVM.IsTutorialHighlightEnabled = false;
					}
				}
			}
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x00071D94 File Offset: 0x0006FF94
		public override void OnFinalize()
		{
			base.OnFinalize();
			Game.Current.EventManager.UnregisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			this.CancelInputKey.OnFinalize();
			this.DoneInputKey.OnFinalize();
			this.PreviousCharacterInputKey.OnFinalize();
			this.NextCharacterInputKey.OnFinalize();
			this._heroList.ForEach(delegate(CharacterDeveloperHeroItemVM h)
			{
				h.OnFinalize();
			});
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x00071E18 File Offset: 0x00070018
		// (set) Token: 0x06001EB9 RID: 7865 RVA: 0x00071E20 File Offset: 0x00070020
		[DataSourceProperty]
		public string CurrentCharacterNameText
		{
			get
			{
				return this._currentCharacterNameText;
			}
			set
			{
				if (value != this._currentCharacterNameText)
				{
					this._currentCharacterNameText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentCharacterNameText");
				}
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x00071E43 File Offset: 0x00070043
		// (set) Token: 0x06001EBB RID: 7867 RVA: 0x00071E4C File Offset: 0x0007004C
		[DataSourceProperty]
		public CharacterDeveloperHeroItemVM CurrentCharacter
		{
			get
			{
				return this._currentCharacter;
			}
			set
			{
				if (value != this._currentCharacter)
				{
					if (this._currentCharacter != null)
					{
						if (this._currentCharacter.IsInspectingAnAttribute)
						{
							this._currentCharacter.ExecuteStopInspectingCurrentAttribute();
						}
						if (this._currentCharacter.PerkSelection.IsActive)
						{
							this._currentCharacter.PerkSelection.ExecuteDeactivate();
						}
					}
					this._currentCharacter = value;
					CharacterDeveloperHeroItemVM currentCharacter = this._currentCharacter;
					this.CurrentCharacterNameText = ((currentCharacter != null) ? currentCharacter.HeroNameText : null) ?? string.Empty;
					base.OnPropertyChangedWithValue<CharacterDeveloperHeroItemVM>(value, "CurrentCharacter");
				}
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06001EBC RID: 7868 RVA: 0x00071ED8 File Offset: 0x000700D8
		// (set) Token: 0x06001EBD RID: 7869 RVA: 0x00071EE0 File Offset: 0x000700E0
		[DataSourceProperty]
		public SelectorVM<SelectorItemVM> CharacterList
		{
			get
			{
				return this._characterList;
			}
			set
			{
				if (value != this._characterList)
				{
					this._characterList = value;
					base.OnPropertyChangedWithValue<SelectorVM<SelectorItemVM>>(value, "CharacterList");
				}
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x00071EFE File Offset: 0x000700FE
		// (set) Token: 0x06001EBF RID: 7871 RVA: 0x00071F06 File Offset: 0x00070106
		[DataSourceProperty]
		public HintViewModel FocusVisualHint
		{
			get
			{
				return this._focusVisualHint;
			}
			set
			{
				if (value != this._focusVisualHint)
				{
					this._focusVisualHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "FocusVisualHint");
				}
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x00071F24 File Offset: 0x00070124
		// (set) Token: 0x06001EC1 RID: 7873 RVA: 0x00071F2C File Offset: 0x0007012C
		[DataSourceProperty]
		public HintViewModel ResetHint
		{
			get
			{
				return this._resetHint;
			}
			set
			{
				if (value != this._resetHint)
				{
					this._resetHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ResetHint");
				}
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x00071F4A File Offset: 0x0007014A
		// (set) Token: 0x06001EC3 RID: 7875 RVA: 0x00071F52 File Offset: 0x00070152
		[DataSourceProperty]
		public ElementNotificationVM TutorialNotification
		{
			get
			{
				return this._tutorialNotification;
			}
			set
			{
				if (value != this._tutorialNotification)
				{
					this._tutorialNotification = value;
					base.OnPropertyChangedWithValue<ElementNotificationVM>(value, "TutorialNotification");
				}
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06001EC4 RID: 7876 RVA: 0x00071F70 File Offset: 0x00070170
		// (set) Token: 0x06001EC5 RID: 7877 RVA: 0x00071F78 File Offset: 0x00070178
		[DataSourceProperty]
		public bool IsPlayerAccompanied
		{
			get
			{
				return this._isPlayerAccompanied;
			}
			set
			{
				if (value != this._isPlayerAccompanied)
				{
					this._isPlayerAccompanied = value;
					base.OnPropertyChangedWithValue(value, "IsPlayerAccompanied");
				}
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x00071F96 File Offset: 0x00070196
		// (set) Token: 0x06001EC7 RID: 7879 RVA: 0x00071F9E File Offset: 0x0007019E
		[DataSourceProperty]
		public string UnspentCharacterPointsText
		{
			get
			{
				return this._unspentFocusPointsText;
			}
			set
			{
				if (value != this._unspentFocusPointsText)
				{
					this._unspentFocusPointsText = value;
					base.OnPropertyChangedWithValue<string>(value, "UnspentCharacterPointsText");
				}
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x00071FC1 File Offset: 0x000701C1
		// (set) Token: 0x06001EC9 RID: 7881 RVA: 0x00071FC9 File Offset: 0x000701C9
		[DataSourceProperty]
		public string TraitsText
		{
			get
			{
				return this._traitsText;
			}
			set
			{
				if (value != this._traitsText)
				{
					this._traitsText = value;
					base.OnPropertyChangedWithValue<string>(value, "TraitsText");
				}
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x00071FEC File Offset: 0x000701EC
		// (set) Token: 0x06001ECB RID: 7883 RVA: 0x00071FF4 File Offset: 0x000701F4
		[DataSourceProperty]
		public string PartyRoleText
		{
			get
			{
				return this._partyRoleText;
			}
			set
			{
				if (value != this._partyRoleText)
				{
					this._partyRoleText = value;
					base.OnPropertyChangedWithValue<string>(value, "PartyRoleText");
				}
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x00072017 File Offset: 0x00070217
		// (set) Token: 0x06001ECD RID: 7885 RVA: 0x0007201F File Offset: 0x0007021F
		[DataSourceProperty]
		public HintViewModel UnspentCharacterPointsHint
		{
			get
			{
				return this._unspentCharacterPointsHint;
			}
			set
			{
				if (value != this._unspentCharacterPointsHint)
				{
					this._unspentCharacterPointsHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "UnspentCharacterPointsHint");
				}
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x0007203D File Offset: 0x0007023D
		// (set) Token: 0x06001ECF RID: 7887 RVA: 0x00072045 File Offset: 0x00070245
		[DataSourceProperty]
		public HintViewModel UnspentAttributePointsHint
		{
			get
			{
				return this._unspentAttributePointsHint;
			}
			set
			{
				if (value != this._unspentAttributePointsHint)
				{
					this._unspentAttributePointsHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "UnspentAttributePointsHint");
				}
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x00072063 File Offset: 0x00070263
		// (set) Token: 0x06001ED1 RID: 7889 RVA: 0x0007206B File Offset: 0x0007026B
		[DataSourceProperty]
		public HintViewModel LevelHint
		{
			get
			{
				return this._levelHint;
			}
			set
			{
				if (value != this._levelHint)
				{
					this._levelHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "LevelHint");
				}
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x00072089 File Offset: 0x00070289
		// (set) Token: 0x06001ED3 RID: 7891 RVA: 0x00072091 File Offset: 0x00070291
		[DataSourceProperty]
		public HintViewModel UnopenedPerksHint
		{
			get
			{
				return this._unopenedPerksHint;
			}
			set
			{
				if (value != this._unopenedPerksHint)
				{
					this._unopenedPerksHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "UnopenedPerksHint");
				}
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x000720AF File Offset: 0x000702AF
		// (set) Token: 0x06001ED5 RID: 7893 RVA: 0x000720B7 File Offset: 0x000702B7
		[DataSourceProperty]
		public BasicTooltipViewModel PreviousCharacterHint
		{
			get
			{
				return this._previousCharacterHint;
			}
			set
			{
				if (value != this._previousCharacterHint)
				{
					this._previousCharacterHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "PreviousCharacterHint");
				}
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x000720D5 File Offset: 0x000702D5
		// (set) Token: 0x06001ED7 RID: 7895 RVA: 0x000720DD File Offset: 0x000702DD
		[DataSourceProperty]
		public BasicTooltipViewModel NextCharacterHint
		{
			get
			{
				return this._nextCharacterHint;
			}
			set
			{
				if (value != this._nextCharacterHint)
				{
					this._nextCharacterHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "NextCharacterHint");
				}
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x000720FB File Offset: 0x000702FB
		// (set) Token: 0x06001ED9 RID: 7897 RVA: 0x00072103 File Offset: 0x00070303
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

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x00072126 File Offset: 0x00070326
		// (set) Token: 0x06001EDB RID: 7899 RVA: 0x0007212E File Offset: 0x0007032E
		[DataSourceProperty]
		public string ResetLbl
		{
			get
			{
				return this._resetLbl;
			}
			set
			{
				if (value != this._resetLbl)
				{
					this._resetLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "ResetLbl");
				}
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x00072151 File Offset: 0x00070351
		// (set) Token: 0x06001EDD RID: 7901 RVA: 0x00072159 File Offset: 0x00070359
		[DataSourceProperty]
		public string CancelLbl
		{
			get
			{
				return this._cancelLbl;
			}
			set
			{
				if (value != this._cancelLbl)
				{
					this._cancelLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelLbl");
				}
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x0007217C File Offset: 0x0007037C
		// (set) Token: 0x06001EDF RID: 7903 RVA: 0x00072184 File Offset: 0x00070384
		[DataSourceProperty]
		public string SkillFocusText
		{
			get
			{
				return this._skillFocusText;
			}
			set
			{
				if (value != this._skillFocusText)
				{
					this._skillFocusText = value;
					base.OnPropertyChangedWithValue<string>(value, "SkillFocusText");
				}
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x000721A7 File Offset: 0x000703A7
		// (set) Token: 0x06001EE1 RID: 7905 RVA: 0x000721AF File Offset: 0x000703AF
		[DataSourceProperty]
		public string AddFocusText
		{
			get
			{
				return this._addFocusText;
			}
			set
			{
				if (value != this._addFocusText)
				{
					this._addFocusText = value;
					base.OnPropertyChangedWithValue<string>(value, "AddFocusText");
				}
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x000721D2 File Offset: 0x000703D2
		// (set) Token: 0x06001EE3 RID: 7907 RVA: 0x000721DA File Offset: 0x000703DA
		[DataSourceProperty]
		public string SkillsText
		{
			get
			{
				return this._skillsText;
			}
			set
			{
				if (value != this._skillsText)
				{
					this._skillsText = value;
					base.OnPropertyChangedWithValue<string>(value, "SkillsText");
				}
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x000721FD File Offset: 0x000703FD
		// (set) Token: 0x06001EE5 RID: 7909 RVA: 0x00072205 File Offset: 0x00070405
		[DataSourceProperty]
		public int UnopenedPerksNumForCurrentCharacter
		{
			get
			{
				return this._unopenedPerksNumForCurrentCharacter;
			}
			set
			{
				if (value != this._unopenedPerksNumForCurrentCharacter)
				{
					this._unopenedPerksNumForCurrentCharacter = value;
					base.OnPropertyChangedWithValue(value, "UnopenedPerksNumForCurrentCharacter");
				}
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x00072223 File Offset: 0x00070423
		// (set) Token: 0x06001EE7 RID: 7911 RVA: 0x0007222B File Offset: 0x0007042B
		[DataSourceProperty]
		public bool HasUnopenedPerksForCurrentCharacter
		{
			get
			{
				return this._hasUnopenedPerksForCurrentCharacter;
			}
			set
			{
				if (value != this._hasUnopenedPerksForCurrentCharacter)
				{
					this._hasUnopenedPerksForCurrentCharacter = value;
					base.OnPropertyChangedWithValue(value, "HasUnopenedPerksForCurrentCharacter");
				}
			}
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x00072249 File Offset: 0x00070449
		private TextObject GetPreviousCharacterKeyText()
		{
			if (this.PreviousCharacterInputKey == null || this._getKeyTextFromKeyId == null)
			{
				return TextObject.GetEmpty();
			}
			return this._getKeyTextFromKeyId(this.PreviousCharacterInputKey.KeyID);
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x00072277 File Offset: 0x00070477
		private TextObject GetNextCharacterKeyText()
		{
			if (this.NextCharacterInputKey == null || this._getKeyTextFromKeyId == null)
			{
				return TextObject.GetEmpty();
			}
			return this._getKeyTextFromKeyId(this.NextCharacterInputKey.KeyID);
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x000722A5 File Offset: 0x000704A5
		public void SetCancelInputKey(HotKey gameKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(gameKey, true);
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x000722B4 File Offset: 0x000704B4
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x000722C3 File Offset: 0x000704C3
		public void SetResetInputKey(HotKey hotKey)
		{
			this.ResetInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x000722D2 File Offset: 0x000704D2
		public void SetPreviousCharacterInputKey(HotKey hotKey)
		{
			this.PreviousCharacterInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
			this.SetPreviousCharacterHint();
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x000722E7 File Offset: 0x000704E7
		public void SetNextCharacterInputKey(HotKey hotKey)
		{
			this.NextCharacterInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
			this.SetNextCharacterHint();
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x000722FC File Offset: 0x000704FC
		public void SetGetKeyTextFromKeyIDFunc(Func<string, TextObject> getKeyTextFromKeyId)
		{
			this._getKeyTextFromKeyId = getKeyTextFromKeyId;
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x00072305 File Offset: 0x00070505
		// (set) Token: 0x06001EF1 RID: 7921 RVA: 0x0007230D File Offset: 0x0007050D
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

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x0007232B File Offset: 0x0007052B
		// (set) Token: 0x06001EF3 RID: 7923 RVA: 0x00072333 File Offset: 0x00070533
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

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x00072351 File Offset: 0x00070551
		// (set) Token: 0x06001EF5 RID: 7925 RVA: 0x00072359 File Offset: 0x00070559
		[DataSourceProperty]
		public InputKeyItemVM ResetInputKey
		{
			get
			{
				return this._resetInputKey;
			}
			set
			{
				if (value != this._resetInputKey)
				{
					this._resetInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ResetInputKey");
				}
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x00072377 File Offset: 0x00070577
		// (set) Token: 0x06001EF7 RID: 7927 RVA: 0x0007237F File Offset: 0x0007057F
		[DataSourceProperty]
		public InputKeyItemVM PreviousCharacterInputKey
		{
			get
			{
				return this._previousCharacterInputKey;
			}
			set
			{
				if (value != this._previousCharacterInputKey)
				{
					this._previousCharacterInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "PreviousCharacterInputKey");
				}
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x0007239D File Offset: 0x0007059D
		// (set) Token: 0x06001EF9 RID: 7929 RVA: 0x000723A5 File Offset: 0x000705A5
		[DataSourceProperty]
		public InputKeyItemVM NextCharacterInputKey
		{
			get
			{
				return this._nextCharacterInputKey;
			}
			set
			{
				if (value != this._nextCharacterInputKey)
				{
					this._nextCharacterInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "NextCharacterInputKey");
				}
			}
		}

		// Token: 0x04000E4E RID: 3662
		private readonly Action _closeCharacterDeveloper;

		// Token: 0x04000E4F RID: 3663
		private readonly List<CharacterDeveloperHeroItemVM> _heroList;

		// Token: 0x04000E50 RID: 3664
		private readonly IViewDataTracker _viewDataTracker;

		// Token: 0x04000E51 RID: 3665
		public readonly ReadOnlyCollection<CharacterDeveloperHeroItemVM> HeroList;

		// Token: 0x04000E52 RID: 3666
		private int _heroIndex;

		// Token: 0x04000E53 RID: 3667
		private string _latestTutorialElementID;

		// Token: 0x04000E54 RID: 3668
		private Func<string, TextObject> _getKeyTextFromKeyId;

		// Token: 0x04000E55 RID: 3669
		private bool _isActivePerkHighlightsApplied;

		// Token: 0x04000E56 RID: 3670
		private readonly string _availablePerksHighlighId = "AvailablePerks";

		// Token: 0x04000E57 RID: 3671
		private string _skillsText;

		// Token: 0x04000E58 RID: 3672
		private string _doneLbl;

		// Token: 0x04000E59 RID: 3673
		private string _resetLbl;

		// Token: 0x04000E5A RID: 3674
		private string _cancelLbl;

		// Token: 0x04000E5B RID: 3675
		private string _unspentFocusPointsText;

		// Token: 0x04000E5C RID: 3676
		private string _traitsText;

		// Token: 0x04000E5D RID: 3677
		private string _partyRoleText;

		// Token: 0x04000E5E RID: 3678
		private HintViewModel _unspentCharacterPointsHint;

		// Token: 0x04000E5F RID: 3679
		private HintViewModel _unspentAttributePointsHint;

		// Token: 0x04000E60 RID: 3680
		private HintViewModel _levelHint;

		// Token: 0x04000E61 RID: 3681
		private HintViewModel _unopenedPerksHint;

		// Token: 0x04000E62 RID: 3682
		private BasicTooltipViewModel _previousCharacterHint;

		// Token: 0x04000E63 RID: 3683
		private BasicTooltipViewModel _nextCharacterHint;

		// Token: 0x04000E64 RID: 3684
		private string _addFocusText;

		// Token: 0x04000E65 RID: 3685
		private bool _isPlayerAccompanied;

		// Token: 0x04000E66 RID: 3686
		private string _skillFocusText;

		// Token: 0x04000E67 RID: 3687
		private ElementNotificationVM _tutorialNotification;

		// Token: 0x04000E68 RID: 3688
		private HintViewModel _resetHint;

		// Token: 0x04000E69 RID: 3689
		private HintViewModel _focusVisualHint;

		// Token: 0x04000E6A RID: 3690
		private CharacterDeveloperHeroItemVM _currentCharacter;

		// Token: 0x04000E6B RID: 3691
		private string _currentCharacterNameText;

		// Token: 0x04000E6C RID: 3692
		private SelectorVM<SelectorItemVM> _characterList;

		// Token: 0x04000E6D RID: 3693
		private int _unopenedPerksNumForCurrentCharacter;

		// Token: 0x04000E6E RID: 3694
		private bool _hasUnopenedPerksForCurrentCharacter;

		// Token: 0x04000E6F RID: 3695
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000E70 RID: 3696
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000E71 RID: 3697
		private InputKeyItemVM _resetInputKey;

		// Token: 0x04000E72 RID: 3698
		private InputKeyItemVM _previousCharacterInputKey;

		// Token: 0x04000E73 RID: 3699
		private InputKeyItemVM _nextCharacterInputKey;
	}
}
