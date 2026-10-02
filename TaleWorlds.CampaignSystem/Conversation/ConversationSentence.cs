using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using TaleWorlds.CampaignSystem.Conversation.Persuasion;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Conversation
{
	// Token: 0x0200023A RID: 570
	public class ConversationSentence
	{
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060022C6 RID: 8902 RVA: 0x0009A394 File Offset: 0x00098594
		// (set) Token: 0x060022C7 RID: 8903 RVA: 0x0009A39C File Offset: 0x0009859C
		public TextObject Text { get; private set; }

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x060022C8 RID: 8904 RVA: 0x0009A3A5 File Offset: 0x000985A5
		// (set) Token: 0x060022C9 RID: 8905 RVA: 0x0009A3AD File Offset: 0x000985AD
		public int Index { get; internal set; }

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x0009A3B6 File Offset: 0x000985B6
		// (set) Token: 0x060022CB RID: 8907 RVA: 0x0009A3BE File Offset: 0x000985BE
		public string Id { get; private set; }

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x0009A3C7 File Offset: 0x000985C7
		// (set) Token: 0x060022CD RID: 8909 RVA: 0x0009A3D0 File Offset: 0x000985D0
		public bool IsPlayer
		{
			get
			{
				return this.GetFlags(ConversationSentence.DialogLineFlags.PlayerLine);
			}
			internal set
			{
				this.set_flags(value, ConversationSentence.DialogLineFlags.PlayerLine);
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x060022CE RID: 8910 RVA: 0x0009A3DA File Offset: 0x000985DA
		// (set) Token: 0x060022CF RID: 8911 RVA: 0x0009A3E3 File Offset: 0x000985E3
		public bool IsRepeatable
		{
			get
			{
				return this.GetFlags(ConversationSentence.DialogLineFlags.RepeatForObjects);
			}
			internal set
			{
				this.set_flags(value, ConversationSentence.DialogLineFlags.RepeatForObjects);
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x060022D0 RID: 8912 RVA: 0x0009A3ED File Offset: 0x000985ED
		// (set) Token: 0x060022D1 RID: 8913 RVA: 0x0009A3F6 File Offset: 0x000985F6
		public bool IsSpecial
		{
			get
			{
				return this.GetFlags(ConversationSentence.DialogLineFlags.SpecialLine);
			}
			internal set
			{
				this.set_flags(value, ConversationSentence.DialogLineFlags.SpecialLine);
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x060022D2 RID: 8914 RVA: 0x0009A400 File Offset: 0x00098600
		// (set) Token: 0x060022D3 RID: 8915 RVA: 0x0009A409 File Offset: 0x00098609
		public bool IsUsedOnce
		{
			get
			{
				return this.GetFlags(ConversationSentence.DialogLineFlags.UsedOnce);
			}
			internal set
			{
				this.set_flags(value, ConversationSentence.DialogLineFlags.UsedOnce);
			}
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x0009A413 File Offset: 0x00098613
		private bool GetFlags(ConversationSentence.DialogLineFlags flag)
		{
			return (this._flags & (uint)flag) > 0U;
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x0009A420 File Offset: 0x00098620
		private void set_flags(bool val, ConversationSentence.DialogLineFlags newFlag)
		{
			if (val)
			{
				this._flags |= (uint)newFlag;
				return;
			}
			this._flags &= (uint)(~(uint)newFlag);
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x060022D6 RID: 8918 RVA: 0x0009A443 File Offset: 0x00098643
		// (set) Token: 0x060022D7 RID: 8919 RVA: 0x0009A44B File Offset: 0x0009864B
		public int Priority { get; private set; }

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x0009A454 File Offset: 0x00098654
		// (set) Token: 0x060022D9 RID: 8921 RVA: 0x0009A45C File Offset: 0x0009865C
		public int InputToken { get; private set; }

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x060022DA RID: 8922 RVA: 0x0009A465 File Offset: 0x00098665
		// (set) Token: 0x060022DB RID: 8923 RVA: 0x0009A46D File Offset: 0x0009866D
		public int OutputToken { get; private set; }

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x060022DC RID: 8924 RVA: 0x0009A476 File Offset: 0x00098676
		// (set) Token: 0x060022DD RID: 8925 RVA: 0x0009A47E File Offset: 0x0009867E
		public object RelatedObject { get; private set; }

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x0009A490 File Offset: 0x00098690
		// (set) Token: 0x060022DE RID: 8926 RVA: 0x0009A487 File Offset: 0x00098687
		public bool IsWithVariation { get; private set; }

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x0009A498 File Offset: 0x00098698
		// (set) Token: 0x060022E1 RID: 8929 RVA: 0x0009A4A0 File Offset: 0x000986A0
		public PersuasionOptionArgs PersuationOptionArgs { get; private set; }

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x060022E2 RID: 8930 RVA: 0x0009A4A9 File Offset: 0x000986A9
		public bool HasPersuasion
		{
			get
			{
				return this._onPersuasionOption != null;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x0009A4B4 File Offset: 0x000986B4
		public string SkillName
		{
			get
			{
				if (!this.HasPersuasion)
				{
					return "";
				}
				return this.PersuationOptionArgs.SkillUsed.ToString();
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x0009A4D4 File Offset: 0x000986D4
		public string TraitName
		{
			get
			{
				if (!this.HasPersuasion)
				{
					return "";
				}
				if (this.PersuationOptionArgs.TraitUsed == null)
				{
					return "";
				}
				return this.PersuationOptionArgs.TraitUsed.ToString();
			}
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x0009A508 File Offset: 0x00098708
		internal ConversationSentence(string idString, TextObject text, string inputToken, string outputToken, ConversationSentence.OnConditionDelegate conditionDelegate, ConversationSentence.OnClickableConditionDelegate clickableConditionDelegate, ConversationSentence.OnConsequenceDelegate consequenceDelegate, uint flags = 0U, int priority = 100, int agentIndex = 0, int nextAgentIndex = 0, object relatedObject = null, bool withVariation = false, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, ConversationSentence.OnPersuasionOptionDelegate persuasionOptionDelegate = null)
		{
			this.Index = Campaign.Current.ConversationManager.CreateConversationSentenceIndex();
			this.Id = idString;
			this.Text = text;
			this.InputToken = Campaign.Current.ConversationManager.GetStateIndex(inputToken);
			this.OutputToken = Campaign.Current.ConversationManager.GetStateIndex(outputToken);
			this.OnCondition = conditionDelegate;
			this.OnClickableCondition = clickableConditionDelegate;
			this.OnConsequence = consequenceDelegate;
			this._flags = flags;
			this.Priority = priority;
			this.AgentIndex = agentIndex;
			this.NextAgentIndex = nextAgentIndex;
			this.RelatedObject = relatedObject;
			this.IsWithVariation = withVariation;
			this.IsSpeaker = speakerDelegate;
			this.IsListener = listenerDelegate;
			this._onPersuasionOption = persuasionOptionDelegate;
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x0009A5D2 File Offset: 0x000987D2
		internal ConversationSentence(int index)
		{
			this.Index = index;
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x0009A5E8 File Offset: 0x000987E8
		public ConversationSentence Variation(params object[] list)
		{
			Game.Current.GameTextManager.AddGameText(this.Id).AddVariation((string)list[0], list.Skip<object>(1).ToArray<object>());
			return this;
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x0009A619 File Offset: 0x00098819
		internal void RunConsequence(Game game)
		{
			if (this.OnConsequence != null)
			{
				this.OnConsequence();
			}
			Campaign.Current.ConversationManager.OnConsequence(this);
			if (this.HasPersuasion)
			{
				ConversationManager.PersuasionCommitProgress(this.PersuationOptionArgs);
			}
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x0009A654 File Offset: 0x00098854
		internal bool RunCondition()
		{
			bool flag = true;
			if (this.OnCondition != null)
			{
				flag = this.OnCondition();
			}
			if (flag && this.HasPersuasion)
			{
				this.PersuationOptionArgs = this._onPersuasionOption();
			}
			Campaign.Current.ConversationManager.OnCondition(this);
			return flag;
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x0009A6A4 File Offset: 0x000988A4
		internal bool RunClickableCondition()
		{
			bool flag = true;
			if (this.OnClickableCondition != null)
			{
				flag = this.OnClickableCondition(out this.HintText);
			}
			Campaign.Current.ConversationManager.OnClickableCondition(this);
			return flag;
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x0009A6E0 File Offset: 0x000988E0
		public void Deserialize(XmlNode node, Type typeOfConversationCallbacks, ConversationManager conversationManager, int defaultPriority)
		{
			if (node.Attributes == null)
			{
				throw new TWXmlLoadException("node.Attributes != null");
			}
			this.Id = node.Attributes["id"].Value;
			XmlNode xmlNode = node.Attributes["on_condition"];
			if (xmlNode != null)
			{
				string innerText = xmlNode.InnerText;
				this._methodOnCondition = typeOfConversationCallbacks.GetMethod(innerText);
				if (this._methodOnCondition == null)
				{
					throw new MBMethodNameNotFoundException(innerText);
				}
				this.OnCondition = Delegate.CreateDelegate(typeof(ConversationSentence.OnConditionDelegate), null, this._methodOnCondition) as ConversationSentence.OnConditionDelegate;
			}
			XmlNode xmlNode2 = node.Attributes["on_clickable_condition"];
			if (xmlNode2 != null)
			{
				string innerText2 = xmlNode2.InnerText;
				this._methodOnClickableCondition = typeOfConversationCallbacks.GetMethod(innerText2);
				if (this._methodOnClickableCondition == null)
				{
					throw new MBMethodNameNotFoundException(innerText2);
				}
				this.OnClickableCondition = Delegate.CreateDelegate(typeof(ConversationSentence.OnClickableConditionDelegate), null, this._methodOnClickableCondition) as ConversationSentence.OnClickableConditionDelegate;
			}
			XmlNode xmlNode3 = node.Attributes["on_consequence"];
			if (xmlNode3 != null)
			{
				string innerText3 = xmlNode3.InnerText;
				this._methodOnConsequence = typeOfConversationCallbacks.GetMethod(innerText3);
				if (this._methodOnConsequence == null)
				{
					throw new MBMethodNameNotFoundException(innerText3);
				}
				this.OnConsequence = Delegate.CreateDelegate(typeof(ConversationSentence.OnConsequenceDelegate), null, this._methodOnConsequence) as ConversationSentence.OnConsequenceDelegate;
			}
			XmlNode xmlNode4 = node.Attributes["is_player"];
			if (xmlNode4 != null)
			{
				string innerText4 = xmlNode4.InnerText;
				this.IsPlayer = Convert.ToBoolean(innerText4);
			}
			XmlNode xmlNode5 = node.Attributes["is_repeatable"];
			if (xmlNode5 != null)
			{
				string innerText5 = xmlNode5.InnerText;
				this.IsRepeatable = Convert.ToBoolean(innerText5);
			}
			XmlNode xmlNode6 = node.Attributes["is_speacial_option"];
			if (xmlNode6 != null)
			{
				string innerText6 = xmlNode6.InnerText;
				this.IsSpecial = Convert.ToBoolean(innerText6);
			}
			XmlNode xmlNode7 = node.Attributes["is_used_once"];
			if (xmlNode7 != null)
			{
				string innerText7 = xmlNode7.InnerText;
				this.IsUsedOnce = Convert.ToBoolean(innerText7);
			}
			XmlNode xmlNode8 = node.Attributes["text"];
			if (xmlNode8 != null)
			{
				this.Text = new TextObject(xmlNode8.InnerText, null);
			}
			XmlNode xmlNode9 = node.Attributes["istate"];
			if (xmlNode9 != null)
			{
				this.InputToken = conversationManager.GetStateIndex(xmlNode9.InnerText);
			}
			XmlNode xmlNode10 = node.Attributes["ostate"];
			if (xmlNode10 != null)
			{
				this.OutputToken = conversationManager.GetStateIndex(xmlNode10.InnerText);
			}
			XmlNode xmlNode11 = node.Attributes["priority"];
			this.Priority = ((xmlNode11 != null) ? int.Parse(xmlNode11.InnerText) : defaultPriority);
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x0009A992 File Offset: 0x00098B92
		public static object CurrentProcessedRepeatObject
		{
			get
			{
				return Campaign.Current.ConversationManager.GetCurrentProcessedRepeatObject();
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x060022ED RID: 8941 RVA: 0x0009A9A3 File Offset: 0x00098BA3
		public static object SelectedRepeatObject
		{
			get
			{
				return Campaign.Current.ConversationManager.GetSelectedRepeatObject();
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x0009A9B4 File Offset: 0x00098BB4
		public static TextObject SelectedRepeatLine
		{
			get
			{
				return Campaign.Current.ConversationManager.GetCurrentDialogLine();
			}
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x0009A9C5 File Offset: 0x00098BC5
		public static void SetObjectsToRepeatOver(IReadOnlyList<object> objectsToRepeatOver, int maxRepeatedDialogsInConversation = 5)
		{
			Campaign.Current.ConversationManager.SetDialogRepeatCount(objectsToRepeatOver, maxRepeatedDialogsInConversation);
		}

		// Token: 0x04000A59 RID: 2649
		public const int DefaultPriority = 100;

		// Token: 0x04000A5D RID: 2653
		public int AgentIndex;

		// Token: 0x04000A5E RID: 2654
		public int NextAgentIndex;

		// Token: 0x04000A5F RID: 2655
		public bool IsClickable = true;

		// Token: 0x04000A60 RID: 2656
		public TextObject HintText;

		// Token: 0x04000A65 RID: 2661
		private MethodInfo _methodOnCondition;

		// Token: 0x04000A66 RID: 2662
		public ConversationSentence.OnConditionDelegate OnCondition;

		// Token: 0x04000A67 RID: 2663
		private MethodInfo _methodOnClickableCondition;

		// Token: 0x04000A68 RID: 2664
		public ConversationSentence.OnClickableConditionDelegate OnClickableCondition;

		// Token: 0x04000A69 RID: 2665
		private MethodInfo _methodOnConsequence;

		// Token: 0x04000A6A RID: 2666
		public ConversationSentence.OnConsequenceDelegate OnConsequence;

		// Token: 0x04000A6B RID: 2667
		public ConversationSentence.OnMultipleConversationConsequenceDelegate IsSpeaker;

		// Token: 0x04000A6C RID: 2668
		public ConversationSentence.OnMultipleConversationConsequenceDelegate IsListener;

		// Token: 0x04000A6D RID: 2669
		private uint _flags;

		// Token: 0x04000A6F RID: 2671
		private ConversationSentence.OnPersuasionOptionDelegate _onPersuasionOption;

		// Token: 0x0200064A RID: 1610
		public enum DialogLineFlags
		{
			// Token: 0x040019C7 RID: 6599
			PlayerLine = 1,
			// Token: 0x040019C8 RID: 6600
			RepeatForObjects,
			// Token: 0x040019C9 RID: 6601
			SpecialLine = 4,
			// Token: 0x040019CA RID: 6602
			UsedOnce = 8
		}

		// Token: 0x0200064B RID: 1611
		// (Invoke) Token: 0x0600513B RID: 20795
		public delegate bool OnConditionDelegate();

		// Token: 0x0200064C RID: 1612
		// (Invoke) Token: 0x0600513F RID: 20799
		public delegate bool OnClickableConditionDelegate(out TextObject explanation);

		// Token: 0x0200064D RID: 1613
		// (Invoke) Token: 0x06005143 RID: 20803
		public delegate PersuasionOptionArgs OnPersuasionOptionDelegate();

		// Token: 0x0200064E RID: 1614
		// (Invoke) Token: 0x06005147 RID: 20807
		public delegate void OnConsequenceDelegate();

		// Token: 0x0200064F RID: 1615
		// (Invoke) Token: 0x0600514B RID: 20811
		public delegate bool OnMultipleConversationConsequenceDelegate(IAgent agent);
	}
}
