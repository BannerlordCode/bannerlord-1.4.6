using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServerManager.ToBattleServer
{
	// Token: 0x020000DF RID: 223
	[MessageDescription("BattleServerManager", "BattleServer", true)]
	[Serializable]
	public class FriendlyDamageKickPlayerResponseMessage : Message
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00004CC7 File Offset: 0x00002EC7
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x00004CCF File Offset: 0x00002ECF
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x06000412 RID: 1042 RVA: 0x00004CD8 File Offset: 0x00002ED8
		public FriendlyDamageKickPlayerResponseMessage()
		{
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00004CE0 File Offset: 0x00002EE0
		public FriendlyDamageKickPlayerResponseMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
