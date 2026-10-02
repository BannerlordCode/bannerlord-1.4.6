using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Core.ImageIdentifiers;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.BarterSystem.Barterables
{
	// Token: 0x02000480 RID: 1152
	public class DeclareWarBarterable : Barterable
	{
		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06004928 RID: 18728 RVA: 0x0017343E File Offset: 0x0017163E
		public override string StringID
		{
			get
			{
				return "declare_war_barterable";
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06004929 RID: 18729 RVA: 0x00173445 File Offset: 0x00171645
		// (set) Token: 0x0600492A RID: 18730 RVA: 0x0017344D File Offset: 0x0017164D
		public IFaction DeclaringFaction { get; private set; }

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x0600492B RID: 18731 RVA: 0x00173456 File Offset: 0x00171656
		// (set) Token: 0x0600492C RID: 18732 RVA: 0x0017345E File Offset: 0x0017165E
		public IFaction OtherFaction { get; private set; }

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x0600492D RID: 18733 RVA: 0x00173467 File Offset: 0x00171667
		public override TextObject Name
		{
			get
			{
				TextObject textObject = new TextObject("{=GZwNgIon}Declare war against {OTHER_FACTION}", null);
				textObject.SetTextVariable("OTHER_FACTION", this.OtherFaction.Name);
				return textObject;
			}
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x0017348B File Offset: 0x0017168B
		public DeclareWarBarterable(IFaction declaringFaction, IFaction otherFaction)
			: base(declaringFaction.Leader, null)
		{
			this.DeclaringFaction = declaringFaction;
			this.OtherFaction = otherFaction;
		}

		// Token: 0x0600492F RID: 18735 RVA: 0x001734A8 File Offset: 0x001716A8
		public override void Apply()
		{
			DeclareWarAction.ApplyByDefault(base.OriginalOwner.MapFaction, this.OtherFaction.MapFaction);
		}

		// Token: 0x06004930 RID: 18736 RVA: 0x001734C8 File Offset: 0x001716C8
		public override int GetUnitValueForFaction(IFaction faction)
		{
			int num = 0;
			Clan clan = ((faction is Clan) ? ((Clan)faction) : ((Kingdom)faction).RulingClan);
			if (faction.MapFaction == base.OriginalOwner.MapFaction)
			{
				TextObject textObject;
				num = (int)Campaign.Current.Models.DiplomacyModel.GetScoreOfDeclaringWar(base.OriginalOwner.MapFaction, this.OtherFaction.MapFaction, clan, out textObject, false);
			}
			else if (faction.MapFaction == this.OtherFaction.MapFaction)
			{
				TextObject textObject;
				num = (int)Campaign.Current.Models.DiplomacyModel.GetScoreOfDeclaringWar(this.OtherFaction.MapFaction, base.OriginalOwner.MapFaction, clan, out textObject, false);
			}
			return num;
		}

		// Token: 0x06004931 RID: 18737 RVA: 0x0017357C File Offset: 0x0017177C
		public override ImageIdentifier GetVisualIdentifier()
		{
			return null;
		}
	}
}
