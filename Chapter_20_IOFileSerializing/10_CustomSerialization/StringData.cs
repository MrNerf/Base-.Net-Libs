using System;
using System.Runtime.Serialization;

namespace _10_CustomSerialization
{
    [Serializable]
    public class StringData : ISerializable
    {
        private string _dataItemOne = "Первый блок данных";

        private string _dataItemTwo = "Второй блок данных";

        public StringData() { }

        private StringData(SerializationInfo serializationInfo, StreamingContext context)
        {
            //Восстановить формат данных из потока
            _dataItemOne = serializationInfo.GetString("First_Item").ToLower();
            _dataItemTwo = serializationInfo.GetString("second_item").ToLower();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("First_Item", _dataItemOne.ToUpper());
            info.AddValue("second_item", _dataItemTwo.ToUpper());
        }
    }
}
