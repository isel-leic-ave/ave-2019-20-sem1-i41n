using System;
using System.Reflection;
using System.Reflection.Emit;

class App {
    static void Main() {
        Type klass = BuildType();
        object obj = Activator.CreateInstance(klass);
        MethodInfo mi = klass.GetMethod("MyMethod");
        mi.Invoke(obj, new object[]{"ola"});
        Console.WriteLine(obj);        
    }
    
    public static Type BuildType() {
        AssemblyName myAssemblyName = new AssemblyName();
        myAssemblyName.Name = "TempLib";

        // Define a dynamic assembly in the current application domain.
        AssemblyBuilder myAssemblyBuilder = AppDomain
            .CurrentDomain
            .DefineDynamicAssembly(myAssemblyName, AssemblyBuilderAccess.RunAndSave);

        // Define a dynamic module in this assembly.
        ModuleBuilder myModuleBuilder = myAssemblyBuilder.
                                      DefineDynamicModule(myAssemblyName.Name, myAssemblyName.Name + ".dll");

        // Define a runtime class with specified name and attributes.
        TypeBuilder myTypeBuilder = myModuleBuilder.DefineType
                                       ("TempClass",TypeAttributes.Public);

        // Add 'Greeting' field to the class, with the specified attribute and type.
        FieldBuilder greetingField = myTypeBuilder
            .DefineField("Greeting", typeof(String), FieldAttributes.Public);
            
        Type[] myMethodArgs = { typeof(String) };

        // Add 'MyMethod' method to the class, with the specified attribute and signature.
        MethodBuilder myMethod = myTypeBuilder.DefineMethod(
            "MyMethod",
            MethodAttributes.Public, 
            CallingConventions.Standard, 
            null, // Return type
            myMethodArgs);
        // Add IL to MyMethod body
        ILGenerator methodIL = myMethod.GetILGenerator();
        methodIL.EmitWriteLine("In the method...");
        methodIL.Emit(OpCodes.Ldarg_0);
        methodIL.Emit(OpCodes.Ldarg_1);
        methodIL.Emit(OpCodes.Stfld, greetingField);
        methodIL.Emit(OpCodes.Ret);
        
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