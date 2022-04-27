using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DualRobotLib;

namespace DualRobotDemo
{
    /// <summary>
    /// Dual Robot Example Code
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DualRobotLib.Core core = new Core();

            // 1. establish connection
            // core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR15, "192.168.3.125", 60008);

            // 2. movement test

            // 2.1 define the tcp
            float[] tcp_cr15 = { 0, 55, 700, 0, 0, 0 };
            core.SetTCP(Model.CR15, tcp_cr15);

            // 2.2 define the tcp speed
            core.SetSpeed(Model.CR15, 100);

            // 2.3 define the user frame
            core.SetUserFrame(Model.CR15);

            // 2.4.1 Set Single Point
            float[] pos_cr15 = { 50, 0, 0, 0, 0, 0 };
            core.SetSinglePoint(Model.CR15, pos_cr15);

            // 2.4.2 Move
            core.MoveSinglePoint(Model.CR15);

            // 2.4.3 Set Single Point
            float[] pos_cr15_2 = { -50, 0, 0, 0, 0, 0 };
            core.SetSinglePoint(Model.CR15, pos_cr15_2);

            // 2.4.4 Move
            core.MoveSinglePoint(Model.CR15);

            // 2.4.5 Set Single Point
            float[] pos_cr15_3 = { 0, 0, 0, 0, 0, 0 };
            core.SetSinglePoint(Model.CR15, pos_cr15_3);

            // 2.4.6 Move
            core.MoveSinglePoint(Model.CR15);
        }
    }
}
