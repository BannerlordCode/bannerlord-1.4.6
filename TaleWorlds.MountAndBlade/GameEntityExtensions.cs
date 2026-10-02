using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000221 RID: 545
	public static class GameEntityExtensions
	{
		// Token: 0x06002091 RID: 8337 RVA: 0x00072574 File Offset: 0x00070774
		public static GameEntity Instantiate(Scene scene, MissionWeapon weapon, bool showHolsterWithWeapon, bool needBatchedVersion)
		{
			WeaponData weaponData = weapon.GetWeaponData(needBatchedVersion);
			WeaponStatsData[] weaponStatsData = weapon.GetWeaponStatsData();
			WeaponData ammoWeaponData = weapon.GetAmmoWeaponData(needBatchedVersion);
			WeaponStatsData[] ammoWeaponStatsData = weapon.GetAmmoWeaponStatsData();
			GameEntity gameEntity = MBAPI.IMBGameEntityExtensions.CreateFromWeapon(scene.Pointer, in weaponData, weaponStatsData, weaponStatsData.Length, in ammoWeaponData, ammoWeaponStatsData, ammoWeaponStatsData.Length, showHolsterWithWeapon);
			weaponData.DeinitializeManagedPointers();
			return gameEntity;
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000725C7 File Offset: 0x000707C7
		public static void CreateSimpleSkeleton(this GameEntity gameEntity, string skeletonName)
		{
			gameEntity.Skeleton = MBAPI.IMBSkeletonExtensions.CreateSimpleSkeleton(skeletonName);
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000725DA File Offset: 0x000707DA
		public static void CreateSimpleSkeleton(this WeakGameEntity gameEntity, string skeletonName)
		{
			gameEntity.Skeleton = MBAPI.IMBSkeletonExtensions.CreateSimpleSkeleton(skeletonName);
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000725F0 File Offset: 0x000707F0
		public static void CreateAgentSkeleton(this GameEntity gameEntity, string skeletonName, bool isHumanoid, MBActionSet actionSet, string monsterUsageSetName, Monster monster)
		{
			AnimationSystemData animationSystemData = monster.FillAnimationSystemData(actionSet, 1f, false);
			gameEntity.Skeleton = MBAPI.IMBSkeletonExtensions.CreateAgentSkeleton(skeletonName, isHumanoid, actionSet.Index, monsterUsageSetName, ref animationSystemData);
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x00072628 File Offset: 0x00070828
		public static void CreateAgentSkeleton(this WeakGameEntity gameEntity, string skeletonName, bool isHumanoid, MBActionSet actionSet, string monsterUsageSetName, Monster monster)
		{
			AnimationSystemData animationSystemData = monster.FillAnimationSystemData(actionSet, 1f, false);
			gameEntity.Skeleton = MBAPI.IMBSkeletonExtensions.CreateAgentSkeleton(skeletonName, isHumanoid, actionSet.Index, monsterUsageSetName, ref animationSystemData);
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x00072661 File Offset: 0x00070861
		public static void CreateSkeletonWithActionSet(this GameEntity gameEntity, ref AnimationSystemData animationSystemData)
		{
			gameEntity.Skeleton = MBSkeletonExtensions.CreateWithActionSet(ref animationSystemData);
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x0007266F File Offset: 0x0007086F
		public static void CreateSkeletonWithActionSet(this WeakGameEntity gameEntity, ref AnimationSystemData animationSystemData)
		{
			gameEntity.Skeleton = MBSkeletonExtensions.CreateWithActionSet(ref animationSystemData);
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x0007267E File Offset: 0x0007087E
		public static void FadeOut(this GameEntity gameEntity, float interval, bool isRemovingFromScene)
		{
			MBAPI.IMBGameEntityExtensions.FadeOut(gameEntity.Pointer, interval, isRemovingFromScene);
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x00072692 File Offset: 0x00070892
		public static void FadeIn(this GameEntity gameEntity, bool resetAlpha = true)
		{
			MBAPI.IMBGameEntityExtensions.FadeIn(gameEntity.Pointer, resetAlpha);
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000726A5 File Offset: 0x000708A5
		public static void HideIfNotFadingOut(this GameEntity gameEntity)
		{
			MBAPI.IMBGameEntityExtensions.HideIfNotFadingOut(gameEntity.Pointer);
		}
	}
}
