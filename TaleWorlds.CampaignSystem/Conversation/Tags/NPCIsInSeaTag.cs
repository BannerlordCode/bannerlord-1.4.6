using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200028A RID: 650
	public class NPCIsInSeaTag : ConversationTag
	{
		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x060023DB RID: 9179 RVA: 0x0009BB97 File Offset: 0x00099D97
		public override string StringId
		{
			get
			{
				return "NPCIsInSeaTag";
			}
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x0009BBA0 File Offset: 0x00099DA0
		public override bool IsApplicableTo(CharacterObject character)
		{
			bool flag = false;
			if (character.IsHero)
			{
				flag = (character.HeroObject.IsPrisoner ? character.HeroObject.PartyBelongedToAsPrisoner.MobileParty : character.HeroObject.PartyBelongedTo).IsCurrentlyAtSea;
			}
			return flag;
		}

		// Token: 0x04000AC1 RID: 2753
		public const string Id = "NPCIsInSeaTag";
	}
}
