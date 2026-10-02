using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.BarterSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Handlers;
using TaleWorlds.CampaignSystem.Incidents;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Naval;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;
using TaleWorlds.SaveSystem.Load;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000030 RID: 48
	public class Campaign : GameType
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00013F65 File Offset: 0x00012165
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00013F6C File Offset: 0x0001216C
		public static float MapDiagonal { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00013F74 File Offset: 0x00012174
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00013F7B File Offset: 0x0001217B
		public static float MapDiagonalSquared { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00013F83 File Offset: 0x00012183
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00013F8A File Offset: 0x0001218A
		public static Vec2 MapMinimumPosition { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00013F92 File Offset: 0x00012192
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00013F99 File Offset: 0x00012199
		public static Vec2 MapMaximumPosition { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00013FA1 File Offset: 0x000121A1
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00013FA8 File Offset: 0x000121A8
		public static float MapMaximumHeight { get; private set; }

		// Token: 0x060001FE RID: 510 RVA: 0x00013FB0 File Offset: 0x000121B0
		public float GetAverageDistanceBetweenClosestTwoTownsWithNavigationType(MobileParty.NavigationType navigationType)
		{
			return this._averageDistanceBetweenClosestTwoTowns[navigationType];
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00013FBE File Offset: 0x000121BE
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00013FC6 File Offset: 0x000121C6
		[CachedData]
		public float AverageWage { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00013FCF File Offset: 0x000121CF
		public string NewGameVersion
		{
			get
			{
				return this._newGameVersion;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00013FD7 File Offset: 0x000121D7
		public MBReadOnlyList<string> PreviouslyUsedModules
		{
			get
			{
				return this._previouslyUsedModules;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00013FDF File Offset: 0x000121DF
		public MBReadOnlyList<string> UsedGameVersions
		{
			get
			{
				return this._usedGameVersions;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00013FE7 File Offset: 0x000121E7
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00013FEF File Offset: 0x000121EF
		[SaveableProperty(83)]
		public bool EnabledCheatsBefore { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00013FF8 File Offset: 0x000121F8
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00014000 File Offset: 0x00012200
		[SaveableProperty(82)]
		public string PlatformID { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00014009 File Offset: 0x00012209
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00014011 File Offset: 0x00012211
		internal CampaignEventDispatcher CampaignEventDispatcher { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0001401A File Offset: 0x0001221A
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00014022 File Offset: 0x00012222
		[SaveableProperty(80)]
		public string UniqueGameId { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0001402B File Offset: 0x0001222B
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00014033 File Offset: 0x00012233
		public SaveHandler SaveHandler { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0001403C File Offset: 0x0001223C
		public override bool SupportsSaving
		{
			get
			{
				return this.GameMode == CampaignGameMode.Campaign;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00014047 File Offset: 0x00012247
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0001404F File Offset: 0x0001224F
		[SaveableProperty(211)]
		public CampaignObjectManager CampaignObjectManager { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00014058 File Offset: 0x00012258
		public override bool IsDevelopment
		{
			get
			{
				return this.GameMode == CampaignGameMode.Tutorial;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00014063 File Offset: 0x00012263
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0001406B File Offset: 0x0001226B
		[SaveableProperty(3)]
		public bool IsCraftingEnabled { get; set; } = true;

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00014074 File Offset: 0x00012274
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0001407C File Offset: 0x0001227C
		[SaveableProperty(4)]
		public bool IsBannerEditorEnabled { get; set; } = true;

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00014085 File Offset: 0x00012285
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0001408D File Offset: 0x0001228D
		[SaveableProperty(5)]
		public bool IsFaceGenEnabled { get; set; } = true;

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00014096 File Offset: 0x00012296
		public ICampaignBehaviorManager CampaignBehaviorManager
		{
			get
			{
				return this._campaignBehaviorManager;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0001409E File Offset: 0x0001229E
		// (set) Token: 0x0600021A RID: 538 RVA: 0x000140A6 File Offset: 0x000122A6
		[SaveableProperty(8)]
		public QuestManager QuestManager { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000140AF File Offset: 0x000122AF
		// (set) Token: 0x0600021C RID: 540 RVA: 0x000140B7 File Offset: 0x000122B7
		[SaveableProperty(9)]
		public IssueManager IssueManager { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000140C0 File Offset: 0x000122C0
		// (set) Token: 0x0600021E RID: 542 RVA: 0x000140C8 File Offset: 0x000122C8
		[SaveableProperty(11)]
		public FactionManager FactionManager { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000140D1 File Offset: 0x000122D1
		// (set) Token: 0x06000220 RID: 544 RVA: 0x000140D9 File Offset: 0x000122D9
		[SaveableProperty(12)]
		public CharacterRelationManager CharacterRelationManager { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000140E2 File Offset: 0x000122E2
		// (set) Token: 0x06000222 RID: 546 RVA: 0x000140EA File Offset: 0x000122EA
		[SaveableProperty(14)]
		public Romance Romance { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000140F3 File Offset: 0x000122F3
		// (set) Token: 0x06000224 RID: 548 RVA: 0x000140FB File Offset: 0x000122FB
		[SaveableProperty(16)]
		public PlayerCaptivity PlayerCaptivity { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00014104 File Offset: 0x00012304
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0001410C File Offset: 0x0001230C
		[SaveableProperty(17)]
		internal Clan PlayerDefaultFaction { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00014115 File Offset: 0x00012315
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0001411D File Offset: 0x0001231D
		public CampaignMission.ICampaignMissionManager CampaignMissionManager { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00014126 File Offset: 0x00012326
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0001412E File Offset: 0x0001232E
		public ISkillLevelingManager SkillLevelingManager { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00014137 File Offset: 0x00012337
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0001413F File Offset: 0x0001233F
		public IMapSceneCreator MapSceneCreator { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00014148 File Offset: 0x00012348
		public override bool IsInventoryAccessibleAtMission
		{
			get
			{
				return this.GameMode == CampaignGameMode.Tutorial;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00014153 File Offset: 0x00012353
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0001415B File Offset: 0x0001235B
		public GameMenuCallbackManager GameMenuCallbackManager { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00014164 File Offset: 0x00012364
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0001416C File Offset: 0x0001236C
		public VisualCreator VisualCreator { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00014175 File Offset: 0x00012375
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0001417D File Offset: 0x0001237D
		[SaveableProperty(28)]
		public MapStateData MapStateData { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00014186 File Offset: 0x00012386
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0001418E File Offset: 0x0001238E
		public DefaultPerks DefaultPerks { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00014197 File Offset: 0x00012397
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0001419F File Offset: 0x0001239F
		public DefaultTraits DefaultTraits { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000238 RID: 568 RVA: 0x000141A8 File Offset: 0x000123A8
		// (set) Token: 0x06000239 RID: 569 RVA: 0x000141B0 File Offset: 0x000123B0
		public DefaultPolicies DefaultPolicies { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600023A RID: 570 RVA: 0x000141B9 File Offset: 0x000123B9
		// (set) Token: 0x0600023B RID: 571 RVA: 0x000141C1 File Offset: 0x000123C1
		public DefaultBuildingTypes DefaultBuildingTypes { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600023C RID: 572 RVA: 0x000141CA File Offset: 0x000123CA
		// (set) Token: 0x0600023D RID: 573 RVA: 0x000141D2 File Offset: 0x000123D2
		public DefaultIssueEffects DefaultIssueEffects { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600023E RID: 574 RVA: 0x000141DB File Offset: 0x000123DB
		// (set) Token: 0x0600023F RID: 575 RVA: 0x000141E3 File Offset: 0x000123E3
		public DefaultItems DefaultItems { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000141EC File Offset: 0x000123EC
		// (set) Token: 0x06000241 RID: 577 RVA: 0x000141F4 File Offset: 0x000123F4
		public DefaultFigureheads DefaultFigureheads { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000242 RID: 578 RVA: 0x000141FD File Offset: 0x000123FD
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00014205 File Offset: 0x00012405
		public DefaultSiegeStrategies DefaultSiegeStrategies { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0001420E File Offset: 0x0001240E
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00014216 File Offset: 0x00012416
		internal MBReadOnlyList<PerkObject> AllPerks { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0001421F File Offset: 0x0001241F
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00014227 File Offset: 0x00012427
		public DefaultSkillEffects DefaultSkillEffects { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00014230 File Offset: 0x00012430
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00014238 File Offset: 0x00012438
		public DefaultVillageTypes DefaultVillageTypes { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00014241 File Offset: 0x00012441
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00014249 File Offset: 0x00012449
		internal MBReadOnlyList<TraitObject> AllTraits { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00014252 File Offset: 0x00012452
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0001425A File Offset: 0x0001245A
		internal MBReadOnlyList<MBEquipmentRoster> AllEquipmentRosters { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00014263 File Offset: 0x00012463
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0001426B File Offset: 0x0001246B
		public DefaultCulturalFeats DefaultFeats { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00014274 File Offset: 0x00012474
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0001427C File Offset: 0x0001247C
		internal MBReadOnlyList<PolicyObject> AllPolicies { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00014285 File Offset: 0x00012485
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0001428D File Offset: 0x0001248D
		internal MBReadOnlyList<BuildingType> AllBuildingTypes { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00014296 File Offset: 0x00012496
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0001429E File Offset: 0x0001249E
		internal MBReadOnlyList<IssueEffect> AllIssueEffects { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000256 RID: 598 RVA: 0x000142A7 File Offset: 0x000124A7
		// (set) Token: 0x06000257 RID: 599 RVA: 0x000142AF File Offset: 0x000124AF
		internal MBReadOnlyList<SiegeStrategy> AllSiegeStrategies { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000258 RID: 600 RVA: 0x000142B8 File Offset: 0x000124B8
		// (set) Token: 0x06000259 RID: 601 RVA: 0x000142C0 File Offset: 0x000124C0
		internal MBReadOnlyList<VillageType> AllVillageTypes { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600025A RID: 602 RVA: 0x000142C9 File Offset: 0x000124C9
		// (set) Token: 0x0600025B RID: 603 RVA: 0x000142D1 File Offset: 0x000124D1
		internal MBReadOnlyList<SkillEffect> AllSkillEffects { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600025C RID: 604 RVA: 0x000142DA File Offset: 0x000124DA
		// (set) Token: 0x0600025D RID: 605 RVA: 0x000142E2 File Offset: 0x000124E2
		internal MBReadOnlyList<FeatObject> AllFeats { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000142EB File Offset: 0x000124EB
		// (set) Token: 0x0600025F RID: 607 RVA: 0x000142F3 File Offset: 0x000124F3
		internal MBReadOnlyList<SkillObject> AllSkills { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000142FC File Offset: 0x000124FC
		// (set) Token: 0x06000261 RID: 609 RVA: 0x00014304 File Offset: 0x00012504
		internal MBReadOnlyList<SiegeEngineType> AllSiegeEngineTypes { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0001430D File Offset: 0x0001250D
		// (set) Token: 0x06000263 RID: 611 RVA: 0x00014315 File Offset: 0x00012515
		internal MBReadOnlyList<ItemCategory> AllItemCategories { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0001431E File Offset: 0x0001251E
		// (set) Token: 0x06000265 RID: 613 RVA: 0x00014326 File Offset: 0x00012526
		internal MBReadOnlyList<CharacterAttribute> AllCharacterAttributes { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0001432F File Offset: 0x0001252F
		// (set) Token: 0x06000267 RID: 615 RVA: 0x00014337 File Offset: 0x00012537
		internal MBReadOnlyList<ItemObject> AllItems { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00014340 File Offset: 0x00012540
		// (set) Token: 0x06000269 RID: 617 RVA: 0x00014348 File Offset: 0x00012548
		public float EstimatedMaximumLordPartySpeedExceptPlayer { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00014351 File Offset: 0x00012551
		// (set) Token: 0x0600026B RID: 619 RVA: 0x00014359 File Offset: 0x00012559
		public float EstimatedAverageLordPartySpeed { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00014362 File Offset: 0x00012562
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0001436A File Offset: 0x0001256A
		public float EstimatedAverageCaravanPartySpeed { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00014373 File Offset: 0x00012573
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0001437B File Offset: 0x0001257B
		public float EstimatedAverageVillagerPartySpeed { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00014384 File Offset: 0x00012584
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0001438C File Offset: 0x0001258C
		public float EstimatedAverageBanditPartySpeed { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00014395 File Offset: 0x00012595
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0001439D File Offset: 0x0001259D
		public float EstimatedAverageLordPartyNavalSpeed { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000143A6 File Offset: 0x000125A6
		// (set) Token: 0x06000275 RID: 629 RVA: 0x000143AE File Offset: 0x000125AE
		public float EstimatedAverageCaravanPartyNavalSpeed { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000143B7 File Offset: 0x000125B7
		// (set) Token: 0x06000277 RID: 631 RVA: 0x000143BF File Offset: 0x000125BF
		public float EstimatedAverageVillagerPartyNavalSpeed { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000143C8 File Offset: 0x000125C8
		// (set) Token: 0x06000279 RID: 633 RVA: 0x000143D0 File Offset: 0x000125D0
		public float EstimatedAverageBanditPartyNavalSpeed { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000143D9 File Offset: 0x000125D9
		// (set) Token: 0x0600027B RID: 635 RVA: 0x000143E1 File Offset: 0x000125E1
		[SaveableProperty(100)]
		internal MapTimeTracker MapTimeTracker { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600027C RID: 636 RVA: 0x000143EA File Offset: 0x000125EA
		// (set) Token: 0x0600027D RID: 637 RVA: 0x000143F2 File Offset: 0x000125F2
		public bool TimeControlModeLock { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600027E RID: 638 RVA: 0x000143FB File Offset: 0x000125FB
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00014403 File Offset: 0x00012603
		public CampaignTimeControlMode TimeControlMode
		{
			get
			{
				return this._timeControlMode;
			}
			set
			{
				if (!this.TimeControlModeLock && value != this._timeControlMode)
				{
					this._timeControlMode = value;
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0001441D File Offset: 0x0001261D
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00014425 File Offset: 0x00012625
		public bool IsMapTooltipLongForm { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0001442E File Offset: 0x0001262E
		// (set) Token: 0x06000283 RID: 643 RVA: 0x00014436 File Offset: 0x00012636
		public float SpeedUpMultiplier { get; set; } = 4f;

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0001443F File Offset: 0x0001263F
		public float CampaignDt
		{
			get
			{
				return this._dt;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00014447 File Offset: 0x00012647
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0001444F File Offset: 0x0001264F
		public bool TrueSight { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00014458 File Offset: 0x00012658
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0001445F File Offset: 0x0001265F
		public static Campaign Current { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00014467 File Offset: 0x00012667
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0001446F File Offset: 0x0001266F
		[SaveableProperty(37)]
		public CampaignGameMode GameMode { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00014478 File Offset: 0x00012678
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00014480 File Offset: 0x00012680
		[SaveableProperty(38)]
		public float PlayerProgress { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00014489 File Offset: 0x00012689
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00014491 File Offset: 0x00012691
		public GameMenuManager GameMenuManager { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0001449A File Offset: 0x0001269A
		public GameModels Models
		{
			get
			{
				return this._gameModels;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000290 RID: 656 RVA: 0x000144A2 File Offset: 0x000126A2
		// (set) Token: 0x06000291 RID: 657 RVA: 0x000144AA File Offset: 0x000126AA
		public SandBoxManager SandBoxManager { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000292 RID: 658 RVA: 0x000144B3 File Offset: 0x000126B3
		public Campaign.GameLoadingType CampaignGameLoadingType
		{
			get
			{
				return this._gameLoadingType;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000144BC File Offset: 0x000126BC
		public Campaign(CampaignGameMode gameMode)
		{
			this.GameMode = gameMode;
			this.Options = new CampaignOptions();
			this.CampaignObjectManager = new CampaignObjectManager();
			this.CurrentConversationContext = ConversationContext.Default;
			this.QuestManager = new QuestManager();
			this.IssueManager = new IssueManager();
			this.FactionManager = new FactionManager();
			this.CharacterRelationManager = new CharacterRelationManager();
			this.Romance = new Romance();
			this.PlayerCaptivity = new PlayerCaptivity();
			this.BarterManager = new BarterManager();
			this.GameMenuCallbackManager = new GameMenuCallbackManager();
			this._campaignPeriodicEventManager = new CampaignPeriodicEventManager();
			this._tickData = new CampaignTickCacheDataStore();
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000294 RID: 660 RVA: 0x000145B0 File Offset: 0x000127B0
		// (set) Token: 0x06000295 RID: 661 RVA: 0x000145B8 File Offset: 0x000127B8
		[SaveableProperty(40)]
		public SiegeEventManager SiegeEventManager { get; internal set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000145C1 File Offset: 0x000127C1
		// (set) Token: 0x06000297 RID: 663 RVA: 0x000145C9 File Offset: 0x000127C9
		[SaveableProperty(41)]
		public MapEventManager MapEventManager { get; internal set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000145D2 File Offset: 0x000127D2
		// (set) Token: 0x06000299 RID: 665 RVA: 0x000145DA File Offset: 0x000127DA
		[SaveableProperty(43)]
		public MapMarkerManager MapMarkerManager { get; internal set; }

		// Token: 0x0600029A RID: 666 RVA: 0x000145E3 File Offset: 0x000127E3
		public void AddCustomManager<T>() where T : ICustomSystemManager, new()
		{
			this._customManagers.Add(new T());
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000145FC File Offset: 0x000127FC
		public T GetCustomManager<T>() where T : ICustomSystemManager
		{
			foreach (ICustomSystemManager customSystemManager in this._customManagers)
			{
				if (customSystemManager.GetType() == typeof(T))
				{
					return (T)((object)customSystemManager);
				}
			}
			return default(T);
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00014674 File Offset: 0x00012874
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0001467C File Offset: 0x0001287C
		internal CampaignEvents CampaignEvents { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00014688 File Offset: 0x00012888
		public MenuContext CurrentMenuContext
		{
			get
			{
				GameStateManager gameStateManager = base.CurrentGame.GameStateManager;
				TutorialState tutorialState = gameStateManager.ActiveState as TutorialState;
				if (tutorialState != null)
				{
					return tutorialState.MenuContext;
				}
				MapState mapState = gameStateManager.ActiveState as MapState;
				if (mapState != null)
				{
					return mapState.MenuContext;
				}
				GameState activeState = gameStateManager.ActiveState;
				MapState mapState2;
				if (((activeState != null) ? activeState.Predecessor : null) != null && (mapState2 = gameStateManager.ActiveState.Predecessor as MapState) != null)
				{
					return mapState2.MenuContext;
				}
				return null;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600029F RID: 671 RVA: 0x000146FD File Offset: 0x000128FD
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x00014705 File Offset: 0x00012905
		internal List<MBCampaignEvent> CustomPeriodicCampaignEvents { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0001470E File Offset: 0x0001290E
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00014716 File Offset: 0x00012916
		public bool IsMainPartyWaiting
		{
			get
			{
				return this._isMainPartyWaiting;
			}
			private set
			{
				this._isMainPartyWaiting = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0001471F File Offset: 0x0001291F
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00014727 File Offset: 0x00012927
		[SaveableProperty(45)]
		private int _curMapFrame { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00014730 File Offset: 0x00012930
		internal LocatorGrid<Settlement> SettlementLocator
		{
			get
			{
				LocatorGrid<Settlement> locatorGrid;
				if ((locatorGrid = this._settlementLocator) == null)
				{
					locatorGrid = (this._settlementLocator = new LocatorGrid<Settlement>(5f, 32, 32));
				}
				return locatorGrid;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00014760 File Offset: 0x00012960
		internal LocatorGrid<MobileParty> MobilePartyLocator
		{
			get
			{
				LocatorGrid<MobileParty> locatorGrid;
				if ((locatorGrid = this._mobilePartyLocator) == null)
				{
					locatorGrid = (this._mobilePartyLocator = new LocatorGrid<MobileParty>(5f, 32, 32));
				}
				return locatorGrid;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0001478E File Offset: 0x0001298E
		public IMapScene MapSceneWrapper
		{
			get
			{
				return this._mapSceneWrapper;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00014796 File Offset: 0x00012996
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0001479E File Offset: 0x0001299E
		[SaveableProperty(54)]
		public PlayerEncounter PlayerEncounter { get; internal set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000147A7 File Offset: 0x000129A7
		// (set) Token: 0x060002AB RID: 683 RVA: 0x000147AF File Offset: 0x000129AF
		[CachedData]
		internal LocationEncounter LocationEncounter { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000147B8 File Offset: 0x000129B8
		// (set) Token: 0x060002AD RID: 685 RVA: 0x000147C0 File Offset: 0x000129C0
		internal NameGenerator NameGenerator { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002AE RID: 686 RVA: 0x000147C9 File Offset: 0x000129C9
		// (set) Token: 0x060002AF RID: 687 RVA: 0x000147D1 File Offset: 0x000129D1
		[SaveableProperty(58)]
		public BarterManager BarterManager { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x000147DA File Offset: 0x000129DA
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x000147E2 File Offset: 0x000129E2
		[SaveableProperty(69)]
		public bool IsMainHeroDisguised { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x000147EB File Offset: 0x000129EB
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x000147F3 File Offset: 0x000129F3
		public Equipment DeadBattleEquipment { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x000147FC File Offset: 0x000129FC
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x00014804 File Offset: 0x00012A04
		public Equipment DeadCivilianEquipment { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0001480D File Offset: 0x00012A0D
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x00014815 File Offset: 0x00012A15
		public Equipment DefaultStealthEquipment { get; private set; }

		// Token: 0x060002B8 RID: 696 RVA: 0x00014820 File Offset: 0x00012A20
		public void InitializeMainParty()
		{
			this.InitializeSinglePlayerReferences();
			CampaignVec2 campaignVec = NavigationHelper.FindReachablePointAroundPosition(this.Settlements.Find((Settlement x) => x.IsTown).GatePosition, MobileParty.MainParty.NavigationCapability, 20f, 0f, false);
			this.MainParty.InitializeMobilePartyAtPosition(base.CurrentGame.ObjectManager.GetObject<PartyTemplateObject>("main_hero_party_template"), campaignVec);
			LordPartyComponent.ConvertPartyToLordParty(this.MainParty, Hero.MainHero, Hero.MainHero);
			this.MainParty.ItemRoster.AddToCounts(DefaultItems.Grain, 1);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000148CC File Offset: 0x00012ACC
		[LoadInitializationCallback]
		private void OnLoad(MetaData metaData, ObjectLoadData objectLoadData)
		{
			this._campaignEntitySystem = new EntitySystem<CampaignEntityComponent>();
			this.PlayerFormationPreferences = this._playerFormationPreferences.GetReadOnlyDictionary<CharacterObject, FormationClass>();
			this.SpeedUpMultiplier = 4f;
			if (this.UniqueGameId == null && MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.2.2", 0))
			{
				this.UniqueGameId = "oldSave";
			}
			if (MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.3.0", 0))
			{
				if (this._previouslyUsedModules == null)
				{
					this._previouslyUsedModules = new MBList<string>();
				}
				MBList<string> mblist = new MBList<string>(this._previouslyUsedModules);
				this._previouslyUsedModules.Clear();
				if (mblist.Any<string>())
				{
					this._previouslyUsedModules.Add(string.Join(MBSaveLoad.ModuleCodeSeperator.ToString(), mblist.Select<string, string>((string x) => x + MBSaveLoad.ModuleVersionSeperator.ToString() + ApplicationVersion.Empty.ToString())));
				}
			}
			if (MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.3.0", 0))
			{
				this.UnlockedFigureheadsByMainHero = new List<Figurehead>();
				this._customManagers = new List<ICustomSystemManager>();
				this.MapMarkerManager = new MapMarkerManager();
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000149FC File Offset: 0x00012BFC
		private void InitializeForSavedGame()
		{
			foreach (Settlement settlement in Settlement.All)
			{
				settlement.Party.OnFinishLoadState();
			}
			foreach (MobileParty mobileParty in this.MobileParties.ToList<MobileParty>())
			{
				mobileParty.Party.OnFinishLoadState();
			}
			foreach (Settlement settlement2 in Settlement.All)
			{
				settlement2.OnFinishLoadState();
			}
			this.GameMenuCallbackManager = new GameMenuCallbackManager();
			this.GameMenuCallbackManager.OnGameLoad();
			this.IssueManager.InitializeForSavedGame();
			this.MinSettlementX = float.MaxValue;
			this.MinSettlementY = float.MaxValue;
			this.MaxSettlementX = float.MinValue;
			this.MaxSettlementY = float.MinValue;
			foreach (Settlement settlement3 in Settlement.All)
			{
				if (settlement3.Position.X < this.MinSettlementX)
				{
					this.MinSettlementX = settlement3.Position.X;
				}
				if (settlement3.Position.Y < this.MinSettlementY)
				{
					this.MinSettlementY = settlement3.Position.Y;
				}
				if (settlement3.Position.X > this.MaxSettlementX)
				{
					this.MaxSettlementX = settlement3.Position.X;
				}
				if (settlement3.Position.Y > this.MaxSettlementY)
				{
					this.MaxSettlementY = settlement3.Position.Y;
				}
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00014C0C File Offset: 0x00012E0C
		private void OnGameLoaded(CampaignGameStarter starter)
		{
			TroopRoster.CalculateCachedStatsOnLoad();
			this._tickData = new CampaignTickCacheDataStore();
			base.ObjectManager.PreAfterLoad();
			this.CampaignObjectManager.PreAfterLoad();
			this.IssueManager.PreAfterLoad();
			this.QuestManager.PreAfterLoad();
			base.ObjectManager.AfterLoad();
			this.CampaignObjectManager.AfterLoad();
			this.CharacterRelationManager.AfterLoad();
			this.FactionManager.AfterLoad();
			CampaignEventDispatcher.Instance.OnGameEarlyLoaded(starter);
			CampaignEventDispatcher.Instance.OnGameLoaded(starter);
			this.InitializeForSavedGame();
			this._tickData.InitializeDataCache();
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00014CA8 File Offset: 0x00012EA8
		private void OnDataLoadFinished(CampaignGameStarter starter)
		{
			this._towns = new MBList<Town>();
			this._castles = new MBList<Town>();
			this._villages = new MBList<Village>();
			this._hideouts = new MBList<Hideout>();
			for (int i = 0; i < Settlement.All.Count; i++)
			{
				Settlement settlement = Settlement.All[i];
				if (settlement.IsTown)
				{
					this._towns.Add(settlement.Town);
				}
				else if (settlement.IsCastle)
				{
					this._castles.Add(settlement.Town);
				}
				else if (settlement.IsVillage)
				{
					this._villages.Add(settlement.Village);
				}
				else if (settlement.IsHideout)
				{
					this._hideouts.Add(settlement.Hideout);
				}
			}
			this._campaignPeriodicEventManager.InitializeTickers();
			this.CreateCampaignEvents();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00014D80 File Offset: 0x00012F80
		private void OnSessionStart(CampaignGameStarter starter)
		{
			CampaignEventDispatcher.Instance.OnSessionStart(starter);
			CampaignEventDispatcher.Instance.OnAfterSessionStart(starter);
			CampaignEvents.DailyTickSettlementEvent.AddNonSerializedListener(this, new Action<Settlement>(this.DailyTickSettlement));
			this.ConversationManager.Build();
			foreach (Settlement settlement in this.Settlements)
			{
				settlement.OnSessionStart();
			}
			this.IsCraftingEnabled = true;
			this.IsBannerEditorEnabled = true;
			this.IsFaceGenEnabled = true;
			this.MapEventManager.OnAfterLoad();
			this.SiegeEventManager.OnAfterLoad();
			this.KingdomManager.RegisterEvents();
			this.KingdomManager.OnSessionStart();
			this.CampaignInformationManager.RegisterEvents();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00014E54 File Offset: 0x00013054
		private void DailyTickSettlement(Settlement settlement)
		{
			if (settlement.IsVillage)
			{
				settlement.Village.DailyTick();
				return;
			}
			if (settlement.Town != null)
			{
				settlement.Town.DailyTick();
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00014E80 File Offset: 0x00013080
		private void GameInitTick()
		{
			foreach (Settlement settlement in Settlement.All)
			{
				settlement.Party.UpdateVisibilityAndInspected(MobileParty.MainParty.Position, 0f);
			}
			foreach (MobileParty mobileParty in this.MobileParties)
			{
				mobileParty.Party.UpdateVisibilityAndInspected(MobileParty.MainParty.Position, 0f);
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00014F38 File Offset: 0x00013138
		internal void HourlyTick(MBCampaignEvent campaignEvent, object[] delegateParams)
		{
			CampaignEventDispatcher.Instance.HourlyTick();
			MapState mapState = Game.Current.GameStateManager.ActiveState as MapState;
			if (mapState == null)
			{
				return;
			}
			mapState.OnHourlyTick();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00014F62 File Offset: 0x00013162
		internal void QuarterHourlyTick(MBCampaignEvent campaignEvent, object[] delegateParams)
		{
			CampaignEventDispatcher.Instance.QuarterHourlyTick();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00014F70 File Offset: 0x00013170
		internal void DailyTick(MBCampaignEvent campaignEvent, object[] delegateParams)
		{
			this.PlayerProgress = (this.PlayerProgress + this.Models.PlayerProgressionModel.GetPlayerProgress()) / 2f;
			Debug.Print("Before Daily Tick: " + CampaignTime.Now.ToString(), 0, Debug.DebugColor.White, 17592186044416UL);
			CampaignEventDispatcher.Instance.DailyTick();
			if ((int)this.Models.CampaignTimeModel.CampaignStartTime.ElapsedDaysUntilNow % CampaignTime.DaysInWeek == 0)
			{
				CampaignEventDispatcher.Instance.WeeklyTick();
				this.OnWeeklyTick();
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00015009 File Offset: 0x00013209
		public void WaitAsyncTasks()
		{
			if (this.CampaignLateAITickTask != null)
			{
				this.CampaignLateAITickTask.Wait();
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001501E File Offset: 0x0001321E
		private void OnWeeklyTick()
		{
			this.LogEntryHistory.DeleteOutdatedLogs();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0001502C File Offset: 0x0001322C
		public CampaignTimeControlMode GetSimplifiedTimeControlMode()
		{
			switch (this.TimeControlMode)
			{
			case CampaignTimeControlMode.Stop:
				return CampaignTimeControlMode.Stop;
			case CampaignTimeControlMode.UnstoppablePlay:
				return CampaignTimeControlMode.UnstoppablePlay;
			case CampaignTimeControlMode.UnstoppableFastForward:
			case CampaignTimeControlMode.UnstoppableFastForwardForPartyWaitTime:
				return CampaignTimeControlMode.UnstoppableFastForward;
			case CampaignTimeControlMode.StoppablePlay:
				if (!this.IsMainPartyWaiting)
				{
					return CampaignTimeControlMode.StoppablePlay;
				}
				return CampaignTimeControlMode.Stop;
			case CampaignTimeControlMode.StoppableFastForward:
				if (!this.IsMainPartyWaiting)
				{
					return CampaignTimeControlMode.StoppableFastForward;
				}
				return CampaignTimeControlMode.Stop;
			default:
				return CampaignTimeControlMode.Stop;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0001507F File Offset: 0x0001327F
		private void CheckMainPartyNeedsUpdate()
		{
			MobileParty.MainParty.Ai.CheckPartyNeedsUpdate();
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00015090 File Offset: 0x00013290
		private void TickMapTime(float realDt)
		{
			float num = 0f;
			float speedUpMultiplier = this.SpeedUpMultiplier;
			float num2 = 0.25f * realDt;
			this.IsMainPartyWaiting = MobileParty.MainParty.ComputeIsWaiting();
			switch (this.TimeControlMode)
			{
			case CampaignTimeControlMode.Stop:
			case CampaignTimeControlMode.FastForwardStop:
				break;
			case CampaignTimeControlMode.UnstoppablePlay:
				num = num2;
				break;
			case CampaignTimeControlMode.UnstoppableFastForward:
			case CampaignTimeControlMode.UnstoppableFastForwardForPartyWaitTime:
				num = num2 * speedUpMultiplier;
				break;
			case CampaignTimeControlMode.StoppablePlay:
				if (!this.IsMainPartyWaiting)
				{
					num = num2;
				}
				break;
			case CampaignTimeControlMode.StoppableFastForward:
				if (!this.IsMainPartyWaiting)
				{
					num = num2 * speedUpMultiplier;
				}
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			this._dt = num;
			this.MapTimeTracker.Tick(4320f * num);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00015130 File Offset: 0x00013330
		public void OnGameOver()
		{
			if (CampaignOptions.IsIronmanMode)
			{
				this.SaveHandler.QuickSaveCurrentGame();
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00015144 File Offset: 0x00013344
		internal void RealTick(float realDt)
		{
			this.WaitAsyncTasks();
			this.CheckMainPartyNeedsUpdate();
			this.TickMapTime(realDt);
			foreach (CampaignEntityComponent campaignEntityComponent in this._campaignEntitySystem.GetComponents())
			{
				campaignEntityComponent.OnTick(realDt, this._dt);
			}
			if (!this.GameStarted)
			{
				this.GameStarted = true;
				this._tickData.InitializeDataCache();
				this.SiegeEventManager.Tick(this._dt);
			}
			this._tickData.RealTick(this._dt, realDt);
			this.SiegeEventManager.Tick(this._dt);
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00015204 File Offset: 0x00013404
		public static float CurrentTime
		{
			get
			{
				return (float)CampaignTime.Now.ToHours;
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00015220 File Offset: 0x00013420
		public void SetTimeSpeed(int speed)
		{
			switch (speed)
			{
			case 0:
				if (this.TimeControlMode == CampaignTimeControlMode.UnstoppableFastForward || this.TimeControlMode == CampaignTimeControlMode.StoppableFastForward)
				{
					this.TimeControlMode = CampaignTimeControlMode.FastForwardStop;
					return;
				}
				if (this.TimeControlMode != CampaignTimeControlMode.FastForwardStop && this.TimeControlMode != CampaignTimeControlMode.Stop)
				{
					this.TimeControlMode = CampaignTimeControlMode.Stop;
					return;
				}
				break;
			case 1:
				if (((this.TimeControlMode == CampaignTimeControlMode.Stop || this.TimeControlMode == CampaignTimeControlMode.FastForwardStop) && this.MainParty.DefaultBehavior == AiBehavior.Hold) || this.IsMainPartyWaiting || (MobileParty.MainParty.Army != null && MobileParty.MainParty.Army.LeaderParty != MobileParty.MainParty))
				{
					this.TimeControlMode = CampaignTimeControlMode.UnstoppablePlay;
					return;
				}
				this.TimeControlMode = CampaignTimeControlMode.StoppablePlay;
				return;
			case 2:
				if (((this.TimeControlMode == CampaignTimeControlMode.Stop || this.TimeControlMode == CampaignTimeControlMode.FastForwardStop) && this.MainParty.DefaultBehavior == AiBehavior.Hold) || this.IsMainPartyWaiting || (MobileParty.MainParty.Army != null && MobileParty.MainParty.Army.LeaderParty != MobileParty.MainParty))
				{
					this.TimeControlMode = CampaignTimeControlMode.UnstoppableFastForward;
					return;
				}
				this.TimeControlMode = CampaignTimeControlMode.StoppableFastForward;
				break;
			default:
				return;
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00015328 File Offset: 0x00013528
		public static void LateAITick()
		{
			Campaign.Current.LateAITickAux();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00015334 File Offset: 0x00013534
		internal void LateAITickAux()
		{
			if (this._dt > 0f || this.CurrentTickCount < 3)
			{
				this.PartiesThink(this._dt);
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00015358 File Offset: 0x00013558
		internal void Tick()
		{
			int curMapFrame = this._curMapFrame;
			this._curMapFrame = curMapFrame + 1;
			this.CurrentTickCount++;
			if (this._dt > 0f || this.CurrentTickCount < 3)
			{
				CampaignEventDispatcher.Instance.Tick(this._dt);
				this._campaignPeriodicEventManager.OnTick(this._dt);
				this.MapEventManager.Tick();
				this._lastNonZeroDtFrame = this._curMapFrame;
				this._campaignPeriodicEventManager.MobilePartyHourlyTick();
			}
			if (this._dt > 0f)
			{
				this._campaignPeriodicEventManager.TickPeriodicEvents();
			}
			this._tickData.Tick();
			Campaign.Current.PlayerCaptivity.Update(this._dt);
			if (this._dt > 0f || (MobileParty.MainParty.MapEvent == null && this._curMapFrame == this._lastNonZeroDtFrame + 1))
			{
				EncounterManager.Tick(this._dt);
				MapState mapState = Game.Current.GameStateManager.ActiveState as MapState;
				if (mapState != null && mapState.AtMenu && !mapState.MenuContext.GameMenu.IsWaitActive)
				{
					this._dt = 0f;
				}
			}
			if (this._dt > 0f || this.CurrentTickCount < 3)
			{
				this._campaignPeriodicEventManager.TickPartialHourlyAi();
			}
			MapState mapState2;
			if ((mapState2 = Game.Current.GameStateManager.ActiveState as MapState) != null && mapState2.NextIncident != null)
			{
				if (mapState2.NextIncident.CanIncidentBeInvoked())
				{
					mapState2.StartIncident(mapState2.NextIncident);
				}
				mapState2.NextIncident = null;
			}
			MapState mapState3;
			if ((mapState3 = Game.Current.GameStateManager.ActiveState as MapState) != null && !mapState3.AtMenu)
			{
				string genericStateMenu = this.Models.EncounterGameMenuModel.GetGenericStateMenu();
				if (!string.IsNullOrEmpty(genericStateMenu))
				{
					GameMenu.ActivateGameMenu(genericStateMenu);
				}
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00015528 File Offset: 0x00013728
		private void CreateCampaignEvents()
		{
			long numTicks = (CampaignTime.Now - Campaign.Current.Models.CampaignTimeModel.CampaignStartTime).NumTicks;
			CampaignTime campaignTime = CampaignTime.Days(1f);
			if (numTicks % CampaignTime.TimeTicksPerDay != 0L)
			{
				campaignTime = CampaignTime.Days((float)(numTicks % CampaignTime.TimeTicksPerDay) / (float)CampaignTime.TimeTicksPerDay);
			}
			this._dailyTickEvent = CampaignPeriodicEventManager.CreatePeriodicEvent(CampaignTime.Days(1f), campaignTime);
			this._dailyTickEvent.AddHandler(new MBCampaignEvent.CampaignEventDelegate(this.DailyTick));
			CampaignTime campaignTime2 = CampaignTime.Hours(0.5f);
			if (numTicks % CampaignTime.TimeTicksPerHour != 0L)
			{
				campaignTime2 = CampaignTime.Hours((float)(numTicks % CampaignTime.TimeTicksPerHour) / (float)CampaignTime.TimeTicksPerHour);
			}
			this._hourlyTickEvent = CampaignPeriodicEventManager.CreatePeriodicEvent(CampaignTime.Hours(1f), campaignTime2);
			this._hourlyTickEvent.AddHandler(new MBCampaignEvent.CampaignEventDelegate(this.HourlyTick));
			campaignTime2 = CampaignTime.Hours(0.125f);
			if (numTicks % (CampaignTime.TimeTicksPerHour / 4L) != 0L)
			{
				campaignTime2 = CampaignTime.Hours((float)(numTicks % (CampaignTime.TimeTicksPerHour / 4L)) / (float)(CampaignTime.TimeTicksPerHour / 4L));
			}
			this._QuarterHourlyTickEvent = CampaignPeriodicEventManager.CreatePeriodicEvent(CampaignTime.Hours(0.25f), campaignTime2);
			this._QuarterHourlyTickEvent.AddHandler(new MBCampaignEvent.CampaignEventDelegate(this.QuarterHourlyTick));
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00015668 File Offset: 0x00013868
		private void PartiesThink(float dt)
		{
			for (int i = 0; i < this.MobileParties.Count; i++)
			{
				this.MobileParties[i].Ai.Tick(dt);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000156A4 File Offset: 0x000138A4
		public TComponent GetEntityComponent<TComponent>() where TComponent : CampaignEntityComponent
		{
			EntitySystem<CampaignEntityComponent> campaignEntitySystem = this._campaignEntitySystem;
			if (campaignEntitySystem == null)
			{
				return default(TComponent);
			}
			return campaignEntitySystem.GetComponent<TComponent>();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000156CA File Offset: 0x000138CA
		public TComponent AddEntityComponent<TComponent>() where TComponent : CampaignEntityComponent, new()
		{
			return this._campaignEntitySystem.AddComponent<TComponent>();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000156D7 File Offset: 0x000138D7
		public void RemoveEntityComponent<TComponent>() where TComponent : CampaignEntityComponent
		{
			this._campaignEntitySystem.RemoveComponent<TComponent>();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000156E4 File Offset: 0x000138E4
		public void RemoveEntityComponent<TComponent>(TComponent component) where TComponent : CampaignEntityComponent
		{
			this._campaignEntitySystem.RemoveComponent(component);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000156F7 File Offset: 0x000138F7
		public List<TComponent> GetComponents<TComponent>() where TComponent : CampaignEntityComponent
		{
			return this._campaignEntitySystem.GetComponents<TComponent>();
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00015704 File Offset: 0x00013904
		public MBReadOnlyList<CampaignEntityComponent> CampaignEntityComponents
		{
			get
			{
				return this._campaignEntitySystem.Components;
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00015711 File Offset: 0x00013911
		public T GetCampaignBehavior<T>()
		{
			return this._campaignBehaviorManager.GetBehavior<T>();
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0001571E File Offset: 0x0001391E
		public IEnumerable<T> GetCampaignBehaviors<T>()
		{
			return this._campaignBehaviorManager.GetBehaviors<T>();
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001572B File Offset: 0x0001392B
		public void AddCampaignBehaviorManager(ICampaignBehaviorManager manager)
		{
			this._campaignBehaviorManager = manager;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00015734 File Offset: 0x00013934
		public MBReadOnlyList<Hero> AliveHeroes
		{
			get
			{
				return this.CampaignObjectManager.AliveHeroes;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00015741 File Offset: 0x00013941
		public MBReadOnlyList<Hero> DeadOrDisabledHeroes
		{
			get
			{
				return this.CampaignObjectManager.DeadOrDisabledHeroes;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0001574E File Offset: 0x0001394E
		public MBReadOnlyList<MobileParty> MobileParties
		{
			get
			{
				return this.CampaignObjectManager.MobileParties;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0001575B File Offset: 0x0001395B
		public MBReadOnlyList<MobileParty> CaravanParties
		{
			get
			{
				return this.CampaignObjectManager.CaravanParties;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00015768 File Offset: 0x00013968
		public MBReadOnlyList<MobileParty> PatrolParties
		{
			get
			{
				return this.CampaignObjectManager.PatrolParties;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00015775 File Offset: 0x00013975
		public MBReadOnlyList<MobileParty> VillagerParties
		{
			get
			{
				return this.CampaignObjectManager.VillagerParties;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00015782 File Offset: 0x00013982
		public MBReadOnlyList<MobileParty> MilitiaParties
		{
			get
			{
				return this.CampaignObjectManager.MilitiaParties;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0001578F File Offset: 0x0001398F
		public MBReadOnlyList<MobileParty> GarrisonParties
		{
			get
			{
				return this.CampaignObjectManager.GarrisonParties;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0001579C File Offset: 0x0001399C
		public MBReadOnlyList<MobileParty> CustomParties
		{
			get
			{
				return this.CampaignObjectManager.CustomParties;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x000157A9 File Offset: 0x000139A9
		public MBReadOnlyList<MobileParty> LordParties
		{
			get
			{
				return this.CampaignObjectManager.LordParties;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000157B6 File Offset: 0x000139B6
		public MBReadOnlyList<MobileParty> BanditParties
		{
			get
			{
				return this.CampaignObjectManager.BanditParties;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x000157C3 File Offset: 0x000139C3
		public MBReadOnlyList<MobileParty> PartiesWithoutPartyComponent
		{
			get
			{
				return this.CampaignObjectManager.PartiesWithoutPartyComponent;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x000157D0 File Offset: 0x000139D0
		public MBReadOnlyList<Settlement> Settlements
		{
			get
			{
				return this.CampaignObjectManager.Settlements;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x000157DD File Offset: 0x000139DD
		public IEnumerable<IFaction> Factions
		{
			get
			{
				return this.CampaignObjectManager.Factions;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x000157EA File Offset: 0x000139EA
		public MBReadOnlyList<Kingdom> Kingdoms
		{
			get
			{
				return this.CampaignObjectManager.Kingdoms;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x000157F7 File Offset: 0x000139F7
		public MBReadOnlyList<Clan> Clans
		{
			get
			{
				return this.CampaignObjectManager.Clans;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00015804 File Offset: 0x00013A04
		public MBReadOnlyList<CharacterObject> Characters
		{
			get
			{
				return this._characters;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0001580C File Offset: 0x00013A0C
		public MBReadOnlyList<WorkshopType> Workshops
		{
			get
			{
				return this._workshops;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00015814 File Offset: 0x00013A14
		public MBReadOnlyList<ItemModifier> ItemModifiers
		{
			get
			{
				return this._itemModifiers;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0001581C File Offset: 0x00013A1C
		public MBReadOnlyList<ItemModifierGroup> ItemModifierGroups
		{
			get
			{
				return this._itemModifierGroups;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00015824 File Offset: 0x00013A24
		public MBReadOnlyList<Concept> Concepts
		{
			get
			{
				return this._concepts;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0001582C File Offset: 0x00013A2C
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00015834 File Offset: 0x00013A34
		[SaveableProperty(60)]
		public MobileParty MainParty { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0001583D File Offset: 0x00013A3D
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00015845 File Offset: 0x00013A45
		public PartyBase CameraFollowParty
		{
			get
			{
				return this._cameraFollowParty;
			}
			set
			{
				this._cameraFollowParty = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0001584E File Offset: 0x00013A4E
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00015856 File Offset: 0x00013A56
		[SaveableProperty(62)]
		public CampaignInformationManager CampaignInformationManager { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0001585F File Offset: 0x00013A5F
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00015867 File Offset: 0x00013A67
		[SaveableProperty(63)]
		public VisualTrackerManager VisualTrackerManager { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00015870 File Offset: 0x00013A70
		public LogEntryHistory LogEntryHistory
		{
			get
			{
				return this._logEntryHistory;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00015878 File Offset: 0x00013A78
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x00015880 File Offset: 0x00013A80
		public EncyclopediaManager EncyclopediaManager { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00015889 File Offset: 0x00013A89
		// (set) Token: 0x060002FB RID: 763 RVA: 0x00015891 File Offset: 0x00013A91
		public ConversationManager ConversationManager { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0001589A File Offset: 0x00013A9A
		public bool IsDay
		{
			get
			{
				return !this.IsNight;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000158A8 File Offset: 0x00013AA8
		public bool IsNight
		{
			get
			{
				return CampaignTime.Now.IsNightTime;
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000158C2 File Offset: 0x00013AC2
		internal int GeneratePartyId(PartyBase party)
		{
			int lastPartyIndex = this._lastPartyIndex;
			this._lastPartyIndex++;
			return lastPartyIndex;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000158D8 File Offset: 0x00013AD8
		private void LoadMapScene()
		{
			this._mapSceneWrapper = this.MapSceneCreator.CreateMapScene();
			this._mapSceneWrapper.SetSceneLevels(new List<string> { "level_1", "level_2", "level_3", "siege", "raid", "burned" });
			this._mapSceneWrapper.Load();
			Vec2 vec;
			Vec2 vec2;
			float num;
			this._mapSceneWrapper.GetMapBorders(out vec, out vec2, out num);
			Campaign.MapMinimumPosition = vec;
			Campaign.MapMaximumPosition = vec2;
			Campaign.MapMaximumHeight = num;
			Campaign.MapDiagonal = Campaign.MapMinimumPosition.Distance(Campaign.MapMaximumPosition);
			Campaign.MapDiagonalSquared = Campaign.MapDiagonal * Campaign.MapDiagonal;
			Campaign.PlayerRegionSwitchCostFromLandToSea = (int)(Campaign.MapDiagonal * (float)this.Models.MapDistanceModel.RegionSwitchCostFromLandToSea * 0.2f);
			Campaign.PathFindingMaxCostLimit = Math.Max(Campaign.PlayerRegionSwitchCostFromLandToSea * 100, (int)(Campaign.MapDiagonal * 500f));
			this._mapSceneWrapper.AfterLoad();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000159EC File Offset: 0x00013BEC
		private void InitializeCachedLists()
		{
			MBObjectManager objectManager = Game.Current.ObjectManager;
			this._characters = objectManager.GetObjectTypeList<CharacterObject>();
			this._workshops = objectManager.GetObjectTypeList<WorkshopType>();
			this._itemModifiers = objectManager.GetObjectTypeList<ItemModifier>();
			this._itemModifierGroups = objectManager.GetObjectTypeList<ItemModifierGroup>();
			this._concepts = objectManager.GetObjectTypeList<Concept>();
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00015A40 File Offset: 0x00013C40
		private void InitializeDefaultEquipments()
		{
			this.DeadBattleEquipment = Game.Current.ObjectManager.GetObject<MBEquipmentRoster>("default_battle_equipment_roster_neutral").DefaultEquipment;
			this.DeadCivilianEquipment = Game.Current.ObjectManager.GetObject<MBEquipmentRoster>("default_civilian_equipment_roster_neutral").DefaultEquipment;
			this.DefaultStealthEquipment = Game.Current.ObjectManager.GetObject<MBEquipmentRoster>("default_stealth_equipment_roster").DefaultEquipment;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00015AAC File Offset: 0x00013CAC
		public override void OnDestroy()
		{
			this.WaitAsyncTasks();
			GameTexts.ClearInstance();
			IMapScene mapSceneWrapper = this._mapSceneWrapper;
			if (mapSceneWrapper != null)
			{
				mapSceneWrapper.Destroy();
			}
			ConversationManager.Clear();
			MBTextManager.ClearAll();
			GameSceneDataManager.Destroy();
			this.CampaignInformationManager.DeRegisterEvents();
			ICampaignBehaviorManager campaignBehaviorManager = this._campaignBehaviorManager;
			if (campaignBehaviorManager != null)
			{
				campaignBehaviorManager.ClearBehaviors();
			}
			MBSaveLoad.OnGameDestroy();
			Campaign.Current = null;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00015B0B File Offset: 0x00013D0B
		public void InitializeSinglePlayerReferences()
		{
			this.IsSinglePlayerReferencesInitialized = true;
			this.InitializeGamePlayReferences();
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00015B1C File Offset: 0x00013D1C
		private void CreateLists()
		{
			this.AllPerks = MBObjectManager.Instance.GetObjectTypeList<PerkObject>();
			this.AllTraits = MBObjectManager.Instance.GetObjectTypeList<TraitObject>();
			this.AllEquipmentRosters = MBObjectManager.Instance.GetObjectTypeList<MBEquipmentRoster>();
			this.AllPolicies = MBObjectManager.Instance.GetObjectTypeList<PolicyObject>();
			this.AllBuildingTypes = MBObjectManager.Instance.GetObjectTypeList<BuildingType>();
			this.AllIssueEffects = MBObjectManager.Instance.GetObjectTypeList<IssueEffect>();
			this.AllSiegeStrategies = MBObjectManager.Instance.GetObjectTypeList<SiegeStrategy>();
			this.AllVillageTypes = MBObjectManager.Instance.GetObjectTypeList<VillageType>();
			this.AllSkillEffects = MBObjectManager.Instance.GetObjectTypeList<SkillEffect>();
			this.AllFeats = MBObjectManager.Instance.GetObjectTypeList<FeatObject>();
			this.AllSkills = MBObjectManager.Instance.GetObjectTypeList<SkillObject>();
			this.AllSiegeEngineTypes = MBObjectManager.Instance.GetObjectTypeList<SiegeEngineType>();
			this.AllItemCategories = MBObjectManager.Instance.GetObjectTypeList<ItemCategory>();
			this.AllCharacterAttributes = MBObjectManager.Instance.GetObjectTypeList<CharacterAttribute>();
			this.AllItems = MBObjectManager.Instance.GetObjectTypeList<ItemObject>();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00015C1C File Offset: 0x00013E1C
		private void CheckMapUpdate()
		{
			uint sceneXmlCrc = this.MapSceneWrapper.GetSceneXmlCrc();
			uint sceneNavigationMeshCrc = this.MapSceneWrapper.GetSceneNavigationMeshCrc();
			if (sceneXmlCrc != this._campaignMapSceneXmlCrc || sceneNavigationMeshCrc != this._campaignMapSceneNavigationMeshCrc)
			{
				this.CalculateCachedValues();
				foreach (Settlement settlement in this.Settlements)
				{
					settlement.CheckPositionsForMapChangeAndUpdateIfNeeded();
				}
				foreach (MapEvent mapEvent in this.MapEventManager.MapEvents)
				{
					mapEvent.CheckPositionsForMapChangeAndUpdateIfNeeded();
				}
				foreach (Kingdom kingdom in this.Kingdoms)
				{
					foreach (Army army in kingdom.Armies)
					{
						army.CheckPositionsForMapChangeAndUpdateIfNeeded();
					}
				}
				foreach (MobileParty mobileParty in this.MobileParties)
				{
					mobileParty.CheckPositionsForMapChangeAndUpdateIfNeeded();
					mobileParty.CheckAiForMapChangeAndUpdateIfNeeded();
				}
				this._campaignMapSceneXmlCrc = sceneXmlCrc;
				this._campaignMapSceneNavigationMeshCrc = sceneNavigationMeshCrc;
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00015DB4 File Offset: 0x00013FB4
		private void CalculateCachedValues()
		{
			this.EstimatedMaximumLordPartySpeedExceptPlayer = 10f;
			this.EstimatedAverageLordPartySpeed = 3.36f;
			this.EstimatedAverageCaravanPartySpeed = 4.2f;
			this.EstimatedAverageVillagerPartySpeed = 3.43f;
			this.EstimatedAverageBanditPartySpeed = 3.41f;
			this.EstimatedAverageLordPartyNavalSpeed = this.EstimatedAverageLordPartySpeed * 1.2f;
			this.EstimatedAverageCaravanPartyNavalSpeed = 3.53f;
			this.EstimatedAverageVillagerPartyNavalSpeed = 4.01f;
			this.EstimatedAverageBanditPartyNavalSpeed = 3.57f;
			this.CalculateAverageDistanceBetweenTowns();
			this.CalculateAverageWage();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00015E38 File Offset: 0x00014038
		private void CalculateAverageWage()
		{
			float num = 0f;
			float num2 = 0f;
			foreach (CultureObject cultureObject in MBObjectManager.Instance.GetObjectTypeList<CultureObject>())
			{
				if (cultureObject.IsMainCulture)
				{
					foreach (PartyTemplateStack partyTemplateStack in cultureObject.DefaultPartyTemplate.Stacks)
					{
						int troopWage = partyTemplateStack.Character.TroopWage;
						float num3 = (float)(partyTemplateStack.MaxValue + partyTemplateStack.MinValue) * 0.5f;
						num += (float)troopWage * num3;
						num2 += num3;
					}
				}
			}
			if (num2 > 0f)
			{
				this.AverageWage = num / num2;
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00015F28 File Offset: 0x00014128
		private void CalculateAverageDistanceBetweenTowns()
		{
			this._averageDistanceBetweenClosestTwoTowns = new Dictionary<MobileParty.NavigationType, float>();
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			int num4 = 0;
			foreach (Town town in this.AllTowns)
			{
				float num5 = float.MaxValue;
				float num6 = float.MaxValue;
				float num7 = float.MaxValue;
				foreach (Town town2 in this.AllTowns)
				{
					if (town != town2)
					{
						float distance = Campaign.Current.Models.MapDistanceModel.GetDistance(town.Settlement, town2.Settlement, false, false, MobileParty.NavigationType.Default);
						if (distance < Campaign.MapDiagonal && distance < num6)
						{
							num6 = distance;
						}
						if (town.Settlement.HasPort && town2.Settlement.HasPort)
						{
							float distance2 = Campaign.Current.Models.MapDistanceModel.GetDistance(town.Settlement, town2.Settlement, true, true, MobileParty.NavigationType.Naval);
							if (distance2 < Campaign.MapDiagonal && distance2 < num7)
							{
								num7 = distance2;
							}
						}
						float num8 = Campaign.Current.Models.MapDistanceModel.GetDistance(town.Settlement, town2.Settlement, false, false, MobileParty.NavigationType.All);
						if (town.Settlement.HasPort)
						{
							float distance3 = Campaign.Current.Models.MapDistanceModel.GetDistance(town.Settlement, town2.Settlement, true, false, MobileParty.NavigationType.All);
							if (distance3 < Campaign.MapDiagonal && distance3 < num8)
							{
								num8 = distance3;
							}
						}
						if (town2.Settlement.HasPort)
						{
							float distance4 = Campaign.Current.Models.MapDistanceModel.GetDistance(town.Settlement, town2.Settlement, false, true, MobileParty.NavigationType.All);
							if (distance4 < Campaign.MapDiagonal && distance4 < num8)
							{
								num8 = distance4;
							}
						}
						if (town.Settlement.HasPort && town2.Settlement.HasPort)
						{
							float distance5 = Campaign.Current.Models.MapDistanceModel.GetDistance(town.Settlement, town2.Settlement, true, true, MobileParty.NavigationType.All);
							if (distance5 < Campaign.MapDiagonal && distance5 < num8)
							{
								num8 = distance5;
							}
						}
						if (num8 < num5)
						{
							num5 = num8;
						}
					}
				}
				if (num5 < Campaign.MapDiagonal)
				{
					num += num5;
				}
				if (num7 < Campaign.MapDiagonal)
				{
					num2 += num7;
				}
				if (num6 < Campaign.MapDiagonal)
				{
					num3 += num6;
				}
				num4++;
			}
			this._averageDistanceBetweenClosestTwoTowns.Add(MobileParty.NavigationType.Default, num3 / (float)num4);
			this._averageDistanceBetweenClosestTwoTowns.Add(MobileParty.NavigationType.Naval, num2 / (float)num4);
			this._averageDistanceBetweenClosestTwoTowns.Add(MobileParty.NavigationType.All, num / (float)num4);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0001621C File Offset: 0x0001441C
		public void InitializeGamePlayReferences()
		{
			base.CurrentGame.PlayerTroop = base.CurrentGame.ObjectManager.GetObject<CharacterObject>("main_hero");
			if (Hero.MainHero.Mother != null)
			{
				Hero.MainHero.Mother.SetHasMet();
			}
			if (Hero.MainHero.Father != null)
			{
				Hero.MainHero.Father.SetHasMet();
			}
			this.PlayerDefaultFaction = this.CampaignObjectManager.Find<Clan>("player_faction");
			GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, 1000, true);
			Hero.MainHero.ChangeState(Hero.CharacterStates.Active);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000162B4 File Offset: 0x000144B4
		private void InitializeScenes()
		{
			foreach (ModuleInfo moduleInfo in ModuleHelper.GetActiveModules())
			{
				string text = ModuleHelper.GetModuleFullPath(moduleInfo.Id) + "ModuleData/";
				string text2 = text + "sp_battle_scenes.xml";
				string text3 = text + "conversation_scenes.xml";
				string text4 = text + "meeting_scenes.xml";
				if (File.Exists(text2))
				{
					GameSceneDataManager.Instance.LoadSPBattleScenes(text2);
				}
				if (File.Exists(text3))
				{
					GameSceneDataManager.Instance.LoadConversationScenes(text3);
				}
				if (File.Exists(text4))
				{
					GameSceneDataManager.Instance.LoadMeetingScenes(text4);
				}
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00016370 File Offset: 0x00014570
		public void SetLoadingParameters(Campaign.GameLoadingType gameLoadingType)
		{
			Campaign.Current = this;
			this._gameLoadingType = gameLoadingType;
			if (gameLoadingType == Campaign.GameLoadingType.SavedCampaign)
			{
				Campaign.Current.GameStarted = true;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0001638E File Offset: 0x0001458E
		public void AddCampaignEventReceiver(CampaignEventReceiver receiver)
		{
			this.CampaignEventDispatcher.AddCampaignEventReceiver(receiver);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001639C File Offset: 0x0001459C
		protected override void OnInitialize()
		{
			this.CampaignEvents = new CampaignEvents();
			this.CustomPeriodicCampaignEvents = new List<MBCampaignEvent>();
			this.CampaignEventDispatcher = new CampaignEventDispatcher(new CampaignEventReceiver[] { this.CampaignEvents, this.IssueManager, this.QuestManager });
			this.SandBoxManager = Game.Current.AddGameHandler<SandBoxManager>();
			this.SaveHandler = new SaveHandler();
			this.VisualCreator = new VisualCreator();
			this.GameMenuManager = new GameMenuManager();
			this._towns = new MBList<Town>();
			this._castles = new MBList<Town>();
			this._villages = new MBList<Village>();
			this._hideouts = new MBList<Hideout>();
			if (this._gameLoadingType != Campaign.GameLoadingType.Editor)
			{
				this.CreateManagers();
			}
			CampaignGameStarter campaignGameStarter = new CampaignGameStarter(this.GameMenuManager, this.ConversationManager);
			this.SandBoxManager.Initialize(campaignGameStarter);
			base.GameManager.InitializeGameStarter(base.CurrentGame, campaignGameStarter);
			GameSceneDataManager.Initialize();
			if (this._gameLoadingType == Campaign.GameLoadingType.NewCampaign || this._gameLoadingType == Campaign.GameLoadingType.SavedCampaign)
			{
				this.InitializeScenes();
			}
			base.GameManager.OnGameStart(base.CurrentGame, campaignGameStarter);
			base.CurrentGame.SetBasicModels(campaignGameStarter.Models);
			this._gameModels = base.CurrentGame.AddGameModelsManager<GameModels>(campaignGameStarter.Models);
			CampaignTime.Initialize();
			base.CurrentGame.CreateGameManager();
			if (this._gameLoadingType == Campaign.GameLoadingType.SavedCampaign)
			{
				this.InitializeDefaultCampaignObjects();
			}
			else
			{
				this.MapTimeTracker = new MapTimeTracker(this.Models.CampaignTimeModel.CampaignStartTime);
			}
			base.GameManager.BeginGameStart(base.CurrentGame);
			if (this._gameLoadingType != Campaign.GameLoadingType.SavedCampaign)
			{
				this.OnNewCampaignStart();
			}
			this.CreateLists();
			this.InitializeBasicObjectXmls();
			if (this._gameLoadingType != Campaign.GameLoadingType.SavedCampaign)
			{
				base.GameManager.OnNewCampaignStart(base.CurrentGame, campaignGameStarter);
			}
			this.SandBoxManager.OnCampaignStart(campaignGameStarter, base.GameManager, this._gameLoadingType == Campaign.GameLoadingType.SavedCampaign);
			if (this._gameLoadingType == Campaign.GameLoadingType.NewCampaign || this._gameLoadingType == Campaign.GameLoadingType.SavedCampaign)
			{
				this.DetermineSavedStats(this._gameLoadingType);
			}
			if (this._gameLoadingType != Campaign.GameLoadingType.SavedCampaign)
			{
				this.AddCampaignBehaviorManager(new CampaignBehaviorManager(campaignGameStarter.CampaignBehaviors));
				base.GameManager.OnAfterCampaignStart(base.CurrentGame);
			}
			else
			{
				base.GameManager.OnGameLoaded(base.CurrentGame, campaignGameStarter);
				this._campaignBehaviorManager.InitializeCampaignBehaviors(campaignGameStarter.CampaignBehaviors);
				this._campaignBehaviorManager.LoadBehaviorData();
				this._campaignBehaviorManager.RegisterEvents();
			}
			foreach (INonReadyObjectHandler nonReadyObjectHandler in this.GetCampaignBehaviors<INonReadyObjectHandler>())
			{
				nonReadyObjectHandler.OnBeforeNonReadyObjectsDeleted();
			}
			if (this._gameLoadingType != Campaign.GameLoadingType.Tutorial)
			{
				campaignGameStarter.UnregisterNonReadyObjects();
			}
			if (this._gameLoadingType == Campaign.GameLoadingType.SavedCampaign)
			{
				this.InitializeCampaignObjectsOnAfterLoad();
			}
			else if (this._gameLoadingType == Campaign.GameLoadingType.NewCampaign || this._gameLoadingType == Campaign.GameLoadingType.Tutorial)
			{
				this.CampaignObjectManager.InitializeOnNewGame();
			}
			this.InitializeCachedLists();
			this.InitializeDefaultEquipments();
			this.NameGenerator.Initialize();
			base.CurrentGame.OnGameStart();
			base.GameManager.OnGameInitializationFinished(base.CurrentGame);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000166AC File Offset: 0x000148AC
		private void CalculateCachedStatsOnLoad()
		{
			ItemRoster.CalculateCachedStatsOnLoad();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000166B3 File Offset: 0x000148B3
		private void InitializeBasicObjectXmls()
		{
			base.ObjectManager.LoadXML("SPCultures", false);
			base.ObjectManager.LoadXML("Concepts", false);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000166D8 File Offset: 0x000148D8
		private void InitializeDefaultCampaignObjects()
		{
			base.CurrentGame.InitializeDefaultGameObjects();
			this.DefaultItems = new DefaultItems();
			base.CurrentGame.LoadBasicFiles();
			base.ObjectManager.LoadXML("Items", false);
			base.ObjectManager.LoadXML("EquipmentRosters", false);
			base.ObjectManager.LoadXML("partyTemplates", false);
			WeaponDescription @object = MBObjectManager.Instance.GetObject<WeaponDescription>("OneHandedBastardSwordAlternative");
			if (@object != null)
			{
				@object.IsHiddenFromUI = true;
			}
			WeaponDescription object2 = MBObjectManager.Instance.GetObject<WeaponDescription>("OneHandedBastardAxeAlternative");
			if (object2 != null)
			{
				object2.IsHiddenFromUI = true;
			}
			this.DefaultIssueEffects = new DefaultIssueEffects();
			this.DefaultTraits = new DefaultTraits();
			this.DefaultPolicies = new DefaultPolicies();
			this.DefaultPerks = new DefaultPerks();
			this.DefaultBuildingTypes = new DefaultBuildingTypes();
			this.DefaultVillageTypes = new DefaultVillageTypes();
			this.DefaultSiegeStrategies = new DefaultSiegeStrategies();
			this.DefaultSkillEffects = new DefaultSkillEffects();
			this.DefaultFeats = new DefaultCulturalFeats();
			this.DefaultFigureheads = new DefaultFigureheads();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000167DB File Offset: 0x000149DB
		private void InitializeManagers()
		{
			this.KingdomManager = new KingdomManager();
			this.CampaignInformationManager = new CampaignInformationManager();
			this.VisualTrackerManager = new VisualTrackerManager();
			this.TournamentManager = new TournamentManager();
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001680C File Offset: 0x00014A0C
		private void InitializeCampaignObjectsOnAfterLoad()
		{
			this.CampaignObjectManager.InitializeOnLoad();
			this.FactionManager.PreAfterLoad();
			List<PerkObject> list = this.AllPerks.Where<PerkObject>((PerkObject x) => !x.IsTrash).ToList<PerkObject>();
			this.AllPerks = new MBReadOnlyList<PerkObject>(list);
			this.LogEntryHistory.OnAfterLoad();
			foreach (Kingdom kingdom in this.Kingdoms)
			{
				foreach (Army army in kingdom.Armies)
				{
					army.OnAfterLoad();
				}
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000168F4 File Offset: 0x00014AF4
		private void OnNewCampaignStart()
		{
			Game.Current.PlayerTroop = null;
			this.MapStateData = new MapStateData();
			this.InitializeDefaultCampaignObjects();
			this.MainParty = MBObjectManager.Instance.CreateObject<MobileParty>("player_party");
			this.InitializeManagers();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0001692D File Offset: 0x00014B2D
		protected override void BeforeRegisterTypes(MBObjectManager objectManager)
		{
			objectManager.RegisterType<FeatObject>("feat", "Feats", 0U, true, false);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00016944 File Offset: 0x00014B44
		protected override void OnRegisterTypes(MBObjectManager objectManager)
		{
			objectManager.RegisterType<MobileParty>("MobileParty", "MobileParties", 14U, true, true);
			objectManager.RegisterType<CharacterObject>("NPCCharacter", "NPCCharacters", 16U, true, false);
			if (this.GameMode == CampaignGameMode.Tutorial)
			{
				objectManager.RegisterType<BasicCharacterObject>("NPCCharacter", "MPCharacters", 43U, true, false);
			}
			objectManager.RegisterType<CultureObject>("Culture", "SPCultures", 17U, true, false);
			objectManager.RegisterType<Clan>("Faction", "Factions", 18U, true, true);
			objectManager.RegisterType<PerkObject>("Perk", "Perks", 19U, true, false);
			objectManager.RegisterType<Kingdom>("Kingdom", "Kingdoms", 20U, true, true);
			objectManager.RegisterType<TraitObject>("Trait", "Traits", 21U, true, false);
			objectManager.RegisterType<VillageType>("VillageType", "VillageTypes", 22U, true, false);
			objectManager.RegisterType<BuildingType>("BuildingType", "BuildingTypes", 23U, true, false);
			objectManager.RegisterType<PartyTemplateObject>("PartyTemplate", "partyTemplates", 24U, true, false);
			objectManager.RegisterType<Settlement>("Settlement", "Settlements", 25U, true, false);
			objectManager.RegisterType<WorkshopType>("WorkshopType", "WorkshopTypes", 26U, true, false);
			objectManager.RegisterType<Village>("Village", "Components", 27U, true, false);
			objectManager.RegisterType<Hideout>("Hideout", "Components", 30U, true, false);
			objectManager.RegisterType<Town>("Town", "Components", 31U, true, false);
			objectManager.RegisterType<Hero>("Hero", "Heroes", 32U, true, true);
			objectManager.RegisterType<MenuContext>("MenuContext", "MenuContexts", 35U, true, false);
			objectManager.RegisterType<PolicyObject>("Policy", "Policies", 36U, true, false);
			objectManager.RegisterType<Concept>("Concept", "Concepts", 37U, true, false);
			objectManager.RegisterType<IssueEffect>("IssueEffect", "IssueEffects", 39U, true, false);
			objectManager.RegisterType<SiegeStrategy>("SiegeStrategy", "SiegeStrategies", 40U, true, false);
			objectManager.RegisterType<SkillEffect>("SkillEffect", "SkillEffects", 53U, true, false);
			objectManager.RegisterType<LocationComplexTemplate>("LocationComplexTemplate", "LocationComplexTemplates", 42U, true, false);
			objectManager.RegisterType<RetirementSettlementComponent>("RetirementSettlementComponent", "Components", 56U, true, false);
			objectManager.RegisterType<MissionShipObject>("MissionShip", "MissionShips", 57U, true, false);
			objectManager.RegisterType<ShipHull>("ShipHull", "ShipHulls", 58U, true, false);
			objectManager.RegisterType<ShipSlot>("ShipSlot", "ShipSlots", 59U, true, false);
			objectManager.RegisterType<ShipUpgradePiece>("ShipUpgradePiece", "ShipUpgradePieces", 60U, true, false);
			objectManager.RegisterType<Incident>("Incident", "Incidents", 62U, true, false);
			objectManager.RegisterType<Figurehead>("Figurehead", "Figureheads", 63U, true, false);
			objectManager.RegisterType<ShipPhysicsReference>("ShipPhysicsReference", "ShipPhysicsReferences", 64U, true, false);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00016BDA File Offset: 0x00014DDA
		private void CreateManagers()
		{
			this.EncyclopediaManager = new EncyclopediaManager();
			this.ConversationManager = new ConversationManager();
			this.NameGenerator = new NameGenerator();
			this.SkillLevelingManager = new DefaultSkillLevelingManager();
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00016C08 File Offset: 0x00014E08
		private void OnNewGameCreated(CampaignGameStarter gameStarter)
		{
			this.OnNewGameCreatedInternal();
			GameManagerBase gameManager = base.GameManager;
			if (gameManager != null)
			{
				gameManager.OnNewGameCreated(base.CurrentGame, gameStarter);
			}
			CampaignEventDispatcher.Instance.OnNewGameCreated(gameStarter);
			this.OnAfterNewGameCreatedInternal();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00016C3C File Offset: 0x00014E3C
		private void OnNewGameCreatedInternal()
		{
			this.UniqueGameId = MiscHelper.GenerateCampaignId(12);
			this._newGameVersion = MBSaveLoad.CurrentVersion.ToString();
			this.PlatformID = ApplicationPlatform.CurrentPlatform.ToString();
			this.PlayerTraitDeveloper = new PropertyOwner<PropertyObject>();
			TraitLevelingHelper.UpdateTraitXPAccordingToTraitLevels();
			this.TimeControlMode = CampaignTimeControlMode.Stop;
			this._campaignEntitySystem = new EntitySystem<CampaignEntityComponent>();
			this.SiegeEventManager = new SiegeEventManager();
			this.MapEventManager = new MapEventManager();
			this.MapMarkerManager = new MapMarkerManager();
			this.MinSettlementX = float.MaxValue;
			this.MinSettlementY = float.MaxValue;
			this.MaxSettlementX = float.MinValue;
			this.MaxSettlementY = float.MinValue;
			foreach (Settlement settlement in Settlement.All)
			{
				if (settlement.Position.X < this.MinSettlementX)
				{
					this.MinSettlementX = settlement.Position.X;
				}
				if (settlement.Position.Y < this.MinSettlementY)
				{
					this.MinSettlementY = settlement.Position.Y;
				}
				if (settlement.Position.X > this.MaxSettlementX)
				{
					this.MaxSettlementX = settlement.Position.X;
				}
				if (settlement.Position.Y > this.MaxSettlementY)
				{
					this.MaxSettlementY = settlement.Position.Y;
				}
			}
			this.CampaignBehaviorManager.RegisterEvents();
			this.CameraFollowParty = this.MainParty.Party;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00016E08 File Offset: 0x00015008
		private void OnAfterNewGameCreatedInternal()
		{
			Hero.MainHero.Gold = 1000;
			if (Clan.PlayerClan.Influence != 0f)
			{
				ChangeClanInfluenceAction.Apply(Clan.PlayerClan, -Clan.PlayerClan.Influence);
			}
			Hero.MainHero.ChangeState(Hero.CharacterStates.Active);
			this.GameInitTick();
			this._playerFormationPreferences = new Dictionary<CharacterObject, FormationClass>();
			this.PlayerFormationPreferences = this._playerFormationPreferences.GetReadOnlyDictionary<CharacterObject, FormationClass>();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00016E78 File Offset: 0x00015078
		protected override void DoLoadingForGameType(GameTypeLoadingStates gameTypeLoadingState, out GameTypeLoadingStates nextState)
		{
			nextState = GameTypeLoadingStates.None;
			switch (gameTypeLoadingState)
			{
			case GameTypeLoadingStates.InitializeFirstStep:
				base.CurrentGame.Initialize();
				nextState = GameTypeLoadingStates.WaitSecondStep;
				return;
			case GameTypeLoadingStates.WaitSecondStep:
				nextState = GameTypeLoadingStates.LoadVisualsThirdState;
				return;
			case GameTypeLoadingStates.LoadVisualsThirdState:
				if (this.GameMode == CampaignGameMode.Campaign)
				{
					this.LoadMapScene();
				}
				nextState = GameTypeLoadingStates.PostInitializeFourthState;
				return;
			case GameTypeLoadingStates.PostInitializeFourthState:
			{
				CampaignGameStarter gameStarter = this.SandBoxManager.GameStarter;
				if (this._gameLoadingType == Campaign.GameLoadingType.SavedCampaign)
				{
					this.CheckMapUpdate();
					this.OnDataLoadFinished(gameStarter);
					this.CalculateCachedValues();
					this.CalculateCachedStatsOnLoad();
					base.GameManager.OnAfterGameLoaded(base.CurrentGame);
					this.OnGameLoaded(gameStarter);
					this.OnSessionStart(gameStarter);
					foreach (Hero hero in Hero.AllAliveHeroes)
					{
						hero.CheckInvalidEquipmentsAndReplaceIfNeeded();
					}
					using (List<Hero>.Enumerator enumerator = Hero.DeadOrDisabledHeroes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Hero hero2 = enumerator.Current;
							hero2.CheckInvalidEquipmentsAndReplaceIfNeeded();
						}
						goto IL_019D;
					}
				}
				if (this._gameLoadingType == Campaign.GameLoadingType.NewCampaign)
				{
					this._campaignMapSceneXmlCrc = this.MapSceneWrapper.GetSceneXmlCrc();
					this._campaignMapSceneNavigationMeshCrc = this.MapSceneWrapper.GetSceneNavigationMeshCrc();
					this.OnDataLoadFinished(gameStarter);
					this.CalculateCachedValues();
					MBSaveLoad.OnNewGame();
					this.InitializeMainParty();
					foreach (Settlement settlement in Settlement.All)
					{
						settlement.OnGameCreated();
					}
					MBObjectManager.Instance.RemoveTemporaryTypes();
					this.OnNewGameCreated(gameStarter);
					this.OnSessionStart(gameStarter);
					Debug.Print("Finished starting a new game.", 0, Debug.DebugColor.White, 17592186044416UL);
				}
				IL_019D:
				base.GameManager.OnAfterGameInitializationFinished(base.CurrentGame, gameStarter);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0001705C File Offset: 0x0001525C
		private void DetermineSavedStats(Campaign.GameLoadingType gameLoadingType)
		{
			if (this._previouslyUsedModules == null)
			{
				this._previouslyUsedModules = new MBList<string>();
			}
			if (this._usedGameVersions == null)
			{
				this._usedGameVersions = new MBList<string>();
			}
			string text = MBSaveLoad.CurrentVersion.ToString();
			string text2 = string.Join(MBSaveLoad.ModuleCodeSeperator.ToString(), from x in ModuleHelper.GetActiveModules()
				select x.Id + MBSaveLoad.ModuleVersionSeperator.ToString() + x.Version);
			if (this._usedGameVersions.Count <= 0 || this._usedGameVersions.Last<string>() != text)
			{
				this._usedGameVersions.Add(text);
			}
			if (this._previouslyUsedModules.LastOrDefault<string>() != text2)
			{
				this._previouslyUsedModules.Add(text2);
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0001712B File Offset: 0x0001532B
		public override void OnMissionIsStarting(string missionName, MissionInitializerRecord rec)
		{
			if (rec.PlayingInCampaignMode)
			{
				CampaignEventDispatcher.Instance.BeforeMissionOpened();
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001713F File Offset: 0x0001533F
		public override void InitializeParameters()
		{
			ManagedParameters.Instance.Initialize(ModuleHelper.GetXmlPath("Native", "managed_campaign_parameters"));
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0001715A File Offset: 0x0001535A
		public void SetTimeControlModeLock(bool isLocked)
		{
			this.TimeControlModeLock = isLocked;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00017163 File Offset: 0x00015363
		public override bool IsPartyWindowAccessibleAtMission
		{
			get
			{
				return this.GameMode == CampaignGameMode.Tutorial;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0001716E File Offset: 0x0001536E
		internal MBReadOnlyList<Town> AllTowns
		{
			get
			{
				return this._towns;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00017176 File Offset: 0x00015376
		internal MBReadOnlyList<Town> AllCastles
		{
			get
			{
				return this._castles;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0001717E File Offset: 0x0001537E
		internal MBReadOnlyList<Village> AllVillages
		{
			get
			{
				return this._villages;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00017186 File Offset: 0x00015386
		internal MBReadOnlyList<Hideout> AllHideouts
		{
			get
			{
				return this._hideouts;
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00017190 File Offset: 0x00015390
		public void OnPlayerCharacterChanged(out bool isMainPartyChanged)
		{
			isMainPartyChanged = false;
			if (MobileParty.MainParty != Hero.MainHero.PartyBelongedTo)
			{
				isMainPartyChanged = true;
			}
			this.MainParty = Hero.MainHero.PartyBelongedTo;
			if (Hero.MainHero.CurrentSettlement != null && !Hero.MainHero.IsPrisoner)
			{
				if (this.MainParty == null)
				{
					LeaveSettlementAction.ApplyForCharacterOnly(Hero.MainHero);
				}
				else
				{
					LeaveSettlementAction.ApplyForParty(this.MainParty);
				}
			}
			if (Hero.MainHero.IsFugitive)
			{
				Hero.MainHero.ChangeState(Hero.CharacterStates.Active);
			}
			this.PlayerTraitDeveloper = new PropertyOwner<PropertyObject>();
			TraitLevelingHelper.UpdateTraitXPAccordingToTraitLevels();
			if (this.MainParty == null)
			{
				this.MainParty = MobileParty.CreateParty("player_party_" + Hero.MainHero.StringId, null);
				LordPartyComponent.ConvertPartyToLordParty(this.MainParty, Hero.MainHero, Hero.MainHero);
				isMainPartyChanged = true;
				CampaignVec2 campaignVec;
				if (Hero.MainHero.IsPrisoner)
				{
					this.MainParty.RemovePartyLeader();
					PartyBase partyBelongedToAsPrisoner = Hero.MainHero.PartyBelongedToAsPrisoner;
					if (partyBelongedToAsPrisoner.IsMobile)
					{
						campaignVec = partyBelongedToAsPrisoner.MobileParty.Position;
					}
					else
					{
						campaignVec = partyBelongedToAsPrisoner.Settlement.GatePosition;
					}
					this.MainParty.IsActive = false;
				}
				else
				{
					CampaignVec2 campaignPosition = Hero.MainHero.GetCampaignPosition();
					campaignVec = ((campaignPosition.IsValid() && campaignPosition != CampaignVec2.Zero) ? campaignPosition : SettlementHelper.GetBestSettlementToSpawnAround(Hero.MainHero).GatePosition);
					this.MainParty.IsActive = true;
					this.MainParty.MemberRoster.AddToCounts(Hero.MainHero.CharacterObject, 1, true, 0, 0, true, -1);
				}
				this.MainParty.InitializeMobilePartyAtPosition(campaignVec);
			}
			PartyBase.MainParty.ItemRoster.UpdateVersion();
			PartyBase.MainParty.MemberRoster.UpdateVersion();
			PartyBase.MainParty.PrisonRoster.UpdateVersion();
			if (MobileParty.MainParty.IsActive)
			{
				PartyBase.MainParty.SetAsCameraFollowParty();
			}
			PartyBase.MainParty.UpdateVisibilityAndInspected(MobileParty.MainParty.Position, 0f);
			if (Hero.MainHero.Mother != null)
			{
				Hero.MainHero.Mother.SetHasMet();
			}
			if (Hero.MainHero.Father != null)
			{
				Hero.MainHero.Father.SetHasMet();
			}
			this.MainParty.SetWagePaymentLimit(Campaign.Current.Models.PartyWageModel.MaxWagePaymentLimit);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000173D5 File Offset: 0x000155D5
		public void SetPlayerFormationPreference(CharacterObject character, FormationClass formation)
		{
			if (!this._playerFormationPreferences.ContainsKey(character))
			{
				this._playerFormationPreferences.Add(character, formation);
				return;
			}
			this._playerFormationPreferences[character] = formation;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00017400 File Offset: 0x00015600
		public override void OnStateChanged(GameState oldState)
		{
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00017402 File Offset: 0x00015602
		public void UnlockFigurehead(Figurehead figurehead)
		{
			this.UnlockedFigureheadsByMainHero.Add(figurehead);
			CampaignEventDispatcher.Instance.OnFigureheadUnlocked(figurehead);
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0001741B File Offset: 0x0001561B
		// (set) Token: 0x06000329 RID: 809 RVA: 0x00017423 File Offset: 0x00015623
		[SaveableProperty(68)]
		public PropertyOwner<PropertyObject> PlayerTraitDeveloper { get; private set; }

		// Token: 0x0600032A RID: 810 RVA: 0x0001742C File Offset: 0x0001562C
		internal static void AutoGeneratedStaticCollectObjectsCampaign(object o, List<object> collectedObjects)
		{
			((Campaign)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0001743C File Offset: 0x0001563C
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			collectedObjects.Add(this.Options);
			collectedObjects.Add(this.TournamentManager);
			collectedObjects.Add(this.UnlockedFigureheadsByMainHero);
			collectedObjects.Add(this.KingdomManager);
			collectedObjects.Add(this._campaignPeriodicEventManager);
			collectedObjects.Add(this._previouslyUsedModules);
			collectedObjects.Add(this._usedGameVersions);
			collectedObjects.Add(this._campaignBehaviorManager);
			collectedObjects.Add(this._customManagers);
			collectedObjects.Add(this._cameraFollowParty);
			collectedObjects.Add(this._logEntryHistory);
			collectedObjects.Add(this._playerFormationPreferences);
			collectedObjects.Add(this.CampaignObjectManager);
			collectedObjects.Add(this.QuestManager);
			collectedObjects.Add(this.IssueManager);
			collectedObjects.Add(this.FactionManager);
			collectedObjects.Add(this.CharacterRelationManager);
			collectedObjects.Add(this.Romance);
			collectedObjects.Add(this.PlayerCaptivity);
			collectedObjects.Add(this.PlayerDefaultFaction);
			collectedObjects.Add(this.MapStateData);
			collectedObjects.Add(this.MapTimeTracker);
			collectedObjects.Add(this.SiegeEventManager);
			collectedObjects.Add(this.MapEventManager);
			collectedObjects.Add(this.MapMarkerManager);
			collectedObjects.Add(this.PlayerEncounter);
			collectedObjects.Add(this.BarterManager);
			collectedObjects.Add(this.MainParty);
			collectedObjects.Add(this.CampaignInformationManager);
			collectedObjects.Add(this.VisualTrackerManager);
			collectedObjects.Add(this.PlayerTraitDeveloper);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000175C4 File Offset: 0x000157C4
		internal static object AutoGeneratedGetMemberValueEnabledCheatsBefore(object o)
		{
			return ((Campaign)o).EnabledCheatsBefore;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000175D6 File Offset: 0x000157D6
		internal static object AutoGeneratedGetMemberValuePlatformID(object o)
		{
			return ((Campaign)o).PlatformID;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000175E3 File Offset: 0x000157E3
		internal static object AutoGeneratedGetMemberValueUniqueGameId(object o)
		{
			return ((Campaign)o).UniqueGameId;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000175F0 File Offset: 0x000157F0
		internal static object AutoGeneratedGetMemberValueCampaignObjectManager(object o)
		{
			return ((Campaign)o).CampaignObjectManager;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000175FD File Offset: 0x000157FD
		internal static object AutoGeneratedGetMemberValueIsCraftingEnabled(object o)
		{
			return ((Campaign)o).IsCraftingEnabled;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001760F File Offset: 0x0001580F
		internal static object AutoGeneratedGetMemberValueIsBannerEditorEnabled(object o)
		{
			return ((Campaign)o).IsBannerEditorEnabled;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00017621 File Offset: 0x00015821
		internal static object AutoGeneratedGetMemberValueIsFaceGenEnabled(object o)
		{
			return ((Campaign)o).IsFaceGenEnabled;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00017633 File Offset: 0x00015833
		internal static object AutoGeneratedGetMemberValueQuestManager(object o)
		{
			return ((Campaign)o).QuestManager;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00017640 File Offset: 0x00015840
		internal static object AutoGeneratedGetMemberValueIssueManager(object o)
		{
			return ((Campaign)o).IssueManager;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0001764D File Offset: 0x0001584D
		internal static object AutoGeneratedGetMemberValueFactionManager(object o)
		{
			return ((Campaign)o).FactionManager;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0001765A File Offset: 0x0001585A
		internal static object AutoGeneratedGetMemberValueCharacterRelationManager(object o)
		{
			return ((Campaign)o).CharacterRelationManager;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00017667 File Offset: 0x00015867
		internal static object AutoGeneratedGetMemberValueRomance(object o)
		{
			return ((Campaign)o).Romance;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00017674 File Offset: 0x00015874
		internal static object AutoGeneratedGetMemberValuePlayerCaptivity(object o)
		{
			return ((Campaign)o).PlayerCaptivity;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00017681 File Offset: 0x00015881
		internal static object AutoGeneratedGetMemberValuePlayerDefaultFaction(object o)
		{
			return ((Campaign)o).PlayerDefaultFaction;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001768E File Offset: 0x0001588E
		internal static object AutoGeneratedGetMemberValueMapStateData(object o)
		{
			return ((Campaign)o).MapStateData;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0001769B File Offset: 0x0001589B
		internal static object AutoGeneratedGetMemberValueMapTimeTracker(object o)
		{
			return ((Campaign)o).MapTimeTracker;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000176A8 File Offset: 0x000158A8
		internal static object AutoGeneratedGetMemberValueGameMode(object o)
		{
			return ((Campaign)o).GameMode;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000176BA File Offset: 0x000158BA
		internal static object AutoGeneratedGetMemberValuePlayerProgress(object o)
		{
			return ((Campaign)o).PlayerProgress;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000176CC File Offset: 0x000158CC
		internal static object AutoGeneratedGetMemberValueSiegeEventManager(object o)
		{
			return ((Campaign)o).SiegeEventManager;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000176D9 File Offset: 0x000158D9
		internal static object AutoGeneratedGetMemberValueMapEventManager(object o)
		{
			return ((Campaign)o).MapEventManager;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000176E6 File Offset: 0x000158E6
		internal static object AutoGeneratedGetMemberValueMapMarkerManager(object o)
		{
			return ((Campaign)o).MapMarkerManager;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000176F3 File Offset: 0x000158F3
		internal static object AutoGeneratedGetMemberValue_curMapFrame(object o)
		{
			return ((Campaign)o)._curMapFrame;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00017705 File Offset: 0x00015905
		internal static object AutoGeneratedGetMemberValuePlayerEncounter(object o)
		{
			return ((Campaign)o).PlayerEncounter;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00017712 File Offset: 0x00015912
		internal static object AutoGeneratedGetMemberValueBarterManager(object o)
		{
			return ((Campaign)o).BarterManager;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0001771F File Offset: 0x0001591F
		internal static object AutoGeneratedGetMemberValueIsMainHeroDisguised(object o)
		{
			return ((Campaign)o).IsMainHeroDisguised;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00017731 File Offset: 0x00015931
		internal static object AutoGeneratedGetMemberValueMainParty(object o)
		{
			return ((Campaign)o).MainParty;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001773E File Offset: 0x0001593E
		internal static object AutoGeneratedGetMemberValueCampaignInformationManager(object o)
		{
			return ((Campaign)o).CampaignInformationManager;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0001774B File Offset: 0x0001594B
		internal static object AutoGeneratedGetMemberValueVisualTrackerManager(object o)
		{
			return ((Campaign)o).VisualTrackerManager;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00017758 File Offset: 0x00015958
		internal static object AutoGeneratedGetMemberValuePlayerTraitDeveloper(object o)
		{
			return ((Campaign)o).PlayerTraitDeveloper;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00017765 File Offset: 0x00015965
		internal static object AutoGeneratedGetMemberValueOptions(object o)
		{
			return ((Campaign)o).Options;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00017772 File Offset: 0x00015972
		internal static object AutoGeneratedGetMemberValueTournamentManager(object o)
		{
			return ((Campaign)o).TournamentManager;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001777F File Offset: 0x0001597F
		internal static object AutoGeneratedGetMemberValueIsSinglePlayerReferencesInitialized(object o)
		{
			return ((Campaign)o).IsSinglePlayerReferencesInitialized;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00017791 File Offset: 0x00015991
		internal static object AutoGeneratedGetMemberValueLastTimeControlMode(object o)
		{
			return ((Campaign)o).LastTimeControlMode;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000177A3 File Offset: 0x000159A3
		internal static object AutoGeneratedGetMemberValueMainHeroIllDays(object o)
		{
			return ((Campaign)o).MainHeroIllDays;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000177B5 File Offset: 0x000159B5
		internal static object AutoGeneratedGetMemberValueUnlockedFigureheadsByMainHero(object o)
		{
			return ((Campaign)o).UnlockedFigureheadsByMainHero;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000177C2 File Offset: 0x000159C2
		internal static object AutoGeneratedGetMemberValueKingdomManager(object o)
		{
			return ((Campaign)o).KingdomManager;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000177CF File Offset: 0x000159CF
		internal static object AutoGeneratedGetMemberValue_campaignPeriodicEventManager(object o)
		{
			return ((Campaign)o)._campaignPeriodicEventManager;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000177DC File Offset: 0x000159DC
		internal static object AutoGeneratedGetMemberValue_isMainPartyWaiting(object o)
		{
			return ((Campaign)o)._isMainPartyWaiting;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000177EE File Offset: 0x000159EE
		internal static object AutoGeneratedGetMemberValue_newGameVersion(object o)
		{
			return ((Campaign)o)._newGameVersion;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000177FB File Offset: 0x000159FB
		internal static object AutoGeneratedGetMemberValue_previouslyUsedModules(object o)
		{
			return ((Campaign)o)._previouslyUsedModules;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00017808 File Offset: 0x00015A08
		internal static object AutoGeneratedGetMemberValue_campaignMapSceneXmlCrc(object o)
		{
			return ((Campaign)o)._campaignMapSceneXmlCrc;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001781A File Offset: 0x00015A1A
		internal static object AutoGeneratedGetMemberValue_campaignMapSceneNavigationMeshCrc(object o)
		{
			return ((Campaign)o)._campaignMapSceneNavigationMeshCrc;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001782C File Offset: 0x00015A2C
		internal static object AutoGeneratedGetMemberValue_usedGameVersions(object o)
		{
			return ((Campaign)o)._usedGameVersions;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00017839 File Offset: 0x00015A39
		internal static object AutoGeneratedGetMemberValue_campaignBehaviorManager(object o)
		{
			return ((Campaign)o)._campaignBehaviorManager;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00017846 File Offset: 0x00015A46
		internal static object AutoGeneratedGetMemberValue_customManagers(object o)
		{
			return ((Campaign)o)._customManagers;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00017853 File Offset: 0x00015A53
		internal static object AutoGeneratedGetMemberValue_lastPartyIndex(object o)
		{
			return ((Campaign)o)._lastPartyIndex;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00017865 File Offset: 0x00015A65
		internal static object AutoGeneratedGetMemberValue_cameraFollowParty(object o)
		{
			return ((Campaign)o)._cameraFollowParty;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00017872 File Offset: 0x00015A72
		internal static object AutoGeneratedGetMemberValue_logEntryHistory(object o)
		{
			return ((Campaign)o)._logEntryHistory;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001787F File Offset: 0x00015A7F
		internal static object AutoGeneratedGetMemberValue_playerFormationPreferences(object o)
		{
			return ((Campaign)o)._playerFormationPreferences;
		}

		// Token: 0x0400003A RID: 58
		public const float ConfigTimeMultiplier = 0.25f;

		// Token: 0x0400003B RID: 59
		private EntitySystem<CampaignEntityComponent> _campaignEntitySystem;

		// Token: 0x04000041 RID: 65
		public static int PlayerRegionSwitchCostFromLandToSea;

		// Token: 0x04000042 RID: 66
		public static int PathFindingMaxCostLimit;

		// Token: 0x04000043 RID: 67
		public ITask CampaignLateAITickTask;

		// Token: 0x04000044 RID: 68
		[SaveableField(210)]
		private CampaignPeriodicEventManager _campaignPeriodicEventManager;

		// Token: 0x04000045 RID: 69
		private Dictionary<MobileParty.NavigationType, float> _averageDistanceBetweenClosestTwoTowns;

		// Token: 0x04000047 RID: 71
		[SaveableField(53)]
		private bool _isMainPartyWaiting;

		// Token: 0x04000048 RID: 72
		[SaveableField(344)]
		private string _newGameVersion;

		// Token: 0x04000049 RID: 73
		[SaveableField(78)]
		private MBList<string> _previouslyUsedModules;

		// Token: 0x0400004A RID: 74
		[SaveableField(85)]
		private uint _campaignMapSceneXmlCrc;

		// Token: 0x0400004B RID: 75
		[SaveableField(86)]
		private uint _campaignMapSceneNavigationMeshCrc;

		// Token: 0x0400004C RID: 76
		[SaveableField(81)]
		private MBList<string> _usedGameVersions;

		// Token: 0x04000052 RID: 82
		[SaveableField(7)]
		private ICampaignBehaviorManager _campaignBehaviorManager;

		// Token: 0x04000054 RID: 84
		private CampaignTickCacheDataStore _tickData;

		// Token: 0x04000055 RID: 85
		[SaveableField(2)]
		public readonly CampaignOptions Options;

		// Token: 0x04000056 RID: 86
		public MBReadOnlyDictionary<CharacterObject, FormationClass> PlayerFormationPreferences;

		// Token: 0x04000057 RID: 87
		[SaveableField(13)]
		public ITournamentManager TournamentManager;

		// Token: 0x04000058 RID: 88
		public float MinSettlementX;

		// Token: 0x04000059 RID: 89
		public float MaxSettlementX;

		// Token: 0x0400005A RID: 90
		public float MinSettlementY;

		// Token: 0x0400005B RID: 91
		public float MaxSettlementY;

		// Token: 0x0400005C RID: 92
		[SaveableField(27)]
		public bool IsSinglePlayerReferencesInitialized;

		// Token: 0x0400005D RID: 93
		private LocatorGrid<MobileParty> _mobilePartyLocator;

		// Token: 0x0400005E RID: 94
		private LocatorGrid<Settlement> _settlementLocator;

		// Token: 0x0400005F RID: 95
		private GameModels _gameModels;

		// Token: 0x04000062 RID: 98
		[SaveableField(31)]
		public CampaignTimeControlMode LastTimeControlMode = CampaignTimeControlMode.UnstoppablePlay;

		// Token: 0x04000063 RID: 99
		private IMapScene _mapSceneWrapper;

		// Token: 0x04000064 RID: 100
		public bool GameStarted;

		// Token: 0x04000066 RID: 102
		private Campaign.GameLoadingType _gameLoadingType;

		// Token: 0x04000067 RID: 103
		public ConversationContext CurrentConversationContext;

		// Token: 0x04000068 RID: 104
		[CachedData]
		private float _dt;

		// Token: 0x0400006B RID: 107
		private CampaignTimeControlMode _timeControlMode;

		// Token: 0x0400006C RID: 108
		public int CurrentTickCount;

		// Token: 0x0400009B RID: 155
		[SaveableField(30)]
		public int MainHeroIllDays = -1;

		// Token: 0x040000A8 RID: 168
		[SaveableField(42)]
		private List<ICustomSystemManager> _customManagers = new List<ICustomSystemManager>();

		// Token: 0x040000AC RID: 172
		private MBCampaignEvent _dailyTickEvent;

		// Token: 0x040000AD RID: 173
		private MBCampaignEvent _hourlyTickEvent;

		// Token: 0x040000AE RID: 174
		private MBCampaignEvent _QuarterHourlyTickEvent;

		// Token: 0x040000B0 RID: 176
		[CachedData]
		private int _lastNonZeroDtFrame;

		// Token: 0x040000B1 RID: 177
		public int DefaultWeatherNodeDimension;

		// Token: 0x040000BA RID: 186
		[SaveableField(333)]
		public List<Figurehead> UnlockedFigureheadsByMainHero = new List<Figurehead>();

		// Token: 0x040000BB RID: 187
		private MBList<Town> _towns;

		// Token: 0x040000BC RID: 188
		private MBList<Town> _castles;

		// Token: 0x040000BD RID: 189
		private MBList<Village> _villages;

		// Token: 0x040000BE RID: 190
		private MBList<Hideout> _hideouts;

		// Token: 0x040000BF RID: 191
		private MBReadOnlyList<CharacterObject> _characters;

		// Token: 0x040000C0 RID: 192
		private MBReadOnlyList<WorkshopType> _workshops;

		// Token: 0x040000C1 RID: 193
		private MBReadOnlyList<ItemModifier> _itemModifiers;

		// Token: 0x040000C2 RID: 194
		private MBReadOnlyList<Concept> _concepts;

		// Token: 0x040000C3 RID: 195
		private MBReadOnlyList<ItemModifierGroup> _itemModifierGroups;

		// Token: 0x040000C4 RID: 196
		[SaveableField(79)]
		private int _lastPartyIndex;

		// Token: 0x040000C6 RID: 198
		[SaveableField(61)]
		private PartyBase _cameraFollowParty;

		// Token: 0x040000C9 RID: 201
		[SaveableField(64)]
		private readonly LogEntryHistory _logEntryHistory = new LogEntryHistory();

		// Token: 0x040000CC RID: 204
		[SaveableField(65)]
		public KingdomManager KingdomManager;

		// Token: 0x040000CE RID: 206
		[SaveableField(77)]
		private Dictionary<CharacterObject, FormationClass> _playerFormationPreferences;

		// Token: 0x020004F9 RID: 1273
		[Flags]
		public enum PartyRestFlags : uint
		{
			// Token: 0x04001570 RID: 5488
			None = 0U,
			// Token: 0x04001571 RID: 5489
			SafeMode = 1U
		}

		// Token: 0x020004FA RID: 1274
		public enum GameLoadingType
		{
			// Token: 0x04001573 RID: 5491
			Tutorial,
			// Token: 0x04001574 RID: 5492
			NewCampaign,
			// Token: 0x04001575 RID: 5493
			SavedCampaign,
			// Token: 0x04001576 RID: 5494
			Editor
		}
	}
}
