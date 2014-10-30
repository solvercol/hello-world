using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.AccionesPC.Contracts;
using Application.MainModule.AccionesPC.IServices;

namespace Application.MainModule.AccionesPC.Services
{
    public class SfTBL_ModuloAPC_CausasManagementServices : ISfTBL_ModuloAPC_CausasManagementServices
    {

         #region Fields
         readonly ITBL_ModuloAPC_CausasRepository _TBLModuloAPCCausasRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloAPC_CausasManagementServices( ITBL_ModuloAPC_CausasRepository TBLModuloAPCCausasRepository)
         {
            if (TBLModuloAPCCausasRepository == null)
                throw new ArgumentNullException("TBLModuloAPCCausasRepository");
            _TBLModuloAPCCausasRepository = TBLModuloAPCCausasRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloAPC_Causas NewEntity()
         {
            return new TBL_ModuloAPC_Causas();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloAPC_Causas entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloAPCCausasRepository.UnitOfWork;
            _TBLModuloAPCCausasRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloAPC_Causas entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloAPCCausasRepository.UnitOfWork;
            _TBLModuloAPCCausasRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloAPC_Causas entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloAPCCausasRepository.UnitOfWork;

            _TBLModuloAPCCausasRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloAPC_Causas FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloAPC_Causas> specification = new DirectSpecification<TBL_ModuloAPC_Causas>(u => u.IdCausa == id);

            return _TBLModuloAPCCausasRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloAPC_Causas FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloAPC_Causas> specification = new DirectSpecification<TBL_ModuloAPC_Causas>(u => u.Code == id);

            return _TBLModuloAPCCausasRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloAPC_Causas> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloAPC_Causas> specification = new DirectSpecification<TBL_ModuloAPC_Causas>(u => u.IsActive == isActive);
            return _TBLModuloAPCCausasRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloAPC_Causas> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloAPC_Causas> onlyEnabledSpec = new DirectSpecification<TBL_ModuloAPC_Causas>(u => u.IsActive);

            return _TBLModuloAPCCausasRepository.GetPagedElements(pageIndex, pageCount, u => u.IdCausa, onlyEnabledSpec, true).ToList();
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

            if (_TBLModuloAPCCausasRepository != null)
            {
                _TBLModuloAPCCausasRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public TBL_ModuloAPC_Causas GetById(decimal id)
        {
            Specification<TBL_ModuloAPC_Causas> onlyEnabledSpec = new DirectSpecification<TBL_ModuloAPC_Causas>(u => u.IdCausa == id);

            return _TBLModuloAPCCausasRepository.GetCompleteEntity(onlyEnabledSpec);
        }

        public List<TBL_ModuloAPC_Causas> GetByIdSolicitud(decimal idSolicitud)
        {
            Specification<TBL_ModuloAPC_Causas> onlyEnabledSpec = new DirectSpecification<TBL_ModuloAPC_Causas>(u => u.IdSolicitudAPC == idSolicitud);

            return _TBLModuloAPCCausasRepository.GetCompleteEntityList(onlyEnabledSpec);
        }
    }
}
    