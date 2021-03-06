using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Reclamos.Contracts;
using Application.MainModule.Reclamos.IServices;

namespace Application.MainModule.Reclamos.Services
{
    public class SfTBL_ModuloReclamos_AnexosAlternativaManagementServices : ISfTBL_ModuloReclamos_AnexosAlternativaManagementServices
    {

         #region Fields
         readonly ITBL_ModuloReclamos_AnexosAlternativaRepository _TBLModuloReclamosAnexosAlternativaRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloReclamos_AnexosAlternativaManagementServices( ITBL_ModuloReclamos_AnexosAlternativaRepository TBLModuloReclamosAnexosAlternativaRepository)
         {
            if (TBLModuloReclamosAnexosAlternativaRepository == null)
                throw new ArgumentNullException("TBLModuloReclamosAnexosAlternativaRepository");
            _TBLModuloReclamosAnexosAlternativaRepository = TBLModuloReclamosAnexosAlternativaRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloReclamos_AnexosAlternativa NewEntity()
         {
            return new TBL_ModuloReclamos_AnexosAlternativa();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloReclamos_AnexosAlternativa entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosAnexosAlternativaRepository.UnitOfWork;
            _TBLModuloReclamosAnexosAlternativaRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloReclamos_AnexosAlternativa entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloReclamosAnexosAlternativaRepository.UnitOfWork;
            _TBLModuloReclamosAnexosAlternativaRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloReclamos_AnexosAlternativa entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosAnexosAlternativaRepository.UnitOfWork;

            _TBLModuloReclamosAnexosAlternativaRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloReclamos_AnexosAlternativa FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_AnexosAlternativa> specification = new DirectSpecification<TBL_ModuloReclamos_AnexosAlternativa>(u => u.IdAnexoAlternativa == id);

            return _TBLModuloReclamosAnexosAlternativaRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloReclamos_AnexosAlternativa FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloReclamos_AnexosAlternativa> specification = new DirectSpecification<TBL_ModuloReclamos_AnexosAlternativa>(u => u.Code == id);

            return _TBLModuloReclamosAnexosAlternativaRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloReclamos_AnexosAlternativa> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloReclamos_AnexosAlternativa> specification = new DirectSpecification<TBL_ModuloReclamos_AnexosAlternativa>(u => u.IsActive == isActive);
            return _TBLModuloReclamosAnexosAlternativaRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloReclamos_AnexosAlternativa> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloReclamos_AnexosAlternativa> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_AnexosAlternativa>(u => u.IsActive);

            return _TBLModuloReclamosAnexosAlternativaRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
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

            if (_TBLModuloReclamosAnexosAlternativaRepository != null)
            {
                _TBLModuloReclamosAnexosAlternativaRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<TBL_ModuloReclamos_AnexosAlternativa> GetByIdAlternativa(decimal idAlternativa)
        {
            Specification<TBL_ModuloReclamos_AnexosAlternativa> spec = new DirectSpecification<TBL_ModuloReclamos_AnexosAlternativa>(u => u.IdAlternativa == idAlternativa);

            return _TBLModuloReclamosAnexosAlternativaRepository.GetBySpec(spec).ToList();
        }

        public TBL_ModuloReclamos_AnexosAlternativa GetById(decimal idArchivo)
        {
            Specification<TBL_ModuloReclamos_AnexosAlternativa> spec = new DirectSpecification<TBL_ModuloReclamos_AnexosAlternativa>(u => u.IdAnexoAlternativa == idArchivo);

            return _TBLModuloReclamosAnexosAlternativaRepository.GetEntityBySpec(spec);
        }
    }
}
    