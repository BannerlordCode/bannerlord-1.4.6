using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.Compass
{
	// Token: 0x02000067 RID: 103
	public class CompassMarkerVM : ViewModel
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x0001C1F2 File Offset: 0x0001A3F2
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x0001C1FA File Offset: 0x0001A3FA
		public float Angle { get; private set; }

		// Token: 0x06000807 RID: 2055 RVA: 0x0001C203 File Offset: 0x0001A403
		public CompassMarkerVM(bool isPrimary, float angle, string text)
		{
			this.IsPrimary = isPrimary;
			this.Angle = angle;
			this.Text = (this.IsPrimary ? text : ("-" + text + "-"));
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001C23A File Offset: 0x0001A43A
		public void Refresh(float circleX, float x, float distance)
		{
			this.FullPosition = circleX;
			this.Position = x;
			this.Distance = MathF.Round(distance);
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0001C256 File Offset: 0x0001A456
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x0001C25E File Offset: 0x0001A45E
		[DataSourceProperty]
		public bool IsPrimary
		{
			get
			{
				return this._isPrimary;
			}
			set
			{
				if (value != this._isPrimary)
				{
					this._isPrimary = value;
					base.OnPropertyChangedWithValue(value, "IsPrimary");
				}
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0001C27C File Offset: 0x0001A47C
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x0001C284 File Offset: 0x0001A484
		[DataSourceProperty]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (value != this._text)
				{
					this._text = value;
					base.OnPropertyChangedWithValue<string>(value, "Text");
				}
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0001C2A7 File Offset: 0x0001A4A7
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x0001C2AF File Offset: 0x0001A4AF
		[DataSourceProperty]
		public int Distance
		{
			get
			{
				return this._distance;
			}
			set
			{
				if (value != this._distance)
				{
					this._distance = value;
					base.OnPropertyChangedWithValue(value, "Distance");
				}
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0001C2CD File Offset: 0x0001A4CD
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x0001C2D5 File Offset: 0x0001A4D5
		[DataSourceProperty]
		public float Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (MathF.Abs(value - this._position) > 1E-45f)
				{
					this._position = value;
					base.OnPropertyChangedWithValue(value, "Position");
				}
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0001C2FE File Offset: 0x0001A4FE
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x0001C306 File Offset: 0x0001A506
		[DataSourceProperty]
		public float FullPosition
		{
			get
			{
				return this._fullPosition;
			}
			set
			{
				if (MathF.Abs(value - this._fullPosition) > 1E-45f)
				{
					this._fullPosition = value;
					base.OnPropertyChangedWithValue(value, "FullPosition");
				}
			}
		}

		// Token: 0x04000397 RID: 919
		private bool _isPrimary;

		// Token: 0x04000398 RID: 920
		private string _text;

		// Token: 0x04000399 RID: 921
		private int _distance;

		// Token: 0x0400039A RID: 922
		private float _position;

		// Token: 0x0400039B RID: 923
		private float _fullPosition;
	}
}
