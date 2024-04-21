namespace RESTServer
{
    public class SenzorPodaci
    {
        private string vreme;
        private string idSenzora;
        private float co;
        private float humidity;
        private bool light;
        private bool motion;
        private float smoke;
        private float temp;

        public SenzorPodaci(string vreme, string idSenzora, float co, float humidity, bool light, bool motion, float smoke, float temp)
        {
            this.vreme = vreme;
            this.idSenzora = idSenzora;
            this.co = co;
            this.humidity = humidity;
            this.light = light;
            this.motion = motion;
            this.smoke = smoke;
            this.temp = temp;
        }

        public string Vreme { get => vreme; set => vreme = value; }
        public string IdSenzora { get => idSenzora; set => idSenzora = value; }
        public float Co { get => co; set => co = value; }
        public float Humidity { get => humidity; set => humidity = value; }
            
        public bool Light { get => light; set => light = value; }
            
        public bool Motion { get => motion; set => motion = value; }

        public float Smoke { get => smoke; set => smoke = value; }
        public float Temp { get => temp; set => temp = value; }
        
    }
}
