using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Entity.Structure;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class QuestQuestOrderHandler : GameStructurePacketHandler<C2SQuestQuestOrderReq>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(QuestQuestOrderHandler));
        
        public QuestQuestOrderHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SQuestQuestOrderReq> packet)
        {
            client.Send(new S2CQuestQuestOrderRes());
        }
    }
}