using Nest;
using PatientsManagement.Common.Models;

namespace PatientsManagement.Elastic
{
    public interface IQueryService
    {
        ISearchRequest PrepareQuery(SearchDescriptor<Patient> descriptor, string queryString);
    }
}