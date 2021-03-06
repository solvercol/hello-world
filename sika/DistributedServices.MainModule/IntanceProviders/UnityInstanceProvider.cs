﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.Web.Services.Description;
using Microsoft.Practices.Unity;

namespace DistributedServices.MainModule.IntanceProviders
{
    /// <summary>
    /// Crea instancias de los servicios wcf por medio de inyeccion de dependencias.
    /// </summary>
    public class UnityInstanceProvider : IInstanceProvider
    {
        #region Members

        Type _serviceType;
        IUnityContainer _container;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of unity instance provider
        /// </summary>
        /// <param name="serviceType">The service where we apply the instance provider</param>
        public UnityInstanceProvider(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            _serviceType = serviceType;
            _container = Container.Current;
        }

        #endregion

        #region IInstance Provider Members

        /// <summary>
        /// <see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/>
        /// </summary>
        /// <param name="instanceContext"><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></param>
        /// <param name="message"><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></param>
        /// <returns><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></returns>
        public object GetInstance(InstanceContext instanceContext, System.ServiceModel.Channels.Message message)
        {
            //This is the only call to UNITY container in the whole solution
            return _container.Resolve(_serviceType);
        }
        /// <summary>
        /// <see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/>
        /// </summary>
        /// <param name="instanceContext"><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></param>
        /// <returns><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></returns>
        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        /// <summary>
        /// <see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/>
        /// </summary>
        /// <param name="instanceContext"><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></param>
        /// <param name="instance"><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></param>
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            if (instance is IDisposable)
                ((IDisposable)instance).Dispose();
        }

        #endregion

    }
}