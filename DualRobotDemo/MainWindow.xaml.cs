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
            // core.Connect(Model.CR15, "192.168.0.125", 60008);
            // core.Connect(Model.CR7, "192.168.0.124", 60008);

            // (2) Get Calibrated Co-Frame Data
            // % Pos_Cr7_CalliBase 
            // % cal-0: 779.422, -37.794, -339.305, 0.351, 0.523, -92.267
            // % cal-1: 779.282, -38.284, -286.598, 0.588, 0.510, -92.060
            // % cal-2: 778.281, -38.520, -336.517, 0.810, 0.687, -92.389
            //
            // % Pos_Cr15_CalliBase 
            // % cal-0: 1249.821, 22.977, -774.474, 0.149, 0.109, 89.261 //error: 3-4mm
            // % cal-1: 1252.218, 23.278, -722.122, 0.082, 0.179, 89.359 //error: 2.5mm
            // % cal-2: 1252.171, 24.657, -770.478, 0.269, 0.423, 89.225 //error: 0.2mm
            double[] Pos_Cr7_CalliBase = { 778.281, -38.520, -336.517, 0.810, 0.687, -92.389 };
            double[] Pos_Cr15_CalliBase = { 1252.171, 24.657, -770.478, 0.269, 0.423, 89.225 };
            core.RobotBaseCalibrationInit(Pos_Cr7_CalliBase, Pos_Cr15_CalliBase);

            // (3) Get Tool Antenna TCP Data 

            float[] origin_wpr_cr7 = { 176.753f, 8.741f, 174.332f };
            float[] default_wpr_cr7 = { -3.884f, 53.543f, -9.229f };

            float[] origin_wpr_cr15 = { -0.596f, 29.023f, -2.64f };
            float[] default_wpr_cr15 = { 105.071f, -0.865f, 89.288f };

            var cal_wpr_cr7 = core.GetToolFixtureWPR(SceneName.Scene1B, Model.CR7, origin_wpr_cr7, default_wpr_cr7);
            var cal_wpr_cr15 = core.GetToolFixtureWPR(SceneName.Scene1B, Model.CR15, origin_wpr_cr15, default_wpr_cr15);

            float[] cal_pin_tcp_cr7 = { -61.97f, 1.016f, 193.006f, cal_wpr_cr7[0], cal_wpr_cr7[1], cal_wpr_cr7[2] };
            float[] cal_pin_tcp_cr15 = { -1.946f, -35.828f, 174.092f, cal_wpr_cr15[0], cal_wpr_cr15[1], cal_wpr_cr15[2] };
            float cal_pin_length_cr7 = 45.23f + 5.02f;
            float cal_pin_length_cr15 = 45.33f + 5.02f;
            var fixture_tcp_cr7 = core.GetToolFixtureTCP(Model.CR7, cal_pin_tcp_cr7, cal_pin_length_cr7);
            var fixture_tcp_cr15 = core.GetToolFixtureTCP(Model.CR15, cal_pin_tcp_cr15, cal_pin_length_cr15);

            // offset-1: cr7: 6.27f;  cr15:6.24f;
            // offset-2: cr7: 16.05f; cr15: 16.03f;
            // offset-3: cr7: 71.53f; cr15: 16.02f;
            // offset-4: cr7: 16.05f; cr15: 16.02f;
            // offset-5: cr7: 71.53f; cr15: 105.13f;
            // offset-6: cr7: 16.05f; cr15: 105.13f;
            // offset-7: cr7: 45.14f; cr15: 105.13f;
            float[] antenna_offset_cr7 = { 0.0f, 0.0f, 45.14f, 0.0f, 0.0f, 0.0f };
            float[] antenna_offset_cr15 = { 0.0f, 0.0f, 105.13f, 0.0f, 0.0f, 0.0f };
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
            double[] param = { 100, 350, 350, 4, 4, 0 };
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

            core.Scene1C(MovementType.QuickCheck, MovementStage.One);

            core.Scene1C(MovementType.QuickCheck, MovementStage.Two);
        }
    }
}
