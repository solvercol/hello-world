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
    [KnownType(typeof(TBL_ModuloReclamos_CategoriasReclamo))]
    [KnownType(typeof(TBL_ModuloReclamos_Reclamo))]
    
    public partial class TBL_ModuloReclamos_TipoReclamo: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int IdTipoReclamo
        {
            get { return _idTipoReclamo; }
            set
            {
                if (_idTipoReclamo != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdTipoReclamo' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idTipoReclamo = value;
                    OnPropertyChanged("IdTipoReclamo");
                }
            }
        }
        private int _idTipoReclamo;
    
        [DataMember]
        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged("Nombre");
                }
            }
        }
        private string _nombre;
    
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
        public TrackableCollection<TBL_ModuloReclamos_ActividadesReclamo> TBL_ModuloReclamos_ActividadesReclamo
        {
            get
            {
                if (_tBL_ModuloReclamos_ActividadesReclamo == null)
                {
                    _tBL_ModuloReclamos_ActividadesReclamo = new TrackableCollection<TBL_ModuloReclamos_ActividadesReclamo>();
                    _tBL_ModuloReclamos_ActividadesReclamo.CollectionChanged += FixupTBL_ModuloReclamos_ActividadesReclamo;
                }
                return _tBL_ModuloReclamos_ActividadesReclamo;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloReclamos_ActividadesReclamo, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloReclamos_ActividadesReclamo != null)
                    {
                        _tBL_ModuloReclamos_ActividadesReclamo.CollectionChanged -= FixupTBL_ModuloReclamos_ActividadesReclamo;
                    }
                    _tBL_ModuloReclamos_ActividadesReclamo = value;
                    if (_tBL_ModuloReclamos_ActividadesReclamo != null)
                    {
                        _tBL_ModuloReclamos_ActividadesReclamo.CollectionChanged += FixupTBL_ModuloReclamos_ActividadesReclamo;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloReclamos_ActividadesReclamo");
                }
            }
        }
        private TrackableCollection<TBL_ModuloReclamos_ActividadesReclamo> _tBL_ModuloReclamos_ActividadesReclamo;
    
        [DataMember]
        public TrackableCollection<TBL_ModuloReclamos_CategoriasReclamo> TBL_ModuloReclamos_CategoriasReclamo
        {
            get
            {
                if (_tBL_ModuloReclamos_CategoriasReclamo == null)
                {
                    _tBL_ModuloReclamos_CategoriasReclamo = new TrackableCollection<TBL_ModuloReclamos_CategoriasReclamo>();
                    _tBL_ModuloReclamos_CategoriasReclamo.CollectionChanged += FixupTBL_ModuloReclamos_CategoriasReclamo;
                }
                return _tBL_ModuloReclamos_CategoriasReclamo;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloReclamos_CategoriasReclamo, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloReclamos_CategoriasReclamo != null)
                    {
                        _tBL_ModuloReclamos_CategoriasReclamo.CollectionChanged -= FixupTBL_ModuloReclamos_CategoriasReclamo;
                    }
                    _tBL_ModuloReclamos_CategoriasReclamo = value;
                    if (_tBL_ModuloReclamos_CategoriasReclamo != null)
                    {
                        _tBL_ModuloReclamos_CategoriasReclamo.CollectionChanged += FixupTBL_ModuloReclamos_CategoriasReclamo;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloReclamos_CategoriasReclamo");
                }
            }
        }
        private TrackableCollection<TBL_ModuloReclamos_CategoriasReclamo> _tBL_ModuloReclamos_CategoriasReclamo;
    
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
            TBL_ModuloReclamos_ActividadesReclamo.Clear();
            TBL_ModuloReclamos_CategoriasReclamo.Clear();
            TBL_ModuloReclamos_Reclamo.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupTBL_Admin_Usuarios(TBL_Admin_Usuarios previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_TipoReclamo.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_TipoReclamo.Remove(this);
            }
    
            if (TBL_Admin_Usuarios != null)
            {
                if (!TBL_Admin_Usuarios.TBL_ModuloReclamos_TipoReclamo.Contains(this))
                {
                    TBL_Admin_Usuarios.TBL_ModuloReclamos_TipoReclamo.Add(this);
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
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_TipoReclamo1.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_TipoReclamo1.Remove(this);
            }
    
            if (TBL_Admin_Usuarios1 != null)
            {
                if (!TBL_Admin_Usuarios1.TBL_ModuloReclamos_TipoReclamo1.Contains(this))
                {
                    TBL_Admin_Usuarios1.TBL_ModuloReclamos_TipoReclamo1.Add(this);
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
    
        private void FixupTBL_ModuloReclamos_ActividadesReclamo(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloReclamos_ActividadesReclamo item in e.NewItems)
                {
                    item.TBL_ModuloReclamos_TipoReclamo = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloReclamos_ActividadesReclamo", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloReclamos_ActividadesReclamo item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_ModuloReclamos_TipoReclamo, this))
                    {
                        item.TBL_ModuloReclamos_TipoReclamo = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloReclamos_ActividadesReclamo", item);
                    }
                }
            }
        }
    
        private void FixupTBL_ModuloReclamos_CategoriasReclamo(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloReclamos_CategoriasReclamo item in e.NewItems)
                {
                    item.TBL_ModuloReclamos_TipoReclamo = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloReclamos_CategoriasReclamo", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloReclamos_CategoriasReclamo item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_ModuloReclamos_TipoReclamo, this))
                    {
                        item.TBL_ModuloReclamos_TipoReclamo = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloReclamos_CategoriasReclamo", item);
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
                    item.TBL_ModuloReclamos_TipoReclamo = this;
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
                    if (ReferenceEquals(item.TBL_ModuloReclamos_TipoReclamo, this))
                    {
                        item.TBL_ModuloReclamos_TipoReclamo = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloReclamos_Reclamo", item);
                    }
                }
            }
        }

        #endregion
    }
}
