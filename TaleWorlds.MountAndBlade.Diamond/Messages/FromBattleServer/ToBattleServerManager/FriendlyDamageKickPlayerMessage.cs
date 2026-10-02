using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000D5 RID: 213
	[MessageDescription("BattleServer", "BattleServerManager", false)]
	[Serializable]
	public class FriendlyDamageKickPlayerMessage : Message
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00004B2E File Offset: 0x00002D2E
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x00004B36 File Offset: 0x00002D36
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00004B3F File Offset: 0x00002D3F
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x00004B47 File Offset: 0x00002D47
		[TupleElementNames(new string[] { "killCount", "damage" })]
		[JsonProperty]
		public Dictionary<int, ValueTuple<int, float>> RoundDamageMap
		{
			[return: TupleElementNames(new string[] { "killCount", "damage" })]
			get;
			[param: TupleElementNames(new string[] { "killCount", "damage" })]
			private set;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00004B50 File Offset: 0x00002D50
		public FriendlyDamageKickPlayerMessage()
		{
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00004B58 File Offset: 0x00002D58
		public FriendlyDamageKickPlayerMessage(PlayerId playerId, [TupleElementNames(new string[] { "killCount", "damage" })] Dictionary<int, ValueTuple<int, float>> roundDamageMap)
		{
			this.PlayerId = playerId;
			this.RoundDamageMap = roundDamageMap;
		}
	}
}
