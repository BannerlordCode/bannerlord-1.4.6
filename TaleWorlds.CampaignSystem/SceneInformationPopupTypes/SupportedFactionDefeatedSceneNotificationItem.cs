using System;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.SceneInformationPopupTypes
{
	// Token: 0x020000CC RID: 204
	public class SupportedFactionDefeatedSceneNotificationItem : SceneNotificationData
	{
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0005EC63 File Offset: 0x0005CE63
		public Kingdom Faction { get; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0005EC6B File Offset: 0x0005CE6B
		public bool PlayerWantsRestore { get; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0005EC73 File Offset: 0x0005CE73
		public override string SceneID
		{
			get
			{
				return "scn_supported_faction_defeated_notification";
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0005EC7C File Offset: 0x0005CE7C
		public override TextObject TitleText
		{
			get
			{
				GameTexts.SetVariable("FORMAL_NAME", CampaignSceneNotificationHelper.GetFormalNameForKingdom(this.Faction));
				GameTexts.SetVariable("PLAYER_WANTS_RESTORE", this.PlayerWantsRestore ? 1 : 0);
				GameTexts.SetVariable("DAY_OF_YEAR", CampaignSceneNotificationHelper.GetFormalDayAndSeasonText(this._creationCampaignTime));
				GameTexts.SetVariable("YEAR", this._creationCampaignTime.GetYear);
				return GameTexts.FindText("str_supported_faction_defeated", null);
			}
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0005ECEC File Offset: 0x0005CEEC
		public override Banner[] GetBanners()
		{
			return new Banner[]
			{
				this.Faction.Banner,
				this.Faction.Banner
			};
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0005ED10 File Offset: 0x0005CF10
		public SupportedFactionDefeatedSceneNotificationItem(Kingdom faction, bool playerWantsRestore)
		{
			this.Faction = faction;
			this.PlayerWantsRestore = playerWantsRestore;
			this._creationCampaignTime = CampaignTime.Now;
		}

		// Token: 0x040006CD RID: 1741
		private readonly CampaignTime _creationCampaignTime;
	}
}
