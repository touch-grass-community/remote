import android.content.Context

object ApplicationContextSingleton {
    lateinit var appContext: Context

    fun initialize(context: Context) {
        appContext = context.applicationContext
    }
}
