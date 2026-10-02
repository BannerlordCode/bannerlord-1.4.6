using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000091 RID: 145
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class FriendRequestResponseMessage : Message
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00003DCE File Offset: 0x00001FCE
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00003DD6 File Offset: 0x00001FD6
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00003DDF File Offset: 0x00001FDF
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x00003DE7 File Offset: 0x00001FE7
		[JsonProperty]
		public bool DontUseNameForUnknownPlayer { get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00003DF0 File Offset: 0x00001FF0
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x00003DF8 File Offset: 0x00001FF8
		[JsonProperty]
		public bool IsAccepted { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00003E01 File Offset: 0x00002001
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x00003E09 File Offset: 0x00002009
		[JsonProperty]
		public bool IsBlocked { get; private set; }

		// Token: 0x060002B9 RID: 697 RVA: 0x00003E12 File Offset: 0x00002012
		public FriendRequestResponseMessage()
		{
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00003E1A File Offset: 0x0000201A
		public FriendRequestResponseMessage(PlayerId playerId, bool dontUseNameForUnknownPlayer, bool isAccepted, bool isBlocked)
		{
			this.PlayerId = playerId;
			this.DontUseNameForUnknownPlayer = dontUseNameForUnknownPlayer;
			this.IsAccepted = isAccepted;
			this.IsBlocked = isBlocked;
		}
	}
}
