using System;

namespace TaleWorlds.CampaignSystem.TournamentGames
{
	// Token: 0x020002D6 RID: 726
	public class TournamentRound
	{
		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x000A56CF File Offset: 0x000A38CF
		// (set) Token: 0x06002799 RID: 10137 RVA: 0x000A56D7 File Offset: 0x000A38D7
		public TournamentMatch[] Matches { get; private set; }

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x000A56E0 File Offset: 0x000A38E0
		// (set) Token: 0x0600279B RID: 10139 RVA: 0x000A56E8 File Offset: 0x000A38E8
		public int CurrentMatchIndex { get; private set; }

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x000A56F1 File Offset: 0x000A38F1
		public TournamentMatch CurrentMatch
		{
			get
			{
				if (this.CurrentMatchIndex >= this.Matches.Length)
				{
					return null;
				}
				return this.Matches[this.CurrentMatchIndex];
			}
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x000A5714 File Offset: 0x000A3914
		public TournamentRound(int participantCount, int numberOfMatches, int numberOfTeamsPerMatch, int numberOfWinnerParticipants, TournamentGame.QualificationMode qualificationMode)
		{
			this.Matches = new TournamentMatch[numberOfMatches];
			this.CurrentMatchIndex = 0;
			int num = participantCount / numberOfMatches;
			for (int i = 0; i < numberOfMatches; i++)
			{
				this.Matches[i] = new TournamentMatch(num, numberOfTeamsPerMatch, numberOfWinnerParticipants / numberOfMatches, qualificationMode);
			}
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x000A5760 File Offset: 0x000A3960
		public void OnMatchEnded()
		{
			int currentMatchIndex = this.CurrentMatchIndex;
			this.CurrentMatchIndex = currentMatchIndex + 1;
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000A5780 File Offset: 0x000A3980
		public void EndMatch()
		{
			this.CurrentMatch.End();
			int currentMatchIndex = this.CurrentMatchIndex;
			this.CurrentMatchIndex = currentMatchIndex + 1;
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000A57A8 File Offset: 0x000A39A8
		public void AddParticipant(TournamentParticipant participant, bool firstTime = false)
		{
			foreach (TournamentMatch tournamentMatch in this.Matches)
			{
				if (tournamentMatch.IsParticipantRequired())
				{
					tournamentMatch.AddParticipant(participant, firstTime);
					return;
				}
			}
		}
	}
}
