using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000244 RID: 580
	public class HighRegisterTag : ConversationTag
	{
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x0009ABC6 File Offset: 0x00098DC6
		public override string StringId
		{
			get
			{
				return "HighRegisterTag";
			}
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x0009ABCD File Offset: 0x00098DCD
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && ConversationTagHelper.UsesHighRegister(character);
		}

		// Token: 0x04000A7A RID: 2682
		public const string Id = "HighRegisterTag";
	}
}
