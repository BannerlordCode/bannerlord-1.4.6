using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.MapSiege
{
	// Token: 0x02000053 RID: 83
	public class MapSiegeProductionVM : ViewModel
	{
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00013C20 File Offset: 0x00011E20
		private SiegeEvent Siege
		{
			get
			{
				return PlayerSiege.PlayerSiegeEvent;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00013C27 File Offset: 0x00011E27
		private BattleSideEnum PlayerSide
		{
			get
			{
				return PlayerSiege.PlayerSide;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00013C2E File Offset: 0x00011E2E
		private Settlement Settlement
		{
			get
			{
				return this.Siege.BesiegedSettlement;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00013C3B File Offset: 0x00011E3B
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x00013C43 File Offset: 0x00011E43
		public MapSiegePOIVM LatestSelectedPOI { get; private set; }

		// Token: 0x06000538 RID: 1336 RVA: 0x00013C4C File Offset: 0x00011E4C
		public MapSiegeProductionVM()
		{
			this.PossibleProductionMachines = new MBBindingList<MapSiegeProductionMachineVM>();
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00013C5F File Offset: 0x00011E5F
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PossibleProductionMachines.ApplyActionOnAllItems(delegate(MapSiegeProductionMachineVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00013C94 File Offset: 0x00011E94
		public void Update()
		{
			if (this.IsEnabled && this.LatestSelectedPOI.Machine == null)
			{
				if (this.PossibleProductionMachines.Any<MapSiegeProductionMachineVM>((MapSiegeProductionMachineVM m) => m.IsReserveOption))
				{
					this.ExecuteDisable();
				}
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00013CE8 File Offset: 0x00011EE8
		public void OnMachineSelection(MapSiegePOIVM poi)
		{
			this.PossibleProductionMachines.Clear();
			this.LatestSelectedPOI = poi;
			MapSiegePOIVM latestSelectedPOI = this.LatestSelectedPOI;
			if (((latestSelectedPOI != null) ? latestSelectedPOI.Machine : null) != null)
			{
				this.PossibleProductionMachines.Add(new MapSiegeProductionMachineVM(new Action<MapSiegeProductionMachineVM>(this.OnPossibleMachineSelection), !this.LatestSelectedPOI.Machine.IsActive && !this.LatestSelectedPOI.Machine.IsBeingRedeployed));
			}
			else
			{
				IEnumerable<SiegeEngineType> enumerable;
				switch (poi.Type)
				{
				case MapSiegePOIVM.POIType.DefenderSiegeMachine:
					enumerable = this.GetAllDefenderMachines();
					goto IL_00BE;
				case MapSiegePOIVM.POIType.AttackerRamSiegeMachine:
					enumerable = this.GetAllAttackerRamMachines();
					goto IL_00BE;
				case MapSiegePOIVM.POIType.AttackerTowerSiegeMachine:
					enumerable = this.GetAllAttackerTowerMachines();
					goto IL_00BE;
				case MapSiegePOIVM.POIType.AttackerRangedSiegeMachine:
					enumerable = this.GetAllAttackerRangedMachines();
					goto IL_00BE;
				}
				this.IsEnabled = false;
				return;
				IL_00BE:
				using (IEnumerator<SiegeEngineType> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SiegeEngineType desMachine = enumerator.Current;
						int num = this.Siege.GetSiegeEventSide(this.PlayerSide).SiegeEngines.ReservedSiegeEngines.Count<SiegeEvent.SiegeEngineConstructionProgress>((SiegeEvent.SiegeEngineConstructionProgress m) => m.SiegeEngine == desMachine);
						this.PossibleProductionMachines.Add(new MapSiegeProductionMachineVM(desMachine, num, new Action<MapSiegeProductionMachineVM>(this.OnPossibleMachineSelection)));
					}
				}
			}
			this.IsEnabled = true;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00013E4C File Offset: 0x0001204C
		private void OnPossibleMachineSelection(MapSiegeProductionMachineVM machine)
		{
			if (this.LatestSelectedPOI.Machine == null || this.LatestSelectedPOI.Machine.SiegeEngine != machine.Engine)
			{
				ISiegeEventSide siegeEventSide = this.Siege.GetSiegeEventSide(this.PlayerSide);
				if (machine.IsReserveOption && this.LatestSelectedPOI.Machine != null)
				{
					bool flag = this.LatestSelectedPOI.Machine.IsActive || this.LatestSelectedPOI.Machine.IsBeingRedeployed;
					siegeEventSide.SiegeEngines.RemoveDeployedSiegeEngine(this.LatestSelectedPOI.MachineIndex, this.LatestSelectedPOI.Machine.SiegeEngine.IsRanged, flag);
				}
				else
				{
					SiegeEvent.SiegeEngineConstructionProgress siegeEngineConstructionProgress = siegeEventSide.SiegeEngines.ReservedSiegeEngines.FirstOrDefault<SiegeEvent.SiegeEngineConstructionProgress>((SiegeEvent.SiegeEngineConstructionProgress e) => e.SiegeEngine == machine.Engine);
					if (siegeEngineConstructionProgress == null)
					{
						float siegeEngineHitPoints = Campaign.Current.Models.SiegeEventModel.GetSiegeEngineHitPoints(PlayerSiege.PlayerSiegeEvent, machine.Engine, this.PlayerSide);
						siegeEngineConstructionProgress = new SiegeEvent.SiegeEngineConstructionProgress(machine.Engine, 0f, siegeEngineHitPoints);
					}
					if (siegeEventSide.SiegeStrategy != DefaultSiegeStrategies.Custom && Campaign.Current.Models.EncounterModel.GetLeaderOfSiegeEvent(this.Siege, siegeEventSide.BattleSide) == Hero.MainHero)
					{
						siegeEventSide.SetSiegeStrategy(DefaultSiegeStrategies.Custom);
					}
					siegeEventSide.SiegeEngines.DeploySiegeEngineAtIndex(siegeEngineConstructionProgress, this.LatestSelectedPOI.MachineIndex);
				}
				this.Siege.BesiegedSettlement.Party.SetVisualAsDirty();
				Game.Current.EventManager.TriggerEvent<PlayerStartEngineConstructionEvent>(new PlayerStartEngineConstructionEvent(machine.Engine));
			}
			this.IsEnabled = false;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001400E File Offset: 0x0001220E
		public void ExecuteDisable()
		{
			this.IsEnabled = false;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00014017 File Offset: 0x00012217
		private IEnumerable<SiegeEngineType> GetAllDefenderMachines()
		{
			return Campaign.Current.Models.SiegeEventModel.GetAvailableDefenderSiegeEngines(PartyBase.MainParty);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00014032 File Offset: 0x00012232
		private IEnumerable<SiegeEngineType> GetAllAttackerRangedMachines()
		{
			return Campaign.Current.Models.SiegeEventModel.GetAvailableAttackerRangedSiegeEngines(PartyBase.MainParty);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001404D File Offset: 0x0001224D
		private IEnumerable<SiegeEngineType> GetAllAttackerRamMachines()
		{
			return Campaign.Current.Models.SiegeEventModel.GetAvailableAttackerRamSiegeEngines(PartyBase.MainParty);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00014068 File Offset: 0x00012268
		private IEnumerable<SiegeEngineType> GetAllAttackerTowerMachines()
		{
			return Campaign.Current.Models.SiegeEventModel.GetAvailableAttackerTowerSiegeEngines(PartyBase.MainParty);
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00014083 File Offset: 0x00012283
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0001408B File Offset: 0x0001228B
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x000140A9 File Offset: 0x000122A9
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x000140B1 File Offset: 0x000122B1
		[DataSourceProperty]
		public MBBindingList<MapSiegeProductionMachineVM> PossibleProductionMachines
		{
			get
			{
				return this._possibleProductionMachines;
			}
			set
			{
				if (value != this._possibleProductionMachines)
				{
					this._possibleProductionMachines = value;
					base.OnPropertyChangedWithValue<MBBindingList<MapSiegeProductionMachineVM>>(value, "PossibleProductionMachines");
				}
			}
		}

		// Token: 0x0400029B RID: 667
		private MBBindingList<MapSiegeProductionMachineVM> _possibleProductionMachines;

		// Token: 0x0400029C RID: 668
		private bool _isEnabled;
	}
}
