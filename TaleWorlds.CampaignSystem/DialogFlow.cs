using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000084 RID: 132
	public class DialogFlow
	{
		// Token: 0x060010E3 RID: 4323 RVA: 0x00050E64 File Offset: 0x0004F064
		private DialogFlow(string startingToken, int priority = 100)
		{
			this._currentToken = startingToken;
			this.Priority = priority;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00050E88 File Offset: 0x0004F088
		private DialogFlow Line(TextObject text, bool byPlayer, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, bool isRepeatable = false, string inputToken = null, string outputToken = null)
		{
			string text2 = outputToken ?? Campaign.Current.ConversationManager.CreateToken();
			this.AddLine(text, inputToken ?? this._currentToken, text2, byPlayer, speakerDelegate, listenerDelegate, isRepeatable, false, false);
			this._currentToken = text2;
			return this;
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00050ED0 File Offset: 0x0004F0D0
		public DialogFlow Variation(string text, params object[] propertiesAndWeights)
		{
			return this.Variation(new TextObject(text, null), propertiesAndWeights);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00050EE0 File Offset: 0x0004F0E0
		public DialogFlow Variation(TextObject text, params object[] propertiesAndWeights)
		{
			for (int i = 0; i < propertiesAndWeights.Length; i += 2)
			{
				string text2 = (string)propertiesAndWeights[i];
				int num = Convert.ToInt32(propertiesAndWeights[i + 1]);
				List<GameTextManager.ChoiceTag> list = new List<GameTextManager.ChoiceTag>();
				list.Add(new GameTextManager.ChoiceTag(text2, num));
				this.Lines[this.Lines.Count - 1].AddVariation(text, list);
			}
			return this;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00050F42 File Offset: 0x0004F142
		public DialogFlow NpcLine(string npcText, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, string inputToken = null, string outputToken = null)
		{
			return this.NpcLine(new TextObject(npcText, null), speakerDelegate, listenerDelegate, inputToken, outputToken);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00050F57 File Offset: 0x0004F157
		public DialogFlow NpcLine(TextObject npcText, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, string inputToken = null, string outputToken = null)
		{
			return this.Line(npcText, false, speakerDelegate, listenerDelegate, false, inputToken, outputToken);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00050F68 File Offset: 0x0004F168
		public DialogFlow NpcLineWithVariation(string npcText, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, string inputToken = null, string outputToken = null)
		{
			DialogFlow dialogFlow = this.Line(TextObject.GetEmpty(), false, speakerDelegate, listenerDelegate, false, inputToken, outputToken);
			List<GameTextManager.ChoiceTag> list = new List<GameTextManager.ChoiceTag>();
			list.Add(new GameTextManager.ChoiceTag("DefaultTag", 1));
			this.Lines[this.Lines.Count - 1].AddVariation(new TextObject(npcText, null), list);
			return dialogFlow;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00050FC4 File Offset: 0x0004F1C4
		public DialogFlow NpcLineWithVariation(TextObject npcText, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, string inputToken = null, string outputToken = null)
		{
			DialogFlow dialogFlow = this.Line(TextObject.GetEmpty(), false, speakerDelegate, listenerDelegate, false, inputToken, outputToken);
			List<GameTextManager.ChoiceTag> list = new List<GameTextManager.ChoiceTag>();
			list.Add(new GameTextManager.ChoiceTag("DefaultTag", 1));
			this.Lines[this.Lines.Count - 1].AddVariation(npcText, list);
			return dialogFlow;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0005101A File Offset: 0x0004F21A
		public DialogFlow PlayerLine(string playerText, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, string inputToken = null, string outputToken = null)
		{
			return this.Line(new TextObject(playerText, null), true, null, listenerDelegate, false, inputToken, outputToken);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00051030 File Offset: 0x0004F230
		public DialogFlow PlayerLine(TextObject playerText, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, string inputToken = null, string outputToken = null)
		{
			return this.Line(playerText, true, null, listenerDelegate, false, inputToken, outputToken);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00051040 File Offset: 0x0004F240
		private DialogFlow BeginOptions(bool byPlayer, string inputToken = null, bool optionUsedOnce = false)
		{
			this._curDialogFlowContext = new DialogFlowContext(inputToken ?? this._currentToken, byPlayer, this._curDialogFlowContext, optionUsedOnce);
			return this;
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00051061 File Offset: 0x0004F261
		public DialogFlow BeginPlayerOptions(string inputToken = null, bool optionUsedOnce = false)
		{
			return this.BeginOptions(true, inputToken, optionUsedOnce);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0005106C File Offset: 0x0004F26C
		public DialogFlow BeginNpcOptions(string inputToken = null, bool optionUsedOnce = false)
		{
			return this.BeginOptions(false, inputToken, optionUsedOnce);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00051078 File Offset: 0x0004F278
		private DialogFlow Option(TextObject text, bool byPlayer, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, bool isRepeatable = false, bool isSpecialOption = false, string inputToken = null, string outputToken = null)
		{
			string text2 = outputToken ?? Campaign.Current.ConversationManager.CreateToken();
			this.AddLine(text, inputToken ?? this._curDialogFlowContext.Token, text2, byPlayer, speakerDelegate, listenerDelegate, isRepeatable, isSpecialOption, this._curDialogFlowContext.OptionsUsedOnlyOnce);
			this._currentToken = text2;
			return this;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x000510D0 File Offset: 0x0004F2D0
		public DialogFlow PlayerOption(string text, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, string inputToken = null, string outputToken = null)
		{
			return this.PlayerOption(new TextObject(text, null), listenerDelegate, inputToken, outputToken);
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x000510E4 File Offset: 0x0004F2E4
		public DialogFlow PlayerOption(TextObject text, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, string inputToken = null, string outputToken = null)
		{
			this.Option(text, true, null, listenerDelegate, false, false, inputToken, outputToken);
			return this;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00051104 File Offset: 0x0004F304
		public DialogFlow PlayerSpecialOption(TextObject text, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, string inputToken = null, string outputToken = null)
		{
			this.Option(text, true, null, listenerDelegate, false, true, inputToken, outputToken);
			return this;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00051124 File Offset: 0x0004F324
		public DialogFlow PlayerRepeatableOption(TextObject text, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, string inputToken = null, string outputToken = null)
		{
			this.Option(text, true, null, listenerDelegate, true, false, inputToken, outputToken);
			return this;
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00051144 File Offset: 0x0004F344
		public DialogFlow NpcOption(string text, ConversationSentence.OnConditionDelegate conditionDelegate, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, string inputToken = null, string outputToken = null)
		{
			this.Option(new TextObject(text, null), false, speakerDelegate, listenerDelegate, false, false, inputToken, outputToken);
			this._lastLine.ConditionDelegate = conditionDelegate;
			return this;
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00051178 File Offset: 0x0004F378
		public DialogFlow NpcOption(TextObject text, ConversationSentence.OnConditionDelegate conditionDelegate, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, string inputToken = null, string outputToken = null)
		{
			this.Option(text, false, speakerDelegate, listenerDelegate, false, false, inputToken, outputToken);
			this._lastLine.ConditionDelegate = conditionDelegate;
			return this;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x000511A4 File Offset: 0x0004F3A4
		public DialogFlow NpcOptionWithVariation(string text, ConversationSentence.OnConditionDelegate conditionDelegate, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, string inputToken = null, string outputToken = null)
		{
			this.NpcOptionWithVariation(new TextObject(text, null), conditionDelegate, speakerDelegate, listenerDelegate, inputToken, outputToken);
			return this;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x000511C0 File Offset: 0x0004F3C0
		public DialogFlow NpcOptionWithVariation(TextObject text, ConversationSentence.OnConditionDelegate conditionDelegate, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, string inputToken = null, string outputToken = null)
		{
			this.Option(TextObject.GetEmpty(), false, speakerDelegate, listenerDelegate, false, false, inputToken, outputToken);
			List<GameTextManager.ChoiceTag> list = new List<GameTextManager.ChoiceTag>();
			list.Add(new GameTextManager.ChoiceTag("DefaultTag", 1));
			this._lastLine.AddVariation(text, list);
			this._lastLine.ConditionDelegate = conditionDelegate;
			return this;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00051214 File Offset: 0x0004F414
		private DialogFlow EndOptions(bool byPlayer)
		{
			this._curDialogFlowContext = this._curDialogFlowContext.Parent;
			return this;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00051228 File Offset: 0x0004F428
		public DialogFlow EndPlayerOptions()
		{
			return this.EndOptions(true);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00051231 File Offset: 0x0004F431
		public DialogFlow EndNpcOptions()
		{
			return this.EndOptions(false);
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0005123A File Offset: 0x0004F43A
		public DialogFlow Condition(ConversationSentence.OnConditionDelegate conditionDelegate)
		{
			this._lastLine.ConditionDelegate = conditionDelegate;
			return this;
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00051249 File Offset: 0x0004F449
		public DialogFlow ClickableCondition(ConversationSentence.OnClickableConditionDelegate clickableConditionDelegate)
		{
			this._lastLine.ClickableConditionDelegate = clickableConditionDelegate;
			return this;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00051258 File Offset: 0x0004F458
		public DialogFlow Consequence(ConversationSentence.OnConsequenceDelegate consequenceDelegate)
		{
			this._lastLine.ConsequenceDelegate = consequenceDelegate;
			return this;
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00051267 File Offset: 0x0004F467
		public static DialogFlow CreateDialogFlow(string inputToken = null, int priority = 100)
		{
			return new DialogFlow(inputToken ?? Campaign.Current.ConversationManager.CreateToken(), priority);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00051284 File Offset: 0x0004F484
		private DialogFlowLine AddLine(TextObject text, string inputToken, string outputToken, bool byPlayer, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate, bool isRepeatable, bool isSpecialOption = false, bool usedOncePerConversation = false)
		{
			DialogFlowLine dialogFlowLine = new DialogFlowLine();
			dialogFlowLine.Text = text;
			dialogFlowLine.InputToken = inputToken;
			dialogFlowLine.OutputToken = outputToken;
			dialogFlowLine.ByPlayer = byPlayer;
			dialogFlowLine.SpeakerDelegate = speakerDelegate;
			dialogFlowLine.ListenerDelegate = listenerDelegate;
			dialogFlowLine.IsRepeatable = isRepeatable;
			dialogFlowLine.IsSpecialOption = isSpecialOption;
			dialogFlowLine.IsUsedOnce = usedOncePerConversation;
			this.Lines.Add(dialogFlowLine);
			this._lastLine = dialogFlowLine;
			return dialogFlowLine;
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x000512F0 File Offset: 0x0004F4F0
		public DialogFlow NpcDefaultOption(string text)
		{
			return this.NpcOption(text, null, null, null, null, null);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x000512FE File Offset: 0x0004F4FE
		public DialogFlow GenerateToken(out string token)
		{
			token = Campaign.Current.ConversationManager.CreateToken();
			return this;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00051312 File Offset: 0x0004F512
		public DialogFlow GotoDialogState(string input)
		{
			this._lastLine.OutputToken = input;
			this._currentToken = input;
			return this;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00051328 File Offset: 0x0004F528
		public DialogFlow GotoDialogStateBranched(string input, ConversationSentence.OnConditionDelegate conditionDelegate, string alternative)
		{
			string text = ((conditionDelegate != null && conditionDelegate()) ? input : alternative);
			this._lastLine.OutputToken = text;
			this._currentToken = text;
			return this;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00051359 File Offset: 0x0004F559
		public DialogFlow GetOutputToken(out string oState)
		{
			oState = this._lastLine.OutputToken;
			return this;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00051369 File Offset: 0x0004F569
		public DialogFlow GoBackToDialogState(string iState)
		{
			this._currentToken = iState;
			return this;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00051373 File Offset: 0x0004F573
		public DialogFlow CloseDialog()
		{
			this.GotoDialogState("close_window");
			return this;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00051382 File Offset: 0x0004F582
		private ConversationSentence AddDialogLine(ConversationSentence dialogLine)
		{
			Campaign.Current.ConversationManager.AddDialogLine(dialogLine);
			return dialogLine;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00051398 File Offset: 0x0004F598
		public ConversationSentence AddPlayerLine(string id, string inputToken, string outputToken, string text, ConversationSentence.OnConditionDelegate conditionDelegate, ConversationSentence.OnConsequenceDelegate consequenceDelegate, object relatedObject, int priority = 100, ConversationSentence.OnClickableConditionDelegate clickableConditionDelegate = null, ConversationSentence.OnPersuasionOptionDelegate persuasionOptionDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			return this.AddDialogLine(new ConversationSentence(id, new TextObject(text, null), inputToken, outputToken, conditionDelegate, clickableConditionDelegate, consequenceDelegate, 1U, priority, 0, 0, relatedObject, false, speakerDelegate, listenerDelegate, persuasionOptionDelegate));
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x000513D4 File Offset: 0x0004F5D4
		public ConversationSentence AddDialogLine(string id, string inputToken, string outputToken, string text, ConversationSentence.OnConditionDelegate conditionDelegate, ConversationSentence.OnConsequenceDelegate consequenceDelegate, object relatedObject, int priority = 100, ConversationSentence.OnClickableConditionDelegate clickableConditionDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			return this.AddDialogLine(new ConversationSentence(id, new TextObject(text, null), inputToken, outputToken, conditionDelegate, clickableConditionDelegate, consequenceDelegate, 0U, priority, 0, 0, relatedObject, false, speakerDelegate, listenerDelegate, null));
		}

		// Token: 0x0400054E RID: 1358
		internal readonly List<DialogFlowLine> Lines = new List<DialogFlowLine>();

		// Token: 0x0400054F RID: 1359
		internal readonly int Priority;

		// Token: 0x04000550 RID: 1360
		private string _currentToken;

		// Token: 0x04000551 RID: 1361
		private DialogFlowLine _lastLine;

		// Token: 0x04000552 RID: 1362
		private DialogFlowContext _curDialogFlowContext;
	}
}
