using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000213 RID: 531
	public struct FormationDeploymentOrder
	{
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x0006BA55 File Offset: 0x00069C55
		// (set) Token: 0x06001EF3 RID: 7923 RVA: 0x0006BA5D File Offset: 0x00069C5D
		public int Key { get; private set; }

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x0006BA66 File Offset: 0x00069C66
		// (set) Token: 0x06001EF5 RID: 7925 RVA: 0x0006BA6E File Offset: 0x00069C6E
		public int Offset { get; private set; }

		// Token: 0x06001EF6 RID: 7926 RVA: 0x0006BA78 File Offset: 0x00069C78
		private FormationDeploymentOrder(FormationClass formationClass, int offset = 0)
		{
			int formationClassPriority = FormationDeploymentOrder.GetFormationClassPriority(formationClass);
			this.Offset = MathF.Max(0, offset);
			this.Key = formationClassPriority + this.Offset * 11;
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x0006BAAA File Offset: 0x00069CAA
		public static FormationDeploymentOrder GetDeploymentOrder(FormationClass fClass, int offset = 0)
		{
			return new FormationDeploymentOrder(fClass, offset);
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x0006BAB3 File Offset: 0x00069CB3
		public static FormationDeploymentOrder.DeploymentOrderComparer GetComparer()
		{
			FormationDeploymentOrder.DeploymentOrderComparer deploymentOrderComparer;
			if ((deploymentOrderComparer = FormationDeploymentOrder._comparer) == null)
			{
				deploymentOrderComparer = (FormationDeploymentOrder._comparer = new FormationDeploymentOrder.DeploymentOrderComparer());
			}
			return deploymentOrderComparer;
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0006BACC File Offset: 0x00069CCC
		private static int GetFormationClassPriority(FormationClass fClass)
		{
			switch (fClass)
			{
			case FormationClass.Infantry:
				return 2;
			case FormationClass.Ranged:
				return 5;
			case FormationClass.Cavalry:
				return 4;
			case FormationClass.HorseArcher:
				return 6;
			case FormationClass.NumberOfDefaultFormations:
				return 0;
			case FormationClass.HeavyInfantry:
				return 1;
			case FormationClass.LightCavalry:
				return 7;
			case FormationClass.HeavyCavalry:
				return 3;
			case FormationClass.NumberOfRegularFormations:
				return 9;
			case FormationClass.Bodyguard:
				return 8;
			default:
				return 10;
			}
		}

		// Token: 0x04000A99 RID: 2713
		private static FormationDeploymentOrder.DeploymentOrderComparer _comparer;

		// Token: 0x0200051D RID: 1309
		public class DeploymentOrderComparer : IComparer<FormationDeploymentOrder>
		{
			// Token: 0x06003BE5 RID: 15333 RVA: 0x000EFAAC File Offset: 0x000EDCAC
			public int Compare(FormationDeploymentOrder a, FormationDeploymentOrder b)
			{
				return a.Key - b.Key;
			}
		}
	}
}
