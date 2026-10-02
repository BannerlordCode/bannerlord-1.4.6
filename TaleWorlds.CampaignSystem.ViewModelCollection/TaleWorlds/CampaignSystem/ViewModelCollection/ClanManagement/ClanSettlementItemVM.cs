using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x0200012F RID: 303
	public class ClanSettlementItemVM : ViewModel
	{
		// Token: 0x06001C5A RID: 7258 RVA: 0x00068EF4 File Offset: 0x000670F4
		public ClanSettlementItemVM(Settlement settlement, Action<ClanSettlementItemVM> onSelection, Action onShowSendMembers, ITeleportationCampaignBehavior teleportationBehavior)
		{
			this.Settlement = settlement;
			this._onSelection = onSelection;
			this._onShowSendMembers = onShowSendMembers;
			this._teleportationBehavior = teleportationBehavior;
			this.IsFortification = settlement.IsFortification;
			SettlementComponent settlementComponent = settlement.SettlementComponent;
			this.FileName = ((settlementComponent == null) ? "placeholder" : (settlementComponent.BackgroundMeshName + "_t"));
			this.ItemProperties = new MBBindingList<SelectableFiefItemPropertyVM>();
			this.ProfitItemProperties = new MBBindingList<ProfitItemPropertyVM>();
			this.TotalProfit = new ProfitItemPropertyVM(GameTexts.FindText("str_profit", null).ToString(), 0, ProfitItemPropertyVM.PropertyType.None, null, null);
			this.ImageName = ((settlementComponent != null) ? settlementComponent.WaitMeshName : "");
			this.VillagesOwned = new MBBindingList<ClanSettlementItemVM>();
			this.Notables = new MBBindingList<HeroVM>();
			this.Members = new MBBindingList<HeroVM>();
			this._patrolsBehavior = Campaign.Current.GetCampaignBehavior<IPatrolPartiesCampaignBehavior>();
			this.RefreshValues();
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x00068FDC File Offset: 0x000671DC
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.VillagesText = GameTexts.FindText("str_villages", null).ToString();
			this.NotablesText = GameTexts.FindText("str_center_notables", null).ToString();
			this.MembersText = GameTexts.FindText("str_members", null).ToString();
			this.Name = this.Settlement.Name.ToString();
			this.UpdateProperties();
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x0006904D File Offset: 0x0006724D
		protected virtual ClanSettlementItemVM CreateSettlementItem(Settlement settlement, Action<ClanSettlementItemVM> onSelection, Action onShowSendMembers, ITeleportationCampaignBehavior teleportationBehavior)
		{
			return new ClanSettlementItemVM(settlement, onSelection, onShowSendMembers, teleportationBehavior);
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x00069059 File Offset: 0x00067259
		public void OnSettlementSelection()
		{
			this._onSelection(this);
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x00069067 File Offset: 0x00067267
		public void ExecuteLink()
		{
			MBInformationManager.HideInformations();
			Campaign.Current.EncyclopediaManager.GoToLink(this.Settlement.EncyclopediaLink);
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x00069088 File Offset: 0x00067288
		public void ExecuteCloseTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x0006908F File Offset: 0x0006728F
		public void ExecuteOpenTooltip()
		{
			InformationManager.ShowTooltip(typeof(Settlement), new object[] { this.Settlement });
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x000690AF File Offset: 0x000672AF
		public void ExecuteSendMembers()
		{
			Action onShowSendMembers = this._onShowSendMembers;
			if (onShowSendMembers == null)
			{
				return;
			}
			onShowSendMembers();
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x000690C1 File Offset: 0x000672C1
		private void OnGovernorChanged(Hero oldHero, Hero newHero)
		{
			ChangeGovernorAction.Apply(this.Settlement.Town, newHero);
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x000690D4 File Offset: 0x000672D4
		private bool IsGovernorAssignable(Hero oldHero, Hero newHero)
		{
			return newHero.IsActive && newHero.GovernorOf == null;
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x000690EC File Offset: 0x000672EC
		protected virtual void UpdateProperties()
		{
			this.ItemProperties.Clear();
			this.VillagesOwned.Clear();
			this.Notables.Clear();
			this.Members.Clear();
			foreach (Village village in this.Settlement.BoundVillages)
			{
				this.VillagesOwned.Add(this.CreateSettlementItem(village.Settlement, null, null, null));
			}
			this.HasNotables = !this.Settlement.Notables.IsEmpty<Hero>();
			foreach (Hero hero in this.Settlement.Notables)
			{
				this.Notables.Add(new HeroVM(hero, false));
			}
			foreach (Hero hero2 in this.Settlement.HeroesWithoutParty.Where<Hero>((Hero h) => h.Clan == Clan.PlayerClan))
			{
				this.Members.Add(new HeroVM(hero2, false));
			}
			this.HasGovernor = false;
			if (!this.Settlement.IsVillage)
			{
				Town town = this.Settlement.Town;
				Hero hero3 = ((((town != null) ? town.Governor : null) != null) ? this.Settlement.Town.Governor : CampaignUIHelper.GetTeleportingGovernor(this.Settlement, this._teleportationBehavior));
				this.HasGovernor = hero3 != null;
				this.Governor = (this.HasGovernor ? new HeroVM(hero3, false) : null);
			}
			this.IsFortification = this.Settlement.IsFortification;
			if (this.Settlement.Town != null)
			{
				BasicTooltipViewModel basicTooltipViewModel = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownWallsTooltip(this.Settlement.Town));
				this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_walls", null).ToString(), this.Settlement.Town.GetWallLevel().ToString(), 0, SelectableItemPropertyVM.PropertyType.Wall, basicTooltipViewModel, false));
			}
			if (this.Settlement.Town != null)
			{
				BasicTooltipViewModel basicTooltipViewModel2 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownGarrisonTooltip(this.Settlement.Town));
				int num = (int)SettlementHelper.GetGarrisonChangeExplainedNumber(this.Settlement.Town).ResultNumber;
				Collection<SelectableFiefItemPropertyVM> itemProperties = this.ItemProperties;
				string text = GameTexts.FindText("str_garrison", null).ToString();
				MobileParty garrisonParty = this.Settlement.Town.GarrisonParty;
				itemProperties.Add(new SelectableFiefItemPropertyVM(text, ((garrisonParty != null) ? garrisonParty.Party.NumberOfAllMembers.ToString() : null) ?? "0", num, SelectableItemPropertyVM.PropertyType.Garrison, basicTooltipViewModel2, false));
			}
			int num2 = (int)this.Settlement.Militia;
			List<TooltipProperty> militiaHint = (this.Settlement.IsVillage ? CampaignUIHelper.GetVillageMilitiaTooltip(this.Settlement.Village) : CampaignUIHelper.GetTownMilitiaTooltip(this.Settlement.Town));
			int num3 = ((this.Settlement.Town != null) ? ((int)this.Settlement.Town.MilitiaChange) : ((int)this.Settlement.Village.MilitiaChange));
			this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_militia", null).ToString(), num2.ToString(), num3, SelectableItemPropertyVM.PropertyType.Militia, new BasicTooltipViewModel(() => militiaHint), false));
			if (this.Settlement.Town != null)
			{
				BasicTooltipViewModel basicTooltipViewModel3 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownFoodTooltip(this.Settlement.Town));
				int num4 = (int)this.Settlement.Town.FoodChange;
				this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_food_stocks", null).ToString(), ((int)this.Settlement.Town.FoodStocks).ToString(), num4, SelectableItemPropertyVM.PropertyType.Food, basicTooltipViewModel3, false));
			}
			if (this.Settlement.IsFortification)
			{
				int num5 = ((this.Settlement.Town != null) ? ((int)this.Settlement.Town.ProsperityChange) : ((int)this.Settlement.Village.HearthChange));
				BasicTooltipViewModel basicTooltipViewModel4;
				if (this.Settlement.Town != null)
				{
					basicTooltipViewModel4 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownProsperityTooltip(this.Settlement.Town));
				}
				else
				{
					basicTooltipViewModel4 = new BasicTooltipViewModel(() => CampaignUIHelper.GetVillageProsperityTooltip(this.Settlement.Village));
				}
				this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_prosperity", null).ToString(), string.Format("{0:0.##}", this.Settlement.Town.Prosperity), num5, SelectableItemPropertyVM.PropertyType.Prosperity, basicTooltipViewModel4, false));
			}
			if (this.Settlement.Town != null)
			{
				BasicTooltipViewModel basicTooltipViewModel5 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownLoyaltyTooltip(this.Settlement.Town));
				int num6 = (int)this.Settlement.Town.LoyaltyChange;
				bool flag = this.Settlement.IsTown && this.Settlement.Town.Loyalty < (float)Campaign.Current.Models.SettlementLoyaltyModel.RebelliousStateStartLoyaltyThreshold;
				this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_loyalty", null).ToString(), string.Format("{0:0.#}", this.Settlement.Town.Loyalty), num6, SelectableItemPropertyVM.PropertyType.Loyalty, basicTooltipViewModel5, flag));
			}
			if (this.Settlement.Town != null)
			{
				BasicTooltipViewModel basicTooltipViewModel6 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownSecurityTooltip(this.Settlement.Town));
				int num7 = (int)this.Settlement.Town.SecurityChange;
				this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_security", null).ToString(), string.Format("{0:0.#}", this.Settlement.Town.Security), num7, SelectableItemPropertyVM.PropertyType.Security, basicTooltipViewModel6, false));
			}
			if (this.Settlement.IsTown)
			{
				BasicTooltipViewModel basicTooltipViewModel7 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownPatrolTooltip(this.Settlement.Town));
				this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_patrol", null).ToString(), this._patrolsBehavior.GetSettlementPatrolStatus(this.Settlement).ToString(), 0, SelectableItemPropertyVM.PropertyType.Patrol, basicTooltipViewModel7, false));
			}
			TextObject textObject;
			this.IsSendMembersEnabled = CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out textObject);
			TextObject textObject2 = new TextObject("{=PTGsYoPc}Assign your clan members to {SETTLEMENT_NAME}", null);
			textObject2.SetTextVariable("SETTLEMENT_NAME", this.Settlement.Name.ToString());
			this.SendMembersHint = new HintViewModel(this.IsSendMembersEnabled ? textObject2 : textObject, null);
			this.UpdateProfitProperties();
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x000697B4 File Offset: 0x000679B4
		protected virtual void UpdateProfitProperties()
		{
			this.ProfitItemProperties.Clear();
			if (this.Settlement.Town != null)
			{
				Town town = this.Settlement.Town;
				ClanFinanceModel clanFinanceModel = Campaign.Current.Models.ClanFinanceModel;
				int num = 0;
				int num2 = (int)Campaign.Current.Models.SettlementTaxModel.CalculateTownTax(town, false).ResultNumber;
				int num3 = (int)clanFinanceModel.CalculateTownIncomeFromTariffs(Clan.PlayerClan, town, false).ResultNumber;
				int num4 = clanFinanceModel.CalculateTownIncomeFromProjects(town);
				if (num2 != 0)
				{
					this.ProfitItemProperties.Add(new ProfitItemPropertyVM(new TextObject("{=qeclv74c}Taxes", null).ToString(), num2, ProfitItemPropertyVM.PropertyType.Tax, null, null));
					num += num2;
				}
				if (num3 != 0)
				{
					this.ProfitItemProperties.Add(new ProfitItemPropertyVM(new TextObject("{=eIgC6YGp}Tariffs", null).ToString(), num3, ProfitItemPropertyVM.PropertyType.Tariff, null, null));
					num += num3;
				}
				if (town.GarrisonParty != null && town.GarrisonParty.IsActive)
				{
					int totalWage = town.GarrisonParty.TotalWage;
					if (totalWage != 0)
					{
						this.ProfitItemProperties.Add(new ProfitItemPropertyVM(new TextObject("{=5dkPxmZG}Garrison Wages", null).ToString(), -totalWage, ProfitItemPropertyVM.PropertyType.Garrison, null, null));
						num -= totalWage;
					}
				}
				foreach (Village village in town.Villages)
				{
					int num5 = clanFinanceModel.CalculateVillageIncome(Clan.PlayerClan, village, false);
					if (num5 != 0)
					{
						this.ProfitItemProperties.Add(new ProfitItemPropertyVM(village.Name.ToString(), num5, ProfitItemPropertyVM.PropertyType.Village, null, null));
						num += num5;
					}
				}
				if (num4 != 0)
				{
					Collection<ProfitItemPropertyVM> profitItemProperties = this.ProfitItemProperties;
					string text = new TextObject("{=J8ddrAOf}Governor Effects", null).ToString();
					int num6 = num4;
					ProfitItemPropertyVM.PropertyType propertyType = ProfitItemPropertyVM.PropertyType.Governor;
					HeroVM governor = this.Governor;
					profitItemProperties.Add(new ProfitItemPropertyVM(text, num6, propertyType, (governor != null) ? governor.ImageIdentifier : null, null));
					num += num4;
				}
				this.TotalProfit.Value = num;
			}
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x000699B0 File Offset: 0x00067BB0
		private bool IsSettlementSlotAssignable(Hero oldHero, Hero newHero)
		{
			return (oldHero == null || !oldHero.IsHumanPlayerCharacter) && !newHero.IsHumanPlayerCharacter && newHero.IsActive && (newHero.PartyBelongedTo == null || newHero.PartyBelongedTo.LeaderHero != newHero) && newHero.PartyBelongedToAsPrisoner == null;
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x000699FF File Offset: 0x00067BFF
		private void ExecuteOpenSettlementPage()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this.Settlement.EncyclopediaLink);
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x00069A1B File Offset: 0x00067C1B
		// (set) Token: 0x06001C69 RID: 7273 RVA: 0x00069A23 File Offset: 0x00067C23
		[DataSourceProperty]
		public HeroVM Governor
		{
			get
			{
				return this._governor;
			}
			set
			{
				if (value != this._governor)
				{
					this._governor = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "Governor");
				}
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x00069A41 File Offset: 0x00067C41
		// (set) Token: 0x06001C6B RID: 7275 RVA: 0x00069A49 File Offset: 0x00067C49
		[DataSourceProperty]
		public MBBindingList<SelectableFiefItemPropertyVM> ItemProperties
		{
			get
			{
				return this._itemProperties;
			}
			set
			{
				if (value != this._itemProperties)
				{
					this._itemProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<SelectableFiefItemPropertyVM>>(value, "ItemProperties");
				}
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x00069A67 File Offset: 0x00067C67
		// (set) Token: 0x06001C6D RID: 7277 RVA: 0x00069A6F File Offset: 0x00067C6F
		[DataSourceProperty]
		public MBBindingList<ProfitItemPropertyVM> ProfitItemProperties
		{
			get
			{
				return this._profitItemProperties;
			}
			set
			{
				if (value != this._profitItemProperties)
				{
					this._profitItemProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<ProfitItemPropertyVM>>(value, "ProfitItemProperties");
				}
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x00069A8D File Offset: 0x00067C8D
		// (set) Token: 0x06001C6F RID: 7279 RVA: 0x00069A95 File Offset: 0x00067C95
		[DataSourceProperty]
		public ProfitItemPropertyVM TotalProfit
		{
			get
			{
				return this._totalProfit;
			}
			set
			{
				if (value != this._totalProfit)
				{
					this._totalProfit = value;
					base.OnPropertyChangedWithValue<ProfitItemPropertyVM>(value, "TotalProfit");
				}
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x00069AB3 File Offset: 0x00067CB3
		// (set) Token: 0x06001C71 RID: 7281 RVA: 0x00069ABB File Offset: 0x00067CBB
		[DataSourceProperty]
		public string FileName
		{
			get
			{
				return this._fileName;
			}
			set
			{
				if (value != this._fileName)
				{
					this._fileName = value;
					base.OnPropertyChangedWithValue<string>(value, "FileName");
				}
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x00069ADE File Offset: 0x00067CDE
		// (set) Token: 0x06001C73 RID: 7283 RVA: 0x00069AE6 File Offset: 0x00067CE6
		[DataSourceProperty]
		public string ImageName
		{
			get
			{
				return this._imageName;
			}
			set
			{
				if (value != this._imageName)
				{
					this._imageName = value;
					base.OnPropertyChangedWithValue<string>(value, "ImageName");
				}
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x00069B09 File Offset: 0x00067D09
		// (set) Token: 0x06001C75 RID: 7285 RVA: 0x00069B11 File Offset: 0x00067D11
		[DataSourceProperty]
		public string VillagesText
		{
			get
			{
				return this._villagesText;
			}
			set
			{
				if (value != this._villagesText)
				{
					this._villagesText = value;
					base.OnPropertyChangedWithValue<string>(value, "VillagesText");
				}
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x00069B34 File Offset: 0x00067D34
		// (set) Token: 0x06001C77 RID: 7287 RVA: 0x00069B3C File Offset: 0x00067D3C
		[DataSourceProperty]
		public string NotablesText
		{
			get
			{
				return this._notablesText;
			}
			set
			{
				if (value != this._notablesText)
				{
					this._notablesText = value;
					base.OnPropertyChangedWithValue<string>(value, "NotablesText");
				}
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x00069B5F File Offset: 0x00067D5F
		// (set) Token: 0x06001C79 RID: 7289 RVA: 0x00069B67 File Offset: 0x00067D67
		[DataSourceProperty]
		public string MembersText
		{
			get
			{
				return this._membersText;
			}
			set
			{
				if (value != this._membersText)
				{
					this._membersText = value;
					base.OnPropertyChangedWithValue<string>(value, "MembersText");
				}
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x00069B8A File Offset: 0x00067D8A
		// (set) Token: 0x06001C7B RID: 7291 RVA: 0x00069B92 File Offset: 0x00067D92
		[DataSourceProperty]
		public bool IsFortification
		{
			get
			{
				return this._isFortification;
			}
			set
			{
				if (value != this._isFortification)
				{
					this._isFortification = value;
					base.OnPropertyChangedWithValue(value, "IsFortification");
				}
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x00069BB0 File Offset: 0x00067DB0
		// (set) Token: 0x06001C7D RID: 7293 RVA: 0x00069BB8 File Offset: 0x00067DB8
		[DataSourceProperty]
		public bool HasGovernor
		{
			get
			{
				return this._hasGovernor;
			}
			set
			{
				if (value != this._hasGovernor)
				{
					this._hasGovernor = value;
					base.OnPropertyChangedWithValue(value, "HasGovernor");
				}
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x00069BD6 File Offset: 0x00067DD6
		// (set) Token: 0x06001C7F RID: 7295 RVA: 0x00069BDE File Offset: 0x00067DDE
		[DataSourceProperty]
		public bool HasNotables
		{
			get
			{
				return this._hasNotables;
			}
			set
			{
				if (value != this._hasNotables)
				{
					this._hasNotables = value;
					base.OnPropertyChangedWithValue(value, "HasNotables");
				}
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x00069BFC File Offset: 0x00067DFC
		// (set) Token: 0x06001C81 RID: 7297 RVA: 0x00069C04 File Offset: 0x00067E04
		[DataSourceProperty]
		public bool IsSendMembersEnabled
		{
			get
			{
				return this._isSendMembersEnabled;
			}
			set
			{
				if (value != this._isSendMembersEnabled)
				{
					this._isSendMembersEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsSendMembersEnabled");
				}
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x00069C22 File Offset: 0x00067E22
		// (set) Token: 0x06001C83 RID: 7299 RVA: 0x00069C2A File Offset: 0x00067E2A
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

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x00069C48 File Offset: 0x00067E48
		// (set) Token: 0x06001C85 RID: 7301 RVA: 0x00069C50 File Offset: 0x00067E50
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

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x00069C73 File Offset: 0x00067E73
		// (set) Token: 0x06001C87 RID: 7303 RVA: 0x00069C7B File Offset: 0x00067E7B
		[DataSourceProperty]
		public MBBindingList<ClanSettlementItemVM> VillagesOwned
		{
			get
			{
				return this._villagesOwned;
			}
			set
			{
				if (value != this._villagesOwned)
				{
					this._villagesOwned = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanSettlementItemVM>>(value, "VillagesOwned");
				}
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x00069C99 File Offset: 0x00067E99
		// (set) Token: 0x06001C89 RID: 7305 RVA: 0x00069CA1 File Offset: 0x00067EA1
		[DataSourceProperty]
		public MBBindingList<HeroVM> Notables
		{
			get
			{
				return this._notables;
			}
			set
			{
				if (value != this._notables)
				{
					this._notables = value;
					base.OnPropertyChangedWithValue<MBBindingList<HeroVM>>(value, "Notables");
				}
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x00069CBF File Offset: 0x00067EBF
		// (set) Token: 0x06001C8B RID: 7307 RVA: 0x00069CC7 File Offset: 0x00067EC7
		[DataSourceProperty]
		public MBBindingList<HeroVM> Members
		{
			get
			{
				return this._members;
			}
			set
			{
				if (value != this._members)
				{
					this._members = value;
					base.OnPropertyChangedWithValue<MBBindingList<HeroVM>>(value, "Members");
				}
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x00069CE5 File Offset: 0x00067EE5
		// (set) Token: 0x06001C8D RID: 7309 RVA: 0x00069CED File Offset: 0x00067EED
		[DataSourceProperty]
		public HintViewModel SendMembersHint
		{
			get
			{
				return this._sendMembersHint;
			}
			set
			{
				if (value != this._sendMembersHint)
				{
					this._sendMembersHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "SendMembersHint");
				}
			}
		}

		// Token: 0x04000D3A RID: 3386
		private readonly Action<ClanSettlementItemVM> _onSelection;

		// Token: 0x04000D3B RID: 3387
		private readonly Action _onShowSendMembers;

		// Token: 0x04000D3C RID: 3388
		private readonly ITeleportationCampaignBehavior _teleportationBehavior;

		// Token: 0x04000D3D RID: 3389
		private readonly IPatrolPartiesCampaignBehavior _patrolsBehavior;

		// Token: 0x04000D3E RID: 3390
		public readonly Settlement Settlement;

		// Token: 0x04000D3F RID: 3391
		private string _name;

		// Token: 0x04000D40 RID: 3392
		private HeroVM _governor;

		// Token: 0x04000D41 RID: 3393
		private string _fileName;

		// Token: 0x04000D42 RID: 3394
		private string _imageName;

		// Token: 0x04000D43 RID: 3395
		private string _villagesText;

		// Token: 0x04000D44 RID: 3396
		private string _notablesText;

		// Token: 0x04000D45 RID: 3397
		private string _membersText;

		// Token: 0x04000D46 RID: 3398
		private bool _isFortification;

		// Token: 0x04000D47 RID: 3399
		private bool _isSelected;

		// Token: 0x04000D48 RID: 3400
		private bool _hasGovernor;

		// Token: 0x04000D49 RID: 3401
		private bool _hasNotables;

		// Token: 0x04000D4A RID: 3402
		private bool _isSendMembersEnabled;

		// Token: 0x04000D4B RID: 3403
		private MBBindingList<SelectableFiefItemPropertyVM> _itemProperties;

		// Token: 0x04000D4C RID: 3404
		private MBBindingList<ProfitItemPropertyVM> _profitItemProperties;

		// Token: 0x04000D4D RID: 3405
		private ProfitItemPropertyVM _totalProfit;

		// Token: 0x04000D4E RID: 3406
		private MBBindingList<ClanSettlementItemVM> _villagesOwned;

		// Token: 0x04000D4F RID: 3407
		private MBBindingList<HeroVM> _notables;

		// Token: 0x04000D50 RID: 3408
		private MBBindingList<HeroVM> _members;

		// Token: 0x04000D51 RID: 3409
		private HintViewModel _sendMembersHint;
	}
}
