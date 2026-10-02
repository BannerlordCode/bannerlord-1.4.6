using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000D2 RID: 210
	[EncyclopediaViewModel(typeof(Kingdom))]
	public class EncyclopediaFactionPageVM : EncyclopediaContentPageVM
	{
		// Token: 0x060013E2 RID: 5090 RVA: 0x0004FD18 File Offset: 0x0004DF18
		public EncyclopediaFactionPageVM(EncyclopediaPageArgs args)
			: base(args)
		{
			this._faction = base.Obj as Kingdom;
			this.Clans = new MBBindingList<EncyclopediaFactionVM>();
			this.Enemies = new MBBindingList<EncyclopediaFactionVM>();
			this.TradeAgreements = new MBBindingList<EncyclopediaFactionVM>();
			this.Alliances = new MBBindingList<EncyclopediaFactionVM>();
			this.Settlements = new MBBindingList<EncyclopediaSettlementVM>();
			this.History = new MBBindingList<EncyclopediaHistoryEventVM>();
			base.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(this._faction);
			this.RefreshValues();
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0004FDA8 File Offset: 0x0004DFA8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.StrengthHint = new HintViewModel(GameTexts.FindText("str_strength", null), null);
			this.ProsperityHint = new HintViewModel(GameTexts.FindText("str_prosperity", null), null);
			this.MembersText = GameTexts.FindText("str_members", null).ToString();
			this.ClansText = new TextObject("{=bfQLwMUp}Clans", null).ToString();
			this.EnemiesText = new TextObject("{=zZlWRZjO}Wars", null).ToString();
			this.TradeAgreementsText = new TextObject("{=pWyRg13f}Trade Agreements", null).ToString();
			this.AlliancesText = new TextObject("{=f90A6PGd}Alliances", null).ToString();
			this.SettlementsText = new TextObject("{=LBNzsqyb}Fiefs", null).ToString();
			this.VillagesText = GameTexts.FindText("str_villages", null).ToString();
			TextObject encyclopediaText = this._faction.EncyclopediaText;
			this.InformationText = ((encyclopediaText != null) ? encyclopediaText.ToString() : null) ?? string.Empty;
			base.UpdateBookmarkHintText();
			this.Refresh();
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0004FEB8 File Offset: 0x0004E0B8
		public override void Refresh()
		{
			base.IsLoadingOver = false;
			this.Clans.Clear();
			this.Enemies.Clear();
			this.TradeAgreements.Clear();
			this.Alliances.Clear();
			this.Settlements.Clear();
			this.History.Clear();
			this.Leader = new HeroVM(this._faction.Leader, false);
			this.LeaderText = GameTexts.FindText("str_leader", null).ToString();
			this.NameText = this._faction.Name.ToString();
			this.DescriptorText = GameTexts.FindText("str_kingdom_faction", null).ToString();
			int num = 0;
			float num2 = 0f;
			EncyclopediaPage pageOf = Campaign.Current.EncyclopediaManager.GetPageOf(typeof(Hero));
			foreach (Hero hero in this._faction.AliveLords)
			{
				if (pageOf.IsValidEncyclopediaItem(hero))
				{
					num += hero.Gold;
				}
			}
			this.Banner = new BannerImageIdentifierVM(this._faction.Banner, true);
			foreach (MobileParty mobileParty in MobileParty.AllLordParties)
			{
				if (mobileParty.MapFaction == this._faction && !mobileParty.IsDisbanding)
				{
					num2 += mobileParty.Party.CalculateCurrentStrength();
				}
			}
			this.ProsperityText = num.ToString();
			this.StrengthText = num2.ToString();
			for (int i = Campaign.Current.LogEntryHistory.GameActionLogs.Count - 1; i >= 0; i--)
			{
				IEncyclopediaLog encyclopediaLog;
				if ((encyclopediaLog = Campaign.Current.LogEntryHistory.GameActionLogs[i] as IEncyclopediaLog) != null && encyclopediaLog.IsVisibleInEncyclopediaPageOf<Kingdom>(this._faction))
				{
					this.History.Add(new EncyclopediaHistoryEventVM(encyclopediaLog));
				}
			}
			EncyclopediaPage pageOf2 = Campaign.Current.EncyclopediaManager.GetPageOf(typeof(Clan));
			IOrderedEnumerable<IFaction> orderedEnumerable = Campaign.Current.Factions.OrderBy<IFaction, bool>((IFaction x) => !x.IsKingdomFaction).ThenBy<IFaction, string>((IFaction f) => f.Name.ToString());
			using (IEnumerator<IFaction> enumerator3 = orderedEnumerable.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					IFaction factionObject = enumerator3.Current;
					if (pageOf2.IsValidEncyclopediaItem(factionObject) && factionObject != this._faction && !factionObject.IsBanditFaction && FactionManager.IsAtWarAgainstFaction(this._faction, factionObject.MapFaction) && !this.Enemies.Any<EncyclopediaFactionVM>((EncyclopediaFactionVM x) => x.Faction == factionObject.MapFaction))
					{
						this.Enemies.Add(new EncyclopediaFactionVM(factionObject.MapFaction));
					}
				}
			}
			foreach (IFaction faction in orderedEnumerable.Where<IFaction>((IFaction x) => x.IsKingdomFaction))
			{
				Kingdom kingdom;
				if (pageOf2.IsValidEncyclopediaItem(faction) && faction != this._faction && (kingdom = faction as Kingdom) != null)
				{
					if (this.HasTradeAgreementWithFaction(this._faction, kingdom.MapFaction) && !this.TradeAgreements.Any<EncyclopediaFactionVM>((EncyclopediaFactionVM x) => x.Faction == kingdom.MapFaction))
					{
						this.TradeAgreements.Add(new EncyclopediaFactionVM(kingdom.MapFaction));
					}
					if (DiplomacyHelper.HasAllianceWithFaction(this._faction, kingdom.MapFaction) && !this.Alliances.Any<EncyclopediaFactionVM>((EncyclopediaFactionVM x) => x.Faction == kingdom.MapFaction))
					{
						this.Alliances.Add(new EncyclopediaFactionVM(kingdom.MapFaction));
					}
				}
			}
			foreach (Clan clan in Campaign.Current.Clans.Where<Clan>((Clan c) => c.Kingdom == this._faction))
			{
				this.Clans.Add(new EncyclopediaFactionVM(clan));
			}
			EncyclopediaPage pageOf3 = Campaign.Current.EncyclopediaManager.GetPageOf(typeof(Settlement));
			foreach (Settlement settlement in from s in Settlement.All
				where s.IsTown || s.IsCastle
				orderby s.IsCastle, s.IsTown
				select s)
			{
				if ((settlement.MapFaction == this._faction || (settlement.OwnerClan == this._faction.RulingClan && settlement.OwnerClan.Leader != null)) && pageOf3.IsValidEncyclopediaItem(settlement))
				{
					this.Settlements.Add(new EncyclopediaSettlementVM(settlement));
				}
			}
			base.IsLoadingOver = true;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x000504C0 File Offset: 0x0004E6C0
		private bool HasTradeAgreementWithFaction(IFaction faction1, IFaction faction2)
		{
			if (faction1 == null || faction2 == null || faction1 == faction2 || faction1.IsEliminated || faction2.IsEliminated || !faction1.IsKingdomFaction || !faction2.IsKingdomFaction)
			{
				return false;
			}
			ITradeAgreementsCampaignBehavior campaignBehavior = Campaign.Current.GetCampaignBehavior<ITradeAgreementsCampaignBehavior>();
			TradeAgreementsCampaignBehavior.TradeAgreement tradeAgreement;
			return campaignBehavior != null && campaignBehavior.HasTradeAgreement(faction1 as Kingdom, faction2 as Kingdom, out tradeAgreement);
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0005051C File Offset: 0x0004E71C
		public override string GetName()
		{
			return this._faction.Name.ToString();
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00050530 File Offset: 0x0004E730
		public override string GetNavigationBarURL()
		{
			return HyperlinkTexts.GetGenericHyperlinkText("Home", GameTexts.FindText("str_encyclopedia_home", null).ToString()) + " \\ " + HyperlinkTexts.GetGenericHyperlinkText("ListPage-Kingdoms", GameTexts.FindText("str_encyclopedia_kingdoms", null).ToString()) + " \\ " + this.GetName();
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00050598 File Offset: 0x0004E798
		public override void ExecuteSwitchBookmarkedState()
		{
			base.ExecuteSwitchBookmarkedState();
			if (base.IsBookmarked)
			{
				Campaign.Current.EncyclopediaManager.ViewDataTracker.AddEncyclopediaBookmarkToItem(this._faction);
				return;
			}
			Campaign.Current.EncyclopediaManager.ViewDataTracker.RemoveEncyclopediaBookmarkFromItem(this._faction);
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x000505E8 File Offset: 0x0004E7E8
		// (set) Token: 0x060013EA RID: 5098 RVA: 0x000505F0 File Offset: 0x0004E7F0
		[DataSourceProperty]
		public MBBindingList<EncyclopediaFactionVM> Clans
		{
			get
			{
				return this._clans;
			}
			set
			{
				if (value != this._clans)
				{
					this._clans = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaFactionVM>>(value, "Clans");
				}
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x0005060E File Offset: 0x0004E80E
		// (set) Token: 0x060013EC RID: 5100 RVA: 0x00050616 File Offset: 0x0004E816
		[DataSourceProperty]
		public MBBindingList<EncyclopediaFactionVM> Enemies
		{
			get
			{
				return this._enemies;
			}
			set
			{
				if (value != this._enemies)
				{
					this._enemies = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaFactionVM>>(value, "Enemies");
				}
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x00050634 File Offset: 0x0004E834
		// (set) Token: 0x060013EE RID: 5102 RVA: 0x0005063C File Offset: 0x0004E83C
		[DataSourceProperty]
		public MBBindingList<EncyclopediaFactionVM> TradeAgreements
		{
			get
			{
				return this._tradeAgreements;
			}
			set
			{
				if (value != this._tradeAgreements)
				{
					this._tradeAgreements = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaFactionVM>>(value, "TradeAgreements");
				}
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x0005065A File Offset: 0x0004E85A
		// (set) Token: 0x060013F0 RID: 5104 RVA: 0x00050662 File Offset: 0x0004E862
		[DataSourceProperty]
		public MBBindingList<EncyclopediaFactionVM> Alliances
		{
			get
			{
				return this._alliances;
			}
			set
			{
				if (value != this._alliances)
				{
					this._alliances = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaFactionVM>>(value, "Alliances");
				}
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x00050680 File Offset: 0x0004E880
		// (set) Token: 0x060013F2 RID: 5106 RVA: 0x00050688 File Offset: 0x0004E888
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
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaSettlementVM>>(value, "Settlements");
				}
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x000506A6 File Offset: 0x0004E8A6
		// (set) Token: 0x060013F4 RID: 5108 RVA: 0x000506AE File Offset: 0x0004E8AE
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

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x000506CC File Offset: 0x0004E8CC
		// (set) Token: 0x060013F6 RID: 5110 RVA: 0x000506D4 File Offset: 0x0004E8D4
		[DataSourceProperty]
		public HeroVM Leader
		{
			get
			{
				return this._leader;
			}
			set
			{
				if (value != this._leader)
				{
					this._leader = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "Leader");
				}
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x000506F2 File Offset: 0x0004E8F2
		// (set) Token: 0x060013F8 RID: 5112 RVA: 0x000506FA File Offset: 0x0004E8FA
		[DataSourceProperty]
		public BannerImageIdentifierVM Banner
		{
			get
			{
				return this._banner;
			}
			set
			{
				if (value != this._banner)
				{
					this._banner = value;
					base.OnPropertyChangedWithValue<BannerImageIdentifierVM>(value, "Banner");
				}
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x00050718 File Offset: 0x0004E918
		// (set) Token: 0x060013FA RID: 5114 RVA: 0x00050720 File Offset: 0x0004E920
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

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x00050743 File Offset: 0x0004E943
		// (set) Token: 0x060013FC RID: 5116 RVA: 0x0005074B File Offset: 0x0004E94B
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

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x0005076E File Offset: 0x0004E96E
		// (set) Token: 0x060013FE RID: 5118 RVA: 0x00050776 File Offset: 0x0004E976
		[DataSourceProperty]
		public string EnemiesText
		{
			get
			{
				return this._enemiesText;
			}
			set
			{
				if (value != this._enemiesText)
				{
					this._enemiesText = value;
					base.OnPropertyChangedWithValue<string>(value, "EnemiesText");
				}
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x00050799 File Offset: 0x0004E999
		// (set) Token: 0x06001400 RID: 5120 RVA: 0x000507A1 File Offset: 0x0004E9A1
		[DataSourceProperty]
		public string TradeAgreementsText
		{
			get
			{
				return this._tradeAgreementsText;
			}
			set
			{
				if (value != this._tradeAgreementsText)
				{
					this._tradeAgreementsText = value;
					base.OnPropertyChangedWithValue<string>(value, "TradeAgreementsText");
				}
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x000507C4 File Offset: 0x0004E9C4
		// (set) Token: 0x06001402 RID: 5122 RVA: 0x000507CC File Offset: 0x0004E9CC
		[DataSourceProperty]
		public string AlliancesText
		{
			get
			{
				return this._alliancesText;
			}
			set
			{
				if (value != this._alliancesText)
				{
					this._alliancesText = value;
					base.OnPropertyChangedWithValue<string>(value, "AlliancesText");
				}
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x000507EF File Offset: 0x0004E9EF
		// (set) Token: 0x06001404 RID: 5124 RVA: 0x000507F7 File Offset: 0x0004E9F7
		[DataSourceProperty]
		public string ClansText
		{
			get
			{
				return this._clansText;
			}
			set
			{
				if (value != this._clansText)
				{
					this._clansText = value;
					base.OnPropertyChangedWithValue<string>(value, "ClansText");
				}
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x0005081A File Offset: 0x0004EA1A
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x00050822 File Offset: 0x0004EA22
		[DataSourceProperty]
		public string SettlementsText
		{
			get
			{
				return this._settlementsText;
			}
			set
			{
				if (value != this._settlementsText)
				{
					this._settlementsText = value;
					base.OnPropertyChangedWithValue<string>(value, "SettlementsText");
				}
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x00050845 File Offset: 0x0004EA45
		// (set) Token: 0x06001408 RID: 5128 RVA: 0x0005084D File Offset: 0x0004EA4D
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

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x00050870 File Offset: 0x0004EA70
		// (set) Token: 0x0600140A RID: 5130 RVA: 0x00050878 File Offset: 0x0004EA78
		[DataSourceProperty]
		public string LeaderText
		{
			get
			{
				return this._leaderText;
			}
			set
			{
				if (value != this._leaderText)
				{
					this._leaderText = value;
					base.OnPropertyChangedWithValue<string>(value, "LeaderText");
				}
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0005089B File Offset: 0x0004EA9B
		// (set) Token: 0x0600140C RID: 5132 RVA: 0x000508A3 File Offset: 0x0004EAA3
		[DataSourceProperty]
		public string DescriptorText
		{
			get
			{
				return this._descriptorText;
			}
			set
			{
				if (value != this._descriptorText)
				{
					this._descriptorText = value;
					base.OnPropertyChangedWithValue<string>(value, "DescriptorText");
				}
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x000508C6 File Offset: 0x0004EAC6
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x000508CE File Offset: 0x0004EACE
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

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x000508F1 File Offset: 0x0004EAF1
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x000508F9 File Offset: 0x0004EAF9
		[DataSourceProperty]
		public string ProsperityText
		{
			get
			{
				return this._prosperityText;
			}
			set
			{
				if (value != this._prosperityText)
				{
					this._prosperityText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProsperityText");
				}
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x0005091C File Offset: 0x0004EB1C
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x00050924 File Offset: 0x0004EB24
		[DataSourceProperty]
		public string StrengthText
		{
			get
			{
				return this._strengthText;
			}
			set
			{
				if (value != this._strengthText)
				{
					this._strengthText = value;
					base.OnPropertyChangedWithValue<string>(value, "StrengthText");
				}
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x00050947 File Offset: 0x0004EB47
		// (set) Token: 0x06001414 RID: 5140 RVA: 0x0005094F File Offset: 0x0004EB4F
		[DataSourceProperty]
		public HintViewModel ProsperityHint
		{
			get
			{
				return this._prosperityHint;
			}
			set
			{
				if (value != this._prosperityHint)
				{
					this._prosperityHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ProsperityHint");
				}
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x0005096D File Offset: 0x0004EB6D
		// (set) Token: 0x06001416 RID: 5142 RVA: 0x00050975 File Offset: 0x0004EB75
		[DataSourceProperty]
		public HintViewModel StrengthHint
		{
			get
			{
				return this._strengthHint;
			}
			set
			{
				if (value != this._strengthHint)
				{
					this._strengthHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "StrengthHint");
				}
			}
		}

		// Token: 0x0400091B RID: 2331
		private Kingdom _faction;

		// Token: 0x0400091C RID: 2332
		private MBBindingList<EncyclopediaFactionVM> _clans;

		// Token: 0x0400091D RID: 2333
		private MBBindingList<EncyclopediaFactionVM> _enemies;

		// Token: 0x0400091E RID: 2334
		private MBBindingList<EncyclopediaFactionVM> _tradeAgreements;

		// Token: 0x0400091F RID: 2335
		private MBBindingList<EncyclopediaFactionVM> _alliances;

		// Token: 0x04000920 RID: 2336
		private MBBindingList<EncyclopediaSettlementVM> _settlements;

		// Token: 0x04000921 RID: 2337
		private MBBindingList<EncyclopediaHistoryEventVM> _history;

		// Token: 0x04000922 RID: 2338
		private HeroVM _leader;

		// Token: 0x04000923 RID: 2339
		private BannerImageIdentifierVM _banner;

		// Token: 0x04000924 RID: 2340
		private string _membersText;

		// Token: 0x04000925 RID: 2341
		private string _enemiesText;

		// Token: 0x04000926 RID: 2342
		private string _tradeAgreementsText;

		// Token: 0x04000927 RID: 2343
		private string _alliancesText;

		// Token: 0x04000928 RID: 2344
		private string _clansText;

		// Token: 0x04000929 RID: 2345
		private string _settlementsText;

		// Token: 0x0400092A RID: 2346
		private string _villagesText;

		// Token: 0x0400092B RID: 2347
		private string _leaderText;

		// Token: 0x0400092C RID: 2348
		private string _descriptorText;

		// Token: 0x0400092D RID: 2349
		private string _prosperityText;

		// Token: 0x0400092E RID: 2350
		private string _strengthText;

		// Token: 0x0400092F RID: 2351
		private string _informationText;

		// Token: 0x04000930 RID: 2352
		private HintViewModel _prosperityHint;

		// Token: 0x04000931 RID: 2353
		private HintViewModel _strengthHint;

		// Token: 0x04000932 RID: 2354
		private string _nameText;
	}
}
