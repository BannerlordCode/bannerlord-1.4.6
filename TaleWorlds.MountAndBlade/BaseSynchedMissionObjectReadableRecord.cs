using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000363 RID: 867
	[DefineSynchedMissionObjectType(typeof(SynchedMissionObject))]
	public struct BaseSynchedMissionObjectReadableRecord
	{
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060031B6 RID: 12726 RVA: 0x000CB381 File Offset: 0x000C9581
		// (set) Token: 0x060031B7 RID: 12727 RVA: 0x000CB389 File Offset: 0x000C9589
		public bool SetVisibilityExcludeParents { get; private set; }

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060031B8 RID: 12728 RVA: 0x000CB392 File Offset: 0x000C9592
		// (set) Token: 0x060031B9 RID: 12729 RVA: 0x000CB39A File Offset: 0x000C959A
		public bool SynchTransform { get; private set; }

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060031BA RID: 12730 RVA: 0x000CB3A3 File Offset: 0x000C95A3
		// (set) Token: 0x060031BB RID: 12731 RVA: 0x000CB3AB File Offset: 0x000C95AB
		public MatrixFrame GameObjectFrame { get; private set; }

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060031BC RID: 12732 RVA: 0x000CB3B4 File Offset: 0x000C95B4
		// (set) Token: 0x060031BD RID: 12733 RVA: 0x000CB3BC File Offset: 0x000C95BC
		public bool SynchronizeFrameOverTime { get; private set; }

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060031BE RID: 12734 RVA: 0x000CB3C5 File Offset: 0x000C95C5
		// (set) Token: 0x060031BF RID: 12735 RVA: 0x000CB3CD File Offset: 0x000C95CD
		public MatrixFrame LastSynchedFrame { get; private set; }

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060031C0 RID: 12736 RVA: 0x000CB3D6 File Offset: 0x000C95D6
		// (set) Token: 0x060031C1 RID: 12737 RVA: 0x000CB3DE File Offset: 0x000C95DE
		public float Duration { get; private set; }

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x000CB3E7 File Offset: 0x000C95E7
		// (set) Token: 0x060031C3 RID: 12739 RVA: 0x000CB3EF File Offset: 0x000C95EF
		public bool HasSkeleton { get; private set; }

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x000CB3F8 File Offset: 0x000C95F8
		// (set) Token: 0x060031C5 RID: 12741 RVA: 0x000CB400 File Offset: 0x000C9600
		public bool SynchAnimation { get; private set; }

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x000CB409 File Offset: 0x000C9609
		// (set) Token: 0x060031C7 RID: 12743 RVA: 0x000CB411 File Offset: 0x000C9611
		public int AnimationIndex { get; private set; }

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060031C8 RID: 12744 RVA: 0x000CB41A File Offset: 0x000C961A
		// (set) Token: 0x060031C9 RID: 12745 RVA: 0x000CB422 File Offset: 0x000C9622
		public float AnimationSpeed { get; private set; }

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060031CA RID: 12746 RVA: 0x000CB42B File Offset: 0x000C962B
		// (set) Token: 0x060031CB RID: 12747 RVA: 0x000CB433 File Offset: 0x000C9633
		public float AnimationParameter { get; private set; }

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060031CC RID: 12748 RVA: 0x000CB43C File Offset: 0x000C963C
		// (set) Token: 0x060031CD RID: 12749 RVA: 0x000CB444 File Offset: 0x000C9644
		public bool IsSkeletonAnimationPaused { get; private set; }

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x060031CE RID: 12750 RVA: 0x000CB44D File Offset: 0x000C964D
		// (set) Token: 0x060031CF RID: 12751 RVA: 0x000CB455 File Offset: 0x000C9655
		public bool SynchColors { get; private set; }

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x000CB45E File Offset: 0x000C965E
		// (set) Token: 0x060031D1 RID: 12753 RVA: 0x000CB466 File Offset: 0x000C9666
		public uint Color { get; private set; }

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x060031D2 RID: 12754 RVA: 0x000CB46F File Offset: 0x000C966F
		// (set) Token: 0x060031D3 RID: 12755 RVA: 0x000CB477 File Offset: 0x000C9677
		public uint Color2 { get; private set; }

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x060031D4 RID: 12756 RVA: 0x000CB480 File Offset: 0x000C9680
		// (set) Token: 0x060031D5 RID: 12757 RVA: 0x000CB488 File Offset: 0x000C9688
		public bool IsDisabled { get; private set; }

		// Token: 0x060031D6 RID: 12758 RVA: 0x000CB494 File Offset: 0x000C9694
		public bool ReadFromNetwork(ref bool bufferReadValid)
		{
			this.SetVisibilityExcludeParents = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			this.SynchTransform = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			if (this.SynchTransform)
			{
				this.GameObjectFrame = GameNetworkMessage.ReadMatrixFrameFromPacket(ref bufferReadValid);
				this.SynchronizeFrameOverTime = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
				if (this.SynchronizeFrameOverTime)
				{
					this.LastSynchedFrame = GameNetworkMessage.ReadMatrixFrameFromPacket(ref bufferReadValid);
					this.Duration = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.FlagCapturePointDurationCompressionInfo, ref bufferReadValid);
				}
			}
			this.HasSkeleton = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			if (this.HasSkeleton)
			{
				this.SynchAnimation = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
				if (this.SynchAnimation)
				{
					this.AnimationIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.AnimationIndexCompressionInfo, ref bufferReadValid);
					this.AnimationSpeed = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.AnimationSpeedCompressionInfo, ref bufferReadValid);
					this.AnimationParameter = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.AnimationProgressCompressionInfo, ref bufferReadValid);
					this.IsSkeletonAnimationPaused = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
				}
			}
			this.SynchColors = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			if (this.SynchColors)
			{
				this.Color = GameNetworkMessage.ReadUintFromPacket(CompressionBasic.ColorCompressionInfo, ref bufferReadValid);
				this.Color2 = GameNetworkMessage.ReadUintFromPacket(CompressionBasic.ColorCompressionInfo, ref bufferReadValid);
			}
			this.IsDisabled = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			return bufferReadValid;
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000CB5A9 File Offset: 0x000C97A9
		public void SetSetVisibilityExcludeParents(bool visible)
		{
			this.SetVisibilityExcludeParents = visible;
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000CB5B4 File Offset: 0x000C97B4
		public static ValueTuple<BaseSynchedMissionObjectReadableRecord, ISynchedMissionObjectReadableRecord> CreateFromNetworkWithTypeIndex(int typeIndex)
		{
			bool flag = true;
			BaseSynchedMissionObjectReadableRecord baseSynchedMissionObjectReadableRecord = default(BaseSynchedMissionObjectReadableRecord);
			baseSynchedMissionObjectReadableRecord.ReadFromNetwork(ref flag);
			ISynchedMissionObjectReadableRecord synchedMissionObjectReadableRecord = null;
			if (typeIndex >= 0)
			{
				synchedMissionObjectReadableRecord = Activator.CreateInstance(GameNetwork.GetSynchedMissionObjectReadableRecordTypeFromIndex(typeIndex)) as ISynchedMissionObjectReadableRecord;
				synchedMissionObjectReadableRecord.ReadFromNetwork(ref flag);
			}
			return new ValueTuple<BaseSynchedMissionObjectReadableRecord, ISynchedMissionObjectReadableRecord>(baseSynchedMissionObjectReadableRecord, synchedMissionObjectReadableRecord);
		}
	}
}
