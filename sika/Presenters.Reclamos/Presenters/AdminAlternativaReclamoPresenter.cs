using System;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Domain.MainModule.Reclamos.DTO;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;
using System.Collections.Generic;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.Presenters
{
    public class AdminAlternativaReclamoPresenter : Presenter<IAdminAlternativaReclamoView>
    {
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_ModuloReclamos_AlternativasManagementServices _alternativaReclamoService;
        readonly ISfTBL_ModuloReclamos_AnexosAlternativaManagementServices _anexosService;

        public AdminAlternativaReclamoPresenter(ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                                ISfTBL_ModuloReclamos_AlternativasManagementServices alternativaReclamoService,
                                                ISfTBL_ModuloReclamos_AnexosAlternativaManagementServices anexosService)
        {
            _reclamoService = reclamoService;
            _alternativaReclamoService = alternativaReclamoService;
            _anexosService = anexosService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;            
            LoadInitData();
        }

        void CheckUserAction()
        {

        }

        public void LoadInitData()
        {
            LoadAlternativaReclamo();
        }

        public void LoadAlternativaReclamo()
        {
            if (string.IsNullOrEmpty(View.IdAlternativa)) return;

            try
            {
                var model = _alternativaReclamoService.GetById(Convert.ToDecimal(View.IdAlternativa));

                if (model != null)
                {
                    View.Causas = model.Causas;
                    View.Factores = model.Factores;
                    View.Alternativa = model.Alternativa;
                    View.Responsable = model.TBL_Admin_Usuarios2.Nombres;
                    View.FechaAlternativa = model.FechaAlternativa;
                    View.Seguimiento = model.Seguimiento;
                    View.Estado = model.Estado;

                    View.EnableEdit(false);
                    View.CanRegister = View.UserSession.IdUser == model.IdResponsable && model.Estado == "Asignada";

                    LoadReclamo(model.IdReclamo);
                    LoadArhchivosAdjuntos();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadReclamo(decimal idReclamo)
        {
            try
            {
                var reclamo = _reclamoService.GetReclamoWithNavById(idReclamo);

                if (reclamo != null)
                {
                    var dtoProducto = (Dto_Producto)reclamo.DtoProducto;
                    var dtoCliente = (Dto_Cliente)reclamo.DtoCliente;

                    View.TipoReclamo = reclamo.TBL_ModuloReclamos_TipoReclamo.Nombre;
                    View.NumeroReclamo = reclamo.NumeroReclamo;
                    View.IdReclamo = string.Format("{0}", idReclamo);

                    if (View.TipoReclamo == "Producto")
                    {
                        View.TitleReclamo = dtoProducto.NombreProducto;
                        View.TitleReclamoFrom = dtoCliente.NombreCliente;
                        View.Unidad = reclamo.UnidadZona;
                        View.FechaReclamo = string.Format("{0:dd/MM/yyyy}", reclamo.CreateOn);
                        View.Asesor = reclamo.AsesoradoPor.Nombres;                        
                    }
                    else
                    {
                        View.TitleReclamo = reclamo.TBL_ModuloReclamos_CategoriasReclamo.Nombre;
                        if (dtoCliente != null && !string.IsNullOrEmpty(dtoCliente.NombreCliente))
                        {
                            View.TitleReclamoFrom = dtoCliente.NombreCliente;
                        }
                        else
                        {
                            View.TitleReclamoFrom = string.Format("{0} / {1}", reclamo.NombreReclama, reclamo.ProcedimientoInternoAfectado);
                        }
                        View.Unidad = reclamo.UnidadZona;
                        View.FechaReclamo = string.Format("{0:dd/MM/yyyy}", reclamo.CreateOn);
                        if (reclamo.AsesoradoPor != null)
                            View.Asesor = reclamo.AsesoradoPor.Nombres;
                    }
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void UpdateAlternativaReclamo()
        {
            if (string.IsNullOrEmpty(View.IdAlternativa)) return;

            try
            {
                var model = _alternativaReclamoService.GetById(Convert.ToDecimal(View.IdAlternativa));

                if (model != null)
                {
                    model.Causas = View.Causas;
                    model.Factores = View.Factores;
                    model.Alternativa = View.Alternativa;
                    model.Seguimiento = View.Seguimiento;
                    //model.Estado = View.Estado;
                    model.ModifiedBy = View.UserSession.IdUser;
                    model.ModifiedOn = DateTime.Now;

                    _alternativaReclamoService.Modify(model);

                    LoadAlternativaReclamo();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void MarcarRealizadaAlternativaReclamo()
        {
            if (string.IsNullOrEmpty(View.IdAlternativa)) return;

            try
            {
                var model = _alternativaReclamoService.GetById(Convert.ToDecimal(View.IdAlternativa));

                if (model != null)
                {
                    model.Estado = "Realizada";
                    model.ModifiedBy = View.UserSession.IdUser;
                    model.ModifiedOn = DateTime.Now;

                    _alternativaReclamoService.Modify(model);

                    LoadAlternativaReclamo();
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
                var archivo = new TBL_ModuloReclamos_AnexosAlternativa();
                archivo.IdAlternativa = Convert.ToDecimal(View.IdAlternativa);
                archivo.NombreArchivo = View.NombreArchivoAdjunto;
                archivo.Archivo = View.ArchivoAdjunto;
                archivo.CreateBy = View.UserSession.IdUser;
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
                    archivo.Id = string.Format("{0}", model.IdAnexoAlternativa);
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
            if (string.IsNullOrEmpty(View.IdAlternativa)) return;

            try
            {
                var anexos = _anexosService.GetByIdAlternativa(Convert.ToDecimal(View.IdAlternativa));

                if (anexos.Any())
                {

                    var archivosAdjuntos = new List<DTO_ValueKey>();
                    foreach (var anexo in anexos)
                    {
                        var archivo = new DTO_ValueKey();
                        archivo.Id = string.Format("{0}", anexo.IdAnexoAlternativa);
                        archivo.Value = anexo.NombreArchivo;
                        archivo.ComplexValue = anexo.Archivo;

                        archivosAdjuntos.Add(archivo);
                    }

                    View.LoadArchivosAdjuntos(archivosAdjuntos);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}