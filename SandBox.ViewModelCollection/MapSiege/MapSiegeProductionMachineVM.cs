using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.MapSiege
{
	// Token: 0x02000054 RID: 84
	public class MapSiegeProductionMachineVM : ViewModel
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x000140CF File Offset: 0x000122CF
		public SiegeEngineType Engine { get; }

		// Token: 0x06000547 RID: 1351 RVA: 0x000140D7 File Offset: 0x000122D7
		public MapSiegeProductionMachineVM(SiegeEngineType engineType, int number, Action<MapSiegeProductionMachineVM> onSelection)
		{
			this._onSelection = onSelection;
			this.Engine = engineType;
			this.NumberOfMachines = number;
			this.MachineID = engineType.StringId;
			this.IsReserveOption = false;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00014107 File Offset: 0x00012307
		public MapSiegeProductionMachineVM(Action<MapSiegeProductionMachineVM> onSelection, bool isCancel)
		{
			this._onSelection = onSelection;
			this.Engine = null;
			this.NumberOfMachines = 0;
			this.MachineID = "reserve";
			this.IsReserveOption = true;
			this._isCancel = isCancel;
			this.RefreshValues();
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00014143 File Offset: 0x00012343
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ActionText = (this._isCancel ? GameTexts.FindText("str_cancel", null).ToString() : GameTexts.FindText("str_siege_move_to_reserve", null).ToString());
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001417B File Offset: 0x0001237B
		public void OnSelection()
		{
			this._onSelection(this);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00014189 File Offset: 0x00012389
		public void ExecuteShowTooltip()
		{
			if (this.Engine != null)
			{
				InformationManager.ShowTooltip(typeof(List<TooltipProperty>), new object[] { SandBoxUIHelper.GetSiegeEngineTooltip(this.Engine) });
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000141B6 File Offset: 0x000123B6
		public void ExecuteHideTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x000141BD File Offset: 0x000123BD
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x000141C5 File Offset: 0x000123C5
		[DataSourceProperty]
		public int MachineType
		{
			get
			{
				return this._machineType;
			}
			set
			{
				if (value != this._machineType)
				{
					this._machineType = value;
					base.OnPropertyChangedWithValue(value, "MachineType");
				}
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x000141E3 File Offset: 0x000123E3
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x000141EB File Offset: 0x000123EB
		[DataSourceProperty]
		public string MachineID
		{
			get
			{
				return this._machineID;
			}
			set
			{
				if (value != this._machineID)
				{
					this._machineID = value;
					base.OnPropertyChangedWithValue<string>(value, "MachineID");
				}
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0001420E File Offset: 0x0001240E
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x00014216 File Offset: 0x00012416
		[DataSourceProperty]
		public int NumberOfMachines
		{
			get
			{
				return this._numberOfMachines;
			}
			set
			{
				if (value != this._numberOfMachines)
				{
					this._numberOfMachines = value;
					base.OnPropertyChangedWithValue(value, "NumberOfMachines");
				}
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00014234 File Offset: 0x00012434
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0001423C File Offset: 0x0001243C
		[DataSourceProperty]
		public string ActionText
		{
			get
			{
				return this._actionText;
			}
			set
			{
				if (value != this._actionText)
				{
					this._actionText = value;
					base.OnPropertyChangedWithValue<string>(value, "ActionText");
				}
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0001425F File Offset: 0x0001245F
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x00014267 File Offset: 0x00012467
		[DataSourceProperty]
		public bool IsReserveOption
		{
			get
			{
				return this._isReserveOption;
			}
			set
			{
				if (value != this._isReserveOption)
				{
					this._isReserveOption = value;
					base.OnPropertyChangedWithValue(value, "IsReserveOption");
				}
			}
		}

		// Token: 0x0400029D RID: 669
		private Action<MapSiegeProductionMachineVM> _onSelection;

		// Token: 0x0400029F RID: 671
		private bool _isCancel;

		// Token: 0x040002A0 RID: 672
		private int _machineType;

		// Token: 0x040002A1 RID: 673
		private int _numberOfMachines;

		// Token: 0x040002A2 RID: 674
		private string _machineID;

		// Token: 0x040002A3 RID: 675
		private bool _isReserveOption;

		// Token: 0x040002A4 RID: 676
		private string _actionText;
	}
}
