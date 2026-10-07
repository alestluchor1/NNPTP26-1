using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynomial() => Coefficients = new List<ComplexNumber>();

        public void Add(ComplexNumber coefficient) =>
            Coefficients.Add(coefficient);

        public Polynomial Derive()
        {
            Polynomial polynomialDerivative = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                polynomialDerivative.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { Real = i }));
            }

            return polynomialDerivative;
        }

        public ComplexNumber Evaluate(double value)
        {
            return Evaluate(new ComplexNumber()
            {
                Real = value,
                Imaginary = 0
            });
        }

        public ComplexNumber Evaluate(ComplexNumber value)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int index = 0; index < Coefficients.Count; index++)
            {
                ComplexNumber coefficient = Coefficients[index];
                ComplexNumber currentX = value;

                if (index > 0)
                {
                    for (int j = 0; j < index - 1; j++)
                        currentX = currentX.Multiply(value);

                    coefficient = coefficient.Multiply(currentX);
                }
                result = result.Add(coefficient);
            }
            return result;
        }

        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < Coefficients.Count; i++)
            {
                result += Coefficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        result += "x";
                    }
                }
                if (i + 1 < Coefficients.Count)
                {
                    result += " + ";
                }
            }
            return result;
        }
    }
}
