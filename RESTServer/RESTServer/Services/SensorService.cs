using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;

namespace RESTServer.Services
{
    public class SensorService: ISensorService
    {
        private readonly GrpcChannel channel;
        private readonly SenzorSoba.SenzorSobaClient client;
        private static SensorService instance;
        private static readonly object lockObject = new object();

        //var client = new SenzorSoba.SenzorSobaClient(channel);
        public SensorService()
        {
            this.channel = GrpcChannel.ForAddress("http://nodejsumrezi:50051");
            this.client = new SenzorSoba.SenzorSobaClient(channel);

        }
        

        public static SensorService GetInstance()
        {
            // Double-checked locking to ensure thread safety
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SensorService();
                    }
                }
            }
            return instance;
        }

        public async Task<SenzorPodaci> GetSensorData(string idSenzora)
        {
           
            return await client.GetPodaciAsync(new SenzorID { IdSenzora = idSenzora }); 
        }

        public async Task<Odgovor> AddSensorData(SenzorPodaci podaci)
        {
            
            return await client.PutPodaciAsync(podaci);
        }

        public async Task<Odgovor> UpdateData(SenzorPodaci podaci)
        {
          
            return await client.UpdatePodaciAsync(podaci);
        }

        public async Task<Odgovor> DeleteData(string id)
        {
            
            return await client.DeletePodaciAsync(new SenzorID { IdSenzora = id }); 
        }

        public async Task<Value> GetMinPodaci(Query query)
        {
            Console.WriteLine("GetMinPodaci");
            Console.WriteLine(query.IdSenzora);
            return await client.GetMinPodaciAsync(query);
        }

        public async Task<Value> GetMaxPodaci(Query query)
        {
            Console.WriteLine("GetMaxPodaci");
                Console.WriteLine(query.IdSenzora);
            return await client.GetMaxPodaciAsync(query);
        }

        public async Task<Value> GetAvgPodaci(Query query)
        {
            Console.WriteLine("GetAvgPodaci");
            Console.WriteLine(query.IdSenzora);
            return await client.GetAvgPodaciAsync(query);
        }
    }
}
