using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000255 RID: 597
	public class PlayerIsRulerTag : ConversationTag
	{
		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x0600233C RID: 9020 RVA: 0x0009AFC7 File Offset: 0x000991C7
		public override string StringId
		{
			get
			{
				return "PlayerIsRulerTag";
			}
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x0009AFCE File Offset: 0x000991CE
		public override bool IsApplicableTo(CharacterObject character)
		{
			return Hero.MainHero.Clan.Leader == Hero.MainHero;
		}

		// Token: 0x04000A8B RID: 2699
		public const string Id = "PlayerIsRulerTag";
	}
}
