using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Server.Network;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class CharacterEditUpdateCharacterEditParamExHandler : GameRequestPacketHandler<C2SCharacterEditUpdateCharacterEditParamExReq, S2CCharacterEditUpdateCharacterEditParamExRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(CharacterEditUpdateCharacterEditParamExHandler));
        
        public CharacterEditUpdateCharacterEditParamExHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SCharacterEditUpdateCharacterEditParamExReq> request, S2CCharacterEditUpdateCharacterEditParamExRes response)
        {
            // TODO: Substract GG
            client.Character.EditInfo = request.Structure.EditInfo;
            Server.Database.UpdateEditInfo(client.Character);
            
            if(request.Structure.FirstName.Length > 0) {
                client.Character.FirstName = request.Structure.FirstName;
                Server.Database.UpdateCharacterBaseInfo(client.Character);
            }

            foreach(Client other in Server.ClientLookup.GetAll()) {
                other.Send(new S2CCharacterEditUpdateEditParamExNtc() {
                    CharacterId = client.Character.CharacterId,
                    PawnId = 0,
                    EditInfo = client.Character.EditInfo,
                    Name = client.Character.FirstName
                });
            }
        }
    }
}