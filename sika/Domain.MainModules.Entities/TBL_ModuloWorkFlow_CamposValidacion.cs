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
    [KnownType(typeof(TBL_Admin_EstadosProceso))]
    
    public partial class TBL_ModuloWorkFlow_CamposValidacion: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int IdRequerido
        {
            get { return _idRequerido; }
            set
            {
                if (_idRequerido != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdRequerido' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idRequerido = value;
                    OnPropertyChanged("IdRequerido");
                }
            }
        }
        private int _idRequerido;
    
        [DataMember]
        public int IdEstado
        {
            get { return _idEstado; }
            set
            {
                if (_idEstado != value)
                {
                    ChangeTracker.RecordOriginalValue("IdEstado", _idEstado);
                    if (!IsDeserializing)
                    {
                        if (TBL_Admin_EstadosProceso != null && TBL_Admin_EstadosProceso.IdEstado != value)
                        {
                            TBL_Admin_EstadosProceso = null;
                        }
                    }
                    _idEstado = value;
                    OnPropertyChanged("IdEstado");
                }
            }
        }
        private int _idEstado;
    
        [DataMember]
        public string CampoValidar
        {
            get { return _campoValidar; }
            set
            {
                if (_campoValidar != value)
                {
                    _campoValidar = value;
                    OnPropertyChanged("CampoValidar");
                }
            }
        }
        private string _campoValidar;
    
        [DataMember]
        public string TipoValidacion
        {
            get { return _tipoValidacion; }
            set
            {
                if (_tipoValidacion != value)
                {
                    _tipoValidacion = value;
                    OnPropertyChanged("TipoValidacion");
                }
            }
        }
        private string _tipoValidacion;
    
        [DataMember]
        public string ReglaValidacion
        {
            get { return _reglaValidacion; }
            set
            {
                if (_reglaValidacion != value)
                {
                    _reglaValidacion = value;
                    OnPropertyChanged("ReglaValidacion");
                }
            }
        }
        private string _reglaValidacion;
    
        [DataMember]
        public string CampoDependencia
        {
            get { return _campoDependencia; }
            set
            {
                if (_campoDependencia != value)
                {
                    _campoDependencia = value;
                    OnPropertyChanged("CampoDependencia");
                }
            }
        }
        private string _campoDependencia;
    
        [DataMember]
        public string ReglaDependencia
        {
            get { return _reglaDependencia; }
            set
            {
                if (_reglaDependencia != value)
                {
                    _reglaDependencia = value;
                    OnPropertyChanged("ReglaDependencia");
                }
            }
        }
        private string _reglaDependencia;
    
        [DataMember]
        public string MensajeValidacion
        {
            get { return _mensajeValidacion; }
            set
            {
                if (_mensajeValidacion != value)
                {
                    _mensajeValidacion = value;
                    OnPropertyChanged("MensajeValidacion");
                }
            }
        }
        private string _mensajeValidacion;
    
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
        public TBL_Admin_EstadosProceso TBL_Admin_EstadosProceso
        {
            get { return _tBL_Admin_EstadosProceso; }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_EstadosProceso, value))
                {
                    var previousValue = _tBL_Admin_EstadosProceso;
                    _tBL_Admin_EstadosProceso = value;
                    FixupTBL_Admin_EstadosProceso(previousValue);
                    OnNavigationPropertyChanged("TBL_Admin_EstadosProceso");
                }
            }
        }
        private TBL_Admin_EstadosProceso _tBL_Admin_EstadosProceso;

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
            TBL_Admin_EstadosProceso = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupTBL_Admin_EstadosProceso(TBL_Admin_EstadosProceso previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloWorkFlow_CamposValidacion.Contains(this))
            {
                previousValue.TBL_ModuloWorkFlow_CamposValidacion.Remove(this);
            }
    
            if (TBL_Admin_EstadosProceso != null)
            {
                if (!TBL_Admin_EstadosProceso.TBL_ModuloWorkFlow_CamposValidacion.Contains(this))
                {
                    TBL_Admin_EstadosProceso.TBL_ModuloWorkFlow_CamposValidacion.Add(this);
                }
    
                IdEstado = TBL_Admin_EstadosProceso.IdEstado;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_Admin_EstadosProceso")
                    && (ChangeTracker.OriginalValues["TBL_Admin_EstadosProceso"] == TBL_Admin_EstadosProceso))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_Admin_EstadosProceso");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_Admin_EstadosProceso", previousValue);
                }
                if (TBL_Admin_EstadosProceso != null && !TBL_Admin_EstadosProceso.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_Admin_EstadosProceso.StartTracking();
                }
            }
        }

        #endregion
    }
}
