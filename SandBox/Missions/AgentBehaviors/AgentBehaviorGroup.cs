using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x020000A2 RID: 162
	public abstract class AgentBehaviorGroup
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0002CC87 File Offset: 0x0002AE87
		public Agent OwnerAgent
		{
			get
			{
				return this.Navigator.OwnerAgent;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0002CC94 File Offset: 0x0002AE94
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0002CC9C File Offset: 0x0002AE9C
		public AgentBehavior ScriptedBehavior { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0002CCA5 File Offset: 0x0002AEA5
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0002CCAD File Offset: 0x0002AEAD
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (this._isActive != value)
				{
					this._isActive = value;
					if (this._isActive)
					{
						this.OnActivate();
						return;
					}
					this.OnDeactivate();
				}
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0002CCD4 File Offset: 0x0002AED4
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0002CCDC File Offset: 0x0002AEDC
		public Mission Mission { get; private set; }

		// Token: 0x060006B7 RID: 1719 RVA: 0x0002CCE5 File Offset: 0x0002AEE5
		protected AgentBehaviorGroup(AgentNavigator navigator, Mission mission)
		{
			this.Mission = mission;
			this.Behaviors = new List<AgentBehavior>();
			this.Navigator = navigator;
			this._isActive = false;
			this.ScriptedBehavior = null;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0002CD20 File Offset: 0x0002AF20
		public T AddBehavior<T>() where T : AgentBehavior
		{
			T t = Activator.CreateInstance(typeof(T), new object[] { this }) as T;
			if (t != null)
			{
				foreach (AgentBehavior agentBehavior in this.Behaviors)
				{
					if (agentBehavior.GetType() == t.GetType())
					{
						return agentBehavior as T;
					}
				}
				this.Behaviors.Add(t);
				return t;
			}
			return t;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0002CDD4 File Offset: 0x0002AFD4
		public T GetBehavior<T>() where T : AgentBehavior
		{
			foreach (AgentBehavior agentBehavior in this.Behaviors)
			{
				if (agentBehavior is T)
				{
					return (T)((object)agentBehavior);
				}
			}
			return default(T);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0002CE3C File Offset: 0x0002B03C
		public bool HasBehavior<T>() where T : AgentBehavior
		{
			using (List<AgentBehavior>.Enumerator enumerator = this.Behaviors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current is T)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0002CE98 File Offset: 0x0002B098
		public void RemoveBehavior<T>() where T : AgentBehavior
		{
			for (int i = 0; i < this.Behaviors.Count; i++)
			{
				if (this.Behaviors[i] is T)
				{
					bool isActive = this.Behaviors[i].IsActive;
					this.Behaviors[i].IsActive = false;
					if (this.ScriptedBehavior == this.Behaviors[i])
					{
						this.ScriptedBehavior = null;
					}
					this.Behaviors.RemoveAt(i);
					if (isActive)
					{
						this.ForceThink(0f);
					}
				}
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0002CF28 File Offset: 0x0002B128
		public void SetScriptedBehavior<T>() where T : AgentBehavior
		{
			foreach (AgentBehavior agentBehavior in this.Behaviors)
			{
				if (agentBehavior is T)
				{
					this.ScriptedBehavior = agentBehavior;
					this.ForceThink(0f);
					break;
				}
			}
			foreach (AgentBehavior agentBehavior2 in this.Behaviors)
			{
				if (agentBehavior2 != this.ScriptedBehavior)
				{
					agentBehavior2.IsActive = false;
				}
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0002CFDC File Offset: 0x0002B1DC
		public void DisableScriptedBehavior()
		{
			if (this.ScriptedBehavior != null)
			{
				this.ScriptedBehavior.IsActive = false;
				this.ScriptedBehavior = null;
				this.ForceThink(0f);
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0002D004 File Offset: 0x0002B204
		public void DisableAllBehaviors()
		{
			foreach (AgentBehavior agentBehavior in this.Behaviors)
			{
				agentBehavior.IsActive = false;
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0002D058 File Offset: 0x0002B258
		public AgentBehavior GetActiveBehavior()
		{
			foreach (AgentBehavior agentBehavior in this.Behaviors)
			{
				if (agentBehavior.IsActive)
				{
					return agentBehavior;
				}
			}
			return null;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0002D0B4 File Offset: 0x0002B2B4
		public virtual void Tick(float dt, bool isSimulation)
		{
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0002D0B6 File Offset: 0x0002B2B6
		public virtual void ConversationTick()
		{
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0002D0B8 File Offset: 0x0002B2B8
		public virtual void OnAgentRemoved(Agent agent)
		{
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0002D0BA File Offset: 0x0002B2BA
		protected virtual void OnActivate()
		{
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0002D0BC File Offset: 0x0002B2BC
		protected virtual void OnDeactivate()
		{
			foreach (AgentBehavior agentBehavior in this.Behaviors)
			{
				agentBehavior.IsActive = false;
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0002D110 File Offset: 0x0002B310
		public virtual float GetScore(bool isSimulation)
		{
			return 0f;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0002D117 File Offset: 0x0002B317
		public virtual void ForceThink(float inSeconds)
		{
		}

		// Token: 0x04000397 RID: 919
		public AgentNavigator Navigator;

		// Token: 0x04000398 RID: 920
		public List<AgentBehavior> Behaviors;

		// Token: 0x04000399 RID: 921
		protected float CheckBehaviorTime = 5f;

		// Token: 0x0400039A RID: 922
		protected Timer CheckBehaviorTimer;

		// Token: 0x0400039C RID: 924
		private bool _isActive;
	}
}
