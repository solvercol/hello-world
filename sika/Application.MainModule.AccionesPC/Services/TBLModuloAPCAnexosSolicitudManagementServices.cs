using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.AccionesPC.Contracts;
using Application.MainModule.AccionesPC.IServices;

namespace Application.MainModule.AccionesPC.Services
{
    public class SfTBL_ModuloAPC_AnexosSolicitudManagementServices : ISfTBL_ModuloAPC_AnexosSolicitudManagementServices
    {

         #region Fields
         readonly ITBL_ModuloAPC_AnexosSolicitudRepository _TBLModuloAPCAnexosSolicitudRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloAPC_AnexosSolicitudManagementServices( ITBL_ModuloAPC_AnexosSolicitudRepository TBLModuloAPCAnexosSolicitudRepository)
         {
            if (TBLModuloAPCAnexosSolicitudRepository == null)
                throw new ArgumentNullException("TBLModuloAPCAnexosSolicitudRepository");
            _TBLModuloAPCAnexosSolicitudRepository = TBLModuloAPCAnexosSolicitudRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloAPC_AnexosSolicitud NewEntity()
         {
            return new TBL_ModuloAPC_AnexosSolicitud();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloAPC_AnexosSolicitud entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloAPCAnexosSolicitudRepository.UnitOfWork;
            _TBLModuloAPCAnexosSolicitudRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloAPC_AnexosSolicitud entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloAPCAnexosSolicitudRepository.UnitOfWork;
            _TBLModuloAPCAnexosSolicitudRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloAPC_AnexosSolicitud entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloAPCAnexosSolicitudRepository.UnitOfWork;

            _TBLModuloAPCAnexosSolicitudRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloAPC_AnexosSolicitud FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloAPC_AnexosSolicitud> specification = new DirectSpecification<TBL_ModuloAPC_AnexosSolicitud>(u => u.IdAnexoSolicitud == id);

            return _TBLModuloAPCAnexosSolicitudRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloAPC_AnexosSolicitud FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloAPC_AnexosSolicitud> specification = new DirectSpecification<TBL_ModuloAPC_AnexosSolicitud>(u => u.Code == id);

            return _TBLModuloAPCAnexosSolicitudRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloAPC_AnexosSolicitud> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloAPC_AnexosSolicitud> specification = new DirectSpecification<TBL_ModuloAPC_AnexosSolicitud>(u => u.IsActive == isActive);
            return _TBLModuloAPCAnexosSolicitudRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloAPC_AnexosSolicitud> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloAPC_AnexosSolicitud> onlyEnabledSpec = new DirectSpecification<TBL_ModuloAPC_AnexosSolicitud>(u => u.IsActive);

            return _TBLModuloAPCAnexosSolicitudRepository.GetPagedElements(pageIndex, pageCount, u => u.IdAnexoSolicitud, onlyEnabledSpec, true).ToList();
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

            if (_TBLModuloAPCAnexosSolicitudRepository != null)
            {
                _TBLModuloAPCAnexosSolicitudRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<TBL_ModuloAPC_AnexosSolicitud> GetByIdSolicitud(decimal id)
        {
            Specification<TBL_ModuloAPC_AnexosSolicitud> onlyEnabledSpec = new DirectSpecification<TBL_ModuloAPC_AnexosSolicitud>(u => u.IsActive && u.IdSolicitudAPC == id);

            return _TBLModuloAPCAnexosSolicitudRepository.GetBySpec(onlyEnabledSpec).ToList();
        }

        public TBL_ModuloAPC_AnexosSolicitud GetById(decimal id)
        {
            Specification<TBL_ModuloAPC_AnexosSolicitud> onlyEnabledSpec = new DirectSpecification<TBL_ModuloAPC_AnexosSolicitud>(u => u.IsActive && u.IdAnexoSolicitud == id);

            return _TBLModuloAPCAnexosSolicitudRepository.GetEntityBySpec(onlyEnabledSpec);
        }
    }
}
    