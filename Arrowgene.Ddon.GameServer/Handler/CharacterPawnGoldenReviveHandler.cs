using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class CharacterPawnGoldenReviveHandler : GameRequestPacketHandler<C2SCharacterPawnGoldenReviveReq, S2CCharacterPawnGoldenReviveRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(CharacterPawnGoldenReviveHandler));

        public CharacterPawnGoldenReviveHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SCharacterPawnGoldenReviveReq> request, S2CCharacterPawnGoldenReviveRes response)
        {
            // TODO: Proper implementation
            response.GoldenGemstonePoint = 120;
        }
    }
}
