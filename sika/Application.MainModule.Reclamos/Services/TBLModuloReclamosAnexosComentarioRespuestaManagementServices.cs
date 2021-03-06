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
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Reclamos.Contracts;
using Application.MainModule.Reclamos.IServices;

namespace Application.MainModule.Reclamos.Services
{
    public class SfTBL_ModuloReclamos_AnexosComentarioRespuestaManagementServices : ISfTBL_ModuloReclamos_AnexosComentarioRespuestaManagementServices
    {

         #region Fields
         readonly ITBL_ModuloReclamos_AnexosComentarioRespuestaRepository _TBLModuloReclamosAnexosComentarioRespuestaRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloReclamos_AnexosComentarioRespuestaManagementServices( ITBL_ModuloReclamos_AnexosComentarioRespuestaRepository TBLModuloReclamosAnexosComentarioRespuestaRepository)
         {
            if (TBLModuloReclamosAnexosComentarioRespuestaRepository == null)
                throw new ArgumentNullException("TBLModuloReclamosAnexosComentarioRespuestaRepository");
            _TBLModuloReclamosAnexosComentarioRespuestaRepository = TBLModuloReclamosAnexosComentarioRespuestaRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloReclamos_AnexosComentarioRespuesta NewEntity()
         {
            return new TBL_ModuloReclamos_AnexosComentarioRespuesta();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloReclamos_AnexosComentarioRespuesta entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosAnexosComentarioRespuestaRepository.UnitOfWork;
            _TBLModuloReclamosAnexosComentarioRespuestaRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloReclamos_AnexosComentarioRespuesta entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloReclamosAnexosComentarioRespuestaRepository.UnitOfWork;
            _TBLModuloReclamosAnexosComentarioRespuestaRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloReclamos_AnexosComentarioRespuesta entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosAnexosComentarioRespuestaRepository.UnitOfWork;

            _TBLModuloReclamosAnexosComentarioRespuestaRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloReclamos_AnexosComentarioRespuesta FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_AnexosComentarioRespuesta> specification = new DirectSpecification<TBL_ModuloReclamos_AnexosComentarioRespuesta>(u => u.IdAnexoComentarioRespuesta == id);

            return _TBLModuloReclamosAnexosComentarioRespuestaRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloReclamos_AnexosComentarioRespuesta FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloReclamos_AnexosComentarioRespuesta> specification = new DirectSpecification<TBL_ModuloReclamos_AnexosComentarioRespuesta>(u => u.Code == id);

            return _TBLModuloReclamosAnexosComentarioRespuestaRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloReclamos_AnexosComentarioRespuesta> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloReclamos_AnexosComentarioRespuesta> specification = new DirectSpecification<TBL_ModuloReclamos_AnexosComentarioRespuesta>(u => u.IsActive == isActive);
            return _TBLModuloReclamosAnexosComentarioRespuestaRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloReclamos_AnexosComentarioRespuesta> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloReclamos_AnexosComentarioRespuesta> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_AnexosComentarioRespuesta>(u => u.IsActive);

            return _TBLModuloReclamosAnexosComentarioRespuestaRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
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

            if (_TBLModuloReclamosAnexosComentarioRespuestaRepository != null)
            {
                _TBLModuloReclamosAnexosComentarioRespuestaRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public TBL_ModuloReclamos_AnexosComentarioRespuesta GetById(decimal id)
        {
            Specification<TBL_ModuloReclamos_AnexosComentarioRespuesta> spec = new DirectSpecification<TBL_ModuloReclamos_AnexosComentarioRespuesta>(u => u.IdAnexoComentarioRespuesta == id);

            return _TBLModuloReclamosAnexosComentarioRespuestaRepository.GetEntityBySpec(spec);
        }

        public List<TBL_ModuloReclamos_AnexosComentarioRespuesta> GetByComentarioId(decimal idComentario)
        {
            Specification<TBL_ModuloReclamos_AnexosComentarioRespuesta> spec = new DirectSpecification<TBL_ModuloReclamos_AnexosComentarioRespuesta>(u => u.IdComentarioRespuesta == idComentario);

            return _TBLModuloReclamosAnexosComentarioRespuestaRepository.GetBySpec(spec).ToList();
        }
    }
}
    