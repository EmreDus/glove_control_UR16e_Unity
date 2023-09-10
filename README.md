# Unity UR16e Robot Arm Control with Augmented Reality Glove

![Project Demo](link_to_project_demo.gif)

This project combines the power of Unity, UR16e robot arm, and an augmented reality glove using ESP32 for wireless communication. The goal is to enable precise control of the robot arm through an Android smartphone's augmented reality interface and touch controls.

## Project Overview

In this project, we achieved the following key functionalities:

- Utilized inverse kinematics calculations for the UR16e robot arm within the Unity environment, allowing the end effector to move to desired positions.

- Created an augmented reality experience on an Android device, enabling users to visualize and interact with the robot arm in real-time.

- Implemented touch controls on the smartphone, allowing users to intuitively control the robot arm's movements.

- Utilized three MPU6050 gyro sensors connected to an ESP32 to capture the rotation angles of the upper arm, lower arm, and hand.

- Transferred these rotation angles from the ESP32 to the Unity environment wirelessly, enabling the Unity environment to accurately track the hand's position.

- Established communication between the ESP32 and Unity environment through a Local Host connection.

- Combined the tracked hand position with the UR16e robot arm's inverse kinematics calculations, enabling the robot arm's end effector to follow the hand's position accurately.

- Demonstrated the robot arm's capabilities in an augmented reality setting, including observing the robot arm on a QR code marker and controlling it using the augmented reality glove.

## Project Demo

[Link to Project Demo Video](https://www.youtube.com/watch?v=FRx_d1CK45s)
