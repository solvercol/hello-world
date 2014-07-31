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
using Applications.MainModule.Admin.IServices;
using Applications.MainModule.Admin.Resources;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contracts;

namespace Applications.MainModule.Admin.Services
{
    public class SfTBL_Admin_OpcionesMenuManagementServices : ISfTBL_Admin_OpcionesMenuManagementServices
    {

         #region Fields
         readonly ITBL_Admin_OpcionesMenuRepository _tblAdminOpcionesMenuRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_Admin_OpcionesMenuManagementServices( ITBL_Admin_OpcionesMenuRepository tblAdminOpcionesMenuRepository)
         {
            if (tblAdminOpcionesMenuRepository == null)
                throw new ArgumentNullException("tblAdminOpcionesMenuRepository");
            _tblAdminOpcionesMenuRepository = tblAdminOpcionesMenuRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_Admin_OpcionesMenu NewEntity()
         {
            return new TBL_Admin_OpcionesMenu();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_Admin_OpcionesMenu entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblAdminOpcionesMenuRepository.UnitOfWork;
            _tblAdminOpcionesMenuRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_Admin_OpcionesMenu entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _tblAdminOpcionesMenuRepository.UnitOfWork;
            _tblAdminOpcionesMenuRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_Admin_OpcionesMenu entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblAdminOpcionesMenuRepository.UnitOfWork;

            _tblAdminOpcionesMenuRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_Admin_OpcionesMenu FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_Admin_OpcionesMenu> specification = new DirectSpecification<TBL_Admin_OpcionesMenu>(u => u.IdOpcionMenu == id);

            return _tblAdminOpcionesMenuRepository.GetEntityBySpec(specification);
          
         }

       


          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_Admin_OpcionesMenu> FindBySpec(bool isActive)
         {
            Specification<TBL_Admin_OpcionesMenu> specification = new DirectSpecification<TBL_Admin_OpcionesMenu>(u => u.Activo == isActive);
            return _tblAdminOpcionesMenuRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_Admin_OpcionesMenu> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_Admin_OpcionesMenu> onlyEnabledSpec = new DirectSpecification<TBL_Admin_OpcionesMenu>(u => u.Activo);

            return _tblAdminOpcionesMenuRepository.GetPagedElements(pageIndex, pageCount, u => u.IdOpcionMenu, onlyEnabledSpec, true).ToList();
         }

         public IEnumerable<TBL_Admin_OpcionesMenu> FindListByIdModule(string id)
         {

             Specification<TBL_Admin_OpcionesMenu> specification = new DirectSpecification<TBL_Admin_OpcionesMenu>(u => u.Activo);

             if(!string.IsNullOrEmpty(id))
             {
                 var idModule = Convert.ToInt32(id);
                 specification &= new DirectSpecification<TBL_Admin_OpcionesMenu>(u => u.AplicationId == idModule);
             }

             specification &= new DirectSpecification<TBL_Admin_OpcionesMenu>(u => u.TBL_Admin_Modulos.TBL_Admin_TypeByModules.Any(x=> x.TBL_Admin_ModuleType.AutoActivar));

             return _tblAdminOpcionesMenuRepository.FindAllOptionsBySpec(specification).ToList();
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

            if (_tblAdminOpcionesMenuRepository != null)
            {
                _tblAdminOpcionesMenuRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    