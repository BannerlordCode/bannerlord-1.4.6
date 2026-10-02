using System;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace SandBox.View.Map
{
	// Token: 0x02000047 RID: 71
	public class MapConversationTableauData
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000170B4 File Offset: 0x000152B4
		// (set) Token: 0x06000261 RID: 609 RVA: 0x000170BC File Offset: 0x000152BC
		public ConversationCharacterData PlayerCharacterData { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000170C5 File Offset: 0x000152C5
		// (set) Token: 0x06000263 RID: 611 RVA: 0x000170CD File Offset: 0x000152CD
		public ConversationCharacterData ConversationPartnerData { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000170D6 File Offset: 0x000152D6
		// (set) Token: 0x06000265 RID: 613 RVA: 0x000170DE File Offset: 0x000152DE
		public TerrainType ConversationTerrainType { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000266 RID: 614 RVA: 0x000170E7 File Offset: 0x000152E7
		// (set) Token: 0x06000267 RID: 615 RVA: 0x000170EF File Offset: 0x000152EF
		public float TimeOfDay { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000268 RID: 616 RVA: 0x000170F8 File Offset: 0x000152F8
		// (set) Token: 0x06000269 RID: 617 RVA: 0x00017100 File Offset: 0x00015300
		public bool IsCurrentTerrainUnderSnow { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00017109 File Offset: 0x00015309
		// (set) Token: 0x0600026B RID: 619 RVA: 0x00017111 File Offset: 0x00015311
		public Settlement Settlement { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0001711A File Offset: 0x0001531A
		// (set) Token: 0x0600026D RID: 621 RVA: 0x00017122 File Offset: 0x00015322
		public string LocationId { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0001712B File Offset: 0x0001532B
		// (set) Token: 0x0600026F RID: 623 RVA: 0x00017133 File Offset: 0x00015333
		public bool IsSnowing { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0001713C File Offset: 0x0001533C
		// (set) Token: 0x06000271 RID: 625 RVA: 0x00017144 File Offset: 0x00015344
		public bool IsRaining { get; private set; }

		// Token: 0x06000272 RID: 626 RVA: 0x0001714D File Offset: 0x0001534D
		private MapConversationTableauData()
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00017158 File Offset: 0x00015358
		public static MapConversationTableauData CreateFrom(ConversationCharacterData playerCharacterData, ConversationCharacterData conversationPartnerData, TerrainType terrainType, float timeOfDay, bool isCurrentTerrainUnderSnow, Settlement settlement, string locationId, bool isRaining, bool isSnowing)
		{
			return new MapConversationTableauData
			{
				PlayerCharacterData = playerCharacterData,
				ConversationPartnerData = conversationPartnerData,
				ConversationTerrainType = terrainType,
				TimeOfDay = timeOfDay,
				IsCurrentTerrainUnderSnow = isCurrentTerrainUnderSnow,
				Settlement = settlement,
				LocationId = locationId,
				IsRaining = isRaining,
				IsSnowing = isSnowing
			};
		}
	}
}
