package com.touchgrass.remotecontrol.services
import ApplicationContextSingleton
import android.content.Context
import android.os.Build
import android.os.VibrationEffect
import android.os.Vibrator

class AndroidVibrationService(private val context: Context) : VibrationService {
    override fun vibrate(duration: Long) {
        val vibrator = context.getSystemService(Context.VIBRATOR_SERVICE) as Vibrator
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            val vibrationEffect = VibrationEffect.createOneShot(duration, VibrationEffect.DEFAULT_AMPLITUDE)
            vibrator.vibrate(vibrationEffect)
        } else {
            vibrator.vibrate(duration)
        }
    }
}


actual fun performVibration() {
    AndroidVibrationService(ApplicationContextSingleton.appContext).vibrate(100L)
}