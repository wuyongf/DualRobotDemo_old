using System;
using DualRobotLib;

namespace DualRobotDemo
{
    internal class DualRobot_ExampleCode
    {
        /// Methods
        public void Connect()
        {
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            // core.Connect(Model.CR7, "192.168.3.124", 60008);
            var result = core.Connect(Model.CR7, "127.0.0.1", 60008);

            Console.WriteLine("result:" + result);
        }

        public void Disconnect()
        {
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            // core.Connect(Model.CR7, "192.168.3.124", 60008);
            var result = core.Connect(Model.CR7, "127.0.0.1", 60008);
            result = core.Connect(Model.CR7, "127.0.0.1", 60008);
            result = core.Disconnect(Model.CR7);
            result = core.Connect(Model.CR7, "127.0.0.1", 60008);
            result = core.Disconnect(Model.CR7);
            result = core.Connect(Model.CR7, "127.0.0.1", 60008);
            result = core.Disconnect(Model.CR7);
            result = core.Connect(Model.CR7, "127.0.0.1", 60008);
            result = core.Disconnect(Model.CR7);
            result = core.Connect(Model.CR7, "127.0.0.1", 60008);


            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Disconnect(Model.CR15);
            core.Connect(Model.CR15, "127.0.0.1", 9021);

            Console.WriteLine("result:" + result);
        }

        public void GetTcpDistance()
        {
            // (1) prerequisite: finish step3
            // (2) define tcp
            // (3) get tcp distance in real time

            // (1)
            // step3:
            // a. uninstall calibration pin.
            // b. 3 point method.
            // c. record calibrated user frame data. 
            //  c.1. robot connection.
            //  c.2. record calibrated user frame data. 

            DualRobotLib.Core core = new Core();
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);
            // core.Connect(Model.CR15, "192.168.3.125", 60008);
            // core.Connect(Model.CR7, "192.168.3.124", 60008);

            // examples: calibrated user frame data
            double[] Pos_Cr7_CalliBase = { 708.92, 1.98, 51.15, -2.11, 46.69, -0.4 };
            double[] Pos_Cr15_CalliBase = { 1337.41, -1.24, -386.08, -178.4, 44.25, -177.79 };
            core.RobotBaseCalibrationInit(Pos_Cr7_CalliBase, Pos_Cr15_CalliBase);

            // (2)
            float[] tcp_cr7 = { 9, 0, 123, 0, -45, 0 };
            float[] tcp_cr15 = { 0, 0, 140, 0, 45, -90 };
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);

            // (3)
            var tcp_distance = core.GetTcpDistance();

