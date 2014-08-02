//------------------------------------------------------------------------------
// <auto-generated>
//     Este codigo fue generado por una plantilla T4 de propiedad de Walter molano.
//     El cambio  de algunas lineas de codigo podran causar comportamientos
//     inesperados de la aplicacion.  Abril 24 de 2012.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contracts;
using Application.MainModule.IApplicationServices;

namespace Application.MainModule.ApplicationServices
{
    public class SfTBL_Admin_ModuleServiceManagementServices : ISfTBL_Admin_ModuleServiceManagementServices
    {

         #region Fields
         readonly ITBL_Admin_ModuleServiceRepository _TBLAdminModuleServiceRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_Admin_ModuleServiceManagementServices( ITBL_Admin_ModuleServiceRepository TBLAdminModuleServiceRepository)
         {
            if (TBLAdminModuleServiceRepository == null)
                throw new ArgumentNullException("TBLAdminModuleServiceRepository");
            _TBLAdminModuleServiceRepository = TBLAdminModuleServiceRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_Admin_ModuleService NewEntity()
         {
            return new TBL_Admin_ModuleService();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_Admin_ModuleService entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLAdminModuleServiceRepository.UnitOfWork;
            _TBLAdminModuleServiceRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_Admin_ModuleService entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLAdminModuleServiceRepository.UnitOfWork;
            _TBLAdminModuleServiceRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_Admin_ModuleService entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLAdminModuleServiceRepository.UnitOfWork;

            _TBLAdminModuleServiceRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_Admin_ModuleService FindById(int id)
         {
           /* if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_Admin_ModuleService> specification = new DirectSpecification<TBL_Admin_ModuleService>(u => u.ID == id);

            return _TBLAdminModuleServiceRepository.GetEntityBySpec(specification);
            */
            return null;
         }

         public TBL_Admin_ModuleService FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_Admin_ModuleService> specification = new DirectSpecification<TBL_Admin_ModuleService>(u => u.Code == id);

            return _TBLAdminModuleServiceRepository.GetEntityBySpec(specification);
         }


          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_Admin_ModuleService> FindBySpec(bool isActive)
         {
            Specification<TBL_Admin_ModuleService> specification = new DirectSpecification<TBL_Admin_ModuleService>(u => u.IsActive == isActive);
            return _TBLAdminModuleServiceRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_Admin_ModuleService> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_Admin_ModuleService> onlyEnabledSpec = new DirectSpecification<TBL_Admin_ModuleService>(u => u.IsActive);

            return _TBLAdminModuleServiceRepository.GetPagedElements(pageIndex, pageCount, u => u.Code, onlyEnabledSpec, true).ToList();
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

            if (_TBLAdminModuleServiceRepository != null)
            {
                _TBLAdminModuleServiceRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    