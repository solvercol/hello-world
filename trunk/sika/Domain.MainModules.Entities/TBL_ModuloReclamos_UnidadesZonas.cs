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
    [KnownType(typeof(TBL_ModuloReclamos_Unidad))]
    [KnownType(typeof(TBL_ModuloReclamos_Zona))]
    
    public partial class TBL_ModuloReclamos_UnidadesZonas: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int IdUnidad
        {
            get { return _idUnidad; }
            set
            {
                if (_idUnidad != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdUnidad' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    if (!IsDeserializing)
                    {
                        if (TBL_ModuloReclamos_Unidad != null && TBL_ModuloReclamos_Unidad.IdUnidad != value)
                        {
                            TBL_ModuloReclamos_Unidad = null;
                        }
                    }
                    _idUnidad = value;
                    OnPropertyChanged("IdUnidad");
                }
            }
        }
        private int _idUnidad;
    
        [DataMember]
        public int IdZona
        {
            get { return _idZona; }
            set
            {
                if (_idZona != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdZona' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    if (!IsDeserializing)
                    {
                        if (TBL_ModuloReclamos_Zona != null && TBL_ModuloReclamos_Zona.IdZona != value)
                        {
                            TBL_ModuloReclamos_Zona = null;
                        }
                    }
                    _idZona = value;
                    OnPropertyChanged("IdZona");
                }
            }
        }
        private int _idZona;
    
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
        public int IdGerente
        {
            get { return _idGerente; }
            set
            {
                if (_idGerente != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdGerente' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    if (!IsDeserializing)
                    {
                        if (TBL_Admin_Usuarios1 != null && TBL_Admin_Usuarios1.IdUser != value)
                        {
                            TBL_Admin_Usuarios1 = null;
                        }
                    }
                    _idGerente = value;
                    OnPropertyChanged("IdGerente");
                }
            }
        }
        private int _idGerente;
    
        [DataMember]
        public decimal TarifaFletes
        {
            get { return _tarifaFletes; }
            set
            {
                if (_tarifaFletes != value)
                {
                    _tarifaFletes = value;
                    OnPropertyChanged("TarifaFletes");
                }
            }
        }
        private decimal _tarifaFletes;
    
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
                        if (TBL_Admin_Usuarios2 != null && TBL_Admin_Usuarios2.IdUser != value)
                        {
                            TBL_Admin_Usuarios2 = null;
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
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added && value != null)
                    {
                        // This the dependent end of an identifying relationship, so the principal end cannot be changed if it is already set,
                        // otherwise it can only be set to an entity with a primary key that is the same value as the dependent's foreign key.
                        if (IdGerente != value.IdUser)
                        {
                            throw new InvalidOperationException("The principal end of an identifying relationship can only be changed when the dependent end is in the Added state.");
                        }
                    }
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
        public TBL_ModuloReclamos_Unidad TBL_ModuloReclamos_Unidad
        {
            get { return _tBL_ModuloReclamos_Unidad; }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloReclamos_Unidad, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added && value != null)
                    {
                        // This the dependent end of an identifying relationship, so the principal end cannot be changed if it is already set,
                        // otherwise it can only be set to an entity with a primary key that is the same value as the dependent's foreign key.
                        if (IdUnidad != value.IdUnidad)
                        {
                            throw new InvalidOperationException("The principal end of an identifying relationship can only be changed when the dependent end is in the Added state.");
                        }
                    }
                    var previousValue = _tBL_ModuloReclamos_Unidad;
                    _tBL_ModuloReclamos_Unidad = value;
                    FixupTBL_ModuloReclamos_Unidad(previousValue);
                    OnNavigationPropertyChanged("TBL_ModuloReclamos_Unidad");
                }
            }
        }
        private TBL_ModuloReclamos_Unidad _tBL_ModuloReclamos_Unidad;
    
        [DataMember]
        public TBL_ModuloReclamos_Zona TBL_ModuloReclamos_Zona
        {
            get { return _tBL_ModuloReclamos_Zona; }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloReclamos_Zona, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added && value != null)
                    {
                        // This the dependent end of an identifying relationship, so the principal end cannot be changed if it is already set,
                        // otherwise it can only be set to an entity with a primary key that is the same value as the dependent's foreign key.
                        if (IdZona != value.IdZona)
                        {
                            throw new InvalidOperationException("The principal end of an identifying relationship can only be changed when the dependent end is in the Added state.");
                        }
                    }
                    var previousValue = _tBL_ModuloReclamos_Zona;
                    _tBL_ModuloReclamos_Zona = value;
                    FixupTBL_ModuloReclamos_Zona(previousValue);
                    OnNavigationPropertyChanged("TBL_ModuloReclamos_Zona");
                }
            }
        }
        private TBL_ModuloReclamos_Zona _tBL_ModuloReclamos_Zona;

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
    
        // This entity type is the dependent end in at least one association that performs cascade deletes.
        // This event handler will process notifications that occur when the principal end is deleted.
        internal void HandleCascadeDelete(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                this.MarkAsDeleted();
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
            TBL_ModuloReclamos_Unidad = null;
            TBL_ModuloReclamos_Zona = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupTBL_Admin_Usuarios(TBL_Admin_Usuarios previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_UnidadesZonas.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_UnidadesZonas.Remove(this);
            }
    
            if (TBL_Admin_Usuarios != null)
            {
                if (!TBL_Admin_Usuarios.TBL_ModuloReclamos_UnidadesZonas.Contains(this))
                {
                    TBL_Admin_Usuarios.TBL_ModuloReclamos_UnidadesZonas.Add(this);
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
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_UnidadesZonas1.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_UnidadesZonas1.Remove(this);
            }
    
            if (TBL_Admin_Usuarios1 != null)
            {
                if (!TBL_Admin_Usuarios1.TBL_ModuloReclamos_UnidadesZonas1.Contains(this))
                {
                    TBL_Admin_Usuarios1.TBL_ModuloReclamos_UnidadesZonas1.Add(this);
                }
    
                IdGerente = TBL_Admin_Usuarios1.IdUser;
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
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_UnidadesZonas2.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_UnidadesZonas2.Remove(this);
            }
    
            if (TBL_Admin_Usuarios2 != null)
            {
                if (!TBL_Admin_Usuarios2.TBL_ModuloReclamos_UnidadesZonas2.Contains(this))
                {
                    TBL_Admin_Usuarios2.TBL_ModuloReclamos_UnidadesZonas2.Add(this);
                }
    
                ModifiedBy = TBL_Admin_Usuarios2.IdUser;
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
    
        private void FixupTBL_ModuloReclamos_Unidad(TBL_ModuloReclamos_Unidad previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_UnidadesZonas.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_UnidadesZonas.Remove(this);
            }
    
            if (TBL_ModuloReclamos_Unidad != null)
            {
                if (!TBL_ModuloReclamos_Unidad.TBL_ModuloReclamos_UnidadesZonas.Contains(this))
                {
                    TBL_ModuloReclamos_Unidad.TBL_ModuloReclamos_UnidadesZonas.Add(this);
                }
    
                IdUnidad = TBL_ModuloReclamos_Unidad.IdUnidad;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_ModuloReclamos_Unidad")
                    && (ChangeTracker.OriginalValues["TBL_ModuloReclamos_Unidad"] == TBL_ModuloReclamos_Unidad))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_ModuloReclamos_Unidad");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_ModuloReclamos_Unidad", previousValue);
                }
                if (TBL_ModuloReclamos_Unidad != null && !TBL_ModuloReclamos_Unidad.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_ModuloReclamos_Unidad.StartTracking();
                }
            }
        }
    
        private void FixupTBL_ModuloReclamos_Zona(TBL_ModuloReclamos_Zona previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_UnidadesZonas.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_UnidadesZonas.Remove(this);
            }
    
            if (TBL_ModuloReclamos_Zona != null)
            {
                if (!TBL_ModuloReclamos_Zona.TBL_ModuloReclamos_UnidadesZonas.Contains(this))
                {
                    TBL_ModuloReclamos_Zona.TBL_ModuloReclamos_UnidadesZonas.Add(this);
                }
    
                IdZona = TBL_ModuloReclamos_Zona.IdZona;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_ModuloReclamos_Zona")
                    && (ChangeTracker.OriginalValues["TBL_ModuloReclamos_Zona"] == TBL_ModuloReclamos_Zona))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_ModuloReclamos_Zona");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_ModuloReclamos_Zona", previousValue);
                }
                if (TBL_ModuloReclamos_Zona != null && !TBL_ModuloReclamos_Zona.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_ModuloReclamos_Zona.StartTracking();
                }
            }
        }

        #endregion
    }
}
