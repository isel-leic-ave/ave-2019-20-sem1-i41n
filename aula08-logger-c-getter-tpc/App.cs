using System;

public class Student {
    int nr;
    string name;
    int group;
    string githubId;

    public Student(int nr, string name, int group, string githubId)
    {
        this.nr = nr;
        this.name = name;
        this.group = group;
        this.githubId = githubId;
    }
    
    public int Nr { get {return nr; } }
    public string Name { get {return name; } }
    public int Group { get {return group; } }
    public string GithubId { get {return githubId; } }

}

class Point{
    public readonly int x, y;
    public Point(int x, int y) {
        this.x = x;
        this.y = y;
    }
    public double Module{
        get{
            return Math.Sqrt(x*x + y*y);
        }
    }
    
}

class Account {
    public static readonly int CODE = 4342;
    public long balance;
    public Account(long b) { balance = b; }
}

class App {
    static void Main(){
        Point p = new Point(7, 9);
        Student s = new Student(154134, "Ze Manel", 5243, "ze");
        
        Student[] classroom = {
            new Student(154134, "Ze Manel", 5243, "ze"),
            new Student(765864, "Maria El", 4677, "ma"),
            new Student(456757, "Antonias", 3153, "an"),
        };
        
        Account a = new Account(1300);
        Console.WriteLine("###### Logging Fields and Methods");
        Logger loggerFm = new Logger();
        loggerFm.ReadFields();
        loggerFm.ReadMethods();
        loggerFm.Log(p);
        loggerFm.Log(s);
        loggerFm.Log(classroom);
        Console.WriteLine("###### Logging properties");
        Logger loggerP = new Logger();
        loggerP.ReadProperties();
        loggerP.Log(p);
        loggerP.Log(s);
        loggerP.Log(classroom);
    }
}







