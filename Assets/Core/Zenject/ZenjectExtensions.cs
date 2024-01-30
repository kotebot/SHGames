using UnityEngine;
using Zenject;

namespace Core.Zenject
{
    public static class ZenjectExtensions
    {
        #region Bind

        public static ConcreteIdBinderNonGeneric Bind<TContract, TContract1>(this DiContainer container)
        {
            return container.Bind(typeof(TContract), typeof(TContract1));
        }
        
        public static ConcreteIdBinderNonGeneric Bind<TContract, TContract1, TContract2>(this DiContainer container)
        {
            return container.Bind(typeof(TContract), typeof(TContract1), typeof(TContract2));
        }
        
        public static ConcreteIdBinderNonGeneric Bind<TContract, TContract1, TContract2, TContract3>(this DiContainer container)
        {
            return container.Bind(typeof(TContract), typeof(TContract1), typeof(TContract2), typeof(TContract3));
        }
        
        public static ConcreteIdBinderNonGeneric Bind<TContract, TContract1, TContract2, TContract3, TContract4>(this DiContainer container)
        {
            return container.Bind(typeof(TContract), typeof(TContract1), typeof(TContract2), typeof(TContract3), typeof(TContract4));
        }
        
        public static ConcreteIdBinderNonGeneric Bind<TContract, TContract1, TContract2, TContract3, TContract4, TContract5>(this DiContainer container)
        {
            return container.Bind(typeof(TContract), typeof(TContract1), typeof(TContract2), typeof(TContract3), typeof(TContract4), typeof(TContract5));
        }

        #endregion
        
        public static TMono Instantiate<TMono>(this DiContainer container, TMono prefab, Transform parent = null) where TMono : MonoBehaviour
        {
            return container.InstantiatePrefab(prefab, parent).GetComponent<TMono>();
        }

        public static TMono Instantiate<TMono>(this DiContainer container, TMono prefab, Transform parent, Vector3 position) where TMono : MonoBehaviour
        {
            return container.InstantiatePrefab(prefab, position, Quaternion.identity, parent).GetComponent<TMono>();
        }

    }
}