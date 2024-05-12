using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Model;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class ConnectionMoveOutServerHandler : GameRequestPacketHandler<C2SConnectionMoveOutServerReq, S2CConnectionMoveOutServerRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(ConnectionMoveOutServerHandler));

        public ConnectionMoveOutServerHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SConnectionMoveOutServerReq> request, S2CConnectionMoveOutServerRes response)
        {
            Logger.Debug(client, $"Creating SessionKey");
            GameToken token = GameToken.GenerateGameToken(client.Account.Id, client.Character.CharacterId);
            if (!Database.SetToken(token))
            {
                Logger.Error(client, "Failed to store SessionKey");
                throw new ResponseErrorException();
            }

            Logger.Info(client, $"Created SessionKey:{token.Token} for CharacterId:{client.Character.CharacterId}");
            response.SessionKey = token.Token;
        }
    }
}
