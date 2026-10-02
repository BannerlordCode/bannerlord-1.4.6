using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000274 RID: 628
	public class BasicLeaveMissionLogic : MissionLogic
	{
		// Token: 0x06002332 RID: 9010 RVA: 0x0007CD7B File Offset: 0x0007AF7B
		public BasicLeaveMissionLogic()
			: this(false)
		{
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x0007CD84 File Offset: 0x0007AF84
		public BasicLeaveMissionLogic(bool askBeforeLeave)
			: this(askBeforeLeave, 5)
		{
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x0007CD8E File Offset: 0x0007AF8E
		public BasicLeaveMissionLogic(bool askBeforeLeave, int minRetreatDistance)
		{
			this._askBeforeLeave = askBeforeLeave;
			this._minRetreatDistance = minRetreatDistance;
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x0007CDA4 File Offset: 0x0007AFA4
		public override bool MissionEnded(ref MissionResult missionResult)
		{
			return base.Mission.MainAgent != null && !base.Mission.MainAgent.IsActive();
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x0007CDC8 File Offset: 0x0007AFC8
		public override InquiryData OnEndMissionRequest(out bool canPlayerLeave)
		{
			canPlayerLeave = true;
			if (base.Mission.MainAgent != null && base.Mission.MainAgent.IsActive() && (float)this._minRetreatDistance > 0f && base.Mission.IsPlayerCloseToAnEnemy((float)this._minRetreatDistance))
			{
				canPlayerLeave = false;
				MBInformationManager.AddQuickInformation(GameTexts.FindText("str_can_not_retreat", null), 0, null, null, "");
			}
			else if (this._askBeforeLeave)
			{
				return new InquiryData("", GameTexts.FindText("str_give_up_fight", null).ToString(), true, true, GameTexts.FindText("str_ok", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), new Action(base.Mission.OnEndMissionResult), null, "", 0f, null, null, null);
			}
			return null;
		}

		// Token: 0x04000D7E RID: 3454
		private readonly bool _askBeforeLeave;

		// Token: 0x04000D7F RID: 3455
		private readonly int _minRetreatDistance;
	}
}
