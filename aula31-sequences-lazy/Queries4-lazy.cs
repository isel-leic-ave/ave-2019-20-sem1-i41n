using System;
using System.Collections;
using System.Text;
using System.IO;

delegate bool Predicate(object item);

delegate object Function(object item);

class TakeEnumerable : IEnumerable {
    IEnumerable src;
    int limit;
    public TakeEnumerable(IEnumerable src, int limit) {
        this.src = src;
        this.limit = limit;
    }
    public IEnumerator GetEnumerator() {
        return new TakeEnumerator(src, limit);
    }
}
class TakeEnumerator : IEnumerator {
    IEnumerator srcIter;
    int limit;
    public TakeEnumerator(IEnumerable src, int limit) {
        this.srcIter = src.GetEnumerator();
        this.limit = limit;
    }
    public bool MoveNext() {
        return limit-- > 0 && srcIter.MoveNext();
    }
    public object Current {
        get { return srcIter.Current; }
    }
    public void Reset() {
        srcIter.Reset();
    }
}

class ConvertEnumerable : IEnumerable {
    IEnumerable src;
    Function mapper;
    public ConvertEnumerable(IEnumerable src, Function mapper) {
        this.src = src;
        this.mapper = mapper;
    }
    public IEnumerator GetEnumerator() {
        return new ConvertEnumerator(src, mapper);
    }
}
class ConvertEnumerator : IEnumerator {
    IEnumerator srcIter;
    Function mapper;
    public ConvertEnumerator(IEnumerable src, Function mapper) {
        this.srcIter = src.GetEnumerator();
        this.mapper = mapper;
    }
    public bool MoveNext() {
        return srcIter.MoveNext();
    }
    public object Current {
        get { return mapper(srcIter.Current); }
    }
    public void Reset() {
        srcIter.Reset();
    }
}

class FilterEnumerable : IEnumerable {
    IEnumerable src;
    Predicate p;
    public FilterEnumerable(IEnumerable src, Predicate p) {
        this.src = src;
        this.p = p;
    }
    public IEnumerator GetEnumerator() {
        return new FilterEnumerator(src, p);
    }
}
class FilterEnumerator : IEnumerator {
    IEnumerator srcIter;
    Predicate p;
    public FilterEnumerator(IEnumerable src, Predicate p) {
        this.srcIter = src.GetEnumerator();
        this.p = p;
    }
    public bool MoveNext() {
        while(srcIter.MoveNext()) {
            if(p(srcIter.Current))
                return true;
        }
        return false;
    }
    public object Current {
        get { return srcIter.Current; }
    }
    public void Reset() {
        srcIter.Reset();
    }
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
     
    static IEnumerable Convert(IEnumerable src, Function mapper) {
        return new ConvertEnumerable(src, mapper);
    }  
    static IEnumerable Take(IEnumerable src, int limit) { // <=> Top, ou limit(), ...
        return new TakeEnumerable(src, limit);
    }
    static IEnumerable Filter(IEnumerable src, Predicate pred) {
        return new FilterEnumerable(src, pred);
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
                            Console.WriteLine("Filter..." + item); 
                            return ((Student) item).name.StartsWith("D");
                        }),
                    item => { 
                        count++;
                        Console.WriteLine("Convert..." + item); 
                        return ((Student) item).name.Split(' ')[0];
                    }),
                4);
                        
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
