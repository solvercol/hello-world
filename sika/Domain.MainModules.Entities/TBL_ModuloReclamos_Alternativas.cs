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
    [KnownType(typeof(TBL_ModuloReclamos_Reclamo))]
    [KnownType(typeof(TBL_ModuloReclamos_AnexosAlternativa))]
    
    public partial class TBL_ModuloReclamos_Alternativas: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public decimal IdAlternativa
        {
            get { return _idAlternativa; }
            set
            {
                if (_idAlternativa != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdAlternativa' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idAlternativa = value;
                    OnPropertyChanged("IdAlternativa");
                }
            }
        }
        private decimal _idAlternativa;
    
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
        public string Causas
        {
            get { return _causas; }
            set
            {
                if (_causas != value)
                {
                    _causas = value;
                    OnPropertyChanged("Causas");
                }
            }
        }
        private string _causas;
    
        [DataMember]
        public string Factores
        {
            get { return _factores; }
            set
            {
                if (_factores != value)
                {
                    _factores = value;
                    OnPropertyChanged("Factores");
                }
            }
        }
        private string _factores;
    
        [DataMember]
        public string Alternativa
        {
            get { return _alternativa; }
            set
            {
                if (_alternativa != value)
                {
                    _alternativa = value;
                    OnPropertyChanged("Alternativa");
                }
            }
        }
        private string _alternativa;
    
        [DataMember]
        public int IdResponsable
        {
            get { return _idResponsable; }
            set
            {
                if (_idResponsable != value)
                {
                    ChangeTracker.RecordOriginalValue("IdResponsable", _idResponsable);
                    if (!IsDeserializing)
                    {
                        if (TBL_Admin_Usuarios2 != null && TBL_Admin_Usuarios2.IdUser != value)
                        {
                            TBL_Admin_Usuarios2 = null;
                        }
                    }
                    _idResponsable = value;
                    OnPropertyChanged("IdResponsable");
                }
            }
        }
        private int _idResponsable;
    
        [DataMember]
        public System.DateTime FechaAlternativa
        {
            get { return _fechaAlternativa; }
            set
            {
                if (_fechaAlternativa != value)
                {
                    _fechaAlternativa = value;
                    OnPropertyChanged("FechaAlternativa");
                }
            }
        }
        private System.DateTime _fechaAlternativa;
    
        [DataMember]
        public string Seguimiento
        {
            get { return _seguimiento; }
            set
            {
                if (_seguimiento != value)
                {
                    _seguimiento = value;
                    OnPropertyChanged("Seguimiento");
                }
            }
        }
        private string _seguimiento;
    
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
        public Nullable<System.DateTime> FechaCierre
        {
            get { return _fechaCierre; }
            set
            {
                if (_fechaCierre != value)
                {
                    _fechaCierre = value;
                    OnPropertyChanged("FechaCierre");
                }
            }
        }
        private Nullable<System.DateTime> _fechaCierre;
    
        [DataMember]
        public string LogCierre
        {
            get { return _logCierre; }
            set
            {
                if (_logCierre != value)
                {
                    _logCierre = value;
                    OnPropertyChanged("LogCierre");
                }
            }
        }
        private string _logCierre;
    
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
        public TrackableCollection<TBL_ModuloReclamos_AnexosAlternativa> TBL_ModuloReclamos_AnexosAlternativa
        {
            get
            {
                if (_tBL_ModuloReclamos_AnexosAlternativa == null)
                {
                    _tBL_ModuloReclamos_AnexosAlternativa = new TrackableCollection<TBL_ModuloReclamos_AnexosAlternativa>();
                    _tBL_ModuloReclamos_AnexosAlternativa.CollectionChanged += FixupTBL_ModuloReclamos_AnexosAlternativa;
                }
                return _tBL_ModuloReclamos_AnexosAlternativa;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloReclamos_AnexosAlternativa, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloReclamos_AnexosAlternativa != null)
                    {
                        _tBL_ModuloReclamos_AnexosAlternativa.CollectionChanged -= FixupTBL_ModuloReclamos_AnexosAlternativa;
                    }
                    _tBL_ModuloReclamos_AnexosAlternativa = value;
                    if (_tBL_ModuloReclamos_AnexosAlternativa != null)
                    {
                        _tBL_ModuloReclamos_AnexosAlternativa.CollectionChanged += FixupTBL_ModuloReclamos_AnexosAlternativa;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloReclamos_AnexosAlternativa");
                }
            }
        }
        private TrackableCollection<TBL_ModuloReclamos_AnexosAlternativa> _tBL_ModuloReclamos_AnexosAlternativa;

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
            TBL_ModuloReclamos_Reclamo = null;
            TBL_ModuloReclamos_AnexosAlternativa.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupTBL_Admin_Usuarios(TBL_Admin_Usuarios previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_Alternativas.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_Alternativas.Remove(this);
            }
    
            if (TBL_Admin_Usuarios != null)
            {
                if (!TBL_Admin_Usuarios.TBL_ModuloReclamos_Alternativas.Contains(this))
                {
                    TBL_Admin_Usuarios.TBL_ModuloReclamos_Alternativas.Add(this);
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
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_Alternativas1.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_Alternativas1.Remove(this);
            }
    
            if (TBL_Admin_Usuarios1 != null)
            {
                if (!TBL_Admin_Usuarios1.TBL_ModuloReclamos_Alternativas1.Contains(this))
                {
                    TBL_Admin_Usuarios1.TBL_ModuloReclamos_Alternativas1.Add(this);
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
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_Alternativas2.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_Alternativas2.Remove(this);
            }
    
            if (TBL_Admin_Usuarios2 != null)
            {
                if (!TBL_Admin_Usuarios2.TBL_ModuloReclamos_Alternativas2.Contains(this))
                {
                    TBL_Admin_Usuarios2.TBL_ModuloReclamos_Alternativas2.Add(this);
                }
    
                IdResponsable = TBL_Admin_Usuarios2.IdUser;
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
    
        private void FixupTBL_ModuloReclamos_Reclamo(TBL_ModuloReclamos_Reclamo previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_Alternativas.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_Alternativas.Remove(this);
            }
    
            if (TBL_ModuloReclamos_Reclamo != null)
            {
                if (!TBL_ModuloReclamos_Reclamo.TBL_ModuloReclamos_Alternativas.Contains(this))
                {
                    TBL_ModuloReclamos_Reclamo.TBL_ModuloReclamos_Alternativas.Add(this);
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
    
        private void FixupTBL_ModuloReclamos_AnexosAlternativa(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloReclamos_AnexosAlternativa item in e.NewItems)
                {
                    item.TBL_ModuloReclamos_Alternativas = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloReclamos_AnexosAlternativa", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloReclamos_AnexosAlternativa item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_ModuloReclamos_Alternativas, this))
                    {
                        item.TBL_ModuloReclamos_Alternativas = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloReclamos_AnexosAlternativa", item);
                    }
                }
            }
        }

        #endregion
    }
}
