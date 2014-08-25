//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System.Linq;
using Domain.MainModule.Documentos.Contracts;
using Infraestructure.Data.Core;
using Infraestructure.Data.Core.Extensions;
using Infrastructure.CrossCutting.Logging;
using Domain.MainModules.Entities;
using Infrastructure.Data.MainModule.UnitOfWork;
using Domain.Core.Specification;
using System;
using System.Globalization;
using Infraestructura.Data.Documentos.Resources;
using System.Collections.Generic;

namespace Infraestructura.Data.Documentos.Repositories
{
    public class TBL_ModuloDocumentos_DocumentoRepository 
        : GenericRepository<TBL_ModuloDocumentos_Documento>, ITBL_ModuloDocumentos_DocumentoRepository 
    {
        IMainModuleUnitOfWork _currentUnitOfWork;

        public TBL_ModuloDocumentos_DocumentoRepository(IMainModuleUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
            _currentUnitOfWork = unitOfWork;
        }

        public TBL_ModuloDocumentos_Documento GetDocumentoById(int id)
        {
            if (id > 0)
            {
                var set = _currentUnitOfWork.CreateSet<TBL_ModuloDocumentos_Documento>();

                return set.Where(c => c.IdDocumento == id)
                          .Select(c => c)
                          .SingleOrDefault();
            }
            return null;
        }

        public TBL_ModuloDocumentos_Documento GetDocumentoByIdWithAttachments(ISpecification<TBL_ModuloDocumentos_Documento> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");
            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {
                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloDocumentos_Documento
                                    .Include(r => r.TBL_ModuloDocumentos_DocumentoAdjunto)
                                    .Include(r => r.TBL_ModuloDocumentos_Categorias)
                                    .Include(r => r.TBL_ModuloDocumentos_Categorias1)
                                    .Include(r => r.TBL_ModuloDocumentos_Categorias2)
                                    .Include(r => r.TBL_ModuloDocumentos_Estados)
                                    .Where(specific)
                                    .SingleOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));

        }

        public List<TBL_ModuloDocumentos_Documento> GetDocumentsWithCategories(ISpecification<TBL_ModuloDocumentos_Documento> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");
            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {
                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloDocumentos_Documento
                    .Include(r => r.TBL_ModuloDocumentos_Categorias)
                    .Include(r => r.TBL_ModuloDocumentos_Categorias1)
                    .Include(r => r.TBL_ModuloDocumentos_Categorias2)
                    .Include(r => r.TBL_ModuloDocumentos_Estados)
                    .Include(r => r.TBL_ModuloDocumentos_DocumentoAdjunto)
                    .Where(specific).ToList();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));

        }

        public TBL_ModuloDocumentos_Documento GetDocumentsByIdWithCategories(ISpecification<TBL_ModuloDocumentos_Documento> specification)
        {
            //validate specification
            if (specification == null)
                throw new ArgumentNullException("specification");
            var activeContext = UnitOfWork as IMainModuleUnitOfWork;
            if (activeContext != null)
            {
                //perform operation in this repository
                var specific = specification.SatisfiedBy();
                return activeContext.TBL_ModuloDocumentos_Documento
                    .Include(r => r.TBL_ModuloDocumentos_Categorias)
                    .Include(r => r.TBL_ModuloDocumentos_Categorias1)
                    .Include(r => r.TBL_ModuloDocumentos_Categorias2)
                    .Include(r => r.TBL_ModuloDocumentos_Estados)
                    .Include(r => r.TBL_ModuloDocumentos_DocumentoAdjunto)
                    .Where(specific).FirstOrDefault();
            }
            throw new InvalidOperationException(string.Format(
                CultureInfo.InvariantCulture,
                Messages.exception_InvalidStoreContext,
                GetType().Name));

        }  

    }
}
    