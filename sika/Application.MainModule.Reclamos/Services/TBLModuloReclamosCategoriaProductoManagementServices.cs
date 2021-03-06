using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Reclamos.Contracts;
using Application.MainModule.Reclamos.IServices;

namespace Application.MainModule.Reclamos.Services
{
    public class SfTBL_ModuloReclamos_CategoriaProductoManagementServices : ISfTBL_ModuloReclamos_CategoriaProductoManagementServices
    {

         #region Fields
         readonly ITBL_ModuloReclamos_CategoriaProductoRepository _TBLModuloReclamosCategoriaProductoRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloReclamos_CategoriaProductoManagementServices( ITBL_ModuloReclamos_CategoriaProductoRepository TBLModuloReclamosCategoriaProductoRepository)
         {
            if (TBLModuloReclamosCategoriaProductoRepository == null)
                throw new ArgumentNullException("TBLModuloReclamosCategoriaProductoRepository");
            _TBLModuloReclamosCategoriaProductoRepository = TBLModuloReclamosCategoriaProductoRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloReclamos_CategoriaProducto NewEntity()
         {
            return new TBL_ModuloReclamos_CategoriaProducto();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloReclamos_CategoriaProducto entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosCategoriaProductoRepository.UnitOfWork;
            _TBLModuloReclamosCategoriaProductoRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloReclamos_CategoriaProducto entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloReclamosCategoriaProductoRepository.UnitOfWork;
            _TBLModuloReclamosCategoriaProductoRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloReclamos_CategoriaProducto entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosCategoriaProductoRepository.UnitOfWork;

            _TBLModuloReclamosCategoriaProductoRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloReclamos_CategoriaProducto FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_CategoriaProducto> specification = new DirectSpecification<TBL_ModuloReclamos_CategoriaProducto>(u => u.IdCategoria == id);

            return _TBLModuloReclamosCategoriaProductoRepository.GetCategoriaBySpec(specification);
           
         }

		 /*
         public TBL_ModuloReclamos_CategoriaProducto FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloReclamos_CategoriaProducto> specification = new DirectSpecification<TBL_ModuloReclamos_CategoriaProducto>(u => u.Code == id);

            return _TBLModuloReclamosCategoriaProductoRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloReclamos_CategoriaProducto> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloReclamos_CategoriaProducto> specification = new DirectSpecification<TBL_ModuloReclamos_CategoriaProducto>(u => u.IsActive == isActive);
            return _TBLModuloReclamosCategoriaProductoRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloReclamos_CategoriaProducto> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloReclamos_CategoriaProducto> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_CategoriaProducto>(u => u.IdCategoria != null);

            return _TBLModuloReclamosCategoriaProductoRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
         }

         public List<TBL_ModuloReclamos_CategoriaProducto> FindPaged(int pageIndex, int pageCount, string search)
         {
             if (pageIndex < 0)
                 throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

             if (pageCount <= 0)
                 throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


             if (!string.IsNullOrEmpty(search))
             {
                 Specification<TBL_ModuloReclamos_CategoriaProducto> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_CategoriaProducto>
                    (u => (u.Nombre.Contains(search) ||
                        u.Descripcion.Contains(search)));


                 return _TBLModuloReclamosCategoriaProductoRepository.GetPagedElements(pageIndex, pageCount, u => u.IdCategoria, onlyEnabledSpec, true).ToList();
             }
             else
             {
                 Specification<TBL_ModuloReclamos_CategoriaProducto> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_CategoriaProducto>(u => u.IdCategoria != null);

                 return _TBLModuloReclamosCategoriaProductoRepository.GetPagedElements(pageIndex, pageCount, u => u.IdCategoria, onlyEnabledSpec, true).ToList();
             }
         }

         public TBL_ModuloReclamos_CategoriaProducto GetByNombre(string name)
         {
             Specification<TBL_ModuloReclamos_CategoriaProducto> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_CategoriaProducto>(u => u.IsActive && u.Nombre == name);

             return _TBLModuloReclamosCategoriaProductoRepository.GetEntityBySpec(onlyEnabledSpec);
         }

         public int CountByPaged(string search)
         {
             if (!string.IsNullOrEmpty(search))
             {

                 Specification<TBL_ModuloReclamos_CategoriaProducto> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_CategoriaProducto>
                        (u => (u.Nombre.Contains(search) ||
                        u.Descripcion.Contains(search)));
                 return _TBLModuloReclamosCategoriaProductoRepository.GetBySpec(onlyEnabledSpec).Count();
             }
             else
             {
                 Specification<TBL_ModuloReclamos_CategoriaProducto> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_CategoriaProducto>(u => u.IdCategoria != null);

                 return _TBLModuloReclamosCategoriaProductoRepository.GetBySpec(onlyEnabledSpec).Count();
             }
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

            if (_TBLModuloReclamosCategoriaProductoRepository != null)
            {
                _TBLModuloReclamosCategoriaProductoRepository.UnitOfWork.Dispose();
            }
        }

        #endregion        
    }
}
    