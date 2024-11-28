package com.touchgrass.remotecontrol

interface Platform {
    val name: String
}

expect fun getPlatform(): Platform