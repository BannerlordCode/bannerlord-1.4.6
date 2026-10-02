using System;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000204 RID: 516
	public class MultiplayerBattleMoraleModel : BattleMoraleModel
	{
		// Token: 0x06001E02 RID: 7682 RVA: 0x000678ED File Offset: 0x00065AED
		[return: TupleElementNames(new string[] { "affectedSideMaxMoraleLoss", "affectorSideMaxMoraleGain" })]
		public override ValueTuple<float, float> CalculateMaxMoraleChangeDueToAgentIncapacitated(Agent affectedAgent, AgentState affectedAgentState, Agent affectorAgent, in KillingBlow killingBlow)
		{
			return new ValueTuple<float, float>(0f, 0f);
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x000678FE File Offset: 0x00065AFE
		[return: TupleElementNames(new string[] { "affectedSideMaxMoraleLoss", "affectorSideMaxMoraleGain" })]
		public override ValueTuple<float, float> CalculateMaxMoraleChangeDueToAgentPanicked(Agent agent)
		{
			return new ValueTuple<float, float>(0f, 0f);
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x0006790F File Offset: 0x00065B0F
		public override float CalculateMoraleChangeToCharacter(Agent agent, float maxMoraleChange)
		{
			return 0f;
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x00067916 File Offset: 0x00065B16
		public override float GetEffectiveInitialMorale(Agent agent, float baseMorale)
		{
			return baseMorale;
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x00067919 File Offset: 0x00065B19
		public override bool CanPanicDueToMorale(Agent agent)
		{
			return true;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x0006791C File Offset: 0x00065B1C
		public override float CalculateCasualtiesFactor(BattleSideEnum battleSide)
		{
			return 1f;
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x00067923 File Offset: 0x00065B23
		public override float GetAverageMorale(Formation formation)
		{
			return 0f;
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x0006792A File Offset: 0x00065B2A
		public override float CalculateMoraleChangeOnShipSunk(IShipOrigin shipOrigin)
		{
			return 0f;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x00067931 File Offset: 0x00065B31
		public override float CalculateMoraleOnRamming(Agent agent, IShipOrigin rammingShip, IShipOrigin rammedShip)
		{
			return agent.GetMorale();
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x00067939 File Offset: 0x00065B39
		public override float CalculateMoraleOnShipsConnected(Agent agent, IShipOrigin ownerShip, IShipOrigin targetShip)
		{
			return agent.GetMorale();
		}
	}
}
