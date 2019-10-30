using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;

public interface IGetter {
    string GetName();
    object GetValue(object target);
}

public class GetterProperty : IGetter{
    PropertyInfo p;
    public GetterProperty(PropertyInfo p) {this.p = p;}
    public string GetName() { return p.Name; }
    public object GetValue(object target) { 
        return p.GetValue(target);
    }
}

public class Logger {

    List<MethodInfo> configuration = new List<MethodInfo>();

    public void Log(object o) {
        Type t = o.GetType();
        if(t.IsArray) LogArray((IEnumerable) o);
        else {
            LogObject(o, InitProperties(t));
        }
    }
    
    public static IEnumerable<IGetter> InitProperties(Type t) {
        List<IGetter> l = new List<IGetter>();
        foreach(PropertyInfo p in t.GetProperties()) {
            // l.Add(new GetterProperty(p));
            Type klassGetter = BuildGetter(t, p);
            l.Add((IGetter) Activator.CreateInstance(klassGetter));
        }
        return l;
    }
    
    
    public void LogArray(IEnumerable o) {
        Type elemType = o.GetType().GetElementType(); // Tipo dos elementos do Array
        var getters = InitProperties(elemType);
        Console.WriteLine("Array of " + elemType.Name + "[");
        foreach(object item in o) LogObject(item, getters); // * 
        Console.WriteLine("]");
    }
    
    public static void LogObject(object o, IEnumerable<IGetter> gs) {
        Type t = o.GetType();
        Console.Write(t.Name + "{");
        foreach(IGetter g in gs) {
            Console.Write(g.GetName() + ": ");
            Console.Write(g.GetValue(o) + ", ");
        }
        Console.WriteLine("}");
    }
    
    static Type BuildGetter(Type klass, PropertyInfo p) {
        AssemblyName myAssemblyName = new AssemblyName();
        myAssemblyName.Name = "LibGetter" + klass.Name + p.Name;

        // Define a dynamic assembly in the current application domain.
        AssemblyBuilder myAssemblyBuilder = AppDomain
            .CurrentDomain
            .DefineDynamicAssembly(myAssemblyName, AssemblyBuilderAccess.RunAndSave);

        // Define a dynamic module in this assembly.
        ModuleBuilder myModuleBuilder = myAssemblyBuilder.
                                      DefineDynamicModule(myAssemblyName.Name, myAssemblyName.Name + ".dll");

        // Define a runtime class with specified name and attributes.
        TypeBuilder myTypeBuilder = myModuleBuilder.DefineType(
            "Getter" + klass.Name + p.Name, 
            TypeAttributes.Public,
            typeof(object),
            new Type[]{typeof(IGetter)});

        // Add 'MyMethod' method to the class, with the specified attribute and signature.
        MethodBuilder getName = myTypeBuilder.DefineMethod(
            "GetName",
            MethodAttributes.Public, 
            CallingConventions.Standard, 
            typeof(string), // Return type
            new Type[0]);
            
        // Add IL to MyMethod body
        ILGenerator methodIL = getName.GetILGenerator();
        // methodIL.EmitWriteLine("In the method...");
        methodIL.Emit(OpCodes.Ldstr, p.Name);
        methodIL.Emit(OpCodes.Ret);
        
        // ToDo: GetValue()
        
        // Create the TempClass
        Type t = myTypeBuilder.CreateType();
        
        // The following line saves the single-module assembly. This
        // requires AssemblyBuilderAccess to include Save. You can now
        // type "ildasm MyDynamicAsm.dll" at the command prompt, and 
        // examine the assembly. You can also write a program that has
        // a reference to the assembly, and use the MyDynamicType type.
        // 
        myAssemblyBuilder.Save(myAssemblyName.Name + ".dll");
        return t;
    }
}

public class LogAttribute : Attribute {
    public virtual string Print(object val) {
        return val.ToString();
    }
}