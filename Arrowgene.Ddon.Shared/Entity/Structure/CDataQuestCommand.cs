using Arrowgene.Buffers;

namespace Arrowgene.Ddon.Shared.Entity.Structure
{
    public class CDataQuestCommand
    {
        public ushort Command { get; set; }
        public int Param01 { get; set; }
        public int Param02 { get; set; }
        public int Param03 { get; set; }
        public int Param04 { get; set; }

        public CDataQuestCommand() {}
        public CDataQuestCommand(ushort command)
        {
            Command = command;
        }
        public CDataQuestCommand(ushort command, int param01)
        {
            Command = command;
            Param01 = param01;
        }
        public CDataQuestCommand(ushort command, int param01, int param02)
        {
            Command = command;
            Param01 = param01;
            Param02 = param02;
        }
        public CDataQuestCommand(ushort command, int param01, int param02, int param03)
        {
            Command = command;
            Param01 = param01;
            Param02 = param02;
            Param03 = param03;
        }
        public CDataQuestCommand(ushort command, int param01, int param02, int param03, int param04)
        {
            Command = command;
            Param01 = param01;
            Param02 = param02;
            Param03 = param03;
            Param04 = param04;
        }
    
        public class Serializer : EntitySerializer<CDataQuestCommand>
        {
            public override void Write(IBuffer buffer, CDataQuestCommand obj)
            {
                WriteUInt16(buffer, obj.Command);
                WriteInt32(buffer, obj.Param01);
                WriteInt32(buffer, obj.Param02);
                WriteInt32(buffer, obj.Param03);
                WriteInt32(buffer, obj.Param04);
            }
        
            public override CDataQuestCommand Read(IBuffer buffer)
            {
                CDataQuestCommand obj = new CDataQuestCommand();
                obj.Command = ReadUInt16(buffer);
                obj.Param01 = ReadInt32(buffer);
                obj.Param02 = ReadInt32(buffer);
                obj.Param03 = ReadInt32(buffer);
                obj.Param04 = ReadInt32(buffer);
                return obj;
            }
        }

        public enum AnnounceType : int
        {
            QUEST_ANNOUNCE_TYPE_ACCEPT = 0x0,
            QUEST_ANNOUNCE_TYPE_CLEAR = 0x1,
            QUEST_ANNOUNCE_TYPE_FAILED = 0x2,
            QUEST_ANNOUNCE_TYPE_UPDATE = 0x3,
            QUEST_ANNOUNCE_TYPE_DISCOVERED = 0x4,
            QUEST_ANNOUNCE_TYPE_CAUTION = 0x5,
            QUEST_ANNOUNCE_TYPE_START = 0x6,
            QUEST_ANNOUNCE_TYPE_EX_UPDATE = 0x7,
            QUEST_ANNOUNCE_TYPE_END = 0x8,
            QUEST_ANNOUNCE_TYPE_STAGE_START = 0x9,
            QUEST_ANNOUNCE_TYPE_STAGE_CLEAR = 0xA,
            QUEST_ANNOUNCE_TYPE_CANCEL = 0xB,
        }

        public static CDataQuestCommand ResultSetAnnounce(AnnounceType announceType, bool isDiscovered = false) => new CDataQuestCommand(4, (int) announceType, isDiscovered ? 1 : 0);
    }
}

