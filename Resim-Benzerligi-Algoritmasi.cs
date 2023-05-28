using System;
using System.Collections.Generic;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ImageSimilarity
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] imageFiles = Directory.GetFiles(@"C:\Users\Kadir\source\repos\AlgoKs2\AlgoKs2\Images");// Görüntülerin olduğu dizini belirtin
            // Görüntülerin olduğu dizini belirtin

            List<Image<Bgr, byte>> images = new List<Image<Bgr, byte>>();
            Dictionary<int, Image<Gray, float>> processedImages = new Dictionary<int, Image<Gray, float>>(); // Önceden işlenmiş görüntüleri depolamak için sözlük

            for (int i = 0; i < imageFiles.Length; i++)
            {
                images.Add(new Image<Bgr, byte>(imageFiles[i]));

                // Görüntüyü ön işleme ve önbelleğe alma
                Image<Gray, byte> grayImage = images[i].Convert<Gray, byte>();
                Image<Gray, float> processedImage = grayImage.MatchTemplate(grayImage, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);
                processedImages.Add(i, processedImage);
            }

            List<Tuple<string, string, double>> similarityList = FindMostSimilarImages(images, processedImages);

            foreach (var similarity in similarityList)
            {
                Console.WriteLine("Image 1: " + similarity.Item1);
                Console.WriteLine("Image 2: " + similarity.Item2);
                Console.WriteLine("Similarity: " + similarity.Item3);
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        static List<Tuple<string, string, double>> FindMostSimilarImages(List<Image<Bgr, byte>> images, Dictionary<int, Image<Gray, float>> processedImages)
        {
            List<Tuple<string, string, double>> similarityList = new List<Tuple<string, string, double>>();

            for (int i = 0; i < images.Count; i++)
            {
                for (int j = i + 1; j < images.Count; j++)
                {
                    double similarity = CalculateSimilarity(processedImages[i], processedImages[j]);
                    similarityList.Add(new Tuple<string, string, double>(i.ToString(), j.ToString(), similarity));
                }
            }

            similarityList.Sort((x, y) => y.Item3.CompareTo(x.Item3)); // Benzerlik oranına göre listeyi sıralar

            return similarityList;
        }

        static double CalculateSimilarity(Image<Gray, float> processedImage1, Image<Gray, float> processedImage2)
        {
            processedImage1.MinMax(out _, out double[] maxValues1, out _, out _);
            processedImage2.MinMax(out _, out double[] maxValues2, out _, out _);

            return Math.Max(maxValues1[0], maxValues2[0]);
        }
    }
}
