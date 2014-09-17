using System.Runtime.Serialization;
using System;

namespace Domain.MainModule.Reclamos.DTO
{
    [Serializable]
    public class Dto_Producto : ISerializable
    {
        public Dto_Producto()
        {
        }

        public string NombreProducto { get { return string.Format("{0} - {1}", CodigoProducto, Producto); } }

        public string CodigoProducto { get; set; }
        public string Producto { get; set; }
        public string Unidad { get; set; }
        public decimal PesoNeto { get; set; }
        public decimal PrecioLista { get; set; }
        public string GrupoCompradores { get; set; }
        public string CampoApl { get; set; }
        public string Categoria { get; set; }
        public string SubCategoria { get; set; }
        public int? IdCategoriaProducto { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {            
            info.AddValue("CodigoProducto", CodigoProducto, typeof(string));
            info.AddValue("Producto", Producto, typeof(string));
            info.AddValue("Unidad", Unidad, typeof(string));
            info.AddValue("PesoNeto", PesoNeto, typeof(decimal));
            info.AddValue("PrecioLista", PrecioLista, typeof(decimal));
            info.AddValue("GrupoCompradores", GrupoCompradores, typeof(string));
            info.AddValue("CampoApl", CampoApl, typeof(string));
            info.AddValue("Categoria", Categoria, typeof(string));
            info.AddValue("SubCategoria", SubCategoria, typeof(string));
        }
    }
}