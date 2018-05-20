using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace _00.SliceFile
{
    public class StartUp
    {
        private const int BufferSize = 4096;
        public static void Main()
        {
            SliceAsync();

            Console.WriteLine("Anything else?");
        }

        private static void SliceAsync()
        {
            string fileName = Console.ReadLine();
            string destinationFolder = Console.ReadLine();
            int pieces = int.Parse(Console.ReadLine());

            Task.Run(() => Slice(fileName, destinationFolder, pieces));

            Console.WriteLine("Slice complete");
        }

        private static void Slice(string fileName, string destinationFolder, int pieces)
        {
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                var fileInfo = new FileInfo(fileName);
                long partLenght = (stream.Length / pieces) + 1;
                long currentByte = 0;

                for (int currentPath = 1; currentPath <= pieces; currentPath++)
                {
                    string filePath = $"{destinationFolder}/Part-{currentPath}{fileInfo.Extension}";

                    using (var destiantion = new FileStream(filePath, FileMode.Create))
                    {
                        var buffer = new byte[BufferSize];

                        while (currentByte <= partLenght * currentPath)
                        {
                            int readBytesCount = stream.Read(buffer, 0, buffer.Length);

                            if (readBytesCount == 0)
                            {
                                break;
                            }

                            destiantion.Write(buffer, 0, buffer.Length);

                            currentByte += readBytesCount;
                        }
                    }
                }

            }
        }
    }
}
