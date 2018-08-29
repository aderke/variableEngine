using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Evvosoft.ReplaceVariables.Service.Interfaces;
using Evvosoft.VariableEngine.Business.Contracts;
using Evvosoft.VariableEngine.Business.Models;
using Evvosoft.VariableEngine.Business.Processor;

namespace Evvosoft.ReplaceVariables.Service
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class ReplaceVariablesActor : Actor, IReplaceVariablesActor
    {
        private readonly IVariableProcessor Processor;

        /// <summary>
        /// Initializes a new instance of Service
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public ReplaceVariablesActor(ActorService actorService, ActorId actorId) 
            : base(actorService, actorId)
        {
            this.Processor = new VariableProcessor();
        }

        public async Task<string> ReplaceVariables(Dictionary<string, string> replaces)
        {
            var variables = await StateManager.GetStateAsync<MatchExpression>("variables");

            return this.Processor.ReplaceVariables(variables, replaces);
        }

        public async Task<MatchExpression> SaveVariables(MatchExpression expression)
        {
           return await StateManager.AddOrUpdateStateAsync("variables", expression, (id, value) => expression);
        }
    }
}
