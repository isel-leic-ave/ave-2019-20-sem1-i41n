import java.io.FileWriter;
import java.io.IOException;
import java.util.Scanner;

public class App {

    public static void main(String [] args) throws IOException  {
        TestUsing();
    }
    static void TestUsing() throws IOException
    {
        Scanner Console = new Scanner(System.in);
        // 
        // Uma instância de FileStream mantém um handle para um recurso nativo, 
        // i.e. um ficheiro.
        //
        try(FileWriter fs = new FileWriter("out.txt")) {
            // Wait for user to hit <Enter>
            System.out.println("Wait for user to hit <Enter>");
		    Console.nextLine();
        } 
        System.out.println("Wait for user to hit <Enter>");
		Console.nextLine();
	}    
}