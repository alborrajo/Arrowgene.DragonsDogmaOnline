using System;
using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Entity.Structure;
using Arrowgene.Ddon.Shared.Model;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class CharacterCharacterSearchHandler : GameRequestPacketHandler<C2SCharacterCharacterSearchReq, S2CCharacterCharacterSearchRes>
    {
        private static readonly ServerLogger Logger =
            LogProvider.Logger<ServerLogger>(typeof(CharacterCharacterSearchHandler));

        public CharacterCharacterSearchHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SCharacterCharacterSearchReq> request, S2CCharacterCharacterSearchRes response)
        {
            foreach (Character character in Server.ClientLookup.GetAllCharacter())
            {
                if (!character.FirstName.Contains(
                        request.Structure.SearchParam.FirstName, StringComparison.InvariantCultureIgnoreCase)
                    &&
                    !character.LastName.Contains(
                        request.Structure.SearchParam.LastName, StringComparison.InvariantCultureIgnoreCase)
                   )
                {
                    continue;
                }

                if (character == client.Character)
                {
                    continue;
                }

                CDataCharacterListElement characterListElement = new CDataCharacterListElement();
                GameStructure.CDataCharacterListElement(characterListElement, character);
                response.CharacterList.Add(characterListElement);
            }

            Logger.Info(client, $"Found: {response.CharacterList.Count} Characters matching for query " +
                                $"FirstName:{request.Structure.SearchParam.FirstName} " +
                                $"LastName:{request.Structure.SearchParam.LastName}");
        }
    }
}
