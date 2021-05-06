using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using System.Net;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

namespace ProyectoGasolineras
{
    /// <summary>
    /// Lógica de interacción para info.xaml
    /// </summary>
    public partial class info : Window
    {
        
        
        List<Gasolinera> gasolineras = new List<Gasolinera>();
        public info(string id, string idMunicipio)
        {
            InitializeComponent();
            

            gasolineras = obtenerGasolineras(idMunicipio);
            gasolineras = gasolineras.Where(gas => gas.IDEESS.Contains(id)).ToList();
            Gasolinera gasolinera = new Gasolinera();
            gasolinera = gasolineras[0];
            mostrarDatos(gasolinera);
        }


        private List<Gasolinera> obtenerGasolineras(string id)
        {
            Gasolinera gasolinera = new Gasolinera();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://sedeaplicaciones.minetur.gob.es/ServiciosRESTCarburantes/PreciosCarburantes/EstacionesTerrestres/FiltroMunicipio/" + id);
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            // Set credentials to use for this request.
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Console.WriteLine("Content length is {0}", response.ContentLength);
            Console.WriteLine("Content type is {0}", response.ContentType);

            // Get the stream associated with the response.
            Stream receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format.
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

            var salida = readStream.ReadToEnd();

            JsonDocument gas = JsonDocument.Parse(salida);
            JsonElement estaciones = gas.RootElement.GetProperty("ListaEESSPrecio");
            var salidaText = estaciones.GetRawText();

            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CustomContractResolver();

            List<Gasolinera> gasolineras = null;
            gasolineras = JsonConvert.DeserializeObject<List<Gasolinera>>(salidaText, settings);



            return gasolineras;
        }

        private void mostrarDatos(Gasolinera gasolinera)
        {
            txtGasoleo.Content = gasolinera.Precio_x0020_Gasoleo_x0020_A + " €";
            txtCP.Content = gasolinera.CP;
            txtDireccion.Content = gasolinera.Dirección;
            txtHorario.Content = gasolinera.Horario;
        }

        
    }
}
