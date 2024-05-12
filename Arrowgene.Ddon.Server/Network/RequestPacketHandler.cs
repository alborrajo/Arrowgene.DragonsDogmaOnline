using System;
using Arrowgene.Ddon.Database;
using Arrowgene.Ddon.Shared.Entity;
using Arrowgene.Ddon.Shared.Model;
using Arrowgene.Ddon.Shared.Network;

namespace Arrowgene.Ddon.Server.Network
{
    public abstract class RequestPacketHandler<TClient, TReqStruct, TResStruct> : StructurePacketHandler<TClient, TReqStruct>
        where TClient : Client
        where TReqStruct : class, IPacketStructure, new()
        where TResStruct : ServerResponse, new()
    {
        protected RequestPacketHandler(DdonServer<TClient> server) : base(server)
        {
        }

        public abstract void Handle(TClient client, StructurePacket<TReqStruct> request, TResStruct response);

        public sealed override void Handle(TClient client, StructurePacket<TReqStruct> request)
        {
            var response = new TResStruct();
            try
            {
                Handle(client, request, response);
            }
            catch (ResponseErrorException ex)
            {
                response.Error = (uint) ex.ErrorCode;
                client.Send(response);
            }
            catch (Exception)
            {
                response.Error = (uint) ErrorCode.ERROR_CODE_FAIL;
                client.Send(response);
                throw;
            }    
        }

    }
}
