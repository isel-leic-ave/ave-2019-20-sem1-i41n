using System;

class App
{
    interface IPrinter {
        void Print();
    }

    class Student {
        int nr; String name;
    }
    struct Point : IPrinter{
        public int x, y;
        public Point(int x, int y) {
            this.x = x; 
            this.y = y;
        }
        public void Print() {
            Console.WriteLine("{0}, {1}", x, y);
        }
    }
    
    
    public static void Main(String[] args)
    {   
        Student s = new Student();  // instanciar  => newobj
        Point p1 = new Point();     // inicializar => initobj
        Point p2 = new Point(5, 7); // call 
        
        Object o = p1; // box
        IPrinter i = p1; // box
        
        // ((Point) o).x = 11; // ERRO de compilação
        Point p3 = (Point) o;
        p3.x  = 11;
        ((IPrinter) o).Print(); // castclass + callvirt Print => 0, 0
        o = SetPointX(o, 17);
        ((IPrinter) o).Print(); // castclass + callvirt Print => 17, 0
        p3 = (Point) SetPointX(p3, 23); // box: p3 -> object + unbox
        p3.Print();
    }
    
    static object SetPointX(Object target, int nr) {
        Point p = (Point) target; // unbox
        p.x  = nr; // !!!! target permanece inalterado!!!!
        return p; // box
    }
}