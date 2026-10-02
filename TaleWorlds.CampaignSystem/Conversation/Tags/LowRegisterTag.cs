using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000245 RID: 581
	public class LowRegisterTag : ConversationTag
	{
		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x0009ABE7 File Offset: 0x00098DE7
		public override string StringId
		{
			get
			{
				return "LowRegisterTag";
			}
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x0009ABEE File Offset: 0x00098DEE
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && !ConversationTagHelper.UsesHighRegister(character) && ConversationTagHelper.UsesLowRegister(character);
		}

		// Token: 0x04000A7B RID: 2683
		public const string Id = "LowRegisterTag";
	}
}
