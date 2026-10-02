using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002F3 RID: 755
	public class BaseNetworkComponentData : UdpNetworkComponent
	{
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002B65 RID: 11109 RVA: 0x000A70CA File Offset: 0x000A52CA
		// (set) Token: 0x06002B66 RID: 11110 RVA: 0x000A70D2 File Offset: 0x000A52D2
		public int CurrentBattleIndex { get; private set; }

		// Token: 0x06002B67 RID: 11111 RVA: 0x000A70DB File Offset: 0x000A52DB
		public void UpdateCurrentBattleIndex(int currentBattleIndex)
		{
			this.CurrentBattleIndex = currentBattleIndex;
		}

		// Token: 0x040010D7 RID: 4311
		public const float MaxIntermissionStateTime = 240f;
	}
}
