using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000338 RID: 824
	public class PathTracker
	{
		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002E30 RID: 11824 RVA: 0x000B2874 File Offset: 0x000B0A74
		// (set) Token: 0x06002E31 RID: 11825 RVA: 0x000B287C File Offset: 0x000B0A7C
		public float TotalDistanceTraveled { get; set; }

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002E32 RID: 11826 RVA: 0x000B2885 File Offset: 0x000B0A85
		public bool HasChanged
		{
			get
			{
				return this._path != null && this._version < this._path.GetVersion();
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002E33 RID: 11827 RVA: 0x000B28AA File Offset: 0x000B0AAA
		public bool IsValid
		{
			get
			{
				return this._path != null;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002E34 RID: 11828 RVA: 0x000B28B8 File Offset: 0x000B0AB8
		public bool HasReachedEnd
		{
			get
			{
				return this.TotalDistanceTraveled >= this._path.TotalDistance;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002E35 RID: 11829 RVA: 0x000B28D0 File Offset: 0x000B0AD0
		public float PathTraveledPercentage
		{
			get
			{
				return this.TotalDistanceTraveled / this._path.TotalDistance;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06002E36 RID: 11830 RVA: 0x000B28E4 File Offset: 0x000B0AE4
		public MatrixFrame CurrentFrame
		{
			get
			{
				MatrixFrame frameForDistance = this._path.GetFrameForDistance(this.TotalDistanceTraveled);
				frameForDistance.rotation.RotateAboutUp(3.1415927f);
				frameForDistance.rotation.ApplyScaleLocal(in this._initialScale);
				return frameForDistance;
			}
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000B2927 File Offset: 0x000B0B27
		public PathTracker(Path path, Vec3 initialScaleOfEntity)
		{
			this._path = path;
			this._initialScale = initialScaleOfEntity;
			if (path != null)
			{
				this.UpdateVersion();
			}
			this.Reset();
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000B2959 File Offset: 0x000B0B59
		public void UpdateVersion()
		{
			this._version = this._path.GetVersion();
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000B296C File Offset: 0x000B0B6C
		public bool PathExists()
		{
			return this._path != null;
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000B297A File Offset: 0x000B0B7A
		public void Advance(float deltaDistance)
		{
			this.TotalDistanceTraveled += deltaDistance;
			this.TotalDistanceTraveled = MathF.Min(this.TotalDistanceTraveled, this._path.TotalDistance);
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000B29A6 File Offset: 0x000B0BA6
		public float GetPathLength()
		{
			return this._path.TotalDistance;
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000B29B3 File Offset: 0x000B0BB3
		public void CurrentFrameAndColor(out MatrixFrame frame, out Vec3 color)
		{
			this._path.GetFrameAndColorForDistance(this.TotalDistanceTraveled, out frame, out color);
			frame.rotation.RotateAboutUp(3.1415927f);
			frame.rotation.ApplyScaleLocal(in this._initialScale);
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000B29E9 File Offset: 0x000B0BE9
		public void Reset()
		{
			this.TotalDistanceTraveled = 0f;
		}

		// Token: 0x04001257 RID: 4695
		private readonly Path _path;

		// Token: 0x04001258 RID: 4696
		private readonly Vec3 _initialScale;

		// Token: 0x04001259 RID: 4697
		private int _version = -1;
	}
}
