using System;

namespace TaleWorlds.Core
{
	// Token: 0x02000092 RID: 146
	public struct ShipSlotAndPieceName
	{
		// Token: 0x060008B5 RID: 2229 RVA: 0x0001D06D File Offset: 0x0001B26D
		public ShipSlotAndPieceName(string slotName, string pieceName)
		{
			this.SlotName = slotName;
			this.PieceName = pieceName;
		}

		// Token: 0x04000463 RID: 1123
		public string SlotName;

		// Token: 0x04000464 RID: 1124
		public string PieceName;
	}
}
