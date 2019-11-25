using System;
using System.Collections;

class App {

    static IEnumerable Foo() {
        Console.WriteLine("Computing item 1");
        yield return "ola";
        Console.WriteLine("Computing item 2");
        yield return "super";
        Console.WriteLine("Computing item 3");
        yield return "isel";
    }
    static void Main() {
        Console.WriteLine("Invoking Foo");
        var src = Foo();
        Console.ReadLine();
        Console.WriteLine("Invoking GetEnumerator");
        var iter = src.GetEnumerator();
        Console.ReadLine();
        iter.MoveNext();
        Console.WriteLine(iter.Current); // > ola
        iter.MoveNext();
        Console.WriteLine(iter.Current); // > super
        //
        // Th isel item was not computed
    }
}