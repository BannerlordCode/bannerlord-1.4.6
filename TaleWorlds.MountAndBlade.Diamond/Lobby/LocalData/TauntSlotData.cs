using System;
using System.Collections.Generic;
using System.Linq;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData
{
	// Token: 0x02000174 RID: 372
	public class TauntSlotData : MultiplayerLocalData
	{
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x0001108A File Offset: 0x0000F28A
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x00011092 File Offset: 0x0000F292
		public string PlayerId { get; set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0001109B File Offset: 0x0000F29B
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x000110A3 File Offset: 0x0000F2A3
		public List<TauntIndexData> TauntIndices { get; set; }

		// Token: 0x06000A7C RID: 2684 RVA: 0x000110AC File Offset: 0x0000F2AC
		public TauntSlotData(string playerId)
		{
			this.PlayerId = playerId;
			this.TauntIndices = new List<TauntIndexData>();
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x000110C8 File Offset: 0x0000F2C8
		public override bool HasSameContentWith(MultiplayerLocalData other)
		{
			TauntSlotData tauntSlotData;
			return (tauntSlotData = other as TauntSlotData) != null && this.PlayerId == tauntSlotData.PlayerId && this.TauntIndices.SequenceEqual<TauntIndexData>(tauntSlotData.TauntIndices);
		}
	}
}
