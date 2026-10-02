using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000285 RID: 645
	public class ImpulsiveTag : ConversationTag
	{
		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x0009BA65 File Offset: 0x00099C65
		public override string StringId
		{
			get
			{
				return "ImpulsiveTag";
			}
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x0009BA6C File Offset: 0x00099C6C
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && character.HeroObject.GetTraitLevel(DefaultTraits.Calculating) < 0;
		}

		// Token: 0x04000ABC RID: 2748
		public const string Id = "ImpulsiveTag";
	}
}
