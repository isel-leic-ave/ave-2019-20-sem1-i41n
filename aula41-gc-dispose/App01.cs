using System;
using System.IO;

class App {

    static void Main() {
        // TestGC();
        // TestDispose();
        TestUsing();
    }
    /**
     * Do not use this kind of operations in production code.
     * GC.Collect() should only be used for experimental or tests pruposes.
     */
    static void PrintRunningGC(){
        Console.WriteLine("Running GC for generation ");
        GC.Collect();  // !!! DON'T use in production
        GC.WaitForPendingFinalizers();
    }
    
    static void TestUsing()
    {
        // 
        // Uma instância de FileStream mantém um handle para um recurso nativo, 
        // i.e. um ficheiro.
        //
        using(FileStream fs = new FileStream("out.txt", FileMode.Create)) {
            // Wait for user to hit <Enter>
            Console.WriteLine("Wait for user to hit <Enter>");
		    Console.ReadLine();
        } 
        Console.WriteLine("Wait for user to hit <Enter>");
		Console.ReadLine();
	}

    static void TestDispose()
    {
        // 
        // Uma instância de FileStream mantém um handle para um recurso nativo, 
        // i.e. um ficheiro.
        //
        FileStream fs = null;
        try {
            fs = new FileStream("out.txt", FileMode.Create);
            // Wait for user to hit <Enter>
            Console.WriteLine("Wait for user to hit <Enter>");
		    Console.ReadLine();
        } finally {
            if(fs != null) {
                fs.Dispose();
                fs = null;
            }
        }
        Console.WriteLine("Wait for user to hit <Enter>");
		Console.ReadLine();
	} 
    static void TestGC()
    {
        // 
        // Uma instância de FileStream mantém um handle para um recurso nativo, 
        // i.e. um ficheiro.
        //
        FileStream fs = new FileStream("out.txt", FileMode.Create);
		// Wait for user to hit <Enter>
        Console.WriteLine("Wait for user to hit <Enter>");
		Console.ReadLine();
		
        PrintRunningGC(); // DON'T do it in production
        
        Console.WriteLine("Wait for user to hit <Enter>");
		Console.ReadLine();
	}    
}