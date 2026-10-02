using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CraftingSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign.Order
{
	// Token: 0x02000110 RID: 272
	public class CraftingOrderPopupVM : ViewModel
	{
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060018F8 RID: 6392 RVA: 0x0005F6EE File Offset: 0x0005D8EE
		public bool HasOrders
		{
			get
			{
				return this.CraftingOrders.Count > 0;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0005F6FE File Offset: 0x0005D8FE
		public bool HasEnabledOrders
		{
			get
			{
				return this.CraftingOrders.Count<CraftingOrderItemVM>((CraftingOrderItemVM x) => x.IsEnabled) > 0;
			}
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0005F72D File Offset: 0x0005D92D
		public CraftingOrderPopupVM(Action<CraftingOrderItemVM> onDoneAction, Func<CraftingAvailableHeroItemVM> getCurrentCraftingHero, Func<CraftingOrder, IEnumerable<CraftingStatData>> getOrderStatDatas)
		{
			this._onDoneAction = onDoneAction;
			this._getCurrentCraftingHero = getCurrentCraftingHero;
			this._getOrderStatDatas = getOrderStatDatas;
			this._craftingBehavior = Campaign.Current.GetCampaignBehavior<ICraftingCampaignBehavior>();
			this.CraftingOrders = new MBBindingList<CraftingOrderItemVM>();
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0005F768 File Offset: 0x0005D968
		public void RefreshOrders()
		{
			this.CraftingOrders.Clear();
			if (Campaign.Current.GameMode == CampaignGameMode.Tutorial)
			{
				return;
			}
			IReadOnlyDictionary<Town, CraftingCampaignBehavior.CraftingOrderSlots> craftingOrders = this._craftingBehavior.CraftingOrders;
			Settlement currentSettlement = Settlement.CurrentSettlement;
			CraftingCampaignBehavior.CraftingOrderSlots craftingOrderSlots = craftingOrders[(currentSettlement != null) ? currentSettlement.Town : null];
			if (craftingOrderSlots == null)
			{
				return;
			}
			CraftingOrderPopupVM.OrderComparer orderComparer = new CraftingOrderPopupVM.OrderComparer();
			List<CraftingOrder> list = craftingOrderSlots.CustomOrders.Where<CraftingOrder>((CraftingOrder x) => x != null).ToList<CraftingOrder>();
			list.Sort(orderComparer);
			List<CraftingOrder> list2 = craftingOrderSlots.Slots.Where<CraftingOrder>((CraftingOrder x) => x != null).ToList<CraftingOrder>();
			list2.Sort(orderComparer);
			CampaignUIHelper.IssueQuestFlags issueQuestFlags = CampaignUIHelper.IssueQuestFlags.None;
			for (int i = 0; i < list.Count; i++)
			{
				List<CraftingStatData> list3 = this._getOrderStatDatas(list[i]).ToList<CraftingStatData>();
				CampaignUIHelper.IssueQuestFlags questFlagsForOrder = this.GetQuestFlagsForOrder(list[i]);
				this.CraftingOrders.Add(new CraftingOrderItemVM(list[i], new Action<CraftingOrderItemVM>(this.SelectOrder), this._getCurrentCraftingHero, list3, questFlagsForOrder));
				issueQuestFlags |= questFlagsForOrder;
			}
			this.QuestType = (int)issueQuestFlags;
			for (int j = 0; j < list2.Count; j++)
			{
				List<CraftingStatData> list4 = this._getOrderStatDatas(list2[j]).ToList<CraftingStatData>();
				this.CraftingOrders.Add(new CraftingOrderItemVM(list2[j], new Action<CraftingOrderItemVM>(this.SelectOrder), this._getCurrentCraftingHero, list4, CampaignUIHelper.IssueQuestFlags.None));
			}
			TextObject textObject = new TextObject("{=MkVTRqAw}Orders ({ORDER_COUNT})", null);
			textObject.SetTextVariable("ORDER_COUNT", this.CraftingOrders.Count);
			this.OrderCountText = textObject.ToString();
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0005F935 File Offset: 0x0005DB35
		private CampaignUIHelper.IssueQuestFlags GetQuestFlagsForOrder(CraftingOrder order)
		{
			if (Campaign.Current.QuestManager.TrackedObjects.ContainsKey(order))
			{
				return CampaignUIHelper.IssueQuestFlags.ActiveIssue;
			}
			return CampaignUIHelper.IssueQuestFlags.None;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0005F951 File Offset: 0x0005DB51
		public void SelectOrder(CraftingOrderItemVM order)
		{
			if (this.SelectedCraftingOrder != null)
			{
				this.SelectedCraftingOrder.IsSelected = false;
			}
			this.SelectedCraftingOrder = order;
			this.SelectedCraftingOrder.IsSelected = true;
			this._onDoneAction(order);
			this.IsVisible = false;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0005F98D File Offset: 0x0005DB8D
		public void ExecuteOpenPopup()
		{
			this.IsVisible = true;
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0005F996 File Offset: 0x0005DB96
		public void ExecuteCloseWithoutSelection()
		{
			this.IsVisible = false;
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0005F99F File Offset: 0x0005DB9F
		// (set) Token: 0x06001901 RID: 6401 RVA: 0x0005F9A7 File Offset: 0x0005DBA7
		[DataSourceProperty]
		public bool IsVisible
		{
			get
			{
				return this._isVisible;
			}
			set
			{
				if (value != this._isVisible)
				{
					this._isVisible = value;
					base.OnPropertyChangedWithValue(value, "IsVisible");
					Game game = Game.Current;
					if (game == null)
					{
						return;
					}
					game.EventManager.TriggerEvent<CraftingOrderSelectionOpenedEvent>(new CraftingOrderSelectionOpenedEvent(this._isVisible));
				}
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x0005F9E4 File Offset: 0x0005DBE4
		// (set) Token: 0x06001903 RID: 6403 RVA: 0x0005F9EC File Offset: 0x0005DBEC
		[DataSourceProperty]
		public int QuestType
		{
			get
			{
				return this._questType;
			}
			set
			{
				if (value != this._questType)
				{
					this._questType = value;
					base.OnPropertyChangedWithValue(value, "QuestType");
				}
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x0005FA0A File Offset: 0x0005DC0A
		// (set) Token: 0x06001905 RID: 6405 RVA: 0x0005FA12 File Offset: 0x0005DC12
		[DataSourceProperty]
		public string OrderCountText
		{
			get
			{
				return this._orderCountText;
			}
			set
			{
				if (value != this._orderCountText)
				{
					this._orderCountText = value;
					base.OnPropertyChangedWithValue<string>(value, "OrderCountText");
				}
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x0005FA35 File Offset: 0x0005DC35
		// (set) Token: 0x06001907 RID: 6407 RVA: 0x0005FA3D File Offset: 0x0005DC3D
		[DataSourceProperty]
		public CraftingOrderItemVM SelectedCraftingOrder
		{
			get
			{
				return this._selectedCraftingOrder;
			}
			set
			{
				if (value != this._selectedCraftingOrder)
				{
					this._selectedCraftingOrder = value;
					base.OnPropertyChangedWithValue<CraftingOrderItemVM>(value, "SelectedCraftingOrder");
				}
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x0005FA5B File Offset: 0x0005DC5B
		// (set) Token: 0x06001909 RID: 6409 RVA: 0x0005FA63 File Offset: 0x0005DC63
		[DataSourceProperty]
		public MBBindingList<CraftingOrderItemVM> CraftingOrders
		{
			get
			{
				return this._craftingOrders;
			}
			set
			{
				if (value != this._craftingOrders)
				{
					this._craftingOrders = value;
					base.OnPropertyChangedWithValue<MBBindingList<CraftingOrderItemVM>>(value, "CraftingOrders");
				}
			}
		}

		// Token: 0x04000B78 RID: 2936
		private Action<CraftingOrderItemVM> _onDoneAction;

		// Token: 0x04000B79 RID: 2937
		private Func<CraftingAvailableHeroItemVM> _getCurrentCraftingHero;

		// Token: 0x04000B7A RID: 2938
		private Func<CraftingOrder, IEnumerable<CraftingStatData>> _getOrderStatDatas;

		// Token: 0x04000B7B RID: 2939
		private readonly ICraftingCampaignBehavior _craftingBehavior;

		// Token: 0x04000B7C RID: 2940
		private bool _isVisible;

		// Token: 0x04000B7D RID: 2941
		private int _questType;

		// Token: 0x04000B7E RID: 2942
		private string _orderCountText;

		// Token: 0x04000B7F RID: 2943
		private MBBindingList<CraftingOrderItemVM> _craftingOrders;

		// Token: 0x04000B80 RID: 2944
		private CraftingOrderItemVM _selectedCraftingOrder;

		// Token: 0x02000277 RID: 631
		private class OrderComparer : IComparer<CraftingOrder>
		{
			// Token: 0x060025A5 RID: 9637 RVA: 0x00081EF9 File Offset: 0x000800F9
			public int Compare(CraftingOrder x, CraftingOrder y)
			{
				return (int)(x.OrderDifficulty - y.OrderDifficulty);
			}
		}
	}
}
