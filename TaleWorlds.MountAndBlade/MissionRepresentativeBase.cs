using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002CC RID: 716
	public abstract class MissionRepresentativeBase : PeerComponent
	{
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x0600296B RID: 10603 RVA: 0x0009BB41 File Offset: 0x00099D41
		protected MissionRepresentativeBase.PlayerTypes PlayerType
		{
			get
			{
				if (!base.Peer.Communicator.IsNetworkActive)
				{
					return MissionRepresentativeBase.PlayerTypes.Bot;
				}
				if (!base.Peer.Communicator.IsServerPeer)
				{
					return MissionRepresentativeBase.PlayerTypes.Client;
				}
				return MissionRepresentativeBase.PlayerTypes.Server;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x0600296C RID: 10604 RVA: 0x0009BB6C File Offset: 0x00099D6C
		// (set) Token: 0x0600296D RID: 10605 RVA: 0x0009BB74 File Offset: 0x00099D74
		public Agent ControlledAgent { get; private set; }

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x0600296E RID: 10606 RVA: 0x0009BB80 File Offset: 0x00099D80
		// (set) Token: 0x0600296F RID: 10607 RVA: 0x0009BBC0 File Offset: 0x00099DC0
		public int Gold
		{
			get
			{
				if (this._gold < 0)
				{
					return this._gold;
				}
				bool flag;
				MultiplayerOptions.Instance.GetOptionFromOptionType(MultiplayerOptions.OptionType.UnlimitedGold, MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions).GetValue(out flag);
				if (!flag)
				{
					return this._gold;
				}
				return 2000;
			}
			private set
			{
				if (value < 0)
				{
					this._gold = value;
					return;
				}
				bool flag;
				MultiplayerOptions.Instance.GetOptionFromOptionType(MultiplayerOptions.OptionType.UnlimitedGold, MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions).GetValue(out flag);
				this._gold = ((!flag) ? value : 2000);
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002970 RID: 10608 RVA: 0x0009BBFE File Offset: 0x00099DFE
		public MissionPeer MissionPeer
		{
			get
			{
				if (this._missionPeer == null)
				{
					this._missionPeer = base.GetComponent<MissionPeer>();
				}
				return this._missionPeer;
			}
		}

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x06002971 RID: 10609 RVA: 0x0009BC1C File Offset: 0x00099E1C
		// (remove) Token: 0x06002972 RID: 10610 RVA: 0x0009BC54 File Offset: 0x00099E54
		public event Action OnGoldUpdated;

		// Token: 0x06002974 RID: 10612 RVA: 0x0009BC91 File Offset: 0x00099E91
		public void SetAgent(Agent agent)
		{
			this.ControlledAgent = agent;
			if (this.ControlledAgent != null)
			{
				this.ControlledAgent.SetMissionRepresentative(this);
				this.OnAgentSpawned();
			}
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x0009BCB4 File Offset: 0x00099EB4
		public virtual void OnAgentSpawned()
		{
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x0009BCB6 File Offset: 0x00099EB6
		public virtual void Tick(float dt)
		{
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x0009BCB8 File Offset: 0x00099EB8
		public void UpdateGold(int gold)
		{
			this.Gold = gold;
			Action onGoldUpdated = this.OnGoldUpdated;
			if (onGoldUpdated == null)
			{
				return;
			}
			onGoldUpdated();
		}

		// Token: 0x04000FE3 RID: 4067
		private int _gold;

		// Token: 0x04000FE4 RID: 4068
		private MissionPeer _missionPeer;

		// Token: 0x020005B0 RID: 1456
		protected enum PlayerTypes
		{
			// Token: 0x04001EEB RID: 7915
			Bot,
			// Token: 0x04001EEC RID: 7916
			Client,
			// Token: 0x04001EED RID: 7917
			Server
		}
	}
}
