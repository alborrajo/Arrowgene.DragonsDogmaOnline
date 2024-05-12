using System;
using System.Collections.Generic;
using Arrowgene.Ddon.Database.Model;
using Arrowgene.Ddon.Server;
using Arrowgene.Ddon.Shared.Entity.PacketStructure;
using Arrowgene.Ddon.Shared.Model;
using Arrowgene.Ddon.Shared.Network;
using Arrowgene.Logging;

namespace Arrowgene.Ddon.LoginServer.Handler
{
    public class ClientLoginHandler : LoginRequestPacketHandler<C2LLoginReq, L2CLoginRes>
    {
        private static readonly ServerLogger Logger = LogProvider.Logger<ServerLogger>(typeof(ClientLoginHandler));

        private readonly LoginServerSetting _setting;
        private readonly object _tokensInFLightLock;
        private readonly HashSet<string> _tokensInFlight;

        public ClientLoginHandler(DdonLoginServer server) : base(server)
        {
            _setting = server.Setting;
            _tokensInFLightLock = new object();
            _tokensInFlight = new HashSet<string>();
        }

        public override void Handle(LoginClient client, StructurePacket<C2LLoginReq> request, L2CLoginRes response)
        {
            DateTime now = DateTime.UtcNow;
            client.SetChallengeCompleted(true);

            string oneTimeToken = request.Structure.OneTimeToken;
            Logger.Debug(client, $"Received LoginToken:{oneTimeToken} for platform:{request.Structure.PlatformType}");

            response.OneTimeToken = oneTimeToken;

            if (!LockToken(oneTimeToken))
            {
                Logger.Error(client, $"OneTimeToken {oneTimeToken} is in process.");
                throw new ResponseErrorException();
            }

            try
            {
                Account account = Database.SelectAccountByLoginToken(oneTimeToken);
                if (_setting.AccountRequired)
                {
                    if (account == null)
                    {
                        Logger.Error(client, "Invalid OneTimeToken");
                        throw new ResponseErrorException();
                    }
                }
                else
                {
                    // allow easy access
                    // assume token as account name, password & email
                    if (account == null)
                    {
                        account = Database.SelectAccountByName(oneTimeToken);
                        if (account == null)
                        {
                            account = Database.CreateAccount(oneTimeToken, oneTimeToken, oneTimeToken);
                            if (account == null)
                            {
                                Logger.Error(client,
                                    "Could not create account from OneTimeToken, choose another token");
                                throw new ResponseErrorException();
                            }

                            Logger.Info(client, "Created new account from OneTimeToken");
                        }

                        account.LoginToken = oneTimeToken;
                        account.LoginTokenCreated = now;
                    }
                }

                if (!account.LoginTokenCreated.HasValue)
                {
                    Logger.Error(client, "No login token exists");
                    throw new ResponseErrorException();
                }

                TimeSpan loginTokenAge = account.LoginTokenCreated.Value - now;
                if (loginTokenAge > TimeSpan.FromDays(7)) // TODO convert to setting
                {
                    Logger.Error(client, $"OneTimeToken Created at: {account.LoginTokenCreated} expired.");
                    throw new ResponseErrorException();
                }

                List<Connection> connections = Database.SelectConnectionsByAccountId(account.Id);
                if (connections.Count > 0)
                {
                    // todo check connection age?
                    Logger.Error(client, $"Already logged in");
                    throw new ResponseErrorException();
                }
                
                // Order Important,
                // account need to be only assigned after
                // verification that no connection exists, and before
                // registering the connection
                client.Account = account;
                
                Connection connection = new Connection();
                connection.ServerId = Server.Id;
                connection.AccountId = account.Id;
                connection.Type = ConnectionType.LoginServer;
                connection.Created = now;
                if (!Database.InsertConnection(connection))
                {
                    Logger.Error(client, $"Failed to register login connection");
                    throw new ResponseErrorException();
                }

                client.Account.LastAuthentication = now;
                client.UpdateIdentity();
                Database.UpdateAccount(client.Account);

                Logger.Info(client, "Logged In");
            }
            finally
            {
                ReleaseToken(oneTimeToken);
            }
        }

        private void ReleaseToken(string token)
        {
            lock (_tokensInFLightLock)
            {
                if (!_tokensInFlight.Contains(token))
                {
                    return;
                }

                _tokensInFlight.Remove(token);
            }
        }

        /// <summary>
        /// Locks a token, which can not be used in any other thread until released
        /// </summary>
        /// <param name="token"></param>
        /// <returns>true if token was locked, false if token already locked</returns>
        private bool LockToken(string token)
        {
            lock (_tokensInFLightLock)
            {
                if (_tokensInFlight.Contains(token))
                {
                    return false;
                }

                _tokensInFlight.Add(token);
                return true;
            }
        }
    }
}
