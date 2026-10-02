using System;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade.Launcher.Library.UserDatas
{
	// Token: 0x0200001B RID: 27
	public class UserModData
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005CAB File Offset: 0x00003EAB
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00005CB3 File Offset: 0x00003EB3
		public string Id { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005CBC File Offset: 0x00003EBC
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00005CC4 File Offset: 0x00003EC4
		public string LastKnownVersion { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00005CCD File Offset: 0x00003ECD
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00005CD5 File Offset: 0x00003ED5
		public bool IsSelected { get; set; }

		// Token: 0x06000122 RID: 290 RVA: 0x00005CDE File Offset: 0x00003EDE
		public UserModData()
		{
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005CE6 File Offset: 0x00003EE6
		public UserModData(string id, string lastKnownVersion, bool isSelected)
		{
			this.Id = id;
			this.LastKnownVersion = lastKnownVersion;
			this.IsSelected = isSelected;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005D04 File Offset: 0x00003F04
		public bool IsUpdatedToBeDefault(ModuleInfo module)
		{
			return this.LastKnownVersion != module.Version.ToString() && module.IsDefault;
		}
	}
}
