using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Data;
using WmiPresenter.Containers;
using WmiPresenter.Data;
using WmiController.Prototypes;

namespace WmiPresenter.Export
{
    public class InstanceExport
    {
        private readonly Worksheet _worksheet;
        private readonly Application _app;
        private readonly Workbook _workbook;

        public InstanceExport(string path)
        {
            _app = new Application();
            _workbook = _app.Workbooks.Open(path);
            string tempname = "Отчет " + DateTime.Now.ToFileTime().ToString();
            _workbook.SaveAs(tempname);
            _workbook.Close();
            _workbook = _app.Workbooks.Open(path.Split('\\')[0] + tempname);
            _worksheet = _workbook.ActiveSheet;
            _app.Visible = true;


        }

        public void InsertStatistics(ComputerSystem comp, PerfFormattedDataPerfOsSystem mem, List<Processor> processors /*ObservableCollectionEx<StatisticsItem> statistics*/)
        {
            if (comp == null || mem == null || processors == null) return;
            int i = 1;
            _worksheet.Cells[2, 1] = "Имя компьютера";
            _worksheet.Cells[2, 2] = comp.Name ?? string.Empty;
            _worksheet.Cells[3, 1] = "Пользователь";
            _worksheet.Cells[3, 2] = comp.UserName ?? string.Empty;
            _worksheet.Cells[4, 1] = "Количество процессоров";
            _worksheet.Cells[4, 2] = comp.NumberOfProcessors.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
            _worksheet.Cells[5, 1] = "Количество памяти в байтах";
            _worksheet.Cells[5, 2] = comp.TotalPhysicalMemory.ToString() ?? string.Empty;
            _worksheet.Cells[6, 1] = "Количество памяти в мегабайтах";
            _worksheet.Cells[6, 2] = (comp.TotalPhysicalMemory / (1024 * 1024)).ToString() ?? string.Empty;
            _worksheet.Cells[7, 1] = "Время работы системы от последней (пере)загрузки";
            _worksheet.Cells[7, 2] = TimeSpan.FromSeconds(mem.SystemUpTime).ToString() ?? string.Empty;
            _worksheet.Cells[8, 1] = "Количество запущенных процессов";
            _worksheet.Cells[8, 2] = mem.Processes.ToString() ?? string.Empty;
            _worksheet.Cells[9, 1] = "Количество потоков";
            _worksheet.Cells[9, 2] = mem.Threads.ToString() ?? string.Empty;
            _worksheet.Cells[10, 1] = "Количество системных вызовов в секунду";
            _worksheet.Cells[10, 2] = mem.SystemCallsPersec.ToString() ?? string.Empty;
            _worksheet.Cells[11, 1] = "Количество переключений контекста в секунду";
            _worksheet.Cells[11, 2] = mem.ContextSwitchesPersec.ToString() ?? string.Empty;
            _worksheet.Cells[12, 1] = "Количество читаемых из файлов байтов в секунду";
            _worksheet.Cells[12, 2] = mem.FileReadBytesPersec.ToString() ?? string.Empty;
            _worksheet.Cells[13, 1] = "Количество записываемых в файлы байтов в секунду";
            _worksheet.Cells[13, 2] = mem.FileWriteBytesPersec.ToString() ?? string.Empty;
            foreach (var processor in processors)
            {
                _worksheet.Cells[14 + i, 1] = string.Format("Процессор [{0}] - текущая частота в мегагерцах", i);
                _worksheet.Cells[14 + i, 2] = processor.CurrentClockSpeed;
                _worksheet.Cells[15 + i, 1] =
                    string.Format("Процессор [{0}] - текущая частота от внешнего источника в мегагерцах", i);
                _worksheet.Cells[15 + i, 2] = processor.ExtClock;
                _worksheet.Cells[16 + i, 1] = string.Format("Процессор [{0}] - максимальная частота в мегагерцах", i);
                _worksheet.Cells[16 + i, 2] = processor.MaxClockSpeed;
                _worksheet.Cells[17 + i, 1] = string.Format("Процессор [{0}] - разрядность", i);
                _worksheet.Cells[17 + i, 2] = processor.AddressWidth;
                _worksheet.Cells[18 + i, 1] = string.Format("Процессор [{0}] - количество ядер", i);
                _worksheet.Cells[18 + i, 2] = processor.NumberOfCores;
                _worksheet.Cells[19 + i, 1] = string.Format("Процессор [{0}] - загрузка в процентах", i);
                _worksheet.Cells[19 + i, 2] = processor.LoadPercentage;
                _worksheet.Cells[20 + i, 1] = string.Format("Процессор [{0}] - загрузка", i);
                _worksheet.Cells[20 + i, 2] = processor.LoadPercentage;
                i += 7;
            }


        }

        public void InsertChart(List<Tuple<double, ushort, uint>> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                _worksheet.Cells[49, i + 2] = data[i].Item1.ToString();
                _worksheet.Cells[50, i + 2] = data[i].Item2.ToString();
                _worksheet.Cells[51, i + 2] = data[i].Item3.ToString();
            }

            ChartObjects chartobjt = _worksheet.ChartObjects(Type.Missing);
            ChartObject chartobject = chartobjt.Add(0, 500, data.Count * 80, 300);

            Chart chart = chartobject.Chart;
            chart.ChartType = XlChartType.xlLineMarkers;
            SeriesCollection seriacollection = chart.SeriesCollection();
            Series processor = seriacollection.NewSeries();
            Series memory = seriacollection.NewSeries();
            processor.Name = "Загрузка ЦП";
            memory.Name = "Загрузка памяти";

            Range Xvalues = _worksheet.Range[_worksheet.Cells[49, 2], _worksheet.Cells[49, data.Count + 2]];

            Range Value = _worksheet.Range[_worksheet.Cells[50, 2], _worksheet.Cells[50, data.Count + 1]];
            Range Value2 = _worksheet.Range[_worksheet.Cells[51, 2], _worksheet.Cells[51, data.Count + 1]];
            processor.Values = Value;
            processor.XValues = Xvalues;
            memory.Values = Value2;
            memory.XValues = Xvalues;
        }

    }
}

