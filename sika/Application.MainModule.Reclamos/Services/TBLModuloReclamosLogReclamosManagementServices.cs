using System;
using System.Collections.Generic;
using System.Linq;
using Application.MainModule.Reclamos.IServices;
using Domain.MainModule.Reclamos.Contracts;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Application.MainModule.Reclamos.Services
{
    public class SfTBL_ModuloReclamos_LogReclamosManagementServices : ISfTBL_ModuloReclamos_LogReclamosManagementServices
    {

         #region Fields
         readonly ITBL_ModuloReclamos_LogReclamosRepository _tblModuloReclamosLogReclamosRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloReclamos_LogReclamosManagementServices( ITBL_ModuloReclamos_LogReclamosRepository tblModuloReclamosLogReclamosRepository)
         {
            if (tblModuloReclamosLogReclamosRepository == null)
                throw new ArgumentNullException("tblModuloReclamosLogReclamosRepository");
            _tblModuloReclamosLogReclamosRepository = tblModuloReclamosLogReclamosRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloReclamos_LogReclamos NewEntity()
         {
            return new TBL_ModuloReclamos_LogReclamos();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloReclamos_LogReclamos entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblModuloReclamosLogReclamosRepository.UnitOfWork;
            _tblModuloReclamosLogReclamosRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloReclamos_LogReclamos entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _tblModuloReclamosLogReclamosRepository.UnitOfWork;
            _tblModuloReclamosLogReclamosRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloReclamos_LogReclamos entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblModuloReclamosLogReclamosRepository.UnitOfWork;

            _tblModuloReclamosLogReclamosRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloReclamos_LogReclamos FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_LogReclamos> specification = new DirectSpecification<TBL_ModuloReclamos_LogReclamos>(u => u.IdLog != null);

            return _tblModuloReclamosLogReclamosRepository.GetEntityBySpec(specification);
           
         }

		
		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloReclamos_LogReclamos> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloReclamos_LogReclamos> specification = new DirectSpecification<TBL_ModuloReclamos_LogReclamos>(u => u.IsActive == isActive);
            return _tblModuloReclamosLogReclamosRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloReclamos_LogReclamos> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloReclamos_LogReclamos> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_LogReclamos>(u => u.IsActive);

            return _tblModuloReclamosLogReclamosRepository.GetPagedElements(pageIndex, pageCount, u => u.IdLog, onlyEnabledSpec, true).ToList();
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

            if (_tblModuloReclamosLogReclamosRepository != null)
            {
                _tblModuloReclamosLogReclamosRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public TBL_ModuloReclamos_LogReclamos FindById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_LogReclamos> specification = new DirectSpecification<TBL_ModuloReclamos_LogReclamos>(u => u.IdLog == id);

            return _tblModuloReclamosLogReclamosRepository.GetEntityBySpec(specification);
        }


        public List<TBL_ModuloReclamos_LogReclamos> GetByIdReclamo(decimal idReclamo)
        {
            Specification<TBL_ModuloReclamos_LogReclamos> specification = new DirectSpecification<TBL_ModuloReclamos_LogReclamos>(u => u.IdReclamo == idReclamo);

            return _tblModuloReclamosLogReclamosRepository.GetBySpec(specification).ToList();
        }
    }
}
    