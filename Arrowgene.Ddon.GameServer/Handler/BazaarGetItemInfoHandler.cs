using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Entity.Structure;
using Arrowgene.Ddon.Shared.Model;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class BazaarGetItemInfoHandler : GameRequestPacketHandler<C2SBazaarGetItemInfoReq, S2CBazaarGetItemInfoRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(BazaarGetItemInfoHandler));
        
        public BazaarGetItemInfoHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SBazaarGetItemInfoReq> request, S2CBazaarGetItemInfoRes response)
        {
            ClientItemInfo queriedItem = ClientItemInfo.GetInfoForItemId(Server.AssetRepository.ClientItemInfos, request.Structure.ItemId);
            for (ushort i = 1; i <= 10; i++)
            {
                response.BazaarItemList.Add(new CDataBazaarItemInfo()
                {
                    BazaarId = 0,
                    Sequence = 0,
                    ItemBaseInfo = new CDataBazaarItemBaseInfo() {
                        ItemId = request.Structure.ItemId,
                        Num = i,
                        Price = queriedItem.Price,
                    },
                    ExhibitionTime = 0
                });
            }
        }
    }
}