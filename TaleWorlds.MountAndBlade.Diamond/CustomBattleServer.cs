using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Messages.FromCustomBattleServer.ToCustomBattleServerManager;
using Messages.FromCustomBattleServerManager.ToCustomBattleServer;
using TaleWorlds.Diamond;
using TaleWorlds.Diamond.ClientApplication;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000110 RID: 272
	public class CustomBattleServer : Client<CustomBattleServer>
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x000074DA File Offset: 0x000056DA
		public bool Finished
		{
			get
			{
				return this._state == CustomBattleServer.State.Finished;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x000074E5 File Offset: 0x000056E5
		public bool IsRegistered
		{
			get
			{
				return this._state == CustomBattleServer.State.RegisteredGame || this._state == CustomBattleServer.State.RegisteredServer;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x000074FB File Offset: 0x000056FB
		public bool IsPlaying
		{
			get
			{
				return this._state == CustomBattleServer.State.RegisteredGame;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00007506 File Offset: 0x00005706
		public bool Connected
		{
			get
			{
				return this.CurrentState != CustomBattleServer.State.Working && this.CurrentState > CustomBattleServer.State.Idle;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0000751C File Offset: 0x0000571C
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x00007524 File Offset: 0x00005724
		public CustomBattleServer.State CurrentState
		{
			get
			{
				return this._state;
			}
			private set
			{
				if (this._state != value)
				{
					CustomBattleServer.State state = this._state;
					this._state = value;
					if (this._handler != null)
					{
						this._handler.OnStateChanged(state);
					}
				}
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0000755C File Offset: 0x0000575C
		public bool IsIdle
		{
			get
			{
				return this._state == CustomBattleServer.State.RegisteredGame && this._customBattlePlayers.Count == 0 && this._useTimeoutTimer && this._timeoutTimer.ElapsedMilliseconds > (long)this._timeoutDuration;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00007594 File Offset: 0x00005794
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x0000759C File Offset: 0x0000579C
		public string CustomGameType { get; private set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x000075A5 File Offset: 0x000057A5
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x000075AD File Offset: 0x000057AD
		public string CustomGameScene { get; private set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x000075B6 File Offset: 0x000057B6
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x000075BE File Offset: 0x000057BE
		public int Port { get; private set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x000075C7 File Offset: 0x000057C7
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x000075CF File Offset: 0x000057CF
		public MultipleBattleResult BattleResult { get; private set; }

		// Token: 0x060005ED RID: 1517 RVA: 0x000075D8 File Offset: 0x000057D8
		public CustomBattleServer(DiamondClientApplication diamondClientApplication, IClientSessionProvider<CustomBattleServer> provider)
			: base(diamondClientApplication, provider, false)
		{
			this._peerId = new PeerId(Guid.NewGuid());
			this._customBattlePlayers = new List<PlayerId>();
			this._requestedPlayers = new List<PlayerId>();
			this._timeoutTimer = new Stopwatch();
			this._terminationTime = null;
			this._state = CustomBattleServer.State.Idle;
			this._timer = new Stopwatch();
			this._timer.Start();
			if (!base.Application.Parameters.TryGetParameterAsInt("CustomBattleServer.TimeoutDuration", out this._timeoutDuration))
			{
				this._timeoutDuration = this._defaultServerTimeoutDuration;
			}
			this._badgeComponent = null;
			this._badgeComponentPlayers = new List<PlayerData>();
			this.BattleResult = new MultipleBattleResult();
			base.AddMessageHandler<ClientWantsToConnectCustomGameMessage>(new ClientMessageHandler<ClientWantsToConnectCustomGameMessage>(this.OnClientWantsToConnectCustomGameMessage));
			base.AddMessageHandler<ClientQuitFromCustomGameMessage>(new ClientMessageHandler<ClientQuitFromCustomGameMessage>(this.OnClientQuitFromCustomGameMessage));
			base.AddMessageHandler<TerminateOperationCustomMessage>(new ClientMessageHandler<TerminateOperationCustomMessage>(this.OnTerminateOperationCustomMessage));
			base.AddMessageHandler<SetChatFilterListsMessage>(new ClientMessageHandler<SetChatFilterListsMessage>(this.OnSetChatFilterListsMessage));
			base.AddMessageHandler<PlayerDisconnectedFromLobbyMessage>(new ClientMessageHandler<PlayerDisconnectedFromLobbyMessage>(this.OnPlayerDisconnectedFromLobbyMessage));
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000076F4 File Offset: 0x000058F4
		public void SetBadgeComponent(IBadgeComponent badgeComponent)
		{
			this._badgeComponent = badgeComponent;
			if (this._badgeComponent != null)
			{
				foreach (PlayerData playerData in this._badgeComponentPlayers)
				{
					this._badgeComponent.OnPlayerJoin(playerData);
				}
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0000775C File Offset: 0x0000595C
		public void Connect(ICustomBattleServerSessionHandler handler, string authToken, bool isSinglePlatformServer, string[] loadedModuleIDs, bool allowsOptionalModules, bool isPlayerHosted)
		{
			this._handler = handler;
			this._authToken = authToken;
			this._allowsOptionalModules = allowsOptionalModules;
			this._useTimeoutTimer = !isPlayerHosted;
			this._isSinglePlatformServer = isSinglePlatformServer;
			this._loadedModules = new List<ModuleInfoModel>();
			foreach (ModuleInfo moduleInfo in ModuleHelper.GetSortedModules(loadedModuleIDs))
			{
				if (!allowsOptionalModules && moduleInfo.Category == ModuleCategory.MultiplayerOptional)
				{
					throw new InvalidOperationException("Optional modules are explicitly disallowed, yet an optional module (" + moduleInfo.Id + ") was loaded! You must use category 'Server' instead of 'MultiplayerOptional'.");
				}
				ModuleInfoModel moduleInfoModel;
				if (ModuleInfoModel.TryCreateForSession(moduleInfo, out moduleInfoModel))
				{
					this._loadedModules.Add(moduleInfoModel);
				}
			}
			this.CurrentState = CustomBattleServer.State.Working;
			base.BeginConnect();
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00007828 File Offset: 0x00005A28
		public override void OnConnected()
		{
			base.OnConnected();
			this.CurrentState = CustomBattleServer.State.Connected;
			if (this._handler != null)
			{
				this._handler.OnConnected();
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000784A File Offset: 0x00005A4A
		public override void OnCantConnect()
		{
			base.OnCantConnect();
			this.CurrentState = CustomBattleServer.State.Idle;
			if (this._handler != null)
			{
				this._handler.OnCantConnect();
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000786C File Offset: 0x00005A6C
		public override void OnDisconnected()
		{
			base.OnDisconnected();
			this.CurrentState = CustomBattleServer.State.Idle;
			if (this._handler != null)
			{
				this._handler.OnDisconnected();
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00007890 File Offset: 0x00005A90
		protected override void OnTick()
		{
			if (this._terminationTime != null && this._terminationTime < DateTime.UtcNow)
			{
				throw new Exception("Now I am become Death, the destroyer of worlds");
			}
			long elapsedMilliseconds = this._timer.ElapsedMilliseconds;
			float num = (float)(elapsedMilliseconds - this._previousTimeInMS);
			this._previousTimeInMS = elapsedMilliseconds;
			float num2 = num / 1000f;
			this._battleResultUpdateTimeElapsed += num2;
			if (this._battleResultUpdateTimeElapsed >= 5f)
			{
				if (this._latestQueuedBattleResult != null && this._latestQueuedTeamScores != null && this._latestQueuedPlayerScores != null)
				{
					base.SendMessage(new CustomBattleServerStatsUpdateMessage(this._latestQueuedBattleResult, this._latestQueuedTeamScores, this._latestQueuedPlayerScores));
					this._latestQueuedBattleResult = null;
					this._latestQueuedTeamScores = null;
					this._latestQueuedPlayerScores = null;
				}
				this._battleResultUpdateTimeElapsed = 0f;
			}
			CustomBattleServer.State state = this._state;
			if (state == CustomBattleServer.State.Connected)
			{
				this.DoLogin();
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00007984 File Offset: 0x00005B84
		private async void DoLogin()
		{
			this._state = CustomBattleServer.State.SessionRequested;
			LoginResult loginResult = await base.Login(new CustomBattleServerReadyMessage(this._peerId, base.ApplicationVersion, this._authToken, this._loadedModules.ToArray(), this._allowsOptionalModules));
			if (loginResult != null && loginResult.Successful)
			{
				this._state = CustomBattleServer.State.RegisteredServer;
			}
			else
			{
				Console.WriteLine("Login Failed! Server is shutting down.");
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000079BD File Offset: 0x00005BBD
		private void OnClientWantsToConnectCustomGameMessage(ClientWantsToConnectCustomGameMessage message)
		{
			this.HandleOnClientWantsToConnectCustomGameMessage(message);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000079C8 File Offset: 0x00005BC8
		private async void HandleOnClientWantsToConnectCustomGameMessage(ClientWantsToConnectCustomGameMessage message)
		{
			List<PlayerJoinGameResponseDataFromHost> responses = new List<PlayerJoinGameResponseDataFromHost>();
			if (this.CurrentState == CustomBattleServer.State.Finished)
			{
				foreach (PlayerJoinGameData playerJoinGameData2 in message.PlayerJoinGameData)
				{
					responses.Add(new PlayerJoinGameResponseDataFromHost
					{
						PlayerId = playerJoinGameData2.PlayerId,
						PeerIndex = -1,
						SessionKey = -1,
						CustomGameJoinResponse = CustomGameJoinResponse.CustomGameServerFinishing
					});
				}
			}
			else
			{
				PlayerJoinGameData[] requestedPlayers = message.PlayerJoinGameData;
				for (int k = 0; k < requestedPlayers.Length; k++)
				{
					if (requestedPlayers[k] != null)
					{
						PlayerJoinGameData playerJoinGameData3 = requestedPlayers[k];
						Debug.Print(string.Concat(new object[] { "Player ", playerJoinGameData3.Name, " - ", playerJoinGameData3.PlayerId, " with IP address ", playerJoinGameData3.IpAddress, " wants to join the game" }), 0, Debug.DebugColor.White, 17592186044416UL);
					}
				}
				int j;
				for (int i = 0; i < requestedPlayers.Length; i = j + 1)
				{
					if (requestedPlayers[i] != null)
					{
						List<PlayerJoinGameData> requestedGroup = new List<PlayerJoinGameData>();
						PlayerJoinGameData playerJoinGameData4 = requestedPlayers[i];
						Guid? guid = playerJoinGameData4.PartyId;
						if (guid == null)
						{
							requestedGroup.Add(playerJoinGameData4);
						}
						else
						{
							for (int l = i; l < requestedPlayers.Length; l++)
							{
								PlayerJoinGameData playerJoinGameData5 = requestedPlayers[l];
								guid = playerJoinGameData4.PartyId;
								if (guid.Equals((playerJoinGameData5 != null) ? playerJoinGameData5.PartyId : null))
								{
									requestedGroup.Add(playerJoinGameData5);
									requestedPlayers[l] = null;
								}
							}
						}
						bool flag = true;
						foreach (PlayerJoinGameData playerJoinGameData6 in requestedGroup)
						{
							if (this._requestedPlayers.Contains(playerJoinGameData6.PlayerId) || this._customBattlePlayers.Contains(playerJoinGameData6.PlayerId))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							this._timeoutTimer.Restart();
							foreach (PlayerJoinGameData playerJoinGameData7 in requestedGroup)
							{
								this._requestedPlayers.Add(playerJoinGameData7.PlayerId);
							}
							if (this._handler != null)
							{
								PlayerJoinGameResponseDataFromHost[] array = await this._handler.OnClientWantsToConnectCustomGame(requestedGroup.ToArray());
								if (this._badgeComponent != null)
								{
									foreach (PlayerJoinGameResponseDataFromHost playerJoinGameResponseDataFromHost in array)
									{
										if (playerJoinGameResponseDataFromHost.CustomGameJoinResponse == CustomGameJoinResponse.Success)
										{
											foreach (PlayerJoinGameData playerJoinGameData8 in requestedGroup)
											{
												if (playerJoinGameData8.PlayerId.Equals(playerJoinGameResponseDataFromHost.PlayerId))
												{
													this._badgeComponent.OnPlayerJoin(playerJoinGameData8.PlayerData);
													this._badgeComponentPlayers.Add(playerJoinGameData8.PlayerData);
												}
											}
										}
									}
								}
								responses.AddRange(array);
							}
						}
						else
						{
							foreach (PlayerJoinGameData playerJoinGameData9 in requestedGroup)
							{
								responses.Add(new PlayerJoinGameResponseDataFromHost
								{
									PlayerId = playerJoinGameData9.PlayerId,
									PeerIndex = -1,
									SessionKey = -1,
									CustomGameJoinResponse = CustomGameJoinResponse.NotAllPlayersReady
								});
							}
						}
						requestedGroup = null;
					}
					j = i;
				}
				requestedPlayers = null;
			}
			this.ResponseCustomGameClientConnection(responses.ToArray());
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00007A0C File Offset: 0x00005C0C
		private void OnClientQuitFromCustomGameMessage(ClientQuitFromCustomGameMessage message)
		{
			if (this.CurrentState == CustomBattleServer.State.RegisteredGame && this._customBattlePlayers.Contains(message.PlayerId))
			{
				if (this._handler != null)
				{
					this._handler.OnClientQuitFromCustomGame(message.PlayerId);
				}
				this._customBattlePlayers.Remove(message.PlayerId);
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00007A60 File Offset: 0x00005C60
		public void OnPlayerDisconnectedFromLobbyMessage(PlayerDisconnectedFromLobbyMessage message)
		{
			this.HandlePlayerDisconnect(message.PlayerId, DisconnectType.DisconnectedFromLobby);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00007A70 File Offset: 0x00005C70
		private void OnTerminateOperationCustomMessage(TerminateOperationCustomMessage message)
		{
			Random random = new Random();
			this._terminationTime = new DateTime?(DateTime.UtcNow.AddMilliseconds((double)random.Next(3000, 10000)));
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00007AAC File Offset: 0x00005CAC
		private void OnSetChatFilterListsMessage(SetChatFilterListsMessage message)
		{
			if (this._handler != null)
			{
				this._handler.OnChatFilterListsReceived(message.ProfanityList, message.AllowList);
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00007AD0 File Offset: 0x00005CD0
		public void ResponseCustomGameClientConnection(PlayerJoinGameResponseDataFromHost[] playerJoinData)
		{
			if (this.CurrentState == CustomBattleServer.State.RegisteredGame)
			{
				foreach (PlayerJoinGameResponseDataFromHost playerJoinGameResponseDataFromHost in playerJoinData)
				{
					this._requestedPlayers.Remove(playerJoinGameResponseDataFromHost.PlayerId);
					if (playerJoinGameResponseDataFromHost.CustomGameJoinResponse == CustomGameJoinResponse.Success)
					{
						this._customBattlePlayers.Add(playerJoinGameResponseDataFromHost.PlayerId);
					}
				}
				base.SendMessage(new ResponseCustomGameClientConnectionMessage(playerJoinData));
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00007B34 File Offset: 0x00005D34
		public async Task RegisterGame(string gameModule, string gameType, string serverName, int maxPlayerCount, string scene, string uniqueSceneId, int port, string region, string gamePassword, string adminPassword, int permission)
		{
			await this.RegisterGame(0, gameModule, gameType, serverName, maxPlayerCount, scene, uniqueSceneId, port, region, gamePassword, adminPassword, permission, string.Empty);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00007BDC File Offset: 0x00005DDC
		public async Task RegisterGame(int gameDefinitionId, string gameModule, string gameType, string serverName, int maxPlayerCount, string scene, string uniqueSceneId, int port, string region, string gamePassword, string adminPassword, int permission, string overriddenIP)
		{
			this.Port = port;
			this.CustomGameType = gameType;
			this.CustomGameScene = scene;
			string text = null;
			bool flag = false;
			if (base.Application.Parameters.TryGetParameter("CustomBattleServer.Host.Address", out text))
			{
				flag = true;
			}
			if (overriddenIP != string.Empty)
			{
				flag = true;
				text = overriddenIP;
			}
			RegisterCustomGameMessageResponseMessage registerCustomGameMessageResponseMessage = await base.CallFunction<RegisterCustomGameMessageResponseMessage>(new RegisterCustomGameMessage(gameDefinitionId, gameModule, gameType, serverName, text, maxPlayerCount, scene, uniqueSceneId, gamePassword, adminPassword, port, region, permission, !this._isSinglePlatformServer, flag));
			this._shouldReportActivities = registerCustomGameMessageResponseMessage.ShouldReportActivities;
			this.CurrentState = CustomBattleServer.State.RegisteredGame;
			this._timeoutTimer.Start();
			if (this._handler != null)
			{
				this._handler.OnSuccessfulGameRegister();
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00007C93 File Offset: 0x00005E93
		public void UpdateCustomGameData(string newGameType, string newMap, int newCount)
		{
			base.SendMessage(new UpdateCustomGameData(newGameType, newMap, newCount));
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00007CA3 File Offset: 0x00005EA3
		public void KickPlayer(PlayerId id, bool banPlayer)
		{
			ICustomBattleServerSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPlayerKickRequested(id, banPlayer);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00007CB7 File Offset: 0x00005EB7
		public void HandlePlayerDisconnect(PlayerId playerId, DisconnectType disconnectType)
		{
			this._timeoutTimer.Restart();
			this._customBattlePlayers.Remove(playerId);
			base.SendMessage(new PlayerDisconnectedMessage(playerId, disconnectType));
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00007CDE File Offset: 0x00005EDE
		public void FinishAsIdle(GameLog[] gameLogs)
		{
			this.FinishGame(gameLogs);
			base.BeginDisconnect();
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00007CED File Offset: 0x00005EED
		public void FinishGame(GameLog[] gameLogs)
		{
			this.CurrentState = CustomBattleServer.State.Finished;
			if (this._handler != null)
			{
				this._handler.OnGameFinished();
			}
			IBadgeComponent badgeComponent = this._badgeComponent;
			base.SendMessage(new CustomBattleServerFinishingMessage(gameLogs, (badgeComponent != null) ? badgeComponent.DataDictionary : null, this.BattleResult));
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00007D2D File Offset: 0x00005F2D
		public void UpdateGameProperties(string gameType, string scene, string uniqueSceneId)
		{
			this.CustomGameType = gameType;
			this.CustomGameScene = scene;
			base.SendMessage(new UpdateGamePropertiesMessage(gameType, scene, uniqueSceneId));
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00007D4B File Offset: 0x00005F4B
		public void BeforeStartingNextBattle(GameLog[] gameLogs)
		{
			IBadgeComponent badgeComponent = this._badgeComponent;
			if (badgeComponent != null)
			{
				badgeComponent.OnStartingNextBattle();
			}
			if (gameLogs != null && gameLogs.Length != 0)
			{
				base.SendMessage(new AddGameLogsMessage(gameLogs));
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00007D71 File Offset: 0x00005F71
		public void BattleStarted(Dictionary<PlayerId, int> playerTeams, string cultureTeam1, string cultureTeam2)
		{
			if (this._shouldReportActivities)
			{
				base.SendMessage(new CustomBattleStartedMessage(this.CustomGameType, playerTeams, new List<string> { cultureTeam2, cultureTeam1 }));
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00007DA0 File Offset: 0x00005FA0
		public void BattleFinished(BattleResult battleResult, Dictionary<int, int> teamScores, Dictionary<PlayerId, int> playerScores)
		{
			if (this._shouldReportActivities)
			{
				base.SendMessage(new CustomBattleFinishedMessage(battleResult, teamScores, playerScores));
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00007DB8 File Offset: 0x00005FB8
		public void UpdateBattleStats(BattleResult battleResult, Dictionary<int, int> teamScores, Dictionary<PlayerId, int> playerScores)
		{
			if (this._shouldReportActivities)
			{
				this._latestQueuedBattleResult = battleResult;
				this._latestQueuedTeamScores = teamScores;
				this._latestQueuedPlayerScores = playerScores;
			}
		}

		// Token: 0x04000236 RID: 566
		private CustomBattleServer.State _state;

		// Token: 0x04000237 RID: 567
		private string _authToken;

		// Token: 0x04000238 RID: 568
		private List<ModuleInfoModel> _loadedModules;

		// Token: 0x04000239 RID: 569
		private bool _allowsOptionalModules;

		// Token: 0x0400023A RID: 570
		private bool _isSinglePlatformServer;

		// Token: 0x0400023B RID: 571
		private Stopwatch _timer;

		// Token: 0x0400023C RID: 572
		private long _previousTimeInMS;

		// Token: 0x04000241 RID: 577
		private ICustomBattleServerSessionHandler _handler;

		// Token: 0x04000242 RID: 578
		private PeerId _peerId;

		// Token: 0x04000243 RID: 579
		private List<PlayerId> _customBattlePlayers;

		// Token: 0x04000244 RID: 580
		private List<PlayerId> _requestedPlayers;

		// Token: 0x04000245 RID: 581
		private int _defaultServerTimeoutDuration = 600000;

		// Token: 0x04000246 RID: 582
		private int _timeoutDuration;

		// Token: 0x04000247 RID: 583
		private Stopwatch _timeoutTimer;

		// Token: 0x04000248 RID: 584
		private DateTime? _terminationTime;

		// Token: 0x04000249 RID: 585
		private bool _useTimeoutTimer;

		// Token: 0x0400024A RID: 586
		private IBadgeComponent _badgeComponent;

		// Token: 0x0400024B RID: 587
		private readonly List<PlayerData> _badgeComponentPlayers;

		// Token: 0x0400024C RID: 588
		private bool _shouldReportActivities;

		// Token: 0x0400024D RID: 589
		private const float BattleResultUpdatePeriod = 5f;

		// Token: 0x0400024E RID: 590
		private float _battleResultUpdateTimeElapsed;

		// Token: 0x0400024F RID: 591
		private BattleResult _latestQueuedBattleResult;

		// Token: 0x04000250 RID: 592
		private Dictionary<int, int> _latestQueuedTeamScores;

		// Token: 0x04000251 RID: 593
		private Dictionary<PlayerId, int> _latestQueuedPlayerScores;

		// Token: 0x0200018D RID: 397
		public enum State
		{
			// Token: 0x04000551 RID: 1361
			Idle,
			// Token: 0x04000552 RID: 1362
			Working,
			// Token: 0x04000553 RID: 1363
			Connected,
			// Token: 0x04000554 RID: 1364
			SessionRequested,
			// Token: 0x04000555 RID: 1365
			RegisteredServer,
			// Token: 0x04000556 RID: 1366
			RegisteredGame,
			// Token: 0x04000557 RID: 1367
			Finished
		}
	}
}
