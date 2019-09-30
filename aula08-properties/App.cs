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
    public Point(int x, int y) {
        this.X = x;
        this.Y = y;
    }
    
    public int X { get; set; } // Auto-implemented properties
    public int Y { get; set; }
    
    public double Module{
        get{
            return Math.Sqrt(X*X + Y*Y);
        }
    }
    
}

class App {
    static void Main(){
        Point p = new Point(7, 9);
        Student s = new Student(154134, "Ze Manel", 5243, "ze");

    }
}







