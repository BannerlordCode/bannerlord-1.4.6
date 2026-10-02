using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000257 RID: 599
	public class NpcIsMaleTag : ConversationTag
	{
		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06002342 RID: 9026 RVA: 0x0009B005 File Offset: 0x00099205
		public override string StringId
		{
			get
			{
				return "NpcIsMaleTag";
			}
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x0009B00C File Offset: 0x0009920C
		public override bool IsApplicableTo(CharacterObject character)
		{
			return !character.IsFemale;
		}

		// Token: 0x04000A8D RID: 2701
		public const string Id = "NpcIsMaleTag";
	}
}
