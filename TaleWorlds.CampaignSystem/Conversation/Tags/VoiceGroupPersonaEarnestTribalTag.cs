using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000290 RID: 656
	public class VoiceGroupPersonaEarnestTribalTag : ConversationTag
	{
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x0009BCBE File Offset: 0x00099EBE
		public override string StringId
		{
			get
			{
				return "VoiceGroupPersonaEarnestTribalTag";
			}
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x0009BCC5 File Offset: 0x00099EC5
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.GetPersona() == DefaultTraits.PersonaEarnest && ConversationTagHelper.TribalVoiceGroup(character);
		}

		// Token: 0x04000AC7 RID: 2759
		public const string Id = "VoiceGroupPersonaEarnestTribalTag";
	}
}
