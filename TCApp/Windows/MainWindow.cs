using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RentCenter.Gantt;
using RentCenter.Window.Databases;

namespace RentCenter.Window
{
    public partial class MainWindow : Form
    {
        #region Поля и свойства

        private List<Room> _rooms = new List<Room>();
        private List<PictureBox> _map = new List<PictureBox>();
        private Dictionary<string, decimal> _percents = new Dictionary<string, decimal>();

        #endregion

        #region Инициализация

        private void InitializeMap()
        {
            using (var db = new DBConnectorDataContext())
            {
                var rooms = db.floors_;

                foreach (var room in rooms)
                {
                    #region Карта

                    var tab = Floor1;
                    switch (room.Floor)
                    {
                        case 1:
                            tab = Floor1;
                            break;
                        case 2:
                            tab = Floor2;
                            break;
                        case 3:
                            tab = Floor3;
                            break;
                    }

                    var label = new Label
                    {
                        BackColor = Color.Transparent,
                        ForeColor = Color.White,
                        Font = new Font("Arial", 12f, FontStyle.Bold),
                        Text = room.Room.ToString(),
                        Size = new Size(30, 15)
                    };

                    _map.Add(new PictureBox
                    {
                        Parent = tab,
                        Size = new Size(room.Width, room.Height),
                        Top = room.Y,
                        Left = room.X,
                        BorderStyle = BorderStyle.FixedSingle,
                        Tag = new Point(room.Floor, room.Room),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        BackColor = Color.Gray,
                        Cursor = Cursors.Hand,
                    });
                    _map.Last().Controls.Add(label);
                    _map.Last().MouseClick += RoomDialog;

                    #endregion

                    _rooms.Add(new Room(room.Floor, room.Room, room.Width * room.Height, room.Price));
                }
            }
        }

        private void InitializeRents()
        {
            using (var db = new DBConnectorDataContext())
            {
                var rents = db.rents_;

                foreach (var rent in rents)
                {
                    var place = _rooms.Find(r => r.Floor == rent.Floor && r.Index == rent.Room);
                    place.IsArended = true;
                    place.Rents.Add(new Rent(rent.Renter, rent.Rent_Start, rent.Rent_End,
                        Color.FromName(rent.RentColor)));
                    ChangeRent(new Point(rent.Floor, rent.Room));
                }
            }
        }

        private void InitializeCosts()
        {
            using (var db = new DBConnectorDataContext())
            {
                var costs = db.costs_;
                var percents = db.percent_;

                foreach (var cost in costs)
                    TotalCosts.Rows.Add(cost.Type, cost.Info, cost.FromDate, cost.ToDate, cost.Cost, cost.Percents, cost.Total, cost.PerMoth > 0);

                foreach (var percent in percents)
                {
                    CostType.Items.Add(percent.Type);
                    _percents.Add(percent.Type, percent.Perc);
                }
            }
        }

        #endregion

        #region Сохранение

        private void MainWindowClosing(object sender, FormClosingEventArgs e)
        {
            using (var db = new DBConnectorDataContext())
            {
                db.rents_.DeleteAllOnSubmit(db.rents_);
                db.costs_.DeleteAllOnSubmit(db.costs_);
                db.percent_.DeleteAllOnSubmit(db.percent_);

                foreach (var room in _rooms)
                foreach (var rent in room.Rents)
                {
                    db.rents_.InsertOnSubmit(new rents_
                    {
                        Floor = room.Floor,
                        Room = room.Index,
                        Rent_Start = rent.RentStart,
                        Rent_End = rent.RentEnd,
                        Renter = rent.Renter,
                        RentColor = rent.Color.Name
                    });
                }

                for (int i = 0; i < TotalCosts.RowCount; i++)
                    db.costs_.InsertOnSubmit(new costs_
                    {
                        Type = (string) TotalCosts["Type", i].Value,
                        Info = (string) TotalCosts["Info", i].Value,
                        FromDate = (DateTime) TotalCosts["From", i].Value,
                        ToDate = (DateTime) TotalCosts["To", i].Value,
                        Cost = (decimal) TotalCosts["Cost", i].Value,
                        Percents = (decimal)TotalCosts["Percent", i].Value,
                        Total = (decimal)TotalCosts["Price", i].Value,
                        PerMoth = (bool)TotalCosts["PerMonth", i].Value ? 1 : -1
                    });

                foreach (var item in CostType.Items)
                {
                    var type = item.ToString();
                    db.percent_.InsertOnSubmit(new percent_
                    {
                        Type = type,
                        Perc = _percents[type]
                    });
                }

                db.SubmitChanges();
            }
        }

