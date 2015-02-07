using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.DirectSound;

namespace AIoLoCC
{
  public class MainForm : System.Windows.Forms.Form
  {
    private IContainer components;

    private Microsoft.DirectX.DirectSound.Device dxSoundDevice = null;
    private TrackBar tbVolume;
    private Label lblAmbient;
    private TrackBar tbCPUOverride;
    private CheckBox chkCPUOverride;
    private ZedGraph.ZedGraphControl zedGraph1;
    private Label label1;
    private Microsoft.DirectX.DirectSound.SecondaryBuffer soundBuffer = null;
    private CheckBox chkRAMOverride;
    private TrackBar tbRAMOverride;
    private CheckBox chkPlay;
    private GroupBox gbCPU;
    private GroupBox gbRAM;
    private TrackBar tbCPUSensitivity;
    private Label lblCPUSensitivity;
    private Label lblCPUSensitivityValue;
    private Label lblRAMSensitivityValue;
    private TrackBar tbRAMSensitivity;
    private Label lblRAMSensitivity;
    private TrackBar tbCPUFrequency;
    private Label lblRAMFrequencyValue;
    private Label lblRAMFrequency;
    private TrackBar tbRAMFrequency;
    private Label lblCPUFrequencyValue;
    private Label lblCPUFrequency;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel lblStatus;
    private NotifyIcon niTray;

    private Ambient m_ambient = new Ambient();

    public MainForm()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();

      m_ambient.OnTrigger += HandleOnTrigger;

      tbCPUSensitivity.Value = (int)(dDeltaMin_CPU * (double)(RATE));
      lblCPUSensitivityValue.Text = String.Format("{0}%", (int)(dDeltaMin_CPU * (double)(RATE)));

      tbRAMSensitivity.Value = (int)(dDeltaMin_RAM / 1000000d * (double)(RATE));
      lblRAMSensitivityValue.Text = String.Format("{0}MB", (int)(dDeltaMin_RAM / 1000000d * (double)(RATE)));

      tbCPUFrequency.Value = (int)(dHz_CPU);
      lblCPUFrequencyValue.Text = String.Format("{0}Hz", dHz_CPU);

