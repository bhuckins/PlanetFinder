<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Echo.aspx.cs" Inherits="PlanetFinder.Echo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <section class="row" aria-labelledby="title">
            <h1 id="title">Echo</h1>
        </section>

        <div class="row">
            <section class="col-md-6" aria-labelledby="echoInputTitle">
                <h2 id="echoInputTitle">Input</h2>
                <p>
                    Please enter the text you want echoed below
                </p>
                <p>
                    <asp:TextBox ID="txtEchoInput" runat="server" Width="200px" />
                    <br />
                    <br />
                    <asp:Button ID="btnEcho" runat="server" Text="Echo" OnClick="btnEcho_Click" />
                </p>
            </section>
            <section class="col-md-6" aria-labelledby="echoOutputTitle">
                <h2 id="echoOutputTitle">Output</h2>
                <p>
                    Here is your echoed text
                </p>
                <p>
                    <asp:TextBox ID="txtEchoOutput" runat="server" ReadOnly="true" BackColor="LightGray" Width="200px" />
                </p>
            </section>
        </div>
    </main>
</asp:Content>
