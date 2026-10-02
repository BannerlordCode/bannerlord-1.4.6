using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;
using TaleWorlds.MountAndBlade.Network.Gameplay.Perks;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000310 RID: 784
	public abstract class MPPerkCondition
	{
		// Token: 0x06002CB5 RID: 11445 RVA: 0x000AC448 File Offset: 0x000AA648
		static MPPerkCondition()
		{
			foreach (Type type in from t in PerkAssemblyCollection.GetPerkAssemblyTypes()
				where t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(MPPerkCondition))
				select t)
			{
				FieldInfo field = type.GetField("StringType", BindingFlags.Static | BindingFlags.NonPublic);
				string text = (string)((field != null) ? field.GetValue(null) : null);
				MPPerkCondition.Registered.Add(text, type);
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x000AC4D8 File Offset: 0x000AA6D8
		public virtual MPPerkCondition.PerkEventFlags EventFlags
		{
			get
			{
				return MPPerkCondition.PerkEventFlags.None;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002CB7 RID: 11447 RVA: 0x000AC4DB File Offset: 0x000AA6DB
		public virtual bool IsPeerCondition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002CB8 RID: 11448
		public abstract bool Check(MissionPeer peer);

		// Token: 0x06002CB9 RID: 11449
		public abstract bool Check(Agent agent);

		// Token: 0x06002CBA RID: 11450 RVA: 0x000AC4DE File Offset: 0x000AA6DE
		protected virtual bool IsGameModesValid(List<string> gameModes)
		{
			return true;
		}

		// Token: 0x06002CBB RID: 11451
		protected abstract void Deserialize(XmlNode node);

		// Token: 0x06002CBC RID: 11452 RVA: 0x000AC4E4 File Offset: 0x000AA6E4
		public static MPPerkCondition CreateFrom(List<string> gameModes, XmlNode node)
		{
			string text;
			if (node == null)
			{
				text = null;
			}
			else
			{
				XmlAttributeCollection attributes = node.Attributes;
				if (attributes == null)
				{
					text = null;
				}
				else
				{
					XmlAttribute xmlAttribute = attributes["type"];
					text = ((xmlAttribute != null) ? xmlAttribute.Value : null);
				}
			}
			string text2 = text;
			MPPerkCondition mpperkCondition = (MPPerkCondition)Activator.CreateInstance(MPPerkCondition.Registered[text2], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, CultureInfo.InvariantCulture);
			mpperkCondition.Deserialize(node);
			return mpperkCondition;
		}

		// Token: 0x040011B9 RID: 4537
		protected static Dictionary<string, Type> Registered = new Dictionary<string, Type>();

		// Token: 0x020005F8 RID: 1528
		[Flags]
		public enum PerkEventFlags
		{
			// Token: 0x04002013 RID: 8211
			None = 0,
			// Token: 0x04002014 RID: 8212
			MoraleChange = 1,
			// Token: 0x04002015 RID: 8213
			FlagCapture = 2,
			// Token: 0x04002016 RID: 8214
			FlagRemoval = 4,
			// Token: 0x04002017 RID: 8215
			HealthChange = 8,
			// Token: 0x04002018 RID: 8216
			AliveBotCountChange = 16,
			// Token: 0x04002019 RID: 8217
			PeerControlledAgentChange = 32,
			// Token: 0x0400201A RID: 8218
			BannerPickUp = 64,
			// Token: 0x0400201B RID: 8219
			BannerDrop = 128,
			// Token: 0x0400201C RID: 8220
			SpawnEnd = 256,
			// Token: 0x0400201D RID: 8221
			MountHealthChange = 512,
			// Token: 0x0400201E RID: 8222
			MountChange = 1024,
			// Token: 0x0400201F RID: 8223
			AgentEventsMask = 1576
		}
	}
}
