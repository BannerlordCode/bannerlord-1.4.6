using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002CA RID: 714
	public class PeerVisualsHolder
	{
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x0009AD94 File Offset: 0x00098F94
		// (set) Token: 0x06002941 RID: 10561 RVA: 0x0009AD9C File Offset: 0x00098F9C
		public MissionPeer Peer { get; private set; }

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002942 RID: 10562 RVA: 0x0009ADA5 File Offset: 0x00098FA5
		// (set) Token: 0x06002943 RID: 10563 RVA: 0x0009ADAD File Offset: 0x00098FAD
		public int VisualsIndex { get; private set; }

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002944 RID: 10564 RVA: 0x0009ADB6 File Offset: 0x00098FB6
		// (set) Token: 0x06002945 RID: 10565 RVA: 0x0009ADBE File Offset: 0x00098FBE
		public IAgentVisual AgentVisuals { get; private set; }

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x0009ADC7 File Offset: 0x00098FC7
		// (set) Token: 0x06002947 RID: 10567 RVA: 0x0009ADCF File Offset: 0x00098FCF
		public IAgentVisual MountAgentVisuals { get; private set; }

		// Token: 0x06002948 RID: 10568 RVA: 0x0009ADD8 File Offset: 0x00098FD8
		public PeerVisualsHolder(MissionPeer peer, int index, IAgentVisual agentVisuals, IAgentVisual mountVisuals)
		{
			this.Peer = peer;
			this.VisualsIndex = index;
			this.AgentVisuals = agentVisuals;
			this.MountAgentVisuals = mountVisuals;
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x0009ADFD File Offset: 0x00098FFD
		public void SetMountVisuals(IAgentVisual mountAgentVisuals)
		{
			this.MountAgentVisuals = mountAgentVisuals;
		}
	}
}
