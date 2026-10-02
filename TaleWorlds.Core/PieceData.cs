using System;

namespace TaleWorlds.Core
{
	// Token: 0x02000049 RID: 73
	public struct PieceData
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00015974 File Offset: 0x00013B74
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x0001597C File Offset: 0x00013B7C
		public CraftingPiece.PieceTypes PieceType { get; private set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00015985 File Offset: 0x00013B85
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x0001598D File Offset: 0x00013B8D
		public int Order { get; private set; }

		// Token: 0x06000639 RID: 1593 RVA: 0x00015996 File Offset: 0x00013B96
		public PieceData(CraftingPiece.PieceTypes pieceType, int order)
		{
			this.PieceType = pieceType;
			this.Order = order;
		}
	}
}
