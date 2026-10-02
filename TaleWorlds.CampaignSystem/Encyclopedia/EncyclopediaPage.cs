using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.Encyclopedia
{
	// Token: 0x02000177 RID: 375
	public abstract class EncyclopediaPage
	{
		// Token: 0x06001B63 RID: 7011
		protected abstract IEnumerable<EncyclopediaListItem> InitializeListItems();

		// Token: 0x06001B64 RID: 7012
		protected abstract IEnumerable<EncyclopediaFilterGroup> InitializeFilterItems();

		// Token: 0x06001B65 RID: 7013
		protected abstract IEnumerable<EncyclopediaSortController> InitializeSortControllers();

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0008DE2A File Offset: 0x0008C02A
		// (set) Token: 0x06001B67 RID: 7015 RVA: 0x0008DE32 File Offset: 0x0008C032
		public int HomePageOrderIndex { get; protected set; }

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x0008DE3B File Offset: 0x0008C03B
		public EncyclopediaPage Parent { get; }

		// Token: 0x06001B69 RID: 7017 RVA: 0x0008DE44 File Offset: 0x0008C044
		public EncyclopediaPage()
		{
			this._filters = this.InitializeFilterItems();
			this._items = this.InitializeListItems();
			this._sortControllers = new List<EncyclopediaSortController>
			{
				new EncyclopediaSortController(new TextObject("{=koX9okuG}None", null), new EncyclopediaListItemNameComparer())
			};
			((List<EncyclopediaSortController>)this._sortControllers).AddRange(this.InitializeSortControllers());
			foreach (object obj in base.GetType().GetCustomAttributesSafe(typeof(EncyclopediaModel), true))
			{
				if (obj is EncyclopediaModel)
				{
					this._identifierTypes = (obj as EncyclopediaModel).PageTargetTypes;
					break;
				}
			}
			this._identifiers = new Dictionary<Type, string>();
			foreach (Type type in this._identifierTypes)
			{
				if (Game.Current.ObjectManager.HasType(type))
				{
					this._identifiers.Add(type, Game.Current.ObjectManager.FindRegisteredClassPrefix(type));
				}
				else
				{
					string text = type.Name.ToString();
					if (text == "Clan")
					{
						text = "Faction";
					}
					this._identifiers.Add(type, text);
				}
			}
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0008DF79 File Offset: 0x0008C179
		public virtual bool IsRelevant()
		{
			return true;
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0008DF7C File Offset: 0x0008C17C
		public bool HasIdentifierType(Type identifierType)
		{
			return this._identifierTypes.Contains(identifierType);
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x0008DF8A File Offset: 0x0008C18A
		internal bool HasIdentifier(string identifier)
		{
			return this._identifiers.ContainsValue(identifier);
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x0008DF98 File Offset: 0x0008C198
		public string GetIdentifier(Type identifierType)
		{
			if (this._identifiers.ContainsKey(identifierType))
			{
				return this._identifiers[identifierType];
			}
			return "";
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x0008DFBA File Offset: 0x0008C1BA
		public string[] GetIdentifierNames()
		{
			return this._identifiers.Values.ToArray<string>();
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x0008DFCC File Offset: 0x0008C1CC
		public bool IsFiltered(object o)
		{
			using (IEnumerator<EncyclopediaFilterGroup> enumerator = this.GetFilterItems().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Predicate(o))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x0008E028 File Offset: 0x0008C228
		public virtual string GetViewFullyQualifiedName()
		{
			return "";
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x0008E02F File Offset: 0x0008C22F
		public virtual string GetStringID()
		{
			return "";
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x0008E036 File Offset: 0x0008C236
		public virtual TextObject GetName()
		{
			return TextObject.GetEmpty();
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0008E03D File Offset: 0x0008C23D
		public virtual MBObjectBase GetObject(string typeName, string stringID)
		{
			return MBObjectManager.Instance.GetObject(typeName, stringID);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0008E04B File Offset: 0x0008C24B
		public virtual bool IsValidEncyclopediaItem(object o)
		{
			return false;
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0008E04E File Offset: 0x0008C24E
		public virtual TextObject GetDescriptionText()
		{
			return TextObject.GetEmpty();
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0008E055 File Offset: 0x0008C255
		public IEnumerable<EncyclopediaListItem> GetListItems()
		{
			return this._items;
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0008E05D File Offset: 0x0008C25D
		public IEnumerable<EncyclopediaFilterGroup> GetFilterItems()
		{
			return this._filters;
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0008E065 File Offset: 0x0008C265
		public IEnumerable<EncyclopediaSortController> GetSortControllers()
		{
			return this._sortControllers;
		}

		// Token: 0x04000943 RID: 2371
		private readonly Type[] _identifierTypes;

		// Token: 0x04000944 RID: 2372
		private readonly Dictionary<Type, string> _identifiers;

		// Token: 0x04000945 RID: 2373
		private IEnumerable<EncyclopediaFilterGroup> _filters;

		// Token: 0x04000946 RID: 2374
		private IEnumerable<EncyclopediaListItem> _items;

		// Token: 0x04000947 RID: 2375
		private IEnumerable<EncyclopediaSortController> _sortControllers;
	}
}
