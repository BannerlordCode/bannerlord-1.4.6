using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.MissionViews
{
	// Token: 0x02000084 RID: 132
	public class ReplayMissionView : MissionView
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x00025537 File Offset: 0x00023737
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._resetTime = 0f;
			this._replayMissionLogic = base.Mission.GetMissionBehavior<ReplayMissionLogic>();
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0002555C File Offset: 0x0002375C
		public override void OnPreMissionTick(float dt)
		{
			base.OnPreMissionTick(dt);
			base.Mission.Recorder.ProcessRecordUntilTime(base.Mission.CurrentTime - this._resetTime);
			bool isInputOverridden = this._isInputOverridden;
			if (base.Mission.CurrentState == Mission.State.Continuing && base.Mission.Recorder.IsEndOfRecord())
			{
				if (MBEditor._isEditorMissionOn)
				{
					MBEditor.LeaveEditMissionMode();
					return;
				}
				base.Mission.EndMission();
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000255D1 File Offset: 0x000237D1
		public void OverrideInput(bool isOverridden)
		{
			this._isInputOverridden = isOverridden;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000255DC File Offset: 0x000237DC
		public void ResetReplay()
		{
			this._resetTime = base.Mission.CurrentTime;
			base.Mission.ResetMission();
			base.Mission.Teams.Clear();
			base.Mission.Recorder.RestartRecord();
			MBCommon.UnPauseGameEngine();
			base.Mission.Scene.TimeSpeed = 1f;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00025640 File Offset: 0x00023840
		public void Rewind(float time)
		{
			this._resetTime = MathF.Min(this._resetTime + time, base.Mission.CurrentTime);
			base.Mission.ResetMission();
			base.Mission.Teams.Clear();
			base.Mission.Recorder.RestartRecord();
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00025696 File Offset: 0x00023896
		public void FastForward(float time)
		{
			this._resetTime -= time;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000256A6 File Offset: 0x000238A6
		public void Pause()
		{
			if (!MBCommon.IsPaused && base.Mission.Scene.TimeSpeed.ApproximatelyEqualsTo(1f, 1E-05f))
			{
				MBCommon.PauseGameEngine();
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000256D8 File Offset: 0x000238D8
		public void Resume()
		{
			if (MBCommon.IsPaused || !base.Mission.Scene.TimeSpeed.ApproximatelyEqualsTo(1f, 1E-05f))
			{
				MBCommon.UnPauseGameEngine();
				base.Mission.Scene.TimeSpeed = 1f;
			}
		}

		// Token: 0x040002D5 RID: 725
		private float _resetTime;

		// Token: 0x040002D6 RID: 726
		private bool _isInputOverridden;

		// Token: 0x040002D7 RID: 727
		private ReplayMissionLogic _replayMissionLogic;
	}
}
