using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x0200038C RID: 908
	public class ClanState : GameState
	{
		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x060034D9 RID: 13529 RVA: 0x000D9529 File Offset: 0x000D7729
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x060034DA RID: 13530 RVA: 0x000D952C File Offset: 0x000D772C
		// (set) Token: 0x060034DB RID: 13531 RVA: 0x000D9534 File Offset: 0x000D7734
		public Hero InitialSelectedHero { get; private set; }

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x060034DC RID: 13532 RVA: 0x000D953D File Offset: 0x000D773D
		// (set) Token: 0x060034DD RID: 13533 RVA: 0x000D9545 File Offset: 0x000D7745
		public PartyBase InitialSelectedParty { get; private set; }

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x060034DE RID: 13534 RVA: 0x000D954E File Offset: 0x000D774E
		// (set) Token: 0x060034DF RID: 13535 RVA: 0x000D9556 File Offset: 0x000D7756
		public Settlement InitialSelectedSettlement { get; private set; }

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x060034E0 RID: 13536 RVA: 0x000D955F File Offset: 0x000D775F
		// (set) Token: 0x060034E1 RID: 13537 RVA: 0x000D9567 File Offset: 0x000D7767
		public Workshop InitialSelectedWorkshop { get; private set; }

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x060034E2 RID: 13538 RVA: 0x000D9570 File Offset: 0x000D7770
		// (set) Token: 0x060034E3 RID: 13539 RVA: 0x000D9578 File Offset: 0x000D7778
		public Alley InitialSelectedAlley { get; private set; }

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x060034E4 RID: 13540 RVA: 0x000D9581 File Offset: 0x000D7781
		// (set) Token: 0x060034E5 RID: 13541 RVA: 0x000D9589 File Offset: 0x000D7789
		public IClanStateHandler Handler
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

		// Token: 0x060034E6 RID: 13542 RVA: 0x000D9592 File Offset: 0x000D7792
		public ClanState()
		{
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x000D959A File Offset: 0x000D779A
		public ClanState(Hero initialSelectedHero)
		{
			this.InitialSelectedHero = initialSelectedHero;
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x000D95A9 File Offset: 0x000D77A9
		public ClanState(PartyBase initialSelectedParty)
		{
			this.InitialSelectedParty = initialSelectedParty;
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x000D95B8 File Offset: 0x000D77B8
		public ClanState(Settlement initialSelectedSettlement)
		{
			this.InitialSelectedSettlement = initialSelectedSettlement;
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x000D95C7 File Offset: 0x000D77C7
		public ClanState(Workshop initialSelectedWorkshop)
		{
			this.InitialSelectedWorkshop = initialSelectedWorkshop;
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000D95D6 File Offset: 0x000D77D6
		public ClanState(Alley initialSelectedAlley)
		{
			this.InitialSelectedAlley = initialSelectedAlley;
		}

		// Token: 0x04000F22 RID: 3874
		private IClanStateHandler _handler;
	}
}
