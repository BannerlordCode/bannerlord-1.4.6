using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TaleWorlds.Diamond;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000146 RID: 326
	public class PlayerStatsBaseJsonConverter : JsonConverter
	{
		// Token: 0x06000902 RID: 2306 RVA: 0x0000D306 File Offset: 0x0000B506
		public override bool CanConvert(Type objectType)
		{
			return typeof(AccessObject).IsAssignableFrom(objectType);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0000D318 File Offset: 0x0000B518
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject jobject = JObject.Load(reader);
			string text = (string)jobject["gameType"];
			if (text == null)
			{
				text = (string)jobject["GameType"];
			}
			PlayerStatsBase playerStatsBase;
			if (text == "Skirmish")
			{
				playerStatsBase = new PlayerStatsSkirmish();
			}
			else if (text == "Captain")
			{
				playerStatsBase = new PlayerStatsCaptain();
			}
			else if (text == "TeamDeathmatch")
			{
				playerStatsBase = new PlayerStatsTeamDeathmatch();
			}
			else if (text == "Siege")
			{
				playerStatsBase = new PlayerStatsSiege();
			}
			else if (text == "Duel")
			{
				playerStatsBase = new PlayerStatsDuel();
			}
			else
			{
				if (!(text == "Battle"))
				{
					return null;
				}
				playerStatsBase = new PlayerStatsBattle();
			}
			serializer.Populate(jobject.CreateReader(), playerStatsBase);
			return playerStatsBase;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0000D3E0 File Offset: 0x0000B5E0
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0000D3E3 File Offset: 0x0000B5E3
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
	}
}
