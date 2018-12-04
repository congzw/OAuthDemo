using System;
// ReSharper disable once CheckNamespace

namespace Demos.Common
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 保证不是默认值
        /// </summary>
        /// <param name="value"></param>
        public static void ShouldNotDefault(this object value)
        {
            MakeSureIsNotDefault(value);
        }

        private static void MakeSureIsNotDefault<T>(this T instance)
        {
            var value = default(T);
            bool isEqual = Equals(instance, value);

            if (isEqual)
            {
                string exMessage = string.Format("值不能为:{0}", instance);
                exMessage = exMessage == "值不能为:" ? "值不能为:null" : exMessage;
                throw new ArgumentException(exMessage);
            }
        }
    }
}
