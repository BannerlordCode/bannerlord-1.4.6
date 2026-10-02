using System;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia
{
	// Token: 0x020000CA RID: 202
	public class EncyclopediaLinkVM : ViewModel
	{
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0004DDC4 File Offset: 0x0004BFC4
		// (set) Token: 0x06001341 RID: 4929 RVA: 0x0004DDCC File Offset: 0x0004BFCC
		[DataSourceProperty]
		public string ActiveLink
		{
			get
			{
				return this._activeLink;
			}
			set
			{
				if (this._activeLink != value)
				{
					this._activeLink = value;
					base.OnPropertyChangedWithValue<string>(value, "ActiveLink");
				}
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0004DDEF File Offset: 0x0004BFEF
		public void ExecuteActiveLink()
		{
			if (!string.IsNullOrEmpty(this.ActiveLink))
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this.ActiveLink);
			}
		}

		// Token: 0x040008D2 RID: 2258
		private string _activeLink;
	}
}
