using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class ContextMasterThrowHandler : GameRequestPacketHandler<C2SContextMasterThrowReq, S2CContextMasterThrowRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(ContextMasterThrowHandler));
        
        public ContextMasterThrowHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SContextMasterThrowReq> request, S2CContextMasterThrowRes response)
        {
            client.Party.SendToAll(new S2CContextMasterThrowNtc()
            {
                Info = request.Structure.Info
            });
        }
    }
}