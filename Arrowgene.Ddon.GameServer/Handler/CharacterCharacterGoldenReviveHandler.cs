using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class CharacterCharacterGoldenReviveHandler : GameRequestPacketHandler<C2SCharacterCharacterGoldenReviveReq, S2CCharacterCharacterGoldenReviveRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(CharacterCharacterGoldenReviveHandler));

        public CharacterCharacterGoldenReviveHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SCharacterCharacterGoldenReviveReq> request, S2CCharacterCharacterGoldenReviveRes response)
        {
            // TODO: Proper implementation
            response.GP = 69;
        }
    }
}