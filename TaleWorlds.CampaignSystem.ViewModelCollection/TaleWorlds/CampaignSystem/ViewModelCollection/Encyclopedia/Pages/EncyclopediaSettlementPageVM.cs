using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000D8 RID: 216
	[EncyclopediaViewModel(typeof(Settlement))]
	public class EncyclopediaSettlementPageVM : EncyclopediaContentPageVM
	{
		// Token: 0x06001499 RID: 5273 RVA: 0x00052034 File Offset: 0x00050234
		public EncyclopediaSettlementPageVM(EncyclopediaPageArgs args)
			: base(args)
		{
			this._settlement = base.Obj as Settlement;
			this.NotableCharacters = new MBBindingList<HeroVM>();
			this.Settlements = new MBBindingList<EncyclopediaSettlementVM>();
			this.History = new MBBindingList<EncyclopediaHistoryEventVM>();
			this._isVisualTrackerSelected = Campaign.Current.VisualTrackerManager.CheckTracked(this._settlement);
			this.IsFortification = this._settlement.IsFortification;
			this.SettlementImageID = this._settlement.SettlementComponent.WaitMeshName;
			base.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(this._settlement);
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			this.RefreshValues();
			TextObject textObject;
			if (CampaignUIHelper.IsSettlementInformationHidden(this._settlement, out textObject))
			{
				Game.Current.EventManager.TriggerEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedEvent(EncyclopediaPages.Settlement, true));
			}
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00052124 File Offset: 0x00050324
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.SettlementName = this._settlement.Name.ToString();
			this.SettlementsText = GameTexts.FindText("str_villages", null).ToString();
			this.NotableCharactersText = GameTexts.FindText("str_notable_characters", null).ToString();
			this.OwnerText = GameTexts.FindText("str_owner", null).ToString();
			this.TrackText = GameTexts.FindText("str_settlement_track", null).ToString();
			this.ShowInMapHint = new HintViewModel(GameTexts.FindText("str_show_on_map", null), null);
			this.InformationText = this._settlement.EncyclopediaText.ToString();
			base.UpdateBookmarkHintText();
			this.Refresh();
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x000521E0 File Offset: 0x000503E0
		public override void Refresh()
		{
			base.IsLoadingOver = false;
			SettlementComponent settlementComponent = this._settlement.SettlementComponent;
			this.NotableCharacters.Clear();
			this.Settlements.Clear();
			this.History.Clear();
			this.IsFortification = this._settlement.IsFortification;
			if (this._settlement.IsFortification)
			{
				this.SettlementType = 0;
				EncyclopediaPage pageOf = Campaign.Current.EncyclopediaManager.GetPageOf(typeof(Settlement));
				using (List<Village>.Enumerator enumerator = this._settlement.BoundVillages.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Village village = enumerator.Current;
						if (pageOf.IsValidEncyclopediaItem(village.Owner.Settlement))
						{
							this.Settlements.Add(new EncyclopediaSettlementVM(village.Owner.Settlement));
						}
					}
					goto IL_00F2;
				}
			}
			if (this._settlement.IsVillage)
			{
				this.SettlementType = 1;
			}
			IL_00F2:
			if (!this._settlement.IsCastle)
			{
				EncyclopediaPage pageOf2 = Campaign.Current.EncyclopediaManager.GetPageOf(typeof(Hero));
				foreach (Hero hero in this._settlement.Notables)
				{
					if (pageOf2.IsValidEncyclopediaItem(hero))
					{
						this.NotableCharacters.Add(new HeroVM(hero, false));
					}
				}
			}
			GameTexts.SetVariable("STR1", GameTexts.FindText("str_enc_sf_culture", null).ToString());
			GameTexts.SetVariable("STR2", this._settlement.Culture.Name.ToString());
			this.CultureText = GameTexts.FindText("str_STR1_space_STR2", null).ToString();
			this.OwnerText = GameTexts.FindText("str_owner", null).ToString();
			this.Owner = new HeroVM(this._settlement.OwnerClan.Leader, false);
			this.OwnerBanner = new EncyclopediaFactionVM(this._settlement.OwnerClan);
			this.SettlementPath = settlementComponent.BackgroundMeshName;
			this.SettlementCropPosition = (double)settlementComponent.BackgroundCropPosition;
			this.HasBoundSettlement = this._settlement.IsVillage;
			this.BoundSettlement = (this.HasBoundSettlement ? new EncyclopediaSettlementVM(this._settlement.Village.Bound) : null);
			this.BoundSettlementText = "";
			if (this.HasBoundSettlement)
			{
				GameTexts.SetVariable("SETTLEMENT_LINK", this._settlement.Village.Bound.EncyclopediaLinkWithName);
				this.BoundSettlementText = GameTexts.FindText("str_bound_settlement_encyclopedia", null).ToString();
			}
			TextObject textObject;
			bool flag = CampaignUIHelper.IsSettlementInformationHidden(this._settlement, out textObject);
			string text = GameTexts.FindText("str_missing_info_indicator", null).ToString();
			string text2 = (flag ? text : ((int)this._settlement.Militia).ToString());
			if (this._settlement.IsFortification)
			{
				MBBindingList<EncyclopediaSettlementPageStatItemVM> mbbindingList = new MBBindingList<EncyclopediaSettlementPageStatItemVM>();
				mbbindingList.Add(new EncyclopediaSettlementPageStatItemVM(new BasicTooltipViewModel(() => CampaignUIHelper.GetTownWallsTooltip(this._settlement.Town)), EncyclopediaSettlementPageStatItemVM.DescriptionType.Wall, flag ? text : this._settlement.Town.GetWallLevel().ToString()));
				BasicTooltipViewModel basicTooltipViewModel = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownGarrisonTooltip(this._settlement.Town));
				EncyclopediaSettlementPageStatItemVM.DescriptionType descriptionType = EncyclopediaSettlementPageStatItemVM.DescriptionType.Garrison;
				string text3;
				if (!flag)
				{
					MobileParty garrisonParty = this._settlement.Town.GarrisonParty;
					text3 = ((garrisonParty != null) ? garrisonParty.Party.NumberOfAllMembers.ToString() : null);
				}
				else
				{
					text3 = text;
				}
				mbbindingList.Add(new EncyclopediaSettlementPageStatItemVM(basicTooltipViewModel, descriptionType, text3));
				mbbindingList.Add(new EncyclopediaSettlementPageStatItemVM(new BasicTooltipViewModel(() => CampaignUIHelper.GetTownMilitiaTooltip(this._settlement.Town)), EncyclopediaSettlementPageStatItemVM.DescriptionType.Militia, text2));
				mbbindingList.Add(new EncyclopediaSettlementPageStatItemVM(new BasicTooltipViewModel(() => CampaignUIHelper.GetTownFoodTooltip(this._settlement.Town)), EncyclopediaSettlementPageStatItemVM.DescriptionType.Food, flag ? text : ((int)this._settlement.Town.FoodStocks).ToString()));
				this.LeftSideProperties = mbbindingList;
				this.RightSideProperties = new MBBindingList<EncyclopediaSettlementPageStatItemVM>
				{
					new EncyclopediaSettlementPageStatItemVM(new BasicTooltipViewModel(() => CampaignUIHelper.GetTownProsperityTooltip(this._settlement.Town)), EncyclopediaSettlementPageStatItemVM.DescriptionType.Prosperity, flag ? text : ((int)this._settlement.Town.Prosperity).ToString()),
					new EncyclopediaSettlementPageStatItemVM(new BasicTooltipViewModel(() => CampaignUIHelper.GetTownLoyaltyTooltip(this._settlement.Town)), EncyclopediaSettlementPageStatItemVM.DescriptionType.Loyalty, flag ? text : ((int)this._settlement.Town.Loyalty).ToString()),
					new EncyclopediaSettlementPageStatItemVM(new BasicTooltipViewModel(() => CampaignUIHelper.GetTownSecurityTooltip(this._settlement.Town)), EncyclopediaSettlementPageStatItemVM.DescriptionType.Security, flag ? text : ((int)this._settlement.Town.Security).ToString())
				};
			}
			else
			{
				this.LeftSideProperties = new MBBindingList<EncyclopediaSettlementPageStatItemVM>
				{
					new EncyclopediaSettlementPageStatItemVM(new BasicTooltipViewModel(() => CampaignUIHelper.GetVillageMilitiaTooltip(this._settlement.Village)), EncyclopediaSettlementPageStatItemVM.DescriptionType.Militia, text2)
				};
				this.RightSideProperties = new MBBindingList<EncyclopediaSettlementPageStatItemVM>
				{
					new EncyclopediaSettlementPageStatItemVM(new BasicTooltipViewModel(() => CampaignUIHelper.GetVillageProsperityTooltip(this._settlement.Village)), EncyclopediaSettlementPageStatItemVM.DescriptionType.Prosperity, flag ? text : ((int)this._settlement.Village.Hearth).ToString())
				};
			}
			this.NameText = this._settlement.Name.ToString();
			for (int i = Campaign.Current.LogEntryHistory.GameActionLogs.Count - 1; i >= 0; i--)
			{
				IEncyclopediaLog encyclopediaLog;
				if ((encyclopediaLog = Campaign.Current.LogEntryHistory.GameActionLogs[i] as IEncyclopediaLog) != null && encyclopediaLog.IsVisibleInEncyclopediaPageOf<Settlement>(this._settlement))
				{
					this.History.Add(new EncyclopediaHistoryEventVM(encyclopediaLog));
				}
			}
			this.IsVisualTrackerSelected = Campaign.Current.VisualTrackerManager.CheckTracked(this._settlement);
			base.IsLoadingOver = true;
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x000527B4 File Offset: 0x000509B4
		public override string GetName()
		{
			return this._settlement.Name.ToString();
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x000527C8 File Offset: 0x000509C8
		public void ExecuteTrack()
		{
			if (!this.IsVisualTrackerSelected)
			{
				Campaign.Current.VisualTrackerManager.RegisterObject(this._settlement);
				this.IsVisualTrackerSelected = true;
			}
			else
			{
				Campaign.Current.VisualTrackerManager.RemoveTrackedObject(this._settlement, false);
				this.IsVisualTrackerSelected = false;
			}
			Game.Current.EventManager.TriggerEvent<PlayerToggleTrackSettlementFromEncyclopediaEvent>(new PlayerToggleTrackSettlementFromEncyclopediaEvent(this._settlement, this.IsVisualTrackerSelected));
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00052838 File Offset: 0x00050A38
		public override string GetNavigationBarURL()
		{
			return HyperlinkTexts.GetGenericHyperlinkText("Home", GameTexts.FindText("str_encyclopedia_home", null).ToString()) + " \\ " + HyperlinkTexts.GetGenericHyperlinkText("ListPage-Settlements", GameTexts.FindText("str_encyclopedia_settlements", null).ToString()) + " \\ " + this.GetName();
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0005289D File Offset: 0x00050A9D
		public void ExecuteBoundSettlementLink()
		{
			if (this.HasBoundSettlement)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this._settlement.Village.Bound.EncyclopediaLink);
			}
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x000528CC File Offset: 0x00050ACC
		public override void ExecuteSwitchBookmarkedState()
		{
			base.ExecuteSwitchBookmarkedState();
			if (base.IsBookmarked)
			{
				Campaign.Current.EncyclopediaManager.ViewDataTracker.AddEncyclopediaBookmarkToItem(this._settlement);
				return;
			}
			Campaign.Current.EncyclopediaManager.ViewDataTracker.RemoveEncyclopediaBookmarkFromItem(this._settlement);
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0005291C File Offset: 0x00050B1C
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent evnt)
		{
			this.IsTrackerButtonHighlightEnabled = evnt.NewNotificationElementID == "EncyclopediaItemTrackButton";
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x00052934 File Offset: 0x00050B34
		public override void OnFinalize()
		{
			base.OnFinalize();
			Game.Current.EventManager.UnregisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x00052957 File Offset: 0x00050B57
		// (set) Token: 0x060014A4 RID: 5284 RVA: 0x0005295F File Offset: 0x00050B5F
		[DataSourceProperty]
		public EncyclopediaFactionVM OwnerBanner
		{
			get
			{
				return this._ownerBanner;
			}
			set
			{
				if (value != this._ownerBanner)
				{
					this._ownerBanner = value;
					base.OnPropertyChangedWithValue<EncyclopediaFactionVM>(value, "OwnerBanner");
				}
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x0005297D File Offset: 0x00050B7D
		// (set) Token: 0x060014A6 RID: 5286 RVA: 0x00052985 File Offset: 0x00050B85
		[DataSourceProperty]
		public EncyclopediaSettlementVM BoundSettlement
		{
			get
			{
				return this._boundSettlement;
			}
			set
			{
				if (value != this._boundSettlement)
				{
					this._boundSettlement = value;
					base.OnPropertyChangedWithValue<EncyclopediaSettlementVM>(value, "BoundSettlement");
				}
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x000529A3 File Offset: 0x00050BA3
		// (set) Token: 0x060014A8 RID: 5288 RVA: 0x000529AB File Offset: 0x00050BAB
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

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x000529C9 File Offset: 0x00050BC9
		// (set) Token: 0x060014AA RID: 5290 RVA: 0x000529D1 File Offset: 0x00050BD1
		[DataSourceProperty]
		public bool IsTrackerButtonHighlightEnabled
		{
			get
			{
				return this._isTrackerButtonHighlightEnabled;
			}
			set
			{
				if (value != this._isTrackerButtonHighlightEnabled)
				{
					this._isTrackerButtonHighlightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsTrackerButtonHighlightEnabled");
				}
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x000529EF File Offset: 0x00050BEF
		// (set) Token: 0x060014AC RID: 5292 RVA: 0x000529F7 File Offset: 0x00050BF7
		[DataSourceProperty]
		public bool HasBoundSettlement
		{
			get
			{
				return this._hasBoundSettlement;
			}
			set
			{
				if (value != this._hasBoundSettlement)
				{
					this._hasBoundSettlement = value;
					base.OnPropertyChangedWithValue(value, "HasBoundSettlement");
				}
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x00052A15 File Offset: 0x00050C15
		// (set) Token: 0x060014AE RID: 5294 RVA: 0x00052A1D File Offset: 0x00050C1D
		[DataSourceProperty]
		public double SettlementCropPosition
		{
			get
			{
				return this._settlementCropPosition;
			}
			set
			{
				if (value != this._settlementCropPosition)
				{
					this._settlementCropPosition = value;
					base.OnPropertyChangedWithValue(value, "SettlementCropPosition");
				}
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x00052A3B File Offset: 0x00050C3B
		// (set) Token: 0x060014B0 RID: 5296 RVA: 0x00052A43 File Offset: 0x00050C43
		[DataSourceProperty]
		public string BoundSettlementText
		{
			get
			{
				return this._boundSettlementText;
			}
			set
			{
				if (value != this._boundSettlementText)
				{
					this._boundSettlementText = value;
					base.OnPropertyChangedWithValue<string>(value, "BoundSettlementText");
				}
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x00052A66 File Offset: 0x00050C66
		// (set) Token: 0x060014B2 RID: 5298 RVA: 0x00052A6E File Offset: 0x00050C6E
		[DataSourceProperty]
		public string TrackText
		{
			get
			{
				return this._trackText;
			}
			set
			{
				if (value != this._trackText)
				{
					this._trackText = value;
					base.OnPropertyChangedWithValue<string>(value, "TrackText");
				}
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x00052A91 File Offset: 0x00050C91
		// (set) Token: 0x060014B4 RID: 5300 RVA: 0x00052A99 File Offset: 0x00050C99
		[DataSourceProperty]
		public string SettlementPath
		{
			get
			{
				return this._settlementPath;
			}
			set
			{
				if (value != this._settlementPath)
				{
					this._settlementPath = value;
					base.OnPropertyChangedWithValue<string>(value, "SettlementPath");
				}
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x00052ABC File Offset: 0x00050CBC
		// (set) Token: 0x060014B6 RID: 5302 RVA: 0x00052AC4 File Offset: 0x00050CC4
		[DataSourceProperty]
		public string SettlementName
		{
			get
			{
				return this._settlementName;
			}
			set
			{
				if (value != this._settlementName)
				{
					this._settlementName = value;
					base.OnPropertyChangedWithValue<string>(value, "SettlementName");
				}
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x00052AE7 File Offset: 0x00050CE7
		// (set) Token: 0x060014B8 RID: 5304 RVA: 0x00052AEF File Offset: 0x00050CEF
		[DataSourceProperty]
		public string InformationText
		{
			get
			{
				return this._informationText;
			}
			set
			{
				if (value != this._informationText)
				{
					this._informationText = value;
					base.OnPropertyChangedWithValue<string>(value, "InformationText");
				}
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x00052B12 File Offset: 0x00050D12
		// (set) Token: 0x060014BA RID: 5306 RVA: 0x00052B1A File Offset: 0x00050D1A
		[DataSourceProperty]
		public HeroVM Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				if (value != this._owner)
				{
					this._owner = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "Owner");
				}
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x00052B38 File Offset: 0x00050D38
		// (set) Token: 0x060014BC RID: 5308 RVA: 0x00052B40 File Offset: 0x00050D40
		[DataSourceProperty]
		public string SettlementsText
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
					base.OnPropertyChanged("VillagesText");
				}
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x00052B62 File Offset: 0x00050D62
		// (set) Token: 0x060014BE RID: 5310 RVA: 0x00052B6A File Offset: 0x00050D6A
		[DataSourceProperty]
		public string SettlementImageID
		{
			get
			{
				return this._settlementImageID;
			}
			set
			{
				if (value != this._settlementImageID)
				{
					this._settlementImageID = value;
					base.OnPropertyChangedWithValue<string>(value, "SettlementImageID");
				}
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x00052B8D File Offset: 0x00050D8D
		// (set) Token: 0x060014C0 RID: 5312 RVA: 0x00052B95 File Offset: 0x00050D95
		[DataSourceProperty]
		public string NotableCharactersText
		{
			get
			{
				return this._notableCharactersText;
			}
			set
			{
				if (value != this._notableCharactersText)
				{
					this._notableCharactersText = value;
					base.OnPropertyChangedWithValue<string>(value, "NotableCharactersText");
				}
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x00052BB8 File Offset: 0x00050DB8
		// (set) Token: 0x060014C2 RID: 5314 RVA: 0x00052BC0 File Offset: 0x00050DC0
		[DataSourceProperty]
		public int SettlementType
		{
			get
			{
				return this._settlementType;
			}
			set
			{
				if (value != this._settlementType)
				{
					this._settlementType = value;
					base.OnPropertyChangedWithValue(value, "SettlementType");
				}
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00052BDE File Offset: 0x00050DDE
		// (set) Token: 0x060014C4 RID: 5316 RVA: 0x00052BE6 File Offset: 0x00050DE6
		[DataSourceProperty]
		public MBBindingList<EncyclopediaHistoryEventVM> History
		{
			get
			{
				return this._history;
			}
			set
			{
				if (value != this._history)
				{
					this._history = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaHistoryEventVM>>(value, "History");
				}
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x00052C04 File Offset: 0x00050E04
		// (set) Token: 0x060014C6 RID: 5318 RVA: 0x00052C0C File Offset: 0x00050E0C
		[DataSourceProperty]
		public MBBindingList<EncyclopediaSettlementVM> Settlements
		{
			get
			{
				return this._settlements;
			}
			set
			{
				if (value != this._settlements)
				{
					this._settlements = value;
					base.OnPropertyChanged("Villages");
				}
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x00052C29 File Offset: 0x00050E29
		// (set) Token: 0x060014C8 RID: 5320 RVA: 0x00052C31 File Offset: 0x00050E31
		[DataSourceProperty]
		public MBBindingList<HeroVM> NotableCharacters
		{
			get
			{
				return this._notableCharacters;
			}
			set
			{
				if (value != this._notableCharacters)
				{
					this._notableCharacters = value;
					base.OnPropertyChangedWithValue<MBBindingList<HeroVM>>(value, "NotableCharacters");
				}
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00052C4F File Offset: 0x00050E4F
		// (set) Token: 0x060014CA RID: 5322 RVA: 0x00052C57 File Offset: 0x00050E57
		[DataSourceProperty]
		public HintViewModel ShowInMapHint
		{
			get
			{
				return this._showInMapHint;
			}
			set
			{
				if (value != this._showInMapHint)
				{
					this._showInMapHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ShowInMapHint");
				}
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x00052C75 File Offset: 0x00050E75
		// (set) Token: 0x060014CC RID: 5324 RVA: 0x00052C7D File Offset: 0x00050E7D
		[DataSourceProperty]
		public MBBindingList<EncyclopediaSettlementPageStatItemVM> LeftSideProperties
		{
			get
			{
				return this._leftSideProperties;
			}
			set
			{
				if (value != this._leftSideProperties)
				{
					this._leftSideProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaSettlementPageStatItemVM>>(value, "LeftSideProperties");
				}
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x00052C9B File Offset: 0x00050E9B
		// (set) Token: 0x060014CE RID: 5326 RVA: 0x00052CA3 File Offset: 0x00050EA3
		[DataSourceProperty]
		public MBBindingList<EncyclopediaSettlementPageStatItemVM> RightSideProperties
		{
			get
			{
				return this._rightSideProperties;
			}
			set
			{
				if (value != this._rightSideProperties)
				{
					this._rightSideProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaSettlementPageStatItemVM>>(value, "RightSideProperties");
				}
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00052CC1 File Offset: 0x00050EC1
		// (set) Token: 0x060014D0 RID: 5328 RVA: 0x00052CC9 File Offset: 0x00050EC9
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00052CEC File Offset: 0x00050EEC
		// (set) Token: 0x060014D2 RID: 5330 RVA: 0x00052CF4 File Offset: 0x00050EF4
		[DataSourceProperty]
		public string CultureText
		{
			get
			{
				return this._cultureText;
			}
			set
			{
				if (value != this._cultureText)
				{
					this._cultureText = value;
					base.OnPropertyChangedWithValue<string>(value, "CultureText");
				}
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00052D17 File Offset: 0x00050F17
		// (set) Token: 0x060014D4 RID: 5332 RVA: 0x00052D1F File Offset: 0x00050F1F
		[DataSourceProperty]
		public string OwnerText
		{
			get
			{
				return this._ownerText;
			}
			set
			{
				if (value != this._ownerText)
				{
					this._ownerText = value;
					base.OnPropertyChangedWithValue<string>(value, "OwnerText");
				}
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00052D42 File Offset: 0x00050F42
		// (set) Token: 0x060014D6 RID: 5334 RVA: 0x00052D4A File Offset: 0x00050F4A
		[DataSourceProperty]
		public bool IsVisualTrackerSelected
		{
			get
			{
				return this._isVisualTrackerSelected;
			}
			set
			{
				if (value != this._isVisualTrackerSelected)
				{
					this._isVisualTrackerSelected = value;
					base.OnPropertyChangedWithValue(value, "IsVisualTrackerSelected");
				}
			}
		}

		// Token: 0x0400096E RID: 2414
		protected readonly Settlement _settlement;

		// Token: 0x0400096F RID: 2415
		private int _settlementType;

		// Token: 0x04000970 RID: 2416
		private MBBindingList<EncyclopediaHistoryEventVM> _history;

		// Token: 0x04000971 RID: 2417
		private MBBindingList<EncyclopediaSettlementVM> _settlements;

		// Token: 0x04000972 RID: 2418
		private EncyclopediaSettlementVM _boundSettlement;

		// Token: 0x04000973 RID: 2419
		private MBBindingList<HeroVM> _notableCharacters;

		// Token: 0x04000974 RID: 2420
		private EncyclopediaFactionVM _ownerBanner;

		// Token: 0x04000975 RID: 2421
		private HintViewModel _showInMapHint;

		// Token: 0x04000976 RID: 2422
		private MBBindingList<EncyclopediaSettlementPageStatItemVM> _leftSideProperties;

		// Token: 0x04000977 RID: 2423
		private MBBindingList<EncyclopediaSettlementPageStatItemVM> _rightSideProperties;

		// Token: 0x04000978 RID: 2424
		private HeroVM _owner;

		// Token: 0x04000979 RID: 2425
		private string _ownerText;

		// Token: 0x0400097A RID: 2426
		private string _nameText;

		// Token: 0x0400097B RID: 2427
		private string _cultureText;

		// Token: 0x0400097C RID: 2428
		private string _villagesText;

		// Token: 0x0400097D RID: 2429
		private string _notableCharactersText;

		// Token: 0x0400097E RID: 2430
		private string _settlementPath;

		// Token: 0x0400097F RID: 2431
		private string _settlementName;

		// Token: 0x04000980 RID: 2432
		private string _informationText;

		// Token: 0x04000981 RID: 2433
		private string _settlementImageID;

		// Token: 0x04000982 RID: 2434
		private string _boundSettlementText;

		// Token: 0x04000983 RID: 2435
		private string _trackText;

		// Token: 0x04000984 RID: 2436
		private double _settlementCropPosition;

		// Token: 0x04000985 RID: 2437
		private bool _isFortification;

		// Token: 0x04000986 RID: 2438
		private bool _isVisualTrackerSelected;

		// Token: 0x04000987 RID: 2439
		private bool _hasBoundSettlement;

		// Token: 0x04000988 RID: 2440
		private bool _isTrackerButtonHighlightEnabled;

		// Token: 0x02000248 RID: 584
		private enum SettlementTypes
		{
			// Token: 0x04001269 RID: 4713
			Town,
			// Token: 0x0400126A RID: 4714
			LoneVillage,
			// Token: 0x0400126B RID: 4715
			VillageWithCastle
		}
	}
}
