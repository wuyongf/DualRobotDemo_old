using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            // (1) Connection
            DualRobotLib.Core core = new Core();
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);
            // core.Connect(Model.CR15, "192.168.3.125", 60008);
            // core.Connect(Model.CR7, "192.168.3.124", 60008);

            // (2) Get Calibrated Co-Frame Data
            double[] Pos_Cr7_CalliBase = { 798.94, 34.77, -312.96, .27, .62, -89.45 };
            double[] Pos_Cr15_CalliBase = { 1250.02, -30.16, -746.29, -.38, .51, 88.10 };
            core.RobotBaseCalibrationInit(Pos_Cr7_CalliBase, Pos_Cr15_CalliBase);

            // (3) Get Tool Antenna TCP Data

            float[] origin_wpr_cr7 = { -178.22f, 11.71f, 169.22f };
            float[] default_wpr_cr7 = { 2.90f, 55.78f, -6.16f };

            float[] origin_wpr_cr15 = { -4.682f, 25.697f, -0.15f };
            float[] default_wpr_cr15 = { 108.315f, -3.697f, 89.489f };

            var cal_wpr_cr7 = core.GetToolFixtureWPR(SceneName.Scene1B, Model.CR7, origin_wpr_cr7, default_wpr_cr7);
            var cal_wpr_cr15 = core.GetToolFixtureWPR(SceneName.Scene1B, Model.CR15, origin_wpr_cr15, default_wpr_cr15);

            float[] cal_pin_tcp_cr7 = { -97.414f, 0.117f, 229.286f, cal_wpr_cr7[0], cal_wpr_cr7[1], cal_wpr_cr7[2] };
            float[] cal_pin_tcp_cr15 = { 0.365f, -35.732f, 175.563f, cal_wpr_cr15[0], cal_wpr_cr15[1], cal_wpr_cr15[2] };
            float cal_pin_length_cr7 = 50.0f;
            float cal_pin_length_cr15 = 50.0f;
            var fixture_tcp_cr7 = core.GetToolFixtureTCP(Model.CR7, cal_pin_tcp_cr7, cal_pin_length_cr7);
            var fixture_tcp_cr15 = core.GetToolFixtureTCP(Model.CR15, cal_pin_tcp_cr15, cal_pin_length_cr15);

            float[] antenna_offset_cr7 = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
            float[] antenna_offset_cr15 = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
            var tcp_cr7 = core.GetToolAntennaTCP(Model.CR7, fixture_tcp_cr7, antenna_offset_cr7);
            var tcp_cr15 = core.GetToolAntennaTCP(Model.CR15, fixture_tcp_cr15, antenna_offset_cr15);
            Console.WriteLine("tcp_cr7: " + tcp_cr7);
            Console.WriteLine("tcp_cr15: " + tcp_cr15);

            // (4) Robot Initialization
            // examples: tcp data
            //float[] tcp_cr7 = { -61.7107f, 0, 193.7107f, 0, -45, 0 };
            //float[] tcp_cr15 = { 0, 0, 140, 0, 45, -90 };
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            // c. examples.
            //double[] param = { 200, 350, 350, 4, 4, 0 };
            double[] param = { 0, 350, 350, 4, 4, 0 };
            core.SceneParamInit(SceneName.Scene1C, param);
            // d.
            core.SceneRobotInit(SceneName.Scene1C);
            // e.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // step5: Scene1C
            // Thread th_scene1c = new Thread(() => core.Scene1C(MovementType.QuickCheck));
            // th_scene1c.Start();

            // Thread th1 = new Thread(() => core.thread_MoveFlag(Model.CR15));
            // th1.Start();

            core.Scene1C(MovementType.QuickCheck);

            Console.WriteLine("Waiting...");

            // Thread.Sleep(100000000);

            Console.WriteLine("Done");
        }

        void CancelScene(ref DualRobotLib.Core core)
        {
            Thread.Sleep(20000);

            core.CancelCurrentScene();

            Thread.Sleep(5000);

            double[] param = { 250, 299.9, 229.9, 14, 11, 0 };
            core.SceneParamInit(SceneName.Scene1C, param);
            // d.
            core.SceneRobotInit(SceneName.Scene1C);
            // e.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);
            
            core.Scene1C(MovementType.QuickCheck);
        }
    }
}
