using System;
using System.Collections.Generic;

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

class Polygon {
    private List<Point> pts = new List<Point>();
    public IEnumerable<Point> Points => pts;
    public Polygon(params Point [] pts) {
        this.pts.AddRange(pts);
    }
}

public class Person
{
    public string Name { get; set; }
    public Person Sibling { get; set; }
    public Person(string name, Person sibling)
    {
        this.Name = name;
        this.Sibling = sibling;
    }
}
class App {
    static void Main(){
        Point p = new Point(7, 9);
        Polygon pl = new Polygon(new Point(21,4), new Point(44,2), new Point(1,11));
        Person twins = new Person("Ze", new Person("Maria", new Person("Manel", null)));
        Logger logger = new Logger();
        logger.ReadProperties();
        logger.Log(p);
        logger.Log(pl);
        logger.Log(twins);
    }
}
