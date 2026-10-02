using System;
using System.Collections.Generic;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000D4 RID: 212
	public class HeroRelationComparer : IComparer<HeroVM>
	{
		// Token: 0x0600147F RID: 5247 RVA: 0x00051E66 File Offset: 0x00050066
		public HeroRelationComparer(Hero pageHero, bool isAscending, bool showLeadersFirst)
		{
			this._pageHero = pageHero;
			this._isAscending = isAscending;
			this._showLeadersFirst = showLeadersFirst;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00051E84 File Offset: 0x00050084
		int IComparer<HeroVM>.Compare(HeroVM x, HeroVM y)
		{
			int num;
			if (this._showLeadersFirst)
			{
				num = y.IsKingdomLeader.CompareTo(x.IsKingdomLeader);
				if (num != 0)
				{
					return num;
				}
			}
			int relation = this._pageHero.GetRelation(x.Hero);
			int relation2 = this._pageHero.GetRelation(y.Hero);
			num = relation.CompareTo(relation2) * (this._isAscending ? 1 : (-1));
			if (num == 0)
			{
				num = x.NameText.CompareTo(y.NameText);
			}
			return num;
		}

		// Token: 0x04000965 RID: 2405
		private readonly Hero _pageHero;

		// Token: 0x04000966 RID: 2406
		private readonly bool _isAscending;

		// Token: 0x04000967 RID: 2407
		private readonly bool _showLeadersFirst;
	}
}
