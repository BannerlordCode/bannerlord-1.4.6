using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200027D RID: 637
	public class SturgianTag : ConversationTag
	{
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x0009B8FD File Offset: 0x00099AFD
		public override string StringId
		{
			get
			{
				return "SturgianTag";
			}
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x0009B904 File Offset: 0x00099B04
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.Culture.StringId == "sturgia";
		}

		// Token: 0x04000AB4 RID: 2740
		public const string Id = "SturgianTag";
	}
}
