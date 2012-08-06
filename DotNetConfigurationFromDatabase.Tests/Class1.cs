using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetConfigurationFromDatabase.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
        private IConfigurationSectionBuilder _configurationSectionBuilder;

        public ConfigurationTests()
        {
            _configurationSectionBuilder = new ConfigurationSectionBuilder();
        }

        [TestMethod]
        public void Can_build_configuration_with_default_value()
        {
            var result = _configurationSectionBuilder.BuildSection<TestConfiguration>("<config requiredProperty=\"required\"></config>");

            Assert.AreEqual("defaultValue", result.OptionalProperty);
        }

        [TestMethod]
        public void Can_build_configuration_with_default_value_set()
        {
            var result = _configurationSectionBuilder.BuildSection<TestConfiguration>("<config requiredProperty=\"required\" optionalProperty=\"setValue\"></config>");

            Assert.AreEqual("setValue", result.OptionalProperty);
        }

        [TestMethod]
        public void Building_configuration_without_required_poperty_throws_error()
        {
            try
            {
                _configurationSectionBuilder.BuildSection<TestConfiguration>(
                    "<config optionalProperty=\"setValue\"></config>");
            }catch(Exception ex)
            {
                Assert.IsNotNull(ex);
                return;
            }
        }

        public class TestConfiguration : ConfigurationSection
        {
            [ConfigurationProperty("optionalProperty", DefaultValue = "defaultValue")]
            public string OptionalProperty
            {
                get { return (string)base["optionalProperty"]; }
                set { base["optionalProperty"] = value; }
            }

            [ConfigurationProperty("requiredProperty", IsRequired = true)]
            public string RequiredProperty
            {
                get { return (string)base["requiredProperty"]; }
                set { base["requiredProperty"] = value; }
            }
        }
    }
}
