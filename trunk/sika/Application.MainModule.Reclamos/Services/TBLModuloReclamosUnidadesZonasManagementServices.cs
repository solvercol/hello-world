using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Reclamos.Contracts;
using Application.MainModule.Reclamos.IServices;
using Domain.MainModule.Reclamos.Spec;

namespace Application.MainModule.Reclamos.Services
{
    public class SfTBL_ModuloReclamos_UnidadesZonasManagementServices : ISfTBL_ModuloReclamos_UnidadesZonasManagementServices
    {

         #region Fields
         readonly ITBL_ModuloReclamos_UnidadesZonasRepository _TBLModuloReclamosUnidadesZonasRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloReclamos_UnidadesZonasManagementServices( ITBL_ModuloReclamos_UnidadesZonasRepository TBLModuloReclamosUnidadesZonasRepository)
         {
            if (TBLModuloReclamosUnidadesZonasRepository == null)
                throw new ArgumentNullException("TBLModuloReclamosUnidadesZonasRepository");
            _TBLModuloReclamosUnidadesZonasRepository = TBLModuloReclamosUnidadesZonasRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloReclamos_UnidadesZonas NewEntity()
         {
            return new TBL_ModuloReclamos_UnidadesZonas();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloReclamos_UnidadesZonas entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosUnidadesZonasRepository.UnitOfWork;
            _TBLModuloReclamosUnidadesZonasRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloReclamos_UnidadesZonas entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloReclamosUnidadesZonasRepository.UnitOfWork;
            _TBLModuloReclamosUnidadesZonasRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloReclamos_UnidadesZonas entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosUnidadesZonasRepository.UnitOfWork;

            _TBLModuloReclamosUnidadesZonasRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloReclamos_UnidadesZonas FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_UnidadesZonas> specification = new DirectSpecification<TBL_ModuloReclamos_UnidadesZonas>(u => u.IdUnidad == id);

            return _TBLModuloReclamosUnidadesZonasRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloReclamos_UnidadesZonas FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloReclamos_UnidadesZonas> specification = new DirectSpecification<TBL_ModuloReclamos_UnidadesZonas>(u => u.Code == id);

            return _TBLModuloReclamosUnidadesZonasRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloReclamos_UnidadesZonas> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloReclamos_UnidadesZonas> specification = new DirectSpecification<TBL_ModuloReclamos_UnidadesZonas>(u => u.IsActive == isActive);
            return _TBLModuloReclamosUnidadesZonasRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloReclamos_UnidadesZonas> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloReclamos_UnidadesZonas> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_UnidadesZonas>(u => u.IsActive);

            return _TBLModuloReclamosUnidadesZonasRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
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

            if (_TBLModuloReclamosUnidadesZonasRepository != null)
            {
                _TBLModuloReclamosUnidadesZonasRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public TBL_ModuloReclamos_UnidadesZonas GetByUnidadZona(string unidad, string zona)
        {
            var spec = TBL_ModuloReclamos_UnidadesZonas_Spec.SpecByUnidadZona(unidad, zona);

            return _TBLModuloReclamosUnidadesZonasRepository.GetEntityBySpec(spec);
        }
    }
}
    