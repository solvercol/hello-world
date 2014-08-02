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
    [KnownType(typeof(TBL_Admin_SistemaNotificaciones))]
    [KnownType(typeof(TBL_Admin_Roles))]
    [KnownType(typeof(TBL_ModuloDocumentos_Documento))]
    [KnownType(typeof(TBL_ModuloDocumentos_HistorialDocumento))]
    
    public partial class TBL_Admin_Usuarios: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int IdUser
        {
            get { return _idUser; }
            set
            {
                if (_idUser != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdUser' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idUser = value;
                    OnPropertyChanged("IdUser");
                }
            }
        }
        private int _idUser;
    
        [DataMember]
        public string CodigoUser
        {
            get { return _codigoUser; }
            set
            {
                if (_codigoUser != value)
                {
                    _codigoUser = value;
                    OnPropertyChanged("CodigoUser");
                }
            }
        }
        private string _codigoUser;
    
        [DataMember]
        public string Nombres
        {
            get { return _nombres; }
            set
            {
                if (_nombres != value)
                {
                    _nombres = value;
                    OnPropertyChanged("Nombres");
                }
            }
        }
        private string _nombres;
    
        [DataMember]
        public System.DateTime FechaIngreso
        {
            get { return _fechaIngreso; }
            set
            {
                if (_fechaIngreso != value)
                {
                    _fechaIngreso = value;
                    OnPropertyChanged("FechaIngreso");
                }
            }
        }
        private System.DateTime _fechaIngreso;
    
        [DataMember]
        public Nullable<System.DateTime> lastlogin
        {
            get { return _lastlogin; }
            set
            {
                if (_lastlogin != value)
                {
                    _lastlogin = value;
                    OnPropertyChanged("lastlogin");
                }
            }
        }
        private Nullable<System.DateTime> _lastlogin;
    
        [DataMember]
        public string lastip
        {
            get { return _lastip; }
            set
            {
                if (_lastip != value)
                {
                    _lastip = value;
                    OnPropertyChanged("lastip");
                }
            }
        }
        private string _lastip;
    
        [DataMember]
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }
        private string _userName;
    
        [DataMember]
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }
        private string _password;
    
        [DataMember]
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        private string _email;
    
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
        public TrackableCollection<TBL_Admin_SistemaNotificaciones> TBL_Admin_SistemaNotificaciones
        {
            get
            {
                if (_tBL_Admin_SistemaNotificaciones == null)
                {
                    _tBL_Admin_SistemaNotificaciones = new TrackableCollection<TBL_Admin_SistemaNotificaciones>();
                    _tBL_Admin_SistemaNotificaciones.CollectionChanged += FixupTBL_Admin_SistemaNotificaciones;
                }
                return _tBL_Admin_SistemaNotificaciones;
            }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_SistemaNotificaciones, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_Admin_SistemaNotificaciones != null)
                    {
                        _tBL_Admin_SistemaNotificaciones.CollectionChanged -= FixupTBL_Admin_SistemaNotificaciones;
                    }
                    _tBL_Admin_SistemaNotificaciones = value;
                    if (_tBL_Admin_SistemaNotificaciones != null)
                    {
                        _tBL_Admin_SistemaNotificaciones.CollectionChanged += FixupTBL_Admin_SistemaNotificaciones;
                    }
                    OnNavigationPropertyChanged("TBL_Admin_SistemaNotificaciones");
                }
            }
        }
        private TrackableCollection<TBL_Admin_SistemaNotificaciones> _tBL_Admin_SistemaNotificaciones;
    
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
    
        [DataMember]
        public TrackableCollection<TBL_Admin_Roles> TBL_Admin_Roles1
        {
            get
            {
                if (_tBL_Admin_Roles1 == null)
                {
                    _tBL_Admin_Roles1 = new TrackableCollection<TBL_Admin_Roles>();
                    _tBL_Admin_Roles1.CollectionChanged += FixupTBL_Admin_Roles1;
                }
                return _tBL_Admin_Roles1;
            }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_Roles1, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_Admin_Roles1 != null)
                    {
                        _tBL_Admin_Roles1.CollectionChanged -= FixupTBL_Admin_Roles1;
                    }
                    _tBL_Admin_Roles1 = value;
                    if (_tBL_Admin_Roles1 != null)
                    {
                        _tBL_Admin_Roles1.CollectionChanged += FixupTBL_Admin_Roles1;
                    }
                    OnNavigationPropertyChanged("TBL_Admin_Roles1");
                }
            }
        }
        private TrackableCollection<TBL_Admin_Roles> _tBL_Admin_Roles1;
    
        [DataMember]
        public TrackableCollection<TBL_ModuloDocumentos_Documento> TBL_ModuloDocumentos_Documento
        {
            get
            {
                if (_tBL_ModuloDocumentos_Documento == null)
                {
                    _tBL_ModuloDocumentos_Documento = new TrackableCollection<TBL_ModuloDocumentos_Documento>();
                    _tBL_ModuloDocumentos_Documento.CollectionChanged += FixupTBL_ModuloDocumentos_Documento;
                }
                return _tBL_ModuloDocumentos_Documento;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloDocumentos_Documento, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloDocumentos_Documento != null)
                    {
                        _tBL_ModuloDocumentos_Documento.CollectionChanged -= FixupTBL_ModuloDocumentos_Documento;
                    }
                    _tBL_ModuloDocumentos_Documento = value;
                    if (_tBL_ModuloDocumentos_Documento != null)
                    {
                        _tBL_ModuloDocumentos_Documento.CollectionChanged += FixupTBL_ModuloDocumentos_Documento;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloDocumentos_Documento");
                }
            }
        }
        private TrackableCollection<TBL_ModuloDocumentos_Documento> _tBL_ModuloDocumentos_Documento;
    
        [DataMember]
        public TrackableCollection<TBL_ModuloDocumentos_Documento> TBL_ModuloDocumentos_Documento1
        {
            get
            {
                if (_tBL_ModuloDocumentos_Documento1 == null)
                {
                    _tBL_ModuloDocumentos_Documento1 = new TrackableCollection<TBL_ModuloDocumentos_Documento>();
                    _tBL_ModuloDocumentos_Documento1.CollectionChanged += FixupTBL_ModuloDocumentos_Documento1;
                }
                return _tBL_ModuloDocumentos_Documento1;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloDocumentos_Documento1, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloDocumentos_Documento1 != null)
                    {
                        _tBL_ModuloDocumentos_Documento1.CollectionChanged -= FixupTBL_ModuloDocumentos_Documento1;
                    }
                    _tBL_ModuloDocumentos_Documento1 = value;
                    if (_tBL_ModuloDocumentos_Documento1 != null)
                    {
                        _tBL_ModuloDocumentos_Documento1.CollectionChanged += FixupTBL_ModuloDocumentos_Documento1;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloDocumentos_Documento1");
                }
            }
        }
        private TrackableCollection<TBL_ModuloDocumentos_Documento> _tBL_ModuloDocumentos_Documento1;
    
        [DataMember]
        public TrackableCollection<TBL_ModuloDocumentos_Documento> TBL_ModuloDocumentos_Documento2
        {
            get
            {
                if (_tBL_ModuloDocumentos_Documento2 == null)
                {
                    _tBL_ModuloDocumentos_Documento2 = new TrackableCollection<TBL_ModuloDocumentos_Documento>();
                    _tBL_ModuloDocumentos_Documento2.CollectionChanged += FixupTBL_ModuloDocumentos_Documento2;
                }
                return _tBL_ModuloDocumentos_Documento2;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloDocumentos_Documento2, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloDocumentos_Documento2 != null)
                    {
                        _tBL_ModuloDocumentos_Documento2.CollectionChanged -= FixupTBL_ModuloDocumentos_Documento2;
                    }
                    _tBL_ModuloDocumentos_Documento2 = value;
                    if (_tBL_ModuloDocumentos_Documento2 != null)
                    {
                        _tBL_ModuloDocumentos_Documento2.CollectionChanged += FixupTBL_ModuloDocumentos_Documento2;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloDocumentos_Documento2");
                }
            }
        }
        private TrackableCollection<TBL_ModuloDocumentos_Documento> _tBL_ModuloDocumentos_Documento2;
    
        [DataMember]
        public TrackableCollection<TBL_ModuloDocumentos_HistorialDocumento> TBL_ModuloDocumentos_HistorialDocumento
        {
            get
            {
                if (_tBL_ModuloDocumentos_HistorialDocumento == null)
                {
                    _tBL_ModuloDocumentos_HistorialDocumento = new TrackableCollection<TBL_ModuloDocumentos_HistorialDocumento>();
                    _tBL_ModuloDocumentos_HistorialDocumento.CollectionChanged += FixupTBL_ModuloDocumentos_HistorialDocumento;
                }
                return _tBL_ModuloDocumentos_HistorialDocumento;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloDocumentos_HistorialDocumento, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloDocumentos_HistorialDocumento != null)
                    {
                        _tBL_ModuloDocumentos_HistorialDocumento.CollectionChanged -= FixupTBL_ModuloDocumentos_HistorialDocumento;
                    }
                    _tBL_ModuloDocumentos_HistorialDocumento = value;
                    if (_tBL_ModuloDocumentos_HistorialDocumento != null)
                    {
                        _tBL_ModuloDocumentos_HistorialDocumento.CollectionChanged += FixupTBL_ModuloDocumentos_HistorialDocumento;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloDocumentos_HistorialDocumento");
                }
            }
        }
        private TrackableCollection<TBL_ModuloDocumentos_HistorialDocumento> _tBL_ModuloDocumentos_HistorialDocumento;
    
        [DataMember]
        public TrackableCollection<TBL_ModuloDocumentos_HistorialDocumento> TBL_ModuloDocumentos_HistorialDocumento1
        {
            get
            {
                if (_tBL_ModuloDocumentos_HistorialDocumento1 == null)
                {
                    _tBL_ModuloDocumentos_HistorialDocumento1 = new TrackableCollection<TBL_ModuloDocumentos_HistorialDocumento>();
                    _tBL_ModuloDocumentos_HistorialDocumento1.CollectionChanged += FixupTBL_ModuloDocumentos_HistorialDocumento1;
                }
                return _tBL_ModuloDocumentos_HistorialDocumento1;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloDocumentos_HistorialDocumento1, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloDocumentos_HistorialDocumento1 != null)
                    {
                        _tBL_ModuloDocumentos_HistorialDocumento1.CollectionChanged -= FixupTBL_ModuloDocumentos_HistorialDocumento1;
                    }
                    _tBL_ModuloDocumentos_HistorialDocumento1 = value;
                    if (_tBL_ModuloDocumentos_HistorialDocumento1 != null)
                    {
                        _tBL_ModuloDocumentos_HistorialDocumento1.CollectionChanged += FixupTBL_ModuloDocumentos_HistorialDocumento1;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloDocumentos_HistorialDocumento1");
                }
            }
        }
        private TrackableCollection<TBL_ModuloDocumentos_HistorialDocumento> _tBL_ModuloDocumentos_HistorialDocumento1;
    
        [DataMember]
        public TrackableCollection<TBL_ModuloDocumentos_HistorialDocumento> TBL_ModuloDocumentos_HistorialDocumento2
        {
            get
            {
                if (_tBL_ModuloDocumentos_HistorialDocumento2 == null)
                {
                    _tBL_ModuloDocumentos_HistorialDocumento2 = new TrackableCollection<TBL_ModuloDocumentos_HistorialDocumento>();
                    _tBL_ModuloDocumentos_HistorialDocumento2.CollectionChanged += FixupTBL_ModuloDocumentos_HistorialDocumento2;
                }
                return _tBL_ModuloDocumentos_HistorialDocumento2;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloDocumentos_HistorialDocumento2, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloDocumentos_HistorialDocumento2 != null)
                    {
                        _tBL_ModuloDocumentos_HistorialDocumento2.CollectionChanged -= FixupTBL_ModuloDocumentos_HistorialDocumento2;
                    }
                    _tBL_ModuloDocumentos_HistorialDocumento2 = value;
                    if (_tBL_ModuloDocumentos_HistorialDocumento2 != null)
                    {
                        _tBL_ModuloDocumentos_HistorialDocumento2.CollectionChanged += FixupTBL_ModuloDocumentos_HistorialDocumento2;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloDocumentos_HistorialDocumento2");
                }
            }
        }
        private TrackableCollection<TBL_ModuloDocumentos_HistorialDocumento> _tBL_ModuloDocumentos_HistorialDocumento2;

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
            TBL_Admin_SistemaNotificaciones.Clear();
            TBL_Admin_Roles.Clear();
            TBL_Admin_Roles1.Clear();
            TBL_ModuloDocumentos_Documento.Clear();
            TBL_ModuloDocumentos_Documento1.Clear();
            TBL_ModuloDocumentos_Documento2.Clear();
            TBL_ModuloDocumentos_HistorialDocumento.Clear();
            TBL_ModuloDocumentos_HistorialDocumento1.Clear();
            TBL_ModuloDocumentos_HistorialDocumento2.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupTBL_Admin_SistemaNotificaciones(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_Admin_SistemaNotificaciones item in e.NewItems)
                {
                    item.TBL_Admin_Usuarios = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_Admin_SistemaNotificaciones", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_Admin_SistemaNotificaciones item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_Admin_Usuarios, this))
                    {
                        item.TBL_Admin_Usuarios = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_Admin_SistemaNotificaciones", item);
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
                    if (!item.TBL_Admin_Usuarios.Contains(this))
                    {
                        item.TBL_Admin_Usuarios.Add(this);
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
                    if (item.TBL_Admin_Usuarios.Contains(this))
                    {
                        item.TBL_Admin_Usuarios.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_Admin_Roles", item);
                    }
                }
            }
        }
    
        private void FixupTBL_Admin_Roles1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_Admin_Roles item in e.NewItems)
                {
                    if (!item.TBL_Admin_Usuarios1.Contains(this))
                    {
                        item.TBL_Admin_Usuarios1.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_Admin_Roles1", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_Admin_Roles item in e.OldItems)
                {
                    if (item.TBL_Admin_Usuarios1.Contains(this))
                    {
                        item.TBL_Admin_Usuarios1.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_Admin_Roles1", item);
                    }
                }
            }
        }
    
        private void FixupTBL_ModuloDocumentos_Documento(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloDocumentos_Documento item in e.NewItems)
                {
                    item.TBL_Admin_Usuarios = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloDocumentos_Documento", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloDocumentos_Documento item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_Admin_Usuarios, this))
                    {
                        item.TBL_Admin_Usuarios = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloDocumentos_Documento", item);
                    }
                }
            }
        }
    
        private void FixupTBL_ModuloDocumentos_Documento1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloDocumentos_Documento item in e.NewItems)
                {
                    item.TBL_Admin_Usuarios1 = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloDocumentos_Documento1", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloDocumentos_Documento item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_Admin_Usuarios1, this))
                    {
                        item.TBL_Admin_Usuarios1 = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloDocumentos_Documento1", item);
                    }
                }
            }
        }
    
        private void FixupTBL_ModuloDocumentos_Documento2(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloDocumentos_Documento item in e.NewItems)
                {
                    item.TBL_Admin_Usuarios2 = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloDocumentos_Documento2", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloDocumentos_Documento item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_Admin_Usuarios2, this))
                    {
                        item.TBL_Admin_Usuarios2 = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloDocumentos_Documento2", item);
                    }
                }
            }
        }
    
        private void FixupTBL_ModuloDocumentos_HistorialDocumento(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloDocumentos_HistorialDocumento item in e.NewItems)
                {
                    item.TBL_Admin_Usuarios = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloDocumentos_HistorialDocumento", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloDocumentos_HistorialDocumento item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_Admin_Usuarios, this))
                    {
                        item.TBL_Admin_Usuarios = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloDocumentos_HistorialDocumento", item);
                    }
                }
            }
        }
    
        private void FixupTBL_ModuloDocumentos_HistorialDocumento1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloDocumentos_HistorialDocumento item in e.NewItems)
                {
                    item.TBL_Admin_Usuarios1 = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloDocumentos_HistorialDocumento1", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloDocumentos_HistorialDocumento item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_Admin_Usuarios1, this))
                    {
                        item.TBL_Admin_Usuarios1 = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloDocumentos_HistorialDocumento1", item);
                    }
                }
            }
        }
    
        private void FixupTBL_ModuloDocumentos_HistorialDocumento2(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloDocumentos_HistorialDocumento item in e.NewItems)
                {
                    item.TBL_Admin_Usuarios2 = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloDocumentos_HistorialDocumento2", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloDocumentos_HistorialDocumento item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_Admin_Usuarios2, this))
                    {
                        item.TBL_Admin_Usuarios2 = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloDocumentos_HistorialDocumento2", item);
                    }
                }
            }
        }

        #endregion
    }
}
