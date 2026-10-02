using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200037C RID: 892
	public abstract class UsableMachine : SynchedMissionObject, IFocusable, IOrderable, IDetachment
	{
		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x060032BA RID: 12986 RVA: 0x000D0A9C File Offset: 0x000CEC9C
		// (set) Token: 0x060032BB RID: 12987 RVA: 0x000D0AA4 File Offset: 0x000CECA4
		public MBList<StandingPoint> StandingPoints { get; private set; }

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x060032BC RID: 12988 RVA: 0x000D0AAD File Offset: 0x000CECAD
		// (set) Token: 0x060032BD RID: 12989 RVA: 0x000D0AB5 File Offset: 0x000CECB5
		public StandingPoint PilotStandingPoint { get; private set; }

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x060032BE RID: 12990 RVA: 0x000D0ABE File Offset: 0x000CECBE
		// (set) Token: 0x060032BF RID: 12991 RVA: 0x000D0AC6 File Offset: 0x000CECC6
		public int PilotStandingPointSlotIndex { get; private set; }

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x000D0ACF File Offset: 0x000CECCF
		// (set) Token: 0x060032C1 RID: 12993 RVA: 0x000D0AD7 File Offset: 0x000CECD7
		protected internal List<StandingPoint> AmmoPickUpPoints { get; private set; }

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x060032C2 RID: 12994 RVA: 0x000D0AE0 File Offset: 0x000CECE0
		// (set) Token: 0x060032C3 RID: 12995 RVA: 0x000D0AE8 File Offset: 0x000CECE8
		private protected List<GameEntity> WaitStandingPoints { protected get; private set; }

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x060032C4 RID: 12996 RVA: 0x000D0AF1 File Offset: 0x000CECF1
		// (set) Token: 0x060032C5 RID: 12997 RVA: 0x000D0AF9 File Offset: 0x000CECF9
		public DestructableComponent DestructionComponent { get; private set; }

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x060032C6 RID: 12998 RVA: 0x000D0B02 File Offset: 0x000CED02
		public bool IsDestructible
		{
			get
			{
				return this.DestructionComponent != null;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x060032C7 RID: 12999 RVA: 0x000D0B0D File Offset: 0x000CED0D
		public bool IsDestroyed
		{
			get
			{
				return this.DestructionComponent != null && this.DestructionComponent.IsDestroyed;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x060032C8 RID: 13000 RVA: 0x000D0B24 File Offset: 0x000CED24
		// (set) Token: 0x060032C9 RID: 13001 RVA: 0x000D0B2C File Offset: 0x000CED2C
		private protected bool IsDetachmentRecentlyEvaluated { protected get; private set; }

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x060032CA RID: 13002 RVA: 0x000D0B35 File Offset: 0x000CED35
		public Agent PilotAgent
		{
			get
			{
				StandingPoint pilotStandingPoint = this.PilotStandingPoint;
				if (pilotStandingPoint == null)
				{
					return null;
				}
				return pilotStandingPoint.UserAgent;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x060032CB RID: 13003 RVA: 0x000D0B48 File Offset: 0x000CED48
		public bool IsLoose
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x060032CC RID: 13004 RVA: 0x000D0B4C File Offset: 0x000CED4C
		public virtual float SinkingReferenceOffset
		{
			get
			{
				return base.GameEntity.GetGlobalScale().z * 0.5f;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x060032CD RID: 13005 RVA: 0x000D0B72 File Offset: 0x000CED72
		public UsableMachineAIBase Ai
		{
			get
			{
				if (this._ai == null)
				{
					this._ai = this.CreateAIBehaviorObject();
				}
				return this._ai;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060032CE RID: 13006 RVA: 0x000D0B8E File Offset: 0x000CED8E
		public virtual FocusableObjectType FocusableObjectType
		{
			get
			{
				return FocusableObjectType.Item;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x060032CF RID: 13007 RVA: 0x000D0B91 File Offset: 0x000CED91
		public virtual bool IsFocusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x060032D0 RID: 13008 RVA: 0x000D0B94 File Offset: 0x000CED94
		// (set) Token: 0x060032D1 RID: 13009 RVA: 0x000D0B9C File Offset: 0x000CED9C
		public StandingPoint CurrentlyUsedAmmoPickUpPoint
		{
			get
			{
				return this._currentlyUsedAmmoPickUpPoint;
			}
			set
			{
				this._currentlyUsedAmmoPickUpPoint = value;
				base.SetScriptComponentToTick(this.GetTickRequirement());
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x060032D2 RID: 13010 RVA: 0x000D0BB1 File Offset: 0x000CEDB1
		public bool HasAIPickingUpAmmo
		{
			get
			{
				return this.CurrentlyUsedAmmoPickUpPoint != null;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x060032D3 RID: 13011 RVA: 0x000D0BBC File Offset: 0x000CEDBC
		// (set) Token: 0x060032D4 RID: 13012 RVA: 0x000D0BC4 File Offset: 0x000CEDC4
		public bool IsDisabledForAI { get; protected set; }

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x060032D5 RID: 13013 RVA: 0x000D0BCD File Offset: 0x000CEDCD
		public MBReadOnlyList<Formation> UserFormations
		{
			get
			{
				return this._userFormations;
			}
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x000D0BD8 File Offset: 0x000CEDD8
		protected UsableMachine()
		{
			this._components = new List<UsableMissionObjectComponent>();
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x000D0C30 File Offset: 0x000CEE30
		public void AddComponent(UsableMissionObjectComponent component)
		{
			this._components.Add(component);
			component.OnAdded(base.Scene);
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x000D0C56 File Offset: 0x000CEE56
		public void RemoveComponent(UsableMissionObjectComponent component)
		{
			component.OnRemoved();
			this._components.Remove(component);
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000D0C78 File Offset: 0x000CEE78
		public T GetComponent<T>() where T : UsableMissionObjectComponent
		{
			using (List<UsableMissionObjectComponent>.Enumerator enumerator = this._components.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					T t;
					if ((t = enumerator.Current as T) != null)
					{
						return t;
					}
				}
			}
			return default(T);
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000D0CE8 File Offset: 0x000CEEE8
		public virtual OrderType GetOrder(BattleSideEnum side)
		{
			return OrderType.Use;
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x000D0CEC File Offset: 0x000CEEEC
		public virtual UsableMachineAIBase CreateAIBehaviorObject()
		{
			return null;
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000D0CF0 File Offset: 0x000CEEF0
		public WeakGameEntity GetValidVacantReachableStandingPointForAgent(Agent agent)
		{
			float num = float.MaxValue;
			StandingPoint standingPoint = null;
			foreach (StandingPoint standingPoint2 in this.StandingPoints)
			{
				if (!standingPoint2.IsDisabledForAgent(agent) && (!standingPoint2.HasUser || standingPoint2.HasAIUser))
				{
					WorldFrame worldFrame = standingPoint2.GetUserFrameForAgent(agent);
					float num2 = worldFrame.Origin.AsVec2.DistanceSquared(agent.Position.AsVec2);
					float num3;
					if (standingPoint2.UseOwnPositionInsteadOfWorldPosition)
					{
						num3 = standingPoint2.GameEntity.GlobalPosition.z;
					}
					else
					{
						worldFrame = standingPoint2.GetUserFrameForAgent(agent);
						num3 = worldFrame.Origin.GetGroundVec3().z;
					}
					float num4 = num3;
					if (agent.CanReachAndUseObject(standingPoint2, num2) && num2 < num && MathF.Abs(num4 - agent.Position.z) < 1.5f)
					{
						num = num2;
						standingPoint = standingPoint2;
					}
				}
			}
			if (standingPoint == null)
			{
				return WeakGameEntity.Invalid;
			}
			return standingPoint.GameEntity;
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000D0E10 File Offset: 0x000CF010
		public void SetAI(UsableMachineAIBase ai)
		{
			this._ai = ai;
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000D0E1C File Offset: 0x000CF01C
		public WeakGameEntity GetValidStandingPointForAgentWithoutDistanceCheck(Agent agent)
		{
			float num = float.MaxValue;
			StandingPoint standingPoint = null;
			foreach (StandingPoint standingPoint2 in this.StandingPoints)
			{
				if (!standingPoint2.IsDisabledForAgent(agent) && (!standingPoint2.HasUser || standingPoint2.HasAIUser))
				{
					WorldFrame worldFrame = standingPoint2.GetUserFrameForAgent(agent);
					float num2 = worldFrame.Origin.AsVec2.DistanceSquared(agent.Position.AsVec2);
					if (num2 < num)
					{
						worldFrame = standingPoint2.GetUserFrameForAgent(agent);
						if (MathF.Abs(worldFrame.Origin.GetGroundVec3().z - agent.Position.z) < 1.5f)
						{
							num = num2;
							standingPoint = standingPoint2;
						}
					}
				}
			}
			if (standingPoint == null)
			{
				return WeakGameEntity.Invalid;
			}
			return standingPoint.GameEntity;
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000D0F0C File Offset: 0x000CF10C
		public StandingPoint GetVacantStandingPointForAI(Agent agent)
		{
			if (this.PilotStandingPoint != null && !this.PilotStandingPoint.IsDisabledForAgent(agent) && !this.AmmoPickUpPoints.Contains(this.PilotStandingPoint))
			{
				return this.PilotStandingPoint;
			}
			float num = 100000000f;
			StandingPoint standingPoint = null;
			foreach (StandingPoint standingPoint2 in this.StandingPoints)
			{
				bool flag = true;
				if (this.AmmoPickUpPoints.Contains(standingPoint2))
				{
					foreach (StandingPoint standingPoint3 in this.StandingPoints)
					{
						if (standingPoint3 is StandingPointWithWeaponRequirement && !this.AmmoPickUpPoints.Contains(standingPoint3) && (standingPoint3.IsDeactivated || standingPoint3.HasUser || standingPoint3.HasAIMovingTo))
						{
							flag = false;
							break;
						}
					}
				}
				if (flag && !standingPoint2.IsDisabledForAgent(agent))
				{
					float num2 = (agent.Position - standingPoint2.GetUserFrameForAgent(agent).Origin.GetGroundVec3()).LengthSquared;
					if (!standingPoint2.IsDisabledForPlayers)
					{
						num2 -= 100000f;
					}
					if (num2 < num)
					{
						num = num2;
						standingPoint = standingPoint2;
					}
				}
			}
			return standingPoint;
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000D1074 File Offset: 0x000CF274
		public StandingPoint GetTargetStandingPointOfAIAgent(Agent agent)
		{
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				if (standingPoint.IsAIMovingTo(agent))
				{
					return standingPoint;
				}
			}
			return null;
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000D10D0 File Offset: 0x000CF2D0
		public override void OnMissionEnded()
		{
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				Agent userAgent = standingPoint.UserAgent;
				if (userAgent != null)
				{
					userAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
				}
				standingPoint.IsDeactivated = true;
			}
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000D1134 File Offset: 0x000CF334
		public override void SetVisibleSynched(bool value, bool forceChildrenVisible = false)
		{
			base.SetVisibleSynched(value, forceChildrenVisible);
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000D1140 File Offset: 0x000CF340
		public override void SetPhysicsStateSynched(bool value, bool setChildren = true)
		{
			base.SetPhysicsStateSynched(value, setChildren);
			this.SetAbilityOfFaces(value);
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				standingPoint.OnParentMachinePhysicsStateChanged();
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x060032E4 RID: 13028 RVA: 0x000D11A0 File Offset: 0x000CF3A0
		public int UserCountNotInStruckAction
		{
			get
			{
				int num = 0;
				foreach (StandingPoint standingPoint in this.StandingPoints)
				{
					if (standingPoint.HasUser && !standingPoint.UserAgent.IsInBeingStruckAction)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x060032E5 RID: 13029 RVA: 0x000D1208 File Offset: 0x000CF408
		public int UserCountIncludingInStruckAction
		{
			get
			{
				int num = 0;
				using (List<StandingPoint>.Enumerator enumerator = this.StandingPoints.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.HasUser)
						{
							num++;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060032E6 RID: 13030 RVA: 0x000D1264 File Offset: 0x000CF464
		public virtual int MaxUserCount
		{
			get
			{
				return this.StandingPoints.Count;
			}
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x000D1271 File Offset: 0x000CF471
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this.CollectAndSetStandingPoints();
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000D1280 File Offset: 0x000CF480
		protected internal override void OnInit()
		{
			base.OnInit();
			this.IsDisabledForAttackerAIDueToEnemyInRange = new QueryData<bool>(delegate
			{
				bool flag = false;
				if (this.EnemyRangeToStopUsing > 0f && base.GameEntity != null)
				{
					ref MatrixFrame ptr = ref base.GameEntity.GetGlobalFrame();
					Vec3 vec = new Vec3(this.MachinePositionOffsetToStopUsingLocal, 0f, -1f);
					Vec3 vec2 = ptr.rotation.TransformToParent(in vec);
					Vec3 vec3 = base.GameEntity.GlobalPosition + vec2;
					Agent closestEnemyAgent = Mission.Current.GetClosestEnemyAgent(Mission.Current.Teams.Attacker, vec3, this.EnemyRangeToStopUsing);
					flag = closestEnemyAgent != null && closestEnemyAgent.Position.z > vec3.z - 2f && closestEnemyAgent.Position.z < vec3.z + 4f;
				}
				return flag;
			}, 1f);
			this.IsDisabledForDefenderAIDueToEnemyInRange = new QueryData<bool>(delegate
			{
				bool flag2 = false;
				if (this.EnemyRangeToStopUsing > 0f && base.GameEntity != null)
				{
					ref MatrixFrame ptr2 = ref base.GameEntity.GetGlobalFrame();
					Vec3 vec4 = new Vec3(this.MachinePositionOffsetToStopUsingLocal, 0f, -1f);
					Vec3 vec5 = ptr2.rotation.TransformToParent(in vec4);
					Vec3 vec6 = base.GameEntity.GlobalPosition + vec5;
					Agent closestEnemyAgent2 = Mission.Current.GetClosestEnemyAgent(Mission.Current.Teams.Defender, vec6, this.EnemyRangeToStopUsing);
					flag2 = closestEnemyAgent2 != null && closestEnemyAgent2.Position.z > vec6.z - 2f && closestEnemyAgent2.Position.z < vec6.z + 4f;
				}
				return flag2;
			}, 1f);
			this.CollectAndSetStandingPoints();
			this.AmmoPickUpPoints = new List<StandingPoint>();
			this.DestructionComponent = base.GameEntity.GetFirstScriptOfType<DestructableComponent>();
			this.PilotStandingPoint = null;
			for (int i = 0; i < this.StandingPoints.Count; i++)
			{
				StandingPoint standingPoint = this.StandingPoints[i];
				if (standingPoint.GameEntity.HasTag(this.PilotStandingPointTag))
				{
					this.PilotStandingPoint = standingPoint;
					this.PilotStandingPointSlotIndex = i;
				}
				if (standingPoint.GameEntity.HasTag(this.AmmoPickUpTag))
				{
					this.AmmoPickUpPoints.Add(standingPoint);
				}
				standingPoint.InitializeDefendingAgents();
			}
			this.WaitStandingPoints = TaleWorlds.Engine.GameEntity.CreateFromWeakEntity(base.GameEntity).CollectChildrenEntitiesWithTag(this.WaitStandingPointTag);
			if (this.WaitStandingPoints.Count > 0)
			{
				this.ActiveWaitStandingPoint = this.WaitStandingPoints[0];
			}
			this._userFormations = new MBList<Formation>();
			this.UsableStandingPoints = new List<ValueTuple<int, StandingPoint>>();
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000D13C4 File Offset: 0x000CF5C4
		private void CollectAndSetStandingPoints()
		{
			if (base.GameEntity.Parent.IsValid && base.GameEntity.Parent.HasTag("machine_parent"))
			{
				this.StandingPoints = base.GameEntity.Parent.CollectScriptComponentsIncludingChildrenRecursive<StandingPoint>();
				return;
			}
			this.StandingPoints = base.GameEntity.CollectScriptComponentsIncludingChildrenRecursive<StandingPoint>();
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000D1434 File Offset: 0x000CF634
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			bool flag = false;
			using (List<UsableMissionObjectComponent>.Enumerator enumerator = this._components.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsOnTickRequired())
					{
						flag = true;
						break;
					}
				}
			}
			if (base.GameEntity.IsVisibleIncludeParents() && (flag || (!GameNetwork.IsClientOrReplay && this.HasAIPickingUpAmmo) || base.GameEntity.BodyFlag.HasAnyFlag(BodyFlags.Sinking)))
			{
				return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick;
			}
			return base.GetTickRequirement();
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x000D14D8 File Offset: 0x000CF6D8
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (this.MakeVisibilityCheck && !base.GameEntity.IsVisibleIncludeParents())
			{
				return;
			}
			if (base.GameEntity.BodyFlag.HasAnyFlag(BodyFlags.Sinking) && base.GameEntity.GetGlobalFrame().origin.z + this.SinkingReferenceOffset < base.Scene.GetWaterLevelAtPosition(base.GameEntity.GetFrame().origin.AsVec2, !GameNetwork.IsMultiplayer, false))
			{
				this.Disable();
			}
			if (!GameNetwork.IsClientOrReplay && this.HasAIPickingUpAmmo && !this.CurrentlyUsedAmmoPickUpPoint.HasAIMovingTo && !this.CurrentlyUsedAmmoPickUpPoint.HasAIUser)
			{
				this.CurrentlyUsedAmmoPickUpPoint = null;
			}
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnTick(dt);
			}
			bool isClientOrReplay = GameNetwork.IsClientOrReplay;
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x000D15F0 File Offset: 0x000CF7F0
		private static string DebugGetMemberNameOf<T>(object instance, T sp) where T : class
		{
			Type type = instance.GetType();
			foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (!(propertyInfo.GetMethod == null))
				{
					if (propertyInfo.GetValue(instance) == sp)
					{
						return propertyInfo.Name;
					}
					IReadOnlyList<StandingPoint> readOnlyList;
					if (propertyInfo.GetType().IsGenericType && (propertyInfo.GetType().GetGenericTypeDefinition() == typeof(List<>) || propertyInfo.GetType().GetGenericTypeDefinition() == typeof(MBList<>) || propertyInfo.GetType().GetGenericTypeDefinition() == typeof(MBReadOnlyList<>)) && (readOnlyList = propertyInfo.GetValue(instance) as IReadOnlyList<StandingPoint>) != null)
					{
						for (int j = 0; j < readOnlyList.Count; j++)
						{
							StandingPoint standingPoint = readOnlyList[j];
							if (sp == standingPoint)
							{
								return string.Concat(new object[] { propertyInfo.Name, "[", j, "]" });
							}
						}
					}
				}
			}
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (fieldInfo.GetValue(instance) == sp)
				{
					return fieldInfo.Name;
				}
				IReadOnlyList<StandingPoint> readOnlyList2;
				if (fieldInfo.FieldType.IsGenericType && (fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>) || fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(MBList<>) || fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(MBReadOnlyList<>)) && (readOnlyList2 = fieldInfo.GetValue(instance) as IReadOnlyList<StandingPoint>) != null)
				{
					for (int k = 0; k < readOnlyList2.Count; k++)
					{
						StandingPoint standingPoint2 = readOnlyList2[k];
						if (sp == standingPoint2)
						{
							return string.Concat(new object[] { fieldInfo.Name, "[", k, "]" });
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x000D1830 File Offset: 0x000CFA30
		[Conditional("_RGL_KEEP_ASSERTS")]
		protected virtual void DebugTick(float dt)
		{
			if (MBDebug.IsDisplayingHighLevelAI)
			{
				foreach (StandingPoint standingPoint in this.StandingPoints)
				{
					Vec3 globalPosition = standingPoint.GameEntity.GlobalPosition;
					Vec3.One / 3f;
					bool isDeactivated = standingPoint.IsDeactivated;
				}
			}
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x000D18A8 File Offset: 0x000CFAA8
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnEditorTick(dt);
			}
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x000D1900 File Offset: 0x000CFB00
		protected internal override void OnEditorValidate()
		{
			base.OnEditorValidate();
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnEditorValidate();
			}
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x000D1958 File Offset: 0x000CFB58
		public virtual void OnFocusGain(Agent userAgent)
		{
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnFocusGain(userAgent);
			}
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x000D19AC File Offset: 0x000CFBAC
		public virtual void OnFocusLose(Agent userAgent)
		{
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnFocusLose(userAgent);
			}
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x000D1A00 File Offset: 0x000CFC00
		public virtual void OnPilotAssignedDuringSpawn()
		{
			Debug.FailedAssert("This method must have been overridden", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Usables\\UsableMachine.cs", "OnPilotAssignedDuringSpawn", 615);
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x000D1A1B File Offset: 0x000CFC1B
		public virtual TextObject GetInfoTextForBeingNotInteractable(Agent userAgent)
		{
			return null;
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060032F4 RID: 13044 RVA: 0x000D1A1E File Offset: 0x000CFC1E
		public virtual bool HasWaitFrame
		{
			get
			{
				return this.ActiveWaitStandingPoint != null;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060032F5 RID: 13045 RVA: 0x000D1A2C File Offset: 0x000CFC2C
		public MatrixFrame WaitFrame
		{
			get
			{
				if (this.ActiveWaitStandingPoint != null)
				{
					return this.ActiveWaitStandingPoint.GetGlobalFrame();
				}
				return MatrixFrame.Identity;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060032F6 RID: 13046 RVA: 0x000D1A4D File Offset: 0x000CFC4D
		public GameEntity WaitEntity
		{
			get
			{
				return this.ActiveWaitStandingPoint;
			}
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x000D1A58 File Offset: 0x000CFC58
		protected internal override void OnMissionReset()
		{
			base.OnMissionReset();
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnMissionReset();
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060032F8 RID: 13048 RVA: 0x000D1AB0 File Offset: 0x000CFCB0
		public virtual bool IsDeactivated
		{
			get
			{
				return this._isMachineDeactivated || this.IsDestroyed;
			}
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x000D1AC4 File Offset: 0x000CFCC4
		public void Deactivate()
		{
			this._isMachineDeactivated = true;
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				standingPoint.IsDeactivated = true;
			}
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x000D1B1C File Offset: 0x000CFD1C
		public void Activate()
		{
			this._isMachineDeactivated = false;
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				standingPoint.IsDeactivated = false;
			}
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x000D1B74 File Offset: 0x000CFD74
		public virtual bool IsDisabledForBattleSide(BattleSideEnum sideEnum)
		{
			return this.IsDeactivated;
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000D1B7C File Offset: 0x000CFD7C
		public virtual bool IsDisabledForBattleSideAI(BattleSideEnum sideEnum)
		{
			return base.IsDisabled || this.IsDisabledForAI || this.IsDeactivated || (this.EnemyRangeToStopUsing > 0f && sideEnum != BattleSideEnum.None && this.IsDisabledDueToEnemyInRange(sideEnum));
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000D1BB4 File Offset: 0x000CFDB4
		public virtual bool ShouldAutoLeaveDetachmentWhenDisabled(BattleSideEnum sideEnum)
		{
			return true;
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x000D1BB7 File Offset: 0x000CFDB7
		protected bool IsDisabledDueToEnemyInRange(BattleSideEnum sideEnum)
		{
			if (sideEnum == BattleSideEnum.Attacker)
			{
				return this.IsDisabledForAttackerAIDueToEnemyInRange.Value;
			}
			return this.IsDisabledForDefenderAIDueToEnemyInRange.Value;
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x000D1BD4 File Offset: 0x000CFDD4
		public virtual bool AutoAttachUserToFormation(BattleSideEnum sideEnum)
		{
			return true;
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x000D1BD7 File Offset: 0x000CFDD7
		public virtual bool HasToBeDefendedByUser(BattleSideEnum sideEnum)
		{
			return false;
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000D1BDC File Offset: 0x000CFDDC
		public virtual void Disable()
		{
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				if (standingPoint.HasUser)
				{
					standingPoint.UserAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
				}
				if (standingPoint.HasAIMovingTo)
				{
					standingPoint.MovingAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
				}
			}
			foreach (Team team in Mission.Current.Teams.Where<Team>((Team t) => t.DetachmentManager.ContainsDetachment(this)))
			{
				team.DetachmentManager.DestroyDetachment(this);
			}
			foreach (StandingPoint standingPoint2 in this.StandingPoints)
			{
				if (!standingPoint2.GameEntity.HasTag(this.AmmoPickUpTag))
				{
					if (standingPoint2.HasUser)
					{
						standingPoint2.UserAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					}
					standingPoint2.SetIsDeactivatedSynched(true);
				}
			}
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnMissionObjectDisabled();
			}
			if (this.ShouldDisableTickIfMachineDisabled())
			{
				base.SetScriptComponentToTick(ScriptComponentBehavior.TickRequirement.None);
			}
			base.SetDisabled(false);
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000D1D6C File Offset: 0x000CFF6C
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnRemoved();
			}
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x000D1DC4 File Offset: 0x000CFFC4
		public override string ToString()
		{
			string text = base.GetType() + " with Components:";
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				text = string.Concat(new object[] { text, "[", usableMissionObjectComponent, "]" });
			}
			return text;
		}

		// Token: 0x06003304 RID: 13060
		public abstract TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject);

		// Token: 0x06003305 RID: 13061 RVA: 0x000D1E48 File Offset: 0x000D0048
		public virtual StandingPoint GetBestPointAlternativeTo(StandingPoint standingPoint, Agent agent)
		{
			return standingPoint;
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000D1E4C File Offset: 0x000D004C
		public virtual bool IsInRangeToCheckAlternativePoints(Agent agent)
		{
			float num = ((this.StandingPoints.Count > 0) ? (agent.GetInteractionDistanceToUsable(this.StandingPoints[0]) + 1f) : 2f);
			return base.GameEntity.GlobalPosition.DistanceSquared(agent.Position) < num * num;
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000D1EA8 File Offset: 0x000D00A8
		void IDetachment.OnFormationLeave(Formation formation)
		{
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				Agent userAgent = standingPoint.UserAgent;
				if (userAgent != null && userAgent.Formation == formation && userAgent.IsAIControlled)
				{
					this.OnFormationLeaveHelper(formation, userAgent);
				}
				Agent movingAgent = standingPoint.MovingAgent;
				if (movingAgent != null && movingAgent.Formation == formation)
				{
					this.OnFormationLeaveHelper(formation, movingAgent);
				}
				for (int i = standingPoint.GetDefendingAgentCount() - 1; i >= 0; i--)
				{
					Agent agent = standingPoint.DefendingAgents[i];
					if (agent.Formation == formation)
					{
						this.OnFormationLeaveHelper(formation, agent);
					}
				}
			}
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000D1F70 File Offset: 0x000D0170
		private void OnFormationLeaveHelper(Formation formation, Agent agent)
		{
			((IDetachment)this).RemoveAgent(agent);
			formation.AttachUnit(agent);
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000D1F80 File Offset: 0x000D0180
		bool IDetachment.IsAgentUsingOrInterested(Agent agent)
		{
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				if (agent.CurrentlyUsedGameObject == standingPoint || (agent.IsAIControlled && agent.AIInterestedInGameObject(standingPoint)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000D1FF0 File Offset: 0x000D01F0
		protected virtual float GetWeightOfStandingPoint(StandingPoint sp)
		{
			if (!sp.HasAIMovingTo)
			{
				return 0.6f;
			}
			return 0.2f;
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000D2005 File Offset: 0x000D0205
		float IDetachment.GetDetachmentWeight(BattleSideEnum side)
		{
			return this.GetDetachmentWeightAux(side);
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000D2010 File Offset: 0x000D0210
		protected virtual float GetDetachmentWeightAux(BattleSideEnum side)
		{
			if (this.IsDisabledForBattleSideAI(side))
			{
				return float.MinValue;
			}
			this.UsableStandingPoints.Clear();
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < this.StandingPoints.Count; i++)
			{
				StandingPoint standingPoint = this.StandingPoints[i];
				if (standingPoint.IsUsableBySide(side))
				{
					if (!standingPoint.HasAIMovingTo)
					{
						if (!flag2)
						{
							this.UsableStandingPoints.Clear();
						}
						flag2 = true;
					}
					else if (flag2 || standingPoint.MovingAgent.Formation.Team.Side != side)
					{
						goto IL_0081;
					}
					flag = true;
					this.UsableStandingPoints.Add(new ValueTuple<int, StandingPoint>(i, standingPoint));
				}
				IL_0081:;
			}
			this.AreUsableStandingPointsVacant = flag2;
			if (!flag)
			{
				return float.MinValue;
			}
			if (flag2)
			{
				return 1f;
			}
			if (!this.IsDetachmentRecentlyEvaluated)
			{
				return 0.1f;
			}
			return 0.01f;
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000D20DC File Offset: 0x000D02DC
		void IDetachment.GetSlotIndexWeightTuples(List<ValueTuple<int, float>> slotIndexWeightTuples)
		{
			foreach (ValueTuple<int, StandingPoint> valueTuple in this.UsableStandingPoints)
			{
				StandingPoint item = valueTuple.Item2;
				slotIndexWeightTuples.Add(new ValueTuple<int, float>(valueTuple.Item1, this.GetWeightOfStandingPoint(item) * ((!this.AreUsableStandingPointsVacant && item.HasRecentlyBeenRechecked) ? 0.1f : 1f)));
			}
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x000D2164 File Offset: 0x000D0364
		bool IDetachment.IsSlotAtIndexAvailableForAgent(int slotIndex, Agent agent)
		{
			return agent.CanBeAssignedForScriptedMovement() && !this.StandingPoints[slotIndex].IsDisabledForAgent(agent) && !this.IsAgentOnInconvenientNavmesh(agent, this.StandingPoints[slotIndex]);
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x000D219C File Offset: 0x000D039C
		protected virtual bool IsAgentOnInconvenientNavmesh(Agent agent, StandingPoint standingPoint)
		{
			if (Mission.Current.MissionTeamAIType != Mission.MissionTeamAITypeEnum.Siege)
			{
				return false;
			}
			int currentNavigationFaceId = agent.GetCurrentNavigationFaceId();
			TeamAISiegeComponent teamAISiegeComponent;
			if ((teamAISiegeComponent = agent.Team.TeamAI as TeamAISiegeComponent) != null)
			{
				if (teamAISiegeComponent is TeamAISiegeAttacker && currentNavigationFaceId % 10 == 1)
				{
					return true;
				}
				if (teamAISiegeComponent is TeamAISiegeDefender && currentNavigationFaceId % 10 != 1)
				{
					return true;
				}
				foreach (int num in teamAISiegeComponent.DifficultNavmeshIDs)
				{
					if (currentNavigationFaceId == num)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x000D2244 File Offset: 0x000D0444
		bool IDetachment.IsAgentEligible(Agent agent)
		{
			return true;
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x000D2248 File Offset: 0x000D0448
		public void AddAgentAtSlotIndex(Agent agent, int slotIndex)
		{
			StandingPoint standingPoint = this.StandingPoints[slotIndex];
			if (standingPoint.HasAIMovingTo)
			{
				Agent movingAgent = standingPoint.MovingAgent;
				if (movingAgent != null)
				{
					((IDetachment)this).RemoveAgent(movingAgent);
					Formation formation = movingAgent.Formation;
					if (formation != null)
					{
						formation.AttachUnit(movingAgent);
					}
				}
			}
			if (standingPoint.HasDefendingAgent)
			{
				for (int i = standingPoint.DefendingAgents.Count - 1; i >= 0; i--)
				{
					Agent agent2 = standingPoint.DefendingAgents[i];
					if (agent2 != null)
					{
						((IDetachment)this).RemoveAgent(agent2);
						Formation formation2 = agent2.Formation;
						if (formation2 != null)
						{
							formation2.AttachUnit(agent2);
						}
					}
				}
			}
			((IDetachment)this).AddAgent(agent, slotIndex, Agent.AIScriptedFrameFlags.None);
			Formation formation3 = agent.Formation;
			if (formation3 != null)
			{
				formation3.DetachUnit(agent, false);
			}
			agent.Detachment = this;
			agent.SetDetachmentWeight(1f);
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x000D2304 File Offset: 0x000D0504
		public void SetIsDisabledForAI(bool isDisabledForAI)
		{
			if (this.IsDisabledForAI != isDisabledForAI)
			{
				this.IsDisabledForAI = isDisabledForAI;
			}
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x000D2316 File Offset: 0x000D0516
		Agent IDetachment.GetMovingAgentAtSlotIndex(int slotIndex)
		{
			return this.StandingPoints[slotIndex].MovingAgent;
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x000D2329 File Offset: 0x000D0529
		bool IDetachment.IsDetachmentRecentlyEvaluated()
		{
			return this.IsDetachmentRecentlyEvaluated;
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x000D2331 File Offset: 0x000D0531
		void IDetachment.UnmarkDetachment()
		{
			this.IsDetachmentRecentlyEvaluated = false;
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x000D233C File Offset: 0x000D053C
		void IDetachment.MarkSlotAtIndex(int slotIndex)
		{
			int count = this.UsableStandingPoints.Count;
			int num = this._reevaluatedCount + 1;
			this._reevaluatedCount = num;
			if (num >= count)
			{
				foreach (ValueTuple<int, StandingPoint> valueTuple in this.UsableStandingPoints)
				{
					valueTuple.Item2.HasRecentlyBeenRechecked = false;
				}
				this.IsDetachmentRecentlyEvaluated = true;
				this._reevaluatedCount = 0;
				return;
			}
			this.StandingPoints[slotIndex].HasRecentlyBeenRechecked = true;
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x000D23D4 File Offset: 0x000D05D4
		float? IDetachment.GetWeightOfNextSlot(BattleSideEnum side)
		{
			if (this.IsDisabledForBattleSideAI(side))
			{
				return null;
			}
			StandingPoint suitableStandingPointFor = this.GetSuitableStandingPointFor(side, null, null, null);
			if (suitableStandingPointFor != null)
			{
				return new float?(this.GetWeightOfStandingPoint(suitableStandingPointFor));
			}
			return null;
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x000D2418 File Offset: 0x000D0618
		float IDetachment.GetExactCostOfAgentAtSlot(Agent candidate, int slotIndex)
		{
			StandingPoint standingPoint = this.StandingPoints[slotIndex];
			Vec3 globalPosition = standingPoint.GameEntity.GlobalPosition;
			WorldPosition worldPosition = new WorldPosition(candidate.Mission.Scene, globalPosition);
			WorldPosition worldPosition2 = candidate.GetWorldPosition();
			float maxValue;
			if (!standingPoint.Scene.GetPathDistanceBetweenPositions(ref worldPosition, ref worldPosition2, candidate.Monster.BodyCapsuleRadius, out maxValue))
			{
				maxValue = float.MaxValue;
			}
			return maxValue;
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x000D2480 File Offset: 0x000D0680
		List<float> IDetachment.GetTemplateCostsOfAgent(Agent candidate, List<float> oldValue)
		{
			List<float> list = oldValue ?? new List<float>(this.StandingPoints.Count);
			list.Clear();
			for (int i = 0; i < this.StandingPoints.Count; i++)
			{
				list.Add(float.MaxValue);
			}
			foreach (ValueTuple<int, StandingPoint> valueTuple in this.UsableStandingPoints)
			{
				float num = valueTuple.Item2.GameEntity.GlobalPosition.Distance(candidate.Position);
				list[valueTuple.Item1] = num * MissionGameModels.Current.AgentStatCalculateModel.GetDetachmentCostMultiplierOfAgent(candidate, this);
			}
			return list;
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x000D2550 File Offset: 0x000D0750
		float IDetachment.GetTemplateWeightOfAgent(Agent candidate)
		{
			Scene scene = Mission.Current.Scene;
			Vec3 globalPosition = base.GameEntity.GlobalPosition;
			WorldPosition worldPosition = candidate.GetWorldPosition();
			WorldPosition worldPosition2 = new WorldPosition(scene, UIntPtr.Zero, globalPosition, true);
			float maxValue;
			if (!scene.GetPathDistanceBetweenPositions(ref worldPosition2, ref worldPosition, candidate.Monster.BodyCapsuleRadius, out maxValue))
			{
				maxValue = float.MaxValue;
			}
			return maxValue;
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000D25B0 File Offset: 0x000D07B0
		float IDetachment.GetWeightOfOccupiedSlot(Agent agent)
		{
			return this.GetWeightOfStandingPoint(this.StandingPoints.FirstOrDefault<StandingPoint>((StandingPoint sp) => sp.UserAgent == agent || sp.IsAIMovingTo(agent)));
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x000D25E8 File Offset: 0x000D07E8
		WorldFrame? IDetachment.GetAgentFrame(Agent agent)
		{
			return null;
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x000D25FE File Offset: 0x000D07FE
		void IDetachment.RemoveAgent(Agent agent)
		{
			agent.StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.None);
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x000D2608 File Offset: 0x000D0808
		public int GetNumberOfUsableSlots()
		{
			return this.UsableStandingPoints.Count;
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x000D2618 File Offset: 0x000D0818
		public bool IsStandingPointAvailableForAgent(Agent agent)
		{
			bool flag = false;
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				if (!standingPoint.IsDeactivated && (standingPoint.IsInstantUse || ((!standingPoint.HasUser || standingPoint.UserAgent == agent) && (!standingPoint.HasAIMovingTo || standingPoint.IsAIMovingTo(agent)))) && !standingPoint.IsDisabledForAgent(agent) && !this.IsStandingPointNotUsedOnAccountOfBeingAmmoLoad(standingPoint))
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x000D26B0 File Offset: 0x000D08B0
		float? IDetachment.GetWeightOfAgentAtNextSlot(List<Agent> candidates, out Agent match)
		{
			BattleSideEnum side = candidates[0].Team.Side;
			StandingPoint suitableStandingPointFor = this.GetSuitableStandingPointFor(side, null, candidates, null);
			if (suitableStandingPointFor == null)
			{
				match = null;
				return null;
			}
			match = UsableMachineAIBase.GetSuitableAgentForStandingPoint(this, suitableStandingPointFor, candidates, new List<Agent>());
			if (match == null)
			{
				return null;
			}
			float? weightOfNextSlot = ((IDetachment)this).GetWeightOfNextSlot(side);
			float num = 1f;
			float? num2 = weightOfNextSlot;
			float num3 = num;
			if (num2 == null)
			{
				return null;
			}
			return new float?(num2.GetValueOrDefault() * num3);
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x000D273C File Offset: 0x000D093C
		float? IDetachment.GetWeightOfAgentAtNextSlot(List<ValueTuple<Agent, float>> candidates, out Agent match)
		{
			BattleSideEnum side = candidates[0].Item1.Team.Side;
			StandingPoint suitableStandingPointFor = this.GetSuitableStandingPointFor(side, null, null, candidates);
			if (suitableStandingPointFor == null)
			{
				match = null;
				return null;
			}
			float? weightOfNextSlot = ((IDetachment)this).GetWeightOfNextSlot(side);
			match = UsableMachineAIBase.GetSuitableAgentForStandingPoint(this, suitableStandingPointFor, candidates, new List<Agent>(), weightOfNextSlot.Value);
			if (match == null)
			{
				return null;
			}
			float num = 1f;
			float? num2 = weightOfNextSlot;
			float num3 = num;
			if (num2 == null)
			{
				return null;
			}
			return new float?(num2.GetValueOrDefault() * num3);
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x000D27D4 File Offset: 0x000D09D4
		float? IDetachment.GetWeightOfAgentAtOccupiedSlot(Agent detachedAgent, List<Agent> candidates, out Agent match)
		{
			BattleSideEnum side = candidates[0].Team.Side;
			match = null;
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				if (standingPoint.IsAIMovingTo(detachedAgent) || standingPoint.UserAgent == detachedAgent)
				{
					match = UsableMachineAIBase.GetSuitableAgentForStandingPoint(this, standingPoint, candidates, new List<Agent>());
					break;
				}
			}
			if (match == null)
			{
				return null;
			}
			float? weightOfNextSlot = ((IDetachment)this).GetWeightOfNextSlot(side);
			float num = 1f;
			if (weightOfNextSlot == null)
			{
				return null;
			}
			return new float?(weightOfNextSlot.GetValueOrDefault() * num * 0.5f);
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x000D28A0 File Offset: 0x000D0AA0
		void IDetachment.AddAgent(Agent agent, int slotIndex, Agent.AIScriptedFrameFlags customFlags)
		{
			StandingPoint standingPoint = ((slotIndex == -1) ? this.GetSuitableStandingPointFor(agent.Team.Side, agent, null, null) : this.StandingPoints[slotIndex]);
			if (standingPoint != null)
			{
				if (standingPoint.HasAIMovingTo && !standingPoint.IsInstantUse)
				{
					standingPoint.MovingAgent.StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
				}
				while (standingPoint.HasDefendingAgent)
				{
					standingPoint.DefendingAgents[0].StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
				}
				if (customFlags == Agent.AIScriptedFrameFlags.None)
				{
					customFlags = this.Ai.GetScriptedFrameFlags(agent);
				}
				agent.AIMoveToGameObjectEnable(standingPoint, this, customFlags);
				if (standingPoint.GameEntity.HasTag(this.AmmoPickUpTag))
				{
					this.CurrentlyUsedAmmoPickUpPoint = standingPoint;
					return;
				}
			}
			else
			{
				Debug.FailedAssert("false", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Usables\\UsableMachine.cs", "AddAgent", 1449);
			}
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x000D2962 File Offset: 0x000D0B62
		void IDetachment.FormationStartUsing(Formation formation)
		{
			this._userFormations.Add(formation);
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000D2970 File Offset: 0x000D0B70
		void IDetachment.FormationStopUsing(Formation formation)
		{
			this._userFormations.Remove(formation);
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x000D297F File Offset: 0x000D0B7F
		public bool IsUsedByFormation(Formation formation)
		{
			return this._userFormations.Contains(formation);
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x000D298D File Offset: 0x000D0B8D
		void IDetachment.ResetEvaluation()
		{
			this._isEvaluated = false;
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x000D2996 File Offset: 0x000D0B96
		bool IDetachment.IsEvaluated()
		{
			return this._isEvaluated;
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000D299E File Offset: 0x000D0B9E
		void IDetachment.SetAsEvaluated()
		{
			this._isEvaluated = true;
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000D29A7 File Offset: 0x000D0BA7
		float IDetachment.GetDetachmentWeightFromCache()
		{
			return this._cachedDetachmentWeight;
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x000D29AF File Offset: 0x000D0BAF
		float IDetachment.ComputeAndCacheDetachmentWeight(BattleSideEnum side)
		{
			this._cachedDetachmentWeight = this.GetDetachmentWeightAux(side);
			return this._cachedDetachmentWeight;
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000D29C4 File Offset: 0x000D0BC4
		protected internal virtual bool IsStandingPointNotUsedOnAccountOfBeingAmmoLoad(StandingPoint standingPoint)
		{
			return this.AmmoPickUpPoints.Contains(standingPoint) && (this.StandingPoints.Any<StandingPoint>((StandingPoint standingPoint2) => (standingPoint2.IsDeactivated || standingPoint2.HasUser || standingPoint2.HasAIMovingTo) && !standingPoint2.GameEntity.HasTag(this.AmmoPickUpTag) && standingPoint2 is StandingPointWithWeaponRequirement) || this.HasAIPickingUpAmmo);
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x000D29FC File Offset: 0x000D0BFC
		protected virtual StandingPoint GetSuitableStandingPointFor(BattleSideEnum side, Agent agent = null, List<Agent> agents = null, List<ValueTuple<Agent, float>> agentValuePairs = null)
		{
			return this.StandingPoints.FirstOrDefault<StandingPoint>((StandingPoint sp) => !sp.IsDeactivated && (sp.IsInstantUse || (!sp.HasUser && !sp.HasAIMovingTo)) && (agent == null || !sp.IsDisabledForAgent(agent)) && (agents == null || agents.Any<Agent>((Agent a) => !sp.IsDisabledForAgent(a))) && (agentValuePairs == null || agentValuePairs.Any<ValueTuple<Agent, float>>((ValueTuple<Agent, float> avp) => !sp.IsDisabledForAgent(avp.Item1))) && !this.IsStandingPointNotUsedOnAccountOfBeingAmmoLoad(sp));
		}

		// Token: 0x0600332E RID: 13102
		public abstract TextObject GetDescriptionText(WeakGameEntity gameEntity);

		// Token: 0x0600332F RID: 13103 RVA: 0x000D2A43 File Offset: 0x000D0C43
		protected virtual bool ShouldDisableTickIfMachineDisabled()
		{
			return true;
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x000D2A46 File Offset: 0x000D0C46
		public void SetEnemyRangeToStopUsing(float value)
		{
			this.EnemyRangeToStopUsing = value;
		}

		// Token: 0x04001586 RID: 5510
		public const string UsableMachineParentTag = "machine_parent";

		// Token: 0x04001587 RID: 5511
		public string PilotStandingPointTag = "Pilot";

		// Token: 0x04001588 RID: 5512
		public string AmmoPickUpTag = "ammopickup";

		// Token: 0x04001589 RID: 5513
		public string WaitStandingPointTag = "Wait";

		// Token: 0x0400158F RID: 5519
		protected GameEntity ActiveWaitStandingPoint;

		// Token: 0x04001590 RID: 5520
		private readonly List<UsableMissionObjectComponent> _components;

		// Token: 0x04001592 RID: 5522
		protected bool AreUsableStandingPointsVacant = true;

		// Token: 0x04001594 RID: 5524
		protected List<ValueTuple<int, StandingPoint>> UsableStandingPoints;

		// Token: 0x04001595 RID: 5525
		private int _reevaluatedCount;

		// Token: 0x04001596 RID: 5526
		private bool _isEvaluated;

		// Token: 0x04001597 RID: 5527
		private float _cachedDetachmentWeight;

		// Token: 0x04001598 RID: 5528
		protected float EnemyRangeToStopUsing;

		// Token: 0x04001599 RID: 5529
		protected Vec2 MachinePositionOffsetToStopUsingLocal = Vec2.Zero;

		// Token: 0x0400159A RID: 5530
		protected bool MakeVisibilityCheck = true;

		// Token: 0x0400159B RID: 5531
		private UsableMachineAIBase _ai;

		// Token: 0x0400159C RID: 5532
		private StandingPoint _currentlyUsedAmmoPickUpPoint;

		// Token: 0x0400159D RID: 5533
		protected QueryData<bool> IsDisabledForAttackerAIDueToEnemyInRange;

		// Token: 0x0400159E RID: 5534
		protected QueryData<bool> IsDisabledForDefenderAIDueToEnemyInRange;

		// Token: 0x040015A0 RID: 5536
		private MBList<Formation> _userFormations;

		// Token: 0x040015A1 RID: 5537
		private bool _isMachineDeactivated;
	}
}
