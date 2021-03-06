﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ICollection<Microsoft.Web.WebPages.OAuth.AuthenticationClientData>>" %>

<% if (Model.Count == 0) { %>
    <div class="message-info">
        <p>Es sind keine externen Authentifizierungsdienste konfiguriert. In <a href="http://go.microsoft.com/fwlink/?LinkId=252166">diesem Artikel</a>
        finden Sie weitere Informationen zum Einrichten dieser ASP.NET-Anwendung für die Unterstützung der Anmeldung über externe Dienste.</p>
    </div>
<% } else {
    using (Html.BeginForm("ExternalLogin", "Account", new { ReturnUrl = ViewBag.ReturnUrl })) { %>
    <%: Html.AntiForgeryToken() %>
    <fieldset id="socialLoginList">
        <legend>Mit einem anderen Dienst anmelden</legend>
        <p>
        <% foreach (Microsoft.Web.WebPages.OAuth.AuthenticationClientData p in Model) { %>
            <button type="submit" name="provider" value="<%: p.AuthenticationClient.ProviderName %>" title="Anmelden mithilfe Ihres <%: p.DisplayName %> Kontos"><%: p.DisplayName%></button>
        <% } %>
        </p>
    </fieldset>
    <% }
} %>
