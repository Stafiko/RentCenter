using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace RentCenter.Window
{
    public partial class RoomDialog : Form
    {
        private static RoomDialog _dialogForm;
        private Room _room;
        private Tuple<DateTime, DateTime> _lastTime;
        public RoomDialog()
        {
            InitializeComponent(); 
        }

        public static void Dialog(Room room)
        {
            _dialogForm = new RoomDialog
            {
                _room = room,
                RentList = { DataSource = room.Rents.Select(r => r.Renter).ToArray() },
                RentColor = {DataSource = typeof(Color).
                    GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public)
                    .Select(x=>x.Name).ToArray()}
            };
            _dialogForm.Text += $" {room.Index}";

            _dialogForm.RentList.SelectedIndex = -1;

            _dialogForm.RentStart.ValueChanged += ChangeCost;
            _dialogForm.RentEnd.ValueChanged += ChangeCost;

            _dialogForm._lastTime = new Tuple<DateTime, DateTime>
                (_dialogForm.RentStart.MinDate, _dialogForm.RentEnd.MaxDate);

            _dialogForm.RentColor.DrawItem += (sender, args) =>
            {
                var g = args.Graphics;
                var rect = args.Bounds;
                if (args.Index < 0) return;
                var n = ((ComboBox)sender).Items[args.Index].ToString();
                var f = new Font("Arial", 9, FontStyle.Regular);
                var c = Color.FromName(n);
                var b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            };
            _dialogForm.ShowDialog();
        }

        private static void ChangeCost(object sender, EventArgs e)
        {
            var d = _dialogForm;

            if ((d.RentEnd.Value - d.RentStart.Value).TotalDays < 1)
            {
                d.RentStart.Value = d._lastTime.Item1;
                d.RentEnd.Value = d._lastTime.Item2;
                return;
            }

            d.RentPrice.Text =
                $@"{d._room.Area * d._room.Price * (d.RentEnd.Value - d.RentStart.Value).TotalDays} руб.";
            d._lastTime = new Tuple<DateTime, DateTime>
                (d.RentStart.Value, d.RentEnd.Value);
        }

        private void AddRent(object sender, EventArgs e)
        {
            if (!DataCorrect()) return;
            var d = _dialogForm;

            var rent = new Rent(d.RentName.Text, d.RentStart.Value, d.RentEnd.Value,
                Color.FromName((string)d.RentColor.SelectedItem));

            if (d._room.Rents.Any(r => r.CompareTo(rent) == 0))
            {
                MessageBox.Show("Даты накладываются по времени", "Ошибка!", MessageBoxButtons.OK);
                return;
            }

            _room.Rents.Add(rent);
            _room.IsArended = true;
            MessageBox.Show("Аренда добавлена", "Успешно", MessageBoxButtons.OK);
            RefreshList();
        }

        private void DelRent(object sender, EventArgs e)
        {
            if (!FormCorrect()) return;
            var d = _dialogForm;
            var i = d.RentList.SelectedIndex;

            _room.Rents.RemoveAt(i);
            if (!_room.Rents.Any()) _room.IsArended = false;
            d.RentList.SelectedIndex = 0;
            MessageBox.Show("Аренда удалена", "Успешно", MessageBoxButtons.OK);
            RefreshList();
        }


        private void ChangeRent(object sender, EventArgs e)
        {
            if (!FormCorrect() || !DataCorrect()) return;
            var d = _dialogForm;
            var i = d.RentList.SelectedIndex;

            var rent = new Rent(d.RentName.Text, d.RentStart.Value, d.RentEnd.Value,
                Color.FromName((string) d.RentColor.SelectedItem));

            if (d._room.Rents.Where(r => r != d._room.Rents[i])
                .Any(r => r.CompareTo(rent) == 0))
            {
                MessageBox.Show("Даты накладываются по времени", "Ошибка!", MessageBoxButtons.OK);
                return;
            }

            _room.Rents[i].Renter = d.RentName.Text;
            _room.Rents[i].RentStart = d.RentStart.Value;
            _room.Rents[i].RentEnd = d.RentEnd.Value;
            _room.Rents[i].Color = Color.FromName((string)d.RentColor.SelectedItem);
            MessageBox.Show("Аренда изменена", "Успешно", MessageBoxButtons.OK);
            RefreshList();
        }

        private void SelectRent(object sender, EventArgs e)
        {
            if(!FormCorrect()) return;
            var d = _dialogForm;
            var i = d.RentList.SelectedIndex;

            d.RentColor.SelectedItem = _room.Rents[i].Color.Name;
            d.RentName.Text = d.RentList.SelectedItem.ToString();
            d.RentStart.Value = _room.Rents[i].RentStart;
            d.RentEnd.Value = _room.Rents[i].RentEnd;
            d._lastTime = new Tuple<DateTime, DateTime>
                (d.RentStart.Value, d.RentEnd.Value);
        }

        private void RefreshList()
        {
            _dialogForm.RentList.DataSource = _room.Rents.Select(r => r.Renter).ToArray();
        }

        private static bool FormCorrect()
        {
            if (_dialogForm == null) return false;
            return _dialogForm.RentList.SelectedIndex != -1;
        }

        private static bool DataCorrect()
        {
            if (string.IsNullOrEmpty(_dialogForm.RentName.Text)) return false;
            return _dialogForm.RentColor.Text != "Transparent";
        }
    }
}
