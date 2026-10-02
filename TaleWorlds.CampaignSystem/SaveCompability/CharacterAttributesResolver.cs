using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;
using TaleWorlds.SaveSystem.Resolvers;

namespace TaleWorlds.CampaignSystem.SaveCompability
{
	// Token: 0x020000CF RID: 207
	public class CharacterAttributesResolver : IConflictResolver
	{
		// Token: 0x06001461 RID: 5217 RVA: 0x0005EDF9 File Offset: 0x0005CFF9
		public bool IsApplicable(ApplicationVersion version)
		{
			return version != ApplicationVersion.Empty && version.IsOlderThan(ApplicationVersion.FromString("v1.3.0", 0));
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0005EE1C File Offset: 0x0005D01C
		public MemberTypeId GetFieldMemberWithId(MemberTypeId memberTypeId)
		{
			if (memberTypeId == new MemberTypeId(3, 10))
			{
				return new MemberTypeId(TypeDefinitionBase.GetClassLevel(this.GetNewType()), 10);
			}
			return MemberTypeId.Invalid;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0005EE46 File Offset: 0x0005D046
		public Type GetNewType()
		{
			return typeof(PropertyOwner<CharacterAttribute>);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0005EE52 File Offset: 0x0005D052
		public MemberTypeId GetPropertyMemberWithId(MemberTypeId memberTypeId)
		{
			return MemberTypeId.Invalid;
		}
	}
}
