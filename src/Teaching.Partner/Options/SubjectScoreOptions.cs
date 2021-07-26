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
    /// 科目成绩选项。
    /// </summary>
    public class SubjectScoreOptions
    {
        /// <summary>
        /// 学号。
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 姓名。
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 分级。
        /// </summary>
        public StudentLevel Level { get; set; }

        /// <summary>
        /// 平时成绩。
        /// </summary>
        public int NormalScore { get; set; }

        /// <summary>
        /// 期末成绩。
        /// </summary>
        public int FinalScore { get; set; }
    }
}
