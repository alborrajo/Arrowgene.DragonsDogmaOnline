using System;
using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class CharacterGetReviveChargeableTimeHandler : GameRequestPacketHandler<C2SCharacterGetReviveChargeableTimeReq, S2CCharacterGetReviveChargeableTimeRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(CharacterGetReviveChargeableTimeHandler));
        


        public CharacterGetReviveChargeableTimeHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SCharacterGetReviveChargeableTimeReq> packet, S2CCharacterGetReviveChargeableTimeRes response)
        {
            if(Server.LastRevivalPowerRechargeTime.ContainsKey(client.Character.CharacterId))
            {
                DateTime lastRechargeTime = Server.LastRevivalPowerRechargeTime[client.Character.CharacterId];
                DateTime nextRechargeTime = lastRechargeTime.Add(DdonGameServer.RevivalPowerRechargeTimeSpan);
                TimeSpan remainTimeSpan = nextRechargeTime - DateTime.UtcNow;
                response.RemainTime = (uint) Math.Max(0, remainTimeSpan.TotalSeconds);
            }
            else
            {
                response.RemainTime = 0;
            }
        }
    }
}
