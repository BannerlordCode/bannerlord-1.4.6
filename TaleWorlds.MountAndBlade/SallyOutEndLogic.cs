using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000296 RID: 662
	public class SallyOutEndLogic : MissionLogic
	{
		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060024A8 RID: 9384 RVA: 0x00084D05 File Offset: 0x00082F05
		// (set) Token: 0x060024A9 RID: 9385 RVA: 0x00084D0D File Offset: 0x00082F0D
		public bool IsSallyOutOver { get; private set; }

		// Token: 0x060024AA RID: 9386 RVA: 0x00084D18 File Offset: 0x00082F18
		public override void OnMissionTick(float dt)
		{
			if (this.CheckTimer(dt))
			{
				if (this._checkState == SallyOutEndLogic.EndConditionCheckState.Deactive)
				{
					using (IEnumerator<Team> enumerator = base.Mission.Teams.Where<Team>((Team t) => t.Side == BattleSideEnum.Defender).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Team team = enumerator.Current;
							foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
							{
								if (formation.CountOfUnits > 0 && formation.CountOfUnits > 0 && !TeamAISiegeComponent.IsFormationInsideCastle(formation, true, 0.1f))
								{
									this._checkState = SallyOutEndLogic.EndConditionCheckState.Active;
									return;
								}
							}
						}
						return;
					}
				}
				if (this._checkState == SallyOutEndLogic.EndConditionCheckState.Idle)
				{
					this._checkState = SallyOutEndLogic.EndConditionCheckState.Active;
				}
			}
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x00084E10 File Offset: 0x00083010
		public override bool MissionEnded(ref MissionResult missionResult)
		{
			if (this.IsSallyOutOver)
			{
				missionResult = MissionResult.CreateSuccessful(base.Mission, false);
				return true;
			}
			if (this._checkState != SallyOutEndLogic.EndConditionCheckState.Active)
			{
				return false;
			}
			foreach (Team team in base.Mission.Teams)
			{
				BattleSideEnum side = team.Side;
				if (side != BattleSideEnum.Defender)
				{
					if (side == BattleSideEnum.Attacker && TeamAISiegeComponent.IsFormationGroupInsideCastle(team.FormationsIncludingSpecialAndEmpty, false, 0.1f))
					{
						this._checkState = SallyOutEndLogic.EndConditionCheckState.Idle;
						return false;
					}
				}
				else if (team.FormationsIncludingEmpty.Any<Formation>((Formation f) => f.CountOfUnits > 0 && !TeamAISiegeComponent.IsFormationInsideCastle(f, false, 0.9f)))
				{
					this._checkState = SallyOutEndLogic.EndConditionCheckState.Idle;
					return false;
				}
			}
			this.IsSallyOutOver = true;
			missionResult = MissionResult.CreateSuccessful(base.Mission, false);
			return true;
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x00084F00 File Offset: 0x00083100
		private bool CheckTimer(float dt)
		{
			this._dtSum += dt;
			if (this._dtSum < this._nextCheckTime)
			{
				return false;
			}
			this._dtSum = 0f;
			this._nextCheckTime = 0.8f + MBRandom.RandomFloat * 0.4f;
			return true;
		}

		// Token: 0x04000E24 RID: 3620
		private SallyOutEndLogic.EndConditionCheckState _checkState;

		// Token: 0x04000E26 RID: 3622
		private float _nextCheckTime;

		// Token: 0x04000E27 RID: 3623
		private float _dtSum;

		// Token: 0x02000569 RID: 1385
		private enum EndConditionCheckState
		{
			// Token: 0x04001E20 RID: 7712
			Deactive,
			// Token: 0x04001E21 RID: 7713
			Active,
			// Token: 0x04001E22 RID: 7714
			Idle
		}
	}
}
