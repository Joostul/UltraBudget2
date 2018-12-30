using Newtonsoft.Json;
using System.IO;
using System.Text;
using UltraBudget2.Models;

namespace UltraBudget2.Extensions
{
    public static class BudgetDaoExtensions
    {
        public static byte[] ToByteArray(this BudgetDao dao)
        {
            if (dao == null)
                return null;
            TextWriter textWriter = new StringWriter();
            JsonSerializer jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(textWriter, dao);

            var encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(textWriter.ToString());

            return bytes;
        }
    }
}
