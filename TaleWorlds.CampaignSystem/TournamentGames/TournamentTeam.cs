using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.TournamentGames
{
	// Token: 0x020002D7 RID: 727
	public class TournamentTeam
	{
		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060027A1 RID: 10145 RVA: 0x000A57DF File Offset: 0x000A39DF
		// (set) Token: 0x060027A2 RID: 10146 RVA: 0x000A57E7 File Offset: 0x000A39E7
		public int TeamSize { get; private set; }

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060027A3 RID: 10147 RVA: 0x000A57F0 File Offset: 0x000A39F0
		// (set) Token: 0x060027A4 RID: 10148 RVA: 0x000A57F8 File Offset: 0x000A39F8
		public uint TeamColor { get; private set; }

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x000A5801 File Offset: 0x000A3A01
		// (set) Token: 0x060027A6 RID: 10150 RVA: 0x000A5809 File Offset: 0x000A3A09
		public Banner TeamBanner { get; private set; }

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x000A5812 File Offset: 0x000A3A12
		// (set) Token: 0x060027A8 RID: 10152 RVA: 0x000A581A File Offset: 0x000A3A1A
		public bool IsPlayerTeam { get; private set; }

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x060027A9 RID: 10153 RVA: 0x000A5823 File Offset: 0x000A3A23
		public IEnumerable<TournamentParticipant> Participants
		{
			get
			{
				return this._participants.AsEnumerable<TournamentParticipant>();
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x060027AA RID: 10154 RVA: 0x000A5830 File Offset: 0x000A3A30
		public int Score
		{
			get
			{
				int num = 0;
				foreach (TournamentParticipant tournamentParticipant in this._participants)
				{
					num += tournamentParticipant.Score;
				}
				return num;
			}
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000A5888 File Offset: 0x000A3A88
		public TournamentTeam(int teamSize, uint teamColor, Banner teamBanner)
		{
			this.TeamColor = teamColor;
			this.TeamBanner = teamBanner;
			this.TeamSize = teamSize;
			this._participants = new List<TournamentParticipant>();
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000A58B0 File Offset: 0x000A3AB0
		public bool IsParticipantRequired()
		{
			return this._participants.Count < this.TeamSize;
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000A58C5 File Offset: 0x000A3AC5
		public void AddParticipant(TournamentParticipant participant)
		{
			participant.IsAssigned = true;
			this._participants.Add(participant);
			participant.SetTeam(this);
			if (participant.IsPlayer)
			{
				this.IsPlayerTeam = true;
			}
		}

		// Token: 0x04000BA2 RID: 2978
		private List<TournamentParticipant> _participants;
	}
}
