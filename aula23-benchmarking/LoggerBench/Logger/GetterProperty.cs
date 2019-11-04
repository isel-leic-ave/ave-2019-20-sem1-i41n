using System.Reflection;

namespace Logger
{
    public class GetterProperty : IGetter
    {
        PropertyInfo p;
        public GetterProperty(PropertyInfo p) { this.p = p; }
        public string GetName() { return p.Name; }
        public object GetValue(object target)
        {
            return p.GetValue(target);
        }
    }

}
