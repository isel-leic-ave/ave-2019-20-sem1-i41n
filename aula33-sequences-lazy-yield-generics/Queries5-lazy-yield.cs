using System;
using System.Collections;
using System.Text;
using System.IO;

delegate bool Predicate(object item);

delegate object Function(object item);


class App {
    static IEnumerable Lines(string path)
    { 
        using(StreamReader file = new StreamReader(path)) // <=> try-with resources do Java >= 7
        {
            string line;
            while ((line = file.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
     
    static IEnumerable Convert(IEnumerable src, Function conv) {
        foreach(object o in src) {
            yield return conv(o); // <=> conv.Invoke(o)
        }
    }        
    static IEnumerable Filter(IEnumerable stds, Predicate p) {
        foreach(object item in stds) {
            if(p(item)) // <=> p.Invoke(item)
                yield return item;
        }
    }
    static IEnumerable Skip(IEnumerable src, int nr) { 
        // To Do...
        throw new Exception("To do... TPC");
    }
    static IEnumerable Take(IEnumerable src, int limit) { // <=> Top, ou limit(), ...
        foreach(object item in src) {
            if(limit-- > 0) 
                yield return item;
            else break;
        }
    }
    
    static void Main()
    {
        int count = 0;
        IEnumerable names = 
            Take(
                Convert(
                    Filter(
                        Filter(
                            Convert(
                                Lines("i41N.txt"),
                                item => { 
                                    count++;
                                    return Student.Parse(item.ToString());
                                }),
                            item => { 
                                    count++;
                                    return((Student) item).nr > 42000;
                            }),
                        item => { 
                            count++;
                            // Console.WriteLine("Filter..." + item); 
                            return ((Student) item).name.StartsWith("D");
                        }),
                    item => { 
                        count++;
                        // Console.WriteLine("Convert..." + item); 
                        return ((Student) item).name.Split(' ')[0];
                    }),
                7);
                        
        Console.ReadLine();
        foreach(object l in names)
            Console.WriteLine(l);
            
        Console.WriteLine(count);
            
    }
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
    
    public static Student Parse(object src){
        return Student.Parse(src.ToString());
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
