using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

static class App {
    static IEnumerable<String> Lines(string path)
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
     
    static IEnumerable<R> Convert<T, R>(this IEnumerable<T> src, Func<T,R> conv) {
        foreach(T o in src) {
            yield return conv(o); // <=> conv.Invoke(o)
        }
    }        
    static IEnumerable<T> Filter<T>(this IEnumerable<T> stds, Predicate<T> p) {
        foreach(T item in stds) {
            if(p(item)) // <=> p.Invoke(item)
                yield return item;
        }
    }
    static IEnumerable<T> Skip<T>(this IEnumerable<T> src, int nr) { 
        // To Do...
        throw new Exception("To do... TPC");
    }
    static IEnumerable<T> Take<T>(this IEnumerable<T> src, int limit) { // <=> Top, ou limit(), ...
        foreach(T item in src) {
            if(limit-- > 0) 
                yield return item;
            else break;
        }
    }
    
    static void Main()
    {
        IEnumerable<String> names = Lines("i41N.txt")
            .Convert(item => Student.Parse(item.ToString()))
            .Filter(std => std.nr > 42000)
            .Filter(std => std.name.StartsWith("D"))
            .Convert(std => std.name.Split(' ')[0])
            .Take(7);
                        
        Console.ReadLine();
        foreach(string l in names)
            Console.WriteLine(l);       
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
