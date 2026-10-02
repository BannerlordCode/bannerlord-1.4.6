using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.ComponentInterfaces
{
	// Token: 0x020003FE RID: 1022
	public abstract class BattleBannerBearersModel : MBGameModel<BattleBannerBearersModel>
	{
		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x000E4E3B File Offset: 0x000E303B
		protected BannerBearerLogic BannerBearerLogic
		{
			get
			{
				return this._bannerBearerLogic;
			}
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x000E4E43 File Offset: 0x000E3043
		public void InitializeModel(BannerBearerLogic bannerBearerLogic)
		{
			this._bannerBearerLogic = bannerBearerLogic;
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x000E4E4C File Offset: 0x000E304C
		public void FinalizeModel()
		{
			this._bannerBearerLogic = null;
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000E4E58 File Offset: 0x000E3058
		public bool IsFormationBanner(Formation formation, SpawnedItemEntity item)
		{
			if (formation == null)
			{
				return false;
			}
			BannerBearerLogic bannerBearerLogic = this.BannerBearerLogic;
			return bannerBearerLogic != null && bannerBearerLogic.IsFormationBanner(formation, item);
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000E4E80 File Offset: 0x000E3080
		public bool IsBannerSearchingAgent(Agent agent)
		{
			BannerBearerLogic bannerBearerLogic = this.BannerBearerLogic;
			return bannerBearerLogic != null && bannerBearerLogic.IsBannerSearchingAgent(agent);
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x000E4EA0 File Offset: 0x000E30A0
		public bool IsInteractableFormationBanner(SpawnedItemEntity item, Agent interactingAgent)
		{
			BannerBearerLogic bannerBearerLogic = this.BannerBearerLogic;
			Formation formation = ((bannerBearerLogic != null) ? bannerBearerLogic.GetFormationFromBanner(item) : null);
			return formation == null || formation.Captain == interactingAgent || interactingAgent.Formation == formation || (interactingAgent.IsPlayerControlled && interactingAgent.Team == formation.Team);
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x000E4EF2 File Offset: 0x000E30F2
		public bool HasFormationBanner(Formation formation)
		{
			if (formation == null)
			{
				return false;
			}
			BannerBearerLogic bannerBearerLogic = this.BannerBearerLogic;
			return ((bannerBearerLogic != null) ? bannerBearerLogic.GetFormationBanner(formation) : null) != null;
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x000E4F10 File Offset: 0x000E3110
		public bool HasBannerOnGround(Formation formation)
		{
			if (formation == null)
			{
				return false;
			}
			BannerBearerLogic bannerBearerLogic = this.BannerBearerLogic;
			return bannerBearerLogic != null && bannerBearerLogic.HasBannerOnGround(formation);
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x000E4F35 File Offset: 0x000E3135
		public ItemObject GetFormationBanner(Formation formation)
		{
			if (formation == null)
			{
				return null;
			}
			BannerBearerLogic bannerBearerLogic = this.BannerBearerLogic;
			if (bannerBearerLogic == null)
			{
				return null;
			}
			return bannerBearerLogic.GetFormationBanner(formation);
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x000E4F50 File Offset: 0x000E3150
		public List<Agent> GetFormationBannerBearers(Formation formation)
		{
			if (formation == null)
			{
				return new List<Agent>();
			}
			BannerBearerLogic bannerBearerLogic = this.BannerBearerLogic;
			if (bannerBearerLogic != null)
			{
				return bannerBearerLogic.GetFormationBannerBearers(formation);
			}
			return new List<Agent>();
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x000E4F7D File Offset: 0x000E317D
		public BannerComponent GetActiveBanner(Formation formation)
		{
			if (formation == null)
			{
				return null;
			}
			BannerBearerLogic bannerBearerLogic = this.BannerBearerLogic;
			if (bannerBearerLogic == null)
			{
				return null;
			}
			return bannerBearerLogic.GetActiveBanner(formation);
		}

		// Token: 0x060037AB RID: 14251
		public abstract int GetMinimumFormationTroopCountToBearBanners();

		// Token: 0x060037AC RID: 14252
		public abstract float GetBannerInteractionDistance(Agent interactingAgent);

		// Token: 0x060037AD RID: 14253
		public abstract bool CanBannerBearerProvideEffectToFormation(Agent agent, Formation formation);

		// Token: 0x060037AE RID: 14254
		public abstract bool CanAgentPickUpAnyBanner(Agent agent);

		// Token: 0x060037AF RID: 14255
		public abstract bool CanAgentBecomeBannerBearer(Agent agent);

		// Token: 0x060037B0 RID: 14256
		public abstract int GetAgentBannerBearingPriority(Agent agent);

		// Token: 0x060037B1 RID: 14257
		public abstract bool CanFormationDeployBannerBearers(Formation formation);

		// Token: 0x060037B2 RID: 14258
		public abstract int GetDesiredNumberOfBannerBearersForFormation(Formation formation);

		// Token: 0x060037B3 RID: 14259
		public abstract ItemObject GetBannerBearerReplacementWeapon(BasicCharacterObject agentCharacter);

		// Token: 0x040017D9 RID: 6105
		public const float DefaultDetachmentCostMultiplier = 10f;

		// Token: 0x040017DA RID: 6106
		private BannerBearerLogic _bannerBearerLogic;
	}
}
