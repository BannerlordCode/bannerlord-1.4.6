using System;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000158 RID: 344
	[Serializable]
	public class ServerNotification
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x0000ED17 File Offset: 0x0000CF17
		public ServerNotificationType Type { get; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0000ED1F File Offset: 0x0000CF1F
		public string Message { get; }

		// Token: 0x06000993 RID: 2451 RVA: 0x0000ED27 File Offset: 0x0000CF27
		public ServerNotification(ServerNotificationType type, string message)
		{
			this.Type = type;
			this.Message = message;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0000ED40 File Offset: 0x0000CF40
		public TextObject GetTextObjectOfMessage()
		{
			TextObject textObject;
			if (!GameTexts.TryGetText(this.Message, out textObject, null))
			{
				textObject = new TextObject("{=!}" + this.Message, null);
			}
			return textObject;
		}
	}
}
