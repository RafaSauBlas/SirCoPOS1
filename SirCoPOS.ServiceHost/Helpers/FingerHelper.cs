using SecuGen.FDxSDKPro.Windows;
using SecuGen.SecuSearchSDK3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.ServiceHost.Helpers
{
    public class FingerHelper
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpLibFileName);
        private static bool is64Bit = IntPtr.Size == 8;
        public FingerHelper()
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var dir2 = System.IO.Directory.GetCurrentDirectory();

            var rootApplicationPath = AppDomain.CurrentDomain.BaseDirectory;
            var nativeBinaryPath = is64Bit
                ? Path.Combine(rootApplicationPath, @"SecuGen\x64\")
                : Path.Combine(rootApplicationPath, @"SecuGen\x86\");

            var files = System.IO.Directory.GetFiles(nativeBinaryPath);
            foreach (var file in files)
            {
                var filename = Path.GetFileName(file);
                var nfile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
                if (!File.Exists(nfile))
                {
                    File.Copy(file, nfile);
                }
                var hLib = FingerHelper.LoadLibrary(file);
            }

            InitFingerPrint();
            InitSecuSearch();            
            //SecuGen.FDxSDKPro.Windows.SGFingerPrintManager
        }
        private void InitFingerPrint()
        {
            m_FPM = new SGFingerPrintManager();            
            var iError = m_FPM.Init(SGFPMDeviceName.DEV_AUTO);
            if (iError != (int)SGFPMError.ERROR_NONE)
                throw new NotSupportedException();
        }
        private void InitSecuSearch()
        {
            SSearch = new SecuSearch();

            SSParam param;

            // How many candiates as results of search
            param.CandidateCount = 10;

            // How many engines to be created internally.
            // If the concurrency is zero, SecuSearch automatically determines the total count of the CPU cores and use all the cores.
            param.Concurrency = 0;

            // license file - Ask SecuGen with your volume number which bin\VolNoReader.exe returns.
            //param.LicenseFile = "./license.dat";
            param.LicenseFile = "";

            // Whether or not to allow any amount of fingerprint rotations.
            param.EnableRotation = true;

            SSError error = SSearch.InitializeEngine(param);
            switch (error)
            {
                case SSError.NONE:
                    Initialized = true;
                    break;
                case SSError.SECUSEARCHAPI_DLL_UNLOADED:
                    if (IntPtr.Size == 8)
                        Console.WriteLine("{0} is not loaded.", SSConstants.SECUSEARCH_API_DLL_NAME_64BIT);
                    else
                        Console.WriteLine("{0} is not loaded.", SSConstants.SECUSEARCH_API_DLL_NAME_32BIT);
                    break;
                case SSError.SET_LOCK_PAGE_PRIVILEGE:
                    Console.WriteLine("Cannot enable the SE_LOCK_MEMORY privilege");
                    break;
                case SSError.LICENSE_LOAD:
                case SSError.LICENSE_KEY:
                case SSError.LICENSE_EXPIRED:
                    Console.WriteLine("License file({0}) is missing or not valid", /*param.LicenseFile*/"lic");
                    break;
                default:
                    Console.WriteLine("failed to initialize SecuSearch(code = {0})", error);
                    break;
            }
        }
        public bool Initialized;
        private SGFingerPrintManager m_FPM;
        public SecuSearch SSearch;
        //public bool Verify(byte[] current, byte[] stored)
        //{
        //    int iError;
        //    bool matched = false;            

        //    iError = m_FPM.MatchTemplate(current, stored, SGFPMSecurityLevel.NORMAL, ref matched);

        //    if (iError == (int)SGFPMError.ERROR_NONE)
        //    {
        //        return matched;
        //    }
        //    else
        //        throw new NotSupportedException();
        //}
        public bool Verify(byte[] current, byte[] stored)
        {
            SSearch.ClearFPDB();
            var iError = SSearch.RegisterFP(stored, 1);
            if (iError != SSError.NONE)
                throw new NotSupportedException();
            uint id = 0;
            iError = SSearch.IdentifyFP(current, SSConfLevel.NORMAL, ref id);
            if (iError == SSError.IDENTIFICATION_FAIL)
                return false;
            if (iError != SSError.NONE)
                throw new NotSupportedException();
            return id == 1;
        }
        public int? Identify(byte[] current, IDictionary<int, byte[]> options)
        {
            SSearch.ClearFPDB();
            SSError iError;
            foreach (var option in options)
            {
                iError = SSearch.RegisterFP(option.Value, (uint)option.Key);
                if (iError != SSError.NONE)
                    throw new NotSupportedException();
            }
            uint id = 0;
            iError = SSearch.IdentifyFP(current, SSConfLevel.NORMAL, ref id);
            if (iError == SSError.IDENTIFICATION_FAIL)
                return null;
            if (iError != SSError.NONE)
                throw new NotSupportedException();
            return (int)id;
        }
    }
}
