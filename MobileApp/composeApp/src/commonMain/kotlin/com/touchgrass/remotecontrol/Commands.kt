package com.touchgrass.remotecontrol

enum class Commands (val code : Int) {
    UP(0),
    LEFT ( 1),
    RIGHT ( 2),
    DOWN ( 3),
    A ( 4),
    B ( 5),
    X ( 6),
    Y ( 7),
    LPAD ( 8),
    RPAD ( 9),
    L1 ( 10),
    L2 ( 11),
    R1 ( 12),
    R2 ( 13),
    SELECT ( 14),
    START ( 15),
    LSTICK ( 16),
    RSTICK ( 17);

    override fun toString(): String {
        val displayString = when (this) {
            UP -> "Volume up"
            LEFT -> "Volume down"
            RIGHT -> ""
            DOWN -> ""
            A -> "-10 seconds"
            B -> "+10 seconds"
            X -> "Skip intro"
            Y -> "Next episode"
            LPAD -> ""
            RPAD -> ""
            L1 -> ""
            L2 -> ""
            R1 -> ""
            R2 -> ""
            SELECT -> ""
            START -> "Pause + Fullscreen"
            LSTICK -> ""
            RSTICK -> ""
        }
        return displayString.ifEmpty { "Not mapped" }
    }
}