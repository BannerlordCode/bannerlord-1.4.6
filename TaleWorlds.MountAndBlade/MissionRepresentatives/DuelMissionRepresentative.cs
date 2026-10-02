using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade.MissionRepresentatives
{
	// Token: 0x020003C2 RID: 962
	public class DuelMissionRepresentative : MissionRepresentativeBase
	{
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x060035F3 RID: 13811 RVA: 0x000DE3B7 File Offset: 0x000DC5B7
		// (set) Token: 0x060035F4 RID: 13812 RVA: 0x000DE3BF File Offset: 0x000DC5BF
		public int Bounty { get; private set; }

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x060035F5 RID: 13813 RVA: 0x000DE3C8 File Offset: 0x000DC5C8
		// (set) Token: 0x060035F6 RID: 13814 RVA: 0x000DE3D0 File Offset: 0x000DC5D0
		public int Score { get; private set; }

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x060035F7 RID: 13815 RVA: 0x000DE3D9 File Offset: 0x000DC5D9
		// (set) Token: 0x060035F8 RID: 13816 RVA: 0x000DE3E1 File Offset: 0x000DC5E1
		public int NumberOfWins { get; private set; }

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x060035F9 RID: 13817 RVA: 0x000DE3EA File Offset: 0x000DC5EA
		private bool _isInDuel
		{
			get
			{
				return base.MissionPeer != null && base.MissionPeer.Team != null && base.MissionPeer.Team.IsDefender;
			}
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x000DE413 File Offset: 0x000DC613
		public override void Initialize()
		{
			this._requesters = new List<Tuple<MissionPeer, MissionTime>>();
			if (GameNetwork.IsServerOrRecorder)
			{
				this._missionMultiplayerDuel = Mission.Current.GetMissionBehavior<MissionMultiplayerDuel>();
			}
			Mission.Current.SetMissionMode(MissionMode.Duel, true);
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x000DE444 File Offset: 0x000DC644
		public void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode mode)
		{
			if (GameNetwork.IsClient)
			{
				GameNetwork.NetworkMessageHandlerRegisterer networkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegisterer(mode);
				networkMessageHandlerRegisterer.Register<NetworkMessages.FromServer.DuelRequest>(new GameNetworkMessage.ServerMessageHandlerDelegate<NetworkMessages.FromServer.DuelRequest>(this.HandleServerEventDuelRequest));
				networkMessageHandlerRegisterer.Register<DuelSessionStarted>(new GameNetworkMessage.ServerMessageHandlerDelegate<DuelSessionStarted>(this.HandleServerEventDuelSessionStarted));
				networkMessageHandlerRegisterer.Register<DuelPreparationStartedForTheFirstTime>(new GameNetworkMessage.ServerMessageHandlerDelegate<DuelPreparationStartedForTheFirstTime>(this.HandleServerEventDuelStarted));
				networkMessageHandlerRegisterer.Register<DuelEnded>(new GameNetworkMessage.ServerMessageHandlerDelegate<DuelEnded>(this.HandleServerEventDuelEnded));
				networkMessageHandlerRegisterer.Register<DuelRoundEnded>(new GameNetworkMessage.ServerMessageHandlerDelegate<DuelRoundEnded>(this.HandleServerEventDuelRoundEnded));
				networkMessageHandlerRegisterer.Register<DuelPointsUpdateMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<DuelPointsUpdateMessage>(this.HandleServerPointUpdate));
			}
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000DE4CC File Offset: 0x000DC6CC
		public void OnInteraction()
		{
			if (this._focusedObject != null)
			{
				DuelZoneLandmark duelZoneLandmark;
				Agent focusedAgent;
				if ((focusedAgent = this._focusedObject as Agent) != null)
				{
					if (focusedAgent.IsActive())
					{
						if (this._requesters.Any<Tuple<MissionPeer, MissionTime>>((Tuple<MissionPeer, MissionTime> req) => req.Item1 == focusedAgent.MissionPeer))
						{
							for (int i = 0; i < this._requesters.Count; i++)
							{
								if (this._requesters[i].Item1 == base.MissionPeer)
								{
									this._requesters.Remove(this._requesters[i]);
									break;
								}
							}
							MissionRepresentativeBase.PlayerTypes playerTypes = base.PlayerType;
							if (playerTypes == MissionRepresentativeBase.PlayerTypes.Client)
							{
								GameNetwork.BeginModuleEventAsClient();
								GameNetwork.WriteMessage(new DuelResponse(focusedAgent.MissionRepresentative.Peer.Communicator as NetworkCommunicator, true));
								GameNetwork.EndModuleEventAsClient();
								return;
							}
							if (playerTypes != MissionRepresentativeBase.PlayerTypes.Server)
							{
								return;
							}
							this._missionMultiplayerDuel.DuelRequestAccepted(focusedAgent, base.ControlledAgent);
							return;
						}
						else
						{
							MissionRepresentativeBase.PlayerTypes playerTypes = base.PlayerType;
							if (playerTypes == MissionRepresentativeBase.PlayerTypes.Client)
							{
								Action<MissionPeer> onDuelRequestSentEvent = this.OnDuelRequestSentEvent;
								if (onDuelRequestSentEvent != null)
								{
									onDuelRequestSentEvent(focusedAgent.MissionPeer);
								}
								GameNetwork.BeginModuleEventAsClient();
								GameNetwork.WriteMessage(new NetworkMessages.FromClient.DuelRequest(focusedAgent.Index));
								GameNetwork.EndModuleEventAsClient();
								return;
							}
							if (playerTypes != MissionRepresentativeBase.PlayerTypes.Server)
							{
								return;
							}
							this._missionMultiplayerDuel.DuelRequestReceived(base.MissionPeer, focusedAgent.MissionPeer);
							return;
						}
					}
				}
				else if ((duelZoneLandmark = this._focusedObject as DuelZoneLandmark) != null)
				{
					if (this._isInDuel)
					{
						InformationManager.DisplayMessage(new InformationMessage(new TextObject("{=v5EqMSlD}Can't change arena preference while in duel.", null).ToString()));
						return;
					}
					GameNetwork.BeginModuleEventAsClient();
					GameNetwork.WriteMessage(new RequestChangePreferredTroopType(duelZoneLandmark.ZoneTroopType));
					GameNetwork.EndModuleEventAsClient();
					Action<TroopType> onMyPreferredZoneChanged = this.OnMyPreferredZoneChanged;
					if (onMyPreferredZoneChanged == null)
					{
						return;
					}
					onMyPreferredZoneChanged(duelZoneLandmark.ZoneTroopType);
				}
			}
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000DE6A0 File Offset: 0x000DC8A0
		private void HandleServerEventDuelRequest(NetworkMessages.FromServer.DuelRequest message)
		{
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(message.RequesterAgentIndex, false);
			Mission.MissionNetworkHelper.GetAgentFromIndex(message.RequestedAgentIndex, false);
			this.DuelRequested(agentFromIndex, message.SelectedAreaTroopType);
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x000DE6D4 File Offset: 0x000DC8D4
		private void HandleServerEventDuelSessionStarted(DuelSessionStarted message)
		{
			this.OnDuelPreparation(message.RequesterPeer.GetComponent<MissionPeer>(), message.RequestedPeer.GetComponent<MissionPeer>());
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x000DE6F4 File Offset: 0x000DC8F4
		private void HandleServerEventDuelStarted(DuelPreparationStartedForTheFirstTime message)
		{
			MissionPeer component = message.RequesterPeer.GetComponent<MissionPeer>();
			MissionPeer component2 = message.RequesteePeer.GetComponent<MissionPeer>();
			Action<MissionPeer, MissionPeer, int> onDuelPreparationStartedForTheFirstTimeEvent = this.OnDuelPreparationStartedForTheFirstTimeEvent;
			if (onDuelPreparationStartedForTheFirstTimeEvent == null)
			{
				return;
			}
			onDuelPreparationStartedForTheFirstTimeEvent(component, component2, message.AreaIndex);
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x000DE731 File Offset: 0x000DC931
		private void HandleServerEventDuelEnded(DuelEnded message)
		{
			Action<MissionPeer> onDuelEndedEvent = this.OnDuelEndedEvent;
			if (onDuelEndedEvent == null)
			{
				return;
			}
			onDuelEndedEvent(message.WinnerPeer.GetComponent<MissionPeer>());
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000DE74E File Offset: 0x000DC94E
		private void HandleServerEventDuelRoundEnded(DuelRoundEnded message)
		{
			Action<MissionPeer> onDuelRoundEndedEvent = this.OnDuelRoundEndedEvent;
			if (onDuelRoundEndedEvent == null)
			{
				return;
			}
			onDuelRoundEndedEvent(message.WinnerPeer.GetComponent<MissionPeer>());
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000DE76B File Offset: 0x000DC96B
		private void HandleServerPointUpdate(DuelPointsUpdateMessage message)
		{
			DuelMissionRepresentative component = message.NetworkCommunicator.GetComponent<DuelMissionRepresentative>();
			component.Bounty = message.Bounty;
			component.Score = message.Score;
			component.NumberOfWins = message.NumberOfWins;
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000DE79C File Offset: 0x000DC99C
		public void DuelRequested(Agent requesterAgent, TroopType selectedAreaTroopType)
		{
			this._requesters.Add(new Tuple<MissionPeer, MissionTime>(requesterAgent.MissionPeer, MissionTime.Now + MissionTime.Seconds(10f)));
			switch (base.PlayerType)
			{
			case MissionRepresentativeBase.PlayerTypes.Bot:
				this._missionMultiplayerDuel.DuelRequestAccepted(requesterAgent, base.ControlledAgent);
				return;
			case MissionRepresentativeBase.PlayerTypes.Client:
			{
				if (!base.IsMine)
				{
					GameNetwork.BeginModuleEventAsServer(base.Peer);
					GameNetwork.WriteMessage(new NetworkMessages.FromServer.DuelRequest(requesterAgent.Index, base.ControlledAgent.Index, selectedAreaTroopType));
					GameNetwork.EndModuleEventAsServer();
					return;
				}
				Action<MissionPeer, TroopType> onDuelRequestedEvent = this.OnDuelRequestedEvent;
				if (onDuelRequestedEvent == null)
				{
					return;
				}
				onDuelRequestedEvent(requesterAgent.MissionPeer, selectedAreaTroopType);
				return;
			}
			case MissionRepresentativeBase.PlayerTypes.Server:
			{
				Action<MissionPeer, TroopType> onDuelRequestedEvent2 = this.OnDuelRequestedEvent;
				if (onDuelRequestedEvent2 == null)
				{
					return;
				}
				onDuelRequestedEvent2(requesterAgent.MissionPeer, selectedAreaTroopType);
				return;
			}
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000DE86C File Offset: 0x000DCA6C
		public bool CheckHasRequestFromAndRemoveRequestIfNeeded(MissionPeer requestOwner)
		{
			if (requestOwner != null && requestOwner.Representative == this)
			{
				this._requesters.Clear();
				return false;
			}
			Tuple<MissionPeer, MissionTime> tuple = this._requesters.FirstOrDefault<Tuple<MissionPeer, MissionTime>>((Tuple<MissionPeer, MissionTime> req) => req.Item1 == requestOwner);
			if (tuple == null)
			{
				return false;
			}
			if (requestOwner.ControlledAgent == null || !requestOwner.ControlledAgent.IsActive())
			{
				this._requesters.Remove(tuple);
				return false;
			}
			if (!tuple.Item2.IsPast)
			{
				return true;
			}
			this._requesters.Remove(tuple);
			return false;
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x000DE914 File Offset: 0x000DCB14
		public void OnDuelPreparation(MissionPeer requesterPeer, MissionPeer requesteePeer)
		{
			MissionRepresentativeBase.PlayerTypes playerType = base.PlayerType;
			if (playerType != MissionRepresentativeBase.PlayerTypes.Client)
			{
				if (playerType == MissionRepresentativeBase.PlayerTypes.Server)
				{
					Action<MissionPeer, int> onDuelPrepStartedEvent = this.OnDuelPrepStartedEvent;
					if (onDuelPrepStartedEvent != null)
					{
						onDuelPrepStartedEvent((base.MissionPeer == requesterPeer) ? requesteePeer : requesterPeer, 3);
					}
				}
			}
			else if (base.IsMine)
			{
				Action<MissionPeer, int> onDuelPrepStartedEvent2 = this.OnDuelPrepStartedEvent;
				if (onDuelPrepStartedEvent2 != null)
				{
					onDuelPrepStartedEvent2((base.MissionPeer == requesterPeer) ? requesteePeer : requesterPeer, 3);
				}
			}
			else
			{
				GameNetwork.BeginModuleEventAsServer(base.Peer);
				GameNetwork.WriteMessage(new DuelSessionStarted(requesterPeer.GetNetworkPeer(), requesteePeer.GetNetworkPeer()));
				GameNetwork.EndModuleEventAsServer();
			}
			Tuple<MissionPeer, MissionTime> tuple = this._requesters.FirstOrDefault<Tuple<MissionPeer, MissionTime>>((Tuple<MissionPeer, MissionTime> req) => req.Item1 == requesterPeer);
			if (tuple != null)
			{
				this._requesters.Remove(tuple);
			}
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x000DE9F3 File Offset: 0x000DCBF3
		public void OnObjectFocused(IFocusable focusedObject)
		{
			this._focusedObject = focusedObject;
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x000DE9FC File Offset: 0x000DCBFC
		public void OnObjectFocusLost()
		{
			this._focusedObject = null;
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000DEA05 File Offset: 0x000DCC05
		public override void OnAgentSpawned()
		{
			if (base.ControlledAgent.Team != null && base.ControlledAgent.Team.Side == BattleSideEnum.Attacker)
			{
				Action onAgentSpawnedWithoutDuelEvent = this.OnAgentSpawnedWithoutDuelEvent;
				if (onAgentSpawnedWithoutDuelEvent == null)
				{
					return;
				}
				onAgentSpawnedWithoutDuelEvent();
			}
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000DEA37 File Offset: 0x000DCC37
		public void ResetBountyAndNumberOfWins()
		{
			this.Bounty = 0;
			this.NumberOfWins = 0;
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000DEA48 File Offset: 0x000DCC48
		public void OnDuelWon(float gainedScore)
		{
			this.Bounty += (int)(gainedScore / 5f);
			this.Score += (int)gainedScore;
			int numberOfWins = this.NumberOfWins;
			this.NumberOfWins = numberOfWins + 1;
		}

		// Token: 0x0400170F RID: 5903
		public const int DuelPrepTime = 3;

		// Token: 0x04001710 RID: 5904
		public Action<MissionPeer, TroopType> OnDuelRequestedEvent;

		// Token: 0x04001711 RID: 5905
		public Action<MissionPeer> OnDuelRequestSentEvent;

		// Token: 0x04001712 RID: 5906
		public Action<MissionPeer, int> OnDuelPrepStartedEvent;

		// Token: 0x04001713 RID: 5907
		public Action OnAgentSpawnedWithoutDuelEvent;

		// Token: 0x04001714 RID: 5908
		public Action<MissionPeer, MissionPeer, int> OnDuelPreparationStartedForTheFirstTimeEvent;

		// Token: 0x04001715 RID: 5909
		public Action<MissionPeer> OnDuelEndedEvent;

		// Token: 0x04001716 RID: 5910
		public Action<MissionPeer> OnDuelRoundEndedEvent;

		// Token: 0x04001717 RID: 5911
		public Action<TroopType> OnMyPreferredZoneChanged;

		// Token: 0x04001718 RID: 5912
		private List<Tuple<MissionPeer, MissionTime>> _requesters;

		// Token: 0x04001719 RID: 5913
		private MissionMultiplayerDuel _missionMultiplayerDuel;

		// Token: 0x0400171A RID: 5914
		private IFocusable _focusedObject;
	}
}
