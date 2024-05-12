using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Server.Network;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Model;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class CharacterEditUpdatePawnEditParamHandler : GameRequestPacketHandler<C2SCharacterEditUpdatePawnEditParamReq, S2CCharacterEditUpdatePawnEditParamRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(CharacterEditUpdatePawnEditParamHandler));

        public CharacterEditUpdatePawnEditParamHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SCharacterEditUpdatePawnEditParamReq> request, S2CCharacterEditUpdatePawnEditParamRes response)
        {
            // TODO: Substract GG/Tickets
            Pawn pawn = client.Character.PawnBySlotNo(request.Structure.SlotNo);
            pawn.EditInfo = request.Structure.EditInfo;
            Server.Database.UpdateEditInfo(pawn);
            foreach(Client other in Server.ClientLookup.GetAll()) {
                other.Send(new S2CCharacterEditUpdateEditParamNtc() {
                    CharacterId = pawn.CharacterId,
                    PawnId = pawn.PawnId,
                    EditInfo = pawn.EditInfo
                });
            }
        }
    }
}