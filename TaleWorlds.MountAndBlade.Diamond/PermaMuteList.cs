using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaleWorlds.Library;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200013C RID: 316
	public static class PermaMuteList
	{
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0000C58E File Offset: 0x0000A78E
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x0000C595 File Offset: 0x0000A795
		public static bool HasMutedPlayersLoaded { get; private set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		private static PlatformFilePath PermaMuteFilePath
		{
			get
			{
				PlatformDirectoryPath platformDirectoryPath = new PlatformDirectoryPath(PlatformFileType.User, "Data");
				return new PlatformFilePath(platformDirectoryPath, "Muted.json");
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0000C5C8 File Offset: 0x0000A7C8
		[TupleElementNames(new string[] { "Id", "Name" })]
		public static IReadOnlyList<ValueTuple<string, string>> MutedPlayers
		{
			[return: TupleElementNames(new string[] { "Id", "Name" })]
			get
			{
				List<ValueTuple<string, string>> list;
				if (!PermaMuteList.HasMutedPlayersLoaded || !PermaMuteList._mutedPlayers.TryGetValue(PermaMuteList.CurrentPlayerId, out list))
				{
					return new List<ValueTuple<string, string>>();
				}
				return list;
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0000C602 File Offset: 0x0000A802
		public static void SetPermanentMuteAvailableCallback(Func<bool> getPermanentMuteAvailable)
		{
			PermaMuteList._getPermanentMuteAvailable = getPermanentMuteAvailable;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0000C60C File Offset: 0x0000A80C
		public static async Task LoadMutedPlayers(PlayerId currentPlayerId)
		{
			PermaMuteList.CurrentPlayerId = currentPlayerId.ToString();
			if (FileHelper.FileExists(PermaMuteList.PermaMuteFilePath))
			{
				try
				{
					Dictionary<string, List<ValueTuple<string, string>>> dictionary = JsonConvert.DeserializeObject<Dictionary<string, List<ValueTuple<string, string>>>>(await FileHelper.GetFileContentStringAsync(PermaMuteList.PermaMuteFilePath));
					if (dictionary != null)
					{
						PermaMuteList._mutedPlayers = dictionary;
					}
					PermaMuteList.HasMutedPlayersLoaded = true;
				}
				catch (Exception ex)
				{
					Debug.FailedAssert("Could not load muted players. " + ex.Message, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\PermaMuteList.cs", "LoadMutedPlayers", 61);
					try
					{
						FileHelper.DeleteFile(PermaMuteList.PermaMuteFilePath);
					}
					catch (Exception ex2)
					{
						Debug.FailedAssert("Could not delete muted players file. " + ex2.Message, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\PermaMuteList.cs", "LoadMutedPlayers", 68);
					}
				}
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0000C654 File Offset: 0x0000A854
		public static async void SaveMutedPlayers()
		{
			try
			{
				byte[] array = Common.SerializeObjectAsJson(PermaMuteList._mutedPlayers);
				await FileHelper.SaveFileAsync(PermaMuteList.PermaMuteFilePath, array);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert("Could not save muted players. " + ex.Message, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\PermaMuteList.cs", "SaveMutedPlayers", 83);
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0000C688 File Offset: 0x0000A888
		public static bool IsPlayerMuted(PlayerId player)
		{
			Func<bool> getPermanentMuteAvailable = PermaMuteList._getPermanentMuteAvailable;
			if ((getPermanentMuteAvailable == null || getPermanentMuteAvailable()) && PermaMuteList.CurrentPlayerId != null)
			{
				string text = player.ToString();
				Dictionary<string, List<ValueTuple<string, string>>> mutedPlayers = PermaMuteList._mutedPlayers;
				lock (mutedPlayers)
				{
					List<ValueTuple<string, string>> list;
					if (!PermaMuteList._mutedPlayers.TryGetValue(PermaMuteList.CurrentPlayerId, out list))
					{
						return false;
					}
					using (List<ValueTuple<string, string>>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.Item1 == text)
							{
								return true;
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0000C750 File Offset: 0x0000A950
		public static void MutePlayer(PlayerId player, string name)
		{
			Func<bool> getPermanentMuteAvailable = PermaMuteList._getPermanentMuteAvailable;
			if (getPermanentMuteAvailable == null || getPermanentMuteAvailable())
			{
				Dictionary<string, List<ValueTuple<string, string>>> mutedPlayers = PermaMuteList._mutedPlayers;
				lock (mutedPlayers)
				{
					List<ValueTuple<string, string>> list;
					if (!PermaMuteList._mutedPlayers.TryGetValue(PermaMuteList.CurrentPlayerId, out list))
					{
						list = new List<ValueTuple<string, string>>();
						PermaMuteList._mutedPlayers.Add(PermaMuteList.CurrentPlayerId, list);
					}
					list.Add(new ValueTuple<string, string>(player.ToString(), name));
				}
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		public static void RemoveMutedPlayer(PlayerId player)
		{
			Func<bool> getPermanentMuteAvailable = PermaMuteList._getPermanentMuteAvailable;
			if (getPermanentMuteAvailable == null || getPermanentMuteAvailable())
			{
				string text = player.ToString();
				Dictionary<string, List<ValueTuple<string, string>>> mutedPlayers = PermaMuteList._mutedPlayers;
				lock (mutedPlayers)
				{
					List<ValueTuple<string, string>> list;
					if (PermaMuteList._mutedPlayers.TryGetValue(PermaMuteList.CurrentPlayerId, out list))
					{
						int num = -1;
						for (int i = 0; i < list.Count; i++)
						{
							if (list[i].Item1 == text)
							{
								num = i;
								break;
							}
						}
						if (num >= 0)
						{
							list.RemoveAt(num);
						}
					}
				}
			}
		}

		// Token: 0x04000392 RID: 914
		[TupleElementNames(new string[] { "Id", "Name" })]
		private static Dictionary<string, List<ValueTuple<string, string>>> _mutedPlayers = new Dictionary<string, List<ValueTuple<string, string>>>();

		// Token: 0x04000393 RID: 915
		private static string CurrentPlayerId;

		// Token: 0x04000394 RID: 916
		private static Func<bool> _getPermanentMuteAvailable;
	}
}
