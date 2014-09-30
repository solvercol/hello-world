using System;
using System.Collections.Generic;
using System.Linq;
using Domain.MainModules.Entities;
using Domain.Core.Specification;
using Domain.MainModule.Reclamos.Contracts;
using Application.MainModule.Reclamos.IServices;

namespace Application.MainModule.Reclamos.Services
{
    public class SfTBL_ModuloReclamos_AsesoresManagementServices : ISfTBL_ModuloReclamos_AsesoresManagementServices
    {

         #region Fields
         readonly ITBL_ModuloReclamos_AsesoresRepository _TBLModuloReclamosAsesoresRepository;
         #endregion

         #region Constructor
         /// <summary>
         /// Constructor de la Calse 
         /// </summary>
         public SfTBL_ModuloReclamos_AsesoresManagementServices( ITBL_ModuloReclamos_AsesoresRepository TBLModuloReclamosAsesoresRepository)
         {
            if (TBLModuloReclamosAsesoresRepository == null)
                throw new ArgumentNullException("TBLModuloReclamosAsesoresRepository");
            _TBLModuloReclamosAsesoresRepository = TBLModuloReclamosAsesoresRepository;
         }
         #endregion

         #region Members
         /// <summary>
         /// Crea una nueva instancia de la entidad 
         /// </summary>
         public TBL_ModuloReclamos_Asesores NewEntity()
         {
            return new TBL_ModuloReclamos_Asesores();
         }

         /// <summary>
         /// Inserta un nuevo registro en la Base de Datos.
         /// </summary>
         public void Add(TBL_ModuloReclamos_Asesores entity)
         {
            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosAsesoresRepository.UnitOfWork;
            _TBLModuloReclamosAsesoresRepository.Add(entity);
            //Complete changes in this unit of work
            unitOfWork.Commit();
         }

          /// <summary>
          /// Actualiza el registro en la Base de Datos.
          /// </summary>
         public void Modify(TBL_ModuloReclamos_Asesores entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Modificar : El objeto esta nulo."));

            var unitOfWork = _TBLModuloReclamosAsesoresRepository.UnitOfWork;
            _TBLModuloReclamosAsesoresRepository.Modify(entity);
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Elimina el registro en la Base de Datos.
          /// </summary>
         public void Remove(TBL_ModuloReclamos_Asesores entity)
         {
            if (entity == null)
                throw new ArgumentNullException(string.Format("Eliminar : El objeto esta nulo."));

            //Begin unit of work ( if Transaction is required init here a new TransactionScope element
            var unitOfWork = _TBLModuloReclamosAsesoresRepository.UnitOfWork;

            _TBLModuloReclamosAsesoresRepository.Remove(entity);

            //Complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
         }

          /// <summary>
          /// Obtiene una única entidad filtrada por ID.
          /// </summary>
         public TBL_ModuloReclamos_Asesores FindById(int id)
         {
            if (id == 0)
                throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

            Specification<TBL_ModuloReclamos_Asesores> specification = new DirectSpecification<TBL_ModuloReclamos_Asesores>(u => u.IdUsuario == id);

            return _TBLModuloReclamosAsesoresRepository.GetAsesoresBySpec(specification);
           
         }

		 /*
         public TBL_ModuloReclamos_Asesores FindById(string id)
         {
             if (string.IsNullOrEmpty(id))
                 throw new ArgumentNullException(string.Format("Busqueda por Id : El parametro es nulo."));

              Specification<TBL_ModuloReclamos_Asesores> specification = new DirectSpecification<TBL_ModuloReclamos_Asesores>(u => u.Code == id);

            return _TBLModuloReclamosAsesoresRepository.GetEntityBySpec(specification);
         }
		 */

		

          /// <summary>
          /// Obtiene el listado de entidades activas.
          /// </summary>
         public List<TBL_ModuloReclamos_Asesores> FindBySpec(bool isActive)
         {
            Specification<TBL_ModuloReclamos_Asesores> specification = new DirectSpecification<TBL_ModuloReclamos_Asesores>(u => u.IdUnidad != null);
            return _TBLModuloReclamosAsesoresRepository.GetBySpec(specification).ToList();
         }

          /// <summary>
          /// Obtiene el listado de entidades activas y paginadas.
          /// </summary>
         public List<TBL_ModuloReclamos_Asesores> FindPaged(int pageIndex, int pageCount)
         {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


            Specification<TBL_ModuloReclamos_Asesores> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_Asesores>(u => u.IdUnidad != null);

            return _TBLModuloReclamosAsesoresRepository.GetPagedElements(pageIndex, pageCount, u => u.IdUsuario, onlyEnabledSpec, true).ToList();
         }

         public List<TBL_ModuloReclamos_Asesores> FindPaged(int pageIndex, int pageCount, string search)
         {
             if (pageIndex < 0)
                 throw new ArgumentException(Resources.Messages.exception_InvalidPageIndex, "pageIndex");

             if (pageCount <= 0)
                 throw new ArgumentException(Resources.Messages.exception_InvalidPageCount, "pageCount");


             if (!string.IsNullOrEmpty(search))
             {
                 Specification<TBL_ModuloReclamos_Asesores> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_Asesores>
                    (u => (u.TBL_ModuloReclamos_Unidad.Nombre.Contains(search) ||
                        u.TBL_ModuloReclamos_Zona.Descripcion.Contains(search)));


                 return _TBLModuloReclamosAsesoresRepository.GetPagedElements(pageIndex, pageCount, u => u.IdUsuario, onlyEnabledSpec, true).ToList();
             }
             else
             {
                 Specification<TBL_ModuloReclamos_Asesores> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_Asesores>(u => u.IdUsuario != null);

                 return _TBLModuloReclamosAsesoresRepository.GetPagedElements(pageIndex, pageCount, u => u.IdUsuario, onlyEnabledSpec, true).ToList();
             }
         }


         public int CountByPaged(string search)
         {
             if (!string.IsNullOrEmpty(search))
             {

                 Specification<TBL_ModuloReclamos_Asesores> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_Asesores>
                    (u => (u.TBL_ModuloReclamos_Unidad.Nombre.Contains(search) ||
                        u.TBL_ModuloReclamos_Zona.Descripcion.Contains(search)));

                 return _TBLModuloReclamosAsesoresRepository.GetBySpec(onlyEnabledSpec).Count();
             }
             else
             {
                 Specification<TBL_ModuloReclamos_Asesores> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_Asesores>(u => u.IdUsuario != null);

                 return _TBLModuloReclamosAsesoresRepository.GetBySpec(onlyEnabledSpec).Count();
             }
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

            if (_TBLModuloReclamosAsesoresRepository != null)
            {
                _TBLModuloReclamosAsesoresRepository.UnitOfWork.Dispose();
            }
        }

        #endregion

        public List<TBL_Admin_Usuarios> GetAll()
        {
            Specification<TBL_ModuloReclamos_Asesores> onlyEnabledSpec = new DirectSpecification<TBL_ModuloReclamos_Asesores>(u => u.IdUnidad != null);

            return _TBLModuloReclamosAsesoresRepository.GetUsuariosBySpec(onlyEnabledSpec);
        }
    }
}
    