using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.Library;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000D2 RID: 210
	[MessageDescription("BattleServer", "BattleServerManager", true)]
	[Serializable]
	public class BattleServerReadyMessage : LoginMessage
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003CC RID: 972 RVA: 0x000049D9 File Offset: 0x00002BD9
		// (set) Token: 0x060003CD RID: 973 RVA: 0x000049E1 File Offset: 0x00002BE1
		[JsonProperty]
		public ApplicationVersion ApplicationVersion { get; private set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000049EA File Offset: 0x00002BEA
		// (set) Token: 0x060003CF RID: 975 RVA: 0x000049F2 File Offset: 0x00002BF2
		[JsonProperty]
		public string AssignedAddress { get; private set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x000049FB File Offset: 0x00002BFB
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x00004A03 File Offset: 0x00002C03
		[JsonProperty]
		public ushort AssignedPort { get; private set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00004A0C File Offset: 0x00002C0C
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00004A14 File Offset: 0x00002C14
		[JsonProperty]
		public string Region { get; private set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00004A1D File Offset: 0x00002C1D
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x00004A25 File Offset: 0x00002C25
		[JsonProperty]
		public sbyte Priority { get; private set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00004A2E File Offset: 0x00002C2E
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x00004A36 File Offset: 0x00002C36
		[JsonProperty]
		public string Password { get; private set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00004A3F File Offset: 0x00002C3F
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x00004A47 File Offset: 0x00002C47
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x060003DA RID: 986 RVA: 0x00004A50 File Offset: 0x00002C50
		public BattleServerReadyMessage()
		{
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00004A58 File Offset: 0x00002C58
		public BattleServerReadyMessage(PeerId peerId, ApplicationVersion applicationVersion, string assignedAddress, ushort assignedPort, string region, sbyte priority, string password, string gameType)
			: base(peerId, null)
		{
			this.ApplicationVersion = applicationVersion;
			this.AssignedAddress = assignedAddress;
			this.AssignedPort = assignedPort;
			this.Region = region;
			this.Priority = priority;
			this.Password = password;
			this.GameType = gameType;
		}
	}
}
