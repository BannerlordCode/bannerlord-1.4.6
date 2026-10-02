using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.Encyclopedia.Pages
{
	// Token: 0x0200017E RID: 382
	[EncyclopediaModel(new Type[] { typeof(Clan) })]
	public class DefaultEncyclopediaClanPage : EncyclopediaPage
	{
		// Token: 0x06001B8D RID: 7053 RVA: 0x0008E14F File Offset: 0x0008C34F
		public DefaultEncyclopediaClanPage()
		{
			base.HomePageOrderIndex = 500;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x0008E162 File Offset: 0x0008C362
		protected override IEnumerable<EncyclopediaListItem> InitializeListItems()
		{
			using (IEnumerator<Clan> enumerator = Clan.NonBanditFactions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Clan clan = enumerator.Current;
					if (this.IsValidEncyclopediaItem(clan))
					{
						yield return new EncyclopediaListItem(clan, clan.Name.ToString(), "", clan.StringId, base.GetIdentifier(typeof(Clan)), true, delegate
						{
							InformationManager.ShowTooltip(typeof(Clan), new object[] { clan });
						});
					}
				}
			}
			IEnumerator<Clan> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x0008E174 File Offset: 0x0008C374
		protected override IEnumerable<EncyclopediaFilterGroup> InitializeFilterItems()
		{
			List<EncyclopediaFilterGroup> list = new List<EncyclopediaFilterGroup>();
			List<EncyclopediaFilterItem> list2 = new List<EncyclopediaFilterItem>();
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=QwpHoMJu}Minor", null), (object f) => ((IFaction)f).IsMinorFaction));
			list.Add(new EncyclopediaFilterGroup(list2, new TextObject("{=zMMqgxb1}Type", null)));
			List<EncyclopediaFilterItem> list3 = new List<EncyclopediaFilterItem>();
			list3.Add(new EncyclopediaFilterItem(new TextObject("{=lEHjxPTs}Ally", null), (object f) => DiplomacyHelper.IsSameFactionAndNotEliminated((IFaction)f, Hero.MainHero.MapFaction)));
			list3.Add(new EncyclopediaFilterItem(new TextObject("{=sPmQz21k}Enemy", null), (object f) => FactionManager.IsAtWarAgainstFaction((IFaction)f, Hero.MainHero.MapFaction) && !((IFaction)f).IsBanditFaction));
			list3.Add(new EncyclopediaFilterItem(new TextObject("{=3PzgpFGq}Neutral", null), (object f) => FactionManager.IsNeutralWithFaction((IFaction)f, Hero.MainHero.MapFaction)));
			list.Add(new EncyclopediaFilterGroup(list3, new TextObject("{=L7wn49Uz}Diplomacy", null)));
			List<EncyclopediaFilterItem> list4 = new List<EncyclopediaFilterItem>();
			list4.Add(new EncyclopediaFilterItem(new TextObject("{=SlubkZ1A}Eliminated", null), (object f) => ((IFaction)f).IsEliminated));
			list4.Add(new EncyclopediaFilterItem(new TextObject("{=YRbSBxqT}Active", null), (object f) => !((IFaction)f).IsEliminated));
			list.Add(new EncyclopediaFilterGroup(list4, new TextObject("{=DXczLzml}Status", null)));
			List<EncyclopediaFilterItem> list5 = new List<EncyclopediaFilterItem>();
			using (List<CultureObject>.Enumerator enumerator = (from x in Game.Current.ObjectManager.GetObjectTypeList<CultureObject>()
				where x.IsMainCulture
				select x into f
				orderby f.Name.ToString()
				select f).ToList<CultureObject>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CultureObject culture = enumerator.Current;
					if (culture.StringId != "neutral_culture" && !culture.IsBandit)
					{
						list5.Add(new EncyclopediaFilterItem(culture.Name, (object c) => ((IFaction)c).Culture == culture));
					}
				}
			}
			list.Add(new EncyclopediaFilterGroup(list5, GameTexts.FindText("str_culture", null)));
			return list;
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x0008E42C File Offset: 0x0008C62C
		protected override IEnumerable<EncyclopediaSortController> InitializeSortControllers()
		{
			return new List<EncyclopediaSortController>
			{
				new EncyclopediaSortController(new TextObject("{=qtII2HbK}Wealth", null), new DefaultEncyclopediaClanPage.EncyclopediaListClanWealthComparer()),
				new EncyclopediaSortController(new TextObject("{=cc1d7mkq}Tier", null), new DefaultEncyclopediaClanPage.EncyclopediaListClanTierComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_strength", null), new DefaultEncyclopediaClanPage.EncyclopediaListClanStrengthComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_fiefs", null), new DefaultEncyclopediaClanPage.EncyclopediaListClanFiefComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_members", null), new DefaultEncyclopediaClanPage.EncyclopediaListClanMemberComparer())
			};
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0008E4C5 File Offset: 0x0008C6C5
		public override string GetViewFullyQualifiedName()
		{
			return "EncyclopediaClanPage";
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x0008E4CC File Offset: 0x0008C6CC
		public override TextObject GetName()
		{
			return GameTexts.FindText("str_clans", null);
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0008E4D9 File Offset: 0x0008C6D9
		public override TextObject GetDescriptionText()
		{
			return GameTexts.FindText("str_clan_description", null);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x0008E4E6 File Offset: 0x0008C6E6
		public override string GetStringID()
		{
			return "EncyclopediaClan";
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x0008E4ED File Offset: 0x0008C6ED
		public override MBObjectBase GetObject(string typeName, string stringID)
		{
			return Campaign.Current.CampaignObjectManager.Find<Clan>(stringID);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x0008E4FF File Offset: 0x0008C6FF
		public override bool IsValidEncyclopediaItem(object o)
		{
			return o is IFaction;
		}

		// Token: 0x020005BF RID: 1471
		private class EncyclopediaListClanWealthComparer : DefaultEncyclopediaClanPage.EncyclopediaListClanComparer
		{
			// Token: 0x06004F0A RID: 20234 RVA: 0x00182A38 File Offset: 0x00180C38
			private string GetClanWealthStatusText(Clan _clan)
			{
				string text = string.Empty;
				if (_clan.Leader.Gold < 15000)
				{
					text = new TextObject("{=SixPXaNh}Very Poor", null).ToString();
				}
				else if (_clan.Leader.Gold < 45000)
				{
					text = new TextObject("{=poorWealthStatus}Poor", null).ToString();
				}
				else if (_clan.Leader.Gold < 135000)
				{
					text = new TextObject("{=averageWealthStatus}Average", null).ToString();
				}
				else if (_clan.Leader.Gold < 405000)
				{
					text = new TextObject("{=UbRqC0Yz}Rich", null).ToString();
				}
				else
				{
					text = new TextObject("{=oJmRg2ms}Very Rich", null).ToString();
				}
				return text;
			}

			// Token: 0x06004F0B RID: 20235 RVA: 0x00182AF4 File Offset: 0x00180CF4
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareClans(x, y, DefaultEncyclopediaClanPage.EncyclopediaListClanWealthComparer._comparison);
			}

			// Token: 0x06004F0C RID: 20236 RVA: 0x00182B04 File Offset: 0x00180D04
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Clan clan;
				if ((clan = item.Object as Clan) != null)
				{
					return this.GetClanWealthStatusText(clan);
				}
				Debug.FailedAssert("Unable to get the gold of a non-clan object.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaClanPage.cs", "GetComparedValueText", 157);
				return "";
			}

			// Token: 0x04001853 RID: 6227
			private static Func<Clan, Clan, int> _comparison = (Clan c1, Clan c2) => c1.Gold.CompareTo(c2.Gold);
		}

		// Token: 0x020005C0 RID: 1472
		private class EncyclopediaListClanTierComparer : DefaultEncyclopediaClanPage.EncyclopediaListClanComparer
		{
			// Token: 0x06004F0F RID: 20239 RVA: 0x00182B65 File Offset: 0x00180D65
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareClans(x, y, DefaultEncyclopediaClanPage.EncyclopediaListClanTierComparer._comparison);
			}

			// Token: 0x06004F10 RID: 20240 RVA: 0x00182B74 File Offset: 0x00180D74
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Clan clan;
				if ((clan = item.Object as Clan) != null)
				{
					return clan.Tier.ToString();
				}
				Debug.FailedAssert("Unable to get the tier of a non-clan object.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaClanPage.cs", "GetComparedValueText", 178);
				return "";
			}

			// Token: 0x04001854 RID: 6228
			private static Func<Clan, Clan, int> _comparison = (Clan c1, Clan c2) => c1.Tier.CompareTo(c2.Tier);
		}

		// Token: 0x020005C1 RID: 1473
		private class EncyclopediaListClanStrengthComparer : DefaultEncyclopediaClanPage.EncyclopediaListClanComparer
		{
			// Token: 0x06004F13 RID: 20243 RVA: 0x00182BDC File Offset: 0x00180DDC
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareClans(x, y, DefaultEncyclopediaClanPage.EncyclopediaListClanStrengthComparer._comparison);
			}

			// Token: 0x06004F14 RID: 20244 RVA: 0x00182BEC File Offset: 0x00180DEC
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Clan clan;
				if ((clan = item.Object as Clan) != null)
				{
					return ((int)clan.CurrentTotalStrength).ToString();
				}
				Debug.FailedAssert("Unable to get the strength of a non-clan object.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaClanPage.cs", "GetComparedValueText", 199);
				return "";
			}

			// Token: 0x04001855 RID: 6229
			private static Func<Clan, Clan, int> _comparison = (Clan c1, Clan c2) => c1.CurrentTotalStrength.CompareTo(c2.CurrentTotalStrength);
		}

		// Token: 0x020005C2 RID: 1474
		private class EncyclopediaListClanFiefComparer : DefaultEncyclopediaClanPage.EncyclopediaListClanComparer
		{
			// Token: 0x06004F17 RID: 20247 RVA: 0x00182C55 File Offset: 0x00180E55
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareClans(x, y, DefaultEncyclopediaClanPage.EncyclopediaListClanFiefComparer._comparison);
			}

			// Token: 0x06004F18 RID: 20248 RVA: 0x00182C64 File Offset: 0x00180E64
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Clan clan;
				if ((clan = item.Object as Clan) != null)
				{
					return clan.Fiefs.Count.ToString();
				}
				Debug.FailedAssert("Unable to get the fief count of a non-clan object.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaClanPage.cs", "GetComparedValueText", 220);
				return "";
			}

			// Token: 0x04001856 RID: 6230
			private static Func<Clan, Clan, int> _comparison = (Clan c1, Clan c2) => c1.Fiefs.Count.CompareTo(c2.Fiefs.Count);
		}

		// Token: 0x020005C3 RID: 1475
		private class EncyclopediaListClanMemberComparer : DefaultEncyclopediaClanPage.EncyclopediaListClanComparer
		{
			// Token: 0x06004F1B RID: 20251 RVA: 0x00182CD1 File Offset: 0x00180ED1
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareClans(x, y, DefaultEncyclopediaClanPage.EncyclopediaListClanMemberComparer._comparison);
			}

			// Token: 0x06004F1C RID: 20252 RVA: 0x00182CE0 File Offset: 0x00180EE0
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Clan clan;
				if ((clan = item.Object as Clan) != null)
				{
					return clan.Heroes.Count.ToString();
				}
				Debug.FailedAssert("Unable to get members of a non-clan object.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaClanPage.cs", "GetComparedValueText", 241);
				return "";
			}

			// Token: 0x04001857 RID: 6231
			private static Func<Clan, Clan, int> _comparison = (Clan c1, Clan c2) => c1.Heroes.Count.CompareTo(c2.Heroes.Count);
		}

		// Token: 0x020005C4 RID: 1476
		public abstract class EncyclopediaListClanComparer : EncyclopediaListItemComparerBase
		{
			// Token: 0x06004F1F RID: 20255 RVA: 0x00182D50 File Offset: 0x00180F50
			public int CompareClans(EncyclopediaListItem x, EncyclopediaListItem y, Func<Clan, Clan, int> comparison)
			{
				Clan clan;
				Clan clan2;
				if ((clan = x.Object as Clan) == null || (clan2 = y.Object as Clan) == null)
				{
					Debug.FailedAssert("Both objects should be clans.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaClanPage.cs", "CompareClans", 256);
					return 0;
				}
				int num = comparison(clan, clan2) * (base.IsAscending ? 1 : (-1));
				if (num == 0)
				{
					return base.ResolveEquality(x, y);
				}
				return num;
			}
		}
	}
}
