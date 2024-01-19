using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.Ddon.Shared.Model;
        
namespace Arrowgene.Ddon.Shared.Entity.Structure
{
    public class CDataQuestExp
    {
        public byte Unk0 { get; set; }
        public byte ExpMode { get; set; }
        public uint ExpAmount { get; set; }
    
        public class Serializer : EntitySerializer<CDataQuestExp>
        {
            public override void Write(IBuffer buffer, CDataQuestExp obj)
            {
                WriteByte(buffer, obj.Unk0);
                WriteByte(buffer, obj.ExpMode);
                WriteUInt32(buffer, obj.ExpAmount);
            }
        
            public override CDataQuestExp Read(IBuffer buffer)
            {
                CDataQuestExp obj = new CDataQuestExp();
                obj.Unk0 = ReadByte(buffer);
                obj.ExpMode = ReadByte(buffer);
                obj.ExpAmount = ReadUInt32(buffer);
                return obj;
            }
        }
    }
}