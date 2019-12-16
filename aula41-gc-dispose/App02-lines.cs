using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

static class App {

    class LinesIter : IEnumerable, IEnumerator, IDisposable {
        StreamReader file;
        String line;
        public LinesIter(StreamReader file) {
            this.file = file;
        }
        public IEnumerator GetEnumerator() { return this; }
        public bool MoveNext() {
            line = file.ReadLine();
            return line != null;
        }
        public object Current {
            get { return line; }
        }
        public void Reset() {
            throw new Exception("Reset not supported!");
        }
        public void Dispose() {
            if(file != null) {
                file.Dispose();
                file = null;
            }
        }
    } 

    static IEnumerable Lines(string path)
    {
        // using(StreamReader file = new StreamReader(path))  {
            return new LinesIter(new StreamReader(path));
        //}
        /*
        using(StreamReader file = new StreamReader(path)) // <=> try-with resources do Java >= 7
        {
            string line;
            while ((line = file.ReadLine()) != null)
            {
                yield return line;
            }
        }
        */
        // ??? files.Dispose();
    }
     
    
    static void Main()
    {
        foreach(object l in Lines("i41N.txt"))
            Console.WriteLine(l);
        Console.ReadLine();

    }
}
