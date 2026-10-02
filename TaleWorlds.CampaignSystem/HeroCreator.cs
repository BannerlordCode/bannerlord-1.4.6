using System;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x0200008E RID: 142
	public static class HeroCreator
	{
		// Token: 0x0600124C RID: 4684 RVA: 0x00053FDC File Offset: 0x000521DC
		public static Hero CreateNotable(Occupation occupation, Settlement settlement = null)
		{
			CharacterObject randomTemplateByOccupation = Campaign.Current.Models.HeroCreationModel.GetRandomTemplateByOccupation(occupation, settlement);
			ValueTuple<CampaignTime, CampaignTime> birthAndDeathDay = Campaign.Current.Models.HeroCreationModel.GetBirthAndDeathDay(randomTemplateByOccupation, true, -1);
			CampaignTime item = birthAndDeathDay.Item1;
			CampaignTime item2 = birthAndDeathDay.Item2;
			Hero hero = HeroCreator.CreateHero(randomTemplateByOccupation, true, item, item2);
			HeroCreator.HeroInitializationArgs heroInitializationArgs = new HeroCreator.HeroInitializationArgs(hero, false).SetGenerateFirstAndFullName(true);
			if (settlement != null)
			{
				heroInitializationArgs.SetBornSettlement(settlement);
			}
			heroInitializationArgs.SetAppearance(new StaticBodyProperties?(Campaign.Current.Models.HeroCreationModel.GetStaticBodyProperties(hero, false, 0f)), -1f, -1f, -1, -1, -1);
			HeroCreator.InitializeHeroFromSettings(heroInitializationArgs.Hero, heroInitializationArgs);
			return hero;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00054090 File Offset: 0x00052290
		public static Hero CreateSpecialHero(CharacterObject template, Settlement bornSettlement = null, Clan faction = null, Clan supporterOfClan = null, int age = -1)
		{
			ValueTuple<CampaignTime, CampaignTime> birthAndDeathDay = Campaign.Current.Models.HeroCreationModel.GetBirthAndDeathDay(template, true, age);
			CampaignTime item = birthAndDeathDay.Item1;
			CampaignTime item2 = birthAndDeathDay.Item2;
			Hero hero = HeroCreator.CreateHero(template, true, item, item2);
			HeroCreator.HeroInitializationArgs heroInitializationArgs = new HeroCreator.HeroInitializationArgs(hero, false).SetGenerateFirstAndFullName(true);
			if (bornSettlement != null)
			{
				heroInitializationArgs.SetBornSettlement(bornSettlement);
			}
			if (faction != null)
			{
				heroInitializationArgs.SetClan(faction);
			}
			if (supporterOfClan != null)
			{
				heroInitializationArgs.SetSupporterOf(supporterOfClan);
			}
			HeroCreator.InitializeHeroFromSettings(heroInitializationArgs.Hero, heroInitializationArgs);
			return hero;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00054108 File Offset: 0x00052308
		public static Hero CreateChild(CharacterObject template, Settlement bornSettlement, Clan clan, int age)
		{
			ValueTuple<CampaignTime, CampaignTime> birthAndDeathDay = Campaign.Current.Models.HeroCreationModel.GetBirthAndDeathDay(template, true, age);
			CampaignTime item = birthAndDeathDay.Item1;
			CampaignTime item2 = birthAndDeathDay.Item2;
			Hero hero = HeroCreator.CreateHero(template, true, item, item2);
			HeroCreator.HeroInitializationArgs heroInitializationArgs = new HeroCreator.HeroInitializationArgs(hero, false).SetGenerateFirstAndFullName(true).SetBornSettlement(bornSettlement).SetClan(clan)
				.SetLevel(1);
			HeroCreator.InitializeHeroFromSettings(heroInitializationArgs.Hero, heroInitializationArgs);
			return hero;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00054170 File Offset: 0x00052370
		public static Hero CreateRelativeNotableHero(Hero relative)
		{
			CharacterObject randomTemplateByOccupation = Campaign.Current.Models.HeroCreationModel.GetRandomTemplateByOccupation(relative.Occupation, relative.HomeSettlement);
			ValueTuple<CampaignTime, CampaignTime> birthAndDeathDay = Campaign.Current.Models.HeroCreationModel.GetBirthAndDeathDay(randomTemplateByOccupation, true, -1);
			CampaignTime item = birthAndDeathDay.Item1;
			CampaignTime item2 = birthAndDeathDay.Item2;
			Hero hero = HeroCreator.CreateHero(randomTemplateByOccupation, true, item, item2);
			BodyProperties bodyPropertiesMin = relative.CharacterObject.GetBodyPropertiesMin(false);
			BodyProperties bodyPropertiesMin2 = randomTemplateByOccupation.GetBodyPropertiesMin(false);
			int defaultFaceSeed = relative.CharacterObject.GetDefaultFaceSeed(1);
			MBBodyProperty bodyPropertyRange = hero.CharacterObject.BodyPropertyRange;
			BodyProperties randomBodyProperties = BodyProperties.GetRandomBodyProperties(randomTemplateByOccupation.Race, randomTemplateByOccupation.IsFemale, bodyPropertiesMin, bodyPropertiesMin2, 1, defaultFaceSeed, bodyPropertyRange.HairTags, bodyPropertyRange.BeardTags, bodyPropertyRange.TattooTags, 0f);
			HeroCreator.HeroInitializationArgs heroInitializationArgs = new HeroCreator.HeroInitializationArgs(hero, false).SetBornSettlement(relative.HomeSettlement).SetCulture(relative.Culture).SetAppearance(new StaticBodyProperties?(randomBodyProperties.StaticProperties), -1f, -1f, -1, -1, -1)
				.SetGenerateFirstAndFullName(true);
			HeroCreator.InitializeHeroFromSettings(heroInitializationArgs.Hero, heroInitializationArgs);
			return hero;
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00054280 File Offset: 0x00052480
		public static bool CreateBasicHero(string stringId, CharacterObject character, out Hero hero, bool isAlive = true)
		{
			hero = Campaign.Current.CampaignObjectManager.Find<Hero>(stringId);
			if (hero == null)
			{
				ValueTuple<CampaignTime, CampaignTime> birthAndDeathDay = Campaign.Current.Models.HeroCreationModel.GetBirthAndDeathDay(character, isAlive, (int)character.Age);
				CampaignTime item = birthAndDeathDay.Item1;
				CampaignTime item2 = birthAndDeathDay.Item2;
				hero = HeroCreator.CreateHero(character, false, item, item2);
				HeroCreator.HeroInitializationArgs heroInitializationArgs = new HeroCreator.HeroInitializationArgs(hero, false);
				HeroCreator.InitializeHeroFromSettings(heroInitializationArgs.Hero, heroInitializationArgs);
				return true;
			}
			return false;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x000542F0 File Offset: 0x000524F0
		public static Hero DeliverOffSpring(Hero mother, Hero father, bool isOffspringFemale)
		{
			Debug.SilentAssert(mother.CharacterObject.Race == father.CharacterObject.Race, "", false, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\HeroCreator.cs", "DeliverOffSpring", 272);
			CharacterObject characterTemplateForOffspring = Campaign.Current.Models.HeroCreationModel.GetCharacterTemplateForOffspring(mother, father, isOffspringFemale);
			ValueTuple<CampaignTime, CampaignTime> birthAndDeathDay = Campaign.Current.Models.HeroCreationModel.GetBirthAndDeathDay(characterTemplateForOffspring, true, 0);
			CampaignTime item = birthAndDeathDay.Item1;
			CampaignTime item2 = birthAndDeathDay.Item2;
			Hero hero = HeroCreator.CreateHero(characterTemplateForOffspring, true, item, item2);
			HeroCreator.HeroInitializationArgs heroInitializationArgs = new HeroCreator.HeroInitializationArgs(hero, true).SetMother(mother).SetFather(father).SetIsFemale(isOffspringFemale)
				.SetOccupation(isOffspringFemale ? mother.Occupation : father.Occupation)
				.SetLevel(1)
				.SetGenerateFirstAndFullName(true);
			if (mother == Hero.MainHero || father == Hero.MainHero)
			{
				heroInitializationArgs.SetClan(Hero.MainHero.Clan).SetCulture(Hero.MainHero.Culture);
			}
			else
			{
				CultureObject cultureObject = ((MBRandom.RandomFloat < 0.5f) ? father.Culture : mother.Culture);
				heroInitializationArgs.SetClan(father.Clan).SetCulture(cultureObject);
			}
			HeroCreator.InitializeHeroFromSettings(heroInitializationArgs.Hero, heroInitializationArgs);
			return hero;
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00054420 File Offset: 0x00052620
		private static Hero CreateHero(CharacterObject character, bool useCharacterAsTemplate, CampaignTime birthDay, CampaignTime deathDay)
		{
			if (useCharacterAsTemplate)
			{
				Debug.Print("creating hero from template with id: " + character.StringId, 0, Debug.DebugColor.White, 17592186044416UL);
				character = CharacterObject.CreateFrom(character, null);
			}
			else
			{
				Debug.Print("creating hero for character with id: " + character.StringId, 0, Debug.DebugColor.White, 17592186044416UL);
			}
			return new Hero(character.StringId, character, birthDay, deathDay);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00054494 File Offset: 0x00052694
		private static void InitializeHeroFromSettings(Hero hero, HeroCreator.HeroInitializationArgs initializationArgs)
		{
			hero.Mother = initializationArgs.Mother;
			hero.Father = initializationArgs.Father;
			hero.IsFemale = initializationArgs.IsFemale;
			hero.BornSettlement = (initializationArgs.HasBornSettlementBeenSet ? initializationArgs.BornSettlement : Campaign.Current.Models.HeroCreationModel.GetBornSettlement(hero));
			hero.PreferredUpgradeFormation = initializationArgs.PreferredUpgradeFormation ?? Campaign.Current.Models.HeroCreationModel.GetPreferredUpgradeFormation(hero);
			hero.Clan = (initializationArgs.HasClanBeenSet ? initializationArgs.Clan : Campaign.Current.Models.HeroCreationModel.GetClan(hero));
			hero.Culture = initializationArgs.Culture ?? Campaign.Current.Models.HeroCreationModel.GetCulture(hero, hero.BornSettlement, hero.Clan);
			hero.StaticBodyProperties = initializationArgs.StaticBodyProperties ?? Campaign.Current.Models.HeroCreationModel.GetStaticBodyProperties(hero, initializationArgs.IsOffspring, 0.35f);
			hero.SupporterOf = initializationArgs.SupporterOf;
			hero.Level = initializationArgs.Level;
			hero.Weight = initializationArgs.Weight;
			hero.Build = initializationArgs.Build;
			if (initializationArgs.GenerateFirstAndFullName)
			{
				ValueTuple<TextObject, TextObject> valueTuple = Campaign.Current.Models.HeroCreationModel.GenerateFirstAndFullName(hero);
				TextObject item = valueTuple.Item1;
				TextObject item2 = valueTuple.Item2;
				hero.SetName(item2, item);
			}
			else
			{
				hero.SetName(initializationArgs.Name, initializationArgs.FirstName);
			}
			if (initializationArgs.Occupation != hero.Occupation)
			{
				hero.SetNewOccupation(initializationArgs.Occupation);
			}
			foreach (ValueTuple<TraitObject, int> valueTuple2 in Campaign.Current.Models.HeroCreationModel.GetTraitsForHero(hero))
			{
				TraitObject item3 = valueTuple2.Item1;
				int item4 = valueTuple2.Item2;
				hero.SetTraitLevel(item3, item4);
			}
			foreach (ValueTuple<SkillObject, int> valueTuple3 in Campaign.Current.Models.HeroCreationModel.GetDefaultSkillsForHero(hero))
			{
				SkillObject item5 = valueTuple3.Item1;
				int item6 = valueTuple3.Item2;
				hero.SetSkillValue(item5, item6);
			}
			if (initializationArgs.IsOffspring)
			{
				hero.HeroDeveloper.InitializeHeroDeveloper();
				hero.ClearTraits();
			}
			else if (hero.Age >= (float)Campaign.Current.Models.AgeModel.HeroComesOfAge)
			{
				hero.HeroDeveloper.InitializeHeroDeveloper();
			}
			Equipment civilianEquipment = Campaign.Current.Models.HeroCreationModel.GetCivilianEquipment(hero);
			EquipmentHelper.AssignHeroEquipmentFromEquipment(hero, civilianEquipment);
			Equipment battleEquipment = Campaign.Current.Models.HeroCreationModel.GetBattleEquipment(hero);
			EquipmentHelper.AssignHeroEquipmentFromEquipment(hero, battleEquipment);
			CampaignEventDispatcher.Instance.OnHeroCreated(initializationArgs.Hero, initializationArgs.IsOffspring);
		}

		// Token: 0x02000542 RID: 1346
		private class HeroInitializationArgs
		{
			// Token: 0x17000EFD RID: 3837
			// (get) Token: 0x06004D07 RID: 19719 RVA: 0x0017F9D6 File Offset: 0x0017DBD6
			public Hero Hero { get; }

			// Token: 0x17000EFE RID: 3838
			// (get) Token: 0x06004D08 RID: 19720 RVA: 0x0017F9DE File Offset: 0x0017DBDE
			// (set) Token: 0x06004D09 RID: 19721 RVA: 0x0017F9E6 File Offset: 0x0017DBE6
			public TextObject Name { get; private set; }

			// Token: 0x17000EFF RID: 3839
			// (get) Token: 0x06004D0A RID: 19722 RVA: 0x0017F9EF File Offset: 0x0017DBEF
			// (set) Token: 0x06004D0B RID: 19723 RVA: 0x0017F9F7 File Offset: 0x0017DBF7
			public TextObject FirstName { get; private set; }

			// Token: 0x17000F00 RID: 3840
			// (get) Token: 0x06004D0C RID: 19724 RVA: 0x0017FA00 File Offset: 0x0017DC00
			// (set) Token: 0x06004D0D RID: 19725 RVA: 0x0017FA08 File Offset: 0x0017DC08
			public Hero Mother { get; private set; }

			// Token: 0x17000F01 RID: 3841
			// (get) Token: 0x06004D0E RID: 19726 RVA: 0x0017FA11 File Offset: 0x0017DC11
			// (set) Token: 0x06004D0F RID: 19727 RVA: 0x0017FA19 File Offset: 0x0017DC19
			public Hero Father { get; private set; }

			// Token: 0x17000F02 RID: 3842
			// (get) Token: 0x06004D10 RID: 19728 RVA: 0x0017FA22 File Offset: 0x0017DC22
			// (set) Token: 0x06004D11 RID: 19729 RVA: 0x0017FA2A File Offset: 0x0017DC2A
			public bool IsFemale { get; private set; }

			// Token: 0x17000F03 RID: 3843
			// (get) Token: 0x06004D12 RID: 19730 RVA: 0x0017FA33 File Offset: 0x0017DC33
			// (set) Token: 0x06004D13 RID: 19731 RVA: 0x0017FA3B File Offset: 0x0017DC3B
			public Settlement BornSettlement { get; private set; }

			// Token: 0x17000F04 RID: 3844
			// (get) Token: 0x06004D14 RID: 19732 RVA: 0x0017FA44 File Offset: 0x0017DC44
			// (set) Token: 0x06004D15 RID: 19733 RVA: 0x0017FA4C File Offset: 0x0017DC4C
			public int Level { get; private set; }

			// Token: 0x17000F05 RID: 3845
			// (get) Token: 0x06004D16 RID: 19734 RVA: 0x0017FA55 File Offset: 0x0017DC55
			// (set) Token: 0x06004D17 RID: 19735 RVA: 0x0017FA5D File Offset: 0x0017DC5D
			public float Weight { get; private set; }

			// Token: 0x17000F06 RID: 3846
			// (get) Token: 0x06004D18 RID: 19736 RVA: 0x0017FA66 File Offset: 0x0017DC66
			// (set) Token: 0x06004D19 RID: 19737 RVA: 0x0017FA6E File Offset: 0x0017DC6E
			public float Build { get; private set; }

			// Token: 0x17000F07 RID: 3847
			// (get) Token: 0x06004D1A RID: 19738 RVA: 0x0017FA77 File Offset: 0x0017DC77
			// (set) Token: 0x06004D1B RID: 19739 RVA: 0x0017FA7F File Offset: 0x0017DC7F
			public StaticBodyProperties? StaticBodyProperties { get; private set; }

			// Token: 0x17000F08 RID: 3848
			// (get) Token: 0x06004D1C RID: 19740 RVA: 0x0017FA88 File Offset: 0x0017DC88
			// (set) Token: 0x06004D1D RID: 19741 RVA: 0x0017FA90 File Offset: 0x0017DC90
			public FormationClass? PreferredUpgradeFormation { get; private set; }

			// Token: 0x17000F09 RID: 3849
			// (get) Token: 0x06004D1E RID: 19742 RVA: 0x0017FA99 File Offset: 0x0017DC99
			// (set) Token: 0x06004D1F RID: 19743 RVA: 0x0017FAA1 File Offset: 0x0017DCA1
			public Clan Clan { get; private set; }

			// Token: 0x17000F0A RID: 3850
			// (get) Token: 0x06004D20 RID: 19744 RVA: 0x0017FAAA File Offset: 0x0017DCAA
			// (set) Token: 0x06004D21 RID: 19745 RVA: 0x0017FAB2 File Offset: 0x0017DCB2
			public CultureObject Culture { get; private set; }

			// Token: 0x17000F0B RID: 3851
			// (get) Token: 0x06004D22 RID: 19746 RVA: 0x0017FABB File Offset: 0x0017DCBB
			// (set) Token: 0x06004D23 RID: 19747 RVA: 0x0017FAC3 File Offset: 0x0017DCC3
			public Clan SupporterOf { get; private set; }

			// Token: 0x17000F0C RID: 3852
			// (get) Token: 0x06004D24 RID: 19748 RVA: 0x0017FACC File Offset: 0x0017DCCC
			// (set) Token: 0x06004D25 RID: 19749 RVA: 0x0017FAD4 File Offset: 0x0017DCD4
			public Occupation Occupation { get; private set; }

			// Token: 0x17000F0D RID: 3853
			// (get) Token: 0x06004D26 RID: 19750 RVA: 0x0017FADD File Offset: 0x0017DCDD
			// (set) Token: 0x06004D27 RID: 19751 RVA: 0x0017FAE5 File Offset: 0x0017DCE5
			public bool IsOffspring { get; private set; }

			// Token: 0x17000F0E RID: 3854
			// (get) Token: 0x06004D28 RID: 19752 RVA: 0x0017FAEE File Offset: 0x0017DCEE
			// (set) Token: 0x06004D29 RID: 19753 RVA: 0x0017FAF6 File Offset: 0x0017DCF6
			public bool GenerateFirstAndFullName { get; private set; }

			// Token: 0x17000F0F RID: 3855
			// (get) Token: 0x06004D2A RID: 19754 RVA: 0x0017FAFF File Offset: 0x0017DCFF
			// (set) Token: 0x06004D2B RID: 19755 RVA: 0x0017FB07 File Offset: 0x0017DD07
			public bool HasBornSettlementBeenSet { get; private set; }

			// Token: 0x17000F10 RID: 3856
			// (get) Token: 0x06004D2C RID: 19756 RVA: 0x0017FB10 File Offset: 0x0017DD10
			// (set) Token: 0x06004D2D RID: 19757 RVA: 0x0017FB18 File Offset: 0x0017DD18
			public bool HasClanBeenSet { get; private set; }

			// Token: 0x06004D2E RID: 19758 RVA: 0x0017FB24 File Offset: 0x0017DD24
			public HeroInitializationArgs(Hero hero, bool isOffspring)
			{
				DynamicBodyProperties dynamicBodyPropertiesBetweenMinMaxRange = CharacterHelper.GetDynamicBodyPropertiesBetweenMinMaxRange(hero.CharacterObject);
				this.Hero = hero;
				this.IsOffspring = isOffspring;
				this.Name = hero.Name;
				this.FirstName = hero.FirstName;
				this.Mother = hero.Mother;
				this.Father = hero.Father;
				this.IsFemale = hero.IsFemale;
				this.BornSettlement = null;
				this.Level = hero.Level;
				this.Weight = dynamicBodyPropertiesBetweenMinMaxRange.Weight;
				this.Build = dynamicBodyPropertiesBetweenMinMaxRange.Build;
				this.StaticBodyProperties = null;
				this.PreferredUpgradeFormation = null;
				this.Clan = null;
				this.SupporterOf = hero.SupporterOf;
				this.Occupation = hero.Occupation;
				this.Culture = null;
			}

			// Token: 0x06004D2F RID: 19759 RVA: 0x0017FBFC File Offset: 0x0017DDFC
			public HeroCreator.HeroInitializationArgs SetGenerateFirstAndFullName(bool value)
			{
				this.GenerateFirstAndFullName = value;
				return this;
			}

			// Token: 0x06004D30 RID: 19760 RVA: 0x0017FC06 File Offset: 0x0017DE06
			public HeroCreator.HeroInitializationArgs SetName(TextObject name)
			{
				this.Name = name;
				return this;
			}

			// Token: 0x06004D31 RID: 19761 RVA: 0x0017FC10 File Offset: 0x0017DE10
			public HeroCreator.HeroInitializationArgs SetFirstName(TextObject firstName)
			{
				this.FirstName = firstName;
				return this;
			}

			// Token: 0x06004D32 RID: 19762 RVA: 0x0017FC1A File Offset: 0x0017DE1A
			public HeroCreator.HeroInitializationArgs SetMother(Hero mother)
			{
				this.Mother = mother;
				return this;
			}

			// Token: 0x06004D33 RID: 19763 RVA: 0x0017FC24 File Offset: 0x0017DE24
			public HeroCreator.HeroInitializationArgs SetFather(Hero father)
			{
				this.Father = father;
				return this;
			}

			// Token: 0x06004D34 RID: 19764 RVA: 0x0017FC2E File Offset: 0x0017DE2E
			public HeroCreator.HeroInitializationArgs SetIsFemale(bool isFemale)
			{
				this.IsFemale = isFemale;
				return this;
			}

			// Token: 0x06004D35 RID: 19765 RVA: 0x0017FC38 File Offset: 0x0017DE38
			public HeroCreator.HeroInitializationArgs SetBornSettlement(Settlement bornSettlement)
			{
				this.BornSettlement = bornSettlement;
				this.HasBornSettlementBeenSet = true;
				return this;
			}

			// Token: 0x06004D36 RID: 19766 RVA: 0x0017FC49 File Offset: 0x0017DE49
			public HeroCreator.HeroInitializationArgs SetLevel(int level)
			{
				this.Level = level;
				return this;
			}

			// Token: 0x06004D37 RID: 19767 RVA: 0x0017FC54 File Offset: 0x0017DE54
			public HeroCreator.HeroInitializationArgs SetAppearance(StaticBodyProperties? staticBodyProperties, float weight = -1f, float build = -1f, int hair = -1, int beard = -1, int tattoo = -1)
			{
				if (weight > 0f)
				{
					this.Weight = weight;
				}
				if (build > 0f)
				{
					this.Build = build;
				}
				BodyProperties bodyProperties = new BodyProperties(new DynamicBodyProperties(this.Hero.Age, this.Weight, this.Build), staticBodyProperties ?? default(StaticBodyProperties));
				FaceGen.SetHair(ref bodyProperties, hair, beard, tattoo);
				this.StaticBodyProperties = new StaticBodyProperties?(bodyProperties.StaticProperties);
				return this;
			}

			// Token: 0x06004D38 RID: 19768 RVA: 0x0017FCDF File Offset: 0x0017DEDF
			public HeroCreator.HeroInitializationArgs SetPreferredUpgradeFormation(FormationClass preferredUpgradeFormation)
			{
				this.PreferredUpgradeFormation = new FormationClass?(preferredUpgradeFormation);
				return this;
			}

			// Token: 0x06004D39 RID: 19769 RVA: 0x0017FCEE File Offset: 0x0017DEEE
			public HeroCreator.HeroInitializationArgs SetClan(Clan clan)
			{
				this.Clan = clan;
				this.HasClanBeenSet = true;
				return this;
			}

			// Token: 0x06004D3A RID: 19770 RVA: 0x0017FCFF File Offset: 0x0017DEFF
			public HeroCreator.HeroInitializationArgs SetCulture(CultureObject culture)
			{
				this.Culture = culture;
				return this;
			}

			// Token: 0x06004D3B RID: 19771 RVA: 0x0017FD09 File Offset: 0x0017DF09
			public HeroCreator.HeroInitializationArgs SetSupporterOf(Clan supporterOf)
			{
				this.SupporterOf = supporterOf;
				return this;
			}

			// Token: 0x06004D3C RID: 19772 RVA: 0x0017FD13 File Offset: 0x0017DF13
			public HeroCreator.HeroInitializationArgs SetOccupation(Occupation occupation)
			{
				this.Occupation = occupation;
				return this;
			}
		}
	}
}
