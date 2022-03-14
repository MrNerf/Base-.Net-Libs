using System;

namespace _09_SimpleSerialize
{
    [Serializable]
    public class Car
    {
        public Radio TheRadio = new Radio();
        public bool IsHatchBack;
    }
}
