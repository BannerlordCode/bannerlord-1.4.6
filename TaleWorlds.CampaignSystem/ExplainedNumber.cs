using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000086 RID: 134
	public struct ExplainedNumber
	{
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00052148 File Offset: 0x00050348
		public float ResultNumber
		{
			get
			{
				return MathF.Clamp(this._unclampedResultNumber, this.LimitMinValue, this.LimitMaxValue);
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x00052161 File Offset: 0x00050361
		public int RoundedResultNumber
		{
			get
			{
				return MathF.Round(this.ResultNumber);
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x0005216E File Offset: 0x0005036E
		// (set) Token: 0x06001115 RID: 4373 RVA: 0x00052176 File Offset: 0x00050376
		public float BaseNumber { get; private set; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0005217F File Offset: 0x0005037F
		public bool IncludeDescriptions
		{
			get
			{
				return this._explainer != null;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x0005218A File Offset: 0x0005038A
		public float LimitMinValue
		{
			get
			{
				if (this._limitMinValue == null)
				{
					return float.MinValue;
				}
				return this._limitMinValue.Value;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x000521AB File Offset: 0x000503AB
		public float LimitMaxValue
		{
			get
			{
				if (this._limitMaxValue == null)
				{
					return float.MaxValue;
				}
				return this._limitMaxValue.Value;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x000521CC File Offset: 0x000503CC
		// (set) Token: 0x0600111A RID: 4378 RVA: 0x000521D4 File Offset: 0x000503D4
		public float SumOfFactors { get; private set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x000521DD File Offset: 0x000503DD
		private float _unclampedResultNumber
		{
			get
			{
				return this.BaseNumber + this.BaseNumber * this.SumOfFactors;
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x000521F4 File Offset: 0x000503F4
		public ExplainedNumber(float baseNumber = 0f, bool includeDescriptions = false, TextObject baseText = null)
		{
			this.BaseNumber = baseNumber;
			this._explainer = (includeDescriptions ? new ExplainedNumber.StatExplainer() : null);
			this.SumOfFactors = 0f;
			this._limitMinValue = new float?(float.MinValue);
			this._limitMaxValue = new float?(float.MaxValue);
			if (this._explainer != null && !this.BaseNumber.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				this._explainer.AddLine((baseText ?? ExplainedNumber.BaseText).ToString(), this.BaseNumber, ExplainedNumber.StatExplainer.OperationType.Base);
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00052284 File Offset: 0x00050484
		public string GetExplanations()
		{
			if (this._explainer == null)
			{
				return "";
			}
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			mbstringBuilder.Initialize(16, "GetExplanations");
			foreach (ValueTuple<string, float> valueTuple in this._explainer.GetLines(this.BaseNumber, this._unclampedResultNumber, null, null, null))
			{
				string text = string.Format("{0} : {1}{2:0.##}\n", valueTuple.Item1, (valueTuple.Item2 > 0.001f) ? "+" : "", valueTuple.Item2);
				mbstringBuilder.Append<string>(text);
			}
			return mbstringBuilder.ToStringAndRelease();
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00052350 File Offset: 0x00050550
		[return: TupleElementNames(new string[] { "name", "number" })]
		public List<ValueTuple<string, float>> GetLines()
		{
			if (this._explainer == null)
			{
				return new List<ValueTuple<string, float>>();
			}
			return this._explainer.GetLines(this.BaseNumber, this._unclampedResultNumber, null, null, null);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0005237C File Offset: 0x0005057C
		public void AddFromExplainedNumber(ExplainedNumber explainedNumber, TextObject baseText)
		{
			if (explainedNumber._explainer != null && this._explainer != null)
			{
				TextObject textObject = new TextObject("{=HKoLNyIm}{BASE} Maximum", null);
				TextObject textObject2 = new TextObject("{=0Fliz2vk}{BASE} Minimum", null);
				textObject.SetTextVariable("BASE", baseText);
				textObject2.SetTextVariable("BASE", baseText);
				foreach (ValueTuple<string, float> valueTuple in explainedNumber._explainer.GetLines(explainedNumber.BaseNumber, explainedNumber._unclampedResultNumber, baseText, textObject, textObject2))
				{
					this._explainer.AddLine(valueTuple.Item1, valueTuple.Item2, ExplainedNumber.StatExplainer.OperationType.Add);
				}
			}
			this.BaseNumber += explainedNumber.ResultNumber;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00052454 File Offset: 0x00050654
		public void SubtractFromExplainedNumber(ExplainedNumber explainedNumber, TextObject baseText)
		{
			if (explainedNumber._explainer != null && this._explainer != null)
			{
				TextObject textObject = new TextObject("{=HKoLNyIm}{BASE} Maximum", null);
				TextObject textObject2 = new TextObject("{=0Fliz2vk}{BASE} Minimum", null);
				textObject.SetTextVariable("BASE", baseText);
				textObject2.SetTextVariable("BASE", baseText);
				foreach (ValueTuple<string, float> valueTuple in explainedNumber._explainer.GetLines(explainedNumber.BaseNumber, explainedNumber._unclampedResultNumber, baseText, textObject, textObject2))
				{
					this._explainer.AddLine(valueTuple.Item1, -valueTuple.Item2, ExplainedNumber.StatExplainer.OperationType.Add);
				}
			}
			this.BaseNumber -= explainedNumber.ResultNumber;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0005252C File Offset: 0x0005072C
		public void Add(float value, TextObject description = null, TextObject variable = null)
		{
			if (value.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				return;
			}
			this.BaseNumber += value;
			if (this._explainer != null && description != null && !value.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				if (variable != null)
				{
					description.SetTextVariable("A0", variable);
				}
				this._explainer.AddLine(description.ToString(), value, ExplainedNumber.StatExplainer.OperationType.Add);
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x000525A8 File Offset: 0x000507A8
		public void AddFactor(float value, TextObject description = null)
		{
			if (value.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				return;
			}
			this.SumOfFactors += value;
			if (description != null && this._explainer != null && !value.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				this._explainer.AddLine(description.ToString(), MathF.Round(value, 3) * 100f, ExplainedNumber.StatExplainer.OperationType.Multiply);
			}
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00052618 File Offset: 0x00050818
		public void LimitMin(float minValue)
		{
			this._limitMinValue = new float?(minValue);
			if (this._explainer != null)
			{
				this._explainer.AddLine(ExplainedNumber.LimitMinText.ToString(), minValue, ExplainedNumber.StatExplainer.OperationType.LimitMin);
			}
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00052645 File Offset: 0x00050845
		public void LimitMax(float maxValue, TextObject description = null)
		{
			this._limitMaxValue = new float?(maxValue);
			if (this._explainer != null)
			{
				this._explainer.AddLine((description ?? ExplainedNumber.LimitMaxText).ToString(), maxValue, ExplainedNumber.StatExplainer.OperationType.LimitMax);
			}
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00052677 File Offset: 0x00050877
		public void Clamp(float minValue, float maxValue)
		{
			this.LimitMin(minValue);
			this.LimitMax(maxValue, null);
		}

		// Token: 0x04000553 RID: 1363
		private static readonly TextObject LimitMinText = new TextObject("{=GNalaRaN}Minimum", null);

		// Token: 0x04000554 RID: 1364
		private static readonly TextObject LimitMaxText = new TextObject("{=cfjTtxWv}Maximum", null);

		// Token: 0x04000555 RID: 1365
		private static readonly TextObject BaseText = new TextObject("{=basevalue}Base", null);

		// Token: 0x04000557 RID: 1367
		private float? _limitMinValue;

		// Token: 0x04000558 RID: 1368
		private float? _limitMaxValue;

		// Token: 0x04000559 RID: 1369
		private ExplainedNumber.StatExplainer _explainer;

		// Token: 0x02000541 RID: 1345
		private class StatExplainer
		{
			// Token: 0x17000EF9 RID: 3833
			// (get) Token: 0x06004CFC RID: 19708 RVA: 0x0017F6F6 File Offset: 0x0017D8F6
			// (set) Token: 0x06004CFD RID: 19709 RVA: 0x0017F6FE File Offset: 0x0017D8FE
			public List<ExplainedNumber.StatExplainer.ExplanationLine> Lines { get; private set; } = new List<ExplainedNumber.StatExplainer.ExplanationLine>();

			// Token: 0x17000EFA RID: 3834
			// (get) Token: 0x06004CFE RID: 19710 RVA: 0x0017F707 File Offset: 0x0017D907
			// (set) Token: 0x06004CFF RID: 19711 RVA: 0x0017F70F File Offset: 0x0017D90F
			public ExplainedNumber.StatExplainer.ExplanationLine? BaseLine { get; private set; }

			// Token: 0x17000EFB RID: 3835
			// (get) Token: 0x06004D00 RID: 19712 RVA: 0x0017F718 File Offset: 0x0017D918
			// (set) Token: 0x06004D01 RID: 19713 RVA: 0x0017F720 File Offset: 0x0017D920
			public ExplainedNumber.StatExplainer.ExplanationLine? LimitMinLine { get; private set; }

			// Token: 0x17000EFC RID: 3836
			// (get) Token: 0x06004D02 RID: 19714 RVA: 0x0017F729 File Offset: 0x0017D929
			// (set) Token: 0x06004D03 RID: 19715 RVA: 0x0017F731 File Offset: 0x0017D931
			public ExplainedNumber.StatExplainer.ExplanationLine? LimitMaxLine { get; private set; }

			// Token: 0x06004D04 RID: 19716 RVA: 0x0017F73C File Offset: 0x0017D93C
			[return: TupleElementNames(new string[] { "name", "number" })]
			public List<ValueTuple<string, float>> GetLines(float baseNumber, float unclampedResultNumber, TextObject overrideBaseLineText = null, TextObject overrideMaximumLineText = null, TextObject overrideMinimumLineText = null)
			{
				List<ValueTuple<string, float>> list = new List<ValueTuple<string, float>>();
				if (this.BaseLine != null)
				{
					list.Add(new ValueTuple<string, float>((overrideBaseLineText != null) ? overrideBaseLineText.ToString() : this.BaseLine.Value.Name, this.BaseLine.Value.Number));
				}
				foreach (ExplainedNumber.StatExplainer.ExplanationLine explanationLine in this.Lines)
				{
					float num = explanationLine.Number;
					if (explanationLine.OperationType == ExplainedNumber.StatExplainer.OperationType.Multiply)
					{
						num = baseNumber * num * 0.01f;
					}
					list.Add(new ValueTuple<string, float>(explanationLine.Name, num));
				}
				if (this.LimitMinLine != null && this.LimitMinLine.Value.Number > unclampedResultNumber)
				{
					list.Add(new ValueTuple<string, float>((overrideMinimumLineText != null) ? overrideMinimumLineText.ToString() : this.LimitMinLine.Value.Name, this.LimitMinLine.Value.Number - unclampedResultNumber));
				}
				if (this.LimitMaxLine != null && this.LimitMaxLine.Value.Number < unclampedResultNumber)
				{
					list.Add(new ValueTuple<string, float>((overrideMaximumLineText != null) ? overrideMaximumLineText.ToString() : this.LimitMaxLine.Value.Name, this.LimitMaxLine.Value.Number - unclampedResultNumber));
				}
				return list;
			}

			// Token: 0x06004D05 RID: 19717 RVA: 0x0017F8EC File Offset: 0x0017DAEC
			public void AddLine(string name, float number, ExplainedNumber.StatExplainer.OperationType opType)
			{
				ExplainedNumber.StatExplainer.ExplanationLine explanationLine = new ExplainedNumber.StatExplainer.ExplanationLine(name, number, opType);
				if (opType == ExplainedNumber.StatExplainer.OperationType.Add || opType == ExplainedNumber.StatExplainer.OperationType.Multiply)
				{
					int num = -1;
					for (int i = 0; i < this.Lines.Count; i++)
					{
						if (this.Lines[i].Name.Equals(name) && this.Lines[i].OperationType == opType)
						{
							num = i;
							break;
						}
					}
					if (num < 0)
					{
						this.Lines.Add(explanationLine);
						return;
					}
					explanationLine = new ExplainedNumber.StatExplainer.ExplanationLine(name, number + this.Lines[num].Number, opType);
					this.Lines[num] = explanationLine;
					return;
				}
				else
				{
					if (opType == ExplainedNumber.StatExplainer.OperationType.Base)
					{
						this.BaseLine = new ExplainedNumber.StatExplainer.ExplanationLine?(explanationLine);
						return;
					}
					if (opType == ExplainedNumber.StatExplainer.OperationType.LimitMin)
					{
						this.LimitMinLine = new ExplainedNumber.StatExplainer.ExplanationLine?(explanationLine);
						return;
					}
					if (opType == ExplainedNumber.StatExplainer.OperationType.LimitMax)
					{
						this.LimitMaxLine = new ExplainedNumber.StatExplainer.ExplanationLine?(explanationLine);
					}
					return;
				}
			}

			// Token: 0x020008B0 RID: 2224
			public enum OperationType
			{
				// Token: 0x04002502 RID: 9474
				Base,
				// Token: 0x04002503 RID: 9475
				Add,
				// Token: 0x04002504 RID: 9476
				Multiply,
				// Token: 0x04002505 RID: 9477
				LimitMin,
				// Token: 0x04002506 RID: 9478
				LimitMax
			}

			// Token: 0x020008B1 RID: 2225
			public readonly struct ExplanationLine
			{
				// Token: 0x060068E9 RID: 26857 RVA: 0x001CAD6A File Offset: 0x001C8F6A
				public ExplanationLine(string name, float number, ExplainedNumber.StatExplainer.OperationType operationType)
				{
					this.Name = name;
					this.Number = number;
					this.OperationType = operationType;
				}

				// Token: 0x04002507 RID: 9479
				public readonly float Number;

				// Token: 0x04002508 RID: 9480
				public readonly string Name;

				// Token: 0x04002509 RID: 9481
				public readonly ExplainedNumber.StatExplainer.OperationType OperationType;
			}
		}
	}
}
