using System;
using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class CharacterChargeRevivePointHandler : GameRequestPacketHandler<C2SCharacterChargeRevivePointReq, S2CCharacterChargeRevivePointRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(CharacterChargeRevivePointHandler));
        
        public CharacterChargeRevivePointHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SCharacterChargeRevivePointReq> request, S2CCharacterChargeRevivePointRes response)
        {
            client.Character.StatusInfo.RevivePoint = 3;
            Server.Database.UpdateStatusInfo(client.Character);

            Server.LastRevivalPowerRechargeTime[client.Character.CharacterId] = DateTime.UtcNow;

            S2CCharacterUpdateRevivePointNtc ntc = new S2CCharacterUpdateRevivePointNtc()
            {
                CharacterId = client.Character.CharacterId,
                RevivePoint = client.Character.StatusInfo.RevivePoint
            };
            client.Party.SendToAllExcept(ntc, client);

            response.RevivePoint = client.Character.StatusInfo.RevivePoint;
        }
    }
}
