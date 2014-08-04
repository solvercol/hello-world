﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Application.Core;
using Application.MainModule.Documentos.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Documentos.IViews;

namespace Presenters.Documentos.Presenters
{
    public class VerDocumentoPresenter
        : Presenter<IVerDocumentoView>
    {

        private readonly ISfTBL_ModuloDocumentos_DocumentoManagementServices documentoServices;
        private readonly ISfTBL_ModuloDocumentos_CategoriasManagementServices categoriasServices;
        private readonly ISfTBL_Admin_UsuariosManagementServices usuarioServices;

        public VerDocumentoPresenter
            (
                ISfTBL_ModuloDocumentos_DocumentoManagementServices documentoManagementServices
                ,ISfTBL_ModuloDocumentos_CategoriasManagementServices categoriasManagementServices
                ,ISfTBL_Admin_UsuariosManagementServices usuarioServices
            )
        {
            this.documentoServices = documentoManagementServices;
            this.categoriasServices = categoriasManagementServices;
            this.usuarioServices = usuarioServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.DescargarArchivoEvent += ViewDescargarArchivoEvent;

        }

        void ViewDescargarArchivoEvent(object sender, EventArgs e)
        {
            var documento = documentoServices.FindById(View.IdDocumento);
            View.DescargarArchivo(documento);
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if(View.IsPostBack)return;
            CargarObjeto();
        }

        private void CargarObjeto()
        {
            try
            {
                if(View.IdDocumento == 0)
                    return;

                var oDocumento = documentoServices.FindById(Convert.ToInt32(View.IdDocumento));
                
                if (oDocumento == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Documento"), TypeError.Error));
                    return;
                }

                View.Titulo = oDocumento.Titulo;
                View.Version = oDocumento.Version;

                View.Observaciones = oDocumento.Observaciones;

                View.Archivo = oDocumento.Archivo;

                if (oDocumento.TBL_ModuloDocumentos_Categorias == null)
                    oDocumento.TBL_ModuloDocumentos_Categorias = categoriasServices.FindById(oDocumento.IdCategoria);
                View.Categoria = oDocumento.TBL_ModuloDocumentos_Categorias.Nombre;
                if (oDocumento.TBL_ModuloDocumentos_Categorias1 == null)
                    oDocumento.TBL_ModuloDocumentos_Categorias1 = categoriasServices.FindById(oDocumento.IdSubCategoria);
                View.SubCategoria = oDocumento.TBL_ModuloDocumentos_Categorias1.Nombre;
                if (oDocumento.TBL_ModuloDocumentos_Categorias2 == null)
                    oDocumento.TBL_ModuloDocumentos_Categorias2 = categoriasServices.FindById(oDocumento.IdTipo);
                View.TipoDocumento = oDocumento.TBL_ModuloDocumentos_Categorias2.Nombre;

                View.UsuarioResponsable = usuarioServices.FindById(oDocumento.IdUsuarioResponsable).Nombres;
                View.Activo = oDocumento.IsActive;
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Documento"), TypeError.Error));
            }
        }

    }
}
