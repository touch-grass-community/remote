package com.touchgrass.remotecontrol

import UdpClient
import androidx.compose.foundation.Image
import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.interaction.MutableInteractionSource
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material.AlertDialog
import androidx.compose.material.FloatingActionButton
import androidx.compose.material.Icon
import androidx.compose.material.MaterialTheme
import androidx.compose.material.Text
import androidx.compose.material.TextButton
import androidx.compose.material.TextField
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Close
import androidx.compose.material.ripple
import androidx.compose.material.ripple.rememberRipple
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.text.toUpperCase
import androidx.compose.ui.unit.Dp
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.touchgrass.remotecontrol.components.SquaredCardGrid
import com.touchgrass.remotecontrol.services.performVibration
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.IO
import kotlinx.coroutines.launch
import org.jetbrains.compose.resources.DrawableResource
import org.jetbrains.compose.resources.painterResource
import org.jetbrains.compose.ui.tooling.preview.Preview
import remotecontrol.composeapp.generated.resources.Fast_Forward
import remotecontrol.composeapp.generated.resources.Minus_Math
import remotecontrol.composeapp.generated.resources.Pause
import remotecontrol.composeapp.generated.resources.Plus_Math
import remotecontrol.composeapp.generated.resources.Res
import remotecontrol.composeapp.generated.resources.Right_Arrow
import remotecontrol.composeapp.generated.resources.Shutdown
import remotecontrol.composeapp.generated.resources.compose_multiplatform

val touchGrassColor: Color = Color.hsv(120f, 0.40f, 0.70f)

@Composable
@Preview
fun App() {
    val udpClient = remember { UdpClient() }
    val openAlertDialog = remember { mutableStateOf(true) }
    udpClient.connect("192.168.88.182", 12345)
    MaterialTheme {
        when {
            openAlertDialog.value -> {
                AlertDialogExample(
                    onConfirmation = { ip ->
                        openAlertDialog.value = false
                        udpClient.connect(ip, 12345)
                    },
                )
            }
        }
        Column(
            modifier = Modifier.fillMaxSize().background(Palette.DarkBackground),
            verticalArrangement = Arrangement.Top,
            horizontalAlignment = Alignment.CenterHorizontally
        ) {
            Spacer(modifier = Modifier.height(15.dp).fillMaxWidth())

            Text(
                "TOUCHGRASS",
                fontWeight = FontWeight.Bold,
                modifier = Modifier.align(Alignment.CenterHorizontally)
                    .padding(top = 25.dp, bottom = 10.dp),
                color = Palette.ToughGrassGreen,
                fontSize = 22.sp
            )

            Spacer(modifier = Modifier.height(55.dp).fillMaxWidth())

            Image(
                painterResource(Res.drawable.Shutdown),
                "Power off",
                modifier = Modifier.size(60.dp).clickable(onClick = {
                    udpClient.disconnect()
                    openAlertDialog.value = true
                    performVibration()
                }, indication = ripple(),
                    interactionSource = remember { MutableInteractionSource() })
            )

            Spacer(modifier = Modifier.height(25.dp).fillMaxWidth())

            Row(
                modifier = Modifier.padding(horizontal = 25.dp).fillMaxWidth(),
                horizontalArrangement = Arrangement.SpaceBetween
            ) {
                PlusMinusButton(
                    "VOL",
                    { sendCommand(udpClient, Commands.UP) },
                    { sendCommand(udpClient, Commands.DOWN) })
                PlusMinusButton(
                    "SKIP",
                    { sendCommand(udpClient, Commands.B) },
                    { sendCommand(udpClient, Commands.A) })
            }

            Spacer(modifier = Modifier.height(35.dp).fillMaxWidth())

            Row(
                modifier = Modifier.padding(horizontal = 25.dp).fillMaxWidth(),
                horizontalArrangement = Arrangement.SpaceBetween
            ) {
                SimpleButton(Res.drawable.Pause, "Pause") { sendCommand(udpClient, Commands.START) }
                SimpleButton(Res.drawable.Fast_Forward, "Skip intro") {
                    sendCommand(
                        udpClient,
                        Commands.X
                    )
                }
                SimpleButton(Res.drawable.Right_Arrow, "Go next") {
                    sendCommand(
                        udpClient,
                        Commands.Y
                    )
                }
            }
        }
        /*
        Column(modifier = Modifier.fillMaxSize(), verticalArrangement = Arrangement.SpaceBetween) {
            Text(
                "^TOUCH^GRASS^",
                fontWeight = FontWeight.Bold,
                modifier = Modifier.align(Alignment.CenterHorizontally)
                    .padding(top = 25.dp, bottom = 10.dp),
                color = touchGrassColor
            )

            Image(painterResource(Res.drawable.Shutdown), null)

            MainGrid(udpClient)

            FloatingActionButton(
                onClick = {
                    openAlertDialog.value = true
                    udpClient.disconnect()
                },
                modifier = Modifier
                    .align(Alignment.End)
                    .padding(16.dp),
                backgroundColor = touchGrassColor
            ) {
                Icon(
                    Icons.Default.Close,
                    contentDescription = "Close app",
                    modifier = Modifier.size(50.dp),
                    tint = Color.LightGray
                )
            }
        }*/

    }

}

