using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000319 RID: 793
	internal class MBNetworkPeer : DotNetObject
	{
		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002D31 RID: 11569 RVA: 0x000AF543 File Offset: 0x000AD743
		public NetworkCommunicator NetworkPeer { get; }

		// Token: 0x06002D32 RID: 11570 RVA: 0x000AF54B File Offset: 0x000AD74B
		internal MBNetworkPeer(NetworkCommunicator networkPeer)
		{
			this.NetworkPeer = networkPeer;
		}
	}
}
