using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber number = obj as ComplexNumber;
                return number.Real == Real && number.Imaginary == Imaginary;
            }
            return base.Equals(obj);
        }
  
        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            Real = 0,
            Imaginary = 0
        };

        public ComplexNumber Multiply(ComplexNumber number)
        {
            return new ComplexNumber()
            {
                Real = Real * number.Real - Imaginary * number.Imaginary,
                Imaginary = Real * number.Imaginary + Imaginary * number.Real
            };
        }

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        public ComplexNumber Add(ComplexNumber number)
        {
            return new ComplexNumber()
            {
                Real = Real + number.Real,
                Imaginary = Imaginary + number.Imaginary
            };
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(Imaginary / Real);
        }

        public ComplexNumber Subtract(ComplexNumber number)
        {
            return new ComplexNumber()
            {
                Real = Real - number.Real,
                Imaginary = Imaginary - number.Imaginary
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }

        internal ComplexNumber Divide(ComplexNumber number)
        {
            var numerator = this.Multiply(new ComplexNumber() { Real = number.Real, Imaginary = -number.Imaginary });
            var denominator = number.Real * number.Real + number.Imaginary * number.Imaginary;

            return new ComplexNumber()
            {
                Real = numerator.Real / denominator,
                Imaginary = numerator.Imaginary / denominator
            };
        }
    }
}
