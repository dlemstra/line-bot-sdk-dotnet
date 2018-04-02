using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests.Extensions
{
    public partial class StreamExtensionsTests
    {
        [TestClass]
        public class ToArrayMethod
        {
            private static readonly byte[] Utf8Preamable = Encoding.UTF8.GetPreamble();

            [TestMethod]
            public async Task ShouldReturnNullWhenStreamIsEmpty()
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    byte[] bytes = await memStream.ToArrayAsync();
                    Assert.IsNull(bytes);
                }
            }

            [TestMethod]
            public async Task ShouldReturnArrayWhenStreamStartsWithPreamable()
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    memStream.Write(Utf8Preamable, 0, 2);
                    memStream.Position = 0;

                    byte[] bytes = await memStream.ToArrayAsync();
                    Assert.IsNotNull(bytes);
                    Assert.AreEqual(2, bytes.Length);
                    Assert.AreEqual(Utf8Preamable[0], bytes[0]);
                    Assert.AreEqual(Utf8Preamable[1], bytes[1]);
                }
            }

            [TestMethod]
            public async Task ShouldReturnEmptyArrayWhenStreamOnlyContainsPreamable()
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    memStream.Write(Utf8Preamable, 0, Utf8Preamable.Length);
                    memStream.Position = 0;

                    byte[] bytes = await memStream.ToArrayAsync();
                    Assert.IsNotNull(bytes);
                    Assert.AreEqual(0, bytes.Length);
                }
            }

            [TestMethod]
            public async Task ShouldReturnArrayWhenStreamContainsData()
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    byte[] data = new byte[4] { 68, 73, 82, 75 };
                    memStream.Write(data, 0, data.Length);
                    memStream.Position = 0;

                    byte[] bytes = await memStream.ToArrayAsync();
                    Assert.IsNotNull(bytes);
                    Assert.AreEqual(4, bytes.Length);
                }
            }

            [TestMethod]
            public async Task ShouldReturnArrayWhenStreamContains9000Bytes()
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    byte[] data = Enumerable.Repeat((byte)68, 9000).ToArray();
                    memStream.Write(data, 0, data.Length);
                    memStream.Position = 0;

                    byte[] bytes = await memStream.ToArrayAsync();
                    Assert.IsNotNull(bytes);
                    Assert.AreEqual(9000, bytes.Length);
                }
            }
        }
    }
}
