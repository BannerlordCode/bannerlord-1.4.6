using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200005F RID: 95
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class HasTableauCache : Attribute
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00008BAC File Offset: 0x00006DAC
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x00008BB4 File Offset: 0x00006DB4
		public Type TableauCacheType { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00008BBD File Offset: 0x00006DBD
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x00008BC5 File Offset: 0x00006DC5
		public Type MaterialCacheIDGetType { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x00008BCE File Offset: 0x00006DCE
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x00008BD5 File Offset: 0x00006DD5
		internal static Dictionary<Type, MaterialCacheIDGetMethodDelegate> TableauCacheTypes { get; private set; }

		// Token: 0x06000963 RID: 2403 RVA: 0x00008BDD File Offset: 0x00006DDD
		public HasTableauCache(Type tableauCacheType, Type materialCacheIDGetType)
		{
			this.TableauCacheType = tableauCacheType;
			this.MaterialCacheIDGetType = materialCacheIDGetType;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00008BF4 File Offset: 0x00006DF4
		public static void CollectTableauCacheTypes()
		{
			HasTableauCache.TableauCacheTypes = new Dictionary<Type, MaterialCacheIDGetMethodDelegate>();
			HasTableauCache.CollectTableauCacheTypesFrom(typeof(HasTableauCache).Assembly);
			Assembly[] referencingAssembliesSafe = typeof(HasTableauCache).Assembly.GetReferencingAssembliesSafe(null);
			for (int i = 0; i < referencingAssembliesSafe.Length; i++)
			{
				HasTableauCache.CollectTableauCacheTypesFrom(referencingAssembliesSafe[i]);
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00008C4C File Offset: 0x00006E4C
		private static void CollectTableauCacheTypesFrom(Assembly assembly)
		{
			object[] customAttributesSafe = assembly.GetCustomAttributesSafe(typeof(HasTableauCache), true);
			if (customAttributesSafe.Length != 0)
			{
				foreach (HasTableauCache hasTableauCache in customAttributesSafe)
				{
					MethodInfo method = hasTableauCache.MaterialCacheIDGetType.GetMethod("GetMaterialCacheID", BindingFlags.Static | BindingFlags.Public);
					MaterialCacheIDGetMethodDelegate materialCacheIDGetMethodDelegate = (MaterialCacheIDGetMethodDelegate)Delegate.CreateDelegate(typeof(MaterialCacheIDGetMethodDelegate), method);
					HasTableauCache.TableauCacheTypes.Add(hasTableauCache.TableauCacheType, materialCacheIDGetMethodDelegate);
				}
			}
		}
	}
}
