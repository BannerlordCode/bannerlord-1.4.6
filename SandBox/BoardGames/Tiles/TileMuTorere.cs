using System;
using SandBox.BoardGames.Objects;
using TaleWorlds.Engine;

namespace SandBox.BoardGames.Tiles
{
	// Token: 0x020000F6 RID: 246
	public class TileMuTorere : Tile1D
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0005D2A6 File Offset: 0x0005B4A6
		public int XLeftTile { get; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x0005D2AE File Offset: 0x0005B4AE
		public int XRightTile { get; }

		// Token: 0x06000C69 RID: 3177 RVA: 0x0005D2B6 File Offset: 0x0005B4B6
		public TileMuTorere(GameEntity entity, BoardGameDecal decal, int x, int xLeft, int xRight)
			: base(entity, decal, x)
		{
			this.XLeftTile = xLeft;
			this.XRightTile = xRight;
		}
	}
}
