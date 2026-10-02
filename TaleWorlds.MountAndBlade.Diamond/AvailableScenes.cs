using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000EA RID: 234
	[Serializable]
	public class AvailableScenes
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000051C0 File Offset: 0x000033C0
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x000051C7 File Offset: 0x000033C7
		public static AvailableScenes Empty { get; private set; } = new AvailableScenes(new Dictionary<string, string[]>());

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x000051E0 File Offset: 0x000033E0
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x000051E8 File Offset: 0x000033E8
		public Dictionary<string, string[]> ScenesByGameTypes { get; set; }

		// Token: 0x0600047A RID: 1146 RVA: 0x000051F1 File Offset: 0x000033F1
		public AvailableScenes()
		{
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000051F9 File Offset: 0x000033F9
		public AvailableScenes(Dictionary<string, string[]> scenesByGameTypes)
		{
			this.ScenesByGameTypes = scenesByGameTypes;
		}
	}
}
