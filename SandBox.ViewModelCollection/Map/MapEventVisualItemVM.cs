using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.ViewModelCollection.Map
{
	// Token: 0x02000046 RID: 70
	public class MapEventVisualItemVM : ViewModel
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00011BEB File Offset: 0x0000FDEB
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x00011BF3 File Offset: 0x0000FDF3
		public MapEvent MapEvent { get; private set; }

		// Token: 0x06000477 RID: 1143 RVA: 0x00011BFC File Offset: 0x0000FDFC
		public MapEventVisualItemVM(Camera mapCamera, MapEvent mapEvent)
		{
			this._mapCamera = mapCamera;
			this.MapEvent = mapEvent;
			this._mapEventPositionCache = mapEvent.Position;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00011C1E File Offset: 0x0000FE1E
		public void UpdateProperties()
		{
			this.EventType = (int)SandBoxUIHelper.GetMapEventVisualTypeFromMapEvent(this.MapEvent);
			this._isAVisibleEvent = this.MapEvent.IsVisible;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00011C44 File Offset: 0x0000FE44
		public void ParallelUpdatePosition()
		{
			this._latestX = 0f;
			this._latestY = 0f;
			this._latestW = 0f;
			if (this._mapEventPositionCache != this.MapEvent.Position)
			{
				this._mapEventPositionCache = this.MapEvent.Position;
			}
			MBWindowManager.WorldToScreenInsideUsableArea(this._mapCamera, this._mapEventPositionCache.AsVec3() + new Vec3(0f, 0f, 1.5f, -1f), ref this._latestX, ref this._latestY, ref this._latestW);
			this._bindPosition = new Vec2(this._latestX, this._latestY);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00011CF9 File Offset: 0x0000FEF9
		public void DetermineIsVisibleOnMap()
		{
			this._bindIsVisibleOnMap = this._latestW > 0f && this._mapCamera.Position.z < 200f && this._isAVisibleEvent;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00011D2E File Offset: 0x0000FF2E
		public void UpdateBindingProperties()
		{
			this.Position = this._bindPosition;
			this.IsVisibleOnMap = this._bindIsVisibleOnMap;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00011D48 File Offset: 0x0000FF48
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00011D50 File Offset: 0x0000FF50
		public Vec2 Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (this._position != value)
				{
					this._position = value;
					base.OnPropertyChangedWithValue(value, "Position");
				}
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00011D73 File Offset: 0x0000FF73
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x00011D7B File Offset: 0x0000FF7B
		public int EventType
		{
			get
			{
				return this._eventType;
			}
			set
			{
				if (this._eventType != value)
				{
					this._eventType = value;
					base.OnPropertyChangedWithValue(value, "EventType");
				}
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00011D99 File Offset: 0x0000FF99
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x00011DA1 File Offset: 0x0000FFA1
		public bool IsVisibleOnMap
		{
			get
			{
				return this._isVisibleOnMap;
			}
			set
			{
				if (this._isVisibleOnMap != value)
				{
					this._isVisibleOnMap = value;
					base.OnPropertyChangedWithValue(value, "IsVisibleOnMap");
				}
			}
		}

		// Token: 0x04000242 RID: 578
		private Camera _mapCamera;

		// Token: 0x04000243 RID: 579
		private bool _isAVisibleEvent;

		// Token: 0x04000244 RID: 580
		private CampaignVec2 _mapEventPositionCache;

		// Token: 0x04000245 RID: 581
		private const float CameraDistanceCutoff = 200f;

		// Token: 0x04000246 RID: 582
		private Vec2 _bindPosition;

		// Token: 0x04000247 RID: 583
		private bool _bindIsVisibleOnMap;

		// Token: 0x04000248 RID: 584
		private float _latestX;

		// Token: 0x04000249 RID: 585
		private float _latestY;

		// Token: 0x0400024A RID: 586
		private float _latestW;

		// Token: 0x0400024B RID: 587
		private Vec2 _position;

		// Token: 0x0400024C RID: 588
		private int _eventType;

		// Token: 0x0400024D RID: 589
		private bool _isVisibleOnMap;
	}
}
