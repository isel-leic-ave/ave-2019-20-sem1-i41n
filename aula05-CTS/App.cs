using System;

class App
{
    class Student {
        int nr; String name;
    }
    struct Point {
        int x, y;
    }
    
    
    public static void Main(String[] args)
    {
        System.String s1 = "Ola";
        String s2 = s1;           // s1 e s2 tipo referência
        string s3 = s2;           // s3 tipo referência mas primitivo
        
        // Em runtime apenas é conhecido System.String
        Console.WriteLine(s3.GetType().FullName); // > System.String
        
        int n1 = 7;           // n1 tipo valor primitivo
        System.Int32 n2 = n1; // n2 tipo valor
        Console.WriteLine(n1.GetType().FullName); // > System.Int32
        
        Student s = new Student(); // instanciar  => newobj
        Point p = new Point();     // inicializar => initobj
    }
}