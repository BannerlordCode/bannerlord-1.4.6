using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000347 RID: 839
	public interface ICastleKeyPosition
	{
		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002F4B RID: 12107
		// (set) Token: 0x06002F4C RID: 12108
		IPrimarySiegeWeapon AttackerSiegeWeapon { get; set; }

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06002F4D RID: 12109
		TacticalPosition MiddlePosition { get; }

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06002F4E RID: 12110
		TacticalPosition WaitPosition { get; }

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002F4F RID: 12111
		WorldFrame MiddleFrame { get; }

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002F50 RID: 12112
		WorldFrame DefenseWaitFrame { get; }

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002F51 RID: 12113
		FormationAI.BehaviorSide DefenseSide { get; }

		// Token: 0x06002F52 RID: 12114
		Vec3 GetPosition();
	}
}
