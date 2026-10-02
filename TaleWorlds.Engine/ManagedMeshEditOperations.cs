using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200005A RID: 90
	[EngineClass("rglManaged_mesh_edit_operations")]
	public sealed class ManagedMeshEditOperations : NativeObject
	{
		// Token: 0x060008D6 RID: 2262 RVA: 0x00007BB4 File Offset: 0x00005DB4
		internal ManagedMeshEditOperations(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00007BC3 File Offset: 0x00005DC3
		public static ManagedMeshEditOperations Create(Mesh meshToEdit)
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.Create(meshToEdit.Pointer);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00007BD5 File Offset: 0x00005DD5
		public void Weld()
		{
			EngineApplicationInterface.IManagedMeshEditOperations.Weld(base.Pointer);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00007BE7 File Offset: 0x00005DE7
		public int AddVertex(Vec3 vertexPos)
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.AddVertex(base.Pointer, ref vertexPos);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00007BFB File Offset: 0x00005DFB
		public int AddFaceCorner(int vertexIndex, Vec2 uv0, Vec3 color, Vec3 normal)
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.AddFaceCorner1(base.Pointer, vertexIndex, ref uv0, ref color, ref normal);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00007C14 File Offset: 0x00005E14
		public int AddFaceCorner(int vertexIndex, Vec2 uv0, Vec2 uv1, Vec3 color, Vec3 normal)
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.AddFaceCorner2(base.Pointer, vertexIndex, ref uv0, ref uv1, ref color, ref normal);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00007C2F File Offset: 0x00005E2F
		public int AddFace(int patchNode0, int patchNode1, int patchNode2)
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.AddFace(base.Pointer, patchNode0, patchNode1, patchNode2);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00007C44 File Offset: 0x00005E44
		public void AddTriangle(Vec3 p1, Vec3 p2, Vec3 p3, Vec2 uv1, Vec2 uv2, Vec2 uv3, Vec3 color)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddTriangle1(base.Pointer, ref p1, ref p2, ref p3, ref uv1, ref uv2, ref uv3, ref color);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00007C70 File Offset: 0x00005E70
		public void AddTriangle(Vec3 p1, Vec3 p2, Vec3 p3, Vec3 n1, Vec3 n2, Vec3 n3, Vec2 uv1, Vec2 uv2, Vec2 uv3, Vec3 c1, Vec3 c2, Vec3 c3)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddTriangle2(base.Pointer, ref p1, ref p2, ref p3, ref n1, ref n2, ref n3, ref uv1, ref uv2, ref uv3, ref c1, ref c2, ref c3);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00007CA5 File Offset: 0x00005EA5
		public void AddRectangle3(Vec3 o, Vec2 size, Vec2 uv_origin, Vec2 uvSize, Vec3 color)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddRectangle3(base.Pointer, ref o, ref size, ref uv_origin, ref uvSize, ref color);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00007CC1 File Offset: 0x00005EC1
		public void AddRectangleWithInverseUV(Vec3 o, Vec2 size, Vec2 uv_origin, Vec2 uvSize, Vec3 color)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddRectangleWithInverseUV(base.Pointer, ref o, ref size, ref uv_origin, ref uvSize, ref color);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00007CDD File Offset: 0x00005EDD
		public void AddRect(Vec3 originBegin, Vec3 originEnd, Vec2 uvBegin, Vec2 uvEnd, Vec3 color)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddRect(base.Pointer, ref originBegin, ref originEnd, ref uvBegin, ref uvEnd, ref color);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00007CF9 File Offset: 0x00005EF9
		public void AddRectWithZUp(Vec3 originBegin, Vec3 originEnd, Vec2 uvBegin, Vec2 uvEnd, Vec3 color)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddRectWithZUp(base.Pointer, ref originBegin, ref originEnd, ref uvBegin, ref uvEnd, ref color);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00007D15 File Offset: 0x00005F15
		public void InvertFacesWindingOrder()
		{
			EngineApplicationInterface.IManagedMeshEditOperations.InvertFacesWindingOrder(base.Pointer);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00007D27 File Offset: 0x00005F27
		public void ScaleVertices(float newScale)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ScaleVertices1(base.Pointer, newScale);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00007D3A File Offset: 0x00005F3A
		public void MoveVerticesAlongNormal(float moveAmount)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.MoveVerticesAlongNormal(base.Pointer, moveAmount);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00007D4D File Offset: 0x00005F4D
		public void ScaleVertices(Vec3 newScale, bool keepUvX = false, float maxUvSize = 1f)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ScaleVertices2(base.Pointer, ref newScale, keepUvX, maxUvSize);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00007D63 File Offset: 0x00005F63
		public void TranslateVertices(Vec3 newOrigin)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.TranslateVertices(base.Pointer, ref newOrigin);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00007D78 File Offset: 0x00005F78
		public void AddMeshAux(Mesh mesh, MatrixFrame frame, sbyte boneNo, Vec3 color, bool transformNormal, bool heightGradient, bool addSkinData, bool useDoublePrecision = true)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddMeshAux(base.Pointer, mesh.Pointer, ref frame, boneNo, ref color, transformNormal, heightGradient, addSkinData, useDoublePrecision);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00007DA8 File Offset: 0x00005FA8
		public int ComputeTangents(bool checkFixedNormals)
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.ComputeTangents(base.Pointer, checkFixedNormals);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00007DBB File Offset: 0x00005FBB
		public void GenerateGrid(Vec2i numEdges, Vec2 edgeScale)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.GenerateGrid(base.Pointer, ref numEdges, ref edgeScale);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00007DD1 File Offset: 0x00005FD1
		public void RescaleMesh2d(Vec2 scaleSizeMin, Vec2 scaleSizeMax)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.RescaleMesh2d(base.Pointer, ref scaleSizeMin, ref scaleSizeMax);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00007DE7 File Offset: 0x00005FE7
		public void RescaleMesh2dRepeatX(Vec2 scaleSizeMin, Vec2 scaleSizeMax, float frameThickness = 0f, int frameSide = 0)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.RescaleMesh2dRepeatX(base.Pointer, ref scaleSizeMin, ref scaleSizeMax, frameThickness, frameSide);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00007E00 File Offset: 0x00006000
		public void RescaleMesh2dRepeatY(Vec2 scaleSizeMin, Vec2 scaleSizeMax, float frameThickness = 0f, int frameSide = 0)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.RescaleMesh2dRepeatY(base.Pointer, ref scaleSizeMin, ref scaleSizeMax, frameThickness, frameSide);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00007E19 File Offset: 0x00006019
		public void RescaleMesh2dRepeatXWithTiling(Vec2 scaleSizeMin, Vec2 scaleSizeMax, float frameThickness = 0f, int frameSide = 0, float xyRatio = 0f)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.RescaleMesh2dRepeatXWithTiling(base.Pointer, ref scaleSizeMin, ref scaleSizeMax, frameThickness, frameSide, xyRatio);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00007E34 File Offset: 0x00006034
		public void RescaleMesh2dRepeatYWithTiling(Vec2 scaleSizeMin, Vec2 scaleSizeMax, float frameThickness = 0f, int frameSide = 0, float xyRatio = 0f)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.RescaleMesh2dRepeatYWithTiling(base.Pointer, ref scaleSizeMin, ref scaleSizeMax, frameThickness, frameSide, xyRatio);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00007E4F File Offset: 0x0000604F
		public void RescaleMesh2dWithoutChangingUV(Vec2 scaleSizeMin, Vec2 scaleSizeMax, float remaining)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.RescaleMesh2dRepeatYWithTiling(base.Pointer, ref scaleSizeMin, ref scaleSizeMax, remaining, 0, 0f);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00007E6C File Offset: 0x0000606C
		public void AddLine(Vec3 start, Vec3 end, Vec3 color, float lineWidth = 0.004f)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddLine(base.Pointer, ref start, ref end, ref color, lineWidth);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00007E86 File Offset: 0x00006086
		public void ComputeCornerNormals(bool checkFixedNormals = false, bool smoothCornerNormals = true)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ComputeCornerNormals(base.Pointer, checkFixedNormals, smoothCornerNormals);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00007E9A File Offset: 0x0000609A
		public void ComputeCornerNormalsWithSmoothingData()
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ComputeCornerNormalsWithSmoothingData(base.Pointer);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00007EAC File Offset: 0x000060AC
		public void AddMesh(Mesh mesh, MatrixFrame frame)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddMesh(base.Pointer, mesh.Pointer, ref frame);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00007EC6 File Offset: 0x000060C6
		public void AddMeshWithSkinData(Mesh mesh, MatrixFrame frame, sbyte boneIndex)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddMeshWithSkinData(base.Pointer, mesh.Pointer, ref frame, boneIndex);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00007EE1 File Offset: 0x000060E1
		public void AddMeshWithColor(Mesh mesh, MatrixFrame frame, Vec3 vertexColor, bool useDoublePrecision = true)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddMeshWithColor(base.Pointer, mesh.Pointer, ref frame, ref vertexColor, useDoublePrecision);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00007EFF File Offset: 0x000060FF
		public void AddMeshToBone(Mesh mesh, MatrixFrame frame, sbyte boneIndex)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddMeshToBone(base.Pointer, mesh.Pointer, ref frame, boneIndex);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00007F1A File Offset: 0x0000611A
		public void AddMeshWithFixedNormals(Mesh mesh, MatrixFrame frame)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddMeshWithFixedNormals(base.Pointer, mesh.Pointer, ref frame);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00007F34 File Offset: 0x00006134
		public void AddMeshWithFixedNormalsWithHeightGradientColor(Mesh mesh, MatrixFrame frame)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddMeshWithFixedNormalsWithHeightGradientColor(base.Pointer, mesh.Pointer, ref frame);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00007F4E File Offset: 0x0000614E
		public void AddSkinnedMeshWithColor(Mesh mesh, MatrixFrame frame, Vec3 vertexColor, bool useDoublePrecision = true)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddSkinnedMeshWithColor(base.Pointer, mesh.Pointer, ref frame, ref vertexColor, useDoublePrecision);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00007F6C File Offset: 0x0000616C
		public void SetCornerVertexColor(int cornerNo, Vec3 vertexColor)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.SetCornerVertexColor(base.Pointer, cornerNo, ref vertexColor);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00007F81 File Offset: 0x00006181
		public void SetCornerUV(int cornerNo, Vec2 newUV, int uvNumber = 0)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.SetCornerUV(base.Pointer, cornerNo, ref newUV, uvNumber);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00007F97 File Offset: 0x00006197
		public void ReserveVertices(int count)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ReserveVertices(base.Pointer, count);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00007FAA File Offset: 0x000061AA
		public void ReserveFaceCorners(int count)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ReserveFaceCorners(base.Pointer, count);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00007FBD File Offset: 0x000061BD
		public void ReserveFaces(int count)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ReserveFaces(base.Pointer, count);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00007FD0 File Offset: 0x000061D0
		public int RemoveDuplicatedCorners()
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.RemoveDuplicatedCorners(base.Pointer);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00007FE2 File Offset: 0x000061E2
		public void TransformVerticesToParent(MatrixFrame frame)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.TransformVerticesToParent(base.Pointer, ref frame);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00007FF6 File Offset: 0x000061F6
		public void TransformVerticesToLocal(MatrixFrame frame)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.TransformVerticesToLocal(base.Pointer, ref frame);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0000800A File Offset: 0x0000620A
		public void SetVertexColor(Vec3 color)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.SetVertexColor(base.Pointer, ref color);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0000801E File Offset: 0x0000621E
		public Vec3 GetVertexColor(int faceCornerIndex)
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.GetVertexColor(base.Pointer, faceCornerIndex);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00008031 File Offset: 0x00006231
		public void SetVertexColorAlpha(float newAlpha)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.SetVertexColorAlpha(base.Pointer, newAlpha);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00008044 File Offset: 0x00006244
		public float GetVertexColorAlpha()
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.GetVertexColorAlpha(base.Pointer);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00008056 File Offset: 0x00006256
		public void EnsureTransformedVertices()
		{
			EngineApplicationInterface.IManagedMeshEditOperations.EnsureTransformedVertices(base.Pointer);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00008068 File Offset: 0x00006268
		public void ApplyCPUSkinning(Skeleton skeleton)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ApplyCPUSkinning(base.Pointer, skeleton.Pointer);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00008080 File Offset: 0x00006280
		public void UpdateOverlappedVertexNormals(Mesh attachedToMesh, MatrixFrame attachFrame, float mergeRadiusSQ = 0.0025f)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.UpdateOverlappedVertexNormals(base.Pointer, attachedToMesh.Pointer, ref attachFrame, mergeRadiusSQ);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0000809B File Offset: 0x0000629B
		public void ClearAll()
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ClearAll(base.Pointer);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000080AD File Offset: 0x000062AD
		public void SetTangentsOfFaceCorner(int faceCornerIndex, Vec3 tangent, Vec3 binormal)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.SetTangentsOfFaceCorner(base.Pointer, faceCornerIndex, ref tangent, ref binormal);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x000080C4 File Offset: 0x000062C4
		public void SetPositionOfVertex(int vertexIndex, Vec3 position)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.SetPositionOfVertex(base.Pointer, vertexIndex, ref position);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x000080D9 File Offset: 0x000062D9
		public Vec3 GetPositionOfVertex(int vertexIndex)
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.GetPositionOfVertex(base.Pointer, vertexIndex);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x000080EC File Offset: 0x000062EC
		public void RemoveFace(int faceIndex)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.RemoveFace(base.Pointer, faceIndex);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000080FF File Offset: 0x000062FF
		public void FinalizeEditing()
		{
			EngineApplicationInterface.IManagedMeshEditOperations.FinalizeEditing(base.Pointer);
		}
	}
}
