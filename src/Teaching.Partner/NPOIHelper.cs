using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Teaching.Partner
{
    public class NPOIHelper
    {
        public static void ExportExcel(List<SubjectScoreOptions> scores, string fileName,
            string sheetName = "Sheet1")
        {
            if (scores == null)
                throw new ArgumentNullException(nameof(scores));

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();

                ISheet sheet = workbook.CreateSheet(sheetName);

                sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 10));

                var header = sheet.CreateRow(0);

                header.CreateCell(0).SetCellValue("序号");
                //sheet.AutoSizeColumn(0);

                header.CreateCell(1).SetCellValue("学号");
                //sheet.AutoSizeColumn(1);

                header.CreateCell(2).SetCellValue("姓名");
                //sheet.AutoSizeColumn(2);

                header.CreateCell(3).SetCellValue("分级");
                //sheet.AutoSizeColumn(3);

                header.CreateCell(4).SetCellValue("平时成绩");
                //sheet.AutoSizeColumn(4);

                header.CreateCell(5).SetCellValue("期末成绩");
                //sheet.AutoSizeColumn(5);

                for (var i = 0; i < scores.Count; i++)
                {
                    var score = scores[i];
                    var row = sheet.CreateRow(i + 1);

                    // 序号
                    row.CreateCell(0).SetCellValue(i + 1);
                    //sheet.AutoSizeColumn(0);

                    // 学号
                    row.CreateCell(1).SetCellValue(score.Id);
                    //sheet.AutoSizeColumn(1);

                    // 姓名
                    row.CreateCell(2).SetCellValue(score.Name);
                    //sheet.AutoSizeColumn(2);

                    // 分级
                    row.CreateCell(3).SetCellValue(score.Level.ToString());
                    //sheet.AutoSizeColumn(3);

                    // 平时成绩
                    row.CreateCell(4).SetCellValue(score.NormalScore);
                    //sheet.AutoSizeColumn(4);

                    // 期末成绩
                    row.CreateCell(5).SetCellValue(score.FinalScore);
                    //sheet.AutoSizeColumn(5);
                }

                workbook.Write(fs);
            }
        }

        public static void ExportCSV(List<SubjectScoreOptions> scores, string fileName)
        {
            if (scores == null)
                throw new ArgumentNullException(nameof(scores));

            var lines = scores.Select(s => $"{s.Id},{s.Name},{s.Level},{s.NormalScore},{s.FinalScore}").ToList();
            lines.Insert(0, "学号,姓名,分级,平时成绩,期末成绩");

            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }

    }
}
