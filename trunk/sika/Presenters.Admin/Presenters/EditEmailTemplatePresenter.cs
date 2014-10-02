using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Admin.IViews;

namespace Presenters.Admin.Presenters
{
    public class EditEmailTemplatePresenter : Presenter<IEditEmailTemplateView>
    {
        private readonly ISfTBL_Admin_PlantillasManagementServices _plantilla;
        private readonly ISfTBL_Admin_PaisesManagementServices _paises;

        public EditEmailTemplatePresenter(ISfTBL_Admin_PlantillasManagementServices plantilla, ISfTBL_Admin_PaisesManagementServices paises)
        {
            _plantilla = plantilla;
            _paises = paises;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
            View.DeleteEvent += ViewDeleteEvent;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            GetPaises();
            Load();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(View.IdPlantilla))
                GuardarPlantilla();
            else
                ActualizarPlantilla();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            EliminarPlantilla();
        }



        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdPlantilla)) return;

            var plantilla = _plantilla.FindById(Convert.ToInt32(View.IdPlantilla));

            if (plantilla == null) return;

            if (plantilla.Contenido != null)
            {
                var memstrm = new MemoryStream(plantilla.Contenido);
                string sb;
                using (var memrdr = new StreamReader(memstrm))
                {
                    sb = memrdr.ReadToEnd();
                }
                DecodificarContenido(sb.TrimEnd());
            }
            
            View.NombrePlantilla = plantilla.Nombre;
            View.Activo = plantilla.IsActive;
            View.IdPais = plantilla.IdPais.ToString();
            View.CodeTemplate = plantilla.Codigo;
        }



        /// <summary>
        /// Inserta un nuevo registro en Base de datos.
        /// </summary>
        private void GuardarPlantilla()
        {

            try
            {

                var plantilla = _plantilla.NewEntity();
                plantilla.IdPais = Convert.ToInt32(View.IdPais);
                plantilla.Nombre = View.NombrePlantilla;
                plantilla.IsActive = View.Activo;
                plantilla.CreateOn = DateTime.Now;
                plantilla.CreateBy = View.UserSession.IdUser.ToString();
                plantilla.ModifiedOn = DateTime.Now;
                plantilla.ModifiedBy = View.UserSession.IdUser.ToString();
                plantilla.IdPais = Convert.ToInt32(View.IdPais);
                plantilla.Codigo = View.CodeTemplate;
                var uniEncoding = new UTF8Encoding();
                var encabezado = "[subject]\r\n";
                encabezado += View.Encabezado.Trim();
                encabezado += "\r\n[/subject]\r\n";

                encabezado += "[body]\r\n";
                encabezado += View.Cuerpo.Trim();
                encabezado += "\r\n[/body]\r\n";

                var byteEncabezado = uniEncoding.GetBytes(encabezado);

                using (var ms = new MemoryStream())
                {
                    ms.Write(byteEncabezado, 0, byteEncabezado.Length);
                    plantilla.Contenido = ms.GetBuffer();
                }
                _plantilla.Add(plantilla);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        /// <summary>
        /// Actualizar la plantilla en la base de datos.
        /// </summary>
        private void ActualizarPlantilla()
        {

            try
            {

                if (View.IdPlantilla == "") return;
                var plantilla = _plantilla.FindById(Convert.ToInt32(View.IdPlantilla));
                if (plantilla == null) return;

                plantilla.IdPais = Convert.ToInt32(View.IdPais);
                plantilla.Nombre = View.NombrePlantilla;
                plantilla.IsActive = View.Activo;
                plantilla.ModifiedOn = DateTime.Now;
                plantilla.ModifiedBy = View.UserSession.IdUser.ToString();
                plantilla.Codigo = View.CodeTemplate;
                var uniEncoding = new UTF8Encoding();
                var encabezado = "[subject]\r\n";
                encabezado += View.Encabezado;
                encabezado += "\r\n[/subject]\r\n";

                encabezado += "[body]\r\n";
                encabezado += View.Cuerpo;
                encabezado += "\r\n[/body]\r\n";
                var byteEncabezado = uniEncoding.GetBytes(encabezado);
                using (var ms = new MemoryStream())
                {
                    ms.Write(byteEncabezado, 0, byteEncabezado.Length);
                    plantilla.Contenido = ms.GetBuffer();
                }
                _plantilla.Modify(plantilla);

                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.EditError), TypeError.Error));
            }
        }

        /// <summary>
        /// Elimina la plantilla seleccionada en Base de Datos
        /// </summary>
        private void EliminarPlantilla()
        {
            try
            {
                if (View.IdPlantilla == "") return;
                var plantilla = _plantilla.FindById(Convert.ToInt32(View.IdPlantilla));
                if (plantilla == null) return;
                _plantilla.Remove(plantilla);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError), TypeError.Error));
            }
        }

        private void GetPaises()
        {
            try
            {
                var listado = _paises.FindBySpec(true);
                View.ListadoPaises(listado);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError," Listado de Paises"), TypeError.Error));
            }
        }

        /// <summary>
        /// Obtiene el contenido asociado al cuerpo y al encabezado por medio de expresiones regulares.
        /// </summary>
        /// <param name="emailTemplateContent"></param>
        private void DecodificarContenido(string emailTemplateContent)
        {
            var subjectRegex = new Regex(@"\[subject\]\r\n(.*)\r\n\[\/subject\]"
                , RegexOptions.Compiled | RegexOptions.Singleline);
            var bodyRegex = new Regex(@"\[body\]\r\n(.*)\r\n\[/body\]"
                , RegexOptions.Compiled | RegexOptions.Singleline);

            View.Encabezado = subjectRegex.Match(emailTemplateContent).Groups[1].Value;
            View.Cuerpo = bodyRegex.Match(emailTemplateContent).Groups[1].Value;

        }
    }
}