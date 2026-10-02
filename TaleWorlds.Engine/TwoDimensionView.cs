using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000096 RID: 150
	[EngineClass("rglTwo_dimension_view")]
	public sealed class TwoDimensionView : View
	{
		// Token: 0x06000D22 RID: 3362 RVA: 0x0000EB38 File Offset: 0x0000CD38
		internal TwoDimensionView(UIntPtr pointer)
			: base(pointer)
		{
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0000EB41 File Offset: 0x0000CD41
		public static TwoDimensionView CreateTwoDimension(string viewName)
		{
			return EngineApplicationInterface.ITwoDimensionView.CreateTwoDimensionView(viewName);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0000EB4E File Offset: 0x0000CD4E
		public void BeginFrame()
		{
			EngineApplicationInterface.ITwoDimensionView.BeginFrame(base.Pointer);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0000EB60 File Offset: 0x0000CD60
		public void EndFrame()
		{
			EngineApplicationInterface.ITwoDimensionView.EndFrame(base.Pointer);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0000EB72 File Offset: 0x0000CD72
		public void Clear()
		{
			EngineApplicationInterface.ITwoDimensionView.Clear(base.Pointer);
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0000EB84 File Offset: 0x0000CD84
		public void CreateMeshFromDescription(WeakMaterial material, TwoDimensionMeshDrawData meshDrawData)
		{
			EngineApplicationInterface.ITwoDimensionView.AddNewMesh(base.Pointer, material.Pointer, ref meshDrawData);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0000EB9F File Offset: 0x0000CD9F
		public bool CreateTextMeshFromCache(Material material, TwoDimensionTextMeshDrawData meshDrawData)
		{
			return EngineApplicationInterface.ITwoDimensionView.AddCachedTextMesh(base.Pointer, material.Pointer, ref meshDrawData);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0000EBBC File Offset: 0x0000CDBC
		public void CreateTextMeshFromDescription(float[] vertices, float[] uvs, uint[] indices, int indexCount, Material material, TwoDimensionTextMeshDrawData meshDrawData)
		{
			EngineApplicationInterface.ITwoDimensionView.AddNewTextMesh(base.Pointer, vertices, uvs, indices, vertices.Length / 2, indexCount, material.Pointer, ref meshDrawData);
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0000EBEC File Offset: 0x0000CDEC
		public WeakMaterial GetOrCreateMaterial(Texture mainTexture, Texture overlayTexture)
		{
			return new WeakMaterial(EngineApplicationInterface.ITwoDimensionView.GetOrCreateMaterial(base.Pointer, (mainTexture != null) ? mainTexture.Pointer : UIntPtr.Zero, (overlayTexture != null) ? overlayTexture.Pointer : UIntPtr.Zero));
		}
	}
}
