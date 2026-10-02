using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200009E RID: 158
	public struct WorldFrame
	{
		// Token: 0x06000EF6 RID: 3830 RVA: 0x00011608 File Offset: 0x0000F808
		public WorldFrame(Mat3 rotation, WorldPosition origin)
		{
			this.Rotation = rotation;
			this.Origin = origin;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00011618 File Offset: 0x0000F818
		public bool IsValid
		{
			get
			{
				return this.Origin.IsValid;
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00011628 File Offset: 0x0000F828
		public MatrixFrame ToGroundMatrixFrame()
		{
			Vec3 groundVec = this.Origin.GetGroundVec3();
			return new MatrixFrame(in this.Rotation, in groundVec);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00011650 File Offset: 0x0000F850
		public MatrixFrame ToGroundMatrixFrameMT()
		{
			Vec3 groundVec3MT = this.Origin.GetGroundVec3MT();
			return new MatrixFrame(in this.Rotation, in groundVec3MT);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00011678 File Offset: 0x0000F878
		public MatrixFrame ToNavMeshMatrixFrame()
		{
			Vec3 navMeshVec = this.Origin.GetNavMeshVec3();
			return new MatrixFrame(in this.Rotation, in navMeshVec);
		}

		// Token: 0x04000207 RID: 519
		public Mat3 Rotation;

		// Token: 0x04000208 RID: 520
		public WorldPosition Origin;

		// Token: 0x04000209 RID: 521
		public static readonly WorldFrame Invalid = new WorldFrame(Mat3.Identity, WorldPosition.Invalid);
	}
}
