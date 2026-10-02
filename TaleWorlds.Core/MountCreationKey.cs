using System;
using System.Globalization;

namespace TaleWorlds.Core
{
	// Token: 0x020000C0 RID: 192
	public class MountCreationKey
	{
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x000229CE File Offset: 0x00020BCE
		// (set) Token: 0x06000AAC RID: 2732 RVA: 0x000229D6 File Offset: 0x00020BD6
		public byte _leftFrontLegColorIndex { get; private set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x000229DF File Offset: 0x00020BDF
		// (set) Token: 0x06000AAE RID: 2734 RVA: 0x000229E7 File Offset: 0x00020BE7
		public byte _rightFrontLegColorIndex { get; private set; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x000229F0 File Offset: 0x00020BF0
		// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x000229F8 File Offset: 0x00020BF8
		public byte _leftBackLegColorIndex { get; private set; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00022A01 File Offset: 0x00020C01
		// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x00022A09 File Offset: 0x00020C09
		public byte _rightBackLegColorIndex { get; private set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00022A12 File Offset: 0x00020C12
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x00022A1A File Offset: 0x00020C1A
		public byte MaterialIndex { get; private set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00022A23 File Offset: 0x00020C23
		// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x00022A2B File Offset: 0x00020C2B
		public byte MeshMultiplierIndex { get; private set; }

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00022A34 File Offset: 0x00020C34
		public MountCreationKey(byte leftFrontLegColorIndex, byte rightFrontLegColorIndex, byte leftBackLegColorIndex, byte rightBackLegColorIndex, byte materialIndex, byte meshMultiplierIndex)
		{
			if (leftFrontLegColorIndex == 3 || rightFrontLegColorIndex == 3)
			{
				leftFrontLegColorIndex = 3;
				rightFrontLegColorIndex = 3;
			}
			this._leftFrontLegColorIndex = leftFrontLegColorIndex;
			this._rightFrontLegColorIndex = rightFrontLegColorIndex;
			this._leftBackLegColorIndex = leftBackLegColorIndex;
			this._rightBackLegColorIndex = rightBackLegColorIndex;
			this.MaterialIndex = materialIndex;
			this.MeshMultiplierIndex = meshMultiplierIndex;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00022A84 File Offset: 0x00020C84
		public static MountCreationKey FromString(string str)
		{
			if (str != null)
			{
				uint num = uint.Parse(str, NumberStyles.HexNumber);
				int bitsFromKey = MountCreationKey.GetBitsFromKey(num, 0, 2);
				int bitsFromKey2 = MountCreationKey.GetBitsFromKey(num, 2, 2);
				int bitsFromKey3 = MountCreationKey.GetBitsFromKey(num, 4, 2);
				int bitsFromKey4 = MountCreationKey.GetBitsFromKey(num, 6, 2);
				int bitsFromKey5 = MountCreationKey.GetBitsFromKey(num, 8, 2);
				int bitsFromKey6 = MountCreationKey.GetBitsFromKey(num, 10, 2);
				return new MountCreationKey((byte)bitsFromKey, (byte)bitsFromKey2, (byte)bitsFromKey3, (byte)bitsFromKey4, (byte)bitsFromKey5, (byte)bitsFromKey6);
			}
			return new MountCreationKey(0, 0, 0, 0, 0, 0);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00022AF8 File Offset: 0x00020CF8
		public override string ToString()
		{
			uint num = 0U;
			this.SetBits(ref num, (int)this._leftFrontLegColorIndex, 0);
			this.SetBits(ref num, (int)this._rightFrontLegColorIndex, 2);
			this.SetBits(ref num, (int)this._leftBackLegColorIndex, 4);
			this.SetBits(ref num, (int)this._rightBackLegColorIndex, 6);
			this.SetBits(ref num, (int)this.MaterialIndex, 8);
			this.SetBits(ref num, (int)this.MeshMultiplierIndex, 10);
			return num.ToString("X");
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00022B70 File Offset: 0x00020D70
		private static int GetBitsFromKey(uint numericKey, int startingBit, int numBits)
		{
			int num = (int)(numericKey >> startingBit);
			uint num2 = (uint)(numBits * numBits - 1);
			return num & (int)num2;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00022B8C File Offset: 0x00020D8C
		private void SetBits(ref uint numericKey, int value, int startingBit)
		{
			uint num = (uint)((uint)value << startingBit);
			numericKey |= num;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00022BA8 File Offset: 0x00020DA8
		public static string GetRandomMountKeyString(ItemObject mountItem, int randomSeed)
		{
			return MountCreationKey.GetRandomMountKey(mountItem, randomSeed).ToString();
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00022BB8 File Offset: 0x00020DB8
		public static MountCreationKey GetRandomMountKey(ItemObject mountItem, int randomSeed)
		{
			MBFastRandom mbfastRandom = new MBFastRandom((uint)randomSeed);
			if (mountItem == null)
			{
				return new MountCreationKey((byte)mbfastRandom.Next(4), (byte)mbfastRandom.Next(4), (byte)mbfastRandom.Next(4), (byte)mbfastRandom.Next(4), 0, 0);
			}
			HorseComponent horseComponent = mountItem.HorseComponent;
			if (horseComponent.HorseMaterialNames != null && horseComponent.HorseMaterialNames.Count > 0)
			{
				int num = mbfastRandom.Next(horseComponent.HorseMaterialNames.Count);
				float num2 = mbfastRandom.NextFloat();
				int num3 = 0;
				float num4 = 0f;
				HorseComponent.MaterialProperty materialProperty = horseComponent.HorseMaterialNames[num];
				for (int i = 0; i < materialProperty.MeshMultiplier.Count; i++)
				{
					num4 += materialProperty.MeshMultiplier[i].Item2;
					if (num2 <= num4)
					{
						num3 = i;
						break;
					}
				}
				return new MountCreationKey((byte)mbfastRandom.Next(4), (byte)mbfastRandom.Next(4), (byte)mbfastRandom.Next(4), (byte)mbfastRandom.Next(4), (byte)num, (byte)num3);
			}
			return new MountCreationKey((byte)mbfastRandom.Next(4), (byte)mbfastRandom.Next(4), (byte)mbfastRandom.Next(4), (byte)mbfastRandom.Next(4), 0, 0);
		}

		// Token: 0x040005E7 RID: 1511
		private const int NumLegColors = 4;
	}
}
