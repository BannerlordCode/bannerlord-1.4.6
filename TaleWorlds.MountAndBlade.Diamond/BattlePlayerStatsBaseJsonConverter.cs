using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F1 RID: 241
	public class BattlePlayerStatsBaseJsonConverter : JsonConverter
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x000055CB File Offset: 0x000037CB
		public override bool CanConvert(Type objectType)
		{
			return typeof(BattlePlayerStatsBase).IsAssignableFrom(objectType);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000055E0 File Offset: 0x000037E0
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			JObject jobject = JObject.Load(reader);
			string text = (string)jobject["GameType"];
			BattlePlayerStatsBase battlePlayerStatsBase;
			if (text == "Skirmish")
			{
				battlePlayerStatsBase = new BattlePlayerStatsSkirmish();
			}
			else if (text == "Captain")
			{
				battlePlayerStatsBase = new BattlePlayerStatsCaptain();
			}
			else if (text == "Siege")
			{
				battlePlayerStatsBase = new BattlePlayerStatsSiege();
			}
			else if (text == "TeamDeathmatch")
			{
				battlePlayerStatsBase = new BattlePlayerStatsTeamDeathmatch();
			}
			else if (text == "Duel")
			{
				battlePlayerStatsBase = new BattlePlayerStatsDuel();
			}
			else
			{
				if (!(text == "Battle"))
				{
					return null;
				}
				battlePlayerStatsBase = new BattlePlayerStatsBattle();
			}
			serializer.Populate(jobject.CreateReader(), battlePlayerStatsBase);
			return battlePlayerStatsBase;
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x000056A0 File Offset: 0x000038A0
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000056A3 File Offset: 0x000038A3
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
	}
}
