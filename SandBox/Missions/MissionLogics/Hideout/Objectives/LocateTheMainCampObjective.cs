using System;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Missions.Objectives;

namespace SandBox.Missions.MissionLogics.Hideout.Objectives
{
	// Token: 0x02000097 RID: 151
	public class LocateTheMainCampObjective : MissionObjective
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0002AEB6 File Offset: 0x000290B6
		public override string UniqueId
		{
			get
			{
				return "hideout_mission_locate_the_main_camp_objective";
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0002AEBD File Offset: 0x000290BD
		public override TextObject Name
		{
			get
			{
				return new TextObject("{=2g03vuC7}Locate the Main Camp", null);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0002AECA File Offset: 0x000290CA
		public override TextObject Description
		{
			get
			{
				return new TextObject("{=wmvJ0bcH}Sneak your way through the sentries.", null);
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0002AED7 File Offset: 0x000290D7
		public LocateTheMainCampObjective(Mission mission)
			: base(mission)
		{
		}
	}
}
