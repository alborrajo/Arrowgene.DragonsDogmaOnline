using Arrowgene.Buffers;
using Arrowgene.Ddon.GameServer.Dump;
using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Server.Network;
using Arrowgene.Ddon.Shared.Entity;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class QuestGetSetQuestListHandler : StructurePacketHandler<GameClient, C2SQuestGetSetQuestListReq>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(QuestGetQuestPartyBonusListHandler));


        public QuestGetSetQuestListHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SQuestGetSetQuestListReq> packet)
        {
            S2CQuestGetSetQuestListRes pcap = EntitySerializer.Get<S2CQuestGetSetQuestListRes>().Read(GameFull.data_Dump_132);
            client.Send(pcap);
            //client.Send(GameFull.Dump_132);
            //client.Send(new Packet(PacketId.S2C_QUEST_GET_SET_QUEST_LIST_RES, new byte[]{0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x16 ,0x00 ,0x00 ,0x00 ,0x00 ,0x0A ,0x1A ,0x28 ,0x9A ,0x40 ,0x01 ,0x00}));
        }
    }
}
