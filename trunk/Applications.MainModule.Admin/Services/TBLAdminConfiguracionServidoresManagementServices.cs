//------------------------------------------------------------------------------
// <auto-generated>
//     Este codigo fue generado por el motor de generacion de codigo de propiedad de Walter molano.
//     El cambio  de algunas lineas de codigo podran causar comportamientos
//     inesperados de la aplicacion.  junio 18 de 2014.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 
using System;
using System.Collections.Generic;
using System.Linq;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contracts;

namespace Applications.MainModule.Admin.Services
{
    public class SfTBL_Admin_ConfiguracionServidoresManagementServices : ISfTBL_Admin_ConfiguracionServidoresManagementServices
    {

         #region Fields
         readonly ITBL_Admin_ConfiguracionServidoresRepository _tblAdminConfiguracionServidoresRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_Admin_ConfiguracionServidoresManagementServices( ITBL_Admin_ConfiguracionServidoresRepository tblAdminConfiguracionServidoresRepository)
         {
            if (tblAdminConfiguracionServidoresRepository == null)
                throw new ArgumentNullException("tblAdminConfiguracionServidoresRepository");
            _tblAdminConfiguracionServidoresRepository = tblAdminConfiguracionServidoresRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_Admin_ConfiguracionServidores NewEntity()
         {
            return new TBL_Admin_ConfiguracionServidores();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_Admin_ConfiguracionServidores entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblAdminConfiguracionServidoresRepository.UnitOfWork;
            _tblAdminConfiguracionServidoresRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_Admin_ConfiguracionServidores entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _tblAdminConfiguracionServidoresRepository.UnitOfWork;
            _tblAdminConfiguracionServidoresRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_Admin_ConfiguracionServidores entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblAdminConfiguracionServidoresRepository.UnitOfWork;

            _tblAdminConfiguracionServidoresRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_Admin_ConfiguracionServidores FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_Admin_ConfiguracionServidores> specification = new DirectSpecification<TBL_Admin_ConfiguracionServidores>(u => u.IdConfiguracion == id);

            return _tblAdminConfiguracionServidoresRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_Admin_ConfiguracionServidores FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_Admin_ConfiguracionServidores> specification = new DirectSpecification<TBL_Admin_ConfiguracionServidores>(u => u.Code == id);

            return _TBLAdminConfiguracionServidoresRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_Admin_ConfiguracionServidores> FindBySpec(bool isActive)
         {
            Specification<TBL_Admin_ConfiguracionServidores> specification = new DirectSpecification<TBL_Admin_ConfiguracionServidores>(u => u.IsActive == isActive);
            return _tblAdminConfiguracionServidoresRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_Admin_ConfiguracionServidores> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_Admin_ConfiguracionServidores> onlyEnabledSpec = new DirectSpecification<TBL_Admin_ConfiguracionServidores>(u => u.IsActive);

            return _tblAdminConfiguracionServidoresRepository.GetPagedElements(pageIndex, pageCount, u => u.IdConfiguracion, onlyEnabledSpec, true).ToList();
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

            if (_tblAdminConfiguracionServidoresRepository != null)
            {
                _tblAdminConfiguracionServidoresRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    