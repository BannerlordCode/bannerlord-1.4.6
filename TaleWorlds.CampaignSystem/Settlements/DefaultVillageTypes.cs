using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Settlements
{
	// Token: 0x020003BD RID: 957
	public class DefaultVillageTypes
	{
		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x060038D2 RID: 14546 RVA: 0x000E96E2 File Offset: 0x000E78E2
		private static DefaultVillageTypes Instance
		{
			get
			{
				return Campaign.Current.DefaultVillageTypes;
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x060038D3 RID: 14547 RVA: 0x000E96EE File Offset: 0x000E78EE
		// (set) Token: 0x060038D4 RID: 14548 RVA: 0x000E96F6 File Offset: 0x000E78F6
		public IList<ItemObject> ConsumableRawItems { get; private set; }

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x060038D5 RID: 14549 RVA: 0x000E96FF File Offset: 0x000E78FF
		public static VillageType EuropeHorseRanch
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeEuropeHorseRanch;
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x060038D6 RID: 14550 RVA: 0x000E970B File Offset: 0x000E790B
		public static VillageType BattanianHorseRanch
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeBattanianHorseRanch;
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x060038D7 RID: 14551 RVA: 0x000E9717 File Offset: 0x000E7917
		public static VillageType SturgianHorseRanch
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeSturgianHorseRanch;
			}
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x060038D8 RID: 14552 RVA: 0x000E9723 File Offset: 0x000E7923
		public static VillageType VlandianHorseRanch
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeVlandianHorseRanch;
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x060038D9 RID: 14553 RVA: 0x000E972F File Offset: 0x000E792F
		public static VillageType SteppeHorseRanch
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeSteppeHorseRanch;
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x060038DA RID: 14554 RVA: 0x000E973B File Offset: 0x000E793B
		public static VillageType DesertHorseRanch
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeDesertHorseRanch;
			}
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x060038DB RID: 14555 RVA: 0x000E9747 File Offset: 0x000E7947
		public static VillageType WheatFarm
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeWheatFarm;
			}
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x060038DC RID: 14556 RVA: 0x000E9753 File Offset: 0x000E7953
		public static VillageType Lumberjack
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeLumberjack;
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x060038DD RID: 14557 RVA: 0x000E975F File Offset: 0x000E795F
		public static VillageType ClayMine
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeClayMine;
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x000E976B File Offset: 0x000E796B
		public static VillageType SaltMine
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeSaltMine;
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x000E9777 File Offset: 0x000E7977
		public static VillageType IronMine
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeIronMine;
			}
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x060038E0 RID: 14560 RVA: 0x000E9783 File Offset: 0x000E7983
		public static VillageType Fisherman
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeFisherman;
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x060038E1 RID: 14561 RVA: 0x000E978F File Offset: 0x000E798F
		public static VillageType CattleRange
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeCattleRange;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x060038E2 RID: 14562 RVA: 0x000E979B File Offset: 0x000E799B
		public static VillageType SheepFarm
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeSheepFarm;
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x060038E3 RID: 14563 RVA: 0x000E97A7 File Offset: 0x000E79A7
		public static VillageType HogFarm
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeHogFarm;
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x060038E4 RID: 14564 RVA: 0x000E97B3 File Offset: 0x000E79B3
		public static VillageType VineYard
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeVineYard;
			}
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x060038E5 RID: 14565 RVA: 0x000E97BF File Offset: 0x000E79BF
		public static VillageType FlaxPlant
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeFlaxPlant;
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x060038E6 RID: 14566 RVA: 0x000E97CB File Offset: 0x000E79CB
		public static VillageType DateFarm
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeDateFarm;
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x060038E7 RID: 14567 RVA: 0x000E97D7 File Offset: 0x000E79D7
		public static VillageType OliveTrees
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeOliveTrees;
			}
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x060038E8 RID: 14568 RVA: 0x000E97E3 File Offset: 0x000E79E3
		public static VillageType SilkPlant
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeSilkPlant;
			}
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x060038E9 RID: 14569 RVA: 0x000E97EF File Offset: 0x000E79EF
		public static VillageType SilverMine
		{
			get
			{
				return DefaultVillageTypes.Instance.VillageTypeSilverMine;
			}
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x060038EA RID: 14570 RVA: 0x000E97FB File Offset: 0x000E79FB
		// (set) Token: 0x060038EB RID: 14571 RVA: 0x000E9803 File Offset: 0x000E7A03
		internal VillageType VillageTypeEuropeHorseRanch { get; private set; }

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x060038EC RID: 14572 RVA: 0x000E980C File Offset: 0x000E7A0C
		// (set) Token: 0x060038ED RID: 14573 RVA: 0x000E9814 File Offset: 0x000E7A14
		internal VillageType VillageTypeBattanianHorseRanch { get; private set; }

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x060038EE RID: 14574 RVA: 0x000E981D File Offset: 0x000E7A1D
		// (set) Token: 0x060038EF RID: 14575 RVA: 0x000E9825 File Offset: 0x000E7A25
		internal VillageType VillageTypeSturgianHorseRanch { get; private set; }

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x060038F0 RID: 14576 RVA: 0x000E982E File Offset: 0x000E7A2E
		// (set) Token: 0x060038F1 RID: 14577 RVA: 0x000E9836 File Offset: 0x000E7A36
		internal VillageType VillageTypeVlandianHorseRanch { get; private set; }

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x060038F2 RID: 14578 RVA: 0x000E983F File Offset: 0x000E7A3F
		// (set) Token: 0x060038F3 RID: 14579 RVA: 0x000E9847 File Offset: 0x000E7A47
		internal VillageType VillageTypeSteppeHorseRanch { get; private set; }

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x060038F4 RID: 14580 RVA: 0x000E9850 File Offset: 0x000E7A50
		// (set) Token: 0x060038F5 RID: 14581 RVA: 0x000E9858 File Offset: 0x000E7A58
		internal VillageType VillageTypeDesertHorseRanch { get; private set; }

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x060038F6 RID: 14582 RVA: 0x000E9861 File Offset: 0x000E7A61
		// (set) Token: 0x060038F7 RID: 14583 RVA: 0x000E9869 File Offset: 0x000E7A69
		internal VillageType VillageTypeWheatFarm { get; private set; }

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x060038F8 RID: 14584 RVA: 0x000E9872 File Offset: 0x000E7A72
		// (set) Token: 0x060038F9 RID: 14585 RVA: 0x000E987A File Offset: 0x000E7A7A
		internal VillageType VillageTypeLumberjack { get; private set; }

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x060038FA RID: 14586 RVA: 0x000E9883 File Offset: 0x000E7A83
		// (set) Token: 0x060038FB RID: 14587 RVA: 0x000E988B File Offset: 0x000E7A8B
		internal VillageType VillageTypeClayMine { get; private set; }

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x060038FC RID: 14588 RVA: 0x000E9894 File Offset: 0x000E7A94
		// (set) Token: 0x060038FD RID: 14589 RVA: 0x000E989C File Offset: 0x000E7A9C
		internal VillageType VillageTypeSaltMine { get; private set; }

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x060038FE RID: 14590 RVA: 0x000E98A5 File Offset: 0x000E7AA5
		// (set) Token: 0x060038FF RID: 14591 RVA: 0x000E98AD File Offset: 0x000E7AAD
		internal VillageType VillageTypeIronMine { get; private set; }

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06003900 RID: 14592 RVA: 0x000E98B6 File Offset: 0x000E7AB6
		// (set) Token: 0x06003901 RID: 14593 RVA: 0x000E98BE File Offset: 0x000E7ABE
		internal VillageType VillageTypeFisherman { get; private set; }

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06003902 RID: 14594 RVA: 0x000E98C7 File Offset: 0x000E7AC7
		// (set) Token: 0x06003903 RID: 14595 RVA: 0x000E98CF File Offset: 0x000E7ACF
		internal VillageType VillageTypeCattleRange { get; private set; }

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06003904 RID: 14596 RVA: 0x000E98D8 File Offset: 0x000E7AD8
		// (set) Token: 0x06003905 RID: 14597 RVA: 0x000E98E0 File Offset: 0x000E7AE0
		internal VillageType VillageTypeSheepFarm { get; private set; }

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06003906 RID: 14598 RVA: 0x000E98E9 File Offset: 0x000E7AE9
		// (set) Token: 0x06003907 RID: 14599 RVA: 0x000E98F1 File Offset: 0x000E7AF1
		internal VillageType VillageTypeHogFarm { get; private set; }

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06003908 RID: 14600 RVA: 0x000E98FA File Offset: 0x000E7AFA
		// (set) Token: 0x06003909 RID: 14601 RVA: 0x000E9902 File Offset: 0x000E7B02
		internal VillageType VillageTypeTrapper { get; private set; }

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x0600390A RID: 14602 RVA: 0x000E990B File Offset: 0x000E7B0B
		// (set) Token: 0x0600390B RID: 14603 RVA: 0x000E9913 File Offset: 0x000E7B13
		internal VillageType VillageTypeVineYard { get; private set; }

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x0600390C RID: 14604 RVA: 0x000E991C File Offset: 0x000E7B1C
		// (set) Token: 0x0600390D RID: 14605 RVA: 0x000E9924 File Offset: 0x000E7B24
		internal VillageType VillageTypeFlaxPlant { get; private set; }

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x0600390E RID: 14606 RVA: 0x000E992D File Offset: 0x000E7B2D
		// (set) Token: 0x0600390F RID: 14607 RVA: 0x000E9935 File Offset: 0x000E7B35
		internal VillageType VillageTypeDateFarm { get; private set; }

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06003910 RID: 14608 RVA: 0x000E993E File Offset: 0x000E7B3E
		// (set) Token: 0x06003911 RID: 14609 RVA: 0x000E9946 File Offset: 0x000E7B46
		internal VillageType VillageTypeOliveTrees { get; private set; }

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06003912 RID: 14610 RVA: 0x000E994F File Offset: 0x000E7B4F
		// (set) Token: 0x06003913 RID: 14611 RVA: 0x000E9957 File Offset: 0x000E7B57
		internal VillageType VillageTypeSilkPlant { get; private set; }

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06003914 RID: 14612 RVA: 0x000E9960 File Offset: 0x000E7B60
		// (set) Token: 0x06003915 RID: 14613 RVA: 0x000E9968 File Offset: 0x000E7B68
		internal VillageType VillageTypeSilverMine { get; private set; }

		// Token: 0x06003916 RID: 14614 RVA: 0x000E9971 File Offset: 0x000E7B71
		public DefaultVillageTypes()
		{
			this.ConsumableRawItems = new List<ItemObject>();
			this.RegisterAll();
			this.AddProductions();
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000E9990 File Offset: 0x000E7B90
		private void RegisterAll()
		{
			this.VillageTypeWheatFarm = this.Create("wheat_farm");
			this.VillageTypeEuropeHorseRanch = this.Create("europe_horse_ranch");
			this.VillageTypeSteppeHorseRanch = this.Create("steppe_horse_ranch");
			this.VillageTypeDesertHorseRanch = this.Create("desert_horse_ranch");
			this.VillageTypeBattanianHorseRanch = this.Create("battanian_horse_ranch");
			this.VillageTypeSturgianHorseRanch = this.Create("sturgian_horse_ranch");
			this.VillageTypeVlandianHorseRanch = this.Create("vlandian_horse_ranch");
			this.VillageTypeLumberjack = this.Create("lumberjack");
			this.VillageTypeClayMine = this.Create("clay_mine");
			this.VillageTypeSaltMine = this.Create("salt_mine");
			this.VillageTypeIronMine = this.Create("iron_mine");
			this.VillageTypeFisherman = this.Create("fisherman");
			this.VillageTypeCattleRange = this.Create("cattle_farm");
			this.VillageTypeSheepFarm = this.Create("sheep_farm");
			this.VillageTypeHogFarm = this.Create("swine_farm");
			this.VillageTypeVineYard = this.Create("vineyard");
			this.VillageTypeFlaxPlant = this.Create("flax_plant");
			this.VillageTypeDateFarm = this.Create("date_farm");
			this.VillageTypeOliveTrees = this.Create("olive_trees");
			this.VillageTypeSilkPlant = this.Create("silk_plant");
			this.VillageTypeSilverMine = this.Create("silver_mine");
			this.VillageTypeTrapper = this.Create("trapper");
			this.InitializeAll();
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000E9B19 File Offset: 0x000E7D19
		private VillageType Create(string stringId)
		{
			return Game.Current.ObjectManager.RegisterPresumedObject<VillageType>(new VillageType(stringId));
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x000E9B30 File Offset: 0x000E7D30
		private void InitializeAll()
		{
			this.VillageTypeWheatFarm.Initialize(new TextObject("{=BPPG2XF7}Wheat Farm", null), "wheat_farm", "wheat_farm_ucon", "wheat_farm_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 50f)
			});
			this.VillageTypeEuropeHorseRanch.Initialize(new TextObject("{=eEh752CZ}Horse Farm", null), "europe_horse_ranch", "ranch_ucon", "europe_horse_ranch_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeSteppeHorseRanch.Initialize(new TextObject("{=eEh752CZ}Horse Farm", null), "steppe_horse_ranch", "ranch_ucon", "steppe_horse_ranch_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeDesertHorseRanch.Initialize(new TextObject("{=eEh752CZ}Horse Farm", null), "desert_horse_ranch", "ranch_ucon", "desert_horse_ranch_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeBattanianHorseRanch.Initialize(new TextObject("{=eEh752CZ}Horse Farm", null), "battanian_horse_ranch", "ranch_ucon", "desert_horse_ranch_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeSturgianHorseRanch.Initialize(new TextObject("{=eEh752CZ}Horse Farm", null), "sturgian_horse_ranch", "ranch_ucon", "desert_horse_ranch_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeVlandianHorseRanch.Initialize(new TextObject("{=eEh752CZ}Horse Farm", null), "vlandian_horse_ranch", "ranch_ucon", "desert_horse_ranch_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeLumberjack.Initialize(new TextObject("{=YYl1W2jU}Forester", null), "lumberjack", "lumberjack_ucon", "lumberjack_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeClayMine.Initialize(new TextObject("{=myuzMhOn}Clay Pits", null), "clay_mine", "clay_mine_ucon", "clay_mine_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeSaltMine.Initialize(new TextObject("{=3aOIY6wl}Salt Mine", null), "salt_mine", "salt_mine_ucon", "salt_mine_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeIronMine.Initialize(new TextObject("{=rHcVKSbA}Iron Mine", null), "iron_mine", "iron_mine_ucon", "iron_mine_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeFisherman.Initialize(new TextObject("{=XpREJNHD}Fishers", null), "fisherman", "fisherman_ucon", "fisherman_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeCattleRange.Initialize(new TextObject("{=bW3csuSZ}Cattle Farms", null), "cattle_farm", "ranch_ucon", "cattle_farm_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeSheepFarm.Initialize(new TextObject("{=QbKbGu2h}Sheep Farms", null), "sheep_farm", "ranch_ucon", "sheep_farm_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeHogFarm.Initialize(new TextObject("{=vqSHB7mJ}Swine Farm", null), "swine_farm", "swine_farm_ucon", "swine_farm_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeVineYard.Initialize(new TextObject("{=ZtxWTS9V}Vineyard", null), "vineyard", "vineyard_ucon", "vineyard_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeFlaxPlant.Initialize(new TextObject("{=Z8ntYx0Y}Flax Field", null), "flax_plant", "flax_plant_ucon", "flax_plant_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeDateFarm.Initialize(new TextObject("{=2NR2E663}Palm Orchard", null), "date_farm", "date_farm_ucon", "date_farm_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeOliveTrees.Initialize(new TextObject("{=ewrkbwI9}Olive Trees", null), "date_farm", "date_farm_ucon", "date_farm_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeSilkPlant.Initialize(new TextObject("{=wTyq7LaM}Silkworm Farm", null), "silk_plant", "silk_plant_ucon", "silk_plant_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeSilverMine.Initialize(new TextObject("{=aJLQz9iZ}Silver Mine", null), "silver_mine", "silver_mine_ucon", "silver_mine_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
			this.VillageTypeTrapper.Initialize(new TextObject("{=RREyouKr}Trapper", null), "trapper", "trapper_ucon", "trapper_burned", new ValueTuple<ItemObject, float>[]
			{
				new ValueTuple<ItemObject, float>(DefaultItems.Grain, 3f)
			});
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000EA0EC File Offset: 0x000E82EC
		private void AddProductions()
		{
			this.AddProductions(this.VillageTypeWheatFarm, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("cow", 0.2f),
				new ValueTuple<string, float>("sheep", 0.4f),
				new ValueTuple<string, float>("hog", 0.8f)
			});
			this.AddProductions(this.VillageTypeEuropeHorseRanch, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("empire_horse", 2.1f),
				new ValueTuple<string, float>("t2_empire_horse", 0.5f),
				new ValueTuple<string, float>("t3_empire_horse", 0.07f),
				new ValueTuple<string, float>("sumpter_horse", 0.5f),
				new ValueTuple<string, float>("mule", 0.5f),
				new ValueTuple<string, float>("saddle_horse", 0.5f),
				new ValueTuple<string, float>("old_horse", 0.5f),
				new ValueTuple<string, float>("hunter", 0.2f),
				new ValueTuple<string, float>("charger", 0.2f)
			});
			this.AddProductions(this.VillageTypeSturgianHorseRanch, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("sturgia_horse", 2.5f),
				new ValueTuple<string, float>("t2_sturgia_horse", 0.7f),
				new ValueTuple<string, float>("t3_sturgia_horse", 0.1f),
				new ValueTuple<string, float>("sumpter_horse", 0.5f),
				new ValueTuple<string, float>("mule", 0.5f),
				new ValueTuple<string, float>("saddle_horse", 0.5f),
				new ValueTuple<string, float>("old_horse", 0.5f),
				new ValueTuple<string, float>("hunter", 0.2f),
				new ValueTuple<string, float>("charger", 0.2f)
			});
			this.AddProductions(this.VillageTypeVlandianHorseRanch, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("vlandia_horse", 2.1f),
				new ValueTuple<string, float>("t2_vlandia_horse", 0.4f),
				new ValueTuple<string, float>("t3_vlandia_horse", 0.08f),
				new ValueTuple<string, float>("sumpter_horse", 0.5f),
				new ValueTuple<string, float>("mule", 0.5f),
				new ValueTuple<string, float>("saddle_horse", 0.5f),
				new ValueTuple<string, float>("old_horse", 0.5f),
				new ValueTuple<string, float>("hunter", 0.2f),
				new ValueTuple<string, float>("charger", 0.2f)
			});
			this.AddProductions(this.VillageTypeBattanianHorseRanch, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("battania_horse", 2.3f),
				new ValueTuple<string, float>("t2_battania_horse", 0.7f),
				new ValueTuple<string, float>("t3_battania_horse", 0.09f),
				new ValueTuple<string, float>("sumpter_horse", 0.5f),
				new ValueTuple<string, float>("mule", 0.5f),
				new ValueTuple<string, float>("saddle_horse", 0.5f),
				new ValueTuple<string, float>("old_horse", 0.5f),
				new ValueTuple<string, float>("hunter", 0.2f),
				new ValueTuple<string, float>("charger", 0.2f)
			});
			this.AddProductions(this.VillageTypeSteppeHorseRanch, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("khuzait_horse", 1.8f),
				new ValueTuple<string, float>("t2_khuzait_horse", 0.4f),
				new ValueTuple<string, float>("t3_khuzait_horse", 0.05f),
				new ValueTuple<string, float>("sumpter_horse", 0.5f),
				new ValueTuple<string, float>("mule", 0.5f)
			});
			this.AddProductions(this.VillageTypeDesertHorseRanch, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("aserai_horse", 1.7f),
				new ValueTuple<string, float>("t2_aserai_horse", 0.3f),
				new ValueTuple<string, float>("t3_aserai_horse", 0.05f),
				new ValueTuple<string, float>("camel", 0.3f),
				new ValueTuple<string, float>("war_camel", 0.08f),
				new ValueTuple<string, float>("pack_camel", 0.3f),
				new ValueTuple<string, float>("sumpter_horse", 0.4f),
				new ValueTuple<string, float>("mule", 0.5f)
			});
			this.AddProductions(this.VillageTypeCattleRange, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("cow", 2f),
				new ValueTuple<string, float>("butter", 4f),
				new ValueTuple<string, float>("cheese", 4f)
			});
			this.AddProductions(this.VillageTypeSheepFarm, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("sheep", 4f),
				new ValueTuple<string, float>("wool", 10f),
				new ValueTuple<string, float>("butter", 2f),
				new ValueTuple<string, float>("cheese", 2f)
			});
			this.AddProductions(this.VillageTypeHogFarm, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("hog", 8f),
				new ValueTuple<string, float>("butter", 2f),
				new ValueTuple<string, float>("cheese", 2f)
			});
			this.AddProductions(this.VillageTypeLumberjack, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("hardwood", 18f)
			});
			this.AddProductions(this.VillageTypeClayMine, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("clay", 10f)
			});
			this.AddProductions(this.VillageTypeSaltMine, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("salt", 15f)
			});
			this.AddProductions(this.VillageTypeIronMine, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("iron", 10f)
			});
			this.AddProductions(this.VillageTypeFisherman, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("fish", 28f)
			});
			this.AddProductions(this.VillageTypeVineYard, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("grape", 11f)
			});
			this.AddProductions(this.VillageTypeFlaxPlant, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("flax", 18f)
			});
			this.AddProductions(this.VillageTypeDateFarm, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("date_fruit", 8f)
			});
			this.AddProductions(this.VillageTypeOliveTrees, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("olives", 12f)
			});
			this.AddProductions(this.VillageTypeSilkPlant, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("cotton", 8f)
			});
			this.AddProductions(this.VillageTypeSilverMine, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("silver", 3f)
			});
			this.AddProductions(this.VillageTypeTrapper, new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("fur", 1.4f)
			});
			this.ConsumableRawItems.Add(Game.Current.ObjectManager.GetObject<ItemObject>("grain"));
			this.ConsumableRawItems.Add(Game.Current.ObjectManager.GetObject<ItemObject>("cheese"));
			this.ConsumableRawItems.Add(Game.Current.ObjectManager.GetObject<ItemObject>("butter"));
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x000EA942 File Offset: 0x000E8B42
		private void AddProductions(VillageType villageType, ValueTuple<string, float>[] productions)
		{
			villageType.AddProductions(productions.Select<ValueTuple<string, float>, ValueTuple<ItemObject, float>>((ValueTuple<string, float> p) => new ValueTuple<ItemObject, float>(Game.Current.ObjectManager.GetObject<ItemObject>(p.Item1), p.Item2)));
		}
	}
}
