using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200028B RID: 651
	public class MissionBoundaryCrossingHandler : MissionLogic
	{
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06002437 RID: 9271 RVA: 0x00083714 File Offset: 0x00081914
		// (remove) Token: 0x06002438 RID: 9272 RVA: 0x0008374C File Offset: 0x0008194C
		public event Action<float, float> StartTime;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06002439 RID: 9273 RVA: 0x00083784 File Offset: 0x00081984
		// (remove) Token: 0x0600243A RID: 9274 RVA: 0x000837BC File Offset: 0x000819BC
		public event Action StopTime;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x0600243B RID: 9275 RVA: 0x000837F4 File Offset: 0x000819F4
		// (remove) Token: 0x0600243C RID: 9276 RVA: 0x0008382C File Offset: 0x00081A2C
		public event Action<float> TimeCount;

		// Token: 0x0600243D RID: 9277 RVA: 0x00083861 File Offset: 0x00081A61
		public MissionBoundaryCrossingHandler(float leewayTime = 10f)
		{
			this._leewayTime = leewayTime;
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x00083870 File Offset: 0x00081A70
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			if (GameNetwork.IsSessionActive)
			{
				this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add);
			}
			if (GameNetwork.IsServer)
			{
				this._agentTimers = new Dictionary<Agent, MissionTimer>();
				this._agentsToPunish = new List<Agent>();
			}
			this._vehicleHandler = base.Mission.GetMissionBehavior<IVehicleHandler>();
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000838BF File Offset: 0x00081ABF
		public override void OnRemoveBehavior()
		{
			if (GameNetwork.IsSessionActive)
			{
				this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Remove);
			}
			base.OnRemoveBehavior();
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000838D8 File Offset: 0x00081AD8
		private void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode mode)
		{
			GameNetwork.NetworkMessageHandlerRegisterer networkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegisterer(mode);
			if (GameNetwork.IsClient)
			{
				networkMessageHandlerRegisterer.Register<SetBoundariesState>(new GameNetworkMessage.ServerMessageHandlerDelegate<SetBoundariesState>(this.HandleServerEventSetPeerBoundariesState));
			}
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x00083908 File Offset: 0x00081B08
		private void OnAgentWentOut(Agent agent, float startTimeInSeconds)
		{
			MissionTimer missionTimer = (GameNetwork.IsClient ? MissionTimer.CreateSynchedTimerClient(startTimeInSeconds, this._leewayTime) : new MissionTimer(this._leewayTime));
			if (GameNetwork.IsServer)
			{
				this._agentTimers.Add(agent, missionTimer);
				MissionPeer missionPeer = agent.MissionPeer;
				NetworkCommunicator networkCommunicator = ((missionPeer != null) ? missionPeer.GetNetworkPeer() : null);
				if (networkCommunicator != null && !networkCommunicator.IsServerPeer)
				{
					GameNetwork.BeginModuleEventAsServer(networkCommunicator);
					GameNetwork.WriteMessage(new SetBoundariesState(true, missionTimer.GetStartTime().NumberOfTicks));
					GameNetwork.EndModuleEventAsServer();
				}
			}
			if (base.Mission.MainAgent == agent)
			{
				this._mainAgentLeaveTimer = missionTimer;
				Action<float, float> startTime = this.StartTime;
				if (startTime != null)
				{
					startTime(this._leewayTime, 0f);
				}
				MatrixFrame cameraFrame = Mission.Current.GetCameraFrame();
				Vec3 vec = cameraFrame.origin + cameraFrame.rotation.u;
				if (Mission.Current.Mode == MissionMode.Battle)
				{
					MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/alerts/report/out_of_map"), vec);
				}
			}
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x00083A00 File Offset: 0x00081C00
		private void OnAgentWentInOrRemoved(Agent agent, bool isAgentRemoved)
		{
			if (GameNetwork.IsServer)
			{
				this._agentTimers.Remove(agent);
				if (!isAgentRemoved)
				{
					MissionPeer missionPeer = agent.MissionPeer;
					NetworkCommunicator networkCommunicator = ((missionPeer != null) ? missionPeer.GetNetworkPeer() : null);
					if (networkCommunicator != null && !networkCommunicator.IsServerPeer)
					{
						GameNetwork.BeginModuleEventAsServer(networkCommunicator);
						GameNetwork.WriteMessage(new SetBoundariesState(false));
						GameNetwork.EndModuleEventAsServer();
					}
				}
			}
			if (base.Mission.MainAgent == agent)
			{
				this._mainAgentLeaveTimer = null;
				Action stopTime = this.StopTime;
				if (stopTime == null)
				{
					return;
				}
				stopTime();
			}
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x00083A80 File Offset: 0x00081C80
		private void HandleAgentPunishmentsServer()
		{
			foreach (Agent agent in this._agentsToPunish)
			{
				Blow blow = new Blow(agent.Index);
				blow.WeaponRecord.FillAsMeleeBlow(null, null, -1, 0);
				blow.DamageType = DamageTypes.Blunt;
				blow.BaseMagnitude = 10000f;
				blow.WeaponRecord.WeaponClass = WeaponClass.Undefined;
				blow.GlobalPosition = agent.Position;
				blow.DamagedPercentage = 1f;
				agent.Die(blow, Agent.KillInfo.Invalid);
			}
			this._agentsToPunish.Clear();
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x00083B38 File Offset: 0x00081D38
		private void DecideOrHandleAgentPunishment(Agent agent)
		{
			if (GameNetwork.IsSessionActive)
			{
				if (GameNetwork.IsServer)
				{
					this._agentsToPunish.Add(agent);
					if (agent.MountAgent != null)
					{
						this._agentsToPunish.Add(agent.MountAgent);
						return;
					}
				}
			}
			else
			{
				base.Mission.RetreatMission();
			}
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x00083B84 File Offset: 0x00081D84
		public override void OnClearScene()
		{
			if (GameNetwork.IsServer)
			{
				using (List<Agent>.Enumerator enumerator = this._agentTimers.Keys.ToList<Agent>().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Agent agent = enumerator.Current;
						this.OnAgentWentInOrRemoved(agent, true);
					}
					return;
				}
			}
			if (this._mainAgentLeaveTimer != null)
			{
				if (base.Mission.MainAgent != null)
				{
					this.OnAgentWentInOrRemoved(base.Mission.MainAgent, true);
					return;
				}
				this._mainAgentLeaveTimer = null;
			}
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x00083C18 File Offset: 0x00081E18
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			this.OnAgentWentInOrRemoved(affectedAgent, true);
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x00083C24 File Offset: 0x00081E24
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (GameNetwork.IsServer)
			{
				for (int i = base.Mission.Agents.Count - 1; i >= 0; i--)
				{
					Agent agent = base.Mission.Agents[i];
					if (agent.MissionPeer != null)
					{
						this.TickForAgentAsServer(agent);
					}
				}
				this.HandleAgentPunishmentsServer();
			}
			else if (!GameNetwork.IsSessionActive && Agent.Main != null)
			{
				this.TickForMainAgent();
			}
			if (this._mainAgentLeaveTimer != null)
			{
				this._mainAgentLeaveTimer.Check(false);
				float num = 1f - this._mainAgentLeaveTimer.GetRemainingTimeInSeconds(true) / this._leewayTime;
				Action<float> timeCount = this.TimeCount;
				if (timeCount == null)
				{
					return;
				}
				timeCount(num);
			}
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x00083CDC File Offset: 0x00081EDC
		private void TickForMainAgent()
		{
			WeakGameEntity weakGameEntity;
			bool flag;
			if (this._vehicleHandler != null && this._vehicleHandler.IsAgentInVehicle(Agent.Main, out weakGameEntity))
			{
				flag = !base.Mission.IsPositionInsideBoundaries(weakGameEntity.GlobalPosition.AsVec2);
			}
			else
			{
				flag = !base.Mission.IsPositionInsideBoundaries(Agent.Main.Position.AsVec2);
			}
			bool flag2 = this._mainAgentLeaveTimer != null;
			this.HandleAgentStateChange(Agent.Main, flag, flag2, this._mainAgentLeaveTimer);
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x00083D64 File Offset: 0x00081F64
		private void TickForAgentAsServer(Agent agent)
		{
			bool flag = !base.Mission.IsPositionInsideBoundaries(agent.Position.AsVec2);
			bool flag2 = this._agentTimers.ContainsKey(agent);
			this.HandleAgentStateChange(agent, flag, flag2, flag2 ? this._agentTimers[agent] : null);
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x00083DB6 File Offset: 0x00081FB6
		private void HandleAgentStateChange(Agent agent, bool isAgentOutside, bool isTimerActiveForAgent, MissionTimer timerInstance)
		{
			if (isAgentOutside && !isTimerActiveForAgent)
			{
				this.OnAgentWentOut(agent, 0f);
				return;
			}
			if (!isAgentOutside && isTimerActiveForAgent)
			{
				this.OnAgentWentInOrRemoved(agent, false);
				return;
			}
			if (isAgentOutside && timerInstance.Check(false))
			{
				this.DecideOrHandleAgentPunishment(agent);
			}
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x00083DF0 File Offset: 0x00081FF0
		private void HandleServerEventSetPeerBoundariesState(SetBoundariesState message)
		{
			if (message.IsOutside)
			{
				this.OnAgentWentOut(base.Mission.MainAgent, message.StateStartTimeInSeconds);
				return;
			}
			this.OnAgentWentInOrRemoved(base.Mission.MainAgent, false);
		}

		// Token: 0x04000DFE RID: 3582
		private float _leewayTime;

		// Token: 0x04000E02 RID: 3586
		private List<Agent> _agentsToPunish;

		// Token: 0x04000E03 RID: 3587
		private Dictionary<Agent, MissionTimer> _agentTimers;

		// Token: 0x04000E04 RID: 3588
		private MissionTimer _mainAgentLeaveTimer;

		// Token: 0x04000E05 RID: 3589
		private IVehicleHandler _vehicleHandler;
	}
}
