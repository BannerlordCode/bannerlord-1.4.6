using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Objects;

namespace SandBox.Missions.MissionEvents
{
	// Token: 0x0200009E RID: 158
	public class ShowQuickInformationEventListenerLogic : MissionLogic
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x0002CA21 File Offset: 0x0002AC21
		public ShowQuickInformationEventListenerLogic()
		{
			Game.Current.EventManager.RegisterEvent<GenericMissionEvent>(new Action<GenericMissionEvent>(this.OnGenericMissionEventTriggered));
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0002CA44 File Offset: 0x0002AC44
		protected override void OnEndMission()
		{
			Game.Current.EventManager.UnregisterEvent<GenericMissionEvent>(new Action<GenericMissionEvent>(this.OnGenericMissionEventTriggered));
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0002CA64 File Offset: 0x0002AC64
		private void OnGenericMissionEventTriggered(GenericMissionEvent missionEvent)
		{
			if (missionEvent.EventId == "show_quick_information_event")
			{
				string[] array = missionEvent.Parameter.Split(new char[] { ' ' });
				SandBoxHelpers.MissionHelper.DisableGenericMissionEventScript(array[0], missionEvent);
				MBInformationManager.AddQuickInformation(GameTexts.FindText(array[1], null), 0, null, null, "");
			}
		}

		// Token: 0x04000390 RID: 912
		private const string ShowQuickInformationEventId = "show_quick_information_event";
	}
}
