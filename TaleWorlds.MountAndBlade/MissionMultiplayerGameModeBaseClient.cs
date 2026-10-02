using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002AF RID: 687
	public abstract class MissionMultiplayerGameModeBaseClient : MissionNetwork, ICameraModeLogic
	{
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x0008E977 File Offset: 0x0008CB77
		// (set) Token: 0x0600268B RID: 9867 RVA: 0x0008E97F File Offset: 0x0008CB7F
		public MissionLobbyComponent MissionLobbyComponent { get; private set; }

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x0008E988 File Offset: 0x0008CB88
		// (set) Token: 0x0600268D RID: 9869 RVA: 0x0008E990 File Offset: 0x0008CB90
		public MissionNetworkComponent MissionNetworkComponent { get; private set; }

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x0008E999 File Offset: 0x0008CB99
		// (set) Token: 0x0600268F RID: 9871 RVA: 0x0008E9A1 File Offset: 0x0008CBA1
		public MissionScoreboardComponent ScoreboardComponent { get; private set; }

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x0008E9AA File Offset: 0x0008CBAA
		// (set) Token: 0x06002691 RID: 9873 RVA: 0x0008E9B2 File Offset: 0x0008CBB2
		public MultiplayerGameNotificationsComponent NotificationsComponent { get; private set; }

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x0008E9BB File Offset: 0x0008CBBB
		// (set) Token: 0x06002693 RID: 9875 RVA: 0x0008E9C3 File Offset: 0x0008CBC3
		public MultiplayerWarmupComponent WarmupComponent { get; private set; }

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x0008E9CC File Offset: 0x0008CBCC
		// (set) Token: 0x06002695 RID: 9877 RVA: 0x0008E9D4 File Offset: 0x0008CBD4
		public IRoundComponent RoundComponent { get; private set; }

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x0008E9DD File Offset: 0x0008CBDD
		// (set) Token: 0x06002697 RID: 9879 RVA: 0x0008E9E5 File Offset: 0x0008CBE5
		public MultiplayerTimerComponent TimerComponent { get; private set; }

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002698 RID: 9880
		public abstract bool IsGameModeUsingGold { get; }

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002699 RID: 9881
		public abstract bool IsGameModeTactical { get; }

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x0008E9EE File Offset: 0x0008CBEE
		public virtual bool IsGameModeUsingCasualGold
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x0600269B RID: 9883
		public abstract bool IsGameModeUsingRoundCountdown { get; }

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x0008E9F1 File Offset: 0x0008CBF1
		public virtual bool IsGameModeUsingAllowCultureChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600269D RID: 9885 RVA: 0x0008E9F4 File Offset: 0x0008CBF4
		public virtual bool IsGameModeUsingAllowTroopChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600269E RID: 9886
		public abstract MultiplayerGameType GameType { get; }

		// Token: 0x0600269F RID: 9887
		public abstract int GetGoldAmount();

		// Token: 0x060026A0 RID: 9888 RVA: 0x0008E9F7 File Offset: 0x0008CBF7
		public virtual SpectatorCameraTypes GetMissionCameraLockMode(bool lockedToMainPlayer)
		{
			return SpectatorCameraTypes.Invalid;
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x060026A1 RID: 9889 RVA: 0x0008E9FA File Offset: 0x0008CBFA
		public bool IsRoundInProgress
		{
			get
			{
				IRoundComponent roundComponent = this.RoundComponent;
				return roundComponent != null && roundComponent.CurrentRoundState == MultiplayerRoundState.InProgress;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060026A2 RID: 9890 RVA: 0x0008EA10 File Offset: 0x0008CC10
		public bool IsInWarmup
		{
			get
			{
				return this.MissionLobbyComponent.IsInWarmup;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060026A3 RID: 9891 RVA: 0x0008EA1D File Offset: 0x0008CC1D
		public float RemainingTime
		{
			get
			{
				return this.TimerComponent.GetRemainingTime(GameNetwork.IsClientOrReplay);
			}
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x0008EA30 File Offset: 0x0008CC30
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this.MissionLobbyComponent = base.Mission.GetMissionBehavior<MissionLobbyComponent>();
			this.MissionNetworkComponent = base.Mission.GetMissionBehavior<MissionNetworkComponent>();
			this.ScoreboardComponent = base.Mission.GetMissionBehavior<MissionScoreboardComponent>();
			this.NotificationsComponent = base.Mission.GetMissionBehavior<MultiplayerGameNotificationsComponent>();
			this.WarmupComponent = base.Mission.GetMissionBehavior<MultiplayerWarmupComponent>();
			this.RoundComponent = base.Mission.GetMissionBehavior<IRoundComponent>();
			this.TimerComponent = base.Mission.GetMissionBehavior<MultiplayerTimerComponent>();
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x0008EABA File Offset: 0x0008CCBA
		public override void EarlyStart()
		{
			this.MissionLobbyComponent.MissionType = this.GameType;
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x0008EAD0 File Offset: 0x0008CCD0
		public bool CheckTimer(out int remainingTime, out int remainingWarningTime, bool forceUpdate = false)
		{
			bool flag = false;
			float num = 0f;
			if (this.WarmupComponent != null && this.MissionLobbyComponent.CurrentMultiplayerState == MissionLobbyComponent.MultiplayerGameState.WaitingFirstPlayers)
			{
				flag = !this.WarmupComponent.IsInWarmup;
			}
			else if (this.RoundComponent != null)
			{
				flag = !this.RoundComponent.CurrentRoundState.StateHasVisualTimer();
				num = this.RoundComponent.LastRoundEndRemainingTime;
			}
			if (forceUpdate || !flag)
			{
				if (flag)
				{
					remainingTime = MathF.Ceiling(num);
				}
				else
				{
					remainingTime = MathF.Ceiling(this.RemainingTime);
				}
				remainingWarningTime = this.GetWarningTimer();
				return true;
			}
			remainingTime = 0;
			remainingWarningTime = 0;
			return false;
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x0008EB64 File Offset: 0x0008CD64
		protected virtual int GetWarningTimer()
		{
			return 0;
		}

		// Token: 0x060026A8 RID: 9896
		public abstract void OnGoldAmountChangedForRepresentative(MissionRepresentativeBase representative, int goldAmount);

		// Token: 0x060026A9 RID: 9897 RVA: 0x0008EB67 File Offset: 0x0008CD67
		public virtual bool CanRequestTroopChange()
		{
			return false;
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x0008EB6A File Offset: 0x0008CD6A
		public virtual bool CanRequestCultureChange()
		{
			return false;
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x0008EB70 File Offset: 0x0008CD70
		public bool IsClassAvailable(MultiplayerClassDivisions.MPHeroClass heroClass)
		{
			FormationClass formationClass;
			if (Enum.TryParse<FormationClass>(heroClass.ClassGroup.StringId, out formationClass))
			{
				return this.MissionLobbyComponent.IsClassAvailable(formationClass);
			}
			Debug.FailedAssert("\"" + heroClass.ClassGroup.StringId + "\" does not match with any FormationClass.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\MissionNetworkLogics\\MultiplayerGameModeLogics\\ClientGameModeLogics\\MissionMultiplayerGameModeBaseClient.cs", "IsClassAvailable", 116);
			return false;
		}
	}
}
