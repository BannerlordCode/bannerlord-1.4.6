using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Encyclopedia.Pages
{
	// Token: 0x02000182 RID: 386
	[EncyclopediaModel(new Type[] { typeof(Settlement) })]
	public class DefaultEncyclopediaSettlementPage : EncyclopediaPage
	{
		// Token: 0x06001BB5 RID: 7093 RVA: 0x0008EE31 File Offset: 0x0008D031
		public DefaultEncyclopediaSettlementPage()
		{
			base.HomePageOrderIndex = 100;
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0008EE41 File Offset: 0x0008D041
		protected override IEnumerable<EncyclopediaListItem> InitializeListItems()
		{
			using (List<Settlement>.Enumerator enumerator = Settlement.All.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Settlement settlement = enumerator.Current;
					if (this.IsValidEncyclopediaItem(settlement))
					{
						yield return new EncyclopediaListItem(settlement, settlement.Name.ToString(), "", settlement.StringId, base.GetIdentifier(typeof(Settlement)), DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(settlement), delegate
						{
							InformationManager.ShowTooltip(typeof(Settlement), new object[] { settlement });
						});
					}
				}
			}
			List<Settlement>.Enumerator enumerator = default(List<Settlement>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0008EE54 File Offset: 0x0008D054
		protected override IEnumerable<EncyclopediaFilterGroup> InitializeFilterItems()
		{
			List<EncyclopediaFilterGroup> list = new List<EncyclopediaFilterGroup>();
			List<EncyclopediaFilterItem> list2 = new List<EncyclopediaFilterItem>();
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=bOTQ7Pta}Town", null), (object s) => ((Settlement)s).IsTown));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=sVXa3zFx}Castle", null), (object s) => ((Settlement)s).IsCastle));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=Ua6CNLBZ}Village", null), (object s) => ((Settlement)s).IsVillage));
			List<EncyclopediaFilterItem> list3 = list2;
			list.Add(new EncyclopediaFilterGroup(list3, new TextObject("{=zMMqgxb1}Type", null)));
			List<EncyclopediaFilterItem> list4 = new List<EncyclopediaFilterItem>();
			using (List<CultureObject>.Enumerator enumerator = (from x in Game.Current.ObjectManager.GetObjectTypeList<CultureObject>()
				where x.IsMainCulture
				select x into f
				orderby f.Name.ToString()
				select f).ToList<CultureObject>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CultureObject culture = enumerator.Current;
					if (culture.StringId != "neutral_culture" && culture.CanHaveSettlement)
					{
						list4.Add(new EncyclopediaFilterItem(culture.Name, (object c) => ((Settlement)c).Culture == culture));
					}
				}
			}
			list.Add(new EncyclopediaFilterGroup(list4, GameTexts.FindText("str_culture", null)));
			return list;
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x0008F030 File Offset: 0x0008D230
		protected override IEnumerable<EncyclopediaSortController> InitializeSortControllers()
		{
			return new List<EncyclopediaSortController>
			{
				new EncyclopediaSortController(GameTexts.FindText("str_garrison", null), new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementGarrisonComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_food", null), new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementFoodComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_security", null), new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementSecurityComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_loyalty", null), new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementLoyaltyComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_militia", null), new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementMilitiaComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_prosperity", null), new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementProsperityComparer())
			};
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0008F0E4 File Offset: 0x0008D2E4
		public override string GetViewFullyQualifiedName()
		{
			return "EncyclopediaSettlementPage";
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0008F0EB File Offset: 0x0008D2EB
		public override TextObject GetName()
		{
			return GameTexts.FindText("str_settlements", null);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0008F0F8 File Offset: 0x0008D2F8
		public override TextObject GetDescriptionText()
		{
			return GameTexts.FindText("str_settlement_description", null);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0008F105 File Offset: 0x0008D305
		public override string GetStringID()
		{
			return "EncyclopediaSettlement";
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0008F10C File Offset: 0x0008D30C
		public override bool IsValidEncyclopediaItem(object o)
		{
			Settlement settlement = o as Settlement;
			return settlement != null && (settlement.IsFortification || settlement.IsVillage);
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x0008F135 File Offset: 0x0008D335
		private static bool CanPlayerSeeValuesOf(Settlement settlement)
		{
			return Campaign.Current.Models.InformationRestrictionModel.DoesPlayerKnowDetailsOf(settlement);
		}

		// Token: 0x020005DA RID: 1498
		private class EncyclopediaListSettlementGarrisonComparer : DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer
		{
			// Token: 0x06004F94 RID: 20372 RVA: 0x00184028 File Offset: 0x00182228
			private static int GarrisonComparison(Town t1, Town t2)
			{
				int num = ((t1.GarrisonParty != null) ? t1.GarrisonParty.MemberRoster.TotalManCount : 0);
				int num2 = ((t2.GarrisonParty != null) ? t2.GarrisonParty.MemberRoster.TotalManCount : 0);
				return num.CompareTo(num2);
			}

			// Token: 0x06004F95 RID: 20373 RVA: 0x00184078 File Offset: 0x00182278
			protected override bool CompareVisibility(Settlement s1, Settlement s2, out int comparisonResult)
			{
				if (s1.IsTown && s2.IsTown)
				{
					if (s1.Town.GarrisonParty == null && s2.Town.GarrisonParty == null)
					{
						comparisonResult = 0;
						return true;
					}
					if (s1.Town.GarrisonParty == null)
					{
						comparisonResult = (base.IsAscending ? 2 : (-2));
						return true;
					}
					if (s2.Town.GarrisonParty == null)
					{
						comparisonResult = (base.IsAscending ? (-2) : 2);
						return true;
					}
				}
				return base.CompareVisibility(s1, s2, out comparisonResult);
			}

			// Token: 0x06004F96 RID: 20374 RVA: 0x001840F9 File Offset: 0x001822F9
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareFiefs(x, y, new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer.SettlementVisibilityComparerDelegate(this.CompareVisibility), new Func<Town, Town, int>(DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementGarrisonComparer.GarrisonComparison));
			}

			// Token: 0x06004F97 RID: 20375 RVA: 0x0018411C File Offset: 0x0018231C
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Settlement settlement;
				if ((settlement = item.Object as Settlement) == null)
				{
					Debug.FailedAssert("Unable to get the garrison of a non-settlement object.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaSettlementPage.cs", "GetComparedValueText", 163);
					return "";
				}
				if (settlement.IsVillage)
				{
					return this._emptyValue.ToString();
				}
				if (!DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(settlement))
				{
					return this._missingValue.ToString();
				}
				MobileParty garrisonParty = settlement.Town.GarrisonParty;
				return ((garrisonParty != null) ? garrisonParty.MemberRoster.TotalManCount.ToString() : null) ?? 0.ToString();
			}
		}

		// Token: 0x020005DB RID: 1499
		private class EncyclopediaListSettlementFoodComparer : DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer
		{
			// Token: 0x06004F99 RID: 20377 RVA: 0x001841B8 File Offset: 0x001823B8
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareFiefs(x, y, new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer.SettlementVisibilityComparerDelegate(this.CompareVisibility), new Func<Town, Town, int>(DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementFoodComparer.FoodComparison));
			}

			// Token: 0x06004F9A RID: 20378 RVA: 0x001841DC File Offset: 0x001823DC
			private static int FoodComparison(Town t1, Town t2)
			{
				return t1.FoodStocks.CompareTo(t2.FoodStocks);
			}

			// Token: 0x06004F9B RID: 20379 RVA: 0x00184200 File Offset: 0x00182400
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Settlement settlement;
				if ((settlement = item.Object as Settlement) == null)
				{
					Debug.FailedAssert("Unable to get the food stocks of a non-settlement object.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaSettlementPage.cs", "GetComparedValueText", 198);
					return "";
				}
				if (settlement.IsVillage)
				{
					return this._emptyValue.ToString();
				}
				if (!DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(settlement))
				{
					return this._missingValue.ToString();
				}
				return ((int)settlement.Town.FoodStocks).ToString();
			}
		}

		// Token: 0x020005DC RID: 1500
		private class EncyclopediaListSettlementSecurityComparer : DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer
		{
			// Token: 0x06004F9D RID: 20381 RVA: 0x0018427F File Offset: 0x0018247F
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareFiefs(x, y, new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer.SettlementVisibilityComparerDelegate(this.CompareVisibility), new Func<Town, Town, int>(DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementSecurityComparer.SecurityComparison));
			}

			// Token: 0x06004F9E RID: 20382 RVA: 0x001842A4 File Offset: 0x001824A4
			private static int SecurityComparison(Town t1, Town t2)
			{
				return t1.Security.CompareTo(t2.Security);
			}

			// Token: 0x06004F9F RID: 20383 RVA: 0x001842C8 File Offset: 0x001824C8
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Settlement settlement;
				if ((settlement = item.Object as Settlement) == null)
				{
					Debug.FailedAssert("Unable to get the security of a non-settlement object.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaSettlementPage.cs", "GetComparedValueText", 233);
					return "";
				}
				if (settlement.IsVillage)
				{
					return this._emptyValue.ToString();
				}
				if (!DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(settlement))
				{
					return this._missingValue.ToString();
				}
				return ((int)settlement.Town.Security).ToString();
			}
		}

		// Token: 0x020005DD RID: 1501
		private class EncyclopediaListSettlementLoyaltyComparer : DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer
		{
			// Token: 0x06004FA1 RID: 20385 RVA: 0x00184347 File Offset: 0x00182547
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareFiefs(x, y, new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer.SettlementVisibilityComparerDelegate(this.CompareVisibility), new Func<Town, Town, int>(DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementLoyaltyComparer.LoyaltyComparison));
			}

			// Token: 0x06004FA2 RID: 20386 RVA: 0x0018436C File Offset: 0x0018256C
			private static int LoyaltyComparison(Town t1, Town t2)
			{
				return t1.Loyalty.CompareTo(t2.Loyalty);
			}

			// Token: 0x06004FA3 RID: 20387 RVA: 0x00184390 File Offset: 0x00182590
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Settlement settlement;
				if ((settlement = item.Object as Settlement) == null)
				{
					Debug.FailedAssert("Unable to get the loyalty of a non-settlement object.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaSettlementPage.cs", "GetComparedValueText", 268);
					return "";
				}
				if (settlement.IsVillage)
				{
					return this._emptyValue.ToString();
				}
				if (!DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(settlement))
				{
					return this._missingValue.ToString();
				}
				return ((int)settlement.Town.Loyalty).ToString();
			}
		}

		// Token: 0x020005DE RID: 1502
		private class EncyclopediaListSettlementMilitiaComparer : DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer
		{
			// Token: 0x06004FA5 RID: 20389 RVA: 0x0018440F File Offset: 0x0018260F
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareSettlements(x, y, new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer.SettlementVisibilityComparerDelegate(this.CompareVisibility), new Func<Settlement, Settlement, int>(DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementMilitiaComparer.MilitiaComparison));
			}

			// Token: 0x06004FA6 RID: 20390 RVA: 0x00184434 File Offset: 0x00182634
			private static int MilitiaComparison(Settlement t1, Settlement t2)
			{
				return t1.Militia.CompareTo(t2.Militia);
			}

			// Token: 0x06004FA7 RID: 20391 RVA: 0x00184458 File Offset: 0x00182658
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Settlement settlement;
				if ((settlement = item.Object as Settlement) == null)
				{
					Debug.FailedAssert("Unable to get the militia of a non-settlement object.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaSettlementPage.cs", "GetComparedValueText", 299);
					return "";
				}
				if (!DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(settlement))
				{
					return this._missingValue.ToString();
				}
				return ((int)settlement.Militia).ToString();
			}
		}

		// Token: 0x020005DF RID: 1503
		private class EncyclopediaListSettlementProsperityComparer : DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer
		{
			// Token: 0x06004FA9 RID: 20393 RVA: 0x001844BE File Offset: 0x001826BE
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareFiefs(x, y, new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer.SettlementVisibilityComparerDelegate(this.CompareVisibility), new Func<Town, Town, int>(DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementProsperityComparer.ProsperityComparison));
			}

			// Token: 0x06004FAA RID: 20394 RVA: 0x001844E4 File Offset: 0x001826E4
			private static int ProsperityComparison(Town t1, Town t2)
			{
				return t1.Prosperity.CompareTo(t2.Prosperity);
			}

			// Token: 0x06004FAB RID: 20395 RVA: 0x00184508 File Offset: 0x00182708
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Settlement settlement;
				if ((settlement = item.Object as Settlement) == null)
				{
					Debug.FailedAssert("Unable to get the prosperity of a non-settlement object.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaSettlementPage.cs", "GetComparedValueText", 334);
					return "";
				}
				if (settlement.IsVillage)
				{
					return this._emptyValue.ToString();
				}
				if (!DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(settlement))
				{
					return this._missingValue.ToString();
				}
				return ((int)settlement.Town.Prosperity).ToString();
			}
		}

		// Token: 0x020005E0 RID: 1504
		public abstract class EncyclopediaListSettlementComparer : EncyclopediaListItemComparerBase
		{
			// Token: 0x06004FAD RID: 20397 RVA: 0x00184588 File Offset: 0x00182788
			protected virtual bool CompareVisibility(Settlement s1, Settlement s2, out int comparisonResult)
			{
				bool flag = DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(s1);
				bool flag2 = DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(s2);
				if (!flag && !flag2)
				{
					comparisonResult = 0;
					return true;
				}
				if (!flag)
				{
					comparisonResult = (base.IsAscending ? 1 : (-1));
					return true;
				}
				if (!flag2)
				{
					comparisonResult = (base.IsAscending ? (-1) : 1);
					return true;
				}
				comparisonResult = 0;
				return false;
			}

			// Token: 0x06004FAE RID: 20398 RVA: 0x001845D8 File Offset: 0x001827D8
			protected int CompareSettlements(EncyclopediaListItem x, EncyclopediaListItem y, DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer.SettlementVisibilityComparerDelegate visibilityComparison, Func<Settlement, Settlement, int> comparison)
			{
				Settlement settlement;
				Settlement settlement2;
				if ((settlement = x.Object as Settlement) == null || (settlement2 = y.Object as Settlement) == null)
				{
					Debug.FailedAssert("Both objects should be settlements.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaSettlementPage.cs", "CompareSettlements", 383);
					return 0;
				}
				int num;
				if (visibilityComparison(settlement, settlement2, out num))
				{
					if (num == 0)
					{
						return base.ResolveEquality(x, y);
					}
					return num * (base.IsAscending ? 1 : (-1));
				}
				else
				{
					int num2 = comparison(settlement, settlement2) * (base.IsAscending ? 1 : (-1));
					if (num2 == 0)
					{
						return base.ResolveEquality(x, y);
					}
					return num2;
				}
			}

			// Token: 0x06004FAF RID: 20399 RVA: 0x0018466C File Offset: 0x0018286C
			protected int CompareFiefs(EncyclopediaListItem x, EncyclopediaListItem y, DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer.SettlementVisibilityComparerDelegate visibilityComparison, Func<Town, Town, int> comparison)
			{
				Settlement settlement;
				Settlement settlement2;
				if ((settlement = x.Object as Settlement) == null || (settlement2 = y.Object as Settlement) == null)
				{
					Debug.FailedAssert("Unable to compare loyalty of non-fief (castle or town) objects.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaSettlementPage.cs", "CompareFiefs", 407);
					return 0;
				}
				int num = settlement.IsVillage.CompareTo(settlement2.IsVillage);
				if (num != 0)
				{
					return num;
				}
				if (settlement.IsVillage && settlement2.IsVillage)
				{
					return base.ResolveEquality(x, y);
				}
				int num2;
				if (visibilityComparison(settlement, settlement2, out num2))
				{
					if (num2 == 0)
					{
						return base.ResolveEquality(x, y);
					}
					return num2 * (base.IsAscending ? 1 : (-1));
				}
				else
				{
					num = comparison(settlement.Town, settlement2.Town) * (base.IsAscending ? 1 : (-1));
					if (num == 0)
					{
						return base.ResolveEquality(x, y);
					}
					return num;
				}
			}

			// Token: 0x020008BE RID: 2238
			// (Invoke) Token: 0x0600690D RID: 26893
			protected delegate bool SettlementVisibilityComparerDelegate(Settlement s1, Settlement s2, out int comparisonResult);
		}
	}
}
