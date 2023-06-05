using System;
using System.Collections.Generic;

public class Service
{
    private IDictionary<Type, object> services = new Dictionary<Type, object>();

    public void Register(object o)
    {
        var type = o.GetType();

        services[type] = o;
    }

    public T Get<T>()
    {
        var type = typeof(T);

        if (!services.ContainsKey(type))
        {
            throw new InvalidOperationException($"{type.FullName} is not registered.");
        }

        return (T)services[type];
    }
}
