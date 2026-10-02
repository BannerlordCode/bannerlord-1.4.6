using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000344 RID: 836
	[Obsolete]
	public class DestructableMissionObject : MissionObject
	{
		// Token: 0x06002F42 RID: 12098 RVA: 0x000B8D21 File Offset: 0x000B6F21
		protected internal override void OnEditorInit()
		{
			Debug.FailedAssert("This scene is using old prefabs with the old destruction system, please update!", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\DestructableMissionObject.cs", "OnEditorInit", 18);
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000B8D39 File Offset: 0x000B6F39
		protected internal override void OnInit()
		{
			Debug.FailedAssert("This scene is using old prefabs with the old destruction system, please update! The game will now close!", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\DestructableMissionObject.cs", "OnInit", 23);
			Environment.Exit(0);
		}
	}
}
