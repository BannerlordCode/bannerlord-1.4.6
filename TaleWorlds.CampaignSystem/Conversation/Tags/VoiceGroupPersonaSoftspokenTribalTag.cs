using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000299 RID: 665
	public class VoiceGroupPersonaSoftspokenTribalTag : ConversationTag
	{
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x0009BE14 File Offset: 0x0009A014
		public override string StringId
		{
			get
			{
				return "VoiceGroupPersonaSoftspokenTribalTag";
			}
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x0009BE1B File Offset: 0x0009A01B
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.GetPersona() == DefaultTraits.PersonaSoftspoken && ConversationTagHelper.TribalVoiceGroup(character);
		}

		// Token: 0x04000AD0 RID: 2768
		public const string Id = "VoiceGroupPersonaSoftspokenTribalTag";
	}
}
