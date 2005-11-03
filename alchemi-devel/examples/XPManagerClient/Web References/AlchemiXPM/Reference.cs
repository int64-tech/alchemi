﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 1.1.4322.573.
// 
namespace XPManagerClient.AlchemiXPM {
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="CrossPlatformManagerSoap", Namespace="http://www.alchemi.net")]
    public class CrossPlatformManager : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public CrossPlatformManager() {
            this.Url = "http://localhost/Alchemi.CrossPlatformManager/CrossPlatformManager.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.alchemi.net/GetJobState", RequestNamespace="http://www.alchemi.net", ResponseNamespace="http://www.alchemi.net", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int GetJobState(string username, string password, string taskId, int jobId) {
            object[] results = this.Invoke("GetJobState", new object[] {
                        username,
                        password,
                        taskId,
                        jobId});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetJobState(string username, string password, string taskId, int jobId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetJobState", new object[] {
                        username,
                        password,
                        taskId,
                        jobId}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGetJobState(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.alchemi.net/GetFinishedJobs", RequestNamespace="http://www.alchemi.net", ResponseNamespace="http://www.alchemi.net", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetFinishedJobs(string username, string password, string taskId) {
            object[] results = this.Invoke("GetFinishedJobs", new object[] {
                        username,
                        password,
                        taskId});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetFinishedJobs(string username, string password, string taskId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetFinishedJobs", new object[] {
                        username,
                        password,
                        taskId}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndGetFinishedJobs(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.alchemi.net/AddJob", RequestNamespace="http://www.alchemi.net", ResponseNamespace="http://www.alchemi.net", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void AddJob(string username, string password, string taskId, int jobId, int priority, string jobXml) {
            this.Invoke("AddJob", new object[] {
                        username,
                        password,
                        taskId,
                        jobId,
                        priority,
                        jobXml});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginAddJob(string username, string password, string taskId, int jobId, int priority, string jobXml, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("AddJob", new object[] {
                        username,
                        password,
                        taskId,
                        jobId,
                        priority,
                        jobXml}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndAddJob(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.alchemi.net/SubmitTask", RequestNamespace="http://www.alchemi.net", ResponseNamespace="http://www.alchemi.net", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SubmitTask(string username, string password, string taskXml) {
            object[] results = this.Invoke("SubmitTask", new object[] {
                        username,
                        password,
                        taskXml});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSubmitTask(string username, string password, string taskXml, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SubmitTask", new object[] {
                        username,
                        password,
                        taskXml}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndSubmitTask(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.alchemi.net/CreateTask", RequestNamespace="http://www.alchemi.net", ResponseNamespace="http://www.alchemi.net", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CreateTask(string username, string password) {
            object[] results = this.Invoke("CreateTask", new object[] {
                        username,
                        password});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCreateTask(string username, string password, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("CreateTask", new object[] {
                        username,
                        password}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndCreateTask(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.alchemi.net/Ping", RequestNamespace="http://www.alchemi.net", ResponseNamespace="http://www.alchemi.net", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Ping() {
            this.Invoke("Ping", new object[0]);
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginPing(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Ping", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public void EndPing(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.alchemi.net/ListLiveApps", RequestNamespace="http://www.alchemi.net", ResponseNamespace="http://www.alchemi.net", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet ListLiveApps() {
            object[] results = this.Invoke("ListLiveApps", new object[0]);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginListLiveApps(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ListLiveApps", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndListLiveApps(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.alchemi.net/GetApplicationState", RequestNamespace="http://www.alchemi.net", ResponseNamespace="http://www.alchemi.net", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int GetApplicationState(string username, string password, string taskId) {
            object[] results = this.Invoke("GetApplicationState", new object[] {
                        username,
                        password,
                        taskId});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetApplicationState(string username, string password, string taskId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetApplicationState", new object[] {
                        username,
                        password,
                        taskId}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGetApplicationState(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.alchemi.net/AbortTask", RequestNamespace="http://www.alchemi.net", ResponseNamespace="http://www.alchemi.net", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void AbortTask(string username, string password, string taskId) {
            this.Invoke("AbortTask", new object[] {
                        username,
                        password,
                        taskId});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginAbortTask(string username, string password, string taskId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("AbortTask", new object[] {
                        username,
                        password,
                        taskId}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndAbortTask(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.alchemi.net/AbortJob", RequestNamespace="http://www.alchemi.net", ResponseNamespace="http://www.alchemi.net", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void AbortJob(string username, string password, string taskId, int jobId) {
            this.Invoke("AbortJob", new object[] {
                        username,
                        password,
                        taskId,
                        jobId});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginAbortJob(string username, string password, string taskId, int jobId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("AbortJob", new object[] {
                        username,
                        password,
                        taskId,
                        jobId}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndAbortJob(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
    }
}
