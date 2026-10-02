using System;

namespace SandBox.View.Conversation
{
	// Token: 0x02000078 RID: 120
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class ConversationViewEventHandler : Attribute
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00027C69 File Offset: 0x00025E69
		public string Id { get; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00027C71 File Offset: 0x00025E71
		public ConversationViewEventHandler.EventType Type { get; }

		// Token: 0x0600053F RID: 1343 RVA: 0x00027C79 File Offset: 0x00025E79
		public ConversationViewEventHandler(string id, ConversationViewEventHandler.EventType type)
		{
			this.Id = id;
			this.Type = type;
		}

		// Token: 0x020000CB RID: 203
		public enum EventType
		{
			// Token: 0x040003D2 RID: 978
			OnCondition,
			// Token: 0x040003D3 RID: 979
			OnConsequence
		}
	}
}
