using AF_RobotService.Models;
using AF_RobotService.Models.Objects.Robots;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AF_RobotService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RobotController : ControllerBase
    {
        BinanceFuturesRobot robot;
        BinanceFuturesTestRobot testRobot;

        public string StartTestRobot()
        {
            testRobot = new BinanceFuturesTestRobot();
            testRobot.isWork = true;
            testRobot.Work();

            return "Started";
        }
        public string StartRobot()
        {
            robot = new BinanceFuturesRobot();
            robot.isWork = true;
            robot.Work();

            return "Started";
        }

        public string StopRobot()
        {
            robot.isWork = false;

            return "Stopped";
        }
    }
}
