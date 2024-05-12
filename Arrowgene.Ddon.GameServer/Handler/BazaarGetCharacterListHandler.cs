using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class BazaarGetCharacterListHandler : GameRequestPacketHandler<C2SBazaarGetCharacterListReq, S2CBazaarGetCharacterListRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(BazaarGetCharacterListHandler));
        
        public BazaarGetCharacterListHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SBazaarGetCharacterListReq> request, S2CBazaarGetCharacterListRes response)
        {
            // TODO: Set response.BazaarList
        }
    }
}