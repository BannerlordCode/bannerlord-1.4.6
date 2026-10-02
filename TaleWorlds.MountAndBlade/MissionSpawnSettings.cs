using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000293 RID: 659
	public struct MissionSpawnSettings
	{
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x00084989 File Offset: 0x00082B89
		// (set) Token: 0x06002486 RID: 9350 RVA: 0x00084991 File Offset: 0x00082B91
		public float GlobalReinforcementInterval
		{
			get
			{
				return this._globalReinforcementInterval;
			}
			set
			{
				this._globalReinforcementInterval = MathF.Max(value, 1f);
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x000849A4 File Offset: 0x00082BA4
		// (set) Token: 0x06002488 RID: 9352 RVA: 0x000849AC File Offset: 0x00082BAC
		public float DefenderAdvantageFactor
		{
			get
			{
				return this._defenderAdvantageFactor;
			}
			set
			{
				this._defenderAdvantageFactor = MathF.Clamp(value, 0.1f, 10f);
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002489 RID: 9353 RVA: 0x000849C4 File Offset: 0x00082BC4
		// (set) Token: 0x0600248A RID: 9354 RVA: 0x000849CC File Offset: 0x00082BCC
		public float MaximumBattleSideRatio
		{
			get
			{
				return this._maximumBattleSizeRatio;
			}
			set
			{
				this._maximumBattleSizeRatio = MathF.Clamp(value, 0.5f, 0.99f);
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x0600248B RID: 9355 RVA: 0x000849E4 File Offset: 0x00082BE4
		// (set) Token: 0x0600248C RID: 9356 RVA: 0x000849EC File Offset: 0x00082BEC
		public MissionSpawnSettings.InitialSpawnMethod InitialTroopsSpawnMethod { get; private set; }

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x000849F5 File Offset: 0x00082BF5
		// (set) Token: 0x0600248E RID: 9358 RVA: 0x000849FD File Offset: 0x00082BFD
		public MissionSpawnSettings.ReinforcementTimingMethod ReinforcementTroopsTimingMethod { get; private set; }

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x00084A06 File Offset: 0x00082C06
		// (set) Token: 0x06002490 RID: 9360 RVA: 0x00084A0E File Offset: 0x00082C0E
		public MissionSpawnSettings.ReinforcementSpawnMethod ReinforcementTroopsSpawnMethod { get; private set; }

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002491 RID: 9361 RVA: 0x00084A17 File Offset: 0x00082C17
		// (set) Token: 0x06002492 RID: 9362 RVA: 0x00084A1F File Offset: 0x00082C1F
		public float ReinforcementBatchPercentage
		{
			get
			{
				return this._reinforcementBatchPercentage;
			}
			set
			{
				this._reinforcementBatchPercentage = MathF.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002493 RID: 9363 RVA: 0x00084A37 File Offset: 0x00082C37
		// (set) Token: 0x06002494 RID: 9364 RVA: 0x00084A3F File Offset: 0x00082C3F
		public float DesiredReinforcementPercentage
		{
			get
			{
				return this._desiredReinforcementPercentage;
			}
			set
			{
				this._desiredReinforcementPercentage = MathF.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x00084A57 File Offset: 0x00082C57
		// (set) Token: 0x06002496 RID: 9366 RVA: 0x00084A5F File Offset: 0x00082C5F
		public float ReinforcementWavePercentage
		{
			get
			{
				return this._reinforcementWavePercentage;
			}
			set
			{
				this._reinforcementWavePercentage = MathF.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002497 RID: 9367 RVA: 0x00084A77 File Offset: 0x00082C77
		// (set) Token: 0x06002498 RID: 9368 RVA: 0x00084A7F File Offset: 0x00082C7F
		public int MaximumReinforcementWaveCount
		{
			get
			{
				return this._maximumReinforcementWaveCount;
			}
			set
			{
				this._maximumReinforcementWaveCount = MathF.Max(value, 0);
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002499 RID: 9369 RVA: 0x00084A8E File Offset: 0x00082C8E
		// (set) Token: 0x0600249A RID: 9370 RVA: 0x00084A96 File Offset: 0x00082C96
		public float DefenderReinforcementBatchPercentage
		{
			get
			{
				return this._defenderReinforcementBatchPercentage;
			}
			set
			{
				this._defenderReinforcementBatchPercentage = MathF.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x0600249B RID: 9371 RVA: 0x00084AAE File Offset: 0x00082CAE
		// (set) Token: 0x0600249C RID: 9372 RVA: 0x00084AB6 File Offset: 0x00082CB6
		public float AttackerReinforcementBatchPercentage
		{
			get
			{
				return this._attackerReinforcementBatchPercentage;
			}
			set
			{
				this._attackerReinforcementBatchPercentage = MathF.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x00084AD0 File Offset: 0x00082CD0
		public MissionSpawnSettings(MissionSpawnSettings.InitialSpawnMethod initialTroopsSpawnMethod, MissionSpawnSettings.ReinforcementTimingMethod reinforcementTimingMethod, MissionSpawnSettings.ReinforcementSpawnMethod reinforcementTroopsSpawnMethod, float globalReinforcementInterval = 0f, float reinforcementBatchPercentage = 0f, float desiredReinforcementPercentage = 0f, float reinforcementWavePercentage = 0f, int maximumReinforcementWaveCount = 0, float defenderReinforcementBatchPercentage = 0f, float attackerReinforcementBatchPercentage = 0f, float defenderAdvantageFactor = 1f, float maximumBattleSizeRatio = 0.75f)
		{
			this = default(MissionSpawnSettings);
			this.InitialTroopsSpawnMethod = initialTroopsSpawnMethod;
			this.ReinforcementTroopsTimingMethod = reinforcementTimingMethod;
			this.ReinforcementTroopsSpawnMethod = reinforcementTroopsSpawnMethod;
			this.GlobalReinforcementInterval = globalReinforcementInterval;
			this.ReinforcementBatchPercentage = reinforcementBatchPercentage;
			this.DesiredReinforcementPercentage = desiredReinforcementPercentage;
			this.ReinforcementWavePercentage = reinforcementWavePercentage;
			this.MaximumReinforcementWaveCount = maximumReinforcementWaveCount;
			this.DefenderReinforcementBatchPercentage = defenderReinforcementBatchPercentage;
			this.AttackerReinforcementBatchPercentage = attackerReinforcementBatchPercentage;
			this.DefenderAdvantageFactor = defenderAdvantageFactor;
			this.MaximumBattleSideRatio = maximumBattleSizeRatio;
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x00084B44 File Offset: 0x00082D44
		public static MissionSpawnSettings CreateDefaultSpawnSettings()
		{
			return new MissionSpawnSettings(MissionSpawnSettings.InitialSpawnMethod.BattleSizeAllocating, MissionSpawnSettings.ReinforcementTimingMethod.GlobalTimer, MissionSpawnSettings.ReinforcementSpawnMethod.Balanced, 10f, 0.05f, 0.166f, 0f, 0, 0f, 0f, 1f, 0.75f);
		}

		// Token: 0x04000E0E RID: 3598
		public const float MinimumReinforcementInterval = 1f;

		// Token: 0x04000E0F RID: 3599
		public const float MinimumDefenderAdvantageFactor = 0.1f;

		// Token: 0x04000E10 RID: 3600
		public const float MaximumDefenderAdvantageFactor = 10f;

		// Token: 0x04000E11 RID: 3601
		public const float MinimumBattleSizeRatioLimit = 0.5f;

		// Token: 0x04000E12 RID: 3602
		public const float MaximumBattleSizeRatioLimit = 0.99f;

		// Token: 0x04000E13 RID: 3603
		public const float DefaultMaximumBattleSizeRatio = 0.75f;

		// Token: 0x04000E14 RID: 3604
		public const float DefaultDefenderAdvantageFactor = 1f;

		// Token: 0x04000E18 RID: 3608
		private float _globalReinforcementInterval;

		// Token: 0x04000E19 RID: 3609
		private float _defenderAdvantageFactor;

		// Token: 0x04000E1A RID: 3610
		private float _maximumBattleSizeRatio;

		// Token: 0x04000E1B RID: 3611
		private float _reinforcementBatchPercentage;

		// Token: 0x04000E1C RID: 3612
		private float _desiredReinforcementPercentage;

		// Token: 0x04000E1D RID: 3613
		private float _reinforcementWavePercentage;

		// Token: 0x04000E1E RID: 3614
		private int _maximumReinforcementWaveCount;

		// Token: 0x04000E1F RID: 3615
		private float _defenderReinforcementBatchPercentage;

		// Token: 0x04000E20 RID: 3616
		private float _attackerReinforcementBatchPercentage;

		// Token: 0x02000566 RID: 1382
		public enum ReinforcementSpawnMethod
		{
			// Token: 0x04001E16 RID: 7702
			Balanced,
			// Token: 0x04001E17 RID: 7703
			Wave,
			// Token: 0x04001E18 RID: 7704
			Fixed
		}

		// Token: 0x02000567 RID: 1383
		public enum ReinforcementTimingMethod
		{
			// Token: 0x04001E1A RID: 7706
			GlobalTimer,
			// Token: 0x04001E1B RID: 7707
			CustomTimer
		}

		// Token: 0x02000568 RID: 1384
		public enum InitialSpawnMethod
		{
			// Token: 0x04001E1D RID: 7709
			BattleSizeAllocating,
			// Token: 0x04001E1E RID: 7710
			FreeAllocation
		}
	}
}
