using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoGasolineras
{
    public class CustomContractResolver : DefaultContractResolver
    {
        private Dictionary<string, string> PropertyMappings { get; set; }

        public CustomContractResolver()
        {
            this.PropertyMappings = new Dictionary<string, string>
        {
            {"CP", "C.P."},
            {"Direccion", "Dirección"},
            {"Longitud_x0020__x0028_WGS84_x0029_", "Longitud (WGS84)"},
            {"Remision", "Remisión"},
            {"Rotulo", "Rótulo"},
            {"BioEtanol", "% BioEtanol"},
            {"Estermetilico", "% Éster metílico"},
            {"Precio_x0020_Gasoleo_x0020_A", "Precio Gasoleo A"},

        };
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            string resolvedName = null;
            var resolved = this.PropertyMappings.TryGetValue(propertyName, out resolvedName);
            return (resolved) ? resolvedName : base.ResolvePropertyName(propertyName);
        }
    }
}
