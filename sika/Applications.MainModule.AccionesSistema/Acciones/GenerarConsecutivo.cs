using System;
using System.Collections.Generic;
using System.Transactions;
using Domain.MainModule.AccionesPC.Contracts;
using Domain.MainModule.Contracts;
using Infraestructure.CrossCutting.Security.IServices;
using Infrastructure.CrossCutting.IDtoService;
using Infrastructure.CrossCutting.IoC;
using Infrastructure.CrossCutting.NetFramework.Enums;

namespace Applications.MainModule.AccionesSistema.Acciones
{
    public class GenerarConsecutivo
    {
        private readonly ITBL_Admin_OptionListRepository _optiosRepository;
        private readonly ITBL_ModuloAPC_SolicitudRepository _solicitudRepository;
        private readonly IAutentication _autenticationService;
        private readonly List<string> _errores = new List<string>();

        public GenerarConsecutivo()
        {
            _optiosRepository = IoC.Resolve<ITBL_Admin_OptionListRepository>();
            _solicitudRepository = IoC.Resolve<ITBL_ModuloAPC_SolicitudRepository>();
            _autenticationService = IoC.Resolve<IAutentication>();
        }


        public bool GenerarConsecutivoPedido(IDocumentDto oDocument)
        {

            if (oDocument == null)
            {
                _errores.Add("Error al leer el Id del Pedido. el parámetro es nulo.");
                return false;
            }

            var txSettings = new TransactionOptions()
            {
                Timeout = TransactionManager.DefaultTimeout,
                IsolationLevel = IsolationLevel.Serializable
            };


            using (var scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                var unitOfWork = _solicitudRepository.UnitOfWork;

                var consecutivo = RetornarUltimoConsecutivo();

                var oSolicitud = _solicitudRepository.GetSolicitudById(Convert.ToInt32(oDocument.IdDocument));

                if (oSolicitud == null)
                    throw new InvalidOperationException(string.Format("Error al recuperar la solicitud [{0}] desde la Base de Datos..",oDocument.IdDocument));

                oSolicitud.Consecutivo = consecutivo;

                //var newConsecutivo = RetornarConsecutivo(consecutivo.Length);

                //oSolicitud.NumeroPreorden = string.Format("H{0}{1}", newConsecutivo, consecutivo);

                oSolicitud.ModifiedOn = DateTime.Now;

                oSolicitud.ModifiedBy = _autenticationService.GetUserFromSession.IdUser;

                _solicitudRepository.Modify(oSolicitud);

                unitOfWork.CommitAndRefreshChanges();

                scope.Complete();

                return true;
            }
        }

        private static string RetornarConsecutivo(int lenConsecutivo)
        {
            var strString = string.Empty;
            for (var i = 0; i < (5 - lenConsecutivo); i++)
            {
                strString += "0";
            }
            return strString;
        }

        private int RetornarUltimoConsecutivo()
        {
            var strConsecutivo = string.Empty;
            var option = _optiosRepository.GetOptionByKey("Consecutivo", (int)ModulosAplicacion.WorkFlow);

            if (option != null)
            {
                strConsecutivo = option.Value;

                var uow = _optiosRepository.UnitOfWork;
                var newConsecutivo = Convert.ToInt32(option.Value) + 1;
                option.Value = newConsecutivo.ToString();
                option.ModifiedBy = _autenticationService.GetUserFromSession.IdUser.ToString();
                option.ModifiedOn = DateTime.Now;
                _optiosRepository.Modify(option);
                uow.CommitAndRefreshChanges();
            }

            return Convert.ToInt32(strConsecutivo);
        }

        public List<string> GetListErrors
        {
            get { return _errores; }
        }



    }
}