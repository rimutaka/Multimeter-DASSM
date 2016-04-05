// Decompiled with JetBrains decompiler
// Type: DT_3391.About
// Assembly: DT-9961T, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 681D06A8-439A-4E73-B916-52A7EBFBD7ED
// Assembly location: C:\Program Files (x86)\Multimeter\Multimeter.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DT_3391
{
  public class About : Form
  {
    private IContainer components = (IContainer) null;
    private Button button1;
    private Label label3;
    private Label label1;

    public About()
    {
      this.InitializeComponent();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (About));
      this.button1 = new Button();
      this.label3 = new Label();
      this.label1 = new Label();
      this.SuspendLayout();
      this.button1.Location = new Point(153, 78);
      this.button1.Name = "button1";
      this.button1.Size = new Size(50, 23);
      this.button1.TabIndex = 3;
      this.button1.Text = "Close";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.label3.AutoSize = true;
      this.label3.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label3.Location = new Point(66, 37);
      this.label3.Name = "label3";
      this.label3.Size = new Size(99, 18);
      this.label3.TabIndex = 5;
      this.label3.Text = "Version:V1.0";
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label1.Location = new Point(65, 9);
      this.label1.Name = "label1";
      this.label1.Size = new Size(82, 19);
      this.label1.TabIndex = 6;
      this.label1.Text = "Multimeter";
      this.AutoScaleDimensions = new SizeF(7f, 15f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.InactiveCaptionText;
      this.ClientSize = new Size(215, 103);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.button1);
      this.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Margin = new Padding(3, 4, 3, 4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "About";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "About ";
      this.Load += new EventHandler(this.About_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void About_Load(object sender, EventArgs e)
    {
      this.BackColor = Color.FromArgb(194, 212, 236);
    }
  }
}
