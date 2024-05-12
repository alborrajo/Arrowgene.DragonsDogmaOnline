using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class ActionSetPlayerActionHistoryHandler : GameRequestPacketHandler<C2SActionSetPlayerActionHistoryReq, S2CActionSetPlayerActionHistoryRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(ActionSetPlayerActionHistoryHandler));

        public ActionSetPlayerActionHistoryHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SActionSetPlayerActionHistoryReq> request, S2CActionSetPlayerActionHistoryRes response)
        {
            // TODO: Implement
        }
    }
}