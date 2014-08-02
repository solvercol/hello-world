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
    [KnownType(typeof(TBL_ModuloDocumentos_Documento))]
    [KnownType(typeof(TBL_ModuloDocumentos_HistorialDocumento))]
    
    public partial class TBL_ModuloDocumentos_Categorias: IObjectWithChangeTracker, INotifyPropertyChanged
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
        public int Nivel
        {
            get { return _nivel; }
            set
            {
                if (_nivel != value)
                {
                    _nivel = value;
                    OnPropertyChanged("Nivel");
                }
            }
        }
        private int _nivel;
    
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
            TBL_ModuloDocumentos_Documento.Clear();
            TBL_ModuloDocumentos_Documento1.Clear();
            TBL_ModuloDocumentos_Documento2.Clear();
            TBL_ModuloDocumentos_HistorialDocumento.Clear();
            TBL_ModuloDocumentos_HistorialDocumento1.Clear();
            TBL_ModuloDocumentos_HistorialDocumento2.Clear();
        }

        #endregion
        #region Association Fixup
    
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
                    item.TBL_ModuloDocumentos_Categorias = this;
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
                    if (ReferenceEquals(item.TBL_ModuloDocumentos_Categorias, this))
                    {
                        item.TBL_ModuloDocumentos_Categorias = null;
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
                    item.TBL_ModuloDocumentos_Categorias1 = this;
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
                    if (ReferenceEquals(item.TBL_ModuloDocumentos_Categorias1, this))
                    {
                        item.TBL_ModuloDocumentos_Categorias1 = null;
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
                    item.TBL_ModuloDocumentos_Categorias2 = this;
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
                    if (ReferenceEquals(item.TBL_ModuloDocumentos_Categorias2, this))
                    {
                        item.TBL_ModuloDocumentos_Categorias2 = null;
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
                    item.TBL_ModuloDocumentos_Categorias = this;
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
                    if (ReferenceEquals(item.TBL_ModuloDocumentos_Categorias, this))
                    {
                        item.TBL_ModuloDocumentos_Categorias = null;
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
                    item.TBL_ModuloDocumentos_Categorias1 = this;
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
                    if (ReferenceEquals(item.TBL_ModuloDocumentos_Categorias1, this))
                    {
                        item.TBL_ModuloDocumentos_Categorias1 = null;
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
                    item.TBL_ModuloDocumentos_Categorias2 = this;
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
                    if (ReferenceEquals(item.TBL_ModuloDocumentos_Categorias2, this))
                    {
                        item.TBL_ModuloDocumentos_Categorias2 = null;
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
