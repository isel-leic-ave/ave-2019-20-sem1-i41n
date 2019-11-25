using System;
using System.Collections;
using System.Text;
using System.IO;

public interface Predicate {
    bool Invoke(object item);
}
public interface Function {
    object Invoke(object item);
}

class App {

    static IEnumerable Lines(string path)
    {
        string line;
        IList res = new ArrayList();
        
        using(StreamReader file = new StreamReader(path)) // <=> try-with resources do Java >= 7
        {
            while ((line = file.ReadLine()) != null)
            {
                res.Add(line);
            }
        }
        return res;
    }
     
    static IEnumerable Convert(IEnumerable src, Function conv) {
        IList res = new ArrayList();
        foreach(object o in src) {
            res.Add(conv.Invoke(o));
        }
        return res;
    }
        
    static IEnumerable Filter(IEnumerable stds, Predicate p) {
        IList res = new ArrayList();
        foreach(object item in stds) {
            if(p.Invoke(item))
                res.Add(item);
        }
        return res;
    }
    
 
    static void Main()
    {
        IEnumerable names = 
            Convert(
                Filter(
                    Filter(
                        Convert(
                            Lines("i41N.txt"),
                            new MapToStudent()),
                        new PredicateNrGreaterThan(42000)), 
                    new PredicateNameStartWith("D")),
                new MapToFirstName());
    
        foreach(object l in names)
            Console.WriteLine(l);
    }
}

class MapToStudent : Function {
    public object Invoke(object item) { return Student.Parse(item.ToString()); }
}
class MapToFirstName : Function {
    public object Invoke(object item) { return ((Student) item).name.Split(' ')[0]; }
}
class PredicateNrGreaterThan : Predicate {
    readonly int nr;
    public PredicateNrGreaterThan(int nr) { this.nr = nr; }
    public bool Invoke(object item) { return ((Student) item).nr > nr; }
}
class PredicateNameStartWith : Predicate {
    readonly string prefix;
    public PredicateNameStartWith(string prefix) { this.prefix = prefix; }
    public bool Invoke(object item) { return ((Student) item).name.StartsWith(prefix); }
}

public class Student
{
    public readonly int nr;
    public readonly string name;
    public readonly int group;
    public readonly string email;
    public readonly string githubId;

    public Student(int nr, String name, int group, string email, string githubId)
    {
        this.nr = nr;
        this.name = name;
        this.group = group;
        this.email = email;
        this.githubId = githubId;
    }

    public override String ToString()
    {
        return String.Format("{0} {1} ({2}, {3})", nr, name, group, githubId);
    }
    public void Print()
    {
        Console.WriteLine(this.ToString());
    }
    
    public static Student Parse(string src){
        string [] words = src.Split('|');
        return new Student(
            int.Parse(words[0]),
            words[1],
            int.Parse(words[2]),
            words[3],
            words[4]);
    }
}
