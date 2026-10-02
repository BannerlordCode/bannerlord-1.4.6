using System;
using TaleWorlds.Engine;

namespace SandBox.BoardGames.Objects
{
	// Token: 0x02000100 RID: 256
	public class Tile : ScriptComponentBehavior
	{
		// Token: 0x06000CCC RID: 3276 RVA: 0x0005E2B0 File Offset: 0x0005C4B0
		protected override void OnInit()
		{
			base.OnInit();
			base.GameEntity.RemoveMultiMesh(base.GameEntity.GetMetaMesh(0));
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0005E2E4 File Offset: 0x0005C4E4
		public void SetVisibility(bool visible)
		{
			base.GameEntity.SetVisibilityExcludeParents(visible);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0005E300 File Offset: 0x0005C500
		protected override bool MovesEntity()
		{
			return false;
		}

		// Token: 0x04000594 RID: 1428
		public MetaMesh TileMesh;
	}
}
