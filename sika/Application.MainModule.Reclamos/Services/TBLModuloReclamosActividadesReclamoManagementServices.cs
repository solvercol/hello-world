using System;
using System.Collections.Generic;
using System.Linq;
using Application.MainModule.Reclamos.IServices;
using Domain.Core.Specification;
using Domain.MainModule.Reclamos.Contracts;
using Domain.MainModules.Entities;

namespace Application.MainModule.Reclamos.Services
{
    public class SfTBL_ModuloReclamos_ActividadesReclamoManagementServices : ISfTBL_ModuloReclamos_ActividadesReclamoManagementServices
    {

         #region Fields
         readonly ITBL_ModuloReclamos_ActividadesReclamoRepository _TBLModuloReclamosActividadesReclamoRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloReclamos_ActividadesReclamoManagementServices( ITBL_ModuloReclamos_ActividadesReclamoRepository TBLModuloReclamosActividadesReclamoRepository)
         {
            if (TBLModuloReclamosActividadesReclamoRepository == null)
                throw new ArgumentNullException("TBLModuloReclamosActividadesReclamoRepository");
            _TBLModuloReclamosActividadesReclamoRepository = TBLModuloReclamosActividadesReclamoRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloReclamos_ActividadesReclamo NewEntity()
         {
            return new TBL_ModuloReclamos_ActividadesReclamo();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloReclamos_ActividadesReclamo entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosActividadesReclamoRepository.UnitOfWork;
            _TBLModuloReclamosActividadesReclamoRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloReclamos_ActividadesReclamo entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloReclamosActividadesReclamoRepository.UnitOfWork;
            _TBLModuloReclamosActividadesReclamoRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloReclamos_ActividadesReclamo entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosActividadesReclamoRepository.UnitOfWork;

            _TBLModuloReclamosActividadesReclamoRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloReclamos_ActividadesReclamo FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_ActividadesReclamo> specification = new DirectSpecification<TBL_ModuloReclamos_ActividadesReclamo>(u => u.IdActividad == id);

            return _TBLModuloReclamosActividadesReclamoRepository.GetActividadBySpec(specification);
           
         }

		 /*
         public TBL_ModuloReclamos_ActividadesReclamo FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloReclamos_ActividadesReclamo> specification = new DirectSpecification<TBL_ModuloReclamos_ActividadesReclamo>(u => u.Code == id);

            return _TBLModuloReclamosActividadesReclamoRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloReclamos_ActividadesReclamo> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloReclamos_ActividadesReclamo> specification = new DirectSpecification<TBL_ModuloReclamos_ActividadesReclamo>(u => u.IsActive == isActive);
            return _TBLModuloReclamosActividadesReclamoRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloReclamos_ActividadesReclamo> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloReclamos_ActividadesReclamo> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_ActividadesReclamo>(u => u.IdActividad != null);

            return _TBLModuloReclamosActividadesReclamoRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
         }

         public List<TBL_ModuloReclamos_ActividadesReclamo> FindPaged(int pageIndex, int pageCount, string search)
         {
             if (pageIndex < 0)
                 throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

             if (pageCount <= 0)
                 throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


             if (!string.IsNullOrEmpty(search))
             {
                 Specification<TBL_ModuloReclamos_ActividadesReclamo> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_ActividadesReclamo>
                    (u => u.Nombre.Contains(search) ||
                    u.Descripcion.Contains(search) ||
                    u.TBL_ModuloReclamos_TipoReclamo.Nombre.Contains(search));


                 return _TBLModuloReclamosActividadesReclamoRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
             }
             else
             {
                 Specification<TBL_ModuloReclamos_ActividadesReclamo> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_ActividadesReclamo>(u => u.IdActividad != null);

                 return _TBLModuloReclamosActividadesReclamoRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
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

            if (_TBLModuloReclamosActividadesReclamoRepository != null)
            {
                _TBLModuloReclamosActividadesReclamoRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<TBL_ModuloReclamos_ActividadesReclamo> GetByTypoReclamo(int idTipoReclamo)
        {
            Specification<TBL_ModuloReclamos_ActividadesReclamo> spec = new DirectSpecification<TBL_ModuloReclamos_ActividadesReclamo>(u => u.IdTipoReclamo == idTipoReclamo);

            return _TBLModuloReclamosActividadesReclamoRepository.GetBySpec(spec).ToList();
        }

        public int CountByPaged(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                Specification<TBL_ModuloReclamos_ActividadesReclamo> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_ActividadesReclamo>
                    (u => u.Nombre.Contains(search) ||
                    u.Descripcion.Contains(search) ||
                    u.TBL_ModuloReclamos_TipoReclamo.Nombre.Contains(search));

                return _TBLModuloReclamosActividadesReclamoRepository.GetBySpec(onlyEnabledSpec).Count();
            }
            else
            {
                Specification<TBL_ModuloReclamos_ActividadesReclamo> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_ActividadesReclamo>(u => u.IdActividad != null);

                return _TBLModuloReclamosActividadesReclamoRepository.GetBySpec(onlyEnabledSpec).Count();
            }
        }
    }
}
    