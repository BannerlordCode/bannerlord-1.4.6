using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Overlay
{
	// Token: 0x020000B4 RID: 180
	[MenuOverlay("ArmyMenuOverlay")]
	public class ArmyMenuOverlayVM : GameMenuOverlay
	{
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x00046916 File Offset: 0x00044B16
		private Army ArmyToUse
		{
			get
			{
				MobileParty mainParty = MobileParty.MainParty;
				Army army;
				if ((army = ((mainParty != null) ? mainParty.Army : null)) == null)
				{
					MobileParty mainParty2 = MobileParty.MainParty;
					if (mainParty2 == null)
					{
						return null;
					}
					MobileParty targetParty = mainParty2.TargetParty;
					if (targetParty == null)
					{
						return null;
					}
					army = targetParty.Army;
				}
				return army;
			}
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00046948 File Offset: 0x00044B48
		public ArmyMenuOverlayVM()
		{
			this.PartyList = new MBBindingList<GameMenuPartyItemVM>();
			base.CurrentOverlayType = 2;
			base.IsInitializationOver = false;
			this.CohesionHint = new BasicTooltipViewModel();
			this.ManCountHint = new BasicTooltipViewModel();
			this.FoodHint = new BasicTooltipViewModel();
			this.TutorialNotification = new ElementNotificationVM();
			this.ManageArmyHint = new HintViewModel();
			this.Refresh();
			this._contextMenuItem = null;
			CampaignEvents.ArmyOverlaySetDirtyEvent.AddNonSerializedListener(this, new Action(this.Refresh));
			CampaignEvents.PartyAttachedAnotherParty.AddNonSerializedListener(this, new Action<MobileParty>(this.OnPartyAttachedAnotherParty));
			CampaignEvents.OnTroopRecruitedEvent.AddNonSerializedListener(this, new Action<Hero, Settlement, Hero, CharacterObject, int>(this.OnTroopRecruited));
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			this._cohesionConceptObj = Concept.All.SingleOrDefault<Concept>((Concept c) => c.StringId == "str_game_objects_army_cohesion");
			base.IsInitializationOver = true;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00046A4F File Offset: 0x00044C4F
		public override void RefreshValues()
		{
			base.RefreshValues();
			ElementNotificationVM tutorialNotification = this.TutorialNotification;
			if (tutorialNotification != null)
			{
				tutorialNotification.RefreshValues();
			}
			this.Refresh();
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00046A70 File Offset: 0x00044C70
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEvents.ArmyOverlaySetDirtyEvent.ClearListeners(this);
			CampaignEvents.PartyAttachedAnotherParty.ClearListeners(this);
			CampaignEvents.OnTroopRecruitedEvent.ClearListeners(this);
			Game.Current.EventManager.UnregisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00046AC0 File Offset: 0x00044CC0
		protected override void ExecuteOnSetAsActiveContextMenuItem(GameMenuPartyItemVM troop)
		{
			base.ExecuteOnSetAsActiveContextMenuItem(troop);
			base.ContextList.Clear();
			MobileParty mobileParty = this._contextMenuItem.Party.MobileParty;
			if (((mobileParty != null) ? mobileParty.Army : null) != null && ArmyMenuOverlayVM.GetIsPlayerArmyLeader(this._contextMenuItem.Party.MobileParty.Army) && this._contextMenuItem.Party.MapEvent == null && this._contextMenuItem.Party != this._contextMenuItem.Party.MobileParty.Army.LeaderParty.Party)
			{
				TextObject textObject;
				bool mapScreenActionIsEnabledWithReason = CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out textObject);
				base.ContextList.Add(new StringItemWithEnabledAndHintVM(new Action<object>(base.ExecuteTroopAction), GameTexts.FindText("str_menu_overlay_context_list", GameMenuOverlay.MenuOverlayContextList.ArmyDismiss.ToString()).ToString(), mapScreenActionIsEnabledWithReason, GameMenuOverlay.MenuOverlayContextList.ArmyDismiss, textObject));
			}
			float getEncounterJoiningRadius = Campaign.Current.Models.EncounterModel.GetEncounterJoiningRadius;
			MapEvent mapEvent = MobileParty.MainParty.MapEvent;
			CampaignVec2 campaignVec = ((mapEvent != null) ? mapEvent.Position : MobileParty.MainParty.Position);
			MobileParty mobileParty2 = troop.Party.MobileParty;
			float? num = ((mobileParty2 != null) ? new float?(mobileParty2.Position.DistanceSquared(campaignVec)) : null);
			float num2 = getEncounterJoiningRadius * getEncounterJoiningRadius;
			bool flag = (num.GetValueOrDefault() < num2) & (num != null);
			bool flag2 = troop.Party.MobileParty.MapEvent == MobileParty.MainParty.MapEvent;
			bool flag3 = PlayerEncounter.EncounteredParty != null && PlayerEncounter.EncounteredParty.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction);
			if (this._contextMenuItem.Party.LeaderHero != null && flag && flag2 && !flag3 && this._contextMenuItem.Party != PartyBase.MainParty)
			{
				PlayerEncounter playerEncounter = PlayerEncounter.Current;
				if (((playerEncounter != null) ? playerEncounter.BattleSimulation : null) == null)
				{
					base.ContextList.Add(new StringItemWithEnabledAndHintVM(new Action<object>(base.ExecuteTroopAction), GameTexts.FindText("str_menu_overlay_context_list", GameMenuOverlay.MenuOverlayContextList.DonateTroops.ToString()).ToString(), true, GameMenuOverlay.MenuOverlayContextList.DonateTroops, null));
					if (MobileParty.MainParty.CurrentSettlement == null && LocationComplex.Current == null)
					{
						base.ContextList.Add(new StringItemWithEnabledAndHintVM(new Action<object>(base.ExecuteTroopAction), GameTexts.FindText("str_menu_overlay_context_list", GameMenuOverlay.MenuOverlayContextList.ConverseWithLeader.ToString()).ToString(), true, GameMenuOverlay.MenuOverlayContextList.ConverseWithLeader, null));
					}
				}
			}
			base.ContextList.Add(new StringItemWithEnabledAndHintVM(new Action<object>(base.ExecuteTroopAction), GameTexts.FindText("str_menu_overlay_context_list", GameMenuOverlay.MenuOverlayContextList.Encyclopedia.ToString()).ToString(), true, GameMenuOverlay.MenuOverlayContextList.Encyclopedia, null));
			CharacterObject characterObject;
			if ((characterObject = this._contextMenuItem.Character) == null)
			{
				Hero leaderHero = this._contextMenuItem.Party.LeaderHero;
				characterObject = ((leaderHero != null) ? leaderHero.CharacterObject : null);
			}
			CharacterObject characterObject2 = characterObject;
			if (characterObject2 == null)
			{
				Debug.FailedAssert("ArmyMenuOverlayVM.ExecuteOnSetAsActiveContextMenuItem called on party with no leader hero: " + this._contextMenuItem.Party.Name, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\GameMenu\\Overlay\\ArmyMenuOverlayVM.cs", "ExecuteOnSetAsActiveContextMenuItem", 124);
				return;
			}
			CampaignEventDispatcher.Instance.OnCharacterPortraitPopUpOpened(characterObject2);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00046E04 File Offset: 0x00045004
		public override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			TextObject textObject;
			this.CanManageArmy = CampaignUIHelper.GetCanManageCurrentArmyWithReason(out textObject);
			this.ManageArmyHint.HintText = textObject;
			for (int i = 0; i < this.PartyList.Count; i++)
			{
				this.PartyList[i].RefreshQuestStatus();
			}
			if (this._isVisualsDirty)
			{
				this.RefreshVisualsOfItems();
				this._isVisualsDirty = false;
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00046E6D File Offset: 0x0004506D
		public sealed override void Refresh()
		{
			if (this.ArmyToUse != null)
			{
				base.IsInitializationOver = false;
				this.UpdateLists();
				this.UpdateProperties();
				base.IsInitializationOver = true;
			}
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00046E94 File Offset: 0x00045094
		private void UpdateProperties()
		{
			MBTextManager.SetTextVariable("newline", "\n", false);
			Army army = this.ArmyToUse;
			if (army == null)
			{
				Debug.FailedAssert("Army is null but trying to update army overlay properties", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\GameMenu\\Overlay\\ArmyMenuOverlayVM.cs", "UpdateProperties", 169);
				return;
			}
			float num = army.LeaderParty.Food;
			foreach (MobileParty mobileParty in army.LeaderParty.AttachedParties)
			{
				num += mobileParty.Food;
			}
			this.Food = (int)num;
			this.Cohesion = (int)army.Cohesion;
			this.ManCountText = CampaignUIHelper.GetPartyNameplateText(army.LeaderParty, true);
			this.FoodHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetArmyFoodTooltip(army));
			this.CohesionHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetArmyCohesionTooltip(army));
			this.ManCountHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetArmyManCountTooltip(army));
			this.IsCohesionWarningEnabled = army.Cohesion <= 30f;
			this.IsPlayerArmyLeader = ArmyMenuOverlayVM.GetIsPlayerArmyLeader(army);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00046FEC File Offset: 0x000451EC
		private void UpdateLists()
		{
			Army armyToUse = this.ArmyToUse;
			if (armyToUse == null)
			{
				Debug.FailedAssert("Army is null but trying to update army overlay lists", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\GameMenu\\Overlay\\ArmyMenuOverlayVM.cs", "UpdateLists", 198);
				return;
			}
			for (int i = this.PartyList.Count - 1; i >= 0; i--)
			{
				GameMenuPartyItemVM partyVM = this.PartyList[i];
				if (!armyToUse.Parties.Any<MobileParty>((MobileParty p) => p.Party == partyVM.Party))
				{
					this.PartyList.RemoveAt(i);
				}
			}
			for (int j = 0; j < armyToUse.Parties.Count; j++)
			{
				MobileParty party = armyToUse.Parties[j];
				if (!this.PartyList.Any<GameMenuPartyItemVM>((GameMenuPartyItemVM p) => p.Party == party.Party))
				{
					bool flag = party == armyToUse.LeaderParty;
					GameMenuPartyItemVM gameMenuPartyItemVM = new GameMenuPartyItemVM(new Action<GameMenuPartyItemVM>(this.ExecuteOnSetAsActiveContextMenuItem), party.Party, true)
					{
						IsLeader = flag
					};
					if (flag)
					{
						this.PartyList.Insert(0, gameMenuPartyItemVM);
					}
					else
					{
						this.PartyList.Add(gameMenuPartyItemVM);
					}
				}
			}
			foreach (GameMenuPartyItemVM gameMenuPartyItemVM2 in this.PartyList)
			{
				gameMenuPartyItemVM2.RefreshProperties();
			}
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00047160 File Offset: 0x00045360
		public void ExecuteOpenArmyManagement()
		{
			Army armyToUse = this.ArmyToUse;
			if (armyToUse != null && ArmyMenuOverlayVM.GetIsPlayerArmyLeader(armyToUse))
			{
				Action openArmyManagement = this.OpenArmyManagement;
				if (openArmyManagement == null)
				{
					return;
				}
				openArmyManagement();
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0004718F File Offset: 0x0004538F
		private void ExecuteCohesionLink()
		{
			if (this._cohesionConceptObj != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this._cohesionConceptObj.EncyclopediaLink);
				return;
			}
			Debug.FailedAssert("Couldn't find Cohesion encyclopedia page", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\GameMenu\\Overlay\\ArmyMenuOverlayVM.cs", "ExecuteCohesionLink", 257);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x000471D0 File Offset: 0x000453D0
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent obj)
		{
			if (obj.NewNotificationElementID != this._latestTutorialElementID)
			{
				if (this._latestTutorialElementID != null)
				{
					this.TutorialNotification.ElementID = string.Empty;
				}
				this._latestTutorialElementID = obj.NewNotificationElementID;
				if (this._latestTutorialElementID != null)
				{
					this.TutorialNotification.ElementID = this._latestTutorialElementID;
				}
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00047230 File Offset: 0x00045430
		private void RefreshVisualsOfItems()
		{
			for (int i = 0; i < this.PartyList.Count; i++)
			{
				this.PartyList[i].RefreshVisual();
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00047264 File Offset: 0x00045464
		private void OnPartyAttachedAnotherParty(MobileParty party)
		{
			MobileParty attachedTo = party.AttachedTo;
			if (((attachedTo != null) ? attachedTo.Army : null) != null && party.AttachedTo.Army == MobileParty.MainParty.Army)
			{
				this._isVisualsDirty = true;
			}
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00047298 File Offset: 0x00045498
		private void OnTroopRecruited(Hero recruiterHero, Settlement settlement, Hero troopSource, CharacterObject troop, int number)
		{
			if (((recruiterHero != null) ? recruiterHero.PartyBelongedTo : null) != null && recruiterHero.IsPartyLeader)
			{
				for (int i = 0; i < this.PartyList.Count; i++)
				{
					if (this.PartyList[i].Party == recruiterHero.PartyBelongedTo.Party)
					{
						this.PartyList[i].RefreshProperties();
						return;
					}
				}
			}
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00047301 File Offset: 0x00045501
		private static bool GetIsPlayerArmyLeader(Army army)
		{
			return army.LeaderParty == MobileParty.MainParty || army.LeaderParty == MobileParty.MainParty.TargetParty;
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00047324 File Offset: 0x00045524
		// (set) Token: 0x060011C4 RID: 4548 RVA: 0x0004732C File Offset: 0x0004552C
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

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0004734A File Offset: 0x0004554A
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x00047352 File Offset: 0x00045552
		[DataSourceProperty]
		public HintViewModel ManageArmyHint
		{
			get
			{
				return this._manageArmyHint;
			}
			set
			{
				if (value != this._manageArmyHint)
				{
					this._manageArmyHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ManageArmyHint");
				}
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00047370 File Offset: 0x00045570
		// (set) Token: 0x060011C8 RID: 4552 RVA: 0x00047378 File Offset: 0x00045578
		[DataSourceProperty]
		public int Cohesion
		{
			get
			{
				return this._cohesion;
			}
			set
			{
				if (value != this._cohesion)
				{
					this._cohesion = value;
					base.OnPropertyChangedWithValue(value, "Cohesion");
				}
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00047396 File Offset: 0x00045596
		// (set) Token: 0x060011CA RID: 4554 RVA: 0x0004739E File Offset: 0x0004559E
		[DataSourceProperty]
		public bool IsCohesionWarningEnabled
		{
			get
			{
				return this._isCohesionWarningEnabled;
			}
			set
			{
				if (value != this._isCohesionWarningEnabled)
				{
					this._isCohesionWarningEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsCohesionWarningEnabled");
				}
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x000473BC File Offset: 0x000455BC
		// (set) Token: 0x060011CC RID: 4556 RVA: 0x000473C4 File Offset: 0x000455C4
		[DataSourceProperty]
		public bool CanManageArmy
		{
			get
			{
				return this._canManageArmy;
			}
			set
			{
				if (value != this._canManageArmy)
				{
					this._canManageArmy = value;
					base.OnPropertyChangedWithValue(value, "CanManageArmy");
				}
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x000473E2 File Offset: 0x000455E2
		// (set) Token: 0x060011CE RID: 4558 RVA: 0x000473EA File Offset: 0x000455EA
		[DataSourceProperty]
		public bool IsPlayerArmyLeader
		{
			get
			{
				return this._isPlayerArmyLeader;
			}
			set
			{
				if (value != this._isPlayerArmyLeader)
				{
					this._isPlayerArmyLeader = value;
					base.OnPropertyChangedWithValue(value, "IsPlayerArmyLeader");
				}
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x00047408 File Offset: 0x00045608
		// (set) Token: 0x060011D0 RID: 4560 RVA: 0x00047410 File Offset: 0x00045610
		[DataSourceProperty]
		public string ManCountText
		{
			get
			{
				return this._manCountText;
			}
			set
			{
				if (value != this._manCountText)
				{
					this._manCountText = value;
					base.OnPropertyChangedWithValue<string>(value, "ManCountText");
				}
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x00047433 File Offset: 0x00045633
		// (set) Token: 0x060011D2 RID: 4562 RVA: 0x0004743B File Offset: 0x0004563B
		[DataSourceProperty]
		public int Food
		{
			get
			{
				return this._food;
			}
			set
			{
				if (value != this._food)
				{
					this._food = value;
					base.OnPropertyChangedWithValue(value, "Food");
				}
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00047459 File Offset: 0x00045659
		// (set) Token: 0x060011D4 RID: 4564 RVA: 0x00047461 File Offset: 0x00045661
		[DataSourceProperty]
		public MBBindingList<GameMenuPartyItemVM> PartyList
		{
			get
			{
				return this._partyList;
			}
			set
			{
				if (value != this._partyList)
				{
					this._partyList = value;
					base.OnPropertyChangedWithValue<MBBindingList<GameMenuPartyItemVM>>(value, "PartyList");
				}
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x0004747F File Offset: 0x0004567F
		// (set) Token: 0x060011D6 RID: 4566 RVA: 0x00047487 File Offset: 0x00045687
		[DataSourceProperty]
		public BasicTooltipViewModel CohesionHint
		{
			get
			{
				return this._cohesionHint;
			}
			set
			{
				if (value != this._cohesionHint)
				{
					this._cohesionHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "CohesionHint");
				}
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x000474A5 File Offset: 0x000456A5
		// (set) Token: 0x060011D8 RID: 4568 RVA: 0x000474AD File Offset: 0x000456AD
		[DataSourceProperty]
		public BasicTooltipViewModel ManCountHint
		{
			get
			{
				return this._manCountHint;
			}
			set
			{
				if (value != this._manCountHint)
				{
					this._manCountHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "ManCountHint");
				}
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x000474CB File Offset: 0x000456CB
		// (set) Token: 0x060011DA RID: 4570 RVA: 0x000474D3 File Offset: 0x000456D3
		[DataSourceProperty]
		public BasicTooltipViewModel FoodHint
		{
			get
			{
				return this._foodHint;
			}
			set
			{
				if (value != this._foodHint)
				{
					this._foodHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "FoodHint");
				}
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x000474F1 File Offset: 0x000456F1
		[DataSourceProperty]
		public MBBindingList<StringItemWithHintVM> IssueList
		{
			get
			{
				if (this._issueList == null)
				{
					this._issueList = new MBBindingList<StringItemWithHintVM>();
				}
				return this._issueList;
			}
		}

		// Token: 0x04000819 RID: 2073
		private const float CohesionWarningMin = 30f;

		// Token: 0x0400081A RID: 2074
		public Action OpenArmyManagement;

		// Token: 0x0400081B RID: 2075
		private readonly Concept _cohesionConceptObj;

		// Token: 0x0400081C RID: 2076
		private string _latestTutorialElementID;

		// Token: 0x0400081D RID: 2077
		private bool _isVisualsDirty;

		// Token: 0x0400081E RID: 2078
		private MBBindingList<GameMenuPartyItemVM> _partyList;

		// Token: 0x0400081F RID: 2079
		private string _manCountText;

		// Token: 0x04000820 RID: 2080
		private int _cohesion;

		// Token: 0x04000821 RID: 2081
		private int _food;

		// Token: 0x04000822 RID: 2082
		private bool _isCohesionWarningEnabled;

		// Token: 0x04000823 RID: 2083
		private bool _isPlayerArmyLeader;

		// Token: 0x04000824 RID: 2084
		private bool _canManageArmy;

		// Token: 0x04000825 RID: 2085
		private HintViewModel _manageArmyHint;

		// Token: 0x04000826 RID: 2086
		public ElementNotificationVM _tutorialNotification;

		// Token: 0x04000827 RID: 2087
		private BasicTooltipViewModel _cohesionHint;

		// Token: 0x04000828 RID: 2088
		private BasicTooltipViewModel _manCountHint;

		// Token: 0x04000829 RID: 2089
		private BasicTooltipViewModel _foodHint;

		// Token: 0x0400082A RID: 2090
		private MBBindingList<StringItemWithHintVM> _issueList;
	}
}
