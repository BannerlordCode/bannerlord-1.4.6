using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000257 RID: 599
	public sealed class MissionGameModels : GameModelsManager
	{
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x0007797B File Offset: 0x00075B7B
		// (set) Token: 0x060021F4 RID: 8692 RVA: 0x00077982 File Offset: 0x00075B82
		public static MissionGameModels Current { get; private set; }

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x0007798A File Offset: 0x00075B8A
		// (set) Token: 0x060021F6 RID: 8694 RVA: 0x00077992 File Offset: 0x00075B92
		public AgentStatCalculateModel AgentStatCalculateModel { get; private set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x0007799B File Offset: 0x00075B9B
		// (set) Token: 0x060021F8 RID: 8696 RVA: 0x000779A3 File Offset: 0x00075BA3
		public ApplyWeatherEffectsModel ApplyWeatherEffectsModel { get; private set; }

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060021F9 RID: 8697 RVA: 0x000779AC File Offset: 0x00075BAC
		// (set) Token: 0x060021FA RID: 8698 RVA: 0x000779B4 File Offset: 0x00075BB4
		public StrikeMagnitudeCalculationModel StrikeMagnitudeModel { get; private set; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060021FB RID: 8699 RVA: 0x000779BD File Offset: 0x00075BBD
		// (set) Token: 0x060021FC RID: 8700 RVA: 0x000779C5 File Offset: 0x00075BC5
		public AgentApplyDamageModel AgentApplyDamageModel { get; private set; }

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x000779CE File Offset: 0x00075BCE
		// (set) Token: 0x060021FE RID: 8702 RVA: 0x000779D6 File Offset: 0x00075BD6
		public AgentDecideKilledOrUnconsciousModel AgentDecideKilledOrUnconsciousModel { get; private set; }

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x000779DF File Offset: 0x00075BDF
		// (set) Token: 0x06002200 RID: 8704 RVA: 0x000779E7 File Offset: 0x00075BE7
		public MissionDifficultyModel MissionDifficultyModel { get; private set; }

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x000779F0 File Offset: 0x00075BF0
		// (set) Token: 0x06002202 RID: 8706 RVA: 0x000779F8 File Offset: 0x00075BF8
		public BattleMoraleModel BattleMoraleModel { get; private set; }

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x00077A01 File Offset: 0x00075C01
		// (set) Token: 0x06002204 RID: 8708 RVA: 0x00077A09 File Offset: 0x00075C09
		public BattleInitializationModel BattleInitializationModel { get; private set; }

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06002205 RID: 8709 RVA: 0x00077A12 File Offset: 0x00075C12
		// (set) Token: 0x06002206 RID: 8710 RVA: 0x00077A1A File Offset: 0x00075C1A
		public BattleSpawnModel BattleSpawnModel { get; private set; }

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x00077A23 File Offset: 0x00075C23
		// (set) Token: 0x06002208 RID: 8712 RVA: 0x00077A2B File Offset: 0x00075C2B
		public BattleBannerBearersModel BattleBannerBearersModel { get; private set; }

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x00077A34 File Offset: 0x00075C34
		// (set) Token: 0x0600220A RID: 8714 RVA: 0x00077A3C File Offset: 0x00075C3C
		public FormationArrangementModel FormationArrangementsModel { get; private set; }

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x00077A45 File Offset: 0x00075C45
		// (set) Token: 0x0600220C RID: 8716 RVA: 0x00077A4D File Offset: 0x00075C4D
		public AutoBlockModel AutoBlockModel { get; private set; }

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x0600220D RID: 8717 RVA: 0x00077A56 File Offset: 0x00075C56
		// (set) Token: 0x0600220E RID: 8718 RVA: 0x00077A5E File Offset: 0x00075C5E
		public DamageParticleModel DamageParticleModel { get; private set; }

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x00077A67 File Offset: 0x00075C67
		// (set) Token: 0x06002210 RID: 8720 RVA: 0x00077A6F File Offset: 0x00075C6F
		public ItemPickupModel ItemPickupModel { get; private set; }

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x00077A78 File Offset: 0x00075C78
		// (set) Token: 0x06002212 RID: 8722 RVA: 0x00077A80 File Offset: 0x00075C80
		public MissionShipParametersModel MissionShipParametersModel { get; private set; }

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x00077A89 File Offset: 0x00075C89
		// (set) Token: 0x06002214 RID: 8724 RVA: 0x00077A91 File Offset: 0x00075C91
		public MissionSiegeEngineCalculationModel MissionSiegeEngineCalculationModel { get; private set; }

		// Token: 0x06002215 RID: 8725 RVA: 0x00077A9C File Offset: 0x00075C9C
		private void GetSpecificGameBehaviors()
		{
			this.AgentStatCalculateModel = base.GetGameModel<AgentStatCalculateModel>();
			this.ApplyWeatherEffectsModel = base.GetGameModel<ApplyWeatherEffectsModel>();
			this.StrikeMagnitudeModel = base.GetGameModel<StrikeMagnitudeCalculationModel>();
			this.AgentApplyDamageModel = base.GetGameModel<AgentApplyDamageModel>();
			this.AgentDecideKilledOrUnconsciousModel = base.GetGameModel<AgentDecideKilledOrUnconsciousModel>();
			this.MissionDifficultyModel = base.GetGameModel<MissionDifficultyModel>();
			this.BattleMoraleModel = base.GetGameModel<BattleMoraleModel>();
			this.BattleInitializationModel = base.GetGameModel<BattleInitializationModel>();
			this.BattleSpawnModel = base.GetGameModel<BattleSpawnModel>();
			this.BattleBannerBearersModel = base.GetGameModel<BattleBannerBearersModel>();
			this.FormationArrangementsModel = base.GetGameModel<FormationArrangementModel>();
			this.AutoBlockModel = base.GetGameModel<AutoBlockModel>();
			this.DamageParticleModel = base.GetGameModel<DamageParticleModel>();
			this.ItemPickupModel = base.GetGameModel<ItemPickupModel>();
			this.MissionShipParametersModel = base.GetGameModel<MissionShipParametersModel>();
			this.MissionSiegeEngineCalculationModel = base.GetGameModel<MissionSiegeEngineCalculationModel>();
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x00077B69 File Offset: 0x00075D69
		private void MakeGameComponentBindings()
		{
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x00077B6B File Offset: 0x00075D6B
		public MissionGameModels(IEnumerable<GameModel> inputComponents)
			: base(inputComponents)
		{
			MissionGameModels.Current = this;
			this.GetSpecificGameBehaviors();
			this.MakeGameComponentBindings();
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x00077B86 File Offset: 0x00075D86
		public static void Clear()
		{
			MissionGameModels.Current = null;
		}
	}
}
