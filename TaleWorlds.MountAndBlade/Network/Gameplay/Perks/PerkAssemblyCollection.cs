using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Network.Gameplay.Perks
{
	// Token: 0x020003C0 RID: 960
	internal static class PerkAssemblyCollection
	{
		// Token: 0x060035EA RID: 13802 RVA: 0x000DE14C File Offset: 0x000DC34C
		public static List<Type> GetPerkAssemblyTypes()
		{
			List<Type> list = new List<Type>();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			List<Assembly> list2 = new List<Assembly>();
			foreach (Assembly assembly in assemblies)
			{
				try
				{
					if (PerkAssemblyCollection.CheckAssemblyForPerks(assembly))
					{
						list2.Add(assembly);
					}
				}
				catch
				{
				}
			}
			foreach (Assembly assembly2 in list2)
			{
				try
				{
					List<Type> typesSafe = assembly2.GetTypesSafe(null);
					list.AddRange(typesSafe);
				}
				catch
				{
				}
			}
			return list;
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x000DE208 File Offset: 0x000DC408
		private static bool CheckAssemblyForPerks(Assembly assembly)
		{
			Assembly assembly2 = Assembly.GetAssembly(typeof(MPPerkObject));
			if (assembly == assembly2)
			{
				return true;
			}
			AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
			for (int i = 0; i < referencedAssemblies.Length; i++)
			{
				if (referencedAssemblies[i].FullName == assembly2.FullName)
				{
					return true;
				}
			}
			return false;
		}
	}
}
