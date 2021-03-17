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
    /// 检查作业信息。
    /// </summary>
    public class CheckJobInfo
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
        /// 标识有效性。
        /// </summary>
        public bool IdValidity { get; set; }

        /// <summary>
        /// 名称有效性。
        /// </summary>
        public bool NameValidity { get; set; }


        /// <summary>
        /// 是否有效。
        /// </summary>
        /// <param name="options">给定的 <see cref="JobOptions"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool IsValid(JobOptions? options)
        {
            var validity = options?.CheckValidity?.ToLower();

            if (string.IsNullOrEmpty(validity)
                || validity.Contains('/')
                || validity == "or")
            {
                // 默认为“or”，表示二者其中之一有效
                return IdValidity || NameValidity;
            }

            return validity switch
            {
                "both" => IdValidity && NameValidity,
                "id" => IdValidity,
                "name" => NameValidity,
                _ => IdValidity || NameValidity
            };
        }

    }
}
