using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using Logger;

public class LoggerReflect
{
    readonly ILoggerPrinter printer;
    static Dictionary<Type, IEnumerable<IGetter>> gs = new Dictionary<Type, IEnumerable<IGetter>>();

    public LoggerReflect(ILoggerPrinter printer)
    {
        this.printer = printer;
    }

    public void Log(object o)
    {
        Type t = o.GetType();
        if (t.IsArray) LogArray((IEnumerable)o);
        else
        {
            IEnumerable<IGetter> getters;
            if (!gs.TryGetValue(t, out getters)) {
                getters = InitProperties(t);
                gs.Add(t, getters);
            }
            LogObject(o, getters);
        }
    }

    public static IEnumerable<IGetter> InitProperties(Type t)
    {
        List<IGetter> l = new List<IGetter>();
        foreach (PropertyInfo p in t.GetProperties())
        {
            l.Add(new GetterProperty(p)); // Usa Reflexão para ler valor de Propriedade
        }
        return l;
    }


    public void LogArray(IEnumerable o)
    {
        Type elemType = o.GetType().GetElementType(); // Tipo dos elementos do Array
        var getters = InitProperties(elemType);
        printer.WriteLine("Array of " + elemType.Name + "[");
        foreach (object item in o) LogObject(item, getters); // * 
        printer.WriteLine("]");
    }

    public void LogObject(object o, IEnumerable<IGetter> gs)
    {
        Type t = o.GetType();
        printer.Write(t.Name + "{");
        foreach (IGetter g in gs)
        {
            printer.Write(g.GetName() + ": ");
            printer.Write(g.GetValue(o) + ", ");
        }
        printer.WriteLine("}");
    }
}