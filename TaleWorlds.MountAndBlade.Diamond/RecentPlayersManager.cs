using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaleWorlds.Library;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000152 RID: 338
	public static class RecentPlayersManager
	{
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x0000DC2C File Offset: 0x0000BE2C
		private static PlatformFilePath RecentPlayerFilePath
		{
			get
			{
				PlatformDirectoryPath platformDirectoryPath = new PlatformDirectoryPath(PlatformFileType.User, "Data");
				return new PlatformFilePath(platformDirectoryPath, "RecentPlayers.json");
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0000DC51 File Offset: 0x0000BE51
		public static MBReadOnlyList<RecentPlayerInfo> RecentPlayers
		{
			get
			{
				return RecentPlayersManager._recentPlayers;
			}
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0000DCC4 File Offset: 0x0000BEC4
		public static async void Initialize()
		{
			await RecentPlayersManager.LoadRecentPlayers();
			RecentPlayersManager.DecayPlayers();
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		private static async Task LoadRecentPlayers()
		{
			if (RecentPlayersManager.IsRecentPlayersCacheDirty)
			{
				if (Common.PlatformFileHelper.FileExists(RecentPlayersManager.RecentPlayerFilePath))
				{
					try
					{
						TaskAwaiter<string> taskAwaiter = FileHelper.GetFileContentStringAsync(RecentPlayersManager.RecentPlayerFilePath).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							await taskAwaiter;
							TaskAwaiter<string> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<string>);
						}
						RecentPlayersManager._recentPlayers = JsonConvert.DeserializeObject<MBList<RecentPlayerInfo>>(taskAwaiter.GetResult());
						if (RecentPlayersManager._recentPlayers == null)
						{
							RecentPlayersManager._recentPlayers = new MBList<RecentPlayerInfo>();
							throw new Exception("_recentPlayers were null.");
						}
					}
					catch (Exception ex)
					{
						Debug.FailedAssert("Could not recent players. " + ex.Message, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\RecentPlayersManager.cs", "LoadRecentPlayers", 83);
						try
						{
							FileHelper.DeleteFile(RecentPlayersManager.RecentPlayerFilePath);
						}
						catch (Exception ex2)
						{
							Debug.FailedAssert("Could not delete recent players file. " + ex2.Message, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\RecentPlayersManager.cs", "LoadRecentPlayers", 90);
						}
					}
				}
				RecentPlayersManager.IsRecentPlayersCacheDirty = false;
			}
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0000DD38 File Offset: 0x0000BF38
		public static async Task<MBReadOnlyList<RecentPlayerInfo>> GetRecentPlayerInfos()
		{
			await RecentPlayersManager.LoadRecentPlayers();
			return RecentPlayersManager.RecentPlayers;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0000DD75 File Offset: 0x0000BF75
		public static PlayerId[] GetRecentPlayerIds()
		{
			return RecentPlayersManager._recentPlayers.Select<RecentPlayerInfo, PlayerId>((RecentPlayerInfo p) => PlayerId.FromString(p.PlayerId)).ToArray<PlayerId>();
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0000DDA8 File Offset: 0x0000BFA8
		public static void AddOrUpdatePlayerEntry(PlayerId playerId, string playerName, InteractionType interactionType, int forcedIndex)
		{
			if (forcedIndex == -1)
			{
				object lockObject = RecentPlayersManager._lockObject;
				lock (lockObject)
				{
					RecentPlayersManager.InteractionTypeInfo interactionTypeInfo = RecentPlayersManager.InteractionTypeScoreDictionary[interactionType];
					RecentPlayerInfo recentPlayerInfo = RecentPlayersManager.TryGetPlayer(playerId);
					if (recentPlayerInfo != null)
					{
						if (interactionTypeInfo.ProcessType == RecentPlayersManager.InteractionTypeInfo.InteractionProcessType.Cumulative)
						{
							recentPlayerInfo.ImportanceScore += interactionTypeInfo.Score;
						}
						else if (interactionTypeInfo.ProcessType == RecentPlayersManager.InteractionTypeInfo.InteractionProcessType.Fixed)
						{
							recentPlayerInfo.ImportanceScore += Math.Max(interactionTypeInfo.Score, recentPlayerInfo.ImportanceScore);
						}
						recentPlayerInfo.PlayerName = playerName;
						recentPlayerInfo.InteractionTime = DateTime.Now;
					}
					else
					{
						recentPlayerInfo = new RecentPlayerInfo();
						recentPlayerInfo.PlayerId = playerId.ToString();
						recentPlayerInfo.ImportanceScore = interactionTypeInfo.Score;
						recentPlayerInfo.InteractionTime = DateTime.Now;
						recentPlayerInfo.PlayerName = playerName;
						RecentPlayersManager._recentPlayers.Add(recentPlayerInfo);
					}
					Action<PlayerId, InteractionType> onRecentPlayerInteraction = RecentPlayersManager.OnRecentPlayerInteraction;
					if (onRecentPlayerInteraction != null)
					{
						onRecentPlayerInteraction(playerId, interactionType);
					}
				}
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000980 RID: 2432 RVA: 0x0000DEAC File Offset: 0x0000C0AC
		// (remove) Token: 0x06000981 RID: 2433 RVA: 0x0000DEE0 File Offset: 0x0000C0E0
		public static event Action<PlayerId, InteractionType> OnRecentPlayerInteraction;

		// Token: 0x06000982 RID: 2434 RVA: 0x0000DF14 File Offset: 0x0000C114
		private static void DecayPlayers()
		{
			object lockObject = RecentPlayersManager._lockObject;
			lock (lockObject)
			{
				List<RecentPlayerInfo> list = new List<RecentPlayerInfo>();
				DateTime now = DateTime.Now;
				foreach (RecentPlayerInfo recentPlayerInfo in RecentPlayersManager._recentPlayers)
				{
					recentPlayerInfo.ImportanceScore -= (int)(now - recentPlayerInfo.InteractionTime).TotalHours;
					if (recentPlayerInfo.ImportanceScore <= 0)
					{
						list.Add(recentPlayerInfo);
					}
				}
				foreach (RecentPlayerInfo recentPlayerInfo2 in list)
				{
					RecentPlayersManager._recentPlayers.Remove(recentPlayerInfo2);
				}
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0000E010 File Offset: 0x0000C210
		public static void TrimPlayers()
		{
			if (RecentPlayersManager._recentPlayers.Count > 200)
			{
				object lockObject = RecentPlayersManager._lockObject;
				lock (lockObject)
				{
					List<RecentPlayerInfo> list = RecentPlayersManager._recentPlayers.OrderByDescending<RecentPlayerInfo, int>((RecentPlayerInfo p) => p.ImportanceScore).Take<RecentPlayerInfo>(160).ToList<RecentPlayerInfo>();
					RecentPlayersManager._recentPlayers.Clear();
					RecentPlayersManager._recentPlayers.AddRange(list);
				}
			}
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0000E0A8 File Offset: 0x0000C2A8
		public static void Serialize()
		{
			try
			{
				byte[] array = Common.SerializeObjectAsJson(RecentPlayersManager._recentPlayers);
				FileHelper.SaveFile(RecentPlayersManager.RecentPlayerFilePath, array);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0000E0E8 File Offset: 0x0000C2E8
		public static IEnumerable<PlayerId> GetPlayersOrdered()
		{
			return from p in RecentPlayersManager._recentPlayers
				orderby p.InteractionTime descending
				select PlayerId.FromString(p.PlayerId);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0000E144 File Offset: 0x0000C344
		private static RecentPlayerInfo TryGetPlayer(PlayerId playerId)
		{
			string text = playerId.ToString();
			foreach (RecentPlayerInfo recentPlayerInfo in RecentPlayersManager._recentPlayers)
			{
				if (recentPlayerInfo.PlayerId == text)
				{
					return recentPlayerInfo;
				}
			}
			return null;
		}

		// Token: 0x040003F5 RID: 1013
		private const string RecentPlayersDirectoryName = "Data";

		// Token: 0x040003F6 RID: 1014
		private const string RecentPlayersFileName = "RecentPlayers.json";

		// Token: 0x040003F7 RID: 1015
		private const int MaxRecentPlayersSize = 200;

		// Token: 0x040003F8 RID: 1016
		private const int DownsizedRecentPlayersSize = 160;

		// Token: 0x040003F9 RID: 1017
		private static bool IsRecentPlayersCacheDirty = true;

		// Token: 0x040003FA RID: 1018
		private static readonly object _lockObject = new object();

		// Token: 0x040003FB RID: 1019
		private static MBList<RecentPlayerInfo> _recentPlayers = new MBList<RecentPlayerInfo>();

		// Token: 0x040003FC RID: 1020
		private static readonly Dictionary<InteractionType, RecentPlayersManager.InteractionTypeInfo> InteractionTypeScoreDictionary = new Dictionary<InteractionType, RecentPlayersManager.InteractionTypeInfo>
		{
			{
				InteractionType.Killed,
				new RecentPlayersManager.InteractionTypeInfo(5, RecentPlayersManager.InteractionTypeInfo.InteractionProcessType.Cumulative)
			},
			{
				InteractionType.KilledBy,
				new RecentPlayersManager.InteractionTypeInfo(5, RecentPlayersManager.InteractionTypeInfo.InteractionProcessType.Cumulative)
			},
			{
				InteractionType.InGameTogether,
				new RecentPlayersManager.InteractionTypeInfo(24, RecentPlayersManager.InteractionTypeInfo.InteractionProcessType.Fixed)
			},
			{
				InteractionType.InPartyTogether,
				new RecentPlayersManager.InteractionTypeInfo(48, RecentPlayersManager.InteractionTypeInfo.InteractionProcessType.Fixed)
			}
		};

		// Token: 0x020001CB RID: 459
		private class InteractionTypeInfo
		{
			// Token: 0x1700035E RID: 862
			// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0001626E File Offset: 0x0001446E
			// (set) Token: 0x06000B41 RID: 2881 RVA: 0x00016276 File Offset: 0x00014476
			public int Score { get; private set; }

			// Token: 0x1700035F RID: 863
			// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0001627F File Offset: 0x0001447F
			// (set) Token: 0x06000B43 RID: 2883 RVA: 0x00016287 File Offset: 0x00014487
			public RecentPlayersManager.InteractionTypeInfo.InteractionProcessType ProcessType { get; private set; }

			// Token: 0x06000B44 RID: 2884 RVA: 0x00016290 File Offset: 0x00014490
			public InteractionTypeInfo(int score, RecentPlayersManager.InteractionTypeInfo.InteractionProcessType type)
			{
				this.Score = score;
				this.ProcessType = type;
			}

			// Token: 0x020001DA RID: 474
			public enum InteractionProcessType
			{
				// Token: 0x040006E0 RID: 1760
				Cumulative,
				// Token: 0x040006E1 RID: 1761
				Fixed
			}
		}
	}
}
