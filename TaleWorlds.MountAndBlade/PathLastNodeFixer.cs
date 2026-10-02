using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200034D RID: 845
	public class PathLastNodeFixer : UsableMissionObjectComponent
	{
		// Token: 0x06002FA5 RID: 12197 RVA: 0x000BBB7C File Offset: 0x000B9D7C
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			Path pathWithName = this._scene.GetPathWithName(this.PathHolder.PathEntity);
			this.Update(pathWithName);
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000BBBAE File Offset: 0x000B9DAE
		protected internal override void OnAdded(Scene scene)
		{
			base.OnAdded(scene);
			this._scene = scene;
			this.Update();
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000BBBC4 File Offset: 0x000B9DC4
		public void Update()
		{
			Path pathWithName = this._scene.GetPathWithName(this.PathHolder.PathEntity);
			this.Update(pathWithName);
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000BBBEF File Offset: 0x000B9DEF
		private void Update(Path path)
		{
			path != null;
		}

		// Token: 0x0400137E RID: 4990
		public IPathHolder PathHolder;

		// Token: 0x0400137F RID: 4991
		private Scene _scene;
	}
}
