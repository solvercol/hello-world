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
    
    public partial class TBL_ModuloDocumentosAnexos_Categorias: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int IdCategoria
        {
            get { return _idCategoria; }
            set
            {
                if (_idCategoria != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdCategoria' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idCategoria = value;
                    OnPropertyChanged("IdCategoria");
                }
            }
        }
        private int _idCategoria;
    
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
        public TrackableCollection<TBL_ModuloDocumentosAnexos_Carpetas> TBL_ModuloDocumentosAnexos_Carpetas
        {
            get
            {
                if (_tBL_ModuloDocumentosAnexos_Carpetas == null)
                {
                    _tBL_ModuloDocumentosAnexos_Carpetas = new TrackableCollection<TBL_ModuloDocumentosAnexos_Carpetas>();
                    _tBL_ModuloDocumentosAnexos_Carpetas.CollectionChanged += FixupTBL_ModuloDocumentosAnexos_Carpetas;
                }
                return _tBL_ModuloDocumentosAnexos_Carpetas;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloDocumentosAnexos_Carpetas, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloDocumentosAnexos_Carpetas != null)
                    {
                        _tBL_ModuloDocumentosAnexos_Carpetas.CollectionChanged -= FixupTBL_ModuloDocumentosAnexos_Carpetas;
                    }
                    _tBL_ModuloDocumentosAnexos_Carpetas = value;
                    if (_tBL_ModuloDocumentosAnexos_Carpetas != null)
                    {
                        _tBL_ModuloDocumentosAnexos_Carpetas.CollectionChanged += FixupTBL_ModuloDocumentosAnexos_Carpetas;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloDocumentosAnexos_Carpetas");
                }
            }
        }
        private TrackableCollection<TBL_ModuloDocumentosAnexos_Carpetas> _tBL_ModuloDocumentosAnexos_Carpetas;

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
            TBL_ModuloDocumentosAnexos_Carpetas.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupTBL_ModuloDocumentosAnexos_Carpetas(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloDocumentosAnexos_Carpetas item in e.NewItems)
                {
                    item.TBL_ModuloDocumentosAnexos_Categorias = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloDocumentosAnexos_Carpetas", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloDocumentosAnexos_Carpetas item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_ModuloDocumentosAnexos_Categorias, this))
                    {
                        item.TBL_ModuloDocumentosAnexos_Categorias = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloDocumentosAnexos_Carpetas", item);
                    }
                }
            }
        }

        #endregion
    }
}