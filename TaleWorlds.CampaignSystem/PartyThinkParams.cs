using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000092 RID: 146
	public class PartyThinkParams
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x0005498A File Offset: 0x00052B8A
		public MBReadOnlyList<ValueTuple<AIBehaviorData, float>> AIBehaviorScores
		{
			get
			{
				return this._aiBehaviorScores;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x00054992 File Offset: 0x00052B92
		public MBReadOnlyList<MobileParty> PossibleArmyMembersUponArmyCreation
		{
			get
			{
				return this._possibleArmyMembersUponArmyCreation;
			}
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0005499A File Offset: 0x00052B9A
		public PartyThinkParams(MobileParty mobileParty)
		{
			this._aiBehaviorScores = new MBList<ValueTuple<AIBehaviorData, float>>(32);
			this._possibleArmyMembersUponArmyCreation = null;
			this.MobilePartyOf = mobileParty;
			this.WillGatherAnArmy = false;
			this.DoNotChangeBehavior = false;
			this.CurrentObjectiveValue = 0f;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x000549D8 File Offset: 0x00052BD8
		public void Reset(MobileParty mobileParty)
		{
			this._aiBehaviorScores.Clear();
			MBList<MobileParty> possibleArmyMembersUponArmyCreation = this._possibleArmyMembersUponArmyCreation;
			if (possibleArmyMembersUponArmyCreation != null)
			{
				possibleArmyMembersUponArmyCreation.Clear();
			}
			this.MobilePartyOf = mobileParty;
			this.WillGatherAnArmy = false;
			this.DoNotChangeBehavior = false;
			this.CurrentObjectiveValue = 0f;
			this.StrengthOfLordsWithoutArmy = 0f;
			this.StrengthOfLordsWithArmy = 0f;
			this.StrengthOfLordsAtSameClanWithoutArmy = 0f;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00054A44 File Offset: 0x00052C44
		public void Initialization()
		{
			this.StrengthOfLordsWithoutArmy = 0f;
			this.StrengthOfLordsWithArmy = 0f;
			this.StrengthOfLordsAtSameClanWithoutArmy = 0f;
			foreach (Hero hero in this.MobilePartyOf.MapFaction.Heroes)
			{
				if (hero.PartyBelongedTo != null)
				{
					MobileParty partyBelongedTo = hero.PartyBelongedTo;
					if (partyBelongedTo.Army != null)
					{
						this.StrengthOfLordsWithArmy += partyBelongedTo.Party.EstimatedStrength;
					}
					else
					{
						this.StrengthOfLordsWithoutArmy += partyBelongedTo.Party.EstimatedStrength;
						Clan clan = hero.Clan;
						Hero leaderHero = this.MobilePartyOf.LeaderHero;
						if (clan == ((leaderHero != null) ? leaderHero.Clan : null))
						{
							this.StrengthOfLordsAtSameClanWithoutArmy += partyBelongedTo.Party.EstimatedStrength;
						}
					}
				}
			}
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00054B44 File Offset: 0x00052D44
		public void SetArmyMembers(MBList<MobileParty> armyMembers)
		{
			this._possibleArmyMembersUponArmyCreation = armyMembers;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00054B50 File Offset: 0x00052D50
		public bool TryGetBehaviorScore(in AIBehaviorData aiBehaviorData, out float score)
		{
			foreach (ValueTuple<AIBehaviorData, float> valueTuple in this._aiBehaviorScores)
			{
				AIBehaviorData item = valueTuple.Item1;
				if (item.Equals(aiBehaviorData))
				{
					score = valueTuple.Item2;
					return true;
				}
			}
			score = 0f;
			return false;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00054BC8 File Offset: 0x00052DC8
		public void SetBehaviorScore(in AIBehaviorData aiBehaviorData, float score)
		{
			for (int i = 0; i < this._aiBehaviorScores.Count; i++)
			{
				if (this._aiBehaviorScores[i].Item1.Equals(aiBehaviorData))
				{
					this._aiBehaviorScores[i] = new ValueTuple<AIBehaviorData, float>(this._aiBehaviorScores[i].Item1, score);
					return;
				}
			}
			Debug.FailedAssert("AIBehaviorScore not found.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\ICampaignBehaviorManager.cs", "SetBehaviorScore", 196);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00054C49 File Offset: 0x00052E49
		public void AddBehaviorScore(in ValueTuple<AIBehaviorData, float> value)
		{
			this._aiBehaviorScores.Add(value);
		}

		// Token: 0x04000619 RID: 1561
		public MobileParty MobilePartyOf;

		// Token: 0x0400061A RID: 1562
		private readonly MBList<ValueTuple<AIBehaviorData, float>> _aiBehaviorScores;

		// Token: 0x0400061B RID: 1563
		private MBList<MobileParty> _possibleArmyMembersUponArmyCreation;

		// Token: 0x0400061C RID: 1564
		public float CurrentObjectiveValue;

		// Token: 0x0400061D RID: 1565
		public bool WillGatherAnArmy;

		// Token: 0x0400061E RID: 1566
		public bool DoNotChangeBehavior;

		// Token: 0x0400061F RID: 1567
		public float StrengthOfLordsWithoutArmy;

		// Token: 0x04000620 RID: 1568
		public float StrengthOfLordsWithArmy;

		// Token: 0x04000621 RID: 1569
		public float StrengthOfLordsAtSameClanWithoutArmy;
	}
}
