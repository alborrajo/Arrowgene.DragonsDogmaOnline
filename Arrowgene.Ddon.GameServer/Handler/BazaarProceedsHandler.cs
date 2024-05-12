#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Entity.Structure;
using Arrowgene.Ddon.Shared.Model;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class BazaarProceedsHandler : GameRequestPacketHandler<C2SBazaarProceedsReq, S2CBazaarProceedsRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(BazaarProceedsHandler));
        
        public BazaarProceedsHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SBazaarProceedsReq> request, S2CBazaarProceedsRes response)
        {
            // TODO: Fetch price by the BazaarId
            ClientItemInfo boughtItemInfo = ClientItemInfo.GetInfoForItemId(Server.AssetRepository.ClientItemInfos, request.Structure.ItemId);
            int totalItemAmount = request.Structure.ItemStorageIndicateNum.Select(x => (int) x.ItemNum).Sum();
            int totalPrice = boughtItemInfo.Price * totalItemAmount;

            response.BazaarId = request.Structure.BazaarId;

            S2CItemUpdateCharacterItemNtc updateCharacterItemNtc = new S2CItemUpdateCharacterItemNtc();
            updateCharacterItemNtc.UpdateType = 0;

            // UPDATE CHARACTER WALLET
            CDataWalletPoint wallet = client.Character.WalletPointList.Where(wp => wp.Type == WalletType.Gold).Single();
            wallet.Value = (uint) Math.Max(0, (int)wallet.Value - totalPrice);
            Database.UpdateWalletPoint(client.Character.CharacterId, wallet);
            updateCharacterItemNtc.UpdateWalletList.Add(new CDataUpdateWalletPoint()
            {
                Type = WalletType.Gold,
                AddPoint = (int) -totalPrice,
                Value = wallet.Value
            });

            // UPDATE INVENTORY
            foreach (CDataItemStorageIndicateNum itemStorageIndicateNum in request.Structure.ItemStorageIndicateNum)
            {
                bool sendToItemBag;
                switch(itemStorageIndicateNum.StorageType) {
                    case 19:
                        // If request.Structure.Destination is 19: Send to corresponding item bag
                        sendToItemBag = true;
                        break;
                    case 20:
                        // If request.Structure.Destination is 20: Send to storage 
                        sendToItemBag = false;
                        break;
                    default:
                        throw new Exception("Unexpected destination when buying goods: "+itemStorageIndicateNum.StorageType);
                }

                List<CDataItemUpdateResult> itemUpdateResult = Server.ItemManager.AddItem(Server, client.Character, sendToItemBag, request.Structure.ItemId, itemStorageIndicateNum.ItemNum);                
                updateCharacterItemNtc.UpdateItemList.AddRange(itemUpdateResult);
            }

            // Send packets
            client.Send(updateCharacterItemNtc);
            // TODO: Send the NTC to the seller?
        }
    }
}