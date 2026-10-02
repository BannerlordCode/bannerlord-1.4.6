using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;

namespace SandBox.View.Map
{
	// Token: 0x0200004A RID: 74
	public class MapConversationView : MapView
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000173E3 File Offset: 0x000155E3
		// (set) Token: 0x06000279 RID: 633 RVA: 0x000173EB File Offset: 0x000155EB
		public bool IsConversationActive { get; protected set; }

		// Token: 0x0600027A RID: 634 RVA: 0x000173F4 File Offset: 0x000155F4
		protected internal virtual void InitializeConversation(ConversationCharacterData playerCharacterData, ConversationCharacterData conversationPartnerData)
		{
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000173F6 File Offset: 0x000155F6
		protected internal override void OnFinalize()
		{
			base.OnFinalize();
			this.DestroyConversationMission();
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00017404 File Offset: 0x00015604
		protected internal virtual void FinalizeConversation()
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00017408 File Offset: 0x00015608
		protected void CreateConversationMissionIfMissing()
		{
			MapConversationView.MapConversationMission mapConversationMission;
			if ((mapConversationMission = CampaignMission.Current as MapConversationView.MapConversationMission) != null)
			{
				this.ConversationMission = mapConversationMission;
				return;
			}
			this.ConversationMission = new MapConversationView.MapConversationMission();
			CampaignMission.Current = this.ConversationMission;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00017441 File Offset: 0x00015641
		protected void DestroyConversationMission()
		{
			MapConversationView.MapConversationMission conversationMission = this.ConversationMission;
			if (conversationMission != null)
			{
				conversationMission.OnFinalize();
			}
			this.ConversationMission = null;
		}

		// Token: 0x0400015B RID: 347
		public MapConversationView.MapConversationMission ConversationMission;

		// Token: 0x020000A3 RID: 163
		public class MapConversationMission : ICampaignMission
		{
			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060005AD RID: 1453 RVA: 0x00028BB5 File Offset: 0x00026DB5
			GameState ICampaignMission.State
			{
				get
				{
					return GameStateManager.Current.ActiveState;
				}
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x060005AE RID: 1454 RVA: 0x00028BC1 File Offset: 0x00026DC1
			IMissionTroopSupplier ICampaignMission.AgentSupplier
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x060005AF RID: 1455 RVA: 0x00028BC4 File Offset: 0x00026DC4
			// (set) Token: 0x060005B0 RID: 1456 RVA: 0x00028BCC File Offset: 0x00026DCC
			Location ICampaignMission.Location { get; set; }

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00028BD5 File Offset: 0x00026DD5
			// (set) Token: 0x060005B2 RID: 1458 RVA: 0x00028BDD File Offset: 0x00026DDD
			Alley ICampaignMission.LastVisitedAlley { get; set; }

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00028BE6 File Offset: 0x00026DE6
			MissionMode ICampaignMission.Mode
			{
				get
				{
					return MissionMode.Conversation;
				}
			}

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00028BE9 File Offset: 0x00026DE9
			// (set) Token: 0x060005B5 RID: 1461 RVA: 0x00028BF1 File Offset: 0x00026DF1
			public MapConversationTableau ConversationTableau { get; private set; }

			// Token: 0x060005B6 RID: 1462 RVA: 0x00028BFA File Offset: 0x00026DFA
			public MapConversationMission()
			{
				CampaignMission.Current = this;
				this._conversationPlayQueue = new Queue<MapConversationView.MapConversationMission.ConversationPlayArgs>();
			}

			// Token: 0x060005B7 RID: 1463 RVA: 0x00028C13 File Offset: 0x00026E13
			public void SetConversationTableau(MapConversationTableau tableau)
			{
				this.ConversationTableau = tableau;
				this.PlayCachedConversations();
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x00028C22 File Offset: 0x00026E22
			public void Tick(float dt)
			{
				this.PlayCachedConversations();
			}

			// Token: 0x060005B9 RID: 1465 RVA: 0x00028C2A File Offset: 0x00026E2A
			public void OnFinalize()
			{
				this.ConversationTableau = null;
				this._conversationPlayQueue = null;
				CampaignMission.Current = null;
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x00028C40 File Offset: 0x00026E40
			private void PlayCachedConversations()
			{
				if (this.ConversationTableau != null)
				{
					while (this._conversationPlayQueue.Count > 0)
					{
						MapConversationView.MapConversationMission.ConversationPlayArgs conversationPlayArgs = this._conversationPlayQueue.Dequeue();
						this.ConversationTableau.OnConversationPlay(conversationPlayArgs.IdleActionId, conversationPlayArgs.IdleFaceAnimId, conversationPlayArgs.ReactionId, conversationPlayArgs.ReactionFaceAnimId, conversationPlayArgs.SoundPath);
					}
				}
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x00028C9A File Offset: 0x00026E9A
			void ICampaignMission.OnConversationPlay(string idleActionId, string idleFaceAnimId, string reactionId, string reactionFaceAnimId, string soundPath)
			{
				if (this.ConversationTableau != null)
				{
					this.ConversationTableau.OnConversationPlay(idleActionId, idleFaceAnimId, reactionId, reactionFaceAnimId, soundPath);
					return;
				}
				this._conversationPlayQueue.Enqueue(new MapConversationView.MapConversationMission.ConversationPlayArgs(idleActionId, idleFaceAnimId, reactionId, reactionFaceAnimId, soundPath));
			}

			// Token: 0x060005BC RID: 1468 RVA: 0x00028CCE File Offset: 0x00026ECE
			void ICampaignMission.AddAgentFollowing(IAgent agent)
			{
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x00028CD0 File Offset: 0x00026ED0
			bool ICampaignMission.AgentLookingAtAgent(IAgent agent1, IAgent agent2)
			{
				return false;
			}

			// Token: 0x060005BE RID: 1470 RVA: 0x00028CD3 File Offset: 0x00026ED3
			bool ICampaignMission.CheckIfAgentCanFollow(IAgent agent)
			{
				return false;
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x00028CD6 File Offset: 0x00026ED6
			bool ICampaignMission.CheckIfAgentCanUnFollow(IAgent agent)
			{
				return false;
			}

			// Token: 0x060005C0 RID: 1472 RVA: 0x00028CD9 File Offset: 0x00026ED9
			void ICampaignMission.EndMission()
			{
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x00028CDB File Offset: 0x00026EDB
			void ICampaignMission.OnCharacterLocationChanged(LocationCharacter locationCharacter, Location fromLocation, Location toLocation)
			{
			}

			// Token: 0x060005C2 RID: 1474 RVA: 0x00028CDD File Offset: 0x00026EDD
			void ICampaignMission.OnCloseEncounterMenu()
			{
			}

			// Token: 0x060005C3 RID: 1475 RVA: 0x00028CDF File Offset: 0x00026EDF
			void ICampaignMission.OnConversationContinue()
			{
			}

			// Token: 0x060005C4 RID: 1476 RVA: 0x00028CE1 File Offset: 0x00026EE1
			void ICampaignMission.OnConversationEnd(IAgent agent)
			{
			}

			// Token: 0x060005C5 RID: 1477 RVA: 0x00028CE3 File Offset: 0x00026EE3
			void ICampaignMission.OnConversationStart(IAgent agent, bool setActionsInstantly)
			{
			}

			// Token: 0x060005C6 RID: 1478 RVA: 0x00028CE5 File Offset: 0x00026EE5
			void ICampaignMission.OnProcessSentence()
			{
			}

			// Token: 0x060005C7 RID: 1479 RVA: 0x00028CE7 File Offset: 0x00026EE7
			void ICampaignMission.RemoveAgentFollowing(IAgent agent)
			{
			}

			// Token: 0x060005C8 RID: 1480 RVA: 0x00028CE9 File Offset: 0x00026EE9
			void ICampaignMission.SetMissionMode(MissionMode newMode, bool atStart)
			{
			}

			// Token: 0x060005C9 RID: 1481 RVA: 0x00028CEB File Offset: 0x00026EEB
			void ICampaignMission.FadeOutCharacter(CharacterObject characterObject)
			{
			}

			// Token: 0x060005CA RID: 1482 RVA: 0x00028CED File Offset: 0x00026EED
			void ICampaignMission.OnGameStateChanged()
			{
				MapConversationTableau conversationTableau = this.ConversationTableau;
				if (conversationTableau != null)
				{
					conversationTableau.RemovePreviousAgentsSoundEvent();
				}
				MapConversationTableau conversationTableau2 = this.ConversationTableau;
				if (conversationTableau2 == null)
				{
					return;
				}
				conversationTableau2.StopConversationSoundEvent();
			}

			// Token: 0x04000341 RID: 833
			private Queue<MapConversationView.MapConversationMission.ConversationPlayArgs> _conversationPlayQueue;

			// Token: 0x020000CE RID: 206
			public struct ConversationPlayArgs
			{
				// Token: 0x06000679 RID: 1657 RVA: 0x0002A0DF File Offset: 0x000282DF
				public ConversationPlayArgs(string idleActionId, string idleFaceAnimId, string reactionId, string reactionFaceAnimId, string soundPath)
				{
					this.IdleActionId = idleActionId;
					this.IdleFaceAnimId = idleFaceAnimId;
					this.ReactionId = reactionId;
					this.ReactionFaceAnimId = reactionFaceAnimId;
					this.SoundPath = soundPath;
				}

				// Token: 0x040003E0 RID: 992
				public readonly string IdleActionId;

				// Token: 0x040003E1 RID: 993
				public readonly string IdleFaceAnimId;

				// Token: 0x040003E2 RID: 994
				public readonly string ReactionId;

				// Token: 0x040003E3 RID: 995
				public readonly string ReactionFaceAnimId;

				// Token: 0x040003E4 RID: 996
				public readonly string SoundPath;
			}
		}
	}
}
