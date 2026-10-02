using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Nameplate
{
	// Token: 0x02000020 RID: 32
	public class SettlementNameplatesVM : ViewModel
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000C7FF File Offset: 0x0000A9FF
		public MBReadOnlyList<SettlementNameplateVM> AllNameplates
		{
			get
			{
				return this._allNameplates;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000C808 File Offset: 0x0000AA08
		public SettlementNameplatesVM(Camera mapCamera, Action<CampaignVec2> fastMoveCameraToPosition)
		{
			this._allNameplates = new MBList<SettlementNameplateVM>(400);
			this._allNameplatesBySettlements = new Dictionary<Settlement, SettlementNameplateVM>(400);
			this.SmallNameplates = new MBBindingList<SettlementNameplateVM>();
			this.MediumNameplates = new MBBindingList<SettlementNameplateVM>();
			this.LargeNameplates = new MBBindingList<SettlementNameplateVM>();
			this._mapCamera = mapCamera;
			this._fastMoveCameraToPosition = fastMoveCameraToPosition;
			CampaignEvents.PartyVisibilityChangedEvent.AddNonSerializedListener(this, new Action<PartyBase>(this.OnPartyBaseVisibilityChange));
			CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
			CampaignEvents.MakePeace.AddNonSerializedListener(this, new Action<IFaction, IFaction, MakePeaceAction.MakePeaceDetail>(this.OnPeaceDeclared));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangeKingdom));
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
			CampaignEvents.OnSiegeEventStartedEvent.AddNonSerializedListener(this, new Action<SiegeEvent>(this.OnSiegeEventStartedOnSettlement));
			CampaignEvents.OnSiegeEventEndedEvent.AddNonSerializedListener(this, new Action<SiegeEvent>(this.OnSiegeEventEndedOnSettlement));
			CampaignEvents.RebelliousClanDisbandedAtSettlement.AddNonSerializedListener(this, new Action<Settlement, Clan>(this.OnRebelliousClanDisbandedAtSettlement));
			CampaignEvents.OnAllianceStartedEvent.AddNonSerializedListener(this, new Action<Kingdom, Kingdom>(this.OnAllianceStarted));
			CampaignEvents.OnAllianceEndedEvent.AddNonSerializedListener(this, new Action<Kingdom, Kingdom>(this.OnAllianceEnded));
			this.UpdateNameplateAuxMTPredicate = new TWParallel.ParallelForAuxPredicate(this.UpdateNameplateAuxMT);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000C964 File Offset: 0x0000AB64
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEventDispatcher.Instance.RemoveListeners(this);
			for (int i = 0; i < this._allNameplates.Count; i++)
			{
				this._allNameplates[i].OnFinalize();
			}
			this._allNameplates.Clear();
			this._allNameplatesBySettlements.Clear();
			this.SmallNameplates.Clear();
			this.MediumNameplates.Clear();
			this.LargeNameplates.Clear();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
		public override void RefreshValues()
		{
			base.RefreshValues();
			for (int i = 0; i < this._allNameplates.Count; i++)
			{
				this._allNameplates[i].RefreshValues();
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		public void Initialize(IEnumerable<Tuple<Settlement, GameEntity>> settlements)
		{
			this._allRegularSettlements = settlements.Where<Tuple<Settlement, GameEntity>>((Tuple<Settlement, GameEntity> x) => !x.Item1.IsHideout && !(x.Item1.SettlementComponent is RetirementSettlementComponent));
			this._allHideouts = settlements.Where<Tuple<Settlement, GameEntity>>((Tuple<Settlement, GameEntity> x) => x.Item1.IsHideout && !(x.Item1.SettlementComponent is RetirementSettlementComponent));
			this._allRetreats = settlements.Where<Tuple<Settlement, GameEntity>>((Tuple<Settlement, GameEntity> x) => !x.Item1.IsHideout && x.Item1.SettlementComponent is RetirementSettlementComponent);
			foreach (Tuple<Settlement, GameEntity> tuple in this._allRegularSettlements)
			{
				if (tuple.Item1.IsVisible)
				{
					SettlementNameplateVM settlementNameplateVM = new SettlementNameplateVM(tuple.Item1, tuple.Item2, this._mapCamera, this._fastMoveCameraToPosition);
					this.AddNameplate(settlementNameplateVM);
				}
			}
			foreach (Tuple<Settlement, GameEntity> tuple2 in this._allHideouts)
			{
				if (tuple2.Item1.Hideout.IsSpotted)
				{
					SettlementNameplateVM settlementNameplateVM2 = new SettlementNameplateVM(tuple2.Item1, tuple2.Item2, this._mapCamera, this._fastMoveCameraToPosition);
					this.AddNameplate(settlementNameplateVM2);
				}
			}
			foreach (Tuple<Settlement, GameEntity> tuple3 in this._allRetreats)
			{
				RetirementSettlementComponent retirementSettlementComponent;
				if ((retirementSettlementComponent = tuple3.Item1.SettlementComponent as RetirementSettlementComponent) != null)
				{
					if (retirementSettlementComponent.IsSpotted)
					{
						SettlementNameplateVM settlementNameplateVM3 = new SettlementNameplateVM(tuple3.Item1, tuple3.Item2, this._mapCamera, this._fastMoveCameraToPosition);
						this.AddNameplate(settlementNameplateVM3);
					}
				}
				else
				{
					Debug.FailedAssert("A seetlement which is IsRetreat doesn't have a retirement component.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\SandBox.ViewModelCollection\\Nameplate\\SettlementNameplatesVM.cs", "Initialize", 120);
				}
			}
			for (int i = 0; i < this._allNameplates.Count; i++)
			{
				SettlementNameplateVM settlementNameplateVM4 = this._allNameplates[i];
				Settlement settlement = settlementNameplateVM4.Settlement;
				if (((settlement != null) ? settlement.SiegeEvent : null) != null)
				{
					SettlementNameplateVM settlementNameplateVM5 = settlementNameplateVM4;
					Settlement settlement2 = settlementNameplateVM4.Settlement;
					settlementNameplateVM5.OnSiegeEventStartedOnSettlement((settlement2 != null) ? settlement2.SiegeEvent : null);
				}
				else if (settlementNameplateVM4.Settlement.IsTown || settlementNameplateVM4.Settlement.IsCastle)
				{
					Clan ownerClan = settlementNameplateVM4.Settlement.OwnerClan;
					if (ownerClan != null && ownerClan.IsRebelClan)
					{
						settlementNameplateVM4.OnRebelliousClanFormed(settlementNameplateVM4.Settlement.OwnerClan);
					}
				}
			}
			this.RefreshRelationsOfNameplates();
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000CCC8 File Offset: 0x0000AEC8
		private void AddNameplate(SettlementNameplateVM nameplate)
		{
			this._allNameplates.Add(nameplate);
			this._allNameplatesBySettlements[nameplate.Settlement] = nameplate;
			switch (nameplate.SettlementTypeEnum)
			{
			case SettlementNameplateVM.Type.Village:
				this.SmallNameplates.Add(nameplate);
				return;
			case SettlementNameplateVM.Type.Castle:
				this.MediumNameplates.Add(nameplate);
				return;
			case SettlementNameplateVM.Type.Town:
				this.LargeNameplates.Add(nameplate);
				return;
			default:
				return;
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000CD34 File Offset: 0x0000AF34
		private void RemoveNameplate(SettlementNameplateVM nameplate)
		{
			this._allNameplates.Remove(nameplate);
			this._allNameplatesBySettlements.Remove(nameplate.Settlement);
			this.SmallNameplates.Remove(nameplate);
			this.MediumNameplates.Remove(nameplate);
			this.LargeNameplates.Remove(nameplate);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000CD88 File Offset: 0x0000AF88
		private void UpdateNameplateAuxMT(int startInclusive, int endExclusive)
		{
			for (int i = startInclusive; i < endExclusive; i++)
			{
				this._allNameplates[i].UpdateNameplateMT(this._cachedCameraPosition);
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000CDB8 File Offset: 0x0000AFB8
		public void Update()
		{
			this._cachedCameraPosition = this._mapCamera.Position;
			TWParallel.For(0, this._allNameplates.Count, this.UpdateNameplateAuxMTPredicate, 16);
			for (int i = 0; i < this._allNameplates.Count; i++)
			{
				this._allNameplates[i].RefreshBindValues();
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000CE18 File Offset: 0x0000B018
		private void OnSiegeEventStartedOnSettlement(SiegeEvent siegeEvent)
		{
			SettlementNameplateVM settlementNameplateVM;
			if (this._allNameplatesBySettlements.TryGetValue(siegeEvent.BesiegedSettlement, out settlementNameplateVM))
			{
				settlementNameplateVM.OnSiegeEventStartedOnSettlement(siegeEvent);
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000CE44 File Offset: 0x0000B044
		private void OnSiegeEventEndedOnSettlement(SiegeEvent siegeEvent)
		{
			SettlementNameplateVM settlementNameplateVM;
			if (this._allNameplatesBySettlements.TryGetValue(siegeEvent.BesiegedSettlement, out settlementNameplateVM))
			{
				settlementNameplateVM.OnSiegeEventEndedOnSettlement(siegeEvent);
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000CE70 File Offset: 0x0000B070
		private void OnMapEventStartedOnSettlement(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
		{
			SettlementNameplateVM settlementNameplateVM;
			if (this._allNameplatesBySettlements.TryGetValue(mapEvent.MapEventSettlement, out settlementNameplateVM))
			{
				settlementNameplateVM.OnMapEventStartedOnSettlement(mapEvent);
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000CE9C File Offset: 0x0000B09C
		private void OnMapEventEndedOnSettlement(MapEvent mapEvent)
		{
			SettlementNameplateVM settlementNameplateVM;
			if (this._allNameplatesBySettlements.TryGetValue(mapEvent.MapEventSettlement, out settlementNameplateVM))
			{
				settlementNameplateVM.OnMapEventEndedOnSettlement();
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
		private void OnPartyBaseVisibilityChange(PartyBase party)
		{
			if (party.IsSettlement)
			{
				Tuple<Settlement, GameEntity> tuple;
				if (party.Settlement.IsHideout)
				{
					tuple = this._allHideouts.SingleOrDefault<Tuple<Settlement, GameEntity>>((Tuple<Settlement, GameEntity> h) => h.Item1.Hideout == party.Settlement.Hideout);
				}
				else if (party.Settlement.SettlementComponent is RetirementSettlementComponent)
				{
					tuple = this._allRetreats.SingleOrDefault<Tuple<Settlement, GameEntity>>((Tuple<Settlement, GameEntity> h) => h.Item1.SettlementComponent as RetirementSettlementComponent == party.Settlement.SettlementComponent as RetirementSettlementComponent);
				}
				else
				{
					tuple = this._allRegularSettlements.SingleOrDefault<Tuple<Settlement, GameEntity>>((Tuple<Settlement, GameEntity> h) => h.Item1 == party.Settlement);
				}
				if (tuple != null)
				{
					SettlementNameplateVM settlementNameplateVM = null;
					if (tuple.Item1 != null)
					{
						this._allNameplatesBySettlements.TryGetValue(tuple.Item1, out settlementNameplateVM);
					}
					if (party.IsVisible && settlementNameplateVM == null)
					{
						SettlementNameplateVM settlementNameplateVM2 = new SettlementNameplateVM(tuple.Item1, tuple.Item2, this._mapCamera, this._fastMoveCameraToPosition);
						this.AddNameplate(settlementNameplateVM2);
						settlementNameplateVM2.RefreshRelationStatus();
						return;
					}
					if (!party.IsVisible && settlementNameplateVM != null)
					{
						this.RemoveNameplate(settlementNameplateVM);
					}
				}
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000CFD9 File Offset: 0x0000B1D9
		private void OnPeaceDeclared(IFaction faction1, IFaction faction2, MakePeaceAction.MakePeaceDetail detail)
		{
			this.OnPeaceOrWarDeclared(faction1, faction2);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000CFE3 File Offset: 0x0000B1E3
		private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail arg3)
		{
			this.OnPeaceOrWarDeclared(faction1, faction2);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000CFED File Offset: 0x0000B1ED
		private void OnPeaceOrWarDeclared(IFaction faction1, IFaction faction2)
		{
			if (faction1 == Hero.MainHero.MapFaction || faction1 == Hero.MainHero.Clan || faction2 == Hero.MainHero.MapFaction || faction2 == Hero.MainHero.Clan)
			{
				this.RefreshRelationsOfNameplates();
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000D029 File Offset: 0x0000B229
		private void OnClanChangeKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification)
		{
			this.RefreshRelationsOfNameplates();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000D034 File Offset: 0x0000B234
		private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero previousOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			SettlementNameplateVM settlementNameplateVM = null;
			if (this._allNameplatesBySettlements.TryGetValue(settlement, out settlementNameplateVM))
			{
				settlementNameplateVM.RefreshDynamicProperties(true);
				settlementNameplateVM.RefreshRelationStatus();
				if (detail == ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail.ByRebellion)
				{
					settlementNameplateVM.OnRebelliousClanFormed(newOwner.Clan);
				}
				else if (previousOwner != null && previousOwner.IsRebel)
				{
					settlementNameplateVM.OnRebelliousClanDisbanded(previousOwner.Clan);
				}
			}
			for (int i = 0; i < settlement.BoundVillages.Count; i++)
			{
				Village village = settlement.BoundVillages[i];
				if (this._allNameplatesBySettlements.TryGetValue(village.Settlement, out settlementNameplateVM))
				{
					settlementNameplateVM.RefreshDynamicProperties(true);
					settlementNameplateVM.RefreshRelationStatus();
				}
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000D0D2 File Offset: 0x0000B2D2
		private void OnAllianceEnded(Kingdom kingdom1, Kingdom kingdom2)
		{
			this.OnAllianceStateChanged(kingdom1, kingdom2);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000D0DC File Offset: 0x0000B2DC
		private void OnAllianceStarted(Kingdom kingdom1, Kingdom kingdom2)
		{
			this.OnAllianceStateChanged(kingdom1, kingdom2);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000D0E6 File Offset: 0x0000B2E6
		private void OnAllianceStateChanged(Kingdom kingdom1, Kingdom kingdom2)
		{
			if (kingdom1 == Hero.MainHero.MapFaction || kingdom2 == Hero.MainHero.MapFaction)
			{
				this.RefreshRelationsOfNameplates();
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000D108 File Offset: 0x0000B308
		public SettlementNameplateVM GetNameplateOfSettlement(Settlement settlement)
		{
			SettlementNameplateVM settlementNameplateVM;
			if (this._allNameplatesBySettlements.TryGetValue(settlement, out settlementNameplateVM))
			{
				return settlementNameplateVM;
			}
			return null;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000D128 File Offset: 0x0000B328
		public void OnRebelliousClanDisbandedAtSettlement(Settlement settlement, Clan clan)
		{
			SettlementNameplateVM settlementNameplateVM;
			if (this._allNameplatesBySettlements.TryGetValue(settlement, out settlementNameplateVM))
			{
				settlementNameplateVM.OnRebelliousClanDisbanded(clan);
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000D14C File Offset: 0x0000B34C
		public void RefreshRelationsOfNameplates()
		{
			for (int i = 0; i < this._allNameplates.Count; i++)
			{
				this._allNameplates[i].RefreshRelationStatus();
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000D180 File Offset: 0x0000B380
		public void RefreshDynamicPropertiesOfNameplates(bool forceUpdate)
		{
			for (int i = 0; i < this._allNameplates.Count; i++)
			{
				this._allNameplates[i].RefreshDynamicProperties(forceUpdate);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000D1B5 File Offset: 0x0000B3B5
		// (set) Token: 0x06000305 RID: 773 RVA: 0x0000D1BD File Offset: 0x0000B3BD
		[DataSourceProperty]
		public MBBindingList<SettlementNameplateVM> SmallNameplates
		{
			get
			{
				return this._smallNameplates;
			}
			set
			{
				if (this._smallNameplates != value)
				{
					this._smallNameplates = value;
					base.OnPropertyChangedWithValue<MBBindingList<SettlementNameplateVM>>(value, "SmallNameplates");
				}
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000D1DB File Offset: 0x0000B3DB
		// (set) Token: 0x06000307 RID: 775 RVA: 0x0000D1E3 File Offset: 0x0000B3E3
		[DataSourceProperty]
		public MBBindingList<SettlementNameplateVM> MediumNameplates
		{
			get
			{
				return this._mediumNameplates;
			}
			set
			{
				if (this._mediumNameplates != value)
				{
					this._mediumNameplates = value;
					base.OnPropertyChangedWithValue<MBBindingList<SettlementNameplateVM>>(value, "MediumNameplates");
				}
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000D201 File Offset: 0x0000B401
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0000D209 File Offset: 0x0000B409
		[DataSourceProperty]
		public MBBindingList<SettlementNameplateVM> LargeNameplates
		{
			get
			{
				return this._largeNameplates;
			}
			set
			{
				if (this._largeNameplates != value)
				{
					this._largeNameplates = value;
					base.OnPropertyChangedWithValue<MBBindingList<SettlementNameplateVM>>(value, "LargeNameplates");
				}
			}
		}

		// Token: 0x0400016A RID: 362
		private readonly Camera _mapCamera;

		// Token: 0x0400016B RID: 363
		private Vec3 _cachedCameraPosition;

		// Token: 0x0400016C RID: 364
		private readonly TWParallel.ParallelForAuxPredicate UpdateNameplateAuxMTPredicate;

		// Token: 0x0400016D RID: 365
		private readonly Action<CampaignVec2> _fastMoveCameraToPosition;

		// Token: 0x0400016E RID: 366
		private IEnumerable<Tuple<Settlement, GameEntity>> _allHideouts;

		// Token: 0x0400016F RID: 367
		private IEnumerable<Tuple<Settlement, GameEntity>> _allRetreats;

		// Token: 0x04000170 RID: 368
		private IEnumerable<Tuple<Settlement, GameEntity>> _allRegularSettlements;

		// Token: 0x04000171 RID: 369
		private MBList<SettlementNameplateVM> _allNameplates;

		// Token: 0x04000172 RID: 370
		private Dictionary<Settlement, SettlementNameplateVM> _allNameplatesBySettlements;

		// Token: 0x04000173 RID: 371
		private MBBindingList<SettlementNameplateVM> _smallNameplates;

		// Token: 0x04000174 RID: 372
		private MBBindingList<SettlementNameplateVM> _mediumNameplates;

		// Token: 0x04000175 RID: 373
		private MBBindingList<SettlementNameplateVM> _largeNameplates;
	}
}
