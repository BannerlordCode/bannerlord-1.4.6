using System;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.SceneInformationPopupTypes
{
	// Token: 0x020000BC RID: 188
	public class FindingSecondBannerPieceSceneNotificationItem : SceneNotificationData
	{
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x0005C852 File Offset: 0x0005AA52
		public Hero PlayerHero { get; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x0005C85A File Offset: 0x0005AA5A
		public override string SceneID
		{
			get
			{
				return "scn_second_banner_piece_notification";
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0005C864 File Offset: 0x0005AA64
		public override TextObject TitleText
		{
			get
			{
				GameTexts.SetVariable("DAY_OF_YEAR", CampaignSceneNotificationHelper.GetFormalDayAndSeasonText(this._creationCampaignTime));
				GameTexts.SetVariable("YEAR", this._creationCampaignTime.GetYear);
				return GameTexts.FindText("str_second_banner_piece_found", null);
			}
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0005C8A9 File Offset: 0x0005AAA9
		public override Banner[] GetBanners()
		{
			return new Banner[] { this.PlayerHero.ClanBanner };
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0005C8BF File Offset: 0x0005AABF
		public FindingSecondBannerPieceSceneNotificationItem(Hero playerHero)
		{
			this.PlayerHero = playerHero;
			this._creationCampaignTime = CampaignTime.Now;
		}

		// Token: 0x0400068D RID: 1677
		private readonly CampaignTime _creationCampaignTime;
	}
}
