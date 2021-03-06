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
using Applications.MainModule.WorkFlow.IServices;
using Domain.MainModule.WorkFlow.Contracts;
using Domain.MainModules.Entities;
using Domain.Core.Specification;

namespace Applications.MainModule.WorkFlow.Services
{
    public class SfTBL_ModuloWorkFlow_ValidacionesSalidaManagementServices : ISfTBL_ModuloWorkFlow_ValidacionesSalidaManagementServices
    {

         #region Fields
         readonly ITBL_ModuloWorkFlow_ValidacionesSalidaRepository _tblModuloWorkFlowValidacionesSalidaRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloWorkFlow_ValidacionesSalidaManagementServices( ITBL_ModuloWorkFlow_ValidacionesSalidaRepository tblModuloWorkFlowValidacionesSalidaRepository)
         {
            if (tblModuloWorkFlowValidacionesSalidaRepository == null)
                throw new ArgumentNullException("tblModuloWorkFlowValidacionesSalidaRepository");
            _tblModuloWorkFlowValidacionesSalidaRepository = tblModuloWorkFlowValidacionesSalidaRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloWorkFlow_ValidacionesSalida NewEntity()
         {
            return new TBL_ModuloWorkFlow_ValidacionesSalida();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloWorkFlow_ValidacionesSalida entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblModuloWorkFlowValidacionesSalidaRepository.UnitOfWork;
            _tblModuloWorkFlowValidacionesSalidaRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloWorkFlow_ValidacionesSalida entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _tblModuloWorkFlowValidacionesSalidaRepository.UnitOfWork;
            _tblModuloWorkFlowValidacionesSalidaRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloWorkFlow_ValidacionesSalida entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _tblModuloWorkFlowValidacionesSalidaRepository.UnitOfWork;

            _tblModuloWorkFlowValidacionesSalidaRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloWorkFlow_ValidacionesSalida FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloWorkFlow_ValidacionesSalida> specification = new DirectSpecification<TBL_ModuloWorkFlow_ValidacionesSalida>(u => u.ID == id);

            return _tblModuloWorkFlowValidacionesSalidaRepository.GetEntityBySpec(specification);
           
         }

		 /*
         public TBL_ModuloWorkFlow_ValidacionesSalida FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloWorkFlow_ValidacionesSalida> specification = new DirectSpecification<TBL_ModuloWorkFlow_ValidacionesSalida>(u => u.Code == id);

            return _TBLModuloWorkFlowValidacionesSalidaRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloWorkFlow_ValidacionesSalida> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloWorkFlow_ValidacionesSalida> specification = new DirectSpecification<TBL_ModuloWorkFlow_ValidacionesSalida>(u => u.IsActive == isActive);
            return _tblModuloWorkFlowValidacionesSalidaRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloWorkFlow_ValidacionesSalida> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloWorkFlow_ValidacionesSalida> onlyEnabledSpec = new DirectSpecification<TBL_ModuloWorkFlow_ValidacionesSalida>(u => u.IsActive);

            return _tblModuloWorkFlowValidacionesSalidaRepository.GetPagedElements(pageIndex, pageCount, u => u.ID, onlyEnabledSpec, true).ToList();
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

            if (_tblModuloWorkFlowValidacionesSalidaRepository != null)
            {
                _tblModuloWorkFlowValidacionesSalidaRepository.UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
    