using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;
using TaleWorlds.SaveSystem.Resolvers;

namespace TaleWorlds.CampaignSystem.SaveCompability
{
	// Token: 0x020000D4 RID: 212
	public class HeroTraitDeveloperResolver : IConflictResolver
	{
		// Token: 0x06001477 RID: 5239 RVA: 0x0005F08B File Offset: 0x0005D28B
		public bool IsApplicable(ApplicationVersion version)
		{
			return version != ApplicationVersion.Empty && version.IsOlderThan(ApplicationVersion.FromString("v1.3.0", 0));
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0005F0AE File Offset: 0x0005D2AE
		public MemberTypeId GetFieldMemberWithId(MemberTypeId memberTypeId)
		{
			if (memberTypeId == new MemberTypeId(3, 10))
			{
				return new MemberTypeId(TypeDefinitionBase.GetClassLevel(this.GetNewType()), 10);
			}
			return MemberTypeId.Invalid;
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0005F0D8 File Offset: 0x0005D2D8
		public Type GetNewType()
		{
			return typeof(PropertyOwner<PropertyObject>);
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0005F0E4 File Offset: 0x0005D2E4
		public MemberTypeId GetPropertyMemberWithId(MemberTypeId memberTypeId)
		{
			return MemberTypeId.Invalid;
		}
	}
}
