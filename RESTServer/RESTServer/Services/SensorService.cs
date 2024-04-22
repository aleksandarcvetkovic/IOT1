using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;

namespace RESTServer.Services
{
    public class SensorService: ISensorService
    {
        private readonly GrpcChannel channel;
        private static SensorService instance;
        private static readonly object lockObject = new object();

        //var client = new SenzorSoba.SenzorSobaClient(channel);
        public SensorService()
        {
            this.channel = GrpcChannel.ForAddress("http://nodejsumrezi:50051");

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
            var channel = GrpcChannel.ForAddress("http://nodejsumrezi:50051");
            var client = new SenzorSoba.SenzorSobaClient(channel);
            var reply = await client.GetPodaciAsync(new SenzorID { IdSenzora = idSenzora });
            return reply;
        }

        public async Task<Odgovor> AddSensorData(SenzorPodaci podaci)
        {
            var channel = GrpcChannel.ForAddress("http://nodejsumrezi:50051");
            var client = new SenzorSoba.SenzorSobaClient(channel);
            var reply = await client.PutPodaciAsync(podaci);
            return reply;
        }

        public async Task<Odgovor> UpdateData(SenzorPodaci podaci)
        {
            var channel = GrpcChannel.ForAddress("http://nodejsumrezi:50051");
            var client = new SenzorSoba.SenzorSobaClient(channel);
            var reply = await client.UpdatePodaciAsync(podaci);

            return reply;
        }

        public async Task<Odgovor> DeleteData(string id)
        {
            var channel = GrpcChannel.ForAddress("http://nodejsumrezi:50051");
            var client = new SenzorSoba.SenzorSobaClient(channel);
            var reply = await client.DeletePodaciAsync(new SenzorID { IdSenzora = id });

            return reply;
        }
    }
}
