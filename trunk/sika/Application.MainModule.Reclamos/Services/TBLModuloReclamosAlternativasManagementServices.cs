using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Reclamos.Contracts;
using Application.MainModule.Reclamos.IServices;

namespace Application.MainModule.Reclamos.Services
{
    public class SfTBL_ModuloReclamos_AlternativasManagementServices : ISfTBL_ModuloReclamos_AlternativasManagementServices
    {

         #region Fields
         readonly ITBL_ModuloReclamos_AlternativasRepository _TBLModuloReclamosAlternativasRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloReclamos_AlternativasManagementServices( ITBL_ModuloReclamos_AlternativasRepository TBLModuloReclamosAlternativasRepository)
         {
            if (TBLModuloReclamosAlternativasRepository == null)
                throw new ArgumentNullException("TBLModuloReclamosAlternativasRepository");
            _TBLModuloReclamosAlternativasRepository = TBLModuloReclamosAlternativasRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloReclamos_Alternativas NewEntity()
         {
            return new TBL_ModuloReclamos_Alternativas();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloReclamos_Alternativas entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosAlternativasRepository.UnitOfWork;
            _TBLModuloReclamosAlternativasRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloReclamos_Alternativas entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloReclamosAlternativasRepository.UnitOfWork;
            _TBLModuloReclamosAlternativasRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloReclamos_Alternativas entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosAlternativasRepository.UnitOfWork;

            _TBLModuloReclamosAlternativasRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloReclamos_Alternativas FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_Alternativas> specification = new DirectSpecification<TBL_ModuloReclamos_Alternativas>(u => u.IdAlternativa == id);

            return _TBLModuloReclamosAlternativasRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloReclamos_Alternativas FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloReclamos_Alternativas> specification = new DirectSpecification<TBL_ModuloReclamos_Alternativas>(u => u.Code == id);

            return _TBLModuloReclamosAlternativasRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloReclamos_Alternativas> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloReclamos_Alternativas> specification = new DirectSpecification<TBL_ModuloReclamos_Alternativas>(u => u.IsActive == isActive);
            return _TBLModuloReclamosAlternativasRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloReclamos_Alternativas> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloReclamos_Alternativas> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_Alternativas>(u => u.IsActive);

            return _TBLModuloReclamosAlternativasRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
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

            if (_TBLModuloReclamosAlternativasRepository != null)
            {
                _TBLModuloReclamosAlternativasRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public TBL_ModuloReclamos_Alternativas GetById(decimal id)
        {
            Specification<TBL_ModuloReclamos_Alternativas> spec = new DirectSpecification<TBL_ModuloReclamos_Alternativas>(u => u.IdAlternativa == id);

            return _TBLModuloReclamosAlternativasRepository.GetCompleteEntityBySpec(spec);
        }

        public List<TBL_ModuloReclamos_Alternativas> GetByIdReclamo(decimal idReclamo)
        {
            Specification<TBL_ModuloReclamos_Alternativas> spec = new DirectSpecification<TBL_ModuloReclamos_Alternativas>(u => u.IdReclamo == idReclamo);

            return _TBLModuloReclamosAlternativasRepository.GetCompleteListBySpec(spec);
        }
    }
}
    