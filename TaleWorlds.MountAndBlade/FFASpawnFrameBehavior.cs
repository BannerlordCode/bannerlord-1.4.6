using System;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002CD RID: 717
	public class FFASpawnFrameBehavior : SpawnFrameBehaviorBase
	{
		// Token: 0x06002978 RID: 10616 RVA: 0x0009BCD1 File Offset: 0x00099ED1
		public override MatrixFrame GetSpawnFrame(Team team, bool hasMount, bool isInitialSpawn)
		{
			return base.GetSpawnFrameFromSpawnPoints(this.SpawnPoints.ToList<GameEntity>(), null, hasMount);
		}
	}
}
