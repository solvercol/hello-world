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
    [KnownType(typeof(TBL_ModuloReclamos_Soluciones))]
    
    public partial class TBL_ModuloReclamos_AnexosSolucion: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public decimal IdAnexoSolucion
        {
            get { return _idAnexoSolucion; }
            set
            {
                if (_idAnexoSolucion != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdAnexoSolucion' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idAnexoSolucion = value;
                    OnPropertyChanged("IdAnexoSolucion");
                }
            }
        }
        private decimal _idAnexoSolucion;
    
        [DataMember]
        public string NombreArchivo
        {
            get { return _nombreArchivo; }
            set
            {
                if (_nombreArchivo != value)
                {
                    _nombreArchivo = value;
                    OnPropertyChanged("NombreArchivo");
                }
            }
        }
        private string _nombreArchivo;
    
        [DataMember]
        public byte[] Archivo
        {
            get { return _archivo; }
            set
            {
                if (_archivo != value)
                {
                    _archivo = value;
                    OnPropertyChanged("Archivo");
                }
            }
        }
        private byte[] _archivo;
    
        [DataMember]
        public decimal IdSolucion
        {
            get { return _idSolucion; }
            set
            {
                if (_idSolucion != value)
                {
                    ChangeTracker.RecordOriginalValue("IdSolucion", _idSolucion);
                    if (!IsDeserializing)
                    {
                        if (TBL_ModuloReclamos_Soluciones != null && TBL_ModuloReclamos_Soluciones.IdSolucion != value)
                        {
                            TBL_ModuloReclamos_Soluciones = null;
                        }
                    }
                    _idSolucion = value;
                    OnPropertyChanged("IdSolucion");
                }
            }
        }
        private decimal _idSolucion;
    
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
        public TBL_ModuloReclamos_Soluciones TBL_ModuloReclamos_Soluciones
        {
            get { return _tBL_ModuloReclamos_Soluciones; }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloReclamos_Soluciones, value))
                {
                    var previousValue = _tBL_ModuloReclamos_Soluciones;
                    _tBL_ModuloReclamos_Soluciones = value;
                    FixupTBL_ModuloReclamos_Soluciones(previousValue);
                    OnNavigationPropertyChanged("TBL_ModuloReclamos_Soluciones");
                }
            }
        }
        private TBL_ModuloReclamos_Soluciones _tBL_ModuloReclamos_Soluciones;

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
            TBL_ModuloReclamos_Soluciones = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupTBL_Admin_Usuarios(TBL_Admin_Usuarios previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_AnexosSolucion.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_AnexosSolucion.Remove(this);
            }
    
            if (TBL_Admin_Usuarios != null)
            {
                if (!TBL_Admin_Usuarios.TBL_ModuloReclamos_AnexosSolucion.Contains(this))
                {
                    TBL_Admin_Usuarios.TBL_ModuloReclamos_AnexosSolucion.Add(this);
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
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_AnexosSolucion1.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_AnexosSolucion1.Remove(this);
            }
    
            if (TBL_Admin_Usuarios1 != null)
            {
                if (!TBL_Admin_Usuarios1.TBL_ModuloReclamos_AnexosSolucion1.Contains(this))
                {
                    TBL_Admin_Usuarios1.TBL_ModuloReclamos_AnexosSolucion1.Add(this);
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
    
        private void FixupTBL_ModuloReclamos_Soluciones(TBL_ModuloReclamos_Soluciones previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_AnexosSolucion.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_AnexosSolucion.Remove(this);
            }
    
            if (TBL_ModuloReclamos_Soluciones != null)
            {
                if (!TBL_ModuloReclamos_Soluciones.TBL_ModuloReclamos_AnexosSolucion.Contains(this))
                {
                    TBL_ModuloReclamos_Soluciones.TBL_ModuloReclamos_AnexosSolucion.Add(this);
                }
    
                IdSolucion = TBL_ModuloReclamos_Soluciones.IdSolucion;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_ModuloReclamos_Soluciones")
                    && (ChangeTracker.OriginalValues["TBL_ModuloReclamos_Soluciones"] == TBL_ModuloReclamos_Soluciones))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_ModuloReclamos_Soluciones");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_ModuloReclamos_Soluciones", previousValue);
                }
                if (TBL_ModuloReclamos_Soluciones != null && !TBL_ModuloReclamos_Soluciones.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_ModuloReclamos_Soluciones.StartTracking();
                }
            }
        }

        #endregion
    }
}
