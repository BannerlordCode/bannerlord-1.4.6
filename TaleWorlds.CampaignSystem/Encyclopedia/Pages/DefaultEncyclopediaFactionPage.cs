using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.Encyclopedia.Pages
{
	// Token: 0x02000180 RID: 384
	[EncyclopediaModel(new Type[] { typeof(Kingdom) })]
	public class DefaultEncyclopediaFactionPage : EncyclopediaPage
	{
		// Token: 0x06001BA0 RID: 7072 RVA: 0x0008E76B File Offset: 0x0008C96B
		public DefaultEncyclopediaFactionPage()
		{
			base.HomePageOrderIndex = 400;
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0008E77E File Offset: 0x0008C97E
		public override string GetViewFullyQualifiedName()
		{
			return "EncyclopediaFactionPage";
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0008E785 File Offset: 0x0008C985
		public override TextObject GetName()
		{
			return GameTexts.FindText("str_kingdoms_group", null);
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0008E792 File Offset: 0x0008C992
		public override TextObject GetDescriptionText()
		{
			return GameTexts.FindText("str_faction_description", null);
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0008E79F File Offset: 0x0008C99F
		public override string GetStringID()
		{
			return "EncyclopediaKingdom";
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0008E7A6 File Offset: 0x0008C9A6
		public override MBObjectBase GetObject(string typeName, string stringID)
		{
			return Campaign.Current.CampaignObjectManager.Find<Kingdom>(stringID);
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0008E7B8 File Offset: 0x0008C9B8
		public override bool IsValidEncyclopediaItem(object o)
		{
			IFaction faction = o as IFaction;
			return faction != null && !faction.IsBanditFaction;
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0008E7DA File Offset: 0x0008C9DA
		protected override IEnumerable<EncyclopediaListItem> InitializeListItems()
		{
			using (List<Kingdom>.Enumerator enumerator = Kingdom.All.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Kingdom kingdom = enumerator.Current;
					if (this.IsValidEncyclopediaItem(kingdom))
					{
						yield return new EncyclopediaListItem(kingdom, kingdom.Name.ToString(), "", kingdom.StringId, base.GetIdentifier(typeof(Kingdom)), true, delegate
						{
							InformationManager.ShowTooltip(typeof(Kingdom), new object[] { kingdom });
						});
					}
				}
			}
			List<Kingdom>.Enumerator enumerator = default(List<Kingdom>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0008E7EC File Offset: 0x0008C9EC
		protected override IEnumerable<EncyclopediaFilterGroup> InitializeFilterItems()
		{
			List<EncyclopediaFilterGroup> list = new List<EncyclopediaFilterGroup>();
			List<EncyclopediaFilterItem> list2 = new List<EncyclopediaFilterItem>();
			list2 = new List<EncyclopediaFilterItem>();
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=lEHjxPTs}Ally", null), (object f) => DiplomacyHelper.IsSameFactionAndNotEliminated((IFaction)f, Hero.MainHero.MapFaction)));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=sPmQz21k}Enemy", null), (object f) => FactionManager.IsAtWarAgainstFaction((IFaction)f, Hero.MainHero.MapFaction) && !((IFaction)f).IsBanditFaction));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=3PzgpFGq}Neutral", null), (object f) => FactionManager.IsNeutralWithFaction((IFaction)f, Hero.MainHero.MapFaction)));
			list.Add(new EncyclopediaFilterGroup(list2, new TextObject("{=L7wn49Uz}Diplomacy", null)));
			return list;
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x0008E8C0 File Offset: 0x0008CAC0
		protected override IEnumerable<EncyclopediaSortController> InitializeSortControllers()
		{
			return new List<EncyclopediaSortController>
			{
				new EncyclopediaSortController(GameTexts.FindText("str_total_strength", null), new DefaultEncyclopediaFactionPage.EncyclopediaListKingdomTotalStrengthComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_fiefs", null), new DefaultEncyclopediaFactionPage.EncyclopediaListKingdomFiefsComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_clans", null), new DefaultEncyclopediaFactionPage.EncyclopediaListKingdomClanComparer())
			};
		}

		// Token: 0x020005CB RID: 1483
		private class EncyclopediaListKingdomTotalStrengthComparer : DefaultEncyclopediaFactionPage.EncyclopediaListKingdomComparer
		{
			// Token: 0x06004F4B RID: 20299 RVA: 0x001832EF File Offset: 0x001814EF
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareKingdoms(x, y, DefaultEncyclopediaFactionPage.EncyclopediaListKingdomTotalStrengthComparer._comparison);
			}

			// Token: 0x06004F4C RID: 20300 RVA: 0x00183300 File Offset: 0x00181500
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Kingdom kingdom;
				if ((kingdom = item.Object as Kingdom) != null)
				{
					return ((int)kingdom.CurrentTotalStrength).ToString();
				}
				Debug.FailedAssert("Unable to get the total strength of a non-kingdom object.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaFactionPage.cs", "GetComparedValueText", 107);
				return "";
			}

			// Token: 0x04001876 RID: 6262
			private static Func<Kingdom, Kingdom, int> _comparison = (Kingdom k1, Kingdom k2) => k1.CurrentTotalStrength.CompareTo(k2.CurrentTotalStrength);
		}

		// Token: 0x020005CC RID: 1484
		private class EncyclopediaListKingdomFiefsComparer : DefaultEncyclopediaFactionPage.EncyclopediaListKingdomComparer
		{
			// Token: 0x06004F4F RID: 20303 RVA: 0x00183366 File Offset: 0x00181566
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareKingdoms(x, y, DefaultEncyclopediaFactionPage.EncyclopediaListKingdomFiefsComparer._comparison);
			}

			// Token: 0x06004F50 RID: 20304 RVA: 0x00183378 File Offset: 0x00181578
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Kingdom kingdom;
				if ((kingdom = item.Object as Kingdom) != null)
				{
					return kingdom.Fiefs.Count.ToString();
				}
				Debug.FailedAssert("Unable to get the fief count from a non-kingdom object.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaFactionPage.cs", "GetComparedValueText", 128);
				return "";
			}

			// Token: 0x04001877 RID: 6263
			private static Func<Kingdom, Kingdom, int> _comparison = (Kingdom k1, Kingdom k2) => k1.Fiefs.Count.CompareTo(k2.Fiefs.Count);
		}

		// Token: 0x020005CD RID: 1485
		private class EncyclopediaListKingdomClanComparer : DefaultEncyclopediaFactionPage.EncyclopediaListKingdomComparer
		{
			// Token: 0x06004F53 RID: 20307 RVA: 0x001833E5 File Offset: 0x001815E5
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareKingdoms(x, y, DefaultEncyclopediaFactionPage.EncyclopediaListKingdomClanComparer._comparison);
			}

			// Token: 0x06004F54 RID: 20308 RVA: 0x001833F4 File Offset: 0x001815F4
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Kingdom kingdom;
				if ((kingdom = item.Object as Kingdom) != null)
				{
					return kingdom.Clans.Count.ToString();
				}
				Debug.FailedAssert("Unable to get the clan count from a non-kingdom object.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaFactionPage.cs", "GetComparedValueText", 149);
				return "";
			}

			// Token: 0x04001878 RID: 6264
			private static Func<Kingdom, Kingdom, int> _comparison = (Kingdom k1, Kingdom k2) => k1.Clans.Count.CompareTo(k2.Clans.Count);
		}

		// Token: 0x020005CE RID: 1486
		public abstract class EncyclopediaListKingdomComparer : EncyclopediaListItemComparerBase
		{
			// Token: 0x06004F57 RID: 20311 RVA: 0x00183464 File Offset: 0x00181664
			public int CompareKingdoms(EncyclopediaListItem x, EncyclopediaListItem y, Func<Kingdom, Kingdom, int> comparison)
			{
				Kingdom kingdom;
				Kingdom kingdom2;
				if ((kingdom = x.Object as Kingdom) == null || (kingdom2 = y.Object as Kingdom) == null)
				{
					Debug.FailedAssert("Both objects should be kingdoms.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaFactionPage.cs", "CompareKingdoms", 164);
					return 0;
				}
				int num = comparison(kingdom, kingdom2) * (base.IsAscending ? 1 : (-1));
				if (num == 0)
				{
					return base.ResolveEquality(x, y);
				}
				return num;
			}
		}
	}
}
