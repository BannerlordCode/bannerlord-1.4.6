using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.SceneInformationPopupTypes
{
	// Token: 0x020000B4 RID: 180
	public class ClanMemberWarDeathSceneNotificationItem : SceneNotificationData
	{
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x0005BDAD File Offset: 0x00059FAD
		public Hero DeadHero { get; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0005BDB5 File Offset: 0x00059FB5
		public override string SceneID
		{
			get
			{
				return "scn_cutscene_family_member_death_war";
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x0005BDBC File Offset: 0x00059FBC
		public override TextObject TitleText
		{
			get
			{
				GameTexts.SetVariable("DAY_OF_YEAR", CampaignSceneNotificationHelper.GetFormalDayAndSeasonText(this._creationCampaignTime));
				GameTexts.SetVariable("YEAR", this._creationCampaignTime.GetYear);
				GameTexts.SetVariable("NAME", this.DeadHero.Name);
				return GameTexts.FindText("str_family_member_death_war", null);
			}
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0005BE16 File Offset: 0x0005A016
		public override Banner[] GetBanners()
		{
			return new Banner[] { this.DeadHero.ClanBanner };
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0005BE2C File Offset: 0x0005A02C
		public override SceneNotificationData.SceneNotificationCharacter[] GetSceneNotificationCharacters()
		{
			List<SceneNotificationData.SceneNotificationCharacter> list = new List<SceneNotificationData.SceneNotificationCharacter>();
			Equipment equipment = this.DeadHero.CivilianEquipment.Clone(false);
			CampaignSceneNotificationHelper.RemoveWeaponsFromEquipment(ref equipment, false, false);
			list.Add(CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(this.DeadHero, equipment, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false));
			foreach (Hero hero in CampaignSceneNotificationHelper.GetMilitaryAudienceForHero(this.DeadHero, true, false).Take<Hero>(5))
			{
				Equipment equipment2 = hero.CivilianEquipment.Clone(false);
				CampaignSceneNotificationHelper.RemoveWeaponsFromEquipment(ref equipment2, false, false);
				list.Add(CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(hero, equipment2, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false));
			}
			return list.ToArray();
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0005BEFC File Offset: 0x0005A0FC
		public ClanMemberWarDeathSceneNotificationItem(Hero deadHero, CampaignTime creationTime)
		{
			this.DeadHero = deadHero;
			this._creationCampaignTime = creationTime;
		}

		// Token: 0x04000678 RID: 1656
		private const int NumberOfAudienceHeroes = 5;

		// Token: 0x0400067A RID: 1658
		private readonly CampaignTime _creationCampaignTime;
	}
}
