using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MicrosoftEdge以阅读模式打开网页
{
    class Program
    {
        private static readonly string BaseDirectory =
            AppDomain.CurrentDomain.BaseDirectory;

        private static async Task Main()
        {
            Console.Title = "MicrosoftEdge以阅读模式打开网页";
            string configFileName = await CheckConfigFile();
            string readAllTextAsync;
            try
            {
                readAllTextAsync =
                    await ReadMicrosoftEdgePathFromConfig(configFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            "输入网址".WriteLine();
            string readLine = Console.ReadLine();
            string s = ConvertUri(readLine);
            Process.Start(readAllTextAsync, s);
        }

        /// <summary>
        /// 转换URI
        /// </summary>
        /// <param name="sourceUri">需要以阅读模式打开的源URI</param>
        /// <returns>符合MicrosoftEdge阅读模式的URI</returns>
        private static string ConvertUri(string sourceUri)
        {
            var urlEncoder = UrlEncoder.Default;
            var uri = new Uri(sourceUri);
            string s = $@"read://{uri.Scheme}_{uri.Host}/?url={
                urlEncoder.Encode(sourceUri)}";
            return s;
        }

        /// <summary>
        /// 从配置文件中读取MicrosoftEdge浏览器的安装路径
        /// </summary>
        /// <param name="configFileName">配置文件路径</param>
        /// <returns>MicrosoftEdge浏览器安装的路径</returns>
        private static async Task<string> ReadMicrosoftEdgePathFromConfig(
            string configFileName)
        {
            string readAllTextAsync = await File.ReadAllTextAsync(configFileName);
            if (File.Exists(readAllTextAsync) == false)
            {
                await File.WriteAllTextAsync(configFileName, "MicrosoftEdge路径错误");
                Process.Start("explorer", readAllTextAsync);
                throw new Exception("MicrosoftEdge路径不正确");
            }

            return readAllTextAsync;
        }

        /// <summary>
        /// 检查配置文件是否存在,如果不存在则创建配置文件
        /// </summary>
        /// <returns>配置文件的路径</returns>
        private static async Task<string> CheckConfigFile()
        {
            string configFileName =
                Path.Combine(BaseDirectory, "MicrosoftEdgePath.Config");
            if (File.Exists(configFileName) == false)
                await File.WriteAllTextAsync(configFileName,
                    @"C:\Program Files (x86)\Microsoft\Edge Dev\Application\msedge.exe",
                    Encoding.UTF8);
            return configFileName;
        }
    }
}