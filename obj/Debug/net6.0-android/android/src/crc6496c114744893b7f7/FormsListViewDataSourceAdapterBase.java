package crc6496c114744893b7f7;


public abstract class FormsListViewDataSourceAdapterBase
	extends com.telerik.widget.list.ListViewAdapterBase
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateListViewHolder:(Landroid/view/ViewGroup;I)Lcom/telerik/widget/list/ListViewHolder;:GetOnCreateListViewHolder_Landroid_view_ViewGroup_IHandler\n" +
			"n_onViewRecycled:(Lcom/telerik/widget/list/ListViewHolder;)V:GetOnViewRecycled_Lcom_telerik_widget_list_ListViewHolder_Handler\n" +
			"n_reorderItem:(II)Z:GetReorderItem_IIHandler\n" +
			"n_updateLayoutParams:(Lcom/telerik/widget/list/ListViewHolder;Z)V:GetUpdateLayoutParams_Lcom_telerik_widget_list_ListViewHolder_ZHandler\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.Controls.Compatibility.DataControlsRenderer.Android.FormsListViewDataSourceAdapterBase, Telerik.Maui.Controls.Compatibility", FormsListViewDataSourceAdapterBase.class, __md_methods);
	}


	public FormsListViewDataSourceAdapterBase ()
	{
		super ();
		if (getClass () == FormsListViewDataSourceAdapterBase.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.DataControlsRenderer.Android.FormsListViewDataSourceAdapterBase, Telerik.Maui.Controls.Compatibility", "", this, new java.lang.Object[] {  });
	}


	public com.telerik.widget.list.ListViewHolder onCreateListViewHolder (android.view.ViewGroup p0, int p1)
	{
		return n_onCreateListViewHolder (p0, p1);
	}

	private native com.telerik.widget.list.ListViewHolder n_onCreateListViewHolder (android.view.ViewGroup p0, int p1);


	public void onViewRecycled (com.telerik.widget.list.ListViewHolder p0)
	{
		n_onViewRecycled (p0);
	}

	private native void n_onViewRecycled (com.telerik.widget.list.ListViewHolder p0);


	public boolean reorderItem (int p0, int p1)
	{
		return n_reorderItem (p0, p1);
	}

	private native boolean n_reorderItem (int p0, int p1);


	public void updateLayoutParams (com.telerik.widget.list.ListViewHolder p0, boolean p1)
	{
		n_updateLayoutParams (p0, p1);
	}

	private native void n_updateLayoutParams (com.telerik.widget.list.ListViewHolder p0, boolean p1);

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
