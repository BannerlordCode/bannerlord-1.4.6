using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MBHelpers;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001F7 RID: 503
	public class CustomBattleMoraleModel : BattleMoraleModel
	{
		// Token: 0x06001D8E RID: 7566 RVA: 0x000653E4 File Offset: 0x000635E4
		[return: TupleElementNames(new string[] { "affectedSideMaxMoraleLoss", "affectorSideMaxMoraleGain" })]
		public override ValueTuple<float, float> CalculateMaxMoraleChangeDueToAgentIncapacitated(Agent affectedAgent, AgentState affectedAgentState, Agent affectorAgent, in KillingBlow killingBlow)
		{
			float battleImportance = affectedAgent.GetBattleImportance();
			Team team = affectedAgent.Team;
			BattleSideEnum battleSideEnum = ((team != null) ? team.Side : BattleSideEnum.None);
			float num = this.CalculateCasualtiesFactor(battleSideEnum);
			SkillObject relevantSkillFromWeaponClass = WeaponComponentData.GetRelevantSkillFromWeaponClass((WeaponClass)killingBlow.WeaponClass);
			bool flag = relevantSkillFromWeaponClass == DefaultSkills.Bow || relevantSkillFromWeaponClass == DefaultSkills.Crossbow || relevantSkillFromWeaponClass == DefaultSkills.Throwing;
			bool flag2 = relevantSkillFromWeaponClass == DefaultSkills.OneHanded || relevantSkillFromWeaponClass == DefaultSkills.TwoHanded || relevantSkillFromWeaponClass == DefaultSkills.Polearm;
			bool flag3 = killingBlow.WeaponRecordWeaponFlags.HasAnyFlag(WeaponFlags.AffectsArea | WeaponFlags.AffectsAreaBig | WeaponFlags.MultiplePenetration);
			float num2 = 0.75f;
			if (flag3)
			{
				num2 = 0.25f;
				if (killingBlow.WeaponRecordWeaponFlags.HasAllFlags(WeaponFlags.Burning | WeaponFlags.MultiplePenetration))
				{
					num2 += num2 * 0.25f;
				}
			}
			else if (flag)
			{
				num2 = 0.5f;
			}
			num2 = Math.Max(0f, num2);
			FactoredNumber factoredNumber = new FactoredNumber(battleImportance * 3f * num2);
			FactoredNumber factoredNumber2 = new FactoredNumber(battleImportance * 4f * num2 * num);
			Formation formation = affectedAgent.Formation;
			BannerComponent activeBanner = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(formation);
			if (activeBanner != null)
			{
				BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.DecreasedMoraleShock, activeBanner, ref factoredNumber2);
			}
			Formation formation2 = affectorAgent.Formation;
			BannerComponent activeBanner2 = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(formation2);
			if (activeBanner2 != null && affectorAgent.Character.DefaultFormationClass == FormationClass.Infantry && flag2)
			{
				BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedMoraleShockByMeleeTroops, activeBanner2, ref factoredNumber);
			}
			return new ValueTuple<float, float>(MathF.Max(factoredNumber2.ResultNumber, 0f), MathF.Max(factoredNumber.ResultNumber, 0f));
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x00065574 File Offset: 0x00063774
		[return: TupleElementNames(new string[] { "affectedSideMaxMoraleLoss", "affectorSideMaxMoraleGain" })]
		public override ValueTuple<float, float> CalculateMaxMoraleChangeDueToAgentPanicked(Agent agent)
		{
			float battleImportance = agent.GetBattleImportance();
			Team team = agent.Team;
			BattleSideEnum battleSideEnum = ((team != null) ? team.Side : BattleSideEnum.None);
			float num = this.CalculateCasualtiesFactor(battleSideEnum);
			float num2 = battleImportance * 2f;
			float num3 = battleImportance * num * 1.1f;
			if (agent.Character != null)
			{
				FactoredNumber factoredNumber = new FactoredNumber(num3);
				Formation formation = agent.Formation;
				BannerComponent activeBanner = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(formation);
				if (activeBanner != null)
				{
					BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.DecreasedMoraleShock, activeBanner, ref factoredNumber);
				}
				num3 = factoredNumber.ResultNumber;
			}
			return new ValueTuple<float, float>(MathF.Max(num3, 0f), MathF.Max(num2, 0f));
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x00065613 File Offset: 0x00063813
		public override float CalculateMoraleChangeToCharacter(Agent agent, float maxMoraleChange)
		{
			return maxMoraleChange / MathF.Max(1f, agent.Character.GetMoraleResistance());
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0006562C File Offset: 0x0006382C
		public override float GetEffectiveInitialMorale(Agent agent, float baseMorale)
		{
			return baseMorale;
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0006562F File Offset: 0x0006382F
		public override bool CanPanicDueToMorale(Agent agent)
		{
			return true;
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x00065634 File Offset: 0x00063834
		public override float CalculateCasualtiesFactor(BattleSideEnum battleSide)
		{
			float num = 1f;
			if (Mission.Current != null && battleSide != BattleSideEnum.None)
			{
				float removedAgentRatioForSide = Mission.Current.GetRemovedAgentRatioForSide(battleSide);
				num += removedAgentRatioForSide * 2f;
				num = MathF.Max(0f, num);
			}
			return num;
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x00065678 File Offset: 0x00063878
		public override float GetAverageMorale(Formation formation)
		{
			float num = 0f;
			int num2 = 0;
			if (formation != null)
			{
				using (List<IFormationUnit>.Enumerator enumerator = formation.Arrangement.GetAllUnits().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Agent agent;
						if ((agent = enumerator.Current as Agent) != null && agent.IsActive() && agent.IsHuman && agent.IsAIControlled)
						{
							num2++;
							num += agent.GetMorale();
						}
					}
				}
			}
			if (num2 > 0)
			{
				return MBMath.ClampFloat(num / (float)num2, 0f, 100f);
			}
			return 0f;
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x00065720 File Offset: 0x00063920
		public override float CalculateMoraleChangeOnShipSunk(IShipOrigin shipOrigin)
		{
			return 0f;
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x00065727 File Offset: 0x00063927
		public override float CalculateMoraleOnRamming(Agent agent, IShipOrigin rammingShip, IShipOrigin rammedShip)
		{
			return agent.GetMorale();
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x0006572F File Offset: 0x0006392F
		public override float CalculateMoraleOnShipsConnected(Agent agent, IShipOrigin ownerShip, IShipOrigin targetShip)
		{
			return agent.GetMorale();
		}
	}
}
