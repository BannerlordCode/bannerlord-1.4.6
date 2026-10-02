using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.ViewModelCollection.MapSiege
{
	// Token: 0x02000052 RID: 82
	public class MapSiegePOIVM : ViewModel
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0001311C File Offset: 0x0001131C
		private SiegeEvent Siege
		{
			get
			{
				return PlayerSiege.PlayerSiegeEvent;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00013123 File Offset: 0x00011323
		private BattleSideEnum PlayerSide
		{
			get
			{
				return PlayerSiege.PlayerSide;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0001312A File Offset: 0x0001132A
		private Settlement Settlement
		{
			get
			{
				return this.Siege.BesiegedSettlement;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00013137 File Offset: 0x00011337
		public MapSiegePOIVM.POIType Type { get; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0001313F File Offset: 0x0001133F
		public int MachineIndex { get; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00013147 File Offset: 0x00011347
		public float LatestW
		{
			get
			{
				return this._latestW;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0001314F File Offset: 0x0001134F
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x00013157 File Offset: 0x00011357
		public SiegeEvent.SiegeEngineConstructionProgress Machine { get; private set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00013160 File Offset: 0x00011360
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x00013168 File Offset: 0x00011368
		public MatrixFrame MapSceneLocationFrame { get; private set; }

		// Token: 0x06000508 RID: 1288 RVA: 0x00013174 File Offset: 0x00011374
		public MapSiegePOIVM(MapSiegePOIVM.POIType type, MatrixFrame mapSceneLocation, Camera mapCamera, int machineIndex, Action<MapSiegePOIVM> onSelection)
		{
			this.Type = type;
			this._onSelection = onSelection;
			this._thisSide = ((this.Type == MapSiegePOIVM.POIType.AttackerRamSiegeMachine || this.Type == MapSiegePOIVM.POIType.AttackerTowerSiegeMachine || this.Type == MapSiegePOIVM.POIType.AttackerRangedSiegeMachine) ? BattleSideEnum.Attacker : BattleSideEnum.Defender);
			this.MapSceneLocationFrame = mapSceneLocation;
			this._mapSceneLocation = this.MapSceneLocationFrame.origin;
			this._mapCamera = mapCamera;
			this.MachineIndex = machineIndex;
			Color color;
			if (this._thisSide != BattleSideEnum.Attacker)
			{
				IFaction mapFaction = this.Siege.BesiegedSettlement.MapFaction;
				color = Color.FromUint((mapFaction != null) ? mapFaction.Color : 0U);
			}
			else
			{
				IFaction mapFaction2 = this.Siege.BesiegerCamp.MapFaction;
				color = Color.FromUint((mapFaction2 != null) ? mapFaction2.Color : 0U);
			}
			this.SidePrimaryColor = color;
			Color color2;
			if (this._thisSide != BattleSideEnum.Attacker)
			{
				IFaction mapFaction3 = this.Siege.BesiegedSettlement.MapFaction;
				color2 = Color.FromUint((mapFaction3 != null) ? mapFaction3.Color2 : 0U);
			}
			else
			{
				IFaction mapFaction4 = this.Siege.BesiegerCamp.MapFaction;
				color2 = Color.FromUint((mapFaction4 != null) ? mapFaction4.Color2 : 0U);
			}
			this.SideSecondaryColor = color2;
			this.IsPlayerSidePOI = this.DetermineIfPOIIsPlayerSide();
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000132A2 File Offset: 0x000114A2
		public void ExecuteSelection()
		{
			this._onSelection(this);
			this.IsSelected = true;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000132B8 File Offset: 0x000114B8
		public void UpdateProperties()
		{
			this.Machine = this.GetDesiredMachine();
			this._bindHasItem = this.Type == MapSiegePOIVM.POIType.WallSection || this.Machine != null;
			SiegeEvent.SiegeEngineConstructionProgress machine = this.Machine;
			this._bindIsConstructing = machine != null && !machine.IsActive;
			this.RefreshMachineType();
			this.RefreshHitpoints();
			this.RefreshQueueIndex();
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00013318 File Offset: 0x00011518
		public void RefreshDistanceValue(float newDistance)
		{
			this._bindIsInVisibleRange = newDistance <= 20f;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001332C File Offset: 0x0001152C
		public void RefreshPosition()
		{
			this._latestX = 0f;
			this._latestY = 0f;
			this._latestW = 0f;
			MBWindowManager.WorldToScreenInsideUsableArea(this._mapCamera, this._mapSceneLocation, ref this._latestX, ref this._latestY, ref this._latestW);
			if (!MathF.IsValidValue(this._latestX) || !MathF.IsValidValue(this._latestY) || !MathF.IsValidValue(this._latestW))
			{
				this._latestX = -10000f;
				this._latestY = -10000f;
				this._latestW = -1f;
			}
			this._bindWPos = this._latestW;
			this._bindWSign = (int)this._bindWPos;
			this._bindIsInside = this.IsInsideWindow();
			this._bindPosition = new Vec2(this._latestX, this._latestY);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00013404 File Offset: 0x00011604
		public void RefreshBinding()
		{
			this.Position = this._bindPosition;
			this.IsInside = this._bindIsInside;
			this.CurrentHitpoints = this._bindCurrentHitpoints;
			this.MaxHitpoints = this._bindMaxHitpoints;
			this.HasItem = this._bindHasItem;
			this.IsConstructing = this._bindIsConstructing;
			this.MachineType = this._bindMachineType;
			this.QueueIndex = this._bindQueueIndex;
			this.IsInVisibleRange = this._bindIsInVisibleRange;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00013480 File Offset: 0x00011680
		private void RefreshHitpoints()
		{
			if (this.Siege == null)
			{
				this._bindCurrentHitpoints = 0f;
				this._bindMaxHitpoints = 0f;
				return;
			}
			MapSiegePOIVM.POIType type = this.Type;
			if (type == MapSiegePOIVM.POIType.WallSection)
			{
				MBReadOnlyList<float> settlementWallSectionHitPointsRatioList = this.Settlement.SettlementWallSectionHitPointsRatioList;
				this._bindMaxHitpoints = this.Settlement.MaxWallHitPoints / (float)this.Settlement.WallSectionCount;
				this._bindCurrentHitpoints = settlementWallSectionHitPointsRatioList[this.MachineIndex] * this._bindMaxHitpoints;
				this._bindMachineType = ((this._bindCurrentHitpoints <= 0f) ? 1 : 0);
				return;
			}
			if (type - MapSiegePOIVM.POIType.DefenderSiegeMachine > 3)
			{
				return;
			}
			if (this.Machine == null)
			{
				this._bindCurrentHitpoints = 0f;
				this._bindMaxHitpoints = 0f;
				return;
			}
			if (this.Machine.IsActive)
			{
				this._bindCurrentHitpoints = this.Machine.Hitpoints;
				this._bindMaxHitpoints = this.Machine.MaxHitPoints;
				return;
			}
			if (this.Machine.IsBeingRedeployed)
			{
				this._bindCurrentHitpoints = this.Machine.RedeploymentProgress;
				this._bindMaxHitpoints = 1f;
				return;
			}
			this._bindCurrentHitpoints = this.Machine.Progress;
			this._bindMaxHitpoints = 1f;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000135B0 File Offset: 0x000117B0
		private void RefreshMachineType()
		{
			if (this.Siege == null)
			{
				this._bindMachineType = -1;
				return;
			}
			MapSiegePOIVM.POIType type = this.Type;
			if (type == MapSiegePOIVM.POIType.WallSection)
			{
				this._bindMachineType = 0;
				return;
			}
			if (type - MapSiegePOIVM.POIType.DefenderSiegeMachine > 3)
			{
				return;
			}
			this._bindMachineType = (int)((this.Machine != null) ? this.GetMachineTypeFromId(this.Machine.SiegeEngine.StringId) : MapSiegePOIVM.MachineTypes.None);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00013610 File Offset: 0x00011810
		private void RefreshQueueIndex()
		{
			int num;
			if (this.Machine == null)
			{
				num = -1;
			}
			else
			{
				num = this.Siege.GetSiegeEventSide(this.PlayerSide).SiegeEngines.DeployedSiegeEngines.Where<SiegeEvent.SiegeEngineConstructionProgress>((SiegeEvent.SiegeEngineConstructionProgress e) => !e.IsActive).ToList<SiegeEvent.SiegeEngineConstructionProgress>().IndexOf(this.Machine);
			}
			this._bindQueueIndex = num;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00013680 File Offset: 0x00011880
		private bool DetermineIfPOIIsPlayerSide()
		{
			MapSiegePOIVM.POIType type = this.Type;
			if (type > MapSiegePOIVM.POIType.DefenderSiegeMachine)
			{
				return type - MapSiegePOIVM.POIType.AttackerRamSiegeMachine <= 2 && this.PlayerSide == BattleSideEnum.Attacker;
			}
			return this.PlayerSide == BattleSideEnum.Defender;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000136B8 File Offset: 0x000118B8
		private bool IsInsideWindow()
		{
			return this._latestX - 100f <= Screen.RealScreenResolutionWidth && this._latestY - 100f <= Screen.RealScreenResolutionHeight && this._latestX + 100f >= 0f && this._latestY + 100f >= 0f;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00013716 File Offset: 0x00011916
		public void ExecuteShowTooltip()
		{
			InformationManager.ShowTooltip(typeof(List<TooltipProperty>), new object[] { SandBoxUIHelper.GetSiegeEngineInProgressTooltip(this.Machine) });
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001373B File Offset: 0x0001193B
		public void ExecuteHideTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00013744 File Offset: 0x00011944
		private MapSiegePOIVM.MachineTypes GetMachineTypeFromId(string id)
		{
			string text = id.ToLower();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num > 746114623U)
			{
				if (num <= 1820818168U)
				{
					if (num <= 1241455715U)
					{
						if (num != 808481256U)
						{
							if (num != 1241455715U)
							{
								return MapSiegePOIVM.MachineTypes.None;
							}
							if (!(text == "ram"))
							{
								return MapSiegePOIVM.MachineTypes.None;
							}
							return MapSiegePOIVM.MachineTypes.Ram;
						}
						else if (!(text == "fire_ballista"))
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
					}
					else if (num != 1748194790U)
					{
						if (num != 1820818168U)
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
						if (!(text == "fire_onager"))
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
						return MapSiegePOIVM.MachineTypes.Mangonel;
					}
					else
					{
						if (!(text == "fire_catapult"))
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
						return MapSiegePOIVM.MachineTypes.Mangonel;
					}
				}
				else if (num <= 1898442385U)
				{
					if (num != 1839032341U)
					{
						if (num != 1898442385U)
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
						if (!(text == "catapult"))
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
						return MapSiegePOIVM.MachineTypes.Mangonel;
					}
					else
					{
						if (!(text == "trebuchet"))
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
						return MapSiegePOIVM.MachineTypes.Trebuchet;
					}
				}
				else if (num != 2806198843U)
				{
					if (num != 4036530155U)
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
					if (!(text == "ballista"))
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
				}
				else
				{
					if (!(text == "onager"))
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
					return MapSiegePOIVM.MachineTypes.Mangonel;
				}
				return MapSiegePOIVM.MachineTypes.Ballista;
			}
			if (num > 473034592U)
			{
				if (num <= 712590611U)
				{
					if (num != 695812992U)
					{
						if (num != 712590611U)
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
						if (!(text == "siege_tower_level2"))
						{
							return MapSiegePOIVM.MachineTypes.None;
						}
					}
					else if (!(text == "siege_tower_level3"))
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
				}
				else if (num != 729368230U)
				{
					if (num != 746114623U)
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
					if (!(text == "fire_mangonel"))
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
					return MapSiegePOIVM.MachineTypes.Mangonel;
				}
				else if (!(text == "siege_tower_level1"))
				{
					return MapSiegePOIVM.MachineTypes.None;
				}
				return MapSiegePOIVM.MachineTypes.SiegeTower;
			}
			if (num != 6339497U)
			{
				if (num != 390431385U)
				{
					if (num != 473034592U)
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
					if (!(text == "mangonel"))
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
				}
				else
				{
					if (!(text == "bricole"))
					{
						return MapSiegePOIVM.MachineTypes.None;
					}
					return MapSiegePOIVM.MachineTypes.Trebuchet;
				}
			}
			else
			{
				if (!(text == "ladder"))
				{
					return MapSiegePOIVM.MachineTypes.None;
				}
				return MapSiegePOIVM.MachineTypes.Ladder;
			}
			return MapSiegePOIVM.MachineTypes.Mangonel;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00013968 File Offset: 0x00011B68
		private SiegeEvent.SiegeEngineConstructionProgress GetDesiredMachine()
		{
			if (this.Siege != null)
			{
				switch (this.Type)
				{
				case MapSiegePOIVM.POIType.DefenderSiegeMachine:
					return this.Siege.GetSiegeEventSide(BattleSideEnum.Defender).SiegeEngines.DeployedRangedSiegeEngines[this.MachineIndex];
				case MapSiegePOIVM.POIType.AttackerRamSiegeMachine:
				case MapSiegePOIVM.POIType.AttackerTowerSiegeMachine:
					return this.Siege.GetSiegeEventSide(BattleSideEnum.Attacker).SiegeEngines.DeployedMeleeSiegeEngines[this.MachineIndex];
				case MapSiegePOIVM.POIType.AttackerRangedSiegeMachine:
					return this.Siege.GetSiegeEventSide(BattleSideEnum.Attacker).SiegeEngines.DeployedRangedSiegeEngines[this.MachineIndex];
				}
				return null;
			}
			return null;
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x000139FD File Offset: 0x00011BFD
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x00013A05 File Offset: 0x00011C05
		public Vec2 Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (this._position != value)
				{
					this._position = value;
					base.OnPropertyChangedWithValue(value, "Position");
				}
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x00013A30 File Offset: 0x00011C30
		public Color SidePrimaryColor
		{
			get
			{
				return this._sidePrimaryColor;
			}
			set
			{
				if (this._sidePrimaryColor != value)
				{
					this._sidePrimaryColor = value;
					base.OnPropertyChangedWithValue(value, "SidePrimaryColor");
				}
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00013A53 File Offset: 0x00011C53
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x00013A5B File Offset: 0x00011C5B
		public Color SideSecondaryColor
		{
			get
			{
				return this._sideSecondaryColor;
			}
			set
			{
				if (this._sideSecondaryColor != value)
				{
					this._sideSecondaryColor = value;
					base.OnPropertyChangedWithValue(value, "SideSecondaryColor");
				}
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00013A7E File Offset: 0x00011C7E
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x00013A86 File Offset: 0x00011C86
		public int QueueIndex
		{
			get
			{
				return this._queueIndex;
			}
			set
			{
				if (this._queueIndex != value)
				{
					this._queueIndex = value;
					base.OnPropertyChangedWithValue(value, "QueueIndex");
				}
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00013AA4 File Offset: 0x00011CA4
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x00013AAC File Offset: 0x00011CAC
		public int MachineType
		{
			get
			{
				return this._machineType;
			}
			set
			{
				if (this._machineType != value)
				{
					this._machineType = value;
					base.OnPropertyChangedWithValue(value, "MachineType");
				}
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00013ACA File Offset: 0x00011CCA
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x00013AD2 File Offset: 0x00011CD2
		public float CurrentHitpoints
		{
			get
			{
				return this._currentHitpoints;
			}
			set
			{
				if (this._currentHitpoints != value)
				{
					this._currentHitpoints = value;
					base.OnPropertyChangedWithValue(value, "CurrentHitpoints");
				}
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00013AF0 File Offset: 0x00011CF0
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x00013AF8 File Offset: 0x00011CF8
		public float MaxHitpoints
		{
			get
			{
				return this._maxHitpoints;
			}
			set
			{
				if (this._maxHitpoints != value)
				{
					this._maxHitpoints = value;
					base.OnPropertyChangedWithValue(value, "MaxHitpoints");
				}
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00013B16 File Offset: 0x00011D16
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00013B1E File Offset: 0x00011D1E
		public bool IsPlayerSidePOI
		{
			get
			{
				return this._isPlayerSidePOI;
			}
			set
			{
				if (this._isPlayerSidePOI != value)
				{
					this._isPlayerSidePOI = value;
					base.OnPropertyChangedWithValue(value, "IsPlayerSidePOI");
				}
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00013B3C File Offset: 0x00011D3C
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00013B44 File Offset: 0x00011D44
		public bool IsFireVersion
		{
			get
			{
				return this._isFireVersion;
			}
			set
			{
				if (this._isFireVersion != value)
				{
					this._isFireVersion = value;
					base.OnPropertyChangedWithValue(value, "IsFireVersion");
				}
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00013B62 File Offset: 0x00011D62
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x00013B6A File Offset: 0x00011D6A
		public bool IsInVisibleRange
		{
			get
			{
				return this._isInVisibleRange;
			}
			set
			{
				if (this._isInVisibleRange != value)
				{
					this._isInVisibleRange = value;
					base.OnPropertyChangedWithValue(value, "IsInVisibleRange");
				}
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00013B88 File Offset: 0x00011D88
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x00013B90 File Offset: 0x00011D90
		public bool IsConstructing
		{
			get
			{
				return this._isConstructing;
			}
			set
			{
				if (this._isConstructing != value)
				{
					this._isConstructing = value;
					base.OnPropertyChangedWithValue(value, "IsConstructing");
				}
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00013BAE File Offset: 0x00011DAE
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x00013BB6 File Offset: 0x00011DB6
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (this._isSelected != value)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00013BD4 File Offset: 0x00011DD4
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x00013BDC File Offset: 0x00011DDC
		public bool HasItem
		{
			get
			{
				return this._hasItem;
			}
			set
			{
				if (this._hasItem != value)
				{
					this._hasItem = value;
					base.OnPropertyChangedWithValue(value, "HasItem");
				}
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00013BFA File Offset: 0x00011DFA
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x00013C02 File Offset: 0x00011E02
		public bool IsInside
		{
			get
			{
				return this._isInside;
			}
			set
			{
				if (this._isInside != value)
				{
					this._isInside = value;
					base.OnPropertyChangedWithValue(value, "IsInside");
				}
			}
		}

		// Token: 0x0400027A RID: 634
		private readonly Vec3 _mapSceneLocation;

		// Token: 0x0400027B RID: 635
		private readonly Camera _mapCamera;

		// Token: 0x0400027C RID: 636
		private readonly BattleSideEnum _thisSide;

		// Token: 0x0400027D RID: 637
		private readonly Action<MapSiegePOIVM> _onSelection;

		// Token: 0x0400027E RID: 638
		private float _latestX;

		// Token: 0x0400027F RID: 639
		private float _latestY;

		// Token: 0x04000280 RID: 640
		private float _latestW;

		// Token: 0x04000281 RID: 641
		private float _bindCurrentHitpoints;

		// Token: 0x04000282 RID: 642
		private float _bindMaxHitpoints;

		// Token: 0x04000283 RID: 643
		private float _bindWPos;

		// Token: 0x04000284 RID: 644
		private int _bindWSign;

		// Token: 0x04000285 RID: 645
		private int _bindMachineType = -1;

		// Token: 0x04000286 RID: 646
		private int _bindQueueIndex;

		// Token: 0x04000287 RID: 647
		private bool _bindIsInside;

		// Token: 0x04000288 RID: 648
		private bool _bindHasItem;

		// Token: 0x04000289 RID: 649
		private bool _bindIsConstructing;

		// Token: 0x0400028A RID: 650
		private Vec2 _bindPosition;

		// Token: 0x0400028B RID: 651
		private bool _bindIsInVisibleRange;

		// Token: 0x0400028C RID: 652
		private Color _sidePrimaryColor;

		// Token: 0x0400028D RID: 653
		private Color _sideSecondaryColor;

		// Token: 0x0400028E RID: 654
		private Vec2 _position;

		// Token: 0x0400028F RID: 655
		private float _currentHitpoints;

		// Token: 0x04000290 RID: 656
		private int _machineType = -1;

		// Token: 0x04000291 RID: 657
		private float _maxHitpoints;

		// Token: 0x04000292 RID: 658
		private int _queueIndex;

		// Token: 0x04000293 RID: 659
		private bool _isInside;

		// Token: 0x04000294 RID: 660
		private bool _hasItem;

		// Token: 0x04000295 RID: 661
		private bool _isConstructing;

		// Token: 0x04000296 RID: 662
		private bool _isPlayerSidePOI;

		// Token: 0x04000297 RID: 663
		private bool _isFireVersion;

		// Token: 0x04000298 RID: 664
		private bool _isInVisibleRange;

		// Token: 0x04000299 RID: 665
		private bool _isSelected;

		// Token: 0x020000A8 RID: 168
		public enum POIType
		{
			// Token: 0x040003CE RID: 974
			WallSection,
			// Token: 0x040003CF RID: 975
			DefenderSiegeMachine,
			// Token: 0x040003D0 RID: 976
			AttackerRamSiegeMachine,
			// Token: 0x040003D1 RID: 977
			AttackerTowerSiegeMachine,
			// Token: 0x040003D2 RID: 978
			AttackerRangedSiegeMachine
		}

		// Token: 0x020000A9 RID: 169
		public enum MachineTypes
		{
			// Token: 0x040003D4 RID: 980
			None = -1,
			// Token: 0x040003D5 RID: 981
			Wall,
			// Token: 0x040003D6 RID: 982
			BrokenWall,
			// Token: 0x040003D7 RID: 983
			Ballista,
			// Token: 0x040003D8 RID: 984
			Trebuchet,
			// Token: 0x040003D9 RID: 985
			Ladder,
			// Token: 0x040003DA RID: 986
			Ram,
			// Token: 0x040003DB RID: 987
			SiegeTower,
			// Token: 0x040003DC RID: 988
			Mangonel
		}
	}
}
