using System;

public enum e
{
	Start = 0,
	Stop = 1,
	Udp = 2,
	Serial = 3,
	CloseBg = 4,
	ShutDown = 5,
	Restart = 6,
	Light = 7,
	Volume = 8,
	[Obsolete("没有退出视频了")]
	Quit = 9,
	[Obsolete("三轴时才有用")]
	RotateLeft = 10,
	[Obsolete("三轴时才有用")]
	RotateRight = 11,
	[Obsolete("三轴时才有用")]
	RotateStop = 12,
	PictureCalibration = 13,
	RestartSteamVR = 14,
	StartPlayAction = 15
}
