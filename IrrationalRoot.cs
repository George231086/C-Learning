using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpLearning
{
    ///<summary>
    /// A small program to make use of some functions that I'd written when solving Project Euler questions. The program asks
    /// for a positive number, then will give some information about its square root. For irrational roots
    /// it will give its continued fraction expansion, its decimal expansion and a rational approximation. 
    ///</summary>
    ///
    ///<remarks>
    /// It expects in32s as input. The program is mainly a bit of fun,
    ///</remarks>

    class IrrationalRoot
    {
        private int n;

        public IrrationalRoot(int n)
        {
            if (n <= 0 || Math.Sqrt(n) % 1 == 0)
            {
                throw new ArgumentException("Will not have real irrational root!");
            }
            this.n = n;
        }

        /// <summary>
        ///  Method to obtain the continued fraction expansion.
        /// </summary>
        /// 
        /// <returns>List of ints in expansion sqrt(n)=[a0;a1,a2,..,ar]</returns>
        /// 
        /// <remarks>
        /// At each step we have an integer and a number of the form N = a0/(sqrt(n)-b0)
        /// which we need to expand. We consider 1/N and rewrite it in the form gamma+(sqrt(n)-a1)/b1.
        /// where gamma = integer part of 1/N. Then setting N2=(sqrt(n)-a1)/b1 we repeat the process
        /// with 1/N2. I computed alpha and beta in terms of a0, bo, n and gamma, so at each step we need
        /// only need to compute gamma, ie taking an integer part, then a1 and b1 can be found by formulas.
        /// All the info we need is encoded in the gamma, a1 and b1 values.
        /// We store them in a list as a tuple, if they occur again we know the continued
        /// fraction sequence will repeat. We return the sequence of integer parts to get the expansion.
        /// </remarks>

        public List<int> getCFExpansion()
        {
            var ints = new List<int>();
            var a0 = 1;
            var b0 = (int)Math.Sqrt(n);
            ints.Add(b0);
            var nums = new List<Tuple<int, int, int>>();
            for (int i = 0; ; i++)
            {
                var num = a0 / (Math.Sqrt(n) - b0);
                var gamma = (int)num;
                var c0 = n - b0 * b0;
                // These formulas for a1 and b1 follow from straighforward algebra.
                var a1 = c0 / a0;
                var b1 = ((gamma * c0 - b0 * a0) * a1) / c0;
                var triple = new Tuple<int, int, int>(gamma, a1, b1);
                if (nums.Contains(triple))
                {
                    // We have seen this triple before, so routine will repeat.
                    return ints;
                }
                ints.Add(gamma);
                nums.Add(triple);
                // restart
                a0 = a1;
                b0 = b1;
            }
        }

        /// <summary>
        /// Print expansion in normal continued fraction notation, ie sqrt(n)=[a0;(a1,a2,..,ar)], () denotes repeating.
        /// </summary>

        public void printCFExpansion()
        {
            var expansion = getCFExpansion();
            var s = new StringBuilder("[" + expansion[0] + ";(");
            for (int i = 1; i < expansion.Count - 1; i++)
            {
                s.Append(expansion[i] + ",");
            }
            s.Append(expansion.Last() + ")]");
            Console.WriteLine(s);
        }


        /// <summary>
        /// Method to compute a rational approximation to sqrt(n) within 10^(decPlacesAccuracy+1);
        /// </summary>
        /// 
        /// <param name="decPlacesAccuracy"></param>
        /// 
        /// <returns> Numerator and denominator of convergent in array [numerator,donominator]</returns>
        /// 
        /// <remarks>We use the fact that truncated continued fractions of sqrt(n) are approximations of sqrt(n). 
        /// If h(m-1)/k(m-1) and h(m)/k(m) are the (m-1)th and mth convergent of sqrt(n), then
        /// h(m+1) is given by a(m+1)*h(m)+h(m-1) and k(m+1) is given by a(m+1)*k(m)+k(m-1), where a(m+1) is
        /// the (m+1)th term in the continued fraction expansion sqrt(n)=[a0;a1,a2,a3,..].
        /// A consequence is that |sqrt(n)-h(m)/k(m)|<1/(k(m+1)k(m)), so by making 1/(k(m+1)k(m)) small
        /// we ensure our convergent h(m)/k(m) is close to sqrt(n). </remarks>

        public BigInteger[] getConvergent(int decPlacesAccuracy)
        {
            // Setting up initial values.
            var contFractExp = getCFExpansion();
            var a0 = contFractExp[0];
            var a1 = contFractExp[1];
            contFractExp.RemoveAt(0);
            BigInteger hOld = a0;
            BigInteger hNew = a1 * a0 + 1;
            BigInteger kOld = 1;
            BigInteger kNew = a1;

            for (int i = 2; ; i++)
            {
                // Continued fraction expansion for irrational sqrts is periodic, so
                // if sqrt(n)=[a0;(a1,a2,..,ar)] then to get a(m) we compute
                // m mod r and select this element from [a1,a2,a3,..,ar].
                // Note we have removed a0 from contFractExp. Also adjustment for zero basing.
                var a = contFractExp[(i - 1) % contFractExp.Count()];
                BigInteger temph = hNew;
                hNew = a * hNew + hOld;
                hOld = temph;

                BigInteger tempk = kNew;
                kNew = a * kNew + kOld;
                kOld = tempk;
                // Check whether we are close enough. Equivalent to |sqrt(n)-h/k|<1/(k(m+1)k(m))<1/10^(dpa+1)=0.000..001
                if (kNew * kOld > BigInteger.Pow(10, decPlacesAccuracy + 1))
                {
                    break;
                }
            }
            // Return numerator and denominator of convergent, we want the exact expression.
            var convergent = new BigInteger[] { hOld, kOld };
            return convergent;
        }

        public void printConvergent(int decPlaces)
        {
            var convergent = getConvergent(decPlaces);
            Console.WriteLine(convergent[0] + "/" + convergent[1]);
        }

        /// <summary>
        /// Method to get decimal expansion within required accuracy.
        /// </summary>
        /// 
        /// <param name="decPlaces"> Accuracy </param>
        /// 
        /// <returns> String representation of expansion.</returns>
        /// 
        /// <remarks>
        /// We get a close enough convergent, then use long division to get the exact decimal expansion
        /// of this convergent up to the required number of decimal places.
        /// </remarks> 

        public string getDecimalExp(int decPlaces)
        {
            var convergent = getConvergent(decPlaces);
            var p = convergent[0];
            var q = convergent[1];
            var dec = "";
            var i = p / q;
            var r = p % q;
            dec += (i.ToString() + ".");
            var count = 0;
            while (count < decPlaces)
            {
                var r2 = r * 10;
                while (r2 / q == 0)
                {
                    r2 *= 10;
                    dec += "0";
                    count++;
                    if (count == decPlaces)
                        return dec;
                }
                i = r2 / q;
                r = r2 % q;
                dec += i.ToString();
                count++;
            }
            return dec;
        }

        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter a positive number, I'll tell you about its square root! type q to quit.");
                string input = Console.ReadLine();
                if (input == "q")
                    break;
                int num; 
                var isInt = Int32.TryParse(input, out num);
                if (isInt && num > 0)
                {
                    if (Math.Sqrt(num) % 1 == 0)
                    {
                        Console.WriteLine("{0} has square root {1}. It is a perfect square!", num, (int)Math.Sqrt(num));
                    }
                    else
                    {
                        Console.WriteLine("{0} has an irrational square root.", num);
                        var root = new IrrationalRoot(num);
                        Console.WriteLine("Its continued fraction expansion is periodic, it is given by");
                        root.printCFExpansion();
                        var decPlaces = 20;
                        Console.WriteLine("The square root of {0} correct to {1} decimal places is" +
                        "given by {2}", num, decPlaces, root.getDecimalExp(decPlaces));
                        Console.WriteLine("A rational approximation correct to at least {0} decimal places is given by", decPlaces);
                        root.printConvergent(decPlaces);
                    }
                }
                else
                    Console.WriteLine("Invalid input, either not a positive integer or too large!");
            }
        }
    }
}

