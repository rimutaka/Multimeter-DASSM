// Decompiled with JetBrains decompiler
// Type: DT_3391.Sampling
// Assembly: DT-9961T, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 681D06A8-439A-4E73-B916-52A7EBFBD7ED
// Assembly location: C:\Program Files (x86)\Multimeter\Multimeter.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DT_3391
{
  public class Sampling : Form
  {
    private IContainer components = (IContainer) null;
    private Button button1;
    private TextBox textBox1;
    private Label label1;
    private Button button2;

    public Sampling()
    {
      this.InitializeComponent();
    }

    private void button2_Click(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.button1 = new Button();
      this.textBox1 = new TextBox();
      this.label1 = new Label();
      this.button2 = new Button();
      this.SuspendLayout();
      this.button1.Location = new Point(74, 182);
      this.button1.Margin = new Padding(3, 4, 3, 4);
      this.button1.Name = "button1";
      this.button1.Size = new Size(76, 37);
      this.button1.TabIndex = 0;
      this.button1.Text = "OK";
      this.button1.UseVisualStyleBackColor = true;
      this.textBox1.Location = new Point(88, 107);
      this.textBox1.Margin = new Padding(3, 4, 3, 4);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(116, 21);
      this.textBox1.TabIndex = 1;
      this.textBox1.Text = "1";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(80, 69);
      this.label1.Name = "label1";
      this.label1.Size = new Size(124, 15);
      this.label1.TabIndex = 2;
      this.label1.Text = "Sampling rate(Unit/s)";
      this.button2.Location = new Point(156, 182);
      this.button2.Margin = new Padding(3, 4, 3, 4);
      this.button2.Name = "button2";
      this.button2.Size = new Size(75, 37);
      this.button2.TabIndex = 3;
      this.button2.Text = "canceled";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.AutoScaleDimensions = new SizeF(7f, 15f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(299, 277);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.button1);
      this.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Margin = new Padding(3, 4, 3, 4);
      this.Name = "Sampling";
      this.Text = "Setting Sampling rate";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
