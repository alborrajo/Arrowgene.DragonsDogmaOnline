using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class BazaarGetExhibitPossibleNumHandler : GameRequestPacketHandler<C2SBazaarGetExhibitPossibleNumReq, S2CBazaarGetExhibitPossibleNumRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(BazaarGetExhibitPossibleNumHandler));
        
        public BazaarGetExhibitPossibleNumHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SBazaarGetExhibitPossibleNumReq> request, S2CBazaarGetExhibitPossibleNumRes response)
        {
            // TODO: set response.Num and response.Add
        }
    }
}