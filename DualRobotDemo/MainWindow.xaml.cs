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

            // 0. Initialize the Core Class
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);

            // 2. reset robot movement
            core.ResetMovement(Model.CR15);
            core.ResetMovement(Model.CR7);

            // 3. define the tcp - Scene2
            float[] tcp_cr15 = { 0, 55, 700, 0, 0, 0 };
            core.SetTCP(Model.CR15, tcp_cr15);

            float[] tcp_cr7 = { -55, -140, 183, 0, 0, 0 };
            core.SetTCP(Model.CR7, tcp_cr7);

            // 4. define the tcp speed
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            // 5. define the user frame, the current tcp position will be the origin.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // 6. Scene 2
            core.Scene2(MovementType.QuickCheck, 100, 10, 15, 15);
        }
    }
}
