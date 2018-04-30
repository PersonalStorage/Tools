using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Helper
{
    /// <summary>
    /// Excel相關小工具
    /// </summary>
    public class Excel
    {
        private readonly string _filePath;
        private readonly IWorkbook _excel;

        public Excel(string fullFileName)
        {
            _filePath = fullFileName;
            _excel = Get(_filePath);
        }

        /// <summary>
        /// 根據路徑取得檔案
        /// </summary>
        /// <param name="fullFileName">完整檔案路徑</param>
        private IWorkbook Get(string fullFileName)
        {
            //先判斷是*.xls還是*.xlsx
            FileInfo info = new FileInfo(fullFileName);
            string extension = info.Extension;

            IWorkbook tempWorkBook = null;

            using (FileStream fs = new FileStream(fullFileName, FileMode.Open))
            {
                if (extension == ".xls")
                    tempWorkBook = new HSSFWorkbook(fs);
                else if (extension == ".xlsx")
                    tempWorkBook = new XSSFWorkbook(fs);
            }

            return tempWorkBook;
        }

        /// <summary>
        /// 把Excel列轉換成Model
        /// </summary>
        /// <typeparam name="TModel">泛型</typeparam>
        /// <param name="sheetName">要抓的Sheet的名字</param>
        /// <param name="columns">要抓的Column名稱</param>
        /// <param name="elements">對應Column的Model內容</param>
        /// <param name="headerRow">起始條件</param>
        public List<TModel> TransToModel<TModel>(string sheetName, string[] columns, string[] elements, int headerRow = 0)
                where TModel : class, new()
        {
            //先取得ISheet
            ISheet sheet = _excel.GetSheet(sheetName);

            //先判columns和elements的欄位數量對不對得上
            if (columns.Length != elements.Length)
                throw new ArgumentException("Please check paramemters, columns and elements, are mapping.");

            //取得Heaader 等下要比對用
            IRow header = sheet.GetRow(headerRow);

            List<TModel> response = new List<TModel>();

            //循列取資料
            for (int i = headerRow + 1; i <= sheet.LastRowNum; i++)
            {
                TModel model = new TModel();
                //把所有的element都撈出來
                var group = model.GetType().GetProperties();

                //抓到對應Header的欄位順序，這段有點浪費資源，要想有沒有更好的做法
                Dictionary<int, string> orders = new Dictionary<int, string>();

                for (int sort = 0; sort < header.Cells.Count(); sort++)
                {
                    orders.Add(sort, header.Cells[sort].StringCellValue);
                }

                //循欄對硬塞進Class裡
                for (int j = 0; j < columns.Count(); j++)
                {
                    var columnName = columns[j];
                    //取得每列對應column底下的值
                    var row = sheet.GetRow(i);
                    var order =
                            orders.Any(x => x.Value == columnName)
                                ? orders.SingleOrDefault(x => x.Value == columnName).Key
                                : -1;

                    if (order == -1)
                        continue;

                    if (group.Select(x => x.Name).Contains(elements[j]))
                    {
                        //確認有沒有值
                        object setValue = null;
                        var ele = group.Single(x => x.Name == elements[j]);
                        var cell = row.Cells[order];

                        //先確認是不是對應的element，是的話就把值塞進去
                        if (ele.Name == elements[j])
                        {
                            switch (ele.PropertyType.Name)
                            {
                                case "Int32":
                                    setValue = int.Parse(cell.NumericCellValue.ToString());
                                    break;
                                case "DateTime":
                                    setValue = cell.DateCellValue;
                                    break;
                                default:
                                    setValue = cell.StringCellValue;
                                    break;
                            }
                            ele.SetValue(model, setValue, null);
                        }
                    }
                }
                response.Add(model);
            }
            return response;
        }
    }
}
