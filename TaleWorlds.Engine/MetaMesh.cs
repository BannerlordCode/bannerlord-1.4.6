using System;
using System.Collections.Generic;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200006B RID: 107
	[EngineClass("rglMeta_mesh")]
	public sealed class MetaMesh : GameEntityComponent
	{
		// Token: 0x060009ED RID: 2541 RVA: 0x00009E3A File Offset: 0x0000803A
		internal MetaMesh(UIntPtr pointer)
			: base(pointer)
		{
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00009E43 File Offset: 0x00008043
		public static MetaMesh CreateMetaMesh(string name = null)
		{
			return EngineApplicationInterface.IMetaMesh.CreateMetaMesh(name);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00009E50 File Offset: 0x00008050
		public bool IsValid
		{
			get
			{
				return base.Pointer != UIntPtr.Zero;
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00009E62 File Offset: 0x00008062
		public int GetLodMaskForMeshAtIndex(int index)
		{
			return EngineApplicationInterface.IMetaMesh.GetLodMaskForMeshAtIndex(base.Pointer, index);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00009E75 File Offset: 0x00008075
		public int GetTotalGpuSize()
		{
			return EngineApplicationInterface.IMetaMesh.GetTotalGpuSize(base.Pointer);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00009E87 File Offset: 0x00008087
		public int RemoveMeshesWithTag(string tag)
		{
			return EngineApplicationInterface.IMetaMesh.RemoveMeshesWithTag(base.Pointer, tag);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00009E9A File Offset: 0x0000809A
		public int RemoveMeshesWithoutTag(string tag)
		{
			return EngineApplicationInterface.IMetaMesh.RemoveMeshesWithoutTag(base.Pointer, tag);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00009EAD File Offset: 0x000080AD
		public int GetMeshCountWithTag(string tag)
		{
			return EngineApplicationInterface.IMetaMesh.GetMeshCountWithTag(base.Pointer, tag);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00009EC0 File Offset: 0x000080C0
		public bool HasVertexBufferOrEditDataOrPackageItem()
		{
			return EngineApplicationInterface.IMetaMesh.HasVertexBufferOrEditDataOrPackageItem(base.Pointer);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00009ED2 File Offset: 0x000080D2
		public bool HasAnyGeneratedLods()
		{
			return EngineApplicationInterface.IMetaMesh.HasAnyGeneratedLods(base.Pointer);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00009EE4 File Offset: 0x000080E4
		public bool HasAnyLods()
		{
			return EngineApplicationInterface.IMetaMesh.HasAnyLods(base.Pointer);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00009EF6 File Offset: 0x000080F6
		public static MetaMesh GetCopy(string metaMeshName, bool showErrors = true, bool mayReturnNull = false)
		{
			return EngineApplicationInterface.IMetaMesh.CreateCopyFromName(metaMeshName, showErrors, mayReturnNull);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00009F05 File Offset: 0x00008105
		public void CopyTo(MetaMesh res, bool copyMeshes = true)
		{
			EngineApplicationInterface.IMetaMesh.CopyTo(base.Pointer, res.Pointer, copyMeshes);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00009F1E File Offset: 0x0000811E
		public void ClearMeshesForOtherLods(int lodToKeep)
		{
			EngineApplicationInterface.IMetaMesh.ClearMeshesForOtherLods(base.Pointer, lodToKeep);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00009F31 File Offset: 0x00008131
		public void ClearMeshesForLod(int lodToClear)
		{
			EngineApplicationInterface.IMetaMesh.ClearMeshesForLod(base.Pointer, lodToClear);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00009F44 File Offset: 0x00008144
		public void ClearMeshesForLowerLods(int lodToClear)
		{
			EngineApplicationInterface.IMetaMesh.ClearMeshesForLowerLods(base.Pointer, lodToClear);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00009F57 File Offset: 0x00008157
		public void ClearMeshes()
		{
			EngineApplicationInterface.IMetaMesh.ClearMeshes(base.Pointer);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00009F69 File Offset: 0x00008169
		public void SetNumLods(int lodToClear)
		{
			EngineApplicationInterface.IMetaMesh.SetNumLods(base.Pointer, lodToClear);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00009F7C File Offset: 0x0000817C
		public static void CheckMetaMeshExistence(string metaMeshName, int lod_count_check)
		{
			EngineApplicationInterface.IMetaMesh.CheckMetaMeshExistence(metaMeshName, lod_count_check);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00009F8A File Offset: 0x0000818A
		public static MetaMesh GetMorphedCopy(string metaMeshName, float morphTarget, bool showErrors)
		{
			return EngineApplicationInterface.IMetaMesh.GetMorphedCopy(metaMeshName, morphTarget, showErrors);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00009F99 File Offset: 0x00008199
		public MetaMesh CreateCopy()
		{
			return EngineApplicationInterface.IMetaMesh.CreateCopy(base.Pointer);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00009FAB File Offset: 0x000081AB
		public void AddMesh(Mesh mesh)
		{
			EngineApplicationInterface.IMetaMesh.AddMesh(base.Pointer, mesh.Pointer, 0U);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00009FC4 File Offset: 0x000081C4
		public void AddMesh(Mesh mesh, uint lodLevel)
		{
			EngineApplicationInterface.IMetaMesh.AddMesh(base.Pointer, mesh.Pointer, lodLevel);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00009FDD File Offset: 0x000081DD
		public void AddMetaMesh(MetaMesh metaMesh)
		{
			EngineApplicationInterface.IMetaMesh.AddMetaMesh(base.Pointer, metaMesh.Pointer);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00009FF5 File Offset: 0x000081F5
		public void SetCullMode(MBMeshCullingMode cullMode)
		{
			EngineApplicationInterface.IMetaMesh.SetCullMode(base.Pointer, cullMode);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0000A008 File Offset: 0x00008208
		public void AddMaterialShaderFlag(string materialShaderFlag)
		{
			for (int i = 0; i < this.MeshCount; i++)
			{
				Mesh meshAtIndex = this.GetMeshAtIndex(i);
				Material material = meshAtIndex.GetMaterial();
				material = material.CreateCopy();
				material.AddMaterialShaderFlag(materialShaderFlag, false);
				meshAtIndex.SetMaterial(material);
			}
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0000A049 File Offset: 0x00008249
		public void MergeMultiMeshes(MetaMesh metaMesh)
		{
			EngineApplicationInterface.IMetaMesh.MergeMultiMeshes(base.Pointer, metaMesh.Pointer);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0000A061 File Offset: 0x00008261
		public void AssignClothBodyFrom(MetaMesh metaMesh)
		{
			EngineApplicationInterface.IMetaMesh.AssignClothBodyFrom(base.Pointer, metaMesh.Pointer);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0000A079 File Offset: 0x00008279
		public void BatchMultiMeshes(MetaMesh metaMesh)
		{
			EngineApplicationInterface.IMetaMesh.BatchMultiMeshes(base.Pointer, metaMesh.Pointer);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0000A091 File Offset: 0x00008291
		public bool HasClothData()
		{
			return EngineApplicationInterface.IMetaMesh.HasClothData(base.Pointer);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0000A0A4 File Offset: 0x000082A4
		public void BatchMultiMeshesMultiple(List<MetaMesh> metaMeshes)
		{
			UIntPtr[] array = new UIntPtr[metaMeshes.Count];
			for (int i = 0; i < metaMeshes.Count; i++)
			{
				array[i] = metaMeshes[i].Pointer;
			}
			EngineApplicationInterface.IMetaMesh.BatchMultiMeshesMultiple(base.Pointer, array, metaMeshes.Count);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0000A0F4 File Offset: 0x000082F4
		public void ClearEditData()
		{
			EngineApplicationInterface.IMetaMesh.ClearEditData(base.Pointer);
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0000A106 File Offset: 0x00008306
		public int MeshCount
		{
			get
			{
				return EngineApplicationInterface.IMetaMesh.GetMeshCount(base.Pointer);
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0000A118 File Offset: 0x00008318
		public Mesh GetMeshAtIndex(int meshIndex)
		{
			return EngineApplicationInterface.IMetaMesh.GetMeshAtIndex(base.Pointer, meshIndex);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0000A12C File Offset: 0x0000832C
		public Mesh GetFirstMeshWithTag(string tag)
		{
			for (int i = 0; i < this.MeshCount; i++)
			{
				Mesh meshAtIndex = this.GetMeshAtIndex(i);
				if (meshAtIndex.HasTag(tag))
				{
					return meshAtIndex;
				}
			}
			return null;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0000A15E File Offset: 0x0000835E
		private void Release()
		{
			EngineApplicationInterface.IMetaMesh.Release(base.Pointer);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0000A170 File Offset: 0x00008370
		public uint GetFactor1()
		{
			return EngineApplicationInterface.IMetaMesh.GetFactor1(base.Pointer);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0000A182 File Offset: 0x00008382
		public void SetGlossMultiplier(float value)
		{
			EngineApplicationInterface.IMetaMesh.SetGlossMultiplier(base.Pointer, value);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0000A195 File Offset: 0x00008395
		public uint GetFactor2()
		{
			return EngineApplicationInterface.IMetaMesh.GetFactor2(base.Pointer);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0000A1A7 File Offset: 0x000083A7
		public void SetFactor1Linear(uint linearFactorColor1)
		{
			EngineApplicationInterface.IMetaMesh.SetFactor1Linear(base.Pointer, linearFactorColor1);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0000A1BA File Offset: 0x000083BA
		public void SetFactor2Linear(uint linearFactorColor2)
		{
			EngineApplicationInterface.IMetaMesh.SetFactor2Linear(base.Pointer, linearFactorColor2);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0000A1CD File Offset: 0x000083CD
		public void SetFactor1(uint factorColor1)
		{
			EngineApplicationInterface.IMetaMesh.SetFactor1(base.Pointer, factorColor1);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0000A1E0 File Offset: 0x000083E0
		public void SetFactor2(uint factorColor2)
		{
			EngineApplicationInterface.IMetaMesh.SetFactor2(base.Pointer, factorColor2);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0000A1F3 File Offset: 0x000083F3
		public void SetVectorArgument(float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			EngineApplicationInterface.IMetaMesh.SetVectorArgument(base.Pointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0000A20A File Offset: 0x0000840A
		public void SetVectorArgument2(float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			EngineApplicationInterface.IMetaMesh.SetVectorArgument2(base.Pointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0000A221 File Offset: 0x00008421
		public Vec3 GetVectorArgument2()
		{
			return EngineApplicationInterface.IMetaMesh.GetVectorArgument2(base.Pointer);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0000A233 File Offset: 0x00008433
		public void SetMaterial(Material material)
		{
			EngineApplicationInterface.IMetaMesh.SetMaterial(base.Pointer, material.Pointer);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0000A24B File Offset: 0x0000844B
		public void SetShaderToMaterial(string shaderName)
		{
			EngineApplicationInterface.IMetaMesh.SetShaderToMaterial(base.Pointer, shaderName);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0000A25E File Offset: 0x0000845E
		public void SetLodBias(int lodBias)
		{
			EngineApplicationInterface.IMetaMesh.SetLodBias(base.Pointer, lodBias);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0000A271 File Offset: 0x00008471
		public void SetBillboarding(BillboardType billboard)
		{
			EngineApplicationInterface.IMetaMesh.SetBillboarding(base.Pointer, billboard);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0000A284 File Offset: 0x00008484
		public void UseHeadBoneFaceGenScaling(Skeleton skeleton, sbyte headLookDirectionBoneIndex, MatrixFrame frame)
		{
			EngineApplicationInterface.IMetaMesh.UseHeadBoneFaceGenScaling(base.Pointer, skeleton.Pointer, headLookDirectionBoneIndex, ref frame);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0000A29F File Offset: 0x0000849F
		public void DrawTextWithDefaultFont(string text, Vec2 textPositionMin, Vec2 textPositionMax, Vec2 size, uint color, TextFlags flags)
		{
			EngineApplicationInterface.IMetaMesh.DrawTextWithDefaultFont(base.Pointer, text, textPositionMin, textPositionMax, size, color, flags);
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0000A2BC File Offset: 0x000084BC
		// (set) Token: 0x06000A22 RID: 2594 RVA: 0x0000A2E4 File Offset: 0x000084E4
		public MatrixFrame Frame
		{
			get
			{
				MatrixFrame matrixFrame = default(MatrixFrame);
				EngineApplicationInterface.IMetaMesh.GetFrame(base.Pointer, ref matrixFrame);
				return matrixFrame;
			}
			set
			{
				EngineApplicationInterface.IMetaMesh.SetFrame(base.Pointer, ref value);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0000A2F8 File Offset: 0x000084F8
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x0000A30A File Offset: 0x0000850A
		public Vec3 VectorUserData
		{
			get
			{
				return EngineApplicationInterface.IMetaMesh.GetVectorUserData(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.IMetaMesh.SetVectorUserData(base.Pointer, ref value);
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0000A31E File Offset: 0x0000851E
		public void PreloadForRendering()
		{
			EngineApplicationInterface.IMetaMesh.PreloadForRendering(base.Pointer);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0000A330 File Offset: 0x00008530
		public int CheckResources()
		{
			return EngineApplicationInterface.IMetaMesh.CheckResources(base.Pointer);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0000A342 File Offset: 0x00008542
		public void PreloadShaders(bool useTableau, bool useTeamColor)
		{
			EngineApplicationInterface.IMetaMesh.PreloadShaders(base.Pointer, useTableau, useTeamColor);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0000A356 File Offset: 0x00008556
		public void RecomputeBoundingBox(bool recomputeMeshes)
		{
			EngineApplicationInterface.IMetaMesh.RecomputeBoundingBox(base.Pointer, recomputeMeshes);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0000A369 File Offset: 0x00008569
		public void AddEditDataUser()
		{
			EngineApplicationInterface.IMetaMesh.AddEditDataUser(base.Pointer);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0000A37B File Offset: 0x0000857B
		public void ReleaseEditDataUser()
		{
			EngineApplicationInterface.IMetaMesh.ReleaseEditDataUser(base.Pointer);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0000A38D File Offset: 0x0000858D
		public void SetEditDataPolicy(EditDataPolicy policy)
		{
			EngineApplicationInterface.IMetaMesh.SetEditDataPolicy(base.Pointer, policy);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0000A3A0 File Offset: 0x000085A0
		public MatrixFrame Fit()
		{
			MatrixFrame identity = MatrixFrame.Identity;
			Vec3 vec = new Vec3(1000000f, 1000000f, 1000000f, -1f);
			Vec3 vec2 = new Vec3(-1000000f, -1000000f, -1000000f, -1f);
			for (int num = 0; num != this.MeshCount; num++)
			{
				Vec3 boundingBoxMin = this.GetMeshAtIndex(num).GetBoundingBoxMin();
				Vec3 boundingBoxMax = this.GetMeshAtIndex(num).GetBoundingBoxMax();
				vec = Vec3.Vec3Min(vec, boundingBoxMin);
				vec2 = Vec3.Vec3Max(vec2, boundingBoxMax);
			}
			Vec3 vec3 = (vec + vec2) * 0.5f;
			float num2 = MathF.Max(vec2.x - vec.x, vec2.y - vec.y);
			float num3 = 0.95f / num2;
			identity.origin -= vec3 * num3;
			identity.rotation.ApplyScaleLocal(num3);
			return identity;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0000A49C File Offset: 0x0000869C
		public BoundingBox GetBoundingBox()
		{
			BoundingBox boundingBox = default(BoundingBox);
			EngineApplicationInterface.IMetaMesh.GetBoundingBox(base.Pointer, ref boundingBox);
			return boundingBox;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0000A4C4 File Offset: 0x000086C4
		public VisibilityMaskFlags GetVisibilityMask()
		{
			return EngineApplicationInterface.IMetaMesh.GetVisibilityMask(base.Pointer);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0000A4D6 File Offset: 0x000086D6
		public void SetVisibilityMask(VisibilityMaskFlags visibilityMask)
		{
			EngineApplicationInterface.IMetaMesh.SetVisibilityMask(base.Pointer, visibilityMask);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0000A4E9 File Offset: 0x000086E9
		public string GetName()
		{
			return EngineApplicationInterface.IMetaMesh.GetName(base.Pointer);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0000A4FC File Offset: 0x000086FC
		public static void GetAllMultiMeshes(ref List<MetaMesh> multiMeshList)
		{
			int multiMeshCount = EngineApplicationInterface.IMetaMesh.GetMultiMeshCount();
			UIntPtr[] array = new UIntPtr[multiMeshCount];
			EngineApplicationInterface.IMetaMesh.GetAllMultiMeshes(array);
			for (int i = 0; i < multiMeshCount; i++)
			{
				multiMeshList.Add(new MetaMesh(array[i]));
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0000A542 File Offset: 0x00008742
		public static MetaMesh GetMultiMesh(string name)
		{
			return EngineApplicationInterface.IMetaMesh.GetMultiMesh(name);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0000A54F File Offset: 0x0000874F
		public void SetContourState(bool alwaysVisible)
		{
			EngineApplicationInterface.IMetaMesh.SetContourState(base.Pointer, alwaysVisible);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0000A562 File Offset: 0x00008762
		public void SetContourColor(uint color)
		{
			EngineApplicationInterface.IMetaMesh.SetContourColor(base.Pointer, color);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0000A575 File Offset: 0x00008775
		public void SetMaterialToSubMeshesWithTag(Material bodyMaterial, string tag)
		{
			EngineApplicationInterface.IMetaMesh.SetMaterialToSubMeshesWithTag(base.Pointer, bodyMaterial.Pointer, tag);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0000A58E File Offset: 0x0000878E
		public void SetFactorColorToSubMeshesWithTag(uint color, string tag)
		{
			EngineApplicationInterface.IMetaMesh.SetFactorColorToSubMeshesWithTag(base.Pointer, color, tag);
		}
	}
}
