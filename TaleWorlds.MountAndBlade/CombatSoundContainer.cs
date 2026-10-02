using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001EE RID: 494
	public static class CombatSoundContainer
	{
		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x0006296A File Offset: 0x00060B6A
		// (set) Token: 0x06001CAD RID: 7341 RVA: 0x00062971 File Offset: 0x00060B71
		public static int SoundCodeMissionCombatBluntHigh { get; private set; }

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x00062979 File Offset: 0x00060B79
		// (set) Token: 0x06001CAF RID: 7343 RVA: 0x00062980 File Offset: 0x00060B80
		public static int SoundCodeMissionCombatBluntLow { get; private set; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x00062988 File Offset: 0x00060B88
		// (set) Token: 0x06001CB1 RID: 7345 RVA: 0x0006298F File Offset: 0x00060B8F
		public static int SoundCodeMissionCombatBluntMed { get; private set; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x00062997 File Offset: 0x00060B97
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x0006299E File Offset: 0x00060B9E
		public static int SoundCodeMissionCombatBoulderHigh { get; private set; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x000629A6 File Offset: 0x00060BA6
		// (set) Token: 0x06001CB5 RID: 7349 RVA: 0x000629AD File Offset: 0x00060BAD
		public static int SoundCodeMissionCombatBoulderLow { get; private set; }

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x000629B5 File Offset: 0x00060BB5
		// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x000629BC File Offset: 0x00060BBC
		public static int SoundCodeMissionCombatBoulderMed { get; private set; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x000629C4 File Offset: 0x00060BC4
		// (set) Token: 0x06001CB9 RID: 7353 RVA: 0x000629CB File Offset: 0x00060BCB
		public static int SoundCodeMissionCombatCutHigh { get; private set; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x000629D3 File Offset: 0x00060BD3
		// (set) Token: 0x06001CBB RID: 7355 RVA: 0x000629DA File Offset: 0x00060BDA
		public static int SoundCodeMissionCombatCutLow { get; private set; }

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x000629E2 File Offset: 0x00060BE2
		// (set) Token: 0x06001CBD RID: 7357 RVA: 0x000629E9 File Offset: 0x00060BE9
		public static int SoundCodeMissionCombatCutMed { get; private set; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x000629F1 File Offset: 0x00060BF1
		// (set) Token: 0x06001CBF RID: 7359 RVA: 0x000629F8 File Offset: 0x00060BF8
		public static int SoundCodeMissionCombatMissileHigh { get; private set; }

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x00062A00 File Offset: 0x00060C00
		// (set) Token: 0x06001CC1 RID: 7361 RVA: 0x00062A07 File Offset: 0x00060C07
		public static int SoundCodeMissionCombatMissileLow { get; private set; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x00062A0F File Offset: 0x00060C0F
		// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x00062A16 File Offset: 0x00060C16
		public static int SoundCodeMissionCombatMissileMed { get; private set; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x00062A1E File Offset: 0x00060C1E
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x00062A25 File Offset: 0x00060C25
		public static int SoundCodeMissionCombatPierceHigh { get; private set; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x00062A2D File Offset: 0x00060C2D
		// (set) Token: 0x06001CC7 RID: 7367 RVA: 0x00062A34 File Offset: 0x00060C34
		public static int SoundCodeMissionCombatPierceLow { get; private set; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x00062A3C File Offset: 0x00060C3C
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x00062A43 File Offset: 0x00060C43
		public static int SoundCodeMissionCombatPierceMed { get; private set; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x00062A4B File Offset: 0x00060C4B
		// (set) Token: 0x06001CCB RID: 7371 RVA: 0x00062A52 File Offset: 0x00060C52
		public static int SoundCodeMissionCombatPunchHigh { get; private set; }

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x00062A5A File Offset: 0x00060C5A
		// (set) Token: 0x06001CCD RID: 7373 RVA: 0x00062A61 File Offset: 0x00060C61
		public static int SoundCodeMissionCombatPunchLow { get; private set; }

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x00062A69 File Offset: 0x00060C69
		// (set) Token: 0x06001CCF RID: 7375 RVA: 0x00062A70 File Offset: 0x00060C70
		public static int SoundCodeMissionCombatPunchMed { get; private set; }

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x00062A78 File Offset: 0x00060C78
		// (set) Token: 0x06001CD1 RID: 7377 RVA: 0x00062A7F File Offset: 0x00060C7F
		public static int SoundCodeMissionCombatThrowingAxeHigh { get; private set; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x00062A87 File Offset: 0x00060C87
		// (set) Token: 0x06001CD3 RID: 7379 RVA: 0x00062A8E File Offset: 0x00060C8E
		public static int SoundCodeMissionCombatThrowingAxeLow { get; private set; }

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x00062A96 File Offset: 0x00060C96
		// (set) Token: 0x06001CD5 RID: 7381 RVA: 0x00062A9D File Offset: 0x00060C9D
		public static int SoundCodeMissionCombatThrowingAxeMed { get; private set; }

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x00062AA5 File Offset: 0x00060CA5
		// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x00062AAC File Offset: 0x00060CAC
		public static int SoundCodeMissionCombatThrowingDaggerHigh { get; private set; }

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x00062AB4 File Offset: 0x00060CB4
		// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x00062ABB File Offset: 0x00060CBB
		public static int SoundCodeMissionCombatThrowingDaggerLow { get; private set; }

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x00062AC3 File Offset: 0x00060CC3
		// (set) Token: 0x06001CDB RID: 7387 RVA: 0x00062ACA File Offset: 0x00060CCA
		public static int SoundCodeMissionCombatThrowingDaggerMed { get; private set; }

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x00062AD2 File Offset: 0x00060CD2
		// (set) Token: 0x06001CDD RID: 7389 RVA: 0x00062AD9 File Offset: 0x00060CD9
		public static int SoundCodeMissionCombatThrowingStoneHigh { get; private set; }

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x00062AE1 File Offset: 0x00060CE1
		// (set) Token: 0x06001CDF RID: 7391 RVA: 0x00062AE8 File Offset: 0x00060CE8
		public static int SoundCodeMissionCombatThrowingStoneLow { get; private set; }

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x00062AF0 File Offset: 0x00060CF0
		// (set) Token: 0x06001CE1 RID: 7393 RVA: 0x00062AF7 File Offset: 0x00060CF7
		public static int SoundCodeMissionCombatThrowingStoneMed { get; private set; }

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x00062AFF File Offset: 0x00060CFF
		// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x00062B06 File Offset: 0x00060D06
		public static int SoundCodeMissionCombatChargeDamage { get; private set; }

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x00062B0E File Offset: 0x00060D0E
		// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x00062B15 File Offset: 0x00060D15
		public static int SoundCodeMissionCombatKick { get; private set; }

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x00062B1D File Offset: 0x00060D1D
		// (set) Token: 0x06001CE7 RID: 7399 RVA: 0x00062B24 File Offset: 0x00060D24
		public static int SoundCodeMissionCombatPlayerhit { get; private set; }

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x00062B2C File Offset: 0x00060D2C
		// (set) Token: 0x06001CE9 RID: 7401 RVA: 0x00062B33 File Offset: 0x00060D33
		public static int SoundCodeMissionCombatWoodShieldBash { get; private set; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x00062B3B File Offset: 0x00060D3B
		// (set) Token: 0x06001CEB RID: 7403 RVA: 0x00062B42 File Offset: 0x00060D42
		public static int SoundCodeMissionCombatMetalShieldBash { get; private set; }

		// Token: 0x06001CEC RID: 7404 RVA: 0x00062B4A File Offset: 0x00060D4A
		static CombatSoundContainer()
		{
			CombatSoundContainer.UpdateMissionCombatSoundCodes();
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x00062B54 File Offset: 0x00060D54
		private static void UpdateMissionCombatSoundCodes()
		{
			CombatSoundContainer.SoundCodeMissionCombatBluntHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/blunt/high");
			CombatSoundContainer.SoundCodeMissionCombatBluntLow = SoundEvent.GetEventIdFromString("event:/mission/combat/blunt/low");
			CombatSoundContainer.SoundCodeMissionCombatBluntMed = SoundEvent.GetEventIdFromString("event:/mission/combat/blunt/med");
			CombatSoundContainer.SoundCodeMissionCombatBoulderHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/boulder/high");
			CombatSoundContainer.SoundCodeMissionCombatBoulderLow = SoundEvent.GetEventIdFromString("event:/mission/combat/boulder/low");
			CombatSoundContainer.SoundCodeMissionCombatBoulderMed = SoundEvent.GetEventIdFromString("event:/mission/combat/boulder/med");
			CombatSoundContainer.SoundCodeMissionCombatCutHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/cut/high");
			CombatSoundContainer.SoundCodeMissionCombatCutLow = SoundEvent.GetEventIdFromString("event:/mission/combat/cut/low");
			CombatSoundContainer.SoundCodeMissionCombatCutMed = SoundEvent.GetEventIdFromString("event:/mission/combat/cut/med");
			CombatSoundContainer.SoundCodeMissionCombatMissileHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/missile/high");
			CombatSoundContainer.SoundCodeMissionCombatMissileLow = SoundEvent.GetEventIdFromString("event:/mission/combat/missile/low");
			CombatSoundContainer.SoundCodeMissionCombatMissileMed = SoundEvent.GetEventIdFromString("event:/mission/combat/missile/med");
			CombatSoundContainer.SoundCodeMissionCombatPierceHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/pierce/high");
			CombatSoundContainer.SoundCodeMissionCombatPierceLow = SoundEvent.GetEventIdFromString("event:/mission/combat/pierce/low");
			CombatSoundContainer.SoundCodeMissionCombatPierceMed = SoundEvent.GetEventIdFromString("event:/mission/combat/pierce/med");
			CombatSoundContainer.SoundCodeMissionCombatPunchHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/punch/high");
			CombatSoundContainer.SoundCodeMissionCombatPunchLow = SoundEvent.GetEventIdFromString("event:/mission/combat/punch/low");
			CombatSoundContainer.SoundCodeMissionCombatPunchMed = SoundEvent.GetEventIdFromString("event:/mission/combat/punch/med");
			CombatSoundContainer.SoundCodeMissionCombatThrowingAxeHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/throwing/high");
			CombatSoundContainer.SoundCodeMissionCombatThrowingAxeLow = SoundEvent.GetEventIdFromString("event:/mission/combat/throwing/low");
			CombatSoundContainer.SoundCodeMissionCombatThrowingAxeMed = SoundEvent.GetEventIdFromString("event:/mission/combat/throwing/med");
			CombatSoundContainer.SoundCodeMissionCombatThrowingDaggerHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/throwing/high");
			CombatSoundContainer.SoundCodeMissionCombatThrowingDaggerLow = SoundEvent.GetEventIdFromString("event:/mission/combat/throwing/low");
			CombatSoundContainer.SoundCodeMissionCombatThrowingDaggerMed = SoundEvent.GetEventIdFromString("event:/mission/combat/throwing/med");
			CombatSoundContainer.SoundCodeMissionCombatThrowingStoneHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/throwingstone/high");
			CombatSoundContainer.SoundCodeMissionCombatThrowingStoneLow = SoundEvent.GetEventIdFromString("event:/mission/combat/throwingstone/low");
			CombatSoundContainer.SoundCodeMissionCombatThrowingStoneMed = SoundEvent.GetEventIdFromString("event:/mission/combat/throwingstone/med");
			CombatSoundContainer.SoundCodeMissionCombatChargeDamage = SoundEvent.GetEventIdFromString("event:/mission/combat/charge/damage");
			CombatSoundContainer.SoundCodeMissionCombatKick = SoundEvent.GetEventIdFromString("event:/mission/combat/kick");
			CombatSoundContainer.SoundCodeMissionCombatPlayerhit = SoundEvent.GetEventIdFromString("event:/mission/combat/playerHit");
			CombatSoundContainer.SoundCodeMissionCombatWoodShieldBash = SoundEvent.GetEventIdFromString("event:/mission/combat/shield/bash");
			CombatSoundContainer.SoundCodeMissionCombatMetalShieldBash = SoundEvent.GetEventIdFromString("event:/mission/combat/shield/metal_bash");
		}
	}
}
