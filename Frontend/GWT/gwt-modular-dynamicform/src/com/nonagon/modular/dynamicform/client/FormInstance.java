package com.nonagon.modular.dynamicform.client;

import java.util.Date;

import com.google.gwt.core.client.JavaScriptObject;
import com.google.gwt.core.client.JsArray;

public class FormInstance extends JavaScriptObject {
	
	public static FormInstance create() {
		
		FormInstance instance = ((FormInstance)JavaScriptObject.createObject());
		instance.init();
		
		return instance;
	}
	
	protected final native void init()/*-{
		
		this.__type = "Nonagon.Modular.DynamicForm.FormInstance, Nonagon.Modular.DynamicForm";
	}-*/;

	protected FormInstance() {}
	
	public final native int getId()/*-{ return this.Id; }-*/;
	
	public final native String getReference()/*-{ return this.Reference; }-*/;
	public final native void setReference(String value)/*-{ this.Reference = value; }-*/;

	public final native int getFormId()/*-{ return this.FormId; }-*/;
	public final native void setFormId(int value)/*-{ this.FormId = value; }-*/;
	
	public final native int getFormRevisionId()/*-{ return this.FormRevisionId; }-*/;
	public final native void setFormRevisionId(int value)/*-{ this.FormRevisionId = value; }-*/;
	
	public final FormStatus getFormStatus() {
		
		String formStatus = jsniGetFormStatus();
		return FormStatus.fromString(formStatus);
	}
	
	public final void setFormStatus(FormStatus value) {
		
		String formStatus = FormStatus.fromEnum(value);
		jsniSetFormStatus(formStatus);
	}

	public final native JsArray<FormInstanceValue> getValues()/*-{ return this.Values; }-*/;
	public final native void setValues(JsArray<FormInstanceValue> value)/*-{ this.Values = value; }-*/;

	public final Date getCreatedDate() { return new Date((long)jsniGetCreatedDate()); } 
	public final Date getLastUpdatedDate() { return new Date((long)jsniGetLastUpdatedDate()); }

	private final native String jsniGetFormStatus()/*-{ return this.Status; }-*/;
	private final native void jsniSetFormStatus(String value)/*-{ this.Status = value; }-*/;
	
	private final native double jsniGetCreatedDate() /*-{ return Date.parse(this.CreatedDate); }-*/;
	private final native double jsniGetLastUpdatedDate() /*-{ return Date.parse(this.LastUpdatedDate); }-*/;
}
