using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002E8 RID: 744
	public class CompressionInfo
	{
		// Token: 0x020005C5 RID: 1477
		[EngineStruct("Integer_compression_info", false, null)]
		public struct Integer
		{
			// Token: 0x06003E58 RID: 15960 RVA: 0x000F4EA4 File Offset: 0x000F30A4
			public Integer(int minimumValue, int maximumValue, bool maximumValueGiven)
			{
				this.maximumValue = maximumValue;
				this.minimumValue = minimumValue;
				uint num = (uint)(maximumValue - minimumValue);
				this.numberOfBits = MBMath.GetNumberOfBitsToRepresentNumber(num);
			}

			// Token: 0x06003E59 RID: 15961 RVA: 0x000F4ECF File Offset: 0x000F30CF
			public Integer(int minimumValue, int numberOfBits)
			{
				this.minimumValue = minimumValue;
				this.numberOfBits = numberOfBits;
				if (minimumValue == -2147483648 && numberOfBits == 32)
				{
					this.maximumValue = int.MaxValue;
					return;
				}
				this.maximumValue = minimumValue + (1 << numberOfBits) - 1;
			}

			// Token: 0x06003E5A RID: 15962 RVA: 0x000F4F08 File Offset: 0x000F3108
			public int GetNumBits()
			{
				return this.numberOfBits;
			}

			// Token: 0x06003E5B RID: 15963 RVA: 0x000F4F10 File Offset: 0x000F3110
			public int GetMaximumValue()
			{
				return this.maximumValue;
			}

			// Token: 0x04001F33 RID: 7987
			[CustomEngineStructMemberData("min_value")]
			private readonly int minimumValue;

			// Token: 0x04001F34 RID: 7988
			[CustomEngineStructMemberData("max_value")]
			private readonly int maximumValue;

			// Token: 0x04001F35 RID: 7989
			[CustomEngineStructMemberData("num_bits")]
			private readonly int numberOfBits;
		}

		// Token: 0x020005C6 RID: 1478
		[EngineStruct("Unsigned_integer_compression_info", false, null)]
		public struct UnsignedInteger
		{
			// Token: 0x06003E5C RID: 15964 RVA: 0x000F4F18 File Offset: 0x000F3118
			public UnsignedInteger(uint minimumValue, uint maximumValue, bool maximumValueGiven)
			{
				this.minimumValue = minimumValue;
				this.maximumValue = maximumValue;
				uint num = maximumValue - minimumValue;
				this.numberOfBits = MBMath.GetNumberOfBitsToRepresentNumber(num);
			}

			// Token: 0x06003E5D RID: 15965 RVA: 0x000F4F43 File Offset: 0x000F3143
			public UnsignedInteger(uint minimumValue, int numberOfBits)
			{
				this.minimumValue = minimumValue;
				this.numberOfBits = numberOfBits;
				if (minimumValue == 0U && numberOfBits == 32)
				{
					this.maximumValue = uint.MaxValue;
					return;
				}
				this.maximumValue = (uint)((ulong)minimumValue + (1UL << numberOfBits) - 1UL);
			}

			// Token: 0x06003E5E RID: 15966 RVA: 0x000F4F77 File Offset: 0x000F3177
			public int GetNumBits()
			{
				return this.numberOfBits;
			}

			// Token: 0x04001F36 RID: 7990
			[CustomEngineStructMemberData("min_value")]
			private readonly uint minimumValue;

			// Token: 0x04001F37 RID: 7991
			[CustomEngineStructMemberData("max_value")]
			private readonly uint maximumValue;

			// Token: 0x04001F38 RID: 7992
			[CustomEngineStructMemberData("num_bits")]
			private readonly int numberOfBits;
		}

		// Token: 0x020005C7 RID: 1479
		[EngineStruct("Integer64_compression_info", false, null)]
		public struct LongInteger
		{
			// Token: 0x06003E5F RID: 15967 RVA: 0x000F4F80 File Offset: 0x000F3180
			public LongInteger(long minimumValue, long maximumValue, bool maximumValueGiven)
			{
				this.maximumValue = maximumValue;
				this.minimumValue = minimumValue;
				ulong num = (ulong)(maximumValue - minimumValue);
				this.numberOfBits = MBMath.GetNumberOfBitsToRepresentNumber(num);
			}

			// Token: 0x06003E60 RID: 15968 RVA: 0x000F4FAC File Offset: 0x000F31AC
			public LongInteger(long minimumValue, int numberOfBits)
			{
				this.minimumValue = minimumValue;
				this.numberOfBits = numberOfBits;
				if (minimumValue == -9223372036854775808L && numberOfBits == 64)
				{
					this.maximumValue = long.MaxValue;
					return;
				}
				this.maximumValue = minimumValue + (1L << numberOfBits) - 1L;
			}

			// Token: 0x06003E61 RID: 15969 RVA: 0x000F4FFA File Offset: 0x000F31FA
			public int GetNumBits()
			{
				return this.numberOfBits;
			}

			// Token: 0x04001F39 RID: 7993
			[CustomEngineStructMemberData("min_value")]
			private readonly long minimumValue;

			// Token: 0x04001F3A RID: 7994
			[CustomEngineStructMemberData("max_value")]
			private readonly long maximumValue;

			// Token: 0x04001F3B RID: 7995
			[CustomEngineStructMemberData("num_bits")]
			private readonly int numberOfBits;
		}

		// Token: 0x020005C8 RID: 1480
		[EngineStruct("Unsigned_integer64_compression_info", false, null)]
		public struct UnsignedLongInteger
		{
			// Token: 0x06003E62 RID: 15970 RVA: 0x000F5004 File Offset: 0x000F3204
			public UnsignedLongInteger(ulong minimumValue, ulong maximumValue, bool maximumValueGiven)
			{
				this.minimumValue = minimumValue;
				this.maximumValue = maximumValue;
				ulong num = maximumValue - minimumValue;
				this.numberOfBits = MBMath.GetNumberOfBitsToRepresentNumber(num);
			}

			// Token: 0x06003E63 RID: 15971 RVA: 0x000F502F File Offset: 0x000F322F
			public UnsignedLongInteger(ulong minimumValue, int numberOfBits)
			{
				this.minimumValue = minimumValue;
				this.numberOfBits = numberOfBits;
				if (minimumValue == 0UL && numberOfBits == 64)
				{
					this.maximumValue = ulong.MaxValue;
					return;
				}
				this.maximumValue = minimumValue + (1UL << numberOfBits) - 1UL;
			}

			// Token: 0x06003E64 RID: 15972 RVA: 0x000F5062 File Offset: 0x000F3262
			public int GetNumBits()
			{
				return this.numberOfBits;
			}

			// Token: 0x04001F3C RID: 7996
			[CustomEngineStructMemberData("min_value")]
			private readonly ulong minimumValue;

			// Token: 0x04001F3D RID: 7997
			[CustomEngineStructMemberData("max_value")]
			private readonly ulong maximumValue;

			// Token: 0x04001F3E RID: 7998
			[CustomEngineStructMemberData("num_bits")]
			private readonly int numberOfBits;
		}

		// Token: 0x020005C9 RID: 1481
		[EngineStruct("Float_compression_info", false, null)]
		public struct Float
		{
			// Token: 0x17000A7F RID: 2687
			// (get) Token: 0x06003E65 RID: 15973 RVA: 0x000F506A File Offset: 0x000F326A
			public static CompressionInfo.Float FullPrecision { get; } = new CompressionInfo.Float(true);

			// Token: 0x06003E66 RID: 15974 RVA: 0x000F5074 File Offset: 0x000F3274
			public Float(float minimumValue, float maximumValue, int numberOfBits)
			{
				this.minimumValue = minimumValue;
				this.maximumValue = maximumValue;
				this.numberOfBits = numberOfBits;
				float num = maximumValue - minimumValue;
				int num2 = (1 << numberOfBits) - 1;
				this.precision = num / (float)num2;
			}

			// Token: 0x06003E67 RID: 15975 RVA: 0x000F50B0 File Offset: 0x000F32B0
			public Float(float minimumValue, int numberOfBits, float precision)
			{
				this.minimumValue = minimumValue;
				this.precision = precision;
				this.numberOfBits = numberOfBits;
				int num = (1 << numberOfBits) - 1;
				float num2 = precision * (float)num;
				this.maximumValue = num2 + minimumValue;
			}

			// Token: 0x06003E68 RID: 15976 RVA: 0x000F50E9 File Offset: 0x000F32E9
			private Float(bool isFullPrecision)
			{
				this.minimumValue = float.MinValue;
				this.maximumValue = float.MaxValue;
				this.precision = 0f;
				this.numberOfBits = 32;
			}

			// Token: 0x06003E69 RID: 15977 RVA: 0x000F5114 File Offset: 0x000F3314
			public int GetNumBits()
			{
				return this.numberOfBits;
			}

			// Token: 0x06003E6A RID: 15978 RVA: 0x000F511C File Offset: 0x000F331C
			public float GetMaximumValue()
			{
				return this.maximumValue;
			}

			// Token: 0x06003E6B RID: 15979 RVA: 0x000F5124 File Offset: 0x000F3324
			public float GetMinimumValue()
			{
				return this.minimumValue;
			}

			// Token: 0x06003E6C RID: 15980 RVA: 0x000F512C File Offset: 0x000F332C
			public float GetPrecision()
			{
				return this.precision;
			}

			// Token: 0x06003E6D RID: 15981 RVA: 0x000F5134 File Offset: 0x000F3334
			public void ClampValueAccordingToLimits(ref float x)
			{
				x = MathF.Clamp(x, this.minimumValue, this.maximumValue);
			}

			// Token: 0x04001F40 RID: 8000
			[CustomEngineStructMemberData("min_value")]
			private readonly float minimumValue;

			// Token: 0x04001F41 RID: 8001
			[CustomEngineStructMemberData("max_value")]
			private readonly float maximumValue;

			// Token: 0x04001F42 RID: 8002
			[CustomEngineStructMemberData(true)]
			private readonly float precision;

			// Token: 0x04001F43 RID: 8003
			[CustomEngineStructMemberData("num_bits")]
			private readonly int numberOfBits;
		}
	}
}
