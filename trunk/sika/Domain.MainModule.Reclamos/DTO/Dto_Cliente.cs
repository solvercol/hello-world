using System.Runtime.Serialization;
using System;

namespace Domain.MainModule.Reclamos.DTO
{
    [Serializable]
    public class Dto_Cliente : ISerializable
    {
        public Dto_Cliente()
        {
        }

        public string NombreCliente { get { return string.Format("{0} - {1}", CodigoCliente, Cliente); } }

        public string CodigoCliente { get; set; }
        public string Cliente { get; set; }
        public string Contacto { get; set; }
        public string Email { get; set; }
        public string Unidad { get; set; }
        public string Zona { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("CodigoCliente", CodigoCliente, typeof(string));
            info.AddValue("Cliente", Cliente, typeof(string));
            info.AddValue("Contacto", Contacto, typeof(string));
            info.AddValue("Email", Email, typeof(string));
            info.AddValue("Unidad", Unidad, typeof(string));
            info.AddValue("Zona", Zona, typeof(string));
        }
    }
}