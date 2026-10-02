using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Messages.FromClient.ToLobbyServer;
using Messages.FromLobbyServer.ToClient;
using TaleWorlds.Core;
using TaleWorlds.Diamond;
using TaleWorlds.Diamond.ClientApplication;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges;
using TaleWorlds.MountAndBlade.Diamond.Ranked;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000122 RID: 290
	public class LobbyClient : Client<LobbyClient>
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00008704 File Offset: 0x00006904
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x0000870B File Offset: 0x0000690B
		private static int FriendListCheckDelay
		{
			get
			{
				return LobbyClient._friendListCheckDelay;
			}
			set
			{
				if (value != LobbyClient._friendListCheckDelay)
				{
					LobbyClient._friendListCheckDelay = value;
				}
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0000871B File Offset: 0x0000691B
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x00008723 File Offset: 0x00006923
		public PlayerData PlayerData { get; private set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0000872C File Offset: 0x0000692C
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x00008734 File Offset: 0x00006934
		public SupportedFeatures SupportedFeatures { get; private set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0000873D File Offset: 0x0000693D
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x00008745 File Offset: 0x00006945
		public ClanInfo ClanInfo { get; private set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0000874E File Offset: 0x0000694E
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x00008756 File Offset: 0x00006956
		public ClanHomeInfo ClanHomeInfo { get; private set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0000875F File Offset: 0x0000695F
		public IReadOnlyList<string> OwnedCosmetics
		{
			get
			{
				return this._ownedCosmetics;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00008767 File Offset: 0x00006967
		public IReadOnlyDictionary<string, List<string>> UsedCosmetics
		{
			get
			{
				return this._usedCosmetics;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0000876F File Offset: 0x0000696F
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x00008777 File Offset: 0x00006977
		public AvailableScenes AvailableScenes { get; private set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x00008780 File Offset: 0x00006980
		public PlayerId PlayerID
		{
			get
			{
				return this._playerId;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00008788 File Offset: 0x00006988
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x00008790 File Offset: 0x00006990
		public bool IsRefreshingPlayerData { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00008799 File Offset: 0x00006999
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x000087A4 File Offset: 0x000069A4
		public LobbyClient.State CurrentState
		{
			get
			{
				return this._state;
			}
			private set
			{
				if (this._state != value)
				{
					LobbyClient.State state = this._state;
					this._state = value;
					ILobbyClientSessionHandler handler = this._handler;
					if (handler == null)
					{
						return;
					}
					handler.OnGameClientStateChange(state);
				}
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x000087DC File Offset: 0x000069DC
		public override long AliveCheckTimeInMiliSeconds
		{
			get
			{
				switch (this.CurrentState)
				{
				case LobbyClient.State.Idle:
				case LobbyClient.State.Working:
				case LobbyClient.State.Connected:
				case LobbyClient.State.SessionRequested:
				case LobbyClient.State.AtLobby:
					return 6000L;
				case LobbyClient.State.SearchingToRejoinBattle:
				case LobbyClient.State.RequestingToSearchBattle:
				case LobbyClient.State.RequestingToCancelSearchBattle:
				case LobbyClient.State.SearchingBattle:
				case LobbyClient.State.QuittingFromBattle:
				case LobbyClient.State.WaitingToRegisterCustomGame:
				case LobbyClient.State.HostingCustomGame:
				case LobbyClient.State.WaitingToJoinCustomGame:
					return 3500L;
				case LobbyClient.State.AtBattle:
				case LobbyClient.State.InCustomGame:
					return 60000L;
				}
				return 1000L;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00008857 File Offset: 0x00006A57
		public bool AtLobby
		{
			get
			{
				return this.CurrentState == LobbyClient.State.AtLobby;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00008862 File Offset: 0x00006A62
		public bool CanPerformLobbyActions
		{
			get
			{
				return this.CurrentState == LobbyClient.State.AtLobby || this.CurrentState == LobbyClient.State.RequestingToSearchBattle || this.CurrentState == LobbyClient.State.SearchingBattle || this.CurrentState == LobbyClient.State.WaitingToJoinCustomGame;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0000888B File Offset: 0x00006A8B
		public string Name
		{
			get
			{
				return this._userName;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00008893 File Offset: 0x00006A93
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0000889B File Offset: 0x00006A9B
		public string LastBattleServerAddressForClient { get; private set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x000088A4 File Offset: 0x00006AA4
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x000088AC File Offset: 0x00006AAC
		public ushort LastBattleServerPortForClient { get; private set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x000088B5 File Offset: 0x00006AB5
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x000088BD File Offset: 0x00006ABD
		public bool LastBattleIsOfficial { get; private set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x000088C6 File Offset: 0x00006AC6
		public bool Connected
		{
			get
			{
				return this.CurrentState != LobbyClient.State.Working && this.CurrentState > LobbyClient.State.Idle;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x000088DC File Offset: 0x00006ADC
		public bool IsIdle
		{
			get
			{
				return this.CurrentState == LobbyClient.State.Idle;
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x000088E7 File Offset: 0x00006AE7
		public void Logout(TextObject logOutReason)
		{
			base.BeginDisconnect();
			this._logOutReason = logOutReason;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x000088F6 File Offset: 0x00006AF6
		public bool LoggedIn
		{
			get
			{
				return this.CurrentState != LobbyClient.State.Idle && this.CurrentState != LobbyClient.State.Working && this.CurrentState != LobbyClient.State.Connected && this.CurrentState != LobbyClient.State.SessionRequested;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00008920 File Offset: 0x00006B20
		public bool IsInGame
		{
			get
			{
				return this.CurrentState == LobbyClient.State.AtBattle || this.CurrentState == LobbyClient.State.HostingCustomGame || this.CurrentState == LobbyClient.State.InCustomGame;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00008942 File Offset: 0x00006B42
		public bool IsHostingCustomGame
		{
			get
			{
				return this._state == LobbyClient.State.HostingCustomGame;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0000894E File Offset: 0x00006B4E
		public bool IsMatchmakingAvailable
		{
			get
			{
				ServerStatus serverStatus = this._serverStatus;
				return serverStatus != null && serverStatus.IsMatchmakingEnabled;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00008961 File Offset: 0x00006B61
		public bool IsAbleToSearchForGame
		{
			get
			{
				return this.IsMatchmakingAvailable && this._matchmakerBlockedTime <= DateTime.Now;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0000897D File Offset: 0x00006B7D
		public bool PartySystemAvailable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00008980 File Offset: 0x00006B80
		public bool IsCustomBattleAvailable
		{
			get
			{
				ServerStatus serverStatus = this._serverStatus;
				return serverStatus != null && serverStatus.IsCustomBattleEnabled;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00008993 File Offset: 0x00006B93
		public IReadOnlyList<ModuleInfoModel> LoadedUnofficialModules
		{
			get
			{
				return this._loadedUnofficialModules;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0000899B File Offset: 0x00006B9B
		public bool HasUnofficialModulesLoaded
		{
			get
			{
				return this.LoadedUnofficialModules.Count > 0;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x000089AB File Offset: 0x00006BAB
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x000089B3 File Offset: 0x00006BB3
		public bool HasUserGeneratedContentPrivilege { get; private set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x000089BC File Offset: 0x00006BBC
		public bool IsPartyLeader
		{
			get
			{
				if (this.Connected)
				{
					object obj = true;
					PartyPlayerInLobbyClient partyPlayerInLobbyClient = this.PlayersInParty.Find((PartyPlayerInLobbyClient p) => p.PlayerId == this._playerId);
					return object.Equals(obj, (partyPlayerInLobbyClient != null) ? new bool?(partyPlayerInLobbyClient.IsPartyLeader) : null);
				}
				return false;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00008A13 File Offset: 0x00006C13
		public bool IsClanLeader
		{
			get
			{
				ClanPlayer clanPlayer = this.PlayersInClan.Find((ClanPlayer p) => p.PlayerId == this._playerId);
				return clanPlayer != null && clanPlayer.Role == ClanPlayerRole.Leader;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00008A3A File Offset: 0x00006C3A
		public bool IsClanOfficer
		{
			get
			{
				ClanPlayer clanPlayer = this.PlayersInClan.Find((ClanPlayer p) => p.PlayerId == this._playerId);
				return clanPlayer != null && clanPlayer.Role == ClanPlayerRole.Officer;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00008A61 File Offset: 0x00006C61
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsEligibleToCreatePremadeGame { get; private set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00008A72 File Offset: 0x00006C72
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00008A7A File Offset: 0x00006C7A
		public CustomBattleId CustomBattleId { get; private set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00008A83 File Offset: 0x00006C83
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x00008A8B File Offset: 0x00006C8B
		public string CustomGameType { get; private set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00008A94 File Offset: 0x00006C94
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00008A9C File Offset: 0x00006C9C
		public string CustomGameScene { get; private set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00008AA5 File Offset: 0x00006CA5
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00008AAD File Offset: 0x00006CAD
		public AvailableCustomGames AvailableCustomGames { get; private set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00008AB6 File Offset: 0x00006CB6
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00008ABE File Offset: 0x00006CBE
		public PremadeGameList AvailablePremadeGames { get; private set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00008AC7 File Offset: 0x00006CC7
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x00008ACF File Offset: 0x00006CCF
		public List<PartyPlayerInLobbyClient> PlayersInParty { get; private set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00008AD8 File Offset: 0x00006CD8
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x00008AE0 File Offset: 0x00006CE0
		public List<ClanPlayer> PlayersInClan { get; private set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00008AE9 File Offset: 0x00006CE9
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x00008AF1 File Offset: 0x00006CF1
		public List<ClanPlayerInfo> PlayerInfosInClan { get; private set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00008AFA File Offset: 0x00006CFA
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00008B02 File Offset: 0x00006D02
		public FriendInfo[] FriendInfos { get; private set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00008B0B File Offset: 0x00006D0B
		public bool IsInParty
		{
			get
			{
				return this.Connected && this.PlayersInParty.Count > 0;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00008B25 File Offset: 0x00006D25
		public bool IsPartyFull
		{
			get
			{
				return this.PlayersInParty.Count == Parameters.MaxPlayerCountInParty;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00008B39 File Offset: 0x00006D39
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x00008B41 File Offset: 0x00006D41
		public string CurrentMatchId { get; private set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00008B4A File Offset: 0x00006D4A
		public bool IsInClan
		{
			get
			{
				return this.PlayersInClan.Count > 0;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x00008B5A File Offset: 0x00006D5A
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x00008B62 File Offset: 0x00006D62
		public bool IsPartyInvitationPopupActive { get; private set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00008B6B File Offset: 0x00006D6B
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x00008B73 File Offset: 0x00006D73
		public bool IsPartyJoinRequestPopupActive { get; private set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00008B7C File Offset: 0x00006D7C
		public bool CanInvitePlayers
		{
			get
			{
				SupportedFeatures supportedFeatures = this.SupportedFeatures;
				return supportedFeatures != null && supportedFeatures.SupportsFeatures(Features.Party) && (!this.IsInParty || this.IsPartyLeader);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00008BA5 File Offset: 0x00006DA5
		public bool CanSuggestPlayers
		{
			get
			{
				SupportedFeatures supportedFeatures = this.SupportedFeatures;
				return supportedFeatures != null && supportedFeatures.SupportsFeatures(Features.Party) && this.IsInParty && !this.IsPartyLeader;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00008BCF File Offset: 0x00006DCF
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x00008BD7 File Offset: 0x00006DD7
		public Guid ClanID { get; private set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00008BE0 File Offset: 0x00006DE0
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x00008BE8 File Offset: 0x00006DE8
		public List<PlayerId> FriendIDs { get; private set; }

		// Token: 0x060006D5 RID: 1749 RVA: 0x00008BF4 File Offset: 0x00006DF4
		public LobbyClient(DiamondClientApplication diamondClientApplication, IClientSessionProvider<LobbyClient> sessionProvider)
			: base(diamondClientApplication, sessionProvider, false)
		{
			this._serverStatusTimer = new Stopwatch();
			this._serverStatusTimer.Start();
			this._matchmakerBlockedTime = DateTime.MinValue;
			this._friendListTimer = new Stopwatch();
			this._friendListTimer.Start();
			this._recentPlayersTimer = new Stopwatch();
			this._recentPlayersTimer.Start();
			this.PlayersInParty = new List<PartyPlayerInLobbyClient>();
			this.PlayersInClan = new List<ClanPlayer>();
			this.PlayerInfosInClan = new List<ClanPlayerInfo>();
			this.FriendInfos = new FriendInfo[0];
			this.ClanID = Guid.Empty;
			this.FriendIDs = new List<PlayerId>();
			this.SupportedFeatures = new SupportedFeatures();
			this._ownedCosmetics = new List<string>();
			this._usedCosmetics = new Dictionary<string, List<string>>();
			this._cachedRankInfos = new TimedDictionaryCache<PlayerId, GameTypeRankInfo[]>(TimeSpan.FromSeconds(10.0));
			this._cachedPlayerStats = new TimedDictionaryCache<PlayerId, PlayerStatsBase[]>(TimeSpan.FromSeconds(10.0));
			this._cachedPlayerDatas = new TimedDictionaryCache<PlayerId, PlayerData>(TimeSpan.FromSeconds(10.0));
			this._cachedPlayerBannerlordIDs = new TimedDictionaryCache<PlayerId, string>(TimeSpan.FromSeconds(30.0));
			this._pendingPlayerRequests = new Dictionary<ValueTuple<LobbyClient.PendingRequest, PlayerId>, Task>();
			base.AddMessageHandler<FindGameAnswerMessage>(new ClientMessageHandler<FindGameAnswerMessage>(this.OnFindGameAnswerMessage));
			base.AddMessageHandler<JoinBattleMessage>(new ClientMessageHandler<JoinBattleMessage>(this.OnJoinBattleMessage));
			base.AddMessageHandler<BattleResultMessage>(new ClientMessageHandler<BattleResultMessage>(this.OnBattleResultMessage));
			base.AddMessageHandler<BattleServerLostMessage>(new ClientMessageHandler<BattleServerLostMessage>(this.OnBattleServerLostMessage));
			base.AddMessageHandler<BattleOverMessage>(new ClientMessageHandler<BattleOverMessage>(this.OnBattleOverMessage));
			base.AddMessageHandler<CancelBattleResponseMessage>(new ClientMessageHandler<CancelBattleResponseMessage>(this.OnCancelBattleResponseMessage));
			base.AddMessageHandler<RejoinRequestRejectedMessage>(new ClientMessageHandler<RejoinRequestRejectedMessage>(this.OnRejoinRequestRejectedMessage));
			base.AddMessageHandler<CancelFindGameMessage>(new ClientMessageHandler<CancelFindGameMessage>(this.OnCancelFindGameMessage));
			base.AddMessageHandler<RequestJoinPartyMessage>(new ClientMessageHandler<RequestJoinPartyMessage>(this.OnRequestJoinPartyMessage));
			base.AddMessageHandler<WhisperReceivedMessage>(new ClientMessageHandler<WhisperReceivedMessage>(this.OnWhisperMessageReceivedMessage));
			base.AddMessageHandler<ClanMessageReceivedMessage>(new ClientMessageHandler<ClanMessageReceivedMessage>(this.OnClanMessageReceivedMessage));
			base.AddMessageHandler<PartyMessageReceivedMessage>(new ClientMessageHandler<PartyMessageReceivedMessage>(this.OnPartyMessageReceivedMessage));
			base.AddMessageHandler<SystemMessage>(new ClientMessageHandler<SystemMessage>(this.OnSystemMessage));
			base.AddMessageHandler<InvitationToPartyMessage>(new ClientMessageHandler<InvitationToPartyMessage>(this.OnInvitationToPartyMessage));
			base.AddMessageHandler<PartyInvitationInvalidMessage>(new ClientMessageHandler<PartyInvitationInvalidMessage>(this.OnPartyInvitationInvalidMessage));
			base.AddMessageHandler<UpdatePlayerDataMessage>(new ClientMessageHandler<UpdatePlayerDataMessage>(this.OnUpdatePlayerDataMessage));
			base.AddMessageHandler<RecentPlayerStatusesMessage>(new ClientMessageHandler<RecentPlayerStatusesMessage>(this.OnRecentPlayerStatusesMessage));
			base.AddMessageHandler<PlayerQuitFromMatchmakerGameResult>(new ClientMessageHandler<PlayerQuitFromMatchmakerGameResult>(this.OnPlayerQuitFromMatchmakerGameResult));
			base.AddMessageHandler<PlayerRemovedFromMatchmakerGame>(new ClientMessageHandler<PlayerRemovedFromMatchmakerGame>(this.OnPlayerRemovedFromMatchmakerGameMessage));
			base.AddMessageHandler<EnterBattleWithPartyAnswer>(new ClientMessageHandler<EnterBattleWithPartyAnswer>(this.OnEnterBattleWithPartyAnswerMessage));
			base.AddMessageHandler<JoinCustomGameResultMessage>(new ClientMessageHandler<JoinCustomGameResultMessage>(this.OnJoinCustomGameResultMessage));
			base.AddMessageHandler<ClientWantsToConnectCustomGameMessage>(new ClientMessageHandler<ClientWantsToConnectCustomGameMessage>(this.OnClientWantsToConnectCustomGameMessage));
			base.AddMessageHandler<ClientQuitFromCustomGameMessage>(new ClientMessageHandler<ClientQuitFromCustomGameMessage>(this.OnClientQuitFromCustomGameMessage));
			base.AddMessageHandler<PlayerRemovedFromCustomGame>(new ClientMessageHandler<PlayerRemovedFromCustomGame>(this.OnPlayerRemovedFromCustomGame));
			base.AddMessageHandler<EnterCustomBattleWithPartyAnswer>(new ClientMessageHandler<EnterCustomBattleWithPartyAnswer>(this.OnEnterCustomBattleWithPartyAnswerMessage));
			base.AddMessageHandler<PlayerInvitedToPartyMessage>(new ClientMessageHandler<PlayerInvitedToPartyMessage>(this.OnPlayerInvitedToPartyMessage));
			base.AddMessageHandler<PlayersAddedToPartyMessage>(new ClientMessageHandler<PlayersAddedToPartyMessage>(this.OnPlayerAddedToPartyMessage));
			base.AddMessageHandler<PlayerRemovedFromPartyMessage>(new ClientMessageHandler<PlayerRemovedFromPartyMessage>(this.OnPlayerRemovedFromPartyMessage));
			base.AddMessageHandler<PlayerAssignedPartyLeaderMessage>(new ClientMessageHandler<PlayerAssignedPartyLeaderMessage>(this.OnPlayerAssignedPartyLeaderMessage));
			base.AddMessageHandler<PlayerSuggestedToPartyMessage>(new ClientMessageHandler<PlayerSuggestedToPartyMessage>(this.OnPlayerSuggestedToPartyMessage));
			base.AddMessageHandler<ServerStatusMessage>(new ClientMessageHandler<ServerStatusMessage>(this.OnServerStatusMessage));
			base.AddMessageHandler<MatchmakerDisabledMessage>(new ClientMessageHandler<MatchmakerDisabledMessage>(this.OnMatchmakerDisabledMessage));
			base.AddMessageHandler<FriendListMessage>(new ClientMessageHandler<FriendListMessage>(this.OnFriendListMessage));
			base.AddMessageHandler<AdminMessage>(new ClientMessageHandler<AdminMessage>(this.OnAdminMessage));
			base.AddMessageHandler<CreateClanAnswerMessage>(new ClientMessageHandler<CreateClanAnswerMessage>(this.OnCreateClanAnswerMessage));
			base.AddMessageHandler<ClanCreationRequestMessage>(new ClientMessageHandler<ClanCreationRequestMessage>(this.OnClanCreationRequestMessage));
			base.AddMessageHandler<ClanCreationRequestAnsweredMessage>(new ClientMessageHandler<ClanCreationRequestAnsweredMessage>(this.OnClanCreationRequestAnsweredMessage));
			base.AddMessageHandler<ClanCreationFailedMessage>(new ClientMessageHandler<ClanCreationFailedMessage>(this.OnClanCreationFailedMessage));
			base.AddMessageHandler<ClanCreationSuccessfulMessage>(new ClientMessageHandler<ClanCreationSuccessfulMessage>(this.OnClanCreationSuccessfulMessage));
			base.AddMessageHandler<ClanInfoChangedMessage>(new ClientMessageHandler<ClanInfoChangedMessage>(this.OnClanInfoChangedMessage));
			base.AddMessageHandler<InvitationToClanMessage>(new ClientMessageHandler<InvitationToClanMessage>(this.OnInvitationToClanMessage));
			base.AddMessageHandler<ClanDisbandedMessage>(new ClientMessageHandler<ClanDisbandedMessage>(this.OnClanDisbandedMessage));
			base.AddMessageHandler<KickedFromClanMessage>(new ClientMessageHandler<KickedFromClanMessage>(this.OnKickedFromClan));
			base.AddMessageHandler<PartyPlayerLeftClanMessage>(new ClientMessageHandler<PartyPlayerLeftClanMessage>(this.OnPartyPlayerLeftClan));
			base.AddMessageHandler<JoinPremadeGameAnswerMessage>(new ClientMessageHandler<JoinPremadeGameAnswerMessage>(this.OnJoinPremadeGameAnswerMessage));
			base.AddMessageHandler<PremadeGameEligibilityStatusMessage>(new ClientMessageHandler<PremadeGameEligibilityStatusMessage>(this.OnPremadeGameEligibilityStatusMessage));
			base.AddMessageHandler<CreatePremadeGameAnswerMessage>(new ClientMessageHandler<CreatePremadeGameAnswerMessage>(this.OnCreatePremadeGameAnswerMessage));
			base.AddMessageHandler<JoinPremadeGameRequestMessage>(new ClientMessageHandler<JoinPremadeGameRequestMessage>(this.OnJoinPremadeGameRequestMessage));
			base.AddMessageHandler<JoinPremadeGameRequestResultMessage>(new ClientMessageHandler<JoinPremadeGameRequestResultMessage>(this.OnJoinPremadeGameRequestResultMessage));
			base.AddMessageHandler<ClanGameCreationCancelledMessage>(new ClientMessageHandler<ClanGameCreationCancelledMessage>(this.OnClanGameCreationCancelledMessage));
			base.AddMessageHandler<SigilChangeAnswerMessage>(new ClientMessageHandler<SigilChangeAnswerMessage>(this.OnSigilChangeAnswerMessage));
			base.AddMessageHandler<LobbyNotificationsMessage>(new ClientMessageHandler<LobbyNotificationsMessage>(this.OnLobbyNotificationsMessage));
			base.AddMessageHandler<CustomBattleOverMessage>(new ClientMessageHandler<CustomBattleOverMessage>(this.OnCustomBattleOverMessage));
			base.AddMessageHandler<RejoinBattleRequestAnswerMessage>(new ClientMessageHandler<RejoinBattleRequestAnswerMessage>(this.OnRejoinBattleRequestAnswerMessage));
			base.AddMessageHandler<PendingBattleRejoinMessage>(new ClientMessageHandler<PendingBattleRejoinMessage>(this.OnPendingBattleRejoinMessage));
			base.AddMessageHandler<ShowAnnouncementMessage>(new ClientMessageHandler<ShowAnnouncementMessage>(this.OnShowAnnouncementMessage));
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0000911C File Offset: 0x0000731C
		public void SetLoadedModules(string[] moduleIDs)
		{
			if (this._loadedUnofficialModules == null)
			{
				this._loadedUnofficialModules = new List<ModuleInfoModel>();
				using (List<ModuleInfo>.Enumerator enumerator = ModuleHelper.GetSortedModules(moduleIDs).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ModuleInfoModel moduleInfoModel;
						if (ModuleInfoModel.TryCreateForSession(enumerator.Current, out moduleInfoModel))
						{
							this._loadedUnofficialModules.Add(moduleInfoModel);
						}
					}
				}
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00009190 File Offset: 0x00007390
		public async Task<AvailableCustomGames> GetCustomGameServerList()
		{
			this.AssertCanPerformLobbyActions();
			CustomGameServerListResponse customGameServerListResponse = await base.CallFunction<CustomGameServerListResponse>(new RequestCustomGameServerListMessage());
			Debug.Print("Custom game server list received", 0, Debug.DebugColor.White, 17592186044416UL);
			AvailableCustomGames availableCustomGames;
			if (customGameServerListResponse != null)
			{
				this.AvailableCustomGames = customGameServerListResponse.AvailableCustomGames;
				ILobbyClientSessionHandler handler = this._handler;
				if (handler != null)
				{
					handler.OnCustomGameServerListReceived(this.AvailableCustomGames);
				}
				availableCustomGames = this.AvailableCustomGames;
			}
			else
			{
				availableCustomGames = null;
			}
			return availableCustomGames;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000091D5 File Offset: 0x000073D5
		public void QuitFromCustomGame()
		{
			base.SendMessage(new QuitFromCustomGameMessage());
			this.CurrentState = LobbyClient.State.AtLobby;
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnQuitFromCustomGame();
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000091F9 File Offset: 0x000073F9
		public void QuitFromMatchmakerGame()
		{
			if (this.CurrentState == LobbyClient.State.AtBattle)
			{
				this.CheckAndSendMessage(new QuitFromMatchmakerGameMessage());
				this.CurrentState = LobbyClient.State.QuittingFromBattle;
				ILobbyClientSessionHandler handler = this._handler;
				if (handler == null)
				{
					return;
				}
				handler.OnQuitFromMatchmakerGame();
			}
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00009228 File Offset: 0x00007428
		public async Task<bool> RequestJoinCustomGame(CustomBattleId serverId, string password, bool isJoinAsAdmin = false)
		{
			this.CurrentState = LobbyClient.State.WaitingToJoinCustomGame;
			this.CustomBattleId = serverId;
			string text = ((!string.IsNullOrEmpty(password)) ? Common.CalculateMD5Hash(password) : null);
			base.SendMessage(new RequestJoinCustomGameMessage(serverId, text, isJoinAsAdmin));
			while (this.CurrentState == LobbyClient.State.WaitingToJoinCustomGame)
			{
				await Task.Yield();
			}
			bool flag;
			if (this.CurrentState == LobbyClient.State.InCustomGame)
			{
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00009288 File Offset: 0x00007488
		public async Task<bool> RequestJoinPlayerParty(PlayerId targetPlayer, bool inviteRequest)
		{
			this.AssertCanPerformLobbyActions();
			RequestJoinPlayerPartyMessageResult requestJoinPlayerPartyMessageResult = await base.CallFunction<RequestJoinPlayerPartyMessageResult>(new RequestJoinPlayerPartyMessage(targetPlayer, inviteRequest));
			bool flag;
			if (requestJoinPlayerPartyMessageResult != null)
			{
				flag = requestJoinPlayerPartyMessageResult.Success;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000092DD File Offset: 0x000074DD
		public void CancelFindGame()
		{
			this.CurrentState = LobbyClient.State.RequestingToCancelSearchBattle;
			this.CheckAndSendMessage(new CancelBattleRequestMessage());
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000092F1 File Offset: 0x000074F1
		public void FindGame()
		{
			this.CurrentState = LobbyClient.State.RequestingToSearchBattle;
			this.CheckAndSendMessage(new FindGameMessage());
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00009308 File Offset: 0x00007508
		public async Task<bool> FindCustomGame(string[] selectedCustomGameTypes, bool? hasCrossplayPrivilege, string region)
		{
			this.CurrentState = LobbyClient.State.WaitingToJoinCustomGame;
			int i = 0;
			while (i < LobbyClient.CheckForCustomGamesCount)
			{
				CustomGameServerListResponse customGameServerListResponse = await base.CallFunction<CustomGameServerListResponse>(new RequestCustomGameServerListMessage());
				if (customGameServerListResponse != null && customGameServerListResponse.AvailableCustomGames.CustomGameServerInfos.Count > 0)
				{
					List<GameServerEntry> list = customGameServerListResponse.AvailableCustomGames.CustomGameServerInfos.OrderByDescending<GameServerEntry, int>((GameServerEntry c) => c.PlayerCount).ToList<GameServerEntry>();
					bool? flag = hasCrossplayPrivilege;
					bool flag2 = true;
					GameServerEntry.FilterGameServerEntriesBasedOnCrossplay(ref list, (flag.GetValueOrDefault() == flag2) & (flag != null));
					foreach (string text in selectedCustomGameTypes)
					{
						foreach (GameServerEntry gameServerEntry in list)
						{
							if (gameServerEntry.IsOfficial && gameServerEntry.GameType == text && gameServerEntry.Region == region && !gameServerEntry.PasswordProtected && gameServerEntry.MaxPlayerCount >= gameServerEntry.PlayerCount + this.PlayersInParty.Count)
							{
								base.SendMessage(new RequestJoinCustomGameMessage(gameServerEntry.Id, "", false));
								while (this.CurrentState == LobbyClient.State.WaitingToJoinCustomGame)
								{
									await Task.Yield();
								}
								if (this.CurrentState == LobbyClient.State.InCustomGame)
								{
									return true;
								}
								return false;
							}
						}
						List<GameServerEntry>.Enumerator enumerator = default(List<GameServerEntry>.Enumerator);
					}
					await Task.Delay(LobbyClient.CheckForCustomGamesDelay);
				}
				int j = i++;
			}
			this.CurrentState = LobbyClient.State.AtLobby;
			return false;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00009368 File Offset: 0x00007568
		public async Task<LobbyClientConnectResult> Connect(ILobbyClientSessionHandler lobbyClientSessionHandler, ILoginAccessProvider lobbyClientLoginAccessProvider, string overridenUserName, bool hasUserGeneratedContentPrivilege, PlatformInitParams initParams, Func<Task<bool>> preLoginTask)
		{
			base.AccessProvider = lobbyClientLoginAccessProvider;
			base.AccessProvider.Initialize(overridenUserName, initParams);
			this._handler = lobbyClientSessionHandler;
			this.CurrentState = LobbyClient.State.Working;
			this.HasUserGeneratedContentPrivilege = hasUserGeneratedContentPrivilege;
			base.BeginConnect();
			while (this.CurrentState == LobbyClient.State.Working)
			{
				await Task.Yield();
			}
			LobbyClientConnectResult lobbyClientConnectResult;
			if (this.CurrentState != LobbyClient.State.Connected)
			{
				lobbyClientConnectResult = new LobbyClientConnectResult(false, new TextObject("{=3cWg0cWt}Could not connect to server.", null));
			}
			else
			{
				AccessObjectResult accessObjectResult = AccessObjectResult.CreateFailed(new TextObject("{=gAeQdLU5}Failed to acquire access data from platform", null));
				Task getAccessObjectTask = Task.Run(delegate
				{
					accessObjectResult = this.AccessProvider.CreateAccessObject();
				});
				while (!getAccessObjectTask.IsCompleted)
				{
					await Task.Yield();
				}
				if (getAccessObjectTask.IsFaulted)
				{
					throw getAccessObjectTask.Exception ?? new Exception("Get access object task faulted without exception");
				}
				if (getAccessObjectTask.IsCanceled)
				{
					throw new Exception("Get access object task canceled");
				}
				if (!accessObjectResult.Success)
				{
					base.BeginDisconnect();
					lobbyClientConnectResult = new LobbyClientConnectResult(false, accessObjectResult.FailReason ?? new TextObject("{=JO37PkfW}Your platform service is not initialized.", null));
				}
				else
				{
					bool flag = preLoginTask != null;
					if (flag)
					{
						TaskAwaiter<bool> taskAwaiter = preLoginTask().GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							await taskAwaiter;
							TaskAwaiter<bool> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<bool>);
						}
						flag = !taskAwaiter.GetResult();
					}
					if (flag)
					{
						base.BeginDisconnect();
						lobbyClientConnectResult = new LobbyClientConnectResult(false, new TextObject("{=63X8LERm}Couldn't receive login result from server.", null));
					}
					else
					{
						this._userName = base.AccessProvider.GetUserName();
						this._playerId = base.AccessProvider.GetPlayerId();
						this.CurrentState = LobbyClient.State.SessionRequested;
						string environmentVariable = Environment.GetEnvironmentVariable("Bannerlord.ConnectionPassword");
						LoginResult loginResult = await base.Login(new InitializeSession(this._playerId, this._userName, accessObjectResult.AccessObject, base.Application.ApplicationVersion, environmentVariable, this._loadedUnofficialModules.ToArray()));
						if (loginResult == null)
						{
							base.BeginDisconnect();
							lobbyClientConnectResult = new LobbyClientConnectResult(false, new TextObject("{=63X8LERm}Couldn't receive login result from server.", null));
						}
						else if (!loginResult.Successful)
						{
							base.BeginDisconnect();
							lobbyClientConnectResult = LobbyClientConnectResult.FromServerConnectResult(loginResult.ErrorCode, loginResult.ErrorParameters);
						}
						else
						{
							InitializeSessionResponse initializeSessionResponse = (InitializeSessionResponse)loginResult.LoginResultObject;
							this.PlayerData = initializeSessionResponse.PlayerData;
							this._serverStatus = initializeSessionResponse.ServerStatus;
							this.SupportedFeatures = initializeSessionResponse.SupportedFeatures;
							this.AvailableScenes = initializeSessionResponse.AvailableScenes;
							this._logOutReason = new TextObject("{=i4MNr0bo}Disconnected from the Lobby.", null);
							await PermaMuteList.LoadMutedPlayers(this.PlayerData.PlayerId);
							this._ownedCosmetics.Clear();
							this._usedCosmetics.Clear();
							ILobbyClientSessionHandler handler = this._handler;
							if (handler != null)
							{
								handler.OnPlayerDataReceived(this.PlayerData);
							}
							ILobbyClientSessionHandler handler2 = this._handler;
							if (handler2 != null)
							{
								handler2.OnServerStatusReceived(initializeSessionResponse.ServerStatus);
							}
							LobbyClient.FriendListCheckDelay = this._serverStatus.FriendListUpdatePeriod * 1000;
							if (initializeSessionResponse.HasPendingRejoin)
							{
								ILobbyClientSessionHandler handler3 = this._handler;
								if (handler3 != null)
								{
									handler3.OnPendingRejoin();
								}
							}
							this.CurrentState = LobbyClient.State.AtLobby;
							lobbyClientConnectResult = new LobbyClientConnectResult(true, null);
						}
					}
				}
			}
			return lobbyClientConnectResult;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000093E0 File Offset: 0x000075E0
		public void KickPlayer(PlayerId id, bool banPlayer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000093E7 File Offset: 0x000075E7
		public void ChangeRegion(string region)
		{
			if (this.PlayerData == null || this.PlayerData.LastRegion != region)
			{
				this.CheckAndSendMessage(new ChangeRegionMessage(region));
			}
			if (this.CurrentState == LobbyClient.State.AtLobby)
			{
				this.PlayerData.LastRegion = region;
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00009428 File Offset: 0x00007628
		public void ChangeGameTypes(string[] gameTypes)
		{
			bool flag = this.PlayerData == null || this.PlayerData.LastGameTypes.Length != gameTypes.Length;
			if (!flag)
			{
				foreach (string text in gameTypes)
				{
					if (!this.PlayerData.LastGameTypes.Contains(text))
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				this.CheckAndSendMessage(new ChangeGameTypesMessage(gameTypes));
			}
			if (this.CurrentState == LobbyClient.State.AtLobby)
			{
				this.PlayerData.LastGameTypes = gameTypes;
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000094A8 File Offset: 0x000076A8
		private void CheckAndSendMessage(Message message)
		{
			base.SendMessage(message);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000094B1 File Offset: 0x000076B1
		public override void OnConnected()
		{
			base.OnConnected();
			this.CurrentState = LobbyClient.State.Connected;
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnConnected();
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x000094D0 File Offset: 0x000076D0
		public override void OnCantConnect()
		{
			base.OnCantConnect();
			this.CurrentState = LobbyClient.State.Idle;
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnCantConnect();
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x000094F0 File Offset: 0x000076F0
		public override void OnDisconnected()
		{
			base.OnDisconnected();
			bool loggedIn = this.LoggedIn;
			this.CurrentState = LobbyClient.State.Idle;
			this.PlayerData = null;
			this.PlayersInParty.Clear();
			this.PlayersInClan.Clear();
			this.ClanHomeInfo = null;
			this._matchmakerBlockedTime = DateTime.MinValue;
			this.FriendInfos = new FriendInfo[0];
			PermaMuteList.SaveMutedPlayers();
			this._ownedCosmetics.Clear();
			this._usedCosmetics.Clear();
			ILobbyClientSessionHandler handler = this._handler;
			if (handler != null)
			{
				handler.OnDisconnected(loggedIn ? this._logOutReason : null);
			}
			this.RemoveLobbyClientHandler();
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0000958A File Offset: 0x0000778A
		public void RemoveLobbyClientHandler()
		{
			this._handler = null;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00009593 File Offset: 0x00007793
		private void OnFindGameAnswerMessage(FindGameAnswerMessage message)
		{
			if (!message.Successful)
			{
				this.CurrentState = LobbyClient.State.AtLobby;
			}
			else
			{
				this.CurrentState = LobbyClient.State.SearchingBattle;
			}
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnFindGameAnswer(message.Successful, message.SelectedAndEnabledGameTypes, false);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000095CC File Offset: 0x000077CC
		private void OnJoinBattleMessage(JoinBattleMessage message)
		{
			BattleServerInformationForClient battleServerInformation = message.BattleServerInformation;
			string text;
			if (base.Application.ProxyAddressMap.TryGetValue(battleServerInformation.ServerAddress, out text))
			{
				battleServerInformation.ServerAddress = text;
			}
			this.LastBattleServerAddressForClient = battleServerInformation.ServerAddress;
			this.LastBattleServerPortForClient = battleServerInformation.ServerPort;
			this.CurrentMatchId = battleServerInformation.MatchId;
			this.LastBattleIsOfficial = true;
			string text2 = "Successful matchmaker game join response\n";
			text2 = text2 + "Address: " + this.LastBattleServerAddressForClient + "\n";
			text2 = string.Concat(new object[] { text2, "Port: ", this.LastBattleServerPortForClient, "\n" });
			text2 = text2 + "Match Id: " + this.CurrentMatchId + "\n";
			Debug.Print(text2, 0, Debug.DebugColor.White, 17592186044416UL);
			ILobbyClientSessionHandler handler = this._handler;
			if (handler != null)
			{
				handler.OnBattleServerInformationReceived(battleServerInformation);
			}
			this.CurrentState = LobbyClient.State.AtBattle;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000096C0 File Offset: 0x000078C0
		private void OnBattleOverMessage(BattleOverMessage message)
		{
			if (this.CurrentState == LobbyClient.State.AtBattle || this.CurrentState == LobbyClient.State.QuittingFromBattle || this.CurrentState == LobbyClient.State.AtLobby)
			{
				this.CurrentState = LobbyClient.State.AtLobby;
				ILobbyClientSessionHandler handler = this._handler;
				if (handler == null)
				{
					return;
				}
				handler.OnMatchmakerGameOver(message.OldExperience, message.NewExperience, message.EarnedBadges, message.GoldGained, message.OldInfo, message.NewInfo, message.BattleCancelReason);
			}
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0000972B File Offset: 0x0000792B
		private void OnBattleResultMessage(BattleResultMessage message)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnBattleResultReceived();
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0000973D File Offset: 0x0000793D
		private void OnBattleServerLostMessage(BattleServerLostMessage message)
		{
			if (this.CurrentState == LobbyClient.State.AtBattle || this.CurrentState == LobbyClient.State.SearchingToRejoinBattle)
			{
				this.CurrentState = LobbyClient.State.AtLobby;
			}
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnBattleServerLost();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00009769 File Offset: 0x00007969
		private void OnCancelBattleResponseMessage(CancelBattleResponseMessage message)
		{
			if (message.Successful)
			{
				ILobbyClientSessionHandler handler = this._handler;
				if (handler != null)
				{
					handler.OnCancelJoiningBattle();
				}
				this.CurrentState = LobbyClient.State.AtLobby;
				return;
			}
			if (this.CurrentState == LobbyClient.State.RequestingToCancelSearchBattle)
			{
				this.CurrentState = LobbyClient.State.SearchingBattle;
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0000979C File Offset: 0x0000799C
		private void OnRejoinRequestRejectedMessage(RejoinRequestRejectedMessage message)
		{
			this.CurrentState = LobbyClient.State.AtLobby;
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnRejoinRequestRejected();
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x000097B5 File Offset: 0x000079B5
		private void OnCancelFindGameMessage(CancelFindGameMessage message)
		{
			if (this.CurrentState == LobbyClient.State.SearchingBattle)
			{
				this.CancelFindGame();
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000097C6 File Offset: 0x000079C6
		private void OnWhisperMessageReceivedMessage(WhisperReceivedMessage message)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnWhisperMessageReceived(message.FromPlayer, message.ToPlayer, message.Message);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000097EA File Offset: 0x000079EA
		private void OnClanMessageReceivedMessage(ClanMessageReceivedMessage message)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnClanMessageReceived(message.PlayerName, message.Message);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00009808 File Offset: 0x00007A08
		private void OnPartyMessageReceivedMessage(PartyMessageReceivedMessage message)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPartyMessageReceived(message.PlayerName, message.Message);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00009826 File Offset: 0x00007A26
		private void OnPlayerQuitFromMatchmakerGameResult(PlayerQuitFromMatchmakerGameResult message)
		{
			if (this.CurrentState == LobbyClient.State.QuittingFromBattle)
			{
				this.CurrentState = LobbyClient.State.AtLobby;
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0000983C File Offset: 0x00007A3C
		private void OnEnterBattleWithPartyAnswerMessage(EnterBattleWithPartyAnswer message)
		{
			if (!message.Successful)
			{
				this.CurrentState = LobbyClient.State.AtLobby;
				return;
			}
			if (this.CurrentState == LobbyClient.State.AtLobby || this.CurrentState == LobbyClient.State.RequestingToSearchBattle)
			{
				this.CurrentState = LobbyClient.State.SearchingBattle;
			}
			else if (this.CurrentState != LobbyClient.State.SearchingBattle)
			{
				LobbyClient.State currentState = this.CurrentState;
			}
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnEnterBattleWithPartyAnswer(message.SelectedAndEnabledGameTypes);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0000989C File Offset: 0x00007A9C
		private void OnJoinCustomGameResultMessage(JoinCustomGameResultMessage message)
		{
			if (!message.Success && message.Response == CustomGameJoinResponse.AlreadyRequestedWaitingForServerResponse)
			{
				ILobbyClientSessionHandler handler = this._handler;
				if (handler == null)
				{
					return;
				}
				handler.OnSystemMessageReceived(new TextObject("{=ivKntfNA}Already requested to join, waiting for server response", null).ToString());
				return;
			}
			else if (message.Success)
			{
				message.JoinGameData.GameServerProperties.CheckAndReplaceProxyAddress(base.Application.ProxyAddressMap);
				this.CurrentState = LobbyClient.State.InCustomGame;
				this.LastBattleServerAddressForClient = message.JoinGameData.GameServerProperties.Address;
				this.LastBattleServerPortForClient = (ushort)message.JoinGameData.GameServerProperties.Port;
				this.LastBattleIsOfficial = message.JoinGameData.GameServerProperties.IsOfficial;
				this.CurrentMatchId = message.MatchId;
				string text = "Successful custom game join response\n";
				text = text + "Server Name: " + message.JoinGameData.GameServerProperties.Name + "\n";
				text = text + "Host Name: " + message.JoinGameData.GameServerProperties.HostName + "\n";
				text = text + "Address: " + this.LastBattleServerAddressForClient + "\n";
				text = string.Concat(new object[] { text, "Port: ", this.LastBattleServerPortForClient, "\n" });
				text = text + "Match Id: " + this.CurrentMatchId + "\n";
				text = text + "Is Official: " + message.JoinGameData.GameServerProperties.IsOfficial.ToString() + "\n";
				Debug.Print(text, 0, Debug.DebugColor.White, 17592186044416UL);
				ILobbyClientSessionHandler handler2 = this._handler;
				if (handler2 == null)
				{
					return;
				}
				handler2.OnJoinCustomGameResponse(message.Success, message.JoinGameData, message.Response, message.IsAdmin);
				return;
			}
			else
			{
				this.CurrentState = LobbyClient.State.AtLobby;
				ILobbyClientSessionHandler handler3 = this._handler;
				if (handler3 == null)
				{
					return;
				}
				handler3.OnJoinCustomGameFailureResponse(message.Response);
				return;
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00009A80 File Offset: 0x00007C80
		private void OnClientWantsToConnectCustomGameMessage(ClientWantsToConnectCustomGameMessage message)
		{
			this.AssertCanPerformLobbyActions();
			List<PlayerJoinGameResponseDataFromHost> list = new List<PlayerJoinGameResponseDataFromHost>();
			PlayerJoinGameData[] playerJoinGameData = message.PlayerJoinGameData;
			for (int i = 0; i < playerJoinGameData.Length; i++)
			{
				if (playerJoinGameData[i] != null)
				{
					List<PlayerJoinGameData> list2 = new List<PlayerJoinGameData>();
					PlayerJoinGameData playerJoinGameData2 = playerJoinGameData[i];
					Guid? guid = playerJoinGameData2.PartyId;
					if (guid == null)
					{
						list2.Add(playerJoinGameData2);
					}
					else
					{
						for (int j = i; j < playerJoinGameData.Length; j++)
						{
							PlayerJoinGameData playerJoinGameData3 = playerJoinGameData[j];
							guid = playerJoinGameData2.PartyId;
							if (guid.Equals((playerJoinGameData3 != null) ? playerJoinGameData3.PartyId : null))
							{
								list2.Add(playerJoinGameData3);
								playerJoinGameData[j] = null;
							}
						}
					}
					if (this._handler != null)
					{
						PlayerJoinGameResponseDataFromHost[] array = this._handler.OnClientWantsToConnectCustomGame(list2.ToArray());
						list.AddRange(array);
					}
				}
			}
			this.ResponseCustomGameClientConnection(list.ToArray());
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00009B6B File Offset: 0x00007D6B
		private void OnClientQuitFromCustomGameMessage(ClientQuitFromCustomGameMessage message)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnClientQuitFromCustomGame(message.PlayerId);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00009B83 File Offset: 0x00007D83
		private void OnEnterCustomBattleWithPartyAnswerMessage(EnterCustomBattleWithPartyAnswer message)
		{
			if (!message.Successful)
			{
				this.CurrentState = LobbyClient.State.AtLobby;
				return;
			}
			if (this.CurrentState == LobbyClient.State.AtLobby)
			{
				this.CurrentState = LobbyClient.State.WaitingToJoinCustomGame;
			}
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnEnterCustomBattleWithPartyAnswer();
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00009BB6 File Offset: 0x00007DB6
		private void OnPlayerRemovedFromMatchmakerGameMessage(PlayerRemovedFromMatchmakerGame message)
		{
			this.CurrentState = LobbyClient.State.AtLobby;
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnRemovedFromMatchmakerGame(message.DisconnectType);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00009BD5 File Offset: 0x00007DD5
		private void OnPlayerRemovedFromCustomGame(PlayerRemovedFromCustomGame message)
		{
			this.CurrentState = LobbyClient.State.AtLobby;
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnRemovedFromCustomGame(message.DisconnectType);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00009BF4 File Offset: 0x00007DF4
		private void OnSystemMessage(SystemMessage message)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnSystemMessageReceived(message.GetDescription().ToString());
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00009C11 File Offset: 0x00007E11
		private void OnAdminMessage(AdminMessage message)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnAdminMessageReceived(message.Message);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00009C29 File Offset: 0x00007E29
		private void OnInvitationToPartyMessage(InvitationToPartyMessage message)
		{
			this.IsPartyInvitationPopupActive = true;
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPartyInvitationReceived(message.InviterPlayerName, message.InviterPlayerId);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00009C4E File Offset: 0x00007E4E
		private void OnPartyInvitationInvalidMessage(PartyInvitationInvalidMessage message)
		{
			this.IsPartyInvitationPopupActive = false;
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPartyInvitationInvalidated();
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00009C67 File Offset: 0x00007E67
		private void OnRequestJoinPartyMessage(RequestJoinPartyMessage message)
		{
			this.IsPartyJoinRequestPopupActive = true;
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPartyJoinRequestReceived(message.PlayerId, message.ViaPlayerId, message.ViaPlayerName);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00009C92 File Offset: 0x00007E92
		private void OnPlayerInvitedToPartyMessage(PlayerInvitedToPartyMessage message)
		{
			this.PlayersInParty.Add(new PartyPlayerInLobbyClient(message.PlayerId, message.PlayerName, false));
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPlayerInvitedToParty(message.PlayerId);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00009CC8 File Offset: 0x00007EC8
		private void OnPlayerAddedToPartyMessage(PlayersAddedToPartyMessage message)
		{
			foreach (ValueTuple<PlayerId, string, bool> valueTuple in message.Players)
			{
				PlayerId playerId = valueTuple.Item1;
				string item = valueTuple.Item2;
				bool item2 = valueTuple.Item3;
				PartyPlayerInLobbyClient partyPlayerInLobbyClient = this.PlayersInParty.Find((PartyPlayerInLobbyClient p) => p.PlayerId == playerId);
				if (partyPlayerInLobbyClient != null)
				{
					partyPlayerInLobbyClient.SetAtParty();
				}
				else
				{
					partyPlayerInLobbyClient = new PartyPlayerInLobbyClient(playerId, item, item2);
					this.PlayersInParty.Add(partyPlayerInLobbyClient);
					partyPlayerInLobbyClient.SetAtParty();
				}
				if (playerId != this.PlayerID)
				{
					RecentPlayersManager.AddOrUpdatePlayerEntry(playerId, item, InteractionType.InPartyTogether, -1);
				}
			}
			foreach (ValueTuple<PlayerId, string> valueTuple2 in message.InvitedPlayers)
			{
				PlayerId item3 = valueTuple2.Item1;
				string item4 = valueTuple2.Item2;
				this.PlayersInParty.Add(new PartyPlayerInLobbyClient(item3, item4, false));
			}
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPlayersAddedToParty(message.Players, message.InvitedPlayers);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00009E24 File Offset: 0x00008024
		private void OnPlayerRemovedFromPartyMessage(PlayerRemovedFromPartyMessage message)
		{
			if (message.PlayerId == this._playerId)
			{
				this.PlayersInParty.Clear();
			}
			else
			{
				this.PlayersInParty.RemoveAll((PartyPlayerInLobbyClient partyPlayer) => partyPlayer.PlayerId == message.PlayerId);
			}
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPlayerRemovedFromParty(message.PlayerId, message.Reason);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00009EA4 File Offset: 0x000080A4
		private void OnPlayerAssignedPartyLeaderMessage(PlayerAssignedPartyLeaderMessage message)
		{
			PartyPlayerInLobbyClient partyPlayerInLobbyClient = this.PlayersInParty.FirstOrDefault<PartyPlayerInLobbyClient>((PartyPlayerInLobbyClient p) => p.IsPartyLeader);
			if (partyPlayerInLobbyClient != null)
			{
				partyPlayerInLobbyClient.SetMember();
			}
			PartyPlayerInLobbyClient partyPlayerInLobbyClient2 = this.PlayersInParty.FirstOrDefault<PartyPlayerInLobbyClient>((PartyPlayerInLobbyClient partyPlayer) => partyPlayer.PlayerId == message.PartyLeaderId);
			if (partyPlayerInLobbyClient2 != null)
			{
				partyPlayerInLobbyClient2.SetLeader();
			}
			else
			{
				this.KickPlayerFromParty(this.PlayerID);
			}
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPlayerAssignedPartyLeader(message.PartyLeaderId);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00009F3D File Offset: 0x0000813D
		private void OnPlayerSuggestedToPartyMessage(PlayerSuggestedToPartyMessage message)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPlayerSuggestedToParty(message.PlayerId, message.PlayerName, message.SuggestingPlayerId, message.SuggestingPlayerName);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00009F67 File Offset: 0x00008167
		private void OnUpdatePlayerDataMessage(UpdatePlayerDataMessage updatePlayerDataMessage)
		{
			this.PlayerData = updatePlayerDataMessage.PlayerData;
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPlayerDataReceived(this.PlayerData);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00009F8C File Offset: 0x0000818C
		private void OnServerStatusMessage(ServerStatusMessage serverStatusMessage)
		{
			this._serverStatusTimer.Restart();
			this._serverStatus = serverStatusMessage.ServerStatus;
			if (!this.IsAbleToSearchForGame && this.CurrentState == LobbyClient.State.SearchingBattle)
			{
				this.CancelFindGame();
			}
			if (this._handler != null)
			{
				this._handler.OnServerStatusReceived(this._serverStatus);
				LobbyClient.FriendListCheckDelay = this._serverStatus.FriendListUpdatePeriod * 1000;
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00009FF6 File Offset: 0x000081F6
		private void OnFriendListMessage(FriendListMessage friendListMessage)
		{
			this._friendListTimer.Restart();
			this.FriendInfos = friendListMessage.Friends;
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnFriendListReceived(friendListMessage.Friends);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0000A028 File Offset: 0x00008228
		private void OnMatchmakerDisabledMessage(MatchmakerDisabledMessage matchmakerDisabledMessage)
		{
			if (matchmakerDisabledMessage.RemainingTime > 0)
			{
				this._matchmakerBlockedTime = DateTime.Now.AddSeconds((double)matchmakerDisabledMessage.RemainingTime);
				return;
			}
			this._matchmakerBlockedTime = DateTime.MinValue;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0000A064 File Offset: 0x00008264
		private void OnClanCreationRequestMessage(ClanCreationRequestMessage clanCreationRequestMessage)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnClanInvitationReceived(clanCreationRequestMessage.ClanName, clanCreationRequestMessage.ClanTag, true);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0000A083 File Offset: 0x00008283
		private void OnClanCreationRequestAnsweredMessage(ClanCreationRequestAnsweredMessage clanCreationRequestAnsweredMessage)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnClanInvitationAnswered(clanCreationRequestAnsweredMessage.PlayerId, clanCreationRequestAnsweredMessage.ClanCreationAnswer);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0000A0A1 File Offset: 0x000082A1
		private void OnClanCreationSuccessfulMessage(ClanCreationSuccessfulMessage clanCreationSuccessfulMessage)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnClanCreationSuccessful();
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0000A0B3 File Offset: 0x000082B3
		private void OnClanCreationFailedMessage(ClanCreationFailedMessage clanCreationFailedMessage)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnClanCreationFailed();
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0000A0C5 File Offset: 0x000082C5
		private void OnCreateClanAnswerMessage(CreateClanAnswerMessage createClanAnswerMessage)
		{
			if (createClanAnswerMessage.Successful)
			{
				ILobbyClientSessionHandler handler = this._handler;
				if (handler == null)
				{
					return;
				}
				handler.OnClanCreationStarted();
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0000A0DF File Offset: 0x000082DF
		public void SendWhisper(string playerName, string message)
		{
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0000A0E1 File Offset: 0x000082E1
		private void OnRecentPlayerStatusesMessage(RecentPlayerStatusesMessage message)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnRecentPlayerStatusesReceived(message.Friends);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0000A0F9 File Offset: 0x000082F9
		public void FleeBattle()
		{
			this.CheckAndSendMessage(new RejoinBattleRequestMessage(false));
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0000A107 File Offset: 0x00008307
		public void SendPartyMessage(string message)
		{
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0000A109 File Offset: 0x00008309
		private void OnClanInfoChangedMessage(ClanInfoChangedMessage clanInfoChangedMessage)
		{
			this.UpdateClanInfo(clanInfoChangedMessage.ClanHomeInfo);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0000A118 File Offset: 0x00008318
		protected override void OnTick()
		{
			if (this.LoggedIn && !this.IsInGame)
			{
				if (this._serverStatusTimer != null && this._serverStatusTimer.ElapsedMilliseconds > (long)LobbyClient.ServerStatusCheckDelay)
				{
					this._serverStatusTimer.Restart();
					this.CheckAndSendMessage(new GetServerStatusMessage());
				}
				if (this._friendListTimer != null && this._friendListTimer.ElapsedMilliseconds > (long)LobbyClient.FriendListCheckDelay)
				{
					this._friendListTimer.Restart();
					this.CheckAndSendMessage(new GetFriendListMessage());
				}
				if (this._recentPlayersTimer != null && this._recentPlayersTimer.ElapsedMilliseconds > (long)LobbyClient.RecentPlayersCheckDelay)
				{
					this._recentPlayersTimer.Restart();
					RecentPlayersManager.TrimPlayers();
					PlayerId[] recentPlayerIds = RecentPlayersManager.GetRecentPlayerIds();
					if (recentPlayerIds.Length != 0)
					{
						this.CheckAndSendMessage(new GetRecentPlayersStatusMessage(recentPlayerIds));
					}
				}
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0000A1DE File Offset: 0x000083DE
		private void OnInvitationToClanMessage(InvitationToClanMessage invitationToClanMessage)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnClanInvitationReceived(invitationToClanMessage.ClanName, invitationToClanMessage.ClanTag, false);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0000A1FD File Offset: 0x000083FD
		public void RejoinBattle()
		{
			this.CheckAndSendMessage(new RejoinBattleRequestMessage(true));
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0000A20B File Offset: 0x0000840B
		private void OnJoinPremadeGameAnswerMessage(JoinPremadeGameAnswerMessage joinPremadeGameAnswerMessage)
		{
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0000A20D File Offset: 0x0000840D
		public void OnBattleResultsSeen()
		{
			this.AssertCanPerformLobbyActions();
			this.CheckAndSendMessage(new BattleResultSeenMessage());
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0000A220 File Offset: 0x00008420
		private void OnCreatePremadeGameAnswerMessage(CreatePremadeGameAnswerMessage createPremadeGameAnswerMessage)
		{
			if (createPremadeGameAnswerMessage.Successful)
			{
				ILobbyClientSessionHandler handler = this._handler;
				if (handler == null)
				{
					return;
				}
				handler.OnPremadeGameCreated();
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0000A23A File Offset: 0x0000843A
		private void OnJoinPremadeGameRequestMessage(JoinPremadeGameRequestMessage joinPremadeGameRequestMessage)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnJoinPremadeGameRequested(joinPremadeGameRequestMessage.ClanName, joinPremadeGameRequestMessage.Sigil, joinPremadeGameRequestMessage.ChallengerPartyId, joinPremadeGameRequestMessage.ChallengerPlayers, joinPremadeGameRequestMessage.ChallengerPartyLeaderId, joinPremadeGameRequestMessage.PremadeGameType);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0000A270 File Offset: 0x00008470
		private void OnJoinPremadeGameRequestResultMessage(JoinPremadeGameRequestResultMessage joinPremadeGameRequestResultMessage)
		{
			if (joinPremadeGameRequestResultMessage.Successful)
			{
				ILobbyClientSessionHandler handler = this._handler;
				if (handler != null)
				{
					handler.OnJoinPremadeGameRequestSuccessful();
				}
				this.CurrentState = LobbyClient.State.WaitingToJoinPremadeGame;
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0000A294 File Offset: 0x00008494
		private async void OnClanDisbandedMessage(ClanDisbandedMessage clanDisbandedMessage)
		{
			ClanHomeInfo clanHomeInfo = await this.GetClanHomeInfo();
			this.UpdateClanInfo(clanHomeInfo);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0000A2CD File Offset: 0x000084CD
		private void OnClanGameCreationCancelledMessage(ClanGameCreationCancelledMessage clanGameCreationCancelledMessage)
		{
			this.CurrentState = LobbyClient.State.AtLobby;
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPremadeGameCreationCancelled();
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0000A2E6 File Offset: 0x000084E6
		private void OnPremadeGameEligibilityStatusMessage(PremadeGameEligibilityStatusMessage premadeGameEligibilityStatusMessage)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler != null)
			{
				handler.OnPremadeGameEligibilityStatusReceived(premadeGameEligibilityStatusMessage.EligibleGameTypes.Length != 0);
			}
			this.IsEligibleToCreatePremadeGame = premadeGameEligibilityStatusMessage.EligibleGameTypes.Length != 0;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0000A314 File Offset: 0x00008514
		private async void OnKickedFromClan(KickedFromClanMessage kickedFromClanMessage)
		{
			ClanHomeInfo clanHomeInfo = await this.GetClanHomeInfo();
			this.UpdateClanInfo(clanHomeInfo);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0000A350 File Offset: 0x00008550
		private async void OnPartyPlayerLeftClan(PartyPlayerLeftClanMessage partyPlayerLeftClanMessage)
		{
			ClanHomeInfo clanHomeInfo = await this.GetClanHomeInfo();
			this.UpdateClanInfo(clanHomeInfo);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0000A389 File Offset: 0x00008589
		private void OnCustomBattleOverMessage(CustomBattleOverMessage message)
		{
			this.CurrentState = LobbyClient.State.AtLobby;
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnMatchmakerGameOver(message.OldExperience, message.NewExperience, new List<string>(), message.GoldGain, null, null, BattleCancelReason.None);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0000A3BC File Offset: 0x000085BC
		public void AcceptClanInvitation()
		{
			this.CheckAndSendMessage(new AcceptClanInvitationMessage());
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0000A3C9 File Offset: 0x000085C9
		public void DeclineClanInvitation()
		{
			this.CheckAndSendMessage(new DeclineClanInvitationMessage());
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0000A3D6 File Offset: 0x000085D6
		private void OnShowAnnouncementMessage(ShowAnnouncementMessage message)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnAnnouncementReceived(message.Announcement);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0000A3F0 File Offset: 0x000085F0
		public void MarkNotificationAsRead(int notificationID)
		{
			UpdateNotificationsMessage updateNotificationsMessage = new UpdateNotificationsMessage(new int[] { notificationID });
			this.CheckAndSendMessage(updateNotificationsMessage);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0000A414 File Offset: 0x00008614
		private void OnRejoinBattleRequestAnswerMessage(RejoinBattleRequestAnswerMessage rejoinBattleRequestAnswerMessage)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler != null)
			{
				handler.OnRejoinBattleRequestAnswered(rejoinBattleRequestAnswerMessage.IsSuccessful);
			}
			if (rejoinBattleRequestAnswerMessage.IsSuccessful && rejoinBattleRequestAnswerMessage.IsRejoinAccepted)
			{
				this.CurrentState = LobbyClient.State.SearchingBattle;
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000A444 File Offset: 0x00008644
		public void AcceptClanCreationRequest()
		{
			this.CheckAndSendMessage(new AcceptClanCreationRequestMessage());
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0000A451 File Offset: 0x00008651
		private void OnPendingBattleRejoinMessage(PendingBattleRejoinMessage pendingBattleRejoinMessage)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPendingRejoin();
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0000A463 File Offset: 0x00008663
		private void OnSigilChangeAnswerMessage(SigilChangeAnswerMessage message)
		{
			if (message.Successful)
			{
				ILobbyClientSessionHandler handler = this._handler;
				if (handler == null)
				{
					return;
				}
				handler.OnSigilChanged();
			}
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0000A47D File Offset: 0x0000867D
		public void DeclineClanCreationRequest()
		{
			this.CheckAndSendMessage(new DeclineClanCreationRequestMessage());
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0000A48A File Offset: 0x0000868A
		public void PromoteToClanLeader(PlayerId playerId, bool dontUseNameForUnknownPlayer)
		{
			this.CheckAndSendMessage(new PromoteToClanLeaderMessage(playerId, dontUseNameForUnknownPlayer));
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0000A499 File Offset: 0x00008699
		private void OnLobbyNotificationsMessage(LobbyNotificationsMessage message)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnNotificationsReceived(message.Notifications);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0000A4B1 File Offset: 0x000086B1
		public void KickFromClan(PlayerId playerId)
		{
			this.CheckAndSendMessage(new KickFromClanMessage(playerId));
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0000A4C0 File Offset: 0x000086C0
		public async Task<CheckClanParameterValidResult> ClanNameExists(string clanName)
		{
			return await base.CallFunction<CheckClanParameterValidResult>(new CheckClanNameValidMessage(clanName));
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0000A510 File Offset: 0x00008710
		public async Task<CheckClanParameterValidResult> ClanTagExists(string clanTag)
		{
			return await base.CallFunction<CheckClanParameterValidResult>(new CheckClanTagValidMessage(clanTag));
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0000A560 File Offset: 0x00008760
		public async Task<ClanHomeInfo> GetClanHomeInfo()
		{
			GetClanHomeInfoResult getClanHomeInfoResult = await base.CallFunction<GetClanHomeInfoResult>(new GetClanHomeInfoMessage());
			ClanHomeInfo clanHomeInfo;
			if (getClanHomeInfoResult != null)
			{
				this.UpdateClanInfo(getClanHomeInfoResult.ClanHomeInfo);
				clanHomeInfo = getClanHomeInfoResult.ClanHomeInfo;
			}
			else
			{
				this.UpdateClanInfo(null);
				clanHomeInfo = null;
			}
			return clanHomeInfo;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0000A5A5 File Offset: 0x000087A5
		public void AssignAsClanOfficer(PlayerId playerId, bool dontUseNameForUnknownPlayer)
		{
			this.CheckAndSendMessage(new AssignAsClanOfficerMessage(playerId, dontUseNameForUnknownPlayer));
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0000A5B4 File Offset: 0x000087B4
		public void RemoveClanOfficerRoleForPlayer(PlayerId playerId)
		{
			this.CheckAndSendMessage(new RemoveClanOfficerRoleForPlayerMessage(playerId));
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0000A5C4 File Offset: 0x000087C4
		private void UpdateClanInfo(ClanHomeInfo clanHomeInfo)
		{
			this.PlayersInClan.Clear();
			this.PlayerInfosInClan.Clear();
			this.ClanID = Guid.Empty;
			this.ClanInfo = null;
			this.ClanHomeInfo = clanHomeInfo;
			if (clanHomeInfo != null)
			{
				if (clanHomeInfo.IsInClan)
				{
					foreach (ClanPlayer clanPlayer in clanHomeInfo.ClanInfo.Players)
					{
						this.PlayersInClan.Add(clanPlayer);
					}
					foreach (ClanPlayerInfo clanPlayerInfo in clanHomeInfo.ClanPlayerInfos)
					{
						this.PlayerInfosInClan.Add(clanPlayerInfo);
					}
					this.ClanID = clanHomeInfo.ClanInfo.ClanId;
				}
				this.ClanInfo = clanHomeInfo.ClanInfo;
			}
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnClanInfoChanged();
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0000A68C File Offset: 0x0000888C
		public async Task<ClanLeaderboardInfo> GetClanLeaderboardInfo()
		{
			GetClanLeaderboardResult getClanLeaderboardResult = await base.CallFunction<GetClanLeaderboardResult>(new GetClanLeaderboardMessage());
			ClanLeaderboardInfo clanLeaderboardInfo;
			if (getClanLeaderboardResult != null)
			{
				clanLeaderboardInfo = getClanLeaderboardResult.ClanLeaderboardInfo;
			}
			else
			{
				clanLeaderboardInfo = null;
			}
			return clanLeaderboardInfo;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0000A6D4 File Offset: 0x000088D4
		public async Task<ClanInfo> GetPlayerClanInfo(PlayerId playerId)
		{
			GetPlayerClanInfoResult getPlayerClanInfoResult = await base.CallFunction<GetPlayerClanInfoResult>(new GetPlayerClanInfo(playerId));
			ClanInfo clanInfo;
			if (((getPlayerClanInfoResult != null) ? getPlayerClanInfoResult.ClanInfo : null) != null)
			{
				clanInfo = getPlayerClanInfoResult.ClanInfo;
			}
			else
			{
				clanInfo = null;
			}
			return clanInfo;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0000A721 File Offset: 0x00008921
		public void SendClanMessage(string message)
		{
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0000A724 File Offset: 0x00008924
		public async Task<PremadeGameList> GetPremadeGameList()
		{
			GetPremadeGameListResult getPremadeGameListResult = await base.CallFunction<GetPremadeGameListResult>(new GetPremadeGameListMessage());
			PremadeGameList premadeGameList;
			if (getPremadeGameListResult != null)
			{
				this.AvailablePremadeGames = getPremadeGameListResult.GameList;
				ILobbyClientSessionHandler handler = this._handler;
				if (handler != null)
				{
					handler.OnPremadeGameListReceived();
				}
				premadeGameList = getPremadeGameListResult.GameList;
			}
			else
			{
				premadeGameList = null;
			}
			return premadeGameList;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0000A76C File Offset: 0x0000896C
		public async Task<AvailableScenes> GetAvailableScenes()
		{
			GetAvailableScenesResult getAvailableScenesResult = await base.CallFunction<GetAvailableScenesResult>(new GetAvailableScenesMessage());
			AvailableScenes availableScenes;
			if (getAvailableScenesResult != null)
			{
				availableScenes = getAvailableScenesResult.AvailableScenes;
			}
			else
			{
				availableScenes = null;
			}
			return availableScenes;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0000A7B4 File Offset: 0x000089B4
		public async Task<PublishedLobbyNewsArticle[]> GetLobbyNews()
		{
			GetPublishedLobbyNewsMessageResult getPublishedLobbyNewsMessageResult = await base.CallFunction<GetPublishedLobbyNewsMessageResult>(new GetPublishedLobbyNewsMessage());
			PublishedLobbyNewsArticle[] array;
			if (getPublishedLobbyNewsMessageResult != null)
			{
				array = getPublishedLobbyNewsMessageResult.Content;
			}
			else
			{
				array = null;
			}
			return array;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0000A7F9 File Offset: 0x000089F9
		public void SetClanInformationText(string informationText)
		{
			this.CheckAndSendMessage(new SetClanInformationMessage(informationText));
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0000A807 File Offset: 0x00008A07
		public void AddClanAnnouncement(string announcement)
		{
			this.CheckAndSendMessage(new AddClanAnnouncementMessage(announcement));
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0000A815 File Offset: 0x00008A15
		public void EditClanAnnouncement(int announcementId, string text)
		{
			this.CheckAndSendMessage(new EditClanAnnouncementMessage(announcementId, text));
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0000A824 File Offset: 0x00008A24
		public void RemoveClanAnnouncement(int announcementId)
		{
			this.CheckAndSendMessage(new RemoveClanAnnouncementMessage(announcementId));
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0000A832 File Offset: 0x00008A32
		public void ChangeClanFaction(string faction)
		{
			this.CheckAndSendMessage(new ChangeClanFactionMessage(faction));
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0000A840 File Offset: 0x00008A40
		public void ChangeClanSigil(string sigil)
		{
			this.CheckAndSendMessage(new ChangeClanSigilMessage(sigil));
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0000A84E File Offset: 0x00008A4E
		public void DestroyClan()
		{
			this.CheckAndSendMessage(new DestroyClanMessage());
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0000A85B File Offset: 0x00008A5B
		public void InviteToClan(PlayerId invitedPlayerId, bool dontUseNameForUnknownPlayer)
		{
			this.CheckAndSendMessage(new InviteToClanMessage(invitedPlayerId, dontUseNameForUnknownPlayer));
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0000A86C File Offset: 0x00008A6C
		public async void CreatePremadeGame(string name, string gameType, string mapName, string factionA, string factionB, string password, PremadeGameType premadeGameType)
		{
			this.CurrentState = LobbyClient.State.WaitingToCreatePremadeGame;
			string text = ((!string.IsNullOrEmpty(password)) ? Common.CalculateMD5Hash(password) : null);
			CreatePremadeGameMessageResult createPremadeGameMessageResult = await base.CallFunction<CreatePremadeGameMessageResult>(new CreatePremadeGameMessage(name, gameType, mapName, factionA, factionB, text, premadeGameType));
			if (createPremadeGameMessageResult == null || !createPremadeGameMessageResult.Successful)
			{
				this.CurrentState = LobbyClient.State.AtLobby;
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0000A8E1 File Offset: 0x00008AE1
		public void CancelCreatingPremadeGame()
		{
			this.CheckAndSendMessage(new CancelCreatingPremadeGameMessage());
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		public void RequestToJoinPremadeGame(Guid gameId, string password)
		{
			string text = Common.CalculateMD5Hash(password);
			this.CheckAndSendMessage(new RequestToJoinPremadeGameMessage(gameId, text));
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0000A911 File Offset: 0x00008B11
		public void AcceptJoinPremadeGameRequest(Guid partyId)
		{
			this.CheckAndSendMessage(new AcceptJoinPremadeGameRequestMessage(partyId));
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0000A91F File Offset: 0x00008B1F
		public void DeclineJoinPremadeGameRequest(Guid partyId)
		{
			this.CheckAndSendMessage(new DeclineJoinPremadeGameRequestMessage(partyId));
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0000A92D File Offset: 0x00008B2D
		public void InviteToParty(PlayerId playerId, bool dontUseNameForUnknownPlayer)
		{
			this.CheckAndSendMessage(new InviteToPartyMessage(playerId, dontUseNameForUnknownPlayer));
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0000A93C File Offset: 0x00008B3C
		public void DisbandParty()
		{
			this.CheckAndSendMessage(new DisbandPartyMessage());
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0000A949 File Offset: 0x00008B49
		public void KickPlayerFromParty(PlayerId playerId)
		{
			this.CheckAndSendMessage(new KickPlayerFromPartyMessage(playerId));
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0000A957 File Offset: 0x00008B57
		public void OnPlayerNameUpdated(string name)
		{
			this._userName = name;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0000A960 File Offset: 0x00008B60
		public void ToggleUseClanSigil(bool isUsed)
		{
			this.CheckAndSendMessage(new UpdateUsingClanSigil(isUsed));
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000A96E File Offset: 0x00008B6E
		public void PromotePlayerToPartyLeader(PlayerId playerId)
		{
			this.CheckAndSendMessage(new PromotePlayerToPartyLeaderMessage(playerId));
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000A97C File Offset: 0x00008B7C
		public void ChangeSigil(string sigilId)
		{
			this.CheckAndSendMessage(new ChangePlayerSigilMessage(sigilId));
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000A98C File Offset: 0x00008B8C
		public async Task<bool> InviteToPlatformSession(PlayerId playerId)
		{
			bool flag = false;
			if (this._handler != null)
			{
				flag = await this._handler.OnInviteToPlatformSession(playerId);
			}
			return flag;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0000A9DC File Offset: 0x00008BDC
		public async void EndCustomGame()
		{
			await base.CallFunction<EndHostingCustomGameResult>(new EndHostingCustomGameMessage());
			ILobbyClientSessionHandler handler = this._handler;
			if (handler != null)
			{
				handler.OnCustomGameEnd();
			}
			this.CurrentState = LobbyClient.State.AtLobby;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0000AA18 File Offset: 0x00008C18
		public async void RegisterCustomGame(string gameModule, string gameType, string serverName, int maxPlayerCount, string map, string uniqueMapId, string gamePassword, string adminPassword, int port)
		{
			this.CustomGameType = gameType;
			this.CustomGameScene = map;
			this.CurrentState = LobbyClient.State.WaitingToRegisterCustomGame;
			RegisterCustomGameResult registerCustomGameResult = await base.CallFunction<RegisterCustomGameResult>(new RegisterCustomGameMessage(gameModule, gameType, serverName, maxPlayerCount, map, uniqueMapId, gamePassword, adminPassword, port));
			Debug.Print("Register custom game server response received", 0, Debug.DebugColor.White, 17592186044416UL);
			if (registerCustomGameResult.Success)
			{
				this.CurrentState = LobbyClient.State.HostingCustomGame;
				ILobbyClientSessionHandler handler = this._handler;
				if (handler != null)
				{
					handler.OnRegisterCustomGameServerResponse();
				}
			}
			else
			{
				this.CurrentState = LobbyClient.State.AtLobby;
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0000AA9F File Offset: 0x00008C9F
		public void UpdateCustomGameData(string newGameType, string newMap, int newCount)
		{
			base.SendMessage(new UpdateCustomGameData(newGameType, newMap, newCount));
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0000AAAF File Offset: 0x00008CAF
		public void ResponseCustomGameClientConnection(PlayerJoinGameResponseDataFromHost[] playerJoinData)
		{
			base.SendMessage(new ResponseCustomGameClientConnectionMessage(playerJoinData));
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0000AABD File Offset: 0x00008CBD
		public void AcceptPartyInvitation()
		{
			this.IsPartyInvitationPopupActive = false;
			this.CheckAndSendMessage(new AcceptPartyInvitationMessage());
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0000AAD1 File Offset: 0x00008CD1
		public void DeclinePartyInvitation()
		{
			this.IsPartyInvitationPopupActive = false;
			this.CheckAndSendMessage(new DeclinePartyInvitationMessage());
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0000AAE5 File Offset: 0x00008CE5
		public void AcceptPartyJoinRequest(PlayerId playerId)
		{
			this.IsPartyJoinRequestPopupActive = false;
			this.CheckAndSendMessage(new AcceptPartyJoinRequestMessage(playerId));
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0000AAFA File Offset: 0x00008CFA
		public void DeclinePartyJoinRequest(PlayerId playerId, PartyJoinDeclineReason reason)
		{
			this.IsPartyJoinRequestPopupActive = false;
			this.CheckAndSendMessage(new DeclinePartyJoinRequestMessage(playerId, reason));
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0000AB10 File Offset: 0x00008D10
		public void UpdateCharacter(BodyProperties bodyProperties, bool isFemale)
		{
			this.AssertCanPerformLobbyActions();
			base.SendMessage(new UpdateCharacterMessage(bodyProperties, isFemale));
			if (this.CanPerformLobbyActions)
			{
				this.PlayerData.BodyProperties = bodyProperties;
				this.PlayerData.IsFemale = isFemale;
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000AB48 File Offset: 0x00008D48
		public async Task<bool> UpdateShownBadgeId(string shownBadgeId)
		{
			this.AssertCanPerformLobbyActions();
			UpdateShownBadgeIdMessageResult updateShownBadgeIdMessageResult = await base.CallFunction<UpdateShownBadgeIdMessageResult>(new UpdateShownBadgeIdMessage(shownBadgeId));
			if (updateShownBadgeIdMessageResult != null && updateShownBadgeIdMessageResult.Successful)
			{
				this.PlayerData.ShownBadgeId = shownBadgeId;
			}
			return updateShownBadgeIdMessageResult != null && updateShownBadgeIdMessageResult.Successful;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0000AB98 File Offset: 0x00008D98
		public async Task<AnotherPlayerData> GetAnotherPlayerState(PlayerId playerId)
		{
			this.AssertCanPerformLobbyActions();
			GetAnotherPlayerStateMessageResult getAnotherPlayerStateMessageResult = await base.CallFunction<GetAnotherPlayerStateMessageResult>(new GetAnotherPlayerStateMessage(playerId));
			AnotherPlayerData anotherPlayerData;
			if (getAnotherPlayerStateMessageResult != null)
			{
				anotherPlayerData = getAnotherPlayerStateMessageResult.AnotherPlayerData;
			}
			else
			{
				anotherPlayerData = new AnotherPlayerData(AnotherPlayerState.NoAnswer, 0);
			}
			return anotherPlayerData;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		public async Task<PlayerData> GetAnotherPlayerData(PlayerId playerID)
		{
			this.AssertCanPerformLobbyActions();
			await this.WaitForPendingRequestCompletion(LobbyClient.PendingRequest.PlayerData, playerID);
			PlayerData playerData;
			PlayerData playerData2;
			if (this._cachedPlayerDatas.TryGetValue(playerID, out playerData))
			{
				playerData2 = playerData;
			}
			else
			{
				GetAnotherPlayerDataMessageResult getAnotherPlayerDataMessageResult = await this.CreatePendingRequest<GetAnotherPlayerDataMessageResult>(LobbyClient.PendingRequest.PlayerData, playerID, base.CallFunction<GetAnotherPlayerDataMessageResult>(new GetAnotherPlayerDataMessage(playerID)));
				if (((getAnotherPlayerDataMessageResult != null) ? getAnotherPlayerDataMessageResult.AnotherPlayerData : null) != null)
				{
					this._cachedPlayerDatas[playerID] = getAnotherPlayerDataMessageResult.AnotherPlayerData;
				}
				playerData2 = ((getAnotherPlayerDataMessageResult != null) ? getAnotherPlayerDataMessageResult.AnotherPlayerData : null);
			}
			return playerData2;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0000AC38 File Offset: 0x00008E38
		public async Task<MatchmakingQueueStats> GetPlayerCountInQueue()
		{
			GetPlayerCountInQueueResult getPlayerCountInQueueResult = await base.CallFunction<GetPlayerCountInQueueResult>(new GetPlayerCountInQueue());
			MatchmakingQueueStats matchmakingQueueStats;
			if (getPlayerCountInQueueResult != null)
			{
				matchmakingQueueStats = getPlayerCountInQueueResult.MatchmakingQueueStats;
			}
			else
			{
				matchmakingQueueStats = MatchmakingQueueStats.Empty;
			}
			return matchmakingQueueStats;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0000AC80 File Offset: 0x00008E80
		public async Task<List<ValueTuple<PlayerId, AnotherPlayerData>>> GetOtherPlayersState(List<PlayerId> players)
		{
			this.AssertCanPerformLobbyActions();
			GetOtherPlayersStateMessageResult getOtherPlayersStateMessageResult = await base.CallFunction<GetOtherPlayersStateMessageResult>(new GetOtherPlayersStateMessage(players));
			return (getOtherPlayersStateMessageResult != null) ? getOtherPlayersStateMessageResult.States : null;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		public async Task<MatchmakingWaitTimeStats> GetMatchmakingWaitTimes()
		{
			GetAverageMatchmakingWaitTimesResult getAverageMatchmakingWaitTimesResult = await base.CallFunction<GetAverageMatchmakingWaitTimesResult>(new GetAverageMatchmakingWaitTimesMessage());
			MatchmakingWaitTimeStats matchmakingWaitTimeStats;
			if (getAverageMatchmakingWaitTimesResult != null)
			{
				matchmakingWaitTimeStats = getAverageMatchmakingWaitTimesResult.MatchmakingWaitTimeStats;
			}
			else
			{
				matchmakingWaitTimeStats = MatchmakingWaitTimeStats.Empty;
			}
			return matchmakingWaitTimeStats;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0000AD18 File Offset: 0x00008F18
		public async Task<Badge[]> GetPlayerBadges()
		{
			GetPlayerBadgesMessageResult getPlayerBadgesMessageResult = await base.CallFunction<GetPlayerBadgesMessageResult>(new GetPlayerBadgesMessage());
			List<Badge> list = new List<Badge>();
			if (getPlayerBadgesMessageResult != null)
			{
				string[] badges = getPlayerBadgesMessageResult.Badges;
				for (int i = 0; i < badges.Length; i++)
				{
					Badge byId = BadgeManager.GetById(badges[i]);
					if (byId != null)
					{
						list.Add(byId);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0000AD60 File Offset: 0x00008F60
		public async Task<PlayerStatsBase[]> GetPlayerStats(PlayerId playerID)
		{
			PlayerStatsBase[] array;
			PlayerStatsBase[] array2;
			if (this._cachedPlayerStats.TryGetValue(playerID, out array))
			{
				array2 = array;
			}
			else
			{
				GetPlayerStatsMessageResult getPlayerStatsMessageResult = await base.CallFunction<GetPlayerStatsMessageResult>(new GetPlayerStatsMessage(playerID));
				if (((getPlayerStatsMessageResult != null) ? getPlayerStatsMessageResult.PlayerStats : null) != null)
				{
					this._cachedPlayerStats[playerID] = getPlayerStatsMessageResult.PlayerStats;
				}
				array2 = ((getPlayerStatsMessageResult != null) ? getPlayerStatsMessageResult.PlayerStats : null);
			}
			return array2;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		public async Task<GameTypeRankInfo[]> GetGameTypeRankInfo(PlayerId playerID)
		{
			await this.WaitForPendingRequestCompletion(LobbyClient.PendingRequest.RankInfo, playerID);
			GameTypeRankInfo[] array;
			GameTypeRankInfo[] array2;
			if (this._cachedRankInfos.TryGetValue(playerID, out array))
			{
				array2 = array;
			}
			else
			{
				GetPlayerGameTypeRankInfoMessageResult getPlayerGameTypeRankInfoMessageResult = await this.CreatePendingRequest<GetPlayerGameTypeRankInfoMessageResult>(LobbyClient.PendingRequest.RankInfo, playerID, base.CallFunction<GetPlayerGameTypeRankInfoMessageResult>(new GetPlayerGameTypeRankInfoMessage(playerID)));
				if (((getPlayerGameTypeRankInfoMessageResult != null) ? getPlayerGameTypeRankInfoMessageResult.GameTypeRankInfo : null) != null)
				{
					this._cachedRankInfos[playerID] = getPlayerGameTypeRankInfoMessageResult.GameTypeRankInfo;
				}
				array2 = ((getPlayerGameTypeRankInfoMessageResult != null) ? getPlayerGameTypeRankInfoMessageResult.GameTypeRankInfo : null);
			}
			return array2;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0000AE00 File Offset: 0x00009000
		public async Task<int> GetRankedLeaderboardCount(string gameType)
		{
			GetRankedLeaderboardCountMessageResult getRankedLeaderboardCountMessageResult = await base.CallFunction<GetRankedLeaderboardCountMessageResult>(new GetRankedLeaderboardCountMessage(gameType));
			return (getRankedLeaderboardCountMessageResult != null) ? getRankedLeaderboardCountMessageResult.Count : 0;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0000AE50 File Offset: 0x00009050
		public async Task<PlayerLeaderboardData[]> GetRankedLeaderboard(string gameType, int startIndex, int count)
		{
			GetRankedLeaderboardMessageResult getRankedLeaderboardMessageResult = await base.CallFunction<GetRankedLeaderboardMessageResult>(new GetRankedLeaderboardMessage(gameType, startIndex, count));
			return (getRankedLeaderboardMessageResult != null) ? getRankedLeaderboardMessageResult.LeaderboardPlayers : null;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0000AEAD File Offset: 0x000090AD
		public void SendCreateClanMessage(string clanName, string clanTag, string clanFaction, string clanSigil)
		{
			this.AssertCanPerformLobbyActions();
			base.SendMessage(new CreateClanMessage(clanName, clanTag, clanFaction, clanSigil));
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0000AEC5 File Offset: 0x000090C5
		public void GetFriendList()
		{
			this.CheckAndSendMessage(new GetFriendListMessage());
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0000AED2 File Offset: 0x000090D2
		public void AddFriend(PlayerId friendId, bool dontUseNameForUnknownPlayer)
		{
			this.CheckAndSendMessage(new AddFriendMessage(friendId, dontUseNameForUnknownPlayer));
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0000AEE1 File Offset: 0x000090E1
		public void RemoveFriend(PlayerId friendId)
		{
			this.CheckAndSendMessage(new RemoveFriendMessage(friendId));
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0000AEEF File Offset: 0x000090EF
		public void RespondToFriendRequest(PlayerId playerId, bool dontUseNameForUnknownPlayer, bool isAccepted, bool isBlocked = false)
		{
			this.CheckAndSendMessage(new FriendRequestResponseMessage(playerId, dontUseNameForUnknownPlayer, isAccepted, isBlocked));
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0000AF04 File Offset: 0x00009104
		public void ReportPlayer(string gameId, PlayerId player, string playerName, PlayerReportType type, string message)
		{
			Guid guid;
			if (Guid.TryParse(gameId, out guid))
			{
				this.CheckAndSendMessage(new ReportPlayerMessage(guid, player, playerName, type, message));
				return;
			}
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnSystemMessageReceived(new TextObject("{=dnKQbXIZ}Could not report player: Game does not exist.", null).ToString());
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000AF50 File Offset: 0x00009150
		public void ChangeUsername(string username)
		{
			if ((this.PlayerData == null || this.PlayerData.Username != username) && username != null && username.Length >= Parameters.UsernameMinLength && username.Length <= Parameters.UsernameMaxLength && Common.IsAllLetters(username))
			{
				this.CheckAndSendMessage(new ChangeUsernameMessage(username));
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0000AFAC File Offset: 0x000091AC
		public void AddFriendByUsernameAndId(string username, int userId, bool dontUseNameForUnknownPlayer)
		{
			if (username != null && username.Length >= Parameters.UsernameMinLength && username.Length <= Parameters.UsernameMaxLength && Common.IsAllLetters(username) && userId >= 0 && userId <= Parameters.UserIdMax)
			{
				this.CheckAndSendMessage(new AddFriendByUsernameAndIdMessage(username, userId, dontUseNameForUnknownPlayer));
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0000AFF8 File Offset: 0x000091F8
		public async Task<bool> DoesPlayerWithUsernameAndIdExist(string username, int userId)
		{
			bool flag;
			if (username != null && username.Length >= Parameters.UsernameMinLength && username.Length <= Parameters.UsernameMaxLength && Common.IsAllLetters(username) && userId >= 0 && userId <= Parameters.UserIdMax)
			{
				GetPlayerByUsernameAndIdMessageResult getPlayerByUsernameAndIdMessageResult = await base.CallFunction<GetPlayerByUsernameAndIdMessageResult>(new GetPlayerByUsernameAndIdMessage(username, userId));
				flag = getPlayerByUsernameAndIdMessageResult != null && getPlayerByUsernameAndIdMessageResult.PlayerId.IsValid;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000B050 File Offset: 0x00009250
		public bool IsPlayerClanLeader(PlayerId playerID)
		{
			ClanPlayer clanPlayer = this.PlayersInClan.Find((ClanPlayer p) => p.PlayerId == playerID);
			return clanPlayer != null && clanPlayer.Role == ClanPlayerRole.Leader;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0000B090 File Offset: 0x00009290
		public bool IsPlayerClanOfficer(PlayerId playerID)
		{
			ClanPlayer clanPlayer = this.PlayersInClan.Find((ClanPlayer p) => p.PlayerId == playerID);
			return clanPlayer != null && clanPlayer.Role == ClanPlayerRole.Officer;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0000B0D0 File Offset: 0x000092D0
		public async Task<bool> UpdateUsedCosmeticItems([TupleElementNames(new string[] { "cosmeticId", "isEquipped" })] Dictionary<string, List<ValueTuple<string, bool>>> usedCosmetics)
		{
			List<CosmeticItemInfo> list = new List<CosmeticItemInfo>();
			foreach (string text in usedCosmetics.Keys)
			{
				foreach (ValueTuple<string, bool> valueTuple in usedCosmetics[text])
				{
					CosmeticItemInfo cosmeticItemInfo = new CosmeticItemInfo(text, valueTuple.Item1, valueTuple.Item2);
					list.Add(cosmeticItemInfo);
				}
			}
			UpdateUsedCosmeticItemsMessageResult updateUsedCosmeticItemsMessageResult = await base.CallFunction<UpdateUsedCosmeticItemsMessageResult>(new UpdateUsedCosmeticItemsMessage(list));
			if (updateUsedCosmeticItemsMessageResult != null && updateUsedCosmeticItemsMessageResult.Successful)
			{
				foreach (KeyValuePair<string, List<ValueTuple<string, bool>>> keyValuePair in usedCosmetics)
				{
					if (!string.IsNullOrWhiteSpace(keyValuePair.Key))
					{
						List<string> list2;
						if (!this.UsedCosmetics.TryGetValue(keyValuePair.Key, out list2))
						{
							list2 = new List<string>();
							this._usedCosmetics.Add(keyValuePair.Key, list2);
						}
						foreach (ValueTuple<string, bool> valueTuple2 in keyValuePair.Value)
						{
							string item = valueTuple2.Item1;
							if (valueTuple2.Item2)
							{
								list2.Add(item);
							}
							else
							{
								list2.Remove(item);
							}
						}
					}
				}
			}
			return updateUsedCosmeticItemsMessageResult != null && updateUsedCosmeticItemsMessageResult.Successful;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0000B120 File Offset: 0x00009320
		[return: TupleElementNames(new string[] { "isSuccessful", "finalGold" })]
		public async Task<ValueTuple<bool, int>> BuyCosmetic(string cosmeticId)
		{
			BuyCosmeticMessageResult buyCosmeticMessageResult = await base.CallFunction<BuyCosmeticMessageResult>(new BuyCosmeticMessage(cosmeticId));
			if (buyCosmeticMessageResult != null && buyCosmeticMessageResult.Successful)
			{
				this._ownedCosmetics.Add(cosmeticId);
			}
			return new ValueTuple<bool, int>(buyCosmeticMessageResult != null && buyCosmeticMessageResult.Successful, (buyCosmeticMessageResult != null) ? buyCosmeticMessageResult.Gold : 0);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0000B170 File Offset: 0x00009370
		[return: TupleElementNames(new string[] { "isSuccessful", "ownedCosmetics", "usedCosmetics" })]
		public async Task<ValueTuple<bool, List<string>, Dictionary<string, List<string>>>> GetCosmeticsInfo()
		{
			GetUserCosmeticsInfoMessageResult getUserCosmeticsInfoMessageResult = await base.CallFunction<GetUserCosmeticsInfoMessageResult>(new GetUserCosmeticsInfoMessage());
			if (getUserCosmeticsInfoMessageResult != null)
			{
				this._usedCosmetics = getUserCosmeticsInfoMessageResult.UsedCosmetics ?? new Dictionary<string, List<string>>();
				this._ownedCosmetics = getUserCosmeticsInfoMessageResult.OwnedCosmetics ?? new List<string>();
			}
			return new ValueTuple<bool, List<string>, Dictionary<string, List<string>>>(getUserCosmeticsInfoMessageResult != null && getUserCosmeticsInfoMessageResult.Successful, (getUserCosmeticsInfoMessageResult != null) ? getUserCosmeticsInfoMessageResult.OwnedCosmetics : null, (getUserCosmeticsInfoMessageResult != null) ? getUserCosmeticsInfoMessageResult.UsedCosmetics : null);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0000B1B8 File Offset: 0x000093B8
		public async Task<string> GetDedicatedCustomServerAuthToken()
		{
			GetDedicatedCustomServerAuthTokenMessageResult getDedicatedCustomServerAuthTokenMessageResult = await base.CallFunction<GetDedicatedCustomServerAuthTokenMessageResult>(new GetDedicatedCustomServerAuthTokenMessage());
			return (getDedicatedCustomServerAuthTokenMessageResult != null) ? getDedicatedCustomServerAuthTokenMessageResult.AuthToken : null;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0000B200 File Offset: 0x00009400
		public async Task<string> GetOfficialServerProviderName()
		{
			GetOfficialServerProviderNameResult getOfficialServerProviderNameResult = await base.CallFunction<GetOfficialServerProviderNameResult>(new GetOfficialServerProviderNameMessage());
			return ((getOfficialServerProviderNameResult != null) ? getOfficialServerProviderNameResult.Name : null) ?? string.Empty;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0000B248 File Offset: 0x00009448
		public async Task<string> GetPlayerBannerlordID(PlayerId playerId)
		{
			await this.WaitForPendingRequestCompletion(LobbyClient.PendingRequest.BannerlordID, playerId);
			string text;
			string text2;
			if (this._cachedPlayerBannerlordIDs.TryGetValue(playerId, out text))
			{
				text2 = text;
			}
			else
			{
				GetBannerlordIDMessageResult getBannerlordIDMessageResult = await this.CreatePendingRequest<GetBannerlordIDMessageResult>(LobbyClient.PendingRequest.BannerlordID, playerId, base.CallFunction<GetBannerlordIDMessageResult>(new GetBannerlordIDMessage(playerId)));
				if (getBannerlordIDMessageResult != null && getBannerlordIDMessageResult.BannerlordID != null)
				{
					this._cachedPlayerBannerlordIDs[playerId] = getBannerlordIDMessageResult.BannerlordID;
				}
				text2 = ((getBannerlordIDMessageResult != null) ? getBannerlordIDMessageResult.BannerlordID : null) ?? string.Empty;
			}
			return text2;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0000B298 File Offset: 0x00009498
		public bool IsKnownPlayer(PlayerId playerID)
		{
			bool flag = playerID == this._playerId;
			bool flag2 = this.FriendIDs.Contains(playerID);
			bool flag3 = this.IsInParty && this.PlayersInParty.Any<PartyPlayerInLobbyClient>((PartyPlayerInLobbyClient p) => p.PlayerId.Equals(playerID));
			bool flag4 = this.IsInClan && this.PlayersInClan.Any<ClanPlayer>((ClanPlayer p) => p.PlayerId.Equals(playerID));
			return flag || flag2 || flag3 || flag4;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0000B324 File Offset: 0x00009524
		public async Task<long> GetPingToServer(string IpAddress)
		{
			long num;
			try
			{
				using (Ping ping = new Ping())
				{
					PingReply pingReply = await ping.SendPingAsync(IpAddress, (int)TimeSpan.FromSeconds(15.0).TotalMilliseconds);
					num = ((pingReply.Status != IPStatus.Success) ? (-1L) : pingReply.RoundtripTime);
				}
			}
			catch (Exception)
			{
				num = -1L;
			}
			return num;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0000B369 File Offset: 0x00009569
		private void AssertCanPerformLobbyActions()
		{
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0000B36C File Offset: 0x0000956C
		public async Task<bool> SendPSPlayerJoinedToPlayerSessionMessage(ulong inviterPlayerId)
		{
			PSPlayerJoinedToPlayerSessionMessage psplayerJoinedToPlayerSessionMessage = new PSPlayerJoinedToPlayerSessionMessage(inviterPlayerId);
			return (await base.CallFunction<PSPlayerJoinedToPlayerSessionMessageResult>(psplayerJoinedToPlayerSessionMessage)).Successful;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0000B3BC File Offset: 0x000095BC
		public async Task<bool> SendPlatformPlayerJoinedToPlayerSessionMessage(PlayerId inviterPlayerId)
		{
			PlatformPlayerJoinedToPlayerSessionMessage platformPlayerJoinedToPlayerSessionMessage = new PlatformPlayerJoinedToPlayerSessionMessage(inviterPlayerId);
			return (await base.CallFunction<PSPlayerJoinedToPlayerSessionMessageResult>(platformPlayerJoinedToPlayerSessionMessage)).Successful;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0000B40C File Offset: 0x0000960C
		private Task WaitForPendingRequestCompletion(LobbyClient.PendingRequest requestType, PlayerId playerId)
		{
			Task task;
			if (this._pendingPlayerRequests.TryGetValue(new ValueTuple<LobbyClient.PendingRequest, PlayerId>(requestType, playerId), out task))
			{
				return task;
			}
			return Task.CompletedTask;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0000B438 File Offset: 0x00009638
		private async Task<T> CreatePendingRequest<T>(LobbyClient.PendingRequest requestType, PlayerId playerId, Task<T> requestTask)
		{
			ValueTuple<LobbyClient.PendingRequest, PlayerId> key = new ValueTuple<LobbyClient.PendingRequest, PlayerId>(requestType, playerId);
			T t;
			try
			{
				this._pendingPlayerRequests[key] = requestTask;
				t = await requestTask;
			}
			finally
			{
				this._pendingPlayerRequests.Remove(key);
			}
			return t;
		}

		// Token: 0x040002CC RID: 716
		public const string TestRegionCode = "Test";

		// Token: 0x040002CD RID: 717
		private static readonly int ServerStatusCheckDelay = 120000;

		// Token: 0x040002CE RID: 718
		private static int _friendListCheckDelay;

		// Token: 0x040002CF RID: 719
		private static readonly int CheckForCustomGamesCount = 5;

		// Token: 0x040002D0 RID: 720
		private static readonly int CheckForCustomGamesDelay = 5000;

		// Token: 0x040002D1 RID: 721
		private ILobbyClientSessionHandler _handler;

		// Token: 0x040002D2 RID: 722
		private readonly Stopwatch _serverStatusTimer;

		// Token: 0x040002D3 RID: 723
		private readonly Stopwatch _friendListTimer;

		// Token: 0x040002D4 RID: 724
		private readonly Stopwatch _recentPlayersTimer;

		// Token: 0x040002D5 RID: 725
		private static readonly int RecentPlayersCheckDelay = 40000;

		// Token: 0x040002DA RID: 730
		private List<string> _ownedCosmetics;

		// Token: 0x040002DB RID: 731
		private Dictionary<string, List<string>> _usedCosmetics;

		// Token: 0x040002DE RID: 734
		private ServerStatus _serverStatus;

		// Token: 0x040002DF RID: 735
		private DateTime _matchmakerBlockedTime;

		// Token: 0x040002E0 RID: 736
		private TextObject _logOutReason;

		// Token: 0x040002E1 RID: 737
		private LobbyClient.State _state;

		// Token: 0x040002E2 RID: 738
		private string _userName;

		// Token: 0x040002E3 RID: 739
		private PlayerId _playerId;

		// Token: 0x040002E7 RID: 743
		private List<ModuleInfoModel> _loadedUnofficialModules;

		// Token: 0x040002F8 RID: 760
		private TimedDictionaryCache<PlayerId, GameTypeRankInfo[]> _cachedRankInfos;

		// Token: 0x040002F9 RID: 761
		private TimedDictionaryCache<PlayerId, PlayerStatsBase[]> _cachedPlayerStats;

		// Token: 0x040002FA RID: 762
		private TimedDictionaryCache<PlayerId, PlayerData> _cachedPlayerDatas;

		// Token: 0x040002FB RID: 763
		private TimedDictionaryCache<PlayerId, string> _cachedPlayerBannerlordIDs;

		// Token: 0x040002FC RID: 764
		private Dictionary<ValueTuple<LobbyClient.PendingRequest, PlayerId>, Task> _pendingPlayerRequests;

		// Token: 0x02000193 RID: 403
		public enum State
		{
			// Token: 0x04000589 RID: 1417
			Idle,
			// Token: 0x0400058A RID: 1418
			Working,
			// Token: 0x0400058B RID: 1419
			Connected,
			// Token: 0x0400058C RID: 1420
			SessionRequested,
			// Token: 0x0400058D RID: 1421
			AtLobby,
			// Token: 0x0400058E RID: 1422
			SearchingToRejoinBattle,
			// Token: 0x0400058F RID: 1423
			RequestingToSearchBattle,
			// Token: 0x04000590 RID: 1424
			RequestingToCancelSearchBattle,
			// Token: 0x04000591 RID: 1425
			SearchingBattle,
			// Token: 0x04000592 RID: 1426
			AtBattle,
			// Token: 0x04000593 RID: 1427
			QuittingFromBattle,
			// Token: 0x04000594 RID: 1428
			WaitingToCreatePremadeGame,
			// Token: 0x04000595 RID: 1429
			WaitingToJoinPremadeGame,
			// Token: 0x04000596 RID: 1430
			WaitingToRegisterCustomGame,
			// Token: 0x04000597 RID: 1431
			HostingCustomGame,
			// Token: 0x04000598 RID: 1432
			WaitingToJoinCustomGame,
			// Token: 0x04000599 RID: 1433
			InCustomGame
		}

		// Token: 0x02000194 RID: 404
		private enum PendingRequest
		{
			// Token: 0x0400059B RID: 1435
			RankInfo,
			// Token: 0x0400059C RID: 1436
			PlayerData,
			// Token: 0x0400059D RID: 1437
			BannerlordID
		}
	}
}
