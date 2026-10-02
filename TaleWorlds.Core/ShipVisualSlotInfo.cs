using System;

namespace TaleWorlds.Core
{
	// Token: 0x02000091 RID: 145
	public struct ShipVisualSlotInfo
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x0001D05D File Offset: 0x0001B25D
		public ShipVisualSlotInfo(string visualSlotId, string visualPieceId)
		{
			this.VisualSlotTag = visualSlotId;
			this.VisualPieceId = visualPieceId;
		}

		// Token: 0x04000461 RID: 1121
		public string VisualSlotTag;

		// Token: 0x04000462 RID: 1122
		public string VisualPieceId;
	}
}
