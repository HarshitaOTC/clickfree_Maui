package mono.com.telerik.widget.calendar;


public class RadCalendarView_OnFloatingActionButtonClickedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.calendar.RadCalendarView.OnFloatingActionButtonClickedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_OnFloatingActionButtonClicked:()V:GetOnFloatingActionButtonClickedHandler:Com.Telerik.Widget.Calendar.RadCalendarView/IOnFloatingActionButtonClickedListenerInvoker, Telerik.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Calendar.RadCalendarView+IOnFloatingActionButtonClickedListenerImplementor, Telerik.Android.Input", RadCalendarView_OnFloatingActionButtonClickedListenerImplementor.class, __md_methods);
	}


	public RadCalendarView_OnFloatingActionButtonClickedListenerImplementor ()
	{
		super ();
		if (getClass () == RadCalendarView_OnFloatingActionButtonClickedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Calendar.RadCalendarView+IOnFloatingActionButtonClickedListenerImplementor, Telerik.Android.Input", "", this, new java.lang.Object[] {  });
	}


	public void OnFloatingActionButtonClicked ()
	{
		n_OnFloatingActionButtonClicked ();
	}

	private native void n_OnFloatingActionButtonClicked ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
