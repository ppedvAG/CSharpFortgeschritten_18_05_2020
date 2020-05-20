using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_MitEigenenControls
{
    class MeinButton : Button
    {
         int zahl;

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                if (value == Color.Pink)
                    base.BackColor = Color.Yellow;
                else
                    base.BackColor = value;
            }
        }

        public event EventHandler<int> TripleClick;

        int clickCount;
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            clickCount++;
            if (clickCount % 3 == 0)
            {
                TripleClick?.Invoke(this, clickCount);
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            //base.OnPaint(pevent);
            pevent.Graphics.FillRectangle(Brushes.Silver, this.ClientRectangle);
            pevent.Graphics.FillEllipse(Brushes.Red, this.ClientRectangle);
        }
    }
}
