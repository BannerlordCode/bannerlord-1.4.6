using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Missions.NameMarker
{
	// Token: 0x02000034 RID: 52
	public class MissionNameMarkerVM : ViewModel
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00010551 File Offset: 0x0000E751
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00010559 File Offset: 0x0000E759
		public bool IsTargetsAdded { get; private set; }

		// Token: 0x060003F8 RID: 1016 RVA: 0x00010562 File Offset: 0x0000E762
		public MissionNameMarkerVM(List<MissionNameMarkerProvider> providers, Camera missionCamera)
		{
			this.Targets = new MBBindingList<MissionNameMarkerTargetBaseVM>();
			this._providers = providers;
			this._distanceComparer = new MissionNameMarkerVM.MarkerDistanceComparer();
			this._missionCamera = missionCamera;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001058E File Offset: 0x0000E78E
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Targets.ApplyActionOnAllItems(delegate(MissionNameMarkerTargetBaseVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000105C0 File Offset: 0x0000E7C0
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.Targets.ApplyActionOnAllItems(delegate(MissionNameMarkerTargetBaseVM x)
			{
				x.OnFinalize();
			});
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000105F4 File Offset: 0x0000E7F4
		public void Tick(float dt)
		{
			if (!this.IsTargetsAdded)
			{
				List<MissionNameMarkerTargetBaseVM> list = new List<MissionNameMarkerTargetBaseVM>();
				for (int i = 0; i < this._providers.Count; i++)
				{
					this._providers[i].CreateMarkers(list);
				}
				MBReadOnlyList<MissionNameMarkerTargetBaseVM> mbreadOnlyList;
				MBReadOnlyList<MissionNameMarkerTargetBaseVM> mbreadOnlyList2;
				MissionNameMarkerVM.GetTargetDifferences(this.Targets, list, out mbreadOnlyList, out mbreadOnlyList2);
				for (int j = 0; j < mbreadOnlyList.Count; j++)
				{
					this.Targets.Remove(mbreadOnlyList[j]);
				}
				for (int k = 0; k < mbreadOnlyList2.Count; k++)
				{
					this.Targets.Add(mbreadOnlyList2[k]);
				}
				this.IsTargetsAdded = true;
			}
			if (this.IsEnabled)
			{
				this.UpdateTargetScreenPositions(false);
				this._fadeOutTimerStarted = false;
				this._fadeOutTimer = 0f;
				this._prevEnabledState = this.IsEnabled;
			}
			else
			{
				if (this._prevEnabledState)
				{
					this._fadeOutTimerStarted = true;
				}
				if (this._fadeOutTimerStarted)
				{
					this._fadeOutTimer += dt;
				}
				if (this._fadeOutTimer >= 2f)
				{
					this._fadeOutTimerStarted = false;
				}
				this.UpdateTargetScreenPositions(this._fadeOutTimer < 2f);
			}
			this._prevEnabledState = this.IsEnabled;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001072C File Offset: 0x0000E92C
		private static void GetTargetDifferences(IList<MissionNameMarkerTargetBaseVM> currentTargets, IList<MissionNameMarkerTargetBaseVM> newTargets, out MBReadOnlyList<MissionNameMarkerTargetBaseVM> removedTargets, out MBReadOnlyList<MissionNameMarkerTargetBaseVM> addedTargets)
		{
			MBList<MissionNameMarkerTargetBaseVM> mblist = new MBList<MissionNameMarkerTargetBaseVM>();
			MBList<MissionNameMarkerTargetBaseVM> mblist2 = new MBList<MissionNameMarkerTargetBaseVM>();
			for (int i = 0; i < currentTargets.Count; i++)
			{
				MissionNameMarkerTargetBaseVM missionNameMarkerTargetBaseVM = currentTargets[i];
				bool flag = true;
				for (int j = 0; j < newTargets.Count; j++)
				{
					MissionNameMarkerTargetBaseVM missionNameMarkerTargetBaseVM2 = newTargets[j];
					if (missionNameMarkerTargetBaseVM.Equals(missionNameMarkerTargetBaseVM2))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					mblist.Add(missionNameMarkerTargetBaseVM);
				}
			}
			for (int k = 0; k < newTargets.Count; k++)
			{
				MissionNameMarkerTargetBaseVM missionNameMarkerTargetBaseVM3 = newTargets[k];
				bool flag2 = true;
				for (int l = 0; l < currentTargets.Count; l++)
				{
					if (currentTargets[l].Equals(missionNameMarkerTargetBaseVM3))
					{
						flag2 = false;
						break;
					}
				}
				if (flag2)
				{
					mblist2.Add(missionNameMarkerTargetBaseVM3);
				}
			}
			removedTargets = mblist;
			addedTargets = mblist2;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000107F9 File Offset: 0x0000E9F9
		public void SetTargetsDirty()
		{
			this.IsTargetsAdded = false;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00010804 File Offset: 0x0000EA04
		private void UpdateTargetScreenPositions(bool forceUpdate)
		{
			for (int i = 0; i < this.Targets.Count; i++)
			{
				MissionNameMarkerTargetBaseVM missionNameMarkerTargetBaseVM = this.Targets[i];
				if (missionNameMarkerTargetBaseVM.IsEnabled || forceUpdate)
				{
					missionNameMarkerTargetBaseVM.UpdatePosition(this._missionCamera);
				}
			}
			this.Targets.Sort(this._distanceComparer);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001085C File Offset: 0x0000EA5C
		private void UpdateTargetStates(bool state)
		{
			foreach (MissionNameMarkerTargetBaseVM missionNameMarkerTargetBaseVM in this.Targets)
			{
				missionNameMarkerTargetBaseVM.SetEnabledState(state);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x000108A8 File Offset: 0x0000EAA8
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x000108B0 File Offset: 0x0000EAB0
		[DataSourceProperty]
		public MBBindingList<MissionNameMarkerTargetBaseVM> Targets
		{
			get
			{
				return this._targets;
			}
			set
			{
				if (value != this._targets)
				{
					this._targets = value;
					base.OnPropertyChangedWithValue<MBBindingList<MissionNameMarkerTargetBaseVM>>(value, "Targets");
				}
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x000108CE File Offset: 0x0000EACE
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x000108D6 File Offset: 0x0000EAD6
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
					this.UpdateTargetStates(value);
					Game.Current.EventManager.TriggerEvent<MissionNameMarkerToggleEvent>(new MissionNameMarkerToggleEvent(value));
				}
			}
		}

		// Token: 0x0400020E RID: 526
		private readonly Camera _missionCamera;

		// Token: 0x0400020F RID: 527
		private bool _prevEnabledState;

		// Token: 0x04000210 RID: 528
		private bool _fadeOutTimerStarted;

		// Token: 0x04000211 RID: 529
		private float _fadeOutTimer;

		// Token: 0x04000212 RID: 530
		private readonly MissionNameMarkerVM.MarkerDistanceComparer _distanceComparer;

		// Token: 0x04000213 RID: 531
		private readonly List<MissionNameMarkerProvider> _providers;

		// Token: 0x04000214 RID: 532
		private MBBindingList<MissionNameMarkerTargetBaseVM> _targets;

		// Token: 0x04000215 RID: 533
		private bool _isEnabled;

		// Token: 0x0200009D RID: 157
		private class MarkerDistanceComparer : IComparer<MissionNameMarkerTargetBaseVM>
		{
			// Token: 0x060006A6 RID: 1702 RVA: 0x00016F38 File Offset: 0x00015138
			public int Compare(MissionNameMarkerTargetBaseVM x, MissionNameMarkerTargetBaseVM y)
			{
				return y.Distance.CompareTo(x.Distance);
			}
		}
	}
}
