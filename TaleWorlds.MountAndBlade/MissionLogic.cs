using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000290 RID: 656
	public abstract class MissionLogic : MissionBehavior
	{
		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x00084906 File Offset: 0x00082B06
		public override MissionBehaviorType BehaviorType
		{
			get
			{
				return MissionBehaviorType.Logic;
			}
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x00084909 File Offset: 0x00082B09
		public virtual InquiryData OnEndMissionRequest(out bool canLeave)
		{
			canLeave = true;
			return null;
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x0008490F File Offset: 0x00082B0F
		public virtual bool MissionEnded(ref MissionResult missionResult)
		{
			return false;
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x00084912 File Offset: 0x00082B12
		public virtual void OnBattleEnded()
		{
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x00084914 File Offset: 0x00082B14
		public virtual void ShowBattleResults()
		{
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x00084916 File Offset: 0x00082B16
		public virtual void OnRetreatMission()
		{
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x00084918 File Offset: 0x00082B18
		public virtual void OnSurrenderMission()
		{
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x0008491A File Offset: 0x00082B1A
		public virtual void OnAutoDeployTeam(Team team)
		{
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x0008491C File Offset: 0x00082B1C
		public virtual List<EquipmentElement> GetExtraEquipmentElementsForCharacter(BasicCharacterObject character, bool getAllEquipments = false)
		{
			return null;
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x0008491F File Offset: 0x00082B1F
		public virtual void OnMissionResultReady(MissionResult missionResult)
		{
		}
	}
}
