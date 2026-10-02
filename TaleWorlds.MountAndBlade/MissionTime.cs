using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002A0 RID: 672
	public struct MissionTime : IComparable<MissionTime>
	{
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x00086F5B File Offset: 0x0008515B
		public long NumberOfTicks
		{
			get
			{
				return this._numberOfTicks;
			}
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x00086F63 File Offset: 0x00085163
		public MissionTime(long numberOfTicks)
		{
			this._numberOfTicks = numberOfTicks;
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x0600251C RID: 9500 RVA: 0x00086F6C File Offset: 0x0008516C
		private static long CurrentNumberOfTicks
		{
			get
			{
				return Mission.Current.MissionTimeTracker.NumberOfTicks;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x00086F7D File Offset: 0x0008517D
		public static MissionTime DeltaTime
		{
			get
			{
				return new MissionTime(Mission.Current.MissionTimeTracker.DeltaTimeInTicks);
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x00086F93 File Offset: 0x00085193
		private static long DeltaTimeInTicks
		{
			get
			{
				return Mission.Current.MissionTimeTracker.DeltaTimeInTicks;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x00086FA4 File Offset: 0x000851A4
		public static MissionTime Now
		{
			get
			{
				return new MissionTime(Mission.Current.MissionTimeTracker.NumberOfTicks);
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x00086FBA File Offset: 0x000851BA
		public bool IsFuture
		{
			get
			{
				return MissionTime.CurrentNumberOfTicks < this._numberOfTicks;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x00086FC9 File Offset: 0x000851C9
		public bool IsPast
		{
			get
			{
				return MissionTime.CurrentNumberOfTicks > this._numberOfTicks;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002522 RID: 9506 RVA: 0x00086FD8 File Offset: 0x000851D8
		public bool IsNow
		{
			get
			{
				return MissionTime.CurrentNumberOfTicks == this._numberOfTicks;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x00086FE7 File Offset: 0x000851E7
		public float ElapsedHours
		{
			get
			{
				return (float)(MissionTime.CurrentNumberOfTicks - this._numberOfTicks) / 3.6E+10f;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06002524 RID: 9508 RVA: 0x00086FFC File Offset: 0x000851FC
		public float ElapsedSeconds
		{
			get
			{
				return (float)(MissionTime.CurrentNumberOfTicks - this._numberOfTicks) * 1E-07f;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x00087011 File Offset: 0x00085211
		public float ElapsedMilliseconds
		{
			get
			{
				return (float)(MissionTime.CurrentNumberOfTicks - this._numberOfTicks) / 10000f;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x00087026 File Offset: 0x00085226
		public double ToHours
		{
			get
			{
				return (double)this._numberOfTicks / 36000000000.0;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002527 RID: 9511 RVA: 0x00087039 File Offset: 0x00085239
		public double ToMinutes
		{
			get
			{
				return (double)this._numberOfTicks / 600000000.0;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002528 RID: 9512 RVA: 0x0008704C File Offset: 0x0008524C
		public double ToSeconds
		{
			get
			{
				return (double)this._numberOfTicks * 1.0000000116860974E-07;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x0008705F File Offset: 0x0008525F
		public double ToMilliseconds
		{
			get
			{
				return (double)this._numberOfTicks / 10000.0;
			}
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x00087072 File Offset: 0x00085272
		public static MissionTime MillisecondsFromNow(float valueInMilliseconds)
		{
			return new MissionTime((long)(valueInMilliseconds * 10000f + (float)MissionTime.CurrentNumberOfTicks));
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00087088 File Offset: 0x00085288
		public static MissionTime SecondsFromNow(float valueInSeconds)
		{
			return new MissionTime((long)(valueInSeconds * 10000000f + (float)MissionTime.CurrentNumberOfTicks));
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x0008709E File Offset: 0x0008529E
		public bool Equals(MissionTime other)
		{
			return this._numberOfTicks == other._numberOfTicks;
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000870AE File Offset: 0x000852AE
		public override bool Equals(object obj)
		{
			return obj != null && obj is MissionTime && this.Equals((MissionTime)obj);
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000870CC File Offset: 0x000852CC
		public override int GetHashCode()
		{
			return this._numberOfTicks.GetHashCode();
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000870E7 File Offset: 0x000852E7
		public int CompareTo(MissionTime other)
		{
			if (this._numberOfTicks == other._numberOfTicks)
			{
				return 0;
			}
			if (this._numberOfTicks > other._numberOfTicks)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x0008710A File Offset: 0x0008530A
		public static bool operator <(MissionTime x, MissionTime y)
		{
			return x._numberOfTicks < y._numberOfTicks;
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x0008711A File Offset: 0x0008531A
		public static bool operator >(MissionTime x, MissionTime y)
		{
			return x._numberOfTicks > y._numberOfTicks;
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x0008712A File Offset: 0x0008532A
		public static bool operator ==(MissionTime x, MissionTime y)
		{
			return x._numberOfTicks == y._numberOfTicks;
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x0008713A File Offset: 0x0008533A
		public static bool operator !=(MissionTime x, MissionTime y)
		{
			return !(x == y);
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x00087146 File Offset: 0x00085346
		public static bool operator <=(MissionTime x, MissionTime y)
		{
			return x._numberOfTicks <= y._numberOfTicks;
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x00087159 File Offset: 0x00085359
		public static bool operator >=(MissionTime x, MissionTime y)
		{
			return x._numberOfTicks >= y._numberOfTicks;
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x0008716C File Offset: 0x0008536C
		public static MissionTime Milliseconds(float valueInMilliseconds)
		{
			return new MissionTime((long)(valueInMilliseconds * 10000f));
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x0008717B File Offset: 0x0008537B
		public static MissionTime Seconds(float valueInSeconds)
		{
			return new MissionTime((long)(valueInSeconds * 10000000f));
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x0008718A File Offset: 0x0008538A
		public static MissionTime Minutes(float valueInMinutes)
		{
			return new MissionTime((long)(valueInMinutes * 600000000f));
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x00087199 File Offset: 0x00085399
		public static MissionTime Hours(float valueInHours)
		{
			return new MissionTime((long)(valueInHours * 3.6E+10f));
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x000871A8 File Offset: 0x000853A8
		public static MissionTime Zero
		{
			get
			{
				return new MissionTime(0L);
			}
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x000871B1 File Offset: 0x000853B1
		public static MissionTime operator +(MissionTime g1, MissionTime g2)
		{
			return new MissionTime(g1._numberOfTicks + g2._numberOfTicks);
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000871C5 File Offset: 0x000853C5
		public static MissionTime operator -(MissionTime g1, MissionTime g2)
		{
			return new MissionTime(g1._numberOfTicks - g2._numberOfTicks);
		}

		// Token: 0x04000E59 RID: 3673
		public const long TimeTicksPerMilliSecond = 10000L;

		// Token: 0x04000E5A RID: 3674
		public const long TimeTicksPerSecond = 10000000L;

		// Token: 0x04000E5B RID: 3675
		public const long TimeTicksPerMinute = 600000000L;

		// Token: 0x04000E5C RID: 3676
		public const long TimeTicksPerHour = 36000000000L;

		// Token: 0x04000E5D RID: 3677
		public const float InvTimeTicksPerSecond = 1E-07f;

		// Token: 0x04000E5E RID: 3678
		private readonly long _numberOfTicks;
	}
}
