using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200020A RID: 522
	public class CustomBattleCombatant : IBattleCombatant
	{
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x000681DC File Offset: 0x000663DC
		// (set) Token: 0x06001E2D RID: 7725 RVA: 0x000681E4 File Offset: 0x000663E4
		public TextObject Name { get; private set; }

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x000681ED File Offset: 0x000663ED
		// (set) Token: 0x06001E2F RID: 7727 RVA: 0x000681F5 File Offset: 0x000663F5
		public BattleSideEnum Side { get; set; }

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001E30 RID: 7728 RVA: 0x000681FE File Offset: 0x000663FE
		public BasicCharacterObject General
		{
			get
			{
				return this._general;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x00068206 File Offset: 0x00066406
		// (set) Token: 0x06001E32 RID: 7730 RVA: 0x0006820E File Offset: 0x0006640E
		public BasicCultureObject BasicCulture { get; private set; }

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x00068217 File Offset: 0x00066417
		public Tuple<uint, uint> PrimaryColorPair
		{
			get
			{
				return new Tuple<uint, uint>(this.Banner.GetPrimaryColor(), this.Banner.GetFirstIconColor());
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x00068234 File Offset: 0x00066434
		public Tuple<uint, uint> AlternativeColorPair
		{
			get
			{
				return new Tuple<uint, uint>(this.Banner.GetFirstIconColor(), this.Banner.GetPrimaryColor());
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x00068251 File Offset: 0x00066451
		// (set) Token: 0x06001E36 RID: 7734 RVA: 0x00068259 File Offset: 0x00066459
		public Banner Banner { get; private set; }

		// Token: 0x06001E37 RID: 7735 RVA: 0x00068262 File Offset: 0x00066462
		public int GetTacticsSkillAmount()
		{
			if (this._characters.Count > 0)
			{
				return this._characters.Max<BasicCharacterObject>((BasicCharacterObject h) => h.GetSkillValue(DefaultSkills.Tactics));
			}
			return 0;
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x0006829E File Offset: 0x0006649E
		public IEnumerable<BasicCharacterObject> Characters
		{
			get
			{
				return this._characters.AsReadOnly();
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x000682AB File Offset: 0x000664AB
		public int CountOfCharacters
		{
			get
			{
				return this._characters.Count<BasicCharacterObject>();
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001E3A RID: 7738 RVA: 0x000682B8 File Offset: 0x000664B8
		// (set) Token: 0x06001E3B RID: 7739 RVA: 0x000682C0 File Offset: 0x000664C0
		public int NumberOfAllMembers { get; private set; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x000682C9 File Offset: 0x000664C9
		public int NumberOfHealthyMembers
		{
			get
			{
				return this._characters.Count;
			}
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x000682D6 File Offset: 0x000664D6
		public CustomBattleCombatant(TextObject name, BasicCultureObject culture, Banner banner)
		{
			this.Name = name;
			this.BasicCulture = culture;
			this.Banner = banner;
			this._characters = new List<BasicCharacterObject>();
			this._general = null;
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x00068308 File Offset: 0x00066508
		public void AddCharacter(BasicCharacterObject characterObject, int number)
		{
			for (int i = 0; i < number; i++)
			{
				this._characters.Add(characterObject);
			}
			this.NumberOfAllMembers += number;
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x0006833B File Offset: 0x0006653B
		public void SetGeneral(BasicCharacterObject generalCharacter)
		{
			this._general = generalCharacter;
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x00068344 File Offset: 0x00066544
		public bool IsUnderPlayersCommand(BattleSideEnum playerSide)
		{
			return this.Side == playerSide && this.General.IsPlayerCharacter;
		}

		// Token: 0x04000A51 RID: 2641
		private List<BasicCharacterObject> _characters;

		// Token: 0x04000A52 RID: 2642
		private BasicCharacterObject _general;
	}
}
