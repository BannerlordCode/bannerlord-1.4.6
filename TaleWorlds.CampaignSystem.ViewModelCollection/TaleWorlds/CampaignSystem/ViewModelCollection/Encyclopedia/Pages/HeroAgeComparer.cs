using System;
using System.Collections.Generic;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000CF RID: 207
	public class HeroAgeComparer : IComparer<HeroVM>
	{
		// Token: 0x060013C3 RID: 5059 RVA: 0x0004F859 File Offset: 0x0004DA59
		public HeroAgeComparer(bool isAscending)
		{
			this._isAscending = isAscending;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0004F868 File Offset: 0x0004DA68
		int IComparer<HeroVM>.Compare(HeroVM x, HeroVM y)
		{
			int num = x.Hero.Age.CompareTo(y.Hero.Age) * (this._isAscending ? 1 : (-1));
			if (num == 0)
			{
				num = x.NameText.CompareTo(y.NameText);
			}
			return num;
		}

		// Token: 0x0400090D RID: 2317
		private readonly bool _isAscending;
	}
}