      tbRAMFrequency.Value = (int)(dHz_RAM);
      lblRAMFrequencyValue.Text = String.Format("{0}Hz", dHz_RAM);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (components != null) { components.Dispose(); }
        if (dxSoundDevice != null) { dxSoundDevice.Dispose(); }
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.tbVolume = new System.Windows.Forms.TrackBar();
      this.lblAmbient = new System.Windows.Forms.Label();
      this.tbCPUOverride = new System.Windows.Forms.TrackBar();
      this.chkCPUOverride = new System.Windows.Forms.CheckBox();
      this.zedGraph1 = new ZedGraph.ZedGraphControl();
      this.label1 = new System.Windows.Forms.Label();
      this.chkRAMOverride = new System.Windows.Forms.CheckBox();
      this.tbRAMOverride = new System.Windows.Forms.TrackBar();
      this.chkPlay = new System.Windows.Forms.CheckBox();
      this.gbCPU = new System.Windows.Forms.GroupBox();
      this.lblCPUFrequencyValue = new System.Windows.Forms.Label();
      this.lblCPUFrequency = new System.Windows.Forms.Label();
      this.tbCPUFrequency = new System.Windows.Forms.TrackBar();
      this.lblCPUSensitivityValue = new System.Windows.Forms.Label();
      this.tbCPUSensitivity = new System.Windows.Forms.TrackBar();
      this.lblCPUSensitivity = new System.Windows.Forms.Label();
      this.gbRAM = new System.Windows.Forms.GroupBox();
      this.lblRAMFrequencyValue = new System.Windows.Forms.Label();
      this.lblRAMFrequency = new System.Windows.Forms.Label();
      this.tbRAMFrequency = new System.Windows.Forms.TrackBar();
      this.lblRAMSensitivityValue = new System.Windows.Forms.Label();
      this.tbRAMSensitivity = new System.Windows.Forms.TrackBar();
      this.lblRAMSensitivity = new System.Windows.Forms.Label();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
      this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tbCPUOverride)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tbRAMOverride)).BeginInit();
      this.gbCPU.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tbCPUFrequency)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tbCPUSensitivity)).BeginInit();
      this.gbRAM.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tbRAMFrequency)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tbRAMSensitivity)).BeginInit();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tbVolume
      // 
      this.tbVolume.AutoSize = false;
      this.tbVolume.LargeChange = 1000;
      this.tbVolume.Location = new System.Drawing.Point(385, 12);
      this.tbVolume.Maximum = 0;
      this.tbVolume.Minimum = -5000;
      this.tbVolume.Name = "tbVolume";
      this.tbVolume.Size = new System.Drawing.Size(468, 23);
      this.tbVolume.SmallChange = 100;
      this.tbVolume.TabIndex = 3;
      this.tbVolume.TickFrequency = 100;
      this.tbVolume.TickStyle = System.Windows.Forms.TickStyle.None;
      this.tbVolume.Value = -2000;
      this.tbVolume.ValueChanged += new System.EventHandler(this.tbVolume_ValueChanged);
      // 
      // lblAmbient
      // 
      this.lblAmbient.AutoSize = true;
      this.lblAmbient.Location = new System.Drawing.Point(103, 17);
      this.lblAmbient.Name = "lblAmbient";
      this.lblAmbient.Size = new System.Drawing.Size(55, 13);
      this.lblAmbient.TabIndex = 1;
      this.lblAmbient.Text = "lblAmbient";
      // 
      // tbCPUOverride
      // 
      this.tbCPUOverride.AutoSize = false;
      this.tbCPUOverride.Enabled = false;
      this.tbCPUOverride.Location = new System.Drawing.Point(114, 19);
      this.tbCPUOverride.Maximum = 100;
      this.tbCPUOverride.Minimum = 1;
      this.tbCPUOverride.Name = "tbCPUOverride";
      this.tbCPUOverride.Size = new System.Drawing.Size(294, 25);
      this.tbCPUOverride.TabIndex = 5;
      this.tbCPUOverride.TickStyle = System.Windows.Forms.TickStyle.None;
      this.tbCPUOverride.Value = 1;
      // 
      // chkCPUOverride
      // 
      this.chkCPUOverride.AutoSize = true;
      this.chkCPUOverride.Location = new System.Drawing.Point(6, 19);
      this.chkCPUOverride.Name = "chkCPUOverride";
      this.chkCPUOverride.Size = new System.Drawing.Size(102, 17);
      this.chkCPUOverride.TabIndex = 4;
      this.chkCPUOverride.Text = "Manual override";
      this.chkCPUOverride.UseVisualStyleBackColor = true;
      this.chkCPUOverride.CheckedChanged += new System.EventHandler(this.chkManualCPU_CheckedChanged);
      // 
      // zedGraph1
      // 
      this.zedGraph1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.zedGraph1.AutoSize = true;
      this.zedGraph1.IsAutoScrollRange = true;
      this.zedGraph1.Location = new System.Drawing.Point(12, 171);
      this.zedGraph1.Name = "zedGraph1";
      this.zedGraph1.ScrollGrace = 0;
      this.zedGraph1.ScrollMaxX = 0;
      this.zedGraph1.ScrollMaxY = 0;
      this.zedGraph1.ScrollMaxY2 = 0;
      this.zedGraph1.ScrollMinX = 0;
      this.zedGraph1.ScrollMinY = 0;
      this.zedGraph1.ScrollMinY2 = 0;
      this.zedGraph1.Size = new System.Drawing.Size(841, 319);
      this.zedGraph1.TabIndex = 8;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(334, 17);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(45, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Volume:";
      // 
      // chkRAMOverride
      // 
      this.chkRAMOverride.AutoSize = true;
      this.chkRAMOverride.Location = new System.Drawing.Point(6, 19);
      this.chkRAMOverride.Name = "chkRAMOverride";
      this.chkRAMOverride.Size = new System.Drawing.Size(102, 17);
      this.chkRAMOverride.TabIndex = 6;
      this.chkRAMOverride.Text = "Manual override";
      this.chkRAMOverride.UseVisualStyleBackColor = true;
      this.chkRAMOverride.CheckedChanged += new System.EventHandler(this.chkManualRAM_CheckedChanged);
      // 
      // tbRAMOverride
      // 
      this.tbRAMOverride.AutoSize = false;
      this.tbRAMOverride.Enabled = false;
      this.tbRAMOverride.Location = new System.Drawing.Point(114, 19);
      this.tbRAMOverride.Maximum = 100;
      this.tbRAMOverride.Minimum = 1;
      this.tbRAMOverride.Name = "tbRAMOverride";
      this.tbRAMOverride.Size = new System.Drawing.Size(294, 25);
      this.tbRAMOverride.TabIndex = 7;
      this.tbRAMOverride.TickStyle = System.Windows.Forms.TickStyle.None;
      this.tbRAMOverride.Value = 1;
      // 
      // chkPlay
      // 
      this.chkPlay.Appearance = System.Windows.Forms.Appearance.Button;
      this.chkPlay.AutoSize = true;
      this.chkPlay.Checked = true;
      this.chkPlay.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkPlay.Location = new System.Drawing.Point(12, 12);
      this.chkPlay.Name = "chkPlay";
      this.chkPlay.Size = new System.Drawing.Size(39, 23);
      this.chkPlay.TabIndex = 0;
      this.chkPlay.Text = "Stop";
      this.chkPlay.UseVisualStyleBackColor = true;
      this.chkPlay.CheckedChanged += new System.EventHandler(this.chkPlay_CheckedChanged);
      // 
      // gbCPU
      // 
      this.gbCPU.Controls.Add(this.lblCPUFrequencyValue);
      this.gbCPU.Controls.Add(this.lblCPUFrequency);
      this.gbCPU.Controls.Add(this.tbCPUFrequency);
      this.gbCPU.Controls.Add(this.lblCPUSensitivityValue);
      this.gbCPU.Controls.Add(this.tbCPUSensitivity);
      this.gbCPU.Controls.Add(this.lblCPUSensitivity);
      this.gbCPU.Controls.Add(this.chkCPUOverride);
      this.gbCPU.Controls.Add(this.tbCPUOverride);
      this.gbCPU.Location = new System.Drawing.Point(12, 41);
      this.gbCPU.Name = "gbCPU";
      this.gbCPU.Size = new System.Drawing.Size(414, 124);
      this.gbCPU.TabIndex = 9;
      this.gbCPU.TabStop = false;
      this.gbCPU.Text = "Processor";
      // 
      // lblCPUFrequencyValue
      // 
      this.lblCPUFrequencyValue.AutoSize = true;
      this.lblCPUFrequencyValue.Location = new System.Drawing.Point(66, 82);
      this.lblCPUFrequencyValue.Name = "lblCPUFrequencyValue";
      this.lblCPUFrequencyValue.Size = new System.Drawing.Size(20, 13);
      this.lblCPUFrequencyValue.TabIndex = 16;
      this.lblCPUFrequencyValue.Text = "Hz";
      // 
      // lblCPUFrequency
      // 
      this.lblCPUFrequency.AutoSize = true;
      this.lblCPUFrequency.Location = new System.Drawing.Point(6, 82);
      this.lblCPUFrequency.Name = "lblCPUFrequency";
      this.lblCPUFrequency.Size = new System.Drawing.Size(57, 13);
      this.lblCPUFrequency.TabIndex = 15;
      this.lblCPUFrequency.Text = "Frequency";
      // 
      // tbCPUFrequency
      // 
      this.tbCPUFrequency.AutoSize = false;
      this.tbCPUFrequency.Location = new System.Drawing.Point(114, 81);
      this.tbCPUFrequency.Maximum = 20000;
      this.tbCPUFrequency.Minimum = 50;
      this.tbCPUFrequency.Name = "tbCPUFrequency";
      this.tbCPUFrequency.Size = new System.Drawing.Size(294, 25);
      this.tbCPUFrequency.TabIndex = 9;
      this.tbCPUFrequency.TickStyle = System.Windows.Forms.TickStyle.None;
      this.tbCPUFrequency.Value = 50;
      this.tbCPUFrequency.ValueChanged += new System.EventHandler(this.tbCPUFrequency_ValueChanged);
      // 
      // lblCPUSensitivityValue
      // 
      this.lblCPUSensitivityValue.AutoSize = true;
      this.lblCPUSensitivityValue.Location = new System.Drawing.Point(66, 50);
      this.lblCPUSensitivityValue.Name = "lblCPUSensitivityValue";
      this.lblCPUSensitivityValue.Size = new System.Drawing.Size(15, 13);
      this.lblCPUSensitivityValue.TabIndex = 8;
      this.lblCPUSensitivityValue.Text = "%";
      // 
      // tbCPUSensitivity
      // 
      this.tbCPUSensitivity.AutoSize = false;
      this.tbCPUSensitivity.LargeChange = 2;
      this.tbCPUSensitivity.Location = new System.Drawing.Point(114, 50);
      this.tbCPUSensitivity.Maximum = 50;
      this.tbCPUSensitivity.Name = "tbCPUSensitivity";
      this.tbCPUSensitivity.Size = new System.Drawing.Size(294, 25);
      this.tbCPUSensitivity.TabIndex = 7;
      this.tbCPUSensitivity.TickStyle = System.Windows.Forms.TickStyle.None;
      this.tbCPUSensitivity.Value = 5;
      this.tbCPUSensitivity.ValueChanged += new System.EventHandler(this.tbCPUSensitivity_ValueChanged);
      // 
      // lblCPUSensitivity
      // 
      this.lblCPUSensitivity.AutoSize = true;
      this.lblCPUSensitivity.Location = new System.Drawing.Point(6, 50);
      this.lblCPUSensitivity.Name = "lblCPUSensitivity";
      this.lblCPUSensitivity.Size = new System.Drawing.Size(54, 13);
      this.lblCPUSensitivity.TabIndex = 6;
      this.lblCPUSensitivity.Text = "Sensitivity";
      // 
      // gbRAM
      // 
      this.gbRAM.Controls.Add(this.lblRAMFrequencyValue);
      this.gbRAM.Controls.Add(this.lblRAMFrequency);
      this.gbRAM.Controls.Add(this.tbRAMFrequency);
      this.gbRAM.Controls.Add(this.lblRAMSensitivityValue);
      this.gbRAM.Controls.Add(this.tbRAMSensitivity);
      this.gbRAM.Controls.Add(this.lblRAMSensitivity);
      this.gbRAM.Controls.Add(this.chkRAMOverride);
      this.gbRAM.Controls.Add(this.tbRAMOverride);
      this.gbRAM.Location = new System.Drawing.Point(439, 41);
      this.gbRAM.Name = "gbRAM";
      this.gbRAM.Size = new System.Drawing.Size(414, 124);
      this.gbRAM.TabIndex = 10;
      this.gbRAM.TabStop = false;
      this.gbRAM.Text = "Memory";
      // 
      // lblRAMFrequencyValue
      // 
      this.lblRAMFrequencyValue.AutoSize = true;
      this.lblRAMFrequencyValue.Location = new System.Drawing.Point(66, 82);
      this.lblRAMFrequencyValue.Name = "lblRAMFrequencyValue";
      this.lblRAMFrequencyValue.Size = new System.Drawing.Size(20, 13);
      this.lblRAMFrequencyValue.TabIndex = 14;
      this.lblRAMFrequencyValue.Text = "Hz";
      // 
      // lblRAMFrequency
      // 
      this.lblRAMFrequency.AutoSize = true;
      this.lblRAMFrequency.Location = new System.Drawing.Point(6, 82);
      this.lblRAMFrequency.Name = "lblRAMFrequency";
      this.lblRAMFrequency.Size = new System.Drawing.Size(57, 13);
      this.lblRAMFrequency.TabIndex = 13;
      this.lblRAMFrequency.Text = "Frequency";
      // 
      // tbRAMFrequency
      // 
      this.tbRAMFrequency.AutoSize = false;
      this.tbRAMFrequency.Location = new System.Drawing.Point(114, 81);
      this.tbRAMFrequency.Maximum = 20000;
      this.tbRAMFrequency.Minimum = 50;
      this.tbRAMFrequency.Name = "tbRAMFrequency";
      this.tbRAMFrequency.Size = new System.Drawing.Size(294, 25);
      this.tbRAMFrequency.TabIndex = 12;
      this.tbRAMFrequency.TickStyle = System.Windows.Forms.TickStyle.None;
      this.tbRAMFrequency.Value = 50;
      this.tbRAMFrequency.ValueChanged += new System.EventHandler(this.tbRAMFrequency_ValueChanged);
      // 
      // lblRAMSensitivityValue
      // 
      this.lblRAMSensitivityValue.AutoSize = true;
      this.lblRAMSensitivityValue.Location = new System.Drawing.Point(66, 50);
      this.lblRAMSensitivityValue.Name = "lblRAMSensitivityValue";
      this.lblRAMSensitivityValue.Size = new System.Drawing.Size(23, 13);
      this.lblRAMSensitivityValue.TabIndex = 11;
      this.lblRAMSensitivityValue.Text = "MB";
      // 
      // tbRAMSensitivity
      // 
      this.tbRAMSensitivity.AutoSize = false;
      this.tbRAMSensitivity.Location = new System.Drawing.Point(114, 50);
      this.tbRAMSensitivity.Maximum = 500;
      this.tbRAMSensitivity.Name = "tbRAMSensitivity";
      this.tbRAMSensitivity.Size = new System.Drawing.Size(294, 25);
      this.tbRAMSensitivity.TabIndex = 10;
      this.tbRAMSensitivity.TickStyle = System.Windows.Forms.TickStyle.None;
      this.tbRAMSensitivity.Value = 10;
      this.tbRAMSensitivity.ValueChanged += new System.EventHandler(this.tbRAMSensitivity_ValueChanged);
      // 
      // lblRAMSensitivity
      // 
      this.lblRAMSensitivity.AutoSize = true;
      this.lblRAMSensitivity.Location = new System.Drawing.Point(6, 50);
      this.lblRAMSensitivity.Name = "lblRAMSensitivity";
      this.lblRAMSensitivity.Size = new System.Drawing.Size(54, 13);
      this.lblRAMSensitivity.TabIndex = 9;
      this.lblRAMSensitivity.Text = "Sensitivity";
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
      this.statusStrip1.Location = new System.Drawing.Point(0, 523);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(865, 22);
      this.statusStrip1.TabIndex = 11;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // lblStatus
      // 
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(0, 17);
      // 
      // niTray
      // 
      this.niTray.Icon = ((System.Drawing.Icon)(resources.GetObject("niTray.Icon")));
      this.niTray.Visible = true;
      this.niTray.Click += new System.EventHandler(this.niTray_Click);
      // 
      // MainForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(865, 545);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.gbRAM);
      this.Controls.Add(this.gbCPU);
      this.Controls.Add(this.chkPlay);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.zedGraph1);
      this.Controls.Add(this.lblAmbient);
      this.Controls.Add(this.tbVolume);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Name = "MainForm";
      this.Text = "Audible indication of the load on the computer components";
      this.Resize += new System.EventHandler(this.MainForm_Resize);
      ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tbCPUOverride)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tbRAMOverride)).EndInit();
      this.gbCPU.ResumeLayout(false);
      this.gbCPU.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tbCPUFrequency)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tbCPUSensitivity)).EndInit();
      this.gbRAM.ResumeLayout(false);
      this.gbRAM.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tbRAMFrequency)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tbRAMSensitivity)).EndInit();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    #endregion

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        if (soundBuffer != null) { soundBuffer.Stop(); }
        Application.Exit();
      }
      base.OnKeyDown(e);
    }

    private void niTray_Click(object sender, EventArgs e)
    {
      if (this.WindowState == FormWindowState.Minimized)
      {
        this.Show();
        this.WindowState = FormWindowState.Normal;
      }
      else
      {
        this.Hide();
        this.WindowState = FormWindowState.Minimized;
      }
    }

    private void MainForm_Resize(object sender, EventArgs e)
    {
      if (this.WindowState == FormWindowState.Minimized)
      {
        this.Hide();
      }
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      dxSoundDevice = new Microsoft.DirectX.DirectSound.Device();
      dxSoundDevice.SetCooperativeLevel(this, CooperativeLevel.Priority);

      WaveFormat wf = new WaveFormat();
      wf.FormatTag = WaveFormatTag.Pcm;
      wf.BitsPerSample = 8;
      wf.Channels = 1;
      wf.BlockAlign = 1;
      wf.SamplesPerSecond = (int)(dSampleRate);
      wf.AverageBytesPerSecond = wf.SamplesPerSecond * wf.BlockAlign;

      // buffer description         
      BufferDescription bd = new BufferDescription(wf);
      bd.DeferLocation = true;
      bd.GlobalFocus = true;  // play this buffer, even if the app looses focus
      bd.ControlPan = true;
      bd.ControlVolume = true;
      bd.PrimaryBuffer = false;
      bd.BufferBytes = wave.Length;

      soundBuffer = new SecondaryBuffer(bd, dxSoundDevice);
      soundBuffer.Volume = tbVolume.Value;
      soundBuffer.Pan = 0;

      if (chkPlay.Checked)
      {
        m_ambient.Start();
      }
    }

    /// <summary>Alter playback buffer by applying embient sensors</summary>
    private void HandleOnTrigger()
    {
      if (soundBuffer == null) { return; }
      m_ambient.m_bManualCPU = chkCPUOverride.Checked;
      if (chkCPUOverride.Checked) {
        m_ambient.sensor.cpu = tbCPUOverride.Value;
      } else {
        tbCPUOverride.Value = (int)m_ambient.sensor.cpu;
      }
      
      m_ambient.m_bManualRAM = chkRAMOverride.Checked;
      tbRAMOverride.Minimum = 1;
      tbRAMOverride.Maximum = (int)(m_ambient.sensor.ramTotal/1000);
      if (chkRAMOverride.Checked) {
        m_ambient.sensor.ram = (double)(tbRAMOverride.Value) * 1000;
      } else {
        tbRAMOverride.Value = (int)(m_ambient.sensor.ram / 1000);
      }
      lblAmbient.Text = m_ambient.sensor.ToString();

      bool bGenerateCPUWave, bGenerateRAMWave;
      wave = WaveDynamic(sensor, m_ambient.sensor, out bGenerateCPUWave, out bGenerateRAMWave);

      if (bGenerateCPUWave && (wave[wave.Length - 1] == (byte)(2 * byAmpLevel)))
      {
        lblStatus.Text = "["+ dHz_CPU +"]";
      }

      sensor.Copy(m_ambient.sensor);

      BuildDiagram(zedGraph1, wave);

      //soundBuffer.Stop();
      soundBuffer.SetCurrentPosition(0);
      soundBuffer.Write(0, wave, LockFlag.None);
      soundBuffer.Play(0, BufferPlayFlags.Default);
    }

    private const int RATE = 22050;  // values for nSamplesPerSec are 8000 Hz, 11025 Hz, 22050 Hz, and 44100 kHz
    private double dSampleRate = (double)(RATE * 1000d / (double)(Ambient.m_iUpdateRate));
    private Ambient.Sensor sensor = new Ambient.Sensor();
    private byte[] wave = new byte[RATE];
    private byte[] wave1 = new byte[RATE];
    private byte[] wave2 = new byte[RATE];
    private double dDeltaMin_CPU = 10d/(double)(RATE);  // %
    private double dDeltaMin_RAM = 50d*1000000d / (double)(RATE);    // bytes
    private double dHz_CPU = 765d;
    private double dHz_RAM = 8300d;
    private const byte byAmpLevel = 64;

    /// <summary>Take care of the sense, and the sounds will take care of themselves.</summary>
    private byte[] WaveDynamic(Ambient.Sensor s1, Ambient.Sensor s2, out bool bGenerateCPUWave, out bool bGenerateRAMWave)
    {
      double dDelta_CPU = (s2.cpu - s1.cpu) / (double)(wave.Length);
      double dDelta_RAM = (s2.ram - s1.ram) / (double)(wave.Length);
      double dFadeIn = 50d;  // to enable effect should be greater than one
      double dFadeOut = 100d; // to enable effect should be greater than one
      double dAmplitude_CPU = dFadeIn > 0 ? 0d : (double)(byAmpLevel);
      double dAmplitude_RAM = dFadeIn > 0 ? 0d : (double)(byAmpLevel);
      double dCPUHz;
      double dRAMHz;
      bGenerateCPUWave = Math.Abs(dDelta_CPU) > dDeltaMin_CPU && s2.cpu > dDeltaMin_CPU || dDeltaMin_CPU == 0d;
      bGenerateRAMWave = Math.Abs(dDelta_RAM) > dDeltaMin_RAM || dDeltaMin_RAM == 0d;
      
      for (int i = 0; i < wave.Length; i++)
      {
        if (bGenerateCPUWave)
        {
          dCPUHz = dHz_CPU * (s1.cpu / 100d);
          if (dFadeIn > 0 && i < byAmpLevel * dFadeIn && dAmplitude_CPU < byAmpLevel) { dAmplitude_CPU += 1d / dFadeIn; }
          if (dFadeOut > 0 && i > (wave.Length - byAmpLevel * dFadeOut) && dAmplitude_CPU >= 0) { dAmplitude_CPU -= 1d / dFadeOut; }
          wave1[i] = (byte)((double)(byAmpLevel) + dAmplitude_CPU * Math.Sin(((double)(i) * 2.0 * Math.PI * dCPUHz)/ dSampleRate));
        }
        else
        {
          wave1[i] = byAmpLevel;
        }
        
        if (bGenerateRAMWave)
        {
          dRAMHz = dHz_RAM * ((s1.ramTotal - s1.ram) / s1.ramTotal);
          if (dFadeIn > 0 && i < byAmpLevel * dFadeIn && dAmplitude_RAM < byAmpLevel) { dAmplitude_RAM += 1d / dFadeIn; }
          if (dFadeOut > 0 && i > (wave.Length - byAmpLevel * dFadeOut) && dAmplitude_RAM >= 0) { dAmplitude_RAM -= 1d / dFadeOut; }
          wave2[i] = (byte)((double)(byAmpLevel) + dAmplitude_RAM * Math.Sin(((double)(i) * 2d * Math.PI * dRAMHz)/ dSampleRate));
        }
        else
        {
          wave2[i] = byAmpLevel;
        }

        wave[i] = (byte)(wave1[i] + wave2[i]);
        
        s1.cpu += dDelta_CPU;
        s1.ram += dDelta_RAM;
      }

      return wave;
    }

    /* triple wave building codesnippet
    
    int length = (int)(SampleRate * BlockAlign * duration);
    byte[] buffer = new byte[length];

    double A = frequency * 2 * Math.PI / (double)SampleRate;
    for (int i = 0; i < length; i++)
    {
        if (i > 1) buffer[i] = (byte)(2 * Math.Cos(A) * buffer[i - 1] - buffer[i - 2]);
        else if (i > 0) buffer[i] = (byte)(2 * Math.Cos(A) * buffer[i - 1] - (Math.Cos(A)));
        else buffer[i] = (byte)(2 * Math.Cos(A) * Math.Cos(A) - Math.Cos(2 * A));
    }

    public double CalculateGoertzel(byte[] sample, double frequency, int samplerate)
    {
      double Skn, Skn1, Skn2;
      Skn = Skn1 = Skn2 = 0;
      for (int i = 0; i < sample.Length; i++)
      {
        Skn2 = Skn1;
        Skn1 = Skn;
        Skn = 2 * Math.Cos(2 * Math.PI * frequency / samplerate) * Skn1 - Skn2 + sample[i];
      }

      double WNk = Math.Exp(-2 * Math.PI * frequency / samplerate);

      return 20 * Math.Log10(Math.Abs((Skn - WNk * Skn1)));
    }

    public int TestGoertzel(int frequency, byte[] sample, int samplerate)
    {
      int stepsize = frequency / 5;
      Dictionary<int, double> res = new Dictionary<int, double>();
      for (int i = 0; i < 10; i++)
      {
        int freq = stepsize * i;
        res.Add(freq, CalculateGoertzel(sample, freq, samplerate));
      }
    }
    */
    private void BuildDiagram(ZedGraph.ZedGraphControl zg, byte[] _data)
    {
      ZedGraph.GraphPane myPane = zg.GraphPane;
      ZedGraph.LineItem curve = null;

      if (myPane.CurveList.Count >= 1)
      {
        curve = myPane.CurveList[0] as ZedGraph.LineItem;
        curve.Clear();
      }
      else
      {
        curve = new ZedGraph.LineItem("SPS");
      }
      
      // Make sure that the curvelist has at least one curve
      for (int i = 0; i < _data.Length; i++)
      {
        curve.AddPoint(i, _data[i]);
      }

      ZedGraph.IPointList list = curve.Points as ZedGraph.IPointList;
      
      if (myPane.CurveList.Count == 0)
      {
        myPane.AddCurve("Wave", list, Color.DimGray, ZedGraph.SymbolType.None);
      }

      zg.AxisChange();
      zg.Invalidate();
    }

    private void tbVolume_ValueChanged(object sender, EventArgs e)
    {
      if (soundBuffer != null) { soundBuffer.Volume = tbVolume.Value; }
    }

    private void chkManualRAM_CheckedChanged(object sender, EventArgs e)
    {
      tbRAMOverride.Enabled = chkRAMOverride.Checked;
    }

    private void chkManualCPU_CheckedChanged(object sender, EventArgs e)
    {
      tbCPUOverride.Enabled = chkCPUOverride.Checked;
    }

    private void chkPlay_CheckedChanged(object sender, EventArgs e)
    {
      if (chkPlay.Checked)
      {
        m_ambient.Start();
        chkPlay.Text = "Stop";
      }
      else
      {
        m_ambient.Stop();
        chkPlay.Text = "Play";
      }
    }

    private void tbCPUSensitivity_ValueChanged(object sender, EventArgs e)
    {
      dDeltaMin_CPU = (double)(tbCPUSensitivity.Value / (double)(RATE));
      lblCPUSensitivityValue.Text = String.Format("{0}%", (int)(dDeltaMin_CPU * (double)(RATE)));
    }

    private void tbRAMSensitivity_ValueChanged(object sender, EventArgs e)
    {
      dDeltaMin_RAM = (double)(tbRAMSensitivity.Value * 1000000d / (double)(RATE));
      lblRAMSensitivityValue.Text = String.Format("{0}MB", (int)(dDeltaMin_RAM / 1000000d * (double)(RATE)));
    }

    private void tbCPUFrequency_ValueChanged(object sender, EventArgs e)
    {
      dHz_CPU = tbCPUFrequency.Value;
      lblCPUFrequencyValue.Text = String.Format("{0}Hz", dHz_CPU);
    }

    private void tbRAMFrequency_ValueChanged(object sender, EventArgs e)
    {
      dHz_RAM = tbRAMFrequency.Value;
      lblRAMFrequencyValue.Text = String.Format("{0}Hz", dHz_RAM);
    }

  }
}