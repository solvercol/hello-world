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
    [KnownType(typeof(TBL_ModuloReclamos_AnexosComentarioRespuesta))]
    [KnownType(typeof(TBL_ModuloReclamos_ComentariosRespuesta))]
    [KnownType(typeof(TBL_ModuloReclamos_Reclamo))]
    
    public partial class TBL_ModuloReclamos_ComentariosRespuesta: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public decimal IdComentario
        {
            get { return _idComentario; }
            set
            {
                if (_idComentario != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdComentario' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idComentario = value;
                    OnPropertyChanged("IdComentario");
                }
            }
        }
        private decimal _idComentario;
    
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
        public string Asunto
        {
            get { return _asunto; }
            set
            {
                if (_asunto != value)
                {
                    _asunto = value;
                    OnPropertyChanged("Asunto");
                }
            }
        }
        private string _asunto;
    
        [DataMember]
        public string Comentario
        {
            get { return _comentario; }
            set
            {
                if (_comentario != value)
                {
                    _comentario = value;
                    OnPropertyChanged("Comentario");
                }
            }
        }
        private string _comentario;
    
        [DataMember]
        public int IdUsuarioDestino
        {
            get { return _idUsuarioDestino; }
            set
            {
                if (_idUsuarioDestino != value)
                {
                    ChangeTracker.RecordOriginalValue("IdUsuarioDestino", _idUsuarioDestino);
                    if (!IsDeserializing)
                    {
                        if (TBL_Admin_Usuarios2 != null && TBL_Admin_Usuarios2.IdUser != value)
                        {
                            TBL_Admin_Usuarios2 = null;
                        }
                    }
                    _idUsuarioDestino = value;
                    OnPropertyChanged("IdUsuarioDestino");
                }
            }
        }
        private int _idUsuarioDestino;
    
        [DataMember]
        public Nullable<decimal> IdComentarioRelacionado
        {
            get { return _idComentarioRelacionado; }
            set
            {
                if (_idComentarioRelacionado != value)
                {
                    ChangeTracker.RecordOriginalValue("IdComentarioRelacionado", _idComentarioRelacionado);
                    if (!IsDeserializing)
                    {
                        if (TBL_ModuloReclamos_ComentariosRespuesta2 != null && TBL_ModuloReclamos_ComentariosRespuesta2.IdComentario != value)
                        {
                            TBL_ModuloReclamos_ComentariosRespuesta2 = null;
                        }
                    }
                    _idComentarioRelacionado = value;
                    OnPropertyChanged("IdComentarioRelacionado");
                }
            }
        }
        private Nullable<decimal> _idComentarioRelacionado;
    
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
        public TrackableCollection<TBL_ModuloReclamos_AnexosComentarioRespuesta> TBL_ModuloReclamos_AnexosComentarioRespuesta
        {
            get
            {
                if (_tBL_ModuloReclamos_AnexosComentarioRespuesta == null)
                {
                    _tBL_ModuloReclamos_AnexosComentarioRespuesta = new TrackableCollection<TBL_ModuloReclamos_AnexosComentarioRespuesta>();
                    _tBL_ModuloReclamos_AnexosComentarioRespuesta.CollectionChanged += FixupTBL_ModuloReclamos_AnexosComentarioRespuesta;
                }
                return _tBL_ModuloReclamos_AnexosComentarioRespuesta;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloReclamos_AnexosComentarioRespuesta, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloReclamos_AnexosComentarioRespuesta != null)
                    {
                        _tBL_ModuloReclamos_AnexosComentarioRespuesta.CollectionChanged -= FixupTBL_ModuloReclamos_AnexosComentarioRespuesta;
                    }
                    _tBL_ModuloReclamos_AnexosComentarioRespuesta = value;
                    if (_tBL_ModuloReclamos_AnexosComentarioRespuesta != null)
                    {
                        _tBL_ModuloReclamos_AnexosComentarioRespuesta.CollectionChanged += FixupTBL_ModuloReclamos_AnexosComentarioRespuesta;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloReclamos_AnexosComentarioRespuesta");
                }
            }
        }
        private TrackableCollection<TBL_ModuloReclamos_AnexosComentarioRespuesta> _tBL_ModuloReclamos_AnexosComentarioRespuesta;
    
        [DataMember]
        public TrackableCollection<TBL_ModuloReclamos_ComentariosRespuesta> TBL_ModuloReclamos_ComentariosRespuesta1
        {
            get
            {
                if (_tBL_ModuloReclamos_ComentariosRespuesta1 == null)
                {
                    _tBL_ModuloReclamos_ComentariosRespuesta1 = new TrackableCollection<TBL_ModuloReclamos_ComentariosRespuesta>();
                    _tBL_ModuloReclamos_ComentariosRespuesta1.CollectionChanged += FixupTBL_ModuloReclamos_ComentariosRespuesta1;
                }
                return _tBL_ModuloReclamos_ComentariosRespuesta1;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloReclamos_ComentariosRespuesta1, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloReclamos_ComentariosRespuesta1 != null)
                    {
                        _tBL_ModuloReclamos_ComentariosRespuesta1.CollectionChanged -= FixupTBL_ModuloReclamos_ComentariosRespuesta1;
                    }
                    _tBL_ModuloReclamos_ComentariosRespuesta1 = value;
                    if (_tBL_ModuloReclamos_ComentariosRespuesta1 != null)
                    {
                        _tBL_ModuloReclamos_ComentariosRespuesta1.CollectionChanged += FixupTBL_ModuloReclamos_ComentariosRespuesta1;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloReclamos_ComentariosRespuesta1");
                }
            }
        }
        private TrackableCollection<TBL_ModuloReclamos_ComentariosRespuesta> _tBL_ModuloReclamos_ComentariosRespuesta1;
    
        [DataMember]
        public TBL_ModuloReclamos_ComentariosRespuesta TBL_ModuloReclamos_ComentariosRespuesta2
        {
            get { return _tBL_ModuloReclamos_ComentariosRespuesta2; }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloReclamos_ComentariosRespuesta2, value))
                {
                    var previousValue = _tBL_ModuloReclamos_ComentariosRespuesta2;
                    _tBL_ModuloReclamos_ComentariosRespuesta2 = value;
                    FixupTBL_ModuloReclamos_ComentariosRespuesta2(previousValue);
                    OnNavigationPropertyChanged("TBL_ModuloReclamos_ComentariosRespuesta2");
                }
            }
        }
        private TBL_ModuloReclamos_ComentariosRespuesta _tBL_ModuloReclamos_ComentariosRespuesta2;
    
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
        public TrackableCollection<TBL_Admin_Usuarios> TBL_Admin_Usuarios3
        {
            get
            {
                if (_tBL_Admin_Usuarios3 == null)
                {
                    _tBL_Admin_Usuarios3 = new TrackableCollection<TBL_Admin_Usuarios>();
                    _tBL_Admin_Usuarios3.CollectionChanged += FixupTBL_Admin_Usuarios3;
                }
                return _tBL_Admin_Usuarios3;
            }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_Usuarios3, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_Admin_Usuarios3 != null)
                    {
                        _tBL_Admin_Usuarios3.CollectionChanged -= FixupTBL_Admin_Usuarios3;
                    }
                    _tBL_Admin_Usuarios3 = value;
                    if (_tBL_Admin_Usuarios3 != null)
                    {
                        _tBL_Admin_Usuarios3.CollectionChanged += FixupTBL_Admin_Usuarios3;
                    }
                    OnNavigationPropertyChanged("TBL_Admin_Usuarios3");
                }
            }
        }
        private TrackableCollection<TBL_Admin_Usuarios> _tBL_Admin_Usuarios3;

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
            TBL_ModuloReclamos_AnexosComentarioRespuesta.Clear();
            TBL_ModuloReclamos_ComentariosRespuesta1.Clear();
            TBL_ModuloReclamos_ComentariosRespuesta2 = null;
            TBL_ModuloReclamos_Reclamo = null;
            TBL_Admin_Usuarios3.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupTBL_Admin_Usuarios(TBL_Admin_Usuarios previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_ComentariosRespuesta.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_ComentariosRespuesta.Remove(this);
            }
    
            if (TBL_Admin_Usuarios != null)
            {
                if (!TBL_Admin_Usuarios.TBL_ModuloReclamos_ComentariosRespuesta.Contains(this))
                {
                    TBL_Admin_Usuarios.TBL_ModuloReclamos_ComentariosRespuesta.Add(this);
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
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_ComentariosRespuesta1.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_ComentariosRespuesta1.Remove(this);
            }
    
            if (TBL_Admin_Usuarios1 != null)
            {
                if (!TBL_Admin_Usuarios1.TBL_ModuloReclamos_ComentariosRespuesta1.Contains(this))
                {
                    TBL_Admin_Usuarios1.TBL_ModuloReclamos_ComentariosRespuesta1.Add(this);
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
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_ComentariosRespuesta2.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_ComentariosRespuesta2.Remove(this);
            }
    
            if (TBL_Admin_Usuarios2 != null)
            {
                if (!TBL_Admin_Usuarios2.TBL_ModuloReclamos_ComentariosRespuesta2.Contains(this))
                {
                    TBL_Admin_Usuarios2.TBL_ModuloReclamos_ComentariosRespuesta2.Add(this);
                }
    
                IdUsuarioDestino = TBL_Admin_Usuarios2.IdUser;
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
    
        private void FixupTBL_ModuloReclamos_ComentariosRespuesta2(TBL_ModuloReclamos_ComentariosRespuesta previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_ComentariosRespuesta1.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_ComentariosRespuesta1.Remove(this);
            }
    
            if (TBL_ModuloReclamos_ComentariosRespuesta2 != null)
            {
                if (!TBL_ModuloReclamos_ComentariosRespuesta2.TBL_ModuloReclamos_ComentariosRespuesta1.Contains(this))
                {
                    TBL_ModuloReclamos_ComentariosRespuesta2.TBL_ModuloReclamos_ComentariosRespuesta1.Add(this);
                }
    
                IdComentarioRelacionado = TBL_ModuloReclamos_ComentariosRespuesta2.IdComentario;
            }
            else if (!skipKeys)
            {
                IdComentarioRelacionado = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_ModuloReclamos_ComentariosRespuesta2")
                    && (ChangeTracker.OriginalValues["TBL_ModuloReclamos_ComentariosRespuesta2"] == TBL_ModuloReclamos_ComentariosRespuesta2))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_ModuloReclamos_ComentariosRespuesta2");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_ModuloReclamos_ComentariosRespuesta2", previousValue);
                }
                if (TBL_ModuloReclamos_ComentariosRespuesta2 != null && !TBL_ModuloReclamos_ComentariosRespuesta2.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_ModuloReclamos_ComentariosRespuesta2.StartTracking();
                }
            }
        }
    
        private void FixupTBL_ModuloReclamos_Reclamo(TBL_ModuloReclamos_Reclamo previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_ModuloReclamos_ComentariosRespuesta.Contains(this))
            {
                previousValue.TBL_ModuloReclamos_ComentariosRespuesta.Remove(this);
            }
    
            if (TBL_ModuloReclamos_Reclamo != null)
            {
                if (!TBL_ModuloReclamos_Reclamo.TBL_ModuloReclamos_ComentariosRespuesta.Contains(this))
                {
                    TBL_ModuloReclamos_Reclamo.TBL_ModuloReclamos_ComentariosRespuesta.Add(this);
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
    
        private void FixupTBL_ModuloReclamos_AnexosComentarioRespuesta(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloReclamos_AnexosComentarioRespuesta item in e.NewItems)
                {
                    item.TBL_ModuloReclamos_ComentariosRespuesta = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloReclamos_AnexosComentarioRespuesta", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloReclamos_AnexosComentarioRespuesta item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_ModuloReclamos_ComentariosRespuesta, this))
                    {
                        item.TBL_ModuloReclamos_ComentariosRespuesta = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloReclamos_AnexosComentarioRespuesta", item);
                    }
                }
            }
        }
    
        private void FixupTBL_ModuloReclamos_ComentariosRespuesta1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloReclamos_ComentariosRespuesta item in e.NewItems)
                {
                    item.TBL_ModuloReclamos_ComentariosRespuesta2 = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloReclamos_ComentariosRespuesta1", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloReclamos_ComentariosRespuesta item in e.OldItems)
                {
                    if (ReferenceEquals(item.TBL_ModuloReclamos_ComentariosRespuesta2, this))
                    {
                        item.TBL_ModuloReclamos_ComentariosRespuesta2 = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloReclamos_ComentariosRespuesta1", item);
                    }
                }
            }
        }
    
        private void FixupTBL_Admin_Usuarios3(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_Admin_Usuarios item in e.NewItems)
                {
                    if (!item.TBL_ModuloReclamos_ComentariosRespuesta3.Contains(this))
                    {
                        item.TBL_ModuloReclamos_ComentariosRespuesta3.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_Admin_Usuarios3", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_Admin_Usuarios item in e.OldItems)
                {
                    if (item.TBL_ModuloReclamos_ComentariosRespuesta3.Contains(this))
                    {
                        item.TBL_ModuloReclamos_ComentariosRespuesta3.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_Admin_Usuarios3", item);
                    }
                }
            }
        }

        #endregion
    }
}
