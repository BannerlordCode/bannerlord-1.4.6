using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001EF RID: 495
	public static class ItemPhysicsSoundContainer
	{
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x00062D41 File Offset: 0x00060F41
		// (set) Token: 0x06001CEF RID: 7407 RVA: 0x00062D48 File Offset: 0x00060F48
		public static int SoundCodePhysicsBoulderDefault { get; private set; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x00062D50 File Offset: 0x00060F50
		// (set) Token: 0x06001CF1 RID: 7409 RVA: 0x00062D57 File Offset: 0x00060F57
		public static int SoundCodePhysicsArrowlikeDefault { get; private set; }

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x00062D5F File Offset: 0x00060F5F
		// (set) Token: 0x06001CF3 RID: 7411 RVA: 0x00062D66 File Offset: 0x00060F66
		public static int SoundCodePhysicsBowlikeDefault { get; private set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x00062D6E File Offset: 0x00060F6E
		// (set) Token: 0x06001CF5 RID: 7413 RVA: 0x00062D75 File Offset: 0x00060F75
		public static int SoundCodePhysicsDaggerlikeDefault { get; private set; }

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x00062D7D File Offset: 0x00060F7D
		// (set) Token: 0x06001CF7 RID: 7415 RVA: 0x00062D84 File Offset: 0x00060F84
		public static int SoundCodePhysicsGreatswordlikeDefault { get; private set; }

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x00062D8C File Offset: 0x00060F8C
		// (set) Token: 0x06001CF9 RID: 7417 RVA: 0x00062D93 File Offset: 0x00060F93
		public static int SoundCodePhysicsShieldlikeDefault { get; private set; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x00062D9B File Offset: 0x00060F9B
		// (set) Token: 0x06001CFB RID: 7419 RVA: 0x00062DA2 File Offset: 0x00060FA2
		public static int SoundCodePhysicsSpearlikeDefault { get; private set; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x00062DAA File Offset: 0x00060FAA
		// (set) Token: 0x06001CFD RID: 7421 RVA: 0x00062DB1 File Offset: 0x00060FB1
		public static int SoundCodePhysicsSwordlikeDefault { get; private set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x00062DB9 File Offset: 0x00060FB9
		// (set) Token: 0x06001CFF RID: 7423 RVA: 0x00062DC0 File Offset: 0x00060FC0
		public static int SoundCodePhysicsBoulderWood { get; private set; }

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x00062DC8 File Offset: 0x00060FC8
		// (set) Token: 0x06001D01 RID: 7425 RVA: 0x00062DCF File Offset: 0x00060FCF
		public static int SoundCodePhysicsArrowlikeWood { get; private set; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x00062DD7 File Offset: 0x00060FD7
		// (set) Token: 0x06001D03 RID: 7427 RVA: 0x00062DDE File Offset: 0x00060FDE
		public static int SoundCodePhysicsBowlikeWood { get; private set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x00062DE6 File Offset: 0x00060FE6
		// (set) Token: 0x06001D05 RID: 7429 RVA: 0x00062DED File Offset: 0x00060FED
		public static int SoundCodePhysicsDaggerlikeWood { get; private set; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x00062DF5 File Offset: 0x00060FF5
		// (set) Token: 0x06001D07 RID: 7431 RVA: 0x00062DFC File Offset: 0x00060FFC
		public static int SoundCodePhysicsGreatswordlikeWood { get; private set; }

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x00062E04 File Offset: 0x00061004
		// (set) Token: 0x06001D09 RID: 7433 RVA: 0x00062E0B File Offset: 0x0006100B
		public static int SoundCodePhysicsShieldlikeWood { get; private set; }

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x00062E13 File Offset: 0x00061013
		// (set) Token: 0x06001D0B RID: 7435 RVA: 0x00062E1A File Offset: 0x0006101A
		public static int SoundCodePhysicsSpearlikeWood { get; private set; }

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x00062E22 File Offset: 0x00061022
		// (set) Token: 0x06001D0D RID: 7437 RVA: 0x00062E29 File Offset: 0x00061029
		public static int SoundCodePhysicsSwordlikeWood { get; private set; }

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x00062E31 File Offset: 0x00061031
		// (set) Token: 0x06001D0F RID: 7439 RVA: 0x00062E38 File Offset: 0x00061038
		public static int SoundCodePhysicsBoulderStone { get; private set; }

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x00062E40 File Offset: 0x00061040
		// (set) Token: 0x06001D11 RID: 7441 RVA: 0x00062E47 File Offset: 0x00061047
		public static int SoundCodePhysicsArrowlikeStone { get; private set; }

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x00062E4F File Offset: 0x0006104F
		// (set) Token: 0x06001D13 RID: 7443 RVA: 0x00062E56 File Offset: 0x00061056
		public static int SoundCodePhysicsBowlikeStone { get; private set; }

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x00062E5E File Offset: 0x0006105E
		// (set) Token: 0x06001D15 RID: 7445 RVA: 0x00062E65 File Offset: 0x00061065
		public static int SoundCodePhysicsDaggerlikeStone { get; private set; }

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x00062E6D File Offset: 0x0006106D
		// (set) Token: 0x06001D17 RID: 7447 RVA: 0x00062E74 File Offset: 0x00061074
		public static int SoundCodePhysicsGreatswordlikeStone { get; private set; }

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x00062E7C File Offset: 0x0006107C
		// (set) Token: 0x06001D19 RID: 7449 RVA: 0x00062E83 File Offset: 0x00061083
		public static int SoundCodePhysicsShieldlikeStone { get; private set; }

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x00062E8B File Offset: 0x0006108B
		// (set) Token: 0x06001D1B RID: 7451 RVA: 0x00062E92 File Offset: 0x00061092
		public static int SoundCodePhysicsSpearlikeStone { get; private set; }

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x00062E9A File Offset: 0x0006109A
		// (set) Token: 0x06001D1D RID: 7453 RVA: 0x00062EA1 File Offset: 0x000610A1
		public static int SoundCodePhysicsSwordlikeStone { get; private set; }

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x00062EA9 File Offset: 0x000610A9
		// (set) Token: 0x06001D1F RID: 7455 RVA: 0x00062EB0 File Offset: 0x000610B0
		public static int SoundCodePhysicsWater { get; private set; }

		// Token: 0x06001D20 RID: 7456 RVA: 0x00062EB8 File Offset: 0x000610B8
		static ItemPhysicsSoundContainer()
		{
			ItemPhysicsSoundContainer.UpdateItemPhysicsSoundCodes();
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x00062EC0 File Offset: 0x000610C0
		private static void UpdateItemPhysicsSoundCodes()
		{
			ItemPhysicsSoundContainer.SoundCodePhysicsBoulderDefault = SoundEvent.GetEventIdFromString("event:/physics/boulder/default");
			ItemPhysicsSoundContainer.SoundCodePhysicsArrowlikeDefault = SoundEvent.GetEventIdFromString("event:/physics/arrowlike/default");
			ItemPhysicsSoundContainer.SoundCodePhysicsBowlikeDefault = SoundEvent.GetEventIdFromString("event:/physics/bowlike/default");
			ItemPhysicsSoundContainer.SoundCodePhysicsDaggerlikeDefault = SoundEvent.GetEventIdFromString("event:/physics/daggerlike/default");
			ItemPhysicsSoundContainer.SoundCodePhysicsGreatswordlikeDefault = SoundEvent.GetEventIdFromString("event:/physics/greatswordlike/default");
			ItemPhysicsSoundContainer.SoundCodePhysicsShieldlikeDefault = SoundEvent.GetEventIdFromString("event:/physics/shieldlike/default");
			ItemPhysicsSoundContainer.SoundCodePhysicsSpearlikeDefault = SoundEvent.GetEventIdFromString("event:/physics/spearlike/default");
			ItemPhysicsSoundContainer.SoundCodePhysicsSwordlikeDefault = SoundEvent.GetEventIdFromString("event:/physics/swordlike/default");
			ItemPhysicsSoundContainer.SoundCodePhysicsBoulderWood = SoundEvent.GetEventIdFromString("event:/physics/boulder/wood");
			ItemPhysicsSoundContainer.SoundCodePhysicsArrowlikeWood = SoundEvent.GetEventIdFromString("event:/physics/arrowlike/wood");
			ItemPhysicsSoundContainer.SoundCodePhysicsBowlikeWood = SoundEvent.GetEventIdFromString("event:/physics/bowlike/wood");
			ItemPhysicsSoundContainer.SoundCodePhysicsDaggerlikeWood = SoundEvent.GetEventIdFromString("event:/physics/daggerlike/wood");
			ItemPhysicsSoundContainer.SoundCodePhysicsGreatswordlikeWood = SoundEvent.GetEventIdFromString("event:/physics/greatswordlike/wood");
			ItemPhysicsSoundContainer.SoundCodePhysicsShieldlikeWood = SoundEvent.GetEventIdFromString("event:/physics/shieldlike/wood");
			ItemPhysicsSoundContainer.SoundCodePhysicsSpearlikeWood = SoundEvent.GetEventIdFromString("event:/physics/spearlike/wood");
			ItemPhysicsSoundContainer.SoundCodePhysicsSwordlikeWood = SoundEvent.GetEventIdFromString("event:/physics/swordlike/wood");
			ItemPhysicsSoundContainer.SoundCodePhysicsBoulderStone = SoundEvent.GetEventIdFromString("event:/physics/boulder/stone");
			ItemPhysicsSoundContainer.SoundCodePhysicsArrowlikeStone = SoundEvent.GetEventIdFromString("event:/physics/arrowlike/stone");
			ItemPhysicsSoundContainer.SoundCodePhysicsBowlikeStone = SoundEvent.GetEventIdFromString("event:/physics/bowlike/stone");
			ItemPhysicsSoundContainer.SoundCodePhysicsDaggerlikeStone = SoundEvent.GetEventIdFromString("event:/physics/daggerlike/stone");
			ItemPhysicsSoundContainer.SoundCodePhysicsGreatswordlikeStone = SoundEvent.GetEventIdFromString("event:/physics/greatswordlike/stone");
			ItemPhysicsSoundContainer.SoundCodePhysicsShieldlikeStone = SoundEvent.GetEventIdFromString("event:/physics/shieldlike/stone");
			ItemPhysicsSoundContainer.SoundCodePhysicsSpearlikeStone = SoundEvent.GetEventIdFromString("event:/physics/spearlike/stone");
			ItemPhysicsSoundContainer.SoundCodePhysicsSwordlikeStone = SoundEvent.GetEventIdFromString("event:/physics/swordlike/stone");
			ItemPhysicsSoundContainer.SoundCodePhysicsWater = SoundEvent.GetEventIdFromString("event:/physics/water");
		}
	}
}
