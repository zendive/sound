namespace AIoLoCC
{
  partial class UCTrackBar
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.trackBar1 = new System.Windows.Forms.TrackBar();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 6);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "label1";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.label2.Location = new System.Drawing.Point(45, 4);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(37, 15);
      this.label2.TabIndex = 1;
      this.label2.Text = "label2";
      // 
      // trackBar1
      // 
      this.trackBar1.Location = new System.Drawing.Point(88, 4);
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Size = new System.Drawing.Size(279, 45);
      this.trackBar1.TabIndex = 2;
      this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
      // 
      // UCTrackBar
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.trackBar1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Name = "UCTrackBar";
      this.Size = new System.Drawing.Size(370, 31);
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TrackBar trackBar1;
  }
}
