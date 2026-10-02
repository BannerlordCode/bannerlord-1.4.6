using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200031F RID: 799
	public class NetworkCommunication : INetworkCommunication
	{
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x000AFEF7 File Offset: 0x000AE0F7
		VirtualPlayer INetworkCommunication.MyPeer
		{
			get
			{
				NetworkCommunicator myPeer = GameNetwork.MyPeer;
				if (myPeer == null)
				{
					return null;
				}
				return myPeer.VirtualPlayer;
			}
		}
	}
}
