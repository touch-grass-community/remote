import io.ktor.network.sockets.*
import io.ktor.network.selector.*
import io.ktor.utils.io.core.ByteReadPacket
import io.ktor.utils.io.core.toByteArray

class UdpClient {
    private val selectorManager = SelectorManager()
    private lateinit var socket : BoundDatagramSocket
    private lateinit var targetAddress : InetSocketAddress
    fun connect(host: String, port: Int) {
        socket = aSocket(selectorManager).udp().bind(null)
        targetAddress = InetSocketAddress(host, port)
        println("Connected")
    }

    fun disconnect() {
        socket.close()
        println("Disconnected")
    }

    suspend fun send(message: String) {
        // Send a message
        socket.outgoing.send(Datagram(packet = ByteReadPacket(message.toByteArray()), address = targetAddress))
    }
}