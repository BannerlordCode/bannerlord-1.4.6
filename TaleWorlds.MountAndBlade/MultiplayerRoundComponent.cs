using System;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002BD RID: 701
	public class MultiplayerRoundComponent : MissionNetwork, IRoundComponent, IMissionBehavior
	{
		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06002822 RID: 10274 RVA: 0x00098238 File Offset: 0x00096438
		// (remove) Token: 0x06002823 RID: 10275 RVA: 0x00098270 File Offset: 0x00096470
		public event Action OnRoundStarted;

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x06002824 RID: 10276 RVA: 0x000982A8 File Offset: 0x000964A8
		// (remove) Token: 0x06002825 RID: 10277 RVA: 0x000982E0 File Offset: 0x000964E0
		public event Action OnPreparationEnded;

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06002826 RID: 10278 RVA: 0x00098318 File Offset: 0x00096518
		// (remove) Token: 0x06002827 RID: 10279 RVA: 0x00098350 File Offset: 0x00096550
		public event Action OnPreRoundEnding;

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06002828 RID: 10280 RVA: 0x00098388 File Offset: 0x00096588
		// (remove) Token: 0x06002829 RID: 10281 RVA: 0x000983C0 File Offset: 0x000965C0
		public event Action OnRoundEnding;

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x0600282A RID: 10282 RVA: 0x000983F8 File Offset: 0x000965F8
		// (remove) Token: 0x0600282B RID: 10283 RVA: 0x00098430 File Offset: 0x00096630
		public event Action OnPostRoundEnded;

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x0600282C RID: 10284 RVA: 0x00098468 File Offset: 0x00096668
		// (remove) Token: 0x0600282D RID: 10285 RVA: 0x000984A0 File Offset: 0x000966A0
		public event Action OnCurrentRoundStateChanged;

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x000984D5 File Offset: 0x000966D5
		public float RemainingRoundTime
		{
			get
			{
				return this._gameModeClient.TimerComponent.GetRemainingTime(true);
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x0600282F RID: 10287 RVA: 0x000984E8 File Offset: 0x000966E8
		// (set) Token: 0x06002830 RID: 10288 RVA: 0x000984F0 File Offset: 0x000966F0
		public float LastRoundEndRemainingTime { get; private set; }

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002831 RID: 10289 RVA: 0x000984F9 File Offset: 0x000966F9
		// (set) Token: 0x06002832 RID: 10290 RVA: 0x00098501 File Offset: 0x00096701
		public MultiplayerRoundState CurrentRoundState { get; private set; }

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002833 RID: 10291 RVA: 0x0009850A File Offset: 0x0009670A
		// (set) Token: 0x06002834 RID: 10292 RVA: 0x00098512 File Offset: 0x00096712
		public int RoundCount { get; private set; }

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x0009851B File Offset: 0x0009671B
		// (set) Token: 0x06002836 RID: 10294 RVA: 0x00098523 File Offset: 0x00096723
		public BattleSideEnum RoundWinner { get; private set; }

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002837 RID: 10295 RVA: 0x0009852C File Offset: 0x0009672C
		// (set) Token: 0x06002838 RID: 10296 RVA: 0x00098534 File Offset: 0x00096734
		public RoundEndReason RoundEndReason { get; private set; }

		// Token: 0x06002839 RID: 10297 RVA: 0x0009853D File Offset: 0x0009673D
		public override void AfterStart()
		{
			this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add);
			this._gameModeClient = Mission.Current.GetMissionBehavior<MissionMultiplayerGameModeBaseClient>();
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x00098556 File Offset: 0x00096756
		protected override void OnUdpNetworkHandlerClose()
		{
			this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Remove);
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x00098560 File Offset: 0x00096760
		private void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode mode)
		{
			GameNetwork.NetworkMessageHandlerRegisterer networkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegisterer(mode);
			if (GameNetwork.IsClient)
			{
				networkMessageHandlerRegisterer.Register<RoundStateChange>(new GameNetworkMessage.ServerMessageHandlerDelegate<RoundStateChange>(this.HandleServerEventChangeRoundState));
				networkMessageHandlerRegisterer.Register<RoundCountChange>(new GameNetworkMessage.ServerMessageHandlerDelegate<RoundCountChange>(this.HandleServerEventRoundCountChange));
				networkMessageHandlerRegisterer.Register<RoundWinnerChange>(new GameNetworkMessage.ServerMessageHandlerDelegate<RoundWinnerChange>(this.HandleServerEventRoundWinnerChange));
				networkMessageHandlerRegisterer.Register<RoundEndReasonChange>(new GameNetworkMessage.ServerMessageHandlerDelegate<RoundEndReasonChange>(this.HandleServerEventRoundEndReasonChange));
			}
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x000985C4 File Offset: 0x000967C4
		private void HandleServerEventChangeRoundState(RoundStateChange message)
		{
			if (this.CurrentRoundState == MultiplayerRoundState.InProgress)
			{
				this.LastRoundEndRemainingTime = (float)message.RemainingTimeOnPreviousState;
			}
			this.CurrentRoundState = message.RoundState;
			switch (this.CurrentRoundState)
			{
			case MultiplayerRoundState.Preparation:
				this._gameModeClient.TimerComponent.StartTimerAsClient(message.StateStartTimeInSeconds, (float)MultiplayerOptions.OptionType.RoundPreparationTimeLimit.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
				if (this.OnRoundStarted != null)
				{
					this.OnRoundStarted();
				}
				break;
			case MultiplayerRoundState.InProgress:
				this._gameModeClient.TimerComponent.StartTimerAsClient(message.StateStartTimeInSeconds, (float)MultiplayerOptions.OptionType.RoundTimeLimit.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
				if (this.OnPreparationEnded != null)
				{
					this.OnPreparationEnded();
				}
				break;
			case MultiplayerRoundState.Ending:
				this._gameModeClient.TimerComponent.StartTimerAsClient(message.StateStartTimeInSeconds, 3f);
				if (this.OnPreRoundEnding != null)
				{
					this.OnPreRoundEnding();
				}
				if (this.OnRoundEnding != null)
				{
					this.OnRoundEnding();
				}
				break;
			case MultiplayerRoundState.Ended:
				this._gameModeClient.TimerComponent.StartTimerAsClient(message.StateStartTimeInSeconds, 5f);
				if (this.OnPostRoundEnded != null)
				{
					this.OnPostRoundEnded();
				}
				break;
			case MultiplayerRoundState.MatchEnded:
				this._gameModeClient.TimerComponent.StartTimerAsClient(message.StateStartTimeInSeconds, 5f);
				break;
			}
			Action onCurrentRoundStateChanged = this.OnCurrentRoundStateChanged;
			if (onCurrentRoundStateChanged == null)
			{
				return;
			}
			onCurrentRoundStateChanged();
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x0009872D File Offset: 0x0009692D
		private void HandleServerEventRoundCountChange(RoundCountChange message)
		{
			this.RoundCount = message.RoundCount;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x0009873B File Offset: 0x0009693B
		private void HandleServerEventRoundWinnerChange(RoundWinnerChange message)
		{
			this.RoundWinner = message.RoundWinner;
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x00098749 File Offset: 0x00096949
		private void HandleServerEventRoundEndReasonChange(RoundEndReasonChange message)
		{
			this.RoundEndReason = message.RoundEndReason;
		}

		// Token: 0x04000F59 RID: 3929
		public const int RoundEndDelayTime = 3;

		// Token: 0x04000F5A RID: 3930
		public const int RoundEndWaitTime = 8;

		// Token: 0x04000F5B RID: 3931
		public const int MatchEndWaitTime = 5;

		// Token: 0x04000F5C RID: 3932
		public const int WarmupEndWaitTime = 30;

		// Token: 0x04000F63 RID: 3939
		private MissionMultiplayerGameModeBaseClient _gameModeClient;
	}
}
