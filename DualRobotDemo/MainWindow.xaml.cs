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
            // % cal-2: current error: 0.2mm
            double[] Pos_Cr7_CalliBase = { 778.281, -38.520, -336.517, 0.810, 0.687, -92.389 };
            double[] Pos_Cr15_CalliBase = { 1252.171, 24.657, -770.478, 0.269, 0.423, 89.225 };
            core.RobotBaseCalibrationInit(Pos_Cr7_CalliBase, Pos_Cr15_CalliBase);

            // (3) Get Tool Antenna TCP Data 
            float[] origin_wpr_cr7 = { -9.082f, 89.472f, -9.403f };
            float[] default_wpr_cr7 = { 89.529f, -1.760f, 88.218f };

            float[] origin_wpr_cr15 = { -17.024f, 87.760f, -17.928f };
            float[] default_wpr_cr15 = { -91.714f, 0.772f, -90.444f };

            var cal_wpr_cr7 = core.GetToolFixtureWPR(SceneName.Scene2_Sim, Model.CR7, origin_wpr_cr7, default_wpr_cr7);
            var cal_wpr_cr15 = core.GetToolFixtureWPR(SceneName.Scene2_Sim, Model.CR15, origin_wpr_cr15, default_wpr_cr15);

            float[] cal_pin_tcp_cr7 = { -120.609f, 180.878f, 134.808f, cal_wpr_cr7[0], cal_wpr_cr7[1], cal_wpr_cr7[2] };
            float[] cal_pin_tcp_cr15 = { -1.831f, 0.474f, 299.920f, cal_wpr_cr15[0], cal_wpr_cr15[1], cal_wpr_cr15[2] };
            float cal_pin_length_cr7 = 45.23f + 5.02f;
            float cal_pin_length_cr15 = 45.33f + 5.02f;
            var fixture_tcp_cr7 = core.GetToolFixtureTCP(Model.CR7, cal_pin_tcp_cr7, cal_pin_length_cr7);
            var fixture_tcp_cr15 = core.GetToolFixtureTCP(Model.CR15, cal_pin_tcp_cr15, cal_pin_length_cr15);

            // offset-1: cr7: 6.27f;  cr15:6.24f;
            // offset-2: cr7: 16.05f; cr15: 16.03f;
            float[] antenna_offset_cr7 = { 0.0f, 0.0f, 16.05f, 0.0f, 0.0f, 0.0f };
            float[] antenna_offset_cr15 = { 0.0f, 0.0f, 105.13f, 0.0f, 0.0f, 0.0f };
            var tcp_cr7 = core.GetToolAntennaTCP(Model.CR7, fixture_tcp_cr7, antenna_offset_cr7);
            var tcp_cr15 = core.GetToolAntennaTCP(Model.CR15, fixture_tcp_cr15, antenna_offset_cr15);

            // (4) Robot Initialization
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            // (5) Set Station Antenna TCP (Cr7) (Cal. tool + UF: 0)
            float[] station_cal_pin_tcp_cr7 = { 832.066f, -102.571f, 277.335f, 0.953f, 0.102f, -92.282f }; // switch to tool:5 user frame:0
            float station_cal_pin_length = 41.05f;
            var station_center_zero_tcp = core.GetStationCenterZeroTCP(station_cal_pin_tcp_cr7, station_cal_pin_length);

            float[] antenna_offset_station = { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f };
            float station_offset = 0; // 0-290

            var station_antenna_tcp_cr7 = core.GetStationAntennaTCP(station_center_zero_tcp, antenna_offset_station, station_offset);
            Console.WriteLine("station_antenna_tcp_cr7: " + station_antenna_tcp_cr7);

            core.SetStationAntennaTCP_Cr7(station_antenna_tcp_cr7);


            // (6) Align LiftTable Height with station_center_zero_tcp
            double lift_table_align_error = 1.025;
            double stage34_fixture_height = 127;

            // (7) Scene Initialization
            // a. examples.
            double[] param = { 160, 90, 10, 180, 30, 100, 45, 0, 140, lift_table_align_error, stage34_fixture_height };

            core.SceneParamInit(SceneName.Scene2_Sim, param);
            // b.
            core.SceneRobotInit(SceneName.Scene2_Sim);
            // c.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // (8) Execute Scene2_Sim
            //
            core.Scene2_Sim(MovementType.QuickCheck, MovementStage.One);

            core.SceneRobotInit(SceneName.Scene2_Sim);
            core.Scene2_Sim(MovementType.QuickCheck, MovementStage.Two);

            param[1] = 20;
            param[8] = 13;
            core.SceneParamInit(SceneName.Scene2_Sim, param);
            core.SceneRobotInit(SceneName.Scene2_Sim, Model.Null, MovementStage.Three);
            core.Scene2_Sim(MovementType.QuickCheck, MovementStage.Three);

            param[1] = 20;
            param[8] = 13;
            core.SceneParamInit(SceneName.Scene2_Sim, param);
            core.SceneRobotInit(SceneName.Scene2_Sim, Model.Null, MovementStage.Four);
            core.Scene2_Sim(MovementType.QuickCheck, MovementStage.Four);
        }

        void thread_MoveFlage_Motor(ref DualRobotLib.Core core)
        {
            while (true)
            {
                // 5. Get_IsMoving
                //var isMoving = core.MotorIsMoving();
                //Console.WriteLine("isMoving: " + isMoving);

                // 6. Get Degree
                var curDegree = core.MotorGetDegree();
                Console.WriteLine("curDegree: " + curDegree);
            }
        }

        void Scene2_Backup()
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

            #region (3) Get Tool Antenna TCP Data TODO: TCP Calculation

            // // (3) Get Tool Antenna TCP Data 
            //
            // float[] origin_wpr_cr7 = { 176.753f, 8.741f, 174.332f };
            // float[] default_wpr_cr7 = { -3.884f, 53.543f, -9.229f };
            //
            // float[] origin_wpr_cr15 = { -0.596f, 29.023f, -2.64f };
            // float[] default_wpr_cr15 = { 105.071f, -0.865f, 89.288f };
            //
            // var cal_wpr_cr7 = core.GetToolFixtureWPR(SceneName.Scene1B, Model.CR7, origin_wpr_cr7, default_wpr_cr7);
            // var cal_wpr_cr15 = core.GetToolFixtureWPR(SceneName.Scene1B, Model.CR15, origin_wpr_cr15, default_wpr_cr15);
            //
            // float[] cal_pin_tcp_cr7 = { -61.97f, 1.016f, 193.006f, cal_wpr_cr7[0], cal_wpr_cr7[1], cal_wpr_cr7[2] };
            // float[] cal_pin_tcp_cr15 = { -1.946f, -35.828f, 174.092f, cal_wpr_cr15[0], cal_wpr_cr15[1], cal_wpr_cr15[2] };
            // float cal_pin_length_cr7 = 45.23f + 5.02f;
            // float cal_pin_length_cr15 = 45.33f + 5.02f;
            // var fixture_tcp_cr7 = core.GetToolFixtureTCP(Model.CR7, cal_pin_tcp_cr7, cal_pin_length_cr7);
            // var fixture_tcp_cr15 = core.GetToolFixtureTCP(Model.CR15, cal_pin_tcp_cr15, cal_pin_length_cr15);
            //
            // // offset-1: cr7: 6.27f;  cr15:6.24f;
            // // offset-2: cr7: 16.05f; cr15: 16.03f;
            // float[] antenna_offset_cr7 = { 0.0f, 0.0f, 6.27f, 0.0f, 0.0f, 0.0f };
            // float[] antenna_offset_cr15 = { 0.0f, 0.0f, 6.24f, 0.0f, 0.0f, 0.0f };
            // var tcp_cr7 = core.GetToolAntennaTCP(Model.CR7, fixture_tcp_cr7, antenna_offset_cr7);
            // var tcp_cr15 = core.GetToolAntennaTCP(Model.CR15, fixture_tcp_cr15, antenna_offset_cr15);
            // Console.WriteLine("tcp_cr7: " + tcp_cr7);
            // Console.WriteLine("tcp_cr15: " + tcp_cr15);

            #endregion


            // (4) Robot Initialization
            // examples: tcp data
            float[] tcp_cr7 = { -125, 50, 45, 90, 0, 180 };
            float[] tcp_cr15 = { 0, 134, 182, 0, 0, 90 };
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            // todo: (5 - Optional) Set Station Antenna TCP (Cr7) (Cal. tool + UF: 0)
            float[] station_cal_pin_tcp_cr7 = { 834.125f, -101.834f, -246.113f, -0.575f, 3.62f, -101.227f }; // switch to tool:5 user frame:0
            float station_cal_pin_length = 110.2f;
            var station_center_zero_tcp = core.GetStationCenterZeroTCP(station_cal_pin_tcp_cr7, station_cal_pin_length);

            float[] antenna_offset_station = { 0.0f, 0.0f, 110.2f, 0.0f, 0.0f, 0.0f };
            float station_offset = 440; // 0-300
            var station_antenna_tcp_cr7 = core.GetStationAntennaTCP(station_center_zero_tcp, antenna_offset_station, station_offset);
            Console.WriteLine("station_antenna_tcp_cr7: " + station_antenna_tcp_cr7);

            core.SetStationAntennaTCP_Cr7(station_antenna_tcp_cr7);

            // (6) 
            // c. examples.
            double[] param = { 250, 100, 10, 250, 90, 45 };
            core.SceneParamInit(SceneName.Scene2, param);
            // d.
            core.SceneRobotInit(SceneName.Scene2);
            // e.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // step5: Scene2
            core.Scene2(MovementType.QuickCheck);
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

        void Scene2_methods()
        {
            // var r_cr7 = core.GetTcpDistance_Scene2(Model.CR7);
            // Console.WriteLine("r_cr7: " + r_cr7);
            // var r_cr15 = core.GetTcpDistance_Scene2(Model.CR15);
            // Console.WriteLine("r_cr15: " + r_cr15);
        }
    }
}
