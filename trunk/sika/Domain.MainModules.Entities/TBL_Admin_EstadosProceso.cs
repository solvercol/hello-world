//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

using Domain.Core.Entities;

namespace Domain.MainModules.Entities
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(TBL_ModuloWorkFlow_Rutas))]
    [KnownType(typeof(TBL_ModuloAPC_Solicitud))]
    [KnownType(typeof(TBL_ModuloReclamos_Reclamo))]
    [KnownType(typeof(TBL_ModuloWorkFlow_CamposValidacion))]
    
    public partial class TBL_Admin_EstadosProceso: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int IdEstado
        {
            get { return _idEstado; }
            set
            {
                if (_idEstado != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdEstado' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idEstado = value;
                    OnPropertyChanged("IdEstado");
                }
            }
        }
        private int _idEstado;
    
        [DataMember]
        public string Estado
        {
            get { return _estado; }
            set
            {
                if (_estado != value)
                {
                    _estado = value;
                    OnPropertyChanged("Estado");
                }
            }
        }
        private string _estado;
    
        [DataMember]
        public string Descripcion
        {
            get { return _descripcion; }
            set
            {
                if (_descripcion != value)
                {
                    _descripcion = value;
                    OnPropertyChanged("Descripcion");
                }
            }
        }
        private string _descripcion;
    
        [DataMember]
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged("IsActive");
                }
            }
        }
        private bool _isActive;
    
        [DataMember]
        public Nullable<bool> PermiteEdicionCampos
        {
            get { return _permiteEdicionCampos; }
            set
            {
                if (_permiteEdicionCampos != value)
                {
                    _permiteEdicionCampos = value;
                    OnPropertyChanged("PermiteEdicionCampos");
                }
            }
        }
        private Nullable<bool> _permiteEdicionCampos;
    
        [DataMember]
        public Nullable<bool> PermiteComentar
        {
            get { return _permiteComentar; }
            set
            {
                if (_permiteComentar != value)
                {
                    _permiteComentar = value;
                    OnPropertyChanged("PermiteComentar");
                }
            }
        }
        private Nullable<bool> _permiteComentar;
    
        [DataMember]
        public Nullable<bool> PermiteProgActividades
        {
            get { return _permiteProgActividades; }
            set
            {
                if (_permiteProgActividades != value)
                {
                    _permiteProgActividades = value;
                    OnPropertyChanged("PermiteProgActividades");
                }
            }
        }
        private Nullable<bool> _permiteProgActividades;
    
        [DataMember]
        public Nullable<bool> PermiteAdjuntarAnexos
        {
            get { return _permiteAdjuntarAnexos; }
            set
            {
                if (_permiteAdjuntarAnexos != value)
                {
                    _permiteAdjuntarAnexos = value;
                    OnPropertyChanged("PermiteAdjuntarAnexos");
                }
            }
        }
        private Nullable<bool> _permiteAdjuntarAnexos;
    
        [DataMember]
        public string RolAdjuntardocumentos
        {
            get { return _rolAdjuntardocumentos; }
            set
            {
                if (_rolAdjuntardocumentos != value)
                {
                    _rolAdjuntardocumentos = value;
                    OnPropertyChanged("RolAdjuntardocumentos");
                }
            }
        }
        private string _rolAdjuntardocumentos;
    
        [DataMember]
        public string ValidarCategorias
        {
            get { return _validarCategorias; }
            set
            {
                if (_validarCategorias != value)
                {
                    _validarCategorias = value;
                    OnPropertyChanged("ValidarCategorias");
                }
            }
        }
        private string _validarCategorias;
    
        [DataMember]
        public Nullable<int> TipoModulo
        {
            get { return _tipoModulo; }
            set
            {
                if (_tipoModulo != value)
                {
                    _tipoModulo = value;
                    OnPropertyChanged("TipoModulo");
                }
            }
        }
        private Nullable<int> _tipoModulo;
    
        [DataMember]
        public string CreateBy
        {
            get { return _createBy; }
            set
            {
                if (_createBy != value)
                {
                    _createBy = value;
                    OnPropertyChanged("CreateBy");
                }
            }
        }
        private string _createBy;
    
        [DataMember]
        public Nullable<System.DateTime> CreateOn
        {
            get { return _createOn; }
            set
            {
                if (_createOn != value)
                {
                    _createOn = value;
                    OnPropertyChanged("CreateOn");
                }
            }
        }
        private Nullable<System.DateTime> _createOn;
    
        [DataMember]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set
            {
                if (_modifiedBy != value)
                {
                    _modifiedBy = value;
                    OnPropertyChanged("ModifiedBy");
                }
            }
        }
        private string _modifiedBy;
    
        [DataMember]
        public Nullable<System.DateTime> ModifiedOn
        {
            get { return _modifiedOn; }
            set
            {
                if (_modifiedOn != value)
                {
                    _modifiedOn = value;
                    OnPropertyChanged("ModifiedOn");
                }
            }
        }
        private Nullable<System.DateTime> _modifiedOn;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<TBL_ModuloWorkFlow_Rutas> TBL_ModuloWorkFlow_Rutas
        {
            get
            {
                if (_tBL_ModuloWorkFlow_Rutas == null)
                {
                    _tBL_ModuloWorkFlow_Rutas = new TrackableCollection<TBL_ModuloWorkFlow_Rutas>();
                    _tBL_ModuloWorkFlow_Rutas.CollectionChanged += FixupTBL_ModuloWorkFlow_Rutas;
                }
                return _tBL_ModuloWorkFlow_Rutas;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloWorkFlow_Rutas, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloWorkFlow_Rutas != null)
                    {
                        _tBL_ModuloWorkFlow_Rutas.CollectionChanged -= FixupTBL_ModuloWorkFlow_Rutas;
                    }
                    _tBL_ModuloWorkFlow_Rutas = value;
                    if (_tBL_ModuloWorkFlow_Rutas != null)
                    {
                        _tBL_ModuloWorkFlow_Rutas.CollectionChanged += FixupTBL_ModuloWorkFlow_Rutas;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloWorkFlow_Rutas");
                }
            }
        }
        private TrackableCollection<TBL_ModuloWorkFlow_Rutas> _tBL_ModuloWorkFlow_Rutas;
    
        [DataMember]
        public TrackableCollection<TBL_ModuloAPC_Solicitud> TBL_ModuloAPC_Solicitud
        {
            get
            {
                if (_tBL_ModuloAPC_Solicitud == null)
                {
                    _tBL_ModuloAPC_Solicitud = new TrackableCollection<TBL_ModuloAPC_Solicitud>();
                    _tBL_ModuloAPC_Solicitud.CollectionChanged += FixupTBL_ModuloAPC_Solicitud;
                }
                return _tBL_ModuloAPC_Solicitud;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloAPC_Solicitud, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloAPC_Solicitud != null)
                    {
                        _tBL_ModuloAPC_Solicitud.CollectionChanged -= FixupTBL_ModuloAPC_Solicitud;
                    }
                    _tBL_ModuloAPC_Solicitud = value;
                    if (_tBL_ModuloAPC_Solicitud != null)
                    {
                        _tBL_ModuloAPC_Solicitud.CollectionChanged += FixupTBL_ModuloAPC_Solicitud;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloAPC_Solicitud");
                }
            }
        }
        private TrackableCollection<TBL_ModuloAPC_Solicitud> _tBL_ModuloAPC_Solicitud;
    
        [DataMember]
        public TrackableCollection<TBL_ModuloReclamos_Reclamo> TBL_ModuloReclamos_Reclamo
        {
            get
            {
                if (_tBL_ModuloReclamos_Reclamo == null)
                {
                    _tBL_ModuloReclamos_Reclamo = new TrackableCollection<TBL_ModuloReclamos_Reclamo>();
                    _tBL_ModuloReclamos_Reclamo.CollectionChanged += FixupTBL_ModuloReclamos_Reclamo;
                }
                return _tBL_ModuloReclamos_Reclamo;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloReclamos_Reclamo, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloReclamos_Reclamo != null)
                    {
                        _tBL_ModuloReclamos_Reclamo.CollectionChanged -= FixupTBL_ModuloReclamos_Reclamo;
                    }
                    _tBL_ModuloReclamos_Reclamo = value;
                    if (_tBL_ModuloReclamos_Reclamo != null)
                    {
                        _tBL_ModuloReclamos_Reclamo.CollectionChanged += FixupTBL_ModuloReclamos_Reclamo;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloReclamos_Reclamo");
                }
            }
        }
        private TrackableCollection<TBL_ModuloReclamos_Reclamo> _tBL_ModuloReclamos_Reclamo;
    
        [DataMember]
        public TrackableCollection<TBL_ModuloWorkFlow_CamposValidacion> TBL_ModuloWorkFlow_CamposValidacion
        {
            get
            {
                if (_tBL_ModuloWorkFlow_CamposValidacion == null)
                {
                    _tBL_ModuloWorkFlow_CamposValidacion = new TrackableCollection<TBL_ModuloWorkFlow_CamposValidacion>();
                    _tBL_ModuloWorkFlow_CamposValidacion.CollectionChanged += FixupTBL_ModuloWorkFlow_CamposValidacion;
                }
                return _tBL_ModuloWorkFlow_CamposValidacion;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloWorkFlow_CamposValidacion, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloWorkFlow_CamposValidacion != null)
                    {
                        _tBL_ModuloWorkFlow_CamposValidacion.CollectionChanged -= FixupTBL_ModuloWorkFlow_CamposValidacion;
                    }
                    _tBL_ModuloWorkFlow_CamposValidacion = value;
                    if (_tBL_ModuloWorkFlow_CamposValidacion != null)
                    {
                        _tBL_ModuloWorkFlow_CamposValidacion.CollectionChanged += FixupTBL_ModuloWorkFlow_CamposValidacion;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloWorkFlow_CamposValidacion");
                }
            }
        }
        private TrackableCollection<TBL_ModuloWorkFlow_CamposValidacion> _tBL_ModuloWorkFlow_CamposValidacion;
    
        [DataMember]
        public TrackableCollection<TBL_ModuloWorkFlow_Rutas> TBL_ModuloWorkFlow_Rutas1
        {
            get
            {
                if (_tBL_ModuloWorkFlow_Rutas1 == null)
                {
                    _tBL_ModuloWorkFlow_Rutas1 = new TrackableCollection<TBL_ModuloWorkFlow_Rutas>();
                    _tBL_ModuloWorkFlow_Rutas1.CollectionChanged += FixupTBL_ModuloWorkFlow_Rutas1;
                }
                return _tBL_ModuloWorkFlow_Rutas1;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloWorkFlow_Rutas1, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloWorkFlow_Rutas1 != null)
                    {
                        _tBL_ModuloWorkFlow_Rutas1.CollectionChanged -= FixupTBL_ModuloWorkFlow_Rutas1;
                    }
                    _tBL_ModuloWorkFlow_Rutas1 = value;
                    if (_tBL_ModuloWorkFlow_Rutas1 != null)
                    {
                        _tBL_ModuloWorkFlow_Rutas1.CollectionChanged += FixupTBL_ModuloWorkFlow_Rutas1;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloWorkFlow_Rutas1");
                }
            }
        }
        private TrackableCollection<TBL_ModuloWorkFlow_Rutas> _tBL_ModuloWorkFlow_Rutas1;

        #endregion
        #region ChangeTracking
    
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
            {
                ChangeTracker.State = ObjectState.Modified;
            }
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        protected virtual void OnNavigationPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
        private ObjectChangeTracker _changeTracker;
    
        [DataMember]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if (_changeTracker == null)
                {
                    _changeTracker = new ObjectChangeTracker();
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
                return _changeTracker;
            }
            set
            {
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
                }
                _changeTracker = value;
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
            }
        }
    
        private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                ClearNavigationProperties();
            }
        }
    
        protected bool IsDeserializing { get; private set; }
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            IsDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            IsDeserializing = false;
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        protected virtual void ClearNavigationProperties()
        {
            TBL_ModuloWorkFlow_Rutas.Clear();
            TBL_ModuloAPC_Solicitud.Clear();
            TBL_ModuloReclamos_Reclamo.Clear();
            TBL_ModuloWorkFlow_CamposValidacion.Clear();
            TBL_ModuloWorkFlow_Rutas1.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupTBL_ModuloWorkFlow_Rutas(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloWorkFlow_Rutas item in e.NewItems)
                {
                    item.TBL_Admin_EstadosProceso = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloWorkFlow_Rutas", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloWorkFlow_Rutas item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_Admin_EstadosProceso, this))
                    {
                        item.TBL_Admin_EstadosProceso = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloWorkFlow_Rutas", item);
                    }
                }
            }
        }
    
        private void FixupTBL_ModuloAPC_Solicitud(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloAPC_Solicitud item in e.NewItems)
                {
                    item.TBL_Admin_EstadosProceso = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloAPC_Solicitud", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloAPC_Solicitud item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_Admin_EstadosProceso, this))
                    {
                        item.TBL_Admin_EstadosProceso = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloAPC_Solicitud", item);
                    }
                }
            }
        }
    
        private void FixupTBL_ModuloReclamos_Reclamo(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloReclamos_Reclamo item in e.NewItems)
                {
                    item.TBL_Admin_EstadosProceso = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloReclamos_Reclamo", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloReclamos_Reclamo item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_Admin_EstadosProceso, this))
                    {
                        item.TBL_Admin_EstadosProceso = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloReclamos_Reclamo", item);
                    }
                }
            }
        }
    
        private void FixupTBL_ModuloWorkFlow_CamposValidacion(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloWorkFlow_CamposValidacion item in e.NewItems)
                {
                    item.TBL_Admin_EstadosProceso = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloWorkFlow_CamposValidacion", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloWorkFlow_CamposValidacion item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_Admin_EstadosProceso, this))
                    {
                        item.TBL_Admin_EstadosProceso = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloWorkFlow_CamposValidacion", item);
                    }
                }
            }
        }
    
        private void FixupTBL_ModuloWorkFlow_Rutas1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloWorkFlow_Rutas item in e.NewItems)
                {
                    item.TBL_Admin_EstadosProceso1 = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloWorkFlow_Rutas1", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloWorkFlow_Rutas item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_Admin_EstadosProceso1, this))
                    {
                        item.TBL_Admin_EstadosProceso1 = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloWorkFlow_Rutas1", item);
                    }
                }
            }
        }

        #endregion
    }
}
