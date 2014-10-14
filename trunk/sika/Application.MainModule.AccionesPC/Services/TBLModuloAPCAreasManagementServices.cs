using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.AccionesPC.Contracts;
using Application.MainModule.AccionesPC.IServices;

namespace Application.MainModule.AccionesPC.Services
{
    public class SfTBL_ModuloAPC_AreasManagementServices : ISfTBL_ModuloAPC_AreasManagementServices
    {

         #region Fields
         readonly ITBL_ModuloAPC_AreasRepository _TBLModuloAPCAreasRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloAPC_AreasManagementServices( ITBL_ModuloAPC_AreasRepository TBLModuloAPCAreasRepository)
         {
            if (TBLModuloAPCAreasRepository == null)
                throw new ArgumentNullException("TBLModuloAPCAreasRepository");
            _TBLModuloAPCAreasRepository = TBLModuloAPCAreasRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloAPC_Areas NewEntity()
         {
            return new TBL_ModuloAPC_Areas();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloAPC_Areas entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloAPCAreasRepository.UnitOfWork;
            _TBLModuloAPCAreasRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloAPC_Areas entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloAPCAreasRepository.UnitOfWork;
            _TBLModuloAPCAreasRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloAPC_Areas entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloAPCAreasRepository.UnitOfWork;

            _TBLModuloAPCAreasRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloAPC_Areas FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloAPC_Areas> specification = new DirectSpecification<TBL_ModuloAPC_Areas>(u => u.IdArea == id);

            return _TBLModuloAPCAreasRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloAPC_Areas FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloAPC_Areas> specification = new DirectSpecification<TBL_ModuloAPC_Areas>(u => u.Code == id);

            return _TBLModuloAPCAreasRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloAPC_Areas> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloAPC_Areas> specification = new DirectSpecification<TBL_ModuloAPC_Areas>(u => u.IsActive == isActive);
            return _TBLModuloAPCAreasRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloAPC_Areas> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloAPC_Areas> onlyEnabledSpec = new DirectSpecification<TBL_ModuloAPC_Areas>(u => u.IsActive);

            return _TBLModuloAPCAreasRepository.GetPagedElements(pageIndex, pageCount, u => u.IdArea, onlyEnabledSpec, true).ToList();
         }

         public TBL_ModuloAPC_Areas GetById(int id)
         {
             Specification<TBL_ModuloAPC_Areas> onlyEnabledSpec = new DirectSpecification<TBL_ModuloAPC_Areas>(u => u.IsActive && u.IdArea == id);

             return _TBLModuloAPCAreasRepository.GetEntityWithGerente(onlyEnabledSpec);
         }

         public List<TBL_ModuloAPC_Areas> GetEntitiesWithGerente()
         {
             Specification<TBL_ModuloAPC_Areas> onlyEnabledSpec = new DirectSpecification<TBL_ModuloAPC_Areas>(u => u.IsActive);

             return _TBLModuloAPCAreasRepository.GetEntityListWithGerente(onlyEnabledSpec);
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

            if (_TBLModuloAPCAreasRepository != null)
            {
                _TBLModuloAPCAreasRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        
    }
}
    