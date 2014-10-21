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
    [KnownType(typeof(TBL_ModuloDocumentosAnexos_Contenido))]
    
    public partial class TBL_ModuloDocumentosAnexos_Documento: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int IdDocumento
        {
            get { return _idDocumento; }
            set
            {
                if (_idDocumento != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdDocumento' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idDocumento = value;
                    OnPropertyChanged("IdDocumento");
                }
            }
        }
        private int _idDocumento;
    
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
        public Nullable<int> IdEstado
        {
            get { return _idEstado; }
            set
            {
                if (_idEstado != value)
                {
                    _idEstado = value;
                    OnPropertyChanged("IdEstado");
                }
            }
        }
        private Nullable<int> _idEstado;
    
        [DataMember]
        public int IdFolder
        {
            get { return _idFolder; }
            set
            {
                if (_idFolder != value)
                {
                    ChangeTracker.RecordOriginalValue("IdFolder", _idFolder);
                    if (!IsDeserializing)
                    {
                        if (TBL_ModuloDocumentosAnexos_Carpetas != null && TBL_ModuloDocumentosAnexos_Carpetas.IdFolder != value)
                        {
                            TBL_ModuloDocumentosAnexos_Carpetas = null;
                        }
                    }
                    _idFolder = value;
                    OnPropertyChanged("IdFolder");
                }
            }
        }
        private int _idFolder;
    
        [DataMember]
        public Nullable<int> OwnerId
        {
            get { return _ownerId; }
            set
            {
                if (_ownerId != value)
                {
                    _ownerId = value;
                    OnPropertyChanged("OwnerId");
                }
            }
        }
        private Nullable<int> _ownerId;
    
        [DataMember]
        public Nullable<System.DateTime> FechaVencimiento
        {
            get { return _fechaVencimiento; }
            set
            {
                if (_fechaVencimiento != value)
                {
                    _fechaVencimiento = value;
                    OnPropertyChanged("FechaVencimiento");
                }
            }
        }
        private Nullable<System.DateTime> _fechaVencimiento;
    
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
        public TBL_ModuloDocumentosAnexos_Carpetas TBL_ModuloDocumentosAnexos_Carpetas
        {
            get { return _tBL_ModuloDocumentosAnexos_Carpetas; }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloDocumentosAnexos_Carpetas, value))
                {
                    var previousValue = _tBL_ModuloDocumentosAnexos_Carpetas;
                    _tBL_ModuloDocumentosAnexos_Carpetas = value;
                    FixupTBL_ModuloDocumentosAnexos_Carpetas(previousValue);
                    OnNavigationPropertyChanged("TBL_ModuloDocumentosAnexos_Carpetas");
                }
            }
        }
        private TBL_ModuloDocumentosAnexos_Carpetas _tBL_ModuloDocumentosAnexos_Carpetas;
    
        [DataMember]
        public TrackableCollection<TBL_ModuloDocumentosAnexos_Contenido> TBL_ModuloDocumentosAnexos_Contenido
        {
            get
            {
                if (_tBL_ModuloDocumentosAnexos_Contenido == null)
                {
                    _tBL_ModuloDocumentosAnexos_Contenido = new TrackableCollection<TBL_ModuloDocumentosAnexos_Contenido>();
                    _tBL_ModuloDocumentosAnexos_Contenido.CollectionChanged += FixupTBL_ModuloDocumentosAnexos_Contenido;
                }
                return _tBL_ModuloDocumentosAnexos_Contenido;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloDocumentosAnexos_Contenido, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloDocumentosAnexos_Contenido != null)
                    {
                        _tBL_ModuloDocumentosAnexos_Contenido.CollectionChanged -= FixupTBL_ModuloDocumentosAnexos_Contenido;
                    }
                    _tBL_ModuloDocumentosAnexos_Contenido = value;
                    if (_tBL_ModuloDocumentosAnexos_Contenido != null)
                    {
                        _tBL_ModuloDocumentosAnexos_Contenido.CollectionChanged += FixupTBL_ModuloDocumentosAnexos_Contenido;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloDocumentosAnexos_Contenido");
                }
            }
        }
        private TrackableCollection<TBL_ModuloDocumentosAnexos_Contenido> _tBL_ModuloDocumentosAnexos_Contenido;

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
            TBL_ModuloDocumentosAnexos_Carpetas = null;
            TBL_ModuloDocumentosAnexos_Contenido.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupTBL_ModuloDocumentosAnexos_Carpetas(TBL_ModuloDocumentosAnexos_Carpetas previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloDocumentosAnexos_Documento.Contains(this))
            {
                previousValue.TBL_ModuloDocumentosAnexos_Documento.Remove(this);
            }
    
            if (TBL_ModuloDocumentosAnexos_Carpetas != null)
            {
                if (!TBL_ModuloDocumentosAnexos_Carpetas.TBL_ModuloDocumentosAnexos_Documento.Contains(this))
                {
                    TBL_ModuloDocumentosAnexos_Carpetas.TBL_ModuloDocumentosAnexos_Documento.Add(this);
                }
    
                IdFolder = TBL_ModuloDocumentosAnexos_Carpetas.IdFolder;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_ModuloDocumentosAnexos_Carpetas")
                    && (ChangeTracker.OriginalValues["TBL_ModuloDocumentosAnexos_Carpetas"] == TBL_ModuloDocumentosAnexos_Carpetas))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_ModuloDocumentosAnexos_Carpetas");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_ModuloDocumentosAnexos_Carpetas", previousValue);
                }
                if (TBL_ModuloDocumentosAnexos_Carpetas != null && !TBL_ModuloDocumentosAnexos_Carpetas.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_ModuloDocumentosAnexos_Carpetas.StartTracking();
                }
            }
        }
    
        private void FixupTBL_ModuloDocumentosAnexos_Contenido(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloDocumentosAnexos_Contenido item in e.NewItems)
                {
                    item.TBL_ModuloDocumentosAnexos_Documento = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloDocumentosAnexos_Contenido", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloDocumentosAnexos_Contenido item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_ModuloDocumentosAnexos_Documento, this))
                    {
                        item.TBL_ModuloDocumentosAnexos_Documento = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloDocumentosAnexos_Contenido", item);
                    }
                }
            }
        }

        #endregion
    }
}