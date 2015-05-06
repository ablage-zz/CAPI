using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace cswavrec
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button StopButton;
		private System.Windows.Forms.Button StartButton;
        private Button button1;
		private System.Windows.Forms.OpenFileDialog OpenDlg;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.OpenDlg = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(8, 12);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(72, 24);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(88, 12);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(72, 24);
            this.StopButton.TabIndex = 1;
            this.StopButton.Text = "Stop";
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // OpenDlg
            // 
            this.OpenDlg.DefaultExt = "wav";
            this.OpenDlg.Filter = "WAV files|*.wav";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(195, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 27);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(250, 47);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Full-duplex audio sample";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

        private DSELib.Audio.Wave.WaveOutDevice m_Player;
        private DSELib.Audio.Wave.WaveInDevice m_Recorder;
        private DSELib.IO.FIFOStream m_Stream = new DSELib.IO.FIFOStream();

        private void DataArrived(object sender, byte[] lbData)
		{
            m_Stream.Write(lbData, 0, lbData.Length);
            this.Text = m_Stream.Length.ToString();
		}

		private void Stop()
		{
			if (m_Player != null)
				try
				{
                    m_Player.Active = false;
					m_Player.Dispose();
				}
				finally
				{
					m_Player = null;
				}
			if (m_Recorder != null)
				try
				{
                    m_Recorder.Active = false;
					m_Recorder.Dispose();
				}
				finally
				{
					m_Recorder = null;
				}
		}

		private void Start()
		{
			Stop();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
			try
            {//16384
                DSELib.Audio.Wave.Win32.WaveFormat fmt = new DSELib.Audio.Wave.Win32.WaveFormat(DSELib.Audio.Wave.Enums.Enum_SampleRates.SampleRates_8_0_kHz, DSELib.Audio.Wave.Enums.Enum_BitRates.BitRates_8bit, DSELib.Audio.Wave.Enums.Enum_Channels.Channels_Mono);

                m_Player = new DSELib.Audio.Wave.WaveOutDevice(-1, fmt, (int)(fmt.BytesPerSecond / 20));
                m_Player.DataStream = m_Stream;
                m_Player.Active = true;

                m_Recorder = new DSELib.Audio.Wave.WaveInDevice(-1, fmt, (int)(fmt.BytesPerSecond / 40));
                m_Recorder.OnProcessed += new DSELib.Audio.Wave.ProcessedEventHandler(DataArrived);
                m_Recorder.Active = true;
			}
			catch
			{
				Stop();
				throw;
			}
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Stop();
		}

		private void StartButton_Click(object sender, System.EventArgs e)
		{
			Start();
		}

		private void StopButton_Click(object sender, System.EventArgs e)
		{
			Stop();
		}
	}
}
