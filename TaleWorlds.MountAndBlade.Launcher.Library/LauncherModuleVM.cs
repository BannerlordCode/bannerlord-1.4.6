using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x02000014 RID: 20
	public class LauncherModuleVM : ViewModel
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00004704 File Offset: 0x00002904
		public LauncherModuleVM(ModuleInfo moduleInfo, Action<LauncherModuleVM, int, string> onChangeLoadingOrder, Action<LauncherModuleVM> onSelect, Func<ModuleInfo, bool> areAllDependenciesPresent, Func<SubModuleInfo, LauncherDLLData> queryIsSubmoduleDangerous)
		{
			this.Info = moduleInfo;
			this._onSelect = onSelect;
			this._onChangeLoadingOrder = onChangeLoadingOrder;
			this._querySubmoduleVerifyData = queryIsSubmoduleDangerous;
			this._areAllDependenciesPresent = areAllDependenciesPresent;
			this.SubModules = new MBBindingList<LauncherSubModule>();
			this.IsOfficial = this.Info.IsOfficial;
			this.VersionText = this.Info.Version.ToString();
			this.Name = LauncherModuleVM.ProcessModuleName(moduleInfo.Name);
			string text = string.Empty;
			if (moduleInfo.DependedModules.Count > 0)
			{
				text += "Depends on: \n";
				foreach (DependedModule dependedModule in moduleInfo.DependedModules)
				{
					text = text + dependedModule.ModuleId + (dependedModule.IsOptional ? " (optional)" : "") + "\n";
				}
				this.AnyDependencyAvailable = true;
			}
			if (moduleInfo.IncompatibleModules.Count > 0)
			{
				if (this.AnyDependencyAvailable)
				{
					text += "\n----\n";
				}
				text += "Incompatible with: \n";
				foreach (DependedModule dependedModule2 in moduleInfo.IncompatibleModules)
				{
					text = text + dependedModule2.ModuleId + "\n";
				}
				this.AnyDependencyAvailable = true;
			}
			if (moduleInfo.ModulesToLoadAfterThis.Count > 0)
			{
				if (this.AnyDependencyAvailable)
				{
					text += "\n----\n";
				}
				text += "Needs to load before: \n";
				foreach (DependedModule dependedModule3 in moduleInfo.ModulesToLoadAfterThis)
				{
					text = text + dependedModule3.ModuleId + "\n";
				}
				this.AnyDependencyAvailable = true;
			}
			this.DependencyHint = new LauncherHintVM(text);
			this.UpdateIsDisabled();
			bool flag = !moduleInfo.SubModules.Any<SubModuleInfo>(delegate(SubModuleInfo s)
			{
				LauncherDLLData launcherDLLData2 = this._querySubmoduleVerifyData(s);
				return launcherDLLData2 != null && launcherDLLData2.Size == 0U;
			});
			string text2 = "";
			if (flag)
			{
				text2 = "Dangerous code detected.\n\nTaleWorlds is not responsible for consequences arising from running unverified/unofficial code.";
				using (List<SubModuleInfo>.Enumerator enumerator2 = moduleInfo.SubModules.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						SubModuleInfo subModuleInfo = enumerator2.Current;
						this.SubModules.Add(new LauncherSubModule(subModuleInfo));
						LauncherDLLData launcherDLLData = this._querySubmoduleVerifyData(subModuleInfo);
						if (launcherDLLData != null)
						{
							this.IsDangerous = this.IsDangerous || launcherDLLData.IsDangerous;
						}
					}
					goto IL_029B;
				}
			}
			this.IsDangerous = true;
			text2 = "Couldn't verify some or all of the code included in this module.\n\nTaleWorlds is not responsible for consequences arising from running unverified/unofficial code.";
			IL_029B:
			this.DangerousHint = new LauncherHintVM(text2);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000049EC File Offset: 0x00002BEC
		private static string ProcessModuleName(string originalModuleName)
		{
			if (originalModuleName == "NavalDLC")
			{
				return "War Sails";
			}
			return originalModuleName;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004A04 File Offset: 0x00002C04
		private void UpdateIsDisabled()
		{
			this.IsDisabled = !Debugger.IsAttached && ((this.Info.IsRequiredOfficial && this.Info.IsSelected) || !this._areAllDependenciesPresent(this.Info));
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004A52 File Offset: 0x00002C52
		private void ExecuteSelect()
		{
			this._onSelect(this);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004A60 File Offset: 0x00002C60
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00004A68 File Offset: 0x00002C68
		[DataSourceProperty]
		public MBBindingList<LauncherSubModule> SubModules
		{
			get
			{
				return this._subModules;
			}
			set
			{
				if (value != this._subModules)
				{
					this._subModules = value;
					base.OnPropertyChangedWithValue<MBBindingList<LauncherSubModule>>(value, "SubModules");
				}
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004A86 File Offset: 0x00002C86
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004A8E File Offset: 0x00002C8E
		[DataSourceProperty]
		public LauncherHintVM DangerousHint
		{
			get
			{
				return this._dangerousHint;
			}
			set
			{
				if (value != this._dangerousHint)
				{
					this._dangerousHint = value;
					base.OnPropertyChangedWithValue<LauncherHintVM>(value, "DangerousHint");
				}
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004AAC File Offset: 0x00002CAC
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00004AB4 File Offset: 0x00002CB4
		[DataSourceProperty]
		public LauncherHintVM DependencyHint
		{
			get
			{
				return this._dependencyHint;
			}
			set
			{
				if (value != this._dependencyHint)
				{
					this._dependencyHint = value;
					base.OnPropertyChangedWithValue<LauncherHintVM>(value, "DependencyHint");
				}
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004AD2 File Offset: 0x00002CD2
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00004ADA File Offset: 0x00002CDA
		[DataSourceProperty]
		public string VersionText
		{
			get
			{
				return this._versionText;
			}
			set
			{
				if (value != this._versionText)
				{
					this._versionText = value;
					base.OnPropertyChangedWithValue<string>(value, "VersionText");
				}
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004AFD File Offset: 0x00002CFD
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00004B05 File Offset: 0x00002D05
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004B28 File Offset: 0x00002D28
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00004B30 File Offset: 0x00002D30
		[DataSourceProperty]
		public bool IsDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				if (value != this._isDisabled)
				{
					this._isDisabled = value;
					base.OnPropertyChangedWithValue(value, "IsDisabled");
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004B4E File Offset: 0x00002D4E
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00004B56 File Offset: 0x00002D56
		[DataSourceProperty]
		public bool AnyDependencyAvailable
		{
			get
			{
				return this._anyDependencyAvailable;
			}
			set
			{
				if (value != this._anyDependencyAvailable)
				{
					this._anyDependencyAvailable = value;
					base.OnPropertyChangedWithValue(value, "AnyDependencyAvailable");
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004B74 File Offset: 0x00002D74
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00004B7C File Offset: 0x00002D7C
		[DataSourceProperty]
		public bool IsDangerous
		{
			get
			{
				return this._isDangerous;
			}
			set
			{
				if (value != this._isDangerous)
				{
					this._isDangerous = value;
					base.OnPropertyChangedWithValue(value, "IsDangerous");
				}
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004B9A File Offset: 0x00002D9A
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00004BA2 File Offset: 0x00002DA2
		[DataSourceProperty]
		public bool IsOfficial
		{
			get
			{
				return this._isOfficial;
			}
			set
			{
				if (value != this._isOfficial)
				{
					this._isOfficial = value;
					base.OnPropertyChangedWithValue(value, "IsOfficial");
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004BC0 File Offset: 0x00002DC0
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00004BCD File Offset: 0x00002DCD
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this.Info.IsSelected;
			}
			set
			{
				if (!value && this.Info.IsNative)
				{
					return;
				}
				if (value != this.Info.IsSelected)
				{
					this.UpdateIsDisabled();
					this.Info.IsSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x04000055 RID: 85
		public readonly ModuleInfo Info;

		// Token: 0x04000056 RID: 86
		private readonly Action<LauncherModuleVM, int, string> _onChangeLoadingOrder;

		// Token: 0x04000057 RID: 87
		private readonly Action<LauncherModuleVM> _onSelect;

		// Token: 0x04000058 RID: 88
		private readonly Func<SubModuleInfo, LauncherDLLData> _querySubmoduleVerifyData;

		// Token: 0x04000059 RID: 89
		private readonly Func<ModuleInfo, bool> _areAllDependenciesPresent;

		// Token: 0x0400005A RID: 90
		private MBBindingList<LauncherSubModule> _subModules;

		// Token: 0x0400005B RID: 91
		private LauncherHintVM _dangerousHint;

		// Token: 0x0400005C RID: 92
		private LauncherHintVM _dependencyHint;

		// Token: 0x0400005D RID: 93
		private string _name;

		// Token: 0x0400005E RID: 94
		private string _versionText;

		// Token: 0x0400005F RID: 95
		private bool _isDisabled;

		// Token: 0x04000060 RID: 96
		private bool _isDangerous;

		// Token: 0x04000061 RID: 97
		private bool _isOfficial;

		// Token: 0x04000062 RID: 98
		private bool _anyDependencyAvailable;
	}
}
