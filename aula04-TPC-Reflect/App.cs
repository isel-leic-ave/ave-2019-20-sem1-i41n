using System;
using System.Collections;

class A {}
class B : A {}
class C : B {
    public int x, y;
    public void Foo() {} 
}

class App {

    public static void Main(String[] args) {
        PrintBaseTypes("Ola");
        PrintBaseTypes(19);
        PrintBaseTypes(new C());
        PrintBaseTypes(new System.IO.DirectoryInfo("."));

        PrintMembers(new C());
        PrintMethods(new C());
        PrintFields(new C());
    }
    
    public static void PrintMembers(object obj) {
        Console.Write("Members: ");
        foreach(var m in obj.GetType().GetMembers()) Console.Write(m.Name + " ");
        Console.WriteLine();
    }
    public static void PrintMethods(object obj) {
        Console.Write("Methods: ");
        foreach(var m in obj.GetType().GetMethods()) Console.Write(m.Name + " ");
        Console.WriteLine();
    }
    public static void PrintFields(object obj) {
        Console.Write("Fields: ");
        foreach(var m in obj.GetType().GetFields()) Console.Write(m.Name + " ");
        Console.WriteLine();
    }
    
    // Não Fazer => Avaliação em Tempo de Execução
    // static readonly Type typeOfObject = (new Object()).GetType();
    
    public static void PrintBaseTypes(object obj)
    {
        Type t = obj.GetType();
        do {
            Console.Write(t.Name + " ");
            PrintInterfaces(t);
            t = t.BaseType;
        // } while( t != typeOfObject);
        } while( t != typeof(object));
        Console.WriteLine();
    }
    public static void PrintInterfaces(Type t) {
        
    }
}