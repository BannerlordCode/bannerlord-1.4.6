using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200024B RID: 587
	public class PlayerIsDaughterTag : ConversationTag
	{
		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x0009ACEB File Offset: 0x00098EEB
		public override string StringId
		{
			get
			{
				return "PlayerIsDaughterTag";
			}
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x0009ACF2 File Offset: 0x00098EF2
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && Hero.MainHero.IsFemale && (Hero.MainHero.Father == character.HeroObject || Hero.MainHero.Mother == character.HeroObject);
		}

		// Token: 0x04000A81 RID: 2689
		public const string Id = "PlayerIsDaughterTag";
	}
}
