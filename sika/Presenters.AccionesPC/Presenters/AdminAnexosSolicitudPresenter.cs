using System;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.AccionesPC.IViews;
using System.Collections.Generic;
using Domain.MainModules.Entities;

namespace Presenters.AccionesPC.Presenters
{
    public class AdminAnexosSolicitudPresenter : Presenter<IAdminAnexosSolicitudView>
    {
        readonly ISfTBL_ModuloAPC_SolicitudManagementServices _solicitudService;
        readonly ISfTBL_ModuloAPC_AnexosSolicitudManagementServices _anexosService;

        public AdminAnexosSolicitudPresenter(ISfTBL_ModuloAPC_SolicitudManagementServices solicitudService, ISfTBL_ModuloAPC_AnexosSolicitudManagementServices anexosService)
        {
            _solicitudService = solicitudService;
            _anexosService = anexosService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.FilterEvent += ViewFilterEvent;
        }

        void ViewFilterEvent(object sender, EventArgs e)
        {
            LoadInitData();
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadArhchivosAdjuntos();
        }

        public void LoadInitData()
        {
            LoadSolicitud();
            LoadArhchivosAdjuntos();
        }

        void LoadSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;

            try
            {
                var solicitud = _solicitudService.GetById(Convert.ToDecimal(View.IdSolicitud));

                if (solicitud != null)
                {
                    View.CanAddAnexos = ((solicitud.IdEstado == 14) && solicitud.IdResponsableActual == View.UserSession.IdUser);

                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddArchivoAdjunto()
        {
            try
            {
                var archivo = new TBL_ModuloAPC_AnexosSolicitud();
                archivo.IdSolicitudAPC = Convert.ToDecimal(View.IdSolicitud);
                archivo.NombreArchivo = View.NombreArchivoAdjunto;
                archivo.Archivo = View.ArchivoAdjunto;
                archivo.CreateBy = View.UserSession.IdUser;
                archivo.IsActive = true;
                archivo.CreateOn = DateTime.Now;
                archivo.ModifiedBy = View.UserSession.IdUser;
                archivo.ModifiedOn = DateTime.Now;

                _anexosService.Add(archivo);

                LoadArhchivosAdjuntos();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void RemoveArchivoAdjunto(decimal idArchivo)
        {
            try
            {
                var model = _anexosService.GetById(idArchivo);

                if (model != null)
                {
                    _anexosService.Remove(model);

                    LoadArhchivosAdjuntos();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void DownloadArchivoAdjunto(decimal idArchivo)
        {
            try
            {
                var model = _anexosService.GetById(idArchivo);

                if (model != null)
                {
                    var archivo = new DTO_ValueKey();
                    archivo.ComplexValue = model.Archivo;
                    archivo.Id = string.Format("{0}", model.IdAnexoSolicitud);
                    archivo.Value = model.NombreArchivo;

                    View.DescargarArchivo(archivo);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadArhchivosAdjuntos()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;

            try
            {
                var anexos = _anexosService.GetByIdSolicitud(Convert.ToDecimal(View.IdSolicitud));
                var archivosAdjuntos = new List<DTO_ValueKey>();
                if (anexos.Any())
                {
                    foreach (var anexo in anexos)
                    {
                        var archivo = new DTO_ValueKey();
                        archivo.Id = string.Format("{0}", anexo.IdAnexoSolicitud);
                        archivo.Value = anexo.NombreArchivo;
                        archivo.ComplexValue = anexo.Archivo;

                        archivosAdjuntos.Add(archivo);
                    }
                }
                View.LoadArchivosAdjuntos(archivosAdjuntos);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}