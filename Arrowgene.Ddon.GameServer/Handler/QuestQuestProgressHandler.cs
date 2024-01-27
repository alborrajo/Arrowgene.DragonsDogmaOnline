using System.Collections.Generic;
using Arrowgene.Ddon.GameServer.Dump;
using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Server.Network;
using Arrowgene.Ddon.Shared.Entity;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Entity.Structure;
using Arrowgene.Ddon.Shared.Model;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.GameServer.Handler
{
    public class QuestQuestProgressHandler : StructurePacketHandler<GameClient, C2SQuestQuestProgressReq>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(QuestQuestProgressHandler));


        public QuestQuestProgressHandler(DdonGameServer server) : base(server)
        {
        }

        public override void Handle(GameClient client, StructurePacket<C2SQuestQuestProgressReq> packet)
        {
            // to make a world quest
            // 1. space in the quest log (S2CQuestJoinLobbyQuestInfoNtc.QuestDefine)
            // 2. add the quest in S2CQuestGetSetQuestListRes.SetQuestList with at least the following:
            //  - Param.QuestId
            //  - Param.QuestScheduleId (can be made up, just make sure its the same in there as in here)
            //  - Param.QuestProcessStateList.CheckCommandList with its initial state commands (like find enemies in X place)
            //  - Detail.IsDiscovery = true (maybe it still works as false, it just doesnt show up in the map)
            // 3. if the quest has enemies, S2CInstanceGetEnemySetListRes.QuestId must match, and EnemyTargetTypesId must be 4
            // 4. respond to C2SQuestQuestProgressReq with the next state commands
            switch(packet.Structure.QuestScheduleId)
            {
                case 20005010:
                {
                    // Knights Bitter Enemy
                    S2CQuestQuestProgressRes res = new S2CQuestQuestProgressRes();
                    res.QuestScheduleId = packet.Structure.QuestScheduleId;
                    switch (packet.Structure.ProcessNo)
                    {
                        case 0:
                            res.QuestProcessStateList = new List<CDataQuestProcessState>()
                            {
                                new CDataQuestProcessState()
                                {
                                    ProcessNo = 1,
                                    CheckCommandList = new List<CDataQuestProcessState.MtTypedArrayCDataQuestCommand>()
                                    {
                                        new CDataQuestProcessState.MtTypedArrayCDataQuestCommand()
                                        {
                                            ResultCommandList = new List<CDataQuestCommand>()
                                            {
                                                new CDataQuestCommand(2, 100, 26, 0)
                                            }
                                        }
                                    },
                                    ResultCommandList = new List<CDataQuestCommand>()
                                    {
                                        CDataQuestCommand.ResultSetAnnounce(CDataQuestCommand.AnnounceType.QUEST_ANNOUNCE_TYPE_ACCEPT, true),
                                    }
                                }
                            };
                            break;
                        case 1:
                            res.QuestProcessStateList = new List<CDataQuestProcessState>()
                            {
                                new CDataQuestProcessState()
                                {
                                    ResultCommandList = new List<CDataQuestCommand>()
                                    {
                                        CDataQuestCommand.ResultSetAnnounce(CDataQuestCommand.AnnounceType.QUEST_ANNOUNCE_TYPE_CLEAR),
                                    }
                                }
                            };
                            break;
                    }

                    client.Send(res);

                    // Sent for the rest of the party members
                    S2CQuestQuestProgressNtc ntc = new S2CQuestQuestProgressNtc();
                    ntc.ProgressCharacterId = packet.Structure.ProgressCharacterId;
                    ntc.QuestScheduleId = res.QuestScheduleId;
                    ntc.QuestProcessStateList = res.QuestProcessStateList;
                    client.Party.SendToAllExcept(ntc, client);
                    break;
                }

                case 40000035:
                {
                    // A Personal Request
                    S2CQuestQuestProgressRes res = new S2CQuestQuestProgressRes();
                    res.QuestScheduleId = packet.Structure.QuestScheduleId;
                    switch (packet.Structure.ProcessNo)
                    {
                        case 0:
                            res.QuestProcessStateList = new List<CDataQuestProcessState>()
                            {
                                new CDataQuestProcessState()
                                {
                                    ProcessNo = 1,
                                    ResultCommandList = new List<CDataQuestCommand>()
                                    {
                                        CDataQuestCommand.ResultSetAnnounce(CDataQuestCommand.AnnounceType.QUEST_ANNOUNCE_TYPE_CLEAR)
                                    }
                                }
                            };
                            break;
                    }

                    client.Send(res);

                    // Sent for the rest of the party members
                    S2CQuestQuestProgressNtc ntc = new S2CQuestQuestProgressNtc();
                    ntc.ProgressCharacterId = packet.Structure.ProgressCharacterId;
                    ntc.QuestScheduleId = res.QuestScheduleId;
                    ntc.QuestProcessStateList = res.QuestProcessStateList;
                    client.Party.SendToAllExcept(ntc, client);
                    break;
                }

                case 50300010:
                {
                    // Spirit Dragon
                    S2CQuestQuestProgressRes res = new S2CQuestQuestProgressRes();
                    res.QuestScheduleId = packet.Structure.QuestScheduleId;
                    switch (packet.Structure.ProcessNo)
                    {
                        case 0:
                            {
                                CDataStageLayoutId layoutId = new CDataStageLayoutId(381, 0, 2);
                                List<EnemySpawn> enemySpawns = ((DdonGameServer) Server).EnemyManager.GetAssets(layoutId, 0);
                                for(byte i=1; i<enemySpawns.Count; i++)
                                {
                                    S2CInstanceEnemyRepopNtc repopNtc = new S2CInstanceEnemyRepopNtc();
                                    repopNtc.LayoutId = layoutId;
                                    repopNtc.EnemyData.PositionIndex = i;
                                    repopNtc.EnemyData.EnemyInfo = enemySpawns[i].Enemy;
                                    repopNtc.WaitSecond = 0;
                                    client.Party.SendToAll(repopNtc);
                                }
                            }

                            res.QuestProcessStateList = new List<CDataQuestProcessState>()
                            {
                                new CDataQuestProcessState()
                                {
                                    ProcessNo = 1,
                                    CheckCommandList = new List<CDataQuestProcessState.MtTypedArrayCDataQuestCommand>()
                                    {
                                        new CDataQuestProcessState.MtTypedArrayCDataQuestCommand()
                                        {
                                            ResultCommandList = new List<CDataQuestCommand>()
                                            {
                                                new CDataQuestCommand(48,  436, 2, 0, 30)
                                            }
                                        }
                                    }
                                }
                            };
                            break;

                        case 1:
                            {
                                CDataStageLayoutId layoutId = new CDataStageLayoutId(381, 0, 2);
                                List<EnemySpawn> enemySpawns = ((DdonGameServer) Server).EnemyManager.GetAssets(layoutId, 0);
                                for(byte i=1; i<enemySpawns.Count; i++)
                                {
                                    S2CInstanceEnemyRepopNtc repopNtc = new S2CInstanceEnemyRepopNtc();
                                    repopNtc.LayoutId = layoutId;
                                    repopNtc.EnemyData.PositionIndex = i;
                                    repopNtc.EnemyData.EnemyInfo = enemySpawns[i].Enemy;
                                    repopNtc.WaitSecond = 0;
                                    client.Party.SendToAll(repopNtc);
                                }
                            }

                            res.QuestProcessStateList = new List<CDataQuestProcessState>()
                            {
                                new CDataQuestProcessState()
                                {
                                    ProcessNo = 2,
                                    CheckCommandList = new List<CDataQuestProcessState.MtTypedArrayCDataQuestCommand>()
                                    {
                                        new CDataQuestProcessState.MtTypedArrayCDataQuestCommand()
                                        {
                                            ResultCommandList = new List<CDataQuestCommand>()
                                            {
                                                new CDataQuestCommand(2,  436, 2, 0)
                                            }
                                        }
                                    }
                                }
                            };
                            break;
                        
                        case 2:
                            res.QuestProcessStateList = new List<CDataQuestProcessState>()
                            {
                                new CDataQuestProcessState()
                                {
                                    ProcessNo = 3,
                                    ResultCommandList = new List<CDataQuestCommand>()
                                    {
                                        new CDataQuestCommand(13,  436, 1),
                                        CDataQuestCommand.ResultSetAnnounce(CDataQuestCommand.AnnounceType.QUEST_ANNOUNCE_TYPE_CLEAR)
                                    }
                                }
                            };
                            break;
                    }

                    client.Send(res);

                    // Sent for the rest of the party members
                    S2CQuestQuestProgressNtc ntc = new S2CQuestQuestProgressNtc();
                    ntc.ProgressCharacterId = packet.Structure.ProgressCharacterId;
                    ntc.QuestScheduleId = res.QuestScheduleId;
                    ntc.QuestProcessStateList = res.QuestProcessStateList;
                    client.Party.SendToAllExcept(ntc, client);
                    break;
                }

                case 287350:
                {
                    // Hope Bitter End
                    client.Send(GameFull.Dump_652);
                    break;
                }

                default:
                {
                    EntitySerializer<S2CQuestQuestProgressRes> entitySerializer = EntitySerializer.Get<S2CQuestQuestProgressRes>();
                    S2CQuestQuestProgressRes pcap0 = entitySerializer.Read(GameFull.data_Dump_166);
                    client.Send(pcap0);
                    S2CQuestQuestProgressRes pcap1 = entitySerializer.Read(GameFull.data_Dump_168);
                    client.Send(pcap1);
                    S2CQuestQuestProgressRes pcap2 = entitySerializer.Read(GameFull.data_Dump_170);
                    client.Send(pcap2);
                    S2CQuestQuestProgressRes pcap3 = entitySerializer.Read(GameFull.data_Dump_172);
                    client.Send(pcap3);
                    S2CQuestQuestProgressRes pcap4 = entitySerializer.Read(GameFull.data_Dump_175);
                    client.Send(pcap4);
                    S2CQuestQuestProgressRes pcap5 = entitySerializer.Read(GameFull.data_Dump_177);
                    client.Send(pcap5);
                    S2CQuestQuestProgressRes pcap6 = entitySerializer.Read(GameFull.data_Dump_179);
                    client.Send(pcap6);
                    S2CQuestQuestProgressRes pcap7 = entitySerializer.Read(GameFull.data_Dump_181);
                    client.Send(pcap7);
                    S2CQuestQuestProgressRes pcap8 = entitySerializer.Read(GameFull.data_Dump_185);
                    client.Send(pcap8);
                    S2CQuestQuestProgressRes pcap9 = entitySerializer.Read(GameFull.data_Dump_188);
                    client.Send(pcap9);
                    S2CQuestQuestProgressRes pcap10 = entitySerializer.Read(GameFull.data_Dump_190);
                    client.Send(pcap10);
                    S2CQuestQuestProgressRes pcap11 = entitySerializer.Read(GameFull.data_Dump_294);
                    client.Send(pcap11);
                    S2CQuestQuestProgressRes pcap12 = entitySerializer.Read(GameFull.data_Dump_297);
                    client.Send(pcap12);
                    S2CQuestQuestProgressRes pcap13 = entitySerializer.Read(GameFull.data_Dump_299);
                    client.Send(pcap13);
                    S2CQuestQuestProgressRes pcap14 = entitySerializer.Read(GameFull.data_Dump_524);
                    client.Send(pcap14);
                    break;
                }
            }
        }
    }
}
