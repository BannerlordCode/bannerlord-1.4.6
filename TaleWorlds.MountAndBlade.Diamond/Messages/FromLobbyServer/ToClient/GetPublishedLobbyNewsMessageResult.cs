using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	public class GetPublishedLobbyNewsMessageResult : FunctionResult
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00002E9A File Offset: 0x0000109A
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00002EA2 File Offset: 0x000010A2
		[JsonProperty]
		public PublishedLobbyNewsArticle[] Content { get; private set; }

		// Token: 0x06000145 RID: 325 RVA: 0x00002EAB File Offset: 0x000010AB
		public GetPublishedLobbyNewsMessageResult()
		{
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00002EB3 File Offset: 0x000010B3
		public GetPublishedLobbyNewsMessageResult(PublishedLobbyNewsArticle[] content)
		{
			this.Content = content;
		}
	}
}
