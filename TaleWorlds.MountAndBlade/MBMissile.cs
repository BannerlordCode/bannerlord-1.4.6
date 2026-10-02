using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001CE RID: 462
	public abstract class MBMissile
	{
		// Token: 0x06001BB3 RID: 7091 RVA: 0x000604CA File Offset: 0x0005E6CA
		protected MBMissile(Mission mission)
		{
			this._mission = mission;
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x000604D9 File Offset: 0x0005E6D9
		// (set) Token: 0x06001BB5 RID: 7093 RVA: 0x000604E1 File Offset: 0x0005E6E1
		public int Index { get; set; }

		// Token: 0x06001BB6 RID: 7094 RVA: 0x000604EA File Offset: 0x0005E6EA
		public Vec3 GetPosition()
		{
			return MBAPI.IMBMission.GetPositionOfMissile(this._mission.Pointer, this.Index);
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x00060507 File Offset: 0x0005E707
		public Vec3 GetOldPosition()
		{
			return MBAPI.IMBMission.GetOldPositionOfMissile(this._mission.Pointer, this.Index);
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x00060524 File Offset: 0x0005E724
		public Vec3 GetVelocity()
		{
			return MBAPI.IMBMission.GetVelocityOfMissile(this._mission.Pointer, this.Index);
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x00060541 File Offset: 0x0005E741
		public void SetVelocity(in Vec3 velocity)
		{
			MBAPI.IMBMission.SetVelocityOfMissile(this._mission.Pointer, this.Index, in velocity);
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0006055F File Offset: 0x0005E75F
		public bool GetHasRigidBody()
		{
			return MBAPI.IMBMission.GetMissileHasRigidBody(this._mission.Pointer, this.Index);
		}

		// Token: 0x0400090F RID: 2319
		private readonly Mission _mission;
	}
}