            Console.WriteLine("tcp_distance: " + tcp_distance);
        }

        /// CR7
        public void CR7_MoveTo_Demo()
        {
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            core.Connect(Model.CR7, "192.168.3.124", 60008);
            // core.Connect(Model.CR7, "127.0.0.1", 60008);

            // 2. movement test

            core.MoveTo(Model.CR7, Position.Home);

            core.MoveTo(Model.CR7, Position.InitPos_Scene1B);

            core.MoveTo(Model.CR7, Position.Home);

            core.MoveTo(Model.CR7, Position.InitPos_Scene1B);
        }

        public void CR7_BasicMovement_Demo()
        {
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            core.Connect(Model.CR7, "192.168.3.124", 60008);
            // core.Connect(Model.CR7, "127.0.0.1", 60008);

            // 2. movement test

            // 2.1 define the tcp
            float[] tcp_cr7 = { -55, -140, 183, 0, 0, 0 };
            core.SetTCP(Model.CR7, tcp_cr7);

            // 2.2 define the tcp speed
            core.SetSpeed(Model.CR7, 100);

            // 2.3 define the user frame
            core.SetUserFrame(Model.CR7);

            // 2.4.1 Set Single Point
            float[] pos_cr7 = { 50, 0, 0, 0, 0, 0 };
            core.SetSinglePoint(Model.CR7, pos_cr7);

            // 2.4.2 Move
            core.MoveSinglePoint(Model.CR7);

            // 2.4.3 Set Single Point
            float[] pos_cr7_2 = { -50, 0, 0, 0, 0, 0 };
            core.SetSinglePoint(Model.CR7, pos_cr7_2);

            // 2.4.4 Move
            core.MoveSinglePoint(Model.CR7);

            // 2.4.5 Set Single Point
            float[] pos_cr7_3 = { 0, 0, 0, 0, 0, 0 };
            core.SetSinglePoint(Model.CR7, pos_cr7_3);

            // 2.4.6 Move
            core.MoveSinglePoint(Model.CR7);
        }

        public void CR7_Scene2_Demo()
        {
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            core.Connect(Model.CR7, "192.168.3.124", 60008);
            // core.Connect(Model.CR7, "127.0.0.1", 60008);

            // 2. movement test

            // 2.1 define the tcp
            float[] tcp_cr7 = { -45, -125, 183, 0, 0, 0 };
            core.SetTCP(Model.CR7, tcp_cr7);

            // 2.2 define the tcp speed
            core.SetSpeed(Model.CR7, 100);

            // 2.3 define the user frame
            core.SetUserFrame(Model.CR7);

            // 3. Scene2 - single robot
            core.Scene2_CR7_Demo(15, 15);
        }

        /// CR15
        public void CR15_MoveTo_Demo()
        {
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            // core.Connect(Model.CR15, "192.168.3.125", 60008);

            // 2. movement test

            core.MoveTo(Model.CR15, Position.Home);

            core.MoveTo(Model.CR15, Position.InitPos_Scene1B);

            core.MoveTo(Model.CR15, Position.Home);

            core.MoveTo(Model.CR15, Position.InitPos_Scene1B);
        }

        public void CR15_BasicMovement_Demo()
        {
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            // core.Connect(Model.CR15, "192.168.3.125", 60008);

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

        /// DualRobot
        public void DualRobot_Scene1B_Demo_WithCalibration()
        {
            // step1: Robot Installation & Calibration. (skip)
            // step2: TCP Calibration.
            // step3: RobotBase Calibration.
            // step4: Measurement Initialization.
            // step5: Scene1B

            // step2:
            // a. install calibration tool.
            // b. install calibration plate & calibration pin.
            // c. 4 points method.
            // d. record calibrated tcp data.

            // step3:
            // a. uninstall calibration pin.
            // b. 3 point method.
            // c. record calibrated user frame data. 
            //  c.1. robot connection.
            //  c.2. record calibrated user frame data. 

            DualRobotLib.Core core = new Core();
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);
            // core.Connect(Model.CR15, "192.168.3.125", 60008);
            // core.Connect(Model.CR7, "192.168.3.124", 60008);

            // examples: calibrated user frame data
            double[] Pos_Cr7_CalliBase = { 825.25, 6.38, 134.67, -2.11, 46.69, -0.40 };
            double[] Pos_Cr15_CalliBase = { 1223.50, -1.77, -304.52, 1.61, 44.25, -177.78 };
            core.RobotBaseCalibrationInit(Pos_Cr7_CalliBase, Pos_Cr15_CalliBase);

            // step4:
            // a. install testing tools.
            // b. tools setting, tcp speed.
            // c. define scene params.
            // d. robots move to init position.
            // e. define user frame
            //
            // b. examples: tcp data
            float[] tcp_cr7 = { 9, 0, 123, 0, -45, 0 };
            float[] tcp_cr15 = { 0, 0, 140, 0, 45, -90 };
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);
            // c. examples.
            double[] param = { 1000, 130, 0.5, 180, 90, 180, 45 };
            core.SceneParamInit(SceneName.Scene1B, param);
            // d.
            core.SceneRobotInit(SceneName.Scene1B);
            // e.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // step5: Scene1B
            core.Scene1B(MovementType.QuickCheck);
        }

        public void DualRobot_Scene1C_Demo_WithCalibration()
        {
            // step1: Robot Installation & Calibration. (skip)
            // step2: TCP Calibration.
            // step3: RobotBase Calibration.
            // step4: Measurement Initialization.
            // step5: Scene1B

            // step2:
            // a. install calibration tool.
            // b. install calibration plate & calibration pin.
            // c. 4 points method.
            // d. record calibrated tcp data.

            // step3:
            // a. uninstall calibration pin.
            // b. 3 point method.
            // c. record calibrated user frame data. 
            //  c.1. robot connection.
            //  c.2. record calibrated user frame data. 

            DualRobotLib.Core core = new Core();
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);
            // core.Connect(Model.CR15, "192.168.3.125", 60008);
            // core.Connect(Model.CR7, "192.168.3.124", 60008);

            // examples: calibrated user frame data
            double[] Pos_Cr7_CalliBase = { 825.25, 6.38, 134.67, -2.11, 46.69, -0.40 };
            double[] Pos_Cr15_CalliBase = { 1223.50, -1.77, -304.52, 1.61, 44.25, -177.78 };
            core.RobotBaseCalibrationInit(Pos_Cr7_CalliBase, Pos_Cr15_CalliBase);

            // step4:
            // a. install testing tools.
            // b. tools setting, tcp speed.
            // c. define scene params.
            // d. robots move to init position.
            // e. define user frame
            //
            // b. examples: tcp data
            float[] tcp_cr7 = { 9, 0, 123, 0, -45, 0 };
            float[] tcp_cr15 = { 0, 0, 140, 0, 45, -90 };
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);
            // c. examples.
            double[] param = { 250, 300, 300, 100, 100, 0 };
            core.SceneParamInit(SceneName.Scene1C, param);
            // d.
            core.SceneRobotInit(SceneName.Scene1C);
            // e.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // step5: Scene1C
            core.Scene1C(MovementType.QuickCheck);
        }

        public void DualRobot_Scene1B_Demo()
        {
            // 0. Initialize the Core Class
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);
            // core.Connect(Model.CR15, "192.168.3.125", 60008);
            // core.Connect(Model.CR7, "192.168.3.124", 60008);

            // 2. reset robot movement
            core.ResetMovement(Model.CR15);
            core.ResetMovement(Model.CR7);

            // 3. define the tcp - scene1B
            float[] tcp_cr15 = { 0, -702, 842, 0, 45, -90 };
            core.SetTCP(Model.CR15, tcp_cr15);

            float[] tcp_cr7 = { 9, 0, 123, 0, -45, 0 };
            core.SetTCP(Model.CR7, tcp_cr7);

            // 4. define the tcp speed
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            // 5. define the user frame, the current tcp position will be the origin.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // 6. Scene 1B
            core.Scene1B(MovementType.QuickCheck, 130, 0.5, 180, 90, 180, 45);
        }

        public void DualRobot_Scene2_Demo()
        {
            // 0. Initialize the Core Class
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);

            // 2. reset robot movement
            core.ResetMovement(Model.CR15);
            core.ResetMovement(Model.CR7);

            // 3. define the tcp - Scene2
            float[] tcp_cr15 = {0,55,700,0,0,0};
            core.SetTCP(Model.CR15, tcp_cr15);
            
            float[] tcp_cr7 = {-55,-140,183,0,0,0};
            core.SetTCP(Model.CR7, tcp_cr7);

            // 4. define the tcp speed
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            // 5. define the user frame, the current tcp position will be the origin.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // 6. Scene 2
            core.Scene2(MovementType.QuickCheck,100,10,15,15);
        }

        public void DualRobot_Simulation_Demo()
        {
            // Initialize the Core Class
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);

            // 2. reset robot movement
            core.ResetMovement(Model.CR15);
            core.ResetMovement(Model.CR7);

            // // 3. define the tcp (for scene2)
            // float[] tcp_cr15 = {0,55,700,0,0,0};
            // core.SetTCP(Model.CR15, tcp_cr15);
            //
            // float[] tcp_cr7 = {-55,-140,183,0,0,0};
            // core.SetTCP(Model.CR7, tcp_cr7);

            // 3. define the tcp (for scene1)
            float[] tcp_cr15 = { 0, -702, 842, 0, 45, -90 };
            core.SetTCP(Model.CR15, tcp_cr15);

            float[] tcp_cr7 = { 9, 0, 123, 0, -45, 0 };
            core.SetTCP(Model.CR7, tcp_cr7);

            // 4. define the tcp speed
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            // 5. define the user frame, the current tcp position will be the origin.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // // . move -- get move flag - cr15
            // Thread th1 = new Thread(() => core.thread_MoveFlag());
            // th1.Start();

            // 6. Scene 1
            core.Scene1B(MovementType.QuickCheck,130, 0.5, 180, 90, 180, 45);

            // 7. Scene 2
            // core.Scene2(100,10,15,15);

            // Get Methods
            //
            // // 1. get current position
            // core.GetCurPos(Model.CR15);
            // core.GetCurPos(Model.CR7);

            // // 2. get current move status
            // core.GetMoveFlag(Model.CR15);

            // // 3. get current speed
            // var cr15_speed = core.GetSpeed(Model.CR15);
            // var cr7_speed = core.GetSpeed(Model.CR7);

            // Set Methods
            //
            // // 1. set move status after 5g measurement
            // core.SetMoveFlag(Model.CR15, 3);

            // // 2. Set Single Point
            // float[] pos_cr15 = { 0, 0, 0, 0, 0, 0 };
            // core.SetSinglePoint(Model.CR15, pos_cr15);
            // core.MoveSinglePoint(Model.CR15);

        }
    }
}
