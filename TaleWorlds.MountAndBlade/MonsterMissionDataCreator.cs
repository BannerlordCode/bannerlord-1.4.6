using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002DD RID: 733
	public class MonsterMissionDataCreator : IMonsterMissionDataCreator
	{
		// Token: 0x06002A9B RID: 10907 RVA: 0x000A3F46 File Offset: 0x000A2146
		IMonsterMissionData IMonsterMissionDataCreator.CreateMonsterMissionData(Monster monster)
		{
			return new MonsterMissionData(monster);
		}
	}
}
