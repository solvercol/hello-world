using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.AccionesPC.Contracts;
using Application.MainModule.AccionesPC.IServices;

namespace Application.MainModule.AccionesPC.Services
{
    public class SfTBL_ModuloAPC_SolicitudManagementServices : ISfTBL_ModuloAPC_SolicitudManagementServices
    {

         #region Fields
         readonly ITBL_ModuloAPC_SolicitudRepository _TBLModuloAPCSolicitudRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloAPC_SolicitudManagementServices( ITBL_ModuloAPC_SolicitudRepository TBLModuloAPCSolicitudRepository)
         {
            if (TBLModuloAPCSolicitudRepository == null)
                throw new ArgumentNullException("TBLModuloAPCSolicitudRepository");
            _TBLModuloAPCSolicitudRepository = TBLModuloAPCSolicitudRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloAPC_Solicitud NewEntity()
         {
            return new TBL_ModuloAPC_Solicitud();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloAPC_Solicitud entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloAPCSolicitudRepository.UnitOfWork;
            _TBLModuloAPCSolicitudRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloAPC_Solicitud entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloAPCSolicitudRepository.UnitOfWork;
            _TBLModuloAPCSolicitudRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloAPC_Solicitud entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloAPCSolicitudRepository.UnitOfWork;

            _TBLModuloAPCSolicitudRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloAPC_Solicitud FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloAPC_Solicitud> specification = new DirectSpecification<TBL_ModuloAPC_Solicitud>(u => u.IdSolucitudAPC == id);

            return _TBLModuloAPCSolicitudRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloAPC_Solicitud FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloAPC_Solicitud> specification = new DirectSpecification<TBL_ModuloAPC_Solicitud>(u => u.Code == id);

            return _TBLModuloAPCSolicitudRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloAPC_Solicitud> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloAPC_Solicitud> specification = new DirectSpecification<TBL_ModuloAPC_Solicitud>(u => u.IsActive == isActive);
            return _TBLModuloAPCSolicitudRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloAPC_Solicitud> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloAPC_Solicitud> onlyEnabledSpec = new DirectSpecification<TBL_ModuloAPC_Solicitud>(u => u.IsActive);

            return _TBLModuloAPCSolicitudRepository.GetPagedElements(pageIndex, pageCount, u => u.IdSolucitudAPC, onlyEnabledSpec, true).ToList();
         }

         public int ReturnStatusBySolicitudId(decimal id)
         {
            
             return _TBLModuloAPCSolicitudRepository.ReturnStatusBySolicitudId(id);
         }

         public TBL_ModuloAPC_Solicitud GetById(decimal id)
         {
             
             return _TBLModuloAPCSolicitudRepository.GetSolicitudById(id);
         }

         public TBL_ModuloAPC_Solicitud GetWithNavById(decimal id)
         {
             Specification<TBL_ModuloAPC_Solicitud> onlyEnabledSpec = new DirectSpecification<TBL_ModuloAPC_Solicitud>(u => u.IsActive && u.IdSolucitudAPC == id);

             return _TBLModuloAPCSolicitudRepository.GetCompleteEntity(onlyEnabledSpec);
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

            if (_TBLModuloAPCSolicitudRepository != null)
            {
                _TBLModuloAPCSolicitudRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

       
    }
}
    