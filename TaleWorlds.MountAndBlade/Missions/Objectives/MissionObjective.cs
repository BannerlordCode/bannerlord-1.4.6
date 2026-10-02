using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Missions.Objectives
{
	// Token: 0x020003E4 RID: 996
	public abstract class MissionObjective
	{
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060036CD RID: 14029
		public abstract string UniqueId { get; }

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060036CE RID: 14030
		public abstract TextObject Name { get; }

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060036CF RID: 14031
		public abstract TextObject Description { get; }

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060036D0 RID: 14032 RVA: 0x000E30E8 File Offset: 0x000E12E8
		public bool IsActive
		{
			get
			{
				return this.IsStarted && !this.IsCompleted;
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060036D1 RID: 14033 RVA: 0x000E30FD File Offset: 0x000E12FD
		// (set) Token: 0x060036D2 RID: 14034 RVA: 0x000E3105 File Offset: 0x000E1305
		public bool IsStarted { get; private set; }

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060036D3 RID: 14035 RVA: 0x000E310E File Offset: 0x000E130E
		// (set) Token: 0x060036D4 RID: 14036 RVA: 0x000E3116 File Offset: 0x000E1316
		public bool IsCompleted { get; private set; }

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x060036D5 RID: 14037 RVA: 0x000E311F File Offset: 0x000E131F
		// (set) Token: 0x060036D6 RID: 14038 RVA: 0x000E3127 File Offset: 0x000E1327
		public Mission Mission { get; private set; }

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060036D7 RID: 14039 RVA: 0x000E3130 File Offset: 0x000E1330
		// (set) Token: 0x060036D8 RID: 14040 RVA: 0x000E3138 File Offset: 0x000E1338
		public BasicCharacterObject ObjectiveGiver { get; private set; }

		// Token: 0x140000AD RID: 173
		// (add) Token: 0x060036D9 RID: 14041 RVA: 0x000E3144 File Offset: 0x000E1344
		// (remove) Token: 0x060036DA RID: 14042 RVA: 0x000E317C File Offset: 0x000E137C
		public event Action OnUpdated;

		// Token: 0x060036DB RID: 14043 RVA: 0x000E31B1 File Offset: 0x000E13B1
		public MissionObjective(Mission mission)
		{
			this._targets = new MBList<MissionObjectiveTarget>();
			this.Mission = mission;
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x000E31CC File Offset: 0x000E13CC
		internal void Start()
		{
			if (this.IsStarted)
			{
				Debug.FailedAssert("Trying to start an objective that was already started.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Objectives\\MissionObjective.cs", "Start", 38);
				return;
			}
			if (this.IsCompleted)
			{
				Debug.FailedAssert("Trying to start a completed objective. This is not allowed, create a new objective instead.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Objectives\\MissionObjective.cs", "Start", 44);
				return;
			}
			this.IsStarted = true;
			this.OnStart();
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x000E3224 File Offset: 0x000E1424
		internal void Tick(float dt)
		{
			this.CheckNameUpdates();
			this.OnTick(dt);
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x000E3234 File Offset: 0x000E1434
		internal void Complete()
		{
			if (!this.IsStarted)
			{
				Debug.FailedAssert("Trying to complete an objective that was not started yet.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Objectives\\MissionObjective.cs", "Complete", 62);
				return;
			}
			if (this.IsCompleted)
			{
				Debug.FailedAssert("Trying to complete an objective more than once.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Objectives\\MissionObjective.cs", "Complete", 68);
				return;
			}
			this.IsCompleted = true;
			this.OnComplete();
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x000E328C File Offset: 0x000E148C
		private void CheckNameUpdates()
		{
			bool flag = false;
			if (this._cachedName != this.Name)
			{
				this._cachedName = this.Name;
				flag = true;
			}
			if (this._cachedDescription != this.Description)
			{
				this._cachedDescription = this.Description;
				flag = true;
			}
			if (flag)
			{
				Action onUpdated = this.OnUpdated;
				if (onUpdated == null)
				{
					return;
				}
				onUpdated();
			}
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x000E32F0 File Offset: 0x000E14F0
		public virtual MissionObjectiveProgressInfo GetCurrentProgress()
		{
			return default(MissionObjectiveProgressInfo);
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x000E3306 File Offset: 0x000E1506
		internal bool GetIsActivationRequirementsMet()
		{
			return this.IsActivationRequirementsMet();
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x000E330E File Offset: 0x000E150E
		internal bool GetIsCompletionRequirementsMet()
		{
			return this.IsCompletionRequirementsMet();
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x000E3316 File Offset: 0x000E1516
		public void SetObjectiveGiver(BasicCharacterObject objectiveGiver)
		{
			this.ObjectiveGiver = objectiveGiver;
			Action onUpdated = this.OnUpdated;
			if (onUpdated == null)
			{
				return;
			}
			onUpdated();
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x000E3330 File Offset: 0x000E1530
		public void AddTarget(MissionObjectiveTarget target)
		{
			if (target == null)
			{
				Debug.FailedAssert("Cannot add null target to mission objective", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Objectives\\MissionObjective.cs", "AddTarget", 123);
				return;
			}
			if (this._targets.Contains(target))
			{
				Debug.FailedAssert(string.Concat(new string[]
				{
					"Trying to add target (",
					target.GetName().ToString(),
					") twice to mission objective (",
					this.UniqueId,
					")"
				}), "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Objectives\\MissionObjective.cs", "AddTarget", 129);
				return;
			}
			this._targets.Add(target);
			Action onUpdated = this.OnUpdated;
			if (onUpdated == null)
			{
				return;
			}
			onUpdated();
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x000E33D0 File Offset: 0x000E15D0
		public void RemoveTarget(MissionObjectiveTarget target)
		{
			if (target == null)
			{
				Debug.FailedAssert("Cannot remove null target from mission objective", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Objectives\\MissionObjective.cs", "RemoveTarget", 141);
				return;
			}
			if (!this._targets.Contains(target))
			{
				Debug.FailedAssert(string.Concat(new string[]
				{
					"Trying to remove non-existent target (",
					target.GetName().ToString(),
					") from objective (",
					this.UniqueId,
					")"
				}), "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Objectives\\MissionObjective.cs", "RemoveTarget", 147);
				return;
			}
			this._targets.Remove(target);
			Action onUpdated = this.OnUpdated;
			if (onUpdated == null)
			{
				return;
			}
			onUpdated();
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000E3474 File Offset: 0x000E1674
		public void ClearTargets()
		{
			this._targets.Clear();
			Action onUpdated = this.OnUpdated;
			if (onUpdated == null)
			{
				return;
			}
			onUpdated();
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000E3491 File Offset: 0x000E1691
		public MBReadOnlyList<MissionObjectiveTarget> GetTargetsCopy()
		{
			return this._targets.ToMBList<MissionObjectiveTarget>();
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000E34A0 File Offset: 0x000E16A0
		protected MBReadOnlyList<TTarget> GetTargetsCopy<TTarget>() where TTarget : MissionObjectiveTarget
		{
			MBList<TTarget> mblist = new MBList<TTarget>();
			for (int i = 0; i < this._targets.Count; i++)
			{
				TTarget ttarget;
				if ((ttarget = this._targets[i] as TTarget) != null)
				{
					mblist.Add(ttarget);
				}
			}
			return mblist;
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x000E34F0 File Offset: 0x000E16F0
		protected virtual bool IsActivationRequirementsMet()
		{
			return true;
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x000E34F3 File Offset: 0x000E16F3
		protected virtual bool IsCompletionRequirementsMet()
		{
			return false;
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000E34F6 File Offset: 0x000E16F6
		protected virtual void OnStart()
		{
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000E34F8 File Offset: 0x000E16F8
		protected virtual void OnComplete()
		{
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x000E34FA File Offset: 0x000E16FA
		protected virtual void OnTick(float dt)
		{
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x000E34FC File Offset: 0x000E16FC
		protected virtual void OnTargetAdded(MissionObjectiveTarget target)
		{
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x000E34FE File Offset: 0x000E16FE
		protected virtual void OnTargetRemoved(MissionObjectiveTarget target)
		{
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x000E3500 File Offset: 0x000E1700
		protected virtual void OnTargetsCleared()
		{
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x000E3504 File Offset: 0x000E1704
		public static MissionObjective.GenericMissionObjectiveBuilder CreateGenericObjectiveBuilder(Mission mission, string id, TextObject name = null, TextObject description = null)
		{
			GenericMissionObjective genericMissionObjective = new GenericMissionObjective(mission, id, name, description);
			return new MissionObjective.GenericMissionObjectiveBuilder
			{
				Objective = genericMissionObjective
			};
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x000E352C File Offset: 0x000E172C
		public static MissionObjective.GenericMissionObjectiveTargetBuilder<T> CreateGenericTargetBuilder<T>(T target, TextObject name, Vec3 staticPosition)
		{
			GenericMissionObjectiveTarget<T> genericMissionObjectiveTarget = new GenericMissionObjectiveTarget<T>(target);
			genericMissionObjectiveTarget.Name = name;
			genericMissionObjectiveTarget.StaticPosition = staticPosition;
			return new MissionObjective.GenericMissionObjectiveTargetBuilder<T>
			{
				Target = genericMissionObjectiveTarget
			};
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000E355F File Offset: 0x000E175F
		public static MissionObjective.GenericMissionObjectiveTargetBuilder<T> CreateGenericTargetBuilder<T>(T target)
		{
			return MissionObjective.CreateGenericTargetBuilder<T>(target, null, Vec3.Invalid);
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x000E356D File Offset: 0x000E176D
		public static MissionObjective.GenericMissionObjectiveTargetBuilder<T> CreateGenericTargetBuilder<T>(T target, TextObject name)
		{
			return MissionObjective.CreateGenericTargetBuilder<T>(target, name, Vec3.Invalid);
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x000E357B File Offset: 0x000E177B
		public static MissionObjective.GenericMissionObjectiveTargetBuilder<T> CreateGenericTargetBuilder<T>(T target, Vec3 staticPosition)
		{
			return MissionObjective.CreateGenericTargetBuilder<T>(target, null, staticPosition);
		}

		// Token: 0x040017A3 RID: 6051
		private MBList<MissionObjectiveTarget> _targets;

		// Token: 0x040017A5 RID: 6053
		private TextObject _cachedName;

		// Token: 0x040017A6 RID: 6054
		private TextObject _cachedDescription;

		// Token: 0x02000696 RID: 1686
		public struct GenericMissionObjectiveBuilder
		{
			// Token: 0x0600419A RID: 16794 RVA: 0x000FC007 File Offset: 0x000FA207
			public MissionObjective.GenericMissionObjectiveBuilder SetName(TextObject name)
			{
				this.Objective.IName = name;
				return this;
			}

			// Token: 0x0600419B RID: 16795 RVA: 0x000FC01B File Offset: 0x000FA21B
			public MissionObjective.GenericMissionObjectiveBuilder SetDescription(TextObject description)
			{
				this.Objective.IDescription = description;
				return this;
			}

			// Token: 0x0600419C RID: 16796 RVA: 0x000FC02F File Offset: 0x000FA22F
			public MissionObjective.GenericMissionObjectiveBuilder SetObjectiveGiver(BasicCharacterObject objectiveGiver)
			{
				this.Objective.SetObjectiveGiver(objectiveGiver);
				return this;
			}

			// Token: 0x0600419D RID: 16797 RVA: 0x000FC044 File Offset: 0x000FA244
			public MissionObjective.GenericMissionObjectiveBuilder SetInitialTargets(params MissionObjectiveTarget[] targets)
			{
				this.Objective.ClearTargets();
				if (targets != null)
				{
					for (int i = 0; i < targets.Length; i++)
					{
						this.Objective.AddTarget(targets[i]);
					}
				}
				return this;
			}

			// Token: 0x0600419E RID: 16798 RVA: 0x000FC081 File Offset: 0x000FA281
			public MissionObjective.GenericMissionObjectiveBuilder SetIsActivationRequirementsMetCallback(Func<MissionObjective, bool> callback)
			{
				this.Objective.IsActivationRequirementsMetCallback = callback;
				return this;
			}

			// Token: 0x0600419F RID: 16799 RVA: 0x000FC095 File Offset: 0x000FA295
			public MissionObjective.GenericMissionObjectiveBuilder SetIsCompletionRequirementsMetCallback(Func<MissionObjective, bool> callback)
			{
				this.Objective.IsCompletionRequirementsMetCallback = callback;
				return this;
			}

			// Token: 0x060041A0 RID: 16800 RVA: 0x000FC0A9 File Offset: 0x000FA2A9
			public MissionObjective.GenericMissionObjectiveBuilder SetOnStartCallback(Action<MissionObjective> callback)
			{
				this.Objective.OnStartCallback = callback;
				return this;
			}

			// Token: 0x060041A1 RID: 16801 RVA: 0x000FC0BD File Offset: 0x000FA2BD
			public MissionObjective.GenericMissionObjectiveBuilder SetOnCompleteCallback(Action<MissionObjective> callback)
			{
				this.Objective.OnCompleteCallback = callback;
				return this;
			}

			// Token: 0x060041A2 RID: 16802 RVA: 0x000FC0D1 File Offset: 0x000FA2D1
			public MissionObjective.GenericMissionObjectiveBuilder SetOnTickCallback(Action<MissionObjective, float> callback)
			{
				this.Objective.OnTickCallback = callback;
				return this;
			}

			// Token: 0x060041A3 RID: 16803 RVA: 0x000FC0E5 File Offset: 0x000FA2E5
			public MissionObjective.GenericMissionObjectiveBuilder SetProgressCallback(Func<MissionObjective, MissionObjectiveProgressInfo> callback)
			{
				this.Objective.GetProgressCallback = callback;
				return this;
			}

			// Token: 0x060041A4 RID: 16804 RVA: 0x000FC0F9 File Offset: 0x000FA2F9
			public MissionObjective Build()
			{
				return this.Objective;
			}

			// Token: 0x040022C3 RID: 8899
			internal GenericMissionObjective Objective;
		}

		// Token: 0x02000697 RID: 1687
		public struct GenericMissionObjectiveTargetBuilder<T>
		{
			// Token: 0x060041A5 RID: 16805 RVA: 0x000FC101 File Offset: 0x000FA301
			public MissionObjective.GenericMissionObjectiveTargetBuilder<T> SetIsActiveCallback(Func<T, bool> callback)
			{
				this.Target.IsActiveCallback = callback;
				return this;
			}

			// Token: 0x060041A6 RID: 16806 RVA: 0x000FC115 File Offset: 0x000FA315
			public MissionObjective.GenericMissionObjectiveTargetBuilder<T> SetGetGlobalPositionCallback(Func<T, Vec3> callback)
			{
				this.Target.GetGlobalPositionCallback = callback;
				return this;
			}

			// Token: 0x060041A7 RID: 16807 RVA: 0x000FC129 File Offset: 0x000FA329
			public MissionObjective.GenericMissionObjectiveTargetBuilder<T> SetGetNameCallback(Func<T, TextObject> callback)
			{
				this.Target.GetNameCallback = callback;
				return this;
			}

			// Token: 0x060041A8 RID: 16808 RVA: 0x000FC13D File Offset: 0x000FA33D
			public MissionObjectiveTarget<T> Build()
			{
				return this.Target;
			}

			// Token: 0x040022C4 RID: 8900
			internal GenericMissionObjectiveTarget<T> Target;
		}
	}
}
