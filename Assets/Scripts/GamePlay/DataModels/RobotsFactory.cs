using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace EnglishKids.RobotsConstructor
{
    public class RobotsFactory : MonoBehaviour
    {
        public List<RobotController> robotPrefabs;

        public RobotController GetRandomRobot()
        {
            int rnd = Random.Range(0, robotPrefabs.Count);
            return CreateRobot(robotPrefabs[rnd]);
        }

        public RobotController GetRandomRobot(string excludingRobot)
        {
            var robots = robotPrefabs.Where(x => x.GetRobotName() != excludingRobot).ToArray();
            int rnd = Random.Range(0, robots.Length);
            return CreateRobot(robots[rnd]);
        }

        public RobotController CreateRobot(RobotController robotPrefab)
        {
            return Instantiate(robotPrefab);
        }
    }
}
