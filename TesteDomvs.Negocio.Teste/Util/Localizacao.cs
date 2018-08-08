using TesteDomvs.Negocio.Util;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TesteDomvs.Negocio.Util.Tests
{
    [TestClass()]
    public class Localizacao
    {
        [TestMethod()]
        public void IsLatitudeTest()
        {
            var localizacao = new TesteDomvs.Negocio.Util.Localizacao();
            Assert.IsTrue(localizacao.IsLatitude(90));
        }

        [TestMethod()]
        public void IsLongitudeTest()
        {
            var localizacao = new TesteDomvs.Negocio.Util.Localizacao();
            Assert.IsTrue(localizacao.IsLongitude(90));
        }
    }
}
