using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.CommunicationLayer
{
    class TypeNameConverter : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            typeName = typeName.Replace("MyOldNamespace", "MyNewNamespace");
            assemblyName = assemblyName.Replace("MyOldNamespace", "MyNewNamespace");
            return Type.GetType(string.Format("{0}, {1}", typeName, assemblyName));
        }
    }
}
