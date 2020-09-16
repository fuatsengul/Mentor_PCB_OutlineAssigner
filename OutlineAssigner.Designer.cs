namespace xPCB_OutlineAssigner
{
    partial class OutlineAssigner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.generate_routeBorder = new DarkUI.Controls.DarkCheckBox();
            this.routeBorderOffset = new DarkUI.Controls.DarkTextBox();
            this.metroPanel1 = new DarkUI.Controls.DarkPanel();
            this.metroLabel1 = new DarkUI.Controls.DarkLabel();
            this.metroPanel2 = new DarkUI.Controls.DarkPanel();
            this.metroLabel2 = new DarkUI.Controls.DarkLabel();
            this.MFGOutlineOffset = new DarkUI.Controls.DarkTextBox();
            this.generate_mfgOutline = new DarkUI.Controls.DarkCheckBox();
            this.metroPanel4 = new DarkUI.Controls.DarkPanel();
            this.place_origin = new DarkUI.Controls.DarkCheckBox();
            this.GOButton = new DarkUI.Controls.DarkButton();
            this.metroPanel7 = new DarkUI.Controls.DarkPanel();
            this.convert_contours = new DarkUI.Controls.DarkCheckBox();
            this.metroPanel8 = new DarkUI.Controls.DarkPanel();
            this.routerRadius = new DarkUI.Controls.DarkDropdownList();
            this.drawPanelRoute = new DarkUI.Controls.DarkCheckBox();
            this.run_breakAwayTabWizardBtn = new DarkUI.Controls.DarkButton();
            this.metroPanel9 = new DarkUI.Controls.DarkPanel();
            this._cut_edge_conductive_shapes = new DarkUI.Controls.DarkCheckBox();
            this.metroPanel12 = new DarkUI.Controls.DarkPanel();
            this.remove_padstacks = new DarkUI.Controls.DarkCheckBox();
            this.metroPanel13 = new DarkUI.Controls.DarkPanel();
            this.remove_userlayers = new DarkUI.Controls.DarkCheckBox();
            this.darkPanel1 = new DarkUI.Controls.DarkPanel();
            this.clearPanelRoute = new DarkUI.Controls.DarkCheckBox();
            this.darkPanel2 = new DarkUI.Controls.DarkPanel();
            this.metroPanel1.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            this.metroPanel4.SuspendLayout();
            this.metroPanel7.SuspendLayout();
            this.metroPanel8.SuspendLayout();
            this.metroPanel9.SuspendLayout();
            this.metroPanel12.SuspendLayout();
            this.metroPanel13.SuspendLayout();
            this.darkPanel1.SuspendLayout();
            this.darkPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // generate_routeBorder
            // 
            this.generate_routeBorder.Dock = System.Windows.Forms.DockStyle.Left;
            this.generate_routeBorder.Location = new System.Drawing.Point(3, 2);
            this.generate_routeBorder.Name = "generate_routeBorder";
            this.generate_routeBorder.Size = new System.Drawing.Size(184, 22);
            this.generate_routeBorder.TabIndex = 0;
            this.generate_routeBorder.Text = "Adjust Route Border";
            this.generate_routeBorder.UseMnemonic = false;
            this.generate_routeBorder.CheckedChanged += new System.EventHandler(this.UpdateUI);
            // 
            // routeBorderOffset
            // 
            this.routeBorderOffset.Dock = System.Windows.Forms.DockStyle.Left;
            this.routeBorderOffset.Location = new System.Drawing.Point(187, 2);
            this.routeBorderOffset.Name = "routeBorderOffset";
            this.routeBorderOffset.Size = new System.Drawing.Size(40, 22);
            this.routeBorderOffset.TabIndex = 1;
            this.routeBorderOffset.Text = "-12";
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderSides = System.Windows.Forms.AnchorStyles.None;
            this.metroPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel1.Controls.Add(this.metroLabel1);
            this.metroPanel1.Controls.Add(this.routeBorderOffset);
            this.metroPanel1.Controls.Add(this.generate_routeBorder);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel1.Location = new System.Drawing.Point(0, 5);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Padding = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.metroPanel1.Size = new System.Drawing.Size(261, 26);
            this.metroPanel1.TabIndex = 2;
            // 
            // metroLabel1
            // 
            this.metroLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.metroLabel1.Location = new System.Drawing.Point(227, 2);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(71, 22);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "mils";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // metroPanel2
            // 
            this.metroPanel2.BorderSides = System.Windows.Forms.AnchorStyles.Top;
            this.metroPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel2.Controls.Add(this.metroLabel2);
            this.metroPanel2.Controls.Add(this.MFGOutlineOffset);
            this.metroPanel2.Controls.Add(this.generate_mfgOutline);
            this.metroPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel2.Location = new System.Drawing.Point(0, 31);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Padding = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.metroPanel2.Size = new System.Drawing.Size(261, 26);
            this.metroPanel2.TabIndex = 3;
            // 
            // metroLabel2
            // 
            this.metroLabel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.metroLabel2.Location = new System.Drawing.Point(227, 2);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(71, 22);
            this.metroLabel2.TabIndex = 2;
            this.metroLabel2.Text = "mils";
            this.metroLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MFGOutlineOffset
            // 
            this.MFGOutlineOffset.Dock = System.Windows.Forms.DockStyle.Left;
            this.MFGOutlineOffset.Location = new System.Drawing.Point(187, 2);
            this.MFGOutlineOffset.Name = "MFGOutlineOffset";
            this.MFGOutlineOffset.Size = new System.Drawing.Size(40, 22);
            this.MFGOutlineOffset.TabIndex = 1;
            this.MFGOutlineOffset.Text = "400";
            // 
            // generate_mfgOutline
            // 
            this.generate_mfgOutline.Dock = System.Windows.Forms.DockStyle.Left;
            this.generate_mfgOutline.Location = new System.Drawing.Point(3, 2);
            this.generate_mfgOutline.Name = "generate_mfgOutline";
            this.generate_mfgOutline.Size = new System.Drawing.Size(184, 22);
            this.generate_mfgOutline.TabIndex = 0;
            this.generate_mfgOutline.Text = "Adjust Manufacturing Outlines";
            this.generate_mfgOutline.UseMnemonic = false;
            this.generate_mfgOutline.CheckedChanged += new System.EventHandler(this.UpdateUI);
            // 
            // metroPanel4
            // 
            this.metroPanel4.BorderSides = System.Windows.Forms.AnchorStyles.Top;
            this.metroPanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel4.Controls.Add(this.place_origin);
            this.metroPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel4.Location = new System.Drawing.Point(0, 57);
            this.metroPanel4.Name = "metroPanel4";
            this.metroPanel4.Padding = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.metroPanel4.Size = new System.Drawing.Size(261, 26);
            this.metroPanel4.TabIndex = 5;
            // 
            // place_origin
            // 
            this.place_origin.Dock = System.Windows.Forms.DockStyle.Top;
            this.place_origin.Location = new System.Drawing.Point(3, 2);
            this.place_origin.Name = "place_origin";
            this.place_origin.Size = new System.Drawing.Size(248, 26);
            this.place_origin.TabIndex = 0;
            this.place_origin.Text = "Place board origin to bottom left corner";
            this.place_origin.UseMnemonic = false;
            // 
            // GOButton
            // 
            this.GOButton.BorderSides = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GOButton.Checked = false;
            this.GOButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.GOButton.Location = new System.Drawing.Point(184, 7);
            this.GOButton.Name = "GOButton";
            this.GOButton.Size = new System.Drawing.Size(67, 21);
            this.GOButton.TabIndex = 6;
            this.GOButton.TabStop = false;
            this.GOButton.Text = "Run";
            this.GOButton.Click += new System.EventHandler(this.GObutton_Click);
            // 
            // metroPanel7
            // 
            this.metroPanel7.BorderSides = System.Windows.Forms.AnchorStyles.Top;
            this.metroPanel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel7.Controls.Add(this.convert_contours);
            this.metroPanel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel7.Location = new System.Drawing.Point(0, 83);
            this.metroPanel7.Name = "metroPanel7";
            this.metroPanel7.Padding = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.metroPanel7.Size = new System.Drawing.Size(261, 26);
            this.metroPanel7.TabIndex = 6;
            // 
            // convert_contours
            // 
            this.convert_contours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.convert_contours.Location = new System.Drawing.Point(3, 2);
            this.convert_contours.Name = "convert_contours";
            this.convert_contours.Size = new System.Drawing.Size(248, 22);
            this.convert_contours.TabIndex = 0;
            this.convert_contours.Text = "Convert All Contours to No Tool Contour";
            this.convert_contours.UseMnemonic = false;
            // 
            // metroPanel8
            // 
            this.metroPanel8.BorderSides = System.Windows.Forms.AnchorStyles.Top;
            this.metroPanel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel8.Controls.Add(this.routerRadius);
            this.metroPanel8.Controls.Add(this.drawPanelRoute);
            this.metroPanel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel8.Location = new System.Drawing.Point(0, 161);
            this.metroPanel8.Name = "metroPanel8";
            this.metroPanel8.Padding = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.metroPanel8.Size = new System.Drawing.Size(261, 26);
            this.metroPanel8.TabIndex = 7;
            // 
            // routerRadius
            // 
            this.routerRadius.Location = new System.Drawing.Point(145, 3);
            this.routerRadius.Name = "routerRadius";
            this.routerRadius.Size = new System.Drawing.Size(105, 21);
            this.routerRadius.TabIndex = 0;
            // 
            // drawPanelRoute
            // 
            this.drawPanelRoute.Dock = System.Windows.Forms.DockStyle.Left;
            this.drawPanelRoute.Location = new System.Drawing.Point(3, 2);
            this.drawPanelRoute.Name = "drawPanelRoute";
            this.drawPanelRoute.Size = new System.Drawing.Size(129, 22);
            this.drawPanelRoute.TabIndex = 0;
            this.drawPanelRoute.Text = "Draw Route Path";
            this.drawPanelRoute.UseMnemonic = false;
            this.drawPanelRoute.CheckedChanged += new System.EventHandler(this.PanelRoute_CheckChanged);
            // 
            // run_breakAwayTabWizardBtn
            // 
            this.run_breakAwayTabWizardBtn.BorderSides = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.run_breakAwayTabWizardBtn.Checked = false;
            this.run_breakAwayTabWizardBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.run_breakAwayTabWizardBtn.Location = new System.Drawing.Point(3, 7);
            this.run_breakAwayTabWizardBtn.Name = "run_breakAwayTabWizardBtn";
            this.run_breakAwayTabWizardBtn.Size = new System.Drawing.Size(139, 21);
            this.run_breakAwayTabWizardBtn.TabIndex = 7;
            this.run_breakAwayTabWizardBtn.TabStop = false;
            this.run_breakAwayTabWizardBtn.Text = "Break Away Tab Wizard";
            this.run_breakAwayTabWizardBtn.Click += new System.EventHandler(this.run_breakAwayTabWizardBtn_Click);
            // 
            // metroPanel9
            // 
            this.metroPanel9.BorderSides = System.Windows.Forms.AnchorStyles.Top;
            this.metroPanel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel9.Controls.Add(this._cut_edge_conductive_shapes);
            this.metroPanel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel9.Location = new System.Drawing.Point(0, 187);
            this.metroPanel9.Name = "metroPanel9";
            this.metroPanel9.Padding = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.metroPanel9.Size = new System.Drawing.Size(261, 26);
            this.metroPanel9.TabIndex = 10;
            // 
            // _cut_edge_conductive_shapes
            // 
            this._cut_edge_conductive_shapes.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cut_edge_conductive_shapes.Location = new System.Drawing.Point(3, 2);
            this._cut_edge_conductive_shapes.Name = "_cut_edge_conductive_shapes";
            this._cut_edge_conductive_shapes.Size = new System.Drawing.Size(248, 22);
            this._cut_edge_conductive_shapes.TabIndex = 0;
            this._cut_edge_conductive_shapes.Text = "Remove conductors crossed with route path";
            this._cut_edge_conductive_shapes.UseMnemonic = false;
            // 
            // metroPanel12
            // 
            this.metroPanel12.BorderSides = System.Windows.Forms.AnchorStyles.Top;
            this.metroPanel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel12.Controls.Add(this.remove_padstacks);
            this.metroPanel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel12.Location = new System.Drawing.Point(0, 109);
            this.metroPanel12.Name = "metroPanel12";
            this.metroPanel12.Padding = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.metroPanel12.Size = new System.Drawing.Size(261, 26);
            this.metroPanel12.TabIndex = 12;
            // 
            // remove_padstacks
            // 
            this.remove_padstacks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remove_padstacks.Location = new System.Drawing.Point(3, 2);
            this.remove_padstacks.Name = "remove_padstacks";
            this.remove_padstacks.Size = new System.Drawing.Size(248, 22);
            this.remove_padstacks.TabIndex = 0;
            this.remove_padstacks.Text = "Remove Unused Padstacks / Pads / Holes";
            this.remove_padstacks.UseMnemonic = false;
            // 
            // metroPanel13
            // 
            this.metroPanel13.BorderSides = System.Windows.Forms.AnchorStyles.Top;
            this.metroPanel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel13.Controls.Add(this.remove_userlayers);
            this.metroPanel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel13.Location = new System.Drawing.Point(0, 135);
            this.metroPanel13.Name = "metroPanel13";
            this.metroPanel13.Padding = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.metroPanel13.Size = new System.Drawing.Size(261, 26);
            this.metroPanel13.TabIndex = 13;
            // 
            // remove_userlayers
            // 
            this.remove_userlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remove_userlayers.Location = new System.Drawing.Point(3, 2);
            this.remove_userlayers.Name = "remove_userlayers";
            this.remove_userlayers.Size = new System.Drawing.Size(248, 22);
            this.remove_userlayers.TabIndex = 0;
            this.remove_userlayers.Text = "Remove Unused User Layers";
            this.remove_userlayers.UseMnemonic = false;
            // 
            // darkPanel1
            // 
            this.darkPanel1.BorderSides = System.Windows.Forms.AnchorStyles.Top;
            this.darkPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.darkPanel1.Controls.Add(this.clearPanelRoute);
            this.darkPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.darkPanel1.Location = new System.Drawing.Point(0, 213);
            this.darkPanel1.Name = "darkPanel1";
            this.darkPanel1.Padding = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.darkPanel1.Size = new System.Drawing.Size(261, 26);
            this.darkPanel1.TabIndex = 12;
            // 
            // clearPanelRoute
            // 
            this.clearPanelRoute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clearPanelRoute.Location = new System.Drawing.Point(3, 2);
            this.clearPanelRoute.Name = "clearPanelRoute";
            this.clearPanelRoute.Size = new System.Drawing.Size(248, 22);
            this.clearPanelRoute.TabIndex = 3;
            this.clearPanelRoute.Text = "Clear Route Path";
            this.clearPanelRoute.UseMnemonic = false;
            // 
            // darkPanel2
            // 
            this.darkPanel2.BorderSides = System.Windows.Forms.AnchorStyles.Top;
            this.darkPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.darkPanel2.Controls.Add(this.run_breakAwayTabWizardBtn);
            this.darkPanel2.Controls.Add(this.GOButton);
            this.darkPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.darkPanel2.Location = new System.Drawing.Point(0, 239);
            this.darkPanel2.Name = "darkPanel2";
            this.darkPanel2.Padding = new System.Windows.Forms.Padding(3, 7, 10, 6);
            this.darkPanel2.Size = new System.Drawing.Size(261, 34);
            this.darkPanel2.TabIndex = 14;
            // 
            // OutlineAssigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 273);
            this.Controls.Add(this.darkPanel2);
            this.Controls.Add(this.darkPanel1);
            this.Controls.Add(this.metroPanel9);
            this.Controls.Add(this.metroPanel8);
            this.Controls.Add(this.metroPanel13);
            this.Controls.Add(this.metroPanel12);
            this.Controls.Add(this.metroPanel7);
            this.Controls.Add(this.metroPanel4);
            this.Controls.Add(this.metroPanel2);
            this.Controls.Add(this.metroPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OutlineAssigner";
            this.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Outline Assigner";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OutlineAssigner_Load);
            this.Shown += new System.EventHandler(this.OutlineAssigner_Shown);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.metroPanel4.ResumeLayout(false);
            this.metroPanel7.ResumeLayout(false);
            this.metroPanel8.ResumeLayout(false);
            this.metroPanel9.ResumeLayout(false);
            this.metroPanel12.ResumeLayout(false);
            this.metroPanel13.ResumeLayout(false);
            this.darkPanel1.ResumeLayout(false);
            this.darkPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkCheckBox generate_routeBorder;
        private DarkUI.Controls.DarkTextBox routeBorderOffset;
        private DarkUI.Controls.DarkPanel metroPanel1;
        private DarkUI.Controls.DarkLabel metroLabel1;
        private DarkUI.Controls.DarkPanel metroPanel2;
        private DarkUI.Controls.DarkLabel metroLabel2;
        private DarkUI.Controls.DarkTextBox MFGOutlineOffset;
        private DarkUI.Controls.DarkCheckBox generate_mfgOutline;
        private DarkUI.Controls.DarkPanel metroPanel4;
        private DarkUI.Controls.DarkCheckBox place_origin;
        private DarkUI.Controls.DarkButton GOButton;
        private DarkUI.Controls.DarkPanel metroPanel7;
        private DarkUI.Controls.DarkCheckBox convert_contours;
        private DarkUI.Controls.DarkPanel metroPanel8;
        private DarkUI.Controls.DarkCheckBox drawPanelRoute;
        private DarkUI.Controls.DarkButton run_breakAwayTabWizardBtn;
        private DarkUI.Controls.DarkDropdownList routerRadius;
        private DarkUI.Controls.DarkPanel metroPanel9;
        private DarkUI.Controls.DarkCheckBox _cut_edge_conductive_shapes;
        private DarkUI.Controls.DarkPanel metroPanel12;
        private DarkUI.Controls.DarkCheckBox remove_padstacks;
        private DarkUI.Controls.DarkPanel metroPanel13;
        private DarkUI.Controls.DarkCheckBox remove_userlayers;
        private DarkUI.Controls.DarkPanel darkPanel1;
        private DarkUI.Controls.DarkCheckBox clearPanelRoute;
        private DarkUI.Controls.DarkPanel darkPanel2;
    }
}