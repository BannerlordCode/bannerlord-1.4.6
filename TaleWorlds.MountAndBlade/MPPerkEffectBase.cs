using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000313 RID: 787
	public abstract class MPPerkEffectBase
	{
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x000AC814 File Offset: 0x000AAA14
		public virtual bool IsTickRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x000AC817 File Offset: 0x000AAA17
		// (set) Token: 0x06002CC6 RID: 11462 RVA: 0x000AC81F File Offset: 0x000AAA1F
		public bool IsDisabledInWarmup { get; protected set; }

		// Token: 0x06002CC7 RID: 11463 RVA: 0x000AC828 File Offset: 0x000AAA28
		public virtual void OnUpdate(Agent agent, bool newState)
		{
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x000AC82C File Offset: 0x000AAA2C
		public virtual void OnTick(MissionPeer peer, int tickCount)
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
				using (List<IFormationUnit>.Enumerator enumerator = mbreadOnlyList2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Agent agent;
						if ((agent = enumerator.Current as Agent) != null && agent.IsActive())
						{
							this.OnTick(agent, tickCount);
						}
					}
					return;
				}
			}
			if (peer != null)
			{
				Agent controlledAgent = peer.ControlledAgent;
				bool? flag = ((controlledAgent != null) ? new bool?(controlledAgent.IsActive()) : null);
				bool flag2 = true;
				if ((flag.GetValueOrDefault() == flag2) & (flag != null))
				{
					this.OnTick(peer.ControlledAgent, tickCount);
				}
			}
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x000AC904 File Offset: 0x000AAB04
		public virtual void OnTick(Agent agent, int tickCount)
		{
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x000AC906 File Offset: 0x000AAB06
		public virtual float GetDamage(WeaponComponentData attackerWeapon, DamageTypes damageType, bool isAlternativeAttack)
		{
			return 0f;
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x000AC90D File Offset: 0x000AAB0D
		public virtual float GetMountDamage(WeaponComponentData attackerWeapon, DamageTypes damageType, bool isAlternativeAttack)
		{
			return 0f;
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000AC914 File Offset: 0x000AAB14
		public virtual float GetDamageTaken(WeaponComponentData attackerWeapon, DamageTypes damageType)
		{
			return 0f;
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000AC91B File Offset: 0x000AAB1B
		public virtual float GetMountDamageTaken(WeaponComponentData attackerWeapon, DamageTypes damageType)
		{
			return 0f;
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x000AC922 File Offset: 0x000AAB22
		public virtual float GetSpeedBonusEffectiveness(Agent attacker, WeaponComponentData attackerWeapon, DamageTypes damageType)
		{
			return 0f;
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x000AC929 File Offset: 0x000AAB29
		public virtual float GetShieldDamage(bool isCorrectSideBlock)
		{
			return 0f;
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x000AC930 File Offset: 0x000AAB30
		public virtual float GetShieldDamageTaken(bool isCorrectSideBlock)
		{
			return 0f;
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000AC937 File Offset: 0x000AAB37
		public virtual float GetRangedAccuracy()
		{
			return 0f;
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000AC93E File Offset: 0x000AAB3E
		public virtual float GetThrowingWeaponSpeed(WeaponComponentData attackerWeapon)
		{
			return 0f;
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000AC945 File Offset: 0x000AAB45
		public virtual float GetDamageInterruptionThreshold()
		{
			return 0f;
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000AC94C File Offset: 0x000AAB4C
		public virtual float GetMountManeuver()
		{
			return 0f;
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000AC953 File Offset: 0x000AAB53
		public virtual float GetMountSpeed()
		{
			return 0f;
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000AC95A File Offset: 0x000AAB5A
		public virtual float GetRangedHeadShotDamage()
		{
			return 0f;
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000AC961 File Offset: 0x000AAB61
		public virtual int GetGoldOnKill(float attackerValue, float victimValue)
		{
			return 0;
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000AC964 File Offset: 0x000AAB64
		public virtual int GetGoldOnAssist()
		{
			return 0;
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000AC967 File Offset: 0x000AAB67
		public virtual int GetRewardedGoldOnAssist()
		{
			return 0;
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000AC96A File Offset: 0x000AAB6A
		public virtual bool GetIsTeamRewardedOnDeath()
		{
			return false;
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000AC96D File Offset: 0x000AAB6D
		public virtual void CalculateRewardedGoldOnDeath(Agent agent, List<ValueTuple<MissionPeer, int>> teamMembers)
		{
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000AC96F File Offset: 0x000AAB6F
		public virtual float GetDrivenPropertyBonus(DrivenProperty drivenProperty, float baseValue)
		{
			return 0f;
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000AC976 File Offset: 0x000AAB76
		public virtual float GetEncumbrance(bool isOnBody)
		{
			return 0f;
		}

		// Token: 0x06002CDE RID: 11486
		protected abstract void Deserialize(XmlNode node);
	}
}
