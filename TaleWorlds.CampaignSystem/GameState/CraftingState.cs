using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x0200038E RID: 910
	public class CraftingState : GameState
	{
		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x060034EC RID: 13548 RVA: 0x000D95E5 File Offset: 0x000D77E5
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x060034ED RID: 13549 RVA: 0x000D95E8 File Offset: 0x000D77E8
		// (set) Token: 0x060034EE RID: 13550 RVA: 0x000D95F0 File Offset: 0x000D77F0
		public Crafting CraftingLogic { get; private set; }

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060034EF RID: 13551 RVA: 0x000D95F9 File Offset: 0x000D77F9
		// (set) Token: 0x060034F0 RID: 13552 RVA: 0x000D9601 File Offset: 0x000D7801
		public ICraftingStateHandler Handler
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

		// Token: 0x060034F1 RID: 13553 RVA: 0x000D960A File Offset: 0x000D780A
		public void InitializeLogic(Crafting newCraftingLogic, bool isReplacingWeaponClass = false)
		{
			this.CraftingLogic = newCraftingLogic;
			if (this._handler != null)
			{
				if (isReplacingWeaponClass)
				{
					this._handler.OnCraftingLogicRefreshed();
					return;
				}
				this._handler.OnCraftingLogicInitialized();
			}
		}

		// Token: 0x04000F24 RID: 3876
		private ICraftingStateHandler _handler;
	}
}
