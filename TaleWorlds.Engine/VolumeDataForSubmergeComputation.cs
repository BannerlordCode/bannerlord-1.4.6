using System;
using System.Runtime.InteropServices;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200004D RID: 77
	[EngineStruct("rglWater_renderer::Volume_data_for_submerge_computation", false, null)]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct VolumeDataForSubmergeComputation
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00005CA1 File Offset: 0x00003EA1
		public float Height
		{
			get
			{
				return this.LocalScale[(int)this.DynamicUpAxis];
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00005CB4 File Offset: 0x00003EB4
		public float Width
		{
			get
			{
				return this.LocalScale[(int)((this.DynamicUpAxis + 1) % (FloaterVolumeDynamicUpAxis)3)];
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00005CCB File Offset: 0x00003ECB
		public float Depth
		{
			get
			{
				return this.LocalScale[(int)((this.DynamicUpAxis + 2) % (FloaterVolumeDynamicUpAxis)3)];
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00005CE2 File Offset: 0x00003EE2
		public Vec3 Up
		{
			get
			{
				return this.LocalFrame.rotation[(int)this.DynamicUpAxis];
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00005CFA File Offset: 0x00003EFA
		public Vec3 Side
		{
			get
			{
				return this.LocalFrame.rotation[(int)((this.DynamicUpAxis + 1) % (FloaterVolumeDynamicUpAxis)3)];
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00005D16 File Offset: 0x00003F16
		public Vec3 Forward
		{
			get
			{
				return this.LocalFrame.rotation[(int)((this.DynamicUpAxis + 2) % (FloaterVolumeDynamicUpAxis)3)];
			}
		}

		// Token: 0x040000AC RID: 172
		public Vec3 DynamicLocalBottomPos;

		// Token: 0x040000AD RID: 173
		public MatrixFrame LocalFrame;

		// Token: 0x040000AE RID: 174
		public Vec3 LocalScale;

		// Token: 0x040000AF RID: 175
		public FloaterVolumeDynamicUpAxis DynamicUpAxis;

		// Token: 0x040000B0 RID: 176
		public Vec3 OutGlobalWaterSurfaceNormal;

		// Token: 0x040000B1 RID: 177
		public float InOutWaterHeightWrtVolume;
	}
}
