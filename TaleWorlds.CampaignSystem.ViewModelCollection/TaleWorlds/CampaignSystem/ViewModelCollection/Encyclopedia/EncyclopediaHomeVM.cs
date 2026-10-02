using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia
{
	// Token: 0x020000C9 RID: 201
	public class EncyclopediaHomeVM : EncyclopediaPageVM
	{
		// Token: 0x06001335 RID: 4917 RVA: 0x0004DC0C File Offset: 0x0004BE0C
		public EncyclopediaHomeVM(EncyclopediaPageArgs args)
			: base(args)
		{
			this.Lists = new MBBindingList<ListTypeVM>();
			foreach (EncyclopediaPage encyclopediaPage in from p in Campaign.Current.EncyclopediaManager.GetEncyclopediaPages()
				orderby p.HomePageOrderIndex
				select p)
			{
				if (encyclopediaPage.IsRelevant())
				{
					this.Lists.Add(new ListTypeVM(encyclopediaPage));
				}
			}
			this.RefreshValues();
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0004DCB0 File Offset: 0x0004BEB0
		public override void Refresh()
		{
			base.Refresh();
			this.RefreshValues();
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0004DCC0 File Offset: 0x0004BEC0
		public override void RefreshValues()
		{
			base.RefreshValues();
			this._baseName = GameTexts.FindText("str_encyclopedia_name", null).ToString();
			this.HomeTitleText = GameTexts.FindText("str_encyclopedia_name", null).ToString();
			this.Lists.ApplyActionOnAllItems(delegate(ListTypeVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0004DD29 File Offset: 0x0004BF29
		public override string GetNavigationBarURL()
		{
			return GameTexts.FindText("str_encyclopedia_home", null).ToString() + " \\";
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0004DD45 File Offset: 0x0004BF45
		public override string GetName()
		{
			return this._baseName;
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x0004DD4D File Offset: 0x0004BF4D
		// (set) Token: 0x0600133B RID: 4923 RVA: 0x0004DD55 File Offset: 0x0004BF55
		[DataSourceProperty]
		public bool IsListActive
		{
			get
			{
				return this._isListActive;
			}
			set
			{
				if (value != this._isListActive)
				{
					this._isListActive = value;
					base.OnPropertyChangedWithValue(value, "IsListActive");
				}
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0004DD73 File Offset: 0x0004BF73
		// (set) Token: 0x0600133D RID: 4925 RVA: 0x0004DD7B File Offset: 0x0004BF7B
		[DataSourceProperty]
		public string HomeTitleText
		{
			get
			{
				return this._homeTitleText;
			}
			set
			{
				if (value != this._homeTitleText)
				{
					this._homeTitleText = value;
					base.OnPropertyChangedWithValue<string>(value, "HomeTitleText");
				}
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x0004DD9E File Offset: 0x0004BF9E
		// (set) Token: 0x0600133F RID: 4927 RVA: 0x0004DDA6 File Offset: 0x0004BFA6
		[DataSourceProperty]
		public MBBindingList<ListTypeVM> Lists
		{
			get
			{
				return this._lists;
			}
			set
			{
				if (value != this._lists)
				{
					this._lists = value;
					base.OnPropertyChangedWithValue<MBBindingList<ListTypeVM>>(value, "Lists");
				}
			}
		}

		// Token: 0x040008CE RID: 2254
		private string _baseName;

		// Token: 0x040008CF RID: 2255
		private MBBindingList<ListTypeVM> _lists;

		// Token: 0x040008D0 RID: 2256
		private bool _isListActive;

		// Token: 0x040008D1 RID: 2257
		private string _homeTitleText;
	}
}