        #endregion

        #region Методы

        private void ChangeRent(Point point)
        {
            var place = _map.Find(m => (Point) m.Tag == point);
            var room = _rooms.Find(r => r.Floor == point.X && r.Index == point.Y);

            if (!room.IsArended) place.BackColor = Color.Gray;
            else
            {
                var closest = room.Rents.Any(x => x.RentEnd > DateTime.Now)
                    ? room.Rents.Where(x => x.RentEnd > DateTime.Now).OrderBy(x => x.RentEnd).First()
                    : room.Rents.OrderBy(x => x.RentStart).Last();
                place.BackColor = closest.Color;
            }
            place.Invalidate();
        }

        private void RoomDialog(object sender, MouseEventArgs e)
        {
            var point = (Point) (sender as Control).Tag;
            var room = _rooms.Find(r => r.Floor == point.X && r.Index == point.Y);
            Window.RoomDialog.Dialog(room);
            ChangeRent(point);
            InvalidateFloorChart(null, null);
        }

        private void CalculateCosts(object sender, EventArgs e)
        {
            TotalProfit.Rows.Clear();
            TotalProfit.Refresh();

            var rooms = _rooms.Where(r => r.IsArended);
            var minDate = rooms.Min(r => r.Rents.Min(rent => rent.RentStart));
            var maxDate = rooms.Max(r => r.Rents.Max(rent => rent.RentEnd));
            var monthCount = Math.Abs(maxDate.Month - minDate.Month + 12 * (maxDate.Year - minDate.Year)) + 1;

            TotalProfit.Rows.Add(monthCount);

            for (int i = 1; i < 4; i++)
            {
                var nowDate = minDate;
                var floor = rooms.Where(r => r.Floor == i);
                for (int j = 0; j < monthCount; j++)
                {
                    var nextDate = nowDate.AddMonths(1);
                    var price = floor
                        .Sum(room => room.Rents
                            .Where(r => r.RentStart < nextDate && r.RentEnd > nowDate)
                            .Sum(rent => room.Area * room.Price * (nextDate - nowDate).TotalDays));
                    TotalProfit[0, j].Value = $"{nowDate.Month}.{nowDate.Year} - {nextDate.Month}.{nextDate.Year}";
                    TotalProfit[i, j].Value = price;
                    nowDate = nextDate;
                }
            }

            for (int i = 0; i < TotalProfit.Rows.Count; i++)
            {
                var profit = (double) TotalProfit[1, i].Value +
                             (double) TotalProfit[2, i].Value +
                             (double) TotalProfit[3, i].Value;
                var percents = profit * 0.18;
                TotalProfit["Percents", i].Value = percents;
                TotalProfit["Total", i].Value = profit - percents;
            }

            var allCosts = TotalCosts.Rows
                .OfType<DataGridViewRow>()
                .Where(x => x.Cells["Cost"].Value != null && x.Cells["PerMonth"].Value != null)
                .Sum(r =>
                {
                    var cost = (decimal)r.Cells["Cost"].Value;
                    var percents = (decimal) r.Cells["Percent"].Value;
                    var isPerMonth = (bool) r.Cells["PerMonth"].Value;
                    var FromDate = (DateTime)r.Cells["From"].Value;
                    var ToDate = (DateTime)r.Cells["To"].Value;
                    var credit = ((string) r.Cells["Type"].Value).ToLower().Contains("кредит");
                    var month = Math.Abs(ToDate.Month - FromDate.Month + 12 * (ToDate.Year - FromDate.Year));
                    var total = cost;

                    if (credit)
                    {
                        for (int i = 0; i < month; i++)
                            total += total * (percents / 12 / 100);
                        r.Cells["Price"].Value = total;
                        return total;
                    }

                    total = isPerMonth 
                        ? total * (1 + percents / 12 / 100) * month
                        : total * (1 + percents / 100);
                    r.Cells["Price"].Value = total;
                    return total;
                });
            allCosts = Math.Round(allCosts, 2);

            var profitRows = TotalProfit.Rows
                .OfType<DataGridViewRow>()
                .Where(x => x.Cells["Total"].Value != null && (double)x.Cells["Total"].Value > 0);
            var allProfit = profitRows.OfType<DataGridViewRow>().Sum(r => (double) r.Cells["Total"].Value);
            var profitMonths = profitRows.Count();
            var monthlyProfits = TotalProfit.Rows
                .OfType<DataGridViewRow>()
                .Select(r => (double) r.Cells["Total"].Value)
                .ToArray();
            var profitPerMonth = Math.Round(allProfit / profitMonths, 2);

            double pbMonthes = 0;
            double profits = 0;
            for (int i = 0; i < monthCount; i++)
            {
                pbMonthes = i+1;
                profits += monthlyProfits[i];
                if(profits > (double)allCosts)
                    break;
            }
            if (profits == monthCount)
                pbMonthes = profitMonths > 0 ? Math.Round((double) allCosts / (allProfit / profitMonths), 2) : 0;

            var averageRate = allCosts > 0 && monthCount / 12 > 0 
                ? Math.Round(allProfit / (monthCount / 12) / (double) allCosts, 2)
                : 0;
            var profitIndex = allCosts > 0 ? Math.Round(allProfit / (double) allCosts, 2) : 0;

            TotalCounting.Rows.Clear();
            TotalCounting.Refresh();

            TotalCounting.Rows.Add("Общий денежный поток (CF), руб.:", $"{allProfit}");
            TotalCounting.Rows.Add("Средний месячный денежный поток (CFm), руб.:", $"{profitPerMonth}");
            TotalCounting.Rows.Add("Инвестиционные расходы (Inv), руб.:", $"{allCosts}");
            TotalCounting.Rows.Add("Период окупаемости (PB), мес.:", $"{pbMonthes}");
            TotalCounting.Rows.Add("Средняя норма рентабельности (ARR), %:", $"{averageRate}");
            TotalCounting.Rows.Add("Чистый приведенный доход (NPV), руб.:", $"{allProfit - (double) allCosts}");
            TotalCounting.Rows.Add("Индекс прибыльности (PI):", $"{profitIndex}");
        }

