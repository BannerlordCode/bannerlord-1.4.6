using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.MountAndBlade.Diamond.Ranked;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000015 RID: 21
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class BattleOverMessage : Message
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000026F8 File Offset: 0x000008F8
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002700 File Offset: 0x00000900
		[JsonProperty]
		public int OldExperience { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002709 File Offset: 0x00000909
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00002711 File Offset: 0x00000911
		[JsonProperty]
		public int NewExperience { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000271A File Offset: 0x0000091A
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00002722 File Offset: 0x00000922
		[JsonProperty]
		public List<string> EarnedBadges { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000272B File Offset: 0x0000092B
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00002733 File Offset: 0x00000933
		[JsonProperty]
		public int GoldGained { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000273C File Offset: 0x0000093C
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00002744 File Offset: 0x00000944
		[JsonProperty]
		public RankBarInfo OldInfo { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000274D File Offset: 0x0000094D
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00002755 File Offset: 0x00000955
		[JsonProperty]
		public RankBarInfo NewInfo { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000275E File Offset: 0x0000095E
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00002766 File Offset: 0x00000966
		[JsonProperty]
		public BattleCancelReason BattleCancelReason { get; private set; }

		// Token: 0x06000093 RID: 147 RVA: 0x0000276F File Offset: 0x0000096F
		public BattleOverMessage()
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002777 File Offset: 0x00000977
		public BattleOverMessage(int oldExperience, int newExperience, List<string> earnedBadges, int goldGained, BattleCancelReason battleCancelReason = BattleCancelReason.None)
		{
			this.OldExperience = oldExperience;
			this.NewExperience = newExperience;
			this.EarnedBadges = earnedBadges;
			this.GoldGained = goldGained;
			this.BattleCancelReason = battleCancelReason;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000027A4 File Offset: 0x000009A4
		public BattleOverMessage(BattleCancelReason battleCancelReason)
		{
			this.BattleCancelReason = battleCancelReason;
		}
	}
}
