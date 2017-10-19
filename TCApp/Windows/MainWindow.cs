using System;
using System.Collections.Generic;
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

        private List<Room> _rooms;
        private List<PictureBox> _map;

        #endregion

        #region Инициализация

        private void InitializeMap()
        {
            using (var db = new DBConnectorDataContext())
            {
                var rooms = db.floors_;

                _map = new List<PictureBox>();
                _rooms = new List<Room>();

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
                    _map.Last().MouseClick += RoomInfo;

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
                    place.Rents.Add(new Rent(rent.Renter, rent.Rent_Start, rent.Rent_End, Color.FromName(rent.RentColor)));
                    ChangeRent(new Point(rent.Floor, rent.Room));
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

                db.SubmitChanges();
            }
        }

        #endregion

        #region Методы

        private void ChangeRent(Point point)
        {
            var place = _map.Find(m => (Point)m.Tag == point);
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

        private void RoomInfo(object sender, MouseEventArgs e)
        {
            var point = (Point) (sender as Control).Tag;
            var room = _rooms.Find(r => r.Floor == point.X && r.Index == point.Y);
            RoomDialog.Dialog(room);
            ChangeRent(point);
            FloorSelected(null, null);
        }

        private void FloorSelected(object sender, EventArgs eventArgs)
        {
            GanttChart.RemoveBars();
            GanttChart.Invalidate();

            var selected = Floors.SelectedIndex;
            var floor = _rooms.Where(r => r.IsArended && r.Floor == selected + 1).ToArray();
            if(!floor.Any()) return;

            GanttChart.FromDate = floor.Min(x => x.Rents.Min(y => y.RentStart));
            GanttChart.ToDate = floor.Max(x => x.Rents.Max(y => y.RentEnd));

            for (int i = 0; i < floor.Length; i++) 
            {
                var room = floor[i];
                foreach(var rent in room.Rents)
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

            GanttChart.Invalidate();
        }

        private void BarSelected(object sender, MouseEventArgs e)
        {
            var tips = new List<string>();
            if (GanttChart.MouseOverRowText.Length > 0)
            {
                var data = GanttChart.MouseOverRowValue as BarInformation;
                tips.Add($"Арендователь: {data.BarText}");
                tips.Add($"Аренда с {data.FromTime.ToLongDateString()}");
                tips.Add($"Аренда до {data.ToTime.ToLongDateString()}");
            }

            GanttChart.ToolTipTextTitle = GanttChart.MouseOverRowText;
            GanttChart.ToolTipText = tips;
        }

        #endregion

        #region Конструктор

        public MainWindow()
        {
            InitializeComponent();
            InitializeMap();
            InitializeRents();
            FloorSelected(null, null);
            FormClosing += MainWindowClosing;
        }

        #endregion
    }
}
