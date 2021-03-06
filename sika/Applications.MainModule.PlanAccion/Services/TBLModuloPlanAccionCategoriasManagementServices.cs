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
using Applications.MainModule.PlanAccion.IServices;
using Domain.MainModule.PlanAccion.Contracts;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Applications.MainModule.PlanAccion.Services
{
    public class SfTBL_ModuloPlanAccion_CategoriasManagementServices : ISfTBL_ModuloPlanAccion_CategoriasManagementServices
    {

         #region Fields
         readonly ITBL_ModuloPlanAccion_CategoriasRepository _tblModuloPlanAccionCategoriasRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloPlanAccion_CategoriasManagementServices( ITBL_ModuloPlanAccion_CategoriasRepository tblModuloPlanAccionCategoriasRepository)
         {
            if (tblModuloPlanAccionCategoriasRepository == null)
                throw new ArgumentNullException("tblModuloPlanAccionCategoriasRepository");
            _tblModuloPlanAccionCategoriasRepository = tblModuloPlanAccionCategoriasRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloPlanAccion_Categorias NewEntity()
         {
            return new TBL_ModuloPlanAccion_Categorias();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloPlanAccion_Categorias entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblModuloPlanAccionCategoriasRepository.UnitOfWork;
            _tblModuloPlanAccionCategoriasRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloPlanAccion_Categorias entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _tblModuloPlanAccionCategoriasRepository.UnitOfWork;
            _tblModuloPlanAccionCategoriasRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloPlanAccion_Categorias entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblModuloPlanAccionCategoriasRepository.UnitOfWork;

            _tblModuloPlanAccionCategoriasRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloPlanAccion_Categorias FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloPlanAccion_Categorias> specification = new DirectSpecification<TBL_ModuloPlanAccion_Categorias>(u => u.IdCategoria == id);

            return _tblModuloPlanAccionCategoriasRepository.GetEntityBySpec(specification);
           
         }

       

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloPlanAccion_Categorias> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloPlanAccion_Categorias> specification = new DirectSpecification<TBL_ModuloPlanAccion_Categorias>(u => u.IsActive == isActive);
            return _tblModuloPlanAccionCategoriasRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloPlanAccion_Categorias> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloPlanAccion_Categorias> onlyEnabledSpec = new DirectSpecification<TBL_ModuloPlanAccion_Categorias>(u => u.IsActive);

            return _tblModuloPlanAccionCategoriasRepository.GetPagedElements(pageIndex, pageCount, u => u.IdCategoria, onlyEnabledSpec, true).ToList();
         }

         public int CountbyPaged()
         {
            
             Specification<TBL_ModuloPlanAccion_Categorias> onlyEnabledSpec = new DirectSpecification<TBL_ModuloPlanAccion_Categorias>(u => u.IsActive);

             return _tblModuloPlanAccionCategoriasRepository.GetBySpec(onlyEnabledSpec).Count();
         }

        public bool Delete(int idCategoria)
        {
            if (idCategoria <= 0)
                throw new ArgumentException(Resources.Messages.Parameter_isNull, "idCategoria");

            var onlyEnabledSpec = new DirectSpecification<TBL_ModuloPlanAccion_Categorias>(u => u.TBL_ModuloPlanAccion_ConfiguracionActividades.Any());
            var result = _tblModuloPlanAccionCategoriasRepository.GetBySpec(onlyEnabledSpec).Count();
            if (result > 0)
                return false;

            onlyEnabledSpec = new DirectSpecification<TBL_ModuloPlanAccion_Categorias>(u => u.IdCategoria == idCategoria);
            var unitOfWork = _tblModuloPlanAccionCategoriasRepository.UnitOfWork;
            result = _tblModuloPlanAccionCategoriasRepository.BulkDeletebySpec(onlyEnabledSpec);
            unitOfWork.CommitAndRefreshChanges();

            return result > 0;
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

            if (_tblModuloPlanAccionCategoriasRepository != null)
            {
                _tblModuloPlanAccionCategoriasRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    