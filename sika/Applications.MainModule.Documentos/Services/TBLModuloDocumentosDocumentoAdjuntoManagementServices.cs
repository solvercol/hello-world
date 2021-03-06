using System;
using System.Collections.Generic;
using System.Linq;
using Application.MainModule.Documentos.IServices;
using Domain.MainModule.Documentos.Contracts;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Application.MainModule.Documentos.Services
{
    public class SfTBL_ModuloDocumentos_DocumentoAdjuntoManagementServices : ISfTBL_ModuloDocumentos_DocumentoAdjuntoManagementServices
    {

         #region Fields
         readonly ITBL_ModuloDocumentos_DocumentoAdjuntoRepository _TBLModuloDocumentosDocumentoAdjuntoRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloDocumentos_DocumentoAdjuntoManagementServices( ITBL_ModuloDocumentos_DocumentoAdjuntoRepository TBLModuloDocumentosDocumentoAdjuntoRepository)
         {
            if (TBLModuloDocumentosDocumentoAdjuntoRepository == null)
                throw new ArgumentNullException("TBLModuloDocumentosDocumentoAdjuntoRepository");
            _TBLModuloDocumentosDocumentoAdjuntoRepository = TBLModuloDocumentosDocumentoAdjuntoRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloDocumentos_DocumentoAdjunto NewEntity()
         {
            return new TBL_ModuloDocumentos_DocumentoAdjunto();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloDocumentos_DocumentoAdjunto entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloDocumentosDocumentoAdjuntoRepository.UnitOfWork;
            _TBLModuloDocumentosDocumentoAdjuntoRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloDocumentos_DocumentoAdjunto entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloDocumentosDocumentoAdjuntoRepository.UnitOfWork;
            _TBLModuloDocumentosDocumentoAdjuntoRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloDocumentos_DocumentoAdjunto entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloDocumentosDocumentoAdjuntoRepository.UnitOfWork;

            _TBLModuloDocumentosDocumentoAdjuntoRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloDocumentos_DocumentoAdjunto FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloDocumentos_DocumentoAdjunto> specification = new DirectSpecification<TBL_ModuloDocumentos_DocumentoAdjunto>(u => u.IdDocumentoAdjunto == id);

            return _TBLModuloDocumentosDocumentoAdjuntoRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloDocumentos_DocumentoAdjunto FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloDocumentos_DocumentoAdjunto> specification = new DirectSpecification<TBL_ModuloDocumentos_DocumentoAdjunto>(u => u.Code == id);

            return _TBLModuloDocumentosDocumentoAdjuntoRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloDocumentos_DocumentoAdjunto> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloDocumentos_DocumentoAdjunto> specification = new DirectSpecification<TBL_ModuloDocumentos_DocumentoAdjunto>(u => u.IsActive == isActive);
            return _TBLModuloDocumentosDocumentoAdjuntoRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloDocumentos_DocumentoAdjunto> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloDocumentos_DocumentoAdjunto> onlyEnabledSpec = new DirectSpecification<TBL_ModuloDocumentos_DocumentoAdjunto>(u => u.IsActive);

            return _TBLModuloDocumentosDocumentoAdjuntoRepository.GetPagedElements(pageIndex, pageCount, u => u.CrateOn, onlyEnabledSpec, true).ToList();
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

            if (_TBLModuloDocumentosDocumentoAdjuntoRepository != null)
            {
                _TBLModuloDocumentosDocumentoAdjuntoRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<TBL_ModuloDocumentos_DocumentoAdjunto> GetDocumentosAdjuntosByDocId(int idDocumento)
        {
            Specification<TBL_ModuloDocumentos_DocumentoAdjunto> specification = new DirectSpecification<TBL_ModuloDocumentos_DocumentoAdjunto>(u => u.IdDocumento == idDocumento);
            return _TBLModuloDocumentosDocumentoAdjuntoRepository.GetBySpec(specification).ToList();
        }
    }
}
    