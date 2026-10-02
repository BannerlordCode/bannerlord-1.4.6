using System;

namespace TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer
{
	// Token: 0x02000092 RID: 146
	public class MissionConversationView : MissionView
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00026CE4 File Offset: 0x00024EE4
		public static MissionConversationView Current
		{
			get
			{
				return Mission.Current.GetMissionBehavior<MissionConversationView>();
			}
		}
	}
}
