using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ProyectoGasolineras
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        List<Gasolinera> gasolineras = new List<Gasolinera>();
        bool muniSelected = false;

        public MainWindow()
        {
            InitializeComponent();

            //Estan son las coordenadas de Madrid, para que se centre el mapa mas o menos en la peninsula
            double lat = 40.4167;
            double lon = -3.70325;
            miMapa.ZoomLevel = 6;
            miMapa.Mode = new AerialMode();

            Location pos = new Location(lat, lon);
            miMapa.Center = pos;
            cargarComunidades();
            cargarProvincias();
            cargarMunicipios();
        }

        private void cargarComunidades()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://sedeaplicaciones.minetur.gob.es/ServiciosRESTCarburantes/PreciosCarburantes/Listados/ComunidadesAutonomas/");
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

            List<Comunidad> comunidades = new List<Comunidad>();
            comunidades = System.Text.Json.JsonSerializer.Deserialize<List<Comunidad>>(salida);
            
            foreach(Comunidad com in comunidades)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = com.CCAA;
                item.Tag = com.IDCCAA;

                cbComunidad.Items.Add(item);
            }
            

        }

        private void cargarProvincias()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://sedeaplicaciones.minetur.gob.es/ServiciosRESTCarburantes/PreciosCarburantes/Listados/Provincias/");
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

            List<Provincias> provincias = new List<Provincias>();
            provincias = System.Text.Json.JsonSerializer.Deserialize<List<Provincias>>(salida);

            foreach (Provincias prov in provincias)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = prov.Provincia;
                item.Tag = prov.IDPovincia;

                cbProvincia.Items.Add(item);
            }
        }

        private void cargarMunicipios()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://sedeaplicaciones.minetur.gob.es/ServiciosRESTCarburantes/PreciosCarburantes/Listados/Municipios/");
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

            List<Municipios> municipios = new List<Municipios>();
            municipios = System.Text.Json.JsonSerializer.Deserialize<List<Municipios>>(salida);

            foreach (Municipios muni in municipios)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = muni.Municipio;
                item.Tag = muni.IDMunicipio;

                cbMunicipio.Items.Add(item);
            }
        }


        private void comunidad_changed(object sender, SelectionChangedEventArgs e)
        {
            //Limpio los demas combobox y pongo un mensaje en el de municipio
            cbMunicipio.Items.Clear();
            cbProvincia.Items.Clear();

            //Reseteo las opciones de mostrar las gasolineras
            chVisualizarTodas.IsChecked = true;
            rbBarato.IsEnabled = false;
            rbCaro.IsEnabled = false;
            muniSelected = false;

            ComboBoxItem item = new ComboBoxItem();

            item.Content = "No hay provincia seleccionada";
            cbMunicipio.Items.Add(item);

            var idComunidad = ((ComboBoxItem)cbComunidad.SelectedItem).Tag.ToString();

            cargarProvinciasID(idComunidad);

        }

        private void cargarProvinciasID(string idComunidad)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://sedeaplicaciones.minetur.gob.es/ServiciosRESTCarburantes/PreciosCarburantes/Listados/ProvinciasPorComunidad/"+idComunidad);
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

            List<Provincias> provincias = new List<Provincias>();
            provincias = System.Text.Json.JsonSerializer.Deserialize<List<Provincias>>(salida);

            foreach (Provincias prov in provincias)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = prov.Provincia;
                item.Tag = prov.IDPovincia;

                cbProvincia.Items.Add(item);
            }
        }

        private void provincia_changed(object sender, SelectionChangedEventArgs e)
        {
            if(cbProvincia.Items.Count > 0)
            {
                cbMunicipio.Items.Clear();
                chVisualizarTodas.IsChecked = true;
                rbBarato.IsEnabled = false;
                rbCaro.IsEnabled = false;
                muniSelected = false;

                var idProvincia = ((ComboBoxItem)cbProvincia.SelectedItem).Tag.ToString();
                cargarMunicipiosID(idProvincia);
            }
            
        }

        private void cargarMunicipiosID(string idProvincia)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://sedeaplicaciones.minetur.gob.es/ServiciosRESTCarburantes/PreciosCarburantes/Listados/MunicipiosPorProvincia/"+idProvincia);
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

            List<Municipios> municipios = new List<Municipios>();
            municipios = System.Text.Json.JsonSerializer.Deserialize<List<Municipios>>(salida);

            foreach (Municipios muni in municipios)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = muni.Municipio;
                item.Tag = muni.IDMunicipio;

                cbMunicipio.Items.Add(item);
            }
        }

        private void municipio_changed(object sender, SelectionChangedEventArgs e)
        {
            if(cbMunicipio.Items.Count > 0)
            {
                chVisualizarTodas.IsChecked = true;
                rbBarato.IsEnabled = false;
                rbCaro.IsEnabled = false;


                var idMunicipio = ((ComboBoxItem)cbMunicipio.SelectedItem).Tag.ToString();
                muniSelected = true;

                gasolineras = obtenerGasolineras(idMunicipio);

                miMapa.Children.Clear();//Quita los anterios pushpins(si los hay)
                foreach (Gasolinera gasolinera in gasolineras)
                {
                    double lat = Convert.ToDouble(gasolinera.Latitud);
                    double lon = Convert.ToDouble(gasolinera.Longitud_x0020__x0028_WGS84_x0029_);

                    cargarGasolinera(lat, lon, gasolinera.IDEESS);

                }
            }
            
            
        }

        private List<Gasolinera> obtenerGasolineras(string id)
        {
            Gasolinera gasolinera = new Gasolinera();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://sedeaplicaciones.minetur.gob.es/ServiciosRESTCarburantes/PreciosCarburantes/EstacionesTerrestres/FiltroMunicipio/"+id);
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

        private void cargarGasolinera(double latitud, double longitud, string id)
        {

            
            var pin = new Pushpin();
            pin.Location = new Location(latitud, longitud);
            
            pin.Tag = id;
            pin.MouseDown += new MouseButtonEventHandler(pin_MouseDoubleClick);
            miMapa.Center = pin.Location;
            miMapa.ZoomLevel = 12;
            miMapa.Children.Add(pin);

            
        }

        private void pin_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Muestra la información
            //miMapa.Mode = new AerialMode();
            

            Pushpin p = sender as Pushpin;
            //MessageBox.Show(p.Tag.ToString());
            string idGas = p.Tag.ToString();

            string idMuni = string.Empty;
            //string idMuni = gasolineras.Where(gas => gas.IDEESS.Equals(idGas)).ToString();
            /*
            var gasolinera = from gasolin in gasolineras
                             where (gasolin.IDEESS.Contains(p.Tag.ToString()))
                             select gasolin;

            gasolineras = gasolinera.ToList();*/

            foreach(Gasolinera gas in gasolineras)
            {
                if(gas.IDEESS.Equals(p.Tag.ToString()))
                {
                    idMuni = gas.IDMunicipio;
                }
            }





            info popup = new info(p.Tag.ToString(), idMuni);
            popup.ShowDialog();
            
        }

       

        private void chVisualizarTodos_Click(object sender, RoutedEventArgs e)
        {
           
            if(chVisualizarTodas.IsChecked == true)
            {
                rbBarato.IsEnabled = false;
                rbCaro.IsEnabled = false;
                cargarTodasGasolineras();

            }

            if(chVisualizarTodas.IsChecked == false)
            {
                rbBarato.IsEnabled = true;
                rbCaro.IsEnabled = true;
                miMapa.Children.Clear();
            }
        }

        private void cargarTodasGasolineras()
        {
            var idMunicipio = ((ComboBoxItem)cbMunicipio.SelectedItem).Tag.ToString();


            gasolineras = obtenerGasolineras(idMunicipio);

            miMapa.Children.Clear();//Quita los anterios pushpins(si los hay)
            foreach (Gasolinera gasolinera in gasolineras)
            {
                double lat = Convert.ToDouble(gasolinera.Latitud);
                double lon = Convert.ToDouble(gasolinera.Longitud_x0020__x0028_WGS84_x0029_);

                cargarGasolinera(lat, lon, gasolinera.IDEESS);

            }
        }

        private void rbBarato_Click(object sender, RoutedEventArgs e)
        {
            if(rbBarato.IsChecked == true)
            {
                if(cbMunicipio.Items.Count > 0 && muniSelected == true)
                {
                    
                    
                        miMapa.Children.Clear();
                        var idMunicipio = ((ComboBoxItem)cbMunicipio.SelectedItem).Tag.ToString();

                        List<Gasolinera> gasolineras = obtenerGasolineras(idMunicipio);
                        gasolineras = gasolineras.OrderBy(gas => gas.Precio_x0020_Gasoleo_x0020_A).ToList();
                        Gasolinera gasolinera = gasolineras[0];

                        double lat = Convert.ToDouble(gasolinera.Latitud);
                        double lon = Convert.ToDouble(gasolinera.Longitud_x0020__x0028_WGS84_x0029_);

                        cargarGasolinera(lat, lon, gasolinera.IDEESS);
                   
                }
                
            }
        }

        private void rbCaro_Click(object sender, RoutedEventArgs e)
        {
            if(rbCaro.IsChecked == true)
            {
                if(cbMunicipio.Items.Count > 0 && muniSelected == true)
                {
                        
                        miMapa.Children.Clear();
                        var idMunicipio = ((ComboBoxItem)cbMunicipio.SelectedItem).Tag.ToString();

                        List<Gasolinera> gasolineras = obtenerGasolineras(idMunicipio);
                        gasolineras = gasolineras.OrderByDescending(gas => gas.Precio_x0020_Gasoleo_x0020_A).ToList();
                        Gasolinera gasolinera = gasolineras[0];

                        double lat = Convert.ToDouble(gasolinera.Latitud);
                        double lon = Convert.ToDouble(gasolinera.Longitud_x0020__x0028_WGS84_x0029_);

                        cargarGasolinera(lat, lon, gasolinera.IDEESS);

                    
                    
                    
                }
                
            }
        }
    }
}
