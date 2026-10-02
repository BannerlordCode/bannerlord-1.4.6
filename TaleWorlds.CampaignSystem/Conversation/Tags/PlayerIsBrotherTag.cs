using System;
using System.Linq;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200024F RID: 591
	public class PlayerIsBrotherTag : ConversationTag
	{
		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x0009ADDF File Offset: 0x00098FDF
		public override string StringId
		{
			get
			{
				return "PlayerIsBrotherTag";
			}
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x0009ADE6 File Offset: 0x00098FE6
		public override bool IsApplicableTo(CharacterObject character)
		{
			return !Hero.MainHero.IsFemale && character.IsHero && character.HeroObject.Siblings.Contains(Hero.MainHero);
		}

		// Token: 0x04000A85 RID: 2693
		public const string Id = "PlayerIsBrotherTag";
	}
}
