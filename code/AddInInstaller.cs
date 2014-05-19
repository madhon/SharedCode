// Code Access Security configuring installer by Mads Nissen 2005

// http://weblogs.asp.net/mnissen

 

[RunInstaller(true)]

public class Installer : System.Configuration.Install.Installer

{

 

    private readonly string installPolicyLevel = "Machine";

    private readonly string namedPermissionSet = "FullTrust";

    private readonly string codeGroupDescription = "AlphaLogix VSTO Permissions";

    private readonly string productName = "AlphaLogix VSTO Permissions";

    private readonly bool debugBreakOnInstall = false;

 

    private string codeGroupName = "";

 

    /// <summary>

    /// Gets a CodeGroup name based on the productname and URL evidence

    /// </summary>

    private string CodeGroupName

    {

        get

        {

            if (codeGroupName.Length == 0)

            {

                codeGroupName = "["+ productName +"] " + InstallDirectory;

            }

            return codeGroupName;

        }

    }

 

    /// <summary>

    /// Gets the installdirectory with a wildcard suffix for use with URL evidence

    /// </summary>

    private string InstallDirectory

    {

        get

        {

            // Get the install directory of the current installer

            string assemblyPath = this.Context.Parameters["assemblypath"];

            string installDirectory =

                assemblyPath.Substring(0, assemblyPath.LastIndexOf("\\"));

 

            if (!installDirectory.EndsWith(@"\"))

                installDirectory += @"\";

            installDirectory += "*";

 

            return installDirectory;

        }

    }

 

    public Installer()

    {

    }

 

    public override void Install(System.Collections.IDictionary stateSaver)

    {

        base.Install(stateSaver);

 

        try

        {

            ConfigureCodeAccessSecurity();

            // Method not able to persist configuration to config file:

            // SetPortalUrlFromInstallerParameter();

        }

        catch (Exception ex)

        {

            System.Windows.Forms.MessageBox.Show(ex.ToString());

            this.Rollback(stateSaver);

        }

    }

 

    /// <summary>

    /// Configures FullTrust for the entire installdirectory

    /// </summary>

    private void ConfigureCodeAccessSecurity()

    {

        

        PolicyLevel machinePolicyLevel = GetPolicyLevel();

 

        if (null == GetCodeGroup(machinePolicyLevel))

        {

            // Create a new FullTrust permission set

            PermissionSet permissionSet = new NamedPermissionSet(this.namedPermissionSet);

 

            IMembershipCondition membershipCondition =

                new UrlMembershipCondition(InstallDirectory);

 

            // Create the code group

            PolicyStatement policyStatement = new PolicyStatement(permissionSet);

            CodeGroup codeGroup = new UnionCodeGroup(membershipCondition, policyStatement);

            codeGroup.Description = this.codeGroupDescription;

            codeGroup.Name = this.codeGroupName;

 

            // Add the code group

            machinePolicyLevel.RootCodeGroup.AddChild(codeGroup);

 

            // Save changes

            SecurityManager.SavePolicy();

        }

    }

 

    /// <summary>

    /// Gets the currently defined policylevel

    /// </summary>

    /// <returns></returns>

    private PolicyLevel GetPolicyLevel()

    {

        // Find the machine policy level

        PolicyLevel machinePolicyLevel = null;

        System.Collections.IEnumerator policyHierarchy = SecurityManager.PolicyHierarchy();

 

        while (policyHierarchy.MoveNext())

        {

            PolicyLevel level = (PolicyLevel)policyHierarchy.Current;

            if (level.Label.CompareTo(installPolicyLevel) == 0)

            {

                machinePolicyLevel = level;

                break;

            }

        }

 

        if (machinePolicyLevel == null)

        {

            throw new ApplicationException(

                "Could not find Machine Policy level. Code Access Security " +

                "is not configured for this application."

                );

        }

        return machinePolicyLevel;

    }

 

    /// <summary>

    /// Gets current codegroup based on CodeGroupName at the given policylevel

    /// </summary>

    /// <param name="policyLevel"></param>

    /// <returns>null if not found</returns>

    private CodeGroup GetCodeGroup(PolicyLevel policyLevel)

    {

        foreach (CodeGroup codeGroup in policyLevel.RootCodeGroup.Children)

        {

            if (codeGroup.Name.CompareTo(CodeGroupName) == 0)

            {

                return codeGroup;

            }

        }

        return null;

    }

 

    public override void Uninstall(System.Collections.IDictionary savedState)

    {

        if (debugBreakOnInstall)

            System.Diagnostics.Debugger.Break();

 

        base.Uninstall(savedState);

        try

        {

            this.UninstallCodeAccessSecurity();

        }

        catch (Exception ex)

        {

            System.Windows.Forms.MessageBox.Show("Unable to uninstall code access security:\n\n" + ex.ToString());

        }

    }

 

    private void UninstallCodeAccessSecurity()

    {

        PolicyLevel machinePolicyLevel = GetPolicyLevel();

 

        CodeGroup codeGroup = GetCodeGroup(machinePolicyLevel);

        if(codeGroup != null)

        {

            machinePolicyLevel.RootCodeGroup.RemoveChild(codeGroup);

 

            // Save changes

            SecurityManager.SavePolicy();

        }

    }

 }

