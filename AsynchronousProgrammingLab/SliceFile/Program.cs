﻿namespace SliceFile
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main()
        {
            string videoPath = "../../../" + Console.ReadLine();
            string destination = "../../../" + Console.ReadLine();
            int pieces = int.Parse(Console.ReadLine());

            SliceAsync(videoPath, destination, pieces);

            Console.WriteLine("Anything else?");

            while (true)
            {
                Console.ReadLine();
            }
        }

        private static void SliceAsync(string videoPath, string destination, int parts)
        {
            Task.Run(() => Slice(videoPath, destination, parts));
        }

        private static void Slice(string sourceFile, string destinationPath, int parts)
        {
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            using (var source = new FileStream(sourceFile, FileMode.Open))
            {
                FileInfo fileInfo = new FileInfo(sourceFile);

                long partLength = source.Length / parts + 1;
                long currentByte = 0;

                for (int currentPart = 1; currentPart < parts; currentPart++)
                {
                    string filePath = string.Format($"{destinationPath}/Part-{currentPart}{fileInfo.Extension}");
                    using (var destination = new FileStream(filePath, FileMode.Create))
                    {
                        byte[] buffer = new byte[1024];
                        while (currentByte <= partLength * currentPart)
                        {
                            int readBytesCount = source.Read(buffer, 0, buffer.Length);
                            if (readBytesCount == 0)
                            {
                                break;
                            }

                            destination.Write(buffer, 0, buffer.Length);
                            currentByte += readBytesCount;
                        }
                    }

                }

                Console.WriteLine("Slice complete.");
            }
        }
    }
}
