/*****************************************************************************************
Author: Rahul Vats
Aim: To Demonstrate the application of Singleton Pattern
Note: Singleton: A Class which only allows a single instance.
    Example: 
        C# System.Math
        AngularJS: RootScope
Revision History:
Date        Author      Desc
03/17/2017  Rahul Vats  Initial Creation
 * *****************************************************************************************/

using System;

namespace SingletonPattern
{
    public static class SingletonType1
    {
        public static void DoSomething()
        {
            Console.WriteLine("Doing Something");
        }
    }
}
