using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000293 RID: 659
	public class VoiceGroupPersonaCurtTribalTag : ConversationTag
	{
		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x0009BD30 File Offset: 0x00099F30
		public override string StringId
		{
			get
			{
				return "VoiceGroupPersonaCurtTribalTag";
			}
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x0009BD37 File Offset: 0x00099F37
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.GetPersona() == DefaultTraits.PersonaCurt && ConversationTagHelper.TribalVoiceGroup(character);
		}

		// Token: 0x04000ACA RID: 2762
		public const string Id = "VoiceGroupPersonaCurtTribalTag";
	}
}
