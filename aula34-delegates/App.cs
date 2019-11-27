using System;

delegate bool Predicate(object item);
delegate string StringSupplier();

class C {
    readonly string name;
    public C(string name) { this.name = name; }
    public override bool Equals(Object other) {
        C c = other as C;
        if(c == null) return false;
        return name.Equals(c.name); 
    }
}

class App {
    static String Foo() { return "ola"; }
    static void Bar(Predicate p) {}
    static void Main() {
        C obj = new C("ABC"); 
        // StringSupplier f1 = () => obj.ToString(); // Anonymous method, Lambda
        StringSupplier f2 = obj.ToString; // Method Reference <=> Java> obj::ToString
        StringSupplier f3 = new StringSupplier(obj.ToString);
        StringSupplier f4 = App.Foo; // ldnull + ldftn App:: Foo + newobj StringSupplier

        Bar(obj.Equals); // ldloc0 + ldvirtftn C::Equals + newobj Predicate + call App::Bar

        string s1 = f2.Invoke();
        string s2 = f2(); // <=> f2.Invoke()
    }
}
