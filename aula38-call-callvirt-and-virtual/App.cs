using System;

interface I { void MI(); }

class A : I {
    readonly int nr = 73;
    public static void S() {Console.WriteLine("S");}  // static
    public void M(){Console.WriteLine("M" + this.nr);}          // instância (não virtual)
    public void M2(){ this.M(); }          // instância (não virtual)
    public virtual void MV(){Console.WriteLine("MV");} // virtual (de instância)
    public virtual void MI(){Console.WriteLine("MI");}  // interface 
}

class B : A {
    public override void MV() { 
        Console.WriteLine("MV of B");
        base.MV(); // call  
    }
}

struct TV {
    public void M() { Console.WriteLine("M of TV"); }
}
class App {
    static void Main() {
        /*
        A a = new A();
        A.S(); // call => DE
        a.MV(); // lddlo.0 + callvirt => DD
        a.MI(); // lddlo.0 + callvirt => DD
        a = null;
        a.M(); // lddlo.0 + callvirt => DE
        */

        (new B()).MV();
        
        TV tv = new TV(); // IL: initobj
        tv.M(); // lodlocA.0 + call => DE
    }
}
