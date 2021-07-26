#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Teaching.Partner
{
    /// <summary>
    /// <see cref="StudentLevel"/> 信息。
    /// </summary>
    public class StudentLevelInfo
    {
        /// <summary>
        /// 分级。
        /// </summary>
        public StudentLevel Level { get; set; }

        /// <summary>
        /// 起始值。
        /// </summary>
        public int StartValue { get; set; }

        /// <summary>
        /// 结束值。
        /// </summary>
        public int EndValue { get; set; }


        /// <summary>
        /// 计算当前值。
        /// </summary>
        /// <param name="difficulty">给定的难度系数（可选）。</param>
        /// <returns>返回整数值。</returns>
        public virtual int ComputeCurrentValue(double difficulty = 100)
        {
            if (EndValue <= 0)
                return 0;

            var r = new Random();
            var value = r.Next(StartValue, EndValue);

            if (difficulty < 100)
                value = (int)Math.Round(value * (difficulty / 100), 0);

            return value;
        }

    }
}
