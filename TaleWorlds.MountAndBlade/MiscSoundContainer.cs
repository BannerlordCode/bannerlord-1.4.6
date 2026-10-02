using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001F0 RID: 496
	public static class MiscSoundContainer
	{
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x00063044 File Offset: 0x00061244
		// (set) Token: 0x06001D23 RID: 7459 RVA: 0x0006304B File Offset: 0x0006124B
		public static int SoundCodeMovementFoleyDoorOpen { get; private set; }

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x00063053 File Offset: 0x00061253
		// (set) Token: 0x06001D25 RID: 7461 RVA: 0x0006305A File Offset: 0x0006125A
		public static int SoundCodeMovementFoleyDoorClose { get; private set; }

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001D26 RID: 7462 RVA: 0x00063062 File Offset: 0x00061262
		// (set) Token: 0x06001D27 RID: 7463 RVA: 0x00063069 File Offset: 0x00061269
		public static int SoundCodeAmbientNodeSiegeBallistaFire { get; private set; }

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001D28 RID: 7464 RVA: 0x00063071 File Offset: 0x00061271
		// (set) Token: 0x06001D29 RID: 7465 RVA: 0x00063078 File Offset: 0x00061278
		public static int SoundCodeAmbientNodeSiegeMangonelFire { get; private set; }

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x00063080 File Offset: 0x00061280
		// (set) Token: 0x06001D2B RID: 7467 RVA: 0x00063087 File Offset: 0x00061287
		public static int SoundCodeAmbientNodeSiegeTrebuchetFire { get; private set; }

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001D2C RID: 7468 RVA: 0x0006308F File Offset: 0x0006128F
		// (set) Token: 0x06001D2D RID: 7469 RVA: 0x00063096 File Offset: 0x00061296
		public static int SoundCodeAmbientNodeSiegeBallistaHit { get; private set; }

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001D2E RID: 7470 RVA: 0x0006309E File Offset: 0x0006129E
		// (set) Token: 0x06001D2F RID: 7471 RVA: 0x000630A5 File Offset: 0x000612A5
		public static int SoundCodeAmbientNodeSiegeBoulderHit { get; private set; }

		// Token: 0x06001D30 RID: 7472 RVA: 0x000630AD File Offset: 0x000612AD
		static MiscSoundContainer()
		{
			MiscSoundContainer.UpdateMiscSoundCodes();
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x000630B4 File Offset: 0x000612B4
		private static void UpdateMiscSoundCodes()
		{
			MiscSoundContainer.SoundCodeMovementFoleyDoorOpen = SoundEvent.GetEventIdFromString("event:/mission/movement/foley/door_open");
			MiscSoundContainer.SoundCodeMovementFoleyDoorClose = SoundEvent.GetEventIdFromString("event:/mission/movement/foley/door_close");
			MiscSoundContainer.SoundCodeAmbientNodeSiegeBallistaFire = SoundEvent.GetEventIdFromString("event:/map/ambient/node/siege/ballista_fire");
			MiscSoundContainer.SoundCodeAmbientNodeSiegeMangonelFire = SoundEvent.GetEventIdFromString("event:/map/ambient/node/siege/mangonel_fire");
			MiscSoundContainer.SoundCodeAmbientNodeSiegeTrebuchetFire = SoundEvent.GetEventIdFromString("event:/map/ambient/node/siege/trebuchet_fire");
			MiscSoundContainer.SoundCodeAmbientNodeSiegeBallistaHit = SoundEvent.GetEventIdFromString("event:/map/ambient/node/siege/ballista_hit");
			MiscSoundContainer.SoundCodeAmbientNodeSiegeBoulderHit = SoundEvent.GetEventIdFromString("event:/map/ambient/node/siege/boulder_hit");
		}
	}
}
