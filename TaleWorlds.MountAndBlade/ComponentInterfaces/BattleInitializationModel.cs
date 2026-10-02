using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.ComponentInterfaces
{
	// Token: 0x020003FC RID: 1020
	public abstract class BattleInitializationModel : MBGameModel<BattleInitializationModel>
	{
		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x000E4D9A File Offset: 0x000E2F9A
		// (set) Token: 0x06003793 RID: 14227 RVA: 0x000E4DA1 File Offset: 0x000E2FA1
		public static bool BypassPlayerDeployment { get; private set; }

		// Token: 0x06003794 RID: 14228
		public abstract List<FormationClass> GetAllAvailableTroopTypes();

		// Token: 0x06003795 RID: 14229
		protected abstract bool CanPlayerSideDeployWithOrderOfBattleAux();

		// Token: 0x06003796 RID: 14230 RVA: 0x000E4DA9 File Offset: 0x000E2FA9
		public bool CanPlayerSideDeployWithOrderOfBattle()
		{
			if (!this._isCanPlayerSideDeployWithOOBCached)
			{
				this._canPlayerSideDeployWithOOB = !BattleInitializationModel.BypassPlayerDeployment && this.CanPlayerSideDeployWithOrderOfBattleAux();
				this._isCanPlayerSideDeployWithOOBCached = true;
			}
			return this._canPlayerSideDeployWithOOB;
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x000E4DD6 File Offset: 0x000E2FD6
		public void InitializeModel()
		{
			this._isCanPlayerSideDeployWithOOBCached = false;
			this._isInitialized = true;
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x000E4DE6 File Offset: 0x000E2FE6
		public void FinalizeModel()
		{
			this._isInitialized = false;
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x000E4DF0 File Offset: 0x000E2FF0
		public static void SetBypassPlayerDeployment(bool value)
		{
			MissionGameModels missionGameModels = MissionGameModels.Current;
			BattleInitializationModel battleInitializationModel = ((missionGameModels != null) ? missionGameModels.BattleInitializationModel : null);
			if (battleInitializationModel != null && BattleInitializationModel.BypassPlayerDeployment != value)
			{
				battleInitializationModel._isCanPlayerSideDeployWithOOBCached = false;
			}
			BattleInitializationModel.BypassPlayerDeployment = value;
		}

		// Token: 0x040017D4 RID: 6100
		public const int MinimumTroopCountForPlayerDeployment = 20;

		// Token: 0x040017D6 RID: 6102
		private bool _canPlayerSideDeployWithOOB;

		// Token: 0x040017D7 RID: 6103
		private bool _isCanPlayerSideDeployWithOOBCached;

		// Token: 0x040017D8 RID: 6104
		private bool _isInitialized;
	}
}
