using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


  public static class Extensions
  {
      public static void InvokeIfRequired(this Control ctrl, Action action)
      {
          if (ctrl.InvokeRequired)
          {
              ctrl.BeginInvoke(action);

          }
          else
          {
              action();
          }
      }
  }
    public static class TableExtensions
    {
        /// <summary>
        /// Gets the string constructed from joined values in specified column.
        /// </summary>
        /// <param name="dt">The data table.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>the string constructed from joined values in specified column.</returns>
        public static string GetJoinedColumn(this DataTable dt, int columnIndex)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append(dt.Rows[i][columnIndex].ToString());
            }

            return sb.ToString();

        }
         
        /// <summary>
        /// Gets the string constructed from joined values in last column.
        /// </summary>
        /// <param name="dt">The data table.</param>
        /// <returns>the string constructed from joined values in last column.</returns>
        public static string GetJoinedLastColumn(this DataTable dt)
        {
            int columnIndex = dt.Columns.Count - 1;
            return dt.GetJoinedColumn(columnIndex);

        }

        /// <summary>
        /// Gets the string constructed from joined values across table.
        /// </summary>
        /// <param name="dt">The data table.</param>
        /// <returns>the string constructed from joined values across table.</returns>
        public static string GetJoinedTable(this DataTable dt)
        {
            const string NO_VALUE = "*";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    object value = dt.Rows[i][j];
                    string toAppend = (value ?? NO_VALUE).ToString();
                    sb.Append(toAppend);
                }
                sb.Append(Environment.NewLine);
            }

            return sb.ToString().Trim();

        }

        /// <summary>
        /// populate last column of data table with the text.
        /// </summary>
        /// <param name="dt">The data table to populate it's last column.</param
        /// <param name="txt">The text to populate.</param>
        public static void PopulateTableLastColumn(this DataTable dt, string txt)
        {

            int lastColoumnIndex = dt.Columns.Count - 1;
            PopulateTableColumn(dt, txt, lastColoumnIndex);
        }

        /// <summary>
        /// populates first column of data table with the text.
        /// </summary>
        /// <param name="dt">The data table to populate it's first column.</param
        /// <param name="txt">The text to populate.</param>
        public static void PopulateTableFirstColumn(this DataTable dt, string txt)
        {

            PopulateTableColumn(dt, txt, 0);
        }

        /// <summary>
        /// Populates the table specified column.
        /// </summary>
        /// <param name="dt">The data table to populate it's specified column.</param
        /// <param name="txt">The text to populate.</param>
        /// <param name="lastColoumnIndex">the index of the column to populate.</param>
        public static void PopulateTableColumn(this DataTable dt, string txt, int idx)
        {
            for (int i = 0; i < txt.Length; i++)
            {
                DataRow row;

                bool addedNewRow = false;
                if (dt.Rows.Count > i)
                {
                    row = dt.Rows[i];
                }
                else
                {
                    addedNewRow = true;
                    row = dt.NewRow();

                }

                row[idx] = txt[i];

                if (addedNewRow)
                {
                    dt.Rows.Add(row);

                }
            }
        }


        /// <summary>
        /// Gets the cyclic sequences for every row starting at value at last column and advancing to first.
        /// </summary>
        /// <param name="dt">The data table.</param>
        /// <param name="length">The length of requested sequences.</param>
        /// <returns>the cyclic sequence<s/returns>
        public static List<string> GetCyclicSequence(this DataTable dt,int length)
        {
            var sequences = new List<string>();
            for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
            {
                var row = dt.Rows[rowIndex];
                sequences.Add(row.GetCyclicSequence(length));
                
            }

            return sequences;
        }

        /// <summary>
        /// Gets the cyclic sequence starting at value at last column and advancing to first.
        /// </summary>
        /// <param name="row">The row to get the sequence for.</param>
        /// <param name="length">The length of requested sequence.</param>
        /// <returns>
        /// the cyclic sequence
        /// </returns>
        private static string GetCyclicSequence(this DataRow row, int length)
        {
            int lastColIdx = row.Table.Columns.Count -1;
            var sb = new StringBuilder(0);
            for (int i = 0; i < length; i++)
            {
                //this will ensure starting from last column and continuing with first
                int normallizedIndex = (i + lastColIdx) % (lastColIdx + 1);
                sb.Append(row[normallizedIndex].ToString());

            }
            return sb.ToString();
        }

        /// <summary>
        /// Shifts the columns to the right. after this method the columns will be rearranged in a manner that:
        /// co_0 -> col_1 ; co_1 -> col_2 ; col_2 -> col_3; ... ; co_n -> col_0 
        /// </summary>
        /// <param name="dt">The table.</param>
        public static void ShiftColumnsData(this DataTable dt)
        {
            string[] columnNames = dt.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray(); 
            //shift data
            for (int columnIndex = 0; columnIndex < dt.Columns.Count; columnIndex++)
            {
                var colName = columnNames[columnIndex];
                int newIndex = (columnIndex + 1) % dt.Columns.Count;
                dt.Columns[colName].SetOrdinal(newIndex);
            }
            //restore columns names
            for (int columnIndex = 0; columnIndex < dt.Columns.Count; columnIndex++)
            {
                if (columnIndex < dt.Columns.Count -1)
                {
                    //this is for avoiding duplicate column names
                    dt.Columns[columnIndex + 1].ColumnName = "temp" + columnIndex;
                }
                dt.Columns[columnIndex].ColumnName = columnNames[columnIndex];
            }

        }

       /* public static void SetColumnsOrder(this DataTable table, params String[] columnNames)
        {
            for (int columnIndex = 0; columnIndex < columnNames.Length; columnIndex++)
            {
                table.Columns[columnNames[columnIndex]].SetOrdinal(columnIndex);
            }
        }*/
    }

