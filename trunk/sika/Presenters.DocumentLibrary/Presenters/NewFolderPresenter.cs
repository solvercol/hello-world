using System;
using System.Reflection;
using Applcations.MainModule.DocumentLibrary.IServices;
using Application.Core;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.DocumentLibrary.IViews;

namespace Presenters.DocumentLibrary.Presenters
{
    public class NewFolderPresenter : Presenter<INewFolderView>
    {
        private readonly ISfTBL_ModuloDocumentosAnexos_CategoriasManagementServices _categoriServicex;
        private readonly ISfTBL_ModuloDocumentosAnexos_CarpetasManagementServices _carpetasServices;

        public NewFolderPresenter(
            ISfTBL_ModuloDocumentosAnexos_CategoriasManagementServices categoriServicex,
            ISfTBL_ModuloDocumentosAnexos_CarpetasManagementServices carpetasServices)
        {
            _categoriServicex = categoriServicex;
            _carpetasServices = carpetasServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            Save();
        }

        void ViewLoad(object sender, EventArgs e)
        {
            Loadcategories();
        }

        private void Loadcategories()
        {
            try
            {
                var list = _categoriServicex.FindBySpec(true);
                View.Listadocategorias(list);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Listado Categorias"), TypeError.Error));
            }
        }

        private void Save()
        {
            try
            {
                _carpetasServices.SaveFolder(View.IdParent, View.Idcategoria, View.NombreFolder, View.IdContrato,
                                             View.UserSession.IdUser.ToString());
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}