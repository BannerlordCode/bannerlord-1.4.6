using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x0200003F RID: 63
	public class CampaignPeriodicEventManager
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0002590C File Offset: 0x00023B0C
		private double DeltaHours
		{
			get
			{
				return CampaignTime.DeltaTime.ToHours;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x00025928 File Offset: 0x00023B28
		private double DeltaDays
		{
			get
			{
				return CampaignTime.DeltaTime.ToDays;
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00025944 File Offset: 0x00023B44
		internal CampaignPeriodicEventManager()
		{
			this._mobilePartyHourlyTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
			this._mobilePartyDailyTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
			this._hourlyTickMobilePartyTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
			this._hourlyTickSettlementTicker = new CampaignPeriodicEventManager.PeriodicTicker<Settlement>();
			this._dailyTickSettlementTicker = new CampaignPeriodicEventManager.PeriodicTicker<Settlement>();
			this._hourlyTickClanTicker = new CampaignPeriodicEventManager.PeriodicTicker<Clan>();
			this._dailyTickPartyTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
			this._dailyTickTownTicker = new CampaignPeriodicEventManager.PeriodicTicker<Town>();
			this._dailyTickHeroTicker = new CampaignPeriodicEventManager.PeriodicTicker<Hero>();
			this._dailyTickClanTicker = new CampaignPeriodicEventManager.PeriodicTicker<Clan>();
			this._quarterDailyPartyTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
			this._caravanMobilePartyPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
			this._garrisonMobilePartyPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
			this._militiaMobilePartyPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
			this._villagerMobilePartyPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
			this._customMobilePartyPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
			this._patrolPartyPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
			this._banditMobilePartyPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
			this._lordMobilePartyPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
			this._partiesWithoutPartyComponentsPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00025A34 File Offset: 0x00023C34
		[LoadInitializationCallback]
		private void OnLoad(MetaData metaData)
		{
			if (this._caravanMobilePartyPartialHourlyAiEventTicker == null)
			{
				this._caravanMobilePartyPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
				this._garrisonMobilePartyPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
				this._militiaMobilePartyPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
				this._villagerMobilePartyPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
				this._customMobilePartyPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
				this._banditMobilePartyPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
				this._lordMobilePartyPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
				this._partiesWithoutPartyComponentsPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
				this._quarterDailyPartyTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
			}
			if (MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion.IsOlderThan(ApplicationVersion.FromString("v1.3.0", 0)) && this._patrolPartyPartialHourlyAiEventTicker == null)
			{
				this._patrolPartyPartialHourlyAiEventTicker = new CampaignPeriodicEventManager.PeriodicTicker<MobileParty>();
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00025AE0 File Offset: 0x00023CE0
		internal void InitializeTickers()
		{
			this.MinimumPeriodicEventInterval = CampaignTime.Hours(0.05f);
			this._lastGameTime = CampaignTime.Zero;
			MBList<Settlement> mblist = this.ShuffleSettlements();
			this._mobilePartyHourlyTicker.Initialize(MobileParty.All, delegate(MobileParty x)
			{
				x.HourlyTick();
			}, false);
			this._mobilePartyDailyTicker.Initialize(MobileParty.All, delegate(MobileParty x)
			{
				x.DailyTick();
			}, false);
			this._hourlyTickMobilePartyTicker.Initialize(MobileParty.All, delegate(MobileParty x)
			{
				CampaignEventDispatcher.Instance.HourlyTickParty(x);
			}, false);
			this._hourlyTickSettlementTicker.Initialize(mblist, delegate(Settlement x)
			{
				CampaignEventDispatcher.Instance.HourlyTickSettlement(x);
			}, false);
			this._dailyTickSettlementTicker.Initialize(mblist, delegate(Settlement x)
			{
				CampaignEventDispatcher.Instance.DailyTickSettlement(x);
			}, false);
			this._hourlyTickClanTicker.Initialize(Clan.All, delegate(Clan x)
			{
				CampaignEventDispatcher.Instance.HourlyTickClan(x);
			}, false);
			this._dailyTickPartyTicker.Initialize(MobileParty.All, delegate(MobileParty x)
			{
				CampaignEventDispatcher.Instance.DailyTickParty(x);
			}, false);
			this._dailyTickTownTicker.Initialize(Town.AllTowns, delegate(Town x)
			{
				CampaignEventDispatcher.Instance.DailyTickTown(x);
			}, false);
			this._dailyTickHeroTicker.Initialize(Hero.AllAliveHeroes, delegate(Hero x)
			{
				CampaignEventDispatcher.Instance.DailyTickHero(x);
			}, false);
			this._dailyTickClanTicker.Initialize(Clan.All, delegate(Clan x)
			{
				CampaignEventDispatcher.Instance.DailyTickClan(x);
			}, false);
			bool flag = false;
			this._caravanMobilePartyPartialHourlyAiEventTicker.Initialize(MobileParty.AllCaravanParties, delegate(MobileParty x)
			{
				CampaignEventDispatcher.Instance.TickPartialHourlyAi(x);
			}, flag);
			this._garrisonMobilePartyPartialHourlyAiEventTicker.Initialize(MobileParty.AllGarrisonParties, delegate(MobileParty x)
			{
				CampaignEventDispatcher.Instance.TickPartialHourlyAi(x);
			}, flag);
			this._militiaMobilePartyPartialHourlyAiEventTicker.Initialize(MobileParty.AllMilitiaParties, delegate(MobileParty x)
			{
				CampaignEventDispatcher.Instance.TickPartialHourlyAi(x);
			}, flag);
			this._villagerMobilePartyPartialHourlyAiEventTicker.Initialize(MobileParty.AllVillagerParties, delegate(MobileParty x)
			{
				CampaignEventDispatcher.Instance.TickPartialHourlyAi(x);
			}, flag);
			this._customMobilePartyPartialHourlyAiEventTicker.Initialize(MobileParty.AllCustomParties, delegate(MobileParty x)
			{
				CampaignEventDispatcher.Instance.TickPartialHourlyAi(x);
			}, flag);
			this._patrolPartyPartialHourlyAiEventTicker.Initialize(MobileParty.AllPatrolParties, delegate(MobileParty x)
			{
				CampaignEventDispatcher.Instance.TickPartialHourlyAi(x);
			}, flag);
			this._banditMobilePartyPartialHourlyAiEventTicker.Initialize(MobileParty.AllBanditParties, delegate(MobileParty x)
			{
				CampaignEventDispatcher.Instance.TickPartialHourlyAi(x);
			}, flag);
			this._lordMobilePartyPartialHourlyAiEventTicker.Initialize(MobileParty.AllLordParties, delegate(MobileParty x)
			{
				CampaignEventDispatcher.Instance.TickPartialHourlyAi(x);
			}, flag);
			this._partiesWithoutPartyComponentsPartialHourlyAiEventTicker.Initialize(MobileParty.AllPartiesWithoutPartyComponent, delegate(MobileParty x)
			{
				CampaignEventDispatcher.Instance.TickPartialHourlyAi(x);
			}, flag);
			this._quarterDailyPartyTicker.Initialize(MobileParty.All, delegate(MobileParty x)
			{
				CampaignEventDispatcher.Instance.QuarterDailyPartyTick(x);
			}, false);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00025ECC File Offset: 0x000240CC
		private MBList<Settlement> ShuffleSettlements()
		{
			Stack<Settlement> stack = new Stack<Settlement>();
			Stack<Settlement> stack2 = new Stack<Settlement>();
			Stack<Settlement> stack3 = new Stack<Settlement>();
			Stack<Settlement> stack4 = new Stack<Settlement>();
			foreach (Settlement settlement in Settlement.All)
			{
				if (settlement.IsVillage)
				{
					stack.Push(settlement);
				}
				else if (settlement.IsCastle)
				{
					stack2.Push(settlement);
				}
				else if (settlement.IsTown)
				{
					stack3.Push(settlement);
				}
				else
				{
					stack4.Push(settlement);
				}
			}
			float num = (float)Settlement.All.Count;
			float num2 = (float)stack.Count / num;
			float num3 = (float)stack2.Count / num;
			float num4 = (float)stack3.Count / num;
			float num5 = (float)stack4.Count / num;
			float num6 = num2;
			float num7 = num3;
			float num8 = num4;
			float num9 = num5;
			MBList<Settlement> mblist = new MBList<Settlement>();
			while (mblist.Count != Settlement.All.Count)
			{
				num6 += num2;
				if (num6 >= 1f && !stack.IsEmpty<Settlement>())
				{
					mblist.Add(stack.Pop());
					num6 -= 1f;
				}
				num7 += num3;
				if (num7 >= 1f && !stack2.IsEmpty<Settlement>())
				{
					mblist.Add(stack2.Pop());
					num7 -= 1f;
				}
				num8 += num4;
				if (num8 >= 1f && !stack3.IsEmpty<Settlement>())
				{
					mblist.Add(stack3.Pop());
					num8 -= 1f;
				}
				num9 += num5;
				if (num9 >= 1f && !stack4.IsEmpty<Settlement>())
				{
					mblist.Add(stack4.Pop());
					num9 -= 1f;
				}
			}
			return mblist;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000260A0 File Offset: 0x000242A0
		internal void TickPeriodicEvents()
		{
			this.PeriodicHourlyTick();
			this.PeriodicDailyTick();
			this.PeriodicQuarterDailyTick();
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000260B4 File Offset: 0x000242B4
		private void PeriodicQuarterDailyTick()
		{
			double deltaDays = this.DeltaDays;
			this._quarterDailyPartyTicker.PeriodicTickSome(deltaDays * 4.0);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000260DE File Offset: 0x000242DE
		internal void MobilePartyHourlyTick()
		{
			this._mobilePartyHourlyTicker.PeriodicTickSome(this.DeltaHours);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000260F4 File Offset: 0x000242F4
		internal void TickPartialHourlyAi()
		{
			this._caravanMobilePartyPartialHourlyAiEventTicker.PeriodicTickSome(this.DeltaHours * 0.99);
			this._garrisonMobilePartyPartialHourlyAiEventTicker.PeriodicTickSome(this.DeltaHours * 0.99);
			this._militiaMobilePartyPartialHourlyAiEventTicker.PeriodicTickSome(this.DeltaHours * 0.99);
			this._villagerMobilePartyPartialHourlyAiEventTicker.PeriodicTickSome(this.DeltaHours * 0.99);
			this._customMobilePartyPartialHourlyAiEventTicker.PeriodicTickSome(this.DeltaHours * 0.99);
			this._patrolPartyPartialHourlyAiEventTicker.PeriodicTickSome(this.DeltaHours * 0.99);
			this._banditMobilePartyPartialHourlyAiEventTicker.PeriodicTickSome(this.DeltaHours * 0.99);
			this._lordMobilePartyPartialHourlyAiEventTicker.PeriodicTickSome(this.DeltaHours * 0.99);
			this._partiesWithoutPartyComponentsPartialHourlyAiEventTicker.PeriodicTickSome(this.DeltaHours * 0.99);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000261F4 File Offset: 0x000243F4
		private void PeriodicHourlyTick()
		{
			double deltaHours = this.DeltaHours;
			this._hourlyTickMobilePartyTicker.PeriodicTickSome(deltaHours);
			this._hourlyTickSettlementTicker.PeriodicTickSome(deltaHours);
			this._hourlyTickClanTicker.PeriodicTickSome(deltaHours);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0002622C File Offset: 0x0002442C
		private void PeriodicDailyTick()
		{
			double deltaDays = this.DeltaDays;
			this._dailyTickPartyTicker.PeriodicTickSome(deltaDays);
			this._mobilePartyDailyTicker.PeriodicTickSome(deltaDays);
			this._dailyTickTownTicker.PeriodicTickSome(deltaDays);
			this._dailyTickSettlementTicker.PeriodicTickSome(deltaDays);
			this._dailyTickHeroTicker.PeriodicTickSome(deltaDays);
			this._dailyTickClanTicker.PeriodicTickSome(deltaDays);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00026288 File Offset: 0x00024488
		public static MBCampaignEvent CreatePeriodicEvent(CampaignTime triggerPeriod, CampaignTime initialWait)
		{
			MBCampaignEvent mbcampaignEvent = new MBCampaignEvent(triggerPeriod, initialWait);
			Campaign.Current.CustomPeriodicCampaignEvents.Add(mbcampaignEvent);
			return mbcampaignEvent;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000262B0 File Offset: 0x000244B0
		private void DeleteMarkedPeriodicEvents()
		{
			List<MBCampaignEvent> customPeriodicCampaignEvents = Campaign.Current.CustomPeriodicCampaignEvents;
			for (int i = customPeriodicCampaignEvents.Count - 1; i >= 0; i--)
			{
				if (customPeriodicCampaignEvents[i].isEventDeleted)
				{
					customPeriodicCampaignEvents.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000262F0 File Offset: 0x000244F0
		internal void OnTick(float dt)
		{
			this.SignalPeriodicEvents();
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000262F8 File Offset: 0x000244F8
		private void SignalPeriodicEvents()
		{
			if ((this._lastGameTime + this.MinimumPeriodicEventInterval).IsPast)
			{
				this._lastGameTime = CampaignTime.Now;
				List<MBCampaignEvent> customPeriodicCampaignEvents = Campaign.Current.CustomPeriodicCampaignEvents;
				for (int i = customPeriodicCampaignEvents.Count - 1; i >= 0; i--)
				{
					customPeriodicCampaignEvents[i].CheckUpdate();
				}
				this.DeleteMarkedPeriodicEvents();
				MapState mapState = Game.Current.GameStateManager.ActiveState as MapState;
				if (mapState == null)
				{
					return;
				}
				mapState.OnSignalPeriodicEvents();
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00026379 File Offset: 0x00024579
		internal static void AutoGeneratedStaticCollectObjectsCampaignPeriodicEventManager(object o, List<object> collectedObjects)
		{
			((CampaignPeriodicEventManager)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00026388 File Offset: 0x00024588
		protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			collectedObjects.Add(this._mobilePartyHourlyTicker);
			collectedObjects.Add(this._mobilePartyDailyTicker);
			collectedObjects.Add(this._dailyTickPartyTicker);
			collectedObjects.Add(this._hourlyTickMobilePartyTicker);
			collectedObjects.Add(this._hourlyTickSettlementTicker);
			collectedObjects.Add(this._hourlyTickClanTicker);
			collectedObjects.Add(this._dailyTickTownTicker);
			collectedObjects.Add(this._dailyTickSettlementTicker);
			collectedObjects.Add(this._dailyTickHeroTicker);
			collectedObjects.Add(this._dailyTickClanTicker);
			collectedObjects.Add(this._quarterDailyPartyTicker);
			collectedObjects.Add(this._caravanMobilePartyPartialHourlyAiEventTicker);
			collectedObjects.Add(this._garrisonMobilePartyPartialHourlyAiEventTicker);
			collectedObjects.Add(this._militiaMobilePartyPartialHourlyAiEventTicker);
			collectedObjects.Add(this._villagerMobilePartyPartialHourlyAiEventTicker);
			collectedObjects.Add(this._customMobilePartyPartialHourlyAiEventTicker);
			collectedObjects.Add(this._banditMobilePartyPartialHourlyAiEventTicker);
			collectedObjects.Add(this._lordMobilePartyPartialHourlyAiEventTicker);
			collectedObjects.Add(this._partiesWithoutPartyComponentsPartialHourlyAiEventTicker);
			collectedObjects.Add(this._patrolPartyPartialHourlyAiEventTicker);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00026485 File Offset: 0x00024685
		internal static object AutoGeneratedGetMemberValue_mobilePartyHourlyTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._mobilePartyHourlyTicker;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00026492 File Offset: 0x00024692
		internal static object AutoGeneratedGetMemberValue_mobilePartyDailyTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._mobilePartyDailyTicker;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0002649F File Offset: 0x0002469F
		internal static object AutoGeneratedGetMemberValue_dailyTickPartyTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._dailyTickPartyTicker;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x000264AC File Offset: 0x000246AC
		internal static object AutoGeneratedGetMemberValue_hourlyTickMobilePartyTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._hourlyTickMobilePartyTicker;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x000264B9 File Offset: 0x000246B9
		internal static object AutoGeneratedGetMemberValue_hourlyTickSettlementTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._hourlyTickSettlementTicker;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x000264C6 File Offset: 0x000246C6
		internal static object AutoGeneratedGetMemberValue_hourlyTickClanTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._hourlyTickClanTicker;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000264D3 File Offset: 0x000246D3
		internal static object AutoGeneratedGetMemberValue_dailyTickTownTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._dailyTickTownTicker;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000264E0 File Offset: 0x000246E0
		internal static object AutoGeneratedGetMemberValue_dailyTickSettlementTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._dailyTickSettlementTicker;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000264ED File Offset: 0x000246ED
		internal static object AutoGeneratedGetMemberValue_dailyTickHeroTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._dailyTickHeroTicker;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000264FA File Offset: 0x000246FA
		internal static object AutoGeneratedGetMemberValue_dailyTickClanTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._dailyTickClanTicker;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00026507 File Offset: 0x00024707
		internal static object AutoGeneratedGetMemberValue_quarterDailyPartyTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._quarterDailyPartyTicker;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00026514 File Offset: 0x00024714
		internal static object AutoGeneratedGetMemberValue_caravanMobilePartyPartialHourlyAiEventTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._caravanMobilePartyPartialHourlyAiEventTicker;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00026521 File Offset: 0x00024721
		internal static object AutoGeneratedGetMemberValue_garrisonMobilePartyPartialHourlyAiEventTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._garrisonMobilePartyPartialHourlyAiEventTicker;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0002652E File Offset: 0x0002472E
		internal static object AutoGeneratedGetMemberValue_militiaMobilePartyPartialHourlyAiEventTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._militiaMobilePartyPartialHourlyAiEventTicker;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0002653B File Offset: 0x0002473B
		internal static object AutoGeneratedGetMemberValue_villagerMobilePartyPartialHourlyAiEventTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._villagerMobilePartyPartialHourlyAiEventTicker;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00026548 File Offset: 0x00024748
		internal static object AutoGeneratedGetMemberValue_customMobilePartyPartialHourlyAiEventTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._customMobilePartyPartialHourlyAiEventTicker;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00026555 File Offset: 0x00024755
		internal static object AutoGeneratedGetMemberValue_banditMobilePartyPartialHourlyAiEventTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._banditMobilePartyPartialHourlyAiEventTicker;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00026562 File Offset: 0x00024762
		internal static object AutoGeneratedGetMemberValue_lordMobilePartyPartialHourlyAiEventTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._lordMobilePartyPartialHourlyAiEventTicker;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0002656F File Offset: 0x0002476F
		internal static object AutoGeneratedGetMemberValue_partiesWithoutPartyComponentsPartialHourlyAiEventTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._partiesWithoutPartyComponentsPartialHourlyAiEventTicker;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0002657C File Offset: 0x0002477C
		internal static object AutoGeneratedGetMemberValue_patrolPartyPartialHourlyAiEventTicker(object o)
		{
			return ((CampaignPeriodicEventManager)o)._patrolPartyPartialHourlyAiEventTicker;
		}

		// Token: 0x040002A2 RID: 674
		[SaveableField(120)]
		private CampaignPeriodicEventManager.PeriodicTicker<MobileParty> _mobilePartyHourlyTicker;

		// Token: 0x040002A3 RID: 675
		[SaveableField(130)]
		private CampaignPeriodicEventManager.PeriodicTicker<MobileParty> _mobilePartyDailyTicker;

		// Token: 0x040002A4 RID: 676
		[SaveableField(140)]
		private CampaignPeriodicEventManager.PeriodicTicker<MobileParty> _dailyTickPartyTicker;

		// Token: 0x040002A5 RID: 677
		[SaveableField(150)]
		private CampaignPeriodicEventManager.PeriodicTicker<MobileParty> _hourlyTickMobilePartyTicker;

		// Token: 0x040002A6 RID: 678
		[SaveableField(160)]
		private CampaignPeriodicEventManager.PeriodicTicker<Settlement> _hourlyTickSettlementTicker;

		// Token: 0x040002A7 RID: 679
		[SaveableField(170)]
		private CampaignPeriodicEventManager.PeriodicTicker<Clan> _hourlyTickClanTicker;

		// Token: 0x040002A8 RID: 680
		[SaveableField(180)]
		private CampaignPeriodicEventManager.PeriodicTicker<Town> _dailyTickTownTicker;

		// Token: 0x040002A9 RID: 681
		[SaveableField(190)]
		private CampaignPeriodicEventManager.PeriodicTicker<Settlement> _dailyTickSettlementTicker;

		// Token: 0x040002AA RID: 682
		[SaveableField(200)]
		private CampaignPeriodicEventManager.PeriodicTicker<Hero> _dailyTickHeroTicker;

		// Token: 0x040002AB RID: 683
		[SaveableField(210)]
		private CampaignPeriodicEventManager.PeriodicTicker<Clan> _dailyTickClanTicker;

		// Token: 0x040002AC RID: 684
		[SaveableField(320)]
		private CampaignPeriodicEventManager.PeriodicTicker<MobileParty> _quarterDailyPartyTicker;

		// Token: 0x040002AD RID: 685
		[SaveableField(230)]
		private CampaignPeriodicEventManager.PeriodicTicker<MobileParty> _caravanMobilePartyPartialHourlyAiEventTicker;

		// Token: 0x040002AE RID: 686
		[SaveableField(250)]
		private CampaignPeriodicEventManager.PeriodicTicker<MobileParty> _garrisonMobilePartyPartialHourlyAiEventTicker;

		// Token: 0x040002AF RID: 687
		[SaveableField(260)]
		private CampaignPeriodicEventManager.PeriodicTicker<MobileParty> _militiaMobilePartyPartialHourlyAiEventTicker;

		// Token: 0x040002B0 RID: 688
		[SaveableField(270)]
		private CampaignPeriodicEventManager.PeriodicTicker<MobileParty> _villagerMobilePartyPartialHourlyAiEventTicker;

		// Token: 0x040002B1 RID: 689
		[SaveableField(280)]
		private CampaignPeriodicEventManager.PeriodicTicker<MobileParty> _customMobilePartyPartialHourlyAiEventTicker;

		// Token: 0x040002B2 RID: 690
		[SaveableField(290)]
		private CampaignPeriodicEventManager.PeriodicTicker<MobileParty> _banditMobilePartyPartialHourlyAiEventTicker;

		// Token: 0x040002B3 RID: 691
		[SaveableField(300)]
		private CampaignPeriodicEventManager.PeriodicTicker<MobileParty> _lordMobilePartyPartialHourlyAiEventTicker;

		// Token: 0x040002B4 RID: 692
		[SaveableField(310)]
		private CampaignPeriodicEventManager.PeriodicTicker<MobileParty> _partiesWithoutPartyComponentsPartialHourlyAiEventTicker;

		// Token: 0x040002B5 RID: 693
		[SaveableField(330)]
		private CampaignPeriodicEventManager.PeriodicTicker<MobileParty> _patrolPartyPartialHourlyAiEventTicker;

		// Token: 0x040002B6 RID: 694
		private CampaignTime MinimumPeriodicEventInterval;

		// Token: 0x040002B7 RID: 695
		private CampaignTime _lastGameTime;

		// Token: 0x02000508 RID: 1288
		internal class PeriodicTicker<T>
		{
			// Token: 0x17000ED0 RID: 3792
			// (get) Token: 0x06004BE1 RID: 19425 RVA: 0x0017D772 File Offset: 0x0017B972
			// (set) Token: 0x06004BE2 RID: 19426 RVA: 0x0017D77A File Offset: 0x0017B97A
			[SaveableProperty(1)]
			private double TickDebt { get; set; }

			// Token: 0x17000ED1 RID: 3793
			// (get) Token: 0x06004BE3 RID: 19427 RVA: 0x0017D783 File Offset: 0x0017B983
			// (set) Token: 0x06004BE4 RID: 19428 RVA: 0x0017D78B File Offset: 0x0017B98B
			[SaveableProperty(2)]
			private int Index { get; set; }

			// Token: 0x06004BE5 RID: 19429 RVA: 0x0017D794 File Offset: 0x0017B994
			internal PeriodicTicker()
			{
				this.TickDebt = 0.0;
				this.Index = -1;
			}

			// Token: 0x06004BE6 RID: 19430 RVA: 0x0017D7BD File Offset: 0x0017B9BD
			internal void Initialize(MBReadOnlyList<T> list, Action<T> action, bool doParallel)
			{
				this._list = list;
				this._action = action;
				this._doParallel = doParallel;
			}

			// Token: 0x06004BE7 RID: 19431 RVA: 0x0017D7D4 File Offset: 0x0017B9D4
			internal void PeriodicTickSome(double timeUnitsElapsed)
			{
				if (this._list.Count == 0)
				{
					this.TickDebt = 0.0;
					return;
				}
				this.TickDebt += timeUnitsElapsed * (double)this._list.Count;
				while (this.TickDebt > 1.0)
				{
					this.Index++;
					if (this.Index >= this._list.Count)
					{
						this.Index = 0;
					}
					if (this._doParallel)
					{
						this._currentFrameToTickListFlattened.Add(this._list[this.Index]);
					}
					else
					{
						this._action(this._list[this.Index]);
					}
					this.TickDebt -= 1.0;
				}
				if (this._doParallel && this._currentFrameToTickListFlattened.Count > 0)
				{
					TWParallel.For(0, this._currentFrameToTickListFlattened.Count, delegate(int startInclusive, int endExclusive)
					{
						for (int i = startInclusive; i < endExclusive; i++)
						{
							this._action(this._currentFrameToTickListFlattened[i]);
						}
					}, 1);
					this._currentFrameToTickListFlattened.Clear();
				}
			}

			// Token: 0x06004BE8 RID: 19432 RVA: 0x0017D8F4 File Offset: 0x0017BAF4
			public override string ToString()
			{
				object[] array = new object[7];
				array[0] = "PeriodicTicker  @";
				int num = 1;
				object obj;
				if (this.Index != -1)
				{
					T t = this._list[this.Index];
					obj = t.ToString();
				}
				else
				{
					obj = "null";
				}
				array[num] = obj;
				array[2] = "\t\t(";
				array[3] = this.Index;
				array[4] = " / ";
				array[5] = this._list.Count;
				array[6] = ")";
				return string.Concat(array);
			}

			// Token: 0x06004BE9 RID: 19433 RVA: 0x0017D97F File Offset: 0x0017BB7F
			protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
			}

			// Token: 0x040015A0 RID: 5536
			private readonly List<T> _currentFrameToTickListFlattened = new List<T>();

			// Token: 0x040015A3 RID: 5539
			private bool _doParallel;

			// Token: 0x040015A4 RID: 5540
			private MBReadOnlyList<T> _list;

			// Token: 0x040015A5 RID: 5541
			private Action<T> _action;
		}
	}
}
