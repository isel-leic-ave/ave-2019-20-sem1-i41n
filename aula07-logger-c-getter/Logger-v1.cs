using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class Logger {

    public static void Log(object o) {
        Type t = o.GetType();
        if(t.IsArray) LogArray((IEnumerable) o);
        else {
            LogObject(o, InitFields(t), InitMethods(t));
        }
    }
    
    public static IEnumerable<FieldInfo> InitFields(Type t) {
        return t.GetFields();
    }
    public static IEnumerable<MethodInfo> InitMethods(Type t) {
        List<MethodInfo> l = new List<MethodInfo>();
        foreach(MethodInfo m in t.GetMethods()) {
            if(m.ReturnType != typeof(void) && m.GetParameters().Length == 0) {
                l.Add(m);
            }
        }
        return l;
    }
    
    public static void LogArray(IEnumerable o) {
        Type elemType = o.GetType().GetElementType(); // Tipo dos elementos do Array
        var fs = InitFields(elemType ); // 1x
        var ms = InitMethods(elemType ); // 1x
        Console.WriteLine("Array of " + elemType.Name + "[");
        foreach(object item in o) LogObject(item, fs, ms); // * 
        Console.WriteLine("]");
    }
    
    public static void LogObject(object o, IEnumerable<FieldInfo> fs, IEnumerable<MethodInfo> ms) {
        Type t = o.GetType();
        Console.Write(t.Name + "{");
        foreach(FieldInfo f in fs) {
            Console.Write(f.Name + ": ");
            Console.Write(f.GetValue(o) + ", ");
        }
        foreach(MethodInfo m in ms) {
            Console.Write(m.Name + ": ");
            Console.Write(m.Invoke(o, new object[0]) + ", ");
        }
        Console.WriteLine("}");
    }
}