using System;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.MissionRepresentatives;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002B3 RID: 691
	public class MissionMultiplayerTeamDeathmatchClient : MissionMultiplayerGameModeBaseClient
	{
		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06002717 RID: 10007 RVA: 0x00090BCC File Offset: 0x0008EDCC
		// (remove) Token: 0x06002718 RID: 10008 RVA: 0x00090C04 File Offset: 0x0008EE04
		public event Action<GoldGain> OnGoldGainEvent;

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x00090C39 File Offset: 0x0008EE39
		public override bool IsGameModeUsingGold
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x0600271A RID: 10010 RVA: 0x00090C3C File Offset: 0x0008EE3C
		public override bool IsGameModeTactical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x00090C3F File Offset: 0x0008EE3F
		public override bool IsGameModeUsingRoundCountdown
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x00090C42 File Offset: 0x0008EE42
		public override MultiplayerGameType GameType
		{
			get
			{
				return MultiplayerGameType.TeamDeathmatch;
			}
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x00090C45 File Offset: 0x0008EE45
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			base.MissionNetworkComponent.OnMyClientSynchronized += this.OnMyClientSynchronized;
			base.ScoreboardComponent.OnRoundPropertiesChanged += this.OnTeamScoresChanged;
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x00090C7B File Offset: 0x0008EE7B
		public override void OnGoldAmountChangedForRepresentative(MissionRepresentativeBase representative, int goldAmount)
		{
			if (representative != null && base.MissionLobbyComponent.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending)
			{
				representative.UpdateGold(goldAmount);
				base.ScoreboardComponent.PlayerPropertiesChanged(representative.MissionPeer);
			}
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x00090CA6 File Offset: 0x0008EEA6
		public override void AfterStart()
		{
			base.Mission.SetMissionMode(MissionMode.Battle, true);
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x00090CB5 File Offset: 0x0008EEB5
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			if (GameNetwork.IsClient)
			{
				registerer.RegisterBaseHandler<SyncGoldsForSkirmish>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventUpdateGold));
				registerer.RegisterBaseHandler<GoldGain>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventTDMGoldGain));
			}
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x00090CE2 File Offset: 0x0008EEE2
		private void OnMyClientSynchronized()
		{
			this._myRepresentative = GameNetwork.MyPeer.GetComponent<TeamDeathmatchMissionRepresentative>();
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x00090CF4 File Offset: 0x0008EEF4
		private void HandleServerEventUpdateGold(GameNetworkMessage baseMessage)
		{
			SyncGoldsForSkirmish syncGoldsForSkirmish = (SyncGoldsForSkirmish)baseMessage;
			MissionRepresentativeBase component = syncGoldsForSkirmish.VirtualPlayer.GetComponent<MissionRepresentativeBase>();
			this.OnGoldAmountChangedForRepresentative(component, syncGoldsForSkirmish.GoldAmount);
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x00090D24 File Offset: 0x0008EF24
		private void HandleServerEventTDMGoldGain(GameNetworkMessage baseMessage)
		{
			GoldGain goldGain = (GoldGain)baseMessage;
			Action<GoldGain> onGoldGainEvent = this.OnGoldGainEvent;
			if (onGoldGainEvent == null)
			{
				return;
			}
			onGoldGainEvent(goldGain);
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x00090D49 File Offset: 0x0008EF49
		public override int GetGoldAmount()
		{
			return this._myRepresentative.Gold;
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x00090D56 File Offset: 0x0008EF56
		public override void OnRemoveBehavior()
		{
			base.MissionNetworkComponent.OnMyClientSynchronized -= this.OnMyClientSynchronized;
			base.ScoreboardComponent.OnRoundPropertiesChanged -= this.OnTeamScoresChanged;
			base.OnRemoveBehavior();
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x00090D8C File Offset: 0x0008EF8C
		private void OnTeamScoresChanged()
		{
			if (!GameNetwork.IsDedicatedServer && !this._battleEndingNotificationGiven && this._myRepresentative.MissionPeer.Team != null && this._myRepresentative.MissionPeer.Team.Side != BattleSideEnum.None)
			{
				int intValue = MultiplayerOptions.OptionType.MinScoreToWinMatch.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
				float num = (float)(intValue - base.ScoreboardComponent.GetRoundScore(this._myRepresentative.MissionPeer.Team.Side)) / (float)intValue;
				float num2 = (float)(intValue - base.ScoreboardComponent.GetRoundScore(this._myRepresentative.MissionPeer.Team.Side.GetOppositeSide())) / (float)intValue;
				MatrixFrame cameraFrame = Mission.Current.GetCameraFrame();
				Vec3 vec = cameraFrame.origin + cameraFrame.rotation.u;
				if (num <= 0.1f && num2 > 0.1f)
				{
					MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/alerts/report/battle_winning"), vec);
					this._battleEndingNotificationGiven = true;
				}
				if (num2 <= 0.1f && num > 0.1f)
				{
					MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/alerts/report/battle_losing"), vec);
					this._battleEndingNotificationGiven = true;
				}
			}
		}

		// Token: 0x04000ED4 RID: 3796
		private const string BattleWinningSoundEventString = "event:/alerts/report/battle_winning";

		// Token: 0x04000ED5 RID: 3797
		private const string BattleLosingSoundEventString = "event:/alerts/report/battle_losing";

		// Token: 0x04000ED6 RID: 3798
		private const float BattleWinLoseAlertThreshold = 0.1f;

		// Token: 0x04000ED8 RID: 3800
		private TeamDeathmatchMissionRepresentative _myRepresentative;

		// Token: 0x04000ED9 RID: 3801
		private bool _battleEndingNotificationGiven;
	}
}
