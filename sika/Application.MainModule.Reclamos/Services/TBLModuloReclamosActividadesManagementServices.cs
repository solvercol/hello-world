using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Reclamos.Contracts;
using Application.MainModule.Reclamos.IServices;

namespace Application.MainModule.Reclamos.Services
{
    public class SfTBL_ModuloReclamos_ActividadesManagementServices : ISfTBL_ModuloReclamos_ActividadesManagementServices
    {

         #region Fields
         readonly ITBL_ModuloReclamos_ActividadesRepository _TBLModuloReclamosActividadesRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloReclamos_ActividadesManagementServices( ITBL_ModuloReclamos_ActividadesRepository TBLModuloReclamosActividadesRepository)
         {
            if (TBLModuloReclamosActividadesRepository == null)
                throw new ArgumentNullException("TBLModuloReclamosActividadesRepository");
            _TBLModuloReclamosActividadesRepository = TBLModuloReclamosActividadesRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloReclamos_Actividades NewEntity()
         {
            return new TBL_ModuloReclamos_Actividades();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloReclamos_Actividades entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosActividadesRepository.UnitOfWork;
            _TBLModuloReclamosActividadesRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloReclamos_Actividades entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloReclamosActividadesRepository.UnitOfWork;
            _TBLModuloReclamosActividadesRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloReclamos_Actividades entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosActividadesRepository.UnitOfWork;

            _TBLModuloReclamosActividadesRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloReclamos_Actividades FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_Actividades> specification = new DirectSpecification<TBL_ModuloReclamos_Actividades>(u => u.IdActividad == id);

            return _TBLModuloReclamosActividadesRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloReclamos_Actividades FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloReclamos_Actividades> specification = new DirectSpecification<TBL_ModuloReclamos_Actividades>(u => u.Code == id);

            return _TBLModuloReclamosActividadesRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloReclamos_Actividades> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloReclamos_Actividades> specification = new DirectSpecification<TBL_ModuloReclamos_Actividades>(u => u.IsActive == isActive);
            return _TBLModuloReclamosActividadesRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloReclamos_Actividades> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloReclamos_Actividades> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_Actividades>(u => u.IsActive);

            return _TBLModuloReclamosActividadesRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
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

            if (_TBLModuloReclamosActividadesRepository != null)
            {
                _TBLModuloReclamosActividadesRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public TBL_ModuloReclamos_Actividades GetById(decimal id)
        {
            Specification<TBL_ModuloReclamos_Actividades> spec = new DirectSpecification<TBL_ModuloReclamos_Actividades>(u => u.IdActividad == id);

            return _TBLModuloReclamosActividadesRepository.GetCompleteEntityBySpec(spec);
        }

        public List<TBL_ModuloReclamos_Actividades> GetByIdReclamo(decimal idReclamo)
        {
            Specification<TBL_ModuloReclamos_Actividades> spec = new DirectSpecification<TBL_ModuloReclamos_Actividades>(u => u.IdReclamo == idReclamo);

            return _TBLModuloReclamosActividadesRepository.GetCompleteListBySpec(spec);
        }
    }
}    