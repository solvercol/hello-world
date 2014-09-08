﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;



using Domain.Core.Entities;
using Domain.Core;
using Domain.MainModules.Entities;
using System.Reflection;

namespace Infrastructure.Data.MainModule.UnitOfWork
{
    [System.Diagnostics.DebuggerNonUserCode()]
    public partial class MainModuleContext : ObjectContext,IMainModuleUnitOfWork
    {
        public const string ConnectionString = "name=MainModuleContext";
        public const string ContainerName = "MainModuleContext";
    
        #region Constructors
    	
        public MainModuleContext()
            : base(ConnectionString, ContainerName)
        {
            Initialize();
        }
    
        public MainModuleContext(string connectionString)
            : base(connectionString, ContainerName)
        {
            Initialize();
        }
    
        public MainModuleContext(EntityConnection connection)
            : base(connection, ContainerName)
        {
            Initialize();
        }
    
        private void Initialize()
        {
            // Creating proxies requires the use of the ProxyDataContractResolver and
            // may allow lazy loading which can expand the loaded graph during serialization.
            ContextOptions.ProxyCreationEnabled = false;
    		ContextOptions.LazyLoadingEnabled = false;
            ObjectMaterialized += new ObjectMaterializedEventHandler(HandleObjectMaterialized);
        }
    
        private void HandleObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            var entity = e.Entity as IObjectWithChangeTracker;
            if (entity != null)
            {
                bool changeTrackingEnabled = entity.ChangeTracker.ChangeTrackingEnabled;
                try
                {
                    entity.MarkAsUnchanged();
                }
                finally
                {
                    entity.ChangeTracker.ChangeTrackingEnabled = changeTrackingEnabled;
                }
                this.StoreReferenceKeyValues(entity);
            }
        }
    
        #endregion
        #region IMainModuleUnitOfWork
    	
