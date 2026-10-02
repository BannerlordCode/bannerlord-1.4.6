using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200005C RID: 92
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class PlayersAddedToPartyMessage : Message
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00003498 File Offset: 0x00001698
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x000034A0 File Offset: 0x000016A0
		[TupleElementNames(new string[] { "PlayerId", "PlayerName", "IsPartyLeader" })]
		[JsonProperty]
		public List<ValueTuple<PlayerId, string, bool>> Players
		{
			[return: TupleElementNames(new string[] { "PlayerId", "PlayerName", "IsPartyLeader" })]
			get;
			[param: TupleElementNames(new string[] { "PlayerId", "PlayerName", "IsPartyLeader" })]
			private set;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000034A9 File Offset: 0x000016A9
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x000034B1 File Offset: 0x000016B1
		[TupleElementNames(new string[] { "PlayerId", "PlayerName" })]
		[JsonProperty]
		public List<ValueTuple<PlayerId, string>> InvitedPlayers
		{
			[return: TupleElementNames(new string[] { "PlayerId", "PlayerName" })]
			get;
			[param: TupleElementNames(new string[] { "PlayerId", "PlayerName" })]
			private set;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000034BA File Offset: 0x000016BA
		public PlayersAddedToPartyMessage()
		{
			this.Players = new List<ValueTuple<PlayerId, string, bool>>();
			this.InvitedPlayers = new List<ValueTuple<PlayerId, string>>();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000034D8 File Offset: 0x000016D8
		public PlayersAddedToPartyMessage(PlayerId playerId, string playerName, bool isPartyLeader)
			: this()
		{
			this.AddPlayer(playerId, playerName, isPartyLeader);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000034E9 File Offset: 0x000016E9
		public void AddPlayer(PlayerId playerId, string playerName, bool isPartyLeader)
		{
			this.Players.Add(new ValueTuple<PlayerId, string, bool>(playerId, playerName, isPartyLeader));
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000034FE File Offset: 0x000016FE
		public void AddInvitedPlayer(PlayerId playerId, string playerName)
		{
			this.InvitedPlayers.Add(new ValueTuple<PlayerId, string>(playerId, playerName));
		}
	}
}
