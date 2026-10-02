using System;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Missions.Hints
{
	// Token: 0x020003EE RID: 1006
	public class MissionHint
	{
		// Token: 0x06003734 RID: 14132 RVA: 0x000E4865 File Offset: 0x000E2A65
		public MissionHint(TextObject description)
		{
			this.Description = description;
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000E4874 File Offset: 0x000E2A74
		public static MissionHint CreateWithKeyAndAction(TextObject actionText, string hotKeyId)
		{
			TextObject textObject = GameTexts.FindText("str_key_action", null).CopyTextObject();
			textObject.SetTextVariable("KEY", HyperlinkTexts.GetKeyHyperlinkText(hotKeyId, 1f));
			textObject.SetTextVariable("ACTION", actionText);
			return new MissionHint(textObject);
		}

		// Token: 0x040017B9 RID: 6073
		public readonly TextObject Description;
	}
}
