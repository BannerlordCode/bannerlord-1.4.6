using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000421 RID: 1057
	public class OrderOfBattleCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600437D RID: 17277 RVA: 0x00147808 File Offset: 0x00145A08
		public OrderOfBattleCampaignBehavior()
		{
			this._siegeFormationInfos = new List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData>();
			this._siegeArmyFormationInfos = new List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData>();
			this._fieldBattleFormationInfos = new List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData>();
			this._fieldBattleArmyFormationInfos = new List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData>();
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x0014783C File Offset: 0x00145A3C
		public override void RegisterEvents()
		{
			CampaignEvents.OnHeroUnregisteredEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnHeroUnregistered));
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x00147858 File Offset: 0x00145A58
		public override void SyncData(IDataStore dataStore)
		{
			if (dataStore.SyncData<List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData>>("_siegeFormationInfos", ref this._siegeFormationInfos) && this._siegeFormationInfos == null)
			{
				this._siegeFormationInfos = new List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData>();
			}
			if (dataStore.SyncData<List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData>>("_siegeArmyFormationInfos", ref this._siegeArmyFormationInfos) && this._siegeArmyFormationInfos == null)
			{
				this._siegeArmyFormationInfos = new List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData>();
			}
			if (dataStore.SyncData<List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData>>("_formationInfos", ref this._fieldBattleFormationInfos) && this._fieldBattleFormationInfos == null)
			{
				this._fieldBattleFormationInfos = new List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData>();
			}
			if (dataStore.SyncData<List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData>>("_fieldBattleArmyFormationInfos", ref this._fieldBattleArmyFormationInfos) && this._fieldBattleArmyFormationInfos == null)
			{
				this._fieldBattleArmyFormationInfos = new List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData>();
			}
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x00147900 File Offset: 0x00145B00
		public OrderOfBattleCampaignBehavior.OrderOfBattleFormationData GetFormationDataAtIndex(int formationIndex, bool isSiegeBattle, bool isInArmy)
		{
			if (isSiegeBattle)
			{
				if (isInArmy)
				{
					if (this._siegeArmyFormationInfos.Count > formationIndex)
					{
						return this._siegeArmyFormationInfos[formationIndex];
					}
					return null;
				}
				else
				{
					if (this._siegeFormationInfos.Count > formationIndex)
					{
						return this._siegeFormationInfos[formationIndex];
					}
					return null;
				}
			}
			else if (isInArmy)
			{
				if (this._fieldBattleArmyFormationInfos.Count > formationIndex)
				{
					return this._fieldBattleArmyFormationInfos[formationIndex];
				}
				return null;
			}
			else
			{
				if (this._fieldBattleFormationInfos.Count > formationIndex)
				{
					return this._fieldBattleFormationInfos[formationIndex];
				}
				return null;
			}
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x00147989 File Offset: 0x00145B89
		public void SetFormationInfos(List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData> formationInfos, bool isSiegeBattle, bool isInArmy)
		{
			if (isSiegeBattle)
			{
				if (isInArmy)
				{
					this._siegeArmyFormationInfos = new List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData>(formationInfos);
					return;
				}
				this._siegeFormationInfos = new List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData>(formationInfos);
				return;
			}
			else
			{
				if (isInArmy)
				{
					this._fieldBattleArmyFormationInfos = new List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData>(formationInfos);
					return;
				}
				this._fieldBattleFormationInfos = new List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData>(formationInfos);
				return;
			}
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x001479C8 File Offset: 0x00145BC8
		private void OnHeroUnregistered(Hero hero)
		{
			int i = this._siegeFormationInfos.Count - 1;
			Func<Hero, bool> <>9__0;
			while (i >= 0)
			{
				OrderOfBattleCampaignBehavior.OrderOfBattleFormationData orderOfBattleFormationData = this._siegeFormationInfos[i];
				if (orderOfBattleFormationData.Captain == hero)
				{
					goto IL_0055;
				}
				Hero[] heroTroops = orderOfBattleFormationData.HeroTroops;
				if (heroTroops != null && heroTroops.Contains(hero))
				{
					goto IL_0055;
				}
				IL_00D3:
				i--;
				continue;
				IL_0055:
				Hero[] heroTroops2 = orderOfBattleFormationData.HeroTroops;
				Hero[] array;
				if (heroTroops2 == null)
				{
					array = null;
				}
				else
				{
					Func<Hero, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (Hero t) => t != hero);
					}
					array = heroTroops2.Where<Hero>(func).ToArray<Hero>();
				}
				Hero[] array2 = array;
				Hero hero2 = ((orderOfBattleFormationData.Captain == hero) ? null : orderOfBattleFormationData.Captain);
				this._siegeFormationInfos[i] = new OrderOfBattleCampaignBehavior.OrderOfBattleFormationData(hero2, array2, orderOfBattleFormationData.FormationClass, orderOfBattleFormationData.PrimaryClassWeight, orderOfBattleFormationData.SecondaryClassWeight, orderOfBattleFormationData.Filters);
				goto IL_00D3;
			}
			int j = this._fieldBattleFormationInfos.Count - 1;
			Func<Hero, bool> <>9__1;
			while (j >= 0)
			{
				OrderOfBattleCampaignBehavior.OrderOfBattleFormationData orderOfBattleFormationData2 = this._fieldBattleFormationInfos[j];
				if (orderOfBattleFormationData2.Captain == hero)
				{
					goto IL_012E;
				}
				Hero[] heroTroops3 = orderOfBattleFormationData2.HeroTroops;
				if (heroTroops3 != null && heroTroops3.Contains(hero))
				{
					goto IL_012E;
				}
				IL_01B6:
				j--;
				continue;
				IL_012E:
				Hero[] heroTroops4 = orderOfBattleFormationData2.HeroTroops;
				Hero[] array3;
				if (heroTroops4 == null)
				{
					array3 = null;
				}
				else
				{
					Func<Hero, bool> func2;
					if ((func2 = <>9__1) == null)
					{
						func2 = (<>9__1 = (Hero t) => t != hero);
					}
					array3 = heroTroops4.Where<Hero>(func2).ToArray<Hero>();
				}
				Hero[] array4 = array3;
				Hero hero3 = ((orderOfBattleFormationData2.Captain == hero) ? null : orderOfBattleFormationData2.Captain);
				this._fieldBattleFormationInfos[j] = new OrderOfBattleCampaignBehavior.OrderOfBattleFormationData(hero3, array4, orderOfBattleFormationData2.FormationClass, orderOfBattleFormationData2.PrimaryClassWeight, orderOfBattleFormationData2.SecondaryClassWeight, orderOfBattleFormationData2.Filters);
				goto IL_01B6;
			}
		}

		// Token: 0x04001349 RID: 4937
		private List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData> _siegeFormationInfos;

		// Token: 0x0400134A RID: 4938
		private List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData> _siegeArmyFormationInfos;

		// Token: 0x0400134B RID: 4939
		private List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData> _fieldBattleFormationInfos;

		// Token: 0x0400134C RID: 4940
		private List<OrderOfBattleCampaignBehavior.OrderOfBattleFormationData> _fieldBattleArmyFormationInfos;

		// Token: 0x0200082C RID: 2092
		public class OrderOfBattleFormationData
		{
			// Token: 0x06006742 RID: 26434 RVA: 0x001C8ED0 File Offset: 0x001C70D0
			public OrderOfBattleFormationData(Hero captain, Hero[] heroTroops, DeploymentFormationClass formationClass, int primaryWeight, int secondaryWeight, Dictionary<FormationFilterType, bool> filters)
			{
				this.Captain = captain;
				this.HeroTroops = heroTroops;
				this.FormationClass = formationClass;
				this.PrimaryClassWeight = primaryWeight;
				this.SecondaryClassWeight = secondaryWeight;
				this.Filters = new Dictionary<FormationFilterType, bool>();
				foreach (FormationFilterType formationFilterType in filters.Keys)
				{
					this.Filters.Add(formationFilterType, filters[formationFilterType]);
				}
			}

			// Token: 0x06006743 RID: 26435 RVA: 0x001C8F68 File Offset: 0x001C7168
			internal static void AutoGeneratedStaticCollectObjectsOrderOfBattleFormationData(object o, List<object> collectedObjects)
			{
				((OrderOfBattleCampaignBehavior.OrderOfBattleFormationData)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06006744 RID: 26436 RVA: 0x001C8F76 File Offset: 0x001C7176
			protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				collectedObjects.Add(this.Captain);
				collectedObjects.Add(this.Filters);
				collectedObjects.Add(this.HeroTroops);
			}

			// Token: 0x06006745 RID: 26437 RVA: 0x001C8F9C File Offset: 0x001C719C
			internal static object AutoGeneratedGetMemberValueCaptain(object o)
			{
				return ((OrderOfBattleCampaignBehavior.OrderOfBattleFormationData)o).Captain;
			}

			// Token: 0x06006746 RID: 26438 RVA: 0x001C8FA9 File Offset: 0x001C71A9
			internal static object AutoGeneratedGetMemberValueFormationClass(object o)
			{
				return ((OrderOfBattleCampaignBehavior.OrderOfBattleFormationData)o).FormationClass;
			}

			// Token: 0x06006747 RID: 26439 RVA: 0x001C8FBB File Offset: 0x001C71BB
			internal static object AutoGeneratedGetMemberValuePrimaryClassWeight(object o)
			{
				return ((OrderOfBattleCampaignBehavior.OrderOfBattleFormationData)o).PrimaryClassWeight;
			}

			// Token: 0x06006748 RID: 26440 RVA: 0x001C8FCD File Offset: 0x001C71CD
			internal static object AutoGeneratedGetMemberValueSecondaryClassWeight(object o)
			{
				return ((OrderOfBattleCampaignBehavior.OrderOfBattleFormationData)o).SecondaryClassWeight;
			}

			// Token: 0x06006749 RID: 26441 RVA: 0x001C8FDF File Offset: 0x001C71DF
			internal static object AutoGeneratedGetMemberValueFilters(object o)
			{
				return ((OrderOfBattleCampaignBehavior.OrderOfBattleFormationData)o).Filters;
			}

			// Token: 0x0600674A RID: 26442 RVA: 0x001C8FEC File Offset: 0x001C71EC
			internal static object AutoGeneratedGetMemberValueHeroTroops(object o)
			{
				return ((OrderOfBattleCampaignBehavior.OrderOfBattleFormationData)o).HeroTroops;
			}

			// Token: 0x04002315 RID: 8981
			[SaveableField(1)]
			public readonly Hero Captain;

			// Token: 0x04002316 RID: 8982
			[SaveableField(2)]
			public readonly DeploymentFormationClass FormationClass;

			// Token: 0x04002317 RID: 8983
			[SaveableField(3)]
			public readonly int PrimaryClassWeight;

			// Token: 0x04002318 RID: 8984
			[SaveableField(4)]
			public readonly int SecondaryClassWeight;

			// Token: 0x04002319 RID: 8985
			[SaveableField(5)]
			public readonly Dictionary<FormationFilterType, bool> Filters;

			// Token: 0x0400231A RID: 8986
			[SaveableField(6)]
			public readonly Hero[] HeroTroops;
		}
	}
}
