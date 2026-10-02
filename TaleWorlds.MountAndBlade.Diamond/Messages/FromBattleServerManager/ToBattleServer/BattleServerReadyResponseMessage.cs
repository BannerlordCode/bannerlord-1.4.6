using System;
using System.Runtime.Serialization;
using TaleWorlds.Diamond;

namespace Messages.FromBattleServerManager.ToBattleServer
{
	// Token: 0x020000DE RID: 222
	[MessageDescription("BattleServerManager", "BattleServer", true)]
	[DataContract]
	[Serializable]
	public class BattleServerReadyResponseMessage : LoginResultObject
	{
	}
}
