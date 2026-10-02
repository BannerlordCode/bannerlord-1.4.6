using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000304 RID: 772
	public class MissionPeer : PeerComponent
	{
		// Token: 0x1400008D RID: 141
		// (add) Token: 0x06002BD6 RID: 11222 RVA: 0x000A8570 File Offset: 0x000A6770
		// (remove) Token: 0x06002BD7 RID: 11223 RVA: 0x000A85A4 File Offset: 0x000A67A4
		public static event MissionPeer.OnUpdateEquipmentSetIndexEventDelegate OnEquipmentIndexRefreshed;

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x06002BD8 RID: 11224 RVA: 0x000A85D8 File Offset: 0x000A67D8
		// (remove) Token: 0x06002BD9 RID: 11225 RVA: 0x000A860C File Offset: 0x000A680C
		public static event MissionPeer.OnPerkUpdateEventDelegate OnPerkSelectionUpdated;

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06002BDA RID: 11226 RVA: 0x000A8640 File Offset: 0x000A6840
		// (remove) Token: 0x06002BDB RID: 11227 RVA: 0x000A8674 File Offset: 0x000A6874
		public static event MissionPeer.OnTeamChangedDelegate OnPreTeamChanged;

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x06002BDC RID: 11228 RVA: 0x000A86A8 File Offset: 0x000A68A8
		// (remove) Token: 0x06002BDD RID: 11229 RVA: 0x000A86DC File Offset: 0x000A68DC
		public static event MissionPeer.OnTeamChangedDelegate OnTeamChanged;

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x06002BDE RID: 11230 RVA: 0x000A8710 File Offset: 0x000A6910
		// (remove) Token: 0x06002BDF RID: 11231 RVA: 0x000A8748 File Offset: 0x000A6948
		private event MissionPeer.OnCultureChangedDelegate OnCultureChanged;

		// Token: 0x14000092 RID: 146
		// (add) Token: 0x06002BE0 RID: 11232 RVA: 0x000A8780 File Offset: 0x000A6980
		// (remove) Token: 0x06002BE1 RID: 11233 RVA: 0x000A87B4 File Offset: 0x000A69B4
		public static event MissionPeer.OnPlayerKilledDelegate OnPlayerKilled;

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002BE2 RID: 11234 RVA: 0x000A87E7 File Offset: 0x000A69E7
		// (set) Token: 0x06002BE3 RID: 11235 RVA: 0x000A87EF File Offset: 0x000A69EF
		public DateTime JoinTime { get; internal set; }

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x000A87F8 File Offset: 0x000A69F8
		// (set) Token: 0x06002BE5 RID: 11237 RVA: 0x000A8800 File Offset: 0x000A6A00
		public bool EquipmentUpdatingExpired { get; set; }

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x000A8809 File Offset: 0x000A6A09
		// (set) Token: 0x06002BE7 RID: 11239 RVA: 0x000A8811 File Offset: 0x000A6A11
		public bool TeamInitialPerkInfoReady { get; private set; }

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002BE8 RID: 11240 RVA: 0x000A881A File Offset: 0x000A6A1A
		// (set) Token: 0x06002BE9 RID: 11241 RVA: 0x000A8822 File Offset: 0x000A6A22
		public bool HasSpawnedAgentVisuals { get; set; }

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000A882B File Offset: 0x000A6A2B
		// (set) Token: 0x06002BEB RID: 11243 RVA: 0x000A8833 File Offset: 0x000A6A33
		public int SelectedTroopIndex
		{
			get
			{
				return this._selectedTroopIndex;
			}
			set
			{
				if (this._selectedTroopIndex != value)
				{
					this._selectedTroopIndex = value;
					this.ResetSelectedPerks();
					MissionPeer.OnUpdateEquipmentSetIndexEventDelegate onEquipmentIndexRefreshed = MissionPeer.OnEquipmentIndexRefreshed;
					if (onEquipmentIndexRefreshed == null)
					{
						return;
					}
					onEquipmentIndexRefreshed(this, value);
				}
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x000A885C File Offset: 0x000A6A5C
		// (set) Token: 0x06002BED RID: 11245 RVA: 0x000A8864 File Offset: 0x000A6A64
		public int NextSelectedTroopIndex { get; set; }

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x000A886D File Offset: 0x000A6A6D
		public MissionRepresentativeBase Representative
		{
			get
			{
				if (this._representative == null)
				{
					this._representative = base.Peer.GetComponent<MissionRepresentativeBase>();
				}
				return this._representative;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002BEF RID: 11247 RVA: 0x000A888E File Offset: 0x000A6A8E
		public MBReadOnlyList<int[]> Perks
		{
			get
			{
				return this._perks;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002BF0 RID: 11248 RVA: 0x000A8898 File Offset: 0x000A6A98
		public string DisplayedName
		{
			get
			{
				if (GameNetwork.IsDedicatedServer)
				{
					return base.Name;
				}
				if (NetworkMain.CommunityClient.IsInGame)
				{
					return base.Name;
				}
				if (NetworkMain.GameClient.HasUserGeneratedContentPrivilege && (NetworkMain.GameClient.IsKnownPlayer(base.Peer.Id) || !BannerlordConfig.EnableGenericNames))
				{
					VirtualPlayer peer = base.Peer;
					return ((peer != null) ? peer.UserName : null) ?? "";
				}
				if (this.Culture == null || MultiplayerClassDivisions.GetMPHeroClassForPeer(this, false) == null)
				{
					return new TextObject("{=RN6zHak0}Player", null).ToString();
				}
				return MultiplayerClassDivisions.GetMPHeroClassForPeer(this, false).TroopName.ToString();
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06002BF1 RID: 11249 RVA: 0x000A8940 File Offset: 0x000A6B40
		public MBReadOnlyList<MPPerkObject> SelectedPerks
		{
			get
			{
				if (this.SelectedTroopIndex < 0 || this.Team == null || this.Team.Side == BattleSideEnum.None)
				{
					return new MBList<MPPerkObject>();
				}
				if ((this._selectedPerks.Item2 == null || this.SelectedTroopIndex != this._selectedPerks.Item1 || this._selectedPerks.Item2.Count < 3) && !this.RefreshSelectedPerks())
				{
					return new MBReadOnlyList<MPPerkObject>();
				}
				return this._selectedPerks.Item2;
			}
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000A89C0 File Offset: 0x000A6BC0
		public MissionPeer()
		{
			this.SpawnTimer = new Timer(Mission.Current.CurrentTime, 3f, false);
			this._selectedPerks = new ValueTuple<int, MBList<MPPerkObject>>(0, null);
			this._perks = new MBList<int[]>();
			for (int i = 0; i < 16; i++)
			{
				int[] array = new int[3];
				this._perks.Add(array);
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06002BF3 RID: 11251 RVA: 0x000A8A4F File Offset: 0x000A6C4F
		// (set) Token: 0x06002BF4 RID: 11252 RVA: 0x000A8A57 File Offset: 0x000A6C57
		public Timer SpawnTimer { get; internal set; }

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06002BF5 RID: 11253 RVA: 0x000A8A60 File Offset: 0x000A6C60
		// (set) Token: 0x06002BF6 RID: 11254 RVA: 0x000A8A68 File Offset: 0x000A6C68
		public bool HasSpawnTimerExpired { get; set; }

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x000A8A71 File Offset: 0x000A6C71
		// (set) Token: 0x06002BF8 RID: 11256 RVA: 0x000A8A79 File Offset: 0x000A6C79
		public BasicCultureObject VotedForBan { get; private set; }

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002BF9 RID: 11257 RVA: 0x000A8A82 File Offset: 0x000A6C82
		// (set) Token: 0x06002BFA RID: 11258 RVA: 0x000A8A8A File Offset: 0x000A6C8A
		public BasicCultureObject VotedForSelection { get; private set; }

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06002BFB RID: 11259 RVA: 0x000A8A93 File Offset: 0x000A6C93
		// (set) Token: 0x06002BFC RID: 11260 RVA: 0x000A8A9B File Offset: 0x000A6C9B
		public bool WantsToSpawnAsBot { get; set; }

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x000A8AA4 File Offset: 0x000A6CA4
		// (set) Token: 0x06002BFE RID: 11262 RVA: 0x000A8AAC File Offset: 0x000A6CAC
		public int SpawnCountThisRound { get; set; }

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x000A8AB5 File Offset: 0x000A6CB5
		// (set) Token: 0x06002C00 RID: 11264 RVA: 0x000A8ABD File Offset: 0x000A6CBD
		public int RequestedKickPollCount { get; private set; }

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002C01 RID: 11265 RVA: 0x000A8AC6 File Offset: 0x000A6CC6
		// (set) Token: 0x06002C02 RID: 11266 RVA: 0x000A8ACE File Offset: 0x000A6CCE
		public int KillCount
		{
			get
			{
				return this._killCount;
			}
			internal set
			{
				this._killCount = MBMath.ClampInt(value, -1000, 100000);
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x000A8AE6 File Offset: 0x000A6CE6
		// (set) Token: 0x06002C04 RID: 11268 RVA: 0x000A8AEE File Offset: 0x000A6CEE
		public int AssistCount
		{
			get
			{
				return this._assistCount;
			}
			internal set
			{
				this._assistCount = MBMath.ClampInt(value, -1000, 100000);
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x000A8B06 File Offset: 0x000A6D06
		// (set) Token: 0x06002C06 RID: 11270 RVA: 0x000A8B0E File Offset: 0x000A6D0E
		public int DeathCount
		{
			get
			{
				return this._deathCount;
			}
			internal set
			{
				this._deathCount = MBMath.ClampInt(value, -1000, 100000);
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x000A8B26 File Offset: 0x000A6D26
		// (set) Token: 0x06002C08 RID: 11272 RVA: 0x000A8B2E File Offset: 0x000A6D2E
		public int Score
		{
			get
			{
				return this._score;
			}
			internal set
			{
				this._score = MBMath.ClampInt(value, -1000000, 1000000);
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x000A8B46 File Offset: 0x000A6D46
		// (set) Token: 0x06002C0A RID: 11274 RVA: 0x000A8B4E File Offset: 0x000A6D4E
		public int BotsUnderControlAlive
		{
			get
			{
				return this._botsUnderControlAlive;
			}
			set
			{
				if (this._botsUnderControlAlive != value)
				{
					this._botsUnderControlAlive = value;
					MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(this);
					if (perkHandler == null)
					{
						return;
					}
					perkHandler.OnEvent(MPPerkCondition.PerkEventFlags.AliveBotCountChange);
				}
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06002C0B RID: 11275 RVA: 0x000A8B72 File Offset: 0x000A6D72
		// (set) Token: 0x06002C0C RID: 11276 RVA: 0x000A8B7A File Offset: 0x000A6D7A
		public int BotsUnderControlTotal { get; internal set; }

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002C0D RID: 11277 RVA: 0x000A8B83 File Offset: 0x000A6D83
		public bool IsControlledAgentActive
		{
			get
			{
				return this.ControlledAgent != null && this.ControlledAgent.IsActive();
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002C0E RID: 11278 RVA: 0x000A8B9A File Offset: 0x000A6D9A
		// (set) Token: 0x06002C0F RID: 11279 RVA: 0x000A8BA8 File Offset: 0x000A6DA8
		public Agent ControlledAgent
		{
			get
			{
				return this.GetNetworkPeer().ControlledAgent;
			}
			set
			{
				NetworkCommunicator networkPeer = this.GetNetworkPeer();
				if (networkPeer.ControlledAgent != value)
				{
					this.ResetSelectedPerks();
					Agent controlledAgent = networkPeer.ControlledAgent;
					networkPeer.ControlledAgent = value;
					if (controlledAgent != null && controlledAgent.MissionPeer == this && controlledAgent.IsActive())
					{
						controlledAgent.MissionPeer = null;
					}
					if (networkPeer.ControlledAgent != null && networkPeer.ControlledAgent.MissionPeer != this)
					{
						networkPeer.ControlledAgent.MissionPeer = this;
					}
					MissionRepresentativeBase component = networkPeer.VirtualPlayer.GetComponent<MissionRepresentativeBase>();
					if (component != null)
					{
						component.SetAgent(value);
					}
					if (value != null)
					{
						MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(this);
						if (perkHandler == null)
						{
							return;
						}
						perkHandler.OnEvent(value, MPPerkCondition.PerkEventFlags.PeerControlledAgentChange);
					}
				}
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000A8C42 File Offset: 0x000A6E42
		// (set) Token: 0x06002C11 RID: 11281 RVA: 0x000A8C4A File Offset: 0x000A6E4A
		public Agent FollowedAgent
		{
			get
			{
				return this._followedAgent;
			}
			set
			{
				if (this._followedAgent != value)
				{
					this._followedAgent = value;
					if (GameNetwork.IsClient)
					{
						GameNetwork.BeginModuleEventAsClient();
						Agent followedAgent = this._followedAgent;
						GameNetwork.WriteMessage(new SetFollowedAgent((followedAgent != null) ? followedAgent.Index : (-1)));
						GameNetwork.EndModuleEventAsClient();
					}
				}
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06002C12 RID: 11282 RVA: 0x000A8C89 File Offset: 0x000A6E89
		// (set) Token: 0x06002C13 RID: 11283 RVA: 0x000A8C94 File Offset: 0x000A6E94
		public Team Team
		{
			get
			{
				return this._team;
			}
			set
			{
				if (this._team != value)
				{
					if (MissionPeer.OnPreTeamChanged != null)
					{
						MissionPeer.OnPreTeamChanged(this.GetNetworkPeer(), this._team, value);
					}
					Team team = this._team;
					this._team = value;
					string text = "Set the team to: ";
					Team team2 = this._team;
					Debug.Print(text + (((team2 != null) ? team2.Side.ToString() : null) ?? "null") + ", for peer: " + base.Name, 0, Debug.DebugColor.White, 17592186044416UL);
					this._controlledFormation = null;
					if (this._team != null)
					{
						if (GameNetwork.IsServer)
						{
							MBAPI.IMBPeer.SetTeam(base.Peer.Index, this._team.MBTeam.Index);
							GameNetwork.BeginBroadcastModuleEvent();
							GameNetwork.WriteMessage(new SetPeerTeam(this.GetNetworkPeer(), this._team.TeamIndex));
							GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
						}
						if (MissionPeer.OnTeamChanged != null)
						{
							MissionPeer.OnTeamChanged(this.GetNetworkPeer(), team, this._team);
							return;
						}
					}
					else if (GameNetwork.IsServer)
					{
						MBAPI.IMBPeer.SetTeam(base.Peer.Index, -1);
					}
				}
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x000A8DC7 File Offset: 0x000A6FC7
		// (set) Token: 0x06002C15 RID: 11285 RVA: 0x000A8DD0 File Offset: 0x000A6FD0
		public BasicCultureObject Culture
		{
			get
			{
				return this._culture;
			}
			set
			{
				BasicCultureObject culture = this._culture;
				this._culture = value;
				if (GameNetwork.IsServerOrRecorder)
				{
					this.TeamInitialPerkInfoReady = base.Peer.IsMine;
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new ChangeCulture(this, this._culture));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				if (this.OnCultureChanged != null)
				{
					this.OnCultureChanged(this._culture);
				}
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x000A8E3A File Offset: 0x000A703A
		// (set) Token: 0x06002C17 RID: 11287 RVA: 0x000A8E42 File Offset: 0x000A7042
		public Formation ControlledFormation
		{
			get
			{
				return this._controlledFormation;
			}
			set
			{
				if (this._controlledFormation != value)
				{
					this._controlledFormation = value;
				}
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002C18 RID: 11288 RVA: 0x000A8E54 File Offset: 0x000A7054
		public bool IsAgentAliveForChatting
		{
			get
			{
				MissionPeer component = base.GetComponent<MissionPeer>();
				return component != null && (this.IsControlledAgentActive || component.HasSpawnedAgentVisuals);
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002C19 RID: 11289 RVA: 0x000A8E7D File Offset: 0x000A707D
		// (set) Token: 0x06002C1A RID: 11290 RVA: 0x000A8E85 File Offset: 0x000A7085
		public bool IsMutedFromPlatform { get; private set; }

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x000A8E8E File Offset: 0x000A708E
		// (set) Token: 0x06002C1C RID: 11292 RVA: 0x000A8E96 File Offset: 0x000A7096
		public bool IsMuted { get; private set; }

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x000A8E9F File Offset: 0x000A709F
		public bool IsMutedFromGameOrPlatform
		{
			get
			{
				return this.IsMutedFromPlatform || this.IsMuted;
			}
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000A8EB1 File Offset: 0x000A70B1
		public void SetMutedFromPlatform(bool isMuted)
		{
			this.IsMutedFromPlatform = isMuted;
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x000A8EBA File Offset: 0x000A70BA
		public void SetMuted(bool isMuted)
		{
			this.IsMuted = isMuted;
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000A8EC3 File Offset: 0x000A70C3
		public void ResetRequestedKickPollCount()
		{
			this.RequestedKickPollCount = 0;
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x000A8ECC File Offset: 0x000A70CC
		public void IncrementRequestedKickPollCount()
		{
			int requestedKickPollCount = this.RequestedKickPollCount;
			this.RequestedKickPollCount = requestedKickPollCount + 1;
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x000A8EE9 File Offset: 0x000A70E9
		public int GetSelectedPerkIndexWithPerkListIndex(int troopIndex, int perkListIndex)
		{
			return this._perks[troopIndex][perkListIndex];
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000A8EFC File Offset: 0x000A70FC
		public bool SelectPerk(int perkListIndex, int perkIndex, int enforcedSelectedTroopIndex = -1)
		{
			if (this.SelectedTroopIndex >= 0 && enforcedSelectedTroopIndex >= 0 && this.SelectedTroopIndex != enforcedSelectedTroopIndex)
			{
				Debug.Print("SelectedTroopIndex < 0 || enforcedSelectedTroopIndex < 0 || SelectedTroopIndex == enforcedSelectedTroopIndex", 0, Debug.DebugColor.White, 17179869184UL);
				Debug.Print(string.Format("SelectedTroopIndex: {0} enforcedSelectedTroopIndex: {1}", this.SelectedTroopIndex, enforcedSelectedTroopIndex), 0, Debug.DebugColor.White, 17179869184UL);
			}
			int num = ((enforcedSelectedTroopIndex >= 0) ? enforcedSelectedTroopIndex : this.SelectedTroopIndex);
			if (perkIndex != this._perks[num][perkListIndex])
			{
				this._perks[num][perkListIndex] = perkIndex;
				if (this.GetNetworkPeer().IsMine)
				{
					List<MultiplayerClassDivisions.MPHeroClass> list = MultiplayerClassDivisions.GetMPHeroClasses(this.Culture).ToList<MultiplayerClassDivisions.MPHeroClass>();
					int count = list.Count;
					for (int i = 0; i < count; i++)
					{
						if (num == i)
						{
							MultiplayerClassDivisions.MPHeroClass mpheroClass = list[i];
							List<MPPerkSelectionManager.MPPerkSelection> list2 = new List<MPPerkSelectionManager.MPPerkSelection>();
							for (int j = 0; j < 3; j++)
							{
								list2.Add(new MPPerkSelectionManager.MPPerkSelection(this._perks[i][j], j));
							}
							MPPerkSelectionManager.Instance.SetSelectionsForHeroClassTemporarily(mpheroClass, list2);
							break;
						}
					}
				}
				if (num == this.SelectedTroopIndex)
				{
					this.ResetSelectedPerks();
				}
				MissionPeer.OnPerkUpdateEventDelegate onPerkSelectionUpdated = MissionPeer.OnPerkSelectionUpdated;
				if (onPerkSelectionUpdated != null)
				{
					onPerkSelectionUpdated(this);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000A9038 File Offset: 0x000A7238
		public void HandleVoteChange(CultureVoteTypes voteType, BasicCultureObject culture)
		{
			if (voteType != CultureVoteTypes.Ban)
			{
				if (voteType == CultureVoteTypes.Select)
				{
					this.VotedForSelection = culture;
				}
			}
			else
			{
				this.VotedForBan = culture;
			}
			if (GameNetwork.IsServer)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new CultureVoteServer(this.GetNetworkPeer(), voteType, (voteType == CultureVoteTypes.Ban) ? this.VotedForBan : this.VotedForSelection));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			}
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x000A9094 File Offset: 0x000A7294
		public override void OnFinalize()
		{
			base.OnFinalize();
			if (base.IsMine)
			{
				MPPerkSelectionManager.Instance.TryToApplyAndSavePendingChanges();
			}
			this.ResetKillRegistry();
			if (this.HasSpawnedAgentVisuals && Mission.Current != null)
			{
				MultiplayerMissionAgentVisualSpawnComponent missionBehavior = Mission.Current.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>();
				if (missionBehavior != null)
				{
					missionBehavior.RemoveAgentVisuals(this, false);
				}
				this.HasSpawnedAgentVisuals = false;
				this.OnCultureChanged -= this.CultureChanged;
			}
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x000A90FE File Offset: 0x000A72FE
		public override void OnInitialize()
		{
			base.OnInitialize();
			this.OnCultureChanged += this.CultureChanged;
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000A9118 File Offset: 0x000A7318
		public int GetAmountOfAgentVisualsForPeer()
		{
			return this._visuals.Count<PeerVisualsHolder>((PeerVisualsHolder v) => v != null);
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x000A9144 File Offset: 0x000A7344
		public PeerVisualsHolder GetVisuals(int visualIndex)
		{
			if (this._visuals.Count <= 0)
			{
				return null;
			}
			return this._visuals[visualIndex];
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x000A9164 File Offset: 0x000A7364
		public void ClearVisuals(int visualIndex)
		{
			if (visualIndex < this._visuals.Count && this._visuals[visualIndex] != null)
			{
				if (!GameNetwork.IsDedicatedServer)
				{
					MBAgentVisuals visuals = this._visuals[visualIndex].AgentVisuals.GetVisuals();
					visuals.ClearVisualComponents(true, true);
					visuals.ClearAllWeaponMeshes();
					visuals.Reset();
					if (this._visuals[visualIndex].MountAgentVisuals != null)
					{
						MBAgentVisuals visuals2 = this._visuals[visualIndex].MountAgentVisuals.GetVisuals();
						visuals2.ClearVisualComponents(true, true);
						visuals2.ClearAllWeaponMeshes();
						visuals2.Reset();
					}
				}
				this._visuals[visualIndex] = null;
			}
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000A920C File Offset: 0x000A740C
		public void ClearAllVisuals(bool freeResources = false)
		{
			if (this._visuals != null)
			{
				for (int i = this._visuals.Count - 1; i >= 0; i--)
				{
					if (this._visuals[i] != null)
					{
						this.ClearVisuals(i);
					}
				}
				if (freeResources)
				{
					this._visuals = null;
				}
			}
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000A9258 File Offset: 0x000A7458
		public void OnVisualsSpawned(PeerVisualsHolder visualsHolder, int visualIndex)
		{
			if (visualIndex >= this._visuals.Count)
			{
				int num = visualIndex - this._visuals.Count;
				for (int i = 0; i < num + 1; i++)
				{
					this._visuals.Add(null);
				}
			}
			this._visuals[visualIndex] = visualsHolder;
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000A92A8 File Offset: 0x000A74A8
		public IEnumerable<IAgentVisual> GetAllAgentVisualsForPeer()
		{
			int count = this.GetAmountOfAgentVisualsForPeer();
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				yield return this.GetVisuals(i).AgentVisuals;
				num = i;
			}
			yield break;
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000A92B8 File Offset: 0x000A74B8
		public IAgentVisual GetAgentVisualForPeer(int visualsIndex)
		{
			IAgentVisual agentVisual;
			return this.GetAgentVisualForPeer(visualsIndex, out agentVisual);
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000A92D0 File Offset: 0x000A74D0
		public IAgentVisual GetAgentVisualForPeer(int visualsIndex, out IAgentVisual mountAgentVisuals)
		{
			PeerVisualsHolder visuals = this.GetVisuals(visualsIndex);
			mountAgentVisuals = ((visuals != null) ? visuals.MountAgentVisuals : null);
			if (visuals == null)
			{
				return null;
			}
			return visuals.AgentVisuals;
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000A9300 File Offset: 0x000A7500
		public void TickInactivityStatus()
		{
			NetworkCommunicator networkPeer = this.GetNetworkPeer();
			if (!networkPeer.IsMine)
			{
				if (this.ControlledAgent != null && this.ControlledAgent.IsActive())
				{
					if (this._lastActiveTime == MissionTime.Zero)
					{
						this._lastActiveTime = MissionTime.Now;
						this._previousActivityStatus = ValueTuple.Create<Agent.MovementControlFlag, Vec2, Vec3>(this.ControlledAgent.MovementFlags, this.ControlledAgent.MovementInputVector, this.ControlledAgent.LookDirection);
						this._inactiveWarningGiven = false;
						return;
					}
					ValueTuple<Agent.MovementControlFlag, Vec2, Vec3> valueTuple = ValueTuple.Create<Agent.MovementControlFlag, Vec2, Vec3>(this.ControlledAgent.MovementFlags, this.ControlledAgent.MovementInputVector, this.ControlledAgent.LookDirection);
					if (this._previousActivityStatus.Item1 != valueTuple.Item1 || this._previousActivityStatus.Item2.DistanceSquared(valueTuple.Item2) > 1E-05f || this._previousActivityStatus.Item3.DistanceSquared(valueTuple.Item3) > 1E-05f)
					{
						this._lastActiveTime = MissionTime.Now;
						this._previousActivityStatus = valueTuple;
						this._inactiveWarningGiven = false;
					}
					if (this._lastActiveTime.ElapsedSeconds > 180f)
					{
						DisconnectInfo disconnectInfo = networkPeer.PlayerConnectionInfo.GetParameter<DisconnectInfo>("DisconnectInfo") ?? new DisconnectInfo();
						disconnectInfo.Type = DisconnectType.Inactivity;
						networkPeer.PlayerConnectionInfo.AddParameter("DisconnectInfo", disconnectInfo);
						GameNetwork.AddNetworkPeerToDisconnectAsServer(networkPeer);
						return;
					}
					if (this._lastActiveTime.ElapsedSeconds > 120f && !this._inactiveWarningGiven)
					{
						MultiplayerGameNotificationsComponent missionBehavior = Mission.Current.GetMissionBehavior<MultiplayerGameNotificationsComponent>();
						if (missionBehavior != null)
						{
							missionBehavior.PlayerIsInactive(this.GetNetworkPeer());
						}
						this._inactiveWarningGiven = true;
						return;
					}
				}
				else
				{
					this._lastActiveTime = MissionTime.Now;
					this._inactiveWarningGiven = false;
				}
			}
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000A94B4 File Offset: 0x000A76B4
		public void OnKillAnotherPeer(MissionPeer victimPeer)
		{
			if (victimPeer != null)
			{
				if (!this._numberOfTimesPeerKilledPerPeer.ContainsKey(victimPeer))
				{
					this._numberOfTimesPeerKilledPerPeer.Add(victimPeer, 1);
				}
				else
				{
					Dictionary<MissionPeer, int> numberOfTimesPeerKilledPerPeer = this._numberOfTimesPeerKilledPerPeer;
					int num = numberOfTimesPeerKilledPerPeer[victimPeer];
					numberOfTimesPeerKilledPerPeer[victimPeer] = num + 1;
				}
				MissionPeer.OnPlayerKilledDelegate onPlayerKilled = MissionPeer.OnPlayerKilled;
				if (onPlayerKilled == null)
				{
					return;
				}
				onPlayerKilled(this, victimPeer);
			}
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000A950C File Offset: 0x000A770C
		public void OverrideCultureWithTeamCulture()
		{
			MultiplayerOptions.OptionType optionType = ((this.Team.Side == BattleSideEnum.Attacker) ? MultiplayerOptions.OptionType.CultureTeam1 : MultiplayerOptions.OptionType.CultureTeam2);
			this.Culture = MBObjectManager.Instance.GetObject<BasicCultureObject>(optionType.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000A9545 File Offset: 0x000A7745
		public int GetNumberOfTimesPeerKilledPeer(MissionPeer killedPeer)
		{
			if (this._numberOfTimesPeerKilledPerPeer.ContainsKey(killedPeer))
			{
				return this._numberOfTimesPeerKilledPerPeer[killedPeer];
			}
			return 0;
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000A9563 File Offset: 0x000A7763
		public void ResetKillRegistry()
		{
			this._numberOfTimesPeerKilledPerPeer.Clear();
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000A9570 File Offset: 0x000A7770
		public bool RefreshSelectedPerks()
		{
			MBList<MPPerkObject> mblist = new MBList<MPPerkObject>();
			List<List<IReadOnlyPerkObject>> availablePerksForPeer = MultiplayerClassDivisions.GetAvailablePerksForPeer(this);
			if (availablePerksForPeer.Count == 3)
			{
				for (int i = 0; i < 3; i++)
				{
					int num = this._perks[this.SelectedTroopIndex][i];
					if (availablePerksForPeer[i].Count > 0)
					{
						mblist.Add(availablePerksForPeer[i][(num >= 0 && num < availablePerksForPeer[i].Count) ? num : 0].Clone(this));
					}
				}
				this._selectedPerks = new ValueTuple<int, MBList<MPPerkObject>>(this.SelectedTroopIndex, mblist);
				return true;
			}
			return false;
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x000A9608 File Offset: 0x000A7808
		private void ResetSelectedPerks()
		{
			if (this._selectedPerks.Item2 != null)
			{
				foreach (MPPerkObject mpperkObject in this._selectedPerks.Item2)
				{
					mpperkObject.Reset();
				}
			}
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000A966C File Offset: 0x000A786C
		private void CultureChanged(BasicCultureObject newCulture)
		{
			List<MultiplayerClassDivisions.MPHeroClass> list = MultiplayerClassDivisions.GetMPHeroClasses(newCulture).ToList<MultiplayerClassDivisions.MPHeroClass>();
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				MultiplayerClassDivisions.MPHeroClass mpheroClass = list[i];
				List<MPPerkSelectionManager.MPPerkSelection> selectionsForHeroClass = MPPerkSelectionManager.Instance.GetSelectionsForHeroClass(mpheroClass);
				if (selectionsForHeroClass != null)
				{
					int count2 = selectionsForHeroClass.Count;
					for (int j = 0; j < count2; j++)
					{
						MPPerkSelectionManager.MPPerkSelection mpperkSelection = selectionsForHeroClass[j];
						this._perks[i][mpperkSelection.ListIndex] = mpperkSelection.Index;
					}
				}
				else
				{
					for (int k = 0; k < 3; k++)
					{
						this._perks[i][k] = 0;
					}
				}
			}
			if (base.IsMine && GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new TeamInitialPerkInfoMessage(this._perks[this.SelectedTroopIndex]));
				GameNetwork.EndModuleEventAsClient();
			}
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000A974C File Offset: 0x000A794C
		public void OnTeamInitialPerkInfoReceived(int[] perks)
		{
			for (int i = 0; i < 3; i++)
			{
				this.SelectPerk(i, perks[i], -1);
			}
			this.TeamInitialPerkInfoReady = true;
		}

		// Token: 0x0400114C RID: 4428
		public const int NumberOfPerkLists = 3;

		// Token: 0x0400114D RID: 4429
		public const int MaxNumberOfTroopTypesPerCulture = 16;

		// Token: 0x0400114E RID: 4430
		private const float InactivityKickInSeconds = 180f;

		// Token: 0x0400114F RID: 4431
		private const float InactivityWarnInSeconds = 120f;

		// Token: 0x04001150 RID: 4432
		public const int MinKDACount = -1000;

		// Token: 0x04001151 RID: 4433
		public const int MaxKDACount = 100000;

		// Token: 0x04001152 RID: 4434
		public const int MinScore = -1000000;

		// Token: 0x04001153 RID: 4435
		public const int MaxScore = 1000000;

		// Token: 0x04001154 RID: 4436
		public const int MinSpawnTimer = 3;

		// Token: 0x04001155 RID: 4437
		public int CaptainBeingDetachedThreshold = 125;

		// Token: 0x0400115C RID: 4444
		private List<PeerVisualsHolder> _visuals = new List<PeerVisualsHolder>();

		// Token: 0x0400115D RID: 4445
		private Dictionary<MissionPeer, int> _numberOfTimesPeerKilledPerPeer = new Dictionary<MissionPeer, int>();

		// Token: 0x0400115E RID: 4446
		private MissionTime _lastActiveTime = MissionTime.Zero;

		// Token: 0x0400115F RID: 4447
		private ValueTuple<Agent.MovementControlFlag, Vec2, Vec3> _previousActivityStatus;

		// Token: 0x04001160 RID: 4448
		private bool _inactiveWarningGiven;

		// Token: 0x04001165 RID: 4453
		private int _selectedTroopIndex;

		// Token: 0x04001167 RID: 4455
		private Agent _followedAgent;

		// Token: 0x04001168 RID: 4456
		private Team _team;

		// Token: 0x04001169 RID: 4457
		private BasicCultureObject _culture;

		// Token: 0x0400116A RID: 4458
		private Formation _controlledFormation;

		// Token: 0x0400116B RID: 4459
		private MissionRepresentativeBase _representative;

		// Token: 0x0400116C RID: 4460
		private readonly MBList<int[]> _perks;

		// Token: 0x0400116D RID: 4461
		private int _killCount;

		// Token: 0x0400116E RID: 4462
		private int _assistCount;

		// Token: 0x0400116F RID: 4463
		private int _deathCount;

		// Token: 0x04001170 RID: 4464
		private int _score;

		// Token: 0x04001171 RID: 4465
		private ValueTuple<int, MBList<MPPerkObject>> _selectedPerks;

		// Token: 0x04001179 RID: 4473
		private int _botsUnderControlAlive;

		// Token: 0x020005DC RID: 1500
		// (Invoke) Token: 0x06003EA1 RID: 16033
		public delegate void OnUpdateEquipmentSetIndexEventDelegate(MissionPeer lobbyPeer, int equipmentSetIndex);

		// Token: 0x020005DD RID: 1501
		// (Invoke) Token: 0x06003EA5 RID: 16037
		public delegate void OnPerkUpdateEventDelegate(MissionPeer peer);

		// Token: 0x020005DE RID: 1502
		// (Invoke) Token: 0x06003EA9 RID: 16041
		public delegate void OnTeamChangedDelegate(NetworkCommunicator peer, Team previousTeam, Team newTeam);

		// Token: 0x020005DF RID: 1503
		// (Invoke) Token: 0x06003EAD RID: 16045
		public delegate void OnCultureChangedDelegate(BasicCultureObject newCulture);

		// Token: 0x020005E0 RID: 1504
		// (Invoke) Token: 0x06003EB1 RID: 16049
		public delegate void OnPlayerKilledDelegate(MissionPeer killerPeer, MissionPeer killedPeer);
	}
}
