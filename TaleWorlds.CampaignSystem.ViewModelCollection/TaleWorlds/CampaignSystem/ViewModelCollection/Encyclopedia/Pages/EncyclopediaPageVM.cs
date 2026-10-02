using System;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000D6 RID: 214
	public class EncyclopediaPageVM : ViewModel
	{
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x00051F0C File Offset: 0x0005010C
		public object Obj
		{
			get
			{
				return this._args.Obj;
			}
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00051F19 File Offset: 0x00050119
		public virtual string GetName()
		{
			return "";
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00051F20 File Offset: 0x00050120
		public virtual string GetNavigationBarURL()
		{
			return "";
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00051F27 File Offset: 0x00050127
		public virtual void Refresh()
		{
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00051F29 File Offset: 0x00050129
		public EncyclopediaPageVM(EncyclopediaPageArgs args)
		{
			this._args = args;
			this.BookmarkHint = new HintViewModel();
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00051F43 File Offset: 0x00050143
		public virtual void OnTick()
		{
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00051F45 File Offset: 0x00050145
		public virtual void ExecuteSwitchBookmarkedState()
		{
			this.IsBookmarked = !this.IsBookmarked;
			this.UpdateBookmarkHintText();
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00051F5C File Offset: 0x0005015C
		protected void UpdateBookmarkHintText()
		{
			if (this.IsBookmarked)
			{
				this.BookmarkHint.HintText = new TextObject("{=BV5exuPf}Remove From Bookmarks", null);
				return;
			}
			this.BookmarkHint.HintText = new TextObject("{=d8jrv3nA}Add To Bookmarks", null);
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x00051F93 File Offset: 0x00050193
		// (set) Token: 0x0600148B RID: 5259 RVA: 0x00051F9B File Offset: 0x0005019B
		[DataSourceProperty]
		public bool IsLoadingOver
		{
			get
			{
				return this._isLoadingOver;
			}
			set
			{
				if (value != this._isLoadingOver)
				{
					this._isLoadingOver = value;
					base.OnPropertyChangedWithValue(value, "IsLoadingOver");
				}
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00051FB9 File Offset: 0x000501B9
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x00051FC1 File Offset: 0x000501C1
		[DataSourceProperty]
		public bool IsBookmarked
		{
			get
			{
				return this._isBookmarked;
			}
			set
			{
				if (value != this._isBookmarked)
				{
					this._isBookmarked = value;
					base.OnPropertyChanged("IsBookmarked");
				}
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x00051FDE File Offset: 0x000501DE
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x00051FE6 File Offset: 0x000501E6
		[DataSourceProperty]
		public HintViewModel BookmarkHint
		{
			get
			{
				return this._bookmarkHint;
			}
			set
			{
				if (value != this._bookmarkHint)
				{
					this._bookmarkHint = value;
					base.OnPropertyChanged("BookmarkHint");
				}
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x00052003 File Offset: 0x00050203
		// (set) Token: 0x06001491 RID: 5265 RVA: 0x00052006 File Offset: 0x00050206
		[DataSourceProperty]
		public virtual MBBindingList<EncyclopediaListItemVM> Items
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00052008 File Offset: 0x00050208
		// (set) Token: 0x06001493 RID: 5267 RVA: 0x0005200B File Offset: 0x0005020B
		[DataSourceProperty]
		public virtual MBBindingList<EncyclopediaFilterGroupVM> FilterGroups
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0005200D File Offset: 0x0005020D
		// (set) Token: 0x06001495 RID: 5269 RVA: 0x00052010 File Offset: 0x00050210
		[DataSourceProperty]
		public virtual EncyclopediaListSortControllerVM SortController
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x04000969 RID: 2409
		private EncyclopediaPageArgs _args;

		// Token: 0x0400096A RID: 2410
		private bool _isLoadingOver;

		// Token: 0x0400096B RID: 2411
		private bool _isBookmarked;

		// Token: 0x0400096C RID: 2412
		private HintViewModel _bookmarkHint;
	}
}
