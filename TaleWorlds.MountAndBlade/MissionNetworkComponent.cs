using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Missions.Multiplayer;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002AC RID: 684
	public sealed class MissionNetworkComponent : MissionNetwork
	{
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x060025C0 RID: 9664 RVA: 0x0008926C File Offset: 0x0008746C
		// (remove) Token: 0x060025C1 RID: 9665 RVA: 0x000892A4 File Offset: 0x000874A4
		public event Action OnMyClientSynchronized;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x060025C2 RID: 9666 RVA: 0x000892DC File Offset: 0x000874DC
		// (remove) Token: 0x060025C3 RID: 9667 RVA: 0x00089314 File Offset: 0x00087514
		public event Action<NetworkCommunicator> OnClientSynchronizedEvent;

		// Token: 0x060025C4 RID: 9668 RVA: 0x0008934C File Offset: 0x0008754C
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			if (GameNetwork.IsClientOrReplay)
			{
				registerer.RegisterBaseHandler<CreateFreeMountAgent>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventCreateFreeMountAgentEvent));
				registerer.RegisterBaseHandler<CreateAgent>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventCreateAgent));
				registerer.RegisterBaseHandler<SynchronizeAgentSpawnEquipment>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSynchronizeAgentEquipment));
				registerer.RegisterBaseHandler<CreateAgentVisuals>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventCreateAgentVisuals));
				registerer.RegisterBaseHandler<RemoveAgentVisualsForPeer>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventRemoveAgentVisualsForPeer));
				registerer.RegisterBaseHandler<RemoveAgentVisualsFromIndexForPeer>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventRemoveAgentVisualsFromIndexForPeer));
				registerer.RegisterBaseHandler<ReplaceBotWithPlayer>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventReplaceBotWithPlayer));
				registerer.RegisterBaseHandler<SetWieldedItemIndex>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetWieldedItemIndex));
				registerer.RegisterBaseHandler<SetWeaponNetworkData>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetWeaponNetworkData));
				registerer.RegisterBaseHandler<SetWeaponAmmoData>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetWeaponAmmoData));
				registerer.RegisterBaseHandler<SetWeaponReloadPhase>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetWeaponReloadPhase));
				registerer.RegisterBaseHandler<WeaponUsageIndexChangeMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventWeaponUsageIndexChangeMessage));
				registerer.RegisterBaseHandler<StartSwitchingWeaponUsageIndex>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventStartSwitchingWeaponUsageIndex));
				registerer.RegisterBaseHandler<InitializeFormation>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventInitializeFormation));
				registerer.RegisterBaseHandler<SetSpawnedFormationCount>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetSpawnedFormationCount));
				registerer.RegisterBaseHandler<AddTeam>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAddTeam));
				registerer.RegisterBaseHandler<TeamSetIsEnemyOf>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventTeamSetIsEnemyOf));
				registerer.RegisterBaseHandler<AssignFormationToPlayer>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAssignFormationToPlayer));
				registerer.RegisterBaseHandler<ExistingObjectsBegin>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventExistingObjectsBegin));
				registerer.RegisterBaseHandler<ExistingObjectsEnd>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventExistingObjectsEnd));
				registerer.RegisterBaseHandler<ClearMission>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventClearMission));
				registerer.RegisterBaseHandler<CreateMissionObject>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventCreateMissionObject));
				registerer.RegisterBaseHandler<RemoveMissionObject>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventRemoveMissionObject));
				registerer.RegisterBaseHandler<StopPhysicsAndSetFrameOfMissionObject>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventStopPhysicsAndSetFrameOfMissionObject));
				registerer.RegisterBaseHandler<BurstMissionObjectParticles>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventBurstMissionObjectParticles));
				registerer.RegisterBaseHandler<SetMissionObjectVisibility>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectVisibility));
				registerer.RegisterBaseHandler<SetMissionObjectDisabled>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectDisabled));
				registerer.RegisterBaseHandler<SetMissionObjectColors>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectColors));
				registerer.RegisterBaseHandler<SetMissionObjectFrame>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectFrame));
				registerer.RegisterBaseHandler<SetMissionObjectGlobalFrame>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectGlobalFrame));
				registerer.RegisterBaseHandler<SetMissionObjectFrameOverTime>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectFrameOverTime));
				registerer.RegisterBaseHandler<SetMissionObjectGlobalFrameOverTime>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectGlobalFrameOverTime));
				registerer.RegisterBaseHandler<SetMissionObjectAnimationAtChannel>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectAnimationAtChannel));
				registerer.RegisterBaseHandler<SetMissionObjectAnimationChannelParameter>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectAnimationChannelParameter));
				registerer.RegisterBaseHandler<SetMissionObjectAnimationPaused>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectAnimationPaused));
				registerer.RegisterBaseHandler<SetMissionObjectVertexAnimation>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectVertexAnimation));
				registerer.RegisterBaseHandler<SetMissionObjectVertexAnimationProgress>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectVertexAnimationProgress));
				registerer.RegisterBaseHandler<SetMissionObjectImpulse>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMissionObjectImpulse));
				registerer.RegisterBaseHandler<AddMissionObjectBodyFlags>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAddMissionObjectBodyFlags));
				registerer.RegisterBaseHandler<RemoveMissionObjectBodyFlags>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventRemoveMissionObjectBodyFlags));
				registerer.RegisterBaseHandler<SetMachineTargetRotation>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetMachineTargetRotation));
				registerer.RegisterBaseHandler<SetUsableMissionObjectIsDeactivated>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetUsableGameObjectIsDeactivated));
				registerer.RegisterBaseHandler<SetUsableMissionObjectIsDisabledForPlayers>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetUsableGameObjectIsDisabledForPlayers));
				registerer.RegisterBaseHandler<SetRangedSiegeWeaponState>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetRangedSiegeWeaponState));
				registerer.RegisterBaseHandler<SetRangedSiegeWeaponAmmo>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetRangedSiegeWeaponAmmo));
				registerer.RegisterBaseHandler<RangedSiegeWeaponChangeProjectile>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventRangedSiegeWeaponChangeProjectile));
				registerer.RegisterBaseHandler<SetStonePileAmmo>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetStonePileAmmo));
				registerer.RegisterBaseHandler<SetSiegeMachineMovementDistance>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetSiegeMachineMovementDistance));
				registerer.RegisterBaseHandler<SetSiegeLadderState>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetSiegeLadderState));
				registerer.RegisterBaseHandler<SetAgentTargetPositionAndDirection>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetAgentTargetPositionAndDirection));
				registerer.RegisterBaseHandler<SetAgentTargetPosition>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetAgentTargetPosition));
				registerer.RegisterBaseHandler<ClearAgentTargetFrame>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventClearAgentTargetFrame));
				registerer.RegisterBaseHandler<AgentTeleportToFrame>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAgentTeleportToFrame));
				registerer.RegisterBaseHandler<SetSiegeTowerGateState>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetSiegeTowerGateState));
				registerer.RegisterBaseHandler<SetSiegeTowerHasArrivedAtTarget>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetSiegeTowerHasArrivedAtTarget));
				registerer.RegisterBaseHandler<SetBatteringRamHasArrivedAtTarget>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetBatteringRamHasArrivedAtTarget));
				registerer.RegisterBaseHandler<SetPeerTeam>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetPeerTeam));
				registerer.RegisterBaseHandler<SynchronizeMissionTimeTracker>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSyncMissionTimer));
				registerer.RegisterBaseHandler<SetAgentPeer>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetAgentPeer));
				registerer.RegisterBaseHandler<SetAgentIsPlayer>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetAgentIsPlayer));
				registerer.RegisterBaseHandler<SetAgentHealth>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetAgentHealth));
				registerer.RegisterBaseHandler<AgentSetTeam>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAgentSetTeam));
				registerer.RegisterBaseHandler<SetAgentActionSet>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetAgentActionSet));
				registerer.RegisterBaseHandler<MakeAgentDead>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventMakeAgentDead));
				registerer.RegisterBaseHandler<AgentSetFormation>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAgentSetFormation));
				registerer.RegisterBaseHandler<AddPrefabComponentToAgentBone>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAddPrefabComponentToAgentBone));
				registerer.RegisterBaseHandler<SetAgentPrefabComponentVisibility>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetAgentPrefabComponentVisibility));
				registerer.RegisterBaseHandler<UseObject>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventUseObject));
				registerer.RegisterBaseHandler<StopUsingObject>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventStopUsingObject));
				registerer.RegisterBaseHandler<SyncObjectHitpoints>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventHitSynchronizeObjectHitpoints));
				registerer.RegisterBaseHandler<SyncObjectDestructionLevel>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventHitSynchronizeObjectDestructionLevel));
				registerer.RegisterBaseHandler<BurstAllHeavyHitParticles>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventHitBurstAllHeavyHitParticles));
				registerer.RegisterBaseHandler<SynchronizeMissionObject>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSynchronizeMissionObject));
				registerer.RegisterBaseHandler<SpawnWeaponWithNewEntity>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSpawnWeaponWithNewEntity));
				registerer.RegisterBaseHandler<AttachWeaponToSpawnedWeapon>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAttachWeaponToSpawnedWeapon));
				registerer.RegisterBaseHandler<AttachWeaponToAgent>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAttachWeaponToAgent));
				registerer.RegisterBaseHandler<SpawnWeaponAsDropFromAgent>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSpawnWeaponAsDropFromAgent));
				registerer.RegisterBaseHandler<SpawnAttachedWeaponOnSpawnedWeapon>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSpawnAttachedWeaponOnSpawnedWeapon));
				registerer.RegisterBaseHandler<SpawnAttachedWeaponOnCorpse>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSpawnAttachedWeaponOnCorpse));
				registerer.RegisterBaseHandler<HandleMissileCollisionReaction>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventHandleMissileCollisionReaction));
				registerer.RegisterBaseHandler<RemoveEquippedWeapon>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventRemoveEquippedWeapon));
				registerer.RegisterBaseHandler<BarkAgent>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventBarkAgent));
				registerer.RegisterBaseHandler<EquipWeaponWithNewEntity>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventEquipWeaponWithNewEntity));
				registerer.RegisterBaseHandler<AttachWeaponToWeaponInAgentEquipmentSlot>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAttachWeaponToWeaponInAgentEquipmentSlot));
				registerer.RegisterBaseHandler<EquipWeaponFromSpawnedItemEntity>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventEquipWeaponFromSpawnedItemEntity));
				registerer.RegisterBaseHandler<CreateMissile>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventCreateMissile));
				registerer.RegisterBaseHandler<CombatLogNetworkMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventAgentHit));
				registerer.RegisterBaseHandler<ConsumeWeaponAmount>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventConsumeWeaponAmount));
				registerer.RegisterBaseHandler<SetAgentOwningMissionPeer>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSetAgentOwningMissionPeer));
				return;
			}
			if (GameNetwork.IsServer)
			{
				registerer.RegisterBaseHandler<SetFollowedAgent>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventSetFollowedAgent));
				registerer.RegisterBaseHandler<SetMachineRotation>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventSetMachineRotation));
				registerer.RegisterBaseHandler<RequestUseObject>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventRequestUseObject));
				registerer.RegisterBaseHandler<RequestStopUsingObject>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventRequestStopUsingObject));
				registerer.RegisterBaseHandler<ApplyOrder>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplyOrder));
				registerer.RegisterBaseHandler<ApplySiegeWeaponOrder>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplySiegeWeaponOrder));
				registerer.RegisterBaseHandler<ApplyOrderWithPosition>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplyOrderWithPosition));
				registerer.RegisterBaseHandler<ApplyOrderWithFormation>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplyOrderWithFormation));
				registerer.RegisterBaseHandler<ApplyOrderWithFormationAndPercentage>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplyOrderWithFormationAndPercentage));
				registerer.RegisterBaseHandler<ApplyOrderWithFormationAndNumber>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplyOrderWithFormationAndNumber));
				registerer.RegisterBaseHandler<ApplyOrderWithTwoPositions>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplyOrderWithTwoPositions));
				registerer.RegisterBaseHandler<ApplyOrderWithMissionObject>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplyOrderWithGameEntity));
				registerer.RegisterBaseHandler<ApplyOrderWithAgent>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventApplyOrderWithAgent));
				registerer.RegisterBaseHandler<SelectAllFormations>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventSelectAllFormations));
				registerer.RegisterBaseHandler<SelectAllSiegeWeapons>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventSelectAllSiegeWeapons));
				registerer.RegisterBaseHandler<ClearSelectedFormations>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventClearSelectedFormations));
				registerer.RegisterBaseHandler<SelectFormation>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventSelectFormation));
				registerer.RegisterBaseHandler<SelectSiegeWeapon>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventSelectSiegeWeapon));
				registerer.RegisterBaseHandler<UnselectFormation>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventUnselectFormation));
				registerer.RegisterBaseHandler<UnselectSiegeWeapon>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventUnselectSiegeWeapon));
				registerer.RegisterBaseHandler<DropWeapon>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventDropWeapon));
				registerer.RegisterBaseHandler<TauntSelected>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventCheerSelected));
				registerer.RegisterBaseHandler<BarkSelected>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventBarkSelected));
				registerer.RegisterBaseHandler<AgentVisualsBreakInvulnerability>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventBreakAgentVisualsInvulnerability));
				registerer.RegisterBaseHandler<RequestToSpawnAsBot>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventRequestToSpawnAsBot));
			}
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x00089B74 File Offset: 0x00087D74
		private Team GetTeamOfPeer(NetworkCommunicator networkPeer)
		{
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			if (component.ControlledAgent == null)
			{
				MBDebug.Print("peer.ControlledAgent == null", 0, Debug.DebugColor.White, 17592186044416UL);
				return null;
			}
			Team team = component.ControlledAgent.Team;
			if (team == null)
			{
				MBDebug.Print("peersTeam == null", 0, Debug.DebugColor.White, 17592186044416UL);
			}
			return team;
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x00089BCC File Offset: 0x00087DCC
		private OrderController GetOrderControllerOfPeer(NetworkCommunicator networkPeer)
		{
			Team teamOfPeer = this.GetTeamOfPeer(networkPeer);
			if (teamOfPeer != null)
			{
				return teamOfPeer.GetOrderControllerOf(networkPeer.ControlledAgent);
			}
			MBDebug.Print("peersTeam == null", 0, Debug.DebugColor.White, 17592186044416UL);
			return null;
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x00089C08 File Offset: 0x00087E08
		private void HandleServerEventSyncMissionTimer(GameNetworkMessage baseMessage)
		{
			SynchronizeMissionTimeTracker synchronizeMissionTimeTracker = (SynchronizeMissionTimeTracker)baseMessage;
			base.Mission.MissionTimeTracker.UpdateSync(synchronizeMissionTimeTracker.CurrentTime);
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x00089C34 File Offset: 0x00087E34
		private void HandleServerEventSetPeerTeam(GameNetworkMessage baseMessage)
		{
			SetPeerTeam setPeerTeam = (SetPeerTeam)baseMessage;
			MissionPeer component = setPeerTeam.Peer.GetComponent<MissionPeer>();
			component.Team = Mission.MissionNetworkHelper.GetTeamFromTeamIndex(setPeerTeam.TeamIndex);
			if (setPeerTeam.Peer.IsMine)
			{
				base.Mission.PlayerTeam = component.Team;
			}
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x00089C84 File Offset: 0x00087E84
		private void HandleServerEventCreateFreeMountAgentEvent(GameNetworkMessage baseMessage)
		{
			CreateFreeMountAgent createFreeMountAgent = (CreateFreeMountAgent)baseMessage;
			Mission mission = base.Mission;
			EquipmentElement horseItem = createFreeMountAgent.HorseItem;
			EquipmentElement horseHarnessItem = createFreeMountAgent.HorseHarnessItem;
			Vec3 position = createFreeMountAgent.Position;
			Vec2 vec = createFreeMountAgent.Direction;
			vec = vec.Normalized();
			mission.SpawnMonster(horseItem, horseHarnessItem, in position, in vec, createFreeMountAgent.AgentIndex);
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x00089CD0 File Offset: 0x00087ED0
		private void HandleServerEventCreateAgent(GameNetworkMessage baseMessage)
		{
			CreateAgent createAgent = (CreateAgent)baseMessage;
			BasicCharacterObject character = createAgent.Character;
			NetworkCommunicator peer = createAgent.Peer;
			MissionPeer missionPeer = ((peer != null) ? peer.GetComponent<MissionPeer>() : null);
			Team teamFromTeamIndex = Mission.MissionNetworkHelper.GetTeamFromTeamIndex(createAgent.TeamIndex);
			AgentBuildData agentBuildData = new AgentBuildData(character).MissionPeer(createAgent.IsPlayerAgent ? missionPeer : null).Monster(createAgent.Monster).TroopOrigin(new BasicBattleAgentOrigin(character))
				.Equipment(createAgent.SpawnEquipment)
				.EquipmentSeed(createAgent.BodyPropertiesSeed);
			Vec3 position = createAgent.Position;
			AgentBuildData agentBuildData2 = agentBuildData.InitialPosition(in position);
			Vec2 vec = createAgent.Direction;
			vec = vec.Normalized();
			AgentBuildData agentBuildData3 = agentBuildData2.InitialDirection(in vec).MissionEquipment(createAgent.MissionEquipment).Team(teamFromTeamIndex)
				.Index(createAgent.AgentIndex)
				.MountIndex(createAgent.MountAgentIndex)
				.IsFemale(createAgent.IsFemale)
				.ClothingColor1(createAgent.ClothingColor1)
				.ClothingColor2(createAgent.ClothingColor2);
			Formation formation = null;
			if (teamFromTeamIndex != null && createAgent.FormationIndex >= 0 && !GameNetwork.IsReplay)
			{
				formation = teamFromTeamIndex.GetFormation((FormationClass)createAgent.FormationIndex);
				agentBuildData3.Formation(formation);
			}
			if (createAgent.IsPlayerAgent)
			{
				agentBuildData3.BodyProperties(missionPeer.Peer.BodyProperties);
				agentBuildData3.Age((int)agentBuildData3.AgentBodyProperties.Age);
			}
			else
			{
				agentBuildData3.BodyProperties(BodyProperties.GetRandomBodyProperties(agentBuildData3.AgentRace, agentBuildData3.AgentIsFemale, character.GetBodyPropertiesMin(false), character.GetBodyPropertiesMax(false), (int)agentBuildData3.AgentOverridenSpawnEquipment.HairCoverType, agentBuildData3.AgentEquipmentSeed, character.BodyPropertyRange.HairTags, character.BodyPropertyRange.BeardTags, character.BodyPropertyRange.TattooTags, 0f));
			}
			Banner banner = null;
			if (formation != null)
			{
				if (!string.IsNullOrEmpty(formation.BannerCode))
				{
					if (formation.Banner == null)
					{
						banner = new Banner(formation.BannerCode, teamFromTeamIndex.Color, teamFromTeamIndex.Color2);
						formation.Banner = banner;
					}
					else
					{
						banner = formation.Banner;
					}
				}
			}
			else if (missionPeer != null)
			{
				banner = new Banner(missionPeer.Peer.BannerCode, teamFromTeamIndex.Color, teamFromTeamIndex.Color2);
			}
			agentBuildData3.Banner(banner);
			Agent mountAgent = base.Mission.SpawnAgent(agentBuildData3, false).MountAgent;
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x00089F14 File Offset: 0x00088114
		private void HandleServerEventSynchronizeAgentEquipment(GameNetworkMessage baseMessage)
		{
			SynchronizeAgentSpawnEquipment synchronizeAgentSpawnEquipment = (SynchronizeAgentSpawnEquipment)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(synchronizeAgentSpawnEquipment.AgentIndex, false).UpdateSpawnEquipmentAndRefreshVisuals(synchronizeAgentSpawnEquipment.SpawnEquipment);
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x00089F40 File Offset: 0x00088140
		private void HandleServerEventCreateAgentVisuals(GameNetworkMessage baseMessage)
		{
			CreateAgentVisuals createAgentVisuals = (CreateAgentVisuals)baseMessage;
			MissionPeer component = createAgentVisuals.Peer.GetComponent<MissionPeer>();
			BattleSideEnum side = component.Team.Side;
			BasicCharacterObject character = createAgentVisuals.Character;
			BasicCultureObject culture = character.Culture;
			BasicCultureObject @object = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			BasicCultureObject object2 = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam2.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			MultiplayerBattleColors.MultiplayerCultureColorInfo peerColors = MultiplayerBattleColors.CreateWith(@object, object2).GetPeerColors(component);
			AgentBuildData agentBuildData = new AgentBuildData(character).VisualsIndex(createAgentVisuals.VisualsIndex).Equipment(createAgentVisuals.Equipment).EquipmentSeed(createAgentVisuals.BodyPropertiesSeed)
				.IsFemale(createAgentVisuals.IsFemale)
				.ClothingColor1(peerColors.ClothingColor1Uint)
				.ClothingColor2(peerColors.ClothingColor2Uint);
			if (createAgentVisuals.VisualsIndex == 0)
			{
				agentBuildData.BodyProperties(component.Peer.BodyProperties);
			}
			else
			{
				agentBuildData.BodyProperties(BodyProperties.GetRandomBodyProperties(agentBuildData.AgentRace, agentBuildData.AgentIsFemale, character.GetBodyPropertiesMin(false), character.GetBodyPropertiesMax(false), (int)agentBuildData.AgentOverridenSpawnEquipment.HairCoverType, createAgentVisuals.BodyPropertiesSeed, character.BodyPropertyRange.HairTags, character.BodyPropertyRange.BeardTags, character.BodyPropertyRange.TattooTags, 0f));
			}
			base.Mission.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>().SpawnAgentVisualsForPeer(component, agentBuildData, createAgentVisuals.SelectedEquipmentSetIndex, false, createAgentVisuals.TroopCountInFormation);
			if (agentBuildData.AgentVisualsIndex == 0)
			{
				component.HasSpawnedAgentVisuals = true;
				component.EquipmentUpdatingExpired = false;
			}
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x0008A0B4 File Offset: 0x000882B4
		private void HandleServerEventRemoveAgentVisualsForPeer(GameNetworkMessage baseMessage)
		{
			MissionPeer component = ((RemoveAgentVisualsForPeer)baseMessage).Peer.GetComponent<MissionPeer>();
			base.Mission.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>().RemoveAgentVisuals(component, false);
			component.HasSpawnedAgentVisuals = false;
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x0008A0EB File Offset: 0x000882EB
		private void HandleServerEventRemoveAgentVisualsFromIndexForPeer(GameNetworkMessage baseMessage)
		{
			((RemoveAgentVisualsFromIndexForPeer)baseMessage).Peer.GetComponent<MissionPeer>();
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x0008A100 File Offset: 0x00088300
		private void HandleServerEventReplaceBotWithPlayer(GameNetworkMessage baseMessage)
		{
			ReplaceBotWithPlayer replaceBotWithPlayer = (ReplaceBotWithPlayer)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(replaceBotWithPlayer.BotAgentIndex, false);
			if (agentFromIndex.Formation != null)
			{
				agentFromIndex.Formation.PlayerOwner = agentFromIndex;
			}
			MissionPeer component = replaceBotWithPlayer.Peer.GetComponent<MissionPeer>();
			agentFromIndex.MissionPeer = replaceBotWithPlayer.Peer.GetComponent<MissionPeer>();
			agentFromIndex.Formation = component.ControlledFormation;
			agentFromIndex.Health = (float)replaceBotWithPlayer.Health;
			if (agentFromIndex.MountAgent != null)
			{
				agentFromIndex.MountAgent.Health = (float)replaceBotWithPlayer.MountHealth;
			}
			if (agentFromIndex.Formation != null)
			{
				agentFromIndex.Team.AssignPlayerAsSergeantOfFormation(component, component.ControlledFormation.FormationIndex);
			}
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x0008A1A4 File Offset: 0x000883A4
		private void HandleServerEventSetWieldedItemIndex(GameNetworkMessage baseMessage)
		{
			SetWieldedItemIndex setWieldedItemIndex = (SetWieldedItemIndex)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(setWieldedItemIndex.AgentIndex, false);
			if (agentFromIndex != null)
			{
				agentFromIndex.SetWieldedItemIndexAsClient(setWieldedItemIndex.IsLeftHand ? Agent.HandIndex.OffHand : Agent.HandIndex.MainHand, setWieldedItemIndex.WieldedItemIndex, setWieldedItemIndex.IsWieldedInstantly, setWieldedItemIndex.IsWieldedOnSpawn, setWieldedItemIndex.MainHandCurrentUsageIndex);
				agentFromIndex.UpdateAgentStats();
			}
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x0008A1F8 File Offset: 0x000883F8
		private void HandleServerEventSetWeaponNetworkData(GameNetworkMessage baseMessage)
		{
			SetWeaponNetworkData setWeaponNetworkData = (SetWeaponNetworkData)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(setWeaponNetworkData.AgentIndex, false);
			ItemObject item = agentFromIndex.Equipment[setWeaponNetworkData.WeaponEquipmentIndex].Item;
			WeaponComponentData weaponComponentData = ((item != null) ? item.PrimaryWeapon : null);
			if (weaponComponentData != null)
			{
				if (weaponComponentData.WeaponFlags.HasAnyFlag(WeaponFlags.HasHitPoints))
				{
					agentFromIndex.ChangeWeaponHitPoints(setWeaponNetworkData.WeaponEquipmentIndex, setWeaponNetworkData.DataValue);
					return;
				}
				if (weaponComponentData.IsConsumable)
				{
					agentFromIndex.SetWeaponAmountInSlot(setWeaponNetworkData.WeaponEquipmentIndex, setWeaponNetworkData.DataValue, true);
				}
			}
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x0008A284 File Offset: 0x00088484
		private void HandleServerEventSetWeaponAmmoData(GameNetworkMessage baseMessage)
		{
			SetWeaponAmmoData setWeaponAmmoData = (SetWeaponAmmoData)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(setWeaponAmmoData.AgentIndex, false);
			if (agentFromIndex.Equipment[setWeaponAmmoData.WeaponEquipmentIndex].CurrentUsageItem.IsRangedWeapon)
			{
				agentFromIndex.SetWeaponAmmoAsClient(setWeaponAmmoData.WeaponEquipmentIndex, setWeaponAmmoData.AmmoEquipmentIndex, setWeaponAmmoData.Ammo);
				return;
			}
			Debug.FailedAssert("Invalid item type.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\MissionNetworkLogics\\MissionNetworkComponent.cs", "HandleServerEventSetWeaponAmmoData", 468);
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x0008A2F8 File Offset: 0x000884F8
		private void HandleServerEventSetWeaponReloadPhase(GameNetworkMessage baseMessage)
		{
			SetWeaponReloadPhase setWeaponReloadPhase = (SetWeaponReloadPhase)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(setWeaponReloadPhase.AgentIndex, false).SetWeaponReloadPhaseAsClient(setWeaponReloadPhase.EquipmentIndex, setWeaponReloadPhase.ReloadPhase);
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x0008A32C File Offset: 0x0008852C
		private void HandleServerEventWeaponUsageIndexChangeMessage(GameNetworkMessage baseMessage)
		{
			WeaponUsageIndexChangeMessage weaponUsageIndexChangeMessage = (WeaponUsageIndexChangeMessage)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(weaponUsageIndexChangeMessage.AgentIndex, false).SetUsageIndexOfWeaponInSlotAsClient(weaponUsageIndexChangeMessage.SlotIndex, weaponUsageIndexChangeMessage.UsageIndex);
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x0008A360 File Offset: 0x00088560
		private void HandleServerEventStartSwitchingWeaponUsageIndex(GameNetworkMessage baseMessage)
		{
			StartSwitchingWeaponUsageIndex startSwitchingWeaponUsageIndex = (StartSwitchingWeaponUsageIndex)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(startSwitchingWeaponUsageIndex.AgentIndex, false).StartSwitchingWeaponUsageIndexAsClient(startSwitchingWeaponUsageIndex.EquipmentIndex, startSwitchingWeaponUsageIndex.UsageIndex, startSwitchingWeaponUsageIndex.CurrentMovementFlagUsageDirection);
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x0008A398 File Offset: 0x00088598
		private void HandleServerEventInitializeFormation(GameNetworkMessage baseMessage)
		{
			InitializeFormation initializeFormation = (InitializeFormation)baseMessage;
			Mission.MissionNetworkHelper.GetTeamFromTeamIndex(initializeFormation.TeamIndex).GetFormation((FormationClass)initializeFormation.FormationIndex).BannerCode = initializeFormation.BannerCode;
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x0008A3D0 File Offset: 0x000885D0
		private void HandleServerEventSetSpawnedFormationCount(GameNetworkMessage baseMessage)
		{
			SetSpawnedFormationCount setSpawnedFormationCount = (SetSpawnedFormationCount)baseMessage;
			base.Mission.NumOfFormationsSpawnedTeamOne = setSpawnedFormationCount.NumOfFormationsTeamOne;
			base.Mission.NumOfFormationsSpawnedTeamTwo = setSpawnedFormationCount.NumOfFormationsTeamTwo;
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x0008A408 File Offset: 0x00088608
		private void HandleServerEventAddTeam(GameNetworkMessage baseMessage)
		{
			AddTeam addTeam = (AddTeam)baseMessage;
			Banner banner = (string.IsNullOrEmpty(addTeam.BannerCode) ? null : new Banner(addTeam.BannerCode, addTeam.Color, addTeam.Color2));
			base.Mission.Teams.Add(addTeam.Side, addTeam.Color, addTeam.Color2, banner, addTeam.IsPlayerGeneral, addTeam.IsPlayerSergeant, true);
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x0008A478 File Offset: 0x00088678
		private void HandleServerEventTeamSetIsEnemyOf(GameNetworkMessage baseMessage)
		{
			TeamSetIsEnemyOf teamSetIsEnemyOf = (TeamSetIsEnemyOf)baseMessage;
			Team teamFromTeamIndex = Mission.MissionNetworkHelper.GetTeamFromTeamIndex(teamSetIsEnemyOf.Team1Index);
			Team teamFromTeamIndex2 = Mission.MissionNetworkHelper.GetTeamFromTeamIndex(teamSetIsEnemyOf.Team2Index);
			teamFromTeamIndex.SetIsEnemyOf(teamFromTeamIndex2, teamSetIsEnemyOf.IsEnemyOf);
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x0008A4B0 File Offset: 0x000886B0
		private void HandleServerEventAssignFormationToPlayer(GameNetworkMessage baseMessage)
		{
			AssignFormationToPlayer assignFormationToPlayer = (AssignFormationToPlayer)baseMessage;
			MissionPeer component = assignFormationToPlayer.Peer.GetComponent<MissionPeer>();
			component.Team.AssignPlayerAsSergeantOfFormation(component, assignFormationToPlayer.FormationClass);
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x0008A4E2 File Offset: 0x000886E2
		private void HandleServerEventExistingObjectsBegin(GameNetworkMessage baseMessage)
		{
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x0008A4E4 File Offset: 0x000886E4
		private void HandleServerEventExistingObjectsEnd(GameNetworkMessage baseMessage)
		{
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x0008A4E6 File Offset: 0x000886E6
		private void HandleServerEventClearMission(GameNetworkMessage baseMessage)
		{
			base.Mission.ResetMission();
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x0008A4F4 File Offset: 0x000886F4
		private void HandleServerEventCreateMissionObject(GameNetworkMessage baseMessage)
		{
			CreateMissionObject createMissionObject = (CreateMissionObject)baseMessage;
			GameEntity gameEntity = GameEntity.Instantiate(base.Mission.Scene, createMissionObject.Prefab, createMissionObject.Frame, true);
			MissionObject firstScriptOfType = gameEntity.GetFirstScriptOfType<MissionObject>();
			if (firstScriptOfType != null)
			{
				firstScriptOfType.Id = createMissionObject.ObjectId;
				int num = 0;
				using (IEnumerator<GameEntity> enumerator = gameEntity.GetChildren().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MissionObject firstScriptOfType2;
						if ((firstScriptOfType2 = enumerator.Current.GetFirstScriptOfType<MissionObject>()) != null)
						{
							firstScriptOfType2.Id = createMissionObject.ChildObjectIds[num++];
						}
					}
				}
			}
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x0008A5A0 File Offset: 0x000887A0
		private void HandleServerEventRemoveMissionObject(GameNetworkMessage baseMessage)
		{
			RemoveMissionObject message = (RemoveMissionObject)baseMessage;
			MissionObject missionObject = base.Mission.MissionObjects.FirstOrDefault<MissionObject>((MissionObject mo) => mo.Id == message.ObjectId);
			if (missionObject == null)
			{
				return;
			}
			missionObject.GameEntity.Remove(82);
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x0008A5F0 File Offset: 0x000887F0
		private void HandleServerEventStopPhysicsAndSetFrameOfMissionObject(GameNetworkMessage baseMessage)
		{
			StopPhysicsAndSetFrameOfMissionObject message = (StopPhysicsAndSetFrameOfMissionObject)baseMessage;
			SpawnedItemEntity spawnedItemEntity = (SpawnedItemEntity)base.Mission.MissionObjects.FirstOrDefault<MissionObject>((MissionObject mo) => mo.Id == message.ObjectId);
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(message.ParentId);
			if (spawnedItemEntity == null)
			{
				return;
			}
			spawnedItemEntity.StopPhysicsAndSetFrameForClient(message.Frame, GameEntity.CreateFromWeakEntity((missionObjectFromMissionObjectId != null) ? missionObjectFromMissionObjectId.GameEntity : WeakGameEntity.Invalid));
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x0008A66C File Offset: 0x0008886C
		private void HandleServerEventBurstMissionObjectParticles(GameNetworkMessage baseMessage)
		{
			BurstMissionObjectParticles burstMissionObjectParticles = (BurstMissionObjectParticles)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(burstMissionObjectParticles.MissionObjectId) as SynchedMissionObject).BurstParticlesSynched(burstMissionObjectParticles.DoChildren);
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x0008A69C File Offset: 0x0008889C
		private void HandleServerEventSetMissionObjectVisibility(GameNetworkMessage baseMessage)
		{
			SetMissionObjectVisibility setMissionObjectVisibility = (SetMissionObjectVisibility)baseMessage;
			Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectVisibility.MissionObjectId).GameEntity.SetVisibilityExcludeParents(setMissionObjectVisibility.Visible);
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x0008A6CE File Offset: 0x000888CE
		private void HandleServerEventSetMissionObjectDisabled(GameNetworkMessage baseMessage)
		{
			Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(((SetMissionObjectDisabled)baseMessage).MissionObjectId).SetDisabledAndMakeInvisible(false, false);
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x0008A6E8 File Offset: 0x000888E8
		private void HandleServerEventSetMissionObjectColors(GameNetworkMessage baseMessage)
		{
			SetMissionObjectColors setMissionObjectColors = (SetMissionObjectColors)baseMessage;
			SynchedMissionObject synchedMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectColors.MissionObjectId) as SynchedMissionObject;
			if (synchedMissionObject != null)
			{
				synchedMissionObject.SetTeamColors(setMissionObjectColors.Color, setMissionObjectColors.Color2);
			}
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x0008A724 File Offset: 0x00088924
		private void HandleServerEventSetMissionObjectFrame(GameNetworkMessage baseMessage)
		{
			SetMissionObjectFrame setMissionObjectFrame = (SetMissionObjectFrame)baseMessage;
			SynchedMissionObject synchedMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectFrame.MissionObjectId) as SynchedMissionObject;
			MatrixFrame frame = setMissionObjectFrame.Frame;
			synchedMissionObject.SetFrameSynched(ref frame, true);
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x0008A758 File Offset: 0x00088958
		private void HandleServerEventSetMissionObjectGlobalFrame(GameNetworkMessage baseMessage)
		{
			SetMissionObjectGlobalFrame setMissionObjectGlobalFrame = (SetMissionObjectGlobalFrame)baseMessage;
			SynchedMissionObject synchedMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectGlobalFrame.MissionObjectId) as SynchedMissionObject;
			MatrixFrame frame = setMissionObjectGlobalFrame.Frame;
			synchedMissionObject.SetGlobalFrameSynched(ref frame, true);
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x0008A78C File Offset: 0x0008898C
		private void HandleServerEventSetMissionObjectFrameOverTime(GameNetworkMessage baseMessage)
		{
			SetMissionObjectFrameOverTime setMissionObjectFrameOverTime = (SetMissionObjectFrameOverTime)baseMessage;
			SynchedMissionObject synchedMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectFrameOverTime.MissionObjectId) as SynchedMissionObject;
			MatrixFrame frame = setMissionObjectFrameOverTime.Frame;
			synchedMissionObject.SetFrameSynchedOverTime(ref frame, setMissionObjectFrameOverTime.Duration, true);
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x0008A7C8 File Offset: 0x000889C8
		private void HandleServerEventSetMissionObjectGlobalFrameOverTime(GameNetworkMessage baseMessage)
		{
			SetMissionObjectGlobalFrameOverTime setMissionObjectGlobalFrameOverTime = (SetMissionObjectGlobalFrameOverTime)baseMessage;
			SynchedMissionObject synchedMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectGlobalFrameOverTime.MissionObjectId) as SynchedMissionObject;
			MatrixFrame frame = setMissionObjectGlobalFrameOverTime.Frame;
			synchedMissionObject.SetGlobalFrameSynchedOverTime(ref frame, setMissionObjectGlobalFrameOverTime.Duration, true);
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x0008A804 File Offset: 0x00088A04
		private void HandleServerEventSetMissionObjectAnimationAtChannel(GameNetworkMessage baseMessage)
		{
			SetMissionObjectAnimationAtChannel setMissionObjectAnimationAtChannel = (SetMissionObjectAnimationAtChannel)baseMessage;
			Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectAnimationAtChannel.MissionObjectId).GameEntity.Skeleton.SetAnimationAtChannel(setMissionObjectAnimationAtChannel.AnimationIndex, setMissionObjectAnimationAtChannel.ChannelNo, setMissionObjectAnimationAtChannel.AnimationSpeed, -1f, 0f);
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x0008A854 File Offset: 0x00088A54
		private void HandleServerEventSetRangedSiegeWeaponAmmo(GameNetworkMessage baseMessage)
		{
			SetRangedSiegeWeaponAmmo setRangedSiegeWeaponAmmo = (SetRangedSiegeWeaponAmmo)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setRangedSiegeWeaponAmmo.RangedSiegeWeaponId) as RangedSiegeWeapon).SetAmmo(setRangedSiegeWeaponAmmo.AmmoCount);
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x0008A884 File Offset: 0x00088A84
		private void HandleServerEventRangedSiegeWeaponChangeProjectile(GameNetworkMessage baseMessage)
		{
			RangedSiegeWeaponChangeProjectile rangedSiegeWeaponChangeProjectile = (RangedSiegeWeaponChangeProjectile)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(rangedSiegeWeaponChangeProjectile.RangedSiegeWeaponId) as RangedSiegeWeapon).ChangeProjectileEntityClient(rangedSiegeWeaponChangeProjectile.Index);
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x0008A8B4 File Offset: 0x00088AB4
		private void HandleServerEventSetStonePileAmmo(GameNetworkMessage baseMessage)
		{
			SetStonePileAmmo setStonePileAmmo = (SetStonePileAmmo)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setStonePileAmmo.StonePileId) as StonePile).SetAmmo(setStonePileAmmo.AmmoCount);
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x0008A8E4 File Offset: 0x00088AE4
		private void HandleServerEventSetRangedSiegeWeaponState(GameNetworkMessage baseMessage)
		{
			SetRangedSiegeWeaponState setRangedSiegeWeaponState = (SetRangedSiegeWeaponState)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setRangedSiegeWeaponState.RangedSiegeWeaponId) as RangedSiegeWeapon).State = setRangedSiegeWeaponState.State;
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x0008A914 File Offset: 0x00088B14
		private void HandleServerEventSetSiegeLadderState(GameNetworkMessage baseMessage)
		{
			SetSiegeLadderState setSiegeLadderState = (SetSiegeLadderState)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setSiegeLadderState.SiegeLadderId) as SiegeLadder).State = setSiegeLadderState.State;
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x0008A944 File Offset: 0x00088B44
		private void HandleServerEventSetSiegeTowerGateState(GameNetworkMessage baseMessage)
		{
			SetSiegeTowerGateState setSiegeTowerGateState = (SetSiegeTowerGateState)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setSiegeTowerGateState.SiegeTowerId) as SiegeTower).State = setSiegeTowerGateState.State;
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x0008A973 File Offset: 0x00088B73
		private void HandleServerEventSetSiegeTowerHasArrivedAtTarget(GameNetworkMessage baseMessage)
		{
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(((SetSiegeTowerHasArrivedAtTarget)baseMessage).SiegeTowerId) as SiegeTower).HasArrivedAtTarget = true;
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x0008A990 File Offset: 0x00088B90
		private void HandleServerEventSetBatteringRamHasArrivedAtTarget(GameNetworkMessage baseMessage)
		{
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(((SetBatteringRamHasArrivedAtTarget)baseMessage).BatteringRamId) as BatteringRam).HasArrivedAtTarget = true;
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x0008A9B0 File Offset: 0x00088BB0
		private void HandleServerEventSetSiegeMachineMovementDistance(GameNetworkMessage baseMessage)
		{
			SetSiegeMachineMovementDistance setSiegeMachineMovementDistance = (SetSiegeMachineMovementDistance)baseMessage;
			UsableMachine usableMachine = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setSiegeMachineMovementDistance.UsableMachineId) as UsableMachine;
			if (usableMachine != null)
			{
				if (usableMachine is SiegeTower)
				{
					((SiegeTower)usableMachine).MovementComponent.SetDistanceTraveledAsClient(setSiegeMachineMovementDistance.Distance);
					return;
				}
				((BatteringRam)usableMachine).MovementComponent.SetDistanceTraveledAsClient(setSiegeMachineMovementDistance.Distance);
			}
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x0008AA10 File Offset: 0x00088C10
		private void HandleServerEventSetMissionObjectAnimationChannelParameter(GameNetworkMessage baseMessage)
		{
			SetMissionObjectAnimationChannelParameter setMissionObjectAnimationChannelParameter = (SetMissionObjectAnimationChannelParameter)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectAnimationChannelParameter.MissionObjectId);
			if (missionObjectFromMissionObjectId != null)
			{
				missionObjectFromMissionObjectId.GameEntity.Skeleton.SetAnimationParameterAtChannel(setMissionObjectAnimationChannelParameter.ChannelNo, setMissionObjectAnimationChannelParameter.Parameter);
			}
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x0008AA54 File Offset: 0x00088C54
		private void HandleServerEventSetMissionObjectVertexAnimation(GameNetworkMessage baseMessage)
		{
			SetMissionObjectVertexAnimation setMissionObjectVertexAnimation = (SetMissionObjectVertexAnimation)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectVertexAnimation.MissionObjectId);
			if (missionObjectFromMissionObjectId != null)
			{
				(missionObjectFromMissionObjectId as VertexAnimator).SetAnimationSynched(setMissionObjectVertexAnimation.BeginKey, setMissionObjectVertexAnimation.EndKey, setMissionObjectVertexAnimation.Speed);
			}
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x0008AA94 File Offset: 0x00088C94
		private void HandleServerEventSetMissionObjectVertexAnimationProgress(GameNetworkMessage baseMessage)
		{
			SetMissionObjectVertexAnimationProgress setMissionObjectVertexAnimationProgress = (SetMissionObjectVertexAnimationProgress)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectVertexAnimationProgress.MissionObjectId);
			if (missionObjectFromMissionObjectId != null)
			{
				(missionObjectFromMissionObjectId as VertexAnimator).SetProgressSynched(setMissionObjectVertexAnimationProgress.Progress);
			}
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x0008AAC8 File Offset: 0x00088CC8
		private void HandleServerEventSetMissionObjectAnimationPaused(GameNetworkMessage baseMessage)
		{
			SetMissionObjectAnimationPaused setMissionObjectAnimationPaused = (SetMissionObjectAnimationPaused)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectAnimationPaused.MissionObjectId);
			if (missionObjectFromMissionObjectId != null)
			{
				if (setMissionObjectAnimationPaused.IsPaused)
				{
					missionObjectFromMissionObjectId.GameEntity.PauseSkeletonAnimation();
					return;
				}
				missionObjectFromMissionObjectId.GameEntity.ResumeSkeletonAnimation();
			}
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x0008AB10 File Offset: 0x00088D10
		private void HandleServerEventAddMissionObjectBodyFlags(GameNetworkMessage baseMessage)
		{
			AddMissionObjectBodyFlags addMissionObjectBodyFlags = (AddMissionObjectBodyFlags)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(addMissionObjectBodyFlags.MissionObjectId);
			if (missionObjectFromMissionObjectId != null)
			{
				missionObjectFromMissionObjectId.GameEntity.AddBodyFlags(addMissionObjectBodyFlags.BodyFlags, addMissionObjectBodyFlags.ApplyToChildren);
			}
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x0008AB50 File Offset: 0x00088D50
		private void HandleServerEventRemoveMissionObjectBodyFlags(GameNetworkMessage baseMessage)
		{
			RemoveMissionObjectBodyFlags removeMissionObjectBodyFlags = (RemoveMissionObjectBodyFlags)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(removeMissionObjectBodyFlags.MissionObjectId);
			if (missionObjectFromMissionObjectId != null)
			{
				missionObjectFromMissionObjectId.GameEntity.RemoveBodyFlags(removeMissionObjectBodyFlags.BodyFlags, removeMissionObjectBodyFlags.ApplyToChildren);
			}
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x0008AB90 File Offset: 0x00088D90
		private void HandleServerEventSetMachineTargetRotation(GameNetworkMessage baseMessage)
		{
			SetMachineTargetRotation setMachineTargetRotation = (SetMachineTargetRotation)baseMessage;
			UsableMachine usableMachine = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMachineTargetRotation.UsableMachineId) as UsableMachine;
			if (usableMachine != null && usableMachine.PilotAgent != null)
			{
				((RangedSiegeWeapon)usableMachine).AimAtRotation(setMachineTargetRotation.HorizontalRotation, setMachineTargetRotation.VerticalRotation);
			}
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x0008ABD8 File Offset: 0x00088DD8
		private void HandleServerEventSetUsableGameObjectIsDeactivated(GameNetworkMessage baseMessage)
		{
			SetUsableMissionObjectIsDeactivated setUsableMissionObjectIsDeactivated = (SetUsableMissionObjectIsDeactivated)baseMessage;
			UsableMissionObject usableMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setUsableMissionObjectIsDeactivated.UsableGameObjectId) as UsableMissionObject;
			if (usableMissionObject != null)
			{
				usableMissionObject.IsDeactivated = setUsableMissionObjectIsDeactivated.IsDeactivated;
			}
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x0008AC0C File Offset: 0x00088E0C
		private void HandleServerEventSetUsableGameObjectIsDisabledForPlayers(GameNetworkMessage baseMessage)
		{
			SetUsableMissionObjectIsDisabledForPlayers setUsableMissionObjectIsDisabledForPlayers = (SetUsableMissionObjectIsDisabledForPlayers)baseMessage;
			UsableMissionObject usableMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setUsableMissionObjectIsDisabledForPlayers.UsableGameObjectId) as UsableMissionObject;
			if (usableMissionObject != null)
			{
				usableMissionObject.IsDisabledForPlayers = setUsableMissionObjectIsDisabledForPlayers.IsDisabledForPlayers;
			}
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x0008AC40 File Offset: 0x00088E40
		private void HandleServerEventSetMissionObjectImpulse(GameNetworkMessage baseMessage)
		{
			SetMissionObjectImpulse setMissionObjectImpulse = (SetMissionObjectImpulse)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMissionObjectImpulse.MissionObjectId);
			if (missionObjectFromMissionObjectId != null)
			{
				Vec3 position = setMissionObjectImpulse.Position;
				missionObjectFromMissionObjectId.GameEntity.ApplyLocalImpulseToDynamicBody(position, setMissionObjectImpulse.Impulse);
			}
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x0008AC7C File Offset: 0x00088E7C
		private void HandleServerEventSetAgentTargetPositionAndDirection(GameNetworkMessage baseMessage)
		{
			SetAgentTargetPositionAndDirection setAgentTargetPositionAndDirection = (SetAgentTargetPositionAndDirection)baseMessage;
			Vec2 position = setAgentTargetPositionAndDirection.Position;
			Vec3 direction = setAgentTargetPositionAndDirection.Direction;
			Mission.MissionNetworkHelper.GetAgentFromIndex(setAgentTargetPositionAndDirection.AgentIndex, false).SetTargetPositionAndDirectionSynched(ref position, ref direction);
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x0008ACB4 File Offset: 0x00088EB4
		private void HandleServerEventSetAgentTargetPosition(GameNetworkMessage baseMessage)
		{
			SetAgentTargetPosition setAgentTargetPosition = (SetAgentTargetPosition)baseMessage;
			Vec2 position = setAgentTargetPosition.Position;
			Mission.MissionNetworkHelper.GetAgentFromIndex(setAgentTargetPosition.AgentIndex, false).SetTargetPositionSynched(ref position);
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x0008ACE0 File Offset: 0x00088EE0
		private void HandleServerEventClearAgentTargetFrame(GameNetworkMessage baseMessage)
		{
			Mission.MissionNetworkHelper.GetAgentFromIndex(((ClearAgentTargetFrame)baseMessage).AgentIndex, false).ClearTargetFrame();
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x0008ACF8 File Offset: 0x00088EF8
		private void HandleServerEventAgentTeleportToFrame(GameNetworkMessage baseMessage)
		{
			AgentTeleportToFrame agentTeleportToFrame = (AgentTeleportToFrame)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(agentTeleportToFrame.AgentIndex, false);
			agentFromIndex.TeleportToPosition(agentTeleportToFrame.Position);
			Vec2 vec = agentTeleportToFrame.Direction.Normalized();
			agentFromIndex.SetMovementDirection(in vec);
			agentFromIndex.LookDirection = vec.ToVec3(0f);
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x0008AD4C File Offset: 0x00088F4C
		private void HandleServerEventSetAgentPeer(GameNetworkMessage baseMessage)
		{
			SetAgentPeer setAgentPeer = (SetAgentPeer)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(setAgentPeer.AgentIndex, true);
			if (agentFromIndex != null)
			{
				NetworkCommunicator peer = setAgentPeer.Peer;
				MissionPeer missionPeer = ((peer != null) ? peer.GetComponent<MissionPeer>() : null);
				agentFromIndex.MissionPeer = missionPeer;
			}
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x0008AD8C File Offset: 0x00088F8C
		private void HandleServerEventSetAgentIsPlayer(GameNetworkMessage baseMessage)
		{
			SetAgentIsPlayer setAgentIsPlayer = (SetAgentIsPlayer)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(setAgentIsPlayer.AgentIndex, false);
			if (agentFromIndex.Controller == AgentControllerType.Player != setAgentIsPlayer.IsPlayer)
			{
				if (!agentFromIndex.IsMine)
				{
					agentFromIndex.Controller = AgentControllerType.None;
					return;
				}
				agentFromIndex.Controller = AgentControllerType.Player;
			}
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x0008ADD8 File Offset: 0x00088FD8
		private void HandleServerEventSetAgentHealth(GameNetworkMessage baseMessage)
		{
			SetAgentHealth setAgentHealth = (SetAgentHealth)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(setAgentHealth.AgentIndex, false).Health = (float)setAgentHealth.Health;
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x0008AE04 File Offset: 0x00089004
		private void HandleServerEventAgentSetTeam(GameNetworkMessage baseMessage)
		{
			AgentSetTeam agentSetTeam = (AgentSetTeam)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(agentSetTeam.AgentIndex, false);
			MBTeam mbteamFromTeamIndex = Mission.MissionNetworkHelper.GetMBTeamFromTeamIndex(agentSetTeam.TeamIndex);
			agentFromIndex.SetTeam(base.Mission.Teams.Find(mbteamFromTeamIndex), false);
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x0008AE48 File Offset: 0x00089048
		private void HandleServerEventSetAgentActionSet(GameNetworkMessage baseMessage)
		{
			SetAgentActionSet setAgentActionSet = (SetAgentActionSet)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(setAgentActionSet.AgentIndex, false);
			AnimationSystemData animationSystemData = agentFromIndex.Monster.FillAnimationSystemData(setAgentActionSet.ActionSet, setAgentActionSet.StepSize, false);
			animationSystemData.NumPaces = setAgentActionSet.NumPaces;
			animationSystemData.MonsterUsageSetIndex = setAgentActionSet.MonsterUsageSetIndex;
			animationSystemData.WalkingSpeedLimit = setAgentActionSet.WalkingSpeedLimit;
			animationSystemData.CrouchWalkingSpeedLimit = setAgentActionSet.CrouchWalkingSpeedLimit;
			agentFromIndex.SetActionSet(ref animationSystemData);
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x0008AEBC File Offset: 0x000890BC
		private void HandleServerEventMakeAgentDead(GameNetworkMessage baseMessage)
		{
			MakeAgentDead makeAgentDead = (MakeAgentDead)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(makeAgentDead.AgentIndex, false).MakeDead(makeAgentDead.IsKilled, makeAgentDead.ActionCodeIndex, makeAgentDead.CorpsesToFadeIndex);
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x0008AEF4 File Offset: 0x000890F4
		private void HandleServerEventAddPrefabComponentToAgentBone(GameNetworkMessage baseMessage)
		{
			AddPrefabComponentToAgentBone addPrefabComponentToAgentBone = (AddPrefabComponentToAgentBone)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(addPrefabComponentToAgentBone.AgentIndex, false).AddSynchedPrefabComponentToBone(addPrefabComponentToAgentBone.PrefabName, addPrefabComponentToAgentBone.BoneIndex);
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x0008AF28 File Offset: 0x00089128
		private void HandleServerEventSetAgentPrefabComponentVisibility(GameNetworkMessage baseMessage)
		{
			SetAgentPrefabComponentVisibility setAgentPrefabComponentVisibility = (SetAgentPrefabComponentVisibility)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(setAgentPrefabComponentVisibility.AgentIndex, false).SetSynchedPrefabComponentVisibility(setAgentPrefabComponentVisibility.ComponentIndex, setAgentPrefabComponentVisibility.Visibility);
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x0008AF5C File Offset: 0x0008915C
		private void HandleServerEventAgentSetFormation(GameNetworkMessage baseMessage)
		{
			AgentSetFormation agentSetFormation = (AgentSetFormation)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(agentSetFormation.AgentIndex, false);
			Team team = agentFromIndex.Team;
			Formation formation = null;
			if (team != null)
			{
				formation = ((agentSetFormation.FormationIndex >= 0) ? team.GetFormation((FormationClass)agentSetFormation.FormationIndex) : null);
			}
			agentFromIndex.Formation = formation;
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x0008AFA8 File Offset: 0x000891A8
		private void HandleServerEventUseObject(GameNetworkMessage baseMessage)
		{
			UseObject useObject = (UseObject)baseMessage;
			UsableMissionObject usableMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(useObject.UsableGameObjectId) as UsableMissionObject;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(useObject.AgentIndex, false);
			if (usableMissionObject != null)
			{
				usableMissionObject.SetUserForClient(agentFromIndex);
			}
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x0008AFE4 File Offset: 0x000891E4
		private void HandleServerEventStopUsingObject(GameNetworkMessage baseMessage)
		{
			StopUsingObject stopUsingObject = (StopUsingObject)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(stopUsingObject.AgentIndex, false);
			if (agentFromIndex == null)
			{
				return;
			}
			agentFromIndex.StopUsingGameObject(stopUsingObject.IsSuccessful, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x0008B018 File Offset: 0x00089218
		private void HandleServerEventHitSynchronizeObjectHitpoints(GameNetworkMessage baseMessage)
		{
			SyncObjectHitpoints syncObjectHitpoints = (SyncObjectHitpoints)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(syncObjectHitpoints.MissionObjectId);
			if (missionObjectFromMissionObjectId != null)
			{
				missionObjectFromMissionObjectId.GameEntity.GetFirstScriptOfType<DestructableComponent>().HitPoint = syncObjectHitpoints.Hitpoints;
			}
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x0008B054 File Offset: 0x00089254
		private void HandleServerEventHitSynchronizeObjectDestructionLevel(GameNetworkMessage baseMessage)
		{
			SyncObjectDestructionLevel syncObjectDestructionLevel = (SyncObjectDestructionLevel)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(syncObjectDestructionLevel.MissionObjectId);
			if (missionObjectFromMissionObjectId == null)
			{
				return;
			}
			missionObjectFromMissionObjectId.GameEntity.GetFirstScriptOfType<DestructableComponent>().SetDestructionLevel(syncObjectDestructionLevel.DestructionLevel, syncObjectDestructionLevel.ForcedIndex, syncObjectDestructionLevel.BlowMagnitude, syncObjectDestructionLevel.BlowPosition, syncObjectDestructionLevel.BlowDirection, false);
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x0008B0AC File Offset: 0x000892AC
		private void HandleServerEventHitBurstAllHeavyHitParticles(GameNetworkMessage baseMessage)
		{
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(((BurstAllHeavyHitParticles)baseMessage).MissionObjectId);
			if (missionObjectFromMissionObjectId == null)
			{
				return;
			}
			missionObjectFromMissionObjectId.GameEntity.GetFirstScriptOfType<DestructableComponent>().BurstHeavyHitParticles();
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x0008B0E0 File Offset: 0x000892E0
		private void HandleServerEventSynchronizeMissionObject(GameNetworkMessage baseMessage)
		{
			SynchronizeMissionObject synchronizeMissionObject = (SynchronizeMissionObject)baseMessage;
			SynchedMissionObject synchedMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(synchronizeMissionObject.MissionObjectId) as SynchedMissionObject;
			ValueTuple<BaseSynchedMissionObjectReadableRecord, ISynchedMissionObjectReadableRecord> recordPair = synchronizeMissionObject.RecordPair;
			synchedMissionObject.OnAfterReadFromNetwork(recordPair, true);
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x0008B114 File Offset: 0x00089314
		private void HandleServerEventSpawnWeaponWithNewEntity(GameNetworkMessage baseMessage)
		{
			SpawnWeaponWithNewEntity spawnWeaponWithNewEntity = (SpawnWeaponWithNewEntity)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(spawnWeaponWithNewEntity.ParentMissionObjectId);
			GameEntity gameEntity = base.Mission.SpawnWeaponWithNewEntityAux(spawnWeaponWithNewEntity.Weapon, spawnWeaponWithNewEntity.WeaponSpawnFlags, spawnWeaponWithNewEntity.Frame, spawnWeaponWithNewEntity.ForcedIndex, missionObjectFromMissionObjectId, spawnWeaponWithNewEntity.HasLifeTime, spawnWeaponWithNewEntity.SpawnedOnACorpse);
			if (!spawnWeaponWithNewEntity.IsVisible)
			{
				gameEntity.SetVisibilityExcludeParents(false);
			}
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x0008B174 File Offset: 0x00089374
		private void HandleServerEventAttachWeaponToSpawnedWeapon(GameNetworkMessage baseMessage)
		{
			AttachWeaponToSpawnedWeapon attachWeaponToSpawnedWeapon = (AttachWeaponToSpawnedWeapon)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(attachWeaponToSpawnedWeapon.MissionObjectId);
			base.Mission.AttachWeaponWithNewEntityToSpawnedWeapon(attachWeaponToSpawnedWeapon.Weapon, missionObjectFromMissionObjectId as SpawnedItemEntity, attachWeaponToSpawnedWeapon.AttachLocalFrame);
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x0008B1B4 File Offset: 0x000893B4
		private void HandleServerEventAttachWeaponToAgent(GameNetworkMessage baseMessage)
		{
			AttachWeaponToAgent attachWeaponToAgent = (AttachWeaponToAgent)baseMessage;
			MatrixFrame attachLocalFrame = attachWeaponToAgent.AttachLocalFrame;
			Mission.MissionNetworkHelper.GetAgentFromIndex(attachWeaponToAgent.AgentIndex, false).AttachWeaponToBone(attachWeaponToAgent.Weapon, null, attachWeaponToAgent.BoneIndex, ref attachLocalFrame);
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x0008B1F0 File Offset: 0x000893F0
		private void HandleServerEventHandleMissileCollisionReaction(GameNetworkMessage baseMessage)
		{
			HandleMissileCollisionReaction handleMissileCollisionReaction = (HandleMissileCollisionReaction)baseMessage;
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(handleMissileCollisionReaction.AttachedMissionObjectId);
			base.Mission.HandleMissileCollisionReaction(handleMissileCollisionReaction.MissileIndex, handleMissileCollisionReaction.CollisionReaction, handleMissileCollisionReaction.AttachLocalFrame, handleMissileCollisionReaction.IsAttachedFrameLocal, Mission.MissionNetworkHelper.GetAgentFromIndex(handleMissileCollisionReaction.AttackerAgentIndex, true), Mission.MissionNetworkHelper.GetAgentFromIndex(handleMissileCollisionReaction.AttachedAgentIndex, true), handleMissileCollisionReaction.AttachedToShield, handleMissileCollisionReaction.AttachedBoneIndex, missionObjectFromMissionObjectId, handleMissileCollisionReaction.BounceBackVelocity, handleMissileCollisionReaction.BounceBackAngularVelocity, handleMissileCollisionReaction.ForcedSpawnIndex);
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x0008B26C File Offset: 0x0008946C
		private void HandleServerEventSpawnWeaponAsDropFromAgent(GameNetworkMessage baseMessage)
		{
			SpawnWeaponAsDropFromAgent spawnWeaponAsDropFromAgent = (SpawnWeaponAsDropFromAgent)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(spawnWeaponAsDropFromAgent.AgentIndex, false);
			Vec3 velocity = spawnWeaponAsDropFromAgent.Velocity;
			Vec3 angularVelocity = spawnWeaponAsDropFromAgent.AngularVelocity;
			base.Mission.SpawnWeaponAsDropFromAgentAux(agentFromIndex, spawnWeaponAsDropFromAgent.EquipmentIndex, ref velocity, ref angularVelocity, spawnWeaponAsDropFromAgent.WeaponSpawnFlags, spawnWeaponAsDropFromAgent.ForcedIndex);
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x0008B2C0 File Offset: 0x000894C0
		private void HandleServerEventSpawnAttachedWeaponOnSpawnedWeapon(GameNetworkMessage baseMessage)
		{
			SpawnAttachedWeaponOnSpawnedWeapon spawnAttachedWeaponOnSpawnedWeapon = (SpawnAttachedWeaponOnSpawnedWeapon)baseMessage;
			SpawnedItemEntity spawnedItemEntity = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(spawnAttachedWeaponOnSpawnedWeapon.SpawnedWeaponId) as SpawnedItemEntity;
			base.Mission.SpawnAttachedWeaponOnSpawnedWeapon(spawnedItemEntity, spawnAttachedWeaponOnSpawnedWeapon.AttachmentIndex, spawnAttachedWeaponOnSpawnedWeapon.ForcedIndex);
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x0008B300 File Offset: 0x00089500
		private void HandleServerEventSpawnAttachedWeaponOnCorpse(GameNetworkMessage baseMessage)
		{
			SpawnAttachedWeaponOnCorpse spawnAttachedWeaponOnCorpse = (SpawnAttachedWeaponOnCorpse)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(spawnAttachedWeaponOnCorpse.AgentIndex, false);
			base.Mission.SpawnAttachedWeaponOnCorpse(agentFromIndex, spawnAttachedWeaponOnCorpse.AttachedIndex, spawnAttachedWeaponOnCorpse.ForcedIndex);
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x0008B33C File Offset: 0x0008953C
		private void HandleServerEventRemoveEquippedWeapon(GameNetworkMessage baseMessage)
		{
			RemoveEquippedWeapon removeEquippedWeapon = (RemoveEquippedWeapon)baseMessage;
			Mission.MissionNetworkHelper.GetAgentFromIndex(removeEquippedWeapon.AgentIndex, false).RemoveEquippedWeapon(removeEquippedWeapon.SlotIndex);
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x0008B368 File Offset: 0x00089568
		private void HandleServerEventBarkAgent(GameNetworkMessage baseMessage)
		{
			BarkAgent barkAgent = (BarkAgent)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(barkAgent.AgentIndex, false);
			agentFromIndex.HandleBark(barkAgent.IndexOfBark);
			if (!this._chatBox.IsPlayerMuted(agentFromIndex.MissionPeer.Peer.Id))
			{
				GameTexts.SetVariable("LEFT", agentFromIndex.NameTextObject);
				GameTexts.SetVariable("RIGHT", SkinVoiceManager.VoiceType.MpBarks[barkAgent.IndexOfBark].GetName());
				InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString(), Color.White, "Bark"));
			}
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x0008B408 File Offset: 0x00089608
		private void HandleServerEventEquipWeaponWithNewEntity(GameNetworkMessage baseMessage)
		{
			EquipWeaponWithNewEntity equipWeaponWithNewEntity = (EquipWeaponWithNewEntity)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(equipWeaponWithNewEntity.AgentIndex, false);
			if (agentFromIndex != null)
			{
				MissionWeapon weapon = equipWeaponWithNewEntity.Weapon;
				agentFromIndex.EquipWeaponWithNewEntity(equipWeaponWithNewEntity.SlotIndex, ref weapon);
			}
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x0008B444 File Offset: 0x00089644
		private void HandleServerEventAttachWeaponToWeaponInAgentEquipmentSlot(GameNetworkMessage baseMessage)
		{
			AttachWeaponToWeaponInAgentEquipmentSlot attachWeaponToWeaponInAgentEquipmentSlot = (AttachWeaponToWeaponInAgentEquipmentSlot)baseMessage;
			MatrixFrame attachLocalFrame = attachWeaponToWeaponInAgentEquipmentSlot.AttachLocalFrame;
			Mission.MissionNetworkHelper.GetAgentFromIndex(attachWeaponToWeaponInAgentEquipmentSlot.AgentIndex, false).AttachWeaponToWeapon(attachWeaponToWeaponInAgentEquipmentSlot.SlotIndex, attachWeaponToWeaponInAgentEquipmentSlot.Weapon, null, ref attachLocalFrame);
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x0008B480 File Offset: 0x00089680
		private void HandleServerEventEquipWeaponFromSpawnedItemEntity(GameNetworkMessage baseMessage)
		{
			EquipWeaponFromSpawnedItemEntity equipWeaponFromSpawnedItemEntity = (EquipWeaponFromSpawnedItemEntity)baseMessage;
			SpawnedItemEntity spawnedItemEntity = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(equipWeaponFromSpawnedItemEntity.SpawnedItemEntityId) as SpawnedItemEntity;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(equipWeaponFromSpawnedItemEntity.AgentIndex, true);
			if (agentFromIndex == null)
			{
				return;
			}
			agentFromIndex.EquipWeaponFromSpawnedItemEntity(equipWeaponFromSpawnedItemEntity.SlotIndex, spawnedItemEntity, equipWeaponFromSpawnedItemEntity.RemoveWeapon);
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x0008B4C8 File Offset: 0x000896C8
		private void HandleServerEventCreateMissile(GameNetworkMessage baseMessage)
		{
			CreateMissile createMissile = (CreateMissile)baseMessage;
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(createMissile.AgentIndex, false);
			if (createMissile.WeaponIndex != EquipmentIndex.None)
			{
				Vec3 vec = createMissile.Direction * createMissile.Speed;
				base.Mission.OnAgentShootMissile(agentFromIndex, createMissile.WeaponIndex, createMissile.Position, vec, createMissile.Orientation, createMissile.HasRigidBody, createMissile.IsPrimaryWeaponShot, createMissile.MissileIndex);
				return;
			}
			MissionObject missionObjectFromMissionObjectId = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(createMissile.MissionObjectToIgnoreId);
			base.Mission.AddCustomMissile(agentFromIndex, createMissile.Weapon, createMissile.Position, createMissile.Direction, createMissile.Orientation, createMissile.Speed, createMissile.Speed, createMissile.HasRigidBody, missionObjectFromMissionObjectId, createMissile.MissileIndex);
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x0008B580 File Offset: 0x00089780
		private void HandleServerEventAgentHit(GameNetworkMessage baseMessage)
		{
			CombatLogManager.GenerateCombatLog(Mission.MissionNetworkHelper.GetCombatLogDataForCombatLogNetworkMessage((CombatLogNetworkMessage)baseMessage));
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x0008B594 File Offset: 0x00089794
		private void HandleServerEventConsumeWeaponAmount(GameNetworkMessage baseMessage)
		{
			ConsumeWeaponAmount consumeWeaponAmount = (ConsumeWeaponAmount)baseMessage;
			(Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(consumeWeaponAmount.SpawnedItemEntityId) as SpawnedItemEntity).ConsumeWeaponAmount(consumeWeaponAmount.ConsumedAmount);
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x0008B5C4 File Offset: 0x000897C4
		private void HandleServerEventSetAgentOwningMissionPeer(GameNetworkMessage baseMessage)
		{
			SetAgentOwningMissionPeer setAgentOwningMissionPeer = (SetAgentOwningMissionPeer)baseMessage;
			Agent agent = Mission.Current.FindAgentWithIndex(setAgentOwningMissionPeer.AgentIndex);
			VirtualPlayer peer = setAgentOwningMissionPeer.Peer;
			MissionPeer missionPeer = ((peer != null) ? peer.GetComponent<MissionPeer>() : null);
			agent.SetOwningAgentMissionPeer(missionPeer);
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x0008B604 File Offset: 0x00089804
		private bool HandleClientEventSetFollowedAgent(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			SetFollowedAgent setFollowedAgent = (SetFollowedAgent)baseMessage;
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			if (component != null)
			{
				Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(setFollowedAgent.AgentIndex, true);
				component.FollowedAgent = agentFromIndex;
			}
			return true;
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x0008B638 File Offset: 0x00089838
		private bool HandleClientEventSetMachineRotation(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			SetMachineRotation setMachineRotation = (SetMachineRotation)baseMessage;
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			UsableMachine usableMachine = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(setMachineRotation.UsableMachineId) as UsableMachine;
			if (component.IsControlledAgentActive && usableMachine is RangedSiegeWeapon)
			{
				RangedSiegeWeapon rangedSiegeWeapon = usableMachine as RangedSiegeWeapon;
				if (component.ControlledAgent == rangedSiegeWeapon.PilotAgent && rangedSiegeWeapon.PilotAgent != null)
				{
					rangedSiegeWeapon.AimAtRotation(setMachineRotation.HorizontalRotation, setMachineRotation.VerticalRotation);
				}
			}
			return true;
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x0008B6A4 File Offset: 0x000898A4
		private bool HandleClientEventRequestUseObject(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			RequestUseObject requestUseObject = (RequestUseObject)baseMessage;
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			UsableMissionObject usableMissionObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(requestUseObject.UsableMissionObjectId) as UsableMissionObject;
			if (usableMissionObject != null && component.ControlledAgent != null && component.ControlledAgent.IsActive())
			{
				Vec3 position = component.ControlledAgent.Position;
				Vec3 globalPosition = usableMissionObject.InteractionEntity.GlobalPosition;
				float num;
				if (usableMissionObject is StandingPoint)
				{
					num = usableMissionObject.GetUserFrameForAgent(component.ControlledAgent).Origin.AsVec2.Distance(component.ControlledAgent.Position.AsVec2);
				}
				else
				{
					Vec3 vec;
					Vec3 vec2;
					usableMissionObject.InteractionEntity.GetPhysicsMinMax(true, out vec, out vec2, false);
					float num2 = globalPosition.Distance(vec);
					float num3 = globalPosition.Distance(vec2);
					float num4 = MathF.Max(num2, num3);
					num = globalPosition.Distance(new Vec3(position.x, position.y, position.z + component.ControlledAgent.GetEyeGlobalHeight(), -1f));
					num -= num4;
					num = MathF.Max(num, 0f);
				}
				if (component.ControlledAgent.CurrentlyUsedGameObject != usableMissionObject && component.ControlledAgent.CanReachAndUseObject(usableMissionObject, num * num * 0.9f * 0.9f) && component.ControlledAgent.ObjectHasVacantPosition(usableMissionObject))
				{
					component.ControlledAgent.UseGameObject(usableMissionObject, requestUseObject.UsedObjectPreferenceIndex);
				}
			}
			return true;
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x0008B814 File Offset: 0x00089A14
		private bool HandleClientEventRequestStopUsingObject(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			RequestStopUsingObject requestStopUsingObject = (RequestStopUsingObject)baseMessage;
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			Agent controlledAgent = component.ControlledAgent;
			if (((controlledAgent != null) ? controlledAgent.CurrentlyUsedGameObject : null) != null)
			{
				component.ControlledAgent.StopUsingGameObject(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
			}
			return true;
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x0008B854 File Offset: 0x00089A54
		private bool HandleClientEventApplyOrder(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ApplyOrder applyOrder = (ApplyOrder)baseMessage;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			if (orderControllerOfPeer != null)
			{
				orderControllerOfPeer.SetOrder(applyOrder.OrderType);
			}
			return true;
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x0008B880 File Offset: 0x00089A80
		private bool HandleClientEventApplySiegeWeaponOrder(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ApplySiegeWeaponOrder applySiegeWeaponOrder = (ApplySiegeWeaponOrder)baseMessage;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			if (orderControllerOfPeer != null)
			{
				orderControllerOfPeer.SiegeWeaponController.SetOrder(applySiegeWeaponOrder.OrderType);
			}
			return true;
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x0008B8B4 File Offset: 0x00089AB4
		private bool HandleClientEventApplyOrderWithPosition(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ApplyOrderWithPosition applyOrderWithPosition = (ApplyOrderWithPosition)baseMessage;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			if (orderControllerOfPeer != null)
			{
				WorldPosition worldPosition = new WorldPosition(base.Mission.Scene, UIntPtr.Zero, applyOrderWithPosition.Position, false);
				orderControllerOfPeer.SetOrderWithPosition(applyOrderWithPosition.OrderType, worldPosition);
			}
			return true;
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x0008B900 File Offset: 0x00089B00
		private bool HandleClientEventApplyOrderWithFormation(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ApplyOrderWithFormation message = (ApplyOrderWithFormation)baseMessage;
			Team teamOfPeer = this.GetTeamOfPeer(networkPeer);
			OrderController orderController = ((teamOfPeer != null) ? teamOfPeer.GetOrderControllerOf(networkPeer.ControlledAgent) : null);
			Formation formation = ((teamOfPeer != null) ? teamOfPeer.FormationsIncludingEmpty.SingleOrDefault<Formation>((Formation f) => f.CountOfUnits > 0 && f.Index == message.FormationIndex) : null);
			if (teamOfPeer != null && orderController != null && formation != null)
			{
				orderController.SetOrderWithFormation(message.OrderType, formation);
			}
			return true;
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x0008B974 File Offset: 0x00089B74
		private bool HandleClientEventApplyOrderWithFormationAndPercentage(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ApplyOrderWithFormationAndPercentage message = (ApplyOrderWithFormationAndPercentage)baseMessage;
			Team teamOfPeer = this.GetTeamOfPeer(networkPeer);
			OrderController orderController = ((teamOfPeer != null) ? teamOfPeer.GetOrderControllerOf(networkPeer.ControlledAgent) : null);
			Formation formation = ((teamOfPeer != null) ? teamOfPeer.FormationsIncludingEmpty.SingleOrDefault<Formation>((Formation f) => f.CountOfUnits > 0 && f.Index == message.FormationIndex) : null);
			float num = (float)message.Percentage * 0.01f;
			if (teamOfPeer != null && orderController != null && formation != null)
			{
				orderController.SetOrderWithFormationAndPercentage(message.OrderType, formation, num);
			}
			return true;
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x0008B9FC File Offset: 0x00089BFC
		private bool HandleClientEventApplyOrderWithFormationAndNumber(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ApplyOrderWithFormationAndNumber message = (ApplyOrderWithFormationAndNumber)baseMessage;
			Team teamOfPeer = this.GetTeamOfPeer(networkPeer);
			OrderController orderController = ((teamOfPeer != null) ? teamOfPeer.GetOrderControllerOf(networkPeer.ControlledAgent) : null);
			Formation formation = ((teamOfPeer != null) ? teamOfPeer.FormationsIncludingEmpty.SingleOrDefault<Formation>((Formation f) => f.CountOfUnits > 0 && f.Index == message.FormationIndex) : null);
			int number = message.Number;
			if (teamOfPeer != null && orderController != null && formation != null)
			{
				orderController.SetOrderWithFormationAndNumber(message.OrderType, formation, number);
			}
			return true;
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x0008BA80 File Offset: 0x00089C80
		private bool HandleClientEventApplyOrderWithTwoPositions(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ApplyOrderWithTwoPositions applyOrderWithTwoPositions = (ApplyOrderWithTwoPositions)baseMessage;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			if (orderControllerOfPeer != null)
			{
				WorldPosition worldPosition = new WorldPosition(base.Mission.Scene, UIntPtr.Zero, applyOrderWithTwoPositions.Position1, false);
				WorldPosition worldPosition2 = new WorldPosition(base.Mission.Scene, UIntPtr.Zero, applyOrderWithTwoPositions.Position2, false);
				orderControllerOfPeer.SetOrderWithTwoPositions(applyOrderWithTwoPositions.OrderType, worldPosition, worldPosition2);
			}
			return true;
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x0008BAEC File Offset: 0x00089CEC
		private bool HandleClientEventApplyOrderWithGameEntity(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			IOrderable orderable = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(((ApplyOrderWithMissionObject)baseMessage).MissionObjectId) as IOrderable;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			if (orderControllerOfPeer != null)
			{
				orderControllerOfPeer.SetOrderWithOrderableObject(orderable);
			}
			return true;
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x0008BB24 File Offset: 0x00089D24
		private bool HandleClientEventApplyOrderWithAgent(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ApplyOrderWithAgent applyOrderWithAgent = (ApplyOrderWithAgent)baseMessage;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(applyOrderWithAgent.AgentIndex, false);
			if (orderControllerOfPeer != null)
			{
				orderControllerOfPeer.SetOrderWithAgent(applyOrderWithAgent.OrderType, agentFromIndex);
			}
			return true;
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x0008BB5F File Offset: 0x00089D5F
		private bool HandleClientEventSelectAllFormations(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			SelectAllFormations selectAllFormations = (SelectAllFormations)baseMessage;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			if (orderControllerOfPeer != null)
			{
				orderControllerOfPeer.SelectAllFormations(false);
			}
			return true;
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x0008BB7C File Offset: 0x00089D7C
		private bool HandleClientEventSelectAllSiegeWeapons(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			SelectAllSiegeWeapons selectAllSiegeWeapons = (SelectAllSiegeWeapons)baseMessage;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			if (orderControllerOfPeer != null)
			{
				orderControllerOfPeer.SiegeWeaponController.SelectAll();
			}
			return true;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x0008BB9D File Offset: 0x00089D9D
		private bool HandleClientEventClearSelectedFormations(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			ClearSelectedFormations clearSelectedFormations = (ClearSelectedFormations)baseMessage;
			OrderController orderControllerOfPeer = this.GetOrderControllerOfPeer(networkPeer);
			if (orderControllerOfPeer != null)
			{
				orderControllerOfPeer.ClearSelectedFormations();
			}
			return true;
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x0008BBBC File Offset: 0x00089DBC
		private bool HandleClientEventSelectFormation(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			SelectFormation message = (SelectFormation)baseMessage;
			Team teamOfPeer = this.GetTeamOfPeer(networkPeer);
			OrderController orderController = ((teamOfPeer != null) ? teamOfPeer.GetOrderControllerOf(networkPeer.ControlledAgent) : null);
			Formation formation = ((teamOfPeer != null) ? teamOfPeer.FormationsIncludingEmpty.SingleOrDefault<Formation>((Formation f) => f.Index == message.FormationIndex && f.CountOfUnits > 0) : null);
			if (teamOfPeer != null && orderController != null && formation != null)
			{
				orderController.SelectFormation(formation);
			}
			return true;
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x0008BC28 File Offset: 0x00089E28
		private bool HandleClientEventSelectSiegeWeapon(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			SelectSiegeWeapon selectSiegeWeapon = (SelectSiegeWeapon)baseMessage;
			Team teamOfPeer = this.GetTeamOfPeer(networkPeer);
			SiegeWeaponController siegeWeaponController = ((teamOfPeer != null) ? teamOfPeer.GetOrderControllerOf(networkPeer.ControlledAgent).SiegeWeaponController : null);
			SiegeWeapon siegeWeapon = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(selectSiegeWeapon.SiegeWeaponId) as SiegeWeapon;
			if (teamOfPeer != null && siegeWeaponController != null && siegeWeapon != null)
			{
				siegeWeaponController.Select(siegeWeapon);
			}
			return true;
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x0008BC80 File Offset: 0x00089E80
		private bool HandleClientEventUnselectFormation(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			UnselectFormation message = (UnselectFormation)baseMessage;
			Team teamOfPeer = this.GetTeamOfPeer(networkPeer);
			OrderController orderController = ((teamOfPeer != null) ? teamOfPeer.GetOrderControllerOf(networkPeer.ControlledAgent) : null);
			Formation formation = ((teamOfPeer != null) ? teamOfPeer.FormationsIncludingEmpty.SingleOrDefault<Formation>((Formation f) => f.CountOfUnits > 0 && f.Index == message.FormationIndex) : null);
			if (teamOfPeer != null && orderController != null && formation != null)
			{
				orderController.DeselectFormation(formation);
			}
			return true;
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x0008BCEC File Offset: 0x00089EEC
		private bool HandleClientEventUnselectSiegeWeapon(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			UnselectSiegeWeapon unselectSiegeWeapon = (UnselectSiegeWeapon)baseMessage;
			Team teamOfPeer = this.GetTeamOfPeer(networkPeer);
			SiegeWeaponController siegeWeaponController = ((teamOfPeer != null) ? teamOfPeer.GetOrderControllerOf(networkPeer.ControlledAgent).SiegeWeaponController : null);
			SiegeWeapon siegeWeapon = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(unselectSiegeWeapon.SiegeWeaponId) as SiegeWeapon;
			if (teamOfPeer != null && siegeWeaponController != null && siegeWeapon != null)
			{
				siegeWeaponController.Deselect(siegeWeapon);
			}
			return true;
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x0008BD44 File Offset: 0x00089F44
		private bool HandleClientEventDropWeapon(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			DropWeapon dropWeapon = (DropWeapon)baseMessage;
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			if (((component != null) ? component.ControlledAgent : null) != null && component.ControlledAgent.IsActive())
			{
				component.ControlledAgent.HandleDropWeapon(dropWeapon.IsDefendPressed, dropWeapon.ForcedSlotIndexToDropWeaponFrom);
			}
			return true;
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x0008BD94 File Offset: 0x00089F94
		private bool HandleClientEventCheerSelected(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			TauntSelected tauntSelected = (TauntSelected)baseMessage;
			bool flag = false;
			if (networkPeer.ControlledAgent != null)
			{
				networkPeer.ControlledAgent.HandleTaunt(tauntSelected.IndexOfTaunt, false);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x0008BDC8 File Offset: 0x00089FC8
		private bool HandleClientEventBarkSelected(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			BarkSelected barkSelected = (BarkSelected)baseMessage;
			bool flag = false;
			if (networkPeer.ControlledAgent != null)
			{
				networkPeer.ControlledAgent.HandleBark(barkSelected.IndexOfBark);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x0008BDFA File Offset: 0x00089FFA
		private bool HandleClientEventBreakAgentVisualsInvulnerability(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			AgentVisualsBreakInvulnerability agentVisualsBreakInvulnerability = (AgentVisualsBreakInvulnerability)baseMessage;
			if (base.Mission == null || base.Mission.GetMissionBehavior<SpawnComponent>() == null || networkPeer.GetComponent<MissionPeer>() == null)
			{
				return false;
			}
			base.Mission.GetMissionBehavior<SpawnComponent>().SetEarlyAgentVisualsDespawning(networkPeer.GetComponent<MissionPeer>(), true);
			return true;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x0008BE3C File Offset: 0x0008A03C
		private bool HandleClientEventRequestToSpawnAsBot(NetworkCommunicator networkPeer, GameNetworkMessage baseMessage)
		{
			RequestToSpawnAsBot requestToSpawnAsBot = (RequestToSpawnAsBot)baseMessage;
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			if (component == null)
			{
				return false;
			}
			if (component.HasSpawnTimerExpired)
			{
				component.WantsToSpawnAsBot = true;
			}
			return true;
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x0008BE6C File Offset: 0x0008A06C
		private void SendExistingObjectsToPeer(NetworkCommunicator networkPeer)
		{
			MBDebug.Print(string.Concat(new object[] { "Sending all existing objects to peer: ", networkPeer.UserName, " with index: ", networkPeer.Index }), 0, Debug.DebugColor.White, 17179869184UL);
			GameNetwork.BeginModuleEventAsServer(networkPeer);
			GameNetwork.WriteMessage(new ExistingObjectsBegin());
			GameNetwork.EndModuleEventAsServer();
			GameNetwork.BeginModuleEventAsServer(networkPeer);
			GameNetwork.WriteMessage(new SynchronizeMissionTimeTracker((float)MissionTime.Now.ToSeconds));
			GameNetwork.EndModuleEventAsServer();
			this.SendTeamsToPeer(networkPeer);
			this.SendTeamRelationsToPeer(networkPeer);
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeersIncludingDisconnectedPeers)
			{
				MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
				if (component != null)
				{
					if (component.Team != null)
					{
						GameNetwork.BeginModuleEventAsServer(networkPeer);
						GameNetwork.WriteMessage(new SetPeerTeam(networkCommunicator, component.Team.TeamIndex));
						GameNetwork.EndModuleEventAsServer();
					}
					if (component.Culture != null)
					{
						GameNetwork.BeginModuleEventAsServer(networkPeer);
						GameNetwork.WriteMessage(new ChangeCulture(component, component.Culture));
						GameNetwork.EndModuleEventAsServer();
					}
				}
			}
			this.SendFormationInformation(networkPeer);
			this.SendAgentsToPeer(networkPeer);
			this.SendSpawnedMissionObjectsToPeer(networkPeer);
			this.SynchronizeMissionObjectsToPeer(networkPeer);
			this.SendMissilesToPeer(networkPeer);
			this.SendTroopSelectionInformation(networkPeer);
			networkPeer.SendExistingObjects(base.Mission);
			GameNetwork.BeginModuleEventAsServer(networkPeer);
			GameNetwork.WriteMessage(new ExistingObjectsEnd());
			GameNetwork.EndModuleEventAsServer();
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x0008BFDC File Offset: 0x0008A1DC
		private void SendTroopSelectionInformation(NetworkCommunicator networkPeer)
		{
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeersIncludingDisconnectedPeers)
			{
				MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
				if (component != null && component.SelectedTroopIndex != 0)
				{
					GameNetwork.BeginModuleEventAsServer(networkPeer);
					GameNetwork.WriteMessage(new UpdateSelectedTroopIndex(networkCommunicator, component.SelectedTroopIndex));
					GameNetwork.EndModuleEventAsServer();
				}
			}
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x0008C050 File Offset: 0x0008A250
		private void SendTeamsToPeer(NetworkCommunicator networkPeer)
		{
			foreach (Team team in base.Mission.Teams)
			{
				MBDebug.Print(string.Concat(new object[] { "Syncing a team to peer: ", networkPeer.UserName, " with index: ", networkPeer.Index }), 0, Debug.DebugColor.White, 17179869184UL);
				GameNetwork.BeginModuleEventAsServer(networkPeer);
				GameNetwork.WriteMessage(new AddTeam(team.TeamIndex, team.Side, team.Color, team.Color2, (team.Banner != null) ? team.Banner.BannerCode : string.Empty, team.IsPlayerGeneral, team.IsPlayerSergeant));
				GameNetwork.EndModuleEventAsServer();
			}
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x0008C140 File Offset: 0x0008A340
		private void SendTeamRelationsToPeer(NetworkCommunicator networkPeer)
		{
			int count = base.Mission.Teams.Count;
			for (int i = 0; i < count; i++)
			{
				for (int j = i; j < count; j++)
				{
					Team team = base.Mission.Teams[i];
					Team team2 = base.Mission.Teams[j];
					if (team.IsEnemyOf(team2))
					{
						GameNetwork.BeginModuleEventAsServer(networkPeer);
						GameNetwork.WriteMessage(new TeamSetIsEnemyOf(team.TeamIndex, team2.TeamIndex, true));
						GameNetwork.EndModuleEventAsServer();
					}
				}
			}
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x0008C1C8 File Offset: 0x0008A3C8
		private void SendFormationInformation(NetworkCommunicator networkPeer)
		{
			MBDebug.Print("formations sending begin-", 0, Debug.DebugColor.White, 17179869184UL);
			foreach (Team team in base.Mission.Teams)
			{
				if (team.IsValid && team.Side != BattleSideEnum.None)
				{
					foreach (Formation formation in team.FormationsIncludingEmpty)
					{
						if (!string.IsNullOrEmpty(formation.BannerCode))
						{
							GameNetwork.BeginModuleEventAsServer(networkPeer);
							GameNetwork.WriteMessage(new InitializeFormation(formation, team.TeamIndex, formation.BannerCode));
							GameNetwork.EndModuleEventAsServer();
						}
					}
				}
			}
			if (!networkPeer.IsServerPeer)
			{
				GameNetwork.BeginModuleEventAsServer(networkPeer);
				GameNetwork.WriteMessage(new SetSpawnedFormationCount(base.Mission.NumOfFormationsSpawnedTeamOne, base.Mission.NumOfFormationsSpawnedTeamTwo));
				GameNetwork.EndModuleEventAsServer();
			}
			MBDebug.Print("formations sending end-", 0, Debug.DebugColor.White, 17179869184UL);
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x0008C2F4 File Offset: 0x0008A4F4
		private void SendAgentVisualsToPeer(NetworkCommunicator networkPeer, Team peerTeam)
		{
			MBDebug.Print("agentvisuals sending begin-", 0, Debug.DebugColor.White, 17179869184UL);
			foreach (MissionPeer missionPeer in from p in GameNetwork.NetworkPeers
				select p.GetComponent<MissionPeer>() into pr
				where pr != null
				select pr)
			{
				if (missionPeer.Team == peerTeam)
				{
					int amountOfAgentVisualsForPeer = missionPeer.GetAmountOfAgentVisualsForPeer();
					for (int i = 0; i < amountOfAgentVisualsForPeer; i++)
					{
						PeerVisualsHolder visuals = missionPeer.GetVisuals(i);
						IAgentVisual agentVisuals = visuals.AgentVisuals;
						MatrixFrame frame = agentVisuals.GetFrame();
						AgentBuildData agentBuildData = new AgentBuildData(MBObjectManager.Instance.GetObject<BasicCharacterObject>(agentVisuals.GetCharacterObjectID())).MissionPeer(missionPeer).Equipment(agentVisuals.GetEquipment()).VisualsIndex(visuals.VisualsIndex)
							.Team(missionPeer.Team)
							.InitialPosition(in frame.origin);
						Vec2 vec = frame.rotation.f.AsVec2;
						vec = vec.Normalized();
						AgentBuildData agentBuildData2 = agentBuildData.InitialDirection(in vec).IsFemale(agentVisuals.GetIsFemale()).BodyProperties(agentVisuals.GetBodyProperties());
						networkPeer.GetComponent<MissionPeer>();
						GameNetwork.BeginModuleEventAsServer(networkPeer);
						GameNetwork.WriteMessage(new CreateAgentVisuals(missionPeer.GetNetworkPeer(), agentBuildData2, missionPeer.SelectedTroopIndex, 0));
						GameNetwork.EndModuleEventAsServer();
					}
				}
			}
			MBDebug.Print("agentvisuals sending end-", 0, Debug.DebugColor.White, 17179869184UL);
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x0008C4B4 File Offset: 0x0008A6B4
		private void SendAgentsToPeer(NetworkCommunicator networkPeer)
		{
			MBDebug.Print("agents sending begin-", 0, Debug.DebugColor.White, 17179869184UL);
			using (List<Agent>.Enumerator enumerator = base.Mission.AllAgents.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Agent agent = enumerator.Current;
					bool isMount = agent.IsMount;
					bool flag = agent.IsAddedAsCorpse();
					AgentState state = agent.State;
					if (!flag && (state == AgentState.Active || ((state == AgentState.Killed || state == AgentState.Unconscious) && (agent.GetAttachedWeaponsCount() > 0 || (!isMount && (agent.GetPrimaryWieldedItemIndex() >= EquipmentIndex.WeaponItemBeginSlot || agent.GetOffhandWieldedItemIndex() >= EquipmentIndex.WeaponItemBeginSlot)) || base.Mission.IsAgentInProximityMap(agent))) || base.Mission.MissilesList.Any<Mission.Missile>((Mission.Missile m) => m.ShooterAgent == agent)))
					{
						if (isMount && agent.RiderAgent == null)
						{
							MBDebug.Print("mount sending " + agent.Index, 0, Debug.DebugColor.White, 17179869184UL);
							GameNetwork.BeginModuleEventAsServer(networkPeer);
							GameNetwork.WriteMessage(new CreateFreeMountAgent(agent, agent.Position, agent.GetMovementDirection()));
							GameNetwork.EndModuleEventAsServer();
							agent.LockAgentReplicationTableDataWithCurrentReliableSequenceNo(networkPeer);
							int attachedWeaponsCount = agent.GetAttachedWeaponsCount();
							for (int i = 0; i < attachedWeaponsCount; i++)
							{
								GameNetwork.BeginModuleEventAsServer(networkPeer);
								GameNetwork.WriteMessage(new AttachWeaponToAgent(agent.GetAttachedWeapon(i), agent.Index, agent.GetAttachedWeaponBoneIndex(i), agent.GetAttachedWeaponFrame(i)));
								GameNetwork.EndModuleEventAsServer();
							}
							if (!agent.IsActive())
							{
								GameNetwork.BeginModuleEventAsServer(networkPeer);
								GameNetwork.WriteMessage(new MakeAgentDead(agent.Index, state == AgentState.Killed, agent.GetCurrentAction(0), -1));
								GameNetwork.EndModuleEventAsServer();
							}
						}
						else if (!isMount)
						{
							MBDebug.Print("human sending " + agent.Index, 0, Debug.DebugColor.White, 17179869184UL);
							Agent agent2 = agent.MountAgent;
							if (agent2 != null && agent2.RiderAgent == null)
							{
								agent2 = null;
							}
							GameNetwork.BeginModuleEventAsServer(networkPeer);
							int index = agent.Index;
							BasicCharacterObject character = agent.Character;
							Monster monster = agent.Monster;
							Equipment spawnEquipment = agent.SpawnEquipment;
							MissionEquipment equipment = agent.Equipment;
							BodyProperties bodyPropertiesValue = agent.BodyPropertiesValue;
							int bodyPropertiesSeed = agent.BodyPropertiesSeed;
							bool isFemale = agent.IsFemale;
							Team team = agent.Team;
							int num = ((team != null) ? team.TeamIndex : (-1));
							Formation formation = agent.Formation;
							int num2 = ((formation != null) ? formation.Index : (-1));
							uint clothingColor = agent.ClothingColor1;
							uint clothingColor2 = agent.ClothingColor2;
							int num3 = ((agent2 != null) ? agent2.Index : (-1));
							Agent mountAgent = agent.MountAgent;
							Equipment equipment2 = ((mountAgent != null) ? mountAgent.SpawnEquipment : null);
							bool flag2 = agent.MissionPeer != null && agent.OwningAgentMissionPeer == null;
							Vec3 position = agent.Position;
							Vec2 movementDirection = agent.GetMovementDirection();
							MissionPeer missionPeer = agent.MissionPeer;
							NetworkCommunicator networkCommunicator;
							if ((networkCommunicator = ((missionPeer != null) ? missionPeer.GetNetworkPeer() : null)) == null)
							{
								MissionPeer owningAgentMissionPeer = agent.OwningAgentMissionPeer;
								networkCommunicator = ((owningAgentMissionPeer != null) ? owningAgentMissionPeer.GetNetworkPeer() : null);
							}
							GameNetwork.WriteMessage(new CreateAgent(index, character, monster, spawnEquipment, equipment, bodyPropertiesValue, bodyPropertiesSeed, isFemale, num, num2, clothingColor, clothingColor2, num3, equipment2, flag2, position, movementDirection, networkCommunicator));
							GameNetwork.EndModuleEventAsServer();
							agent.LockAgentReplicationTableDataWithCurrentReliableSequenceNo(networkPeer);
							if (agent2 != null)
							{
								agent2.LockAgentReplicationTableDataWithCurrentReliableSequenceNo(networkPeer);
							}
							for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
							{
								for (int j = 0; j < agent.Equipment[equipmentIndex].GetAttachedWeaponsCount(); j++)
								{
									GameNetwork.BeginModuleEventAsServer(networkPeer);
									GameNetwork.WriteMessage(new AttachWeaponToWeaponInAgentEquipmentSlot(agent.Equipment[equipmentIndex].GetAttachedWeapon(j), agent.Index, equipmentIndex, agent.Equipment[equipmentIndex].GetAttachedWeaponFrame(j)));
									GameNetwork.EndModuleEventAsServer();
								}
							}
							int num4 = agent.GetAttachedWeaponsCount();
							for (int k = 0; k < num4; k++)
							{
								GameNetwork.BeginModuleEventAsServer(networkPeer);
								GameNetwork.WriteMessage(new AttachWeaponToAgent(agent.GetAttachedWeapon(k), agent.Index, agent.GetAttachedWeaponBoneIndex(k), agent.GetAttachedWeaponFrame(k)));
								GameNetwork.EndModuleEventAsServer();
							}
							if (agent2 != null)
							{
								num4 = agent2.GetAttachedWeaponsCount();
								for (int l = 0; l < num4; l++)
								{
									GameNetwork.BeginModuleEventAsServer(networkPeer);
									GameNetwork.WriteMessage(new AttachWeaponToAgent(agent2.GetAttachedWeapon(l), agent2.Index, agent2.GetAttachedWeaponBoneIndex(l), agent2.GetAttachedWeaponFrame(l)));
									GameNetwork.EndModuleEventAsServer();
								}
							}
							EquipmentIndex primaryWieldedItemIndex = agent.GetPrimaryWieldedItemIndex();
							int num5 = ((primaryWieldedItemIndex != EquipmentIndex.None) ? agent.Equipment[primaryWieldedItemIndex].CurrentUsageIndex : 0);
							GameNetwork.BeginModuleEventAsServer(networkPeer);
							GameNetwork.WriteMessage(new SetWieldedItemIndex(agent.Index, false, true, true, primaryWieldedItemIndex, num5));
							GameNetwork.EndModuleEventAsServer();
							GameNetwork.BeginModuleEventAsServer(networkPeer);
							GameNetwork.WriteMessage(new SetWieldedItemIndex(agent.Index, true, true, true, agent.GetOffhandWieldedItemIndex(), num5));
							GameNetwork.EndModuleEventAsServer();
							MBActionSet mbactionSet = agent.ActionSet;
							if (mbactionSet.IsValid)
							{
								AnimationSystemData animationSystemData = agent.Monster.FillAnimationSystemData(mbactionSet, agent.Character.GetStepSize(), false);
								GameNetwork.BeginModuleEventAsServer(networkPeer);
								GameNetwork.WriteMessage(new SetAgentActionSet(agent.Index, animationSystemData));
								GameNetwork.EndModuleEventAsServer();
								if (!agent.IsActive())
								{
									GameNetwork.BeginModuleEventAsServer(networkPeer);
									GameNetwork.WriteMessage(new MakeAgentDead(agent.Index, state == AgentState.Killed, agent.GetCurrentAction(0), -1));
									GameNetwork.EndModuleEventAsServer();
								}
							}
							else
							{
								mbactionSet = MBActionSet.GetActionSet("as_human_warrior");
								AnimationSystemData animationSystemData2 = agent.Monster.FillAnimationSystemData(mbactionSet, agent.Character.GetStepSize(), false);
								GameNetwork.BeginModuleEventAsServer(networkPeer);
								GameNetwork.WriteMessage(new SetAgentActionSet(agent.Index, animationSystemData2));
								GameNetwork.EndModuleEventAsServer();
								GameNetwork.BeginModuleEventAsServer(networkPeer);
								GameNetwork.WriteMessage(new MakeAgentDead(agent.Index, state == AgentState.Killed, ActionIndexCache.act_death_by_arrow_pelvis, -1));
								GameNetwork.EndModuleEventAsServer();
							}
						}
						else
						{
							MBDebug.Print("agent not sending " + agent.Index, 0, Debug.DebugColor.White, 17179869184UL);
						}
					}
				}
			}
			MBDebug.Print("agents sending end-", 0, Debug.DebugColor.White, 17179869184UL);
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x0008CBDC File Offset: 0x0008ADDC
		private void SendSpawnedMissionObjectsToPeer(NetworkCommunicator networkPeer)
		{
			using (List<MissionObject>.Enumerator enumerator = base.Mission.MissionObjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MissionObject missionObject = enumerator.Current;
					SpawnedItemEntity spawnedItemEntity;
					if ((spawnedItemEntity = missionObject as SpawnedItemEntity) != null)
					{
						WeakGameEntity gameEntity = spawnedItemEntity.GameEntity;
						if (!gameEntity.Parent.IsValid || !gameEntity.Parent.HasScriptOfType<SpawnedItemEntity>())
						{
							MissionObject missionObject2 = null;
							if (spawnedItemEntity.GameEntity.Parent.IsValid)
							{
								missionObject2 = gameEntity.Parent.GetFirstScriptOfType<MissionObject>();
							}
							MatrixFrame matrixFrame = gameEntity.GetGlobalFrame();
							if (missionObject2 != null)
							{
								matrixFrame = missionObject2.GameEntity.GetGlobalFrame().TransformToLocalNonOrthogonal(in matrixFrame);
							}
							matrixFrame.origin.z = MathF.Max(matrixFrame.origin.z, CompressionBasic.PositionCompressionInfo.GetMinimumValue() + 1f);
							Mission.WeaponSpawnFlags weaponSpawnFlags = spawnedItemEntity.SpawnFlags;
							if (weaponSpawnFlags.HasAnyFlag(Mission.WeaponSpawnFlags.WithPhysics) && !gameEntity.GetPhysicsState())
							{
								weaponSpawnFlags = (weaponSpawnFlags & ~Mission.WeaponSpawnFlags.WithPhysics) | Mission.WeaponSpawnFlags.WithStaticPhysics;
							}
							bool flag = true;
							bool flag2 = !spawnedItemEntity.SpawnedOnACorpse && (!gameEntity.Parent.IsValid || missionObject2 != null);
							GameNetwork.BeginModuleEventAsServer(networkPeer);
							GameNetwork.WriteMessage(new SpawnWeaponWithNewEntity(spawnedItemEntity.WeaponCopy, weaponSpawnFlags, spawnedItemEntity.Id.Id, matrixFrame, (missionObject2 != null) ? missionObject2.Id : MissionObjectId.Invalid, flag2, flag, spawnedItemEntity.SpawnedOnACorpse));
							GameNetwork.EndModuleEventAsServer();
							for (int i = 0; i < spawnedItemEntity.WeaponCopy.GetAttachedWeaponsCount(); i++)
							{
								GameNetwork.BeginModuleEventAsServer(networkPeer);
								GameNetwork.WriteMessage(new AttachWeaponToSpawnedWeapon(spawnedItemEntity.WeaponCopy.GetAttachedWeapon(i), spawnedItemEntity.Id, spawnedItemEntity.WeaponCopy.GetAttachedWeaponFrame(i)));
								GameNetwork.EndModuleEventAsServer();
								if (spawnedItemEntity.WeaponCopy.GetAttachedWeapon(i).Item.ItemFlags.HasAnyFlag(ItemFlags.CanBePickedUpFromCorpse))
								{
									if (!gameEntity.GetChild(i).IsValid)
									{
										Debug.Print(string.Concat(new object[]
										{
											"spawnedItemGameEntity child is null. item: ",
											spawnedItemEntity.WeaponCopy.Item.StringId,
											" attached item: ",
											spawnedItemEntity.WeaponCopy.GetAttachedWeapon(i).Item.StringId,
											" attachment index: ",
											i
										}), 0, Debug.DebugColor.White, 17592186044416UL);
									}
									else if (gameEntity.GetChild(i).GetFirstScriptOfType<SpawnedItemEntity>() == null)
									{
										Debug.Print(string.Concat(new object[]
										{
											"spawnedItemGameEntity child SpawnedItemEntity script is null. item: ",
											spawnedItemEntity.WeaponCopy.Item.StringId,
											" attached item: ",
											spawnedItemEntity.WeaponCopy.GetAttachedWeapon(i).Item.StringId,
											" attachment index: ",
											i
										}), 0, Debug.DebugColor.White, 17592186044416UL);
									}
									GameNetwork.BeginModuleEventAsServer(networkPeer);
									GameNetwork.WriteMessage(new SpawnAttachedWeaponOnSpawnedWeapon(spawnedItemEntity.Id, i, gameEntity.GetChild(i).GetFirstScriptOfType<SpawnedItemEntity>().Id.Id));
									GameNetwork.EndModuleEventAsServer();
								}
							}
						}
					}
					else if (missionObject.CreatedAtRuntime)
					{
						Mission.DynamicallyCreatedEntity dynamicallyCreatedEntity = base.Mission.AddedEntitiesInfo.SingleOrDefault<Mission.DynamicallyCreatedEntity>((Mission.DynamicallyCreatedEntity x) => x.ObjectId == missionObject.Id);
						if (dynamicallyCreatedEntity != null)
						{
							GameNetwork.BeginModuleEventAsServer(networkPeer);
							GameNetwork.WriteMessage(new CreateMissionObject(dynamicallyCreatedEntity.ObjectId, dynamicallyCreatedEntity.Prefab, dynamicallyCreatedEntity.Frame, dynamicallyCreatedEntity.ChildObjectIds));
							GameNetwork.EndModuleEventAsServer();
						}
					}
				}
			}
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x0008CFEC File Offset: 0x0008B1EC
		private void SynchronizeMissionObjectsToPeer(NetworkCommunicator networkPeer)
		{
			using (List<MissionObject>.Enumerator enumerator = base.Mission.MissionObjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SynchedMissionObject synchedMissionObject;
					if ((synchedMissionObject = enumerator.Current as SynchedMissionObject) != null)
					{
						GameNetwork.BeginModuleEventAsServer(networkPeer);
						GameNetwork.WriteMessage(new SynchronizeMissionObject(synchedMissionObject));
						GameNetwork.EndModuleEventAsServer();
					}
				}
			}
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x0008D05C File Offset: 0x0008B25C
		private void SendMissilesToPeer(NetworkCommunicator networkPeer)
		{
			foreach (Mission.Missile missile in base.Mission.MissilesList)
			{
				Vec3 velocity = missile.GetVelocity();
				float num = velocity.Normalize();
				Mat3 identity = Mat3.Identity;
				identity.f = velocity;
				identity.Orthonormalize();
				GameNetwork.BeginModuleEventAsServer(networkPeer);
				int index = missile.Index;
				int index2 = missile.ShooterAgent.Index;
				EquipmentIndex equipmentIndex = EquipmentIndex.None;
				MissionWeapon weapon = missile.Weapon;
				Vec3 position = missile.GetPosition();
				Vec3 vec = velocity;
				float num2 = num;
				Mat3 mat = identity;
				bool hasRigidBody = missile.GetHasRigidBody();
				MissionObject missionObjectToIgnore = missile.MissionObjectToIgnore;
				GameNetwork.WriteMessage(new CreateMissile(index, index2, equipmentIndex, weapon, position, vec, num2, mat, hasRigidBody, (missionObjectToIgnore != null) ? missionObjectToIgnore.Id : MissionObjectId.Invalid, false));
				GameNetwork.EndModuleEventAsServer();
			}
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x0008D12C File Offset: 0x0008B32C
		public override void OnPlayerDisconnectedFromServer(NetworkCommunicator networkPeer)
		{
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			if (component != null && component.HasSpawnedAgentVisuals)
			{
				base.Mission.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>().RemoveAgentVisuals(component, false);
				component.HasSpawnedAgentVisuals = false;
			}
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x0008D164 File Offset: 0x0008B364
		protected override void HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			if (!networkPeer.IsServerPeer)
			{
				foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
				{
					if (networkCommunicator.IsSynchronized || networkCommunicator.JustReconnecting)
					{
						networkCommunicator.VirtualPlayer.SynchronizeComponentsTo(networkPeer.VirtualPlayer);
					}
				}
				foreach (NetworkCommunicator networkCommunicator2 in GameNetwork.DisconnectedNetworkPeers)
				{
					networkCommunicator2.VirtualPlayer.SynchronizeComponentsTo(networkPeer.VirtualPlayer);
				}
			}
			MissionPeer missionPeer = networkPeer.AddComponent<MissionPeer>();
			if (networkPeer.JustReconnecting && missionPeer.Team != null)
			{
				MBAPI.IMBPeer.SetTeam(networkPeer.Index, missionPeer.Team.MBTeam.Index);
			}
			missionPeer.JoinTime = DateTime.Now;
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x0008D268 File Offset: 0x0008B468
		protected override void HandleLateNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			if (!networkPeer.IsServerPeer)
			{
				this.SendExistingObjectsToPeer(networkPeer);
			}
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x0008D27C File Offset: 0x0008B47C
		protected override void HandleEarlyPlayerDisconnect(NetworkCommunicator networkPeer)
		{
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			if (component != null)
			{
				Mission mission = base.Mission;
				if (mission != null)
				{
					mission.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>().RemoveAgentVisuals(component, true);
				}
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new RemoveAgentVisualsForPeer(component.GetNetworkPeer()));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				}
				component.HasSpawnedAgentVisuals = false;
			}
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x0008D2D5 File Offset: 0x0008B4D5
		public override void OnRemoveBehavior()
		{
			base.OnRemoveBehavior();
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x0008D2E0 File Offset: 0x0008B4E0
		protected override void HandlePlayerDisconnect(NetworkCommunicator networkPeer)
		{
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			if (component != null)
			{
				if (component.ControlledAgent != null)
				{
					Agent controlledAgent = component.ControlledAgent;
					Blow blow = new Blow(controlledAgent.Index);
					blow.WeaponRecord = default(BlowWeaponRecord);
					blow.DamageType = DamageTypes.Invalid;
					blow.BaseMagnitude = 10000f;
					blow.WeaponRecord.WeaponClass = WeaponClass.Undefined;
					blow.GlobalPosition = controlledAgent.Position;
					blow.DamagedPercentage = 1f;
					controlledAgent.Die(blow, Agent.KillInfo.Invalid);
				}
				if (base.Mission.AllAgents != null)
				{
					foreach (Agent agent in base.Mission.AllAgents)
					{
						if (agent.MissionPeer == component)
						{
							agent.MissionPeer = null;
						}
						if (agent.OwningAgentMissionPeer == component)
						{
							agent.SetOwningAgentMissionPeer(null);
						}
					}
				}
				if (component.ControlledFormation != null)
				{
					component.ControlledFormation.PlayerOwner = null;
				}
			}
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x0008D3F0 File Offset: 0x0008B5F0
		public override void OnAddTeam(Team team)
		{
			base.OnAddTeam(team);
			if (GameNetwork.IsServerOrRecorder)
			{
				MBDebug.Print("----------OnAddTeam-", 0, Debug.DebugColor.White, 17592186044416UL);
				MBDebug.Print("Adding a team and sending it to all clients", 0, Debug.DebugColor.White, 17179869184UL);
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new AddTeam(team.TeamIndex, team.Side, team.Color, team.Color2, (team.Banner != null) ? team.Banner.BannerCode : string.Empty, team.IsPlayerGeneral, team.IsPlayerSergeant));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				return;
			}
			if (team.Side != BattleSideEnum.Attacker && team.Side != BattleSideEnum.Defender && base.Mission.SpectatorTeam == null)
			{
				base.Mission.SpectatorTeam = team;
			}
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x0008D4BA File Offset: 0x0008B6BA
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._chatBox = Game.Current.GetGameHandler<ChatBox>();
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x0008D4D2 File Offset: 0x0008B6D2
		public override void OnClearScene()
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				MBDebug.Print("I am clearing the scene, and sending this message to all clients", 0, Debug.DebugColor.White, 17179869184UL);
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new ClearMission());
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x0008D508 File Offset: 0x0008B708
		public override void OnMissionTick(float dt)
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				this._accumulatedTimeSinceLastTimerSync += dt;
				if (this._accumulatedTimeSinceLastTimerSync > 2f)
				{
					this._accumulatedTimeSinceLastTimerSync -= 2f;
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SynchronizeMissionTimeTracker((float)MissionTime.Now.ToSeconds));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				}
			}
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
			{
				MissionRepresentativeBase component = networkCommunicator.GetComponent<MissionRepresentativeBase>();
				if (component != null)
				{
					component.Tick(dt);
				}
				if (GameNetwork.IsServer && !networkCommunicator.IsServerPeer && !MultiplayerOptions.OptionType.DisableInactivityKick.GetBoolValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions))
				{
					MissionPeer component2 = networkCommunicator.GetComponent<MissionPeer>();
					if (component2 != null)
					{
						component2.TickInactivityStatus();
					}
				}
			}
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x0008D5E8 File Offset: 0x0008B7E8
		protected override void OnEndMission()
		{
			if (GameNetwork.IsServer)
			{
				foreach (MissionPeer missionPeer in VirtualPlayer.Peers<MissionPeer>())
				{
					missionPeer.ControlledAgent = null;
				}
				foreach (Agent agent in base.Mission.AllAgents)
				{
					agent.MissionPeer = null;
				}
			}
			base.OnEndMission();
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x0008D68C File Offset: 0x0008B88C
		public void OnPeerSelectedTeam(MissionPeer missionPeer)
		{
			this.SendAgentVisualsToPeer(missionPeer.GetNetworkPeer(), missionPeer.Team);
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x0008D6A0 File Offset: 0x0008B8A0
		public void OnClientSynchronized(NetworkCommunicator networkPeer)
		{
			Action<NetworkCommunicator> onClientSynchronizedEvent = this.OnClientSynchronizedEvent;
			if (onClientSynchronizedEvent != null)
			{
				onClientSynchronizedEvent(networkPeer);
			}
			if (networkPeer.IsMine)
			{
				Action onMyClientSynchronized = this.OnMyClientSynchronized;
				if (onMyClientSynchronized == null)
				{
					return;
				}
				onMyClientSynchronized();
			}
		}

		// Token: 0x04000E8A RID: 3722
		private float _accumulatedTimeSinceLastTimerSync;

		// Token: 0x04000E8B RID: 3723
		private const float TimerSyncPeriod = 2f;

		// Token: 0x04000E8C RID: 3724
		private ChatBox _chatBox;
	}
}
