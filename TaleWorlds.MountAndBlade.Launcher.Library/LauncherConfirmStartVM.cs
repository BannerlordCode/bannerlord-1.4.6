using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x0200000B RID: 11
	public class LauncherConfirmStartVM : ViewModel
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00003250 File Offset: 0x00001450
		public LauncherConfirmStartVM(Action onConfirm)
		{
			this._onConfirm = onConfirm;
			this.Title = "CAUTION";
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000326C File Offset: 0x0000146C
		public void EnableWith(List<SubModuleInfo> unverifiedSubModules, List<DependentVersionMissmatchItem> missmatchedDependentModules)
		{
			this.IsEnabled = true;
			this.Description = string.Empty;
			if (unverifiedSubModules.Count > 0)
			{
				this.Description += "You're loading unverified code from: \n";
				for (int i = 0; i < unverifiedSubModules.Count; i++)
				{
					this.Description += unverifiedSubModules[i].Name;
					if (i == unverifiedSubModules.Count - 1)
					{
						this.Description += "\n";
					}
					else
					{
						this.Description += ", ";
					}
				}
				this.Description += "\n";
			}
			if (missmatchedDependentModules.Count > 0)
			{
				for (int j = 0; j < missmatchedDependentModules.Count; j++)
				{
					for (int k = 0; k < missmatchedDependentModules[j].MissmatchedDependencies.Count; k++)
					{
						string missmatchedModuleId = missmatchedDependentModules[j].MissmatchedModuleId;
						string moduleId = missmatchedDependentModules[j].MissmatchedDependencies[k].Item1.ModuleId;
						string text = missmatchedDependentModules[j].MissmatchedDependencies[k].Item1.Version.ToString();
						string text2 = missmatchedDependentModules[j].MissmatchedDependencies[k].Item2.ToString();
						this.Description = string.Concat(new string[]
						{
							this.Description, missmatchedModuleId, " depends on ", moduleId, "(", text, "), current version is ", moduleId, "(", text2,
							")\n"
						});
					}
				}
				this.Description += "\n";
			}
			this.Description += "TaleWorlds is not responsible for an unstable experience if it occurs.\n";
			this.Description += "Are you sure?";
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000034AA File Offset: 0x000016AA
		private void ExecuteConfirm()
		{
			Action onConfirm = this._onConfirm;
			if (onConfirm != null)
			{
				onConfirm();
			}
			this.IsEnabled = false;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000034C4 File Offset: 0x000016C4
		private void ExecuteCancel()
		{
			this.IsEnabled = false;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000034CD File Offset: 0x000016CD
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000034D5 File Offset: 0x000016D5
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (this._isEnabled != value)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000034F3 File Offset: 0x000016F3
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000034FB File Offset: 0x000016FB
		[DataSourceProperty]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (this._description != value)
				{
					this._description = value;
					base.OnPropertyChangedWithValue<string>(value, "Description");
				}
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000351E File Offset: 0x0000171E
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003526 File Offset: 0x00001726
		[DataSourceProperty]
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				if (this._title != value)
				{
					this._title = value;
					base.OnPropertyChangedWithValue<string>(value, "Title");
				}
			}
		}

		// Token: 0x04000038 RID: 56
		private readonly Action _onConfirm;

		// Token: 0x04000039 RID: 57
		private bool _isEnabled;

		// Token: 0x0400003A RID: 58
		private string _description;

		// Token: 0x0400003B RID: 59
		private string _title;
	}
}
