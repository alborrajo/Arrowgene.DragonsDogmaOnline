using System;
using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class CharacterCharacterPointReviveHandler : GameRequestPacketHandler<C2SCharacterCharacterPointReviveReq, S2CCharacterCharacterPointReviveRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(CharacterCharacterPointReviveHandler));

        public CharacterCharacterPointReviveHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SCharacterCharacterPointReviveReq> request, S2CCharacterCharacterPointReviveRes response)
        {
            client.Character.StatusInfo.RevivePoint = (byte) Math.Max(0, client.Character.StatusInfo.RevivePoint-1);
            Database.UpdateStatusInfo(client.Character);

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