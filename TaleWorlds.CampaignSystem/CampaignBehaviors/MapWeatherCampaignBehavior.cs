using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000418 RID: 1048
	public class MapWeatherCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x0014476E File Offset: 0x0014296E
		public WeatherNode[] AllWeatherNodes
		{
			get
			{
				return this._weatherNodes;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06004317 RID: 17175 RVA: 0x00144776 File Offset: 0x00142976
		private int DimensionSquared
		{
			get
			{
				return Campaign.Current.DefaultWeatherNodeDimension * Campaign.Current.DefaultWeatherNodeDimension;
			}
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x0014478D File Offset: 0x0014298D
		public override void RegisterEvents()
		{
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunchedEvent));
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x001447A8 File Offset: 0x001429A8
		private void OnSessionLaunchedEvent(CampaignGameStarter obj)
		{
			this.InitializeTheBehavior();
			for (int i = 0; i < this.DimensionSquared; i++)
			{
				this.UpdateWeatherNodeWithIndex(i);
			}
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x001447D3 File Offset: 0x001429D3
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<int>("_lastUpdatedNodeIndex", ref this._lastUpdatedNodeIndex);
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x001447E8 File Offset: 0x001429E8
		private void CreateAndShuffleDataIndicesDeterministic()
		{
			this._weatherNodeDataShuffledIndices = new int[this.DimensionSquared];
			for (int i = 0; i < this.DimensionSquared; i++)
			{
				this._weatherNodeDataShuffledIndices[i] = i;
			}
			MBFastRandom mbfastRandom = new MBFastRandom((uint)Campaign.Current.UniqueGameId.GetDeterministicHashCode());
			for (int j = 0; j < 20; j++)
			{
				for (int k = 0; k < this.DimensionSquared; k++)
				{
					int num = mbfastRandom.Next(this.DimensionSquared);
					int num2 = this._weatherNodeDataShuffledIndices[k];
					this._weatherNodeDataShuffledIndices[k] = this._weatherNodeDataShuffledIndices[num];
					this._weatherNodeDataShuffledIndices[num] = num2;
				}
			}
		}

		// Token: 0x0600431C RID: 17180 RVA: 0x00144888 File Offset: 0x00142A88
		private void InitializeTheBehavior()
		{
			this.CreateAndShuffleDataIndicesDeterministic();
			this._weatherNodes = new WeatherNode[this.DimensionSquared];
			Vec2 terrainSize = Campaign.Current.MapSceneWrapper.GetTerrainSize();
			int defaultWeatherNodeDimension = Campaign.Current.DefaultWeatherNodeDimension;
			int num = defaultWeatherNodeDimension;
			int num2 = defaultWeatherNodeDimension;
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float num3 = (float)i / (float)defaultWeatherNodeDimension * terrainSize.X;
					float num4 = (float)j / (float)defaultWeatherNodeDimension * terrainSize.Y;
					Vec2 vec = new Vec2(num3, num4);
					CampaignVec2 campaignVec = new CampaignVec2(vec, true);
					if (!campaignVec.IsValid())
					{
						campaignVec = new CampaignVec2(vec, false);
					}
					this._weatherNodes[i * defaultWeatherNodeDimension + j] = new WeatherNode(campaignVec);
				}
			}
			this.AddEventHandler();
		}

		// Token: 0x0600431D RID: 17181 RVA: 0x00144950 File Offset: 0x00142B50
		private void AddEventHandler()
		{
			long num = Campaign.Current.Models.MapWeatherModel.WeatherUpdateFrequency.NumTicks - CampaignTime.Now.NumTicks % Campaign.Current.Models.MapWeatherModel.WeatherUpdateFrequency.NumTicks;
			this._weatherTickEvent = CampaignPeriodicEventManager.CreatePeriodicEvent(Campaign.Current.Models.MapWeatherModel.WeatherUpdateFrequency, new CampaignTime(num));
			this._weatherTickEvent.AddHandler(new MBCampaignEvent.CampaignEventDelegate(this.WeatherUpdateTick));
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x001449E1 File Offset: 0x00142BE1
		private void WeatherUpdateTick(MBCampaignEvent campaignEvent, params object[] delegateParams)
		{
			this.UpdateWeatherNodeWithIndex(this._weatherNodeDataShuffledIndices[this._lastUpdatedNodeIndex]);
			this._lastUpdatedNodeIndex++;
			if (this._lastUpdatedNodeIndex == this._weatherNodes.Length)
			{
				this._lastUpdatedNodeIndex = 0;
			}
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x00144A1C File Offset: 0x00142C1C
		private void UpdateWeatherNodeWithIndex(int index)
		{
			WeatherNode weatherNode = this._weatherNodes[index];
			MapWeatherModel.WeatherEvent currentWeatherEvent = weatherNode.CurrentWeatherEvent;
			MapWeatherModel.WeatherEvent weatherEvent = Campaign.Current.Models.MapWeatherModel.UpdateWeatherForPosition(weatherNode.Position, CampaignTime.Now);
			MapWeatherModel.WeatherEventEffectOnTerrain weatherEffectOnTerrainForPosition = Campaign.Current.Models.MapWeatherModel.GetWeatherEffectOnTerrainForPosition(weatherNode.Position.ToVec2());
			if (currentWeatherEvent != weatherEvent || weatherEffectOnTerrainForPosition == MapWeatherModel.WeatherEventEffectOnTerrain.Wet)
			{
				weatherNode.SetVisualDirty();
				return;
			}
			if (currentWeatherEvent == MapWeatherModel.WeatherEvent.Clear && MBRandom.NondeterministicRandomFloat < 0.1f)
			{
				weatherNode.SetVisualDirty();
			}
		}

		// Token: 0x04001332 RID: 4914
		private WeatherNode[] _weatherNodes;

		// Token: 0x04001333 RID: 4915
		private MBCampaignEvent _weatherTickEvent;

		// Token: 0x04001334 RID: 4916
		private int[] _weatherNodeDataShuffledIndices;

		// Token: 0x04001335 RID: 4917
		private int _lastUpdatedNodeIndex;
	}
}
