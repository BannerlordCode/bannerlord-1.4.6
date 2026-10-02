using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000136 RID: 310
	[Serializable]
	public class ModuleInfoModel
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0000C2B7 File Offset: 0x0000A4B7
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x0000C2BF File Offset: 0x0000A4BF
		public string Id { get; private set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0000C2C8 File Offset: 0x0000A4C8
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		public string Name { get; private set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0000C2D9 File Offset: 0x0000A4D9
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x0000C2E1 File Offset: 0x0000A4E1
		public ModuleCategory Category { get; private set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x0000C2EA File Offset: 0x0000A4EA
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x0000C2F2 File Offset: 0x0000A4F2
		public string Version { get; private set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0000C2FB File Offset: 0x0000A4FB
		[JsonIgnore]
		public bool IsOptional
		{
			get
			{
				return this.Category == ModuleCategory.MultiplayerOptional;
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0000C306 File Offset: 0x0000A506
		[JsonConstructor]
		private ModuleInfoModel(string id, string name, string version, ModuleCategory category)
		{
			this.Id = id;
			this.Name = name;
			this.Version = version;
			this.Category = category;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0000C32C File Offset: 0x0000A52C
		internal ModuleInfoModel(ModuleInfo moduleInfo)
			: this(moduleInfo.Id, moduleInfo.Name, moduleInfo.Version.ToString(), moduleInfo.Category)
		{
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0000C365 File Offset: 0x0000A565
		public static bool ShouldIncludeInSession(ModuleInfo moduleInfo)
		{
			return !moduleInfo.IsOfficial && moduleInfo.HasMultiplayerCategory;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0000C377 File Offset: 0x0000A577
		public static bool TryCreateForSession(ModuleInfo moduleInfo, out ModuleInfoModel moduleInfoModel)
		{
			if (ModuleInfoModel.ShouldIncludeInSession(moduleInfo))
			{
				moduleInfoModel = new ModuleInfoModel(moduleInfo);
				return true;
			}
			moduleInfoModel = null;
			return false;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0000C390 File Offset: 0x0000A590
		public override bool Equals(object obj)
		{
			ModuleInfoModel moduleInfoModel;
			return (moduleInfoModel = obj as ModuleInfoModel) != null && this.Id == moduleInfoModel.Id && this.Version == moduleInfoModel.Version;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0000C3CD File Offset: 0x0000A5CD
		public override int GetHashCode()
		{
			return (-612338121 * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Id)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Version);
		}
	}
}
