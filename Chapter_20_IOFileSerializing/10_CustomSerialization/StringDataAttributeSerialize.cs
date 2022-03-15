using System;
using System.Runtime.Serialization;

namespace _10_CustomSerialization
{
    [Serializable]
    public class StringDataAttributeSerialize
    {
        private string _dataItemOne = "Первый блок данных";

        private string _dataItemTwo = "Второй блок данных";

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            _dataItemOne = _dataItemOne.ToUpper();
            _dataItemTwo = _dataItemTwo.ToUpper();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            _dataItemOne = _dataItemOne.ToLower();
            _dataItemTwo = _dataItemTwo.ToLower();
        }

        public override string ToString() => $"{_dataItemOne}\n{_dataItemTwo}";

    }
}
