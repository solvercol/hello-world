using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Reclamos.Contracts;
using Application.MainModule.Reclamos.IServices;
using Application.MainModule.SqlServices.IServices;

namespace Application.MainModule.Reclamos.Services
{
    public class SfTBL_ModuloReclamos_ReclamoManagementServices : ISfTBL_ModuloReclamos_ReclamoManagementServices
    {

         #region Fields
         readonly ITBL_ModuloReclamos_ReclamoRepository _TBLModuloReclamosReclamoRepository;
         readonly IReclamosExternalInterfacesService _reclamosExternarInterfaceService;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloReclamos_ReclamoManagementServices(ITBL_ModuloReclamos_ReclamoRepository TBLModuloReclamosReclamoRepository, IReclamosExternalInterfacesService reclamosExternarInterfaceService)
         {
            if (TBLModuloReclamosReclamoRepository == null)
                throw new ArgumentNullException("TBLModuloReclamosReclamoRepository");
            _TBLModuloReclamosReclamoRepository = TBLModuloReclamosReclamoRepository;
            _reclamosExternarInterfaceService = reclamosExternarInterfaceService;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloReclamos_Reclamo NewEntity()
         {
            return new TBL_ModuloReclamos_Reclamo();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloReclamos_Reclamo entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosReclamoRepository.UnitOfWork;
            _TBLModuloReclamosReclamoRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloReclamos_Reclamo entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloReclamosReclamoRepository.UnitOfWork;
            _TBLModuloReclamosReclamoRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloReclamos_Reclamo entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosReclamoRepository.UnitOfWork;

            _TBLModuloReclamosReclamoRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloReclamos_Reclamo FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_Reclamo> specification = new DirectSpecification<TBL_ModuloReclamos_Reclamo>(u => u.IdReclamo == id);

            return _TBLModuloReclamosReclamoRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloReclamos_Reclamo FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloReclamos_Reclamo> specification = new DirectSpecification<TBL_ModuloReclamos_Reclamo>(u => u.Code == id);

            return _TBLModuloReclamosReclamoRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloReclamos_Reclamo> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloReclamos_Reclamo> specification = new DirectSpecification<TBL_ModuloReclamos_Reclamo>(u => u.IsActive == isActive);
            return _TBLModuloReclamosReclamoRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloReclamos_Reclamo> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloReclamos_Reclamo> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_Reclamo>(u => u.IsActive);

            return _TBLModuloReclamosReclamoRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
         }


       

         #endregion

         #region IDisposable Members

        /// <summary>
        /// Release all resources
        /// </summary>
        public void Dispose()
        {
            //release used unit of work
            //if you have many repositories but  lifetime is per resolve only need
            //dispose one

            if (_TBLModuloReclamosReclamoRepository != null)
            {
                _TBLModuloReclamosReclamoRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public TBL_ModuloReclamos_Reclamo GetReclamoWithNavById(decimal id)
        {
            Specification<TBL_ModuloReclamos_Reclamo> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_Reclamo>(u => u.IdReclamo == id);
            var oReturn = _TBLModuloReclamosReclamoRepository.GetCompleteEntity(onlyEnabledSpec);

            if (oReturn != null)
            {
                if (!string.IsNullOrEmpty(oReturn.CodigoProducto))
                {
                    oReturn.DtoProducto = _reclamosExternarInterfaceService.GetProductByCodigoProducto(oReturn.CodigoProducto);
                }

                if (!string.IsNullOrEmpty(oReturn.CodigoCliente))
                {
                    oReturn.DtoCliente = _reclamosExternarInterfaceService.GetClientByCodigoCliente(oReturn.CodigoCliente);
                }
            }

            return oReturn;
        }

        public TBL_ModuloReclamos_Reclamo GetReclamoById(decimal id)
        {
            Specification<TBL_ModuloReclamos_Reclamo> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_Reclamo>(u => u.IdReclamo == id);

            var oReturn = _TBLModuloReclamosReclamoRepository.GetEntityBySpec(onlyEnabledSpec);

            if (oReturn != null)
            {
                if (!string.IsNullOrEmpty(oReturn.CodigoProducto))
                {
                    oReturn.DtoProducto = _reclamosExternarInterfaceService.GetProductByCodigoProducto(oReturn.CodigoProducto);
                }

                if (!string.IsNullOrEmpty(oReturn.CodigoCliente))
                {
                    oReturn.DtoCliente = _reclamosExternarInterfaceService.GetClientByCodigoCliente(oReturn.CodigoCliente);
                }
            }

            return oReturn;
        }
    }
}
    