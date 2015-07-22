using System;

namespace CSharpLearning
{   
    class RefTest
    {   
        // Unlike Java, c# provides the possibility of passing by reference. Here are some methods
        // to investigate the differences between passing by value and passing by reference

        static void addExclamationRef(ref string s)
        {
            s = s + "!";
        }

        static void addExclamationValue(string s)
        {
            s = s + "!";
        }

        static void zeroA(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = 0;
            }
        }

        static void zeroB(int[] nums)
        {
            int[] nums2 = {1 , 2 ,3};
            nums = nums2;

            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = 0;
            }
        }

        static void Main(string[] args)
        {
            string s = "Hi there";
            // s passed by reference so its reassignment in method is felt, ie s is altered.
            addExclamationRef(ref s);
            Console.WriteLine(s); // Hi there!
            
            s = "Hi there";
            // s passed by value, so reference is copied, then the copy is reassigned. Hence s is unaltered.
            addExclamationValue(s);
            Console.WriteLine(s); // Hi there

            int[] nums = { 1, 2, 3 };
            
            // Reference is copied by method, alterations to the memory pointed to by reference are felt. 
            zeroA(nums);
            Console.WriteLine(nums[0]); // 0
            
            
            nums = new int[]{ 1, 2, 3 };
            
            // In this method the copy of the reference is reassigned, then altered, so nums is
            // unchanged.
            zeroB(nums);
            Console.WriteLine(nums[0]); // 1

        }

    }
}
