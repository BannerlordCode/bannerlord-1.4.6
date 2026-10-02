using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200020B RID: 523
	public class CustomBattleTroopSupplier : IMissionTroopSupplier
	{
		// Token: 0x06001E41 RID: 7745 RVA: 0x0006835C File Offset: 0x0006655C
		public CustomBattleTroopSupplier(CustomBattleCombatant customBattleCombatant, bool isPlayerSide, bool isPlayerGeneral, bool isSallyOut, Func<BasicCharacterObject, bool> customAllocationConditions = null)
		{
			this._customBattleCombatant = customBattleCombatant;
			this._customAllocationConditions = customAllocationConditions;
			this._isPlayerSide = isPlayerSide;
			this._isPlayerGeneral = isPlayerSide && isPlayerGeneral;
			this._isSallyOut = isSallyOut;
			this.ArrangePriorities();
			this._nextTroopRank = 0;
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x000683AC File Offset: 0x000665AC
		private void ArrangePriorities()
		{
			this._characters = new PriorityQueue<float, BasicCharacterObject>(new GenericComparer<float>());
			int[] array = new int[8];
			int[] array2 = new int[8];
			int i;
			int j;
			for (i = 0; i < 8; i = j + 1)
			{
				array[i] = this._customBattleCombatant.Characters.Count<BasicCharacterObject>((BasicCharacterObject character) => character.DefaultFormationClass == (FormationClass)i);
				j = i;
			}
			UnitSpawnPrioritizations unitSpawnPrioritizations = (this._isPlayerSide ? Game.Current.UnitSpawnPrioritization : UnitSpawnPrioritizations.HighLevel);
			int num = array.Sum();
			float num2 = 1000f;
			foreach (BasicCharacterObject basicCharacterObject in this._customBattleCombatant.Characters)
			{
				FormationClass formationClass = basicCharacterObject.GetFormationClass();
				float num3;
				if (this._isSallyOut)
				{
					num3 = this.GetSallyOutAmbushProbabilityOfTroop(basicCharacterObject, num, ref num2);
				}
				else
				{
					num3 = this.GetDefaultProbabilityOfTroop(basicCharacterObject, num, unitSpawnPrioritizations, ref num2, ref array, ref array2);
				}
				array[(int)formationClass]--;
				array2[(int)formationClass]++;
				this._characters.Enqueue(num3, basicCharacterObject);
			}
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x000684F8 File Offset: 0x000666F8
		private float GetSallyOutAmbushProbabilityOfTroop(BasicCharacterObject character, int troopCountTotal, ref float heroProbability)
		{
			float num = 0f;
			if (character.IsHero)
			{
				float num2 = heroProbability;
				heroProbability = num2 - 1f;
				num = num2;
			}
			else
			{
				num += (float)character.Level;
				if (character.HasMount())
				{
					num += 100f;
				}
			}
			return num;
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x00068540 File Offset: 0x00066740
		private float GetDefaultProbabilityOfTroop(BasicCharacterObject character, int troopCountTotal, UnitSpawnPrioritizations unitSpawnPrioritization, ref float heroProbability, ref int[] troopCountByFormationType, ref int[] enqueuedTroopCountByFormationType)
		{
			FormationClass formationClass = character.GetFormationClass();
			float num = (float)troopCountByFormationType[(int)formationClass] / (float)((unitSpawnPrioritization == UnitSpawnPrioritizations.Homogeneous) ? (enqueuedTroopCountByFormationType[(int)formationClass] + 1) : troopCountTotal);
			float num2;
			if (!character.IsHero)
			{
				num2 = num;
			}
			else
			{
				float num3 = heroProbability;
				heroProbability = num3 - 1f;
				num2 = num3;
			}
			float num4 = num2;
			if (!character.IsHero && (unitSpawnPrioritization == UnitSpawnPrioritizations.HighLevel || unitSpawnPrioritization == UnitSpawnPrioritizations.LowLevel))
			{
				num4 += (float)character.Level;
				if (unitSpawnPrioritization == UnitSpawnPrioritizations.LowLevel)
				{
					num4 *= -1f;
				}
			}
			return num4;
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x000685B0 File Offset: 0x000667B0
		public IEnumerable<IAgentOriginBase> SupplyTroops(int numberToAllocate)
		{
			List<BasicCharacterObject> list = this.AllocateTroops(numberToAllocate);
			CustomBattleAgentOrigin[] array = new CustomBattleAgentOrigin[list.Count];
			this._numAllocated += list.Count;
			for (int i = 0; i < array.Length; i++)
			{
				UniqueTroopDescriptor uniqueTroopDescriptor = new UniqueTroopDescriptor(Game.Current.NextUniqueTroopSeed);
				CustomBattleAgentOrigin[] array2 = array;
				int num = i;
				CustomBattleCombatant customBattleCombatant = this._customBattleCombatant;
				BasicCharacterObject basicCharacterObject = list[i];
				bool isPlayerSide = this._isPlayerSide;
				int nextTroopRank = this._nextTroopRank;
				this._nextTroopRank = nextTroopRank + 1;
				array2[num] = new CustomBattleAgentOrigin(customBattleCombatant, basicCharacterObject, this, isPlayerSide, nextTroopRank, uniqueTroopDescriptor);
			}
			if (array.Length < numberToAllocate)
			{
				this._anyTroopRemainsToBeSupplied = false;
			}
			return array;
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x00068644 File Offset: 0x00066844
		public IAgentOriginBase SupplyOneTroop()
		{
			BasicCharacterObject basicCharacterObject = this.AllocateTroop();
			if (basicCharacterObject != null)
			{
				UniqueTroopDescriptor uniqueTroopDescriptor = new UniqueTroopDescriptor(Game.Current.NextUniqueTroopSeed);
				CustomBattleCombatant customBattleCombatant = this._customBattleCombatant;
				BasicCharacterObject basicCharacterObject2 = basicCharacterObject;
				bool isPlayerSide = this._isPlayerSide;
				int nextTroopRank = this._nextTroopRank;
				this._nextTroopRank = nextTroopRank + 1;
				return new CustomBattleAgentOrigin(customBattleCombatant, basicCharacterObject2, this, isPlayerSide, nextTroopRank, uniqueTroopDescriptor);
			}
			this._anyTroopRemainsToBeSupplied = false;
			return null;
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x0006869C File Offset: 0x0006689C
		public IEnumerable<IAgentOriginBase> GetAllTroops()
		{
			CustomBattleAgentOrigin[] array = new CustomBattleAgentOrigin[this._customBattleCombatant.Characters.Count<BasicCharacterObject>()];
			int num = 0;
			foreach (BasicCharacterObject basicCharacterObject in this._customBattleCombatant.Characters)
			{
				array[num] = new CustomBattleAgentOrigin(this._customBattleCombatant, basicCharacterObject, this, this._isPlayerSide, -1, default(UniqueTroopDescriptor));
				num++;
			}
			return array;
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x00068728 File Offset: 0x00066928
		public BasicCharacterObject GetGeneralCharacter()
		{
			return this._customBattleCombatant.General;
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x00068738 File Offset: 0x00066938
		private List<BasicCharacterObject> AllocateTroops(int numberToAllocate)
		{
			if (numberToAllocate > this._characters.Count)
			{
				numberToAllocate = this._characters.Count;
			}
			List<BasicCharacterObject> list = new List<BasicCharacterObject>();
			while (numberToAllocate > 0 && this._characters.Count > 0)
			{
				BasicCharacterObject basicCharacterObject = this._characters.DequeueValue();
				if (this._customAllocationConditions == null || this._customAllocationConditions(basicCharacterObject))
				{
					list.Add(basicCharacterObject);
					numberToAllocate--;
				}
			}
			return list;
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x000687AC File Offset: 0x000669AC
		private BasicCharacterObject AllocateTroop()
		{
			BasicCharacterObject basicCharacterObject = null;
			while (this._characters.Count > 0)
			{
				BasicCharacterObject basicCharacterObject2 = this._characters.DequeueValue();
				if (this._customAllocationConditions == null || this._customAllocationConditions(basicCharacterObject2))
				{
					basicCharacterObject = basicCharacterObject2;
					break;
				}
			}
			return basicCharacterObject;
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x000687F2 File Offset: 0x000669F2
		public void OnTroopWounded()
		{
			this._numWounded++;
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x00068802 File Offset: 0x00066A02
		public void OnTroopKilled()
		{
			this._numKilled++;
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x00068812 File Offset: 0x00066A12
		public void OnTroopRouted()
		{
			this._numRouted++;
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001E4E RID: 7758 RVA: 0x00068822 File Offset: 0x00066A22
		public int NumRemovedTroops
		{
			get
			{
				return this._numWounded + this._numKilled + this._numRouted;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001E4F RID: 7759 RVA: 0x00068838 File Offset: 0x00066A38
		public int NumTroopsNotSupplied
		{
			get
			{
				return this._characters.Count - this._numAllocated;
			}
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0006884C File Offset: 0x00066A4C
		public int GetNumberOfPlayerControllableTroops()
		{
			return this._customBattleCombatant.CountOfCharacters;
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x00068859 File Offset: 0x00066A59
		public bool AnyTroopRemainsToBeSupplied
		{
			get
			{
				return this._anyTroopRemainsToBeSupplied;
			}
		}

		// Token: 0x04000A54 RID: 2644
		private readonly CustomBattleCombatant _customBattleCombatant;

		// Token: 0x04000A55 RID: 2645
		private PriorityQueue<float, BasicCharacterObject> _characters;

		// Token: 0x04000A56 RID: 2646
		private int _numAllocated;

		// Token: 0x04000A57 RID: 2647
		private int _numWounded;

		// Token: 0x04000A58 RID: 2648
		private int _numKilled;

		// Token: 0x04000A59 RID: 2649
		private int _numRouted;

		// Token: 0x04000A5A RID: 2650
		private Func<BasicCharacterObject, bool> _customAllocationConditions;

		// Token: 0x04000A5B RID: 2651
		private bool _anyTroopRemainsToBeSupplied = true;

		// Token: 0x04000A5C RID: 2652
		private readonly bool _isPlayerSide;

		// Token: 0x04000A5D RID: 2653
		private readonly bool _isPlayerGeneral;

		// Token: 0x04000A5E RID: 2654
		private readonly bool _isSallyOut;

		// Token: 0x04000A5F RID: 2655
		private int _nextTroopRank;
	}
}
