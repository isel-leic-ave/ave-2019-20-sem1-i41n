using System;

public class Student {
    int nr;
    string name;
    int group;
    string githubId;
    DateTime birth;

    public Student(int nr, string name, int group, string githubId, DateTime birth)
    {
        this.nr = nr;
        this.name = name;
        this.group = group;
        this.githubId = githubId;
        this.birth = birth;
    }
    
    public int Nr { get {return nr; } }
    public string Name { get {return name; } }
    public int Group { get {return group; } }
    public string GithubId { get {return githubId; } }
    public DateTime Birth {get { return birth; }}
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
        Student s = new Student(154134, "Ze Manel", 5243, "ze", new DateTime(2000, 11, 23));
        
        Student[] classroom = {
            new Student(154134, "Ze Manel", 5243, "ze", new DateTime(2000, 11, 23)),
            new Student(765864, "Maria El", 4677, "ma", new DateTime(2000, 11, 23)),
            new Student(456757, "Antonias", 3153, "an", new DateTime(2000, 11, 23))
        };
        
        Account a = new Account(1300);
        Logger logger = new Logger();
        // logger.Log(classroom);
        logger.Log(s);
    }
}







