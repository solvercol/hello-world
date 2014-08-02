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
    public class SfTBL_Admin_ModuleTypeManagementServices : ISfTBL_Admin_ModuleTypeManagementServices
    {

         #region Fields
         readonly ITBL_Admin_ModuleTypeRepository _TBLAdminModuleTypeRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_Admin_ModuleTypeManagementServices( ITBL_Admin_ModuleTypeRepository TBLAdminModuleTypeRepository)
         {
            if (TBLAdminModuleTypeRepository == null)
                throw new ArgumentNullException("TBLAdminModuleTypeRepository");
            _TBLAdminModuleTypeRepository = TBLAdminModuleTypeRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_Admin_ModuleType NewEntity()
         {
            return new TBL_Admin_ModuleType();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_Admin_ModuleType entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLAdminModuleTypeRepository.UnitOfWork;
            _TBLAdminModuleTypeRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_Admin_ModuleType entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLAdminModuleTypeRepository.UnitOfWork;
            _TBLAdminModuleTypeRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_Admin_ModuleType entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLAdminModuleTypeRepository.UnitOfWork;

            _TBLAdminModuleTypeRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_Admin_ModuleType FindById(int id)
         {
           /* if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_Admin_ModuleType> specification = new DirectSpecification<TBL_Admin_ModuleType>(u => u.ID == id);

            return _TBLAdminModuleTypeRepository.GetEntityBySpec(specification);
            */
            return null;
         }

         public TBL_Admin_ModuleType FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_Admin_ModuleType> specification = new DirectSpecification<TBL_Admin_ModuleType>(u => u.Code == id);

            return _TBLAdminModuleTypeRepository.GetEntityBySpec(specification);
         }


          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_Admin_ModuleType> FindBySpec(bool isActive)
         {
            Specification<TBL_Admin_ModuleType> specification = new DirectSpecification<TBL_Admin_ModuleType>(u => u.IsActive == isActive);
            return _TBLAdminModuleTypeRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_Admin_ModuleType> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_Admin_ModuleType> onlyEnabledSpec = new DirectSpecification<TBL_Admin_ModuleType>(u => u.IsActive);

            return _TBLAdminModuleTypeRepository.GetPagedElements(pageIndex, pageCount, u => u.Code, onlyEnabledSpec, true).ToList();
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

            if (_TBLAdminModuleTypeRepository != null)
            {
                _TBLAdminModuleTypeRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    