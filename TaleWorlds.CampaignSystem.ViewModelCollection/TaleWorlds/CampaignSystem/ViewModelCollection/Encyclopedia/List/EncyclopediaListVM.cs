using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000E2 RID: 226
	public class EncyclopediaListVM : EncyclopediaPageVM
	{
		// Token: 0x06001562 RID: 5474 RVA: 0x00054808 File Offset: 0x00052A08
		public EncyclopediaListVM(EncyclopediaPageArgs args)
			: base(args)
		{
			this.Page = base.Obj as EncyclopediaPage;
			this.Items = new MBBindingList<EncyclopediaListItemVM>();
			this.FilterGroups = new MBBindingList<EncyclopediaFilterGroupVM>();
			this.SortController = new EncyclopediaListSortControllerVM(this.Page, this.Items);
			this.IsInitializationOver = true;
			foreach (EncyclopediaFilterGroup encyclopediaFilterGroup in this.Page.GetFilterItems())
			{
				this.FilterGroups.Add(new EncyclopediaFilterGroupVM(encyclopediaFilterGroup, new Action<EncyclopediaListFilterVM>(this.UpdateFilters)));
			}
			this.IsInitializationOver = false;
			this.Items.Clear();
			foreach (EncyclopediaListItem encyclopediaListItem in this.Page.GetListItems())
			{
				EncyclopediaListItemVM encyclopediaListItemVM = new EncyclopediaListItemVM(encyclopediaListItem);
				encyclopediaListItemVM.IsFiltered = this.Page.IsFiltered(encyclopediaListItemVM.Object);
				this.Items.Add(encyclopediaListItemVM);
			}
			this.RefreshValues();
			this.IsInitializationOver = true;
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00054958 File Offset: 0x00052B58
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent evnt)
		{
			this.IsFilterHighlightEnabled = evnt.NewNotificationElementID == "EncyclopediaFiltersContainer";
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00054970 File Offset: 0x00052B70
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.SortController.RefreshValues();
			this.EmptyListText = GameTexts.FindText("str_encyclopedia_empty_list_error", null).ToString();
			this.Items.ApplyActionOnAllItems(delegate(EncyclopediaListItemVM x)
			{
				x.RefreshValues();
			});
			this.FilterGroups.ApplyActionOnAllItems(delegate(EncyclopediaFilterGroupVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x000549F8 File Offset: 0x00052BF8
		public override void OnFinalize()
		{
			base.OnFinalize();
			Game.Current.EventManager.UnregisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			EncyclopediaListSortControllerVM sortController = this.SortController;
			if (sortController != null)
			{
				sortController.OnFinalize();
			}
			this.SortController = null;
			this.FilterGroups.ApplyActionOnAllItems(delegate(EncyclopediaFilterGroupVM x)
			{
				x.OnFinalize();
			});
			this.FilterGroups.Clear();
			this.Items.ApplyActionOnAllItems(delegate(EncyclopediaListItemVM x)
			{
				x.OnFinalize();
			});
			this.Items.Clear();
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00054AA8 File Offset: 0x00052CA8
		public override string GetName()
		{
			return this.Page.GetName().ToString();
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00054ABC File Offset: 0x00052CBC
		public override string GetNavigationBarURL()
		{
			string text = HyperlinkTexts.GetGenericHyperlinkText("Home", GameTexts.FindText("str_encyclopedia_home", null).ToString()) + " \\ ";
			if (this.Page.HasIdentifierType(typeof(Kingdom)))
			{
				text += GameTexts.FindText("str_encyclopedia_kingdoms", null).ToString();
			}
			else if (this.Page.HasIdentifierType(typeof(Clan)))
			{
				text += GameTexts.FindText("str_encyclopedia_clans", null).ToString();
			}
			else if (this.Page.HasIdentifierType(typeof(Hero)))
			{
				text += GameTexts.FindText("str_encyclopedia_heroes", null).ToString();
			}
			else if (this.Page.HasIdentifierType(typeof(Settlement)))
			{
				text += GameTexts.FindText("str_encyclopedia_settlements", null).ToString();
			}
			else if (this.Page.HasIdentifierType(typeof(CharacterObject)))
			{
				text += GameTexts.FindText("str_encyclopedia_troops", null).ToString();
			}
			else if (this.Page.HasIdentifierType(typeof(Concept)))
			{
				text += GameTexts.FindText("str_encyclopedia_concepts", null).ToString();
			}
			else if (this.Page.HasIdentifierType(typeof(ShipHull)))
			{
				text += GameTexts.FindText("str_encyclopedia_ships", null).ToString();
			}
			return text;
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00054C4C File Offset: 0x00052E4C
		private void ExecuteResetFilters()
		{
			foreach (EncyclopediaFilterGroupVM encyclopediaFilterGroupVM in this.FilterGroups)
			{
				foreach (EncyclopediaListFilterVM encyclopediaListFilterVM in encyclopediaFilterGroupVM.Filters)
				{
					encyclopediaListFilterVM.IsSelected = false;
				}
			}
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00054CCC File Offset: 0x00052ECC
		public void CopyFiltersFrom(Dictionary<EncyclopediaFilterItem, bool> filters)
		{
			this.FilterGroups.ApplyActionOnAllItems(delegate(EncyclopediaFilterGroupVM x)
			{
				x.CopyFiltersFrom(filters);
			});
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00054D00 File Offset: 0x00052F00
		public override void Refresh()
		{
			base.Refresh();
			foreach (EncyclopediaListItemVM encyclopediaListItemVM in this.Items)
			{
				Hero hero;
				Clan clan;
				Concept concept;
				Kingdom kingdom;
				Settlement settlement;
				CharacterObject characterObject;
				ShipHull shipHull;
				if ((hero = encyclopediaListItemVM.Object as Hero) != null)
				{
					encyclopediaListItemVM.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(hero);
				}
				else if ((clan = encyclopediaListItemVM.Object as Clan) != null)
				{
					encyclopediaListItemVM.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(clan);
				}
				else if ((concept = encyclopediaListItemVM.Object as Concept) != null)
				{
					encyclopediaListItemVM.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(concept);
				}
				else if ((kingdom = encyclopediaListItemVM.Object as Kingdom) != null)
				{
					encyclopediaListItemVM.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(kingdom);
				}
				else if ((settlement = encyclopediaListItemVM.Object as Settlement) != null)
				{
					encyclopediaListItemVM.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(settlement);
				}
				else if ((characterObject = encyclopediaListItemVM.Object as CharacterObject) != null)
				{
					encyclopediaListItemVM.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(characterObject);
				}
				else if ((shipHull = encyclopediaListItemVM.Object as ShipHull) != null)
				{
					encyclopediaListItemVM.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(shipHull);
				}
			}
			this._isInitializationOver = false;
			this.IsInitializationOver = true;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00054EB4 File Offset: 0x000530B4
		private void UpdateFilters(EncyclopediaListFilterVM filterVM)
		{
			this.IsInitializationOver = false;
			foreach (EncyclopediaListItemVM encyclopediaListItemVM in this.Items)
			{
				encyclopediaListItemVM.IsFiltered = this.Page.IsFiltered(encyclopediaListItemVM.Object);
			}
			this.IsInitializationOver = true;
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x00054F20 File Offset: 0x00053120
		// (set) Token: 0x0600156D RID: 5485 RVA: 0x00054F28 File Offset: 0x00053128
		[DataSourceProperty]
		public string EmptyListText
		{
			get
			{
				return this._emptyListText;
			}
			set
			{
				if (value != this._emptyListText)
				{
					this._emptyListText = value;
					base.OnPropertyChangedWithValue<string>(value, "EmptyListText");
				}
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x00054F4B File Offset: 0x0005314B
		// (set) Token: 0x0600156F RID: 5487 RVA: 0x00054F53 File Offset: 0x00053153
		[DataSourceProperty]
		public string LastSelectedItemId
		{
			get
			{
				return this._lastSelectedItemId;
			}
			set
			{
				if (value != this._lastSelectedItemId)
				{
					this._lastSelectedItemId = value;
					base.OnPropertyChangedWithValue<string>(value, "LastSelectedItemId");
				}
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x00054F76 File Offset: 0x00053176
		// (set) Token: 0x06001571 RID: 5489 RVA: 0x00054F7E File Offset: 0x0005317E
		[DataSourceProperty]
		public override MBBindingList<EncyclopediaListItemVM> Items
		{
			get
			{
				return this._items;
			}
			set
			{
				if (value != this._items)
				{
					this._items = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaListItemVM>>(value, "Items");
				}
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x00054F9C File Offset: 0x0005319C
		// (set) Token: 0x06001573 RID: 5491 RVA: 0x00054FA4 File Offset: 0x000531A4
		[DataSourceProperty]
		public override EncyclopediaListSortControllerVM SortController
		{
			get
			{
				return this._sortController;
			}
			set
			{
				if (value != this._sortController)
				{
					this._sortController = value;
					base.OnPropertyChangedWithValue<EncyclopediaListSortControllerVM>(value, "SortController");
				}
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x00054FC2 File Offset: 0x000531C2
		// (set) Token: 0x06001575 RID: 5493 RVA: 0x00054FCA File Offset: 0x000531CA
		[DataSourceProperty]
		public bool IsInitializationOver
		{
			get
			{
				return this._isInitializationOver;
			}
			set
			{
				if (value != this._isInitializationOver)
				{
					this._isInitializationOver = value;
					base.OnPropertyChangedWithValue(value, "IsInitializationOver");
				}
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x00054FE8 File Offset: 0x000531E8
		// (set) Token: 0x06001577 RID: 5495 RVA: 0x00054FF0 File Offset: 0x000531F0
		[DataSourceProperty]
		public bool IsFilterHighlightEnabled
		{
			get
			{
				return this._isFilterHighlightEnabled;
			}
			set
			{
				if (value != this._isFilterHighlightEnabled)
				{
					this._isFilterHighlightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsFilterHighlightEnabled");
				}
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x0005500E File Offset: 0x0005320E
		// (set) Token: 0x06001579 RID: 5497 RVA: 0x00055016 File Offset: 0x00053216
		[DataSourceProperty]
		public override MBBindingList<EncyclopediaFilterGroupVM> FilterGroups
		{
			get
			{
				return this._filterGroups;
			}
			set
			{
				if (value != this._filterGroups)
				{
					this._filterGroups = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaFilterGroupVM>>(value, "FilterGroups");
				}
			}
		}

		// Token: 0x040009BF RID: 2495
		public readonly EncyclopediaPage Page;

		// Token: 0x040009C0 RID: 2496
		private MBBindingList<EncyclopediaFilterGroupVM> _filterGroups;

		// Token: 0x040009C1 RID: 2497
		private MBBindingList<EncyclopediaListItemVM> _items;

		// Token: 0x040009C2 RID: 2498
		private EncyclopediaListSortControllerVM _sortController;

		// Token: 0x040009C3 RID: 2499
		private bool _isInitializationOver;

		// Token: 0x040009C4 RID: 2500
		private bool _isFilterHighlightEnabled;

		// Token: 0x040009C5 RID: 2501
		private string _emptyListText;

		// Token: 0x040009C6 RID: 2502
		private string _lastSelectedItemId;
	}
}
