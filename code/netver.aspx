<%@ Page Language="C#" %>
<html>
<body>
    <form id="form1" runat="server">
    <%
    
    string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
    using (Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(uninstallKey))
    {
	        string[] subkeys = rk.GetSubKeyNames();
                foreach (string s in subkeys)
                {
                    if (s.Equals("Microsoft .NET Framework 3.5 SP1"))
                        Response.Write("3.5 SP1 Installed");

                    if (s.Equals("Microsoft .NET Framework 3.5"))
                        Response.Write("3.5 Installed");

                    if (s.Equals("Microsoft .NET Framework 2.0"))
                        Response.Write("2.0 Installed");
                }
    }
    %>
    </form>
</body>
</html>