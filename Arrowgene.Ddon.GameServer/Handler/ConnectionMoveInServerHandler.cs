using Arrowgene.Ddon.Database.Model;
using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Model;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class ConnectionMoveInServerHandler : GameRequestPacketHandler<C2SConnectionMoveInServerReq, S2CConnectionMoveInServerRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(ConnectionMoveInServerHandler));

        public ConnectionMoveInServerHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SConnectionMoveInServerReq> request, S2CConnectionMoveInServerRes response)
        {
            Logger.Debug(client, $"Received SessionKey:{request.Structure.SessionKey}");
            GameToken token = Database.SelectToken(request.Structure.SessionKey);
            if (token == null)
            {
                Logger.Error(client, $"SessionKey:{request.Structure.SessionKey} not found");
                throw new ResponseErrorException();
            }

            Database.DeleteTokenByAccountId(token.AccountId);

            Account account = Database.SelectAccountById(token.AccountId);
            if (account == null)
            {
                Logger.Error(client, $"AccountId:{token.AccountId} not found");
                throw new ResponseErrorException();
            }

            Character character = Database.SelectCharacter(token.CharacterId);
            if (character == null)
            {
                Logger.Error(client, $"CharacterId:{token.CharacterId} not found");
                throw new ResponseErrorException();
            }

            client.Account = account;
            client.Character = character;
            client.Character.Server = Server.AssetRepository.ServerList[0];
            client.UpdateIdentity();

            client.Character.Pawns = Database.SelectPawnsByCharacterId(token.CharacterId);
            foreach (Pawn pawn in client.Character.Pawns)
            {
                pawn.Server = client.Character.Server;
            }

            Logger.Info(client, "Moved Into GameServer");


            // NTC
            client.Send(new S2CItemExtendItemSlotNtc());
            // client.Send(GameFull.Dump_5);
            //  client.Send(GameFull.Dump_6);
        }
    }
}
