using System.Linq;
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
            // Only Knight's Bitter Enemy
            pcap.SetQuestList = pcap.SetQuestList
                .Where(setQuest => setQuest.Param.QuestId == 20005010)
                .ToList(); 
            client.Send(pcap);
        }
    }
}
