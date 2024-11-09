namespace Smartwyre.Domain.Entities.Model.Authentication
{
    using Smartwyre.Domain.Entities.Model.Transversal;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
        
    [Table("perezgomez.usuarios")]
    [Serializable]
    public class User: BaseEntity
    {
        public string nombre { get; set; }

        public string usuario { get; set; }

        public string contrasena { get; set; }

        public string activo { get; set; }

        public int? rol { get; set; }

        public int? idinstitucion { get; set; }

        public string usuariowin { get; set; }

        public string correoelectronico { get; set; }

        public string contrasenacorreo { get; set; }

        public int? vuv { get; set; }

        public int? primeravez { get; set; }


    }
}
