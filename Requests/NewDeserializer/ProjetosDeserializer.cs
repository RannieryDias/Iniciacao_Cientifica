using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests.NewDeserializer
{
    public class ProjetosDeserializer : DeserializerBase
    {

    }

    public static class ProjetosDeserializerExtension
    {
        public static ProjetosDeserializer GenerateProjetosDeserializer(this DeserializerBase deserializer)
        {
            return new ProjetosDeserializer();
        }
    }
}
