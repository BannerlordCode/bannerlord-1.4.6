using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000297 RID: 663
	public class VoiceGroupPersonaIronicUpperTag : ConversationTag
	{
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x0009BDC8 File Offset: 0x00099FC8
		public override string StringId
		{
			get
			{
				return "VoiceGroupPersonaIronicUpperTag";
			}
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x0009BDCF File Offset: 0x00099FCF
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.GetPersona() == DefaultTraits.PersonaIronic && ConversationTagHelper.UsesHighRegister(character);
		}

		// Token: 0x04000ACE RID: 2766
		public const string Id = "VoiceGroupPersonaIronicUpperTag";
	}
}
