using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200028F RID: 655
	public class MissionHardBorderPlacer : MissionLogic
	{
		// Token: 0x06002473 RID: 9331 RVA: 0x000848D0 File Offset: 0x00082AD0
		public override void EarlyStart()
		{
			base.EarlyStart();
			Scene scene = base.Mission.Scene;
			GameEntity gameEntity = GameEntity.CreateEmpty(scene, true, true, true);
			scene.FillEntityWithHardBorderPhysicsBarrier(gameEntity);
		}
	}
}
