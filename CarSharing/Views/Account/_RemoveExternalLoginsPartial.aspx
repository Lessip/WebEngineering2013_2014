<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ICollection<CarSharing.Models.ExternalLogin>>" %>

<% if (Model.Count > 0) { %>
    <h3>Registrierte externe Anmeldungen</h3>
    <table>
        <tbody>
        <% foreach (CarSharing.Models.ExternalLogin externalLogin in Model) { %>
            <tr>
                <td><%: externalLogin.ProviderDisplayName %></td>
                <td>
                    <% if (ViewBag.ShowRemoveButton) {
                        using (Html.BeginForm("Disassociate", "Account")) { %>
                            <%: Html.AntiForgeryToken() %>
                            <div>
                                <%: Html.Hidden("provider", externalLogin.Provider) %>
                                <%: Html.Hidden("providerUserId", externalLogin.ProviderUserId) %>
                                <input type="submit" value="Entfernen" title="Dieses entfernen: <%: externalLogin.ProviderDisplayName %> Anmeldeinformationen aus Ihrem Konto" />
                            </div>
                        <% }
                    } else { %>
                        &nbsp;
                    <% } %>
                </td>
            </tr>
        <% } %>
        </tbody>
    </table>
<% } %>
