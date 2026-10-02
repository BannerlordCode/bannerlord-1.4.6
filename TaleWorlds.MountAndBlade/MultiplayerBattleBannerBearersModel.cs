using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000202 RID: 514
	public class MultiplayerBattleBannerBearersModel : BattleBannerBearersModel
	{
		// Token: 0x06001DF5 RID: 7669 RVA: 0x000678B0 File Offset: 0x00065AB0
		public override int GetMinimumFormationTroopCountToBearBanners()
		{
			return int.MaxValue;
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x000678B7 File Offset: 0x00065AB7
		public override float GetBannerInteractionDistance(Agent interactingAgent)
		{
			return float.MaxValue;
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x000678BE File Offset: 0x00065ABE
		public override bool CanAgentPickUpAnyBanner(Agent agent)
		{
			return false;
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x000678C1 File Offset: 0x00065AC1
		public override bool CanBannerBearerProvideEffectToFormation(Agent agent, Formation formation)
		{
			return false;
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x000678C4 File Offset: 0x00065AC4
		public override bool CanAgentBecomeBannerBearer(Agent agent)
		{
			return false;
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x000678C7 File Offset: 0x00065AC7
		public override int GetAgentBannerBearingPriority(Agent agent)
		{
			return 0;
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x000678CA File Offset: 0x00065ACA
		public override bool CanFormationDeployBannerBearers(Formation formation)
		{
			return false;
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x000678CD File Offset: 0x00065ACD
		public override int GetDesiredNumberOfBannerBearersForFormation(Formation formation)
		{
			return 0;
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x000678D0 File Offset: 0x00065AD0
		public override ItemObject GetBannerBearerReplacementWeapon(BasicCharacterObject agentCharacter)
		{
			return null;
		}
	}
}
