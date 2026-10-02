using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map.Tracker;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Map.Tracker
{
	// Token: 0x0200004B RID: 75
	public class MapTrackerProvider
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600049C RID: 1180 RVA: 0x0001209A File Offset: 0x0001029A
		// (remove) Token: 0x0600049D RID: 1181 RVA: 0x000120B8 File Offset: 0x000102B8
		public event MapTrackerProvider.OnTrackerAddedOrRemovedDelegate OnTrackerAddedOrRemoved
		{
			add
			{
				MapTrackerProvider.TrackerContainer trackerContainer = this._trackerContainer;
				trackerContainer.OnTrackerAddedOrRemoved = (MapTrackerProvider.OnTrackerAddedOrRemovedDelegate)Delegate.Combine(trackerContainer.OnTrackerAddedOrRemoved, value);
			}
			remove
			{
				MapTrackerProvider.TrackerContainer trackerContainer = this._trackerContainer;
				trackerContainer.OnTrackerAddedOrRemoved = (MapTrackerProvider.OnTrackerAddedOrRemovedDelegate)Delegate.Remove(trackerContainer.OnTrackerAddedOrRemoved, value);
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000120D8 File Offset: 0x000102D8
		public MapTrackerProvider()
		{
			CampaignEvents.ArmyCreated.AddNonSerializedListener(this, new Action<Army>(this.OnArmyCreated));
			CampaignEvents.ArmyDispersed.AddNonSerializedListener(this, new Action<Army, Army.ArmyDispersionReason, bool>(this.OnArmyDispersed));
			CampaignEvents.MobilePartyCreated.AddNonSerializedListener(this, new Action<MobileParty>(this.OnMobilePartyCreated));
			CampaignEvents.MobilePartyDestroyed.AddNonSerializedListener(this, new Action<MobileParty, PartyBase>(this.OnPartyDestroyed));
			CampaignEvents.MobilePartyQuestStatusChanged.AddNonSerializedListener(this, new Action<MobileParty, bool>(this.OnPartyQuestStatusChanged));
			CampaignEvents.OnPartyDisbandedEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement>(this.OnPartyDisbanded));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
			CampaignEvents.OnClanCreatedEvent.AddNonSerializedListener(this, new Action<Clan, bool>(this.OnCompanionClanCreated));
			CampaignEvents.OnMapMarkerCreatedEvent.AddNonSerializedListener(this, new Action<MapMarker>(this.OnMapMarkerCreated));
			CampaignEvents.OnMapMarkerRemovedEvent.AddNonSerializedListener(this, new Action<MapMarker>(this.OnMapMarkerRemoved));
			this._trackerContainer = new MapTrackerProvider.TrackerContainer();
			this.ResetTrackers();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000121E2 File Offset: 0x000103E2
		private void OnFinalize()
		{
			this._trackerContainer.ClearTrackers();
			CampaignEventDispatcher.Instance.RemoveListeners(this);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000121FA File Offset: 0x000103FA
		public MapTrackerItemVM[] GetTrackers()
		{
			return this._trackerContainer.GetTrackers();
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00012208 File Offset: 0x00010408
		private void ResetTrackers()
		{
			this._trackerContainer.ClearTrackers();
			MBReadOnlyList<MobileParty> all = MobileParty.All;
			for (int i = 0; i < all.Count; i++)
			{
				MobileParty mobileParty = all[i];
				this.AddIfEligible(mobileParty);
			}
			foreach (Army army in Kingdom.All.SelectMany<Kingdom, Army>((Kingdom k) => k.Armies).ToArray<Army>())
			{
				this.AddIfEligible(army);
			}
			if (Campaign.Current.MapMarkerManager != null)
			{
				foreach (MapMarker mapMarker in Campaign.Current.MapMarkerManager.MapMarkers)
				{
					this.AddIfEligible(mapMarker);
				}
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000122F4 File Offset: 0x000104F4
		private bool CanAddMobileParty(MobileParty party)
		{
			if (!party.IsMainParty && !party.IsMilitia && !party.IsGarrison && !party.IsVillager && !party.IsBandit && !party.IsPatrolParty && !party.IsBanditBossParty && !party.IsCurrentlyUsedByAQuest && (!party.IsCaravan || party.CaravanPartyComponent.Owner == Hero.MainHero))
			{
				if (party.IsLordParty)
				{
					for (int i = 0; i < Clan.PlayerClan.WarPartyComponents.Count; i++)
					{
						if (Clan.PlayerClan.WarPartyComponents[i].MobileParty == party)
						{
							return true;
						}
					}
				}
				for (int j = 0; j < Clan.PlayerClan.Heroes.Count; j++)
				{
					Hero hero = Clan.PlayerClan.Heroes[j];
					for (int k = 0; k < hero.OwnedCaravans.Count; k++)
					{
						if (hero.OwnedCaravans[k].MobileParty == party)
						{
							return true;
						}
					}
				}
			}
			return party.LeaderHero == null && party.IsCurrentlyUsedByAQuest && Campaign.Current.VisualTrackerManager.CheckTracked(party);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001241E File Offset: 0x0001061E
		private bool CanAddArmy(Army army)
		{
			return army.Kingdom == Hero.MainHero.MapFaction && !army.Parties.Contains(MobileParty.MainParty);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00012448 File Offset: 0x00010648
		private void RemoveIfExists(ITrackableCampaignObject trackable)
		{
			MapTrackerItemVM trackerFor = this._trackerContainer.GetTrackerFor(trackable);
			if (trackerFor != null)
			{
				this._trackerContainer.RemoveTracker(trackerFor);
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00012471 File Offset: 0x00010671
		private void AddIfEligible(MobileParty party)
		{
			if (this.CanAddMobileParty(party) && !this._trackerContainer.HasTrackerFor(party))
			{
				this._trackerContainer.AddTracker(new MapMobilePartyTrackItemVM(party));
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001249B File Offset: 0x0001069B
		private void AddIfEligible(Army army)
		{
			if (this.CanAddArmy(army) && !this._trackerContainer.HasTrackerFor(army))
			{
				this._trackerContainer.AddTracker(new MapArmyTrackItemVM(army));
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000124C5 File Offset: 0x000106C5
		private void AddIfEligible(MapMarker mapMarker)
		{
			if (!this._trackerContainer.HasTrackerFor(mapMarker))
			{
				this._trackerContainer.AddTracker(new MapMarkerTrackerItemVM(mapMarker));
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000124E6 File Offset: 0x000106E6
		private void OnPartyDestroyed(MobileParty mobileParty, PartyBase arg2)
		{
			this.RemoveIfExists(mobileParty);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000124EF File Offset: 0x000106EF
		private void OnPartyQuestStatusChanged(MobileParty mobileParty, bool isUsedByQuest)
		{
			if (!isUsedByQuest)
			{
				this.AddIfEligible(mobileParty);
				return;
			}
			if (mobileParty.LeaderHero == null && Campaign.Current.VisualTrackerManager.CheckTracked(mobileParty))
			{
				this.AddIfEligible(mobileParty);
				return;
			}
			this.RemoveIfExists(mobileParty);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00012525 File Offset: 0x00010725
		private void OnPartyDisbanded(MobileParty disbandedParty, Settlement relatedSettlement)
		{
			this.RemoveIfExists(disbandedParty);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001252E File Offset: 0x0001072E
		private void OnMobilePartyCreated(MobileParty mobileParty)
		{
			this.AddIfEligible(mobileParty);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00012537 File Offset: 0x00010737
		private void OnArmyDispersed(Army army, Army.ArmyDispersionReason arg2, bool arg3)
		{
			this.RemoveIfExists(army);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00012540 File Offset: 0x00010740
		private void OnArmyCreated(Army army)
		{
			this.AddIfEligible(army);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00012549 File Offset: 0x00010749
		private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification)
		{
			if (clan == Clan.PlayerClan)
			{
				this.ResetTrackers();
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00012559 File Offset: 0x00010759
		private void OnCompanionClanCreated(Clan clan, bool isCompanion)
		{
			if (isCompanion && clan.Leader.PartyBelongedTo != null)
			{
				this.RemoveIfExists(clan.Leader.PartyBelongedTo);
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001257C File Offset: 0x0001077C
		private void OnMapMarkerRemoved(MapMarker marker)
		{
			this.RemoveIfExists(marker);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00012585 File Offset: 0x00010785
		private void OnMapMarkerCreated(MapMarker marker)
		{
			this.AddIfEligible(marker);
		}

		// Token: 0x04000250 RID: 592
		private MapTrackerProvider.TrackerContainer _trackerContainer;

		// Token: 0x020000A4 RID: 164
		private class TrackerContainer
		{
			// Token: 0x060006B4 RID: 1716 RVA: 0x00016FF0 File Offset: 0x000151F0
			public TrackerContainer()
			{
				this._trackers = new Dictionary<ITrackableCampaignObject, MapTrackerItemVM>();
			}

			// Token: 0x060006B5 RID: 1717 RVA: 0x00017003 File Offset: 0x00015203
			public MapTrackerItemVM[] GetTrackers()
			{
				return this._trackers.Values.ToArray<MapTrackerItemVM>();
			}

			// Token: 0x060006B6 RID: 1718 RVA: 0x00017015 File Offset: 0x00015215
			public bool HasTrackerFor(ITrackableCampaignObject trackable)
			{
				return this.GetTrackerFor(trackable) != null;
			}

			// Token: 0x060006B7 RID: 1719 RVA: 0x00017024 File Offset: 0x00015224
			public MapTrackerItemVM GetTrackerFor(ITrackableCampaignObject trackable)
			{
				MapTrackerItemVM mapTrackerItemVM;
				if (this._trackers.TryGetValue(trackable, out mapTrackerItemVM))
				{
					return mapTrackerItemVM;
				}
				return null;
			}

			// Token: 0x060006B8 RID: 1720 RVA: 0x00017044 File Offset: 0x00015244
			public void AddTracker(MapTrackerItemVM tracker)
			{
				if (this._trackers.ContainsKey(tracker.TrackedObject))
				{
					Debug.FailedAssert("Trying to add a tracker that was already added", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\SandBox.ViewModelCollection\\Map\\Tracker\\MapTrackerProvider.cs", "AddTracker", 54);
					return;
				}
				this._trackers.Add(tracker.TrackedObject, tracker);
				MapTrackerProvider.OnTrackerAddedOrRemovedDelegate onTrackerAddedOrRemoved = this.OnTrackerAddedOrRemoved;
				if (onTrackerAddedOrRemoved == null)
				{
					return;
				}
				onTrackerAddedOrRemoved(tracker, true);
			}

			// Token: 0x060006B9 RID: 1721 RVA: 0x000170A0 File Offset: 0x000152A0
			public void RemoveTracker(MapTrackerItemVM tracker)
			{
				if (!this._trackers.ContainsKey(tracker.TrackedObject))
				{
					Debug.FailedAssert("Trying to remove a tracker that was not added", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\SandBox.ViewModelCollection\\Map\\Tracker\\MapTrackerProvider.cs", "RemoveTracker", 66);
					return;
				}
				this._trackers.Remove(tracker.TrackedObject);
				MapTrackerProvider.OnTrackerAddedOrRemovedDelegate onTrackerAddedOrRemoved = this.OnTrackerAddedOrRemoved;
				if (onTrackerAddedOrRemoved == null)
				{
					return;
				}
				onTrackerAddedOrRemoved(tracker, false);
			}

			// Token: 0x060006BA RID: 1722 RVA: 0x000170FC File Offset: 0x000152FC
			public void ClearTrackers()
			{
				MapTrackerItemVM[] array = this._trackers.Values.ToArray<MapTrackerItemVM>();
				for (int i = 0; i < array.Length; i++)
				{
					this.RemoveTracker(array[i]);
				}
			}

			// Token: 0x040003C6 RID: 966
			private readonly Dictionary<ITrackableCampaignObject, MapTrackerItemVM> _trackers;

			// Token: 0x040003C7 RID: 967
			public MapTrackerProvider.OnTrackerAddedOrRemovedDelegate OnTrackerAddedOrRemoved;
		}

		// Token: 0x020000A5 RID: 165
		// (Invoke) Token: 0x060006BC RID: 1724
		public delegate void OnTrackerAddedOrRemovedDelegate(MapTrackerItemVM tracker, bool added);
	}
}
