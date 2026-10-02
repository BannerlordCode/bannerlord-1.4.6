using System;
using SandBox.BoardGames.Objects;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.BoardGames.Tiles
{
	// Token: 0x020000F7 RID: 247
	public class TilePuluc : Tile1D
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0005D2D1 File Offset: 0x0005B4D1
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x0005D2D9 File Offset: 0x0005B4D9
		public Vec3 PosLeft { get; private set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x0005D2E2 File Offset: 0x0005B4E2
		// (set) Token: 0x06000C6D RID: 3181 RVA: 0x0005D2EA File Offset: 0x0005B4EA
		public Vec3 PosLeftMid { get; private set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0005D2F3 File Offset: 0x0005B4F3
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x0005D2FB File Offset: 0x0005B4FB
		public Vec3 PosRight { get; private set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0005D304 File Offset: 0x0005B504
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x0005D30C File Offset: 0x0005B50C
		public Vec3 PosRightMid { get; private set; }

		// Token: 0x06000C72 RID: 3186 RVA: 0x0005D315 File Offset: 0x0005B515
		public TilePuluc(GameEntity entity, BoardGameDecal decal, int x)
			: base(entity, decal, x)
		{
			this.UpdateTilePosition();
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0005D328 File Offset: 0x0005B528
		public void UpdateTilePosition()
		{
			MatrixFrame globalFrame = base.Entity.GetGlobalFrame();
			MetaMesh tileMesh = base.Entity.GetFirstScriptOfType<Tile>().TileMesh;
			Vec3 vec = tileMesh.GetBoundingBox().max - tileMesh.GetBoundingBox().min;
			Mat3 mat = globalFrame.rotation.TransformToParent(in tileMesh.Frame.rotation);
			Vec3 vec2 = new Vec3(0f, vec.y / 6f, 0f, -1f);
			Vec3 vec3 = mat.TransformToParent(in vec2);
			vec2 = new Vec3(0f, vec.y / 3f, 0f, -1f);
			Vec3 vec4 = mat.TransformToParent(in vec2);
			Vec3 globalPosition = base.Entity.GlobalPosition;
			this.PosLeft = globalPosition + vec4;
			this.PosLeftMid = globalPosition + vec3;
			this.PosRight = globalPosition - vec4;
			this.PosRightMid = globalPosition - vec3;
		}
	}
}
