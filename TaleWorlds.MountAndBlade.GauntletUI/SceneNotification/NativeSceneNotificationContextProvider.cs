using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.GauntletUI.SceneNotification
{
	// Token: 0x0200002B RID: 43
	public class NativeSceneNotificationContextProvider : ISceneNotificationContextProvider
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x0000A61E File Offset: 0x0000881E
		public bool IsContextAllowed(SceneNotificationData.RelevantContextType relevantType)
		{
			return relevantType != SceneNotificationData.RelevantContextType.Mission || GameStateManager.Current.ActiveState is MissionState;
		}
	}
}
