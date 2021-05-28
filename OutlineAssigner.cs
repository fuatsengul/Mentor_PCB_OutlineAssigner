#define V2
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MGCPCB;
using xFS_DrawingTools;
using ClipperLib;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace xPCB_OutlineAssigner
{

    public partial class OutlineAssigner : DarkUI.Forms.DarkForm
    {
        MGCPCB.Document pcbDoc;
        public OutlineAssigner(string appGUID)
        {
            InitializeComponent();
            #region Instance Connection Code
            try
            {
                MGCPCBReleaseEnvironmentLib.IMGCPCBReleaseEnvServer _server =
                    (MGCPCBReleaseEnvironmentLib.IMGCPCBReleaseEnvServer)Activator.CreateInstance(
                        Marshal.GetTypeFromCLSID(
                            new Guid("44983CB8-19B0-4695-937A-6FF0B74ECFC5")
                        )
                    );


                _server.SetEnvironment("");
                string VxVersion = _server.sddVersion;
                string strSDD_HOME = _server.sddHome;
                int length = strSDD_HOME.IndexOf("SDD_HOME");
                strSDD_HOME = strSDD_HOME.Substring(0, length).Replace("\\", "\\\\") + "SDD_HOME";
                _server.SetEnvironment(strSDD_HOME);
                string progID = _server.ProgIDVersion;

                object[,] _releases = (object[,])_server.GetInstalledReleases();
                dynamic pcbApp = null;

                for (int i = 1; i < _releases.Length / 4; i++)
                {
                    string _com_version = Convert.ToString(_releases[i, 0]);
                    try
                    {
                        pcbApp = Interaction.GetObject(null, "MGCPCB.Application." + _com_version);

                        pcbDoc = pcbApp.ActiveDocument;
                        dynamic licApp = Interaction.CreateObject("MGCPCBAutomationLicensing.Application." + _com_version);
                        int _token = licApp.GetToken(pcbDoc.Validate(0));
                        pcbDoc.Validate(_token);

                        break;
                    }
                    catch (Exception m)
                    {
                    }
                }


                if (pcbApp == null)
                {
                    System.Windows.Forms.MessageBox.Show("Could not found active Xpedition or PADSPro Application");
                    System.Environment.Exit(1);
                }

                

            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message + "\r\n" + m.Source + "\r\n" + m.StackTrace);
            }
            #endregion
        }


        private void UpdateUI( object sender, EventArgs e )
        {
            routeBorderOffset.Invoke(new MethodInvoker(delegate { routeBorderOffset.Enabled = generate_routeBorder.Checked; }));
            MFGOutlineOffset.Invoke(new MethodInvoker(delegate { MFGOutlineOffset.Enabled = generate_mfgOutline.Checked; }));
            drawPanelRoute.Invoke(new MethodInvoker(delegate { drawPanelRoute.Enabled = Check_BreakAwayCreatePossible(); }));
            drawPanelRoute.Invoke(new MethodInvoker(delegate { run_breakAwayTabWizardBtn.Enabled = Check_BreakAwayCreatePossible(); }));
            clearPanelRoute.Invoke(new MethodInvoker(delegate { clearPanelRoute.Enabled = !Check_BreakAwayCreatePossible(); }));
            routerRadius.Invoke(new MethodInvoker(delegate { routerRadius.Enabled = drawPanelRoute.Checked; }));
        }

        

        private void CancelButton_Click( object sender, EventArgs e )
        {
            System.Environment.Exit(1);
        }

        private void GObutton_Click( object sender, EventArgs e )
        {
            double Rwidth = 94.488;
            try
            {
                switch (routerRadius.SelectedItem.Text)
                {
                    case "2.4 mm": Rwidth = 94.488; break;
                    case "2.0 mm": Rwidth = 78.74; break;
                    case "1.6 mm": Rwidth = 62.992; break;
                    case "1.2 mm": Rwidth = 47.244; break;
                }
            }
            catch()
            {
            }
            
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += delegate
            {
                pcbDoc.SaveToTemp();
                double offset;
                try
                {
                    
                    //progressBar.Invoke(new MethodInvoker(delegate { progressBar.Visible = true; }));
                    pcbDoc.Application.Gui.SuppressTrivialDialogs = true;
                    pcbDoc.Application.Gui.ProgressBarInitialize(true, "Outline Assigner is Working", 60, 0);

                    pcbDoc.TransactionStart(EPcbDRCMode.epcbDRCModeNone);

                    #region RouteBorder
                    if (generate_routeBorder.Checked && Double.TryParse(routeBorderOffset.Text, out offset))
                    {
                        var _rb = DrawingTools.V2_Oversize((object[,])pcbDoc.BoardOutline.Geometry.get_PointsArray(EPcbUnit.epcbUnitMils), offset);

                        pcbDoc.PutRouteBorder(_rb.Length / 3, _rb, 0, EPcbUnit.epcbUnitMils);
                        generate_routeBorder.Invoke(new MethodInvoker(delegate { generate_routeBorder.Checked = false; }));
                    }
                    #endregion
                    pcbDoc.Application.Gui.ProgressBar(5);
                    #region MFG Outline
                    if (generate_mfgOutline.Checked && Double.TryParse(MFGOutlineOffset.Text, out offset))
                    {
                        {
                            object[,] rect = (object[,])pcbDoc.Application.Utility.CreateRectXYR(
                                pcbDoc.BoardOutline.Extrema.get_MinX(EPcbUnit.epcbUnitMils) - offset,
                                pcbDoc.BoardOutline.Extrema.get_MinY(EPcbUnit.epcbUnitMils) - offset,
                                pcbDoc.BoardOutline.Extrema.get_MaxX(EPcbUnit.epcbUnitMils) + offset,
                                pcbDoc.BoardOutline.Extrema.get_MaxY(EPcbUnit.epcbUnitMils) + offset);

                            pcbDoc.PutManufacturingOutline(rect.Length / 3, rect, EPcbUnit.epcbUnitMils);
                        }
                        {
                            object[,] rect = (object[,])pcbDoc.Application.Utility.CreateRectXYR(
                               pcbDoc.BoardOutline.Extrema.get_MinX(EPcbUnit.epcbUnitMils) - offset,
                               pcbDoc.BoardOutline.Extrema.get_MinY(EPcbUnit.epcbUnitMils) - offset,
                               pcbDoc.BoardOutline.Extrema.get_MaxX(EPcbUnit.epcbUnitMils) + offset,
                               pcbDoc.BoardOutline.Extrema.get_MaxY(EPcbUnit.epcbUnitMils) + offset);

                            pcbDoc.PutTestFixtureOutline(rect.Length / 3, rect, EPcbUnit.epcbUnitMils);
                        }

                        generate_mfgOutline.Invoke(new MethodInvoker(delegate { generate_mfgOutline.Checked = false; }));
                    }
                    #endregion
                    pcbDoc.Application.Gui.ProgressBar(15);
                    #region Place Origin
                    if (place_origin.Checked)
                    {
                        //place origin lower left

                        if (pcbDoc.get_OriginX(EPcbOriginType.epcbOriginBoard, EPcbUnit.epcbUnitMils) != pcbDoc.BoardOutline.Extrema.get_MinX(EPcbUnit.epcbUnitMils) ||
                             pcbDoc.get_OriginY(EPcbOriginType.epcbOriginBoard, EPcbUnit.epcbUnitMils) != pcbDoc.BoardOutline.Extrema.get_MinY(EPcbUnit.epcbUnitMils))
                        {
                            pcbDoc.Application.Gui.SuppressTrivialDialogs = true;
                            pcbDoc.set_OriginX(EPcbOriginType.epcbOriginBoard, EPcbUnit.epcbUnitMils, pcbDoc.BoardOutline.Extrema.get_MinX(EPcbUnit.epcbUnitMils));
                            pcbDoc.set_OriginY(EPcbOriginType.epcbOriginBoard, EPcbUnit.epcbUnitMils, pcbDoc.BoardOutline.Extrema.get_MinY(EPcbUnit.epcbUnitMils));
                        }

                        if (pcbDoc.get_OriginX(EPcbOriginType.epcbOriginNC, EPcbUnit.epcbUnitMils) != pcbDoc.BoardOutline.Extrema.get_MinX(EPcbUnit.epcbUnitMils) ||
                             pcbDoc.get_OriginY(EPcbOriginType.epcbOriginNC, EPcbUnit.epcbUnitMils) != pcbDoc.BoardOutline.Extrema.get_MinY(EPcbUnit.epcbUnitMils))
                        {
                            pcbDoc.set_OriginX(EPcbOriginType.epcbOriginNC, EPcbUnit.epcbUnitMils, 0);
                            pcbDoc.set_OriginY(EPcbOriginType.epcbOriginNC, EPcbUnit.epcbUnitMils, 0);
                            pcbDoc.Application.Gui.SuppressTrivialDialogs = false;
                        }
                        place_origin.Checked = false;
                    }
                    #endregion
                    pcbDoc.Application.Gui.ProgressBar(20);
                    pcbDoc.Application.Gui.ProgressBar(25);
                    pcbDoc.Application.Gui.ProgressBar(30);
                    #region Convert Contours 2 No Tool Contour
                    if (convert_contours.Checked)
                    {
                        bool thickContourExist = false;
                        bool keepThickContours = true;
                        foreach (MGCPCB.Contour _contour in pcbDoc.get_Contours(EPcbSelectionType.epcbSelectAll))
                        {
                            if (_contour.Hole.get_DrillSize(EPcbUnit.epcbUnitMils) > 2)
                            {
                                thickContourExist = true;
                                break;
                            }
                        }
                        if (thickContourExist)
                        {
                            DialogResult _res = MessageBox.Show(
                                    "Caution!\r\n" +
                                    "There's contours with thickness in the design.\r\n" +
                                    "If you press Yes, they will be thinned into its centerline\r\n" +
                                    "But they should be enlarged manually BY YOU\r\n" +
                                    "BTW: the original contours will be mirrored into layer \"Old Contours\"\r\n" +
                                    "\r\n" +
                                    "If you press No, they will remain same but still needs your attention\r\n" +
                                    "If these contours comes from Library, plase notify Library Manager\r\n" +
                                    "\r\n" +
                                    "If you click Cancel, the process will stop and nothing will be applied",
                                    "Caution about some contours",
                                    MessageBoxButtons.YesNoCancel);

                            if (_res == System.Windows.Forms.DialogResult.Yes)
                                keepThickContours = false;
                            else if (_res == System.Windows.Forms.DialogResult.Cancel)
                                return;
                        }
                        foreach (MGCPCB.Contour _contour in pcbDoc.get_Contours(EPcbSelectionType.epcbSelectAll))
                        {
                            if (_contour.Hole.get_DrillSize(EPcbUnit.epcbUnitMils) <= 2 || !keepThickContours)
                            {
                                var _outline = _contour.Geometry.get_Outline(EPcbUnit.epcbUnitCurrent);

                                if (_contour.Hole.get_DrillSize(EPcbUnit.epcbUnitMils) > 2)
                                {
                                    var LYR = pcbDoc.FindUserLayer("Old Contours");
                                    if (LYR == null)
                                        LYR = pcbDoc.SetupParameter.PutUserLayer("Old Contours");

                                    pcbDoc.PutUserLayerGfx(LYR,
                                        _contour.Hole.get_DrillSize(EPcbUnit.epcbUnitCurrent),
                                        ((object[,])_outline).Length / 3,
                                        _outline,
                                        false,
                                        _contour.Component,
                                        EPcbUnit.epcbUnitCurrent
                                    );
                                }

                                pcbDoc.PutContourEx(
                                    ((object[,])_outline).Length / 3,
                                    _outline,
                                    1,
                                    pcbDoc.LayerCount,
                                    pcbDoc.PutHole("No Tool Contour"),
                                    _contour.Compensation,
                                    EPcbContourType.epcbContourBoard,
                                    false,
                                    _contour.Component,
                                    EPcbUnit.epcbUnitCurrent
                                );
                                _contour.Delete();
                            }
                        }
                        convert_contours.Invoke(new MethodInvoker(delegate { convert_contours.Checked = false; }));
                    }
                    #endregion
                    pcbDoc.Application.Gui.ProgressBar(35);
                    #region Break Away Tab
                    if (drawPanelRoute.Checked || clearPanelRoute.Checked)
                    {
                        foreach (MGCPCB.Component comp in pcbDoc.get_Components(EPcbSelectionType.epcbSelectAll, EPcbComponentType.epcbCompAll, EPcbCelltype.epcbCelltypeDrawing, "*"))
                        {
                            if (comp.CellName.StartsWith("Break Away Tab") || comp.CellName.StartsWith("_AutoBreakAwayTab"))
                            {
                                Console.WriteLine("resetting");
                                comp.ResetCellEx(false);
                            }
                        }

                        foreach (MountingHole mh in pcbDoc.get_MountingHoles(EPcbSelectionType.epcbSelectAll))
                        {
                            var prop = mh.FindProperty("xPCB_BreakAwayRemoveFirst");
                            if (prop != null && prop.Value == "Yes")
                                mh.Delete();
                        }

                        List<object[,]> _removePaths = new List<object[,]>();
                        List<DoublePoint> _holePnts = new List<DoublePoint>();
                        foreach (UserLayerGfx _gfx in pcbDoc.get_UserLayerGfxs(EPcbSelectionType.epcbSelectAll, "Break Away Tabs", true))
                        {
                            try
                            {
                                if (_gfx.FindProperty("xPCB_BreakAwayRemoveFirst").Value == "Yes")
                                {
                                    _gfx.Delete();
                                }


                            }
                            catch { }

                            if (drawPanelRoute.Checked)
                            {
                                try
                                {
                                    if (_gfx.FindProperty("xPCB_BreakAwayRemove").Value == "Yes")
                                    {
                                        if (_gfx.Geometry.LineDisplayWidth == 0)
                                            _removePaths.Add((object[,])_gfx.Geometry.get_Outline(EPcbUnit.epcbUnitMils));
                                        _gfx.Delete();
                                    }
                                }
                                catch { }
                            }
                        }
                        if (drawPanelRoute.Checked)
                        {
                            foreach (UserLayerGfx _gfx in pcbDoc.get_UserLayerGfxs(EPcbSelectionType.epcbSelectAll, "Break Away Tabs", true))
                            {

                                if (_gfx.Geometry.IsCircle() && _gfx.Geometry.get_CircleR(EPcbUnit.epcbUnitMils) == 10)
                                {
                                    _holePnts.Add(
                                        new DoublePoint(
                                            _gfx.Geometry.get_CircleX(EPcbUnit.epcbUnitMils),
                                            _gfx.Geometry.get_CircleY(EPcbUnit.epcbUnitMils)
                                        )
                                    );
                                    //_gfx.Delete();
                                }
                            }
                        }

                        if (drawPanelRoute.Checked)
                        {

                            object[,] _outline = DrawingTools.V2_Oversize((object[,])pcbDoc.BoardOutline.Geometry.get_Outline(EPcbUnit.epcbUnitMils), Rwidth / 2); //2.4mm
                            List<object[,]> _finalLines = DrawingTools.V2_BooleanOp2Lines(_outline, _removePaths);


                            foreach (object[,] _line in _finalLines)
                            {
                                var gfx = pcbDoc.PutUserLayerGfx(pcbDoc.FindUserLayer("Break Away Tabs"),
                                    Rwidth,
                                    _line.Length / 3,
                                    _line,
                                    false,
                                    null,
                                    EPcbUnit.epcbUnitMils);
                                gfx.PutProperty("xPCB_BreakAwayRemoveFirst", "Yes");
                                gfx.FixLock = EPcbFixLockType.epcbFixLockFixed;
                            }

                            foreach (Contour cont in pcbDoc.get_Contours(EPcbSelectionType.epcbSelectAll))
                            {
                                var polygon = (object[,])cont.Geometry.get_Outline(EPcbUnit.epcbUnitMils);
                                var gfx = pcbDoc.PutUserLayerGfx(pcbDoc.FindUserLayer("Break Away Tabs"),
                                    0,
                                    polygon.Length / 3,
                                    polygon,
                                    true,
                                    null,
                                    EPcbUnit.epcbUnitMils);
                                gfx.PutProperty("xPCB_BreakAwayRemoveFirst", "Yes");
                                gfx.FixLock = EPcbFixLockType.epcbFixLockFixed;
                            }

                            foreach (DoublePoint dp in _holePnts)
                            {
                                pcbDoc.PutMountingHole(
                                    dp.X,
                                    dp.Y,
                                    pcbDoc.PutPadstack(
                                        1,
                                        pcbDoc.LayerCount,
                                        "Break Away Tab Hole",
                                        false,
                                        true
                                    ),
                                    null,
                                    null,
                                    EPcbAnchorType.epcbAnchorFixed,
                                    EPcbUnit.epcbUnitMils
                                ).PutProperty("xPCB_BreakAwayRemoveFirst", "Yes");
                            }
                        }

                        drawPanelRoute.Invoke(new MethodInvoker(delegate { drawPanelRoute.Checked = false; }));
                        clearPanelRoute.Invoke(new MethodInvoker(delegate { clearPanelRoute.Checked = false; }));
                        UpdateUI(null, null);
                    }
                    #endregion
                    pcbDoc.Application.Gui.ProgressBar(40);
                    #region Conductive Shape Cleaning
                    if (_cut_edge_conductive_shapes.Checked)
                    {
                        List<object[,]> BATRouteObstructs = new List<object[,]>();
                        foreach (MGCPCB.Component comp in pcbDoc.get_Components(EPcbSelectionType.epcbSelectAll, EPcbComponentType.epcbCompAll, EPcbCelltype.epcbCelltypeDrawing, "*"))
                        {
                            if (comp.CellName.StartsWith("Break Away Tab"))
                            {
                                BATRouteObstructs.Add((object[,])comp.get_Obstructs(EPcbObstructType.epcbObstructAll)[1].Geometry.get_PointsArray(EPcbUnit.epcbUnitMils));
                            }
                        }


                        foreach (ConductiveArea _area in pcbDoc.get_ConductiveAreas(EPcbSelectionType.epcbSelectAll))
                        {
                            bool touch = false;
                            foreach (object[,] BAT in BATRouteObstructs)
                            {
                                if (xFS_DrawingTools.DrawingTools.Touches(BAT, (object[,])_area.Geometry.get_PointsArray(EPcbUnit.epcbUnitMils)))
                                {
                                    touch = true;
                                    break;
                                }
                            }
                            if (touch)
                            {
                                List<object[,]> _returnAreas = xFS_DrawingTools.DrawingTools.V2_CutShape(
                                    (object[,])_area.Geometry.get_PointsArray(EPcbUnit.epcbUnitMils),
                                    null,
                                    BATRouteObstructs,
                                    0,
                                    0
                                );

                                if (_returnAreas.Count >= 1)
                                {
                                    _area.Geometry.set_PointsArray(EPcbUnit.epcbUnitMils, _returnAreas[0]);

                                    for (int i = 1; i < _returnAreas.Count; i++)
                                    {
                                        pcbDoc.PutConductiveArea(_area.Layer,
                                            _area.Net,
                                            _returnAreas[i].Length / 3,
                                            _returnAreas[i],
                                            _area.Component,
                                            EPcbAnchorType.epcbAnchorNone,
                                            EPcbUnit.epcbUnitMils
                                        );
                                    }
                                }
                            }
                        }

                        _cut_edge_conductive_shapes.Invoke(new MethodInvoker(delegate { _cut_edge_conductive_shapes.Checked = false; }));
                    }
                    #endregion
                    pcbDoc.Application.Gui.ProgressBar(45);
                    
                    pcbDoc.Application.Gui.ProgressBar(50);
                    #region Delete Unused Padstacks
                    if (remove_padstacks.Checked)
                    {
                        PadstackEditorLib.PadstackEditorDlg pEditor = (PadstackEditorLib.PadstackEditorDlg)pcbDoc.PadstackEditor;
                        foreach (PadstackEditorLib.Padstack ps in pEditor.ActiveDatabase.Padstacks)
                        {
                            try
                            {
                                ps.Delete();
                            }
                            catch
                            {
                                Console.WriteLine("Cannot delete:", ps.Name);
                            }
                        }

                        foreach (PadstackEditorLib.Hole _h in pEditor.ActiveDatabase.Holes)
                        {
                            try
                            {
                                _h.Delete();
                            }
                            catch
                            {
                                Console.WriteLine("Cannot delete hole:", _h.Name);
                            }
                        }

                        foreach (PadstackEditorLib.Pad _p in pEditor.ActiveDatabase.Pads)
                        {
                            try
                            {
                                _p.Delete();
                            }
                            catch
                            {
                                Console.WriteLine("Cannot delete pad:", _p.Name);
                            }
                        }

                        pEditor.SaveActiveDatabase();
                        pEditor.Quit();

                        remove_padstacks.Invoke(new MethodInvoker(delegate { remove_padstacks.Checked = false; }));
                    }
                    #endregion
                    pcbDoc.Application.Gui.ProgressBar(55);
                    #region Delete Unused Layers
                    if (remove_userlayers.Checked)
                    {
                        try
                        {
                            pcbDoc.TransactionStart(EPcbDRCMode.epcbDRCModeNone);
                            foreach (UserLayer ul in pcbDoc.UserLayers)
                            {
                                if (pcbDoc.get_UserLayerGfxs(EPcbSelectionType.epcbSelectAll, ul.Name, true).Count == 0 &&
                                    pcbDoc.get_UserLayerTexts(EPcbSelectionType.epcbSelectAll, ul.Name, true).Count == 0)
                                {
                                    ul.Delete();
                                }
                            }
                        }
                        catch (Exception m)
                        {
                            Console.WriteLine(m.Message);
                        }
                        finally
                        {
                            pcbDoc.TransactionEnd();
                            remove_userlayers.Checked = false;
                        }
                    }
                    #endregion
                }
                catch (Exception m)
                {
                    MessageBox.Show(m.Message + "\r\n" + m.StackTrace);
                }
                finally
                {
                    pcbDoc.TransactionEnd();
                    pcbDoc.Application.Gui.SuppressTrivialDialogs = false;
                    
                    //progressBar.Invoke(new MethodInvoker(delegate { progressBar.Visible = false; }));
                }
            };
            bw.RunWorkerCompleted += delegate
            {
                pcbDoc.Application.Gui.ProgressBarInitialize(true, "Process Completed");
                pcbDoc.Application.Gui.ProgressBarInitialize(false, "Process Completed");
                this.Invoke(new MethodInvoker(delegate {
                    MessageBox.Show(this, "Process Completed", "Outline Assigner", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }));
            };

            bw.RunWorkerAsync();
        }



        private void PanelRoute_CheckChanged( object sender, EventArgs e )
        {
            var cb = ((System.Windows.Forms.CheckBox)sender);


            if ( cb.Checked )
            {
                if ( cb.Name.StartsWith("draw") ) clearPanelRoute.Checked = false;
                else if ( cb.Name.StartsWith("clear") ) drawPanelRoute.Checked = false;
            }
            UpdateUI(sender, e);
        }

        private bool Check_BreakAwayCreatePossible()
        {
            bool found = false;
            foreach ( UserLayerGfx _gfx in pcbDoc.get_UserLayerGfxs(EPcbSelectionType.epcbSelectAll, "Break Away Tabs", true) )
            {
                try
                {
                    if ( _gfx.FindProperty("xPCB_BreakAwayRemoveFirst") != null && _gfx.FindProperty("xPCB_BreakAwayRemoveFirst").Value == "Yes" )
                    {
                        found = true;
                        break;
                    }
                }
                catch { }
            }
            return !found;
        }

        private void OutlineAssigner_Shown( object sender, EventArgs e )
        {
            UpdateUI(null, null);
        }

        private void run_breakAwayTabWizardBtn_Click(object sender, EventArgs e)
        {
            BreakAwayTabWizard wizard = new BreakAwayTabWizard(pcbDoc);
            if (wizard.ready)
            {
                wizard.TopMost = true;
                wizard.ShowDialog(this);
                this.Focus();
            }
            else
                wizard.Dispose();

        }

        private void OutlineAssigner_Load(object sender, EventArgs e)
        {

        }

        private void OutlineAssigner_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            MessageBox.Show(
                "Outline Assigner\r\n" +
                "\r\n" +
                "Milbitt Engineering Inc.\r\n" +
                "www.milbitt.com",
                "About",
                MessageBoxButtons.OK
           );
        }
    }
}
