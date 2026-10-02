using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000125 RID: 293
	[Serializable]
	public class AnotherPlayerData
	{
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0000B70B File Offset: 0x0000990B
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x0000B713 File Offset: 0x00009913
		public AnotherPlayerState PlayerState { get; set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0000B71C File Offset: 0x0000991C
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x0000B724 File Offset: 0x00009924
		public int Experience { get; set; }

		// Token: 0x06000794 RID: 1940 RVA: 0x0000B72D File Offset: 0x0000992D
		public AnotherPlayerData()
		{
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0000B735 File Offset: 0x00009935
		public AnotherPlayerData(AnotherPlayerState anotherPlayerState, int anotherPlayerExperience)
		{
			this.PlayerState = anotherPlayerState;
			this.Experience = anotherPlayerExperience;
		}
	}
}
