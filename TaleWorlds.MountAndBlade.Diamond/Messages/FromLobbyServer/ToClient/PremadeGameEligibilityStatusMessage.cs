using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200005E RID: 94
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class PremadeGameEligibilityStatusMessage : Message
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00003583 File Offset: 0x00001783
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000358B File Offset: 0x0000178B
		[JsonProperty]
		public PremadeGameType[] EligibleGameTypes { get; private set; }

		// Token: 0x060001E6 RID: 486 RVA: 0x00003594 File Offset: 0x00001794
		public PremadeGameEligibilityStatusMessage()
		{
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000359C File Offset: 0x0000179C
		public PremadeGameEligibilityStatusMessage(PremadeGameType[] eligibleGameTypes)
		{
			this.EligibleGameTypes = eligibleGameTypes;
		}
	}
}
