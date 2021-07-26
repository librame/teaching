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
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Teaching.Partner
{
    /// <summary>
    /// <see cref="JsonSerializer"/> 静态扩展。
    /// </summary>
    public static class JsonSerializerExtensions
    {
        private static readonly JsonSerializerOptions _options = InitOptions();

        private static JsonSerializerOptions InitOptions()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());

            return options;
        }


        #region ReadJson and WriteJson

        /// <summary>
        /// 读取 JSON。
        /// </summary>
        /// <typeparam name="T">指定的反序列化类型。</typeparam>
        /// <param name="filePath">给定的文件路径。</param>
        /// <param name="encoding">给定的 <see cref="Encoding"/>（可选）。</param>
        /// <returns>返回反序列化对象。</returns>
        public static T? ReadJson<T>(this string filePath, Encoding? encoding = null)
        {
            var json = File.ReadAllText(filePath, encoding ?? GlobalDefaults.CurrentEncoding);
            return JsonSerializer.Deserialize<T>(json, _options);
        }

        /// <summary>
        /// 读取 JSON。
        /// </summary>
        /// <param name="filePath">给定的文件路径。</param>
        /// <param name="type">给定的反序列化对象类型。</param>
        /// <param name="encoding">给定的 <see cref="Encoding"/>（可选）。</param>
        /// <returns>返回反序列化对象。</returns>
        public static object? ReadJson(this string filePath, Type type, Encoding? encoding = null)
        {
            var json = File.ReadAllText(filePath, encoding ?? GlobalDefaults.CurrentEncoding);
            return JsonSerializer.Deserialize(json, type, _options);
        }


        /// <summary>
        /// 写入 JSON。
        /// </summary>
        /// <param name="filePath">给定的文件路径。</param>
        /// <param name="value">给定的对象值。</param>
        /// <param name="encoding">给定的 <see cref="Encoding"/>（可选）。</param>
        /// <param name="autoCreateDirectory">自动创建目录（可选；默认启用）。</param>
        /// <returns>返回 JSON 字符串。</returns>
        public static string WriteJson(this string filePath, object value, Encoding? encoding = null,
            bool autoCreateDirectory = true)
        {
            var json = JsonSerializer.Serialize(value, _options);

            if (autoCreateDirectory)
                filePath.TryCreateDirectory();

            File.WriteAllText(filePath, json, encoding ?? GlobalDefaults.CurrentEncoding);
            return json;
        }

        #endregion

    }
}
