using System.Collections.Generic;
using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Server.Network;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Entity.Structure;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class QuestGetSetQuestListHandler : StructurePacketHandler<GameClient, C2SQuestGetSetQuestListReq>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(QuestGetQuestPartyBonusListHandler));


        public QuestGetSetQuestListHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SQuestGetSetQuestListReq> packet)
        {
            //S2CQuestGetSetQuestListRes pcap = EntitySerializer.Get<S2CQuestGetSetQuestListRes>().Read(GameFull.data_Dump_132);
            //var knightsBitterEnemy = pcap.SetQuestList
            //    .Where(setQuest => setQuest.Param.QuestId == 20005010)
            //    .Single();

            S2CQuestGetSetQuestListRes res = new S2CQuestGetSetQuestListRes()
            {
                SetQuestList = new List<CDataSetQuestList>()
                {
                    new CDataSetQuestList()
                    {
                        Param = new CDataQuestList()
                        {
                            QuestScheduleId = 20005010,
                            QuestId = 20005010,
                            QuestProcessStateList = new List<CDataQuestProcessState>()
                            {
                                new CDataQuestProcessState()
                                {
                                    CheckCommandList = new List<CDataQuestProcessState.MtTypedArrayCDataQuestCommand>()
                                    {
                                        new CDataQuestProcessState.MtTypedArrayCDataQuestCommand()
                                        {
                                            ResultCommandList = new List<CDataQuestCommand>()
                                            {
                                                new CDataQuestCommand(68,  100, 26, -1)
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        Detail = new CDataSetQuestDetail()
                        {
                            IsDiscovery = true
                        }
                    }
                }
            };

            client.Send(res);
        }
    }
}
