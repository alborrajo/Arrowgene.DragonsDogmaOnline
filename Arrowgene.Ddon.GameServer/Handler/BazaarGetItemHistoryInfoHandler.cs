using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class BazaarGetItemHistoryInfoHandler : GameRequestPacketHandler<C2SBazaarGetItemHistoryInfoReq, S2CBazaarGetItemHistoryInfoRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(BazaarGetItemHistoryInfoHandler));
        
        public BazaarGetItemHistoryInfoHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SBazaarGetItemHistoryInfoReq> request, S2CBazaarGetItemHistoryInfoRes response)
        {
            // TODO: Set response fields
        }
    }
}