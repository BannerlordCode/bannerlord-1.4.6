using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Missions.Objectives
{
	// Token: 0x020003E8 RID: 1000
	internal class GenericMissionObjectiveTarget<T> : MissionObjectiveTarget<T>
	{
		// Token: 0x060036FD RID: 14077 RVA: 0x000E35AF File Offset: 0x000E17AF
		public GenericMissionObjectiveTarget(T target)
			: base(target)
		{
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x000E35B8 File Offset: 0x000E17B8
		public override bool IsActive()
		{
			return this.IsActiveCallback == null || this.IsActiveCallback(base.Target);
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x000E35D5 File Offset: 0x000E17D5
		public override TextObject GetName()
		{
			if (this.GetNameCallback != null)
			{
				return this.GetNameCallback(base.Target);
			}
			if (this.Name != null)
			{
				return this.Name;
			}
			return TextObject.GetEmpty();
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x000E360C File Offset: 0x000E180C
		public override Vec3 GetGlobalPosition()
		{
			if (this.GetGlobalPositionCallback != null)
			{
				return this.GetGlobalPositionCallback(base.Target);
			}
			if (this.StaticPosition.IsValid)
			{
				return this.StaticPosition;
			}
			Debug.FailedAssert("Static target position or position getter callback was not set for generic mission objective target.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Objectives\\MissionObjectiveTarget.cs", "GetGlobalPosition", 74);
			return Vec3.Zero;
		}

		// Token: 0x040017AA RID: 6058
		internal Func<T, bool> IsActiveCallback;

		// Token: 0x040017AB RID: 6059
		internal Func<T, TextObject> GetNameCallback;

		// Token: 0x040017AC RID: 6060
		internal Func<T, Vec3> GetGlobalPositionCallback;

		// Token: 0x040017AD RID: 6061
		internal TextObject Name;

		// Token: 0x040017AE RID: 6062
		internal Vec3 StaticPosition;
	}
}
