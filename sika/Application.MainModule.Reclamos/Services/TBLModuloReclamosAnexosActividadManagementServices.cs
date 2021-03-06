using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Reclamos.Contracts;
using Application.MainModule.Reclamos.IServices;

namespace Application.MainModule.Reclamos.Services
{
    public class SfTBL_ModuloReclamos_AnexosActividadManagementServices : ISfTBL_ModuloReclamos_AnexosActividadManagementServices
    {

         #region Fields
         readonly ITBL_ModuloReclamos_AnexosActividadRepository _TBLModuloReclamosAnexosActividadRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloReclamos_AnexosActividadManagementServices( ITBL_ModuloReclamos_AnexosActividadRepository TBLModuloReclamosAnexosActividadRepository)
         {
            if (TBLModuloReclamosAnexosActividadRepository == null)
                throw new ArgumentNullException("TBLModuloReclamosAnexosActividadRepository");
            _TBLModuloReclamosAnexosActividadRepository = TBLModuloReclamosAnexosActividadRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloReclamos_AnexosActividad NewEntity()
         {
            return new TBL_ModuloReclamos_AnexosActividad();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloReclamos_AnexosActividad entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosAnexosActividadRepository.UnitOfWork;
            _TBLModuloReclamosAnexosActividadRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloReclamos_AnexosActividad entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloReclamosAnexosActividadRepository.UnitOfWork;
            _TBLModuloReclamosAnexosActividadRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloReclamos_AnexosActividad entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosAnexosActividadRepository.UnitOfWork;

            _TBLModuloReclamosAnexosActividadRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloReclamos_AnexosActividad FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_AnexosActividad> specification = new DirectSpecification<TBL_ModuloReclamos_AnexosActividad>(u => u.IdAnexoActividad == id);

            return _TBLModuloReclamosAnexosActividadRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloReclamos_AnexosActividad FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloReclamos_AnexosActividad> specification = new DirectSpecification<TBL_ModuloReclamos_AnexosActividad>(u => u.Code == id);

            return _TBLModuloReclamosAnexosActividadRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloReclamos_AnexosActividad> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloReclamos_AnexosActividad> specification = new DirectSpecification<TBL_ModuloReclamos_AnexosActividad>(u => u.IsActive == isActive);
            return _TBLModuloReclamosAnexosActividadRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloReclamos_AnexosActividad> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloReclamos_AnexosActividad> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_AnexosActividad>(u => u.IsActive);

            return _TBLModuloReclamosAnexosActividadRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
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

            if (_TBLModuloReclamosAnexosActividadRepository != null)
            {
                _TBLModuloReclamosAnexosActividadRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<TBL_ModuloReclamos_AnexosActividad> GetByIdActividad(decimal idActividad)
        {
            Specification<TBL_ModuloReclamos_AnexosActividad> spec = new DirectSpecification<TBL_ModuloReclamos_AnexosActividad>(u => u.IdActividad == idActividad);

            return _TBLModuloReclamosAnexosActividadRepository.GetBySpec(spec).ToList();
        }

        public TBL_ModuloReclamos_AnexosActividad GetById(decimal idArchivo)
        {
            Specification<TBL_ModuloReclamos_AnexosActividad> spec = new DirectSpecification<TBL_ModuloReclamos_AnexosActividad>(u => u.IdAnexoActividad == idArchivo);

            return _TBLModuloReclamosAnexosActividadRepository.GetEntityBySpec(spec);
        }
    }
}
    