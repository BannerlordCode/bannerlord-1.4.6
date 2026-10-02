using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200012E RID: 302
	[Serializable]
	public class LobbyNotification
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0000BB57 File Offset: 0x00009D57
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x0000BB5F File Offset: 0x00009D5F
		public int Id { get; set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0000BB68 File Offset: 0x00009D68
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x0000BB70 File Offset: 0x00009D70
		public NotificationType Type { get; set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0000BB79 File Offset: 0x00009D79
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x0000BB81 File Offset: 0x00009D81
		public DateTime Date { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0000BB8A File Offset: 0x00009D8A
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x0000BB92 File Offset: 0x00009D92
		public string Message { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0000BB9B File Offset: 0x00009D9B
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x0000BBA3 File Offset: 0x00009DA3
		public Dictionary<string, string> Parameters { get; set; }

		// Token: 0x06000829 RID: 2089 RVA: 0x0000BBAC File Offset: 0x00009DAC
		public LobbyNotification()
		{
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0000BBB4 File Offset: 0x00009DB4
		public LobbyNotification(NotificationType type, DateTime date, string message)
		{
			this.Id = -1;
			this.Type = type;
			this.Date = date;
			this.Message = message;
			this.Parameters = new Dictionary<string, string>();
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		public LobbyNotification(int id, NotificationType type, DateTime date, string message, string serializedParameters)
		{
			this.Id = id;
			this.Type = type;
			this.Date = date;
			this.Message = message;
			try
			{
				this.Parameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedParameters);
			}
			catch (Exception)
			{
				this.Parameters = new Dictionary<string, string>();
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0000BC44 File Offset: 0x00009E44
		public string GetParametersAsString()
		{
			string text = "{}";
			try
			{
				text = JsonConvert.SerializeObject(this.Parameters, Formatting.None);
			}
			catch (Exception)
			{
			}
			return text;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0000BC7C File Offset: 0x00009E7C
		public TextObject GetTextObjectOfMessage()
		{
			TextObject textObject;
			if (!GameTexts.TryGetText(this.Message, out textObject, null))
			{
				textObject = new TextObject("{=!}" + this.Message, null);
			}
			return textObject;
		}

		// Token: 0x04000343 RID: 835
		public const string BadgeIdParameterName = "badge_id";

		// Token: 0x04000344 RID: 836
		public const string FriendRequesterParameterName = "friend_requester";
	}
}
