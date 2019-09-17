using System;

class App {

    public static void Main(String[] args) {
        Foo(12);
        Foo(19);
    }
    
    public static void Foo(int nr)
    {
        Console.Write(nr);
        Console.Write(",");
        if(nr == 1) {Console.WriteLine(); return; }
        if(nr%2 == 0) {Foo(nr/2); return; }
        Foo(nr*3 + 1);
    }
}