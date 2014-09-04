using System;
using System.Reflection;
using Applcations.MainModule.DocumentLibrary.IServices;
using Application.Core;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.DocumentLibrary.IViews;

namespace Presenters.DocumentLibrary.Presenters
{
    public class LoadFilePresenter : Presenter<ILoadFileView>
    {
        private readonly ISfTBL_ModuloDocumentosAnexos_DocumentoManagementServices _docServices;

        public LoadFilePresenter(ISfTBL_ModuloDocumentosAnexos_DocumentoManagementServices docServices)
        {
            _docServices = docServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
            View.SaveEvent += ViewSaveEvent;
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            SaveDocument();
        }

        void View_Load(object sender, EventArgs e)
        {

        }

        private void SaveDocument()
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdFolder)) return;
                if (View.Attachments.Length == 0) return;

                _docServices.SaveDocument(Convert.ToInt32(View.IdFolder), View.UserSession,
                                          View.NameFile, View.Comentarios, View.Attachments, View.ContentTypeFile);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

    }
}