using System;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x02000397 RID: 919
	public class KingdomState : GameState
	{
		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x0600351E RID: 13598 RVA: 0x000D978C File Offset: 0x000D798C
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x0600351F RID: 13599 RVA: 0x000D978F File Offset: 0x000D798F
		// (set) Token: 0x06003520 RID: 13600 RVA: 0x000D9797 File Offset: 0x000D7997
		public Army InitialSelectedArmy { get; private set; }

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06003521 RID: 13601 RVA: 0x000D97A0 File Offset: 0x000D79A0
		// (set) Token: 0x06003522 RID: 13602 RVA: 0x000D97A8 File Offset: 0x000D79A8
		public Settlement InitialSelectedSettlement { get; private set; }

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06003523 RID: 13603 RVA: 0x000D97B1 File Offset: 0x000D79B1
		// (set) Token: 0x06003524 RID: 13604 RVA: 0x000D97B9 File Offset: 0x000D79B9
		public Clan InitialSelectedClan { get; private set; }

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06003525 RID: 13605 RVA: 0x000D97C2 File Offset: 0x000D79C2
		// (set) Token: 0x06003526 RID: 13606 RVA: 0x000D97CA File Offset: 0x000D79CA
		public PolicyObject InitialSelectedPolicy { get; private set; }

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06003527 RID: 13607 RVA: 0x000D97D3 File Offset: 0x000D79D3
		// (set) Token: 0x06003528 RID: 13608 RVA: 0x000D97DB File Offset: 0x000D79DB
		public Kingdom InitialSelectedKingdom { get; private set; }

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06003529 RID: 13609 RVA: 0x000D97E4 File Offset: 0x000D79E4
		// (set) Token: 0x0600352A RID: 13610 RVA: 0x000D97EC File Offset: 0x000D79EC
		public KingdomDecision InitialSelectedDecision { get; private set; }

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x0600352B RID: 13611 RVA: 0x000D97F5 File Offset: 0x000D79F5
		// (set) Token: 0x0600352C RID: 13612 RVA: 0x000D97FD File Offset: 0x000D79FD
		public IKingdomStateHandler Handler
		{
			get
			{
				return this._handler;
			}
			set
			{
				this._handler = value;
			}
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000D9806 File Offset: 0x000D7A06
		public KingdomState()
		{
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x000D980E File Offset: 0x000D7A0E
		public KingdomState(KingdomDecision initialSelectedDecision)
		{
			this.InitialSelectedDecision = initialSelectedDecision;
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000D981D File Offset: 0x000D7A1D
		public KingdomState(Army initialSelectedArmy)
		{
			this.InitialSelectedArmy = initialSelectedArmy;
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x000D982C File Offset: 0x000D7A2C
		public KingdomState(Settlement initialSelectedSettlement)
		{
			this.InitialSelectedSettlement = initialSelectedSettlement;
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x000D983C File Offset: 0x000D7A3C
		public KingdomState(IFaction initialSelectedFaction)
		{
			Clan clan;
			if ((clan = initialSelectedFaction as Clan) != null)
			{
				this.InitialSelectedClan = clan;
				return;
			}
			Kingdom kingdom;
			if ((kingdom = initialSelectedFaction as Kingdom) != null)
			{
				this.InitialSelectedKingdom = kingdom;
			}
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x000D9872 File Offset: 0x000D7A72
		public KingdomState(PolicyObject initialSelectedPolicy)
		{
			this.InitialSelectedPolicy = initialSelectedPolicy;
		}

		// Token: 0x04000F33 RID: 3891
		private IKingdomStateHandler _handler;
	}
}
