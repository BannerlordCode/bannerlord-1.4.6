using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CharacterDevelopment
{
	// Token: 0x020003AB RID: 939
	public sealed class FeatObject : PropertyObject
	{
		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x0600367B RID: 13947 RVA: 0x000E4642 File Offset: 0x000E2842
		public static MBReadOnlyList<FeatObject> All
		{
			get
			{
				return Campaign.Current.AllFeats;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x0600367C RID: 13948 RVA: 0x000E464E File Offset: 0x000E284E
		// (set) Token: 0x0600367D RID: 13949 RVA: 0x000E4656 File Offset: 0x000E2856
		public float EffectBonus { get; private set; }

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x0600367E RID: 13950 RVA: 0x000E465F File Offset: 0x000E285F
		// (set) Token: 0x0600367F RID: 13951 RVA: 0x000E4667 File Offset: 0x000E2867
		public FeatObject.AdditionType IncrementType { get; private set; }

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06003680 RID: 13952 RVA: 0x000E4670 File Offset: 0x000E2870
		// (set) Token: 0x06003681 RID: 13953 RVA: 0x000E4678 File Offset: 0x000E2878
		public bool IsPositive { get; private set; }

		// Token: 0x06003682 RID: 13954 RVA: 0x000E4681 File Offset: 0x000E2881
		public FeatObject(string stringId)
			: base(stringId)
		{
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000E468A File Offset: 0x000E288A
		public void Initialize(string name, string description, float effectBonus, bool isPositiveEffect, FeatObject.AdditionType incrementType)
		{
			base.Initialize(new TextObject(name, null), new TextObject(description, null));
			this.EffectBonus = effectBonus;
			this.IncrementType = incrementType;
			this.IsPositive = isPositiveEffect;
			base.AfterInitialized();
		}

		// Token: 0x02000782 RID: 1922
		public enum AdditionType
		{
			// Token: 0x04001EA4 RID: 7844
			Add,
			// Token: 0x04001EA5 RID: 7845
			AddFactor
		}
	}
}
