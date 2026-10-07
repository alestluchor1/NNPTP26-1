using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1
{
    public class NewtonFractal
    {
        private const int maxIterations = 30;
        private const double differenceThreshold = 0.5;
        private const double rootDistanceThreshold = 0.01;

        private const double zeroReplacement = 0.0001;
        private Bitmap bmp;
        private int imageWidth;
        private int imageHeight;
        private double xmin;
        private double xmax;
        private double ymin;
        private double ymax;
        private double xstep;
        private double ystep;
        private List<ComplexNumber> roots;
        private Polynomial polynomial;
        private Polynomial polynomialDerivate;
        private Color[] colors;
        private string output;

        public void Generate(int[] imageSize, double[] boundaries, string output)
        {
            InitializeCalculation(imageSize, boundaries, output);

            Console.WriteLine(polynomial);
            Console.WriteLine(polynomialDerivate);

            CalculatePixels();

            bmp.Save(this.output ?? "../../../out.png");
        }

        private void CalculatePixels()
        {
            for (int i = 0; i < imageWidth; i++)
            {
                for (int j = 0; j < imageHeight; j++)
                {
                    double y = ymin + i * ystep;
                    double x = xmin + j * xstep;

                    ComplexNumber currentPoint = CreatePoint(x, y);

                    int iterationCount;

                    currentPoint = CalculateNewtonIterations(currentPoint, polynomial, polynomialDerivate, out iterationCount);
                    int rootIndex = FindRoot(currentPoint, roots);
                    var color = PixelColor(colors, rootIndex, iterationCount);
                    bmp.SetPixel(j, i, color);
                }
            }
        }

        private void InitializeCalculation(int[] imageSize, double[] boundaries, string output)
        {
            imageWidth = imageSize[0];
            imageHeight = imageSize[1];
            xmin = boundaries[0];
            xmax = boundaries[1];
            ymin = boundaries[2];
            ymax = boundaries[3];

            xstep = (xmax - xmin) / imageWidth;
            ystep = (ymax - ymin) / imageHeight;

            this.output = output;

            bmp = new Bitmap(imageWidth, imageHeight);
            roots = new List<ComplexNumber>();

            polynomial = CreatePolynomial();
            polynomialDerivate = polynomial.Derive();
            colors = CreateColors();
        }

        private static Polynomial CreatePolynomial() 
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });

            return polynomial;
        }

        private static Color[] CreateColors()
        {
            var colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow,
                Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            return colors;
        }

        private static ComplexNumber CreatePoint(double x, double y)
        {
            ComplexNumber currentPoint = new ComplexNumber()
            {
                Real = x,
                Imaginary = y
            };

            if (currentPoint.Real == 0)
                currentPoint.Real = zeroReplacement;

            if (currentPoint.Imaginary == 0)
                currentPoint.Imaginary = zeroReplacement;

            return currentPoint;
        }

        private static ComplexNumber CalculateNewtonIterations(ComplexNumber currentPoint, Polynomial polynomial, Polynomial polynomialDerivate, out int iterationCount)
        {
            iterationCount = 0;

            for (int i = 0; i < maxIterations; i++)
            {
                var currentPointDifference = polynomial.Evaluate(currentPoint).Divide(polynomialDerivate.Evaluate(currentPoint));
                currentPoint = currentPoint.Subtract(currentPointDifference);

                if (Math.Pow(currentPointDifference.Real, 2) + Math.Pow(currentPointDifference.Imaginary, 2) >= differenceThreshold)
                {
                    i--;
                }

                iterationCount++;
            }
            return currentPoint;
        }

        private static int FindRoot(ComplexNumber currentPoint, List<ComplexNumber> roots)
        {
            bool isRootFound = false;
            int rootIndex = 0;

            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(currentPoint.Real - roots[i].Real, 2) + Math.Pow(currentPoint.Imaginary - roots[i].Imaginary, 2) <= rootDistanceThreshold)
                {
                    isRootFound = true;
                    rootIndex = i;
                }
            }
            if (!isRootFound)
            {
                roots.Add(currentPoint);
                rootIndex = roots.Count;
            }
            return rootIndex;
        }

        private static Color PixelColor(Color[] colors, int rootIndex, int iterationCount)
        {
            var color = colors[rootIndex % colors.Length];
            color = Color.FromArgb(color.R, color.G, color.B);
            color = Color.FromArgb(Math.Min(Math.Max(0, color.R - (int)iterationCount * 2), 255), Math.Min(Math.Max(0, color.G - (int)iterationCount * 2), 255), Math.Min(Math.Max(0, color.B - (int)iterationCount * 2), 255));

            return color;
        }

    }
}
