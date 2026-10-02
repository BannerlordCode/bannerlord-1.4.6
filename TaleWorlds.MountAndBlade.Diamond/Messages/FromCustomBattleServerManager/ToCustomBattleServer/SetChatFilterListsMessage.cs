using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromCustomBattleServerManager.ToCustomBattleServer
{
	// Token: 0x02000012 RID: 18
	[MessageDescription("CustomBattleServerManager", "CustomBattleServer", true)]
	[DataContract]
	[Serializable]
	public class SetChatFilterListsMessage : Message
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002688 File Offset: 0x00000888
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002690 File Offset: 0x00000890
		[JsonProperty]
		public string[] ProfanityList { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002699 File Offset: 0x00000899
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000026A1 File Offset: 0x000008A1
		[JsonProperty]
		public string[] AllowList { get; private set; }

		// Token: 0x0600007E RID: 126 RVA: 0x000026AA File Offset: 0x000008AA
		public SetChatFilterListsMessage()
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000026B2 File Offset: 0x000008B2
		public SetChatFilterListsMessage(string[] profanityList, string[] allowList)
		{
			this.ProfanityList = profanityList;
			this.AllowList = allowList;
		}
	}
}
