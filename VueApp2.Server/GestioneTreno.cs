namespace VueApp2.Server
{
    using MySql.Data.MySqlClient;
    using Newtonsoft;
    static public class GestioneTreno
    {
        static string connectionString = "Server=localhost;Database=LudoTrain;Uid=ludotrain;Pwd=admin123;";
        static public Treno PrintTrain(string trainCode, string stationCode, long timestamp)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = string.Format("http://www.viaggiatreno.it/infomobilita/resteasy/viaggiatreno/andamentoTreno/{1}/{0}/{2}", stationCode, trainCode, timestamp.ToString());

                var task = Task.Run(() => client.GetAsync(url));
                task.Wait();
                HttpResponseMessage response = task.Result;
                if (response.IsSuccessStatusCode)
                {
                    var task2 = Task.Run(() => response.Content.ReadAsStringAsync());
                    task2.Wait();
                    string json = task2.Result;
                    Treno trainData;
                    // Deserializza il JSON in un oggetto TrainData
                    trainData = Newtonsoft.Json.JsonConvert.DeserializeObject<Treno>(json);
                    Newtonsoft.Json.JsonSerializer writer = new Newtonsoft.Json.JsonSerializer();
                    // Visualizza i dati del treno
                    return trainData;


                }
                else
                {
                    Console.WriteLine("Errore nella richiesta: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Si è verificato un errore: " + ex.Message);
            }
            return null;
        }

        static public Treno RicercaTreno(string codiceTreno)
        {
            string url = string.Format("http://www.viaggiatreno.it/infomobilita/resteasy/viaggiatreno/cercaNumeroTrenoTrenoAutocomplete/{0}", codiceTreno);
            using (HttpClient client1 = new HttpClient())
            {
                var task = Task.Run(() => client1.GetAsync(url));
                task.Wait();
                HttpResponseMessage response = task.Result;

                if (response.IsSuccessStatusCode)
                {
                    var task2 = Task.Run(() => response.Content.ReadAsStringAsync());
                    task2.Wait();
                    string trainData = task2.Result;
                    try
                    {
                        string traindInfo = trainData.Split('|')[1];
                        return PrintTrain(traindInfo.Split('-')[1].Trim(), traindInfo.Split('-')[0].Trim(), long.Parse(traindInfo.Split('-')[2].Trim()));
                    }
                    catch { }

                }
                else
                {
                    Console.WriteLine("Errore nella richiesta: " + response.StatusCode);
                }
            }
            return null;
        }

        static public Treno[] TreniStazione(string codiceStazione)
        {
            string url = string.Format("http://www.viaggiatreno.it/infomobilita/resteasy/viaggiatreno/cercaNumeroTrenoTrenoAutocomplete/{0}", codiceStazione);
            using (HttpClient client1 = new HttpClient())
            {
                var task = Task.Run(() => client1.GetAsync(url));
                task.Wait();
                HttpResponseMessage response = task.Result;

                if (response.IsSuccessStatusCode)
                {
                    var task2 = Task.Run(() => response.Content.ReadAsStringAsync());
                    task2.Wait();
                    string trainData = task2.Result;
                    try
                    {
                        string traindInfo = trainData.Split('|')[1];
                        return null;
                    }
                    catch { }

                }
                else
                {
                    Console.WriteLine("Errore nella richiesta: " + response.StatusCode);
                }
            }
            return null;
        }
        public static Stazione[] AutoCompletamentoStazione(string stazione)
        {
            List<Stazione> listaStazioni = new List<Stazione>();
            MySqlConnection mySql = new MySqlConnection(connectionString);
            mySql.Open();
            MySqlCommand mySqlCommand = mySql.CreateCommand();
            mySqlCommand.Connection = mySql;
            if(stazione!=string.Empty)
                mySqlCommand.CommandText = string.Format("SELECT * FROM Stazione WHERE nomeStazione LIKE \"{0}%\"", stazione);
            else
                mySqlCommand.CommandText = string.Format("SELECT * FROM Stazione");
            MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
            bool valido = dataReader.Read();
            while (valido)
            {
                listaStazioni.Add(new Stazione(dataReader.GetString("idStazione"), dataReader.GetString("nomeStazione")));
                valido = dataReader.Read();
            }

            return listaStazioni.ToArray();
        }
        
    }

    public class Stazione
    {
        public Stazione(string idStazione, string nomeStazione)
        {
            this.idStazione = idStazione;
            this.nomeStazione = nomeStazione;
        }
        public string idStazione;
        public string nomeStazione;
    }
}
