using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Reclamos.Contracts;
using Application.MainModule.Reclamos.IServices;

namespace Application.MainModule.Reclamos.Services
{
    public class SfTBL_ModuloReclamos_SolucionesManagementServices : ISfTBL_ModuloReclamos_SolucionesManagementServices
    {

         #region Fields
         readonly ITBL_ModuloReclamos_SolucionesRepository _TBLModuloReclamosSolucionesRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloReclamos_SolucionesManagementServices( ITBL_ModuloReclamos_SolucionesRepository TBLModuloReclamosSolucionesRepository)
         {
            if (TBLModuloReclamosSolucionesRepository == null)
                throw new ArgumentNullException("TBLModuloReclamosSolucionesRepository");
            _TBLModuloReclamosSolucionesRepository = TBLModuloReclamosSolucionesRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloReclamos_Soluciones NewEntity()
         {
            return new TBL_ModuloReclamos_Soluciones();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloReclamos_Soluciones entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosSolucionesRepository.UnitOfWork;
            _TBLModuloReclamosSolucionesRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloReclamos_Soluciones entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloReclamosSolucionesRepository.UnitOfWork;
            _TBLModuloReclamosSolucionesRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloReclamos_Soluciones entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosSolucionesRepository.UnitOfWork;

            _TBLModuloReclamosSolucionesRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloReclamos_Soluciones FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_Soluciones> specification = new DirectSpecification<TBL_ModuloReclamos_Soluciones>(u => u.IdSolucion == id);

            return _TBLModuloReclamosSolucionesRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloReclamos_Soluciones FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloReclamos_Soluciones> specification = new DirectSpecification<TBL_ModuloReclamos_Soluciones>(u => u.Code == id);

            return _TBLModuloReclamosSolucionesRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloReclamos_Soluciones> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloReclamos_Soluciones> specification = new DirectSpecification<TBL_ModuloReclamos_Soluciones>(u => u.IsActive == isActive);
            return _TBLModuloReclamosSolucionesRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloReclamos_Soluciones> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloReclamos_Soluciones> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_Soluciones>(u => u.IsActive);

            return _TBLModuloReclamosSolucionesRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
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

            if (_TBLModuloReclamosSolucionesRepository != null)
            {
                _TBLModuloReclamosSolucionesRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public TBL_ModuloReclamos_Soluciones GetById(decimal id)
        {
            Specification<TBL_ModuloReclamos_Soluciones> spec = new DirectSpecification<TBL_ModuloReclamos_Soluciones>(u => u.IdSolucion == id);

            return _TBLModuloReclamosSolucionesRepository.GetCompleteEntityBySpec(spec);
        }

        public List<TBL_ModuloReclamos_Soluciones> GetByIdReclamo(decimal idReclamo)
        {
            Specification<TBL_ModuloReclamos_Soluciones> spec = new DirectSpecification<TBL_ModuloReclamos_Soluciones>(u => u.IdReclamo == idReclamo);

            return _TBLModuloReclamosSolucionesRepository.GetCompleteListBySpec(spec);
        }
    }
}
    