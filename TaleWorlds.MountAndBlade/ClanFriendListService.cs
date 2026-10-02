using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlatformService;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001EA RID: 490
	public class ClanFriendListService : IFriendListService
	{
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x00061A17 File Offset: 0x0005FC17
		bool IFriendListService.InGameStatusFetchable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x00061A1A File Offset: 0x0005FC1A
		bool IFriendListService.AllowsFriendOperations
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x00061A1D File Offset: 0x0005FC1D
		bool IFriendListService.CanInvitePlayersToPlatformSession
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x00061A20 File Offset: 0x0005FC20
		bool IFriendListService.IncludeInAllFriends
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x00061A23 File Offset: 0x0005FC23
		public ClanFriendListService()
		{
			this._clanPlayerInfos = new Dictionary<PlayerId, ClanPlayerInfo>();
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x00061A36 File Offset: 0x0005FC36
		string IFriendListService.GetServiceCodeName()
		{
			return "ClanFriends";
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x00061A3D File Offset: 0x0005FC3D
		TextObject IFriendListService.GetServiceLocalizedName()
		{
			return new TextObject("{=j4F7tTzy}Clan", null);
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x00061A4A File Offset: 0x0005FC4A
		FriendListServiceType IFriendListService.GetFriendListServiceType()
		{
			return FriendListServiceType.Clan;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x00061A4D File Offset: 0x0005FC4D
		IEnumerable<PlayerId> IFriendListService.GetAllFriends()
		{
			return this._clanPlayerInfos.Keys;
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06001C8A RID: 7306 RVA: 0x00061A5C File Offset: 0x0005FC5C
		// (remove) Token: 0x06001C8B RID: 7307 RVA: 0x00061A94 File Offset: 0x0005FC94
		public event Action<PlayerId> OnUserStatusChanged;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001C8C RID: 7308 RVA: 0x00061ACC File Offset: 0x0005FCCC
		// (remove) Token: 0x06001C8D RID: 7309 RVA: 0x00061B04 File Offset: 0x0005FD04
		public event Action<PlayerId> OnFriendRemoved;

		// Token: 0x06001C8E RID: 7310 RVA: 0x00061B3C File Offset: 0x0005FD3C
		async Task<bool> IFriendListService.GetUserOnlineStatus(PlayerId providedId)
		{
			bool flag = false;
			ClanPlayerInfo clanPlayerInfo;
			this._clanPlayerInfos.TryGetValue(providedId, out clanPlayerInfo);
			if (clanPlayerInfo != null)
			{
				flag = clanPlayerInfo.State == AnotherPlayerState.InMultiplayerGame || clanPlayerInfo.State == AnotherPlayerState.AtLobby || clanPlayerInfo.State == AnotherPlayerState.InParty;
			}
			return await Task.FromResult<bool>(flag);
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x00061B8C File Offset: 0x0005FD8C
		async Task<bool> IFriendListService.IsPlayingThisGame(PlayerId providedId)
		{
			return await ((IFriendListService)this).GetUserOnlineStatus(providedId);
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00061BDC File Offset: 0x0005FDDC
		async Task<string> IFriendListService.GetUserName(PlayerId providedId)
		{
			ClanPlayerInfo clanPlayerInfo;
			this._clanPlayerInfos.TryGetValue(providedId, out clanPlayerInfo);
			return await Task.FromResult<string>((clanPlayerInfo != null) ? clanPlayerInfo.PlayerName : null);
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00061C2C File Offset: 0x0005FE2C
		public async Task<PlayerId> GetUserWithName(string name)
		{
			ClanPlayerInfo clanPlayerInfo = this._clanPlayerInfos.Values.FirstOrDefaultQ<ClanPlayerInfo>((ClanPlayerInfo playerInfo) => playerInfo.PlayerName == name);
			return await Task.FromResult<PlayerId>((clanPlayerInfo != null) ? clanPlayerInfo.PlayerId : PlayerId.Empty);
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06001C92 RID: 7314 RVA: 0x00061C7C File Offset: 0x0005FE7C
		// (remove) Token: 0x06001C93 RID: 7315 RVA: 0x00061CB4 File Offset: 0x0005FEB4
		public event Action OnFriendListChanged;

		// Token: 0x06001C94 RID: 7316 RVA: 0x00061CE9 File Offset: 0x0005FEE9
		public IEnumerable<PlayerId> GetPendingRequests()
		{
			return null;
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x00061CEC File Offset: 0x0005FEEC
		public IEnumerable<PlayerId> GetReceivedRequests()
		{
			return null;
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x00061CF0 File Offset: 0x0005FEF0
		private void Dummy()
		{
			if (this.OnUserStatusChanged != null)
			{
				this.OnUserStatusChanged(default(PlayerId));
			}
			if (this.OnFriendRemoved != null)
			{
				this.OnFriendRemoved(default(PlayerId));
			}
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x00061D38 File Offset: 0x0005FF38
		public void OnClanInfoChanged(List<ClanPlayerInfo> playerInfosInClan)
		{
			this._clanPlayerInfos.Clear();
			if (playerInfosInClan != null)
			{
				foreach (ClanPlayerInfo clanPlayerInfo in playerInfosInClan)
				{
					this._clanPlayerInfos.Add(clanPlayerInfo.PlayerId, clanPlayerInfo);
				}
			}
			Action onFriendListChanged = this.OnFriendListChanged;
			if (onFriendListChanged == null)
			{
				return;
			}
			onFriendListChanged();
		}

		// Token: 0x040009BC RID: 2492
		public const string CodeName = "ClanFriends";

		// Token: 0x040009BD RID: 2493
		private readonly Dictionary<PlayerId, ClanPlayerInfo> _clanPlayerInfos;
	}
}
