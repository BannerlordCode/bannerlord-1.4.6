using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001DB RID: 475
	public static class MBSkeletonExtensions
	{
		// Token: 0x06001C09 RID: 7177 RVA: 0x00060DBE File Offset: 0x0005EFBE
		public static Skeleton CreateWithActionSet(ref AnimationSystemData animationSystemData)
		{
			return MBAPI.IMBSkeletonExtensions.CreateWithActionSet(ref animationSystemData);
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x00060DCB File Offset: 0x0005EFCB
		public static float GetSkeletonFaceAnimationTime(Skeleton skeleton)
		{
			return MBAPI.IMBSkeletonExtensions.GetSkeletonFaceAnimationTime(skeleton.Pointer);
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00060DDD File Offset: 0x0005EFDD
		public static void SetSkeletonFaceAnimationTime(Skeleton skeleton, float time)
		{
			MBAPI.IMBSkeletonExtensions.SetSkeletonFaceAnimationTime(skeleton.Pointer, time);
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x00060DF0 File Offset: 0x0005EFF0
		public static string GetSkeletonFaceAnimationName(Skeleton skeleton)
		{
			return MBAPI.IMBSkeletonExtensions.GetSkeletonFaceAnimationName(skeleton.Pointer);
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00060E04 File Offset: 0x0005F004
		public static MatrixFrame GetBoneEntitialFrameAtAnimationProgress(this Skeleton skeleton, sbyte boneIndex, int animationIndex, float progress)
		{
			MatrixFrame matrixFrame = default(MatrixFrame);
			MBAPI.IMBSkeletonExtensions.GetBoneEntitialFrameAtAnimationProgress(skeleton.Pointer, boneIndex, animationIndex, progress, ref matrixFrame);
			return matrixFrame;
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x00060E30 File Offset: 0x0005F030
		public static MatrixFrame GetBoneEntitialFrame(this Skeleton skeleton, sbyte boneNumber, bool forceToUpdate = false)
		{
			MatrixFrame matrixFrame = default(MatrixFrame);
			MBAPI.IMBSkeletonExtensions.GetBoneEntitialFrame(skeleton.Pointer, boneNumber, false, forceToUpdate, ref matrixFrame);
			return matrixFrame;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00060E5B File Offset: 0x0005F05B
		public static void SetFacialAnimation(this Skeleton skeleton, Agent.FacialAnimChannel channel, string faceAnimation, bool playSound, bool loop)
		{
			MBAPI.IMBSkeletonExtensions.SetFacialAnimationOfChannel(skeleton.Pointer, (int)channel, faceAnimation, playSound, loop);
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00060E72 File Offset: 0x0005F072
		public static void SetAgentActionChannel(this Skeleton skeleton, int actionChannelNo, in ActionIndexCache actionIndex, float channelParameter = 0f, float blendPeriodOverride = -0.2f, bool forceFaceMorphRestart = true, float blendWithNextActionFactor = 0f)
		{
			MBAPI.IMBSkeletonExtensions.SetAgentActionChannel(skeleton.Pointer, actionChannelNo, actionIndex.Index, channelParameter, blendPeriodOverride, forceFaceMorphRestart, blendWithNextActionFactor);
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x00060E92 File Offset: 0x0005F092
		public static bool DoesActionContinueWithCurrentActionAtChannel(this Skeleton skeleton, int actionChannelNo, in ActionIndexCache actionIndex)
		{
			return MBAPI.IMBSkeletonExtensions.DoesActionContinueWithCurrentActionAtChannel(skeleton.Pointer, actionChannelNo, actionIndex.Index);
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00060EAB File Offset: 0x0005F0AB
		public static void TickActionChannels(this Skeleton skeleton)
		{
			MBAPI.IMBSkeletonExtensions.TickActionChannels(skeleton.Pointer);
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00060EC0 File Offset: 0x0005F0C0
		public static void SetAnimationAtChannel(this Skeleton skeleton, string animationName, int channelNo, float animationSpeedMultiplier = 1f, float blendInPeriod = -1f, float startProgress = 0f)
		{
			int indexWithID = MBAPI.IMBAnimation.GetIndexWithID(animationName);
			skeleton.SetAnimationAtChannel(indexWithID, channelNo, animationSpeedMultiplier, blendInPeriod, startProgress);
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x00060EE6 File Offset: 0x0005F0E6
		public static void SetAnimationAtChannel(this Skeleton skeleton, int animationIndex, int channelNo, float animationSpeedMultiplier = 1f, float blendInPeriod = -1f, float startProgress = 0f)
		{
			MBAPI.IMBSkeletonExtensions.SetAnimationAtChannel(skeleton.Pointer, animationIndex, channelNo, animationSpeedMultiplier, blendInPeriod, startProgress);
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x00060EFF File Offset: 0x0005F0FF
		public static ActionIndexCache GetActionAtChannel(this Skeleton skeleton, int channelNo)
		{
			return new ActionIndexCache(MBAPI.IMBSkeletonExtensions.GetActionAtChannel(skeleton.Pointer, channelNo));
		}
	}
}
