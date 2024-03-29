namespace VueApp2.Server
{
    public class Fermata
    {
        public string Id { get; set; }
        private string stazione;
        public string Stazione { get { return stazione; } set { if (value == null) stazione = string.Empty; else stazione = value; } }
        private string provvedimenti;
        public string Provvedimenti { get { return provvedimenti; } set { if (value == null) provvedimenti = string.Empty; else provvedimenti = value; } }
        public long Programmata { get; set; }
        public int Ritardo { get; set; }
        private DateTime partenza;
        public string? Partenza_Teorica 
        {
            get
            {
                return partenza.ToShortTimeString();
            }
            set
            {
                if(value!=null)
                {
                    partenza = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(double.Parse(value));
                }
            }
        }
        private DateTime arrivo;
        public string? Arrivo_Teorico
        {
            get
            {
                return arrivo.ToShortTimeString();
            }
            set
            {
                if (value != null)
                {
                    arrivo = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(double.Parse(value));
                }
            }
        }
        public object PartenzaReale { get; set; }
        public object ArrivoReale { get; set; }
        public int RitardoPartenza { get; set; }
        public int RitardoArrivo { get; set; }
    }
    
    public class Treno
    {
        public string NumeroTreno { get; set; }
        public string OrigineZero { get; set; }
        public string DestinazioneZero { get; set; }
        public List<Fermata> Fermate { get; set; }

        public DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
        public string DisplayTrainData()
        {
            string s = ($"\nNumero Treno: {NumeroTreno} \nParte: {OrigineZero}\nArriva: {DestinazioneZero}\n");
            try
            {
                s += $"Orario: {UnixTimeStampToDateTime(Fermate[0].Programmata / 1000)}";
            }
            catch { }

            return s;
            /*foreach (Fermata f in Fermate)
            {
                try
                {
                    Console.WriteLine($"Stazione:{f.Stazione}");
                    Console.WriteLine($"Ritardo Arrivo: {f.RitardoArrivo}");
                    Console.WriteLine($"Ritardo Partenza: {f.RitardoPartenza}");
                    System.DateTime dat_Time = UnixTimeStampToDateTime((long)f.Arrivo_Teorico / 1000);
                    string print_the_Date = dat_Time.ToShortDateString() + " " + dat_Time.ToShortTimeString();
                    Console.WriteLine($"ArrivoTeorico: {print_the_Date}");

                    dat_Time = UnixTimeStampToDateTime((long)f.ArrivoReale / 1000);
                    print_the_Date = dat_Time.ToShortDateString() + " " + dat_Time.ToShortTimeString();
                    Console.WriteLine($"ArrivoReale: {print_the_Date}");



                    dat_Time = UnixTimeStampToDateTime((long)f.Partenza_Teorica / 1000);
                    print_the_Date = dat_Time.ToShortDateString() + " " + dat_Time.ToShortTimeString();
                    Console.WriteLine($"Partenza Teoria: {print_the_Date}");

                    dat_Time = UnixTimeStampToDateTime((long)f.PartenzaReale / 1000);
                    print_the_Date = dat_Time.ToShortDateString() + " " + dat_Time.ToShortTimeString();
                    Console.WriteLine($"PartenzaReale: {print_the_Date}");

                    
                    

                }
                catch (Exception ex) { Debug.WriteLine(ex); }
                Console.WriteLine("\n");
            }
            */
            // Aggiungi qui altri campi che vuoi visualizzare
        }
    }
}
