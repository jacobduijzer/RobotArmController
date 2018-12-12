[![Build Status](https://dev.azure.com/jacob0771/RobotArmController/_apis/build/status/jacobduijzer.RobotArmController?branchName=master)](https://dev.azure.com/jacob0771/RobotArmController/_build/latest?definitionId=1?branchName=master) [![Build status](https://ci.appveyor.com/api/projects/status/qkc463xscrkpd1sh/branch/master?svg=true)](https://ci.appveyor.com/project/jacobduijzer/robotarmcontroller/branch/master) [![codecov](https://codecov.io/gh/jacobduijzer/RobotArmController/branch/master/graph/badge.svg)](https://codecov.io/gh/jacobduijzer/RobotArmController) [![Sonarcloud Status](https://sonarcloud.io/api/project_badges/measure?project=jacobduijzer_RobotArmController&metric=alert_status)](https://sonarcloud.io/dashboard?id=jacobduijzer_RobotArmController)


# RobotArmController

First attempt to create a cross platform library for a robot arm with multiple servo's. Could be used to control
any amount of servo's. Not necessarily for a robot arm, could also be used to control servo's.

Some plans:

- record, save and edit movements of multiple servo's
- gcode

# Configuration

The servos should be configured in the Arduino sketch before starting the .NET RobotArmController.
