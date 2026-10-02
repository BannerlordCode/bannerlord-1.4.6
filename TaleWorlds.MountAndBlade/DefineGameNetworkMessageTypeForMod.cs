using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002ED RID: 749
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefineGameNetworkMessageTypeForMod : Attribute
	{
		// Token: 0x06002AE5 RID: 10981 RVA: 0x000A507D File Offset: 0x000A327D
		public DefineGameNetworkMessageTypeForMod(GameNetworkMessageSendType sendType)
		{
			this.SendType = sendType;
		}

		// Token: 0x040010BD RID: 4285
		public readonly GameNetworkMessageSendType SendType;
	}
}
