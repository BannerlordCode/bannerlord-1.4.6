using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;

namespace SandBox.CampaignBehaviors
{
	// Token: 0x020000E2 RID: 226
	public class StealthCharactersCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06000AE7 RID: 2791 RVA: 0x00050C68 File Offset: 0x0004EE68
		public override void RegisterEvents()
		{
			CampaignEvents.LocationCharactersAreReadyToSpawnEvent.AddNonSerializedListener(this, new Action<Dictionary<string, int>>(this.LocationCharactersAreReadyToSpawn));
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00050C84 File Offset: 0x0004EE84
		private void LocationCharactersAreReadyToSpawn(Dictionary<string, int> unusedPoints)
		{
			Settlement settlement = PlayerEncounter.LocationEncounter.Settlement;
			if (settlement.IsHideout)
			{
				return;
			}
			Location location = settlement.LocationComplex.GetListOfLocations().First<Location>();
			int num;
			if (unusedPoints.TryGetValue("stealth_agent", out num) && num > 0)
			{
				location.AddLocationCharacters(new CreateLocationCharacterDelegate(this.CreateStealthCharacter), settlement.Culture, LocationCharacter.CharacterRelations.Enemy, num);
			}
			if (unusedPoints.TryGetValue("stealth_agent_forced", out num) && num > 0)
			{
				location.AddLocationCharacters(new CreateLocationCharacterDelegate(this.CreteForcedStealthCharacter), settlement.Culture, LocationCharacter.CharacterRelations.Enemy, num);
			}
			if (unusedPoints.TryGetValue("disguise_default_agent", out num) && num > 0)
			{
				location.AddLocationCharacters(new CreateLocationCharacterDelegate(this.CreateDisguiseDefaultCharacter), settlement.Culture, LocationCharacter.CharacterRelations.Enemy, num);
			}
			if (unusedPoints.TryGetValue("disguise_officer_agent", out num) && num > 0)
			{
				location.AddLocationCharacters(new CreateLocationCharacterDelegate(this.CreateDisguiseOfficerCharacter), settlement.Culture, LocationCharacter.CharacterRelations.Enemy, num);
			}
			if (unusedPoints.TryGetValue("disguise_shadow_agent", out num) && num > 0)
			{
				location.AddLocationCharacters(new CreateLocationCharacterDelegate(this.CreateDisguiseShadowTargetCharacter), settlement.Culture, LocationCharacter.CharacterRelations.Enemy, num);
			}
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00050D97 File Offset: 0x0004EF97
		private LocationCharacter CreateStealthCharacter(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			return this.CreateStealthAgentInternal("stealth_agent", "stealth_character");
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00050DA9 File Offset: 0x0004EFA9
		private LocationCharacter CreteForcedStealthCharacter(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			LocationCharacter locationCharacter = this.CreateStealthAgentInternal("stealth_agent_forced", "stealth_character");
			locationCharacter.ForceSpawnInSpecialTargetTag = true;
			return locationCharacter;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00050DC2 File Offset: 0x0004EFC2
		private LocationCharacter CreateDisguiseDefaultCharacter(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			return this.CreateStealthAgentInternal("disguise_default_agent", "disguise_default_character");
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00050DD4 File Offset: 0x0004EFD4
		private LocationCharacter CreateDisguiseOfficerCharacter(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			return this.CreateStealthAgentInternal("disguise_officer_agent", "disguise_officer_character");
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00050DE6 File Offset: 0x0004EFE6
		private LocationCharacter CreateDisguiseShadowTargetCharacter(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			return this.CreateStealthAgentInternal("disguise_shadow_agent", "disguise_shadow_target");
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00050DF8 File Offset: 0x0004EFF8
		private LocationCharacter CreateStealthAgentInternal(string spawnTag, string characterId)
		{
			CharacterObject @object = MBObjectManager.Instance.GetObject<CharacterObject>(characterId);
			int num;
			int num2;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(@object, out num, out num2, "");
			return new LocationCharacter(new AgentData(new SimpleAgentOrigin(@object, -1, null, default(UniqueTroopDescriptor))).Monster(FaceGen.GetMonsterWithSuffix(@object.Race, "_settlement_slow")).Age(MBRandom.RandomInt(num, num2)), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddStealthAgentBehaviors), spawnTag, true, LocationCharacter.CharacterRelations.Enemy, null, true, false, null, false, false, true, null, false);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00050E8D File Offset: 0x0004F08D
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
