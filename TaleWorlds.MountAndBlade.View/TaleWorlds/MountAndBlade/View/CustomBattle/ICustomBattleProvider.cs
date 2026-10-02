using System;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.View.CustomBattle
{
	// Token: 0x020000AC RID: 172
	public interface ICustomBattleProvider
	{
		// Token: 0x060005E8 RID: 1512
		void StartCustomBattle();

		// Token: 0x060005E9 RID: 1513
		TextObject GetName();
	}
}
