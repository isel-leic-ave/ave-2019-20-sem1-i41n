using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;


public class LoggerEmit
{
    readonly ILoggerPrinter printer;
    static Dictionary<Type, IEnumerable<IGetter>> gs = new Dictionary<Type, IEnumerable<IGetter>>();

    public LoggerEmit(ILoggerPrinter printer)
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
            // l.Add(new GetterProperty(p)); // Usa Reflexão para ler valor de Propriedade
            Type klassGetter = BuildGetter(t, p);
            l.Add((IGetter)Activator.CreateInstance(klassGetter));
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

    static Type BuildGetter(Type klass, PropertyInfo p)
    {
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
        // <=> class Getter<klass.name><p.Name> : IGetter 
        //
        TypeBuilder myTypeBuilder = myModuleBuilder.DefineType(
            "Getter" + klass.Name + p.Name,
            TypeAttributes.Public,
            typeof(object),
            new Type[] { typeof(IGetter) });

        buildGetNameMethod(myTypeBuilder, p);
        buildGetValueMethod(myTypeBuilder, klass, p);


        // Create the Getter<klass.name><p.name>
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

    static void buildGetValueMethod(TypeBuilder myTypeBuilder, Type klass, PropertyInfo p)
    {
        MethodBuilder getValue = myTypeBuilder.DefineMethod(
            "GetValue",
            MethodAttributes.Public | MethodAttributes.ReuseSlot |
            MethodAttributes.HideBySig | MethodAttributes.Virtual,
            CallingConventions.Standard,
            typeof(object), // Return type
            new Type[] { typeof(object) });

        // Add IL to GetValue body
        ILGenerator methodIL = getValue.GetILGenerator();
        methodIL.Emit(OpCodes.Ldarg_1); // ldarg.1
        methodIL.Emit(OpCodes.Castclass, klass); // castclass  Student
        methodIL.Emit(OpCodes.Callvirt, p.GetGetMethod());// callvirt  Student::get_Nr()
        if(p.PropertyType.IsValueType)
            methodIL.Emit(OpCodes.Box, p.PropertyType); // box  [mscorlib]System.Int32
        methodIL.Emit(OpCodes.Ret);// ret
    }

    static void buildGetNameMethod(TypeBuilder myTypeBuilder, PropertyInfo p)
    {
        // Add 'GetName' method to the class, with the specified attribute and signature.
        MethodBuilder getName = myTypeBuilder.DefineMethod(
            "GetName",
            MethodAttributes.Public | MethodAttributes.ReuseSlot |
            MethodAttributes.HideBySig | MethodAttributes.Virtual,
            CallingConventions.Standard,
            typeof(string), // Return type
            new Type[0]);

        // Add IL to GetName body
        ILGenerator methodIL = getName.GetILGenerator();
        methodIL.Emit(OpCodes.Ldstr, p.Name);
        methodIL.Emit(OpCodes.Ret);
    }
}

public class LogAttribute : Attribute
{
    public virtual string Print(object val)
    {
        return val.ToString();
    }
}