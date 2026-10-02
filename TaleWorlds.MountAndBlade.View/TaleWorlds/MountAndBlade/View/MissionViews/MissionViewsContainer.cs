using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.View.MissionViews
{
	// Token: 0x02000082 RID: 130
	public class MissionViewsContainer
	{
		// Token: 0x060004E8 RID: 1256 RVA: 0x00024A64 File Offset: 0x00022C64
		public MissionViewsContainer()
		{
			this._missionViews = new List<MissionView>();
			this._missionViewsCopy = this._missionViews.ToList<MissionView>();
			this._missionViewsCopiedFrame = Utilities.EngineFrameNo;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00024A9A File Offset: 0x00022C9A
		public void Add(MissionView missionView)
		{
			this._missionViews.Add(missionView);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00024AA8 File Offset: 0x00022CA8
		public void Remove(MissionView missionView)
		{
			this._missionViews.Remove(missionView);
			missionView.IsFinalized = true;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00024ABE File Offset: 0x00022CBE
		public bool Contains(MissionView missionView)
		{
			return this._missionViews.Contains(missionView);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00024ACC File Offset: 0x00022CCC
		public void ForEach(Action<MissionView> action)
		{
			foreach (MissionView missionView in this.GetMissionViewsCopy())
			{
				if (!missionView.IsFinalized)
				{
					action(missionView);
				}
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00024B28 File Offset: 0x00022D28
		private List<MissionView> GetMissionViewsCopy()
		{
			int engineFrameNo = Utilities.EngineFrameNo;
			if (this._missionViewsCopiedFrame != engineFrameNo)
			{
				this._missionViewsCopy = this._missionViews.ToList<MissionView>();
				this._missionViewsCopiedFrame = engineFrameNo;
			}
			return this._missionViewsCopy;
		}

		// Token: 0x040002C5 RID: 709
		private List<MissionView> _missionViews;

		// Token: 0x040002C6 RID: 710
		private List<MissionView> _missionViewsCopy;

		// Token: 0x040002C7 RID: 711
		private int _missionViewsCopiedFrame = -1;
	}
}
