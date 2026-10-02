using System;
using System.Runtime.Serialization;
using TaleWorlds.Diamond;

namespace Messages.FromCustomBattleServerManager.ToCustomBattleServer
{
	// Token: 0x0200000F RID: 15
	[MessageDescription("CustomBattleServerManager", "CustomBattleServer", true)]
	[DataContract]
	[Serializable]
	public class CustomBattleServerReadyResponseMessage : LoginResultObject
	{
	}
}
