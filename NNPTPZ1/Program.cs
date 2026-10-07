
namespace NNPTPZ1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] imageSize = new int[2];
            const int imageSizeArgumentCount = 2;

            for (int i = 0; i < imageSize.Length; i++)
            {
                imageSize[i] = int.Parse(args[i]);
            }
            double[] boundaries = new double[4];
            for (int i = 0; i < boundaries.Length; i++)
            {
                boundaries[i] = double.Parse(args[i + imageSizeArgumentCount]);
            }
            string output = args[6];

            NewtonFractal fractal = new NewtonFractal();
            fractal.Generate(imageSize, boundaries, output);

        }
    }
}
