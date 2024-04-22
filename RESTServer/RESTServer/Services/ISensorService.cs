using Google.Protobuf.WellKnownTypes;
using System.Data;
using Grpc.Net.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RESTServer
{
    public interface ISensorService
    {
        Task<SenzorPodaci> GetSensorData(string idSenzora);

        Task<Odgovor> AddSensorData(SenzorPodaci podaci);
        Task<Odgovor> UpdateData(SenzorPodaci podaci);
        Task<Odgovor> DeleteData(string id);
        Task<Value> GetMinPodaci(Query query);
        Task<Value> GetMaxPodaci(Query query);
        Task<Value> GetAvgPodaci(Query query);
       

    }
}
