using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace DotNetConfigurationFromDatabase
{
    public class ConfigurationSectionBuilder : IConfigurationSectionBuilder
    {
        public TSection BuildSection<TSection>(string xml) where TSection : System.Configuration.ConfigurationSection
        {
            var myEncoder = new System.Text.ASCIIEncoding();
            byte[] bytes = myEncoder.GetBytes(xml);
            var ms = new MemoryStream(bytes);
            var xmlReader = XmlReader.Create(ms);

            var section = Activator.CreateInstance(typeof(TSection)) as TSection;
            var deserializeMethod = Private.Method<TSection>("DeserializeSection");
            try
            {
                deserializeMethod.Invoke(section, new object[] { xmlReader });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
            return section;
        }
    }
}
