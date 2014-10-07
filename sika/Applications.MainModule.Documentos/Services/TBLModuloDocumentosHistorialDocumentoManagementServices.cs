//------------------------------------------------------------------------------
// <auto-generated>
//     Este codigo fue generado por el motor de generacion de codigo de propiedad de Walter molano.
//     El cambio  de algunas lineas de codigo podran causar comportamientos
//     inesperados de la aplicacion.  junio 18 de 2014.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 
using System;
using System.Collections.Generic;
using System.Linq;
using Application.MainModule.Documentos.IServices;
using Domain.MainModule.Documentos.Contracts;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Application.MainModule.Documentos.Services
{
    public class SfTBL_ModuloDocumentos_HistorialDocumentoManagementServices : ISfTBL_ModuloDocumentos_HistorialDocumentoManagementServices
    {

         #region Fields
         readonly ITBL_ModuloDocumentos_HistorialDocumentoRepository _tblModuloDocumentosHistorialDocumentoRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloDocumentos_HistorialDocumentoManagementServices( ITBL_ModuloDocumentos_HistorialDocumentoRepository tblModuloDocumentosHistorialDocumentoRepository)
         {
            if (tblModuloDocumentosHistorialDocumentoRepository == null)
                throw new ArgumentNullException("tblModuloDocumentosHistorialDocumentoRepository");
            _tblModuloDocumentosHistorialDocumentoRepository = tblModuloDocumentosHistorialDocumentoRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloDocumentos_HistorialDocumento NewEntity()
         {
            return new TBL_ModuloDocumentos_HistorialDocumento();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloDocumentos_HistorialDocumento entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblModuloDocumentosHistorialDocumentoRepository.UnitOfWork;
            _tblModuloDocumentosHistorialDocumentoRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloDocumentos_HistorialDocumento entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _tblModuloDocumentosHistorialDocumentoRepository.UnitOfWork;
            _tblModuloDocumentosHistorialDocumentoRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloDocumentos_HistorialDocumento entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblModuloDocumentosHistorialDocumentoRepository.UnitOfWork;

            _tblModuloDocumentosHistorialDocumentoRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloDocumentos_HistorialDocumento FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloDocumentos_HistorialDocumento> specification = new DirectSpecification<TBL_ModuloDocumentos_HistorialDocumento>(u => u.IdHistorial == id);

            return _tblModuloDocumentosHistorialDocumentoRepository.GetEntityBySpec(specification);
           
         }

         public TBL_ModuloDocumentos_HistorialDocumento GetHistorialByIdWithAttachments(int id)
         {
             if (id == 0)
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));
             Specification<TBL_ModuloDocumentos_HistorialDocumento> specification = new DirectSpecification<TBL_ModuloDocumentos_HistorialDocumento>(h => h.IdHistorial == id);
             return _tblModuloDocumentosHistorialDocumentoRepository.GetHistorialByIdWithAttachments(specification);
         }

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloDocumentos_HistorialDocumento> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloDocumentos_HistorialDocumento> specification = new DirectSpecification<TBL_ModuloDocumentos_HistorialDocumento>(u => u.IsActive == isActive);
            return _tblModuloDocumentosHistorialDocumentoRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloDocumentos_HistorialDocumento> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloDocumentos_HistorialDocumento> onlyEnabledSpec = new DirectSpecification<TBL_ModuloDocumentos_HistorialDocumento>(u => u.IsActive);

            return _tblModuloDocumentosHistorialDocumentoRepository.GetPagedElements(pageIndex, pageCount, u => u.IdHistorial, onlyEnabledSpec, true).ToList();
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

            if (_tblModuloDocumentosHistorialDocumentoRepository != null)
            {
                _tblModuloDocumentosHistorialDocumentoRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    