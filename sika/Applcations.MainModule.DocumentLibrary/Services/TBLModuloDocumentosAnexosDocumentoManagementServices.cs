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
using System.Transactions;
using Applcations.MainModule.DocumentLibrary.IServices;
using Domain.MainModule.DocumentLibrary.Contracts;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Applcations.MainModule.DocumentLibrary.Services
{
    public class SfTBL_ModuloDocumentosAnexos_DocumentoManagementServices : ISfTBL_ModuloDocumentosAnexos_DocumentoManagementServices
    {

         #region Fields
         readonly ITBL_ModuloDocumentosAnexos_DocumentoRepository _tblModuloDocumentosAnexosDocumentoRepository;
        private readonly ITBL_ModuloDocumentosAnexos_ContenidoRepository _contentRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloDocumentosAnexos_DocumentoManagementServices( ITBL_ModuloDocumentosAnexos_DocumentoRepository tblModuloDocumentosAnexosDocumentoRepository, ITBL_ModuloDocumentosAnexos_ContenidoRepository contentRepository)
         {
            if (tblModuloDocumentosAnexosDocumentoRepository == null)
                throw new ArgumentNullException("tblModuloDocumentosAnexosDocumentoRepository");
            _tblModuloDocumentosAnexosDocumentoRepository = tblModuloDocumentosAnexosDocumentoRepository;
             _contentRepository = contentRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloDocumentosAnexos_Documento NewEntity()
         {
            return new TBL_ModuloDocumentosAnexos_Documento();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloDocumentosAnexos_Documento entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblModuloDocumentosAnexosDocumentoRepository.UnitOfWork;
            _tblModuloDocumentosAnexosDocumentoRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloDocumentosAnexos_Documento entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _tblModuloDocumentosAnexosDocumentoRepository.UnitOfWork;
            _tblModuloDocumentosAnexosDocumentoRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloDocumentosAnexos_Documento entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblModuloDocumentosAnexosDocumentoRepository.UnitOfWork;

            _tblModuloDocumentosAnexosDocumentoRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloDocumentosAnexos_Documento FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloDocumentosAnexos_Documento> specification = new DirectSpecification<TBL_ModuloDocumentosAnexos_Documento>(u => u.IdDocumento == id);

            return _tblModuloDocumentosAnexosDocumentoRepository.GetEntityBySpec(specification);
           
         }

	
		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloDocumentosAnexos_Documento> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloDocumentosAnexos_Documento> specification = new DirectSpecification<TBL_ModuloDocumentosAnexos_Documento>(u => u.IsActive == isActive);
            return _tblModuloDocumentosAnexosDocumentoRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloDocumentosAnexos_Documento> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloDocumentosAnexos_Documento> onlyEnabledSpec = new DirectSpecification<TBL_ModuloDocumentosAnexos_Documento>(u => u.IsActive);

            return _tblModuloDocumentosAnexosDocumentoRepository.GetPagedElements(pageIndex, pageCount, u => u.IdDocumento, onlyEnabledSpec, true).ToList();
         }


         public bool SaveDocument(int idFolder, TBL_Admin_Usuarios user, string nameFile, string comentarios, byte[] adjunto, string contentType)
         {

             var txSettings = new TransactionOptions()
             {
                 Timeout = TransactionManager.DefaultTimeout,
                 IsolationLevel = IsolationLevel.Serializable
             };

             using (var scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
             {
                 var unitOfWork = _tblModuloDocumentosAnexosDocumentoRepository.UnitOfWork;

                 var oDoc = NewEntity();
                 oDoc.IdEstado = 1;
                 oDoc.Nombre = nameFile;
                 oDoc.IdFolder = idFolder;
                 oDoc.OwnerId = user.IdUser;
                 oDoc.CreatedBy = user.Nombres;
                 oDoc.CreatedOn = DateTime.Now;
                 oDoc.ModifiedBy = user.Nombres;
                 oDoc.ModifiedOn = DateTime.Now;

                 oDoc.TBL_ModuloDocumentosAnexos_Contenido.Add(AddContentDocument(adjunto, comentarios, contentType, user.Nombres,
                                                                            nameFile));

                 _tblModuloDocumentosAnexosDocumentoRepository.Add(oDoc);

                 unitOfWork.Commit();

                 scope.Complete();

                 return true;
             }

         }

         private static TBL_ModuloDocumentosAnexos_Contenido AddContentDocument(byte[] attach, string comentarios, string contentType, string nombreAutor, string nameFile)
         {
             var oContent = new TBL_ModuloDocumentosAnexos_Contenido
             {
                 Adjunto = attach,
                 Comentarios = comentarios,
                 contentTypeC = contentType,
                 CreatedBy = nombreAutor,
                 CreatedOn = DateTime.Now,
                 IsActive = true,
                 ModifiedBy = nombreAutor,
                 ModifiedOn = DateTime.Now,
                 Nombre = nameFile,
                 Revision = 1
             };
             return oContent;
         }

         public bool BulkDeleteFromId(int id)
         {
             var specification = new DirectSpecification<TBL_ModuloDocumentosAnexos_Documento>(u => u.IdDocumento == id);
             var res = _tblModuloDocumentosAnexosDocumentoRepository.BulkDeletebySpec(specification);
             return res > 0;
         }


        public void DeleteDocumentAndContent( Dictionary<string,string > parameters)
        {
            var txSettings = new TransactionOptions()
            {
                Timeout = TransactionManager.DefaultTimeout,
                IsolationLevel = IsolationLevel.Serializable
            };

            using (var scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                var unitOfWork = _tblModuloDocumentosAnexosDocumentoRepository.UnitOfWork;

                foreach (var parameter in parameters)
                {

                    var result = DeleteContent(Convert.ToInt32(parameter.Value));
                    if(result)
                        BulkDeleteFromId(Convert.ToInt32(parameter.Key));
                }

                unitOfWork.Commit();

                scope.Complete();
            }
        }

        /// <summary>
        /// Elimina el contenido asociado al documento
        /// </summary>
        /// <param name="idcontent"></param>
        /// <returns></returns>
        private bool DeleteContent(int idcontent)
        {
           
            var specification = new DirectSpecification<TBL_ModuloDocumentosAnexos_Contenido>(u => u.IdContenido == idcontent);
            var res = _contentRepository.BulkDeletebySpec(specification);
            return res > 0;
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

            if (_tblModuloDocumentosAnexosDocumentoRepository != null)
            {
                _tblModuloDocumentosAnexosDocumentoRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    