using System;
using MBHelpers;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001F2 RID: 498
	public class CustomAgentApplyDamageModel : AgentApplyDamageModel
	{
		// Token: 0x06001D53 RID: 7507 RVA: 0x00063944 File Offset: 0x00061B44
		public override bool IsDamageIgnored(in AttackInformation attackInformation, in AttackCollisionData collisionData)
		{
			return false;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x00063948 File Offset: 0x00061B48
		public override float ApplyDamageAmplifications(in AttackInformation attackInformation, in AttackCollisionData collisionData, float baseDamage)
		{
			bool flag = (attackInformation.IsAttackerAgentMount ? attackInformation.AttackerRiderAgentCharacter : attackInformation.AttackerAgentCharacter) != null;
			Formation attackerFormation = attackInformation.AttackerFormation;
			BannerComponent activeBanner = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(attackerFormation);
			bool isVictimAgentMount = attackInformation.IsVictimAgentMount;
			Formation victimFormation = attackInformation.VictimFormation;
			BannerComponent activeBanner2 = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(victimFormation);
			FactoredNumber factoredNumber = new FactoredNumber(baseDamage);
			MissionWeapon attackerWeapon = attackInformation.AttackerWeapon;
			WeaponComponentData currentUsageItem = attackerWeapon.CurrentUsageItem;
			if (flag)
			{
				if (currentUsageItem != null)
				{
					if (currentUsageItem.IsMeleeWeapon)
					{
						if (activeBanner != null)
						{
							BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedMeleeDamage, activeBanner, ref factoredNumber);
							if (attackInformation.DoesVictimHaveMountAgent)
							{
								BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedMeleeDamageAgainstMountedTroops, activeBanner, ref factoredNumber);
							}
						}
					}
					else if (currentUsageItem.IsConsumable && activeBanner != null)
					{
						BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedRangedDamage, activeBanner, ref factoredNumber);
					}
				}
				AttackCollisionData attackCollisionData = collisionData;
				if (attackCollisionData.IsHorseCharge)
				{
					if (activeBanner != null)
					{
						BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedChargeDamage, activeBanner, ref factoredNumber);
					}
					if (activeBanner2 != null)
					{
						BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.DecreasedChargeDamage, activeBanner2, ref factoredNumber);
					}
				}
			}
			return factoredNumber.ResultNumber;
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x00063A44 File Offset: 0x00061C44
		public override float ApplyDamageScaling(in AttackInformation attackInformation, in AttackCollisionData collisionData, float baseDamage)
		{
			float num = 1f;
			if (Mission.Current.IsSallyOutBattle)
			{
				DestructableComponent hitObjectDestructibleComponent = attackInformation.HitObjectDestructibleComponent;
				if (hitObjectDestructibleComponent != null && hitObjectDestructibleComponent.GameEntity.GetFirstScriptOfType<SiegeWeapon>() != null)
				{
					num *= 4.5f;
				}
			}
			return baseDamage * num;
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x00063A8C File Offset: 0x00061C8C
		public override float ApplyDamageReductions(in AttackInformation attackInformation, in AttackCollisionData collisionData, float baseDamage)
		{
			bool flag = (attackInformation.IsVictimAgentMount ? attackInformation.VictimRiderAgentCharacter : attackInformation.VictimAgentCharacter) != null;
			Formation victimFormation = attackInformation.VictimFormation;
			BannerComponent activeBanner = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(victimFormation);
			Agent agent = (attackInformation.IsAttackerAgentMount ? attackInformation.AttackerAgent.RiderAgent : attackInformation.AttackerAgent);
			FactoredNumber factoredNumber = new FactoredNumber(baseDamage);
			MissionWeapon attackerWeapon = attackInformation.AttackerWeapon;
			WeaponComponentData currentUsageItem = attackerWeapon.CurrentUsageItem;
			if (flag && currentUsageItem != null)
			{
				if (currentUsageItem.IsConsumable)
				{
					if (activeBanner != null)
					{
						BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.DecreasedRangedAttackDamage, activeBanner, ref factoredNumber);
					}
					if (Mission.Current.IsNavalBattle && agent != null && agent.IsAIControlled && (currentUsageItem.WeaponClass == WeaponClass.Bolt || currentUsageItem.WeaponClass == WeaponClass.Arrow))
					{
						factoredNumber.AddFactor(-0.15f);
					}
				}
				else if (currentUsageItem.IsMeleeWeapon && activeBanner != null)
				{
					BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.DecreasedMeleeAttackDamage, activeBanner, ref factoredNumber);
				}
			}
			return factoredNumber.ResultNumber;
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x00063B76 File Offset: 0x00061D76
		public override float ApplyGeneralDamageModifiers(in AttackInformation attackInformation, in AttackCollisionData collisionData, float baseDamage)
		{
			return baseDamage;
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x00063B79 File Offset: 0x00061D79
		public override void DecideMissileWeaponFlags(Agent attackerAgent, in MissionWeapon missileWeapon, ref WeaponFlags missileWeaponFlags)
		{
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x00063B7C File Offset: 0x00061D7C
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

		// Token: 0x06001D5A RID: 7514 RVA: 0x00063BFC File Offset: 0x00061DFC
		public override bool CanWeaponDealSneakAttack(in AttackInformation attackInformation, WeaponComponentData weapon)
		{
			if (weapon != null && (weapon.IsMeleeWeapon || weapon.WeaponClass == WeaponClass.ThrowingKnife) && attackInformation.IsVictimAgentHuman && !attackInformation.IsVictimPlayer)
			{
				if ((attackInformation.VictimAgentAIStateFlags & Agent.AIStateFlag.Alarmed) == Agent.AIStateFlag.None)
				{
					return true;
				}
				if (!attackInformation.VictimAgentAIStateFlags.HasAllFlags(Agent.AIStateFlag.Alarmed) && !attackInformation.IsAttackerAgentNull && Vec2.DotProduct((attackInformation.AttackerAgentPosition - attackInformation.VictimAgentPosition).AsVec2.Normalized(), attackInformation.VictimAgentMovementDirection) < 0.174f)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x00063C88 File Offset: 0x00061E88
		public override bool CanWeaponDismount(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData)
		{
			return MBMath.IsBetween((int)blow.VictimBodyPart, 0, 6) && ((!attackerAgent.HasMount && blow.StrikeType == StrikeType.Swing && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.CanHook)) || (blow.StrikeType == StrikeType.Thrust && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.CanDismount)));
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x00063CF1 File Offset: 0x00061EF1
		public override void CalculateDefendedBlowStunMultipliers(Agent attackerAgent, Agent defenderAgent, CombatCollisionResult collisionResult, WeaponComponentData attackerWeapon, WeaponComponentData defenderWeapon, ref float attackerStunPeriod, ref float defenderStunPeriod)
		{
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x00063CF4 File Offset: 0x00061EF4
		public override bool CanWeaponKnockback(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData)
		{
			AttackCollisionData attackCollisionData = collisionData;
			return MBMath.IsBetween((int)attackCollisionData.VictimHitBodyPart, 0, 6) && !attackerWeapon.WeaponFlags.HasAnyFlag(WeaponFlags.CanKnockDown) && (attackerWeapon.IsConsumable || (blow.BlowFlag & BlowFlags.CrushThrough) != BlowFlags.None || (blow.StrikeType == StrikeType.Thrust && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.WideGrip)));
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x00063D64 File Offset: 0x00061F64
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

		// Token: 0x06001D5F RID: 7519 RVA: 0x00063DFC File Offset: 0x00061FFC
		public override float GetDismountPenetration(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			float num = 0f;
			if (blow.StrikeType == StrikeType.Swing && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.CanHook))
			{
				num += 0.25f;
			}
			return num;
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x00063E38 File Offset: 0x00062038
		public override float GetKnockBackPenetration(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			return 0f;
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x00063E40 File Offset: 0x00062040
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

		// Token: 0x06001D62 RID: 7522 RVA: 0x00063EB7 File Offset: 0x000620B7
		public override float GetHorseChargePenetration()
		{
			return 0.4f;
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x00063EC0 File Offset: 0x000620C0
		public override float CalculateStaggerThresholdDamage(Agent defenderAgent, in Blow blow)
		{
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

		// Token: 0x06001D64 RID: 7524 RVA: 0x00063EF6 File Offset: 0x000620F6
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

		// Token: 0x06001D65 RID: 7525 RVA: 0x00063F34 File Offset: 0x00062134
		public override float CalculatePassiveAttackDamage(BasicCharacterObject attackerCharacter, in AttackCollisionData collisionData, float baseDamage)
		{
			return baseDamage;
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x00063F37 File Offset: 0x00062137
		public override MeleeCollisionReaction DecidePassiveAttackCollisionReaction(Agent attacker, Agent defender, bool isFatalHit)
		{
			return MeleeCollisionReaction.Bounced;
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x00063F3C File Offset: 0x0006213C
		public override float CalculateShieldDamage(in AttackInformation attackInformation, float baseDamage)
		{
			baseDamage *= 1.25f;
			FactoredNumber factoredNumber = new FactoredNumber(baseDamage);
			Formation victimFormation = attackInformation.VictimFormation;
			BannerComponent activeBanner = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(victimFormation);
			if (activeBanner != null)
			{
				BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.DecreasedShieldDamage, activeBanner, ref factoredNumber);
			}
			return Math.Max(0f, factoredNumber.ResultNumber);
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x00063F93 File Offset: 0x00062193
		public override float CalculateSailFireDamage(Agent attackerAgent, IShipOrigin shipOrigin, float baseDamage, bool damageFromShipMachine)
		{
			return baseDamage;
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x00063F96 File Offset: 0x00062196
		public override float CalculateHullFireDamage(float baseFireDamage, IShipOrigin shipOrigin)
		{
			return baseFireDamage;
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x00063F9C File Offset: 0x0006219C
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

		// Token: 0x06001D6B RID: 7531 RVA: 0x000640B6 File Offset: 0x000622B6
		public override bool CanWeaponIgnoreFriendlyFireChecks(WeaponComponentData weapon)
		{
			return weapon != null && weapon.IsConsumable && weapon.WeaponFlags.HasAnyFlag(WeaponFlags.CanPenetrateShield) && weapon.WeaponFlags.HasAnyFlag(WeaponFlags.MultiplePenetration);
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x000640EC File Offset: 0x000622EC
		public override bool DecideAgentShrugOffBlow(Agent victimAgent, in AttackCollisionData collisionData, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentShrugOffBlow(victimAgent, in collisionData, in blow);
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x000640F6 File Offset: 0x000622F6
		public override bool DecideAgentDismountedByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentDismountedByBlow(attackerAgent, victimAgent, in collisionData, attackerWeapon, in blow);
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00064104 File Offset: 0x00062304
		public override bool DecideAgentKnockedBackByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentKnockedBackByBlow(attackerAgent, victimAgent, in collisionData, attackerWeapon, in blow);
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x00064112 File Offset: 0x00062312
		public override bool DecideAgentKnockedDownByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentKnockedDownByBlow(attackerAgent, victimAgent, in collisionData, attackerWeapon, in blow);
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x00064120 File Offset: 0x00062320
		public override bool DecideMountRearedByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideMountRearedByBlow(attackerAgent, victimAgent, in collisionData, attackerWeapon, in blow);
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x00064130 File Offset: 0x00062330
		public override void DecideWeaponCollisionReaction(in Blow registeredBlow, in AttackCollisionData collisionData, Agent attacker, Agent defender, in MissionWeapon attackerWeapon, bool isFatalHit, bool isShruggedOff, float momentumRemaining, out MeleeCollisionReaction colReaction)
		{
			MissionCombatMechanicsHelper.DecideWeaponCollisionReaction(in registeredBlow, in collisionData, attacker, defender, in attackerWeapon, isFatalHit, isShruggedOff, momentumRemaining, out colReaction);
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x00064151 File Offset: 0x00062351
		public override bool ShouldMissilePassThroughAfterShieldBreak(Agent attackerAgent, WeaponComponentData attackerWeapon)
		{
			return false;
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x00064154 File Offset: 0x00062354
		public override float CalculateRemainingMomentum(float originalMomentum, in Blow b, in AttackCollisionData collisionData, Agent attacker, Agent victim, in MissionWeapon attackerWeapon, bool isCrushThrough)
		{
			return base.CalculateDefaultRemainingMomentum(originalMomentum, in b, in collisionData, attacker, victim, in attackerWeapon, isCrushThrough);
		}

		// Token: 0x04000A39 RID: 2617
		private const float SallyOutSiegeEngineDamageMultiplier = 4.5f;
	}
}
