using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using PatientsManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientsManagement.Elastic
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(
            this IServiceCollection services, string url, string defaultIndex)
        {
            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex)
                .DefaultMappingFor<Patient>(m => m
                    .PropertyName(p => p.Id, "id")
                )
                .DefaultMappingFor<AdditionalContact>(m => m
                    .PropertyName(c => c.Id, "id")
                );

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        }
    }
}

// TODO: study the possibility of switching to keyword analyzer
//client.Indices.Create(defaultIndex, c => c.Settings(s => s.Analysis(a => a.Analyzers(aa => aa
//                .Custom("modified_keyword", ca => ca
//                    .Tokenizer("keyword")
//                    .Filters("lowercase"))))));
/*
https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/writing-analyzers.html
https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/testing-analyzers.html
https://www.elastic.co/guide/en/elasticsearch/reference/current/analysis.html#_specifying_an_index_time_analyzer
https://www.khalidabuhakmeh.com/elasticsearch-lowercase-keyword-analyzer
https://www.elastic.co/guide/en/elasticsearch/reference/current/analysis-keyword-analyzer.html
 */

