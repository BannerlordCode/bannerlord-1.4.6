using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Network.Gameplay.Perks.Conditions;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000315 RID: 789
	public class MPPerkObject : IReadOnlyPerkObject
	{
		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002CEE RID: 11502 RVA: 0x000AC985 File Offset: 0x000AAB85
		public TextObject Name
		{
			get
			{
				return new TextObject(this._name, null);
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002CEF RID: 11503 RVA: 0x000AC993 File Offset: 0x000AAB93
		public TextObject Description
		{
			get
			{
				return new TextObject(this._description, null);
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x000AC9A1 File Offset: 0x000AABA1
		public bool HasBannerBearer { get; }

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x000AC9A9 File Offset: 0x000AABA9
		public List<string> GameModes { get; }

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x000AC9B1 File Offset: 0x000AABB1
		public int PerkListIndex { get; }

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06002CF3 RID: 11507 RVA: 0x000AC9B9 File Offset: 0x000AABB9
		public string IconId { get; }

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x000AC9C1 File Offset: 0x000AABC1
		public string HeroIdleAnimOverride { get; }

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x000AC9C9 File Offset: 0x000AABC9
		public string HeroMountIdleAnimOverride { get; }

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x000AC9D1 File Offset: 0x000AABD1
		public string TroopIdleAnimOverride { get; }

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002CF7 RID: 11511 RVA: 0x000AC9D9 File Offset: 0x000AABD9
		public string TroopMountIdleAnimOverride { get; }

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000AC9E4 File Offset: 0x000AABE4
		public MPPerkObject(MissionPeer peer, string name, string description, List<string> gameModes, int perkListIndex, string iconId, IEnumerable<MPConditionalEffect> conditionalEffects, IEnumerable<MPPerkEffectBase> effects, string heroIdleAnimOverride, string heroMountIdleAnimOverride, string troopIdleAnimOverride, string troopMountIdleAnimOverride)
		{
			this._peer = peer;
			this._name = name;
			this._description = description;
			this.GameModes = gameModes;
			this.PerkListIndex = perkListIndex;
			this.IconId = iconId;
			this._conditionalEffects = new MPConditionalEffect.ConditionalEffectContainer(conditionalEffects);
			this._effects = new List<MPPerkEffectBase>(effects);
			this.HeroIdleAnimOverride = heroIdleAnimOverride;
			this.HeroMountIdleAnimOverride = heroMountIdleAnimOverride;
			this.TroopIdleAnimOverride = troopIdleAnimOverride;
			this.TroopMountIdleAnimOverride = troopMountIdleAnimOverride;
			this._perkEventFlags = MPPerkCondition.PerkEventFlags.None;
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				foreach (MPPerkCondition mpperkCondition in mpconditionalEffect.Conditions)
				{
					this._perkEventFlags |= mpperkCondition.EventFlags;
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect2 in this._conditionalEffects)
			{
				using (List<MPPerkCondition>.Enumerator enumerator2 = mpconditionalEffect2.Conditions.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current is BannerBearerCondition)
						{
							this.HasBannerBearer = true;
						}
					}
				}
			}
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000ACB74 File Offset: 0x000AAD74
		private MPPerkObject(XmlNode node)
		{
			this._peer = null;
			this._conditionalEffects = new MPConditionalEffect.ConditionalEffectContainer();
			this._effects = new List<MPPerkEffectBase>();
			this._name = node.Attributes["name"].Value;
			this._description = node.Attributes["description"].Value;
			this.GameModes = new List<string>(node.Attributes["game_mode"].Value.Split(new char[] { ',' }));
			for (int i = 0; i < this.GameModes.Count; i++)
			{
				this.GameModes[i] = this.GameModes[i].Trim();
			}
			this.IconId = node.Attributes["icon"].Value;
			this.PerkListIndex = 0;
			XmlNode xmlNode = node.Attributes["perk_list"];
			if (xmlNode != null)
			{
				this.PerkListIndex = Convert.ToInt32(xmlNode.Value);
				int perkListIndex = this.PerkListIndex;
				this.PerkListIndex = perkListIndex - 1;
			}
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				if (xmlNode2.NodeType != XmlNodeType.Comment && xmlNode2.NodeType != XmlNodeType.SignificantWhitespace)
				{
					if (xmlNode2.Name == "ConditionalEffect")
					{
						this._conditionalEffects.Add(new MPConditionalEffect(this.GameModes, xmlNode2));
					}
					else if (xmlNode2.Name == "Effect")
					{
						this._effects.Add(MPPerkEffect.CreateFrom(xmlNode2));
					}
					else if (xmlNode2.Name == "OnSpawnEffect")
					{
						this._effects.Add(MPOnSpawnPerkEffect.CreateFrom(xmlNode2));
					}
					else if (xmlNode2.Name == "RandomOnSpawnEffect")
					{
						this._effects.Add(MPRandomOnSpawnPerkEffect.CreateFrom(xmlNode2));
					}
					else
					{
						Debug.FailedAssert("Unknown child element", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Network\\Gameplay\\Perks\\MPPerkObject.cs", ".ctor", 750);
					}
				}
			}
			XmlAttribute xmlAttribute = node.Attributes["hero_idle_anim"];
			this.HeroIdleAnimOverride = ((xmlAttribute != null) ? xmlAttribute.Value : null);
			XmlAttribute xmlAttribute2 = node.Attributes["hero_mount_idle_anim"];
			this.HeroMountIdleAnimOverride = ((xmlAttribute2 != null) ? xmlAttribute2.Value : null);
			XmlAttribute xmlAttribute3 = node.Attributes["troop_idle_anim"];
			this.TroopIdleAnimOverride = ((xmlAttribute3 != null) ? xmlAttribute3.Value : null);
			XmlAttribute xmlAttribute4 = node.Attributes["troop_mount_idle_anim"];
			this.TroopMountIdleAnimOverride = ((xmlAttribute4 != null) ? xmlAttribute4.Value : null);
			this._perkEventFlags = MPPerkCondition.PerkEventFlags.None;
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				foreach (MPPerkCondition mpperkCondition in mpconditionalEffect.Conditions)
				{
					this._perkEventFlags |= mpperkCondition.EventFlags;
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect2 in this._conditionalEffects)
			{
				using (List<MPPerkCondition>.Enumerator enumerator3 = mpconditionalEffect2.Conditions.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						if (enumerator3.Current is BannerBearerCondition)
						{
							this.HasBannerBearer = true;
						}
					}
				}
			}
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000ACF5C File Offset: 0x000AB15C
		public MPPerkObject Clone(MissionPeer peer)
		{
			return new MPPerkObject(peer, this._name, this._description, this.GameModes, this.PerkListIndex, this.IconId, this._conditionalEffects, this._effects, this.HeroIdleAnimOverride, this.HeroMountIdleAnimOverride, this.TroopIdleAnimOverride, this.TroopMountIdleAnimOverride);
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000ACFB1 File Offset: 0x000AB1B1
		public void Reset()
		{
			this._conditionalEffects.ResetStates();
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000ACFC0 File Offset: 0x000AB1C0
		private void OnEvent(bool isWarmup, MPPerkCondition.PerkEventFlags flags)
		{
			if ((flags & this._perkEventFlags) != MPPerkCondition.PerkEventFlags.None)
			{
				foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
				{
					if ((flags & mpconditionalEffect.EventFlags) != MPPerkCondition.PerkEventFlags.None)
					{
						mpconditionalEffect.OnEvent(isWarmup, this._peer, this._conditionalEffects);
					}
				}
			}
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000AD034 File Offset: 0x000AB234
		private void OnEvent(bool isWarmup, Agent agent, MPPerkCondition.PerkEventFlags flags)
		{
			if (((agent != null) ? agent.MissionPeer : null) == null && agent != null)
			{
				MissionPeer owningAgentMissionPeer = agent.OwningAgentMissionPeer;
			}
			if ((flags & this._perkEventFlags) != MPPerkCondition.PerkEventFlags.None)
			{
				foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
				{
					if ((flags & mpconditionalEffect.EventFlags) != MPPerkCondition.PerkEventFlags.None)
					{
						mpconditionalEffect.OnEvent(isWarmup, agent, this._conditionalEffects);
					}
				}
			}
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000AD0BC File Offset: 0x000AB2BC
		private void OnTick(bool isWarmup, int tickCount)
		{
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.IsTickRequired)
				{
					mpconditionalEffect.OnTick(isWarmup, this._peer, tickCount);
				}
			}
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if ((!isWarmup || !mpperkEffectBase.IsDisabledInWarmup) && mpperkEffectBase.IsTickRequired)
				{
					mpperkEffectBase.OnTick(this._peer, tickCount);
				}
			}
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x000AD17C File Offset: 0x000AB37C
		private float GetDamage(bool isWarmup, Agent agent, WeaponComponentData attackerWeapon, DamageTypes damageType, bool isAlternativeAttack)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetDamage(attackerWeapon, damageType, isAlternativeAttack);
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetDamage(attackerWeapon, damageType, isAlternativeAttack);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x000AD2A0 File Offset: 0x000AB4A0
		private float GetMountDamage(bool isWarmup, Agent agent, WeaponComponentData attackerWeapon, DamageTypes damageType, bool isAlternativeAttack)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetMountDamage(attackerWeapon, damageType, isAlternativeAttack);
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetMountDamage(attackerWeapon, damageType, isAlternativeAttack);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x000AD3C4 File Offset: 0x000AB5C4
		private float GetDamageTaken(bool isWarmup, Agent agent, WeaponComponentData attackerWeapon, DamageTypes damageType)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetDamageTaken(attackerWeapon, damageType);
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetDamageTaken(attackerWeapon, damageType);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000AD4E4 File Offset: 0x000AB6E4
		private float GetMountDamageTaken(bool isWarmup, Agent agent, WeaponComponentData attackerWeapon, DamageTypes damageType)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetMountDamageTaken(attackerWeapon, damageType);
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetMountDamageTaken(attackerWeapon, damageType);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000AD604 File Offset: 0x000AB804
		private float GetSpeedBonusEffectiveness(bool isWarmup, Agent agent, WeaponComponentData attackerWeapon, DamageTypes damageType)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetSpeedBonusEffectiveness(agent, attackerWeapon, damageType);
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetSpeedBonusEffectiveness(agent, attackerWeapon, damageType);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000AD728 File Offset: 0x000AB928
		private float GetShieldDamage(bool isWarmup, Agent attacker, Agent defender, bool isCorrectSideBlock)
		{
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetShieldDamage(isCorrectSideBlock);
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(attacker))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetShieldDamage(isCorrectSideBlock);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000AD82C File Offset: 0x000ABA2C
		private float GetShieldDamageTaken(bool isWarmup, Agent attacker, Agent defender, bool isCorrectSideBlock)
		{
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetShieldDamageTaken(isCorrectSideBlock);
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(defender))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetShieldDamageTaken(isCorrectSideBlock);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000AD930 File Offset: 0x000ABB30
		private float GetRangedAccuracy(bool isWarmup, Agent agent)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetRangedAccuracy();
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetRangedAccuracy();
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000ADA4C File Offset: 0x000ABC4C
		private float GetThrowingWeaponSpeed(bool isWarmup, Agent agent, WeaponComponentData attackerWeapon)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetThrowingWeaponSpeed(attackerWeapon);
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetThrowingWeaponSpeed(attackerWeapon);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000ADB68 File Offset: 0x000ABD68
		private float GetDamageInterruptionThreshold(bool isWarmup, Agent agent)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetDamageInterruptionThreshold();
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetDamageInterruptionThreshold();
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000ADC84 File Offset: 0x000ABE84
		private float GetMountManeuver(bool isWarmup, Agent agent)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetMountManeuver();
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetMountManeuver();
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000ADDA0 File Offset: 0x000ABFA0
		private float GetMountSpeed(bool isWarmup, Agent agent)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetMountSpeed();
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetMountSpeed();
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000ADEBC File Offset: 0x000AC0BC
		private float GetRangedHeadShotDamage(bool isWarmup, Agent agent)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetRangedHeadShotDamage();
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetRangedHeadShotDamage();
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000ADFD8 File Offset: 0x000AC1D8
		public int GetExtraTroopCount(bool isWarmup)
		{
			int num = 0;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				IOnSpawnPerkEffect onSpawnPerkEffect = mpperkEffectBase as IOnSpawnPerkEffect;
				if (onSpawnPerkEffect != null && (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup))
				{
					num += onSpawnPerkEffect.GetExtraTroopCount();
				}
			}
			return num;
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x000AE048 File Offset: 0x000AC248
		public List<ValueTuple<EquipmentIndex, EquipmentElement>> GetAlternativeEquipments(bool isWarmup, bool isPlayer, List<ValueTuple<EquipmentIndex, EquipmentElement>> alternativeEquipments, bool getAllEquipments = false)
		{
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				IOnSpawnPerkEffect onSpawnPerkEffect = mpperkEffectBase as IOnSpawnPerkEffect;
				if (onSpawnPerkEffect != null && (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup))
				{
					alternativeEquipments = onSpawnPerkEffect.GetAlternativeEquipments(isPlayer, alternativeEquipments, getAllEquipments);
				}
			}
			return alternativeEquipments;
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000AE0B8 File Offset: 0x000AC2B8
		private float GetDrivenPropertyBonus(bool isWarmup, Agent agent, DrivenProperty drivenProperty, float baseValue)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetDrivenPropertyBonus(drivenProperty, baseValue);
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetDrivenPropertyBonus(drivenProperty, baseValue);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000AE1D8 File Offset: 0x000AC3D8
		public float GetDrivenPropertyBonusOnSpawn(bool isWarmup, bool isPlayer, DrivenProperty drivenProperty, float baseValue)
		{
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				IOnSpawnPerkEffect onSpawnPerkEffect = mpperkEffectBase as IOnSpawnPerkEffect;
				if (onSpawnPerkEffect != null && (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup))
				{
					num += onSpawnPerkEffect.GetDrivenPropertyBonusOnSpawn(isPlayer, drivenProperty, baseValue);
				}
			}
			return num;
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000AE250 File Offset: 0x000AC450
		public float GetHitpoints(bool isWarmup, bool isPlayer)
		{
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				IOnSpawnPerkEffect onSpawnPerkEffect = mpperkEffectBase as IOnSpawnPerkEffect;
				if (onSpawnPerkEffect != null && (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup))
				{
					num += onSpawnPerkEffect.GetHitpoints(isPlayer);
				}
			}
			return num;
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000AE2C4 File Offset: 0x000AC4C4
		private float GetEncumbrance(bool isWarmup, Agent agent, bool isOnBody)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			float num = 0f;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetEncumbrance(isOnBody);
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetEncumbrance(isOnBody);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000AE3E0 File Offset: 0x000AC5E0
		private int GetGoldOnKill(bool isWarmup, Agent agent, float attackerValue, float victimValue)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			int num = 0;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetGoldOnKill(attackerValue, victimValue);
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetGoldOnKill(attackerValue, victimValue);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x000AE4FC File Offset: 0x000AC6FC
		private int GetGoldOnAssist(bool isWarmup, Agent agent)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			int num = 0;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetGoldOnAssist();
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetGoldOnAssist();
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000AE614 File Offset: 0x000AC814
		private int GetRewardedGoldOnAssist(bool isWarmup, Agent agent)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			int num = 0;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					num += mpperkEffectBase.GetRewardedGoldOnAssist();
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							num += mpperkEffectBase2.GetRewardedGoldOnAssist();
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000AE72C File Offset: 0x000AC92C
		private bool GetIsTeamRewardedOnDeath(bool isWarmup, Agent agent)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if ((!isWarmup || !mpperkEffectBase.IsDisabledInWarmup) && mpperkEffectBase.GetIsTeamRewardedOnDeath())
				{
					return true;
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if ((!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup) && mpperkEffectBase2.GetIsTeamRewardedOnDeath())
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000AE84C File Offset: 0x000ACA4C
		private void CalculateRewardedGoldOnDeath(bool isWarmup, Agent agent, List<ValueTuple<MissionPeer, int>> teamMembers)
		{
			Agent agent2;
			if ((agent2 = agent) == null)
			{
				MissionPeer peer = this._peer;
				agent2 = ((peer != null) ? peer.ControlledAgent : null);
			}
			agent = agent2;
			teamMembers.Shuffle<ValueTuple<MissionPeer, int>>();
			foreach (MPPerkEffectBase mpperkEffectBase in this._effects)
			{
				if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
				{
					mpperkEffectBase.CalculateRewardedGoldOnDeath(agent, teamMembers);
				}
			}
			foreach (MPConditionalEffect mpconditionalEffect in this._conditionalEffects)
			{
				if (mpconditionalEffect.Check(agent))
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in mpconditionalEffect.Effects)
					{
						if (!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup)
						{
							mpperkEffectBase2.CalculateRewardedGoldOnDeath(agent, teamMembers);
						}
					}
				}
			}
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000AE960 File Offset: 0x000ACB60
		public static int GetTroopCount(MultiplayerClassDivisions.MPHeroClass heroClass, int botsPerFormation, MPPerkObject.MPOnSpawnPerkHandler onSpawnPerkHandler)
		{
			int num = MathF.Ceiling((float)botsPerFormation * heroClass.TroopMultiplier - 1E-05f);
			if (onSpawnPerkHandler != null)
			{
				num += (int)onSpawnPerkHandler.GetExtraTroopCount();
			}
			return MathF.Max(num, 1);
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000AE996 File Offset: 0x000ACB96
		public static IReadOnlyPerkObject Deserialize(XmlNode node)
		{
			return new MPPerkObject(node);
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x000AE9A0 File Offset: 0x000ACBA0
		public static MPPerkObject.MPPerkHandler GetPerkHandler(Agent agent)
		{
			object obj;
			if (agent == null)
			{
				obj = null;
			}
			else
			{
				MissionPeer missionPeer = agent.MissionPeer;
				obj = ((missionPeer != null) ? missionPeer.SelectedPerks : null);
			}
			object obj2;
			if ((obj2 = obj) == null)
			{
				if (agent == null)
				{
					obj2 = null;
				}
				else
				{
					MissionPeer owningAgentMissionPeer = agent.OwningAgentMissionPeer;
					obj2 = ((owningAgentMissionPeer != null) ? owningAgentMissionPeer.SelectedPerks : null);
				}
			}
			MBReadOnlyList<MPPerkObject> mbreadOnlyList = obj2;
			if (mbreadOnlyList != null && mbreadOnlyList.Count > 0 && !agent.IsMount)
			{
				return new MPPerkObject.MPPerkHandlerInstance(agent);
			}
			return null;
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000AEA00 File Offset: 0x000ACC00
		public static MPPerkObject.MPPerkHandler GetPerkHandler(MissionPeer peer)
		{
			MBReadOnlyList<MPPerkObject> mbreadOnlyList = ((peer != null) ? peer.SelectedPerks : null) ?? ((peer != null) ? peer.SelectedPerks : null);
			if (mbreadOnlyList != null && mbreadOnlyList.Count > 0)
			{
				return new MPPerkObject.MPPerkHandlerInstance(peer);
			}
			return null;
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000AEA40 File Offset: 0x000ACC40
		public static MPPerkObject.MPCombatPerkHandler GetCombatPerkHandler(Agent attacker, Agent defender)
		{
			Agent agent = ((attacker != null && attacker.IsMount) ? attacker.RiderAgent : attacker);
			object obj;
			if (agent == null)
			{
				obj = null;
			}
			else
			{
				MissionPeer missionPeer = agent.MissionPeer;
				obj = ((missionPeer != null) ? missionPeer.SelectedPerks : null);
			}
			object obj2;
			if ((obj2 = obj) == null)
			{
				if (agent == null)
				{
					obj2 = null;
				}
				else
				{
					MissionPeer owningAgentMissionPeer = agent.OwningAgentMissionPeer;
					obj2 = ((owningAgentMissionPeer != null) ? owningAgentMissionPeer.SelectedPerks : null);
				}
			}
			MBReadOnlyList<MPPerkObject> mbreadOnlyList = obj2;
			Agent agent2 = ((defender != null && defender.IsMount) ? defender.RiderAgent : defender);
			object obj3;
			if (agent2 == null)
			{
				obj3 = null;
			}
			else
			{
				MissionPeer missionPeer2 = agent2.MissionPeer;
				obj3 = ((missionPeer2 != null) ? missionPeer2.SelectedPerks : null);
			}
			object obj4;
			if ((obj4 = obj3) == null)
			{
				if (agent2 == null)
				{
					obj4 = null;
				}
				else
				{
					MissionPeer owningAgentMissionPeer2 = agent2.OwningAgentMissionPeer;
					obj4 = ((owningAgentMissionPeer2 != null) ? owningAgentMissionPeer2.SelectedPerks : null);
				}
			}
			MBReadOnlyList<MPPerkObject> mbreadOnlyList2 = obj4;
			if (attacker != defender && ((mbreadOnlyList != null && mbreadOnlyList.Count > 0) || (mbreadOnlyList2 != null && mbreadOnlyList2.Count > 0)))
			{
				return new MPPerkObject.MPCombatPerkHandlerInstance(attacker, defender);
			}
			return null;
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000AEB06 File Offset: 0x000ACD06
		public static MPPerkObject.MPOnSpawnPerkHandler GetOnSpawnPerkHandler(MissionPeer peer)
		{
			if ((((peer != null) ? peer.SelectedPerks : null) ?? ((peer != null) ? peer.SelectedPerks : null)) != null)
			{
				return new MPPerkObject.MPOnSpawnPerkHandlerInstance(peer);
			}
			return null;
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000AEB2E File Offset: 0x000ACD2E
		public static MPPerkObject.MPOnSpawnPerkHandler GetOnSpawnPerkHandler(IEnumerable<IReadOnlyPerkObject> perks)
		{
			if (perks != null)
			{
				return new MPPerkObject.MPOnSpawnPerkHandlerInstance(perks);
			}
			return null;
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000AEB3C File Offset: 0x000ACD3C
		public static void RaiseEventForAllPeers(MPPerkCondition.PerkEventFlags flags)
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
				{
					MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(networkCommunicator.GetComponent<MissionPeer>());
					if (perkHandler != null)
					{
						perkHandler.OnEvent(flags);
					}
				}
			}
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x000AEBA4 File Offset: 0x000ACDA4
		public static void RaiseEventForAllPeersOnTeam(Team side, MPPerkCondition.PerkEventFlags flags)
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
				{
					MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
					if (component != null && component.Team == side)
					{
						MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(component);
						if (perkHandler != null)
						{
							perkHandler.OnEvent(flags);
						}
					}
				}
			}
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000AEC1C File Offset: 0x000ACE1C
		public static void TickAllPeerPerks(int tickCount)
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
				{
					MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
					if (component != null && component.Team != null && component.Culture != null && component.Team.Side != BattleSideEnum.None)
					{
						MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(component);
						if (perkHandler != null)
						{
							perkHandler.OnTick(tickCount);
						}
					}
				}
			}
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000AECA8 File Offset: 0x000ACEA8
		[CommandLineFunctionality.CommandLineArgumentFunction("raise_event", "mp_perks")]
		public static string RaiseEventForAllPeersCommand(List<string> strings)
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				MPPerkCondition.PerkEventFlags perkEventFlags = MPPerkCondition.PerkEventFlags.None;
				using (List<string>.Enumerator enumerator = strings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MPPerkCondition.PerkEventFlags perkEventFlags2;
						if (Enum.TryParse<MPPerkCondition.PerkEventFlags>(enumerator.Current, true, out perkEventFlags2))
						{
							perkEventFlags |= perkEventFlags2;
						}
					}
				}
				MPPerkObject.RaiseEventForAllPeers(perkEventFlags);
				return "Raised event with flags " + perkEventFlags;
			}
			return "Can't run this command on clients";
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000AED24 File Offset: 0x000ACF24
		[CommandLineFunctionality.CommandLineArgumentFunction("tick_perks", "mp_perks")]
		public static string TickAllPeerPerksCommand(List<string> strings)
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				int num;
				if (strings.Count == 0 || !int.TryParse(strings[0], out num))
				{
					num = 1;
				}
				MPPerkObject.TickAllPeerPerks(num);
				return "Peer perks on tick with tick count " + num;
			}
			return "Can't run this command on clients";
		}

		// Token: 0x040011BC RID: 4540
		private readonly MissionPeer _peer;

		// Token: 0x040011BD RID: 4541
		private readonly MPConditionalEffect.ConditionalEffectContainer _conditionalEffects;

		// Token: 0x040011BE RID: 4542
		private readonly MPPerkCondition.PerkEventFlags _perkEventFlags;

		// Token: 0x040011BF RID: 4543
		private readonly string _name;

		// Token: 0x040011C0 RID: 4544
		private readonly string _description;

		// Token: 0x040011C1 RID: 4545
		private readonly List<MPPerkEffectBase> _effects;

		// Token: 0x020005FB RID: 1531
		private class MPOnSpawnPerkHandlerInstance : MPPerkObject.MPOnSpawnPerkHandler
		{
			// Token: 0x06003F49 RID: 16201 RVA: 0x000F678D File Offset: 0x000F498D
			public MPOnSpawnPerkHandlerInstance(IEnumerable<IReadOnlyPerkObject> perks)
				: base(perks)
			{
			}

			// Token: 0x06003F4A RID: 16202 RVA: 0x000F6796 File Offset: 0x000F4996
			public MPOnSpawnPerkHandlerInstance(MissionPeer peer)
				: base(peer)
			{
			}
		}

		// Token: 0x020005FC RID: 1532
		private class MPPerkHandlerInstance : MPPerkObject.MPPerkHandler
		{
			// Token: 0x06003F4B RID: 16203 RVA: 0x000F679F File Offset: 0x000F499F
			public MPPerkHandlerInstance(Agent agent)
				: base(agent)
			{
			}

			// Token: 0x06003F4C RID: 16204 RVA: 0x000F67A8 File Offset: 0x000F49A8
			public MPPerkHandlerInstance(MissionPeer peer)
				: base(peer)
			{
			}
		}

		// Token: 0x020005FD RID: 1533
		private class MPCombatPerkHandlerInstance : MPPerkObject.MPCombatPerkHandler
		{
			// Token: 0x06003F4D RID: 16205 RVA: 0x000F67B1 File Offset: 0x000F49B1
			public MPCombatPerkHandlerInstance(Agent attacker, Agent defender)
				: base(attacker, defender)
			{
			}
		}

		// Token: 0x020005FE RID: 1534
		public class MPOnSpawnPerkHandler
		{
			// Token: 0x17000AA3 RID: 2723
			// (get) Token: 0x06003F4E RID: 16206 RVA: 0x000F67BC File Offset: 0x000F49BC
			public bool IsWarmup
			{
				get
				{
					Mission mission = Mission.Current;
					bool? flag;
					if (mission == null)
					{
						flag = null;
					}
					else
					{
						MissionMultiplayerGameModeBase missionBehavior = mission.GetMissionBehavior<MissionMultiplayerGameModeBase>();
						if (missionBehavior == null)
						{
							flag = null;
						}
						else
						{
							MultiplayerWarmupComponent warmupComponent = missionBehavior.WarmupComponent;
							flag = ((warmupComponent != null) ? new bool?(warmupComponent.IsInWarmup) : null);
						}
					}
					return flag ?? false;
				}
			}

			// Token: 0x06003F4F RID: 16207 RVA: 0x000F6822 File Offset: 0x000F4A22
			protected MPOnSpawnPerkHandler(IEnumerable<IReadOnlyPerkObject> perks)
			{
				this._perks = perks;
			}

			// Token: 0x06003F50 RID: 16208 RVA: 0x000F6831 File Offset: 0x000F4A31
			protected MPOnSpawnPerkHandler(MissionPeer peer)
			{
				this._perks = peer.SelectedPerks;
			}

			// Token: 0x06003F51 RID: 16209 RVA: 0x000F6848 File Offset: 0x000F4A48
			public float GetExtraTroopCount()
			{
				float num = 0f;
				bool isWarmup = this.IsWarmup;
				foreach (IReadOnlyPerkObject readOnlyPerkObject in this._perks)
				{
					if (readOnlyPerkObject != null)
					{
						num += (float)readOnlyPerkObject.GetExtraTroopCount(isWarmup);
					}
				}
				return num;
			}

			// Token: 0x06003F52 RID: 16210 RVA: 0x000F68AC File Offset: 0x000F4AAC
			public IEnumerable<ValueTuple<EquipmentIndex, EquipmentElement>> GetAlternativeEquipments(bool isPlayer)
			{
				List<ValueTuple<EquipmentIndex, EquipmentElement>> list = null;
				bool isWarmup = this.IsWarmup;
				foreach (IReadOnlyPerkObject readOnlyPerkObject in this._perks)
				{
					if (readOnlyPerkObject != null)
					{
						list = readOnlyPerkObject.GetAlternativeEquipments(isWarmup, isPlayer, list, false);
					}
				}
				return list;
			}

			// Token: 0x06003F53 RID: 16211 RVA: 0x000F690C File Offset: 0x000F4B0C
			public float GetDrivenPropertyBonusOnSpawn(bool isPlayer, DrivenProperty drivenProperty, float baseValue)
			{
				float num = 0f;
				bool isWarmup = this.IsWarmup;
				foreach (IReadOnlyPerkObject readOnlyPerkObject in this._perks)
				{
					if (readOnlyPerkObject != null)
					{
						num += readOnlyPerkObject.GetDrivenPropertyBonusOnSpawn(isWarmup, isPlayer, drivenProperty, baseValue);
					}
				}
				return num;
			}

			// Token: 0x06003F54 RID: 16212 RVA: 0x000F6970 File Offset: 0x000F4B70
			public float GetHitpoints(bool isPlayer)
			{
				float num = 0f;
				bool isWarmup = this.IsWarmup;
				foreach (IReadOnlyPerkObject readOnlyPerkObject in this._perks)
				{
					num += readOnlyPerkObject.GetHitpoints(isWarmup, isPlayer);
				}
				return num;
			}

			// Token: 0x04002022 RID: 8226
			private IEnumerable<IReadOnlyPerkObject> _perks;
		}

		// Token: 0x020005FF RID: 1535
		public class MPPerkHandler
		{
			// Token: 0x17000AA4 RID: 2724
			// (get) Token: 0x06003F55 RID: 16213 RVA: 0x000F69D0 File Offset: 0x000F4BD0
			public bool IsWarmup
			{
				get
				{
					Mission mission = Mission.Current;
					bool? flag;
					if (mission == null)
					{
						flag = null;
					}
					else
					{
						MissionMultiplayerGameModeBase missionBehavior = mission.GetMissionBehavior<MissionMultiplayerGameModeBase>();
						if (missionBehavior == null)
						{
							flag = null;
						}
						else
						{
							MultiplayerWarmupComponent warmupComponent = missionBehavior.WarmupComponent;
							flag = ((warmupComponent != null) ? new bool?(warmupComponent.IsInWarmup) : null);
						}
					}
					return flag ?? false;
				}
			}

			// Token: 0x06003F56 RID: 16214 RVA: 0x000F6A38 File Offset: 0x000F4C38
			protected MPPerkHandler(Agent agent)
			{
				this._agent = agent;
				Agent agent2 = this._agent;
				object obj;
				if (agent2 == null)
				{
					obj = null;
				}
				else
				{
					MissionPeer missionPeer = agent2.MissionPeer;
					obj = ((missionPeer != null) ? missionPeer.SelectedPerks : null);
				}
				object obj2;
				if ((obj2 = obj) == null)
				{
					Agent agent3 = this._agent;
					if (agent3 == null)
					{
						obj2 = null;
					}
					else
					{
						MissionPeer owningAgentMissionPeer = agent3.OwningAgentMissionPeer;
						obj2 = ((owningAgentMissionPeer != null) ? owningAgentMissionPeer.SelectedPerks : null);
					}
				}
				this._perks = obj2 ?? new MBList<MPPerkObject>();
			}

			// Token: 0x06003F57 RID: 16215 RVA: 0x000F6AA1 File Offset: 0x000F4CA1
			protected MPPerkHandler(MissionPeer peer)
			{
				this._agent = ((peer != null) ? peer.ControlledAgent : null);
				this._perks = ((peer != null) ? peer.SelectedPerks : null) ?? new MBList<MPPerkObject>();
			}

			// Token: 0x06003F58 RID: 16216 RVA: 0x000F6AD8 File Offset: 0x000F4CD8
			public void OnEvent(MPPerkCondition.PerkEventFlags flags)
			{
				bool isWarmup = this.IsWarmup;
				foreach (MPPerkObject mpperkObject in this._perks)
				{
					mpperkObject.OnEvent(isWarmup, flags);
				}
			}

			// Token: 0x06003F59 RID: 16217 RVA: 0x000F6B34 File Offset: 0x000F4D34
			public void OnEvent(Agent agent, MPPerkCondition.PerkEventFlags flags)
			{
				bool isWarmup = this.IsWarmup;
				foreach (MPPerkObject mpperkObject in this._perks)
				{
					mpperkObject.OnEvent(isWarmup, agent, flags);
				}
			}

			// Token: 0x06003F5A RID: 16218 RVA: 0x000F6B90 File Offset: 0x000F4D90
			public void OnTick(int tickCount)
			{
				bool isWarmup = this.IsWarmup;
				foreach (MPPerkObject mpperkObject in this._perks)
				{
					mpperkObject.OnTick(isWarmup, tickCount);
				}
			}

			// Token: 0x06003F5B RID: 16219 RVA: 0x000F6BEC File Offset: 0x000F4DEC
			public float GetDrivenPropertyBonus(DrivenProperty drivenProperty, float baseValue)
			{
				float num = 0f;
				bool isWarmup = this.IsWarmup;
				foreach (MPPerkObject mpperkObject in this._perks)
				{
					num += mpperkObject.GetDrivenPropertyBonus(isWarmup, this._agent, drivenProperty, baseValue);
				}
				return num;
			}

			// Token: 0x06003F5C RID: 16220 RVA: 0x000F6C58 File Offset: 0x000F4E58
			public float GetRangedAccuracy()
			{
				float num = 0f;
				bool isWarmup = this.IsWarmup;
				foreach (MPPerkObject mpperkObject in this._perks)
				{
					num += mpperkObject.GetRangedAccuracy(isWarmup, this._agent);
				}
				return num;
			}

			// Token: 0x06003F5D RID: 16221 RVA: 0x000F6CC4 File Offset: 0x000F4EC4
			public float GetThrowingWeaponSpeed(WeaponComponentData attackerWeapon)
			{
				float num = 0f;
				bool isWarmup = this.IsWarmup;
				foreach (MPPerkObject mpperkObject in this._perks)
				{
					num += mpperkObject.GetThrowingWeaponSpeed(isWarmup, this._agent, attackerWeapon);
				}
				return num;
			}

			// Token: 0x06003F5E RID: 16222 RVA: 0x000F6D30 File Offset: 0x000F4F30
			public float GetDamageInterruptionThreshold()
			{
				float num = 0f;
				bool isWarmup = this.IsWarmup;
				foreach (MPPerkObject mpperkObject in this._perks)
				{
					num += mpperkObject.GetDamageInterruptionThreshold(isWarmup, this._agent);
				}
				return num;
			}

			// Token: 0x06003F5F RID: 16223 RVA: 0x000F6D9C File Offset: 0x000F4F9C
			public float GetMountManeuver()
			{
				float num = 0f;
				bool isWarmup = this.IsWarmup;
				foreach (MPPerkObject mpperkObject in this._perks)
				{
					num += mpperkObject.GetMountManeuver(isWarmup, this._agent);
				}
				return num;
			}

			// Token: 0x06003F60 RID: 16224 RVA: 0x000F6E08 File Offset: 0x000F5008
			public float GetMountSpeed()
			{
				float num = 0f;
				bool isWarmup = this.IsWarmup;
				foreach (MPPerkObject mpperkObject in this._perks)
				{
					num += mpperkObject.GetMountSpeed(isWarmup, this._agent);
				}
				return num;
			}

			// Token: 0x06003F61 RID: 16225 RVA: 0x000F6E74 File Offset: 0x000F5074
			public int GetGoldOnKill(float attackerValue, float victimValue)
			{
				int num = 0;
				bool isWarmup = this.IsWarmup;
				foreach (MPPerkObject mpperkObject in this._perks)
				{
					num += mpperkObject.GetGoldOnKill(isWarmup, this._agent, attackerValue, victimValue);
				}
				return num;
			}

			// Token: 0x06003F62 RID: 16226 RVA: 0x000F6EDC File Offset: 0x000F50DC
			public int GetGoldOnAssist()
			{
				int num = 0;
				bool isWarmup = this.IsWarmup;
				foreach (MPPerkObject mpperkObject in this._perks)
				{
					num += mpperkObject.GetGoldOnAssist(isWarmup, this._agent);
				}
				return num;
			}

			// Token: 0x06003F63 RID: 16227 RVA: 0x000F6F44 File Offset: 0x000F5144
			public int GetRewardedGoldOnAssist()
			{
				int num = 0;
				bool isWarmup = this.IsWarmup;
				foreach (MPPerkObject mpperkObject in this._perks)
				{
					num += mpperkObject.GetRewardedGoldOnAssist(isWarmup, this._agent);
				}
				return num;
			}

			// Token: 0x06003F64 RID: 16228 RVA: 0x000F6FAC File Offset: 0x000F51AC
			public bool GetIsTeamRewardedOnDeath()
			{
				bool isWarmup = this.IsWarmup;
				using (List<MPPerkObject>.Enumerator enumerator = this._perks.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.GetIsTeamRewardedOnDeath(isWarmup, this._agent))
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x06003F65 RID: 16229 RVA: 0x000F7014 File Offset: 0x000F5214
			public IEnumerable<ValueTuple<MissionPeer, int>> GetTeamGoldRewardsOnDeath()
			{
				if (this.GetIsTeamRewardedOnDeath())
				{
					Agent agent = this._agent;
					MissionPeer missionPeer;
					if ((missionPeer = ((agent != null) ? agent.MissionPeer : null)) == null)
					{
						Agent agent2 = this._agent;
						missionPeer = ((agent2 != null) ? agent2.OwningAgentMissionPeer : null);
					}
					MissionPeer missionPeer2 = missionPeer;
					List<ValueTuple<MissionPeer, int>> list = new List<ValueTuple<MissionPeer, int>>();
					foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
					{
						MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
						if (component != missionPeer2 && component.Team == missionPeer2.Team)
						{
							list.Add(new ValueTuple<MissionPeer, int>(component, 0));
						}
					}
					bool isWarmup = this.IsWarmup;
					foreach (MPPerkObject mpperkObject in this._perks)
					{
						mpperkObject.CalculateRewardedGoldOnDeath(isWarmup, this._agent, list);
					}
					return list;
				}
				return null;
			}

			// Token: 0x06003F66 RID: 16230 RVA: 0x000F7114 File Offset: 0x000F5314
			public float GetEncumbrance(bool isOnBody)
			{
				float num = 0f;
				bool isWarmup = this.IsWarmup;
				foreach (MPPerkObject mpperkObject in this._perks)
				{
					num += mpperkObject.GetEncumbrance(isWarmup, this._agent, isOnBody);
				}
				return num;
			}

			// Token: 0x04002023 RID: 8227
			private readonly Agent _agent;

			// Token: 0x04002024 RID: 8228
			private readonly MBReadOnlyList<MPPerkObject> _perks;
		}

		// Token: 0x02000600 RID: 1536
		public class MPCombatPerkHandler
		{
			// Token: 0x17000AA5 RID: 2725
			// (get) Token: 0x06003F67 RID: 16231 RVA: 0x000F7180 File Offset: 0x000F5380
			public bool IsWarmup
			{
				get
				{
					Mission mission = Mission.Current;
					bool? flag;
					if (mission == null)
					{
						flag = null;
					}
					else
					{
						MissionMultiplayerGameModeBase missionBehavior = mission.GetMissionBehavior<MissionMultiplayerGameModeBase>();
						if (missionBehavior == null)
						{
							flag = null;
						}
						else
						{
							MultiplayerWarmupComponent warmupComponent = missionBehavior.WarmupComponent;
							flag = ((warmupComponent != null) ? new bool?(warmupComponent.IsInWarmup) : null);
						}
					}
					return flag ?? false;
				}
			}

			// Token: 0x06003F68 RID: 16232 RVA: 0x000F71E8 File Offset: 0x000F53E8
			protected MPCombatPerkHandler(Agent attacker, Agent defender)
			{
				this._attacker = attacker;
				this._defender = defender;
				attacker = ((attacker != null && attacker.IsMount) ? attacker.RiderAgent : attacker);
				defender = ((defender != null && defender.IsMount) ? defender.RiderAgent : defender);
				MBList<MPPerkObject> mblist;
				if (attacker == null)
				{
					mblist = null;
				}
				else
				{
					MissionPeer missionPeer = attacker.MissionPeer;
					mblist = ((missionPeer != null) ? missionPeer.SelectedPerks : null);
				}
				MBList<MPPerkObject> mblist2;
				if ((mblist2 = mblist) == null)
				{
					MBList<MPPerkObject> mblist3;
					if (attacker == null)
					{
						mblist3 = null;
					}
					else
					{
						MissionPeer owningAgentMissionPeer = attacker.OwningAgentMissionPeer;
						mblist3 = ((owningAgentMissionPeer != null) ? owningAgentMissionPeer.SelectedPerks : null);
					}
					mblist2 = mblist3 ?? new MBList<MPPerkObject>();
				}
				this._attackerPerks = mblist2;
				MBList<MPPerkObject> mblist4;
				if (defender == null)
				{
					mblist4 = null;
				}
				else
				{
					MissionPeer missionPeer2 = defender.MissionPeer;
					mblist4 = ((missionPeer2 != null) ? missionPeer2.SelectedPerks : null);
				}
				MBList<MPPerkObject> mblist5;
				if ((mblist5 = mblist4) == null)
				{
					MBList<MPPerkObject> mblist6;
					if (defender == null)
					{
						mblist6 = null;
					}
					else
					{
						MissionPeer owningAgentMissionPeer2 = defender.OwningAgentMissionPeer;
						mblist6 = ((owningAgentMissionPeer2 != null) ? owningAgentMissionPeer2.SelectedPerks : null);
					}
					mblist5 = mblist6 ?? new MBList<MPPerkObject>();
				}
				this._defenderPerks = mblist5;
			}

			// Token: 0x06003F69 RID: 16233 RVA: 0x000F72BC File Offset: 0x000F54BC
			public float GetDamage(WeaponComponentData attackerWeapon, DamageTypes damageType, bool isAlternativeAttack)
			{
				float num = 0f;
				if (this._attackerPerks.Count > 0 && this._defender != null)
				{
					bool isWarmup = this.IsWarmup;
					if (this._defender.IsMount)
					{
						foreach (MPPerkObject mpperkObject in this._attackerPerks)
						{
							num += mpperkObject.GetMountDamage(isWarmup, this._attacker, attackerWeapon, damageType, isAlternativeAttack);
						}
					}
					foreach (MPPerkObject mpperkObject2 in this._attackerPerks)
					{
						num += mpperkObject2.GetDamage(isWarmup, this._attacker, attackerWeapon, damageType, isAlternativeAttack);
					}
				}
				return num;
			}

			// Token: 0x06003F6A RID: 16234 RVA: 0x000F73A4 File Offset: 0x000F55A4
			public float GetDamageTaken(WeaponComponentData attackerWeapon, DamageTypes damageType)
			{
				float num = 0f;
				if (this._defenderPerks.Count > 0)
				{
					bool isWarmup = this.IsWarmup;
					if (this._defender.IsMount)
					{
						using (List<MPPerkObject>.Enumerator enumerator = this._defenderPerks.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								MPPerkObject mpperkObject = enumerator.Current;
								num += mpperkObject.GetMountDamageTaken(isWarmup, this._defender, attackerWeapon, damageType);
							}
							return num;
						}
					}
					foreach (MPPerkObject mpperkObject2 in this._defenderPerks)
					{
						num += mpperkObject2.GetDamageTaken(isWarmup, this._defender, attackerWeapon, damageType);
					}
				}
				return num;
			}

			// Token: 0x06003F6B RID: 16235 RVA: 0x000F7480 File Offset: 0x000F5680
			public float GetSpeedBonusEffectiveness(WeaponComponentData attackerWeapon, DamageTypes damageType)
			{
				float num = 0f;
				bool isWarmup = this.IsWarmup;
				foreach (MPPerkObject mpperkObject in this._attackerPerks)
				{
					num += mpperkObject.GetSpeedBonusEffectiveness(isWarmup, this._attacker, attackerWeapon, damageType);
				}
				return num;
			}

			// Token: 0x06003F6C RID: 16236 RVA: 0x000F74EC File Offset: 0x000F56EC
			public float GetShieldDamage(bool isCorrectSideBlock)
			{
				float num = 0f;
				if (this._defender != null)
				{
					bool isWarmup = this.IsWarmup;
					foreach (MPPerkObject mpperkObject in this._attackerPerks)
					{
						num += mpperkObject.GetShieldDamage(isWarmup, this._attacker, this._defender, isCorrectSideBlock);
					}
				}
				return num;
			}

			// Token: 0x06003F6D RID: 16237 RVA: 0x000F7568 File Offset: 0x000F5768
			public float GetShieldDamageTaken(bool isCorrectSideBlock)
			{
				float num = 0f;
				bool isWarmup = this.IsWarmup;
				foreach (MPPerkObject mpperkObject in this._defenderPerks)
				{
					num += mpperkObject.GetShieldDamageTaken(isWarmup, this._attacker, this._defender, isCorrectSideBlock);
				}
				return num;
			}

			// Token: 0x06003F6E RID: 16238 RVA: 0x000F75DC File Offset: 0x000F57DC
			public float GetRangedHeadShotDamage()
			{
				float num = 0f;
				if (this._attacker != null)
				{
					bool isWarmup = this.IsWarmup;
					foreach (MPPerkObject mpperkObject in this._attackerPerks)
					{
						num += mpperkObject.GetRangedHeadShotDamage(isWarmup, this._attacker);
					}
				}
				return num;
			}

			// Token: 0x04002025 RID: 8229
			private readonly Agent _attacker;

			// Token: 0x04002026 RID: 8230
			private readonly Agent _defender;

			// Token: 0x04002027 RID: 8231
			private readonly MBReadOnlyList<MPPerkObject> _attackerPerks;

			// Token: 0x04002028 RID: 8232
			private readonly MBReadOnlyList<MPPerkObject> _defenderPerks;
		}
	}
}
