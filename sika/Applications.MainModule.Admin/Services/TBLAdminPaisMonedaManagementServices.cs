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
    public class SfTBL_Admin_PaisMonedaManagementServices : ISfTBL_Admin_PaisMonedaManagementServices
    {

         #region Fields
         readonly ITBL_Admin_PaisMonedaRepository _tblAdminPaisMonedaRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_Admin_PaisMonedaManagementServices( ITBL_Admin_PaisMonedaRepository tblAdminPaisMonedaRepository)
         {
            if (tblAdminPaisMonedaRepository == null)
                throw new ArgumentNullException("tblAdminPaisMonedaRepository");
            _tblAdminPaisMonedaRepository = tblAdminPaisMonedaRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_Admin_PaisMoneda NewEntity()
         {
            return new TBL_Admin_PaisMoneda();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_Admin_PaisMoneda entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblAdminPaisMonedaRepository.UnitOfWork;
            _tblAdminPaisMonedaRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_Admin_PaisMoneda entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _tblAdminPaisMonedaRepository.UnitOfWork;
            _tblAdminPaisMonedaRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_Admin_PaisMoneda entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblAdminPaisMonedaRepository.UnitOfWork;

            _tblAdminPaisMonedaRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_Admin_PaisMoneda FindById(int id)
         {
           /* if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_Admin_PaisMoneda> specification = new DirectSpecification<TBL_Admin_PaisMoneda>(u => u.ID == id);

            return _TBLAdminPaisMonedaRepository.GetEntityBySpec(specification);
            */
            return null;
         }

         public TBL_Admin_PaisMoneda FindById(int idPais, int idMoneda)
         {
            Specification<TBL_Admin_PaisMoneda> specification = new DirectSpecification<TBL_Admin_PaisMoneda>(u => u.IdMoneda == idMoneda && u.IdPais==idPais);

            return _tblAdminPaisMonedaRepository.GetEntityBySpec(specification);
         }


          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_Admin_PaisMoneda> FindBySpec(bool isActive)
         {
            Specification<TBL_Admin_PaisMoneda> specification = new DirectSpecification<TBL_Admin_PaisMoneda>(u => u.IdPais>= 0);
            return _tblAdminPaisMonedaRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_Admin_PaisMoneda> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_Admin_PaisMoneda> onlyEnabledSpec = new DirectSpecification<TBL_Admin_PaisMoneda>(u => u.IdPais >= 0);

            return _tblAdminPaisMonedaRepository.GetPagedElements(pageIndex, pageCount, u => u.IdPais, onlyEnabledSpec, true).ToList();
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

            if (_tblAdminPaisMonedaRepository != null)
            {
                _tblAdminPaisMonedaRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    