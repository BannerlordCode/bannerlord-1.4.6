using System;

namespace TaleWorlds.Core.ImageIdentifiers
{
	// Token: 0x020000E5 RID: 229
	public class CraftingPieceImageIdentifier : ImageIdentifier
	{
		// Token: 0x06000B8B RID: 2955 RVA: 0x000255E5 File Offset: 0x000237E5
		public CraftingPieceImageIdentifier(CraftingPiece craftingPiece, string pieceUsageId)
		{
			base.Id = ((craftingPiece != null) ? (craftingPiece.StringId + "$" + pieceUsageId) : "");
			base.AdditionalArgs = "";
			base.TextureProviderName = "CraftingPieceImageTextureProvider";
		}
	}
}