/*
From the PS4 build:
(Command, Result command execute function)

(1, bool resultLotOn(cQuestProcess * this, s32 stageNo, s32 lotNo, s32 param03, s32 param04))
(2, bool resultLotOff(cQuestProcess * this, s32 stageNo, s32 lotNo, s32 param03, s32 param04))
(3, bool resultHandItem(cQuestProcess * this, s32 itemId, s32 itemNum, s32 param03, s32 param04))
(4, bool resultSetAnnounce(cQuestProcess * this, s32 announceType, s32 isDiscovered, s32 param03, s32 param04))
(5, bool resultUpdateAnnounce(cQuestProcess * this, s32 type, s32 param02, s32 param03, s32 param04))
(6, bool resultChangeMessage(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(7, bool resultQstFlagOn(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(8, bool resultMyQstFlagOn(cQuestProcess * this, s32 flagNo, s32 param02, s32 param03, s32 param04))
(9, bool resultGlobalFlagOn(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(10, bool resultQstTalkChg(cQuestProcess * this, s32 npcId, s32 msgNo, s32 param03, s32 param04))
(11, bool resultQstTalkDel(cQuestProcess * this, s32 npcId, s32 param02, s32 param03, s32 param04))
(12, bool resultStageJump(cQuestProcess * this, s32 stageNo, s32 startPos, s32 param03, s32 param04))
(13, bool resultEventExec(cQuestProcess * this, s32 stageNo, s32 eventNo, s32 jumpStageNo, s32 jumpStartPosNo))
(14, bool resultCallMessage(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(15, bool resultPrt(cQuestProcess * this, s32 stageNo, s32 x, s32 y, s32 z))
(16, bool resultQstLayoutFlagOn(cQuestProcess * this, s32 flagNo, s32 param02, s32 param03, s32 param04))
(17, bool resultQstLayoutFlagOff(cQuestProcess * this, s32 flagNo, s32 param02, s32 param03, s32 param04))
(18, bool resultQstSceFlagOn(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(19, bool resultQstDogmaOrb(cQuestProcess * this, s32 orbNum, s32 param02, s32 param03, s32 param04))
(20, bool resultGotoMainPwanEdit(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(21, bool resultAddFsmNpcList(cQuestProcess * this, s32 npcId, s32 param02, s32 param03, s32 param04))
(22, bool resultEndCycle(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(23, bool resultAddCycleTimer(cQuestProcess * this, s32 sec, s32 param02, s32 param03, s32 param04))
(24, bool resultAddMarkerAtItem(cQuestProcess * this, s32 stageNo, s32 x, s32 y, s32 z))
(25, bool resultAddMarkerAtDest(cQuestProcess * this, s32 stageNo, s32 x, s32 y, s32 z))
(26, bool resultAddResultPoint(cQuestProcess * this, s32 tableIndex, s32 param02, s32 param03, s32 param04))
(27, bool resultPushImteToPlBag(cQuestProcess * this, s32 itemId, s32 itemNum, s32 param03, s32 param04))
(28, bool resultStartTimer(cQuestProcess * this, s32 timerNo, s32 sec, s32 param03, s32 param04))
(29, bool resultSetRandom(cQuestProcess * this, s32 randomNo, s32 minValue, s32 maxValue, s32 resultValue))
(30, bool resultResetRandom(cQuestProcess * this, s32 randomNo, s32 param02, s32 param03, s32 param04))
(31, bool resultBgmRequest(cQuestProcess * this, s32 type, s32 bgmId, s32 param03, s32 param04))
(32, bool resultBgmStop(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(33, bool resultSetWaypoint(cQuestProcess * this, s32 npcId, s32 waypointNo0, s32 waypointNo1, s32 waypointNo2))
(34, bool resultForceTalkQuest(cQuestProcess * this, s32 npcId, s32 groupSerial, s32 param03, s32 param04))
(35, bool resultTutorialDialog(cQuestProcess * this, s32 guideNo, s32 param02, s32 param03, s32 param04))
(36, bool resultAddKeyItemPoint(cQuestProcess * this, s32 keyItemIdx, s32 pointNum, s32 param03, s32 param04))
(37, bool resultDontSaveProcess(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(38, bool resultInterruptCycleContents(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(39, bool resultQuestEvaluationPoint(cQuestProcess * this, s32 point, s32 param02, s32 param03, s32 param04))
(40, bool resultCheckOrderCondition(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(41, bool resultWorldManageLayoutFlagOn(cQuestProcess * this, s32 flagNo, s32 questId, s32 param03, s32 param04))
(42, bool resultWorldManageLayoutFlagOff(cQuestProcess * this, s32 flagNo, s32 questId, s32 param03, s32 param04))
(43, bool resultPlayEndingForFirstSeason(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(44, bool resultAddCyclePurpose(cQuestProcess * this, s32 announceNo, s32 type, s32 param03, s32 param04))
(45, bool resultRemoveCyclePurpose(cQuestProcess * this, s32 announceNo, s32 param02, s32 param03, s32 param04))
(46, bool resultUpdateAnnounceDirect(cQuestProcess * this, s32 announceNo, s32 type, s32 param03, s32 param04))
(47, bool resultSetCheckPoint(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(48, bool resultReturnCheckPoint(cQuestProcess * this, s32 processNo, s32 param02, s32 param03, s32 param04))
(49, bool resultCallGeneralAnnounce(cQuestProcess * this, s32 type, s32 msgNo, s32 param03, s32 param04))
(50, bool resultTutorialEnemyInvincibleOff(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(51, bool resultSetDiePlayerReturnPos(cQuestProcess * this, s32 stageNo, s32 startPos, s32 outSceNo, s32 param04))
(52, bool resultWorldManageQuestFlagOn(cQuestProcess * this, s32 flagNo, s32 questId, s32 param03, s32 param04))
(53, bool resultWorldManageQuestFlagOff(cQuestProcess * this, s32 flagNo, s32 questId, s32 param03, s32 param04))
(54, bool resultReturnCheckPointEx(cQuestProcess * this, s32 processNo, s32 param02, s32 param03, s32 param04))
(55, bool resultResetCheckPoint(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(56, bool resultResetDiePlayerReturnPos(cQuestProcess * this, s32 stageNo, s32 startPos, s32 param03, s32 param04))
(57, bool resultSetBarricade(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(58, bool resultResetBarricade(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(59, bool resultTutorialEnemyInvincibleOn(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(60, bool resultResetTutorialFlag(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(61, bool resultStartContentsTimer(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(62, bool resultMyQstFlagOff(cQuestProcess * this, s32 flagNo, s32 param02, s32 param03, s32 param04))
(63, bool resultPlayCameraEvent(cQuestProcess * this, s32 stageNo, s32 eventNo, s32 param03, s32 param04))
(64, bool resultEndEndQuest(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(65, bool resultReturnAnnounce(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(66, bool resultAddEndContentsPurpose(cQuestProcess * this, s32 announceNo, s32 type, s32 param03, s32 param04))
(67, bool resultRemoveEndContentsPurpose(cQuestProcess * this, s32 announceNo, s32 param02, s32 param03, s32 param04))
(68, bool resultStopCycleTimer(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(69, bool resultRestartCycleTimer(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(70, bool resultAddAreaPoint(cQuestProcess * this, s32 AreaId, s32 AddPoint, s32 param03, s32 param04))
(71, bool resultLayoutFlagRandomOn(cQuestProcess * this, s32 FlanNo1, s32 FlanNo2, s32 FlanNo3, s32 ResultNo))
(72, bool resultSetDeliverInfo(cQuestProcess * this, s32 stageNo, s32 npcId, s32 groupSerial, s32 param04))
(73, bool resultSetDeliverInfoQuest(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 groupSerial))
(74, bool resultBgmRequestFix(cQuestProcess * this, s32 type, s32 bgmId, s32 param03, s32 param04))
(75, bool resultEventExecCont(cQuestProcess * this, s32 stageNo, s32 eventNo, s32 jumpStageNo, s32 jumpStartPosNo))
(76, bool resultPlPadOff(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(77, bool resultPlPadOn(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(78, bool resultEnableGetSetQuestList(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(79, bool resultStartMissionAnnounce(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(80, bool resultStageAnnounce(cQuestProcess * this, s32 type, s32 num, s32 param03, s32 param04))
(81, bool resultReleaseAnnounce(cQuestProcess * this, s32 id, s32 param02, s32 param03, s32 param04))
(82, bool resultButtonGuideFlagOn(cQuestProcess * this, s32 buttonGuideNo, s32 param02, s32 param03, s32 param04))
(83, bool resultButtonGuideFlagOff(cQuestProcess * this, s32 buttonGuideNo, s32 param02, s32 param03, s32 param04))
(84, bool resultAreaJumpFadeContinue(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(85, bool resultExeEventAfterStageJump(cQuestProcess * this, s32 stageNo, s32 eventNo, s32 startPos, s32 param04))
(86, bool resultExeEventAfterStageJumpContinue(cQuestProcess * this, s32 stageNo, s32 eventNo, s32 startPos, s32 param04))
(87, bool resultPlayMessage(cQuestProcess * this, s32 groupNo, s32 waitTime, s32 param03, s32 param04))
(88, bool resultStopMessage(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(89, bool resultDecideDivideArea(cQuestProcess * this, s32 stageNo, s32 startPosNo, s32 param03, s32 param04))
(90, bool resultShiftPhase(cQuestProcess * this, s32 phaseId, s32 param02, s32 param03, s32 param04))
(91, bool resultReleaseMyRoom(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(92, bool resultDivideSuccess(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(93, bool resultDivideFailed(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(94, bool resultSetProgressBonus(cQuestProcess * this, s32 rewardRank, s32 param02, s32 param03, s32 param04))
(95, bool resultRefreshOmKeyDisp(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(96, bool resultSwitchPawnQuestTalk(cQuestProcess * this, s32 type, s32 param02, s32 param03, s32 param04))
(97, bool resultLinkageEnemyFlagOn(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 flagId))
(98, bool resultLinkageEnemyFlagOff(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 flagId))

(1, bool checkTalkNpc(cQuestProcess * this, s32 stageNo, s32 npcId, s32 param03, s32 param04))
(2, bool checkDieEnemy(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(3, bool checkSceHitIn(cQuestProcess * this, s32 stageNo, s32 sceNo, s32 param03, s32 param04))
(4, bool checkHaveItem(cQuestProcess * this, s32 itemId, s32 itemNum, s32 param03, s32 param04))
(5, bool checkDeliverItem(cQuestProcess * this, s32 itemId, s32 itemNum, s32 npcId, s32 msgNo))
(6, bool checkEmDieLight(cQuestProcess * this, s32 enemyId, s32 enemyLv, s32 enemyNum, s32 param04))
(7, bool checkQstFlagOn(cQuestProcess * this, s32 questId, s32 flagNo, s32 param03, s32 param04))
(8, bool checkQstFlagOff(cQuestProcess * this, s32 questId, s32 flagNo, s32 param03, s32 param04))
(9, bool checkMyQstFlagOn(cQuestProcess * this, s32 flagNo, s32 param02, s32 param03, s32 param04))
(10, bool checkMyQstFlagOff(cQuestProcess * this, s32 flagNo, s32 param02, s32 param03, s32 param04))
(11, bool checkPadding00(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(12, bool checkPadding01(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(13, bool checkPadding02(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(14, bool checkStageNo(cQuestProcess * this, s32 stageNo, s32 param02, s32 param03, s32 param04))
(15, bool checkEventEnd(cQuestProcess * this, s32 stageNo, s32 eventNo, s32 param03, s32 param04))
(16, bool checkPrt(cQuestProcess * this, s32 stageNo, s32 x, s32 y, s32 z))
(17, bool checkClearcount(cQuestProcess * this, s32 minCount, s32 maxCount, s32 param03, s32 param04))
(18, bool checkSceFlagOn(cQuestProcess * this, s32 flagNo, s32 param02, s32 param03, s32 param04))
(19, bool checkSceFlagOff(cQuestProcess * this, s32 flagNo, s32 param02, s32 param03, s32 param04))
(20, bool checkTouchActToNpc(cQuestProcess * this, s32 stageNo, s32 npcId, s32 param03, s32 param04))
(21, bool checkOrderDecide(cQuestProcess * this, s32 npcId, s32 param02, s32 param03, s32 param04))
(22, bool checkIsEndCycle(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(23, bool checkIsInterruptCycle(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(24, bool checkIsFailedCycle(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(25, bool checkIsEndResult(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(26, bool checkNpcTalkAndOrderUi(cQuestProcess * this, s32 stageNo, s32 npcId, s32 noOrderGroupSerial, s32 param04))
(27, bool checkNpcTouchAndOrderUi(cQuestProcess * this, s32 stageNo, s32 npcId, s32 noOrderGroupSerial, s32 param04))
(28, bool checkStageNoNotEq(cQuestProcess * this, s32 stageNo, s32 param02, s32 param03, s32 param04))
(29, bool checkWarlevel(cQuestProcess * this, s32 warLevel, s32 param02, s32 param03, s32 param04))
(30, bool checkTalkNpcWithoutMarker(cQuestProcess * this, s32 stageNo, s32 npcId, s32 param03, s32 param04))
(31, bool checkHaveMoney(cQuestProcess * this, s32 gold, s32 type, s32 param03, s32 param04))
(32, bool checkSetQuestClearNum(cQuestProcess * this, s32 clearNum, s32 areaId, s32 param03, s32 param04))
(33, bool checkMakeCraft(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(34, bool checkPlayEmotion(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(35, bool checkIsEndTimer(cQuestProcess * this, s32 timerNo, s32 param02, s32 param03, s32 param04))
(36, bool checkIsEnemyFound(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(37, bool checkRandomEq(cQuestProcess * this, s32 randomNo, s32 value, s32 param03, s32 param04))
(38, bool checkRandomNotEq(cQuestProcess * this, s32 randomNo, s32 value, s32 param03, s32 param04))
(39, bool checkRandomLess(cQuestProcess * this, s32 randomNo, s32 value, s32 param03, s32 param04))
(40, bool checkRandomNotGreater(cQuestProcess * this, s32 randomNo, s32 value, s32 param03, s32 param04))
(41, bool checkRandomGreater(cQuestProcess * this, s32 randomNo, s32 value, s32 param03, s32 param04))
(42, bool checkRandomNotLess(cQuestProcess * this, s32 randomNo, s32 value, s32 param03, s32 param04))
(43, bool checkClearcount02(cQuestProcess * this, s32 div, s32 value, s32 param03, s32 param04))
(44, bool checkIngameTimeRangeEq(cQuestProcess * this, s32 minTime, s32 maxTime, s32 param03, s32 param04))
(45, bool checkIngameTimeRangeNotEq(cQuestProcess * this, s32 minTime, s32 maxTime, s32 param03, s32 param04))
(46, bool checkPlHp(cQuestProcess * this, s32 hpRate, s32 type, s32 param03, s32 param04))
(47, bool checkEmHpNotLess(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 hpRate))
(48, bool checkEmHpLess(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 hpRate))
(49, bool checkWeatherEq(cQuestProcess * this, s32 weatherId, s32 param02, s32 param03, s32 param04))
(50, bool checkWeatherNotEq(cQuestProcess * this, s32 weatherId, s32 param02, s32 param03, s32 param04))
(51, bool checkPlJobEq(cQuestProcess * this, s32 jobId, s32 param02, s32 param03, s32 param04))
(52, bool checkPlJobNotEq(cQuestProcess * this, s32 jobId, s32 param02, s32 param03, s32 param04))
(53, bool checkPlSexEq(cQuestProcess * this, s32 sex, s32 param02, s32 param03, s32 param04))
(54, bool checkPlSexNotEq(cQuestProcess * this, s32 sex, s32 param02, s32 param03, s32 param04))
(55, bool checkSceHitOut(cQuestProcess * this, s32 stageNo, s32 sceNo, s32 param03, s32 param04))
(56, bool checkWaitOrder(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(57, bool checkOmSetTouch(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(58, bool checkOmReleaseTouch(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(59, bool checkJobLevelNotLess(cQuestProcess * this, s32 checkType, s32 level, s32 param03, s32 param04))
(60, bool checkJobLevelLess(cQuestProcess * this, s32 checkType, s32 level, s32 param03, s32 param04))
(61, bool checkMyQstFlagOnFromFsm(cQuestProcess * this, s32 flagNo, s32 param02, s32 param03, s32 param04))
(62, bool checkSceHitInWithoutMarker(cQuestProcess * this, s32 stageNo, s32 sceNo, s32 param03, s32 param04))
(63, bool checkSceHitOutWithoutMarker(cQuestProcess * this, s32 stageNo, s32 sceNo, s32 param03, s32 param04))
(64, bool checkKeyItemPoint(cQuestProcess * this, s32 idx, s32 num, s32 param03, s32 param04))
(65, bool checkIsNotEndTimer(cQuestProcess * this, s32 timerNo, s32 param02, s32 param03, s32 param04))
(66, bool checkIsMainQuestClear(cQuestProcess * this, s32 questId, s32 param02, s32 param03, s32 param04))
(67, bool checkDogmaOrb(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(68, bool checkIsEnemyFoundForOrder(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(69, bool checkIsTutorialFlagOn(cQuestProcess * this, s32 flagNo, s32 param02, s32 param03, s32 param04))
(70, bool checkQuestOmSetTouch(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(71, bool checkQuestOmReleaseTouch(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(72, bool checkNewTalkNpc(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 questId))
(73, bool checkNewTalkNpcWithoutMarker(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 questId))
(74, bool checkIsTutorialQuestClear(cQuestProcess * this, s32 questId, s32 param02, s32 param03, s32 param04))
(75, bool checkIsMainQuestOrder(cQuestProcess * this, s32 questId, s32 param02, s32 param03, s32 param04))
(76, bool checkIsTutorialQuestOrder(cQuestProcess * this, s32 questId, s32 param02, s32 param03, s32 param04))
(77, bool checkIsTouchPawnDungeonOm(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(78, bool checkIsOpenDoorOmQuestSet(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 questId))
(79, bool checkEmDieForRandomDungeon(cQuestProcess * this, s32 stageNo, s32 enemyId, s32 enemyNum, s32 param04))
(80, bool checkNpcHpNotLess(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 hpRate))
(81, bool checkNpcHpLess(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 hpRate))
(82, bool checkIsEnemyFoundWithoutMarker(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(83, bool checkIsEventBoardAccepted(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(84, bool checkWorldManageQuestFlagOn(cQuestProcess * this, s32 flagNo, s32 questId, s32 param03, s32 param04))
(85, bool checkWorldManageQuestFlagOff(cQuestProcess * this, s32 flagNo, s32 questId, s32 param03, s32 param04))
(86, bool checkTouchEventBoard(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(87, bool checkOpenEntryRaidBoss(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(88, bool checkOepnEntryFortDefense(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(89, bool checkDiePlayer(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(90, bool checkPartyNumNotLessWtihoutPawn(cQuestProcess * this, s32 partyMemberNum, s32 param02, s32 param03, s32 param04))
(91, bool checkPartyNumNotLessWithPawn(cQuestProcess * this, s32 partyMemberNum, s32 param02, s32 param03, s32 param04))
(92, bool checkLostMainPawn(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(93, bool checkSpTalkNpc(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(94, bool checkOepnJobMaster(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(95, bool checkTouchRimStone(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(96, bool checkGetAchievement(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(97, bool checkDummyNotProgress(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(98, bool checkDieRaidBoss(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(99, bool checkCycleTimerZero(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(100, bool checkCycleTimer(cQuestProcess * this, s32 timeSec, s32 param02, s32 param03, s32 param04))
(101, bool checkQuestNpcTalkAndOrderUi(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 questId))
(102, bool checkQuestNpcTouchAndOrderUi(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 questId))
(103, bool checkIsFoundRaidBoss(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 enemyId))
(104, bool checkQuestOmSetTouchWithoutMarker(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(105, bool checkQuestOmReleaseTouchWithoutMarker(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(106, bool checkTutorialTalkNpc(cQuestProcess * this, s32 stageNo, s32 npcId, s32 param03, s32 param04))
(107, bool checkIsLogin(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(108, bool checkIsPlayEndFirstSeasonEndCredit(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(109, bool checkIsKilledTargetEnemySetGroup(cQuestProcess * this, s32 flagNo, s32 param02, s32 param03, s32 param04))
(110, bool checkIsKilledTargetEmSetGrpNoMarker(cQuestProcess * this, s32 flagNo, s32 param02, s32 param03, s32 param04))
(111, bool checkIsLeftCycleTimer(cQuestProcess * this, s32 timeSec, s32 param02, s32 param03, s32 param04))
(112, bool checkOmEndText(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(113, bool checkQuestOmEndText(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(114, bool checkOpenAreaMaster(cQuestProcess * this, s32 areaId, s32 param02, s32 param03, s32 param04))
(115, bool checkHaveItemAllBag(cQuestProcess * this, s32 itemId, s32 itemNum, s32 param03, s32 param04))
(116, bool checkOpenNewspaper(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(117, bool checkOpenQuestBoard(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(118, bool checkStageNoWithoutMarker(cQuestProcess * this, s32 stageNo, s32 param02, s32 param03, s32 param04))
(119, bool checkTalkQuestNpcUnitMarker(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 questId))
(120, bool checkTouchQuestNpcUnitMarker(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 questId))
(121, bool checkIsExistSecondPawn(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(122, bool checkIsOrderJobTutorialQuest(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(123, bool checkIsOpenWarehouse(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(124, bool checkIsMyquestLayoutFlagOn(cQuestProcess * this, s32 FlagNo, s32 param02, s32 param03, s32 param04))
(125, bool checkIsMyquestLayoutFlagOff(cQuestProcess * this, s32 FlagNo, s32 param02, s32 param03, s32 param04))
(126, bool checkIsOpenWarehouseReward(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(127, bool checkIsOrderLightQuest(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(128, bool checkIsOrderWorldQuest(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(129, bool checkIsLostMainPawn(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(130, bool checkIsFullOrderQuest(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(131, bool checkIsBadStatus(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(132, bool checkCheckAreaRank(cQuestProcess * this, s32 AreaId, s32 AreaRank, s32 param03, s32 param04))
(133, bool checkPadding133(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(134, bool checkEnablePartyWarp(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(135, bool checkIsHugeble(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(136, bool checkIsDownEnemy(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(137, bool checkOpenAreaMasterSupplies(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(138, bool checkOpenEntryBoard(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(139, bool checkNoticeInterruptContents(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(140, bool checkOpenRetrySelect(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(141, bool checkIsPlWeakening(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(142, bool checkNoticePartyInvite(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(143, bool checkIsKilledAreaBoss(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(144, bool checkIsPartyReward(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(145, bool checkIsFullBag(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(146, bool checkOpenCraftExam(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(147, bool checkLevelUpCraft(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(148, bool checkIsClearLightQuest(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(149, bool checkOpenJobMasterReward(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(150, bool checkTouchActQuestNpc(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 questId))
(151, bool checkIsLeaderAndJoinPawn(cQuestProcess * this, s32 pawnNum, s32 param02, s32 param03, s32 param04))
(152, bool checkIsAcceptLightQuest(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(153, bool checkIsReleaseWarpPoint(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(154, bool checkIsSetPlayerSkill(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(155, bool checkIsOrderMyQuest(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(156, bool checkIsNotOrderMyQuest(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(157, bool checkHasMypawn(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(158, bool checkIsFavoriteWarpPoint(cQuestProcess * this, s32 warpPointId, s32 param02, s32 param03, s32 param04))
(159, bool checkCraft(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(160, bool checkIsKilledTargetEnemySetGroupGmMain(cQuestProcess * this, s32 flagNo, s32 param02, s32 param03, s32 param04))
(161, bool checkIsKilledTargetEnemySetGroupGmSub(cQuestProcess * this, s32 flagNo, s32 param02, s32 param03, s32 param04))
(162, bool checkHasUsedKey(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 questId))
(163, bool checkIsCycleFlagOffPeriod(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(164, bool checkIsEnemyFoundGmMain(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(165, bool checkIsEnemyFoundGmSub(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(166, bool checkIsLoginBugFixedOnly(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(167, bool checkIsSearchClan(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(168, bool checkIsOpenAreaListUi(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(169, bool checkIsReleaseWarpPointAnyone(cQuestProcess * this, s32 warpPointId, s32 param02, s32 param03, s32 param04))
(170, bool checkDevidePlayer(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(171, bool checkNowPhase(cQuestProcess * this, s32 phaseId, s32 param02, s32 param03, s32 param04))
(172, bool checkIsReleasePortal(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(173, bool checkIsGetAppraiseItem(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(174, bool checkIsSetPartnerPawn(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(175, bool checkIsPresentPartnerPawn(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(176, bool checkIsReleaseMyRoom(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(177, bool checkIsExistDividePlayer(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(178, bool checkNotDividePlayer(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(179, bool checkIsGatherPartyInStage(cQuestProcess * this, s32 stageNo, s32 param02, s32 param03, s32 param04))
(180, bool checkIsFinishedEnemyDivideAction(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(181, bool checkIsOpenDoorOmQuestSetNoMarker(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 questId))
(182, bool checkIsFinishedEventOrderNum(cQuestProcess * this, s32 stageNo, s32 eventNo, s32 param03, s32 param04))
(183, bool checkIsPresentPartnerPawnNoMarker(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(184, bool checkIsOmBrokenLayout(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(185, bool checkIsOmBrokenQuest(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(186, bool checkIsHoldingPeriodCycleContents(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(187, bool checkIsNotHoldingPeriodCycleContents(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(188, bool checkIsResetInstanceArea(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(189, bool checkCheckMoonAge(cQuestProcess * this, s32 moonAgeStart, s32 moonAgeEnd, s32 param03, s32 param04))
(190, bool checkIsOrderPawnQuest(cQuestProcess * this, s32 orderGroupSerial, s32 noOrderGroupSerial, s32 param03, s32 param04))
(191, bool checkIsTakePictures(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(192, bool checkIsStageForMainQuest(cQuestProcess * this, s32 stageNo, s32 param02, s32 param03, s32 param04))
(193, bool checkIsReleasePawnExpedition(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(194, bool checkOpenPpMode(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(195, bool checkPpNotLess(cQuestProcess * this, s32 point, s32 param02, s32 param03, s32 param04))
(196, bool checkOpenPpShop(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(197, bool checkTouchClanBoard(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(198, bool checkIsOneOffGather(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(199, bool checkIsOmBrokenLayoutNoMarker(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(200, bool checkIsOmBrokenQuestNoMarker(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 param04))
(201, bool checkKeyItemPointEq(cQuestProcess * this, s32 idx, s32 num, s32 param03, s32 param04))
(202, bool checkIsEmotion(cQuestProcess * this, s32 actNo, s32 param02, s32 param03, s32 param04))
(203, bool checkIsEquipColor(cQuestProcess * this, s32 color, s32 param02, s32 param03, s32 param04))
(204, bool checkIsEquip(cQuestProcess * this, s32 itemId, s32 param02, s32 param03, s32 param04))
(205, bool checkIsTakePicturesNpc(cQuestProcess * this, s32 stageNo, s32 npcId01, s32 npcId02, s32 npcId03))
(206, bool checkSayMessage(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))
(207, bool checkIsTakePicturesWithoutPawn(cQuestProcess * this, s32 stageNo, s32 x, s32 y, s32 z))
(208, bool checkIsLinkageEnemyFlag(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 flagNo))
(209, bool checkIsLinkageEnemyFlagOff(cQuestProcess * this, s32 stageNo, s32 groupNo, s32 setNo, s32 flagNo))
(210, bool checkIsReleaseSecretRoom(cQuestProcess * this, s32 param01, s32 param02, s32 param03, s32 param04))

  */