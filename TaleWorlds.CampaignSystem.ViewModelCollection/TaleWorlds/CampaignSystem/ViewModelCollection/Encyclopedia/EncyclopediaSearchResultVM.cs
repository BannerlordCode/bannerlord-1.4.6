using System;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia
{
	// Token: 0x020000CC RID: 204
	public class EncyclopediaSearchResultVM : ViewModel
	{
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0004E922 File Offset: 0x0004CB22
		// (set) Token: 0x0600137A RID: 4986 RVA: 0x0004E92A File Offset: 0x0004CB2A
		public string OrgNameText { get; private set; }

		// Token: 0x0600137B RID: 4987 RVA: 0x0004E934 File Offset: 0x0004CB34
		public EncyclopediaSearchResultVM(EncyclopediaListItem source, string searchedText, int matchStartIndex)
		{
			this.MatchStartIndex = matchStartIndex;
			this.LinkId = source.Id;
			this.PageType = source.TypeName;
			this.OrgNameText = source.Name;
			this._nameText = source.Name;
			this.UpdateSearchedText(searchedText);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0004E990 File Offset: 0x0004CB90
		public void UpdateSearchedText(string searchedText)
		{
			this._searchedText = searchedText;
			if (string.IsNullOrEmpty(this.OrgNameText))
			{
				return;
			}
			int num = this.OrgNameText.IndexOf(this._searchedText, StringComparison.InvariantCultureIgnoreCase);
			if (num < 0)
			{
				return;
			}
			int num2 = MBMath.ClampInt(this._searchedText.Length, 0, this.OrgNameText.Length - num);
			if (num2 == 0)
			{
				return;
			}
			string text = this.OrgNameText.Substring(num, num2);
			if (!string.IsNullOrEmpty(text))
			{
				this.NameText = this.OrgNameText.Replace(text, "<a>" + text + "</a>");
			}
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0004EA25 File Offset: 0x0004CC25
		public void Execute()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this.PageType, this.LinkId);
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x0004EA42 File Offset: 0x0004CC42
		// (set) Token: 0x0600137F RID: 4991 RVA: 0x0004EA4A File Offset: 0x0004CC4A
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (this._nameText != value)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x040008E7 RID: 2279
		private string _searchedText;

		// Token: 0x040008E9 RID: 2281
		public readonly int MatchStartIndex;

		// Token: 0x040008EA RID: 2282
		public string LinkId = "";

		// Token: 0x040008EB RID: 2283
		public string PageType;

		// Token: 0x040008EC RID: 2284
		public string _nameText;
	}
}
