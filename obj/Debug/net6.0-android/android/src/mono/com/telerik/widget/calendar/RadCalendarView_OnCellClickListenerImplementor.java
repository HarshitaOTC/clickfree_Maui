package mono.com.telerik.widget.calendar;


public class RadCalendarView_OnCellClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.calendar.RadCalendarView.OnCellClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCellClick:(Lcom/telerik/widget/calendar/CalendarCell;)V:GetOnCellClick_Lcom_telerik_widget_calendar_CalendarCell_Handler:Com.Telerik.Widget.Calendar.RadCalendarView/IOnCellClickListenerInvoker, Telerik.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Calendar.RadCalendarView+IOnCellClickListenerImplementor, Telerik.Android.Input", RadCalendarView_OnCellClickListenerImplementor.class, __md_methods);
	}


	public RadCalendarView_OnCellClickListenerImplementor ()
	{
		super ();
		if (getClass () == RadCalendarView_OnCellClickListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Calendar.RadCalendarView+IOnCellClickListenerImplementor, Telerik.Android.Input", "", this, new java.lang.Object[] {  });
	}


	public void onCellClick (com.telerik.widget.calendar.CalendarCell p0)
	{
		n_onCellClick (p0);
	}

	private native void n_onCellClick (com.telerik.widget.calendar.CalendarCell p0);

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
