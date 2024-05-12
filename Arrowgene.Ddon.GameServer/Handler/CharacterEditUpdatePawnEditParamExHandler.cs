using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Server.Network;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Model;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class CharacterEditUpdatePawnEditParamExHandler : GameRequestPacketHandler<C2SCharacterEditUpdatePawnEditParamExReq, S2CCharacterEditUpdatePawnEditParamExRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(CharacterEditUpdatePawnEditParamExHandler));
        
        public CharacterEditUpdatePawnEditParamExHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SCharacterEditUpdatePawnEditParamExReq> request, S2CCharacterEditUpdatePawnEditParamExRes response)
        {
            // TODO: Substract GG
            Pawn pawn = client.Character.PawnBySlotNo(request.Structure.SlotNo);
            pawn.EditInfo = request.Structure.EditInfo;
            Server.Database.UpdateEditInfo(pawn);
            pawn.Name = request.Structure.Name;
            Server.Database.UpdatePawnBaseInfo(pawn);
            foreach(Client other in Server.ClientLookup.GetAll()) {
                other.Send(new S2CCharacterEditUpdateEditParamExNtc() {
                    CharacterId = pawn.CharacterId,
                    PawnId = pawn.PawnId,
                    EditInfo = pawn.EditInfo,
                    Name = pawn.Name
                });
            }
        }
    }
}