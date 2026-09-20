using AutoFixture;
using Proposicao = Requests.DTO.Proposicao.Dado;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Fixture fixture = new Fixture();

            Proposicao proposicao = fixture.Create<Proposicao>();

            Assert.IsNotNull(proposicao);
        }
    }
}