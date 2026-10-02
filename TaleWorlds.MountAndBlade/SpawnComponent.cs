using System;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002C4 RID: 708
	public class SpawnComponent : MissionLogic
	{
		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x0009A51E File Offset: 0x0009871E
		// (set) Token: 0x060028AF RID: 10415 RVA: 0x0009A526 File Offset: 0x00098726
		public SpawnFrameBehaviorBase SpawnFrameBehavior { get; private set; }

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x0009A52F File Offset: 0x0009872F
		// (set) Token: 0x060028B1 RID: 10417 RVA: 0x0009A537 File Offset: 0x00098737
		public SpawningBehaviorBase SpawningBehavior { get; private set; }

		// Token: 0x060028B2 RID: 10418 RVA: 0x0009A540 File Offset: 0x00098740
		public SpawnComponent(SpawnFrameBehaviorBase spawnFrameBehavior, SpawningBehaviorBase spawningBehavior)
		{
			this.SpawnFrameBehavior = spawnFrameBehavior;
			this.SpawningBehavior = spawningBehavior;
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x0009A556 File Offset: 0x00098756
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._missionMultiplayerGameModeBase = base.Mission.GetMissionBehavior<MissionMultiplayerGameModeBase>();
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x0009A56F File Offset: 0x0009876F
		public bool AreAgentsSpawning()
		{
			return this.SpawningBehavior.AreAgentsSpawning();
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x0009A57C File Offset: 0x0009877C
		public void SetNewSpawnFrameBehavior(SpawnFrameBehaviorBase spawnFrameBehavior)
		{
			this.SpawnFrameBehavior = spawnFrameBehavior;
			if (this.SpawnFrameBehavior != null)
			{
				this.SpawnFrameBehavior.Initialize();
			}
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x0009A598 File Offset: 0x00098798
		public void SetNewSpawningBehavior(SpawningBehaviorBase spawningBehavior)
		{
			this.SpawningBehavior = spawningBehavior;
			if (this.SpawningBehavior != null)
			{
				this.SpawningBehavior.Initialize(this);
			}
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x0009A5B5 File Offset: 0x000987B5
		protected override void OnEndMission()
		{
			base.OnEndMission();
			this.SpawningBehavior.Clear();
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x0009A5C8 File Offset: 0x000987C8
		public static void SetSiegeSpawningBehavior()
		{
			Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawnFrameBehavior(new SiegeSpawnFrameBehavior());
			Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawningBehavior(new SiegeSpawningBehavior());
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x0009A5F2 File Offset: 0x000987F2
		public static void SetFlagDominationSpawningBehavior()
		{
			Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawnFrameBehavior(new FlagDominationSpawnFrameBehavior());
			Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawningBehavior(new FlagDominationSpawningBehavior());
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x0009A61C File Offset: 0x0009881C
		public static void SetWarmupSpawningBehavior()
		{
			Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawnFrameBehavior(new FFASpawnFrameBehavior());
			Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawningBehavior(new WarmupSpawningBehavior());
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x0009A646 File Offset: 0x00098846
		public static void SetSpawningBehaviorForCurrentGameType(MultiplayerGameType currentGameType)
		{
			if (currentGameType == MultiplayerGameType.Siege)
			{
				SpawnComponent.SetSiegeSpawningBehavior();
				return;
			}
			if (currentGameType - MultiplayerGameType.Battle > 2)
			{
				return;
			}
			SpawnComponent.SetFlagDominationSpawningBehavior();
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x0009A65E File Offset: 0x0009885E
		public override void AfterStart()
		{
			base.AfterStart();
			this.SetNewSpawnFrameBehavior(this.SpawnFrameBehavior);
			this.SetNewSpawningBehavior(this.SpawningBehavior);
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x0009A67E File Offset: 0x0009887E
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			this.SpawningBehavior.OnTick(dt);
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x0009A693 File Offset: 0x00098893
		protected void StartSpawnSession()
		{
			this.SpawningBehavior.RequestStartSpawnSession();
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x0009A6A0 File Offset: 0x000988A0
		public MatrixFrame GetSpawnFrame(Team team, bool hasMount, bool isInitialSpawn = false)
		{
			SpawnFrameBehaviorBase spawnFrameBehavior = this.SpawnFrameBehavior;
			if (spawnFrameBehavior == null)
			{
				return MatrixFrame.Identity;
			}
			return spawnFrameBehavior.GetSpawnFrame(team, hasMount, isInitialSpawn);
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x0009A6BA File Offset: 0x000988BA
		protected void SpawnEquipmentUpdated(MissionPeer lobbyPeer, Equipment equipment)
		{
			if (GameNetwork.IsServer && lobbyPeer != null && this.SpawningBehavior.CanUpdateSpawnEquipment(lobbyPeer) && lobbyPeer.HasSpawnedAgentVisuals)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new EquipEquipmentToPeer(lobbyPeer.GetNetworkPeer(), equipment));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			}
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x0009A6F9 File Offset: 0x000988F9
		public void SetEarlyAgentVisualsDespawning(MissionPeer missionPeer, bool canDespawnEarly = true)
		{
			if (missionPeer != null && this.AllowEarlyAgentVisualsDespawning(missionPeer))
			{
				missionPeer.EquipmentUpdatingExpired = canDespawnEarly;
			}
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x0009A70E File Offset: 0x0009890E
		public void ToggleUpdatingSpawnEquipment(bool canUpdate)
		{
			this.SpawningBehavior.ToggleUpdatingSpawnEquipment(canUpdate);
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x0009A71C File Offset: 0x0009891C
		public bool AllowEarlyAgentVisualsDespawning(MissionPeer lobbyPeer)
		{
			MultiplayerClassDivisions.MPHeroClass mpheroClassForPeer = MultiplayerClassDivisions.GetMPHeroClassForPeer(lobbyPeer, false);
			return this._missionMultiplayerGameModeBase.IsClassAvailable(mpheroClassForPeer) && this.SpawningBehavior.AllowEarlyAgentVisualsDespawning(lobbyPeer);
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x0009A74D File Offset: 0x0009894D
		public int GetMaximumReSpawnPeriodForPeer(MissionPeer lobbyPeer)
		{
			return this.SpawningBehavior.GetMaximumReSpawnPeriodForPeer(lobbyPeer);
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x0009A75B File Offset: 0x0009895B
		public override void OnClearScene()
		{
			base.OnClearScene();
			this.SpawningBehavior.OnClearScene();
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x0009A76E File Offset: 0x0009896E
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			this.SpawningBehavior.OnAgentRemoved(affectedAgent, affectorAgent, agentState, blow);
			this.SpawnFrameBehavior.OnAgentRemoved(affectedAgent, affectorAgent, agentState, blow);
		}

		// Token: 0x04000F9D RID: 3997
		private MissionMultiplayerGameModeBase _missionMultiplayerGameModeBase;
	}
}
