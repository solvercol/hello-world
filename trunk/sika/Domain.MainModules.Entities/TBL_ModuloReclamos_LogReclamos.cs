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
    [KnownType(typeof(TBL_ModuloReclamos_Reclamo))]
    
    public partial class TBL_ModuloReclamos_LogReclamos: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int IdLog
        {
            get { return _idLog; }
            set
            {
                if (_idLog != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdLog' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idLog = value;
                    OnPropertyChanged("IdLog");
                }
            }
        }
        private int _idLog;
    
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
        public Nullable<int> CreateBy
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
        private Nullable<int> _createBy;
    
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

        #endregion
        #region Navigation Properties
    
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
            TBL_ModuloReclamos_Reclamo = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupTBL_ModuloReclamos_Reclamo(TBL_ModuloReclamos_Reclamo previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_LogReclamos.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_LogReclamos.Remove(this);
            }
    
            if (TBL_ModuloReclamos_Reclamo != null)
            {
                if (!TBL_ModuloReclamos_Reclamo.TBL_ModuloReclamos_LogReclamos.Contains(this))
                {
                    TBL_ModuloReclamos_Reclamo.TBL_ModuloReclamos_LogReclamos.Add(this);
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

        #endregion
    }
}
