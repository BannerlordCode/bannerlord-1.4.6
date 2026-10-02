using System;
using Helpers;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000262 RID: 610
	public class UnderCommandTag : ConversationTag
	{
		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x0009B31E File Offset: 0x0009951E
		public override string StringId
		{
			get
			{
				return "UnderCommandTag";
			}
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x0009B325 File Offset: 0x00099525
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && character.HeroObject.Spouse != Hero.MainHero && HeroHelper.UnderPlayerCommand(character.HeroObject);
		}

		// Token: 0x04000A98 RID: 2712
		public const string Id = "UnderCommandTag";
	}
}
