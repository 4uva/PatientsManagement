using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using PatientsManagement.Common.Models;

namespace PatientsManagement.Elastic
{
    class QueryService : IQueryService
    {
        List<Field> patientFields = new List<Field>()
        {
            Infer.Field<Patient>(p => p.Name),
            Infer.Field<Patient>(p => p.Surname),
            Infer.Field<Patient>(p => p.Patronymic),
            Infer.Field<Patient>(p => p.PrimaryPhone),
            Infer.Field<Patient>(p => p.AdditionalContacts.First().Phone)
        };

        public ISearchRequest PrepareQuery(SearchDescriptor<Patient> descriptor, string queryString)
        {
            var tokens = queryString.Split();
            return descriptor.Query(q => MatchAllTokens(q, tokens));
        }

        public QueryContainer MatchToken(QueryContainerDescriptor<Patient> q, string token)
        {
            var processedToken = "*" + token.ToLower() + "*";
            var wildcards = patientFields.Select(field => q.Wildcard(field, processedToken)).ToList();
            return new BoolQuery()
            {
                Should = wildcards
            };
        }

        QueryContainer MatchAllTokens(QueryContainerDescriptor<Patient> q, IEnumerable<string> tokens)
        {
            var singleContainers = tokens.Select(token => MatchToken(q, token)).ToList();
            return new BoolQuery()
            {
                Must = singleContainers
            };
        }
    }
}
