using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.Source.Objects
{
	// Token: 0x020003CB RID: 971
	public class NavigationMeshDeactivator : ScriptComponentBehavior
	{
		// Token: 0x06003628 RID: 13864 RVA: 0x000DF817 File Offset: 0x000DDA17
		public void DisableAssignedFaces(Scene scene)
		{
			scene.SetAbilityOfFacesWithId(this.DisableFaceWithId, false);
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000DF827 File Offset: 0x000DDA27
		public void EnableAssignedFaces(Scene scene)
		{
			scene.SetAbilityOfFacesWithId(this.DisableFaceWithId, true);
		}

		// Token: 0x04001739 RID: 5945
		public int DisableFaceWithId = -1;

		// Token: 0x0400173A RID: 5946
		public int DisableFaceWithIdForAnimals = -1;
	}
}
