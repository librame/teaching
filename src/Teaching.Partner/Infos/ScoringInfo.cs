#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Teaching.Partner
{
    /// <summary>
    /// 计分信息。
    /// </summary>
    public class ScoringInfo
    {
        /// <summary>
        /// 标识。
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 分值。
        /// </summary>
        public int Score { get; set; }
    }
}
