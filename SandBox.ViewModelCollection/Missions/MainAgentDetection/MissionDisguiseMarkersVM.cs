using System;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Missions.MainAgentDetection
{
	// Token: 0x02000043 RID: 67
	public class MissionDisguiseMarkersVM : ViewModel
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x000118D8 File Offset: 0x0000FAD8
		public MissionDisguiseMarkersVM()
		{
			this.HostileAgents = new MBBindingList<MissionDisguiseMarkerItemVM>();
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x000118EB File Offset: 0x0000FAEB
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x000118F3 File Offset: 0x0000FAF3
		[DataSourceProperty]
		public MissionDisguiseMarkerItemVM TargetAgent
		{
			get
			{
				return this._targetAgent;
			}
			set
			{
				if (value != this._targetAgent)
				{
					this._targetAgent = value;
					base.OnPropertyChangedWithValue<MissionDisguiseMarkerItemVM>(value, "TargetAgent");
				}
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00011911 File Offset: 0x0000FB11
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00011919 File Offset: 0x0000FB19
		[DataSourceProperty]
		public MBBindingList<MissionDisguiseMarkerItemVM> HostileAgents
		{
			get
			{
				return this._hostileAgents;
			}
			set
			{
				if (value != this._hostileAgents)
				{
					this._hostileAgents = value;
					base.OnPropertyChangedWithValue<MBBindingList<MissionDisguiseMarkerItemVM>>(value, "HostileAgents");
				}
			}
		}

		// Token: 0x04000238 RID: 568
		private MissionDisguiseMarkerItemVM _targetAgent;

		// Token: 0x04000239 RID: 569
		private MBBindingList<MissionDisguiseMarkerItemVM> _hostileAgents;
	}
}
