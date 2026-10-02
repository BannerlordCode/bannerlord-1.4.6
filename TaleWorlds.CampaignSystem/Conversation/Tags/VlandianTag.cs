using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200027A RID: 634
	public class VlandianTag : ConversationTag
	{
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x060023AB RID: 9131 RVA: 0x0009B88B File Offset: 0x00099A8B
		public override string StringId
		{
			get
			{
				return "VlandianTag";
			}
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x0009B892 File Offset: 0x00099A92
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.Culture.StringId == "vlandia";
		}

		// Token: 0x04000AB1 RID: 2737
		public const string Id = "VlandianTag";
	}
}
