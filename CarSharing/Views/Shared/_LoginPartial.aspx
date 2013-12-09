﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<% if (Request.IsAuthenticated) { %>
    Hallo <%: Html.ActionLink(User.Identity.Name, "Manage", "Account", routeValues: null, htmlAttributes: new { @class = "username", title = "Verwalten" }) %>!
    <% using (Html.BeginForm("LogOff", "Account", FormMethod.Post, new { id = "logoutForm" })) { %>
        <%: Html.AntiForgeryToken() %>
        <a href="javascript:document.getElementById('logoutForm').submit()">Abmelden</a>
    <% } %>
<% } else { %>    
        <p  style="margin-top: 0px;><%: Html.ActionLink("Registrieren", "Register", "Account", routeValues: null, htmlAttributes: new { id = "registerLink" })%>
        <%: Html.ActionLink("Anmelden", "Login", "Account", routeValues: null, htmlAttributes: new { id = "loginLink" })%></p>
<% } %>