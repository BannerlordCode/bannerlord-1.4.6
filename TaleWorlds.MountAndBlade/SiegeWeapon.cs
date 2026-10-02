using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000351 RID: 849
	public abstract class SiegeWeapon : UsableMachine, ITargetable
	{
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x000C41FA File Offset: 0x000C23FA
		// (set) Token: 0x06003096 RID: 12438 RVA: 0x000C4202 File Offset: 0x000C2402
		[EditorVisibleScriptComponentVariable(false)]
		public bool ForcedUse { get; private set; }

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x000C420C File Offset: 0x000C240C
		public bool IsUsed
		{
			get
			{
				using (List<Formation>.Enumerator enumerator = base.UserFormations.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Team.Side == this.Side)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000C4270 File Offset: 0x000C2470
		public void SetForcedUse(bool value)
		{
			this.ForcedUse = value;
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x000C4279 File Offset: 0x000C2479
		public virtual BattleSideEnum Side
		{
			get
			{
				return BattleSideEnum.Attacker;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x000C427C File Offset: 0x000C247C
		public override TextObject HitObjectName
		{
			get
			{
				return GameTexts.FindText("str_siege_engine", this.GetSiegeEngineType().StringId);
			}
		}

		// Token: 0x0600309B RID: 12443
		public abstract SiegeEngineType GetSiegeEngineType();

		// Token: 0x0600309C RID: 12444 RVA: 0x000C4294 File Offset: 0x000C2494
		protected virtual bool CalculateIsSufficientlyManned(BattleSideEnum battleSide)
		{
			if (this.GetDetachmentWeightAux(battleSide) < 1f)
			{
				return true;
			}
			foreach (Team team in Mission.Current.Teams)
			{
				if (team.Side == this.Side)
				{
					foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
					{
						if (formation.CountOfUnits > 0 && base.IsUsedByFormation(formation) && (formation.Arrangement.UnitCount > 1 || (formation.Arrangement.UnitCount > 0 && !formation.HasPlayerControlledTroop)))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x000C4384 File Offset: 0x000C2584
		private bool HasNewMovingAgents()
		{
			foreach (StandingPoint standingPoint in base.StandingPoints)
			{
				if (standingPoint.HasAIMovingTo && standingPoint.PreviousUserAgent != standingPoint.MovingAgent)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x000C43F0 File Offset: 0x000C25F0
		protected internal override void OnInit()
		{
			base.OnInit();
			this.ForcedUse = true;
			this._potentialUsingFormations = new List<Formation>();
			this._forcedUseFormations = new List<Formation>();
			base.GameEntity.SetAnimationSoundActivation(true);
			this._removeOnDeployEntities = Mission.Current.Scene.FindEntitiesWithTag(this.RemoveOnDeployTag).ToList<GameEntity>();
			this._addOnDeployEntities = Mission.Current.Scene.FindEntitiesWithTag(this.AddOnDeployTag).ToList<GameEntity>();
			foreach (StandingPoint standingPoint in base.StandingPoints)
			{
				if (!(standingPoint is StandingPointWithWeaponRequirement))
				{
					standingPoint.AutoEquipWeaponsOnUseStopped = true;
				}
			}
			base.SetScriptComponentToTick(this.GetTickRequirement());
			WeakGameEntity firstChildEntityWithTag = base.GameEntity.GetFirstChildEntityWithTag("targeting_entity");
			if (firstChildEntityWithTag.IsValid)
			{
				Vec3 vec = base.GameEntity.ComputeGlobalPhysicsBoundingBoxCenter();
				this._targetingPositionOffset = new Vec3?(firstChildEntityWithTag.GlobalPosition - vec);
			}
			this.EnemyRangeToStopUsing = 5f;
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x000C451C File Offset: 0x000C271C
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (base.GameEntity.IsVisibleIncludeParents() && !GameNetwork.IsClientOrReplay)
			{
				return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick | ScriptComponentBehavior.TickRequirement.TickParallel;
			}
			return base.GetTickRequirement();
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000C4554 File Offset: 0x000C2754
		private void TickAux(bool isParallel)
		{
			if (!GameNetwork.IsClientOrReplay && base.GameEntity.IsVisibleIncludeParents())
			{
				if (this.IsDisabledForBattleSide(this.Side))
				{
					using (List<StandingPoint>.Enumerator enumerator = base.StandingPoints.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							StandingPoint standingPoint = enumerator.Current;
							Agent userAgent = standingPoint.UserAgent;
							if (userAgent != null && !userAgent.IsPlayerControlled && userAgent.Formation != null && userAgent.Formation.Team.Side == this.Side)
							{
								if (isParallel)
								{
									this._needsSingleThreadTickOnce = true;
								}
								else
								{
									userAgent.Formation.StopUsingMachine(this, false);
									this._forcedUseFormations.Remove(userAgent.Formation);
									this._isValidated = false;
								}
							}
						}
						return;
					}
				}
				if (this.ForcedUse)
				{
					bool flag = false;
					foreach (Team team in Mission.Current.Teams)
					{
						if (team.Side == this.Side)
						{
							if (!this.CalculateIsSufficientlyManned(team.Side))
							{
								foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
								{
									if (formation.CountOfUnits > 0 && formation.GetReadonlyMovementOrderReference().OrderEnum != MovementOrder.MovementOrderEnum.Retreat && (formation.Arrangement.UnitCount > 1 || (formation.Arrangement.UnitCount > 0 && !formation.HasPlayerControlledTroop)) && !formation.Detachments.Contains(this))
									{
										if (isParallel)
										{
											this._needsSingleThreadTickOnce = true;
										}
										else
										{
											this._potentialUsingFormations.Add(formation);
										}
									}
								}
								this._areMovingAgentsProcessed = false;
							}
							else if (this.HasNewMovingAgents())
							{
								if (!this._areMovingAgentsProcessed)
								{
									float num = float.MaxValue;
									Formation formation2 = null;
									foreach (Formation formation3 in team.FormationsIncludingSpecialAndEmpty)
									{
										if (formation3.CountOfUnits > 0 && formation3.GetReadonlyMovementOrderReference().OrderEnum != MovementOrder.MovementOrderEnum.Retreat && (formation3.Arrangement.UnitCount > 1 || (formation3.Arrangement.UnitCount > 0 && !formation3.HasPlayerControlledTroop)))
										{
											WorldPosition cachedMedianPosition = formation3.CachedMedianPosition;
											Vec3 vec = base.GameEntity.GlobalPosition;
											float num2 = cachedMedianPosition.DistanceSquaredWithLimit(in vec, 10000f);
											if (num2 < num)
											{
												num = num2;
												formation2 = formation3;
											}
										}
									}
									if (formation2 != null && !base.IsUsedByFormation(formation2))
									{
										if (isParallel)
										{
											this._needsSingleThreadTickOnce = true;
										}
										else
										{
											this._potentialUsingFormations.Clear();
											this._potentialUsingFormations.Add(formation2);
											flag = true;
											this._areMovingAgentsProcessed = true;
										}
									}
									else
									{
										this._areMovingAgentsProcessed = true;
									}
								}
							}
							else
							{
								this._areMovingAgentsProcessed = false;
							}
							if (flag)
							{
								this._potentialUsingFormations[0].StartUsingMachine(this, !this._potentialUsingFormations[0].IsAIControlled);
								this._forcedUseFormations.Add(this._potentialUsingFormations[0]);
								this._potentialUsingFormations.Clear();
								this._isValidated = false;
								flag = false;
							}
							else if (this._potentialUsingFormations.Count > 0)
							{
								float num3 = float.MaxValue;
								Formation formation4 = null;
								foreach (Formation formation5 in this._potentialUsingFormations)
								{
									Vec2 cachedAveragePosition = formation5.CachedAveragePosition;
									Vec3 vec = base.GameEntity.GlobalPosition;
									float num4 = cachedAveragePosition.DistanceSquared(vec.AsVec2);
									if (num4 < num3)
									{
										num3 = num4;
										formation4 = formation5;
									}
								}
								int count = base.StandingPoints.Count;
								int num5 = 0;
								Formation formation6 = null;
								Vec2 vec2 = Vec2.Zero;
								for (int i = 0; i < count; i++)
								{
									Agent previousUserAgent = base.StandingPoints[i].PreviousUserAgent;
									if (previousUserAgent != null)
									{
										if (!previousUserAgent.IsActive() || previousUserAgent.Formation == null || (formation6 != null && previousUserAgent.Formation != formation6))
										{
											num5 = -1;
											break;
										}
										num5++;
										Vec2 vec3 = vec2;
										Vec3 vec = previousUserAgent.Position;
										vec2 = vec3 + vec.AsVec2;
										formation6 = previousUserAgent.Formation;
									}
								}
								Formation formation7 = formation4;
								if (num5 > 0 && this._potentialUsingFormations.Contains(formation6))
								{
									vec2 *= 1f / (float)num5;
									Vec3 vec = base.GameEntity.GlobalPosition;
									if (vec2.DistanceSquared(vec.AsVec2) < num3)
									{
										formation7 = formation6;
									}
								}
								formation7.StartUsingMachine(this, !formation7.IsAIControlled);
								this._forcedUseFormations.Add(formation7);
								this._potentialUsingFormations.Clear();
								this._isValidated = false;
							}
							else if (!this._isValidated)
							{
								if (!this.HasToBeDefendedByUser(team.Side) && this.GetDetachmentWeightAux(team.Side) == -3.4028235E+38f)
								{
									for (int j = this._forcedUseFormations.Count - 1; j >= 0; j--)
									{
										Formation formation8 = this._forcedUseFormations[j];
										if (formation8.Team.Side == this.Side && !this.IsAnyUserBelongsToFormation(formation8))
										{
											if (isParallel)
											{
												if (base.IsUsedByFormation(formation8))
												{
													this._needsSingleThreadTickOnce = true;
													break;
												}
												this._forcedUseFormations.Remove(formation8);
											}
											else
											{
												if (base.IsUsedByFormation(formation8))
												{
													formation8.StopUsingMachine(this, !formation8.IsAIControlled);
												}
												this._forcedUseFormations.Remove(formation8);
											}
										}
									}
									if (isParallel && this._needsSingleThreadTickOnce)
									{
										break;
									}
								}
								if (!isParallel)
								{
									this._isValidated = true;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000C4BAC File Offset: 0x000C2DAC
		protected virtual bool IsAnyUserBelongsToFormation(Formation formation)
		{
			foreach (StandingPoint standingPoint in base.StandingPoints)
			{
				if (standingPoint.UserAgent != null && standingPoint.UserAgent.Formation == formation)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000C4C18 File Offset: 0x000C2E18
		protected internal override void OnTickParallel(float dt)
		{
			this.TickAux(true);
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000C4C21 File Offset: 0x000C2E21
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (this._needsSingleThreadTickOnce)
			{
				this._needsSingleThreadTickOnce = false;
				this.TickAux(false);
			}
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000C4C40 File Offset: 0x000C2E40
		public void TickAuxForInit()
		{
			this.TickAux(false);
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000C4C4C File Offset: 0x000C2E4C
		protected internal virtual void OnDeploymentStateChanged(bool isDeployed)
		{
			foreach (GameEntity gameEntity in this._removeOnDeployEntities)
			{
				gameEntity.SetVisibilityExcludeParents(!isDeployed);
				StrategicArea firstScriptOfType = gameEntity.GetFirstScriptOfType<StrategicArea>();
				if (firstScriptOfType != null)
				{
					firstScriptOfType.OnParentGameEntityVisibilityChanged(!isDeployed);
				}
				else
				{
					foreach (StrategicArea strategicArea in from c in gameEntity.GetChildren()
						where c.HasScriptOfType<StrategicArea>()
						select c.GetFirstScriptOfType<StrategicArea>())
					{
						strategicArea.OnParentGameEntityVisibilityChanged(!isDeployed);
					}
				}
			}
			foreach (GameEntity gameEntity2 in this._addOnDeployEntities)
			{
				gameEntity2.SetVisibilityExcludeParents(isDeployed);
				MissionObject firstScriptOfType2 = gameEntity2.GetFirstScriptOfType<MissionObject>();
				if (firstScriptOfType2 != null)
				{
					firstScriptOfType2.SetAbilityOfFaces(isDeployed);
				}
				StrategicArea firstScriptOfType3 = gameEntity2.GetFirstScriptOfType<StrategicArea>();
				if (firstScriptOfType3 != null)
				{
					firstScriptOfType3.OnParentGameEntityVisibilityChanged(isDeployed);
				}
				else
				{
					foreach (StrategicArea strategicArea2 in from c in gameEntity2.GetChildren()
						where c.HasScriptOfType<StrategicArea>()
						select c.GetFirstScriptOfType<StrategicArea>())
					{
						strategicArea2.OnParentGameEntityVisibilityChanged(isDeployed);
					}
				}
			}
			if (this._addOnDeployEntities.Count > 0 || this._removeOnDeployEntities.Count > 0)
			{
				foreach (StandingPoint standingPoint in base.StandingPoints)
				{
					standingPoint.RefreshGameEntityWithWorldPosition();
				}
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060030A6 RID: 12454 RVA: 0x000C4E98 File Offset: 0x000C3098
		public override bool HasWaitFrame
		{
			get
			{
				return base.HasWaitFrame && (!(this is IPrimarySiegeWeapon) || !(this as IPrimarySiegeWeapon).HasCompletedAction());
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x000C4EBC File Offset: 0x000C30BC
		public override bool IsDeactivated
		{
			get
			{
				return base.IsDisabled || !base.GameEntity.IsValid || !base.GameEntity.IsVisibleIncludeParents() || base.IsDeactivated;
			}
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000C4EF9 File Offset: 0x000C30F9
		public override bool ShouldAutoLeaveDetachmentWhenDisabled(BattleSideEnum sideEnum)
		{
			return this.AutoAttachUserToFormation(sideEnum);
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000C4F02 File Offset: 0x000C3102
		public override bool AutoAttachUserToFormation(BattleSideEnum sideEnum)
		{
			return base.Ai.HasActionCompleted || !base.IsDisabledDueToEnemyInRange(sideEnum);
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000C4F1D File Offset: 0x000C311D
		public override bool HasToBeDefendedByUser(BattleSideEnum sideEnum)
		{
			return !base.Ai.HasActionCompleted && base.IsDisabledDueToEnemyInRange(sideEnum);
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000C4F38 File Offset: 0x000C3138
		protected float GetUserMultiplierOfWeapon()
		{
			int userCountIncludingInStruckAction = base.UserCountIncludingInStruckAction;
			if (userCountIncludingInStruckAction == 0)
			{
				return 0f;
			}
			return 0.7f + 0.3f * (float)userCountIncludingInStruckAction / (float)this.MaxUserCount;
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000C4F6B File Offset: 0x000C316B
		protected virtual float GetDistanceMultiplierOfWeapon(Vec3 weaponPos)
		{
			if (this.GetMinimumDistanceBetweenPositions(weaponPos) > 20f)
			{
				return 0.4f;
			}
			Debug.FailedAssert("Invalid weapon type", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\SiegeWeapon.cs", "GetDistanceMultiplierOfWeapon", 549);
			return 1f;
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000C4FA0 File Offset: 0x000C31A0
		protected virtual float GetMinimumDistanceBetweenPositions(Vec3 position)
		{
			return base.GameEntity.GlobalPosition.DistanceSquared(position);
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000C4FC4 File Offset: 0x000C31C4
		protected float GetHitPointMultiplierOfWeapon()
		{
			if (base.DestructionComponent != null)
			{
				return MathF.Max(1f, 2f - MathF.Log10(base.DestructionComponent.HitPoint / base.DestructionComponent.MaxHitPoint * 10f + 1f));
			}
			return 1f;
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000C5017 File Offset: 0x000C3217
		public WeakGameEntity GetTargetEntity()
		{
			return base.GameEntity;
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000C501F File Offset: 0x000C321F
		public Vec3 GetTargetingOffset()
		{
			if (this._targetingPositionOffset != null)
			{
				return this._targetingPositionOffset.Value;
			}
			return Vec3.Zero;
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000C503F File Offset: 0x000C323F
		public BattleSideEnum GetSide()
		{
			return this.Side;
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000C5048 File Offset: 0x000C3248
		public Vec3 GetTargetGlobalVelocity()
		{
			IMoveableSiegeWeapon moveableSiegeWeapon = this as IMoveableSiegeWeapon;
			if (moveableSiegeWeapon != null)
			{
				return moveableSiegeWeapon.MovementComponent.Velocity;
			}
			return Vec3.Zero;
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000C5070 File Offset: 0x000C3270
		public bool IsDestructable()
		{
			return base.GameEntity.HasScriptOfType<DestructableComponent>();
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000C508B File Offset: 0x000C328B
		public WeakGameEntity Entity()
		{
			return base.GameEntity;
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000C5094 File Offset: 0x000C3294
		public ValueTuple<Vec3, Vec3> ComputeGlobalPhysicsBoundingBoxMinMax()
		{
			return base.GameEntity.ComputeGlobalPhysicsBoundingBoxMinMax();
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000C50AF File Offset: 0x000C32AF
		public virtual void OnShipCaptured(BattleSideEnum newDefaultSide)
		{
		}

		// Token: 0x060030B7 RID: 12471
		public abstract TargetFlags GetTargetFlags();

		// Token: 0x060030B8 RID: 12472
		public abstract float GetTargetValue(List<Vec3> weaponPos);

		// Token: 0x0400145B RID: 5211
		private const string TargetingEntityTag = "targeting_entity";

		// Token: 0x0400145C RID: 5212
		[EditableScriptComponentVariable(true, "")]
		internal string RemoveOnDeployTag = "";

		// Token: 0x0400145D RID: 5213
		[EditableScriptComponentVariable(true, "")]
		internal string AddOnDeployTag = "";

		// Token: 0x0400145E RID: 5214
		private List<GameEntity> _addOnDeployEntities;

		// Token: 0x04001460 RID: 5216
		protected bool _spawnedFromSpawner;

		// Token: 0x04001461 RID: 5217
		private List<GameEntity> _removeOnDeployEntities;

		// Token: 0x04001462 RID: 5218
		private List<Formation> _potentialUsingFormations;

		// Token: 0x04001463 RID: 5219
		private List<Formation> _forcedUseFormations;

		// Token: 0x04001464 RID: 5220
		private bool _needsSingleThreadTickOnce;

		// Token: 0x04001465 RID: 5221
		private bool _areMovingAgentsProcessed;

		// Token: 0x04001466 RID: 5222
		private bool _isValidated;

		// Token: 0x04001467 RID: 5223
		private Vec3? _targetingPositionOffset;
	}
}
