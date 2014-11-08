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
    [KnownType(typeof(TBL_ModuloDocumentosAnexos_Carpetas))]
    [KnownType(typeof(TBL_ModuloReclamos_Reclamo))]
    [KnownType(typeof(TBL_ModuloDocumentosAnexos_Documento))]
    [KnownType(typeof(TBL_Admin_Roles))]
    
    public partial class TBL_ModuloDocumentosAnexos_Carpetas: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int IdFolder
        {
            get { return _idFolder; }
            set
            {
                if (_idFolder != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdFolder' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idFolder = value;
                    OnPropertyChanged("IdFolder");
                }
            }
        }
        private int _idFolder;
    
        [DataMember]
        public Nullable<int> IdParent
        {
            get { return _idParent; }
            set
            {
                if (_idParent != value)
                {
                    ChangeTracker.RecordOriginalValue("IdParent", _idParent);
                    if (!IsDeserializing)
                    {
                        if (TBL_ModuloDocumentosAnexos_Carpetas2 != null && TBL_ModuloDocumentosAnexos_Carpetas2.IdFolder != value)
                        {
                            TBL_ModuloDocumentosAnexos_Carpetas2 = null;
                        }
                    }
                    _idParent = value;
                    OnPropertyChanged("IdParent");
                }
            }
        }
        private Nullable<int> _idParent;
    
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
        public string CreatedBy
        {
            get { return _createdBy; }
            set
            {
                if (_createdBy != value)
                {
                    _createdBy = value;
                    OnPropertyChanged("CreatedBy");
                }
            }
        }
        private string _createdBy;
    
        [DataMember]
        public Nullable<System.DateTime> CreatedOn
        {
            get { return _createdOn; }
            set
            {
                if (_createdOn != value)
                {
                    _createdOn = value;
                    OnPropertyChanged("CreatedOn");
                }
            }
        }
        private Nullable<System.DateTime> _createdOn;
    
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
        public TrackableCollection<TBL_ModuloDocumentosAnexos_Carpetas> TBL_ModuloDocumentosAnexos_Carpetas1
        {
            get
            {
                if (_tBL_ModuloDocumentosAnexos_Carpetas1 == null)
                {
                    _tBL_ModuloDocumentosAnexos_Carpetas1 = new TrackableCollection<TBL_ModuloDocumentosAnexos_Carpetas>();
                    _tBL_ModuloDocumentosAnexos_Carpetas1.CollectionChanged += FixupTBL_ModuloDocumentosAnexos_Carpetas1;
                }
                return _tBL_ModuloDocumentosAnexos_Carpetas1;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloDocumentosAnexos_Carpetas1, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloDocumentosAnexos_Carpetas1 != null)
                    {
                        _tBL_ModuloDocumentosAnexos_Carpetas1.CollectionChanged -= FixupTBL_ModuloDocumentosAnexos_Carpetas1;
                    }
                    _tBL_ModuloDocumentosAnexos_Carpetas1 = value;
                    if (_tBL_ModuloDocumentosAnexos_Carpetas1 != null)
                    {
                        _tBL_ModuloDocumentosAnexos_Carpetas1.CollectionChanged += FixupTBL_ModuloDocumentosAnexos_Carpetas1;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloDocumentosAnexos_Carpetas1");
                }
            }
        }
        private TrackableCollection<TBL_ModuloDocumentosAnexos_Carpetas> _tBL_ModuloDocumentosAnexos_Carpetas1;
    
        [DataMember]
        public TBL_ModuloDocumentosAnexos_Carpetas TBL_ModuloDocumentosAnexos_Carpetas2
        {
            get { return _tBL_ModuloDocumentosAnexos_Carpetas2; }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloDocumentosAnexos_Carpetas2, value))
                {
                    var previousValue = _tBL_ModuloDocumentosAnexos_Carpetas2;
                    _tBL_ModuloDocumentosAnexos_Carpetas2 = value;
                    FixupTBL_ModuloDocumentosAnexos_Carpetas2(previousValue);
                    OnNavigationPropertyChanged("TBL_ModuloDocumentosAnexos_Carpetas2");
                }
            }
        }
        private TBL_ModuloDocumentosAnexos_Carpetas _tBL_ModuloDocumentosAnexos_Carpetas2;
    
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
        public TrackableCollection<TBL_ModuloDocumentosAnexos_Documento> TBL_ModuloDocumentosAnexos_Documento
        {
            get
            {
                if (_tBL_ModuloDocumentosAnexos_Documento == null)
                {
                    _tBL_ModuloDocumentosAnexos_Documento = new TrackableCollection<TBL_ModuloDocumentosAnexos_Documento>();
                    _tBL_ModuloDocumentosAnexos_Documento.CollectionChanged += FixupTBL_ModuloDocumentosAnexos_Documento;
                }
                return _tBL_ModuloDocumentosAnexos_Documento;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloDocumentosAnexos_Documento, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloDocumentosAnexos_Documento != null)
                    {
                        _tBL_ModuloDocumentosAnexos_Documento.CollectionChanged -= FixupTBL_ModuloDocumentosAnexos_Documento;
                    }
                    _tBL_ModuloDocumentosAnexos_Documento = value;
                    if (_tBL_ModuloDocumentosAnexos_Documento != null)
                    {
                        _tBL_ModuloDocumentosAnexos_Documento.CollectionChanged += FixupTBL_ModuloDocumentosAnexos_Documento;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloDocumentosAnexos_Documento");
                }
            }
        }
        private TrackableCollection<TBL_ModuloDocumentosAnexos_Documento> _tBL_ModuloDocumentosAnexos_Documento;
    
        [DataMember]
        public TrackableCollection<TBL_Admin_Roles> TBL_Admin_Roles
        {
            get
            {
                if (_tBL_Admin_Roles == null)
                {
                    _tBL_Admin_Roles = new TrackableCollection<TBL_Admin_Roles>();
                    _tBL_Admin_Roles.CollectionChanged += FixupTBL_Admin_Roles;
                }
                return _tBL_Admin_Roles;
            }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_Roles, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_Admin_Roles != null)
                    {
                        _tBL_Admin_Roles.CollectionChanged -= FixupTBL_Admin_Roles;
                    }
                    _tBL_Admin_Roles = value;
                    if (_tBL_Admin_Roles != null)
                    {
                        _tBL_Admin_Roles.CollectionChanged += FixupTBL_Admin_Roles;
                    }
                    OnNavigationPropertyChanged("TBL_Admin_Roles");
                }
            }
        }
        private TrackableCollection<TBL_Admin_Roles> _tBL_Admin_Roles;

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
            TBL_ModuloDocumentosAnexos_Carpetas1.Clear();
            TBL_ModuloDocumentosAnexos_Carpetas2 = null;
            TBL_ModuloReclamos_Reclamo = null;
            TBL_ModuloDocumentosAnexos_Documento.Clear();
            TBL_Admin_Roles.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupTBL_ModuloDocumentosAnexos_Carpetas2(TBL_ModuloDocumentosAnexos_Carpetas previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloDocumentosAnexos_Carpetas1.Contains(this))
            {
                previousValue.TBL_ModuloDocumentosAnexos_Carpetas1.Remove(this);
            }
    
            if (TBL_ModuloDocumentosAnexos_Carpetas2 != null)
            {
                if (!TBL_ModuloDocumentosAnexos_Carpetas2.TBL_ModuloDocumentosAnexos_Carpetas1.Contains(this))
                {
                    TBL_ModuloDocumentosAnexos_Carpetas2.TBL_ModuloDocumentosAnexos_Carpetas1.Add(this);
                }
    
                IdParent = TBL_ModuloDocumentosAnexos_Carpetas2.IdFolder;
            }
            else if (!skipKeys)
            {
                IdParent = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_ModuloDocumentosAnexos_Carpetas2")
                    && (ChangeTracker.OriginalValues["TBL_ModuloDocumentosAnexos_Carpetas2"] == TBL_ModuloDocumentosAnexos_Carpetas2))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_ModuloDocumentosAnexos_Carpetas2");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_ModuloDocumentosAnexos_Carpetas2", previousValue);
                }
                if (TBL_ModuloDocumentosAnexos_Carpetas2 != null && !TBL_ModuloDocumentosAnexos_Carpetas2.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_ModuloDocumentosAnexos_Carpetas2.StartTracking();
                }
            }
        }
    
        private void FixupTBL_ModuloReclamos_Reclamo(TBL_ModuloReclamos_Reclamo previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloDocumentosAnexos_Carpetas.Contains(this))
            {
                previousValue.TBL_ModuloDocumentosAnexos_Carpetas.Remove(this);
            }
    
            if (TBL_ModuloReclamos_Reclamo != null)
            {
                if (!TBL_ModuloReclamos_Reclamo.TBL_ModuloDocumentosAnexos_Carpetas.Contains(this))
                {
                    TBL_ModuloReclamos_Reclamo.TBL_ModuloDocumentosAnexos_Carpetas.Add(this);
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
    
        private void FixupTBL_ModuloDocumentosAnexos_Carpetas1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloDocumentosAnexos_Carpetas item in e.NewItems)
                {
                    item.TBL_ModuloDocumentosAnexos_Carpetas2 = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloDocumentosAnexos_Carpetas1", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloDocumentosAnexos_Carpetas item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_ModuloDocumentosAnexos_Carpetas2, this))
                    {
                        item.TBL_ModuloDocumentosAnexos_Carpetas2 = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloDocumentosAnexos_Carpetas1", item);
                    }
                }
            }
        }
    
        private void FixupTBL_ModuloDocumentosAnexos_Documento(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloDocumentosAnexos_Documento item in e.NewItems)
                {
                    item.TBL_ModuloDocumentosAnexos_Carpetas = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloDocumentosAnexos_Documento", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloDocumentosAnexos_Documento item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_ModuloDocumentosAnexos_Carpetas, this))
                    {
                        item.TBL_ModuloDocumentosAnexos_Carpetas = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloDocumentosAnexos_Documento", item);
                    }
                }
            }
        }
    
        private void FixupTBL_Admin_Roles(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_Admin_Roles item in e.NewItems)
                {
                    if (!item.TBL_ModuloDocumentosAnexos_Carpetas.Contains(this))
                    {
                        item.TBL_ModuloDocumentosAnexos_Carpetas.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_Admin_Roles", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_Admin_Roles item in e.OldItems)
                {
                    if (item.TBL_ModuloDocumentosAnexos_Carpetas.Contains(this))
                    {
                        item.TBL_ModuloDocumentosAnexos_Carpetas.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_Admin_Roles", item);
                    }
                }
            }
        }

        #endregion
    }
}