        private void InvalidateCostsChart(object sender, EventArgs eventArgs)
        {
            if (FunctionsTabs.SelectedIndex == 0)
            {
                InvalidateFloorChart(null, null);
                return;
            }

            GanttChart.RemoveBars();

            var index = 0;
            var arendedRooms = _rooms.Where(r => r.IsArended);

            for (int i = 0; i < TotalCosts.RowCount; i++)
            {
                var info = (string)TotalCosts["Info", i].Value;
                var cost = (decimal)TotalCosts["Cost", i].Value;
                var from = (DateTime)TotalCosts["From", i].Value;
                var to = (DateTime)TotalCosts["To", i].Value;

                var bar = new BarInformation(
                    info, $"Стоимость: {cost} р.",
                    from, to,
                    Color.Red, Color.Red,
                    index++);
                GanttChart.AddChartBar(
                    bar.RowText, bar.BarText, bar,
                    bar.FromTime, bar.ToTime,
                    bar.Color, bar.HoverColor,
                    bar.RowIndex);
            }

            for (int i = 1; i < 4; i++)
            {
                var floor = arendedRooms.Where(r => r.Floor == i).ToArray();
                foreach (var room in floor)
                {
                    foreach (var rent in room.Rents)
                    {
                        var renter = rent;
                        var bar = new BarInformation(
                            $"Прибыль {i} этажа", $"П{room.Index}: {renter.Renter}",
                            renter.RentStart, renter.RentEnd,
                            renter.Color, renter.Color,
                            index);
                        GanttChart.AddChartBar(
                            bar.RowText, bar.BarText, bar,
                            bar.FromTime, bar.ToTime,
                            bar.Color, bar.HoverColor,
                            bar.RowIndex);
                    }
                }
                index++;
            }

            var minCostsDate = DateTime.MaxValue;
            var maxCostsDate = DateTime.MinValue;
            if (TotalCosts.Rows.Count > 0)
            {
                minCostsDate = TotalCosts.Rows
                    .OfType<DataGridViewRow>()
                    .Where(x => x.Cells["From"].Value != null)
                    .Select(r => (DateTime) r.Cells["From"].Value)
                    .Min();
                maxCostsDate = TotalCosts.Rows
                    .OfType<DataGridViewRow>()
                    .Where(x => x.Cells["To"].Value != null)
                    .Select(r => (DateTime) r.Cells["To"].Value)
                    .Max();
            }

            var minArendDate = arendedRooms.Min(x => x.Rents.Min(y => y.RentStart));
            var maxArendDate = arendedRooms.Max(x => x.Rents.Max(y => y.RentEnd));

            GanttChart.FromDate = minArendDate < minCostsDate ? minArendDate : minCostsDate;
            GanttChart.ToDate = maxArendDate > maxCostsDate ? maxArendDate : maxCostsDate;

            GanttChart.Invalidate();
        }