@Composable
fun SimpleButton(
    resourceDrawable: DrawableResource,
    contentDescription: String,
    size: Dp = 30.dp,
    onTap: () -> Unit
) {
    Box(
        modifier = Modifier.clip(RoundedCornerShape(50.dp)).clickable(onClick = { onTap() },
            indication = ripple(),
            interactionSource = remember { MutableInteractionSource() })
    ) {
        Column(
            modifier = Modifier.width(90.dp).background(Palette.DarkGray)
                .padding(horizontal = 25.dp, vertical = 30.dp),
            horizontalAlignment = Alignment.CenterHorizontally
        ) {
            Image(
                painterResource(resourceDrawable),
                contentDescription,
                modifier = Modifier.size(size)
            )
        }
    }
}


@Composable
fun PlusMinusButton(label: String, onPlus: () -> Unit, onMinus: () -> Unit) {
    Box(modifier = Modifier.clip(RoundedCornerShape(50.dp))) {
        Column(
            modifier = Modifier.width(90.dp).background(Palette.DarkGray),
            horizontalAlignment = Alignment.CenterHorizontally
        ) {
            SimpleButton(Res.drawable.Plus_Math, "Increase") { onPlus() }
            Spacer(modifier = Modifier.height(50.dp))
            Text(label.uppercase(), fontSize = 18.sp, color = Palette.White)
            Spacer(modifier = Modifier.height(50.dp))
            SimpleButton(Res.drawable.Minus_Math, "Decrease") { onMinus() }
        }
    }
}

private val buttons: List<Pair<String, Commands>> = listOf(
    "-10 sec" to Commands.A,
    "+10 sec" to Commands.B,
    "Skip intro" to Commands.X,
    "Volume up" to Commands.UP,
    "Volume down" to Commands.DOWN,
    "Next" to Commands.Y,
    "Pause" to Commands.START,
)


fun sendCommand(udpClient: UdpClient, cmd: Commands) {
    performVibration()
    println("Pressed: $cmd")
    CoroutineScope(Dispatchers.IO).launch {
        udpClient.send(cmd.code.toString())
    }
}

@Composable
fun AlertDialogExample(
    onConfirmation: (String) -> Unit,
) {
    val ip = remember { mutableStateOf("192.168.1.") }
    val ipRegex =
        "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$".toRegex()
    val ipRegexPartial =
        "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){0,3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)?\$".toRegex()
    AlertDialog(
        title = {
            Text("CONNECT TO SERVER", textAlign = TextAlign.Center)
        },
        text = {
            Text("")
            TextField(
                value = ip.value,
                onValueChange = { newIp ->
                    println(newIp)
                    if (newIp.matches(ipRegexPartial) || newIp.isEmpty()) {
                        ip.value =
                            newIp // Only update if the value matches the IP regex or is empty
                    }
                },
                keyboardOptions = KeyboardOptions.Default.copy(keyboardType = KeyboardType.Number),
                textStyle = TextStyle.Default.copy(
                    textAlign = TextAlign.Center,
                    fontSize = 16.sp
                ),
                modifier = Modifier.padding(15.dp).fillMaxWidth()
            )

        },
        onDismissRequest = {
            // do nothing
        },
        confirmButton = {
            TextButton(
                onClick = {
                    if (ip.value.matches(ipRegex)) {
                        println("Connecting to ${ip.value}")
                        onConfirmation(ip.value)
                    }

                }
            ) {
                Text("Connect")
            }
        },
    )
}

