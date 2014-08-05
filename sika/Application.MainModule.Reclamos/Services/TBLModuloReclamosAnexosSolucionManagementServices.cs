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
    public class SfTBL_ModuloReclamos_AnexosSolucionManagementServices : ISfTBL_ModuloReclamos_AnexosSolucionManagementServices
    {

         #region Fields
         readonly ITBL_ModuloReclamos_AnexosSolucionRepository _TBLModuloReclamosAnexosSolucionRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloReclamos_AnexosSolucionManagementServices( ITBL_ModuloReclamos_AnexosSolucionRepository TBLModuloReclamosAnexosSolucionRepository)
         {
            if (TBLModuloReclamosAnexosSolucionRepository == null)
                throw new ArgumentNullException("TBLModuloReclamosAnexosSolucionRepository");
            _TBLModuloReclamosAnexosSolucionRepository = TBLModuloReclamosAnexosSolucionRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloReclamos_AnexosSolucion NewEntity()
         {
            return new TBL_ModuloReclamos_AnexosSolucion();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloReclamos_AnexosSolucion entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosAnexosSolucionRepository.UnitOfWork;
            _TBLModuloReclamosAnexosSolucionRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloReclamos_AnexosSolucion entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloReclamosAnexosSolucionRepository.UnitOfWork;
            _TBLModuloReclamosAnexosSolucionRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloReclamos_AnexosSolucion entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosAnexosSolucionRepository.UnitOfWork;

            _TBLModuloReclamosAnexosSolucionRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloReclamos_AnexosSolucion FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_AnexosSolucion> specification = new DirectSpecification<TBL_ModuloReclamos_AnexosSolucion>(u => u.IdAnexoSolucion == id);

            return _TBLModuloReclamosAnexosSolucionRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloReclamos_AnexosSolucion FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloReclamos_AnexosSolucion> specification = new DirectSpecification<TBL_ModuloReclamos_AnexosSolucion>(u => u.Code == id);

            return _TBLModuloReclamosAnexosSolucionRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloReclamos_AnexosSolucion> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloReclamos_AnexosSolucion> specification = new DirectSpecification<TBL_ModuloReclamos_AnexosSolucion>(u => u.IsActive == isActive);
            return _TBLModuloReclamosAnexosSolucionRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloReclamos_AnexosSolucion> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloReclamos_AnexosSolucion> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_AnexosSolucion>(u => u.IsActive);

            return _TBLModuloReclamosAnexosSolucionRepository.GetPagedElements(pageIndex, pageCount, u => u.CreateOn, onlyEnabledSpec, true).ToList();
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

            if (_TBLModuloReclamosAnexosSolucionRepository != null)
            {
                _TBLModuloReclamosAnexosSolucionRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    