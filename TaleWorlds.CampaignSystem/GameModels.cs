using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000088 RID: 136
	public sealed class GameModels : GameModelsManager
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x000526BA File Offset: 0x000508BA
		// (set) Token: 0x06001128 RID: 4392 RVA: 0x000526C2 File Offset: 0x000508C2
		public MapVisibilityModel MapVisibilityModel { get; private set; }

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x000526CB File Offset: 0x000508CB
		// (set) Token: 0x0600112A RID: 4394 RVA: 0x000526D3 File Offset: 0x000508D3
		public InformationRestrictionModel InformationRestrictionModel { get; private set; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x000526DC File Offset: 0x000508DC
		// (set) Token: 0x0600112C RID: 4396 RVA: 0x000526E4 File Offset: 0x000508E4
		public PartySpeedModel PartySpeedCalculatingModel { get; private set; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x000526ED File Offset: 0x000508ED
		// (set) Token: 0x0600112E RID: 4398 RVA: 0x000526F5 File Offset: 0x000508F5
		public PartyHealingModel PartyHealingModel { get; private set; }

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x000526FE File Offset: 0x000508FE
		// (set) Token: 0x06001130 RID: 4400 RVA: 0x00052706 File Offset: 0x00050906
		public CaravanModel CaravanModel { get; private set; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x0005270F File Offset: 0x0005090F
		// (set) Token: 0x06001132 RID: 4402 RVA: 0x00052717 File Offset: 0x00050917
		public PartyTrainingModel PartyTrainingModel { get; private set; }

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x00052720 File Offset: 0x00050920
		// (set) Token: 0x06001134 RID: 4404 RVA: 0x00052728 File Offset: 0x00050928
		public BarterModel BarterModel { get; private set; }

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x00052731 File Offset: 0x00050931
		// (set) Token: 0x06001136 RID: 4406 RVA: 0x00052739 File Offset: 0x00050939
		public PersuasionModel PersuasionModel { get; private set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x00052742 File Offset: 0x00050942
		// (set) Token: 0x06001138 RID: 4408 RVA: 0x0005274A File Offset: 0x0005094A
		public DefectionModel DefectionModel { get; private set; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x00052753 File Offset: 0x00050953
		// (set) Token: 0x0600113A RID: 4410 RVA: 0x0005275B File Offset: 0x0005095B
		public CombatSimulationModel CombatSimulationModel { get; private set; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x00052764 File Offset: 0x00050964
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x0005276C File Offset: 0x0005096C
		public CombatXpModel CombatXpModel { get; private set; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x00052775 File Offset: 0x00050975
		// (set) Token: 0x0600113E RID: 4414 RVA: 0x0005277D File Offset: 0x0005097D
		public GenericXpModel GenericXpModel { get; private set; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x00052786 File Offset: 0x00050986
		// (set) Token: 0x06001140 RID: 4416 RVA: 0x0005278E File Offset: 0x0005098E
		public TradeAgreementModel TradeAgreementModel { get; private set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x00052797 File Offset: 0x00050997
		// (set) Token: 0x06001142 RID: 4418 RVA: 0x0005279F File Offset: 0x0005099F
		public SmithingModel SmithingModel { get; private set; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x000527A8 File Offset: 0x000509A8
		// (set) Token: 0x06001144 RID: 4420 RVA: 0x000527B0 File Offset: 0x000509B0
		public PartyTradeModel PartyTradeModel { get; private set; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x000527B9 File Offset: 0x000509B9
		// (set) Token: 0x06001146 RID: 4422 RVA: 0x000527C1 File Offset: 0x000509C1
		public RansomValueCalculationModel RansomValueCalculationModel { get; private set; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x000527CA File Offset: 0x000509CA
		// (set) Token: 0x06001148 RID: 4424 RVA: 0x000527D2 File Offset: 0x000509D2
		public RaidModel RaidModel { get; private set; }

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x000527DB File Offset: 0x000509DB
		// (set) Token: 0x0600114A RID: 4426 RVA: 0x000527E3 File Offset: 0x000509E3
		public MobilePartyFoodConsumptionModel MobilePartyFoodConsumptionModel { get; private set; }

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x000527EC File Offset: 0x000509EC
		// (set) Token: 0x0600114C RID: 4428 RVA: 0x000527F4 File Offset: 0x000509F4
		public PartyFoodBuyingModel PartyFoodBuyingModel { get; private set; }

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x000527FD File Offset: 0x000509FD
		// (set) Token: 0x0600114E RID: 4430 RVA: 0x00052805 File Offset: 0x00050A05
		public PartyImpairmentModel PartyImpairmentModel { get; private set; }

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x0005280E File Offset: 0x00050A0E
		// (set) Token: 0x06001150 RID: 4432 RVA: 0x00052816 File Offset: 0x00050A16
		public PartyMoraleModel PartyMoraleModel { get; private set; }

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x0005281F File Offset: 0x00050A1F
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x00052827 File Offset: 0x00050A27
		public PartyDesertionModel PartyDesertionModel { get; private set; }

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x00052830 File Offset: 0x00050A30
		// (set) Token: 0x06001154 RID: 4436 RVA: 0x00052838 File Offset: 0x00050A38
		public PartyTransitionModel PartyTransitionModel { get; private set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x00052841 File Offset: 0x00050A41
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x00052849 File Offset: 0x00050A49
		public DiplomacyModel DiplomacyModel { get; private set; }

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x00052852 File Offset: 0x00050A52
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x0005285A File Offset: 0x00050A5A
		public AllianceModel AllianceModel { get; private set; }

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x00052863 File Offset: 0x00050A63
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x0005286B File Offset: 0x00050A6B
		public MinorFactionsModel MinorFactionsModel { get; private set; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x00052874 File Offset: 0x00050A74
		// (set) Token: 0x0600115C RID: 4444 RVA: 0x0005287C File Offset: 0x00050A7C
		public HideoutModel HideoutModel { get; private set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x00052885 File Offset: 0x00050A85
		// (set) Token: 0x0600115E RID: 4446 RVA: 0x0005288D File Offset: 0x00050A8D
		public KingdomCreationModel KingdomCreationModel { get; private set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x00052896 File Offset: 0x00050A96
		// (set) Token: 0x06001160 RID: 4448 RVA: 0x0005289E File Offset: 0x00050A9E
		public KingdomDecisionPermissionModel KingdomDecisionPermissionModel { get; private set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x000528A7 File Offset: 0x00050AA7
		// (set) Token: 0x06001162 RID: 4450 RVA: 0x000528AF File Offset: 0x00050AAF
		public EmissaryModel EmissaryModel { get; private set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x000528B8 File Offset: 0x00050AB8
		// (set) Token: 0x06001164 RID: 4452 RVA: 0x000528C0 File Offset: 0x00050AC0
		public CharacterDevelopmentModel CharacterDevelopmentModel { get; private set; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x000528C9 File Offset: 0x00050AC9
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x000528D1 File Offset: 0x00050AD1
		public CharacterStatsModel CharacterStatsModel { get; private set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x000528DA File Offset: 0x00050ADA
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x000528E2 File Offset: 0x00050AE2
		public EncounterModel EncounterModel { get; private set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x000528EB File Offset: 0x00050AEB
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x000528F3 File Offset: 0x00050AF3
		public SettlementPatrolModel SettlementPatrolModel { get; private set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x000528FC File Offset: 0x00050AFC
		// (set) Token: 0x0600116C RID: 4460 RVA: 0x00052904 File Offset: 0x00050B04
		public ItemDiscardModel ItemDiscardModel { get; private set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0005290D File Offset: 0x00050B0D
		// (set) Token: 0x0600116E RID: 4462 RVA: 0x00052915 File Offset: 0x00050B15
		public ValuationModel ValuationModel { get; private set; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x0005291E File Offset: 0x00050B1E
		// (set) Token: 0x06001170 RID: 4464 RVA: 0x00052926 File Offset: 0x00050B26
		public PartySizeLimitModel PartySizeLimitModel { get; private set; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x0005292F File Offset: 0x00050B2F
		// (set) Token: 0x06001172 RID: 4466 RVA: 0x00052937 File Offset: 0x00050B37
		public PartyShipLimitModel PartyShipLimitModel { get; private set; }

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x00052940 File Offset: 0x00050B40
		// (set) Token: 0x06001174 RID: 4468 RVA: 0x00052948 File Offset: 0x00050B48
		public InventoryCapacityModel InventoryCapacityModel { get; private set; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x00052951 File Offset: 0x00050B51
		// (set) Token: 0x06001176 RID: 4470 RVA: 0x00052959 File Offset: 0x00050B59
		public PartyWageModel PartyWageModel { get; private set; }

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x00052962 File Offset: 0x00050B62
		// (set) Token: 0x06001178 RID: 4472 RVA: 0x0005296A File Offset: 0x00050B6A
		public VillageProductionCalculatorModel VillageProductionCalculatorModel { get; private set; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x00052973 File Offset: 0x00050B73
		// (set) Token: 0x0600117A RID: 4474 RVA: 0x0005297B File Offset: 0x00050B7B
		public VolunteerModel VolunteerModel { get; private set; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x00052984 File Offset: 0x00050B84
		// (set) Token: 0x0600117C RID: 4476 RVA: 0x0005298C File Offset: 0x00050B8C
		public RomanceModel RomanceModel { get; private set; }

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00052995 File Offset: 0x00050B95
		// (set) Token: 0x0600117E RID: 4478 RVA: 0x0005299D File Offset: 0x00050B9D
		public MobilePartyAIModel MobilePartyAIModel { get; private set; }

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x000529A6 File Offset: 0x00050BA6
		// (set) Token: 0x06001180 RID: 4480 RVA: 0x000529AE File Offset: 0x00050BAE
		public ArmyManagementCalculationModel ArmyManagementCalculationModel { get; private set; }

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x000529B7 File Offset: 0x00050BB7
		// (set) Token: 0x06001182 RID: 4482 RVA: 0x000529BF File Offset: 0x00050BBF
		public BanditDensityModel BanditDensityModel { get; private set; }

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x000529C8 File Offset: 0x00050BC8
		// (set) Token: 0x06001184 RID: 4484 RVA: 0x000529D0 File Offset: 0x00050BD0
		public EncounterGameMenuModel EncounterGameMenuModel { get; private set; }

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x000529D9 File Offset: 0x00050BD9
		// (set) Token: 0x06001186 RID: 4486 RVA: 0x000529E1 File Offset: 0x00050BE1
		public BattleRewardModel BattleRewardModel { get; private set; }

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x000529EA File Offset: 0x00050BEA
		// (set) Token: 0x06001188 RID: 4488 RVA: 0x000529F2 File Offset: 0x00050BF2
		public MapTrackModel MapTrackModel { get; private set; }

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x000529FB File Offset: 0x00050BFB
		// (set) Token: 0x0600118A RID: 4490 RVA: 0x00052A03 File Offset: 0x00050C03
		public MapDistanceModel MapDistanceModel { get; private set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x00052A0C File Offset: 0x00050C0C
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x00052A14 File Offset: 0x00050C14
		public PartyNavigationModel PartyNavigationModel { get; private set; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x00052A1D File Offset: 0x00050C1D
		// (set) Token: 0x0600118E RID: 4494 RVA: 0x00052A25 File Offset: 0x00050C25
		public MapWeatherModel MapWeatherModel { get; private set; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x00052A2E File Offset: 0x00050C2E
		// (set) Token: 0x06001190 RID: 4496 RVA: 0x00052A36 File Offset: 0x00050C36
		public TargetScoreCalculatingModel TargetScoreCalculatingModel { get; private set; }

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x00052A3F File Offset: 0x00050C3F
		// (set) Token: 0x06001192 RID: 4498 RVA: 0x00052A47 File Offset: 0x00050C47
		public TradeItemPriceFactorModel TradeItemPriceFactorModel { get; private set; }

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x00052A50 File Offset: 0x00050C50
		// (set) Token: 0x06001194 RID: 4500 RVA: 0x00052A58 File Offset: 0x00050C58
		public SettlementEconomyModel SettlementEconomyModel { get; private set; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x00052A61 File Offset: 0x00050C61
		// (set) Token: 0x06001196 RID: 4502 RVA: 0x00052A69 File Offset: 0x00050C69
		public SettlementFoodModel SettlementFoodModel { get; private set; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x00052A72 File Offset: 0x00050C72
		// (set) Token: 0x06001198 RID: 4504 RVA: 0x00052A7A File Offset: 0x00050C7A
		public SettlementValueModel SettlementValueModel { get; private set; }

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x00052A83 File Offset: 0x00050C83
		// (set) Token: 0x0600119A RID: 4506 RVA: 0x00052A8B File Offset: 0x00050C8B
		public SettlementMilitiaModel SettlementMilitiaModel { get; private set; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x00052A94 File Offset: 0x00050C94
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x00052A9C File Offset: 0x00050C9C
		public SettlementLoyaltyModel SettlementLoyaltyModel { get; private set; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x00052AA5 File Offset: 0x00050CA5
		// (set) Token: 0x0600119E RID: 4510 RVA: 0x00052AAD File Offset: 0x00050CAD
		public SettlementSecurityModel SettlementSecurityModel { get; private set; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x00052AB6 File Offset: 0x00050CB6
		// (set) Token: 0x060011A0 RID: 4512 RVA: 0x00052ABE File Offset: 0x00050CBE
		public SettlementProsperityModel SettlementProsperityModel { get; private set; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x00052AC7 File Offset: 0x00050CC7
		// (set) Token: 0x060011A2 RID: 4514 RVA: 0x00052ACF File Offset: 0x00050CCF
		public SettlementGarrisonModel SettlementGarrisonModel { get; private set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x00052AD8 File Offset: 0x00050CD8
		// (set) Token: 0x060011A4 RID: 4516 RVA: 0x00052AE0 File Offset: 0x00050CE0
		public ClanTierModel ClanTierModel { get; private set; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x00052AE9 File Offset: 0x00050CE9
		// (set) Token: 0x060011A6 RID: 4518 RVA: 0x00052AF1 File Offset: 0x00050CF1
		public VassalRewardsModel VassalRewardsModel { get; private set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x00052AFA File Offset: 0x00050CFA
		// (set) Token: 0x060011A8 RID: 4520 RVA: 0x00052B02 File Offset: 0x00050D02
		public ClanPoliticsModel ClanPoliticsModel { get; private set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x00052B0B File Offset: 0x00050D0B
		// (set) Token: 0x060011AA RID: 4522 RVA: 0x00052B13 File Offset: 0x00050D13
		public ClanFinanceModel ClanFinanceModel { get; private set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00052B1C File Offset: 0x00050D1C
		// (set) Token: 0x060011AC RID: 4524 RVA: 0x00052B24 File Offset: 0x00050D24
		public SettlementTaxModel SettlementTaxModel { get; private set; }

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x00052B2D File Offset: 0x00050D2D
		// (set) Token: 0x060011AE RID: 4526 RVA: 0x00052B35 File Offset: 0x00050D35
		public HeroAgentLocationModel HeroAgentLocationModel { get; private set; }

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x00052B3E File Offset: 0x00050D3E
		// (set) Token: 0x060011B0 RID: 4528 RVA: 0x00052B46 File Offset: 0x00050D46
		public HeirSelectionCalculationModel HeirSelectionCalculationModel { get; private set; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x00052B4F File Offset: 0x00050D4F
		// (set) Token: 0x060011B2 RID: 4530 RVA: 0x00052B57 File Offset: 0x00050D57
		public HeroDeathProbabilityCalculationModel HeroDeathProbabilityCalculationModel { get; private set; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x00052B60 File Offset: 0x00050D60
		// (set) Token: 0x060011B4 RID: 4532 RVA: 0x00052B68 File Offset: 0x00050D68
		public BuildingConstructionModel BuildingConstructionModel { get; private set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x00052B71 File Offset: 0x00050D71
		// (set) Token: 0x060011B6 RID: 4534 RVA: 0x00052B79 File Offset: 0x00050D79
		public BuildingEffectModel BuildingEffectModel { get; private set; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00052B82 File Offset: 0x00050D82
		// (set) Token: 0x060011B8 RID: 4536 RVA: 0x00052B8A File Offset: 0x00050D8A
		public WallHitPointCalculationModel WallHitPointCalculationModel { get; private set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x00052B93 File Offset: 0x00050D93
		// (set) Token: 0x060011BA RID: 4538 RVA: 0x00052B9B File Offset: 0x00050D9B
		public MarriageModel MarriageModel { get; private set; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x00052BA4 File Offset: 0x00050DA4
		// (set) Token: 0x060011BC RID: 4540 RVA: 0x00052BAC File Offset: 0x00050DAC
		public AgeModel AgeModel { get; private set; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00052BB5 File Offset: 0x00050DB5
		// (set) Token: 0x060011BE RID: 4542 RVA: 0x00052BBD File Offset: 0x00050DBD
		public PlayerProgressionModel PlayerProgressionModel { get; private set; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x00052BC6 File Offset: 0x00050DC6
		// (set) Token: 0x060011C0 RID: 4544 RVA: 0x00052BCE File Offset: 0x00050DCE
		public DailyTroopXpBonusModel DailyTroopXpBonusModel { get; private set; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00052BD7 File Offset: 0x00050DD7
		// (set) Token: 0x060011C2 RID: 4546 RVA: 0x00052BDF File Offset: 0x00050DDF
		public PregnancyModel PregnancyModel { get; private set; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00052BE8 File Offset: 0x00050DE8
		// (set) Token: 0x060011C4 RID: 4548 RVA: 0x00052BF0 File Offset: 0x00050DF0
		public NotablePowerModel NotablePowerModel { get; private set; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00052BF9 File Offset: 0x00050DF9
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x00052C01 File Offset: 0x00050E01
		public MilitaryPowerModel MilitaryPowerModel { get; private set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00052C0A File Offset: 0x00050E0A
		// (set) Token: 0x060011C8 RID: 4552 RVA: 0x00052C12 File Offset: 0x00050E12
		public PrisonerDonationModel PrisonerDonationModel { get; private set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00052C1B File Offset: 0x00050E1B
		// (set) Token: 0x060011CA RID: 4554 RVA: 0x00052C23 File Offset: 0x00050E23
		public NotableSpawnModel NotableSpawnModel { get; private set; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00052C2C File Offset: 0x00050E2C
		// (set) Token: 0x060011CC RID: 4556 RVA: 0x00052C34 File Offset: 0x00050E34
		public TournamentModel TournamentModel { get; private set; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x00052C3D File Offset: 0x00050E3D
		// (set) Token: 0x060011CE RID: 4558 RVA: 0x00052C45 File Offset: 0x00050E45
		public CrimeModel CrimeModel { get; private set; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x00052C4E File Offset: 0x00050E4E
		// (set) Token: 0x060011D0 RID: 4560 RVA: 0x00052C56 File Offset: 0x00050E56
		public DisguiseDetectionModel DisguiseDetectionModel { get; private set; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x00052C5F File Offset: 0x00050E5F
		// (set) Token: 0x060011D2 RID: 4562 RVA: 0x00052C67 File Offset: 0x00050E67
		public BribeCalculationModel BribeCalculationModel { get; private set; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00052C70 File Offset: 0x00050E70
		// (set) Token: 0x060011D4 RID: 4564 RVA: 0x00052C78 File Offset: 0x00050E78
		public TroopSacrificeModel TroopSacrificeModel { get; private set; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x00052C81 File Offset: 0x00050E81
		// (set) Token: 0x060011D6 RID: 4566 RVA: 0x00052C89 File Offset: 0x00050E89
		public SiegeStrategyActionModel SiegeStrategyActionModel { get; private set; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x00052C92 File Offset: 0x00050E92
		// (set) Token: 0x060011D8 RID: 4568 RVA: 0x00052C9A File Offset: 0x00050E9A
		public SiegeEventModel SiegeEventModel { get; private set; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x00052CA3 File Offset: 0x00050EA3
		// (set) Token: 0x060011DA RID: 4570 RVA: 0x00052CAB File Offset: 0x00050EAB
		public SiegeAftermathModel SiegeAftermathModel { get; private set; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x00052CB4 File Offset: 0x00050EB4
		// (set) Token: 0x060011DC RID: 4572 RVA: 0x00052CBC File Offset: 0x00050EBC
		public SiegeLordsHallFightModel SiegeLordsHallFightModel { get; private set; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x00052CC5 File Offset: 0x00050EC5
		// (set) Token: 0x060011DE RID: 4574 RVA: 0x00052CCD File Offset: 0x00050ECD
		public CompanionHiringPriceCalculationModel CompanionHiringPriceCalculationModel { get; private set; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x00052CD6 File Offset: 0x00050ED6
		// (set) Token: 0x060011E0 RID: 4576 RVA: 0x00052CDE File Offset: 0x00050EDE
		public BuildingScoreCalculationModel BuildingScoreCalculationModel { get; private set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00052CE7 File Offset: 0x00050EE7
		// (set) Token: 0x060011E2 RID: 4578 RVA: 0x00052CEF File Offset: 0x00050EEF
		public SettlementAccessModel SettlementAccessModel { get; private set; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x00052CF8 File Offset: 0x00050EF8
		// (set) Token: 0x060011E4 RID: 4580 RVA: 0x00052D00 File Offset: 0x00050F00
		public IssueModel IssueModel { get; private set; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x00052D09 File Offset: 0x00050F09
		// (set) Token: 0x060011E6 RID: 4582 RVA: 0x00052D11 File Offset: 0x00050F11
		public PrisonerRecruitmentCalculationModel PrisonerRecruitmentCalculationModel { get; private set; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x00052D1A File Offset: 0x00050F1A
		// (set) Token: 0x060011E8 RID: 4584 RVA: 0x00052D22 File Offset: 0x00050F22
		public PartyTroopUpgradeModel PartyTroopUpgradeModel { get; private set; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00052D2B File Offset: 0x00050F2B
		// (set) Token: 0x060011EA RID: 4586 RVA: 0x00052D33 File Offset: 0x00050F33
		public TavernMercenaryTroopsModel TavernMercenaryTroopsModel { get; private set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00052D3C File Offset: 0x00050F3C
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x00052D44 File Offset: 0x00050F44
		public WorkshopModel WorkshopModel { get; private set; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00052D4D File Offset: 0x00050F4D
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x00052D55 File Offset: 0x00050F55
		public DifficultyModel DifficultyModel { get; private set; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00052D5E File Offset: 0x00050F5E
		// (set) Token: 0x060011F0 RID: 4592 RVA: 0x00052D66 File Offset: 0x00050F66
		public LocationModel LocationModel { get; private set; }

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x00052D6F File Offset: 0x00050F6F
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x00052D77 File Offset: 0x00050F77
		public PrisonBreakModel PrisonBreakModel { get; private set; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x00052D80 File Offset: 0x00050F80
		// (set) Token: 0x060011F4 RID: 4596 RVA: 0x00052D88 File Offset: 0x00050F88
		public BattleCaptainModel BattleCaptainModel { get; private set; }

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x00052D91 File Offset: 0x00050F91
		// (set) Token: 0x060011F6 RID: 4598 RVA: 0x00052D99 File Offset: 0x00050F99
		public ExecutionRelationModel ExecutionRelationModel { get; private set; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00052DA2 File Offset: 0x00050FA2
		// (set) Token: 0x060011F8 RID: 4600 RVA: 0x00052DAA File Offset: 0x00050FAA
		public BannerItemModel BannerItemModel { get; private set; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00052DB3 File Offset: 0x00050FB3
		// (set) Token: 0x060011FA RID: 4602 RVA: 0x00052DBB File Offset: 0x00050FBB
		public DelayedTeleportationModel DelayedTeleportationModel { get; private set; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x00052DC4 File Offset: 0x00050FC4
		// (set) Token: 0x060011FC RID: 4604 RVA: 0x00052DCC File Offset: 0x00050FCC
		public TroopSupplierProbabilityModel TroopSupplierProbabilityModel { get; private set; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x00052DD5 File Offset: 0x00050FD5
		// (set) Token: 0x060011FE RID: 4606 RVA: 0x00052DDD File Offset: 0x00050FDD
		public CutsceneSelectionModel CutsceneSelectionModel { get; private set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x00052DE6 File Offset: 0x00050FE6
		// (set) Token: 0x06001200 RID: 4608 RVA: 0x00052DEE File Offset: 0x00050FEE
		public EquipmentSelectionModel EquipmentSelectionModel { get; private set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x00052DF7 File Offset: 0x00050FF7
		// (set) Token: 0x06001202 RID: 4610 RVA: 0x00052DFF File Offset: 0x00050FFF
		public AlleyModel AlleyModel { get; private set; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x00052E08 File Offset: 0x00051008
		// (set) Token: 0x06001204 RID: 4612 RVA: 0x00052E10 File Offset: 0x00051010
		public VoiceOverModel VoiceOverModel { get; private set; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x00052E19 File Offset: 0x00051019
		// (set) Token: 0x06001206 RID: 4614 RVA: 0x00052E21 File Offset: 0x00051021
		public CampaignTimeModel CampaignTimeModel { get; private set; }

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x00052E2A File Offset: 0x0005102A
		// (set) Token: 0x06001208 RID: 4616 RVA: 0x00052E32 File Offset: 0x00051032
		public VillageTradeModel VillageTradeModel { get; private set; }

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x00052E3B File Offset: 0x0005103B
		// (set) Token: 0x0600120A RID: 4618 RVA: 0x00052E43 File Offset: 0x00051043
		public HeroCreationModel HeroCreationModel { get; private set; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00052E4C File Offset: 0x0005104C
		// (set) Token: 0x0600120C RID: 4620 RVA: 0x00052E54 File Offset: 0x00051054
		public CampaignShipDamageModel CampaignShipDamageModel { get; private set; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00052E5D File Offset: 0x0005105D
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x00052E65 File Offset: 0x00051065
		public CampaignShipParametersModel CampaignShipParametersModel { get; private set; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x00052E6E File Offset: 0x0005106E
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x00052E76 File Offset: 0x00051076
		public BuildingModel BuildingModel { get; private set; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00052E7F File Offset: 0x0005107F
		// (set) Token: 0x06001212 RID: 4626 RVA: 0x00052E87 File Offset: 0x00051087
		public ShipCostModel ShipCostModel { get; private set; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x00052E90 File Offset: 0x00051090
		// (set) Token: 0x06001214 RID: 4628 RVA: 0x00052E98 File Offset: 0x00051098
		public ShipStatModel ShipStatModel { get; private set; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x00052EA1 File Offset: 0x000510A1
		// (set) Token: 0x06001216 RID: 4630 RVA: 0x00052EA9 File Offset: 0x000510A9
		public SceneModel SceneModel { get; private set; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x00052EB2 File Offset: 0x000510B2
		// (set) Token: 0x06001218 RID: 4632 RVA: 0x00052EBA File Offset: 0x000510BA
		public BodyPropertiesModel BodyPropertiesModel { get; private set; }

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x00052EC3 File Offset: 0x000510C3
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x00052ECB File Offset: 0x000510CB
		public IncidentModel IncidentModel { get; private set; }

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x00052ED4 File Offset: 0x000510D4
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x00052EDC File Offset: 0x000510DC
		public FleetManagementModel FleetManagementModel { get; private set; }

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x00052EE5 File Offset: 0x000510E5
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x00052EED File Offset: 0x000510ED
		public ClanMemberPartyRoleModel ClanMemberPartyRoleModel { get; private set; }

		// Token: 0x0600121F RID: 4639 RVA: 0x00052EF8 File Offset: 0x000510F8
		private void GetSpecificGameBehaviors()
		{
			if (Campaign.Current.GameMode == CampaignGameMode.Campaign || Campaign.Current.GameMode == CampaignGameMode.Tutorial)
			{
				this.CharacterDevelopmentModel = base.GetGameModel<CharacterDevelopmentModel>();
				this.CharacterStatsModel = base.GetGameModel<CharacterStatsModel>();
				this.EncounterModel = base.GetGameModel<EncounterModel>();
				this.SettlementPatrolModel = base.GetGameModel<SettlementPatrolModel>();
				this.ItemDiscardModel = base.GetGameModel<ItemDiscardModel>();
				this.ValuationModel = base.GetGameModel<ValuationModel>();
				this.MapVisibilityModel = base.GetGameModel<MapVisibilityModel>();
				this.InformationRestrictionModel = base.GetGameModel<InformationRestrictionModel>();
				this.PartySpeedCalculatingModel = base.GetGameModel<PartySpeedModel>();
				this.PartyHealingModel = base.GetGameModel<PartyHealingModel>();
				this.CaravanModel = base.GetGameModel<CaravanModel>();
				this.PartyTrainingModel = base.GetGameModel<PartyTrainingModel>();
				this.PartyTradeModel = base.GetGameModel<PartyTradeModel>();
				this.RansomValueCalculationModel = base.GetGameModel<RansomValueCalculationModel>();
				this.RaidModel = base.GetGameModel<RaidModel>();
				this.CombatSimulationModel = base.GetGameModel<CombatSimulationModel>();
				this.CombatXpModel = base.GetGameModel<CombatXpModel>();
				this.GenericXpModel = base.GetGameModel<GenericXpModel>();
				this.TradeAgreementModel = base.GetGameModel<TradeAgreementModel>();
				this.SmithingModel = base.GetGameModel<SmithingModel>();
				this.MobilePartyFoodConsumptionModel = base.GetGameModel<MobilePartyFoodConsumptionModel>();
				this.PartyImpairmentModel = base.GetGameModel<PartyImpairmentModel>();
				this.PartyFoodBuyingModel = base.GetGameModel<PartyFoodBuyingModel>();
				this.PartyMoraleModel = base.GetGameModel<PartyMoraleModel>();
				this.PartyDesertionModel = base.GetGameModel<PartyDesertionModel>();
				this.HideoutModel = base.GetGameModel<HideoutModel>();
				this.DiplomacyModel = base.GetGameModel<DiplomacyModel>();
				this.AllianceModel = base.GetGameModel<AllianceModel>();
				this.PartyTransitionModel = base.GetGameModel<PartyTransitionModel>();
				this.MinorFactionsModel = base.GetGameModel<MinorFactionsModel>();
				this.KingdomCreationModel = base.GetGameModel<KingdomCreationModel>();
				this.EmissaryModel = base.GetGameModel<EmissaryModel>();
				this.KingdomDecisionPermissionModel = base.GetGameModel<KingdomDecisionPermissionModel>();
				this.VillageProductionCalculatorModel = base.GetGameModel<VillageProductionCalculatorModel>();
				this.RomanceModel = base.GetGameModel<RomanceModel>();
				this.VolunteerModel = base.GetGameModel<VolunteerModel>();
				this.ArmyManagementCalculationModel = base.GetGameModel<ArmyManagementCalculationModel>();
				this.BanditDensityModel = base.GetGameModel<BanditDensityModel>();
				this.EncounterGameMenuModel = base.GetGameModel<EncounterGameMenuModel>();
				this.BattleRewardModel = base.GetGameModel<BattleRewardModel>();
				this.MapTrackModel = base.GetGameModel<MapTrackModel>();
				this.MapDistanceModel = base.GetGameModel<MapDistanceModel>();
				this.PartyNavigationModel = base.GetGameModel<PartyNavigationModel>();
				this.MapWeatherModel = base.GetGameModel<MapWeatherModel>();
				this.TargetScoreCalculatingModel = base.GetGameModel<TargetScoreCalculatingModel>();
				this.PartySizeLimitModel = base.GetGameModel<PartySizeLimitModel>();
				this.PartyShipLimitModel = base.GetGameModel<PartyShipLimitModel>();
				this.PartyWageModel = base.GetGameModel<PartyWageModel>();
				this.PlayerProgressionModel = base.GetGameModel<PlayerProgressionModel>();
				this.InventoryCapacityModel = base.GetGameModel<InventoryCapacityModel>();
				this.TradeItemPriceFactorModel = base.GetGameModel<TradeItemPriceFactorModel>();
				this.SettlementValueModel = base.GetGameModel<SettlementValueModel>();
				this.SettlementEconomyModel = base.GetGameModel<SettlementEconomyModel>();
				this.SettlementMilitiaModel = base.GetGameModel<SettlementMilitiaModel>();
				this.SettlementFoodModel = base.GetGameModel<SettlementFoodModel>();
				this.SettlementLoyaltyModel = base.GetGameModel<SettlementLoyaltyModel>();
				this.SettlementSecurityModel = base.GetGameModel<SettlementSecurityModel>();
				this.SettlementProsperityModel = base.GetGameModel<SettlementProsperityModel>();
				this.SettlementGarrisonModel = base.GetGameModel<SettlementGarrisonModel>();
				this.SettlementTaxModel = base.GetGameModel<SettlementTaxModel>();
				this.HeroAgentLocationModel = base.GetGameModel<HeroAgentLocationModel>();
				this.BarterModel = base.GetGameModel<BarterModel>();
				this.PersuasionModel = base.GetGameModel<PersuasionModel>();
				this.DefectionModel = base.GetGameModel<DefectionModel>();
				this.ClanTierModel = base.GetGameModel<ClanTierModel>();
				this.VassalRewardsModel = base.GetGameModel<VassalRewardsModel>();
				this.ClanPoliticsModel = base.GetGameModel<ClanPoliticsModel>();
				this.ClanFinanceModel = base.GetGameModel<ClanFinanceModel>();
				this.HeirSelectionCalculationModel = base.GetGameModel<HeirSelectionCalculationModel>();
				this.HeroDeathProbabilityCalculationModel = base.GetGameModel<HeroDeathProbabilityCalculationModel>();
				this.BuildingConstructionModel = base.GetGameModel<BuildingConstructionModel>();
				this.BuildingEffectModel = base.GetGameModel<BuildingEffectModel>();
				this.WallHitPointCalculationModel = base.GetGameModel<WallHitPointCalculationModel>();
				this.MarriageModel = base.GetGameModel<MarriageModel>();
				this.AgeModel = base.GetGameModel<AgeModel>();
				this.DailyTroopXpBonusModel = base.GetGameModel<DailyTroopXpBonusModel>();
				this.PregnancyModel = base.GetGameModel<PregnancyModel>();
				this.NotablePowerModel = base.GetGameModel<NotablePowerModel>();
				this.NotableSpawnModel = base.GetGameModel<NotableSpawnModel>();
				this.TournamentModel = base.GetGameModel<TournamentModel>();
				this.SiegeStrategyActionModel = base.GetGameModel<SiegeStrategyActionModel>();
				this.SiegeEventModel = base.GetGameModel<SiegeEventModel>();
				this.SiegeAftermathModel = base.GetGameModel<SiegeAftermathModel>();
				this.SiegeLordsHallFightModel = base.GetGameModel<SiegeLordsHallFightModel>();
				this.CrimeModel = base.GetGameModel<CrimeModel>();
				this.DisguiseDetectionModel = base.GetGameModel<DisguiseDetectionModel>();
				this.BribeCalculationModel = base.GetGameModel<BribeCalculationModel>();
				this.CompanionHiringPriceCalculationModel = base.GetGameModel<CompanionHiringPriceCalculationModel>();
				this.TroopSacrificeModel = base.GetGameModel<TroopSacrificeModel>();
				this.BuildingScoreCalculationModel = base.GetGameModel<BuildingScoreCalculationModel>();
				this.SettlementAccessModel = base.GetGameModel<SettlementAccessModel>();
				this.IssueModel = base.GetGameModel<IssueModel>();
				this.PrisonerRecruitmentCalculationModel = base.GetGameModel<PrisonerRecruitmentCalculationModel>();
				this.PartyTroopUpgradeModel = base.GetGameModel<PartyTroopUpgradeModel>();
				this.TavernMercenaryTroopsModel = base.GetGameModel<TavernMercenaryTroopsModel>();
				this.WorkshopModel = base.GetGameModel<WorkshopModel>();
				this.DifficultyModel = base.GetGameModel<DifficultyModel>();
				this.LocationModel = base.GetGameModel<LocationModel>();
				this.MilitaryPowerModel = base.GetGameModel<MilitaryPowerModel>();
				this.PrisonerDonationModel = base.GetGameModel<PrisonerDonationModel>();
				this.PrisonBreakModel = base.GetGameModel<PrisonBreakModel>();
				this.BattleCaptainModel = base.GetGameModel<BattleCaptainModel>();
				this.ExecutionRelationModel = base.GetGameModel<ExecutionRelationModel>();
				this.BannerItemModel = base.GetGameModel<BannerItemModel>();
				this.DelayedTeleportationModel = base.GetGameModel<DelayedTeleportationModel>();
				this.TroopSupplierProbabilityModel = base.GetGameModel<TroopSupplierProbabilityModel>();
				this.CutsceneSelectionModel = base.GetGameModel<CutsceneSelectionModel>();
				this.EquipmentSelectionModel = base.GetGameModel<EquipmentSelectionModel>();
				this.AlleyModel = base.GetGameModel<AlleyModel>();
				this.VoiceOverModel = base.GetGameModel<VoiceOverModel>();
				this.CampaignTimeModel = base.GetGameModel<CampaignTimeModel>();
				this.VillageTradeModel = base.GetGameModel<VillageTradeModel>();
				this.PartyNavigationModel = base.GetGameModel<PartyNavigationModel>();
				this.MobilePartyAIModel = base.GetGameModel<MobilePartyAIModel>();
				this.HeroCreationModel = base.GetGameModel<HeroCreationModel>();
				this.CampaignShipDamageModel = base.GetGameModel<CampaignShipDamageModel>();
				this.CampaignShipParametersModel = base.GetGameModel<CampaignShipParametersModel>();
				this.BuildingModel = base.GetGameModel<BuildingModel>();
				this.ShipCostModel = base.GetGameModel<ShipCostModel>();
				this.SceneModel = base.GetGameModel<SceneModel>();
				this.IncidentModel = base.GetGameModel<IncidentModel>();
				this.BodyPropertiesModel = base.GetGameModel<BodyPropertiesModel>();
				this.FleetManagementModel = base.GetGameModel<FleetManagementModel>();
				this.ShipStatModel = base.GetGameModel<ShipStatModel>();
				this.ClanMemberPartyRoleModel = base.GetGameModel<ClanMemberPartyRoleModel>();
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x000534FE File Offset: 0x000516FE
		public GameModels(IEnumerable<GameModel> inputComponents)
			: base(inputComponents)
		{
			this.GetSpecificGameBehaviors();
		}
	}
}
