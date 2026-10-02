using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001CB RID: 459
	public static class MBGlobals
	{
		// Token: 0x06001B90 RID: 7056 RVA: 0x0006014B File Offset: 0x0005E34B
		public static void InitializeReferences()
		{
			if (!MBGlobals._initialized)
			{
				MBGlobals._actionSets = new Dictionary<string, MBActionSet>();
				MBGlobals._initialized = true;
			}
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x00060164 File Offset: 0x0005E364
		public static MBActionSet GetActionSetWithSuffix(Monster monster, bool isFemale, string suffix)
		{
			return MBGlobals.GetActionSet(ActionSetCode.GenerateActionSetNameWithSuffix(monster, isFemale, suffix));
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00060174 File Offset: 0x0005E374
		public static MBActionSet GetActionSet(string actionSetCode)
		{
			MBActionSet actionSet;
			if (!MBGlobals._actionSets.TryGetValue(actionSetCode, out actionSet))
			{
				actionSet = MBActionSet.GetActionSet(actionSetCode);
				if (!actionSet.IsValid)
				{
					Debug.FailedAssert("No action set found with action set code: " + actionSetCode, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Base\\MBGlobals.cs", "GetActionSet", 40);
					throw new Exception("Invalid action set code");
				}
				MBGlobals._actionSets[actionSetCode] = actionSet;
			}
			return actionSet;
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x000601D4 File Offset: 0x0005E3D4
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x000601EB File Offset: 0x0005E3EB
		public static string GetMethodName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MethodCallExpression)memberExpression.Body).Method.Name;
		}

		// Token: 0x04000909 RID: 2313
		public const float Gravity = 9.806f;

		// Token: 0x0400090A RID: 2314
		public static readonly Vec3 GravitationalAcceleration = new Vec3(0f, 0f, -9.806f, -1f);

		// Token: 0x0400090B RID: 2315
		private static bool _initialized;

		// Token: 0x0400090C RID: 2316
		private static Dictionary<string, MBActionSet> _actionSets;
	}
}
