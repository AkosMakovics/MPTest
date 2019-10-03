using System;
using System.Windows.Forms;
using MissionPlanner;
using MissionPlanner.Plugin;

namespace ParamsPlugin
{
    public class Params : Plugin
    {
        string _Name = "Test Plugin";
        string _Version = "1.0";
        string _Author = "Akos Makovics";

        string[] _params = { "ATC_RAT_PIT_P",
                             "ATC_RAT_PIT_I",
                             "ATC_RAT_PIT_D",
                             "ATC_RAT_RLL_P",
                             "ATC_RAT_RLL_I",
                             "ATC_RAT_RLL_D" };

        int[] _firstLabelPos = { 25, 25 };
        int[] _nextLabelOffset = { 0, 20 };

        int[] _labelSize = { 170, 20 };

        public override string Name { get { return _Name; } }
        public override string Version { get { return _Version; } }
        public override string Author { get { return _Author; } }

        public override bool Exit()
        {
            return true;
        }

        public override bool Init()
        {
            return true;
        }

        public override bool Loaded()
        {
            ToolStripLabel item = new ToolStripLabel("Show Params");
            item.Click += ShowParams;

            ToolStripButton menuParams = new ToolStripButton();

            menuParams.ForeColor = System.Drawing.Color.White;
            menuParams.Name = "Menu Params";
            menuParams.Text = "Params";
            menuParams.Padding = new Padding(0, 0, 0, 10);
            menuParams.Click += new EventHandler(ShowParams);
            
            Host.MainForm.MainMenu.Items.Insert(0, menuParams);

            Host.MainForm.MainMenu.Refresh();

            return true;
        }

        void ShowParams(object sender, EventArgs e)
        {
            if (!Host.comPort.BaseStream.IsOpen)
            {
                MessageBox.Show("No vehicle connected!");
                return;
            }

            Form form = new Form
            {
                Text = "Pitch/Roll Parameters"
            };

            for (int i = 0; i < _params.Length; i++)
            {
                Label lbl = new Label
                {
                    Width = _labelSize[0],
                    Height = _labelSize[1],
                    Text = _params[i] + ": " + Host.comPort.GetParam(_params[i]),
                    Left = _firstLabelPos[0] + (i * _nextLabelOffset[0]),
                    Top = _firstLabelPos[1] + (i * _nextLabelOffset[1])
                };

                form.Controls.Add(lbl);
            }

            form.Show();
        }
    }
}
