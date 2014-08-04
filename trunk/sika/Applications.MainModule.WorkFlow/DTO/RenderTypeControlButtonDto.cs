using System;
using System.Collections.Generic;
using Domain.MainModule.WorkFlow.Enums;

namespace Applications.MainModule.WorkFlow.DTO
{
    [Serializable]
    public class RenderTypeControlButtonDto
    {
        public string TextControl { get; set; }

        public string CurrentStatus { get; set; }

        public string IdCurrentStatus { get; set; }

        public string NextStatus { get; set; }

        public string IdNextStatus { get; set; }

        public string IdDocument { get; set; }

        public string CurrentResponsibe { get; set; }

        public string EmailCurrentResponsibe { get; set; }

        public string IdCurrentResponsibe { get; set; }
        
        /// <summary>
        /// Propiedad que permite identificar si el WF se ejecut� de forma apropiada o presento algun error tanto en la validaci�n como en la ejecuci�n
        /// de alguno de sus m�todos.
        /// </summary>
        public ProcessStatus ProcessStatus { get; set; }

        /// <summary>
        /// Listado que permite obtener los mensajes asociados a los errores como producto de la ejecici�n del WF
        /// </summary>
        public List<string> MessagesError { get; set; }

        /// <summary>
        /// Lista que permite enviar a la lista los nombres de las Ventanas que permiten capturar par�metros para que pueda seguir la ejecuci�n del WF
        /// </summary>
        public List<string> OutputParameters { get; set; }

        /// <summary>
        /// Diccionario que permite enviar al servicios lo capturado por la ventana "InputParameters" etiquetado con el Key y su valor respectivo
        /// </summary>
        public readonly Dictionary<string ,string > Parameters = new Dictionary<string, string>();
    }

}