    	public  IObjectSet<TEntity> CreateSet<TEntity>() 
    		where TEntity : class,IObjectWithChangeTracker
    	{
    		return base.CreateObjectSet<TEntity>() as IObjectSet<TEntity>;
    	}
    	public void RegisterChanges<TEntity>(TEntity item)
    		where TEntity : class, IObjectWithChangeTracker
    	{
    		this.CreateObjectSet<TEntity>().ApplyChanges(item);
    	}
    	public void CommitAndRefreshChanges()
    	{
    		try
    		{
    			//Default option is DetectChangesBeforeSave
    			base.SaveChanges();
    			
    			//accept all changes in STE entities attached in context
                IEnumerable<IObjectWithChangeTracker> steEntities = (from entry in 
    																	this.ObjectStateManager.GetObjectStateEntries(~EntityState.Detached)
                                                                     where 
    																 	entry.Entity != null 
    																 && 
    																 	(entry.Entity as IObjectWithChangeTracker != null)
                                                                     select
    																 	entry.Entity as IObjectWithChangeTracker);
    
                steEntities.ToList().ForEach(ste => ste.MarkAsUnchanged());
    		}
    		catch (OptimisticConcurrencyException ex)
    		{
    			
    			//if client wins refresh data ( queries database and adapt original values
    			//and re-save changes in client
    			base.Refresh(RefreshMode.ClientWins, ex.StateEntries.Select(se => se.Entity));
    			
    			base.SaveChanges(); 
    			
    			//accept all changes in STE entities attached in context
                IEnumerable<IObjectWithChangeTracker> steEntities = (from entry in 
    																	this.ObjectStateManager.GetObjectStateEntries(~EntityState.Detached)
                                                                     where 
    																 	entry.Entity != null 
    																 && 
    																 	(entry.Entity as IObjectWithChangeTracker != null)
                                                                     select
    																 	entry.Entity as IObjectWithChangeTracker);
    
                steEntities.ToList().ForEach(ste => ste.MarkAsUnchanged());
    		}
    	}
    	public  void Commit()
    	{
    		//Default option is DetectChangesBeforeSave
    		base.SaveChanges();
    		
    		//accept all changes in STE entities attached in context
    		IEnumerable<IObjectWithChangeTracker> steEntities = (from entry in 
    																	this.ObjectStateManager.GetObjectStateEntries(~EntityState.Detached)
                                                                     where 
    																 	entry.Entity != null 
    																 && 
    																 	(entry.Entity as IObjectWithChangeTracker != null)
                                                                     select
    																 	entry.Entity as IObjectWithChangeTracker);
    
    		steEntities.ToList().ForEach(ste => ste.MarkAsUnchanged());
    	}
    	public void RollbackChanges()
    	{
    		//Refresh context and override changes
                
    		IEnumerable<object> itemsToRefresh = base.ObjectStateManager.GetObjectStateEntries(EntityState.Modified)
                                                                        .Where(ose=>!ose.IsRelationship && ose.Entity != null)
                                                                        .Select(ose=>ose.Entity);
            base.Refresh(RefreshMode.StoreWins, itemsToRefresh);
    	}
    	public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
    		return base.ExecuteStoreQuery<TEntity>(sqlQuery, parameters);
       	}
    
    	public int ExecuteCommand(string sqlCommand, params object[] parameters)
    	{
    		return base.ExecuteStoreCommand(sqlCommand, parameters);
    	}
    	

        #endregion
        #region ObjectSet Properties
    
        public IObjectSet<TBL_Admin_Ciudades> TBL_Admin_Ciudades
        {
            get { return _tBL_Admin_Ciudades  ?? (_tBL_Admin_Ciudades = CreateObjectSet<TBL_Admin_Ciudades>("TBL_Admin_Ciudades")); }
        }
        private ObjectSet<TBL_Admin_Ciudades> _tBL_Admin_Ciudades;
    
        public IObjectSet<TBL_Admin_ConfiguracionServidores> TBL_Admin_ConfiguracionServidores
        {
            get { return _tBL_Admin_ConfiguracionServidores  ?? (_tBL_Admin_ConfiguracionServidores = CreateObjectSet<TBL_Admin_ConfiguracionServidores>("TBL_Admin_ConfiguracionServidores")); }
        }
        private ObjectSet<TBL_Admin_ConfiguracionServidores> _tBL_Admin_ConfiguracionServidores;
    
        public IObjectSet<TBL_Admin_Departamentos> TBL_Admin_Departamentos
        {
            get { return _tBL_Admin_Departamentos  ?? (_tBL_Admin_Departamentos = CreateObjectSet<TBL_Admin_Departamentos>("TBL_Admin_Departamentos")); }
        }
        private ObjectSet<TBL_Admin_Departamentos> _tBL_Admin_Departamentos;
    
        public IObjectSet<TBL_Admin_DescripcionesTipoProducto> TBL_Admin_DescripcionesTipoProducto
        {
            get { return _tBL_Admin_DescripcionesTipoProducto  ?? (_tBL_Admin_DescripcionesTipoProducto = CreateObjectSet<TBL_Admin_DescripcionesTipoProducto>("TBL_Admin_DescripcionesTipoProducto")); }
        }
        private ObjectSet<TBL_Admin_DescripcionesTipoProducto> _tBL_Admin_DescripcionesTipoProducto;
    
        public IObjectSet<TBL_Admin_DiasAnticipados> TBL_Admin_DiasAnticipados
        {
            get { return _tBL_Admin_DiasAnticipados  ?? (_tBL_Admin_DiasAnticipados = CreateObjectSet<TBL_Admin_DiasAnticipados>("TBL_Admin_DiasAnticipados")); }
        }
        private ObjectSet<TBL_Admin_DiasAnticipados> _tBL_Admin_DiasAnticipados;
    
        public IObjectSet<TBL_Admin_ModuleRepository> TBL_Admin_ModuleRepository
        {
            get { return _tBL_Admin_ModuleRepository  ?? (_tBL_Admin_ModuleRepository = CreateObjectSet<TBL_Admin_ModuleRepository>("TBL_Admin_ModuleRepository")); }
        }
        private ObjectSet<TBL_Admin_ModuleRepository> _tBL_Admin_ModuleRepository;
    
        public IObjectSet<TBL_Admin_ModuleService> TBL_Admin_ModuleService
        {
            get { return _tBL_Admin_ModuleService  ?? (_tBL_Admin_ModuleService = CreateObjectSet<TBL_Admin_ModuleService>("TBL_Admin_ModuleService")); }
        }
        private ObjectSet<TBL_Admin_ModuleService> _tBL_Admin_ModuleService;
    
        public IObjectSet<TBL_Admin_ModuleType> TBL_Admin_ModuleType
        {
            get { return _tBL_Admin_ModuleType  ?? (_tBL_Admin_ModuleType = CreateObjectSet<TBL_Admin_ModuleType>("TBL_Admin_ModuleType")); }
        }
        private ObjectSet<TBL_Admin_ModuleType> _tBL_Admin_ModuleType;
    
        public IObjectSet<TBL_Admin_ModuleVersion> TBL_Admin_ModuleVersion
        {
            get { return _tBL_Admin_ModuleVersion  ?? (_tBL_Admin_ModuleVersion = CreateObjectSet<TBL_Admin_ModuleVersion>("TBL_Admin_ModuleVersion")); }
        }
        private ObjectSet<TBL_Admin_ModuleVersion> _tBL_Admin_ModuleVersion;
    
        public IObjectSet<TBL_Admin_Modulos> TBL_Admin_Modulos
        {
            get { return _tBL_Admin_Modulos  ?? (_tBL_Admin_Modulos = CreateObjectSet<TBL_Admin_Modulos>("TBL_Admin_Modulos")); }
        }
        private ObjectSet<TBL_Admin_Modulos> _tBL_Admin_Modulos;
    
        public IObjectSet<TBL_Admin_Monedas> TBL_Admin_Monedas
        {
            get { return _tBL_Admin_Monedas  ?? (_tBL_Admin_Monedas = CreateObjectSet<TBL_Admin_Monedas>("TBL_Admin_Monedas")); }
        }
        private ObjectSet<TBL_Admin_Monedas> _tBL_Admin_Monedas;
    
        public IObjectSet<TBL_Admin_OpcionesMenu> TBL_Admin_OpcionesMenu
        {
            get { return _tBL_Admin_OpcionesMenu  ?? (_tBL_Admin_OpcionesMenu = CreateObjectSet<TBL_Admin_OpcionesMenu>("TBL_Admin_OpcionesMenu")); }
        }
        private ObjectSet<TBL_Admin_OpcionesMenu> _tBL_Admin_OpcionesMenu;
    
        public IObjectSet<TBL_Admin_OptionList> TBL_Admin_OptionList
        {
            get { return _tBL_Admin_OptionList  ?? (_tBL_Admin_OptionList = CreateObjectSet<TBL_Admin_OptionList>("TBL_Admin_OptionList")); }
        }
        private ObjectSet<TBL_Admin_OptionList> _tBL_Admin_OptionList;
    
        public IObjectSet<TBL_Admin_Paises> TBL_Admin_Paises
        {
            get { return _tBL_Admin_Paises  ?? (_tBL_Admin_Paises = CreateObjectSet<TBL_Admin_Paises>("TBL_Admin_Paises")); }
        }
        private ObjectSet<TBL_Admin_Paises> _tBL_Admin_Paises;
    
        public IObjectSet<TBL_Admin_PaisMoneda> TBL_Admin_PaisMoneda
        {
            get { return _tBL_Admin_PaisMoneda  ?? (_tBL_Admin_PaisMoneda = CreateObjectSet<TBL_Admin_PaisMoneda>("TBL_Admin_PaisMoneda")); }
        }
        private ObjectSet<TBL_Admin_PaisMoneda> _tBL_Admin_PaisMoneda;
    
        public IObjectSet<TBL_Admin_Plantillas> TBL_Admin_Plantillas
        {
            get { return _tBL_Admin_Plantillas  ?? (_tBL_Admin_Plantillas = CreateObjectSet<TBL_Admin_Plantillas>("TBL_Admin_Plantillas")); }
        }
        private ObjectSet<TBL_Admin_Plantillas> _tBL_Admin_Plantillas;
    
        public IObjectSet<TBL_Admin_Roles> TBL_Admin_Roles
        {
            get { return _tBL_Admin_Roles  ?? (_tBL_Admin_Roles = CreateObjectSet<TBL_Admin_Roles>("TBL_Admin_Roles")); }
        }
        private ObjectSet<TBL_Admin_Roles> _tBL_Admin_Roles;
    
        public IObjectSet<TBL_Admin_Secciones> TBL_Admin_Secciones
        {
            get { return _tBL_Admin_Secciones  ?? (_tBL_Admin_Secciones = CreateObjectSet<TBL_Admin_Secciones>("TBL_Admin_Secciones")); }
        }
        private ObjectSet<TBL_Admin_Secciones> _tBL_Admin_Secciones;
    
        public IObjectSet<TBL_Admin_TypeByModules> TBL_Admin_TypeByModules
        {
            get { return _tBL_Admin_TypeByModules  ?? (_tBL_Admin_TypeByModules = CreateObjectSet<TBL_Admin_TypeByModules>("TBL_Admin_TypeByModules")); }
        }
        private ObjectSet<TBL_Admin_TypeByModules> _tBL_Admin_TypeByModules;
    
        public IObjectSet<TBL_ModuloPlanAccion_BancoActividades> TBL_ModuloPlanAccion_BancoActividades
        {
            get { return _tBL_ModuloPlanAccion_BancoActividades  ?? (_tBL_ModuloPlanAccion_BancoActividades = CreateObjectSet<TBL_ModuloPlanAccion_BancoActividades>("TBL_ModuloPlanAccion_BancoActividades")); }
        }
        private ObjectSet<TBL_ModuloPlanAccion_BancoActividades> _tBL_ModuloPlanAccion_BancoActividades;
    
        public IObjectSet<TBL_ModuloPlanAccion_Categorias> TBL_ModuloPlanAccion_Categorias
        {
            get { return _tBL_ModuloPlanAccion_Categorias  ?? (_tBL_ModuloPlanAccion_Categorias = CreateObjectSet<TBL_ModuloPlanAccion_Categorias>("TBL_ModuloPlanAccion_Categorias")); }
        }
        private ObjectSet<TBL_ModuloPlanAccion_Categorias> _tBL_ModuloPlanAccion_Categorias;
    
        public IObjectSet<TBL_ModuloPlanAccion_ConfiguracionActividades> TBL_ModuloPlanAccion_ConfiguracionActividades
        {
            get { return _tBL_ModuloPlanAccion_ConfiguracionActividades  ?? (_tBL_ModuloPlanAccion_ConfiguracionActividades = CreateObjectSet<TBL_ModuloPlanAccion_ConfiguracionActividades>("TBL_ModuloPlanAccion_ConfiguracionActividades")); }
        }
        private ObjectSet<TBL_ModuloPlanAccion_ConfiguracionActividades> _tBL_ModuloPlanAccion_ConfiguracionActividades;
    
        public IObjectSet<TBL_Admin_EstadosProceso> TBL_Admin_EstadosProceso
        {
            get { return _tBL_Admin_EstadosProceso  ?? (_tBL_Admin_EstadosProceso = CreateObjectSet<TBL_Admin_EstadosProceso>("TBL_Admin_EstadosProceso")); }
        }
        private ObjectSet<TBL_Admin_EstadosProceso> _tBL_Admin_EstadosProceso;
    
        public IObjectSet<TBL_Admin_SistemaNotificaciones> TBL_Admin_SistemaNotificaciones
        {
            get { return _tBL_Admin_SistemaNotificaciones  ?? (_tBL_Admin_SistemaNotificaciones = CreateObjectSet<TBL_Admin_SistemaNotificaciones>("TBL_Admin_SistemaNotificaciones")); }
        }
        private ObjectSet<TBL_Admin_SistemaNotificaciones> _tBL_Admin_SistemaNotificaciones;
    
        public IObjectSet<TBL_Admin_Usuarios> TBL_Admin_Usuarios
        {
            get { return _tBL_Admin_Usuarios  ?? (_tBL_Admin_Usuarios = CreateObjectSet<TBL_Admin_Usuarios>("TBL_Admin_Usuarios")); }
        }
        private ObjectSet<TBL_Admin_Usuarios> _tBL_Admin_Usuarios;
    
        public IObjectSet<TBL_ModuloDocumentos_Categorias> TBL_ModuloDocumentos_Categorias
        {
            get { return _tBL_ModuloDocumentos_Categorias  ?? (_tBL_ModuloDocumentos_Categorias = CreateObjectSet<TBL_ModuloDocumentos_Categorias>("TBL_ModuloDocumentos_Categorias")); }
        }
        private ObjectSet<TBL_ModuloDocumentos_Categorias> _tBL_ModuloDocumentos_Categorias;
    
        public IObjectSet<TBL_ModuloDocumentos_Documento> TBL_ModuloDocumentos_Documento
        {
            get { return _tBL_ModuloDocumentos_Documento  ?? (_tBL_ModuloDocumentos_Documento = CreateObjectSet<TBL_ModuloDocumentos_Documento>("TBL_ModuloDocumentos_Documento")); }
        }
        private ObjectSet<TBL_ModuloDocumentos_Documento> _tBL_ModuloDocumentos_Documento;
    
        public IObjectSet<TBL_ModuloDocumentos_DocumentoAdjunto> TBL_ModuloDocumentos_DocumentoAdjunto
        {
            get { return _tBL_ModuloDocumentos_DocumentoAdjunto  ?? (_tBL_ModuloDocumentos_DocumentoAdjunto = CreateObjectSet<TBL_ModuloDocumentos_DocumentoAdjunto>("TBL_ModuloDocumentos_DocumentoAdjunto")); }
        }
        private ObjectSet<TBL_ModuloDocumentos_DocumentoAdjunto> _tBL_ModuloDocumentos_DocumentoAdjunto;
    
        public IObjectSet<TBL_ModuloDocumentos_DocumentoAdjuntoHistorial> TBL_ModuloDocumentos_DocumentoAdjuntoHistorial
        {
            get { return _tBL_ModuloDocumentos_DocumentoAdjuntoHistorial  ?? (_tBL_ModuloDocumentos_DocumentoAdjuntoHistorial = CreateObjectSet<TBL_ModuloDocumentos_DocumentoAdjuntoHistorial>("TBL_ModuloDocumentos_DocumentoAdjuntoHistorial")); }
        }
        private ObjectSet<TBL_ModuloDocumentos_DocumentoAdjuntoHistorial> _tBL_ModuloDocumentos_DocumentoAdjuntoHistorial;
    
        public IObjectSet<TBL_ModuloDocumentos_Estados> TBL_ModuloDocumentos_Estados
        {
            get { return _tBL_ModuloDocumentos_Estados  ?? (_tBL_ModuloDocumentos_Estados = CreateObjectSet<TBL_ModuloDocumentos_Estados>("TBL_ModuloDocumentos_Estados")); }
        }
        private ObjectSet<TBL_ModuloDocumentos_Estados> _tBL_ModuloDocumentos_Estados;
    
        public IObjectSet<TBL_ModuloDocumentos_HistorialDocumento> TBL_ModuloDocumentos_HistorialDocumento
        {
            get { return _tBL_ModuloDocumentos_HistorialDocumento  ?? (_tBL_ModuloDocumentos_HistorialDocumento = CreateObjectSet<TBL_ModuloDocumentos_HistorialDocumento>("TBL_ModuloDocumentos_HistorialDocumento")); }
        }
        private ObjectSet<TBL_ModuloDocumentos_HistorialDocumento> _tBL_ModuloDocumentos_HistorialDocumento;
    
        public IObjectSet<TBL_ModuloDocumentos_LogCambios> TBL_ModuloDocumentos_LogCambios
        {
            get { return _tBL_ModuloDocumentos_LogCambios  ?? (_tBL_ModuloDocumentos_LogCambios = CreateObjectSet<TBL_ModuloDocumentos_LogCambios>("TBL_ModuloDocumentos_LogCambios")); }
        }
        private ObjectSet<TBL_ModuloDocumentos_LogCambios> _tBL_ModuloDocumentos_LogCambios;
    
        public IObjectSet<TBL_ModuloDocumentosAnexos_Carpetas> TBL_ModuloDocumentosAnexos_Carpetas
        {
            get { return _tBL_ModuloDocumentosAnexos_Carpetas  ?? (_tBL_ModuloDocumentosAnexos_Carpetas = CreateObjectSet<TBL_ModuloDocumentosAnexos_Carpetas>("TBL_ModuloDocumentosAnexos_Carpetas")); }
        }
        private ObjectSet<TBL_ModuloDocumentosAnexos_Carpetas> _tBL_ModuloDocumentosAnexos_Carpetas;
    
        public IObjectSet<TBL_ModuloDocumentosAnexos_Categorias> TBL_ModuloDocumentosAnexos_Categorias
        {
            get { return _tBL_ModuloDocumentosAnexos_Categorias  ?? (_tBL_ModuloDocumentosAnexos_Categorias = CreateObjectSet<TBL_ModuloDocumentosAnexos_Categorias>("TBL_ModuloDocumentosAnexos_Categorias")); }
        }
        private ObjectSet<TBL_ModuloDocumentosAnexos_Categorias> _tBL_ModuloDocumentosAnexos_Categorias;
    
        public IObjectSet<TBL_ModuloDocumentosAnexos_Contenido> TBL_ModuloDocumentosAnexos_Contenido
        {
            get { return _tBL_ModuloDocumentosAnexos_Contenido  ?? (_tBL_ModuloDocumentosAnexos_Contenido = CreateObjectSet<TBL_ModuloDocumentosAnexos_Contenido>("TBL_ModuloDocumentosAnexos_Contenido")); }
        }
        private ObjectSet<TBL_ModuloDocumentosAnexos_Contenido> _tBL_ModuloDocumentosAnexos_Contenido;
    
        public IObjectSet<TBL_ModuloDocumentosAnexos_Documento> TBL_ModuloDocumentosAnexos_Documento
        {
            get { return _tBL_ModuloDocumentosAnexos_Documento  ?? (_tBL_ModuloDocumentosAnexos_Documento = CreateObjectSet<TBL_ModuloDocumentosAnexos_Documento>("TBL_ModuloDocumentosAnexos_Documento")); }
        }
        private ObjectSet<TBL_ModuloDocumentosAnexos_Documento> _tBL_ModuloDocumentosAnexos_Documento;
    
        public IObjectSet<TBL_ModuloReclamos_Actividades> TBL_ModuloReclamos_Actividades
        {
            get { return _tBL_ModuloReclamos_Actividades  ?? (_tBL_ModuloReclamos_Actividades = CreateObjectSet<TBL_ModuloReclamos_Actividades>("TBL_ModuloReclamos_Actividades")); }
        }
        private ObjectSet<TBL_ModuloReclamos_Actividades> _tBL_ModuloReclamos_Actividades;
    
        public IObjectSet<TBL_ModuloReclamos_ActividadesReclamo> TBL_ModuloReclamos_ActividadesReclamo
        {
            get { return _tBL_ModuloReclamos_ActividadesReclamo  ?? (_tBL_ModuloReclamos_ActividadesReclamo = CreateObjectSet<TBL_ModuloReclamos_ActividadesReclamo>("TBL_ModuloReclamos_ActividadesReclamo")); }
        }
        private ObjectSet<TBL_ModuloReclamos_ActividadesReclamo> _tBL_ModuloReclamos_ActividadesReclamo;
    
        public IObjectSet<TBL_ModuloReclamos_Alternativas> TBL_ModuloReclamos_Alternativas
        {
            get { return _tBL_ModuloReclamos_Alternativas  ?? (_tBL_ModuloReclamos_Alternativas = CreateObjectSet<TBL_ModuloReclamos_Alternativas>("TBL_ModuloReclamos_Alternativas")); }
        }
        private ObjectSet<TBL_ModuloReclamos_Alternativas> _tBL_ModuloReclamos_Alternativas;
    
        public IObjectSet<TBL_ModuloReclamos_AnexosActividad> TBL_ModuloReclamos_AnexosActividad
        {
            get { return _tBL_ModuloReclamos_AnexosActividad  ?? (_tBL_ModuloReclamos_AnexosActividad = CreateObjectSet<TBL_ModuloReclamos_AnexosActividad>("TBL_ModuloReclamos_AnexosActividad")); }
        }
        private ObjectSet<TBL_ModuloReclamos_AnexosActividad> _tBL_ModuloReclamos_AnexosActividad;
    
        public IObjectSet<TBL_ModuloReclamos_AnexosAlternativa> TBL_ModuloReclamos_AnexosAlternativa
        {
            get { return _tBL_ModuloReclamos_AnexosAlternativa  ?? (_tBL_ModuloReclamos_AnexosAlternativa = CreateObjectSet<TBL_ModuloReclamos_AnexosAlternativa>("TBL_ModuloReclamos_AnexosAlternativa")); }
        }
        private ObjectSet<TBL_ModuloReclamos_AnexosAlternativa> _tBL_ModuloReclamos_AnexosAlternativa;
    
        public IObjectSet<TBL_ModuloReclamos_AnexosComentarioRespuesta> TBL_ModuloReclamos_AnexosComentarioRespuesta
        {
            get { return _tBL_ModuloReclamos_AnexosComentarioRespuesta  ?? (_tBL_ModuloReclamos_AnexosComentarioRespuesta = CreateObjectSet<TBL_ModuloReclamos_AnexosComentarioRespuesta>("TBL_ModuloReclamos_AnexosComentarioRespuesta")); }
        }
        private ObjectSet<TBL_ModuloReclamos_AnexosComentarioRespuesta> _tBL_ModuloReclamos_AnexosComentarioRespuesta;
    
        public IObjectSet<TBL_ModuloReclamos_AnexosSolucion> TBL_ModuloReclamos_AnexosSolucion
        {
            get { return _tBL_ModuloReclamos_AnexosSolucion  ?? (_tBL_ModuloReclamos_AnexosSolucion = CreateObjectSet<TBL_ModuloReclamos_AnexosSolucion>("TBL_ModuloReclamos_AnexosSolucion")); }
        }
        private ObjectSet<TBL_ModuloReclamos_AnexosSolucion> _tBL_ModuloReclamos_AnexosSolucion;
    
        public IObjectSet<TBL_ModuloReclamos_Asesores> TBL_ModuloReclamos_Asesores
        {
            get { return _tBL_ModuloReclamos_Asesores  ?? (_tBL_ModuloReclamos_Asesores = CreateObjectSet<TBL_ModuloReclamos_Asesores>("TBL_ModuloReclamos_Asesores")); }
        }
        private ObjectSet<TBL_ModuloReclamos_Asesores> _tBL_ModuloReclamos_Asesores;
    
        public IObjectSet<TBL_ModuloReclamos_CategoriaProducto> TBL_ModuloReclamos_CategoriaProducto
        {
            get { return _tBL_ModuloReclamos_CategoriaProducto  ?? (_tBL_ModuloReclamos_CategoriaProducto = CreateObjectSet<TBL_ModuloReclamos_CategoriaProducto>("TBL_ModuloReclamos_CategoriaProducto")); }
        }
        private ObjectSet<TBL_ModuloReclamos_CategoriaProducto> _tBL_ModuloReclamos_CategoriaProducto;
    
        public IObjectSet<TBL_ModuloReclamos_CategoriasReclamo> TBL_ModuloReclamos_CategoriasReclamo
        {
            get { return _tBL_ModuloReclamos_CategoriasReclamo  ?? (_tBL_ModuloReclamos_CategoriasReclamo = CreateObjectSet<TBL_ModuloReclamos_CategoriasReclamo>("TBL_ModuloReclamos_CategoriasReclamo")); }
        }
        private ObjectSet<TBL_ModuloReclamos_CategoriasReclamo> _tBL_ModuloReclamos_CategoriasReclamo;
    
        public IObjectSet<TBL_ModuloReclamos_ComentariosRespuesta> TBL_ModuloReclamos_ComentariosRespuesta
        {
            get { return _tBL_ModuloReclamos_ComentariosRespuesta  ?? (_tBL_ModuloReclamos_ComentariosRespuesta = CreateObjectSet<TBL_ModuloReclamos_ComentariosRespuesta>("TBL_ModuloReclamos_ComentariosRespuesta")); }
        }
        private ObjectSet<TBL_ModuloReclamos_ComentariosRespuesta> _tBL_ModuloReclamos_ComentariosRespuesta;
    
        public IObjectSet<TBL_ModuloReclamos_CostosProducto> TBL_ModuloReclamos_CostosProducto
        {
            get { return _tBL_ModuloReclamos_CostosProducto  ?? (_tBL_ModuloReclamos_CostosProducto = CreateObjectSet<TBL_ModuloReclamos_CostosProducto>("TBL_ModuloReclamos_CostosProducto")); }
        }
        private ObjectSet<TBL_ModuloReclamos_CostosProducto> _tBL_ModuloReclamos_CostosProducto;
    
        public IObjectSet<TBL_ModuloReclamos_LogReclamos> TBL_ModuloReclamos_LogReclamos
        {
            get { return _tBL_ModuloReclamos_LogReclamos  ?? (_tBL_ModuloReclamos_LogReclamos = CreateObjectSet<TBL_ModuloReclamos_LogReclamos>("TBL_ModuloReclamos_LogReclamos")); }
        }
        private ObjectSet<TBL_ModuloReclamos_LogReclamos> _tBL_ModuloReclamos_LogReclamos;
    
        public IObjectSet<TBL_ModuloReclamos_Reclamo> TBL_ModuloReclamos_Reclamo
        {
            get { return _tBL_ModuloReclamos_Reclamo  ?? (_tBL_ModuloReclamos_Reclamo = CreateObjectSet<TBL_ModuloReclamos_Reclamo>("TBL_ModuloReclamos_Reclamo")); }
        }
        private ObjectSet<TBL_ModuloReclamos_Reclamo> _tBL_ModuloReclamos_Reclamo;
    
        public IObjectSet<TBL_ModuloReclamos_Soluciones> TBL_ModuloReclamos_Soluciones
        {
            get { return _tBL_ModuloReclamos_Soluciones  ?? (_tBL_ModuloReclamos_Soluciones = CreateObjectSet<TBL_ModuloReclamos_Soluciones>("TBL_ModuloReclamos_Soluciones")); }
        }
        private ObjectSet<TBL_ModuloReclamos_Soluciones> _tBL_ModuloReclamos_Soluciones;
    
        public IObjectSet<TBL_ModuloReclamos_TipoReclamo> TBL_ModuloReclamos_TipoReclamo
        {
            get { return _tBL_ModuloReclamos_TipoReclamo  ?? (_tBL_ModuloReclamos_TipoReclamo = CreateObjectSet<TBL_ModuloReclamos_TipoReclamo>("TBL_ModuloReclamos_TipoReclamo")); }
        }
        private ObjectSet<TBL_ModuloReclamos_TipoReclamo> _tBL_ModuloReclamos_TipoReclamo;
    
        public IObjectSet<TBL_ModuloReclamos_Tracking> TBL_ModuloReclamos_Tracking
        {
            get { return _tBL_ModuloReclamos_Tracking  ?? (_tBL_ModuloReclamos_Tracking = CreateObjectSet<TBL_ModuloReclamos_Tracking>("TBL_ModuloReclamos_Tracking")); }
        }
        private ObjectSet<TBL_ModuloReclamos_Tracking> _tBL_ModuloReclamos_Tracking;
    
        public IObjectSet<TBL_ModuloReclamos_Unidad> TBL_ModuloReclamos_Unidad
        {
            get { return _tBL_ModuloReclamos_Unidad  ?? (_tBL_ModuloReclamos_Unidad = CreateObjectSet<TBL_ModuloReclamos_Unidad>("TBL_ModuloReclamos_Unidad")); }
        }
        private ObjectSet<TBL_ModuloReclamos_Unidad> _tBL_ModuloReclamos_Unidad;
    
        public IObjectSet<TBL_ModuloReclamos_UnidadesZonas> TBL_ModuloReclamos_UnidadesZonas
        {
            get { return _tBL_ModuloReclamos_UnidadesZonas  ?? (_tBL_ModuloReclamos_UnidadesZonas = CreateObjectSet<TBL_ModuloReclamos_UnidadesZonas>("TBL_ModuloReclamos_UnidadesZonas")); }
        }
        private ObjectSet<TBL_ModuloReclamos_UnidadesZonas> _tBL_ModuloReclamos_UnidadesZonas;
    
        public IObjectSet<TBL_ModuloReclamos_Zona> TBL_ModuloReclamos_Zona
        {
            get { return _tBL_ModuloReclamos_Zona  ?? (_tBL_ModuloReclamos_Zona = CreateObjectSet<TBL_ModuloReclamos_Zona>("TBL_ModuloReclamos_Zona")); }
        }
        private ObjectSet<TBL_ModuloReclamos_Zona> _tBL_ModuloReclamos_Zona;
    
        public IObjectSet<TBL_ModuloWorkFlow_CamposValidacion> TBL_ModuloWorkFlow_CamposValidacion
        {
            get { return _tBL_ModuloWorkFlow_CamposValidacion  ?? (_tBL_ModuloWorkFlow_CamposValidacion = CreateObjectSet<TBL_ModuloWorkFlow_CamposValidacion>("TBL_ModuloWorkFlow_CamposValidacion")); }
        }
        private ObjectSet<TBL_ModuloWorkFlow_CamposValidacion> _tBL_ModuloWorkFlow_CamposValidacion;
    
        public IObjectSet<TBL_ModuloWorkFlow_Rutas> TBL_ModuloWorkFlow_Rutas
        {
            get { return _tBL_ModuloWorkFlow_Rutas  ?? (_tBL_ModuloWorkFlow_Rutas = CreateObjectSet<TBL_ModuloWorkFlow_Rutas>("TBL_ModuloWorkFlow_Rutas")); }
        }
        private ObjectSet<TBL_ModuloWorkFlow_Rutas> _tBL_ModuloWorkFlow_Rutas;
    
        public IObjectSet<TBL_ModuloWorkFlow_ValidacionesSalida> TBL_ModuloWorkFlow_ValidacionesSalida
        {
            get { return _tBL_ModuloWorkFlow_ValidacionesSalida  ?? (_tBL_ModuloWorkFlow_ValidacionesSalida = CreateObjectSet<TBL_ModuloWorkFlow_ValidacionesSalida>("TBL_ModuloWorkFlow_ValidacionesSalida")); }
        }
        private ObjectSet<TBL_ModuloWorkFlow_ValidacionesSalida> _tBL_ModuloWorkFlow_ValidacionesSalida;

        #endregion
    }
    
}
