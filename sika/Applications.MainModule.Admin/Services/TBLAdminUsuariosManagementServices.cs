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
using System.Data;
using System.Linq;
//using Application.MainModule.SqlServices.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Contracts;

namespace Applications.MainModule.Admin.Services
{
    public class SfTBL_Admin_UsuariosManagementServices : ISfTBL_Admin_UsuariosManagementServices
    {

         #region Fields
         readonly ITBL_Admin_UsuariosRepository _tblAdminUsuariosRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_Admin_UsuariosManagementServices( ITBL_Admin_UsuariosRepository tblAdminUsuariosRepository)
         {
            if (tblAdminUsuariosRepository == null)
                throw new ArgumentNullException("tblAdminUsuariosRepository");
            _tblAdminUsuariosRepository = tblAdminUsuariosRepository;

         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_Admin_Usuarios NewEntity()
         {
            return new TBL_Admin_Usuarios();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_Admin_Usuarios entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblAdminUsuariosRepository.UnitOfWork;
            _tblAdminUsuariosRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_Admin_Usuarios entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _tblAdminUsuariosRepository.UnitOfWork;
            _tblAdminUsuariosRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_Admin_Usuarios entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblAdminUsuariosRepository.UnitOfWork;

            _tblAdminUsuariosRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_Admin_Usuarios FindById(int id)
         {
           if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_Admin_Usuarios> specification = new DirectSpecification<TBL_Admin_Usuarios>(u => u.IdUser == id);

            return _tblAdminUsuariosRepository.RetornarUsuarioConRoles(specification);
          
         }
        
          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_Admin_Usuarios> FindBySpec(bool isActive)
         {
             Specification<TBL_Admin_Usuarios> specification = new DirectSpecification<TBL_Admin_Usuarios>(u => u.IsActive == isActive);
             return _tblAdminUsuariosRepository.GetBySpec(specification).ToList();
         }

        /// <summary>
          /// Obtiene el listado de entidades activas con roles.
          /// </summary>
         public List<TBL_Admin_Usuarios> FindBySpecWithRols(bool isActive)
         {
            Specification<TBL_Admin_Usuarios> specification = new DirectSpecification<TBL_Admin_Usuarios>(u => u.IsActive == isActive);
            return _tblAdminUsuariosRepository.RetornarUsuariosConRoles(specification).ToList();
         }

        public TBL_Admin_Usuarios GetUserByCredential(string userName, string password)
        {
            Specification<TBL_Admin_Usuarios> specification = new DirectSpecification<TBL_Admin_Usuarios>(u => u.UserName.Equals(userName) && u.Password.Equals(password));

            return _tblAdminUsuariosRepository.RetornarUsuarioConRoles(specification);
        }

        public TBL_Admin_Usuarios GetUserByCredential(string username)
        {
            Specification<TBL_Admin_Usuarios> specification = new DirectSpecification<TBL_Admin_Usuarios>(u => u.UserName.Equals(username));

            return _tblAdminUsuariosRepository.RetornarUsuarioConRoles(specification);
        }

        /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_Admin_Usuarios> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_Admin_Usuarios> onlyEnabledSpec = new DirectSpecification<TBL_Admin_Usuarios>(u => u.IsActive);

            return _tblAdminUsuariosRepository.GetPagedElements(pageIndex, pageCount, u => u.IdUser, onlyEnabledSpec, true).ToList();
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

            if (_tblAdminUsuariosRepository != null)
            {
                _tblAdminUsuariosRepository.UnitOfWork.Dispose();
            }
        }

        #endregion


        public int GetTotalUsers(string searchtext)
        {
            Specification<TBL_Admin_Usuarios> onlyEnabledSpec = new DirectSpecification<TBL_Admin_Usuarios>(u => u.IdUser != 0);

            if (!string.IsNullOrEmpty(searchtext))
            {
                onlyEnabledSpec &= new DirectSpecification<TBL_Admin_Usuarios>(u => u.Nombres.Contains(searchtext)
                                                                                    || u.UserName.Contains(searchtext));
            }

            var items = _tblAdminUsuariosRepository.GetBySpec(onlyEnabledSpec);

            if (items.Any())
                return items.Count();
            else
                return 0;
        }

        public List<TBL_Admin_Usuarios> GetUsers(string searchtext, int page, int size)
        {
            Specification<TBL_Admin_Usuarios> onlyEnabledSpec = new DirectSpecification<TBL_Admin_Usuarios>(u => u.IdUser != 0);

            if (!string.IsNullOrEmpty(searchtext))
            {
                onlyEnabledSpec &= new DirectSpecification<TBL_Admin_Usuarios>(u => u.Nombres.Contains(searchtext)
                                                                                    || u.UserName.Contains(searchtext));
            }

            return _tblAdminUsuariosRepository.GetPagedElements(page, size, u => u.IdUser, onlyEnabledSpec, true).ToList();
        }

        
    }
}
    