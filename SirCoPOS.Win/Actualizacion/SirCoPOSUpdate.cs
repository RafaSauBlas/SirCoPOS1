using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Deployment.Application;
using System.Windows.Forms;

namespace SirCoPOS.Win.Actualizacion
{
    public class SirCoPOSUpdate
    {
        public void actualizasirco()
        {
            UpdateCheckInfo info = null;
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                try
                {
                    info = ad.CheckForDetailedUpdate();
                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("No se pudo descargar la actualización, por favor verifique su conexion a la red o intentelo más tarde. Error: " + dde);
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("No se ha podido buscar una nueva versión, probablemente el manifiesto de la aplicación esté corrupto. Intente reinstalando la aplicación. Error: " + ide.Message);
                    return;
                }
                if (info.UpdateAvailable)
                {
                    if (!info.IsUpdateRequired)
                    {
                        try
                        {
                        ad.Update();
                            MessageBox.Show("Para poder aplicar el cambio de versión la aplicación se reiniciará.", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBox.Show("No se pudo descargar la actualización, por favor verifique su conexion a la red o intentelo más tarde. Error: " + dde);
                            return;
                        }
                    }
                }
            }
        }
    }
}
