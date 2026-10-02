using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.MissionViews
{
	// Token: 0x02000083 RID: 131
	public class ReplayCaptureLogic : MissionView
	{
		// Token: 0x060004EE RID: 1262 RVA: 0x00024B64 File Offset: 0x00022D64
		private void CheckFixedDeltaTimeMode()
		{
			if (this.RenderActive && this.SaveScreenshots)
			{
				base.Mission.FixedDeltaTime = 0.016666668f;
				base.Mission.FixedDeltaTimeMode = true;
				return;
			}
			base.Mission.FixedDeltaTime = 0f;
			base.Mission.FixedDeltaTimeMode = false;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00024BBA File Offset: 0x00022DBA
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x00024BC2 File Offset: 0x00022DC2
		private bool RenderActive
		{
			get
			{
				return this._renderActive;
			}
			set
			{
				this._renderActive = value;
				this.CheckFixedDeltaTimeMode();
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00024BD1 File Offset: 0x00022DD1
		private Camera MissionCamera
		{
			get
			{
				if (base.MissionScreen == null || !(base.MissionScreen.CombatCamera != null))
				{
					return null;
				}
				return base.MissionScreen.CombatCamera;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00024BFB File Offset: 0x00022DFB
		private float ReplayTime
		{
			get
			{
				return base.Mission.CurrentTime - this._replayTimeDiff;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00024C0F File Offset: 0x00022E0F
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x00024C17 File Offset: 0x00022E17
		private bool SaveScreenshots
		{
			get
			{
				return this._saveScreenshots;
			}
			set
			{
				this._saveScreenshots = value;
				this.CheckFixedDeltaTimeMode();
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00024C26 File Offset: 0x00022E26
		private KeyValuePair<float, MatrixFrame> PreviousKey
		{
			get
			{
				return this.GetPreviousKey();
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00024C2E File Offset: 0x00022E2E
		private KeyValuePair<float, MatrixFrame> NextKey
		{
			get
			{
				return this.GetNextKey();
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00024C38 File Offset: 0x00022E38
		private KeyValuePair<float, MatrixFrame> GetPreviousKey()
		{
			KeyValuePair<float, MatrixFrame> keyValuePair = this._invalid;
			if (!this._cameraKeys.Any<KeyValuePair<float, SortedDictionary<int, MatrixFrame>>>())
			{
				return keyValuePair;
			}
			foreach (KeyValuePair<float, SortedDictionary<int, MatrixFrame>> keyValuePair2 in this._cameraKeys)
			{
				if (keyValuePair2.Key <= this.ReplayTime)
				{
					keyValuePair = new KeyValuePair<float, MatrixFrame>(keyValuePair2.Key, keyValuePair2.Value[keyValuePair2.Value.Count - 1]);
				}
			}
			return keyValuePair;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00024CD4 File Offset: 0x00022ED4
		private KeyValuePair<float, MatrixFrame> GetNextKey()
		{
			KeyValuePair<float, MatrixFrame> keyValuePair = this._invalid;
			if (!this._cameraKeys.Any<KeyValuePair<float, SortedDictionary<int, MatrixFrame>>>())
			{
				return keyValuePair;
			}
			foreach (KeyValuePair<float, SortedDictionary<int, MatrixFrame>> keyValuePair2 in this._cameraKeys)
			{
				if (keyValuePair2.Key > this.ReplayTime)
				{
					keyValuePair = new KeyValuePair<float, MatrixFrame>(keyValuePair2.Key, keyValuePair2.Value[0]);
					break;
				}
			}
			return keyValuePair;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00024D64 File Offset: 0x00022F64
		public ReplayCaptureLogic()
		{
			this._cameraKeys = new SortedDictionary<float, SortedDictionary<int, MatrixFrame>>();
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00024D9B File Offset: 0x00022F9B
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._replayLogic = base.Mission.GetMissionBehavior<ReplayMissionView>();
			this._replayLogic.OverrideInput(true);
			if (!MBCommon.IsPaused)
			{
				this._replayLogic.Pause();
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00024DD4 File Offset: 0x00022FD4
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (this._frameSkip && !MBCommon.IsPaused)
			{
				if (!this._isRendered)
				{
					this._isRendered = true;
					return;
				}
				this._replayLogic.Pause();
				this._frameSkip = false;
			}
			if (this.RenderActive)
			{
				this.SaveScreenshot();
				if (!base.Mission.Recorder.IsEndOfRecord())
				{
					KeyValuePair<float, MatrixFrame> previousKey = this.PreviousKey;
					KeyValuePair<float, MatrixFrame> nextKey = this.NextKey;
					this._replayLogic.Resume();
					if (nextKey.Key >= 0f)
					{
						for (int i = 0; i < this._cameraKeys.Count; i++)
						{
							if (previousKey.Key == this._cameraKeys.ElementAt<KeyValuePair<float, SortedDictionary<int, MatrixFrame>>>(i).Key)
							{
								float num = nextKey.Key - previousKey.Key;
								float num2 = (this.ReplayTime - previousKey.Key) / num;
								int count = this._cameraKeys[previousKey.Key].Count;
								MatrixFrame matrixFrame;
								if (this._lastUsedIndex != i && count > 1)
								{
									matrixFrame = this._cameraKeys[previousKey.Key][count - 1];
								}
								else
								{
									matrixFrame = new MatrixFrame
									{
										origin = this._path.GetHermiteFrameForDt(num2, i).origin
									};
									Vec3 vec = previousKey.Value.rotation.s * (1f - num2) + nextKey.Value.rotation.s * num2;
									Vec3 vec2 = previousKey.Value.rotation.u * (1f - num2) + nextKey.Value.rotation.u * num2;
									Vec3 vec3 = previousKey.Value.rotation.f * (1f - num2) + nextKey.Value.rotation.f * num2;
									matrixFrame.rotation.s = vec;
									matrixFrame.rotation.u = vec2;
									matrixFrame.rotation.f = vec3;
								}
								matrixFrame.rotation.s.Normalize();
								matrixFrame.rotation.u.Normalize();
								matrixFrame.rotation.f.Normalize();
								matrixFrame.rotation.Orthonormalize();
								base.MissionScreen.CustomCamera.Frame = matrixFrame;
								this._lastUsedIndex = i;
								return;
							}
						}
						return;
					}
					if (previousKey.Key >= 0f)
					{
						int count2 = this._cameraKeys[previousKey.Key].Count;
						if (count2 > 1)
						{
							MatrixFrame matrixFrame2 = this._cameraKeys[previousKey.Key][count2 - 1];
							matrixFrame2.rotation.s.Normalize();
							matrixFrame2.rotation.u.Normalize();
							matrixFrame2.rotation.f.Normalize();
							matrixFrame2.rotation.Orthonormalize();
							base.MissionScreen.CustomCamera.Frame = matrixFrame2;
							return;
						}
					}
				}
				else
				{
					MBDebug.Print("All images are saved.", 0, Debug.DebugColor.DarkCyan, 64UL);
					this.RenderActive = false;
					this._replayLogic.ResetReplay();
					this._replayTimeDiff = base.Mission.CurrentTime;
					base.MissionScreen.CustomCamera = null;
					this._replayLogic.Pause();
					this.SaveScreenshots = false;
					this._ssNum = 0;
				}
				return;
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00025174 File Offset: 0x00023374
		private void InsertCamKey()
		{
			float replayTime = this.ReplayTime;
			MatrixFrame frame = this.MissionCamera.Frame;
			int num = 0;
			if (this._cameraKeys.ContainsKey(replayTime))
			{
				num = this._cameraKeys[replayTime].Count;
				this._cameraKeys[replayTime].Add(num, frame);
			}
			else
			{
				this._cameraKeys.Add(replayTime, new SortedDictionary<int, MatrixFrame> { { num, frame } });
			}
			MBDebug.Print(string.Concat(new object[] { "Keyframe to \"", replayTime, "\" has been inserted with the index: ", num, ".\n" }), 0, Debug.DebugColor.Green, 64UL);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00025223 File Offset: 0x00023423
		private void MoveToNextFrame()
		{
			this._replayLogic.FastForward(0.016666668f);
			this._replayLogic.Resume();
			this._frameSkip = true;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00025248 File Offset: 0x00023448
		private void GoToKey(float keyTime)
		{
			if (keyTime < 0f || !this._cameraKeys.ContainsKey(keyTime) || keyTime == this.ReplayTime)
			{
				return;
			}
			MatrixFrame matrixFrame;
			if (keyTime < this.ReplayTime)
			{
				matrixFrame = this._cameraKeys[keyTime][this._cameraKeys[keyTime].Count - 1];
				this._replayLogic.Rewind(this.ReplayTime - keyTime);
				this._replayTimeDiff = base.Mission.CurrentTime;
			}
			else
			{
				matrixFrame = this._cameraKeys[keyTime][0];
				this._replayLogic.FastForward(keyTime - this.ReplayTime);
			}
			this.MissionCamera.Frame = matrixFrame;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000252FC File Offset: 0x000234FC
		private void SetPath()
		{
			if (base.Mission.Scene.GetPathWithName("CameraPath") != null)
			{
				base.Mission.Scene.DeletePathWithName("CameraPath");
			}
			base.Mission.Scene.AddPath("CameraPath");
			foreach (KeyValuePair<float, SortedDictionary<int, MatrixFrame>> keyValuePair in this._cameraKeys)
			{
				base.Mission.Scene.AddPathPoint("CameraPath", keyValuePair.Value[0]);
			}
			this._path = base.Mission.Scene.GetPathWithName("CameraPath");
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000253CC File Offset: 0x000235CC
		private void Render(bool saveScreenshots = false)
		{
			if (!this._cameraKeys.ContainsKey(0f))
			{
				this._cameraKeys.Add(0f, new SortedDictionary<int, MatrixFrame> { 
				{
					0,
					this.MissionCamera.Frame
				} });
			}
			else
			{
				this._cameraKeys[0f] = new SortedDictionary<int, MatrixFrame> { 
				{
					0,
					this.MissionCamera.Frame
				} };
			}
			this._replayLogic.ResetReplay();
			this._replayLogic.Pause();
			this._replayTimeDiff = base.Mission.CurrentTime;
			this.SetPath();
			this.SaveScreenshots = saveScreenshots;
			this.RenderActive = true;
			this._lastUsedIndex = 0;
			base.MissionScreen.CustomCamera = base.MissionScreen.CombatCamera;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00025494 File Offset: 0x00023694
		private void SaveScreenshot()
		{
			if (!this.SaveScreenshots)
			{
				return;
			}
			if (string.IsNullOrEmpty(this._directoryPath.Path))
			{
				PlatformDirectoryPath platformDirectoryPath = new PlatformDirectoryPath(PlatformFileType.User, "Captures");
				string text = "Cap_" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
				this._directoryPath = platformDirectoryPath + text;
			}
			Utilities.TakeScreenshot(new PlatformFilePath(this._directoryPath, "time_" + string.Format("{0:000000}", this._ssNum) + ".bmp"));
			this._ssNum++;
		}

		// Token: 0x040002C8 RID: 712
		private ReplayMissionView _replayLogic;

		// Token: 0x040002C9 RID: 713
		private bool _renderActive;

		// Token: 0x040002CA RID: 714
		public const float CaptureFrameRate = 60f;

		// Token: 0x040002CB RID: 715
		private float _replayTimeDiff;

		// Token: 0x040002CC RID: 716
		private bool _frameSkip;

		// Token: 0x040002CD RID: 717
		private Path _path;

		// Token: 0x040002CE RID: 718
		private PlatformDirectoryPath _directoryPath;

		// Token: 0x040002CF RID: 719
		private bool _saveScreenshots;

		// Token: 0x040002D0 RID: 720
		private readonly KeyValuePair<float, MatrixFrame> _invalid = new KeyValuePair<float, MatrixFrame>(-1f, default(MatrixFrame));

		// Token: 0x040002D1 RID: 721
		private SortedDictionary<float, SortedDictionary<int, MatrixFrame>> _cameraKeys;

		// Token: 0x040002D2 RID: 722
		private bool _isRendered;

		// Token: 0x040002D3 RID: 723
		private int _lastUsedIndex;

		// Token: 0x040002D4 RID: 724
		private int _ssNum;
	}
}
