using System;
using System.Collections.Generic;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200008C RID: 140
	[EngineClass("rglSkeleton")]
	public sealed class Skeleton : NativeObject
	{
		// Token: 0x06000C5F RID: 3167 RVA: 0x0000DAB8 File Offset: 0x0000BCB8
		internal Skeleton(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0000DAC7 File Offset: 0x0000BCC7
		public static Skeleton CreateFromModel(string modelName)
		{
			return EngineApplicationInterface.ISkeleton.CreateFromModel(modelName);
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		public static Skeleton CreateFromModelWithNullAnimTree(GameEntity entity, string modelName, float boneScale = 1f)
		{
			return EngineApplicationInterface.ISkeleton.CreateFromModelWithNullAnimTree(entity.Pointer, modelName, boneScale);
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x0000DAE8 File Offset: 0x0000BCE8
		public bool IsValid
		{
			get
			{
				return base.Pointer != UIntPtr.Zero;
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0000DAFA File Offset: 0x0000BCFA
		public string GetName()
		{
			return EngineApplicationInterface.ISkeleton.GetName(this);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0000DB07 File Offset: 0x0000BD07
		public string GetBoneName(sbyte boneIndex)
		{
			return EngineApplicationInterface.ISkeleton.GetBoneName(this, boneIndex);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0000DB15 File Offset: 0x0000BD15
		public sbyte GetBoneChildAtIndex(sbyte boneIndex, sbyte childIndex)
		{
			return EngineApplicationInterface.ISkeleton.GetBoneChildAtIndex(this, boneIndex, childIndex);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0000DB24 File Offset: 0x0000BD24
		public sbyte GetBoneChildCount(sbyte boneIndex)
		{
			return EngineApplicationInterface.ISkeleton.GetBoneChildCount(this, boneIndex);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0000DB32 File Offset: 0x0000BD32
		public sbyte GetParentBoneIndex(sbyte boneIndex)
		{
			return EngineApplicationInterface.ISkeleton.GetParentBoneIndex(this, boneIndex);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0000DB40 File Offset: 0x0000BD40
		public void AddMeshToBone(UIntPtr mesh, sbyte boneIndex)
		{
			EngineApplicationInterface.ISkeleton.AddMeshToBone(base.Pointer, mesh, boneIndex);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0000DB54 File Offset: 0x0000BD54
		public void Freeze(bool p)
		{
			EngineApplicationInterface.ISkeleton.Freeze(base.Pointer, p);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0000DB67 File Offset: 0x0000BD67
		public bool IsFrozen()
		{
			return EngineApplicationInterface.ISkeleton.IsFrozen(base.Pointer);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0000DB79 File Offset: 0x0000BD79
		public void SetBoneLocalFrame(sbyte boneIndex, MatrixFrame localFrame)
		{
			EngineApplicationInterface.ISkeleton.SetBoneLocalFrame(base.Pointer, boneIndex, ref localFrame);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0000DB8E File Offset: 0x0000BD8E
		public sbyte GetBoneCount()
		{
			return EngineApplicationInterface.ISkeleton.GetBoneCount(base.Pointer);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0000DBA0 File Offset: 0x0000BDA0
		public void GetBoneBody(sbyte boneIndex, ref CapsuleData data)
		{
			EngineApplicationInterface.ISkeleton.GetBoneBody(base.Pointer, boneIndex, ref data);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		public static bool SkeletonModelExist(string skeletonModelName)
		{
			return EngineApplicationInterface.ISkeleton.SkeletonModelExist(skeletonModelName);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0000DBC1 File Offset: 0x0000BDC1
		public void ForceUpdateBoneFrames()
		{
			EngineApplicationInterface.ISkeleton.ForceUpdateBoneFrames(base.Pointer);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0000DBD4 File Offset: 0x0000BDD4
		public MatrixFrame GetBoneEntitialFrameWithIndex(sbyte boneIndex)
		{
			MatrixFrame matrixFrame = default(MatrixFrame);
			EngineApplicationInterface.ISkeleton.GetBoneEntitialFrameWithIndex(base.Pointer, boneIndex, ref matrixFrame);
			return matrixFrame;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0000DC00 File Offset: 0x0000BE00
		public MatrixFrame GetBoneEntitialFrameWithName(string boneName)
		{
			MatrixFrame matrixFrame = default(MatrixFrame);
			EngineApplicationInterface.ISkeleton.GetBoneEntitialFrameWithName(base.Pointer, boneName, ref matrixFrame);
			return matrixFrame;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0000DC29 File Offset: 0x0000BE29
		public RagdollState GetCurrentRagdollState()
		{
			return EngineApplicationInterface.ISkeleton.GetCurrentRagdollState(base.Pointer);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0000DC3B File Offset: 0x0000BE3B
		public void ActivateRagdoll()
		{
			EngineApplicationInterface.ISkeleton.ActivateRagdoll(base.Pointer);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0000DC4D File Offset: 0x0000BE4D
		public sbyte GetSkeletonBoneMapping(sbyte boneIndex)
		{
			return EngineApplicationInterface.ISkeleton.GetSkeletonBoneMapping(base.Pointer, boneIndex);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0000DC60 File Offset: 0x0000BE60
		public void AddMesh(Mesh mesh)
		{
			EngineApplicationInterface.ISkeleton.AddMesh(base.Pointer, mesh.Pointer);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0000DC78 File Offset: 0x0000BE78
		public void ClearComponents()
		{
			EngineApplicationInterface.ISkeleton.ClearComponents(base.Pointer);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0000DC8A File Offset: 0x0000BE8A
		public void AddComponent(GameEntityComponent component)
		{
			EngineApplicationInterface.ISkeleton.AddComponent(base.Pointer, component.Pointer);
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0000DCA2 File Offset: 0x0000BEA2
		public bool HasComponent(GameEntityComponent component)
		{
			return EngineApplicationInterface.ISkeleton.HasComponent(base.Pointer, component.Pointer);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0000DCBA File Offset: 0x0000BEBA
		public void RemoveComponent(GameEntityComponent component)
		{
			EngineApplicationInterface.ISkeleton.RemoveComponent(base.Pointer, component.Pointer);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0000DCD2 File Offset: 0x0000BED2
		public void ClearMeshes(bool clearBoneComponents = true)
		{
			EngineApplicationInterface.ISkeleton.ClearMeshes(base.Pointer, clearBoneComponents);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0000DCE5 File Offset: 0x0000BEE5
		public int GetComponentCount(GameEntity.ComponentType componentType)
		{
			return EngineApplicationInterface.ISkeleton.GetComponentCount(base.Pointer, componentType);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		public void UpdateEntitialFramesFromLocalFrames()
		{
			EngineApplicationInterface.ISkeleton.UpdateEntitialFramesFromLocalFrames(base.Pointer);
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0000DD0A File Offset: 0x0000BF0A
		public void ResetFrames()
		{
			EngineApplicationInterface.ISkeleton.ResetFrames(base.Pointer);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0000DD1C File Offset: 0x0000BF1C
		public GameEntityComponent GetComponentAtIndex(GameEntity.ComponentType componentType, int index)
		{
			return EngineApplicationInterface.ISkeleton.GetComponentAtIndex(base.Pointer, componentType, index);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0000DD30 File Offset: 0x0000BF30
		public void SetUsePreciseBoundingVolume(bool value)
		{
			EngineApplicationInterface.ISkeleton.SetUsePreciseBoundingVolume(base.Pointer, value);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0000DD44 File Offset: 0x0000BF44
		public MatrixFrame GetBoneEntitialRestFrame(sbyte boneIndex, bool useBoneMapping)
		{
			MatrixFrame matrixFrame = default(MatrixFrame);
			EngineApplicationInterface.ISkeleton.GetBoneEntitialRestFrame(base.Pointer, boneIndex, useBoneMapping, ref matrixFrame);
			return matrixFrame;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0000DD70 File Offset: 0x0000BF70
		public MatrixFrame GetBoneLocalRestFrame(sbyte boneIndex, bool useBoneMapping = true)
		{
			MatrixFrame matrixFrame = default(MatrixFrame);
			EngineApplicationInterface.ISkeleton.GetBoneLocalRestFrame(base.Pointer, boneIndex, useBoneMapping, ref matrixFrame);
			return matrixFrame;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0000DD9C File Offset: 0x0000BF9C
		public MatrixFrame GetBoneEntitialRestFrame(sbyte boneIndex)
		{
			MatrixFrame matrixFrame = default(MatrixFrame);
			EngineApplicationInterface.ISkeleton.GetBoneEntitialRestFrame(base.Pointer, boneIndex, true, ref matrixFrame);
			return matrixFrame;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0000DDC8 File Offset: 0x0000BFC8
		public MatrixFrame GetBoneEntitialFrameAtChannel(int channelNo, sbyte boneIndex)
		{
			MatrixFrame matrixFrame = default(MatrixFrame);
			EngineApplicationInterface.ISkeleton.GetBoneEntitialFrameAtChannel(base.Pointer, channelNo, boneIndex, ref matrixFrame);
			return matrixFrame;
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
		public MatrixFrame GetBoneEntitialFrame(sbyte boneIndex)
		{
			MatrixFrame matrixFrame = default(MatrixFrame);
			EngineApplicationInterface.ISkeleton.GetBoneEntitialFrame(base.Pointer, boneIndex, ref matrixFrame);
			return matrixFrame;
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0000DE1D File Offset: 0x0000C01D
		public int GetBoneComponentCount(sbyte boneIndex)
		{
			return EngineApplicationInterface.ISkeleton.GetBoneComponentCount(base.Pointer, boneIndex);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0000DE30 File Offset: 0x0000C030
		public GameEntityComponent GetBoneComponentAtIndex(sbyte boneIndex, int componentIndex)
		{
			return EngineApplicationInterface.ISkeleton.GetBoneComponentAtIndex(base.Pointer, boneIndex, componentIndex);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0000DE44 File Offset: 0x0000C044
		public bool HasBoneComponent(sbyte boneIndex, GameEntityComponent component)
		{
			return EngineApplicationInterface.ISkeleton.HasBoneComponent(base.Pointer, boneIndex, component);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0000DE58 File Offset: 0x0000C058
		public void AddComponentToBone(sbyte boneIndex, GameEntityComponent component)
		{
			EngineApplicationInterface.ISkeleton.AddComponentToBone(base.Pointer, boneIndex, component);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0000DE6C File Offset: 0x0000C06C
		public void RemoveBoneComponent(sbyte boneIndex, GameEntityComponent component)
		{
			EngineApplicationInterface.ISkeleton.RemoveBoneComponent(base.Pointer, boneIndex, component);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0000DE80 File Offset: 0x0000C080
		public void ClearMeshesAtBone(sbyte boneIndex)
		{
			EngineApplicationInterface.ISkeleton.ClearMeshesAtBone(base.Pointer, boneIndex);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0000DE93 File Offset: 0x0000C093
		public void TickAnimations(float dt, MatrixFrame globalFrame, bool tickAnimsForChildren)
		{
			EngineApplicationInterface.ISkeleton.TickAnimations(base.Pointer, ref globalFrame, dt, tickAnimsForChildren);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0000DEA9 File Offset: 0x0000C0A9
		public void TickAnimationsAndForceUpdate(float dt, MatrixFrame globalFrame, bool tickAnimsForChildren)
		{
			EngineApplicationInterface.ISkeleton.TickAnimationsAndForceUpdate(base.Pointer, ref globalFrame, dt, tickAnimsForChildren);
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0000DEBF File Offset: 0x0000C0BF
		public float GetAnimationParameterAtChannel(int channelNo)
		{
			return EngineApplicationInterface.ISkeleton.GetSkeletonAnimationParameterAtChannel(base.Pointer, channelNo);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0000DED2 File Offset: 0x0000C0D2
		public void SetAnimationParameterAtChannel(int channelNo, float parameter)
		{
			EngineApplicationInterface.ISkeleton.SetSkeletonAnimationParameterAtChannel(base.Pointer, channelNo, parameter);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0000DEE6 File Offset: 0x0000C0E6
		public float GetAnimationSpeedAtChannel(int channelNo)
		{
			return EngineApplicationInterface.ISkeleton.GetSkeletonAnimationSpeedAtChannel(base.Pointer, channelNo);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0000DEF9 File Offset: 0x0000C0F9
		public void SetAnimationSpeedAtChannel(int channelNo, float speed)
		{
			EngineApplicationInterface.ISkeleton.SetSkeletonAnimationSpeedAtChannel(base.Pointer, channelNo, speed);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0000DF0D File Offset: 0x0000C10D
		public void SetUptoDate(bool value)
		{
			EngineApplicationInterface.ISkeleton.SetSkeletonUptoDate(base.Pointer, value);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0000DF20 File Offset: 0x0000C120
		public string GetAnimationAtChannel(int channelNo)
		{
			return EngineApplicationInterface.ISkeleton.GetAnimationAtChannel(base.Pointer, channelNo);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0000DF33 File Offset: 0x0000C133
		public int GetAnimationIndexAtChannel(int channelNo)
		{
			return EngineApplicationInterface.ISkeleton.GetAnimationIndexAtChannel(base.Pointer, channelNo);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0000DF46 File Offset: 0x0000C146
		public void EnableScriptDrivenPostIntegrateCallback()
		{
			EngineApplicationInterface.ISkeleton.EnableScriptDrivenPostIntegrateCallback(base.Pointer);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0000DF58 File Offset: 0x0000C158
		public void ResetCloths()
		{
			EngineApplicationInterface.ISkeleton.ResetCloths(base.Pointer);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0000DF6A File Offset: 0x0000C16A
		public IEnumerable<Mesh> GetAllMeshes()
		{
			NativeObjectArray nativeObjectArray = NativeObjectArray.Create();
			EngineApplicationInterface.ISkeleton.GetAllMeshes(this, nativeObjectArray);
			foreach (NativeObject nativeObject in ((IEnumerable<NativeObject>)nativeObjectArray))
			{
				Mesh mesh = (Mesh)nativeObject;
				yield return mesh;
			}
			IEnumerator<NativeObject> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0000DF7A File Offset: 0x0000C17A
		public static sbyte GetBoneIndexFromName(string skeletonModelName, string boneName)
		{
			return EngineApplicationInterface.ISkeleton.GetBoneIndexFromName(skeletonModelName, boneName);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0000DF88 File Offset: 0x0000C188
		internal Transformation GetEntitialOutTransform(UIntPtr animResultPointer, sbyte boneIndex)
		{
			return EngineApplicationInterface.ISkeleton.GetEntitialOutTransform(base.Pointer, animResultPointer, boneIndex);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0000DF9C File Offset: 0x0000C19C
		internal void SetOutBoneDisplacement(UIntPtr animResultPointer, sbyte boneIndex, Vec3 displacement)
		{
			EngineApplicationInterface.ISkeleton.SetOutBoneDisplacement(base.Pointer, animResultPointer, boneIndex, displacement);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0000DFB1 File Offset: 0x0000C1B1
		internal void SetOutQuat(UIntPtr animResultPointer, sbyte boneIndex, Mat3 rotation)
		{
			EngineApplicationInterface.ISkeleton.SetOutQuat(base.Pointer, animResultPointer, boneIndex, rotation);
		}

		// Token: 0x040001C4 RID: 452
		public const sbyte MaxBoneCount = 64;
	}
}
