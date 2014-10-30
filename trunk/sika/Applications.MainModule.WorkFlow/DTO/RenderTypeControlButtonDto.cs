using System;
using System.Collections.Generic;
using Infrastructure.CrossCutting.IDtoService;

namespace Applications.MainModule.WorkFlow.DTO
{
    [Serializable]
    public class RenderTypeControlButtonDto : IDocumentDto
    {
        public string TextControl { get; set; }

        public string CurrentStatus { get; set; }

        public string IdCurrentStatus { get; set; }

        public string NextStatus { get; set; }

        public string IdNextStatus { get; set; }

        public string IdDocument { get; set; }

        public string NextResponsibe { get; set; }

        public string EmailNextResponsibe { get; set; }

        public string IdNextResponsibe { get; set; }

        public string CurrentResponsibe { get; set; }

        public string EmailCurrentResponsibe { get; set; }

        public string IdCurrentResponsibe { get; set; }

        public string OrdenCompra { get; set; }

        public string NumeroPedido { get; set; }

        public string Cliente { get; set; }

        public string TipoReclamo { get; set; }

        public string FormulaNextresponsible { get; set; }

        public string Comentarios { get; set; }

        private  List<string> _errores = new List<string>();

        private List<string> _output = new List<string>();

        private Dictionary<string, string> _params = new Dictionary<string, string>();

        public bool IsNextGroupResponsible { get; set; }

        public bool IsCurrentGroupResponsible { get; set; }

        /// <summary>
        /// Propiedad que permite identificar si el WF se ejecutó de forma apropiada o presento algun error tanto en la validación como en la ejecución
        /// de alguno de sus métodos.
        /// </summary>
        public String Processestaus { get; set; }
        /// <summary>
        /// Listado que permite obtener los mensajes asociados a los errores como producto de la ejecición del WF
        /// </summary>
        public List<string> MessagesError
        {
            get { return _errores; } 
            set { _errores = value; }
        }

        /// <summary>
        /// Lista que permite enviar a la lista los nombres de las Ventanas que permiten capturar parámetros para que pueda seguir la ejecución del WF
        /// </summary>
        public List<string> OutputParameters { get { return _output; } set { _output = value; } }

        /// <summary>
        /// Diccionario que permite enviar al servicios lo capturado por la ventana "InputParameters" etiquetado con el Key y su valor respectivo
        /// </summary>
        /// 
        public Dictionary<string, string> Parameters { get { return _params; } set { _params = value; } }
    }

}