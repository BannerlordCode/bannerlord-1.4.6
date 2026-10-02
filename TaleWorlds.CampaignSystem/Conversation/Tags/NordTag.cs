using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000289 RID: 649
	public class NordTag : ConversationTag
	{
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x0009BB71 File Offset: 0x00099D71
		public override string StringId
		{
			get
			{
				return "NordTag";
			}
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x0009BB78 File Offset: 0x00099D78
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.Culture.StringId == "nord";
		}

		// Token: 0x04000AC0 RID: 2752
		public const string Id = "NordTag";
	}
}
