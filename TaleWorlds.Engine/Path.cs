using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000074 RID: 116
	[EngineClass("rglPath")]
	public sealed class Path : NativeObject
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0000AB23 File Offset: 0x00008D23
		public int NumberOfPoints
		{
			get
			{
				return EngineApplicationInterface.IPath.GetNumberOfPoints(base.Pointer);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0000AB35 File Offset: 0x00008D35
		public float TotalDistance
		{
			get
			{
				return EngineApplicationInterface.IPath.GetTotalLength(base.Pointer);
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0000AB47 File Offset: 0x00008D47
		internal Path(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0000AB58 File Offset: 0x00008D58
		public MatrixFrame GetHermiteFrameForDt(float phase, int first_point)
		{
			MatrixFrame identity = MatrixFrame.Identity;
			EngineApplicationInterface.IPath.GetHermiteFrameForDt(base.Pointer, ref identity, phase, first_point);
			return identity;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0000AB80 File Offset: 0x00008D80
		public MatrixFrame GetFrameForDistance(float distance)
		{
			MatrixFrame identity = MatrixFrame.Identity;
			EngineApplicationInterface.IPath.GetHermiteFrameForDistance(base.Pointer, ref identity, distance);
			return identity;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0000ABA8 File Offset: 0x00008DA8
		public MatrixFrame GetNearestFrameWithValidAlphaForDistance(float distance, bool searchForward = true, float alphaThreshold = 0.5f)
		{
			MatrixFrame identity = MatrixFrame.Identity;
			EngineApplicationInterface.IPath.GetNearestHermiteFrameWithValidAlphaForDistance(base.Pointer, ref identity, distance, searchForward, alphaThreshold);
			return identity;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0000ABD1 File Offset: 0x00008DD1
		public void GetFrameAndColorForDistance(float distance, out MatrixFrame frame, out Vec3 color)
		{
			frame = MatrixFrame.Identity;
			EngineApplicationInterface.IPath.GetHermiteFrameAndColorForDistance(base.Pointer, out frame, out color, distance);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0000ABF1 File Offset: 0x00008DF1
		public float GetArcLength(int first_point)
		{
			return EngineApplicationInterface.IPath.GetArcLength(base.Pointer, first_point);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0000AC04 File Offset: 0x00008E04
		public void GetPoints(MatrixFrame[] points)
		{
			EngineApplicationInterface.IPath.GetPoints(base.Pointer, points);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0000AC17 File Offset: 0x00008E17
		public float GetTotalLength()
		{
			return EngineApplicationInterface.IPath.GetTotalLength(base.Pointer);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0000AC29 File Offset: 0x00008E29
		public int GetVersion()
		{
			return EngineApplicationInterface.IPath.GetVersion(base.Pointer);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0000AC3B File Offset: 0x00008E3B
		public void SetFrameOfPoint(int pointIndex, ref MatrixFrame frame)
		{
			EngineApplicationInterface.IPath.SetFrameOfPoint(base.Pointer, pointIndex, ref frame);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0000AC4F File Offset: 0x00008E4F
		public void SetTangentPositionOfPoint(int pointIndex, int tangentIndex, ref Vec3 position)
		{
			EngineApplicationInterface.IPath.SetTangentPositionOfPoint(base.Pointer, pointIndex, tangentIndex, ref position);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0000AC64 File Offset: 0x00008E64
		public int AddPathPoint(int newNodeIndex)
		{
			return EngineApplicationInterface.IPath.AddPathPoint(base.Pointer, newNodeIndex);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0000AC77 File Offset: 0x00008E77
		public void DeletePathPoint(int nodeIndex)
		{
			EngineApplicationInterface.IPath.DeletePathPoint(base.Pointer, nodeIndex);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0000AC8A File Offset: 0x00008E8A
		public bool HasValidAlphaAtPathPoint(int nodeIndex, float alphaThreshold = 0.5f)
		{
			return EngineApplicationInterface.IPath.HasValidAlphaAtPathPoint(base.Pointer, nodeIndex, alphaThreshold);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0000AC9E File Offset: 0x00008E9E
		public string GetName()
		{
			return EngineApplicationInterface.IPath.GetName(base.Pointer);
		}
	}
}
