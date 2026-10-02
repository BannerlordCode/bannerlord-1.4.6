using System;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000206 RID: 518
	public class ConsolesModuleExtension : IPlatformModuleExtension
	{
		// Token: 0x06001E10 RID: 7696 RVA: 0x00067A34 File Offset: 0x00065C34
		public ConsolesModuleExtension()
		{
			this._modulePaths = new List<string>();
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x00067A48 File Offset: 0x00065C48
		public void Initialize(List<string> args)
		{
			string platformModulePaths = Utilities.GetPlatformModulePaths();
			Debug.Print("ConsolesModuleExtension::Initialize::" + platformModulePaths + "\n", 0, Debug.DebugColor.White, 17592186044416UL);
			if (platformModulePaths.Length > 0)
			{
				this._modulePaths = new List<string>(platformModulePaths.Split(new char[] { '$' }));
				return;
			}
			this._modulePaths = new List<string>();
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x00067AAD File Offset: 0x00065CAD
		public string[] GetModulePaths()
		{
			return this._modulePaths.ToArray();
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x00067ABA File Offset: 0x00065CBA
		public void Destroy()
		{
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x00067ABC File Offset: 0x00065CBC
		public void SetLauncherMode(bool isLauncherModeActive)
		{
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x00067ABE File Offset: 0x00065CBE
		public bool CheckEntitlement(string title)
		{
			return true;
		}

		// Token: 0x04000A45 RID: 2629
		private List<string> _modulePaths;
	}
}
