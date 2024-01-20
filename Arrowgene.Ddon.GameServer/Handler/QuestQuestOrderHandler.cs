using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Entity.Structure;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class QuestQuestOrderHandler : GameStructurePacketHandler<C2SQuestQuestOrderReq>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(QuestQuestOrderHandler));
        
        public QuestQuestOrderHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SQuestQuestOrderReq> packet)
        {
            var res = new S2CQuestQuestOrderRes();

            switch(packet.Structure.QuestScheduleId)
            {
                case 40000035:
                    // A Personal Request
                    res.QuestProcessStateList.Add(new CDataQuestProcessState()
                    {
                        CheckCommandList = new List<CDataQuestProcessState.MtTypedArrayCDataQuestCommand>()
                        {
                            new CDataQuestProcessState.MtTypedArrayCDataQuestCommand()
                            {
                                ResultCommandList = new List<CDataQuestCommand>()
                                {
                                    new CDataQuestCommand(5,  13805, 1, -1, -1)
                                }
                            }
                        },
                        ResultCommandList = new List<CDataQuestCommand>()
                        {
                            CDataQuestCommand.ResultSetAnnounce(CDataQuestCommand.AnnounceType.QUEST_ANNOUNCE_TYPE_ACCEPT)
                        }
                    });
                    break;

                case 50300010:
                    // Spirit Dragon EM as a Light Quest, yes
                    res.QuestProcessStateList.Add(new CDataQuestProcessState()
                    {
                        CheckCommandList = new List<CDataQuestProcessState.MtTypedArrayCDataQuestCommand>()
                        {
                            new CDataQuestProcessState.MtTypedArrayCDataQuestCommand()
                            {
                                ResultCommandList = new List<CDataQuestCommand>()
                                {
                                    new CDataQuestCommand(48,  436, 2, 0, 60)
                                }
                            }
                        },
                        ResultCommandList = new List<CDataQuestCommand>()
                        {
                            CDataQuestCommand.ResultSetAnnounce(CDataQuestCommand.AnnounceType.QUEST_ANNOUNCE_TYPE_ACCEPT),
                            new CDataQuestCommand(12, 436, 0), // resultStageJump
                            new CDataQuestCommand(85, 436, 0) // resultExeEventAfterStageJump
                        }
                    });
                    break;
            }

            client.Send(res);
        }
    }
}