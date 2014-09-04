using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Reclamos.Contracts;
using Application.MainModule.Reclamos.IServices;

namespace Application.MainModule.Reclamos.Services
{
    public class SfTBL_ModuloReclamos_ComentariosRespuestaManagementServices : ISfTBL_ModuloReclamos_ComentariosRespuestaManagementServices
    {

         #region Fields
         readonly ITBL_ModuloReclamos_ComentariosRespuestaRepository _TBLModuloReclamosComentariosRespuestaRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloReclamos_ComentariosRespuestaManagementServices( ITBL_ModuloReclamos_ComentariosRespuestaRepository TBLModuloReclamosComentariosRespuestaRepository)
         {
            if (TBLModuloReclamosComentariosRespuestaRepository == null)
                throw new ArgumentNullException("TBLModuloReclamosComentariosRespuestaRepository");
            _TBLModuloReclamosComentariosRespuestaRepository = TBLModuloReclamosComentariosRespuestaRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloReclamos_ComentariosRespuesta NewEntity()
         {
            return new TBL_ModuloReclamos_ComentariosRespuesta();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloReclamos_ComentariosRespuesta entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosComentariosRespuestaRepository.UnitOfWork;
            _TBLModuloReclamosComentariosRespuestaRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloReclamos_ComentariosRespuesta entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloReclamosComentariosRespuestaRepository.UnitOfWork;
            _TBLModuloReclamosComentariosRespuestaRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloReclamos_ComentariosRespuesta entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosComentariosRespuestaRepository.UnitOfWork;

            _TBLModuloReclamosComentariosRespuestaRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloReclamos_ComentariosRespuesta FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_ComentariosRespuesta> specification = new DirectSpecification<TBL_ModuloReclamos_ComentariosRespuesta>(u => u.IdComentario == id);

            return _TBLModuloReclamosComentariosRespuestaRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloReclamos_ComentariosRespuesta FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloReclamos_ComentariosRespuesta> specification = new DirectSpecification<TBL_ModuloReclamos_ComentariosRespuesta>(u => u.Code == id);

            return _TBLModuloReclamosComentariosRespuestaRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloReclamos_ComentariosRespuesta> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloReclamos_ComentariosRespuesta> specification = new DirectSpecification<TBL_ModuloReclamos_ComentariosRespuesta>(u => u.IsActive == isActive);
            return _TBLModuloReclamosComentariosRespuestaRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloReclamos_ComentariosRespuesta> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloReclamos_ComentariosRespuesta> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_ComentariosRespuesta>(u => u.IsActive);

            return _TBLModuloReclamosComentariosRespuestaRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
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

            if (_TBLModuloReclamosComentariosRespuestaRepository != null)
            {
                _TBLModuloReclamosComentariosRespuestaRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public TBL_ModuloReclamos_ComentariosRespuesta GetById(decimal id)
        {

            Specification<TBL_ModuloReclamos_ComentariosRespuesta> spec = new DirectSpecification<TBL_ModuloReclamos_ComentariosRespuesta>(u => u.IdComentario == id);

            return _TBLModuloReclamosComentariosRespuestaRepository.GetCompleteEntityBySpec(spec);
        }

        public List<TBL_ModuloReclamos_ComentariosRespuesta> GetByIdReclamo(decimal idReclamo)
        {
            Specification<TBL_ModuloReclamos_ComentariosRespuesta> spec = new DirectSpecification<TBL_ModuloReclamos_ComentariosRespuesta>(u => u.IdReclamo == idReclamo);

            return _TBLModuloReclamosComentariosRespuestaRepository.GetCompleteListBySpec(spec);
        }


        public List<TBL_ModuloReclamos_ComentariosRespuesta> GetByIdComentarioRelacionado(decimal idComentario)
        {
            Specification<TBL_ModuloReclamos_ComentariosRespuesta> spec = new DirectSpecification<TBL_ModuloReclamos_ComentariosRespuesta>(u => u.IdComentarioRelacionado == idComentario);

            return _TBLModuloReclamosComentariosRespuestaRepository.GetCompleteListBySpec(spec);
        }
    }
}
    