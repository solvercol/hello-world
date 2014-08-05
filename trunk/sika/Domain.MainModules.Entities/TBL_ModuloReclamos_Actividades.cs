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
    [KnownType(typeof(TBL_Admin_Usuarios))]
    [KnownType(typeof(TBL_ModuloReclamos_ActividadesReclamo))]
    [KnownType(typeof(TBL_ModuloReclamos_Reclamo))]
    [KnownType(typeof(TBL_ModuloReclamos_AnexosActividad))]
    
    public partial class TBL_ModuloReclamos_Actividades: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public decimal IdActividad
        {
            get { return _idActividad; }
            set
            {
                if (_idActividad != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdActividad' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idActividad = value;
                    OnPropertyChanged("IdActividad");
                }
            }
        }
        private decimal _idActividad;
    
        [DataMember]
        public decimal IdReclamo
        {
            get { return _idReclamo; }
            set
            {
                if (_idReclamo != value)
                {
                    ChangeTracker.RecordOriginalValue("IdReclamo", _idReclamo);
                    if (!IsDeserializing)
                    {
                        if (TBL_ModuloReclamos_Reclamo != null && TBL_ModuloReclamos_Reclamo.IdReclamo != value)
                        {
                            TBL_ModuloReclamos_Reclamo = null;
                        }
                    }
                    _idReclamo = value;
                    OnPropertyChanged("IdReclamo");
                }
            }
        }
        private decimal _idReclamo;
    
        [DataMember]
        public int IdActividadReclamo
        {
            get { return _idActividadReclamo; }
            set
            {
                if (_idActividadReclamo != value)
                {
                    ChangeTracker.RecordOriginalValue("IdActividadReclamo", _idActividadReclamo);
                    if (!IsDeserializing)
                    {
                        if (TBL_ModuloReclamos_ActividadesReclamo != null && TBL_ModuloReclamos_ActividadesReclamo.IdActividad != value)
                        {
                            TBL_ModuloReclamos_ActividadesReclamo = null;
                        }
                    }
                    _idActividadReclamo = value;
                    OnPropertyChanged("IdActividadReclamo");
                }
            }
        }
        private int _idActividadReclamo;
    
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
        public System.DateTime Fecha
        {
            get { return _fecha; }
            set
            {
                if (_fecha != value)
                {
                    _fecha = value;
                    OnPropertyChanged("Fecha");
                }
            }
        }
        private System.DateTime _fecha;
    
        [DataMember]
        public int IdUsuarioAsignacion
        {
            get { return _idUsuarioAsignacion; }
            set
            {
                if (_idUsuarioAsignacion != value)
                {
                    ChangeTracker.RecordOriginalValue("IdUsuarioAsignacion", _idUsuarioAsignacion);
                    if (!IsDeserializing)
                    {
                        if (TBL_Admin_Usuarios2 != null && TBL_Admin_Usuarios2.IdUser != value)
                        {
                            TBL_Admin_Usuarios2 = null;
                        }
                    }
                    _idUsuarioAsignacion = value;
                    OnPropertyChanged("IdUsuarioAsignacion");
                }
            }
        }
        private int _idUsuarioAsignacion;
    
        [DataMember]
        public string ObservacionesCierre
        {
            get { return _observacionesCierre; }
            set
            {
                if (_observacionesCierre != value)
                {
                    _observacionesCierre = value;
                    OnPropertyChanged("ObservacionesCierre");
                }
            }
        }
        private string _observacionesCierre;
    
        [DataMember]
        public string ObservacionesCancelacion
        {
            get { return _observacionesCancelacion; }
            set
            {
                if (_observacionesCancelacion != value)
                {
                    _observacionesCancelacion = value;
                    OnPropertyChanged("ObservacionesCancelacion");
                }
            }
        }
        private string _observacionesCancelacion;
    
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
        public int CreateBy
        {
            get { return _createBy; }
            set
            {
                if (_createBy != value)
                {
                    ChangeTracker.RecordOriginalValue("CreateBy", _createBy);
                    if (!IsDeserializing)
                    {
                        if (TBL_Admin_Usuarios != null && TBL_Admin_Usuarios.IdUser != value)
                        {
                            TBL_Admin_Usuarios = null;
                        }
                    }
                    _createBy = value;
                    OnPropertyChanged("CreateBy");
                }
            }
        }
        private int _createBy;
    
        [DataMember]
        public System.DateTime CreateOn
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
        private System.DateTime _createOn;
    
        [DataMember]
        public int ModifiedBy
        {
            get { return _modifiedBy; }
            set
            {
                if (_modifiedBy != value)
                {
                    ChangeTracker.RecordOriginalValue("ModifiedBy", _modifiedBy);
                    if (!IsDeserializing)
                    {
                        if (TBL_Admin_Usuarios1 != null && TBL_Admin_Usuarios1.IdUser != value)
                        {
                            TBL_Admin_Usuarios1 = null;
                        }
                    }
                    _modifiedBy = value;
                    OnPropertyChanged("ModifiedBy");
                }
            }
        }
        private int _modifiedBy;
    
        [DataMember]
        public System.DateTime ModifiedOn
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
        private System.DateTime _modifiedOn;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TBL_Admin_Usuarios TBL_Admin_Usuarios
        {
            get { return _tBL_Admin_Usuarios; }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_Usuarios, value))
                {
                    var previousValue = _tBL_Admin_Usuarios;
                    _tBL_Admin_Usuarios = value;
                    FixupTBL_Admin_Usuarios(previousValue);
                    OnNavigationPropertyChanged("TBL_Admin_Usuarios");
                }
            }
        }
        private TBL_Admin_Usuarios _tBL_Admin_Usuarios;
    
        [DataMember]
        public TBL_Admin_Usuarios TBL_Admin_Usuarios1
        {
            get { return _tBL_Admin_Usuarios1; }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_Usuarios1, value))
                {
                    var previousValue = _tBL_Admin_Usuarios1;
                    _tBL_Admin_Usuarios1 = value;
                    FixupTBL_Admin_Usuarios1(previousValue);
                    OnNavigationPropertyChanged("TBL_Admin_Usuarios1");
                }
            }
        }
        private TBL_Admin_Usuarios _tBL_Admin_Usuarios1;
    
        [DataMember]
        public TBL_Admin_Usuarios TBL_Admin_Usuarios2
        {
            get { return _tBL_Admin_Usuarios2; }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_Usuarios2, value))
                {
                    var previousValue = _tBL_Admin_Usuarios2;
                    _tBL_Admin_Usuarios2 = value;
                    FixupTBL_Admin_Usuarios2(previousValue);
                    OnNavigationPropertyChanged("TBL_Admin_Usuarios2");
                }
            }
        }
        private TBL_Admin_Usuarios _tBL_Admin_Usuarios2;
    
        [DataMember]
        public TBL_ModuloReclamos_ActividadesReclamo TBL_ModuloReclamos_ActividadesReclamo
        {
            get { return _tBL_ModuloReclamos_ActividadesReclamo; }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloReclamos_ActividadesReclamo, value))
                {
                    var previousValue = _tBL_ModuloReclamos_ActividadesReclamo;
                    _tBL_ModuloReclamos_ActividadesReclamo = value;
                    FixupTBL_ModuloReclamos_ActividadesReclamo(previousValue);
                    OnNavigationPropertyChanged("TBL_ModuloReclamos_ActividadesReclamo");
                }
            }
        }
        private TBL_ModuloReclamos_ActividadesReclamo _tBL_ModuloReclamos_ActividadesReclamo;
    
        [DataMember]
        public TBL_ModuloReclamos_Reclamo TBL_ModuloReclamos_Reclamo
        {
            get { return _tBL_ModuloReclamos_Reclamo; }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloReclamos_Reclamo, value))
                {
                    var previousValue = _tBL_ModuloReclamos_Reclamo;
                    _tBL_ModuloReclamos_Reclamo = value;
                    FixupTBL_ModuloReclamos_Reclamo(previousValue);
                    OnNavigationPropertyChanged("TBL_ModuloReclamos_Reclamo");
                }
            }
        }
        private TBL_ModuloReclamos_Reclamo _tBL_ModuloReclamos_Reclamo;
    
        [DataMember]
        public TrackableCollection<TBL_ModuloReclamos_AnexosActividad> TBL_ModuloReclamos_AnexosActividad
        {
            get
            {
                if (_tBL_ModuloReclamos_AnexosActividad == null)
                {
                    _tBL_ModuloReclamos_AnexosActividad = new TrackableCollection<TBL_ModuloReclamos_AnexosActividad>();
                    _tBL_ModuloReclamos_AnexosActividad.CollectionChanged += FixupTBL_ModuloReclamos_AnexosActividad;
                }
                return _tBL_ModuloReclamos_AnexosActividad;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloReclamos_AnexosActividad, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloReclamos_AnexosActividad != null)
                    {
                        _tBL_ModuloReclamos_AnexosActividad.CollectionChanged -= FixupTBL_ModuloReclamos_AnexosActividad;
                    }
                    _tBL_ModuloReclamos_AnexosActividad = value;
                    if (_tBL_ModuloReclamos_AnexosActividad != null)
                    {
                        _tBL_ModuloReclamos_AnexosActividad.CollectionChanged += FixupTBL_ModuloReclamos_AnexosActividad;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloReclamos_AnexosActividad");
                }
            }
        }
        private TrackableCollection<TBL_ModuloReclamos_AnexosActividad> _tBL_ModuloReclamos_AnexosActividad;
    
        [DataMember]
        public TrackableCollection<TBL_Admin_Usuarios> TBL_Admin_Usuarios3
        {
            get
            {
                if (_tBL_Admin_Usuarios3 == null)
                {
                    _tBL_Admin_Usuarios3 = new TrackableCollection<TBL_Admin_Usuarios>();
                    _tBL_Admin_Usuarios3.CollectionChanged += FixupTBL_Admin_Usuarios3;
                }
                return _tBL_Admin_Usuarios3;
            }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_Usuarios3, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_Admin_Usuarios3 != null)
                    {
                        _tBL_Admin_Usuarios3.CollectionChanged -= FixupTBL_Admin_Usuarios3;
                    }
                    _tBL_Admin_Usuarios3 = value;
                    if (_tBL_Admin_Usuarios3 != null)
                    {
                        _tBL_Admin_Usuarios3.CollectionChanged += FixupTBL_Admin_Usuarios3;
                    }
                    OnNavigationPropertyChanged("TBL_Admin_Usuarios3");
                }
            }
        }
        private TrackableCollection<TBL_Admin_Usuarios> _tBL_Admin_Usuarios3;

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
            TBL_Admin_Usuarios = null;
            TBL_Admin_Usuarios1 = null;
            TBL_Admin_Usuarios2 = null;
            TBL_ModuloReclamos_ActividadesReclamo = null;
            TBL_ModuloReclamos_Reclamo = null;
            TBL_ModuloReclamos_AnexosActividad.Clear();
            TBL_Admin_Usuarios3.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupTBL_Admin_Usuarios(TBL_Admin_Usuarios previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_Actividades.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_Actividades.Remove(this);
            }
    
            if (TBL_Admin_Usuarios != null)
            {
                if (!TBL_Admin_Usuarios.TBL_ModuloReclamos_Actividades.Contains(this))
                {
                    TBL_Admin_Usuarios.TBL_ModuloReclamos_Actividades.Add(this);
                }
    
                CreateBy = TBL_Admin_Usuarios.IdUser;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_Admin_Usuarios")
                    && (ChangeTracker.OriginalValues["TBL_Admin_Usuarios"] == TBL_Admin_Usuarios))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_Admin_Usuarios");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_Admin_Usuarios", previousValue);
                }
                if (TBL_Admin_Usuarios != null && !TBL_Admin_Usuarios.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_Admin_Usuarios.StartTracking();
                }
            }
        }
    
        private void FixupTBL_Admin_Usuarios1(TBL_Admin_Usuarios previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_Actividades1.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_Actividades1.Remove(this);
            }
    
            if (TBL_Admin_Usuarios1 != null)
            {
                if (!TBL_Admin_Usuarios1.TBL_ModuloReclamos_Actividades1.Contains(this))
                {
                    TBL_Admin_Usuarios1.TBL_ModuloReclamos_Actividades1.Add(this);
                }
    
                ModifiedBy = TBL_Admin_Usuarios1.IdUser;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_Admin_Usuarios1")
                    && (ChangeTracker.OriginalValues["TBL_Admin_Usuarios1"] == TBL_Admin_Usuarios1))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_Admin_Usuarios1");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_Admin_Usuarios1", previousValue);
                }
                if (TBL_Admin_Usuarios1 != null && !TBL_Admin_Usuarios1.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_Admin_Usuarios1.StartTracking();
                }
            }
        }
    
        private void FixupTBL_Admin_Usuarios2(TBL_Admin_Usuarios previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_Actividades2.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_Actividades2.Remove(this);
            }
    
            if (TBL_Admin_Usuarios2 != null)
            {
                if (!TBL_Admin_Usuarios2.TBL_ModuloReclamos_Actividades2.Contains(this))
                {
                    TBL_Admin_Usuarios2.TBL_ModuloReclamos_Actividades2.Add(this);
                }
    
                IdUsuarioAsignacion = TBL_Admin_Usuarios2.IdUser;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_Admin_Usuarios2")
                    && (ChangeTracker.OriginalValues["TBL_Admin_Usuarios2"] == TBL_Admin_Usuarios2))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_Admin_Usuarios2");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_Admin_Usuarios2", previousValue);
                }
                if (TBL_Admin_Usuarios2 != null && !TBL_Admin_Usuarios2.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_Admin_Usuarios2.StartTracking();
                }
            }
        }
    
        private void FixupTBL_ModuloReclamos_ActividadesReclamo(TBL_ModuloReclamos_ActividadesReclamo previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_Actividades.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_Actividades.Remove(this);
            }
    
            if (TBL_ModuloReclamos_ActividadesReclamo != null)
            {
                if (!TBL_ModuloReclamos_ActividadesReclamo.TBL_ModuloReclamos_Actividades.Contains(this))
                {
                    TBL_ModuloReclamos_ActividadesReclamo.TBL_ModuloReclamos_Actividades.Add(this);
                }
    
                IdActividadReclamo = TBL_ModuloReclamos_ActividadesReclamo.IdActividad;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_ModuloReclamos_ActividadesReclamo")
                    && (ChangeTracker.OriginalValues["TBL_ModuloReclamos_ActividadesReclamo"] == TBL_ModuloReclamos_ActividadesReclamo))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_ModuloReclamos_ActividadesReclamo");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_ModuloReclamos_ActividadesReclamo", previousValue);
                }
                if (TBL_ModuloReclamos_ActividadesReclamo != null && !TBL_ModuloReclamos_ActividadesReclamo.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_ModuloReclamos_ActividadesReclamo.StartTracking();
                }
            }
        }
    
        private void FixupTBL_ModuloReclamos_Reclamo(TBL_ModuloReclamos_Reclamo previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_Actividades.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_Actividades.Remove(this);
            }
    
            if (TBL_ModuloReclamos_Reclamo != null)
            {
                if (!TBL_ModuloReclamos_Reclamo.TBL_ModuloReclamos_Actividades.Contains(this))
                {
                    TBL_ModuloReclamos_Reclamo.TBL_ModuloReclamos_Actividades.Add(this);
                }
    
                IdReclamo = TBL_ModuloReclamos_Reclamo.IdReclamo;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_ModuloReclamos_Reclamo")
                    && (ChangeTracker.OriginalValues["TBL_ModuloReclamos_Reclamo"] == TBL_ModuloReclamos_Reclamo))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_ModuloReclamos_Reclamo");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_ModuloReclamos_Reclamo", previousValue);
                }
                if (TBL_ModuloReclamos_Reclamo != null && !TBL_ModuloReclamos_Reclamo.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_ModuloReclamos_Reclamo.StartTracking();
                }
            }
        }
    
        private void FixupTBL_ModuloReclamos_AnexosActividad(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloReclamos_AnexosActividad item in e.NewItems)
                {
                    item.TBL_ModuloReclamos_Actividades = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloReclamos_AnexosActividad", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloReclamos_AnexosActividad item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_ModuloReclamos_Actividades, this))
                    {
                        item.TBL_ModuloReclamos_Actividades = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloReclamos_AnexosActividad", item);
                    }
                }
            }
        }
    
        private void FixupTBL_Admin_Usuarios3(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_Admin_Usuarios item in e.NewItems)
                {
                    if (!item.TBL_ModuloReclamos_Actividades3.Contains(this))
                    {
                        item.TBL_ModuloReclamos_Actividades3.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_Admin_Usuarios3", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_Admin_Usuarios item in e.OldItems)
                {
                    if (item.TBL_ModuloReclamos_Actividades3.Contains(this))
                    {
                        item.TBL_ModuloReclamos_Actividades3.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_Admin_Usuarios3", item);
                    }
                }
            }
        }

        #endregion
    }
}
