using System;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;
using TaleWorlds.SaveSystem.Resolvers;

namespace TaleWorlds.Core.SaveCompability
{
	// Token: 0x020000E2 RID: 226
	public class CharacterSkillsResolver : IConflictResolver
	{
		// Token: 0x06000B84 RID: 2948 RVA: 0x00025506 File Offset: 0x00023706
		public bool IsApplicable(ApplicationVersion version)
		{
			return version != ApplicationVersion.Empty && version.IsOlderThan(ApplicationVersion.FromString("v1.3.0", 0));
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00025529 File Offset: 0x00023729
		public MemberTypeId GetFieldMemberWithId(MemberTypeId memberTypeId)
		{
			if (memberTypeId == new MemberTypeId(3, 10))
			{
				return new MemberTypeId(TypeDefinitionBase.GetClassLevel(this.GetNewType()), 10);
			}
			return MemberTypeId.Invalid;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00025553 File Offset: 0x00023753
		public Type GetNewType()
		{
			return typeof(PropertyOwner<SkillObject>);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002555F File Offset: 0x0002375F
		public MemberTypeId GetPropertyMemberWithId(MemberTypeId memberTypeId)
		{
			return MemberTypeId.Invalid;
		}
	}
}
