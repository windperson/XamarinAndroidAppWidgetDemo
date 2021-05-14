using Android.App;
using Android.Appwidget;
using Android.Content;

namespace XamarinAndroidAppWidgetDemo
{
    [BroadcastReceiver(Label = "@string/widget_name")]
    [IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [MetaData ("android.appwidget.provider", Resource = "@xml/demo_widget")]
    public class DemoWidget : AppWidgetProvider
    {
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            base.OnUpdate(context, appWidgetManager, appWidgetIds);
            context.StartService(new Intent(context, typeof(UpdateService)));
        }
    }
}