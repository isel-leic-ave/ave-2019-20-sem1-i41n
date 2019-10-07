using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public interface IGetter {
    string GetName();
    object GetValue(object target);
}
public class GetterField : IGetter{
    FieldInfo f; 
    public GetterField(FieldInfo f) { this.f = f;}
    public string GetName() { return f.Name; }
    public object GetValue(object target) {
        return f.GetValue(target);
    }
}
public class GetterMethod : IGetter{
    MethodInfo m;
    public GetterMethod(MethodInfo m) {this.m = m;}
    public string GetName() { return m.Name; }
    public object GetValue(object target) { 
        return m.Invoke(target, new object[0]);
    }
}
public class GetterProperty : IGetter{
    PropertyInfo p;
    public GetterProperty(PropertyInfo p) {this.p = p;}
    public string GetName() { return p.Name; }
    public object GetValue(object target) { 
        return p.GetValue(target);
    }
}
public class GetterPropertyAndFormat : IGetter{
    PropertyInfo p;
    LogAttribute log;
    public GetterPropertyAndFormat(PropertyInfo p, LogAttribute log) {
        this.p = p;
        this.log = log;
    }
    public string GetName() { return p.Name; }
    public object GetValue(object target) { 
        object val = p.GetValue(target);
        return log.Print(val);
    }
}

public class Logger {

    List<MethodInfo> configuration = new List<MethodInfo>();

    public void Log(object o) {
        Type t = o.GetType();
        // if(t.IsArray) 
        if(typeof(IEnumerable).IsAssignableFrom(t)) 
            LogArray((IEnumerable) o);
        else {
            LogObject(o, InitGetters(t));
        }
    }
    IEnumerable<IGetter> InitGetters(Type t) {
        var getters = new List<IGetter>();
        foreach(MethodInfo m in configuration){
            getters.AddRange((IEnumerable<IGetter>)m.Invoke(null, new object[]{t}));
        }
        return getters;
    }
    public static IEnumerable<IGetter> InitFields(Type t) {
        List<IGetter> l = new List<IGetter>();
        foreach(FieldInfo m in t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)) {
            l.Add(new GetterField(m));
        }
        return l;
    }
    public static IEnumerable<IGetter> InitMethods(Type t) {
        List<IGetter> l = new List<IGetter>();
        foreach(MethodInfo m in t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)) {
            if(m.ReturnType != typeof(void) && m.GetParameters().Length == 0) {
                l.Add(new GetterMethod(m));
            }
        }
        return l;
    }
    public static IEnumerable<IGetter> InitProperties(Type t) {
        List<IGetter> l = new List<IGetter>();
        foreach(PropertyInfo p in t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)) {
            l.Add(new GetterProperty(p));
        }
        return l;
    }
    public static IEnumerable<IGetter> InitPropertiesAnnotated(Type t) {
        List<IGetter> l = new List<IGetter>();
        foreach(PropertyInfo p in t.GetProperties()) {
            // if(Attribute.IsDefined(p, typeof(LogAttribute), true))
            object [] attrs = p.GetCustomAttributes(typeof(LogAttribute), true);
            if(attrs.Length != 0) {
                LogAttribute a = (LogAttribute) attrs[0];
                l.Add(new GetterPropertyAndFormat(p, a));
            }
        }
        return l;
    }
      
    public void LogArray(IEnumerable o) {
        // Type elemType = o.GetType().GetElementType(); // Tipo dos elementos do Array
        IEnumerable<IGetter> getters = null;
        foreach(object item in o) {
            if(getters == null) {
                Console.WriteLine("Sequence of " + item.GetType().Name + "[");
                getters = InitGetters(item.GetType());
            }
            LogObject(item, getters); 
        }
        Console.WriteLine("]");
    }
    
    public void LogObject(object o, IEnumerable<IGetter> gs) {
        Type t = o.GetType();
        Console.Write(t.Name + "{");
        foreach(IGetter g in gs) {
            Console.Write(g.GetName() + ": ");
            object val = g.GetValue(o);
            if(val == null 
                || val.GetType().IsPrimitive 
                // || val.GetType() == typeof(string)) // Tipo Exactamente igual a String
                // || val.GetType().IsSubclassOf(typeof(string))) // Tipo Compatível com String
                || typeof(string).IsAssignableFrom(val.GetType())) // Tipo Compatível com String
            {
                Console.Write(val + ", ");
            }
            else Log(val);
        }
        Console.WriteLine("}");
    }
    
    static readonly MethodInfo INIT_FIELDS = typeof(Logger).GetMethod("InitFields");
    static readonly MethodInfo INIT_METHODS = typeof(Logger).GetMethod("InitMethods");
    static readonly MethodInfo INIT_PROPERTIES = typeof(Logger).GetMethod("InitProperties");
    static readonly MethodInfo INIT_PROPERTIES_ANNOTATED = typeof(Logger).GetMethod("InitPropertiesAnnotated");
    public void ReadFields(){
        configuration.Add(INIT_FIELDS);
    }
    public void ReadMethods(){
        configuration.Add(INIT_METHODS);
    }
    public void ReadProperties(){
        configuration.Add(INIT_PROPERTIES);
    }
    public void ReadPropertiesAnnotated(){
        configuration.Add(INIT_PROPERTIES_ANNOTATED);
    }
}

public class LogAttribute : Attribute {
    public virtual string Print(object val) {
        return val.ToString();
    }
}