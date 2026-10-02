using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x020000AB RID: 171
	public sealed class SkillEffect : PropertyObject
	{
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x0005AA08 File Offset: 0x00058C08
		public static MBReadOnlyList<SkillEffect> All
		{
			get
			{
				return Campaign.Current.AllSkillEffects;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x0005AA14 File Offset: 0x00058C14
		// (set) Token: 0x06001368 RID: 4968 RVA: 0x0005AA1C File Offset: 0x00058C1C
		public float Bonus { get; private set; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x0005AA25 File Offset: 0x00058C25
		// (set) Token: 0x0600136A RID: 4970 RVA: 0x0005AA2D File Offset: 0x00058C2D
		public float BaseValue { get; private set; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0005AA36 File Offset: 0x00058C36
		// (set) Token: 0x0600136C RID: 4972 RVA: 0x0005AA3E File Offset: 0x00058C3E
		public float LimitMin { get; private set; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x0005AA47 File Offset: 0x00058C47
		// (set) Token: 0x0600136E RID: 4974 RVA: 0x0005AA4F File Offset: 0x00058C4F
		public float LimitMax { get; private set; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x0005AA58 File Offset: 0x00058C58
		// (set) Token: 0x06001370 RID: 4976 RVA: 0x0005AA60 File Offset: 0x00058C60
		public PartyRole Role { get; private set; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x0005AA69 File Offset: 0x00058C69
		// (set) Token: 0x06001372 RID: 4978 RVA: 0x0005AA71 File Offset: 0x00058C71
		public EffectIncrementType IncrementType { get; private set; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0005AA7A File Offset: 0x00058C7A
		// (set) Token: 0x06001374 RID: 4980 RVA: 0x0005AA82 File Offset: 0x00058C82
		public SkillObject EffectedSkill { get; private set; }

		// Token: 0x06001375 RID: 4981 RVA: 0x0005AA8B File Offset: 0x00058C8B
		public SkillEffect(string stringId)
			: base(stringId)
		{
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0005AA94 File Offset: 0x00058C94
		public void Initialize(TextObject description, SkillObject effectedSkill, PartyRole role, float bonus, EffectIncrementType incrementType, float baseValue = 0f, float limitMin = -3.4028235E+38f, float limitMax = 3.4028235E+38f)
		{
			base.Initialize(TextObject.GetEmpty(), description);
			this.Role = role;
			this.Bonus = bonus;
			this.IncrementType = incrementType;
			this.EffectedSkill = effectedSkill;
			this.BaseValue = baseValue;
			this.LimitMin = limitMin;
			this.LimitMax = limitMax;
			base.AfterInitialized();
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0005AAE9 File Offset: 0x00058CE9
		public float GetSkillEffectValue(int skillLevel)
		{
			return MathF.Clamp(this.BaseValue + this.Bonus * (float)skillLevel, this.LimitMin, this.LimitMax);
		}
	}
}
