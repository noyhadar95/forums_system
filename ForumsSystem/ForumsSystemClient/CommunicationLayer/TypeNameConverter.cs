using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystemClient.CommunicationLayer
{
    class TypeNameConverter : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {

            typeName = typeName.Replace("ForumsSystem.Server", "ForumsSystemClient.Resources");
            assemblyName = assemblyName.Replace("ForumsSystem", "ForumsSystemClient");
            return Type.GetType(string.Format("{0}, {1}", typeName, assemblyName));
        }
    }
}

    