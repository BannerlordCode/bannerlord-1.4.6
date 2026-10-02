using System;
using System.Collections.Generic;
using NetworkMessages.FromServer;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200037D RID: 893
	public abstract class UsableMissionObject : SynchedMissionObject, IFocusable, IUsable, IVisible
	{
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06003335 RID: 13109 RVA: 0x000D2C5E File Offset: 0x000D0E5E
		public virtual FocusableObjectType FocusableObjectType
		{
			get
			{
				return FocusableObjectType.Item;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x000D2C61 File Offset: 0x000D0E61
		public virtual bool IsFocusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06003337 RID: 13111 RVA: 0x000D2C64 File Offset: 0x000D0E64
		// (set) Token: 0x06003338 RID: 13112 RVA: 0x000D2C6C File Offset: 0x000D0E6C
		public Agent UserAgent
		{
			get
			{
				return this._userAgent;
			}
			private set
			{
				if (this._userAgent != value)
				{
					this.PreviousUserAgent = this._userAgent;
					this._userAgent = value;
					base.SetScriptComponentToTickMT(this.GetTickRequirement());
				}
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06003339 RID: 13113 RVA: 0x000D2C96 File Offset: 0x000D0E96
		// (set) Token: 0x0600333A RID: 13114 RVA: 0x000D2C9E File Offset: 0x000D0E9E
		public Agent PreviousUserAgent { get; private set; }

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x0600333B RID: 13115 RVA: 0x000D2CA7 File Offset: 0x000D0EA7
		// (set) Token: 0x0600333C RID: 13116 RVA: 0x000D2CAF File Offset: 0x000D0EAF
		public GameEntityWithWorldPosition GameEntityWithWorldPosition { get; private set; }

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x000D2CB8 File Offset: 0x000D0EB8
		// (set) Token: 0x0600333E RID: 13118 RVA: 0x000D2CC0 File Offset: 0x000D0EC0
		public virtual Agent MovingAgent { get; private set; }

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x000D2CC9 File Offset: 0x000D0EC9
		// (set) Token: 0x06003340 RID: 13120 RVA: 0x000D2CD1 File Offset: 0x000D0ED1
		public List<Agent> DefendingAgents { get; private set; }

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06003341 RID: 13121 RVA: 0x000D2CDA File Offset: 0x000D0EDA
		public bool HasDefendingAgent
		{
			get
			{
				return this.DefendingAgents != null && this.GetDefendingAgentCount() > 0;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06003342 RID: 13122 RVA: 0x000D2CEF File Offset: 0x000D0EEF
		public virtual bool DisableCombatActionsOnUse
		{
			get
			{
				return !this.IsInstantUse;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06003343 RID: 13123 RVA: 0x000D2CFA File Offset: 0x000D0EFA
		// (set) Token: 0x06003344 RID: 13124 RVA: 0x000D2D02 File Offset: 0x000D0F02
		public virtual bool LockUserFrames { get; set; }

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06003345 RID: 13125 RVA: 0x000D2D0B File Offset: 0x000D0F0B
		// (set) Token: 0x06003346 RID: 13126 RVA: 0x000D2D13 File Offset: 0x000D0F13
		public virtual bool LockUserPositions { get; set; }

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06003347 RID: 13127 RVA: 0x000D2D1C File Offset: 0x000D0F1C
		// (set) Token: 0x06003348 RID: 13128 RVA: 0x000D2D24 File Offset: 0x000D0F24
		public bool IsInstantUse { get; protected set; }

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06003349 RID: 13129 RVA: 0x000D2D2D File Offset: 0x000D0F2D
		// (set) Token: 0x0600334A RID: 13130 RVA: 0x000D2D38 File Offset: 0x000D0F38
		public bool IsDeactivated
		{
			get
			{
				return this._isDeactivated;
			}
			set
			{
				if (value != this._isDeactivated)
				{
					this._isDeactivated = value;
					if (this._isDeactivated && !GameNetwork.IsClientOrReplay)
					{
						Agent userAgent = this.UserAgent;
						if (userAgent != null)
						{
							userAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
						}
						bool flag = false;
						while (this.HasAIMovingTo)
						{
							this.MovingAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
							flag = true;
						}
						while (this.HasDefendingAgent)
						{
							this.DefendingAgents[0].StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
							flag = true;
						}
						if (flag)
						{
							base.SetScriptComponentToTick(this.GetTickRequirement());
						}
					}
				}
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x0600334B RID: 13131 RVA: 0x000D2DC0 File Offset: 0x000D0FC0
		// (set) Token: 0x0600334C RID: 13132 RVA: 0x000D2DC8 File Offset: 0x000D0FC8
		public bool IsDisabledForPlayers
		{
			get
			{
				return this._isDisabledForPlayers;
			}
			set
			{
				if (value != this._isDisabledForPlayers)
				{
					this._isDisabledForPlayers = value;
					if (this._isDisabledForPlayers && !GameNetwork.IsClientOrReplay && this.UserAgent != null && !this.UserAgent.IsAIControlled)
					{
						this.UserAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					}
				}
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x0600334D RID: 13133 RVA: 0x000D2E16 File Offset: 0x000D1016
		public virtual WeakGameEntity InteractionEntity
		{
			get
			{
				return base.GameEntity;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x000D2E1E File Offset: 0x000D101E
		public bool HasAIUser
		{
			get
			{
				return this.HasUser && this.UserAgent.IsAIControlled;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x0600334F RID: 13135 RVA: 0x000D2E35 File Offset: 0x000D1035
		public bool HasUser
		{
			get
			{
				return this.UserAgent != null;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06003350 RID: 13136 RVA: 0x000D2E40 File Offset: 0x000D1040
		public virtual bool HasAIMovingTo
		{
			get
			{
				return this.MovingAgent != null;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06003351 RID: 13137 RVA: 0x000D2E4C File Offset: 0x000D104C
		// (set) Token: 0x06003352 RID: 13138 RVA: 0x000D2E68 File Offset: 0x000D1068
		public bool IsVisible
		{
			get
			{
				return base.GameEntity.IsVisibleIncludeParents();
			}
			set
			{
				base.GameEntity.SetVisibilityExcludeParents(value);
			}
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x000D2E84 File Offset: 0x000D1084
		protected UsableMissionObject(bool isInstantUse = false)
		{
			this._components = new List<UsableMissionObjectComponent>();
			this.IsInstantUse = isInstantUse;
			this.GameEntityWithWorldPosition = null;
			this._needsSingleThreadTickOnce = false;
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000D2EC2 File Offset: 0x000D10C2
		public virtual void OnUserConversationStart()
		{
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x000D2EC4 File Offset: 0x000D10C4
		public virtual void OnUserConversationEnd()
		{
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x000D2EC6 File Offset: 0x000D10C6
		public void SetAreUserPositionsUpdatedInTheMachineTick(bool value)
		{
			this._areUserPositionsUpdatedInTheMachineTick = value;
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x000D2ECF File Offset: 0x000D10CF
		public bool GetIsUserPositionsUpdatedInTheMachineTick()
		{
			return this._areUserPositionsUpdatedInTheMachineTick;
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x000D2ED7 File Offset: 0x000D10D7
		public void SetIsDeactivatedSynched(bool value)
		{
			if (this.IsDeactivated != value)
			{
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetUsableMissionObjectIsDeactivated(base.Id, value));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				}
				this.IsDeactivated = value;
			}
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x000D2F0D File Offset: 0x000D110D
		public void SetIsDisabledForPlayersSynched(bool value)
		{
			if (this.IsDisabledForPlayers != value)
			{
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetUsableMissionObjectIsDisabledForPlayers(base.Id, value));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				}
				this.IsDisabledForPlayers = value;
			}
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x000D2F43 File Offset: 0x000D1143
		public virtual bool IsDisabledForAgent(Agent agent)
		{
			return this.IsDeactivated || agent.MountAgent != null || (this.IsDisabledForPlayers && !agent.IsAIControlled) || !agent.IsAbleToUseMachine();
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x000D2F70 File Offset: 0x000D1170
		public void AddComponent(UsableMissionObjectComponent component)
		{
			this._components.Add(component);
			component.OnAdded(base.Scene);
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x000D2F96 File Offset: 0x000D1196
		public void RemoveComponent(UsableMissionObjectComponent component)
		{
			component.OnRemoved();
			this._components.Remove(component);
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x000D2FB7 File Offset: 0x000D11B7
		public T GetComponent<T>() where T : UsableMissionObjectComponent
		{
			return this._components.Find((UsableMissionObjectComponent c) => c is T) as T;
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x000D2FED File Offset: 0x000D11ED
		private void CollectChildEntities()
		{
			this.CollectChildEntitiesAux(base.GameEntity);
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x000D2FFC File Offset: 0x000D11FC
		private void CollectChildEntitiesAux(WeakGameEntity entity)
		{
			foreach (WeakGameEntity weakGameEntity in entity.GetChildren())
			{
				this.CollectChildEntity(weakGameEntity);
				if (weakGameEntity.GetScriptCount() == 0)
				{
					this.CollectChildEntitiesAux(weakGameEntity);
				}
			}
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x000D305C File Offset: 0x000D125C
		public void RefreshGameEntityWithWorldPosition()
		{
			this.GameEntityWithWorldPosition = new GameEntityWithWorldPosition(base.GameEntity);
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x000D306F File Offset: 0x000D126F
		protected virtual void CollectChildEntity(WeakGameEntity childEntity)
		{
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x000D3071 File Offset: 0x000D1271
		protected virtual bool VerifyChildEntities(ref string errorMessage)
		{
			return true;
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x000D3074 File Offset: 0x000D1274
		protected internal override void OnInit()
		{
			base.OnInit();
			this.CollectChildEntities();
			this.LockUserFrames = !this.IsInstantUse;
			this.RefreshGameEntityWithWorldPosition();
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000D3097 File Offset: 0x000D1297
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this.CollectChildEntities();
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x000D30A8 File Offset: 0x000D12A8
		protected internal override void OnMissionReset()
		{
			base.OnMissionReset();
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnMissionReset();
			}
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x000D3100 File Offset: 0x000D1300
		public virtual void OnFocusGain(Agent userAgent)
		{
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnFocusGain(userAgent);
			}
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x000D3154 File Offset: 0x000D1354
		public virtual void OnFocusLose(Agent userAgent)
		{
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnFocusLose(userAgent);
			}
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x000D31A8 File Offset: 0x000D13A8
		public virtual TextObject GetInfoTextForBeingNotInteractable(Agent userAgent)
		{
			return TextObject.GetEmpty();
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x000D31AF File Offset: 0x000D13AF
		public virtual void SetUserForClient(Agent userAgent)
		{
			Agent userAgent2 = this.UserAgent;
			if (userAgent2 != null)
			{
				userAgent2.SetUsedGameObjectForClient(null);
			}
			this.UserAgent = userAgent;
			if (userAgent != null)
			{
				userAgent.SetUsedGameObjectForClient(this);
			}
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x000D31D4 File Offset: 0x000D13D4
		public virtual void OnUse(Agent userAgent, sbyte agentBoneIndex)
		{
			if (!GameNetwork.IsClientOrReplay)
			{
				if (!userAgent.IsAIControlled && this.HasAIUser)
				{
					this.UserAgent.StopUsingGameObject(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
				}
				if (this.IsAIMovingTo(userAgent))
				{
					Formation formation = userAgent.Formation;
					if (formation != null)
					{
						formation.Team.DetachmentManager.RemoveAgentAsMovingToDetachment(userAgent);
					}
					this.RemoveMovingAgent(userAgent);
					base.SetScriptComponentToTick(this.GetTickRequirement());
				}
				while (this.HasAIMovingTo && !this.IsInstantUse)
				{
					this.MovingAgent.StopUsingGameObject(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
				}
				foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
				{
					usableMissionObjectComponent.OnUse(userAgent);
				}
				this.UserAgent = userAgent;
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new UseObject(userAgent.Index, base.Id));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
					return;
				}
			}
			else
			{
				if (this.LockUserFrames)
				{
					WorldFrame userFrameForAgent = this.GetUserFrameForAgent(userAgent);
					Vec2 asVec = userFrameForAgent.Origin.AsVec2;
					userAgent.SetTargetPositionAndDirection(in asVec, in userFrameForAgent.Rotation.f);
					return;
				}
				if (this.LockUserPositions)
				{
					userAgent.SetTargetPosition(this.GetUserFrameForAgent(userAgent).Origin.AsVec2);
				}
			}
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x000D332C File Offset: 0x000D152C
		public virtual void OnAIMoveToUse(Agent userAgent, IDetachment detachment)
		{
			this.AddMovingAgent(userAgent);
			Formation formation = userAgent.Formation;
			if (formation != null)
			{
				formation.Team.DetachmentManager.AddAgentAsMovingToDetachment(userAgent, detachment);
			}
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000D3360 File Offset: 0x000D1560
		public virtual void OnUseStopped(Agent userAgent, bool isSuccessful, int preferenceIndex)
		{
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnUseStopped(userAgent, isSuccessful);
			}
			this.UserAgent = null;
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x000D33BC File Offset: 0x000D15BC
		public virtual void OnMoveToStopped(Agent movingAgent)
		{
			Formation formation = movingAgent.Formation;
			if (formation != null)
			{
				formation.Team.DetachmentManager.RemoveAgentAsMovingToDetachment(movingAgent);
			}
			this.RemoveMovingAgent(movingAgent);
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x000D33ED File Offset: 0x000D15ED
		public virtual int GetMovingAgentCount()
		{
			if (this.MovingAgent == null)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x000D33FA File Offset: 0x000D15FA
		public virtual Agent GetMovingAgentWithIndex(int index)
		{
			return this.MovingAgent;
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x000D3402 File Offset: 0x000D1602
		public virtual void RemoveMovingAgent(Agent movingAgent)
		{
			this.MovingAgent = null;
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x000D340B File Offset: 0x000D160B
		public virtual void AddMovingAgent(Agent movingAgent)
		{
			this.MovingAgent = movingAgent;
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x000D3414 File Offset: 0x000D1614
		public void OnAIDefendBegin(Agent agent, IDetachment detachment)
		{
			this.AddDefendingAgent(agent);
			Formation formation = agent.Formation;
			if (formation != null)
			{
				formation.Team.DetachmentManager.AddAgentAsDefendingToDetachment(agent, detachment);
			}
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x000D3446 File Offset: 0x000D1646
		public void OnAIDefendEnd(Agent agent)
		{
			Formation formation = agent.Formation;
			if (formation != null)
			{
				formation.Team.DetachmentManager.RemoveAgentAsDefendingToDetachment(agent);
			}
			this.RemoveDefendingAgent(agent);
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x000D3477 File Offset: 0x000D1677
		public void InitializeDefendingAgents()
		{
			if (this.DefendingAgents == null)
			{
				this.DefendingAgents = new List<Agent>();
			}
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x000D348C File Offset: 0x000D168C
		public int GetDefendingAgentCount()
		{
			return this.DefendingAgents.Count;
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x000D3499 File Offset: 0x000D1699
		public void AddDefendingAgent(Agent agent)
		{
			this.DefendingAgents.Add(agent);
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x000D34A7 File Offset: 0x000D16A7
		public void RemoveDefendingAgent(Agent agent)
		{
			this.DefendingAgents.Remove(agent);
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000D34B6 File Offset: 0x000D16B6
		public bool IsAgentDefending(Agent agent)
		{
			return this.DefendingAgents.Contains(agent);
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x000D34C4 File Offset: 0x000D16C4
		public virtual void SimulateTick(float dt)
		{
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x000D34C8 File Offset: 0x000D16C8
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (this.HasUser || this.HasAIMovingTo)
			{
				return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick | ScriptComponentBehavior.TickRequirement.TickParallel2;
			}
			if (this.HasDefendingAgent)
			{
				return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick;
			}
			using (List<UsableMissionObjectComponent>.Enumerator enumerator = this._components.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsOnTickRequired())
					{
						return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick;
					}
				}
			}
			return base.GetTickRequirement();
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000D355C File Offset: 0x000D175C
		protected internal override void OnTickParallel2(float dt)
		{
			for (int i = this.GetMovingAgentCount() - 1; i >= 0; i--)
			{
				if (!this.GetMovingAgentWithIndex(i).IsActive())
				{
					this._needsSingleThreadTickOnce = true;
				}
			}
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x000D3594 File Offset: 0x000D1794
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnTick(dt);
			}
			if (!this._areUserPositionsUpdatedInTheMachineTick && this.HasUser && this.HasUserPositionsChanged(this.UserAgent))
			{
				if (this.LockUserFrames)
				{
					WorldFrame userFrameForAgent = this.GetUserFrameForAgent(this.UserAgent);
					Agent userAgent = this.UserAgent;
					Vec2 asVec = userFrameForAgent.Origin.AsVec2;
					userAgent.SetTargetPositionAndDirection(in asVec, in userFrameForAgent.Rotation.f);
				}
				else if (this.LockUserPositions)
				{
					this.UserAgent.SetTargetPosition(this.GetUserFrameForAgent(this.UserAgent).Origin.AsVec2);
				}
			}
			if (this._needsSingleThreadTickOnce)
			{
				this._needsSingleThreadTickOnce = false;
				for (int i = this.GetMovingAgentCount() - 1; i >= 0; i--)
				{
					Agent movingAgentWithIndex = this.GetMovingAgentWithIndex(i);
					if (!movingAgentWithIndex.IsActive())
					{
						Formation formation = movingAgentWithIndex.Formation;
						if (formation != null)
						{
							formation.Team.DetachmentManager.RemoveAgentAsMovingToDetachment(movingAgentWithIndex);
						}
						this.RemoveMovingAgent(movingAgentWithIndex);
						base.SetScriptComponentToTick(this.GetTickRequirement());
					}
				}
			}
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000D36E0 File Offset: 0x000D18E0
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnEditorTick(dt);
			}
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000D3738 File Offset: 0x000D1938
		protected internal override void OnEditorValidate()
		{
			base.OnEditorValidate();
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnEditorValidate();
			}
			string text = null;
			if (!this.VerifyChildEntities(ref text))
			{
				MBDebug.ShowWarning(text);
			}
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x000D37A0 File Offset: 0x000D19A0
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnRemoved();
			}
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x000D37F8 File Offset: 0x000D19F8
		public virtual WorldFrame GetUserFrameForAgent(Agent agent)
		{
			return this.GameEntityWithWorldPosition.WorldFrame;
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000D3808 File Offset: 0x000D1A08
		public override string ToString()
		{
			string text = base.GetType() + " with Components:";
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				text = string.Concat(new object[] { text, "[", usableMissionObjectComponent, "]" });
			}
			return text;
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x000D388C File Offset: 0x000D1A8C
		public virtual bool IsAIMovingTo(Agent agent)
		{
			return this.MovingAgent == agent;
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x000D3898 File Offset: 0x000D1A98
		public virtual bool HasUserPositionsChanged(Agent agent)
		{
			return base.GameEntity.GetHasFrameChanged();
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x000D38B4 File Offset: 0x000D1AB4
		public override void WriteToNetwork()
		{
			base.WriteToNetwork();
			GameNetworkMessage.WriteBoolToPacket(this.IsDeactivated);
			GameNetworkMessage.WriteBoolToPacket(this.IsDisabledForPlayers);
			GameNetworkMessage.WriteBoolToPacket(this.UserAgent != null);
			if (this.UserAgent != null)
			{
				GameNetworkMessage.WriteAgentIndexToPacket(this.UserAgent.Index);
			}
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x000D3903 File Offset: 0x000D1B03
		public virtual bool IsUsableByAgent(Agent userAgent)
		{
			return true;
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x000D3906 File Offset: 0x000D1B06
		public void SetCustomLocalFrame(in MatrixFrame customLocalFrame)
		{
			this.GameEntityWithWorldPosition.SetCustomLocalFrame(in customLocalFrame);
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x000D3914 File Offset: 0x000D1B14
		public override void OnEndMission()
		{
			this.UserAgent = null;
			for (int i = this.GetMovingAgentCount() - 1; i >= 0; i--)
			{
				this.RemoveMovingAgent(this.GetMovingAgentWithIndex(i));
			}
			if (this.HasDefendingAgent)
			{
				for (int j = this.GetDefendingAgentCount() - 1; j >= 0; j--)
				{
					this.DefendingAgents.RemoveAt(j);
				}
			}
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x000D397C File Offset: 0x000D1B7C
		public override void OnAfterReadFromNetwork(ValueTuple<BaseSynchedMissionObjectReadableRecord, ISynchedMissionObjectReadableRecord> synchedMissionObjectReadableRecord, bool allowVisibilityUpdate = true)
		{
			base.OnAfterReadFromNetwork(synchedMissionObjectReadableRecord, allowVisibilityUpdate);
			UsableMissionObject.UsableMissionObjectRecord usableMissionObjectRecord = (UsableMissionObject.UsableMissionObjectRecord)synchedMissionObjectReadableRecord.Item2;
			this.IsDeactivated = usableMissionObjectRecord.IsDeactivated;
			this.IsDisabledForPlayers = usableMissionObjectRecord.IsDisabledForPlayers;
			if (usableMissionObjectRecord.IsUserAgentExists)
			{
				Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(usableMissionObjectRecord.AgentIndex, false);
				if (agentFromIndex != null)
				{
					this.SetUserForClient(agentFromIndex);
				}
			}
		}

		// Token: 0x06003389 RID: 13193
		public abstract TextObject GetDescriptionText(WeakGameEntity gameEntity);

		// Token: 0x040015A2 RID: 5538
		private Agent _userAgent;

		// Token: 0x040015A7 RID: 5543
		private bool _areUserPositionsUpdatedInTheMachineTick;

		// Token: 0x040015A8 RID: 5544
		private readonly List<UsableMissionObjectComponent> _components;

		// Token: 0x040015A9 RID: 5545
		[EditableScriptComponentVariable(false, "")]
		public TextObject DescriptionMessage = TextObject.GetEmpty();

		// Token: 0x040015AA RID: 5546
		[EditableScriptComponentVariable(false, "")]
		public TextObject ActionMessage = TextObject.GetEmpty();

		// Token: 0x040015AB RID: 5547
		private bool _needsSingleThreadTickOnce;

		// Token: 0x040015AF RID: 5551
		private bool _isDeactivated;

		// Token: 0x040015B0 RID: 5552
		private bool _isDisabledForPlayers;

		// Token: 0x02000655 RID: 1621
		[DefineSynchedMissionObjectType(typeof(UsableMissionObject))]
		public struct UsableMissionObjectRecord : ISynchedMissionObjectReadableRecord
		{
			// Token: 0x17000AC0 RID: 2752
			// (get) Token: 0x06004048 RID: 16456 RVA: 0x000F86AF File Offset: 0x000F68AF
			// (set) Token: 0x06004049 RID: 16457 RVA: 0x000F86B7 File Offset: 0x000F68B7
			public bool IsDeactivated { get; private set; }

			// Token: 0x17000AC1 RID: 2753
			// (get) Token: 0x0600404A RID: 16458 RVA: 0x000F86C0 File Offset: 0x000F68C0
			// (set) Token: 0x0600404B RID: 16459 RVA: 0x000F86C8 File Offset: 0x000F68C8
			public bool IsDisabledForPlayers { get; private set; }

			// Token: 0x17000AC2 RID: 2754
			// (get) Token: 0x0600404C RID: 16460 RVA: 0x000F86D1 File Offset: 0x000F68D1
			// (set) Token: 0x0600404D RID: 16461 RVA: 0x000F86D9 File Offset: 0x000F68D9
			public bool IsUserAgentExists { get; private set; }

			// Token: 0x17000AC3 RID: 2755
			// (get) Token: 0x0600404E RID: 16462 RVA: 0x000F86E2 File Offset: 0x000F68E2
			// (set) Token: 0x0600404F RID: 16463 RVA: 0x000F86EA File Offset: 0x000F68EA
			public int AgentIndex { get; private set; }

			// Token: 0x06004050 RID: 16464 RVA: 0x000F86F3 File Offset: 0x000F68F3
			public UsableMissionObjectRecord(bool isDeactivated, bool isDisabledForPlayers, bool isUserAgentExists, int agentIndex)
			{
				this.IsDeactivated = isDeactivated;
				this.IsDisabledForPlayers = isDisabledForPlayers;
				this.IsUserAgentExists = isUserAgentExists;
				this.AgentIndex = agentIndex;
			}

			// Token: 0x06004051 RID: 16465 RVA: 0x000F8712 File Offset: 0x000F6912
			public bool ReadFromNetwork(ref bool bufferReadValid)
			{
				this.IsDeactivated = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
				this.IsDisabledForPlayers = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
				this.IsUserAgentExists = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
				if (this.IsUserAgentExists)
				{
					this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref bufferReadValid);
				}
				return bufferReadValid;
			}
		}
	}
}
