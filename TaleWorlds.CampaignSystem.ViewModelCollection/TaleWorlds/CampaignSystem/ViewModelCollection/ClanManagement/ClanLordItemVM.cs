using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x02000126 RID: 294
	public class ClanLordItemVM : ViewModel
	{
		// Token: 0x06001AB3 RID: 6835 RVA: 0x00064494 File Offset: 0x00062694
		public ClanLordItemVM(Hero hero, ITeleportationCampaignBehavior teleportationBehavior, Action<Hero> showHeroOnMap, Action<ClanLordItemVM> onCharacterSelect, Action onRecall, Action onTalk)
		{
			this._hero = hero;
			this._onCharacterSelect = onCharacterSelect;
			this._onRecall = onRecall;
			this._onTalk = onTalk;
			this._showHeroOnMap = showHeroOnMap;
			this._teleportationBehavior = teleportationBehavior;
			CharacterCode characterCode = CampaignUIHelper.GetCharacterCode(hero.CharacterObject, false);
			this.Visual = new CharacterImageIdentifierVM(characterCode);
			this.Skills = new MBBindingList<EncyclopediaSkillVM>();
			this.Traits = new MBBindingList<EncyclopediaTraitItemVM>();
			this.IsFamilyMember = Hero.MainHero.Clan.AliveLords.Contains(this._hero);
			this.Banner_9 = new BannerImageIdentifierVM(hero.ClanBanner, true);
			this.RefreshValues();
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00064580 File Offset: 0x00062780
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this._hero.Name.ToString();
			StringHelpers.SetCharacterProperties("NPC", this._hero.CharacterObject, null, false);
			this.CurrentActionText = ((this._hero != Hero.MainHero) ? CampaignUIHelper.GetHeroBehaviorText(this._hero, this._teleportationBehavior) : "");
			this.LocationText = this.CurrentActionText;
			this.PregnantHint = new HintViewModel(GameTexts.FindText("str_pregnant", null), null);
			this.UpdateProperties();
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00064615 File Offset: 0x00062815
		public void ExecuteLocationLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00064628 File Offset: 0x00062828
		public void UpdateProperties()
		{
			this.RelationToMainHeroText = "";
			this.GovernorOfText = "";
			this.Skills.Clear();
			this.Traits.Clear();
			this.IsMainHero = this._hero == Hero.MainHero;
			this.IsPregnant = this._hero.IsPregnant;
			List<SkillObject> list = TaleWorlds.CampaignSystem.Extensions.Skills.All.ToList<SkillObject>();
			list.Sort(CampaignUIHelper.SkillObjectComparerInstance);
			foreach (SkillObject skillObject in list)
			{
				this.Skills.Add(new EncyclopediaSkillVM(skillObject, this._hero.GetSkillValue(skillObject)));
			}
			foreach (TraitObject traitObject in CampaignUIHelper.GetHeroTraits())
			{
				if (this._hero.GetTraitLevel(traitObject) != 0)
				{
					this.Traits.Add(new EncyclopediaTraitItemVM(traitObject, this._hero));
				}
			}
			this.IsChild = FaceGen.GetMaturityTypeWithAge(this._hero.Age) <= BodyMeshMaturityType.Child;
			if (this._hero != Hero.MainHero)
			{
				this.RelationToMainHeroText = CampaignUIHelper.GetHeroRelationToHeroText(this._hero, Hero.MainHero, true).ToString();
			}
			if (this._hero.GovernorOf != null)
			{
				GameTexts.SetVariable("SETTLEMENT_NAME", this._hero.GovernorOf.Owner.Settlement.EncyclopediaLinkWithName);
				this.GovernorOfText = GameTexts.FindText("str_governor_of_label", null).ToString();
			}
			this.HeroModel = new HeroViewModel(CharacterViewModel.StanceTypes.None);
			this.HeroModel.FillFrom(this._hero, -1, false, false);
			this.Banner_9 = new BannerImageIdentifierVM(this._hero.ClanBanner, true);
			bool flag = MobileParty.MainParty.CurrentSettlement == null || MobileParty.MainParty.CurrentSettlement == this._hero.CurrentSettlement;
			this.CanShowLocationOfHero = this._hero.GetCampaignPosition().IsValid() && this._hero.PartyBelongedTo != MobileParty.MainParty && flag;
			this.ShowOnMapHint = new HintViewModel(this.CanShowLocationOfHero ? this._showLocationOfHeroOnMap : TextObject.GetEmpty(), null);
			TextObject empty = TextObject.GetEmpty();
			bool flag2 = this._hero.PartyBelongedTo == MobileParty.MainParty;
			this.IsTalkVisible = flag2 && !this.IsMainHero;
			this.IsTalkEnabled = this.IsTalkVisible && CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out empty);
			bool flag3;
			bool flag4;
			IMapPoint mapPoint;
			this.IsTeleporting = this._teleportationBehavior.GetTargetOfTeleportingHero(this._hero, out flag3, out flag4, out mapPoint);
			TextObject empty2 = TextObject.GetEmpty();
			this.IsRecallVisible = !this.IsMainHero && !flag2 && !this.IsTeleporting;
			this.IsRecallEnabled = this.IsRecallVisible && CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out empty2) && FactionHelper.IsMainClanMemberAvailableForRecall(this._hero, MobileParty.MainParty, out empty2);
			this.RecallHint = new HintViewModel(this.IsRecallEnabled ? this._recallHeroToMainPartyHintText : empty2, null);
			this.TalkHint = new HintViewModel(this.IsTalkEnabled ? this._talkToHeroHintText : empty, null);
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x00064984 File Offset: 0x00062B84
		public void ExecuteLink()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this._hero.EncyclopediaLink);
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000649A0 File Offset: 0x00062BA0
		public void OnCharacterSelect()
		{
			this._onCharacterSelect(this);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000649AE File Offset: 0x00062BAE
		public virtual void ExecuteBeginHint()
		{
			InformationManager.ShowTooltip(typeof(Hero), new object[] { this._hero, true });
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x000649D7 File Offset: 0x00062BD7
		public virtual void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x000649DE File Offset: 0x00062BDE
		public Hero GetHero()
		{
			return this._hero;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x000649E8 File Offset: 0x00062BE8
		public void ExecuteRename()
		{
			InformationManager.ShowTextInquiry(new TextInquiryData(new TextObject("{=2lFwF07j}Change Name", null).ToString(), string.Empty, true, true, GameTexts.FindText("str_done", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), new Action<string>(this.OnNamingHeroOver), null, false, new Func<string, Tuple<bool, string>>(CampaignUIHelper.IsStringApplicableForHeroName), "", ""), false, false);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00064A5C File Offset: 0x00062C5C
		private void OnNamingHeroOver(string suggestedName)
		{
			if (CampaignUIHelper.IsStringApplicableForHeroName(suggestedName).Item1)
			{
				TextObject textObject = GameTexts.FindText("str_generic_character_firstname", null);
				textObject.SetTextVariable("CHARACTER_FIRSTNAME", new TextObject(suggestedName, null));
				TextObject textObject2 = GameTexts.FindText("str_generic_character_name", null);
				textObject2.SetTextVariable("CHARACTER_NAME", new TextObject(suggestedName, null));
				textObject2.SetTextVariable("CHARACTER_GENDER", this._hero.IsFemale ? 1 : 0);
				textObject.SetTextVariable("CHARACTER_GENDER", this._hero.IsFemale ? 1 : 0);
				this._hero.SetName(textObject2, textObject);
				this.Name = suggestedName;
				MobileParty partyBelongedTo = this._hero.PartyBelongedTo;
				if (((partyBelongedTo != null) ? partyBelongedTo.Army : null) != null && this._hero.PartyBelongedTo.Army.LeaderParty.Owner == this._hero)
				{
					this._hero.PartyBelongedTo.Army.UpdateName();
					return;
				}
			}
			else
			{
				Debug.FailedAssert("Suggested name is not acceptable. This shouldn't happen", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\ClanManagement\\ClanLordItemVM.cs", "OnNamingHeroOver", 203);
			}
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00064B6F File Offset: 0x00062D6F
		public void ExecuteShowOnMap()
		{
			if (this._hero != null && this.CanShowLocationOfHero)
			{
				this._showHeroOnMap(this._hero);
			}
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00064B92 File Offset: 0x00062D92
		public void ExecuteRecall()
		{
			Action onRecall = this._onRecall;
			if (onRecall == null)
			{
				return;
			}
			onRecall();
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00064BA4 File Offset: 0x00062DA4
		public void ExecuteTalk()
		{
			Action onTalk = this._onTalk;
			if (onTalk == null)
			{
				return;
			}
			onTalk();
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00064BB6 File Offset: 0x00062DB6
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.HeroModel.OnFinalize();
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x00064BC9 File Offset: 0x00062DC9
		// (set) Token: 0x06001AC3 RID: 6851 RVA: 0x00064BD1 File Offset: 0x00062DD1
		[DataSourceProperty]
		public MBBindingList<EncyclopediaSkillVM> Skills
		{
			get
			{
				return this._skills;
			}
			set
			{
				if (value != this._skills)
				{
					this._skills = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaSkillVM>>(value, "Skills");
				}
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x00064BEF File Offset: 0x00062DEF
		// (set) Token: 0x06001AC5 RID: 6853 RVA: 0x00064BF7 File Offset: 0x00062DF7
		[DataSourceProperty]
		public MBBindingList<EncyclopediaTraitItemVM> Traits
		{
			get
			{
				return this._traits;
			}
			set
			{
				if (value != this._traits)
				{
					this._traits = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaTraitItemVM>>(value, "Traits");
				}
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x00064C15 File Offset: 0x00062E15
		// (set) Token: 0x06001AC7 RID: 6855 RVA: 0x00064C1D File Offset: 0x00062E1D
		[DataSourceProperty]
		public HeroViewModel HeroModel
		{
			get
			{
				return this._heroModel;
			}
			set
			{
				if (value != this._heroModel)
				{
					this._heroModel = value;
					base.OnPropertyChangedWithValue<HeroViewModel>(value, "HeroModel");
				}
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x00064C3B File Offset: 0x00062E3B
		// (set) Token: 0x06001AC9 RID: 6857 RVA: 0x00064C43 File Offset: 0x00062E43
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x00064C61 File Offset: 0x00062E61
		// (set) Token: 0x06001ACB RID: 6859 RVA: 0x00064C69 File Offset: 0x00062E69
		[DataSourceProperty]
		public bool IsChild
		{
			get
			{
				return this._isChild;
			}
			set
			{
				if (value != this._isChild)
				{
					this._isChild = value;
					base.OnPropertyChangedWithValue(value, "IsChild");
				}
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x00064C87 File Offset: 0x00062E87
		// (set) Token: 0x06001ACD RID: 6861 RVA: 0x00064C8F File Offset: 0x00062E8F
		[DataSourceProperty]
		public bool IsTeleporting
		{
			get
			{
				return this._isTeleporting;
			}
			set
			{
				if (value != this._isTeleporting)
				{
					this._isTeleporting = value;
					base.OnPropertyChangedWithValue(value, "IsTeleporting");
				}
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x00064CAD File Offset: 0x00062EAD
		// (set) Token: 0x06001ACF RID: 6863 RVA: 0x00064CB5 File Offset: 0x00062EB5
		[DataSourceProperty]
		public bool IsRecallVisible
		{
			get
			{
				return this._isRecallVisible;
			}
			set
			{
				if (value != this._isRecallVisible)
				{
					this._isRecallVisible = value;
					base.OnPropertyChangedWithValue(value, "IsRecallVisible");
				}
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x00064CD3 File Offset: 0x00062ED3
		// (set) Token: 0x06001AD1 RID: 6865 RVA: 0x00064CDB File Offset: 0x00062EDB
		[DataSourceProperty]
		public bool IsRecallEnabled
		{
			get
			{
				return this._isRecallEnabled;
			}
			set
			{
				if (value != this._isRecallEnabled)
				{
					this._isRecallEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsRecallEnabled");
				}
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x00064CF9 File Offset: 0x00062EF9
		// (set) Token: 0x06001AD3 RID: 6867 RVA: 0x00064D01 File Offset: 0x00062F01
		[DataSourceProperty]
		public bool IsTalkVisible
		{
			get
			{
				return this._isTalkVisible;
			}
			set
			{
				if (value != this._isTalkVisible)
				{
					this._isTalkVisible = value;
					base.OnPropertyChangedWithValue(value, "IsTalkVisible");
				}
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x00064D1F File Offset: 0x00062F1F
		// (set) Token: 0x06001AD5 RID: 6869 RVA: 0x00064D27 File Offset: 0x00062F27
		[DataSourceProperty]
		public bool IsTalkEnabled
		{
			get
			{
				return this._isTalkEnabled;
			}
			set
			{
				if (value != this._isTalkEnabled)
				{
					this._isTalkEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsTalkEnabled");
				}
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x00064D45 File Offset: 0x00062F45
		// (set) Token: 0x06001AD7 RID: 6871 RVA: 0x00064D4D File Offset: 0x00062F4D
		[DataSourceProperty]
		public bool CanShowLocationOfHero
		{
			get
			{
				return this._canShowLocationOfHero;
			}
			set
			{
				if (value != this._canShowLocationOfHero)
				{
					this._canShowLocationOfHero = value;
					base.OnPropertyChangedWithValue(value, "CanShowLocationOfHero");
				}
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x00064D6B File Offset: 0x00062F6B
		// (set) Token: 0x06001AD9 RID: 6873 RVA: 0x00064D73 File Offset: 0x00062F73
		[DataSourceProperty]
		public bool IsMainHero
		{
			get
			{
				return this._isMainHero;
			}
			set
			{
				if (value != this._isMainHero)
				{
					this._isMainHero = value;
					base.OnPropertyChangedWithValue(value, "IsMainHero");
				}
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x00064D91 File Offset: 0x00062F91
		// (set) Token: 0x06001ADB RID: 6875 RVA: 0x00064D99 File Offset: 0x00062F99
		[DataSourceProperty]
		public bool IsFamilyMember
		{
			get
			{
				return this._isFamilyMember;
			}
			set
			{
				if (value != this._isFamilyMember)
				{
					this._isFamilyMember = value;
					base.OnPropertyChangedWithValue(value, "IsFamilyMember");
				}
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x00064DB7 File Offset: 0x00062FB7
		// (set) Token: 0x06001ADD RID: 6877 RVA: 0x00064DBF File Offset: 0x00062FBF
		[DataSourceProperty]
		public bool IsPregnant
		{
			get
			{
				return this._isPregnant;
			}
			set
			{
				if (value != this._isPregnant)
				{
					this._isPregnant = value;
					base.OnPropertyChangedWithValue(value, "IsPregnant");
				}
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x00064DDD File Offset: 0x00062FDD
		// (set) Token: 0x06001ADF RID: 6879 RVA: 0x00064DE5 File Offset: 0x00062FE5
		[DataSourceProperty]
		public CharacterImageIdentifierVM Visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				if (value != this._visual)
				{
					this._visual = value;
					base.OnPropertyChangedWithValue<CharacterImageIdentifierVM>(value, "Visual");
				}
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x00064E03 File Offset: 0x00063003
		// (set) Token: 0x06001AE1 RID: 6881 RVA: 0x00064E0B File Offset: 0x0006300B
		[DataSourceProperty]
		public BannerImageIdentifierVM Banner_9
		{
			get
			{
				return this._banner_9;
			}
			set
			{
				if (value != this._banner_9)
				{
					this._banner_9 = value;
					base.OnPropertyChangedWithValue<BannerImageIdentifierVM>(value, "Banner_9");
				}
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x00064E29 File Offset: 0x00063029
		// (set) Token: 0x06001AE3 RID: 6883 RVA: 0x00064E31 File Offset: 0x00063031
		[DataSourceProperty]
		public string LocationText
		{
			get
			{
				return this._locationText;
			}
			set
			{
				if (value != this._locationText)
				{
					this._locationText = value;
					base.OnPropertyChangedWithValue<string>(value, "LocationText");
				}
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x00064E54 File Offset: 0x00063054
		// (set) Token: 0x06001AE5 RID: 6885 RVA: 0x00064E5C File Offset: 0x0006305C
		[DataSourceProperty]
		public string CurrentActionText
		{
			get
			{
				return this._currentActionText;
			}
			set
			{
				if (value != this._currentActionText)
				{
					this._currentActionText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentActionText");
				}
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x00064E7F File Offset: 0x0006307F
		// (set) Token: 0x06001AE7 RID: 6887 RVA: 0x00064E87 File Offset: 0x00063087
		[DataSourceProperty]
		public string RelationToMainHeroText
		{
			get
			{
				return this._relationToMainHeroText;
			}
			set
			{
				if (value != this._relationToMainHeroText)
				{
					this._relationToMainHeroText = value;
					base.OnPropertyChangedWithValue<string>(value, "RelationToMainHeroText");
				}
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x00064EAA File Offset: 0x000630AA
		// (set) Token: 0x06001AE9 RID: 6889 RVA: 0x00064EB2 File Offset: 0x000630B2
		[DataSourceProperty]
		public string GovernorOfText
		{
			get
			{
				return this._governorOfText;
			}
			set
			{
				if (value != this._governorOfText)
				{
					this._governorOfText = value;
					base.OnPropertyChangedWithValue<string>(value, "GovernorOfText");
				}
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x00064ED5 File Offset: 0x000630D5
		// (set) Token: 0x06001AEB RID: 6891 RVA: 0x00064EDD File Offset: 0x000630DD
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x00064F00 File Offset: 0x00063100
		// (set) Token: 0x06001AED RID: 6893 RVA: 0x00064F08 File Offset: 0x00063108
		[DataSourceProperty]
		public HintViewModel PregnantHint
		{
			get
			{
				return this._pregnantHint;
			}
			set
			{
				if (value != this._pregnantHint)
				{
					this._pregnantHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "PregnantHint");
				}
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x00064F26 File Offset: 0x00063126
		// (set) Token: 0x06001AEF RID: 6895 RVA: 0x00064F2E File Offset: 0x0006312E
		[DataSourceProperty]
		public HintViewModel ShowOnMapHint
		{
			get
			{
				return this._showOnMapHint;
			}
			set
			{
				if (value != this._showOnMapHint)
				{
					this._showOnMapHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ShowOnMapHint");
				}
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x00064F4C File Offset: 0x0006314C
		// (set) Token: 0x06001AF1 RID: 6897 RVA: 0x00064F54 File Offset: 0x00063154
		[DataSourceProperty]
		public HintViewModel RecallHint
		{
			get
			{
				return this._recallHint;
			}
			set
			{
				if (value != this._recallHint)
				{
					this._recallHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "RecallHint");
				}
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x00064F72 File Offset: 0x00063172
		// (set) Token: 0x06001AF3 RID: 6899 RVA: 0x00064F7A File Offset: 0x0006317A
		[DataSourceProperty]
		public HintViewModel TalkHint
		{
			get
			{
				return this._talkHint;
			}
			set
			{
				if (value != this._talkHint)
				{
					this._talkHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "TalkHint");
				}
			}
		}

		// Token: 0x04000C6F RID: 3183
		private readonly Action<ClanLordItemVM> _onCharacterSelect;

		// Token: 0x04000C70 RID: 3184
		private readonly Action _onRecall;

		// Token: 0x04000C71 RID: 3185
		private readonly Action _onTalk;

		// Token: 0x04000C72 RID: 3186
		private readonly Hero _hero;

		// Token: 0x04000C73 RID: 3187
		private readonly Action<Hero> _showHeroOnMap;

		// Token: 0x04000C74 RID: 3188
		private readonly ITeleportationCampaignBehavior _teleportationBehavior;

		// Token: 0x04000C75 RID: 3189
		private readonly TextObject _prisonerOfText = new TextObject("{=a8nRxITn}Prisoner of {PARTY_NAME}", null);

		// Token: 0x04000C76 RID: 3190
		private readonly TextObject _showLocationOfHeroOnMap = new TextObject("{=aGJYQOef}Show hero's location on map.", null);

		// Token: 0x04000C77 RID: 3191
		private readonly TextObject _recallHeroToMainPartyHintText = new TextObject("{=ANV8UV5f}Recall this member to your party.", null);

		// Token: 0x04000C78 RID: 3192
		private readonly TextObject _talkToHeroHintText = new TextObject("{=j4BdjLYp}Start a conversation with this clan member.", null);

		// Token: 0x04000C79 RID: 3193
		private CharacterImageIdentifierVM _visual;

		// Token: 0x04000C7A RID: 3194
		private BannerImageIdentifierVM _banner_9;

		// Token: 0x04000C7B RID: 3195
		private bool _isSelected;

		// Token: 0x04000C7C RID: 3196
		private bool _isChild;

		// Token: 0x04000C7D RID: 3197
		private bool _isMainHero;

		// Token: 0x04000C7E RID: 3198
		private bool _isFamilyMember;

		// Token: 0x04000C7F RID: 3199
		private bool _isPregnant;

		// Token: 0x04000C80 RID: 3200
		private bool _isTeleporting;

		// Token: 0x04000C81 RID: 3201
		private bool _isRecallVisible;

		// Token: 0x04000C82 RID: 3202
		private bool _isRecallEnabled;

		// Token: 0x04000C83 RID: 3203
		private bool _isTalkVisible;

		// Token: 0x04000C84 RID: 3204
		private bool _isTalkEnabled;

		// Token: 0x04000C85 RID: 3205
		private bool _canShowLocationOfHero;

		// Token: 0x04000C86 RID: 3206
		private string _name;

		// Token: 0x04000C87 RID: 3207
		private string _locationText;

		// Token: 0x04000C88 RID: 3208
		private string _relationToMainHeroText;

		// Token: 0x04000C89 RID: 3209
		private string _governorOfText;

		// Token: 0x04000C8A RID: 3210
		private string _currentActionText;

		// Token: 0x04000C8B RID: 3211
		private HeroViewModel _heroModel;

		// Token: 0x04000C8C RID: 3212
		private MBBindingList<EncyclopediaSkillVM> _skills;

		// Token: 0x04000C8D RID: 3213
		private MBBindingList<EncyclopediaTraitItemVM> _traits;

		// Token: 0x04000C8E RID: 3214
		private HintViewModel _pregnantHint;

		// Token: 0x04000C8F RID: 3215
		private HintViewModel _showOnMapHint;

		// Token: 0x04000C90 RID: 3216
		private HintViewModel _recallHint;

		// Token: 0x04000C91 RID: 3217
		private HintViewModel _talkHint;
	}
}
