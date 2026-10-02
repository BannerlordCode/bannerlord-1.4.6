using System;
using System.Collections.Generic;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Missions.Objectives
{
	// Token: 0x020003E3 RID: 995
	internal class GenericMissionObjective : MissionObjective
	{
		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060036C3 RID: 14019 RVA: 0x000E3002 File Offset: 0x000E1202
		public override string UniqueId
		{
			get
			{
				return this.IUniqueId;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060036C4 RID: 14020 RVA: 0x000E300A File Offset: 0x000E120A
		public override TextObject Name
		{
			get
			{
				return this.IName;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060036C5 RID: 14021 RVA: 0x000E3012 File Offset: 0x000E1212
		public override TextObject Description
		{
			get
			{
				return this.IDescription;
			}
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000E301A File Offset: 0x000E121A
		public GenericMissionObjective(Mission mission, string id, TextObject name, TextObject description)
			: base(mission)
		{
			this._targets = new List<MissionObjectiveTarget>();
			this.IUniqueId = id;
			this.IName = name;
			this.IDescription = description;
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x000E3044 File Offset: 0x000E1244
		public override MissionObjectiveProgressInfo GetCurrentProgress()
		{
			Func<MissionObjective, MissionObjectiveProgressInfo> getProgressCallback = this.GetProgressCallback;
			if (getProgressCallback == null)
			{
				return default(MissionObjectiveProgressInfo);
			}
			return getProgressCallback(this);
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000E306B File Offset: 0x000E126B
		protected override bool IsActivationRequirementsMet()
		{
			return this.IsActivationRequirementsMetCallback == null || this.IsActivationRequirementsMetCallback(this);
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000E3083 File Offset: 0x000E1283
		protected override bool IsCompletionRequirementsMet()
		{
			return this.IsCompletionRequirementsMetCallback == null || this.IsCompletionRequirementsMetCallback(this);
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x000E309B File Offset: 0x000E129B
		protected override void OnStart()
		{
			base.OnStart();
			Action<MissionObjective> onStartCallback = this.OnStartCallback;
			if (onStartCallback == null)
			{
				return;
			}
			onStartCallback(this);
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x000E30B4 File Offset: 0x000E12B4
		protected override void OnComplete()
		{
			base.OnComplete();
			Action<MissionObjective> onCompleteCallback = this.OnCompleteCallback;
			if (onCompleteCallback == null)
			{
				return;
			}
			onCompleteCallback(this);
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000E30CD File Offset: 0x000E12CD
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
			Action<MissionObjective, float> onTickCallback = this.OnTickCallback;
			if (onTickCallback == null)
			{
				return;
			}
			onTickCallback(this, dt);
		}

		// Token: 0x04001795 RID: 6037
		internal string IUniqueId;

		// Token: 0x04001796 RID: 6038
		internal TextObject IName;

		// Token: 0x04001797 RID: 6039
		internal TextObject IDescription;

		// Token: 0x04001798 RID: 6040
		internal Func<MissionObjective, bool> IsActivationRequirementsMetCallback;

		// Token: 0x04001799 RID: 6041
		internal Func<MissionObjective, bool> IsCompletionRequirementsMetCallback;

		// Token: 0x0400179A RID: 6042
		internal Action<MissionObjective> OnStartCallback;

		// Token: 0x0400179B RID: 6043
		internal Action<MissionObjective> OnCompleteCallback;

		// Token: 0x0400179C RID: 6044
		internal Action<MissionObjective, float> OnTickCallback;

		// Token: 0x0400179D RID: 6045
		internal Func<MissionObjective, MissionObjectiveProgressInfo> GetProgressCallback;

		// Token: 0x0400179E RID: 6046
		private List<MissionObjectiveTarget> _targets;
	}
}
