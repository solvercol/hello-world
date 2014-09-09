﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 
using System.Data.Objects;
using Infraestructure.Data.Core;
using Domain.MainModules.Entities;

namespace Infrastructure.Data.MainModule.UnitOfWork
{
    
    ///<sumary>
    ///Base contract for context in Main Module 
    ///</sumary>
    
    public interface IMainModuleUnitOfWork :IQueryableUnitOfWork
    {
       
        #region ObjectSet Properties
    
        IObjectSet<TBL_Admin_Ciudades> TBL_Admin_Ciudades{get;}
        
    
        IObjectSet<TBL_Admin_ConfiguracionServidores> TBL_Admin_ConfiguracionServidores{get;}
        
    
        IObjectSet<TBL_Admin_Departamentos> TBL_Admin_Departamentos{get;}
        
    
        IObjectSet<TBL_Admin_DescripcionesTipoProducto> TBL_Admin_DescripcionesTipoProducto{get;}
        
    
        IObjectSet<TBL_Admin_DiasAnticipados> TBL_Admin_DiasAnticipados{get;}
        
    
        IObjectSet<TBL_Admin_EstadosProceso> TBL_Admin_EstadosProceso{get;}
        
    
        IObjectSet<TBL_Admin_ModuleRepository> TBL_Admin_ModuleRepository{get;}
        
    
        IObjectSet<TBL_Admin_ModuleService> TBL_Admin_ModuleService{get;}
        
    
        IObjectSet<TBL_Admin_ModuleType> TBL_Admin_ModuleType{get;}
        
    
        IObjectSet<TBL_Admin_ModuleVersion> TBL_Admin_ModuleVersion{get;}
        
    
        IObjectSet<TBL_Admin_Modulos> TBL_Admin_Modulos{get;}
        
    
        IObjectSet<TBL_Admin_Monedas> TBL_Admin_Monedas{get;}
        
    
        IObjectSet<TBL_Admin_OpcionesMenu> TBL_Admin_OpcionesMenu{get;}
        
    
        IObjectSet<TBL_Admin_OptionList> TBL_Admin_OptionList{get;}
        
    
        IObjectSet<TBL_Admin_Paises> TBL_Admin_Paises{get;}
        
    
        IObjectSet<TBL_Admin_PaisMoneda> TBL_Admin_PaisMoneda{get;}
        
    
        IObjectSet<TBL_Admin_Plantillas> TBL_Admin_Plantillas{get;}
        
    
        IObjectSet<TBL_Admin_Roles> TBL_Admin_Roles{get;}
        
    
        IObjectSet<TBL_Admin_Secciones> TBL_Admin_Secciones{get;}
        
    
        IObjectSet<TBL_Admin_SistemaNotificaciones> TBL_Admin_SistemaNotificaciones{get;}
        
    
        IObjectSet<TBL_Admin_TypeByModules> TBL_Admin_TypeByModules{get;}
        
    
        IObjectSet<TBL_Admin_Usuarios> TBL_Admin_Usuarios{get;}
        
    
        IObjectSet<TBL_ModuloDocumentos_Categorias> TBL_ModuloDocumentos_Categorias{get;}
        
    
        IObjectSet<TBL_ModuloDocumentos_Documento> TBL_ModuloDocumentos_Documento{get;}
        
    
        IObjectSet<TBL_ModuloDocumentos_DocumentoAdjunto> TBL_ModuloDocumentos_DocumentoAdjunto{get;}
        
    
        IObjectSet<TBL_ModuloDocumentos_DocumentoAdjuntoHistorial> TBL_ModuloDocumentos_DocumentoAdjuntoHistorial{get;}
        
    
        IObjectSet<TBL_ModuloDocumentos_Estados> TBL_ModuloDocumentos_Estados{get;}
        
    
        IObjectSet<TBL_ModuloDocumentos_HistorialDocumento> TBL_ModuloDocumentos_HistorialDocumento{get;}
        
    
        IObjectSet<TBL_ModuloDocumentos_LogCambios> TBL_ModuloDocumentos_LogCambios{get;}
        
    
        IObjectSet<TBL_ModuloDocumentosAnexos_Carpetas> TBL_ModuloDocumentosAnexos_Carpetas{get;}
        
    
        IObjectSet<TBL_ModuloDocumentosAnexos_Categorias> TBL_ModuloDocumentosAnexos_Categorias{get;}
        
    
        IObjectSet<TBL_ModuloDocumentosAnexos_Contenido> TBL_ModuloDocumentosAnexos_Contenido{get;}
        
    
        IObjectSet<TBL_ModuloDocumentosAnexos_Documento> TBL_ModuloDocumentosAnexos_Documento{get;}
        
    
        IObjectSet<TBL_ModuloPlanAccion_BancoActividades> TBL_ModuloPlanAccion_BancoActividades{get;}
        
    
        IObjectSet<TBL_ModuloPlanAccion_Categorias> TBL_ModuloPlanAccion_Categorias{get;}
        
    
        IObjectSet<TBL_ModuloPlanAccion_ConfiguracionActividades> TBL_ModuloPlanAccion_ConfiguracionActividades{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_Actividades> TBL_ModuloReclamos_Actividades{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_ActividadesReclamo> TBL_ModuloReclamos_ActividadesReclamo{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_Alternativas> TBL_ModuloReclamos_Alternativas{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_AnexosActividad> TBL_ModuloReclamos_AnexosActividad{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_AnexosAlternativa> TBL_ModuloReclamos_AnexosAlternativa{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_AnexosComentarioRespuesta> TBL_ModuloReclamos_AnexosComentarioRespuesta{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_AnexosSolucion> TBL_ModuloReclamos_AnexosSolucion{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_Asesores> TBL_ModuloReclamos_Asesores{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_CategoriaProducto> TBL_ModuloReclamos_CategoriaProducto{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_CategoriasReclamo> TBL_ModuloReclamos_CategoriasReclamo{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_ComentariosRespuesta> TBL_ModuloReclamos_ComentariosRespuesta{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_CostosProducto> TBL_ModuloReclamos_CostosProducto{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_LogReclamos> TBL_ModuloReclamos_LogReclamos{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_Reclamo> TBL_ModuloReclamos_Reclamo{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_Soluciones> TBL_ModuloReclamos_Soluciones{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_TipoReclamo> TBL_ModuloReclamos_TipoReclamo{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_Tracking> TBL_ModuloReclamos_Tracking{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_Unidad> TBL_ModuloReclamos_Unidad{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_UnidadesZonas> TBL_ModuloReclamos_UnidadesZonas{get;}
        
    
        IObjectSet<TBL_ModuloReclamos_Zona> TBL_ModuloReclamos_Zona{get;}
        
    
        IObjectSet<TBL_ModuloWorkFlow_CamposValidacion> TBL_ModuloWorkFlow_CamposValidacion{get;}
        
    
        IObjectSet<TBL_ModuloWorkFlow_Rutas> TBL_ModuloWorkFlow_Rutas{get;}
        
    
        IObjectSet<TBL_ModuloWorkFlow_ValidacionesSalida> TBL_ModuloWorkFlow_ValidacionesSalida{get;}
        

        #endregion
    
    }
}
	
