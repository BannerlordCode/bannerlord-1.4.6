using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Encounters
{
	// Token: 0x020002EC RID: 748
	public class LocationEncounter
	{
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x000AED72 File Offset: 0x000ACF72
		public Settlement Settlement { get; }

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060029DC RID: 10716 RVA: 0x000AED7A File Offset: 0x000ACF7A
		// (set) Token: 0x060029DD RID: 10717 RVA: 0x000AED82 File Offset: 0x000ACF82
		public List<AccompanyingCharacter> CharactersAccompanyingPlayer { get; private set; }

		// Token: 0x060029DE RID: 10718 RVA: 0x000AED8B File Offset: 0x000ACF8B
		protected LocationEncounter(Settlement settlement)
		{
			this.Settlement = settlement;
			this.CharactersAccompanyingPlayer = new List<AccompanyingCharacter>();
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x000AEDA8 File Offset: 0x000ACFA8
		public void AddAccompanyingCharacter(LocationCharacter locationCharacter, bool isFollowing = false)
		{
			if (!this.CharactersAccompanyingPlayer.Any<AccompanyingCharacter>((AccompanyingCharacter x) => x.LocationCharacter.Character == locationCharacter.Character))
			{
				AccompanyingCharacter accompanyingCharacter = new AccompanyingCharacter(locationCharacter, isFollowing);
				this.CharactersAccompanyingPlayer.Add(accompanyingCharacter);
			}
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000AEDF4 File Offset: 0x000ACFF4
		public AccompanyingCharacter GetAccompanyingCharacter(LocationCharacter locationCharacter)
		{
			return this.CharactersAccompanyingPlayer.Find((AccompanyingCharacter x) => x.LocationCharacter == locationCharacter);
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000AEE28 File Offset: 0x000AD028
		public AccompanyingCharacter GetAccompanyingCharacter(CharacterObject character)
		{
			return this.CharactersAccompanyingPlayer.Find(delegate(AccompanyingCharacter x)
			{
				LocationCharacter locationCharacter = x.LocationCharacter;
				return ((locationCharacter != null) ? locationCharacter.Character : null) == character;
			});
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000AEE5C File Offset: 0x000AD05C
		public void RemoveAccompanyingCharacter(LocationCharacter locationCharacter)
		{
			if (this.CharactersAccompanyingPlayer.Any<AccompanyingCharacter>((AccompanyingCharacter x) => x.LocationCharacter == locationCharacter))
			{
				AccompanyingCharacter accompanyingCharacter = this.CharactersAccompanyingPlayer.Find((AccompanyingCharacter x) => x.LocationCharacter == locationCharacter);
				this.CharactersAccompanyingPlayer.Remove(accompanyingCharacter);
			}
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000AEEB4 File Offset: 0x000AD0B4
		public void RemoveAccompanyingCharacter(Hero hero)
		{
			for (int i = this.CharactersAccompanyingPlayer.Count - 1; i >= 0; i--)
			{
				if (this.CharactersAccompanyingPlayer[i].LocationCharacter.Character.IsHero && this.CharactersAccompanyingPlayer[i].LocationCharacter.Character.HeroObject == hero)
				{
					this.CharactersAccompanyingPlayer.Remove(this.CharactersAccompanyingPlayer[i]);
					return;
				}
			}
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x000AEF2D File Offset: 0x000AD12D
		public void RemoveAllAccompanyingCharacters()
		{
			this.CharactersAccompanyingPlayer.Clear();
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000AEF3A File Offset: 0x000AD13A
		public void OnCharacterLocationChanged(LocationCharacter locationCharacter, Location fromLocation, Location toLocation)
		{
			if ((fromLocation == CampaignMission.Current.Location && toLocation == null) || (fromLocation == null && toLocation == CampaignMission.Current.Location))
			{
				CampaignMission.Current.OnCharacterLocationChanged(locationCharacter, fromLocation, toLocation);
			}
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x000AEF69 File Offset: 0x000AD169
		public virtual bool IsWorkshopLocation(Location location)
		{
			return false;
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x000AEF6C File Offset: 0x000AD16C
		public virtual bool IsTavern(Location location)
		{
			return false;
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x000AEF6F File Offset: 0x000AD16F
		public virtual IMission CreateAndOpenMissionController(Location nextLocation, Location previousLocation = null, CharacterObject talkToChar = null, string playerSpecialSpawnTag = null)
		{
			return null;
		}

		// Token: 0x04000C1E RID: 3102
		public bool IsInsideOfASettlement;
	}
}
