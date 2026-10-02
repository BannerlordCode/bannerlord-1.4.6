using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x020000F7 RID: 247
	public class DefaultBodyPropertiesModel : BodyPropertiesModel
	{
		// Token: 0x06001688 RID: 5768 RVA: 0x000685BF File Offset: 0x000667BF
		public override int[] GetHairIndicesForCulture(int race, int gender, float age, CultureObject culture)
		{
			return FaceGen.GetHairIndicesByTag(race, gender, age, culture.StringId);
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x000685D0 File Offset: 0x000667D0
		public override int[] GetBeardIndicesForCulture(int race, int gender, float age, CultureObject culture)
		{
			return FaceGen.GetFacialIndicesByTag(race, gender, age, culture.StringId);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x000685E1 File Offset: 0x000667E1
		public override int[] GetTattooIndicesForCulture(int race, int gender, float age, CultureObject culture)
		{
			return FaceGen.GetTattooIndicesByTag(race, gender, age, culture.StringId);
		}
	}
}
