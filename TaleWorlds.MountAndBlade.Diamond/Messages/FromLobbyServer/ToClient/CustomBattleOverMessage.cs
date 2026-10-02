using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000029 RID: 41
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class CustomBattleOverMessage : Message
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00002A94 File Offset: 0x00000C94
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00002A9C File Offset: 0x00000C9C
		[JsonProperty]
		public int OldExperience { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00002AA5 File Offset: 0x00000CA5
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00002AAD File Offset: 0x00000CAD
		[JsonProperty]
		public int NewExperience { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00002AB6 File Offset: 0x00000CB6
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00002ABE File Offset: 0x00000CBE
		[JsonProperty]
		public int GoldGain { get; set; }

		// Token: 0x060000E4 RID: 228 RVA: 0x00002AC7 File Offset: 0x00000CC7
		public CustomBattleOverMessage()
		{
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00002ACF File Offset: 0x00000CCF
		public CustomBattleOverMessage(int oldExperience, int newExperience, int goldGain)
		{
			this.OldExperience = oldExperience;
			this.NewExperience = newExperience;
			this.GoldGain = goldGain;
		}
	}
}
