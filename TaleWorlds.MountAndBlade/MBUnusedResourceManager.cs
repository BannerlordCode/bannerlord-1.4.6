using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001E1 RID: 481
	public class MBUnusedResourceManager
	{
		// Token: 0x06001C53 RID: 7251 RVA: 0x0006116B File Offset: 0x0005F36B
		public static void SetMeshUsed(string meshName)
		{
			MBAPI.IMBWorld.SetMeshUsed(meshName);
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00061178 File Offset: 0x0005F378
		public static void SetMaterialUsed(string meshName)
		{
			MBAPI.IMBWorld.SetMaterialUsed(meshName);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x00061185 File Offset: 0x0005F385
		public static void SetBodyUsed(string bodyName)
		{
			MBAPI.IMBWorld.SetBodyUsed(bodyName);
		}
	}
}
