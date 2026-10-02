using System;
using Helpers;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x02000395 RID: 917
	public class InventoryState : PlayerGameState
	{
		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06003511 RID: 13585 RVA: 0x000D973D File Offset: 0x000D793D
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06003512 RID: 13586 RVA: 0x000D9740 File Offset: 0x000D7940
		// (set) Token: 0x06003513 RID: 13587 RVA: 0x000D9748 File Offset: 0x000D7948
		public InventoryLogic InventoryLogic { get; set; }

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06003514 RID: 13588 RVA: 0x000D9751 File Offset: 0x000D7951
		// (set) Token: 0x06003515 RID: 13589 RVA: 0x000D9759 File Offset: 0x000D7959
		public InventoryScreenHelper.InventoryMode InventoryMode { get; set; }

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06003516 RID: 13590 RVA: 0x000D9762 File Offset: 0x000D7962
		// (set) Token: 0x06003517 RID: 13591 RVA: 0x000D976A File Offset: 0x000D796A
		public Action DoneLogicExtrasDelegate { get; set; }

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06003518 RID: 13592 RVA: 0x000D9773 File Offset: 0x000D7973
		// (set) Token: 0x06003519 RID: 13593 RVA: 0x000D977B File Offset: 0x000D797B
		public IInventoryStateHandler Handler { get; set; }
	}
}
