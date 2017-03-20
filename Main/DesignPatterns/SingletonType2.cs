using System;

namespace SingletonPattern
{
    public class SingletonType2
    {
        private static readonly object mutex = new object();
        private static SingletonType2 instance;

        private SingletonType2()
        {
            //Stuff that must only happen once.
        }

        public static SingletonType2 Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (mutex)
                    {
                        if (instance == null)
                        {
                            instance = new SingletonType2();
                        }
                    }
                }
                return instance;
            }
        }

        //
        public void DoSomething()
        {
            Console.WriteLine("Doing Something");
        }
    }
}