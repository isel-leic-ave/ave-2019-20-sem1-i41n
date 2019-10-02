using System;

[AttributeUsage(AttributeTargets.All, AllowMultiple=true)]
public class MyColorAttribute : Attribute {
    public MyColorAttribute(string color) {this.color = color;}
    public MyColorAttribute() : this(null) {}
    
    private readonly string color;
    public int Id {get; set;}
    public String Desc {get; set; }
    
    public override string ToString() {
        return color + " " + Id + " " + Desc;
    }
}

public class Colors {
    public static void Check(object obj) {
        object[] attrs = obj.GetType().GetCustomAttributes(typeof(MyColorAttribute), true);
        Console.WriteLine(obj.GetType() + " MyColor attributes instances:");
        foreach(object a in attrs) {
            MyColorAttribute c = (MyColorAttribute) a;
            Console.WriteLine("   " + c.ToString());
        }
    }
}