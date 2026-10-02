using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.BattleServerManager.BattleServerManager
{
	// Token: 0x020000E6 RID: 230
	[MessageDescription("BattleServerManager", "BattleServerManager", true)]
	[Serializable]
	public class BattleEndedProcessResultsMessage : Message
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00005020 File Offset: 0x00003220
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x00005028 File Offset: 0x00003228
		[JsonProperty]
		public BattleResult BattleResult { get; private set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00005031 File Offset: 0x00003231
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00005039 File Offset: 0x00003239
		[JsonProperty]
		public List<BadgeDataEntry> BadgeDateEntries { get; private set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00005042 File Offset: 0x00003242
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0000504A File Offset: 0x0000324A
		[JsonProperty]
		public string BattleGameType { get; private set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00005053 File Offset: 0x00003253
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0000505B File Offset: 0x0000325B
		[JsonProperty]
		public string Region { get; private set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00005064 File Offset: 0x00003264
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0000506C File Offset: 0x0000326C
		[TupleElementNames(new string[] { "playerBattleInfo", "shouldSendMessage", "hasLeftGame" })]
		[JsonProperty]
		public List<ValueTuple<PlayerBattleInfo, bool, bool>> PlayersForResults
		{
			[return: TupleElementNames(new string[] { "playerBattleInfo", "shouldSendMessage", "hasLeftGame" })]
			get;
			[param: TupleElementNames(new string[] { "playerBattleInfo", "shouldSendMessage", "hasLeftGame" })]
			private set;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00005075 File Offset: 0x00003275
		public BattleEndedProcessResultsMessage()
		{
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000507D File Offset: 0x0000327D
		public BattleEndedProcessResultsMessage(BattleResult battleResult, List<BadgeDataEntry> badgeDateEntries, string battleGameType, string region, [TupleElementNames(new string[] { "playerBattleInfo", "shouldSendMessage", "hasLeftGame" })] List<ValueTuple<PlayerBattleInfo, bool, bool>> playersForResults)
		{
			this.BattleResult = battleResult;
			this.BadgeDateEntries = badgeDateEntries;
			this.BattleGameType = battleGameType;
			this.Region = region;
			this.PlayersForResults = playersForResults;
		}
	}
}
