using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Server.Network;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class CharacterEditUpdateCharacterEditParamHandler : GameRequestPacketHandler<C2SCharacterEditUpdateCharacterEditParamReq, S2CCharacterEditUpdateCharacterEditParamRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(CharacterEditUpdateCharacterEditParamHandler));

        public CharacterEditUpdateCharacterEditParamHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SCharacterEditUpdateCharacterEditParamReq> request, S2CCharacterEditUpdateCharacterEditParamRes response)
        {
            // TODO: Substract GG/Tickets
            client.Character.EditInfo = request.Structure.EditInfo;
            Server.Database.UpdateEditInfo(client.Character);
            foreach(Client other in Server.ClientLookup.GetAll()) {
                other.Send(new S2CCharacterEditUpdateEditParamNtc() {
                    CharacterId = client.Character.CharacterId,
                    PawnId = 0,
                    EditInfo = client.Character.EditInfo
                });
            }
        }
    }
}