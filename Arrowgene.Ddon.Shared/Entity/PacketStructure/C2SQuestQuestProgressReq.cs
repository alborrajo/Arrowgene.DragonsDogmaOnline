using Arrowgene.Buffers;
using Arrowgene.Ddon.Shared.Network;

namespace Arrowgene.Ddon.Shared.Entity.PacketStructure
{
    public class C2SQuestQuestProgressReq : IPacketStructure
    {
        public PacketId Id => PacketId.C2S_QUEST_QUEST_PROGRESS_REQ;

        public uint KeyId { get; set; }
        public uint ProgressCharacterId { get; set; }
        public uint QuestScheduleId {get; set; }
        public ushort ProcessNo { get; set; }

        public class Serializer : PacketEntitySerializer<C2SQuestQuestProgressReq>
        {
            public override void Write(IBuffer buffer, C2SQuestQuestProgressReq obj)
            {
                WriteUInt32(buffer, obj.KeyId);
                WriteUInt32(buffer, obj.ProgressCharacterId);
                WriteUInt32(buffer, obj.QuestScheduleId);
                WriteUInt16(buffer, obj.ProcessNo);
            }

            public override C2SQuestQuestProgressReq Read(IBuffer buffer)
            {
                C2SQuestQuestProgressReq obj = new C2SQuestQuestProgressReq();
                obj.KeyId = ReadUInt32(buffer);
                obj.ProgressCharacterId = ReadUInt32(buffer);
                obj.QuestScheduleId = ReadUInt32(buffer);
                obj.ProcessNo = ReadUInt16(buffer);
                return obj;
            }
        }

    }
}