public class Tpc3 {
    static class A {}
    static class B extends A {}
    static class C extends B {
        public int x, y;
        public void Foo() {}
    }
    public static void main(String[] args) {
        PrintBaseTypes("Ola");
        PrintBaseTypes(19);
        PrintBaseTypes(new C());
        //PrintBaseTypes(new System.IO.DirectoryInfo("."));
        PrintMembers(new C());
        PrintMethods(new C());
        PrintFields(new C());
    }

    public static void PrintMembers(Object obj) {
        System.out.print("Members: ");
        for (var m : obj.getClass().getDeclaredMethods())
            System.out.print(m.getName()+" ");
        for (var m : obj.getClass().getDeclaredFields())
            System.out.print(m.getName()+" ");
        for (var m : obj.getClass().getDeclaredConstructors())
            System.out.print(m.getName()+" ");
        System.out.println();
    }

    public static void PrintMethods(Object obj) {
        System.out.print("Methods: ");
        for(var m:obj.getClass().getDeclaredMethods())
            System.out.print(m.getName()+" ");
        System.out.println();
    }

    public static void PrintFields(Object obj) {
        System.out.print("Fields: ");
        for(var m:obj.getClass().getDeclaredFields())
            System.out.print(m.getName()+" ");
        System.out.println();
    }

    public static void PrintBaseTypes(Object obj) {
        Class t = obj.getClass();
        do {
            System.out.print(t.getName() + " ");
            PrintInterfaces(t);
            t = t.getSuperclass();
            // } while( t != typeOfObject);
        } while (t != Object.class);
        System.out.println();
    }

    public static void PrintInterfaces(Class t) {
        System.out.print("Interfaces: ");
        for(var m : t.getGenericInterfaces())
            System.out.print(m.getTypeName()+" ");
        System.out.println();
    }
}