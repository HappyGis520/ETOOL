using System;

namespace EllaMakerTool
{
    /// <summary>
    ///     C#的62进制实现
    /// </summary>
    public class Number62
    {
        private const string StringConst = "dF9VUkDyqgP7zEno4OY3QbNc2Rrs80AfuS5BJICxlHej6Wh1iaKXZTLpGvMwtm";

        #region 字符数字相互转换

        private char Convert(int val)
        {
            return StringConst[val];
        }

        private int Convert(char c)
        {
            return StringConst.IndexOf(c);
        }

        #endregion

        /// <summary>
        ///     62进制数
        /// </summary>
        private readonly string _value;

        /// <summary>
        ///     对应10进制长整数
        /// </summary>
        private readonly Int64 _valueLong;

        /// <summary>
        ///     62进制数向10进制转换
        /// </summary>
        /// <param name="value"></param>
        public Number62(string value)
        {
            _value = value;

            int length = value.Length;
            Int64 result = 0;
            for (int i = 0; i < length; i++)
            {
                var val = (Int64)Math.Pow(62, (length - i - 1));
                char c = value[i];
                Int64 tmp = Convert(c);
                result += tmp * val;
            }
            _valueLong = result;
        }

        /// <summary>
        ///     10进制向62进制转换
        /// </summary>
        /// <param name="valueLong"></param>
        public Number62(Int64 valueLong)
        {
            _valueLong = valueLong;
            if (valueLong == 0) //补充
            {
                _value = "0";
                return;
            }
            Int64 result = valueLong;
            while (result > 0)
            {
                Int64 val = result % 62;
                _value = Convert((int)val) + _value;
                result = result / 62;
            }
        }

        /// <summary>
        ///     输出62进制
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _value;
        }

        /// <summary>
        ///     输出10进制
        /// </summary>
        /// <returns></returns>
        public Int64 ToInt64()
        {
            return _valueLong;
        }

        public Int32 ToInt32()
        {
            return (int)_valueLong;
        }

        public static implicit operator Number62(string value)
        {
            return new Number62(value);
        }

        public static implicit operator Number62(Int64 value)
        {
            return new Number62(value);
        }
    }
}
