using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
     
    
    static void Main()
    {
        IEnumerable<String> names = Lines("i41N.txt")
            .Select(item => Student.Parse(item.ToString()))
            .Where(std => std.nr > 42000)
            .Where(std => std.name.StartsWith("D"))
            .Select(std => std.name.Split(' ')[0])
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
