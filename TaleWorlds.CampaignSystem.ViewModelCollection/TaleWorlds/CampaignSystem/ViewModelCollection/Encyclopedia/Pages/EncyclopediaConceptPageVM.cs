using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000D0 RID: 208
	[EncyclopediaViewModel(typeof(Concept))]
	public class EncyclopediaConceptPageVM : EncyclopediaContentPageVM
	{
		// Token: 0x060013C5 RID: 5061 RVA: 0x0004F8B8 File Offset: 0x0004DAB8
		public EncyclopediaConceptPageVM(EncyclopediaPageArgs args)
			: base(args)
		{
			this._concept = base.Obj as Concept;
			Concept.SetConceptTextLinks();
			base.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(this._concept);
			this.RefreshValues();
			this.Refresh();
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0004F90E File Offset: 0x0004DB0E
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TitleText = this._concept.Title.ToString();
			this.DescriptionText = this._concept.Description.ToString();
			base.UpdateBookmarkHintText();
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0004F948 File Offset: 0x0004DB48
		public override void Refresh()
		{
			base.IsLoadingOver = false;
			base.IsLoadingOver = true;
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0004F958 File Offset: 0x0004DB58
		public override string GetName()
		{
			return this._concept.Title.ToString();
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0004F96A File Offset: 0x0004DB6A
		public void ExecuteLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0004F97C File Offset: 0x0004DB7C
		public override string GetNavigationBarURL()
		{
			return HyperlinkTexts.GetGenericHyperlinkText("Home", GameTexts.FindText("str_encyclopedia_home", null).ToString()) + " \\ " + HyperlinkTexts.GetGenericHyperlinkText("ListPage-Concept", GameTexts.FindText("str_encyclopedia_concepts", null).ToString()) + " \\ " + this.GetName();
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0004F9E4 File Offset: 0x0004DBE4
		public override void ExecuteSwitchBookmarkedState()
		{
			base.ExecuteSwitchBookmarkedState();
			if (base.IsBookmarked)
			{
				Campaign.Current.EncyclopediaManager.ViewDataTracker.AddEncyclopediaBookmarkToItem(this._concept);
				return;
			}
			Campaign.Current.EncyclopediaManager.ViewDataTracker.RemoveEncyclopediaBookmarkFromItem(this._concept);
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x0004FA34 File Offset: 0x0004DC34
		// (set) Token: 0x060013CD RID: 5069 RVA: 0x0004FA3C File Offset: 0x0004DC3C
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x0004FA5F File Offset: 0x0004DC5F
		// (set) Token: 0x060013CF RID: 5071 RVA: 0x0004FA67 File Offset: 0x0004DC67
		[DataSourceProperty]
		public string DescriptionText
		{
			get
			{
				return this._descriptionText;
			}
			set
			{
				if (value != this._descriptionText)
				{
					this._descriptionText = value;
					base.OnPropertyChangedWithValue<string>(value, "DescriptionText");
				}
			}
		}

		// Token: 0x0400090E RID: 2318
		private Concept _concept;

		// Token: 0x0400090F RID: 2319
		private string _titleText;

		// Token: 0x04000910 RID: 2320
		private string _descriptionText;
	}
}
