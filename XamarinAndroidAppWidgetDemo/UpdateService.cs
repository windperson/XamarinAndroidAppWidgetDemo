using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using System;

namespace XamarinAndroidAppWidgetDemo
{
    [Service]
    public class UpdateService : Service
    {
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            OnStart(intent, startId);
#pragma warning restore CS0618 // Type or member is obsolete
            return StartCommandResult.Sticky;
        }

        [Obsolete("deprecated")]
        public override void OnStart(Intent intent, int startId)
        {
            // Build the widget update for today
            RemoteViews updateViews = buildUpdate(this);

            // Push update for this widget to the home screen
            ComponentName thisWidget = new ComponentName(this, Java.Lang.Class.FromType(typeof(DemoWidget)).Name);
            AppWidgetManager manager = AppWidgetManager.GetInstance(this);
            manager.UpdateAppWidget(thisWidget, updateViews);
        }

        public RemoteViews buildUpdate(Context context)
        {
            var entry = BlogPost.GetBlogPost();

            // Build an update that holds the updated widget contents
            var updateViews = new RemoteViews(context.PackageName, Resource.Layout.widget_blog_entry);

            updateViews.SetTextViewText(Resource.Id.blog_title, entry.Title);
            updateViews.SetTextViewText(Resource.Id.creator, entry.Creator);

            // When user clicks on widget, launch to Wiktionary definition page
            if (!string.IsNullOrEmpty(entry.Link))
            {
                Intent defineIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(entry.Link));

                PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, defineIntent, 0);
                updateViews.SetOnClickPendingIntent(Resource.Id.widget, pendingIntent);
            }

            return updateViews;
        }


        //Class inheritance required override
        public override IBinder OnBind(Intent intent)
        {
            //do nothing
            return null;
        }
    }
}