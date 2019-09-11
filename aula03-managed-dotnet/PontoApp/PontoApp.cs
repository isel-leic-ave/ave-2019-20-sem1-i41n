using System;

class PontoApp {

    public static void Main(String[] args) {
        Console.WriteLine("Showing lazy class load!");
        // Foo(); // => Colocando em comentários podemos pagar o Ponto.dll para a execução.
    }
    
    public static void Foo()
    {
        Ponto p = new Ponto(5, 7);
        p.print();
        Console.WriteLine("p._x = {0}", p._x);
        Console.WriteLine("p._y = {0}", p._y);
    }
}