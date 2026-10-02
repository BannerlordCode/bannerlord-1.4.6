using System;
using System.Collections.Generic;
using System.Text;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade.Network.Messages
{
	// Token: 0x020003BF RID: 959
	public abstract class GameNetworkMessage
	{
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x060035AB RID: 13739 RVA: 0x000DD3AA File Offset: 0x000DB5AA
		// (set) Token: 0x060035AC RID: 13740 RVA: 0x000DD3B2 File Offset: 0x000DB5B2
		public int MessageId { get; set; }

		// Token: 0x060035AD RID: 13741 RVA: 0x000DD3BC File Offset: 0x000DB5BC
		internal void Write()
		{
			DebugNetworkEventStatistics.StartEvent(base.GetType().Name, this.MessageId);
			GameNetworkMessage.WriteIntToPacket(this.MessageId, GameNetwork.IsClientOrReplay ? CompressionBasic.NetworkComponentEventTypeFromClientCompressionInfo : CompressionBasic.NetworkComponentEventTypeFromServerCompressionInfo);
			this.OnWrite();
			GameNetworkMessage.WriteIntToPacket(5, GameNetworkMessage.TestValueCompressionInfo);
			DebugNetworkEventStatistics.EndEvent();
		}

		// Token: 0x060035AE RID: 13742
		protected abstract void OnWrite();

		// Token: 0x060035AF RID: 13743 RVA: 0x000DD414 File Offset: 0x000DB614
		internal bool Read()
		{
			bool flag = this.OnRead();
			bool flag2 = true;
			if (GameNetworkMessage.ReadIntFromPacket(GameNetworkMessage.TestValueCompressionInfo, ref flag2) != 5)
			{
				throw new MBNetworkBitException(base.GetType().Name);
			}
			return flag;
		}

		// Token: 0x060035B0 RID: 13744
		protected abstract bool OnRead();

		// Token: 0x060035B1 RID: 13745 RVA: 0x000DD449 File Offset: 0x000DB649
		internal MultiplayerMessageFilter GetLogFilter()
		{
			return this.OnGetLogFilter();
		}

		// Token: 0x060035B2 RID: 13746
		protected abstract MultiplayerMessageFilter OnGetLogFilter();

		// Token: 0x060035B3 RID: 13747 RVA: 0x000DD451 File Offset: 0x000DB651
		internal string GetLogFormat()
		{
			return this.OnGetLogFormat();
		}

		// Token: 0x060035B4 RID: 13748
		protected abstract string OnGetLogFormat();

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x060035B5 RID: 13749 RVA: 0x000DD459 File Offset: 0x000DB659
		public static bool IsClientMissionOver
		{
			get
			{
				return GameNetwork.IsClient && !NetworkMain.GameClient.IsInGame && !NetworkMain.CommunityClient.IsInGame;
			}
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x000DD480 File Offset: 0x000DB680
		public static bool ReadBoolFromPacket(ref bool bufferReadValid)
		{
			CompressionInfo.Integer integer = new CompressionInfo.Integer(0, 1);
			int num = 0;
			bufferReadValid = bufferReadValid && MBAPI.IMBNetwork.ReadIntFromPacket(ref integer, out num);
			return num != 0;
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000DD4B4 File Offset: 0x000DB6B4
		public static void WriteBoolToPacket(bool value)
		{
			CompressionInfo.Integer integer = new CompressionInfo.Integer(0, 1);
			MBAPI.IMBNetwork.WriteIntToPacket(value ? 1 : 0, ref integer);
			DebugNetworkEventStatistics.AddDataToStatistic(integer.GetNumBits());
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x000DD4EC File Offset: 0x000DB6EC
		public static int ReadIntFromPacket(CompressionInfo.Integer compressionInfo, ref bool bufferReadValid)
		{
			int num = 0;
			bufferReadValid = bufferReadValid && MBAPI.IMBNetwork.ReadIntFromPacket(ref compressionInfo, out num);
			return num;
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x000DD513 File Offset: 0x000DB713
		public static void WriteIntToPacket(int value, CompressionInfo.Integer compressionInfo)
		{
			MBAPI.IMBNetwork.WriteIntToPacket(value, ref compressionInfo);
			DebugNetworkEventStatistics.AddDataToStatistic(compressionInfo.GetNumBits());
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x000DD530 File Offset: 0x000DB730
		public static uint ReadUintFromPacket(CompressionInfo.UnsignedInteger compressionInfo, ref bool bufferReadValid)
		{
			uint num = 0U;
			bufferReadValid = bufferReadValid && MBAPI.IMBNetwork.ReadUintFromPacket(ref compressionInfo, out num);
			return num;
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000DD557 File Offset: 0x000DB757
		public static void WriteUintToPacket(uint value, CompressionInfo.UnsignedInteger compressionInfo)
		{
			MBAPI.IMBNetwork.WriteUintToPacket(value, ref compressionInfo);
			DebugNetworkEventStatistics.AddDataToStatistic(compressionInfo.GetNumBits());
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000DD574 File Offset: 0x000DB774
		public static long ReadLongFromPacket(CompressionInfo.LongInteger compressionInfo, ref bool bufferReadValid)
		{
			long num = 0L;
			bufferReadValid = bufferReadValid && MBAPI.IMBNetwork.ReadLongFromPacket(ref compressionInfo, out num);
			return num;
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000DD59C File Offset: 0x000DB79C
		public static void WriteLongToPacket(long value, CompressionInfo.LongInteger compressionInfo)
		{
			MBAPI.IMBNetwork.WriteLongToPacket(value, ref compressionInfo);
			DebugNetworkEventStatistics.AddDataToStatistic(compressionInfo.GetNumBits());
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000DD5B8 File Offset: 0x000DB7B8
		public static ulong ReadUlongFromPacket(CompressionInfo.UnsignedLongInteger compressionInfo, ref bool bufferReadValid)
		{
			ulong num = 0UL;
			bufferReadValid = bufferReadValid && MBAPI.IMBNetwork.ReadUlongFromPacket(ref compressionInfo, out num);
			return num;
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000DD5E0 File Offset: 0x000DB7E0
		public static void WriteUlongToPacket(ulong value, CompressionInfo.UnsignedLongInteger compressionInfo)
		{
			MBAPI.IMBNetwork.WriteUlongToPacket(value, ref compressionInfo);
			DebugNetworkEventStatistics.AddDataToStatistic(compressionInfo.GetNumBits());
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000DD5FC File Offset: 0x000DB7FC
		public static float ReadFloatFromPacket(CompressionInfo.Float compressionInfo, ref bool bufferReadValid)
		{
			float num = 0f;
			bufferReadValid = bufferReadValid && MBAPI.IMBNetwork.ReadFloatFromPacket(ref compressionInfo, out num);
			return num;
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000DD627 File Offset: 0x000DB827
		public static void WriteFloatToPacket(float value, CompressionInfo.Float compressionInfo)
		{
			MBAPI.IMBNetwork.WriteFloatToPacket(value, ref compressionInfo);
			DebugNetworkEventStatistics.AddDataToStatistic(compressionInfo.GetNumBits());
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000DD644 File Offset: 0x000DB844
		public static string ReadStringFromPacket(ref bool bufferReadValid)
		{
			byte[] array = new byte[1024];
			int num = GameNetworkMessage.ReadByteArrayFromPacket(array, 0, 1024, ref bufferReadValid);
			return GameNetworkMessage.StringEncoding.GetString(array, 0, num);
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000DD678 File Offset: 0x000DB878
		public static void WriteStringToPacket(string value)
		{
			byte[] array = (string.IsNullOrEmpty(value) ? new byte[0] : GameNetworkMessage.StringEncoding.GetBytes(value));
			GameNetworkMessage.WriteByteArrayToPacket(array, 0, array.Length);
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000DD6AB File Offset: 0x000DB8AB
		public static int ReadByteArrayFromPacket(byte[] buffer, int offset, int bufferCapacity, ref bool bufferReadValid)
		{
			return MBAPI.IMBNetwork.ReadByteArrayFromPacket(buffer, offset, bufferCapacity, ref bufferReadValid);
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000DD6BC File Offset: 0x000DB8BC
		public static void WriteBannerCodeToPacket(string bannerCode)
		{
			List<BannerData> list;
			Banner.TryGetBannerDataFromCode(bannerCode, out list);
			GameNetworkMessage.WriteIntToPacket(list.Count, CompressionBasic.BannerDataCountCompressionInfo);
			for (int i = 0; i < list.Count; i++)
			{
				BannerData bannerData = list[i];
				GameNetworkMessage.WriteIntToPacket(bannerData.MeshId, CompressionBasic.BannerDataMeshIdCompressionInfo);
				GameNetworkMessage.WriteIntToPacket(bannerData.ColorId, CompressionBasic.BannerDataColorIndexCompressionInfo);
				GameNetworkMessage.WriteIntToPacket(bannerData.ColorId2, CompressionBasic.BannerDataColorIndexCompressionInfo);
				GameNetworkMessage.WriteIntToPacket((int)bannerData.Size.X, CompressionBasic.BannerDataSizeCompressionInfo);
				GameNetworkMessage.WriteIntToPacket((int)bannerData.Size.Y, CompressionBasic.BannerDataSizeCompressionInfo);
				GameNetworkMessage.WriteIntToPacket((int)bannerData.Position.X, CompressionBasic.BannerDataSizeCompressionInfo);
				GameNetworkMessage.WriteIntToPacket((int)bannerData.Position.Y, CompressionBasic.BannerDataSizeCompressionInfo);
				GameNetworkMessage.WriteBoolToPacket(bannerData.DrawStroke);
				GameNetworkMessage.WriteBoolToPacket(bannerData.Mirror);
				GameNetworkMessage.WriteIntToPacket((int)bannerData.Rotation, CompressionBasic.BannerDataRotationCompressionInfo);
			}
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000DD7BC File Offset: 0x000DB9BC
		public static string ReadBannerCodeFromPacket(ref bool bufferReadValid)
		{
			int num = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataCountCompressionInfo, ref bufferReadValid);
			MBList<BannerData> mblist = new MBList<BannerData>(num);
			for (int i = 0; i < num; i++)
			{
				BannerData bannerData = new BannerData(GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataMeshIdCompressionInfo, ref bufferReadValid), GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataColorIndexCompressionInfo, ref bufferReadValid), GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataColorIndexCompressionInfo, ref bufferReadValid), new Vec2((float)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataSizeCompressionInfo, ref bufferReadValid), (float)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataSizeCompressionInfo, ref bufferReadValid)), new Vec2((float)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataSizeCompressionInfo, ref bufferReadValid), (float)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataSizeCompressionInfo, ref bufferReadValid)), GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid), GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid), (float)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.BannerDataRotationCompressionInfo, ref bufferReadValid) * 0.0027777778f);
				mblist.Add(bannerData);
			}
			return Banner.GetBannerCodeFromBannerDataList(mblist);
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000DD87A File Offset: 0x000DBA7A
		public static void WriteByteArrayToPacket(byte[] value, int offset, int size)
		{
			MBAPI.IMBNetwork.WriteByteArrayToPacket(value, offset, size);
			DebugNetworkEventStatistics.AddDataToStatistic(MathF.Min(size, 1024) + 10);
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000DD89C File Offset: 0x000DBA9C
		public static MBActionSet ReadActionSetReferenceFromPacket(CompressionInfo.Integer compressionInfo, ref bool bufferReadValid)
		{
			if (bufferReadValid)
			{
				int num;
				bufferReadValid = MBAPI.IMBNetwork.ReadIntFromPacket(ref compressionInfo, out num);
				return new MBActionSet(num);
			}
			return MBActionSet.InvalidActionSet;
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000DD8C9 File Offset: 0x000DBAC9
		public static void WriteActionSetReferenceToPacket(MBActionSet actionSet, CompressionInfo.Integer compressionInfo)
		{
			MBAPI.IMBNetwork.WriteIntToPacket(actionSet.Index, ref compressionInfo);
			DebugNetworkEventStatistics.AddDataToStatistic(compressionInfo.GetNumBits());
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000DD8EC File Offset: 0x000DBAEC
		public static int ReadAgentIndexFromPacket(ref bool bufferReadValid)
		{
			CompressionInfo.Integer agentCompressionInfo = CompressionMission.AgentCompressionInfo;
			int num = -1;
			bufferReadValid = bufferReadValid && MBAPI.IMBNetwork.ReadIntFromPacket(ref agentCompressionInfo, out num);
			return num;
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000DD91C File Offset: 0x000DBB1C
		public static void WriteAgentIndexToPacket(int agentIndex)
		{
			CompressionInfo.Integer agentCompressionInfo = CompressionMission.AgentCompressionInfo;
			MBAPI.IMBNetwork.WriteIntToPacket(agentIndex, ref agentCompressionInfo);
			DebugNetworkEventStatistics.AddDataToStatistic(agentCompressionInfo.GetNumBits());
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000DD948 File Offset: 0x000DBB48
		public static MBObjectBase ReadObjectReferenceFromPacket(MBObjectManager objectManager, CompressionInfo.UnsignedInteger compressionInfo, ref bool bufferReadValid)
		{
			uint num = GameNetworkMessage.ReadUintFromPacket(compressionInfo, ref bufferReadValid);
			if (bufferReadValid && num > 0U)
			{
				MBGUID mbguid = new MBGUID(num);
				return objectManager.GetObject(mbguid);
			}
			return null;
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000DD978 File Offset: 0x000DBB78
		public static void WriteObjectReferenceToPacket(MBObjectBase value, CompressionInfo.UnsignedInteger compressionInfo)
		{
			MBAPI.IMBNetwork.WriteUintToPacket((value != null) ? value.Id.InternalValue : 0U, ref compressionInfo);
			DebugNetworkEventStatistics.AddDataToStatistic(compressionInfo.GetNumBits());
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000DD9B4 File Offset: 0x000DBBB4
		public static VirtualPlayer ReadVirtualPlayerReferenceToPacket(ref bool bufferReadValid, bool canReturnNull = false)
		{
			int num = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.PlayerCompressionInfo, ref bufferReadValid);
			bool flag = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			if ((num >= 0 && !GameNetworkMessage.IsClientMissionOver) & bufferReadValid)
			{
				VirtualPlayer virtualPlayer;
				if (!flag)
				{
					virtualPlayer = GameNetwork.VirtualPlayers[num];
				}
				else
				{
					virtualPlayer = GameNetwork.DisconnectedNetworkPeers[num].VirtualPlayer;
				}
				return virtualPlayer;
			}
			return null;
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000DDA09 File Offset: 0x000DBC09
		public static NetworkCommunicator ReadNetworkPeerReferenceFromPacket(ref bool bufferReadValid, bool canReturnNull = false)
		{
			VirtualPlayer virtualPlayer = GameNetworkMessage.ReadVirtualPlayerReferenceToPacket(ref bufferReadValid, canReturnNull);
			return ((virtualPlayer != null) ? virtualPlayer.Communicator : null) as NetworkCommunicator;
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x000DDA24 File Offset: 0x000DBC24
		public static void WriteVirtualPlayerReferenceToPacket(VirtualPlayer virtualPlayer)
		{
			bool flag = false;
			int num = ((virtualPlayer != null) ? virtualPlayer.Index : (-1));
			if (num >= 0 && GameNetwork.VirtualPlayers[num] != virtualPlayer)
			{
				for (int i = 0; i < GameNetwork.DisconnectedNetworkPeers.Count; i++)
				{
					if (GameNetwork.DisconnectedNetworkPeers[i].VirtualPlayer == virtualPlayer)
					{
						num = i;
						flag = true;
						break;
					}
				}
			}
			GameNetworkMessage.WriteIntToPacket(num, CompressionBasic.PlayerCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(flag);
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000DDA8D File Offset: 0x000DBC8D
		public static void WriteNetworkPeerReferenceToPacket(NetworkCommunicator networkCommunicator)
		{
			GameNetworkMessage.WriteVirtualPlayerReferenceToPacket((networkCommunicator != null) ? networkCommunicator.VirtualPlayer : null);
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x000DDAA0 File Offset: 0x000DBCA0
		public static int ReadTeamIndexFromPacket(ref bool bufferReadValid)
		{
			return GameNetworkMessage.ReadIntFromPacket(CompressionMission.TeamCompressionInfo, ref bufferReadValid);
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000DDAAD File Offset: 0x000DBCAD
		public static void WriteTeamIndexToPacket(int teamIndex)
		{
			GameNetworkMessage.WriteIntToPacket(teamIndex, CompressionMission.TeamCompressionInfo);
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000DDABC File Offset: 0x000DBCBC
		public static MissionObjectId ReadMissionObjectIdFromPacket(ref bool bufferReadValid)
		{
			bool flag = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			int num = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.MissionObjectIDCompressionInfo, ref bufferReadValid);
			if (!bufferReadValid || num == -1 || GameNetworkMessage.IsClientMissionOver)
			{
				if (num != -1)
				{
					MBDebug.Print(string.Concat(new object[]
					{
						"Reading null MissionObject because IsClientMissionOver: ",
						GameNetworkMessage.IsClientMissionOver.ToString(),
						" valid read: ",
						bufferReadValid.ToString(),
						" MissionObject ID: ",
						num,
						" runtime: ",
						flag.ToString()
					}), 0, Debug.DebugColor.White, 17592186044416UL);
				}
				return new MissionObjectId(-1, false);
			}
			return new MissionObjectId(num, flag);
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000DDB66 File Offset: 0x000DBD66
		public static void WriteMissionObjectIdToPacket(MissionObjectId value)
		{
			GameNetworkMessage.WriteBoolToPacket(value.CreatedAtRuntime);
			GameNetworkMessage.WriteIntToPacket(value.Id, CompressionBasic.MissionObjectIDCompressionInfo);
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x000DDB84 File Offset: 0x000DBD84
		public static Vec3 ReadVec3FromPacket(CompressionInfo.Float compressionInfo, ref bool bufferReadValid)
		{
			float num = GameNetworkMessage.ReadFloatFromPacket(compressionInfo, ref bufferReadValid);
			float num2 = GameNetworkMessage.ReadFloatFromPacket(compressionInfo, ref bufferReadValid);
			float num3 = GameNetworkMessage.ReadFloatFromPacket(compressionInfo, ref bufferReadValid);
			return new Vec3(num, num2, num3, -1f);
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000DDBB4 File Offset: 0x000DBDB4
		public static void WriteVec3ToPacket(Vec3 value, CompressionInfo.Float compressionInfo)
		{
			GameNetworkMessage.WriteFloatToPacket(value.x, compressionInfo);
			GameNetworkMessage.WriteFloatToPacket(value.y, compressionInfo);
			GameNetworkMessage.WriteFloatToPacket(value.z, compressionInfo);
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000DDBDC File Offset: 0x000DBDDC
		public static Vec2 ReadVec2FromPacket(CompressionInfo.Float compressionInfo, ref bool bufferReadValid)
		{
			float num = GameNetworkMessage.ReadFloatFromPacket(compressionInfo, ref bufferReadValid);
			float num2 = GameNetworkMessage.ReadFloatFromPacket(compressionInfo, ref bufferReadValid);
			return new Vec2(num, num2);
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000DDBFE File Offset: 0x000DBDFE
		public static void WriteVec2ToPacket(Vec2 value, CompressionInfo.Float compressionInfo)
		{
			GameNetworkMessage.WriteFloatToPacket(value.x, compressionInfo);
			GameNetworkMessage.WriteFloatToPacket(value.y, compressionInfo);
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000DDC18 File Offset: 0x000DBE18
		public static Mat3 ReadRotationMatrixFromPacket(ref bool bufferReadValid)
		{
			Vec3 vec = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.UnitVectorCompressionInfo, ref bufferReadValid);
			Vec3 vec2 = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.UnitVectorCompressionInfo, ref bufferReadValid);
			Vec3 vec3 = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.UnitVectorCompressionInfo, ref bufferReadValid);
			return new Mat3(in vec, in vec2, in vec3);
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000DDC54 File Offset: 0x000DBE54
		public static void WriteRotationMatrixToPacket(Mat3 value)
		{
			GameNetworkMessage.WriteVec3ToPacket(value.s, CompressionBasic.UnitVectorCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(value.f, CompressionBasic.UnitVectorCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(value.u, CompressionBasic.UnitVectorCompressionInfo);
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000DDC88 File Offset: 0x000DBE88
		public static MatrixFrame ReadMatrixFrameFromPacket(ref bool bufferReadValid)
		{
			Vec3 vec = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.PositionCompressionInfo, ref bufferReadValid);
			Vec3 vec2 = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.ScaleCompressionInfo, ref bufferReadValid);
			Mat3 mat = GameNetworkMessage.ReadRotationMatrixFromPacket(ref bufferReadValid);
			MatrixFrame matrixFrame = new MatrixFrame(in mat, in vec);
			matrixFrame.Scale(in vec2);
			return matrixFrame;
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000DDCC8 File Offset: 0x000DBEC8
		public static void WriteMatrixFrameToPacket(MatrixFrame frame)
		{
			Vec3 scaleVector = frame.rotation.GetScaleVector();
			MatrixFrame matrixFrame = frame;
			Vec3 vec = new Vec3(1f / scaleVector.x, 1f / scaleVector.y, 1f / scaleVector.z, -1f);
			matrixFrame.Scale(in vec);
			GameNetworkMessage.WriteVec3ToPacket(matrixFrame.origin, CompressionBasic.PositionCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(scaleVector, CompressionBasic.ScaleCompressionInfo);
			GameNetworkMessage.WriteRotationMatrixToPacket(matrixFrame.rotation);
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000DDD44 File Offset: 0x000DBF44
		public static MatrixFrame ReadNonUniformTransformFromPacket(CompressionInfo.Float positionCompressionInfo, CompressionInfo.Float quaternionCompressionInfo, ref bool bufferReadValid)
		{
			MatrixFrame matrixFrame = GameNetworkMessage.ReadUnitTransformFromPacket(positionCompressionInfo, quaternionCompressionInfo, ref bufferReadValid);
			Vec3 vec = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.ScaleCompressionInfo, ref bufferReadValid);
			matrixFrame.rotation.ApplyScaleLocal(in vec);
			return matrixFrame;
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000DDD78 File Offset: 0x000DBF78
		public static void WriteNonUniformTransformToPacket(MatrixFrame frame, CompressionInfo.Float positionCompressionInfo, CompressionInfo.Float quaternionCompressionInfo)
		{
			MatrixFrame matrixFrame = frame;
			Vec3 vec = matrixFrame.rotation.MakeUnit();
			GameNetworkMessage.WriteUnitTransformToPacket(matrixFrame, positionCompressionInfo, quaternionCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(vec, CompressionBasic.ScaleCompressionInfo);
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x000DDDA8 File Offset: 0x000DBFA8
		public static MatrixFrame ReadTransformFromPacket(CompressionInfo.Float positionCompressionInfo, CompressionInfo.Float quaternionCompressionInfo, ref bool bufferReadValid)
		{
			MatrixFrame matrixFrame = GameNetworkMessage.ReadUnitTransformFromPacket(positionCompressionInfo, quaternionCompressionInfo, ref bufferReadValid);
			if (GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid))
			{
				float num = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.ScaleCompressionInfo, ref bufferReadValid);
				matrixFrame.rotation.ApplyScaleLocal(num);
			}
			return matrixFrame;
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000DDDE0 File Offset: 0x000DBFE0
		public static void WriteTransformToPacket(MatrixFrame frame, CompressionInfo.Float positionCompressionInfo, CompressionInfo.Float quaternionCompressionInfo)
		{
			MatrixFrame matrixFrame = frame;
			Vec3 vec = matrixFrame.rotation.MakeUnit();
			GameNetworkMessage.WriteUnitTransformToPacket(matrixFrame, positionCompressionInfo, quaternionCompressionInfo);
			bool flag = !vec.x.ApproximatelyEqualsTo(1f, CompressionBasic.ScaleCompressionInfo.GetPrecision());
			GameNetworkMessage.WriteBoolToPacket(flag);
			if (flag)
			{
				GameNetworkMessage.WriteFloatToPacket(vec.x, CompressionBasic.ScaleCompressionInfo);
			}
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000DDE3C File Offset: 0x000DC03C
		public static MatrixFrame ReadUnitTransformFromPacket(CompressionInfo.Float positionCompressionInfo, CompressionInfo.Float quaternionCompressionInfo, ref bool bufferReadValid)
		{
			return new MatrixFrame
			{
				origin = GameNetworkMessage.ReadVec3FromPacket(positionCompressionInfo, ref bufferReadValid),
				rotation = GameNetworkMessage.ReadQuaternionFromPacket(quaternionCompressionInfo, ref bufferReadValid).ToMat3()
			};
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000DDE76 File Offset: 0x000DC076
		public static void WriteUnitTransformToPacket(MatrixFrame frame, CompressionInfo.Float positionCompressionInfo, CompressionInfo.Float quaternionCompressionInfo)
		{
			GameNetworkMessage.WriteVec3ToPacket(frame.origin, positionCompressionInfo);
			GameNetworkMessage.WriteQuaternionToPacket(frame.rotation.ToQuaternion(), quaternionCompressionInfo);
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000DDE98 File Offset: 0x000DC098
		public static Quaternion ReadQuaternionFromPacket(CompressionInfo.Float compressionInfo, ref bool bufferReadValid)
		{
			Quaternion quaternion = default(Quaternion);
			float num = 0f;
			int num2 = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.OmittedQuaternionComponentIndexCompressionInfo, ref bufferReadValid);
			for (int i = 0; i < 4; i++)
			{
				if (i != num2)
				{
					quaternion[i] = GameNetworkMessage.ReadFloatFromPacket(compressionInfo, ref bufferReadValid);
					num += quaternion[i] * quaternion[i];
				}
			}
			quaternion[num2] = MathF.Sqrt(1f - num);
			quaternion.SafeNormalize();
			return quaternion;
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x000DDF10 File Offset: 0x000DC110
		public static void WriteQuaternionToPacket(Quaternion q, CompressionInfo.Float compressionInfo)
		{
			int num = -1;
			float num2 = 0f;
			Quaternion quaternion = q;
			quaternion.SafeNormalize();
			for (int i = 0; i < 4; i++)
			{
				float num3 = MathF.Abs(quaternion[i]);
				if (num3 > num2)
				{
					num2 = num3;
					num = i;
				}
			}
			if (quaternion[num] < 0f)
			{
				quaternion.Flip();
			}
			GameNetworkMessage.WriteIntToPacket(num, CompressionBasic.OmittedQuaternionComponentIndexCompressionInfo);
			for (int j = 0; j < 4; j++)
			{
				if (j != num)
				{
					GameNetworkMessage.WriteFloatToPacket(quaternion[j], compressionInfo);
				}
			}
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000DDF9C File Offset: 0x000DC19C
		public static void WriteBodyPropertiesToPacket(BodyProperties bodyProperties)
		{
			GameNetworkMessage.WriteFloatToPacket(bodyProperties.Age, CompressionBasic.AgentAgeCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(bodyProperties.Weight, CompressionBasic.FaceKeyDataCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(bodyProperties.Build, CompressionBasic.FaceKeyDataCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(bodyProperties.KeyPart1, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(bodyProperties.KeyPart2, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(bodyProperties.KeyPart3, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(bodyProperties.KeyPart4, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(bodyProperties.KeyPart5, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(bodyProperties.KeyPart6, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(bodyProperties.KeyPart7, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(bodyProperties.KeyPart8, CompressionBasic.DebugULongNonCompressionInfo);
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000DE064 File Offset: 0x000DC264
		public static BodyProperties ReadBodyPropertiesFromPacket(ref bool bufferReadValid)
		{
			float num = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.AgentAgeCompressionInfo, ref bufferReadValid);
			float num2 = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.FaceKeyDataCompressionInfo, ref bufferReadValid);
			float num3 = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.FaceKeyDataCompressionInfo, ref bufferReadValid);
			ulong num4 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref bufferReadValid);
			ulong num5 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref bufferReadValid);
			ulong num6 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref bufferReadValid);
			ulong num7 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref bufferReadValid);
			ulong num8 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref bufferReadValid);
			ulong num9 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref bufferReadValid);
			ulong num10 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref bufferReadValid);
			ulong num11 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref bufferReadValid);
			if (bufferReadValid)
			{
				return new BodyProperties(new DynamicBodyProperties(num, num2, num3), new StaticBodyProperties(num4, num5, num6, num7, num8, num9, num10, num11));
			}
			return default(BodyProperties);
		}

		// Token: 0x0400170A RID: 5898
		private static readonly Encoding StringEncoding = new UTF8Encoding();

		// Token: 0x0400170B RID: 5899
		private static CompressionInfo.Integer TestValueCompressionInfo = new CompressionInfo.Integer(0, 3);

		// Token: 0x0400170C RID: 5900
		private const int ConstTestValue = 5;

		// Token: 0x02000685 RID: 1669
		// (Invoke) Token: 0x0600415C RID: 16732
		public delegate bool ClientMessageHandlerDelegate<T>(NetworkCommunicator peer, T message) where T : GameNetworkMessage;

		// Token: 0x02000686 RID: 1670
		// (Invoke) Token: 0x06004160 RID: 16736
		public delegate void ServerMessageHandlerDelegate<T>(T message) where T : GameNetworkMessage;
	}
}
