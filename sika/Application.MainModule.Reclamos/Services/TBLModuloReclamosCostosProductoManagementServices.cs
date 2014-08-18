using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Reclamos.Contracts;
using Application.MainModule.Reclamos.IServices;

namespace Application.MainModule.Reclamos.Services
{
    public class SfTBL_ModuloReclamos_CostosProductoManagementServices : ISfTBL_ModuloReclamos_CostosProductoManagementServices
    {

         #region Fields
         readonly ITBL_ModuloReclamos_CostosProductoRepository _TBLModuloReclamosCostosProductoRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloReclamos_CostosProductoManagementServices( ITBL_ModuloReclamos_CostosProductoRepository TBLModuloReclamosCostosProductoRepository)
         {
            if (TBLModuloReclamosCostosProductoRepository == null)
                throw new ArgumentNullException("TBLModuloReclamosCostosProductoRepository");
            _TBLModuloReclamosCostosProductoRepository = TBLModuloReclamosCostosProductoRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloReclamos_CostosProducto NewEntity()
         {
            return new TBL_ModuloReclamos_CostosProducto();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloReclamos_CostosProducto entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosCostosProductoRepository.UnitOfWork;
            _TBLModuloReclamosCostosProductoRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloReclamos_CostosProducto entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloReclamosCostosProductoRepository.UnitOfWork;
            _TBLModuloReclamosCostosProductoRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloReclamos_CostosProducto entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosCostosProductoRepository.UnitOfWork;

            _TBLModuloReclamosCostosProductoRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloReclamos_CostosProducto FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_CostosProducto> specification = new DirectSpecification<TBL_ModuloReclamos_CostosProducto>(u => u.IdCostoProducto == id);

            return _TBLModuloReclamosCostosProductoRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloReclamos_CostosProducto FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloReclamos_CostosProducto> specification = new DirectSpecification<TBL_ModuloReclamos_CostosProducto>(u => u.Code == id);

            return _TBLModuloReclamosCostosProductoRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloReclamos_CostosProducto> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloReclamos_CostosProducto> specification = new DirectSpecification<TBL_ModuloReclamos_CostosProducto>(u => u.IsActive == isActive);
            return _TBLModuloReclamosCostosProductoRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloReclamos_CostosProducto> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloReclamos_CostosProducto> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_CostosProducto>(u => u.IsActive);

            return _TBLModuloReclamosCostosProductoRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
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

            if (_TBLModuloReclamosCostosProductoRepository != null)
            {
                _TBLModuloReclamosCostosProductoRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<TBL_ModuloReclamos_CostosProducto> GetCostosByReclamo(decimal idReclamo)
        {
            Specification<TBL_ModuloReclamos_CostosProducto> spec = new DirectSpecification<TBL_ModuloReclamos_CostosProducto>(u => u.IdReclamo == idReclamo);

            return _TBLModuloReclamosCostosProductoRepository.GetBySpec(spec).ToList();
        }

        public TBL_ModuloReclamos_CostosProducto GetCostosById(decimal idCosto)
        {
            Specification<TBL_ModuloReclamos_CostosProducto> spec = new DirectSpecification<TBL_ModuloReclamos_CostosProducto>(u => u.IdCostoProducto == idCosto);

            return _TBLModuloReclamosCostosProductoRepository.GetEntityBySpec(spec);
        }
    }
}
    