using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MGCPCB;
namespace xPCB_OutlineAssigner
{
    using Path = List<ClipperLib.DoublePoint>;
    public partial class BreakAwayTabWizard : DarkUI.Forms.DarkForm
    {
        int tag = 0;
        List<xFS_DrawingTools.DrawingTools.DoubleVector> _pnts;
        List<xFS_DrawingTools.DrawingTools.DoubleVector> _selectedPnts = new List<xFS_DrawingTools.DrawingTools.DoubleVector>();
        MGCPCB.Command myCMD;
        MGCPCB.Document pcbDoc;
        public bool ready { set; get; }
        public BreakAwayTabWizard(MGCPCB.Document _pcbDoc)
        {
            pcbDoc = _pcbDoc;
            try
            {
                using (var sr = new System.IO.StreamReader(System.Windows.Forms.Application.StartupPath + @"\BreakAwayTabs.txt"))
                {
                    _breakAwayTabs.AddRange(sr.ReadToEnd().Replace("\r", "").Split('\n'));
                }
                this.ready = true;
            }
            catch { }
            InitializeComponent();
        }

        bool myCMD_OnMouseClk(EPcbMouseButton eButton, EPcbKeyboardFlags eFlags, double dX, double dY)
        {
            if (eButton != EPcbMouseButton.epcbMouseButtonLeft)
                return true;

            Dictionary<xFS_DrawingTools.DrawingTools.DoubleVector, double> hs = new Dictionary<xFS_DrawingTools.DrawingTools.DoubleVector, double>();
            foreach (xFS_DrawingTools.DrawingTools.DoubleVector dv in _pnts)
            {
                double _dist = Math.Sqrt(((Math.Abs(dv.X - dX) * Math.Abs(dv.X - dX)) + (Math.Abs(dv.Y - dY) * Math.Abs(dv.Y - dY))));
                if (_dist < 100)
                    hs.Add(dv, _dist);
            }

            if (hs.Count > 0)
            {
                xFS_DrawingTools.DrawingTools.DoubleVector dp = hs.OrderBy(x => x.Value).First().Key;

                if (!_selectedPnts.Exists(x => x.X == dp.X && x.Y == dp.Y))
                {
                    _selectedPnts.Add(dp);
                }
                else
                {
                    _selectedPnts.Remove(dp);
                }

                Path _triangle = new Path();
                _triangle.Add(new ClipperLib.DoublePoint(dp.X * 100, dp.Y * 100));
                _triangle.Add(new ClipperLib.DoublePoint((dp.X + 50) * 100, (dp.Y + 100) * 100));
                _triangle.Add(new ClipperLib.DoublePoint((dp.X - 50) * 100, (dp.Y + 100) * 100));


                var path = xFS_DrawingTools.DrawingTools.getPCBPath(xFS_DrawingTools.DrawingTools.RotatePath(_triangle, dp.R, dp.X * 100, dp.Y * 100));

                pcbDoc.ActiveViewEx.MotionGfxPutPointsArray(path, EPcbMotionGfxPointType.epcbMotionGfxPointStatic, tag, 0,
                    pcbDoc.Application.Utility.NewColor(0, 255, 255), EPcbUnit.epcbUnitMils);

                UpdateList();
            }
            return true;
        }

        private void UpdateList()
        {
            dataGridView1.Invoke(new MethodInvoker(delegate
            {
                dataGridView1.DataSource = _selectedPnts.ToArray();
            }));
        }

        List<string> _breakAwayTabs = new List<string>();
        private void BreakAwayTabWizard_Load(object sender, EventArgs e)
        {

            MGCPCB.Components comps = pcbDoc.get_Components(EPcbSelectionType.epcbSelectAll, EPcbComponentType.epcbCompAll, EPcbCelltype.epcbCelltypeDrawing, "*");
            if (comps.OfType<MGCPCB.Component>().Any(x => x.CellName.StartsWith("Break Away Tab")))
            {
                if (MessageBox.Show("Would you remove previously placed tabs?", "Remove tabs", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (MGCPCB.Component comp in comps)
                    {
                        if (comp.CellName.StartsWith("Break Away Tab"))
                            comp.Delete();
                    }

                }
            }

            _breakAwayTabs.ForEach(item =>
            {
                TabSizeSelect_Combo.Items.Add(new DarkUI.Controls.DarkDropdownItem(item));
            });

            X.DataPropertyName = "X";
            Y.DataPropertyName = "Y";
            R.DataPropertyName = "R";

            tag = pcbDoc.ActiveViewEx.MotionGfxGetTag();
            _pnts = xFS_DrawingTools.DrawingTools.GetPointsInPath(
                (object[,])pcbDoc.BoardOutline.Geometry.get_Outline(EPcbUnit.epcbUnitMils));
            myCMD = pcbDoc.Application.Gui.RegisterCommand("placeBAT", true);
            myCMD.Unit = EPcbUnit.epcbUnitMils;

            foreach (xFS_DrawingTools.DrawingTools.DoubleVector dp in _pnts)
            {
                Path _triangle = new Path();
                _triangle.Add(new ClipperLib.DoublePoint(dp.X * 100, dp.Y * 100));
                _triangle.Add(new ClipperLib.DoublePoint((dp.X + 50) * 100, (dp.Y + 100) * 100));
                _triangle.Add(new ClipperLib.DoublePoint((dp.X - 50) * 100, (dp.Y + 100) * 100));

                var path = xFS_DrawingTools.DrawingTools.getPCBPath(xFS_DrawingTools.DrawingTools.RotatePath(_triangle, dp.R, dp.X * 100, dp.Y * 100));

                pcbDoc.ActiveViewEx.MotionGfxPutPointsArray(path, EPcbMotionGfxPointType.epcbMotionGfxPointStatic, tag, 0,
                    pcbDoc.Application.Utility.NewColor(255, 255, 255), EPcbUnit.epcbUnitMils);

            }
            pcbDoc.ActiveViewEx.MotionGfxDrawAnyUndrawn();

            myCMD.OnMouseClk += myCMD_OnMouseClk;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                pcbDoc.TransactionStart(EPcbDRCMode.epcbDRCModeNone);
                foreach (xFS_DrawingTools.DrawingTools.DoubleVector dv in _selectedPnts)
                {
                    string _cellName = TabSizeSelect_Combo.SelectedItem.Text.Trim();
                    pcbDoc.CreateComponent(_cellName, _cellName, "", true).Place(
                        Math.Round(dv.X, 3),
                        Math.Round(dv.Y, 3),
                        Math.Round(dv.R, 3),
                        true,
                        EPcbAnchorType.epcbAnchorNone,
                        EPcbUnit.epcbUnitMils,
                        EPcbAngleUnit.epcbAngleUnitDegrees
                    );
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message + "\r\n" + m.Source + "\r\n" + m.StackTrace);
            }
            finally
            {
                pcbDoc.TransactionEnd();
                this.Close();
            }
        }

        private void BreakAwayTabWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            pcbDoc.Application.Gui.TerminateCommand();
            pcbDoc.ActiveViewEx.MotionGfxDeleteByTag(tag);
            ((Form)this.Owner).Show();
        }

        private void BreakAwayTabWizard_Shown(object sender, EventArgs e)
        {
            this.SetBounds(this.Owner.Right - this.Width, this.Owner.Top, this.Width, this.Height);
            ((Form)this.Owner).Hide();
        }
    }
}
