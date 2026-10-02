using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200030C RID: 780
	public class MPConditionalEffect
	{
		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x000AB95A File Offset: 0x000A9B5A
		public MBReadOnlyList<MPPerkCondition> Conditions { get; }

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06002C9D RID: 11421 RVA: 0x000AB962 File Offset: 0x000A9B62
		public MBReadOnlyList<MPPerkEffectBase> Effects { get; }

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06002C9E RID: 11422 RVA: 0x000AB96C File Offset: 0x000A9B6C
		public MPPerkCondition.PerkEventFlags EventFlags
		{
			get
			{
				MPPerkCondition.PerkEventFlags perkEventFlags = MPPerkCondition.PerkEventFlags.None;
				foreach (MPPerkCondition mpperkCondition in this.Conditions)
				{
					perkEventFlags |= mpperkCondition.EventFlags;
				}
				return perkEventFlags;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002C9F RID: 11423 RVA: 0x000AB9C4 File Offset: 0x000A9BC4
		public bool IsTickRequired
		{
			get
			{
				using (List<MPPerkEffectBase>.Enumerator enumerator = this.Effects.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsTickRequired)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000ABA20 File Offset: 0x000A9C20
		public MPConditionalEffect(List<string> gameModes, XmlNode node)
		{
			MBList<MPPerkCondition> mblist = new MBList<MPPerkCondition>();
			MBList<MPPerkEffectBase> mblist2 = new MBList<MPPerkEffectBase>();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "Conditions")
				{
					using (IEnumerator enumerator2 = xmlNode.ChildNodes.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							XmlNode xmlNode2 = (XmlNode)obj2;
							if (xmlNode2.NodeType == XmlNodeType.Element)
							{
								mblist.Add(MPPerkCondition.CreateFrom(gameModes, xmlNode2));
							}
						}
						continue;
					}
				}
				if (xmlNode.Name == "Effects")
				{
					foreach (object obj3 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode3 = (XmlNode)obj3;
						if (xmlNode3.NodeType == XmlNodeType.Element)
						{
							MPPerkEffect mpperkEffect = MPPerkEffect.CreateFrom(xmlNode3);
							mblist2.Add(mpperkEffect);
						}
					}
				}
			}
			this.Conditions = mblist;
			this.Effects = mblist2;
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000ABB84 File Offset: 0x000A9D84
		public bool Check(MissionPeer peer)
		{
			using (List<MPPerkCondition>.Enumerator enumerator = this.Conditions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Check(peer))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000ABBE0 File Offset: 0x000A9DE0
		public bool Check(Agent agent)
		{
			using (List<MPPerkCondition>.Enumerator enumerator = this.Conditions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Check(agent))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000ABC3C File Offset: 0x000A9E3C
		public void OnEvent(bool isWarmup, MissionPeer peer, MPConditionalEffect.ConditionalEffectContainer container)
		{
			if (MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) <= 0)
			{
				if (peer == null)
				{
					return;
				}
				Agent controlledAgent = peer.ControlledAgent;
				bool? flag = ((controlledAgent != null) ? new bool?(controlledAgent.IsActive()) : null);
				bool flag2 = true;
				if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
				{
					return;
				}
			}
			bool flag3 = true;
			foreach (MPPerkCondition mpperkCondition in this.Conditions)
			{
				if (mpperkCondition.IsPeerCondition && !mpperkCondition.Check(peer))
				{
					flag3 = false;
					break;
				}
			}
			if (!flag3)
			{
				if (MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) > 0)
				{
					MBReadOnlyList<IFormationUnit> mbreadOnlyList;
					if (peer == null)
					{
						mbreadOnlyList = null;
					}
					else
					{
						Formation controlledFormation = peer.ControlledFormation;
						mbreadOnlyList = ((controlledFormation != null) ? controlledFormation.Arrangement.GetAllUnits() : null);
					}
					MBReadOnlyList<IFormationUnit> mbreadOnlyList2 = mbreadOnlyList;
					if (mbreadOnlyList2 == null)
					{
						return;
					}
					using (List<IFormationUnit>.Enumerator enumerator2 = mbreadOnlyList2.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Agent agent;
							if ((agent = enumerator2.Current as Agent) != null && agent.IsActive())
							{
								this.UpdateAgentState(isWarmup, container, agent, false);
							}
						}
						return;
					}
				}
				this.UpdateAgentState(isWarmup, container, peer.ControlledAgent, false);
				return;
			}
			if (MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) > 0)
			{
				MBReadOnlyList<IFormationUnit> mbreadOnlyList3;
				if (peer == null)
				{
					mbreadOnlyList3 = null;
				}
				else
				{
					Formation controlledFormation2 = peer.ControlledFormation;
					mbreadOnlyList3 = ((controlledFormation2 != null) ? controlledFormation2.Arrangement.GetAllUnits() : null);
				}
				MBReadOnlyList<IFormationUnit> mbreadOnlyList4 = mbreadOnlyList3;
				if (mbreadOnlyList4 == null)
				{
					return;
				}
				using (List<IFormationUnit>.Enumerator enumerator2 = mbreadOnlyList4.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Agent agent2;
						if ((agent2 = enumerator2.Current as Agent) != null && agent2.IsActive())
						{
							bool flag4 = true;
							foreach (MPPerkCondition mpperkCondition2 in this.Conditions)
							{
								if (!mpperkCondition2.IsPeerCondition && !mpperkCondition2.Check(agent2))
								{
									flag4 = false;
									break;
								}
							}
							this.UpdateAgentState(isWarmup, container, agent2, flag4);
						}
					}
					return;
				}
			}
			bool flag5 = true;
			foreach (MPPerkCondition mpperkCondition3 in this.Conditions)
			{
				if (!mpperkCondition3.IsPeerCondition && !mpperkCondition3.Check(peer.ControlledAgent))
				{
					flag5 = false;
					break;
				}
			}
			this.UpdateAgentState(isWarmup, container, peer.ControlledAgent, flag5);
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000ABEDC File Offset: 0x000AA0DC
		public void OnEvent(bool isWarmup, Agent agent, MPConditionalEffect.ConditionalEffectContainer container)
		{
			if (agent != null)
			{
				bool flag = true;
				using (List<MPPerkCondition>.Enumerator enumerator = this.Conditions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.Check(agent))
						{
							flag = false;
							break;
						}
					}
				}
				this.UpdateAgentState(isWarmup, container, agent, flag);
			}
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000ABF44 File Offset: 0x000AA144
		public void OnTick(bool isWarmup, MissionPeer peer, int tickCount)
		{
			if (MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) <= 0)
			{
				if (peer == null)
				{
					return;
				}
				Agent controlledAgent = peer.ControlledAgent;
				bool? flag = ((controlledAgent != null) ? new bool?(controlledAgent.IsActive()) : null);
				bool flag2 = true;
				if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
				{
					return;
				}
			}
			bool flag3 = true;
			foreach (MPPerkCondition mpperkCondition in this.Conditions)
			{
				if (mpperkCondition.IsPeerCondition && !mpperkCondition.Check(peer))
				{
					flag3 = false;
					break;
				}
			}
			if (flag3)
			{
				if (MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) > 0)
				{
					MBReadOnlyList<IFormationUnit> mbreadOnlyList;
					if (peer == null)
					{
						mbreadOnlyList = null;
					}
					else
					{
						Formation controlledFormation = peer.ControlledFormation;
						mbreadOnlyList = ((controlledFormation != null) ? controlledFormation.Arrangement.GetAllUnits() : null);
					}
					MBReadOnlyList<IFormationUnit> mbreadOnlyList2 = mbreadOnlyList;
					if (mbreadOnlyList2 == null)
					{
						return;
					}
					using (List<IFormationUnit>.Enumerator enumerator2 = mbreadOnlyList2.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Agent agent;
							if ((agent = enumerator2.Current as Agent) != null && agent.IsActive())
							{
								bool flag4 = true;
								foreach (MPPerkCondition mpperkCondition2 in this.Conditions)
								{
									if (!mpperkCondition2.IsPeerCondition && !mpperkCondition2.Check(agent))
									{
										flag4 = false;
										break;
									}
								}
								if (flag4)
								{
									foreach (MPPerkEffectBase mpperkEffectBase in this.Effects)
									{
										if ((!isWarmup || !mpperkEffectBase.IsDisabledInWarmup) && mpperkEffectBase.IsTickRequired)
										{
											mpperkEffectBase.OnTick(agent, tickCount);
										}
									}
								}
							}
						}
						return;
					}
				}
				bool flag5 = true;
				foreach (MPPerkCondition mpperkCondition3 in this.Conditions)
				{
					if (!mpperkCondition3.IsPeerCondition && !mpperkCondition3.Check(peer.ControlledAgent))
					{
						flag5 = false;
						break;
					}
				}
				if (flag5)
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in this.Effects)
					{
						if ((!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup) && mpperkEffectBase2.IsTickRequired)
						{
							mpperkEffectBase2.OnTick(peer.ControlledAgent, tickCount);
						}
					}
				}
			}
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000AC204 File Offset: 0x000AA404
		private void UpdateAgentState(bool isWarmup, MPConditionalEffect.ConditionalEffectContainer container, Agent agent, bool state)
		{
			if (container.GetState(this, agent) != state)
			{
				container.SetState(this, agent, state);
				foreach (MPPerkEffectBase mpperkEffectBase in this.Effects)
				{
					if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
					{
						mpperkEffectBase.OnUpdate(agent, state);
					}
				}
			}
		}

		// Token: 0x020005F5 RID: 1525
		public class ConditionalEffectContainer : List<MPConditionalEffect>
		{
			// Token: 0x06003F3B RID: 16187 RVA: 0x000F65EC File Offset: 0x000F47EC
			public ConditionalEffectContainer()
			{
			}

			// Token: 0x06003F3C RID: 16188 RVA: 0x000F65F4 File Offset: 0x000F47F4
			public ConditionalEffectContainer(IEnumerable<MPConditionalEffect> conditionalEffects)
				: base(conditionalEffects)
			{
			}

			// Token: 0x06003F3D RID: 16189 RVA: 0x000F6600 File Offset: 0x000F4800
			public bool GetState(MPConditionalEffect conditionalEffect, Agent agent)
			{
				ConditionalWeakTable<Agent, MPConditionalEffect.ConditionalEffectContainer.ConditionState> conditionalWeakTable;
				MPConditionalEffect.ConditionalEffectContainer.ConditionState conditionState;
				return this._states != null && this._states.TryGetValue(conditionalEffect, out conditionalWeakTable) && conditionalWeakTable.TryGetValue(agent, out conditionState) && conditionState.IsSatisfied;
			}

			// Token: 0x06003F3E RID: 16190 RVA: 0x000F6638 File Offset: 0x000F4838
			public void SetState(MPConditionalEffect conditionalEffect, Agent agent, bool state)
			{
				if (this._states == null)
				{
					this._states = new Dictionary<MPConditionalEffect, ConditionalWeakTable<Agent, MPConditionalEffect.ConditionalEffectContainer.ConditionState>>();
					ConditionalWeakTable<Agent, MPConditionalEffect.ConditionalEffectContainer.ConditionState> conditionalWeakTable = new ConditionalWeakTable<Agent, MPConditionalEffect.ConditionalEffectContainer.ConditionState>();
					conditionalWeakTable.Add(agent, new MPConditionalEffect.ConditionalEffectContainer.ConditionState
					{
						IsSatisfied = state
					});
					this._states.Add(conditionalEffect, conditionalWeakTable);
					return;
				}
				ConditionalWeakTable<Agent, MPConditionalEffect.ConditionalEffectContainer.ConditionState> conditionalWeakTable2;
				if (!this._states.TryGetValue(conditionalEffect, out conditionalWeakTable2))
				{
					conditionalWeakTable2 = new ConditionalWeakTable<Agent, MPConditionalEffect.ConditionalEffectContainer.ConditionState>();
					conditionalWeakTable2.Add(agent, new MPConditionalEffect.ConditionalEffectContainer.ConditionState
					{
						IsSatisfied = state
					});
					this._states.Add(conditionalEffect, conditionalWeakTable2);
					return;
				}
				MPConditionalEffect.ConditionalEffectContainer.ConditionState conditionState;
				if (!conditionalWeakTable2.TryGetValue(agent, out conditionState))
				{
					conditionalWeakTable2.Add(agent, new MPConditionalEffect.ConditionalEffectContainer.ConditionState
					{
						IsSatisfied = state
					});
					return;
				}
				conditionState.IsSatisfied = state;
			}

			// Token: 0x06003F3F RID: 16191 RVA: 0x000F66DC File Offset: 0x000F48DC
			public void ResetStates()
			{
				this._states = null;
			}

			// Token: 0x0400200C RID: 8204
			private Dictionary<MPConditionalEffect, ConditionalWeakTable<Agent, MPConditionalEffect.ConditionalEffectContainer.ConditionState>> _states;

			// Token: 0x020006C4 RID: 1732
			private class ConditionState
			{
				// Token: 0x17000B0E RID: 2830
				// (get) Token: 0x06004237 RID: 16951 RVA: 0x000FCD6F File Offset: 0x000FAF6F
				// (set) Token: 0x06004238 RID: 16952 RVA: 0x000FCD77 File Offset: 0x000FAF77
				public bool IsSatisfied { get; set; }
			}
		}
	}
}
