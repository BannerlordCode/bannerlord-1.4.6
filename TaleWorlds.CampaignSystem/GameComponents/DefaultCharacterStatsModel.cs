using System;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x02000102 RID: 258
	public class DefaultCharacterStatsModel : CharacterStatsModel
	{
		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x00069A21 File Offset: 0x00067C21
		public override int MaxCharacterTier
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00069A24 File Offset: 0x00067C24
		public override int WoundedHitPointLimit(Hero hero)
		{
			return 20;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00069A28 File Offset: 0x00067C28
		public override int GetTier(CharacterObject character)
		{
			if (character.IsHero)
			{
				return 0;
			}
			return MathF.Min(MathF.Max(MathF.Ceiling(((float)character.Level - 5f) / 5f), 0), Campaign.Current.Models.CharacterStatsModel.MaxCharacterTier);
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00069A78 File Offset: 0x00067C78
		public override ExplainedNumber MaxHitpoints(CharacterObject character, bool includeDescriptions = false)
		{
			ExplainedNumber explainedNumber = new ExplainedNumber(100f, includeDescriptions, null);
			PerkHelper.AddPerkBonusForCharacter(DefaultPerks.OneHanded.Trainer, character, true, ref explainedNumber, false);
			PerkHelper.AddPerkBonusForCharacter(DefaultPerks.TwoHanded.ThickHides, character, true, ref explainedNumber, false);
			PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Medicine.DoctorsOath, character, false, ref explainedNumber, false);
			PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Medicine.FortitudeTonic, character, false, ref explainedNumber, false);
			PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Athletics.WellBuilt, character, true, ref explainedNumber, false);
			PerkHelper.AddPerkBonusForCharacter(DefaultPerks.OneHanded.UnwaveringDefense, character, true, ref explainedNumber, false);
			PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Medicine.PreventiveMedicine, character, true, ref explainedNumber, false);
			if (character.IsHero && character.HeroObject.PartyBelongedTo != null && character.HeroObject.PartyBelongedTo.LeaderHero != character.HeroObject && character.HeroObject.PartyBelongedTo.HasPerk(DefaultPerks.Medicine.FortitudeTonic, false))
			{
				explainedNumber.Add(DefaultPerks.Medicine.FortitudeTonic.PrimaryBonus, DefaultPerks.Medicine.FortitudeTonic.Name, null);
			}
			if (character.GetPerkValue(DefaultPerks.Athletics.MightyBlow))
			{
				int num = character.GetSkillValue(DefaultSkills.Athletics) - Campaign.Current.Models.CharacterDevelopmentModel.MaxSkillRequiredForEpicPerkBonus;
				explainedNumber.Add((float)num, DefaultPerks.Athletics.MightyBlow.Name, null);
			}
			return explainedNumber;
		}
	}
}
