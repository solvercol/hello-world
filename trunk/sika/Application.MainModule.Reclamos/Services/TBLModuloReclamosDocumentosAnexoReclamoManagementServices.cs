using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Reclamos.Contracts;
using Application.MainModule.Reclamos.IServices;

namespace Application.MainModule.Reclamos.Services
{
    public class SfTBL_ModuloReclamos_DocumentosAnexoReclamoManagementServices : ISfTBL_ModuloReclamos_DocumentosAnexoReclamoManagementServices
    {
         #region Fields
         readonly ITBL_ModuloReclamos_DocumentosAnexoReclamoRepository _DocumentosAnexoContratoRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloReclamos_DocumentosAnexoReclamoManagementServices(ITBL_ModuloReclamos_DocumentosAnexoReclamoRepository DocumentosAnexoContratoRepository)
         {
            if (DocumentosAnexoContratoRepository == null)
                throw new ArgumentNullException("DocumentosAnexoContratoRepository");
            _DocumentosAnexoContratoRepository = DocumentosAnexoContratoRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloReclamos_DocumentosAnexoReclamo NewEntity()
         {
             return new TBL_ModuloReclamos_DocumentosAnexoReclamo();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloReclamos_DocumentosAnexoReclamo entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _DocumentosAnexoContratoRepository.UnitOfWork;
            _DocumentosAnexoContratoRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloReclamos_DocumentosAnexoReclamo entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _DocumentosAnexoContratoRepository.UnitOfWork;
            _DocumentosAnexoContratoRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloReclamos_DocumentosAnexoReclamo entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _DocumentosAnexoContratoRepository.UnitOfWork;

            _DocumentosAnexoContratoRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloReclamos_DocumentosAnexoReclamo FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_DocumentosAnexoReclamo> specification = new DirectSpecification<TBL_ModuloReclamos_DocumentosAnexoReclamo>(u => u.IdDocumentoReclamo == Guid.NewGuid());

            return _DocumentosAnexoContratoRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public DocumentosAnexoContrato FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<DocumentosAnexoContrato> specification = new DirectSpecification<DocumentosAnexoContrato>(u => u.Code == id);

            return _DocumentosAnexoContratoRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloReclamos_DocumentosAnexoReclamo> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloReclamos_DocumentosAnexoReclamo> specification = new DirectSpecification<TBL_ModuloReclamos_DocumentosAnexoReclamo>(u => u.IsActive == isActive);
            return _DocumentosAnexoContratoRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloReclamos_DocumentosAnexoReclamo> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloReclamos_DocumentosAnexoReclamo> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_DocumentosAnexoReclamo>(u => u.IsActive);

            return _DocumentosAnexoContratoRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
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

            if (_DocumentosAnexoContratoRepository != null)
            {
                _DocumentosAnexoContratoRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<TBL_ModuloReclamos_DocumentosAnexoReclamo> GetAnexosByReclamoCategoria(int idReclamo, string categoria)
        {
            Specification<TBL_ModuloReclamos_DocumentosAnexoReclamo> specification = new DirectSpecification<TBL_ModuloReclamos_DocumentosAnexoReclamo>(u => u.IdReclamo == idReclamo);

            if (!string.IsNullOrEmpty(categoria))
            {
                specification &= new DirectSpecification<TBL_ModuloReclamos_DocumentosAnexoReclamo>(u => u.Categoria == categoria);
            }

            return _DocumentosAnexoContratoRepository.GetCompleteEntityList(specification);
        }

        public TBL_ModuloReclamos_DocumentosAnexoReclamo GetById(Guid id)
        {
            Specification<TBL_ModuloReclamos_DocumentosAnexoReclamo> specification = new DirectSpecification<TBL_ModuloReclamos_DocumentosAnexoReclamo>(u => u.IdDocumentoReclamo == id);
            
            return _DocumentosAnexoContratoRepository.GetCompleteEntity(specification);
        }
    }
}