using System.Collections.Generic;
using System.Threading.Tasks;
using Evvosoft.VariableEngine.Business.Models;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting;

[assembly: FabricTransportActorRemotingProvider(RemotingListener = RemotingListener.V2Listener, RemotingClient = RemotingClient.V2Client)]
namespace Evvosoft.ReplaceVariables.Service.Interfaces
{
    /// <summary>
    /// This interface defines the methods exposed by an actor.
    /// Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface IReplaceVariablesActor : IActor
    {
        Task<string> ReplaceVariables(Dictionary<string, string> replaces);

        Task<MatchExpression> SaveVariables(MatchExpression expression);
    }
}
