using System;
using System.Reflection;
using System.Collections;

public class Logger {

    public static void Log(object o) {
        Type t = o.GetType();
        if(t.IsArray) LogArray((IEnumerable) o);
        else LogObject(o);
    }
    
    public static void LogArray(IEnumerable o) {
        Console.WriteLine("[");
        foreach(object item in o) LogObject(item);
        Console.WriteLine("]");
    }
    
    public static void LogObject(object o) {
        Type t = o.GetType();
        Console.Write(t.Name + "{");
        foreach(FieldInfo f in t.GetFields()) {
            Console.Write(f.Name + ": ");
            if(f.IsStatic)
                Console.Write(f.GetValue(null) + ", ");
            else
                Console.Write(f.GetValue(o) + ", ");
        }
        foreach(MethodInfo m in t.GetMethods()) {
            if(m.ReturnType != typeof(void) && m.GetParameters().Length == 0) {
                Console.Write(m.Name + ": ");
                Console.Write(m.Invoke(o, new object[0]) + ", ");
            }
        }
        Console.WriteLine("}");
    }
}