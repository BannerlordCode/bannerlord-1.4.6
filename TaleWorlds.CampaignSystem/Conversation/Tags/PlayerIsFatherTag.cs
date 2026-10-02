using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200024D RID: 589
	public class PlayerIsFatherTag : ConversationTag
	{
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x0009AD85 File Offset: 0x00098F85
		public override string StringId
		{
			get
			{
				return "PlayerIsFatherTag";
			}
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x0009AD8C File Offset: 0x00098F8C
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && character.HeroObject.Father == Hero.MainHero;
		}

		// Token: 0x04000A83 RID: 2691
		public const string Id = "PlayerIsFatherTag";
	}
}
