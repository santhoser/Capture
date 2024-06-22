using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capture
{
    public partial class SelectAreaForm : Form
    {
        private bool _dragging = false;
        private Point _startPoint;
        private Rectangle _selectionRectangle;

        public Rectangle SelectedArea { get; private set; }
        public SelectAreaForm()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            this.Opacity = 0.5;
            //this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;

            var screens = Screen.AllScreens;
            var bounds = screens.Select(s => s.Bounds).Aggregate(Rectangle.Union);
            this.Bounds = bounds;
            this.TopMost = true;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _dragging = true;
            _startPoint = PointToClient(Cursor.Position);
            //_selectionRectangle = new Rectangle(e.Location, new Size(0, 0));
            //_startPoint = new Point(e.X + this.Left, e.Y + this.Top);
            _selectionRectangle = new Rectangle(_startPoint, new Size(0, 0));
            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_dragging)
            {
                var currentPoint = Cursor.Position;// new Point(e.X + this.Left, e.Y + this.Top);
                int width = Math.Abs(currentPoint.X - _startPoint.X);
                int height = Math.Abs(currentPoint.Y - _startPoint.Y);
                _selectionRectangle = new Rectangle(
                    Math.Min(_startPoint.X, currentPoint.X),
                    Math.Min(_startPoint.Y, currentPoint.Y),
                    width, height);
                this.Invalidate();
            }
            //if (_dragging)
            //{
            //    int width = Math.Abs(e.X - _startPoint.X);
            //    int height = Math.Abs(e.Y - _startPoint.Y);
            //    _selectionRectangle = new Rectangle(
            //        Math.Min(_startPoint.X, e.X),
            //        Math.Min(_startPoint.Y, e.Y),
            //        width, height);
            //    this.Invalidate();
            //}
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _dragging = false;
            SelectedArea = _selectionRectangle;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var pen = new Pen(Color.Red, 2))
            {
                e.Graphics.DrawRectangle(pen, _selectionRectangle);
            }
        }
    }
}
