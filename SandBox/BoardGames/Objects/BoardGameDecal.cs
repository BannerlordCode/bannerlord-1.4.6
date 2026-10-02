using System;
using TaleWorlds.Engine;

namespace SandBox.BoardGames.Objects
{
	// Token: 0x020000FF RID: 255
	public class BoardGameDecal : ScriptComponentBehavior
	{
		// Token: 0x06000CC8 RID: 3272 RVA: 0x0005E272 File Offset: 0x0005C472
		protected override void OnInit()
		{
			base.OnInit();
			this.SetAlpha(0f);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0005E288 File Offset: 0x0005C488
		public void SetAlpha(float alpha)
		{
			base.GameEntity.SetAlpha(alpha);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0005E2A4 File Offset: 0x0005C4A4
		protected override bool MovesEntity()
		{
			return false;
		}
	}
}
