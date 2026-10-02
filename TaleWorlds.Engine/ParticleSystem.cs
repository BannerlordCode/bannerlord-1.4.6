using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000072 RID: 114
	[EngineClass("rglParticle_system_instanced")]
	public sealed class ParticleSystem : GameEntityComponent
	{
		// Token: 0x06000A6D RID: 2669 RVA: 0x0000A9C3 File Offset: 0x00008BC3
		internal ParticleSystem(UIntPtr pointer)
			: base(pointer)
		{
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0000A9CC File Offset: 0x00008BCC
		public static ParticleSystem CreateParticleSystemAttachedToBone(string systemName, Skeleton skeleton, sbyte boneIndex, ref MatrixFrame boneLocalFrame)
		{
			return ParticleSystem.CreateParticleSystemAttachedToBone(ParticleSystemManager.GetRuntimeIdByName(systemName), skeleton, boneIndex, ref boneLocalFrame);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0000A9DC File Offset: 0x00008BDC
		public static ParticleSystem CreateParticleSystemAttachedToBone(int systemRuntimeId, Skeleton skeleton, sbyte boneIndex, ref MatrixFrame boneLocalFrame)
		{
			return EngineApplicationInterface.IParticleSystem.CreateParticleSystemAttachedToBone(systemRuntimeId, skeleton.Pointer, boneIndex, ref boneLocalFrame);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0000A9F1 File Offset: 0x00008BF1
		public static ParticleSystem CreateParticleSystemAttachedToEntity(string systemName, GameEntity parentEntity, ref MatrixFrame boneLocalFrame)
		{
			return ParticleSystem.CreateParticleSystemAttachedToEntity(ParticleSystemManager.GetRuntimeIdByName(systemName), parentEntity, ref boneLocalFrame);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0000AA00 File Offset: 0x00008C00
		public static ParticleSystem CreateParticleSystemAttachedToEntity(string systemName, WeakGameEntity parentEntity, ref MatrixFrame boneLocalFrame)
		{
			return ParticleSystem.CreateParticleSystemAttachedToEntity(ParticleSystemManager.GetRuntimeIdByName(systemName), parentEntity, ref boneLocalFrame);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0000AA0F File Offset: 0x00008C0F
		public static ParticleSystem CreateParticleSystemAttachedToEntity(int systemRuntimeId, GameEntity parentEntity, ref MatrixFrame boneLocalFrame)
		{
			return EngineApplicationInterface.IParticleSystem.CreateParticleSystemAttachedToEntity(systemRuntimeId, parentEntity.Pointer, ref boneLocalFrame);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0000AA23 File Offset: 0x00008C23
		public static ParticleSystem CreateParticleSystemAttachedToEntity(int systemRuntimeId, WeakGameEntity parentEntity, ref MatrixFrame boneLocalFrame)
		{
			return EngineApplicationInterface.IParticleSystem.CreateParticleSystemAttachedToEntity(systemRuntimeId, parentEntity.Pointer, ref boneLocalFrame);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0000AA38 File Offset: 0x00008C38
		public void AddMesh(Mesh mesh)
		{
			EngineApplicationInterface.IMetaMesh.AddMesh(base.Pointer, mesh.Pointer, 0U);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0000AA51 File Offset: 0x00008C51
		public void SetEnable(bool enable)
		{
			EngineApplicationInterface.IParticleSystem.SetEnable(base.Pointer, enable);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0000AA64 File Offset: 0x00008C64
		public void SetRuntimeEmissionRateMultiplier(float multiplier)
		{
			EngineApplicationInterface.IParticleSystem.SetRuntimeEmissionRateMultiplier(base.Pointer, multiplier);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0000AA77 File Offset: 0x00008C77
		public void Restart()
		{
			EngineApplicationInterface.IParticleSystem.Restart(base.Pointer);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0000AA89 File Offset: 0x00008C89
		public void SetLocalFrame(in MatrixFrame newLocalFrame)
		{
			EngineApplicationInterface.IParticleSystem.SetLocalFrame(base.Pointer, in newLocalFrame);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0000AA9C File Offset: 0x00008C9C
		public void SetPreviousGlobalFrame(in MatrixFrame globalFrame)
		{
			EngineApplicationInterface.IParticleSystem.SetPreviousGlobalFrame(base.Pointer, in globalFrame);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		public MatrixFrame GetLocalFrame()
		{
			MatrixFrame identity = MatrixFrame.Identity;
			EngineApplicationInterface.IParticleSystem.GetLocalFrame(base.Pointer, ref identity);
			return identity;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0000AAD6 File Offset: 0x00008CD6
		public bool HasAliveParticles()
		{
			return EngineApplicationInterface.IParticleSystem.HasAliveParticles(base.Pointer);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		public void SetDontRemoveFromEntity(bool value)
		{
			EngineApplicationInterface.IParticleSystem.SetDontRemoveFromEntity(base.Pointer, value);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0000AAFB File Offset: 0x00008CFB
		public void SetParticleEffectByName(string effectName)
		{
			EngineApplicationInterface.IParticleSystem.SetParticleEffectByName(base.Pointer, effectName);
		}
	}
}
