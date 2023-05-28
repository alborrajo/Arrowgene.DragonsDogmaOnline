using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.Ddon.Shared.Entity.Structure;
using Arrowgene.Ddon.Shared.Network;

namespace Arrowgene.Ddon.Shared.Entity.PacketStructure
{
    public class S2CQuestQuestProgressRes : ServerResponse
    {
        public override PacketId Id => PacketId.S2C_QUEST_QUEST_PROGRESS_RES;

        public S2CQuestQuestProgressRes()
        {
            QuestProcessStateList = new List<CDataQuestProcessState>();
        }

        public byte QuestProgresResult { get; set; }
        public uint QuestScheduleId { get; set; }
        public List<CDataQuestProcessState> QuestProcessStateList { get; set; }

        public class Serializer : PacketEntitySerializer<S2CQuestQuestProgressRes>
        {
            public override void Write(IBuffer buffer, S2CQuestQuestProgressRes obj)
            {
                WriteServerResponse(buffer, obj);
                WriteByte(buffer, obj.QuestProgresResult);
                WriteUInt32(buffer, obj.QuestScheduleId);
                WriteEntityList<CDataQuestProcessState>(buffer, obj.QuestProcessStateList);
            }

            public override S2CQuestQuestProgressRes Read(IBuffer buffer)
            {
                S2CQuestQuestProgressRes obj = new S2CQuestQuestProgressRes();
                ReadServerResponse(buffer, obj);
                obj.QuestProgresResult = ReadByte(buffer);
                obj.QuestScheduleId = ReadUInt32(buffer);
                obj.QuestProcessStateList = ReadEntityList<CDataQuestProcessState>(buffer);
                return obj;
            }
        }
    }
}