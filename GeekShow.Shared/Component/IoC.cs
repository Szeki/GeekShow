using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace GeekShow.Shared.Component
{
    public class IoC
    {
        #region Members

        Dictionary<Type, object> _instanceRegistrations;
        Dictionary<Type, Type> _typeRegistrations;

        #endregion

        #region Constructor

        static IoC()
        {
            Container = new IoC();
        }

        private IoC()
        {
            _instanceRegistrations = new Dictionary<Type, object>();
            _typeRegistrations = new Dictionary<Type, Type>();
        }

        #endregion

        #region Properties

        public static IoC Container
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        public void RegisterType(Type typeToResolve, Type resolveType)
        {
            if (!typeToResolve.GetTypeInfo().IsAssignableFrom(resolveType.GetTypeInfo()))
            {
                throw new ArgumentException(string.Format("{0} type is not assignable to {1} type", resolveType.Name, typeToResolve.Name));
            }

            if(_typeRegistrations.ContainsKey(typeToResolve))
            {
                _typeRegistrations[typeToResolve] = resolveType;
            }
            else
            {
                _typeRegistrations.Add(typeToResolve, resolveType);
            }
        }

        public void RegisterType(Type typeToRegister)
        {
            RegisterType(typeToRegister, typeToRegister);
        }

        public void RegisterInstance(Type typeToResolve, object instance)
        {
            if(instance == null)
            {
                throw new ArgumentNullException("Instance cannot be null");
            }

            if(!(typeToResolve.GetTypeInfo().IsAssignableFrom(instance.GetType().GetTypeInfo())))
            {
                throw new ArgumentException(string.Format("{0} type is not assignable to {1} type", instance.GetType().Name, typeToResolve.Name));
            }

            if(_instanceRegistrations.ContainsKey(typeToResolve))
            {
                _instanceRegistrations[typeToResolve] = instance;
            }
            else
            {
                _instanceRegistrations.Add(typeToResolve, instance);
            }
        }

        public void RegisterType<T1, T2>()
        {
            RegisterType(typeof(T1), typeof(T2));
        }

        public void RegisterType<T>()
        {
            RegisterType(typeof(T), typeof(T));
        }

        public void RegisterInstance<T>(T instance)
        {
            RegisterInstance(typeof(T), instance);
        }

        public object ResolveType(Type typeToResolve)
        {
            if(!_instanceRegistrations.ContainsKey(typeToResolve) && !_typeRegistrations.ContainsKey(typeToResolve))
            {
                throw new ArgumentException(string.Format("{0} type was not registered", typeToResolve.Name));
            }

            if(_instanceRegistrations.ContainsKey(typeToResolve))
            {
                return _instanceRegistrations[typeToResolve];
            }

            var resolvedType = _typeRegistrations[typeToResolve];
            var constructor = resolvedType.GetTypeInfo().DeclaredConstructors.FirstOrDefault();
            var constructorParameterInfos = constructor.GetParameters();
            var constructorParameters = new object[constructorParameterInfos.Length];

            for (var i = 0; i < constructorParameterInfos.Length; i++)
            {
                constructorParameters[i] = ResolveType(constructorParameterInfos[i].ParameterType);
            }

            return constructor.Invoke(constructorParameters);
        }

        public T ResolveType<T>()
        {
            return (T)ResolveType(typeof(T));
        }

        #endregion
    }
}
