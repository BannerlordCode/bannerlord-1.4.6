using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003FE RID: 1022
	public interface IEducationLogic
	{
		// Token: 0x0600405C RID: 16476
		void Finalize(Hero child, List<string> chosenOptions);

		// Token: 0x0600405D RID: 16477
		void GetOptionProperties(Hero child, string optionKey, List<string> previousChoices, out TextObject optionTitle, out TextObject description, out TextObject effect, out ValueTuple<CharacterAttribute, int>[] attributes, out ValueTuple<SkillObject, int>[] skills, out ValueTuple<SkillObject, int>[] focusPoints, out EducationCampaignBehavior.EducationCharacterProperties[] characterProperties);

		// Token: 0x0600405E RID: 16478
		void GetPageProperties(Hero child, List<string> previousChoices, out TextObject title, out TextObject description, out TextObject instruction, out EducationCampaignBehavior.EducationCharacterProperties[] defaultProperties, out string[] availableOptions);

		// Token: 0x0600405F RID: 16479
		void GetStageProperties(Hero child, out int pageCount);

		// Token: 0x06004060 RID: 16480
		bool IsValidEducationNotification(EducationMapNotification educationMapNotification);
	}
}
