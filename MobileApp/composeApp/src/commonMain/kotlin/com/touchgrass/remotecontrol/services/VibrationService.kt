package com.touchgrass.remotecontrol.services

interface VibrationService {
    fun vibrate(duration: Long)
}

expect fun performVibration()