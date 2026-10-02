using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001DE RID: 478
	public abstract class MBSubModuleBase
	{
		// Token: 0x06001C20 RID: 7200 RVA: 0x00060FFE File Offset: 0x0005F1FE
		protected internal virtual void OnSubModuleLoad()
		{
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x00061000 File Offset: 0x0005F200
		protected internal virtual void OnSubModuleUnloaded()
		{
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x00061002 File Offset: 0x0005F202
		protected internal virtual void OnBeforeInitialModuleScreenSetAsRoot()
		{
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x00061004 File Offset: 0x0005F204
		protected internal virtual void RegisterSubModuleTypes()
		{
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x00061006 File Offset: 0x0005F206
		protected internal virtual void OnNewModuleLoad()
		{
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x00061008 File Offset: 0x0005F208
		public virtual void OnConfigChanged()
		{
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x0006100A File Offset: 0x0005F20A
		protected internal virtual void OnBeforeGameStart(MBGameManager mbGameManager, List<string> disabledModules)
		{
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0006100C File Offset: 0x0005F20C
		protected internal virtual void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x0006100E File Offset: 0x0005F20E
		protected internal virtual void OnApplicationTick(float dt)
		{
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x00061010 File Offset: 0x0005F210
		protected internal virtual void AfterAsyncTickTick(float dt)
		{
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x00061012 File Offset: 0x0005F212
		protected internal virtual void InitializeGameStarter(Game game, IGameStarter starterObject)
		{
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x00061014 File Offset: 0x0005F214
		public virtual void OnGameLoaded(Game game, object initializerObject)
		{
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x00061016 File Offset: 0x0005F216
		public virtual void OnAfterGameLoaded(Game game)
		{
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x00061018 File Offset: 0x0005F218
		public virtual void OnNewGameCreated(Game game, object initializerObject)
		{
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0006101A File Offset: 0x0005F21A
		public virtual void BeginGameStart(Game game)
		{
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x0006101C File Offset: 0x0005F21C
		public virtual void OnCampaignStart(Game game, object starterObject)
		{
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x0006101E File Offset: 0x0005F21E
		public virtual void RegisterSubModuleObjects(bool isSavedCampaign)
		{
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x00061020 File Offset: 0x0005F220
		public virtual void AfterRegisterSubModuleObjects(bool isSavedCampaign)
		{
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x00061022 File Offset: 0x0005F222
		public virtual void OnMultiplayerGameStart(Game game, object starterObject)
		{
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x00061024 File Offset: 0x0005F224
		public virtual void OnGameInitializationFinished(Game game)
		{
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x00061026 File Offset: 0x0005F226
		public virtual void OnAfterGameInitializationFinished(Game game, object starterObject)
		{
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00061028 File Offset: 0x0005F228
		public virtual bool DoLoading(Game game)
		{
			return true;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0006102B File Offset: 0x0005F22B
		public virtual void OnGameEnd(Game game)
		{
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0006102D File Offset: 0x0005F22D
		public virtual void OnMissionBehaviorInitialize(Mission mission)
		{
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x0006102F File Offset: 0x0005F22F
		public virtual void OnBeforeMissionBehaviorInitialize(Mission mission)
		{
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x00061031 File Offset: 0x0005F231
		public virtual void OnInitialState()
		{
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x00061033 File Offset: 0x0005F233
		protected internal virtual void OnNetworkTick(float dt)
		{
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x00061035 File Offset: 0x0005F235
		public virtual void OnSubModuleActivated()
		{
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x00061037 File Offset: 0x0005F237
		public virtual void OnSubModuleDeactivated()
		{
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x00061039 File Offset: 0x0005F239
		public virtual void InitializeSubModuleGameObjects(Game game)
		{
		}
	}
}
