package mono.com.telerik.widget.calendar.agendaview;


public class AgendaViewItemClickedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.calendar.agendaview.AgendaViewItemClickedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onItemClicked:(Lcom/telerik/widget/calendar/agendaview/CalendarAgendaItemBase;)V:GetOnItemClicked_Lcom_telerik_widget_calendar_agendaview_CalendarAgendaItemBase_Handler:Com.Telerik.Widget.Calendar.Agendaview.IAgendaViewItemClickedListenerInvoker, Telerik.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Calendar.Agendaview.IAgendaViewItemClickedListenerImplementor, Telerik.Android.Input", AgendaViewItemClickedListenerImplementor.class, __md_methods);
	}


	public AgendaViewItemClickedListenerImplementor ()
	{
		super ();
		if (getClass () == AgendaViewItemClickedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Calendar.Agendaview.IAgendaViewItemClickedListenerImplementor, Telerik.Android.Input", "", this, new java.lang.Object[] {  });
	}


	public void onItemClicked (com.telerik.widget.calendar.agendaview.CalendarAgendaItemBase p0)
	{
		n_onItemClicked (p0);
	}

	private native void n_onItemClicked (com.telerik.widget.calendar.agendaview.CalendarAgendaItemBase p0);

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
