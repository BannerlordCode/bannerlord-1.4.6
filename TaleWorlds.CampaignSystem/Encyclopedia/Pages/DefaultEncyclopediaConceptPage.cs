using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Encyclopedia.Pages
{
	// Token: 0x0200017F RID: 383
	[EncyclopediaModel(new Type[] { typeof(Concept) })]
	public class DefaultEncyclopediaConceptPage : EncyclopediaPage
	{
		// Token: 0x06001B97 RID: 7063 RVA: 0x0008E50A File Offset: 0x0008C70A
		public DefaultEncyclopediaConceptPage()
		{
			base.HomePageOrderIndex = 600;
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x0008E51D File Offset: 0x0008C71D
		protected override IEnumerable<EncyclopediaListItem> InitializeListItems()
		{
			foreach (Concept concept in Concept.All)
			{
				yield return new EncyclopediaListItem(concept, concept.Title.ToString(), concept.Description.ToString(), concept.StringId, base.GetIdentifier(typeof(Concept)), true, null);
			}
			List<Concept>.Enumerator enumerator = default(List<Concept>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x0008E530 File Offset: 0x0008C730
		protected override IEnumerable<EncyclopediaFilterGroup> InitializeFilterItems()
		{
			List<EncyclopediaFilterGroup> list = new List<EncyclopediaFilterGroup>();
			List<EncyclopediaFilterItem> list2 = new List<EncyclopediaFilterItem>();
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=uauMia0D} Characters", null), (object c) => Concept.IsGroupMember("Characters", (Concept)c)));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=cwRkqIt4} Kingdoms", null), (object c) => Concept.IsGroupMember("Kingdoms", (Concept)c)));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=x6knoNnC} Clans", null), (object c) => Concept.IsGroupMember("Clans", (Concept)c)));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=GYzkb4iB} Parties", null), (object c) => Concept.IsGroupMember("Parties", (Concept)c)));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=u6GM5Spa} Armies", null), (object c) => Concept.IsGroupMember("Armies", (Concept)c)));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=zPYRGJtD} Troops", null), (object c) => Concept.IsGroupMember("Troops", (Concept)c)));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=3PUkH5Zf} Items", null), (object c) => Concept.IsGroupMember("Items", (Concept)c)));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=xKVBAL3m} Campaign Issues", null), (object c) => Concept.IsGroupMember("CampaignIssues", (Concept)c)));
			list.Add(new EncyclopediaFilterGroup(list2, new TextObject("{=tBx7XXps}Types", null)));
			return list;
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x0008E707 File Offset: 0x0008C907
		protected override IEnumerable<EncyclopediaSortController> InitializeSortControllers()
		{
			return new List<EncyclopediaSortController>();
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0008E70E File Offset: 0x0008C90E
		public override string GetViewFullyQualifiedName()
		{
			return "EncyclopediaConceptPage";
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0008E715 File Offset: 0x0008C915
		public override TextObject GetName()
		{
			return GameTexts.FindText("str_concepts", null);
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0008E722 File Offset: 0x0008C922
		public override TextObject GetDescriptionText()
		{
			return GameTexts.FindText("str_concepts_description", null);
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x0008E72F File Offset: 0x0008C92F
		public override string GetStringID()
		{
			return "EncyclopediaConcept";
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0008E738 File Offset: 0x0008C938
		public override bool IsValidEncyclopediaItem(object o)
		{
			Concept concept = o as Concept;
			return concept != null && concept.Title != null && concept.Description != null;
		}
	}
}
