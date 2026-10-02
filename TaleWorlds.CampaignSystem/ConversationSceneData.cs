using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x0200008B RID: 139
	public struct ConversationSceneData
	{
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x000535A2 File Offset: 0x000517A2
		// (set) Token: 0x0600122F RID: 4655 RVA: 0x000535AA File Offset: 0x000517AA
		public string SceneID { get; private set; }

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x000535B3 File Offset: 0x000517B3
		// (set) Token: 0x06001231 RID: 4657 RVA: 0x000535BB File Offset: 0x000517BB
		public TerrainType Terrain { get; private set; }

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x000535C4 File Offset: 0x000517C4
		// (set) Token: 0x06001233 RID: 4659 RVA: 0x000535CC File Offset: 0x000517CC
		public List<TerrainType> TerrainTypes { get; private set; }

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x000535D5 File Offset: 0x000517D5
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x000535DD File Offset: 0x000517DD
		public ForestDensity ForestDensity { get; private set; }

		// Token: 0x06001236 RID: 4662 RVA: 0x000535E6 File Offset: 0x000517E6
		public ConversationSceneData(string sceneID, TerrainType terrain, List<TerrainType> terrainTypes, ForestDensity forestDensity)
		{
			this.SceneID = sceneID;
			this.Terrain = terrain;
			this.TerrainTypes = terrainTypes;
			this.ForestDensity = forestDensity;
		}
	}
}
