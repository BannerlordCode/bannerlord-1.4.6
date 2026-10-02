using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000200 RID: 512
	public class MultiplayerAgentApplyDamageModel : AgentApplyDamageModel
	{
		// Token: 0x06001DBD RID: 7613 RVA: 0x000662D6 File Offset: 0x000644D6
		public override bool IsDamageIgnored(in AttackInformation attackInformation, in AttackCollisionData collisionData)
		{
			return false;
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x000662D9 File Offset: 0x000644D9
		public override float ApplyDamageAmplifications(in AttackInformation attackInformation, in AttackCollisionData collisionData, float baseDamage)
		{
			return baseDamage;
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x000662DC File Offset: 0x000644DC
		public override float ApplyDamageScaling(in AttackInformation attackInformation, in AttackCollisionData collisionData, float baseDamage)
		{
			return baseDamage;
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x000662DF File Offset: 0x000644DF
		public override float ApplyDamageReductions(in AttackInformation attackInformation, in AttackCollisionData collisionData, float baseDamage)
		{
			return baseDamage;
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x000662E4 File Offset: 0x000644E4
		public override float ApplyGeneralDamageModifiers(in AttackInformation attackInformation, in AttackCollisionData collisionData, float baseDamage)
		{
			float num = baseDamage;
			Agent attackerAgent = attackInformation.AttackerAgent;
			Agent victimAgent = attackInformation.VictimAgent;
			MPPerkObject.MPCombatPerkHandler combatPerkHandler = MPPerkObject.GetCombatPerkHandler(attackerAgent, victimAgent);
			if (combatPerkHandler != null)
			{
				AttackCollisionData attackCollisionData = collisionData;
				if (attackCollisionData.AttackBlockedWithShield)
				{
					float num2 = 1f;
					MPPerkObject.MPCombatPerkHandler mpcombatPerkHandler = combatPerkHandler;
					attackCollisionData = collisionData;
					float num3 = num2 + mpcombatPerkHandler.GetShieldDamage(attackCollisionData.CorrectSideShieldBlock);
					MPPerkObject.MPCombatPerkHandler mpcombatPerkHandler2 = combatPerkHandler;
					attackCollisionData = collisionData;
					float num4 = num3 + mpcombatPerkHandler2.GetShieldDamageTaken(attackCollisionData.CorrectSideShieldBlock);
					num = MathF.Max(0f, num * num4);
				}
				bool flag = MissionCombatMechanicsHelper.IsCollisionBoneDifferentThanWeaponAttachBone(in collisionData, attackInformation.WeaponAttachBoneIndex);
				MissionWeapon attackerWeapon = attackInformation.AttackerWeapon;
				bool flag2;
				if (!attackerWeapon.IsEmpty && !flag)
				{
					attackCollisionData = collisionData;
					if (!attackCollisionData.IsAlternativeAttack)
					{
						attackCollisionData = collisionData;
						if (!attackCollisionData.IsFallDamage)
						{
							attackCollisionData = collisionData;
							flag2 = attackCollisionData.IsHorseCharge;
							goto IL_00C3;
						}
					}
				}
				flag2 = true;
				IL_00C3:
				DamageTypes damageTypes;
				if (!flag2)
				{
					attackCollisionData = collisionData;
					damageTypes = (DamageTypes)attackCollisionData.DamageType;
				}
				else
				{
					damageTypes = DamageTypes.Blunt;
				}
				DamageTypes damageTypes2 = damageTypes;
				float num5 = 0f;
				float num6 = 1f;
				MPPerkObject.MPCombatPerkHandler mpcombatPerkHandler3 = combatPerkHandler;
				WeaponComponentData currentUsageItem = attackerWeapon.CurrentUsageItem;
				DamageTypes damageTypes3 = damageTypes2;
				attackCollisionData = collisionData;
				float num7 = MathF.Max(num5, num6 + mpcombatPerkHandler3.GetDamage(currentUsageItem, damageTypes3, attackCollisionData.IsAlternativeAttack) + combatPerkHandler.GetDamageTaken(attackerWeapon.CurrentUsageItem, damageTypes2));
				if (attackInformation.IsHeadShot && attackerWeapon.CurrentUsageItem != null && (attackerWeapon.CurrentUsageItem.IsConsumable || attackerWeapon.CurrentUsageItem.IsRangedWeapon))
				{
					num7 += combatPerkHandler.GetRangedHeadShotDamage();
				}
				num *= num7;
			}
			return num;
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x00066448 File Offset: 0x00064648
		public override void DecideMissileWeaponFlags(Agent attackerAgent, in MissionWeapon missileWeapon, ref WeaponFlags missileWeaponFlags)
		{
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x0006644C File Offset: 0x0006464C
		public override bool DecideCrushedThrough(Agent attackerAgent, Agent defenderAgent, float totalAttackEnergy, Agent.UsageDirection attackDirection, StrikeType strikeType, WeaponComponentData defendItem, bool isPassiveUsage)
		{
			EquipmentIndex equipmentIndex = attackerAgent.GetOffhandWieldedItemIndex();
			if (equipmentIndex == EquipmentIndex.None)
			{
				equipmentIndex = attackerAgent.GetPrimaryWieldedItemIndex();
			}
			WeaponComponentData weaponComponentData = ((equipmentIndex != EquipmentIndex.None) ? attackerAgent.Equipment[equipmentIndex].CurrentUsageItem : null);
			if (weaponComponentData == null || isPassiveUsage || !weaponComponentData.WeaponFlags.HasAnyFlag(WeaponFlags.CanCrushThrough) || strikeType != StrikeType.Swing || attackDirection != Agent.UsageDirection.AttackUp)
			{
				return false;
			}
			float num = 58f;
			if (defendItem != null && defendItem.IsShield)
			{
				num *= 1.2f;
			}
			return totalAttackEnergy > num;
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x000664CC File Offset: 0x000646CC
		public override bool CanWeaponDealSneakAttack(in AttackInformation attackInformation, WeaponComponentData weapon)
		{
			return false;
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000664D0 File Offset: 0x000646D0
		public override bool CanWeaponDismount(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData)
		{
			return MBMath.IsBetween((int)blow.VictimBodyPart, 0, 6) && ((!attackerAgent.HasMount && blow.StrikeType == StrikeType.Swing && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.CanHook)) || (blow.StrikeType == StrikeType.Thrust && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.CanDismount)));
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x00066539 File Offset: 0x00064739
		public override void CalculateDefendedBlowStunMultipliers(Agent attackerAgent, Agent defenderAgent, CombatCollisionResult collisionResult, WeaponComponentData attackerWeapon, WeaponComponentData defenderWeapon, ref float attackerStunPeriod, ref float defenderStunPeriod)
		{
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x0006653C File Offset: 0x0006473C
		public override bool CanWeaponKnockback(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData)
		{
			AttackCollisionData attackCollisionData = collisionData;
			return MBMath.IsBetween((int)attackCollisionData.VictimHitBodyPart, 0, 6) && !attackerWeapon.WeaponFlags.HasAnyFlag(WeaponFlags.CanKnockDown) && (attackerWeapon.IsConsumable || (blow.BlowFlag & BlowFlags.CrushThrough) != BlowFlags.None || (blow.StrikeType == StrikeType.Thrust && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.WideGrip)));
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x000665AC File Offset: 0x000647AC
		public override bool CanWeaponKnockDown(Agent attackerAgent, Agent victimAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData)
		{
			if (attackerWeapon.WeaponClass == WeaponClass.Boulder || attackerWeapon.WeaponClass == WeaponClass.BallistaBoulder)
			{
				return true;
			}
			AttackCollisionData attackCollisionData = collisionData;
			BoneBodyPartType victimHitBodyPart = attackCollisionData.VictimHitBodyPart;
			bool flag = MBMath.IsBetween((int)victimHitBodyPart, 0, 6);
			if (!victimAgent.HasMount && victimHitBodyPart == BoneBodyPartType.Legs)
			{
				flag = true;
			}
			return flag && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.CanKnockDown) && ((attackerWeapon.IsPolearm && blow.StrikeType == StrikeType.Thrust) || (attackerWeapon.IsMeleeWeapon && blow.StrikeType == StrikeType.Swing && MissionCombatMechanicsHelper.DecideSweetSpotCollision(in collisionData)));
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00066642 File Offset: 0x00064842
		public override float GetDismountPenetration(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			return 0f;
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00066649 File Offset: 0x00064849
		public override float GetKnockBackPenetration(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			return 0f;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00066650 File Offset: 0x00064850
		public override float GetKnockDownPenetration(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			float num = 0f;
			if (attackerWeapon.WeaponClass == WeaponClass.Boulder || attackerWeapon.WeaponClass == WeaponClass.BallistaBoulder)
			{
				num += 0.25f;
			}
			else if (attackerWeapon.IsMeleeWeapon)
			{
				AttackCollisionData attackCollisionData2 = attackCollisionData;
				if (attackCollisionData2.VictimHitBodyPart == BoneBodyPartType.Legs && blow.StrikeType == StrikeType.Swing)
				{
					num += 0.1f;
				}
				else
				{
					attackCollisionData2 = attackCollisionData;
					if (attackCollisionData2.VictimHitBodyPart == BoneBodyPartType.Head)
					{
						num += 0.15f;
					}
				}
			}
			return num;
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x000666C7 File Offset: 0x000648C7
		public override float GetHorseChargePenetration()
		{
			return 0.4f;
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x000666D0 File Offset: 0x000648D0
		public override float CalculateStaggerThresholdDamage(Agent defenderAgent, in Blow blow)
		{
			MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(defenderAgent);
			float? num = ((perkHandler != null) ? new float?(perkHandler.GetDamageInterruptionThreshold()) : null);
			if (num != null && num.Value > 0f)
			{
				return num.Value;
			}
			ManagedParametersEnum managedParametersEnum;
			if (blow.DamageType == DamageTypes.Cut)
			{
				managedParametersEnum = ManagedParametersEnum.DamageInterruptAttackThresholdCut;
			}
			else if (blow.DamageType == DamageTypes.Pierce)
			{
				managedParametersEnum = ManagedParametersEnum.DamageInterruptAttackThresholdPierce;
			}
			else
			{
				managedParametersEnum = ManagedParametersEnum.DamageInterruptAttackThresholdBlunt;
			}
			return ManagedParameters.Instance.GetManagedParameter(managedParametersEnum);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00066745 File Offset: 0x00064945
		public override float CalculateAlternativeAttackDamage(in AttackInformation attackInformation, in AttackCollisionData collisionData, WeaponComponentData weapon)
		{
			if (weapon == null)
			{
				return 2f;
			}
			if (weapon.WeaponClass == WeaponClass.LargeShield)
			{
				return 2f;
			}
			if (weapon.WeaponClass == WeaponClass.SmallShield)
			{
				return 1f;
			}
			if (weapon.IsTwoHanded)
			{
				return 2f;
			}
			return 1f;
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00066783 File Offset: 0x00064983
		public override float CalculatePassiveAttackDamage(BasicCharacterObject attackerCharacter, in AttackCollisionData collisionData, float baseDamage)
		{
			return baseDamage;
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00066786 File Offset: 0x00064986
		public override MeleeCollisionReaction DecidePassiveAttackCollisionReaction(Agent attacker, Agent defender, bool isFatalHit)
		{
			return MeleeCollisionReaction.Bounced;
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0006678C File Offset: 0x0006498C
		public override float CalculateShieldDamage(in AttackInformation attackInformation, float baseDamage)
		{
			baseDamage *= 1.25f;
			MissionMultiplayerFlagDomination missionBehavior = Mission.Current.GetMissionBehavior<MissionMultiplayerFlagDomination>();
			if (missionBehavior != null && missionBehavior.GetMissionType() == MultiplayerGameType.Captain)
			{
				return baseDamage * 0.75f;
			}
			return baseDamage;
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x000667C2 File Offset: 0x000649C2
		public override float CalculateSailFireDamage(Agent attackerAgent, IShipOrigin shipOrigin, float baseDamage, bool damageFromShipMachine)
		{
			return 0f;
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x000667C9 File Offset: 0x000649C9
		public override float CalculateHullFireDamage(float baseFireDamage, IShipOrigin shipOrigin)
		{
			return 0f;
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x000667D0 File Offset: 0x000649D0
		public override float GetDamageMultiplierForBodyPart(BoneBodyPartType bodyPart, DamageTypes type, bool isHuman, bool isMissile)
		{
			float num = 1f;
			switch (bodyPart)
			{
			case BoneBodyPartType.None:
				num = 1f;
				break;
			case BoneBodyPartType.Head:
				switch (type)
				{
				case DamageTypes.Invalid:
					num = 1.5f;
					break;
				case DamageTypes.Cut:
					num = 1.2f;
					break;
				case DamageTypes.Pierce:
					if (isHuman)
					{
						num = (isMissile ? 2f : 1.25f);
					}
					else
					{
						num = 1.2f;
					}
					break;
				case DamageTypes.Blunt:
					num = 1.2f;
					break;
				}
				break;
			case BoneBodyPartType.Neck:
				switch (type)
				{
				case DamageTypes.Invalid:
					num = 1.5f;
					break;
				case DamageTypes.Cut:
					num = 1.2f;
					break;
				case DamageTypes.Pierce:
					if (isHuman)
					{
						num = (isMissile ? 2f : 1.25f);
					}
					else
					{
						num = 1.2f;
					}
					break;
				case DamageTypes.Blunt:
					num = 1.2f;
					break;
				}
				break;
			case BoneBodyPartType.Chest:
			case BoneBodyPartType.Abdomen:
			case BoneBodyPartType.ShoulderLeft:
			case BoneBodyPartType.ShoulderRight:
			case BoneBodyPartType.ArmLeft:
			case BoneBodyPartType.ArmRight:
				if (isHuman)
				{
					num = 1f;
				}
				else
				{
					num = 0.8f;
				}
				break;
			case BoneBodyPartType.Legs:
				num = 0.8f;
				break;
			}
			return num;
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x000668EA File Offset: 0x00064AEA
		public override bool CanWeaponIgnoreFriendlyFireChecks(WeaponComponentData weapon)
		{
			return weapon != null && weapon.IsConsumable && weapon.WeaponFlags.HasAnyFlag(WeaponFlags.CanPenetrateShield) && weapon.WeaponFlags.HasAnyFlag(WeaponFlags.MultiplePenetration);
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00066920 File Offset: 0x00064B20
		public override bool DecideAgentShrugOffBlow(Agent victimAgent, in AttackCollisionData collisionData, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentShrugOffBlow(victimAgent, in collisionData, in blow);
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x0006692A File Offset: 0x00064B2A
		public override bool DecideAgentDismountedByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentDismountedByBlow(attackerAgent, victimAgent, in collisionData, attackerWeapon, in blow);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x00066938 File Offset: 0x00064B38
		public override bool DecideAgentKnockedBackByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentKnockedBackByBlow(attackerAgent, victimAgent, in collisionData, attackerWeapon, in blow);
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x00066946 File Offset: 0x00064B46
		public override bool DecideAgentKnockedDownByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentKnockedDownByBlow(attackerAgent, victimAgent, in collisionData, attackerWeapon, in blow);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x00066954 File Offset: 0x00064B54
		public override bool DecideMountRearedByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideMountRearedByBlow(attackerAgent, victimAgent, in collisionData, attackerWeapon, in blow);
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00066964 File Offset: 0x00064B64
		public override void DecideWeaponCollisionReaction(in Blow registeredBlow, in AttackCollisionData collisionData, Agent attacker, Agent defender, in MissionWeapon attackerWeapon, bool isFatalHit, bool isShruggedOff, float momentumRemaining, out MeleeCollisionReaction colReaction)
		{
			MissionCombatMechanicsHelper.DecideWeaponCollisionReaction(in registeredBlow, in collisionData, attacker, defender, in attackerWeapon, isFatalHit, isShruggedOff, momentumRemaining, out colReaction);
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x00066985 File Offset: 0x00064B85
		public override bool ShouldMissilePassThroughAfterShieldBreak(Agent attackerAgent, WeaponComponentData attackerWeapon)
		{
			return false;
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x00066988 File Offset: 0x00064B88
		public override float CalculateRemainingMomentum(float originalMomentum, in Blow b, in AttackCollisionData collisionData, Agent attacker, Agent victim, in MissionWeapon attackerWeapon, bool isCrushThrough)
		{
			return base.CalculateDefaultRemainingMomentum(originalMomentum, in b, in collisionData, attacker, victim, in attackerWeapon, isCrushThrough);
		}
	}
}
