using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace AffCheckSum
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            serviceInstaller1.ServiceName = "AffCheckSum";
            serviceInstaller1.DisplayName = "AffCheckSum";
            serviceInstaller1.Description = "AffCheckSum";
        }
    }
}
