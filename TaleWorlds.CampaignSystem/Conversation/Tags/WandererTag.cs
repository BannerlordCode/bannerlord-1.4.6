using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000275 RID: 629
	public class WandererTag : ConversationTag
	{
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x0009B775 File Offset: 0x00099975
		public override string StringId
		{
			get
			{
				return "WandererTag";
			}
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x0009B77C File Offset: 0x0009997C
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && character.HeroObject.IsWanderer;
		}

		// Token: 0x04000AAC RID: 2732
		public const string Id = "WandererTag";
	}
}
