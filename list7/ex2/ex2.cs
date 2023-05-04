using System;

namespace ex2
{
    class Integer
    {
        private int _value;
        private uint _freeAccessCount;
        private uint _getAccessCount;
        private uint _setAccessCount;

        public int Value
        {
            get
            {
                ++_getAccessCount;
                if (_getAccessCount > _freeAccessCount)
                {
                    Console.WriteLine("Get access denied.");
                    return -1;
                }
                return _value;
            }
            set
            {
                ++_setAccessCount;
                if (_setAccessCount <= _freeAccessCount)
                {
                    _value = value;
                }
                else
                {
                    Console.WriteLine("Set access denied.");
                }
            }
        }

        public Integer(int n, uint freeAccessCount)
        {
            _freeAccessCount = freeAccessCount;
            Value = n;
            _getAccessCount = _setAccessCount = 0;
        }

        public void Reset()
        {
            _getAccessCount = _setAccessCount = 0;
        }

        public void PrintState()
        {
            Console.WriteLine("Accesses count, get/set: {0}/{1}", _getAccessCount, _setAccessCount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Integer i = new Integer(5, 2);
            Console.WriteLine(i.Value);
            Console.WriteLine(i.Value);
            Console.WriteLine(i.Value);
            i.PrintState();
            Console.WriteLine("Reset");
            i.Reset();
            i.PrintState();
            i.Value = 10;
            Console.WriteLine(i.Value);
            i.Value = 100;
            i.Value = 1000;
            i.PrintState();
            Console.WriteLine(i.Value);
            Console.WriteLine(i.Value);
            i.PrintState();
        }
    }
}