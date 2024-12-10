using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace fn_intput_blob
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function(nameof(Function1))]
        [BlobOutput("output/{name}-output.txt", Connection = "live-dio")]
        public static string Run(
            [BlobTrigger("samples-workitems/{name}", Connection = "live-dio")] string myTriggerItem,
            [BlobInput("samples-workitems/{name}", Connection = "live-dio")] string myBlob,
            FunctionContext context)
        {
            var logger = context.GetLogger("BlobFunction");
            logger.LogInformation("Triggered Item = {myTriggerItem}", myTriggerItem);
            logger.LogInformation("Input Item = {myBlob}", myBlob);

            // Blob Output
            return "blob-output content";
        }


    }
}
