
using System;
using Castle.Core.Interceptor;
using Castle.DynamicProxy;

namespace CodeSoda.ObjectProxy
{
	public class LazyProxy<T> : IInterceptor where T : class, new()
	{
		private readonly Func<T> _loader = null;
		private T _lazyObject = null;
		private bool _loaded = false;

		public LazyProxy(Func<T> loader) {
			this._loader = loader;
		}

		public void Intercept(IInvocation invocation)
		{
			if (!_loaded)
			{
				_lazyObject = _loader();
				_loaded = true;
			}

			var accessor = invocation.Proxy as IProxyTargetAccessor;
			var obj = accessor.DynProxyGetTarget();

			invocation.ReturnValue = invocation.MethodInvocationTarget.Invoke(_lazyObject, invocation.Arguments);
		}

		#region Static

		private static readonly ProxyGenerator Generator = new ProxyGenerator();

		public static TClass Make<TClass>(Func<TClass> loader) where TClass : class, new()
		{
			var lazyObjectProxy = new LazyProxy<TClass>(loader);
			var proxy = Generator.CreateClassProxy<TClass>(lazyObjectProxy);
			return proxy;
		}

		#endregion

	}
}