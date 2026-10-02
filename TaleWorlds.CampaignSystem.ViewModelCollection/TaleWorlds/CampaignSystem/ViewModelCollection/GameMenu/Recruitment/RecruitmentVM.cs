using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Recruitment
{
	// Token: 0x020000B0 RID: 176
	public class RecruitmentVM : ViewModel
	{
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x000448F2 File Offset: 0x00042AF2
		// (set) Token: 0x06001106 RID: 4358 RVA: 0x000448FA File Offset: 0x00042AFA
		public bool IsQuitting { get; private set; }

		// Token: 0x06001107 RID: 4359 RVA: 0x00044904 File Offset: 0x00042B04
		public RecruitmentVM()
		{
			this.VolunteerList = new MBBindingList<RecruitVolunteerVM>();
			this.TroopsInCart = new MBBindingList<RecruitVolunteerTroopVM>();
			this.RefreshValues();
			if (Settlement.CurrentSettlement != null)
			{
				this.RefreshScreen();
			}
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			RecruitVolunteerTroopVM.OnFocused = (Action<RecruitVolunteerTroopVM>)Delegate.Combine(RecruitVolunteerTroopVM.OnFocused, new Action<RecruitVolunteerTroopVM>(this.OnVolunteerTroopFocusChanged));
			RecruitVolunteerOwnerVM.OnFocused = (Action<RecruitVolunteerOwnerVM>)Delegate.Combine(RecruitVolunteerOwnerVM.OnFocused, new Action<RecruitVolunteerOwnerVM>(this.OnVolunteerOwnerFocusChanged));
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x000449D4 File Offset: 0x00042BD4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PartyWageHint = new HintViewModel(GameTexts.FindText("str_weekly_wage", null), null);
			this.TotalWealthHint = new HintViewModel(GameTexts.FindText("str_wealth", null), null);
			this.TotalCostHint = new HintViewModel(GameTexts.FindText("str_total_cost", null), null);
			this.PartyCapacityHint = new HintViewModel();
			this.PartySpeedHint = new BasicTooltipViewModel();
			this.RemainingFoodHint = new HintViewModel();
			this.DoneHint = new HintViewModel();
			this.ResetHint = new HintViewModel(GameTexts.FindText("str_reset", null), null);
			this.DoneText = GameTexts.FindText("str_done", null).ToString();
			this.TitleText = GameTexts.FindText("str_recruitment", null).ToString();
			this._recruitAllTextObject = GameTexts.FindText("str_recruit_all", null);
			this.ResetAllText = GameTexts.FindText("str_reset_all", null).ToString();
			this.CancelText = GameTexts.FindText("str_party_cancel", null).ToString();
			this._playerDoesntHaveEnoughMoneyStr = GameTexts.FindText("str_warning_you_dont_have_enough_money", null).ToString();
			this._playerIsOverPartyLimitStr = GameTexts.FindText("str_party_size_limit_exceeded", null).ToString();
			this.VolunteerList.ApplyActionOnAllItems(delegate(RecruitVolunteerVM x)
			{
				x.RefreshValues();
			});
			this.TroopsInCart.ApplyActionOnAllItems(delegate(RecruitVolunteerTroopVM x)
			{
				x.RefreshValues();
			});
			this.SetRecruitAllHint();
			this.UpdateRecruitAllProperties();
			if (Settlement.CurrentSettlement != null)
			{
				this.RefreshScreen();
			}
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00044B74 File Offset: 0x00042D74
		public void RefreshScreen()
		{
			this.VolunteerList.Clear();
			this.TroopsInCart.Clear();
			int num = 0;
			this.InitialPartySize = PartyBase.MainParty.NumberOfAllMembers;
			this.RefreshPartyProperties();
			foreach (Hero hero in Settlement.CurrentSettlement.Notables)
			{
				if (hero.CanHaveRecruits)
				{
					MBTextManager.SetTextVariable("INDIVIDUAL_NAME", hero.Name, false);
					List<CharacterObject> volunteerTroopsOfHeroForRecruitment = HeroHelper.GetVolunteerTroopsOfHeroForRecruitment(hero);
					RecruitVolunteerVM recruitVolunteerVM = new RecruitVolunteerVM(hero, volunteerTroopsOfHeroForRecruitment, new Action<RecruitVolunteerVM, RecruitVolunteerTroopVM>(this.OnRecruit), new Action<RecruitVolunteerVM, RecruitVolunteerTroopVM>(this.OnRemoveFromCart));
					this.VolunteerList.Add(recruitVolunteerVM);
					num++;
				}
			}
			this.TotalWealth = Hero.MainHero.Gold;
			this.UpdateRecruitAllProperties();
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00044C5C File Offset: 0x00042E5C
		private void OnRecruit(RecruitVolunteerVM recruitNotable, RecruitVolunteerTroopVM recruitTroop)
		{
			if (!recruitTroop.CanBeRecruited)
			{
				return;
			}
			recruitNotable.OnRecruitMoveToCart(recruitTroop);
			recruitTroop.CanBeRecruited = false;
			this.TroopsInCart.Add(recruitTroop);
			recruitTroop.IsInCart = true;
			CampaignEventDispatcher.Instance.OnPlayerStartRecruitment(recruitTroop.Character);
			this.RefreshPartyProperties();
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00044CAC File Offset: 0x00042EAC
		private void RefreshPartyProperties()
		{
			int num = this.TroopsInCart.Sum<RecruitVolunteerTroopVM>((RecruitVolunteerTroopVM t) => t.Wage);
			this.PartyWage = MobileParty.MainParty.TotalWage;
			if (num > 0)
			{
				this.PartyWageText = CampaignUIHelper.GetValueChangeText((float)this.PartyWage, (float)num, "F0");
			}
			else
			{
				this.PartyWageText = this.PartyWage.ToString();
			}
			double num2 = 0.0;
			if (this.TroopsInCart.Count > 0)
			{
				int num3 = 0;
				int num4 = 0;
				using (IEnumerator<RecruitVolunteerTroopVM> enumerator = this.TroopsInCart.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Character.IsMounted)
						{
							num4++;
						}
						else
						{
							num3++;
						}
					}
				}
				ExplainedNumber explainedNumber = Campaign.Current.Models.PartySpeedCalculatingModel.CalculateBaseSpeed(MobileParty.MainParty, false, num3, num4);
				ExplainedNumber explainedNumber2 = Campaign.Current.Models.PartySpeedCalculatingModel.CalculateFinalSpeed(MobileParty.MainParty, explainedNumber);
				ExplainedNumber explainedNumber3 = Campaign.Current.Models.PartySpeedCalculatingModel.CalculateBaseSpeed(MobileParty.MainParty, false, 0, 0);
				ExplainedNumber explainedNumber4 = Campaign.Current.Models.PartySpeedCalculatingModel.CalculateFinalSpeed(MobileParty.MainParty, explainedNumber3);
				num2 = (double)(MathF.Round(explainedNumber2.ResultNumber, 1) - MathF.Round(explainedNumber4.ResultNumber, 1));
			}
			this.PartySpeedText = MobileParty.MainParty.Speed.ToString("0.0");
			this.PartySpeedHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetPartySpeedTooltip(false));
			if (num2 != 0.0)
			{
				this.PartySpeedText = CampaignUIHelper.GetValueChangeText(MobileParty.MainParty.Speed, (float)num2, "0.0");
			}
			int partySizeLimit = PartyBase.MainParty.PartySizeLimit;
			this.CurrentPartySize = PartyBase.MainParty.NumberOfAllMembers + this.TroopsInCart.Count;
			this.PartyCapacity = partySizeLimit;
			this.IsPartyCapacityWarningEnabled = this.CurrentPartySize > this.PartyCapacity;
			GameTexts.SetVariable("LEFT", this.CurrentPartySize.ToString());
			GameTexts.SetVariable("RIGHT", partySizeLimit.ToString());
			this.PartyCapacityText = GameTexts.FindText("str_LEFT_over_RIGHT", null).ToString();
			this.PartyCapacityHint.HintText = new TextObject("{=!}" + PartyBase.MainParty.PartySizeLimitExplainer.ToString(), null);
			float food = MobileParty.MainParty.Food;
			this.RemainingFoodText = MathF.Round(food, 1).ToString();
			float foodChange = MobileParty.MainParty.FoodChange;
			int totalFoodAtInventory = MobileParty.MainParty.TotalFoodAtInventory;
			int numDaysForFoodToLast = MobileParty.MainParty.GetNumDaysForFoodToLast();
			MBTextManager.SetTextVariable("DAY_NUM", numDaysForFoodToLast);
			this.RemainingFoodHint.HintText = GameTexts.FindText("str_food_consumption_tooltip", null);
			this.RemainingFoodHint.HintText.SetTextVariable("DAILY_FOOD_CONSUMPTION", foodChange, 2);
			this.RemainingFoodHint.HintText.SetTextVariable("REMAINING_DAYS", GameTexts.FindText("str_party_food_left", null));
			this.RemainingFoodHint.HintText.SetTextVariable("TOTAL_FOOD_AMOUNT", ((double)totalFoodAtInventory + 0.01 * (double)PartyBase.MainParty.RemainingFoodPercentage).ToString("0.00"));
			this.RemainingFoodHint.HintText.SetTextVariable("TOTAL_FOOD", totalFoodAtInventory);
			int num5 = this.TroopsInCart.Sum<RecruitVolunteerTroopVM>((RecruitVolunteerTroopVM t) => t.Cost);
			this.TotalCostText = num5.ToString();
			bool flag = num5 <= Hero.MainHero.Gold;
			this.IsDoneEnabled = flag;
			this.DoneHint.HintText = new TextObject("{=!}" + this.GetDoneHint(flag), null);
			this.UpdateRecruitAllProperties();
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x000450D0 File Offset: 0x000432D0
		public void ExecuteDone()
		{
			if (this.CurrentPartySize <= this.PartyCapacity)
			{
				this.OnDone();
				return;
			}
			GameTexts.SetVariable("newline", "\n");
			string text = GameTexts.FindText("str_party_over_limit_troops", null).ToString();
			InformationManager.ShowInquiry(new InquiryData(new TextObject("{=uJro3Bua}Over Limit", null).ToString(), text, true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate
			{
				this.OnDone();
			}, null, "", 0f, null, null, null), false, false);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0004516C File Offset: 0x0004336C
		private void OnDone()
		{
			this.RefreshPartyProperties();
			int num = this.TroopsInCart.Sum<RecruitVolunteerTroopVM>((RecruitVolunteerTroopVM t) => t.Cost);
			if (num > Hero.MainHero.Gold)
			{
				Debug.FailedAssert("Execution shouldn't come here. The checks should happen before", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\GameMenu\\Recruitment\\RecruitmentVM.cs", "OnDone", 229);
				return;
			}
			foreach (RecruitVolunteerTroopVM recruitVolunteerTroopVM in this.TroopsInCart)
			{
				recruitVolunteerTroopVM.Owner.OwnerHero.VolunteerTypes[recruitVolunteerTroopVM.Index] = null;
				MobileParty.MainParty.MemberRoster.AddToCounts(recruitVolunteerTroopVM.Character, 1, false, 0, 0, true, -1);
				CampaignEventDispatcher.Instance.OnUnitRecruited(recruitVolunteerTroopVM.Character, 1);
			}
			GiveGoldAction.ApplyBetweenCharacters(Hero.MainHero, null, num, true);
			if (num > 0)
			{
				MBTextManager.SetTextVariable("GOLD_AMOUNT", MathF.Abs(num));
				InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("str_gold_removed_with_icon", null).ToString(), "event:/ui/notification/coins_negative"));
			}
			this.Deactivate();
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00045298 File Offset: 0x00043498
		public void ExecuteForceQuit()
		{
			if (!this.IsQuitting)
			{
				this.IsQuitting = true;
				if (this.TroopsInCart.Count > 0)
				{
					InformationManager.ShowInquiry(new InquiryData(GameTexts.FindText("str_quit", null).ToString(), GameTexts.FindText("str_quit_question", null).ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate
					{
						this.ExecuteReset();
						this.ExecuteDone();
						this.IsQuitting = false;
					}, delegate
					{
						this.IsQuitting = false;
					}, "", 0f, null, null, null), true, false);
					return;
				}
				this.Deactivate();
			}
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00045340 File Offset: 0x00043540
		public void ExecuteReset()
		{
			for (int i = this.TroopsInCart.Count - 1; i >= 0; i--)
			{
				this.TroopsInCart[i].ExecuteRemoveFromCart();
			}
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00045378 File Offset: 0x00043578
		public void ExecuteRecruitAll()
		{
			foreach (RecruitVolunteerVM recruitVolunteerVM in this.VolunteerList.ToList<RecruitVolunteerVM>())
			{
				foreach (RecruitVolunteerTroopVM recruitVolunteerTroopVM in recruitVolunteerVM.Troops.ToList<RecruitVolunteerTroopVM>())
				{
					recruitVolunteerTroopVM.ExecuteRecruit();
				}
			}
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0004540C File Offset: 0x0004360C
		public void Deactivate()
		{
			this.ExecuteReset();
			this.Enabled = false;
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0004541C File Offset: 0x0004361C
		public override void OnFinalize()
		{
			base.OnFinalize();
			RecruitVolunteerTroopVM.OnFocused = (Action<RecruitVolunteerTroopVM>)Delegate.Remove(RecruitVolunteerTroopVM.OnFocused, new Action<RecruitVolunteerTroopVM>(this.OnVolunteerTroopFocusChanged));
			RecruitVolunteerOwnerVM.OnFocused = (Action<RecruitVolunteerOwnerVM>)Delegate.Remove(RecruitVolunteerOwnerVM.OnFocused, new Action<RecruitVolunteerOwnerVM>(this.OnVolunteerOwnerFocusChanged));
			Game.Current.EventManager.UnregisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			this.CancelInputKey.OnFinalize();
			this.DoneInputKey.OnFinalize();
			this.ResetInputKey.OnFinalize();
			this.RecruitAllInputKey.OnFinalize();
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x000454B8 File Offset: 0x000436B8
		private void OnRemoveFromCart(RecruitVolunteerVM recruitNotable, RecruitVolunteerTroopVM recruitTroop)
		{
			if (this.TroopsInCart.Any<RecruitVolunteerTroopVM>((RecruitVolunteerTroopVM r) => r == recruitTroop))
			{
				recruitNotable.OnRecruitRemovedFromCart(recruitTroop);
				recruitTroop.CanBeRecruited = true;
				recruitTroop.IsInCart = false;
				recruitTroop.IsHiglightEnabled = false;
				this.TroopsInCart.Remove(recruitTroop);
				this.RefreshPartyProperties();
			}
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00045533 File Offset: 0x00043733
		private static bool IsBitSet(int num, int bit)
		{
			return 1 == ((num >> bit) & 1);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00045540 File Offset: 0x00043740
		private string GetDoneHint(bool doesPlayerHasEnoughMoney)
		{
			if (!doesPlayerHasEnoughMoney)
			{
				return this._playerDoesntHaveEnoughMoneyStr;
			}
			return null;
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0004554D File Offset: 0x0004374D
		private void SetRecruitAllHint()
		{
			this.RecruitAllHint = new BasicTooltipViewModel(delegate
			{
				GameTexts.SetVariable("HOTKEY", this.GetRecruitAllKey());
				GameTexts.SetVariable("TEXT", GameTexts.FindText("str_recruit_all", null));
				return GameTexts.FindText("str_hotkey_with_hint", null).ToString();
			});
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00045568 File Offset: 0x00043768
		private void UpdateRecruitAllProperties()
		{
			int numberOfAvailableRecruits = this.GetNumberOfAvailableRecruits();
			GameTexts.SetVariable("STR", numberOfAvailableRecruits);
			GameTexts.SetVariable("STR1", this._recruitAllTextObject);
			GameTexts.SetVariable("STR2", GameTexts.FindText("str_STR_in_parentheses", null));
			this.RecruitAllText = GameTexts.FindText("str_STR1_space_STR2", null).ToString();
			this.CanRecruitAll = numberOfAvailableRecruits > 0;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x000455CC File Offset: 0x000437CC
		private int GetNumberOfAvailableRecruits()
		{
			int num = 0;
			foreach (RecruitVolunteerVM recruitVolunteerVM in this.VolunteerList)
			{
				foreach (RecruitVolunteerTroopVM recruitVolunteerTroopVM in recruitVolunteerVM.Troops)
				{
					if (!recruitVolunteerTroopVM.IsInCart && recruitVolunteerTroopVM.CanBeRecruited)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0004565C File Offset: 0x0004385C
		private void OnVolunteerTroopFocusChanged(RecruitVolunteerTroopVM volunteer)
		{
			this.FocusedVolunteerTroop = volunteer;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00045665 File Offset: 0x00043865
		private void OnVolunteerOwnerFocusChanged(RecruitVolunteerOwnerVM owner)
		{
			this.FocusedVolunteerOwner = owner;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00045670 File Offset: 0x00043870
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent obj)
		{
			if (obj.NewNotificationElementID != this._latestTutorialElementID)
			{
				if (this._latestTutorialElementID != null && this._isAvailableTroopsHighlightApplied)
				{
					this.SetAvailableTroopsHighlightState(false);
					this._isAvailableTroopsHighlightApplied = false;
				}
				this._latestTutorialElementID = obj.NewNotificationElementID;
				if (this._latestTutorialElementID != null && !this._isAvailableTroopsHighlightApplied && this._latestTutorialElementID == "AvailableTroops")
				{
					this.SetAvailableTroopsHighlightState(true);
					this._isAvailableTroopsHighlightApplied = true;
				}
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x000456EC File Offset: 0x000438EC
		private void SetAvailableTroopsHighlightState(bool state)
		{
			foreach (RecruitVolunteerVM recruitVolunteerVM in this.VolunteerList)
			{
				foreach (RecruitVolunteerTroopVM recruitVolunteerTroopVM in recruitVolunteerVM.Troops)
				{
					if (recruitVolunteerTroopVM.Wage < Hero.MainHero.Gold && recruitVolunteerTroopVM.PlayerHasEnoughRelation && !recruitVolunteerTroopVM.IsTroopEmpty)
					{
						recruitVolunteerTroopVM.IsHiglightEnabled = state;
					}
				}
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x00045790 File Offset: 0x00043990
		// (set) Token: 0x0600111E RID: 4382 RVA: 0x00045798 File Offset: 0x00043998
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

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x000457B6 File Offset: 0x000439B6
		// (set) Token: 0x06001120 RID: 4384 RVA: 0x000457BE File Offset: 0x000439BE
		[DataSourceProperty]
		public RecruitVolunteerTroopVM FocusedVolunteerTroop
		{
			get
			{
				return this._focusedVolunteerTroop;
			}
			set
			{
				if (value != this._focusedVolunteerTroop)
				{
					this._focusedVolunteerTroop = value;
					base.OnPropertyChangedWithValue<RecruitVolunteerTroopVM>(value, "FocusedVolunteerTroop");
				}
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x000457DC File Offset: 0x000439DC
		// (set) Token: 0x06001122 RID: 4386 RVA: 0x000457E4 File Offset: 0x000439E4
		[DataSourceProperty]
		public RecruitVolunteerOwnerVM FocusedVolunteerOwner
		{
			get
			{
				return this._focusedVolunteerOwner;
			}
			set
			{
				if (value != this._focusedVolunteerOwner)
				{
					this._focusedVolunteerOwner = value;
					base.OnPropertyChangedWithValue<RecruitVolunteerOwnerVM>(value, "FocusedVolunteerOwner");
				}
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x00045802 File Offset: 0x00043A02
		// (set) Token: 0x06001124 RID: 4388 RVA: 0x0004580A File Offset: 0x00043A0A
		[DataSourceProperty]
		public HintViewModel PartyWageHint
		{
			get
			{
				return this._partyWageHint;
			}
			set
			{
				if (value != this._partyWageHint)
				{
					this._partyWageHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "PartyWageHint");
				}
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x00045828 File Offset: 0x00043A28
		// (set) Token: 0x06001126 RID: 4390 RVA: 0x00045830 File Offset: 0x00043A30
		[DataSourceProperty]
		public HintViewModel PartyCapacityHint
		{
			get
			{
				return this._partyCapacityHint;
			}
			set
			{
				if (value != this._partyCapacityHint)
				{
					this._partyCapacityHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "PartyCapacityHint");
				}
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x0004584E File Offset: 0x00043A4E
		// (set) Token: 0x06001128 RID: 4392 RVA: 0x00045856 File Offset: 0x00043A56
		[DataSourceProperty]
		public BasicTooltipViewModel PartySpeedHint
		{
			get
			{
				return this._partySpeedHint;
			}
			set
			{
				if (value != this._partySpeedHint)
				{
					this._partySpeedHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "PartySpeedHint");
				}
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x00045874 File Offset: 0x00043A74
		// (set) Token: 0x0600112A RID: 4394 RVA: 0x0004587C File Offset: 0x00043A7C
		[DataSourceProperty]
		public HintViewModel RemainingFoodHint
		{
			get
			{
				return this._remainingFoodHint;
			}
			set
			{
				if (value != this._remainingFoodHint)
				{
					this._remainingFoodHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "RemainingFoodHint");
				}
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x0004589A File Offset: 0x00043A9A
		// (set) Token: 0x0600112C RID: 4396 RVA: 0x000458A2 File Offset: 0x00043AA2
		[DataSourceProperty]
		public HintViewModel TotalWealthHint
		{
			get
			{
				return this._totalWealthHint;
			}
			set
			{
				if (value != this._totalWealthHint)
				{
					this._totalWealthHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "TotalWealthHint");
				}
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x000458C0 File Offset: 0x00043AC0
		// (set) Token: 0x0600112E RID: 4398 RVA: 0x000458C8 File Offset: 0x00043AC8
		[DataSourceProperty]
		public HintViewModel TotalCostHint
		{
			get
			{
				return this._totalCostHint;
			}
			set
			{
				if (value != this._totalCostHint)
				{
					this._totalCostHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "TotalCostHint");
				}
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x000458E6 File Offset: 0x00043AE6
		// (set) Token: 0x06001130 RID: 4400 RVA: 0x000458EE File Offset: 0x00043AEE
		[DataSourceProperty]
		public HintViewModel DoneHint
		{
			get
			{
				return this._doneHint;
			}
			set
			{
				if (value != this._doneHint)
				{
					this._doneHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DoneHint");
				}
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x0004590C File Offset: 0x00043B0C
		// (set) Token: 0x06001132 RID: 4402 RVA: 0x00045914 File Offset: 0x00043B14
		[DataSourceProperty]
		public BasicTooltipViewModel RecruitAllHint
		{
			get
			{
				return this._recruitAllHint;
			}
			set
			{
				if (value != this._recruitAllHint)
				{
					this._recruitAllHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "RecruitAllHint");
				}
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x00045932 File Offset: 0x00043B32
		// (set) Token: 0x06001134 RID: 4404 RVA: 0x0004593A File Offset: 0x00043B3A
		[DataSourceProperty]
		public int PartyWage
		{
			get
			{
				return this._partyWage;
			}
			set
			{
				if (value != this._partyWage)
				{
					this._partyWage = value;
					base.OnPropertyChangedWithValue(value, "PartyWage");
				}
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x00045958 File Offset: 0x00043B58
		// (set) Token: 0x06001136 RID: 4406 RVA: 0x00045960 File Offset: 0x00043B60
		[DataSourceProperty]
		public string PartyCapacityText
		{
			get
			{
				return this._partyCapacityText;
			}
			set
			{
				if (value != this._partyCapacityText)
				{
					this._partyCapacityText = value;
					base.OnPropertyChangedWithValue<string>(value, "PartyCapacityText");
				}
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x00045983 File Offset: 0x00043B83
		// (set) Token: 0x06001138 RID: 4408 RVA: 0x0004598B File Offset: 0x00043B8B
		[DataSourceProperty]
		public string PartyWageText
		{
			get
			{
				return this._partyWageText;
			}
			set
			{
				if (value != this._partyWageText)
				{
					this._partyWageText = value;
					base.OnPropertyChangedWithValue<string>(value, "PartyWageText");
				}
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x000459AE File Offset: 0x00043BAE
		// (set) Token: 0x0600113A RID: 4410 RVA: 0x000459B6 File Offset: 0x00043BB6
		[DataSourceProperty]
		public string RecruitAllText
		{
			get
			{
				return this._recruitAllText;
			}
			set
			{
				if (value != this._recruitAllText)
				{
					this._recruitAllText = value;
					base.OnPropertyChangedWithValue<string>(value, "RecruitAllText");
				}
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x000459D9 File Offset: 0x00043BD9
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x000459E1 File Offset: 0x00043BE1
		[DataSourceProperty]
		public string PartySpeedText
		{
			get
			{
				return this._partySpeedText;
			}
			set
			{
				if (value != this._partySpeedText)
				{
					this._partySpeedText = value;
					base.OnPropertyChangedWithValue<string>(value, "PartySpeedText");
				}
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x00045A04 File Offset: 0x00043C04
		// (set) Token: 0x0600113E RID: 4414 RVA: 0x00045A0C File Offset: 0x00043C0C
		[DataSourceProperty]
		public string ResetAllText
		{
			get
			{
				return this._resetAllText;
			}
			set
			{
				if (value != this._resetAllText)
				{
					this._resetAllText = value;
					base.OnPropertyChangedWithValue<string>(value, "ResetAllText");
				}
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x00045A2F File Offset: 0x00043C2F
		// (set) Token: 0x06001140 RID: 4416 RVA: 0x00045A37 File Offset: 0x00043C37
		[DataSourceProperty]
		public string CancelText
		{
			get
			{
				return this._cancelText;
			}
			set
			{
				if (value != this._cancelText)
				{
					this._cancelText = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelText");
				}
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x00045A5A File Offset: 0x00043C5A
		// (set) Token: 0x06001142 RID: 4418 RVA: 0x00045A62 File Offset: 0x00043C62
		[DataSourceProperty]
		public string RemainingFoodText
		{
			get
			{
				return this._remainingFoodText;
			}
			set
			{
				if (value != this._remainingFoodText)
				{
					this._remainingFoodText = value;
					base.OnPropertyChangedWithValue<string>(value, "RemainingFoodText");
				}
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x00045A85 File Offset: 0x00043C85
		// (set) Token: 0x06001144 RID: 4420 RVA: 0x00045A8D File Offset: 0x00043C8D
		[DataSourceProperty]
		public string TotalCostText
		{
			get
			{
				return this._totalCostText;
			}
			set
			{
				if (value != this._totalCostText)
				{
					this._totalCostText = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalCostText");
				}
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x00045AB0 File Offset: 0x00043CB0
		// (set) Token: 0x06001146 RID: 4422 RVA: 0x00045AB8 File Offset: 0x00043CB8
		[DataSourceProperty]
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if (value != this._enabled)
				{
					this._enabled = value;
					base.OnPropertyChangedWithValue(value, "Enabled");
				}
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x00045AD8 File Offset: 0x00043CD8
		// (set) Token: 0x06001148 RID: 4424 RVA: 0x00045AE0 File Offset: 0x00043CE0
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

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x00045AFE File Offset: 0x00043CFE
		// (set) Token: 0x0600114A RID: 4426 RVA: 0x00045B06 File Offset: 0x00043D06
		[DataSourceProperty]
		public bool IsPartyCapacityWarningEnabled
		{
			get
			{
				return this._isPartyCapacityWarningEnabled;
			}
			set
			{
				if (value != this._isPartyCapacityWarningEnabled)
				{
					this._isPartyCapacityWarningEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsPartyCapacityWarningEnabled");
				}
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x00045B24 File Offset: 0x00043D24
		// (set) Token: 0x0600114C RID: 4428 RVA: 0x00045B2C File Offset: 0x00043D2C
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x00045B4F File Offset: 0x00043D4F
		// (set) Token: 0x0600114E RID: 4430 RVA: 0x00045B57 File Offset: 0x00043D57
		[DataSourceProperty]
		public string DoneText
		{
			get
			{
				return this._doneText;
			}
			set
			{
				if (value != this._doneText)
				{
					this._doneText = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneText");
				}
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x00045B7A File Offset: 0x00043D7A
		// (set) Token: 0x06001150 RID: 4432 RVA: 0x00045B82 File Offset: 0x00043D82
		[DataSourceProperty]
		public bool CanRecruitAll
		{
			get
			{
				return this._canRecruitAll;
			}
			set
			{
				if (value != this._canRecruitAll)
				{
					this._canRecruitAll = value;
					base.OnPropertyChangedWithValue(value, "CanRecruitAll");
				}
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x00045BA0 File Offset: 0x00043DA0
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x00045BA8 File Offset: 0x00043DA8
		[DataSourceProperty]
		public int TotalWealth
		{
			get
			{
				return this._totalWealth;
			}
			set
			{
				if (value != this._totalWealth)
				{
					this._totalWealth = value;
					base.OnPropertyChangedWithValue(value, "TotalWealth");
				}
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x00045BC6 File Offset: 0x00043DC6
		// (set) Token: 0x06001154 RID: 4436 RVA: 0x00045BCE File Offset: 0x00043DCE
		[DataSourceProperty]
		public int PartyCapacity
		{
			get
			{
				return this._partyCapacity;
			}
			set
			{
				if (value != this._partyCapacity)
				{
					this._partyCapacity = value;
					base.OnPropertyChangedWithValue(value, "PartyCapacity");
				}
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x00045BEC File Offset: 0x00043DEC
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x00045BF4 File Offset: 0x00043DF4
		[DataSourceProperty]
		public int InitialPartySize
		{
			get
			{
				return this._initialPartySize;
			}
			set
			{
				if (value != this._initialPartySize)
				{
					this._initialPartySize = value;
					base.OnPropertyChangedWithValue(value, "InitialPartySize");
				}
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x00045C12 File Offset: 0x00043E12
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x00045C1A File Offset: 0x00043E1A
		[DataSourceProperty]
		public int CurrentPartySize
		{
			get
			{
				return this._currentPartySize;
			}
			set
			{
				if (value != this._currentPartySize)
				{
					this._currentPartySize = value;
					base.OnPropertyChangedWithValue(value, "CurrentPartySize");
				}
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x00045C38 File Offset: 0x00043E38
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x00045C40 File Offset: 0x00043E40
		[DataSourceProperty]
		public MBBindingList<RecruitVolunteerVM> VolunteerList
		{
			get
			{
				return this._volunteerList;
			}
			set
			{
				if (value != this._volunteerList)
				{
					this._volunteerList = value;
					base.OnPropertyChangedWithValue<MBBindingList<RecruitVolunteerVM>>(value, "VolunteerList");
				}
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x00045C5E File Offset: 0x00043E5E
		// (set) Token: 0x0600115C RID: 4444 RVA: 0x00045C66 File Offset: 0x00043E66
		[DataSourceProperty]
		public MBBindingList<RecruitVolunteerTroopVM> TroopsInCart
		{
			get
			{
				return this._troopsInCart;
			}
			set
			{
				if (value != this._troopsInCart)
				{
					this._troopsInCart = value;
					base.OnPropertyChangedWithValue<MBBindingList<RecruitVolunteerTroopVM>>(value, "TroopsInCart");
				}
			}
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00045C84 File Offset: 0x00043E84
		public void SetGetKeyTextFromKeyIDFunc(Func<string, TextObject> getKeyTextFromKeyId)
		{
			this._getKeyTextFromKeyId = getKeyTextFromKeyId;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00045C8D File Offset: 0x00043E8D
		private string GetRecruitAllKey()
		{
			if (this.RecruitAllInputKey == null || this._getKeyTextFromKeyId == null)
			{
				return string.Empty;
			}
			return this._getKeyTextFromKeyId(this.RecruitAllInputKey.KeyID).ToString();
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00045CC0 File Offset: 0x00043EC0
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00045CCF File Offset: 0x00043ECF
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00045CDE File Offset: 0x00043EDE
		public void SetRecruitAllInputKey(HotKey hotKey)
		{
			this.RecruitAllInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
			this.SetRecruitAllHint();
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00045CF3 File Offset: 0x00043EF3
		public void SetResetInputKey(HotKey hotKey)
		{
			this.ResetInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x00045D02 File Offset: 0x00043F02
		// (set) Token: 0x06001164 RID: 4452 RVA: 0x00045D0A File Offset: 0x00043F0A
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

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x00045D28 File Offset: 0x00043F28
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x00045D30 File Offset: 0x00043F30
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

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00045D4E File Offset: 0x00043F4E
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x00045D56 File Offset: 0x00043F56
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

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x00045D74 File Offset: 0x00043F74
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x00045D7C File Offset: 0x00043F7C
		[DataSourceProperty]
		public InputKeyItemVM RecruitAllInputKey
		{
			get
			{
				return this._recruitAllInputKey;
			}
			set
			{
				if (value != this._recruitAllInputKey)
				{
					this._recruitAllInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "RecruitAllInputKey");
				}
			}
		}

		// Token: 0x040007C8 RID: 1992
		private TextObject _recruitAllTextObject;

		// Token: 0x040007C9 RID: 1993
		private string _playerDoesntHaveEnoughMoneyStr;

		// Token: 0x040007CA RID: 1994
		private string _playerIsOverPartyLimitStr;

		// Token: 0x040007CB RID: 1995
		private Func<string, TextObject> _getKeyTextFromKeyId;

		// Token: 0x040007CC RID: 1996
		private bool _isAvailableTroopsHighlightApplied;

		// Token: 0x040007CD RID: 1997
		private string _latestTutorialElementID;

		// Token: 0x040007CE RID: 1998
		private bool _enabled;

		// Token: 0x040007CF RID: 1999
		private bool _isDoneEnabled;

		// Token: 0x040007D0 RID: 2000
		private bool _isPartyCapacityWarningEnabled;

		// Token: 0x040007D1 RID: 2001
		private bool _canRecruitAll;

		// Token: 0x040007D2 RID: 2002
		private string _titleText;

		// Token: 0x040007D3 RID: 2003
		private string _doneText;

		// Token: 0x040007D4 RID: 2004
		private string _recruitAllText;

		// Token: 0x040007D5 RID: 2005
		private string _resetAllText;

		// Token: 0x040007D6 RID: 2006
		private string _cancelText;

		// Token: 0x040007D7 RID: 2007
		private int _totalWealth;

		// Token: 0x040007D8 RID: 2008
		private int _partyCapacity;

		// Token: 0x040007D9 RID: 2009
		private int _initialPartySize;

		// Token: 0x040007DA RID: 2010
		private int _currentPartySize;

		// Token: 0x040007DB RID: 2011
		private MBBindingList<RecruitVolunteerVM> _volunteerList;

		// Token: 0x040007DC RID: 2012
		private MBBindingList<RecruitVolunteerTroopVM> _troopsInCart;

		// Token: 0x040007DD RID: 2013
		private int _partyWage;

		// Token: 0x040007DE RID: 2014
		private string _partyCapacityText = "";

		// Token: 0x040007DF RID: 2015
		private string _partyWageText = "";

		// Token: 0x040007E0 RID: 2016
		private string _partySpeedText = "";

		// Token: 0x040007E1 RID: 2017
		private string _remainingFoodText = "";

		// Token: 0x040007E2 RID: 2018
		private string _totalCostText = "";

		// Token: 0x040007E3 RID: 2019
		private RecruitVolunteerTroopVM _focusedVolunteerTroop;

		// Token: 0x040007E4 RID: 2020
		private RecruitVolunteerOwnerVM _focusedVolunteerOwner;

		// Token: 0x040007E5 RID: 2021
		private HintViewModel _partyWageHint;

		// Token: 0x040007E6 RID: 2022
		private HintViewModel _partyCapacityHint;

		// Token: 0x040007E7 RID: 2023
		private BasicTooltipViewModel _partySpeedHint;

		// Token: 0x040007E8 RID: 2024
		private HintViewModel _remainingFoodHint;

		// Token: 0x040007E9 RID: 2025
		private HintViewModel _totalWealthHint;

		// Token: 0x040007EA RID: 2026
		private HintViewModel _totalCostHint;

		// Token: 0x040007EB RID: 2027
		private HintViewModel _resetHint;

		// Token: 0x040007EC RID: 2028
		private HintViewModel _doneHint;

		// Token: 0x040007ED RID: 2029
		private BasicTooltipViewModel _recruitAllHint;

		// Token: 0x040007EE RID: 2030
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x040007EF RID: 2031
		private InputKeyItemVM _doneInputKey;

		// Token: 0x040007F0 RID: 2032
		private InputKeyItemVM _resetInputKey;

		// Token: 0x040007F1 RID: 2033
		private InputKeyItemVM _recruitAllInputKey;
	}
}
