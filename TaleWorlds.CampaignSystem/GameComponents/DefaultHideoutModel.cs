using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x0200011E RID: 286
	public class DefaultHideoutModel : HideoutModel
	{
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x000754C7 File Offset: 0x000736C7
		public override CampaignTime HideoutHiddenDuration
		{
			get
			{
				return CampaignTime.Days(10f);
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x000754D3 File Offset: 0x000736D3
		public override int CanAttackHideoutStartTime
		{
			get
			{
				return CampaignTime.SunSet + 1;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x000754DC File Offset: 0x000736DC
		public override int CanAttackHideoutEndTime
		{
			get
			{
				return CampaignTime.SunRise;
			}
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x000754E3 File Offset: 0x000736E3
		public override float GetRogueryXpGainAsGhost()
		{
			return MBRandom.RandomFloatRanged(1000f, 1400f);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x000754F4 File Offset: 0x000736F4
		public override float GetRogueryXpGainOnHideoutMissionEnd(bool isSucceeded)
		{
			return (float)(isSucceeded ? MBRandom.RandomInt(700, 1000) : MBRandom.RandomInt(225, 400));
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x0007551C File Offset: 0x0007371C
		public override float GetSendTroopsSuccessChance(Hideout hideout)
		{
			int skillValue = Hero.MainHero.GetSkillValue(DefaultSkills.Tactics);
			int skillValue2 = Hero.MainHero.GetSkillValue(DefaultSkills.Roguery);
			return 0.3f + (float)(skillValue + skillValue2) / (325f + (float)skillValue + (float)skillValue2);
		}
	}
}
