using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Application.MainModule.SqlServices.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.AccionesPC.IViews;
using Applications.MainModule.Admin.IServices;

namespace Presenters.AccionesPC.Presenters
{
    public class MisSolicitudesPorFechaPresenter : Presenter<IMisSolicitudesPorFechaView>
    {
        readonly ISfTBL_ModuloAPC_AreasManagementServices _areasService;
        readonly ISolicitudesAPCAdoService _solicitudesAdoService;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;

        public MisSolicitudesPorFechaPresenter(ISfTBL_ModuloAPC_AreasManagementServices areasService, ISolicitudesAPCAdoService solicitudesAdoService, ISfTBL_Admin_OptionListManagementServices optionListService)
        {
            _areasService = areasService;
            _solicitudesAdoService = solicitudesAdoService;
            _optionListService = optionListService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            InitViewValues();
            LoadInitDate();
            LoadAreasAccion();
            LoadView();
        }

        void InitViewValues()
        {
            View.FechaFilterTo = new DateTime(DateTime.Now.Year, 12, 31);
            View.FilterNoSolicitud = string.Empty;
        }

        void LoadInitDate()
        {
            try
            {
                var op = _optionListService.ObtenerOpcionBykeyModuleId("FechaInitVistas", Convert.ToInt32(View.IdModule));

                if (op != null)
                {
                    View.FechaFilterFrom = Convert.ToDateTime(op.Value);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadAreasAccion()
        {
            try
            {
                var items = _areasService.GetAreasConSolicitudes();

                if (items.Any())
                    items = items.OrderBy(x => x.Nombre).ToList();

                View.LoadAreaAcion(items);
                LoadProcesos();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadProcesos()
        {
            var procesos = new List<DTO_ValueKey>();

            procesos.Add(new DTO_ValueKey() { Id = "Seleccionar", Value = "" });

            try
            {
                if (View.FilterArea != 0)
                {
                    var area = _areasService.GetById(View.FilterArea);

                    if (area != null)
                    {
                        var spltProc = area.Procesos.Split('|');

                        foreach (var proc in spltProc)
                        {
                            procesos.Add(new DTO_ValueKey() { Id = proc, Value = proc });
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }

            View.LoadProcesos(procesos);
        }

        public void LoadView()
        {
            try
            {
                var dt = _solicitudesAdoService.GetVistaMisSolicitudes(View.FechaFilterFrom, View.FechaFilterTo, View.ServerHostPath, View.IdModule, "misolfecha"
                                                                         , View.FilterNoSolicitud, View.FilterTipo, View.FilterArea, View.FilterProceso, View.UserSession.IdUser);
                View.LoadView(dt);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}