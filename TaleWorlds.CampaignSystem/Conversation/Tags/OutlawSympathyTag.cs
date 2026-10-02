using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000243 RID: 579
	public class OutlawSympathyTag : ConversationTag
	{
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x0009AB8B File Offset: 0x00098D8B
		public override string StringId
		{
			get
			{
				return "OutlawSympathyTag";
			}
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x0009AB92 File Offset: 0x00098D92
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && character.HeroObject.IsWanderer && character.HeroObject.GetTraitLevel(DefaultTraits.RogueSkills) > 0;
		}

		// Token: 0x04000A79 RID: 2681
		public const string Id = "OutlawSympathyTag";
	}
}
