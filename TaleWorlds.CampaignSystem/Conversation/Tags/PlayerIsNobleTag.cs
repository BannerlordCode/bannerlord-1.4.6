using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000254 RID: 596
	public class PlayerIsNobleTag : ConversationTag
	{
		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x0009AF8D File Offset: 0x0009918D
		public override string StringId
		{
			get
			{
				return "PlayerIsNobleTag";
			}
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x0009AF94 File Offset: 0x00099194
		public override bool IsApplicableTo(CharacterObject character)
		{
			return Settlement.All.Any<Settlement>((Settlement x) => x.OwnerClan == Hero.MainHero.Clan);
		}

		// Token: 0x04000A8A RID: 2698
		public const string Id = "PlayerIsNobleTag";
	}
}
