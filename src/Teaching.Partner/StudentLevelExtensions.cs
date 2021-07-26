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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Teaching.Partner
{
    /// <summary>
    /// <see cref="StudentLevel"/> 静态扩展。
    /// </summary>
    public static class StudentLevelExtensions
    {
        private static readonly Dictionary<StudentLevel, StudentLevelInfo> _infos = InitInfos();

        private static Dictionary<StudentLevel, StudentLevelInfo> InitInfos()
        {
            var infos = new Dictionary<StudentLevel, StudentLevelInfo>();

            var fields = typeof(StudentLevel).GetFields(BindingFlags.Public | BindingFlags.Static);

            for (var i = 0; i < fields.Length; i++)
            {
                // 得到枚举项
                var level = (StudentLevel)fields[i].GetValue(null)!;

                var info = new StudentLevelInfo()
                {
                    Level = level,
                    EndValue = (int)level, // 默认常量值表示随机范围结束值
                };

                // 计算起始值
                if (infos.Count > 0)
                {
                    // 当前起始值等于前一项的随机范围结束值加1
                    info.StartValue = infos.Values.Last().EndValue + 1;
                }

                infos.Add(level, info);
            }

            return infos;
        }


        /// <summary>
        /// 生成科目成绩列表。
        /// </summary>
        /// <param name="students">给定的 <see cref="StudentOptions"/> 列表。</param>
        /// <param name="difficulty">给定的难度系数（可选）。</param>
        /// <returns>返回 <see cref="SubjectScoreOptions"/> 列表。</returns>
        public static List<SubjectScoreOptions> GenerateScores(this List<StudentOptions> students,
            double difficulty = 100)
        {
            if (students == null)
                throw new ArgumentNullException(nameof(students));

            return students.Select(s => s.GenerateScore(difficulty)).ToList();
        }

        /// <summary>
        /// 生成科目成绩。
        /// </summary>
        /// <param name="student">给定的 <see cref="StudentOptions"/>。</param>
        /// <param name="difficulty">给定的难度系数（可选）。</param>
        /// <returns>返回 <see cref="SubjectScoreOptions"/>。</returns>
        public static SubjectScoreOptions GenerateScore(this StudentOptions student,
            double difficulty = 100)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            if (_infos == null)
                throw new ArgumentNullException(nameof(_infos));

            var info = _infos[student.Level];

            return new SubjectScoreOptions()
            {
                Id = student.Id,
                Name = student.Name,
                Level = student.Level,
                NormalScore = info.ComputeCurrentValue(difficulty),
                FinalScore = info.ComputeCurrentValue(difficulty)
            };
        }


        /// <summary>
        /// 刷新成绩列表。
        /// </summary>
        /// <param name="scores">给定的 <see cref="SubjectScoreOptions"/> 列表。</param>
        /// <param name="difficulty">给定的难度系数（可选）。</param>
        /// <returns>返回 <see cref="SubjectScoreOptions"/> 列表。</returns>
        public static List<SubjectScoreOptions> RefreshScores(this List<SubjectScoreOptions> scores,
            double difficulty = 100)
        {
            if (scores == null)
                throw new ArgumentNullException(nameof(scores));

            foreach (var s in scores)
            {
                s.RefreshScore(difficulty);
            }

            return scores;
        }

        /// <summary>
        /// 刷新成绩。
        /// </summary>
        /// <param name="options">给定的 <see cref="SubjectScoreOptions"/>。</param>
        /// <param name="difficulty">给定的难度系数（可选）。</param>
        /// <returns>返回 <see cref="SubjectScoreOptions"/>。</returns>
        public static SubjectScoreOptions RefreshScore(this SubjectScoreOptions options,
            double difficulty = 100)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var info = _infos[options.Level];

            options.NormalScore = GetDifferentScore(options.NormalScore);
            options.FinalScore = GetDifferentScore(options.FinalScore);

            return options;

            // 确保得到与当前不同的成绩
            int GetDifferentScore(int currentScore)
            {
                var newScore = info!.ComputeCurrentValue(difficulty);

                if (currentScore == newScore)
                    return GetDifferentScore(currentScore);

                return newScore;
            }
        }

    }
}
