using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TaleWorlds.Library;

namespace TaleWorlds.Core
{
	// Token: 0x0200005A RID: 90
	public static class Extensions
	{
		// Token: 0x06000702 RID: 1794 RVA: 0x0001853C File Offset: 0x0001673C
		public static string ToHexadecimalString(this uint number)
		{
			return string.Format("{0:X}", number);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00018550 File Offset: 0x00016750
		public static string Description(this Enum value)
		{
			object[] customAttributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (customAttributes.Length != 0)
			{
				return ((DescriptionAttribute)customAttributes[0]).Description;
			}
			return value.ToString();
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00018597 File Offset: 0x00016797
		public static float NextFloat(this Random random)
		{
			return (float)random.NextDouble();
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000185A0 File Offset: 0x000167A0
		public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
		{
			TKey tkey;
			return source.MaxBy<TSource, TKey>(selector, Comparer<TKey>.Default, out tkey);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000185BB File Offset: 0x000167BB
		public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, out TKey maxKey)
		{
			return source.MaxBy<TSource, TKey>(selector, Comparer<TKey>.Default, out maxKey);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000185CC File Offset: 0x000167CC
		public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer, out TKey maxKey)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			TSource tsource3;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw new InvalidOperationException("Sequence contains no elements");
				}
				TSource tsource = enumerator.Current;
				maxKey = selector(tsource);
				while (enumerator.MoveNext())
				{
					TSource tsource2 = enumerator.Current;
					TKey tkey = selector(tsource2);
					if (comparer.Compare(tkey, maxKey) > 0)
					{
						tsource = tsource2;
						maxKey = tkey;
					}
				}
				tsource3 = tsource;
			}
			return tsource3;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00018684 File Offset: 0x00016884
		public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
		{
			return source.MinBy<TSource, TKey>(selector, Comparer<TKey>.Default);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00018694 File Offset: 0x00016894
		public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			TSource tsource3;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw new InvalidOperationException("Sequence was empty");
				}
				TSource tsource = enumerator.Current;
				TKey tkey = selector(tsource);
				while (enumerator.MoveNext())
				{
					TSource tsource2 = enumerator.Current;
					TKey tkey2 = selector(tsource2);
					if (comparer.Compare(tkey2, tkey) < 0)
					{
						tsource = tsource2;
						tkey = tkey2;
					}
				}
				tsource3 = tsource;
			}
			return tsource3;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00018740 File Offset: 0x00016940
		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.DistinctBy<TSource, TKey>(keySelector, null);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001874A File Offset: 0x0001694A
		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return Extensions.DistinctByImpl<TSource, TKey>(source, keySelector, comparer);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00018770 File Offset: 0x00016970
		private static IEnumerable<TSource> DistinctByImpl<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			return from g in source.GroupBy<TSource, TKey>(keySelector, comparer)
				select g.First<TSource>();
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001879E File Offset: 0x0001699E
		public static string Add(this string str, string appendant, bool newLine = true)
		{
			if (str == null)
			{
				str = "";
			}
			str += appendant;
			if (newLine)
			{
				str += "\n";
			}
			return str;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x000187C4 File Offset: 0x000169C4
		public static IEnumerable<string> Split(this string str, int maxChunkSize)
		{
			for (int i = 0; i < str.Length; i += maxChunkSize)
			{
				yield return str.Substring(i, MathF.Min(maxChunkSize, str.Length - i));
			}
			yield break;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x000187DB File Offset: 0x000169DB
		public static BattleSideEnum GetOppositeSide(this BattleSideEnum side)
		{
			if (side == BattleSideEnum.Attacker)
			{
				return BattleSideEnum.Defender;
			}
			if (side != BattleSideEnum.Defender)
			{
				return side;
			}
			return BattleSideEnum.Attacker;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000187EC File Offset: 0x000169EC
		public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int splitItemCount)
		{
			if (splitItemCount <= 0)
			{
				throw new ArgumentException();
			}
			int i = 0;
			return source.GroupBy<T, int>(delegate(T x)
			{
				int j = i;
				i = j + 1;
				return j % splitItemCount;
			});
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00018830 File Offset: 0x00016A30
		public static bool IsEmpty<T>(this IEnumerable<T> source)
		{
			ICollection<T> collection = source as ICollection<T>;
			if (collection != null)
			{
				return collection.Count == 0;
			}
			ICollection collection2 = source as ICollection;
			if (collection2 != null)
			{
				return collection2.Count == 0;
			}
			return !source.Any<T>();
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00018870 File Offset: 0x00016A70
		public static void Shuffle<T>(this IList<T> list)
		{
			int i = list.Count;
			while (i > 1)
			{
				i--;
				int num = MBRandom.RandomInt(i + 1);
				T t = list[num];
				list[num] = list[i];
				list[i] = t;
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000188B8 File Offset: 0x00016AB8
		public static T GetRandomElement<T>(this IReadOnlyList<T> e)
		{
			if (e.Count == 0)
			{
				return default(T);
			}
			return e[MBRandom.RandomInt(e.Count)];
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000188E8 File Offset: 0x00016AE8
		public static T GetRandomElement<T>(this MBReadOnlyList<T> e)
		{
			if (e.Count == 0)
			{
				return default(T);
			}
			return e[MBRandom.RandomInt(e.Count)];
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00018918 File Offset: 0x00016B18
		public static T GetRandomElement<T>(this MBList<T> e)
		{
			if (e.Count == 0)
			{
				return default(T);
			}
			return e[MBRandom.RandomInt(e.Count)];
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00018948 File Offset: 0x00016B48
		public static T GetRandomElement<T>(this T[] e)
		{
			if (e.Length == 0)
			{
				return default(T);
			}
			return e[MBRandom.RandomInt(e.Length)];
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00018974 File Offset: 0x00016B74
		public static T GetRandomElementInefficiently<T>(this IEnumerable<T> e)
		{
			if (e.IsEmpty<T>())
			{
				return default(T);
			}
			return e.ElementAt<T>(MBRandom.RandomInt(e.Count<T>()));
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x000189A4 File Offset: 0x00016BA4
		public static T GetRandomElementWithPredicate<T>(this T[] e, Func<T, bool> predicate)
		{
			if (e.Length == 0)
			{
				return default(T);
			}
			int num = 0;
			for (int i = 0; i < e.Length; i++)
			{
				if (predicate(e[i]))
				{
					num++;
				}
			}
			if (num == 0)
			{
				return default(T);
			}
			int num2 = MBRandom.RandomInt(num);
			for (int j = 0; j < e.Length; j++)
			{
				if (predicate(e[j]))
				{
					num2--;
					if (num2 < 0)
					{
						return e[j];
					}
				}
			}
			Debug.FailedAssert("false", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.Core\\Extensions.cs", "GetRandomElementWithPredicate", 442);
			return default(T);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00018A4C File Offset: 0x00016C4C
		public static T GetRandomElementWithPredicate<T>(this MBReadOnlyList<T> e, Func<T, bool> predicate)
		{
			if (e.Count == 0)
			{
				return default(T);
			}
			int num = 0;
			for (int i = 0; i < e.Count; i++)
			{
				if (predicate(e[i]))
				{
					num++;
				}
			}
			if (num == 0)
			{
				return default(T);
			}
			int num2 = MBRandom.RandomInt(num);
			for (int j = 0; j < e.Count; j++)
			{
				if (predicate(e[j]))
				{
					num2--;
					if (num2 < 0)
					{
						return e[j];
					}
				}
			}
			Debug.FailedAssert("false", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.Core\\Extensions.cs", "GetRandomElementWithPredicate", 485);
			return default(T);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00018AFD File Offset: 0x00016CFD
		public static T GetRandomElementWithPredicate<T>(this MBList<T> e, Func<T, bool> predicate)
		{
			return e.GetRandomElementWithPredicate<T>(predicate);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00018B08 File Offset: 0x00016D08
		public static T GetRandomElementWithPredicate<T>(this IReadOnlyList<T> e, Func<T, bool> predicate)
		{
			if (e.Count == 0)
			{
				return default(T);
			}
			int num = 0;
			for (int i = 0; i < e.Count; i++)
			{
				if (predicate(e[i]))
				{
					num++;
				}
			}
			if (num == 0)
			{
				return default(T);
			}
			int num2 = MBRandom.RandomInt(num);
			for (int j = 0; j < e.Count; j++)
			{
				if (predicate(e[j]))
				{
					num2--;
					if (num2 < 0)
					{
						return e[j];
					}
				}
			}
			Debug.FailedAssert("false", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.Core\\Extensions.cs", "GetRandomElementWithPredicate", 533);
			return default(T);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00018BBC File Offset: 0x00016DBC
		public static List<Tuple<T1, T2>> CombineWith<T1, T2>(this IEnumerable<T1> list1, IEnumerable<T2> list2)
		{
			List<Tuple<T1, T2>> list3 = new List<Tuple<T1, T2>>();
			foreach (T1 t in list1)
			{
				foreach (T2 t2 in list2)
				{
					list3.Add(new Tuple<T1, T2>(t, t2));
				}
			}
			return list3;
		}
	}
}
