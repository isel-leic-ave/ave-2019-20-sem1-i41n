using System;

public interface IGetter {
    string GetName();
    object GetValue(object target);
}

public class DummyGetterStudentNr : IGetter{

    public string GetName() { return "Nr";}
    public object GetValue(object target) { 
        return ((Student) target).Nr;
    }
}


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
}
