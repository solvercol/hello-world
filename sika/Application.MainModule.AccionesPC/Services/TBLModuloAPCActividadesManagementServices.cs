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
using Domain.MainModule.AccionesPC.Contracts;
using Application.MainModule.AccionesPC.IServices;

namespace Application.MainModule.AccionesPC.Services
{
    public class SfTBL_ModuloAPC_ActividadesManagementServices : ISfTBL_ModuloAPC_ActividadesManagementServices
    {

         #region Fields
         readonly ITBL_ModuloAPC_ActividadesRepository _TBLModuloAPCActividadesRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloAPC_ActividadesManagementServices( ITBL_ModuloAPC_ActividadesRepository TBLModuloAPCActividadesRepository)
         {
            if (TBLModuloAPCActividadesRepository == null)
                throw new ArgumentNullException("TBLModuloAPCActividadesRepository");
            _TBLModuloAPCActividadesRepository = TBLModuloAPCActividadesRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloAPC_Actividades NewEntity()
         {
            return new TBL_ModuloAPC_Actividades();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloAPC_Actividades entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloAPCActividadesRepository.UnitOfWork;
            _TBLModuloAPCActividadesRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloAPC_Actividades entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloAPCActividadesRepository.UnitOfWork;
            _TBLModuloAPCActividadesRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloAPC_Actividades entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloAPCActividadesRepository.UnitOfWork;

            _TBLModuloAPCActividadesRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloAPC_Actividades FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloAPC_Actividades> specification = new DirectSpecification<TBL_ModuloAPC_Actividades>(u => u.IdActividad == id);

            return _TBLModuloAPCActividadesRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloAPC_Actividades FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloAPC_Actividades> specification = new DirectSpecification<TBL_ModuloAPC_Actividades>(u => u.Code == id);

            return _TBLModuloAPCActividadesRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloAPC_Actividades> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloAPC_Actividades> specification = new DirectSpecification<TBL_ModuloAPC_Actividades>(u => u.IsActive == isActive);
            return _TBLModuloAPCActividadesRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloAPC_Actividades> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloAPC_Actividades> onlyEnabledSpec = new DirectSpecification<TBL_ModuloAPC_Actividades>(u => u.IsActive);

            return _TBLModuloAPCActividadesRepository.GetPagedElements(pageIndex, pageCount, u => u.IdActividad, onlyEnabledSpec, true).ToList();
         }

         public TBL_ModuloAPC_Actividades GetById(decimal id)
         {
             Specification<TBL_ModuloAPC_Actividades> spec = new DirectSpecification<TBL_ModuloAPC_Actividades>(u => u.IdActividad == id);

             return _TBLModuloAPCActividadesRepository.GetCompleteEntityBySpec(spec);
         }

         public List<TBL_ModuloAPC_Actividades> GetByIdSolicitud(decimal idSolicitud)
         {
             Specification<TBL_ModuloAPC_Actividades> spec = new DirectSpecification<TBL_ModuloAPC_Actividades>(u => u.IdSolicitudAPC == idSolicitud);

             return _TBLModuloAPCActividadesRepository.GetCompleteListBySpec(spec);
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

            if (_TBLModuloAPCActividadesRepository != null)
            {
                _TBLModuloAPCActividadesRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    