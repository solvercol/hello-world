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
    [KnownType(typeof(TBL_ModuloPlanAccion_BancoActividades))]
    [KnownType(typeof(TBL_ModuloPlanAccion_Categorias))]
    
    public partial class TBL_ModuloPlanAccion_ConfiguracionActividades: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int IdConfiguracion
        {
            get { return _idConfiguracion; }
            set
            {
                if (_idConfiguracion != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdConfiguracion' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idConfiguracion = value;
                    OnPropertyChanged("IdConfiguracion");
                }
            }
        }
        private int _idConfiguracion;
    
        [DataMember]
        public int IdCategoria
        {
            get { return _idCategoria; }
            set
            {
                if (_idCategoria != value)
                {
                    ChangeTracker.RecordOriginalValue("IdCategoria", _idCategoria);
                    if (!IsDeserializing)
                    {
                        if (TBL_ModuloPlanAccion_Categorias != null && TBL_ModuloPlanAccion_Categorias.IdCategoria != value)
                        {
                            TBL_ModuloPlanAccion_Categorias = null;
                        }
                    }
                    _idCategoria = value;
                    OnPropertyChanged("IdCategoria");
                }
            }
        }
        private int _idCategoria;
    
        [DataMember]
        public int IdActividad
        {
            get { return _idActividad; }
            set
            {
                if (_idActividad != value)
                {
                    ChangeTracker.RecordOriginalValue("IdActividad", _idActividad);
                    if (!IsDeserializing)
                    {
                        if (TBL_ModuloPlanAccion_BancoActividades != null && TBL_ModuloPlanAccion_BancoActividades.IdActividad != value)
                        {
                            TBL_ModuloPlanAccion_BancoActividades = null;
                        }
                    }
                    _idActividad = value;
                    OnPropertyChanged("IdActividad");
                }
            }
        }
        private int _idActividad;
    
        [DataMember]
        public bool Oblogatorio
        {
            get { return _oblogatorio; }
            set
            {
                if (_oblogatorio != value)
                {
                    _oblogatorio = value;
                    OnPropertyChanged("Oblogatorio");
                }
            }
        }
        private bool _oblogatorio;
    
        [DataMember]
        public int Secuencia
        {
            get { return _secuencia; }
            set
            {
                if (_secuencia != value)
                {
                    _secuencia = value;
                    OnPropertyChanged("Secuencia");
                }
            }
        }
        private int _secuencia;
    
        [DataMember]
        public bool EsFinal
        {
            get { return _esFinal; }
            set
            {
                if (_esFinal != value)
                {
                    _esFinal = value;
                    OnPropertyChanged("EsFinal");
                }
            }
        }
        private bool _esFinal;
    
        [DataMember]
        public Nullable<bool> Exclusivo
        {
            get { return _exclusivo; }
            set
            {
                if (_exclusivo != value)
                {
                    _exclusivo = value;
                    OnPropertyChanged("Exclusivo");
                }
            }
        }
        private Nullable<bool> _exclusivo;
    
        [DataMember]
        public string RolExclusivo
        {
            get { return _rolExclusivo; }
            set
            {
                if (_rolExclusivo != value)
                {
                    _rolExclusivo = value;
                    OnPropertyChanged("RolExclusivo");
                }
            }
        }
        private string _rolExclusivo;
    
        [DataMember]
        public Nullable<int> NumeroDiasHabiles
        {
            get { return _numeroDiasHabiles; }
            set
            {
                if (_numeroDiasHabiles != value)
                {
                    _numeroDiasHabiles = value;
                    OnPropertyChanged("NumeroDiasHabiles");
                }
            }
        }
        private Nullable<int> _numeroDiasHabiles;
    
        [DataMember]
        public bool PreProgramar
        {
            get { return _preProgramar; }
            set
            {
                if (_preProgramar != value)
                {
                    _preProgramar = value;
                    OnPropertyChanged("PreProgramar");
                }
            }
        }
        private bool _preProgramar;
    
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
        public TBL_ModuloPlanAccion_BancoActividades TBL_ModuloPlanAccion_BancoActividades
        {
            get { return _tBL_ModuloPlanAccion_BancoActividades; }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloPlanAccion_BancoActividades, value))
                {
                    var previousValue = _tBL_ModuloPlanAccion_BancoActividades;
                    _tBL_ModuloPlanAccion_BancoActividades = value;
                    FixupTBL_ModuloPlanAccion_BancoActividades(previousValue);
                    OnNavigationPropertyChanged("TBL_ModuloPlanAccion_BancoActividades");
                }
            }
        }
        private TBL_ModuloPlanAccion_BancoActividades _tBL_ModuloPlanAccion_BancoActividades;
    
        [DataMember]
        public TBL_ModuloPlanAccion_Categorias TBL_ModuloPlanAccion_Categorias
        {
            get { return _tBL_ModuloPlanAccion_Categorias; }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloPlanAccion_Categorias, value))
                {
                    var previousValue = _tBL_ModuloPlanAccion_Categorias;
                    _tBL_ModuloPlanAccion_Categorias = value;
                    FixupTBL_ModuloPlanAccion_Categorias(previousValue);
                    OnNavigationPropertyChanged("TBL_ModuloPlanAccion_Categorias");
                }
            }
        }
        private TBL_ModuloPlanAccion_Categorias _tBL_ModuloPlanAccion_Categorias;

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
            TBL_ModuloPlanAccion_BancoActividades = null;
            TBL_ModuloPlanAccion_Categorias = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupTBL_ModuloPlanAccion_BancoActividades(TBL_ModuloPlanAccion_BancoActividades previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloPlanAccion_ConfiguracionActividades.Contains(this))
            {
                previousValue.TBL_ModuloPlanAccion_ConfiguracionActividades.Remove(this);
            }
    
            if (TBL_ModuloPlanAccion_BancoActividades != null)
            {
                if (!TBL_ModuloPlanAccion_BancoActividades.TBL_ModuloPlanAccion_ConfiguracionActividades.Contains(this))
                {
                    TBL_ModuloPlanAccion_BancoActividades.TBL_ModuloPlanAccion_ConfiguracionActividades.Add(this);
                }
    
                IdActividad = TBL_ModuloPlanAccion_BancoActividades.IdActividad;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_ModuloPlanAccion_BancoActividades")
                    && (ChangeTracker.OriginalValues["TBL_ModuloPlanAccion_BancoActividades"] == TBL_ModuloPlanAccion_BancoActividades))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_ModuloPlanAccion_BancoActividades");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_ModuloPlanAccion_BancoActividades", previousValue);
                }
                if (TBL_ModuloPlanAccion_BancoActividades != null && !TBL_ModuloPlanAccion_BancoActividades.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_ModuloPlanAccion_BancoActividades.StartTracking();
                }
            }
        }
    
        private void FixupTBL_ModuloPlanAccion_Categorias(TBL_ModuloPlanAccion_Categorias previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloPlanAccion_ConfiguracionActividades.Contains(this))
            {
                previousValue.TBL_ModuloPlanAccion_ConfiguracionActividades.Remove(this);
            }
    
            if (TBL_ModuloPlanAccion_Categorias != null)
            {
                if (!TBL_ModuloPlanAccion_Categorias.TBL_ModuloPlanAccion_ConfiguracionActividades.Contains(this))
                {
                    TBL_ModuloPlanAccion_Categorias.TBL_ModuloPlanAccion_ConfiguracionActividades.Add(this);
                }
    
                IdCategoria = TBL_ModuloPlanAccion_Categorias.IdCategoria;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_ModuloPlanAccion_Categorias")
                    && (ChangeTracker.OriginalValues["TBL_ModuloPlanAccion_Categorias"] == TBL_ModuloPlanAccion_Categorias))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_ModuloPlanAccion_Categorias");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_ModuloPlanAccion_Categorias", previousValue);
                }
                if (TBL_ModuloPlanAccion_Categorias != null && !TBL_ModuloPlanAccion_Categorias.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_ModuloPlanAccion_Categorias.StartTracking();
                }
            }
        }

        #endregion
    }
}