        private void InvalidateFloorChart(object sender, EventArgs eventArgs)
        {
            GanttChart.RemoveBars();

            var selected = Floors.SelectedIndex;
            var floor = _rooms.Where(r => r.IsArended && r.Floor == selected + 1).ToArray();
            if (!floor.Any()) return;

            for (int i = 0; i < floor.Length; i++)
            {
                var room = floor[i];
                foreach (var rent in room.Rents)
                {
                    var renter = rent;
                    var bar = new BarInformation(
                        $"Помещение {room.Index}", renter.Renter,
                        renter.RentStart, renter.RentEnd,
                        renter.Color, renter.Color,
                        i);
                    GanttChart.AddChartBar(
                        bar.RowText, bar.BarText, bar,
                        bar.FromTime, bar.ToTime,
                        bar.Color, bar.HoverColor,
                        bar.RowIndex);
                }
            }

            GanttChart.FromDate = floor.Min(x => x.Rents.Min(y => y.RentStart));
            GanttChart.ToDate = floor.Max(x => x.Rents.Max(y => y.RentEnd));

            GanttChart.Invalidate();
        }

        private void ShowChartTooltip(object sender, MouseEventArgs e)
        {
            var tips = new List<string>();
            if (GanttChart.MouseOverRowText.Length > 0)
            {
                var data = GanttChart.MouseOverRowValue as BarInformation;
                tips.Add($"{data.BarText}");
                tips.Add($"С {data.FromTime.ToLongDateString()}");
                tips.Add($"До {data.ToTime.ToLongDateString()}");
            }

            GanttChart.ToolTipTextTitle = GanttChart.MouseOverRowText;
            GanttChart.ToolTipText = tips;
        }

        private void AddCost(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CostInfoBox.Text) || string.IsNullOrWhiteSpace(CostType.Text))
                return;

            if (!CostType.Items.Contains(CostType.Text))
                CostType.Items.Add(CostType.Text);

            if (!_percents.ContainsKey(CostType.Text))
                _percents.Add(CostType.Text, (int) CostPercent.Value);
            else
                _percents[CostType.Text] = (int) CostPercent.Value;

            TotalCosts.Rows.Add(CostType.Text, CostInfoBox.Text, CostDateFrom.Value, CostDateTo.Value, CostPriceNumeric.Value, CostPercent.Value);

            var last = TotalCosts.Rows.GetLastRow(DataGridViewElementStates.None);
            if (PercentLabel.Text == "Ставка")
            {
                TotalCosts["PerMonth", last].Value = true;
                TotalCosts["PerMonth", last].ReadOnly = true;
            }
        }


        private void CostTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (_percents.ContainsKey(CostType.Text))
                CostPercent.Value = _percents[CostType.Text];
        }


        private void CostTypeTextChanged(object sender, EventArgs e)
        {
            PercentLabel.Text = CostType.Text.ToLower().Contains("кредит") 
                    ? "Ставка"
                    : "Налог";
        }

        #endregion

        #region Конструктор

        public MainWindow()
        {
            InitializeComponent();
            InitializeMap();
            InitializeRents();
            InitializeCosts();
            InvalidateFloorChart(null, null);

            #region Подписки

            FunctionsTabs.SelectedIndexChanged += CalculateCosts;
            FunctionsTabs.SelectedIndexChanged += InvalidateCostsChart;

            TotalCosts.CellValueChanged += CalculateCosts;
            TotalCosts.RowsAdded += CalculateCosts;
            TotalCosts.RowsAdded += InvalidateCostsChart;
            TotalCosts.RowsRemoved += CalculateCosts;
            TotalCosts.RowsRemoved += InvalidateCostsChart;

            Floors.SelectedIndexChanged += InvalidateFloorChart;
            GanttChart.MouseMove += ShowChartTooltip;

            FormClosing += MainWindowClosing;

            #endregion
        }

        #endregion
    }
}
