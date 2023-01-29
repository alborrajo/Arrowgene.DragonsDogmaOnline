using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using Arrowgene.Ddon.Client.Data;
using Arrowgene.Ddon.Client.Resource;
using Arrowgene.Ddon.Shared;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.Client
{
    public class ClientResourceRepository
    {
        private static readonly ILogger Logger = LogProvider.Logger<Logger>(typeof(ClientResourceRepository));

        public FieldAreaList FieldAreaList { get; private set; }
        public Dictionary<uint, QuestMarkerInfo> StageEctMarkers { get; private set; }

        private DirectoryInfo _directory;

        public ClientResourceRepository()
        {
            FieldAreaList = new FieldAreaList();
            StageEctMarkers = new Dictionary<uint, QuestMarkerInfo>();
        }

        public void Load(DirectoryInfo romDirectory)
        {
            _directory = romDirectory;
            if (_directory == null || !_directory.Exists)
            {
                Logger.Error("Rom Path Invalid");
                return;
            }
            
            FieldAreaList = GetResource<FieldAreaList>("game_common.arc", "etc/FieldArea/field_area_list");

            string stageArcFolderPath = Path.Combine(romDirectory.FullName, "marker");
            string[] stageArcFilePaths = Directory.GetFiles(romDirectory.FullName, "st*_marker.arc", SearchOption.AllDirectories);
            for (int i = 0; i < stageArcFilePaths.Length; i++)
            {
                string stageArcFilePath = stageArcFilePaths[i];
                string stageBasename = Path.GetFileNameWithoutExtension(stageArcFilePath);
                var match = Regex.Match(stageBasename, "st([0-9]+)_marker");
                if(match.Success) {
                    string stageArcFileRelativePath = stageArcFilePath.Substring(romDirectory.FullName.Length);
                    uint stageId = uint.Parse(match.Groups[1].Value);
                    QuestMarkerInfo stageEctMarkerInfo = GetResource<QuestMarkerInfo>(
                        stageArcFileRelativePath, $"scr/st{stageId:0000}/etc/{stageBasename}_ect", "qmi");
                    StageEctMarkers.Add(stageId, stageEctMarkerInfo);
                }
            }
        }

        private void AddMarker(List<FieldAreaMarkerInfo.MarkerInfo> markers,
            Dictionary<uint, List<FieldAreaMarkerInfo.MarkerInfo>> dst)
        {
            foreach (FieldAreaMarkerInfo.MarkerInfo marker in markers)
            {
                if (!dst.ContainsKey((uint) marker.StageNo))
                {
                    dst[(uint) marker.StageNo] = new List<FieldAreaMarkerInfo.MarkerInfo>();
                }

                dst[(uint) marker.StageNo].Add(marker);
            }
        }

        private void AddAdjoin(List<FieldAreaAdjoinList.AdjoinInfo> adjoins,
            Dictionary<uint, List<FieldAreaAdjoinList.AdjoinInfo>> dst)
        {
            foreach (FieldAreaAdjoinList.AdjoinInfo adjoin in adjoins)
            {
                if (!dst.ContainsKey((uint) adjoin.DestinationStageNo))
                {
                    dst[(uint) adjoin.DestinationStageNo] = new List<FieldAreaAdjoinList.AdjoinInfo>();
                }

                dst[(uint) adjoin.DestinationStageNo].Add(adjoin);
            }
        }

        private T GetFile<T>(string arcPath, string filePath, string ext = null) where T : ClientFile, new()
        {
            return ArcArchive.GetFile<T>(_directory, arcPath, filePath, ext);
        }

        private T GetResource<T>(string arcPath, string filePath, string ext = null) where T : ResourceFile, new()
        {
            return ArcArchive.GetResource<T>(_directory, arcPath, filePath, ext);
        }

        private T GetResource_NoLog<T>(string arcPath, string filePath, string ext = null) where T : ResourceFile, new()
        {
           return ArcArchive.GetResource_NoLog<T>(_directory, arcPath, filePath, ext);
        }
    }
}
