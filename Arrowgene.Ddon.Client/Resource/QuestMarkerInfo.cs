using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arrowgene.Buffers;
using Arrowgene.Ddon.Shared.Model;

namespace Arrowgene.Ddon.Client.Resource
{
    public class QuestMarkerInfo : ResourceFile
    {
        public class MarkerInfo
        {
            public MtVector3 Pos { get; set; }
            public uint GroupNo { get; set; }
            public uint UniqueId { get; set; }
        }

        public List<MarkerInfo> MarkerInfos { get; }

        public QuestMarkerInfo()
        {
            MarkerInfos = new List<MarkerInfo>();
        }

        protected override void ReadResource(IBuffer buffer)
        {
            uint version = ReadUInt32(buffer);
            MarkerInfos.Clear();
            uint unk = ReadUInt32(buffer);
            uint count = ReadUInt32(buffer);
            for (int i = 0; i < count; i++)
            {
                MarkerInfo markerInfo = new MarkerInfo();
                markerInfo.Pos = ReadMtVector3(buffer);
                markerInfo.GroupNo = ReadUInt32(buffer);
                markerInfo.UniqueId = ReadUInt32(buffer);
                MarkerInfos.Add(markerInfo);
            }
        }
    }
}