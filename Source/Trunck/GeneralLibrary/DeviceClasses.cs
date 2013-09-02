using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace LJH.GeneralLibrary
{
    /// <summary>
    /// 表示计算机硬件类别
    /// </summary>
    public class DeviceClasses
    {
        public static Guid ClassesGuid;
        public const int MAX_SIZE_DEVICE_DESCRIPTION = 1000;
        public const int CR_SUCCESS = 0x00000000;
        public const int CR_NO_SUCH_VALUE = 0x00000025;
        public const int CR_INVALID_DATA = 0x0000001F;
        private const int DIGCF_PRESENT = 0x00000002;
        private const int DIOCR_INSTALLER = 0x00000001;
        private const int MAXIMUM_ALLOWED = 0x02000000;
        public const int DMI_MASK = 0x00000001;
        public const int DMI_BKCOLOR = 0x00000002;
        public const int DMI_USERECT = 0x00000004;

        [StructLayout(LayoutKind.Sequential)]
        class SP_DEVINFO_DATA
        {
            public int cbSize;
            public Guid ClassGuid;
            public int DevInst;
            public ulong Reserved;
        }

        [DllImport("cfgmgr32.dll")]
        private static extern UInt32 CM_Enumerate_Classes(UInt32 ClassIndex, ref Guid ClassGuid, UInt32 Params);

        [DllImport("setupapi.dll")]
        private static extern Boolean SetupDiClassNameFromGuidA(ref Guid ClassGuid, StringBuilder ClassName, UInt32 ClassNameSize, ref UInt32 RequiredSize);

        [DllImport("setupapi.dll")]
        private static extern IntPtr SetupDiGetClassDevsA(ref Guid ClassGuid, UInt32 Enumerator, IntPtr hwndParent, UInt32 Flags);

        [DllImport("setupapi.dll")]
        private static extern Boolean SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        [DllImport("setupapi.dll")]
        private static extern IntPtr SetupDiOpenClassRegKeyExA(ref Guid ClassGuid, UInt32 samDesired, int Flags, IntPtr MachineName, UInt32 Reserved);

        [DllImport("setupapi.dll")]
        private static extern Boolean SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, UInt32 MemberIndex, SP_DEVINFO_DATA DeviceInfoData);

        [DllImport("advapi32.dll")]
        private static extern UInt32 RegQueryValueA(IntPtr KeyClass, UInt32 SubKey, StringBuilder ClassDescription, ref UInt32 sizeB);

        /// <summary>
        /// 设备类型图标信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class SP_CLASSIMAGELIST_DATA
        {
            public int cbSize;
            public ImageList ImageList;
            public ulong Reserved;
        }
        public struct RECT
        {
            long left;
            long top;
            long right;
            long bottom;
        }

        /// <summary>
        /// 载入图片
        /// </summary>
        /// <param name="hInstance"></param>
        /// <param name="Reserved"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int LoadBitmapW(int hInstance, ulong Reserved);

        /// <summary>
        /// 获取图标
        /// </summary>
        /// <param name="ClassImageListData"></param>
        /// <returns></returns>
        [DllImport("setupapi.dll")]
        public static extern Boolean SetupDiGetClassImageList(out SP_CLASSIMAGELIST_DATA ClassImageListData);
        [DllImport("setupapi.dll")]
        public static extern int SetupDiDrawMiniIcon(Graphics hdc, RECT rc, int MiniIconIndex, int Flags);
        [DllImport("setupapi.dll")]
        public static extern bool SetupDiGetClassBitmapIndex(Guid ClassGuid, out int MiniIconIndex);
        [DllImport("setupapi.dll")]
        public static extern int SetupDiLoadClassIcon(ref Guid classGuid, out IntPtr hIcon, out int index);

        /// <summary>
        /// 枚举设备类型
        /// </summary>
        /// <param name="ClassIndex"></param>
        /// <param name="ClassName"></param>
        /// <param name="ClassDescription"></param>
        /// <param name="DevicePresent"></param>
        /// <returns></returns>
        public static int EnumerateClasses(UInt32 ClassIndex, StringBuilder ClassName, StringBuilder ClassDescription, ref bool DevicePresent)
        {
            Guid ClassGuid = Guid.Empty;
            IntPtr NewDeviceInfoSet;
            UInt32 result;
            SP_DEVINFO_DATA DeviceInfoData = new SP_DEVINFO_DATA();
            bool resNam = false;
            UInt32 RequiredSize = 0;
            result = CM_Enumerate_Classes(ClassIndex, ref ClassGuid, 0);
            DevicePresent = false;
            SP_CLASSIMAGELIST_DATA imagelist = new SP_CLASSIMAGELIST_DATA();
            if (result != CR_SUCCESS)
            {
                return (int)result;
            }
            resNam = SetupDiClassNameFromGuidA(ref ClassGuid, ClassName, RequiredSize, ref RequiredSize);
            if (RequiredSize > 0)
            {
                ClassName.Capacity = (int)RequiredSize;
                resNam = SetupDiClassNameFromGuidA(ref ClassGuid, ClassName, RequiredSize, ref RequiredSize);
            }
            NewDeviceInfoSet = SetupDiGetClassDevsA(ref ClassGuid, 0, IntPtr.Zero, DIGCF_PRESENT);
            if (NewDeviceInfoSet.ToInt32() == -1)
            {
                DevicePresent = false;
                return 0;
            }

            UInt32 numD = 0;
            DeviceInfoData.cbSize = 28;
            DeviceInfoData.DevInst = 0;
            DeviceInfoData.ClassGuid = System.Guid.Empty;
            DeviceInfoData.Reserved = 0;

            Boolean res1 = SetupDiEnumDeviceInfo(
            NewDeviceInfoSet,
            numD,
            DeviceInfoData);

            if (!res1)
            {
                DevicePresent = false;
                return 0;
            }
            SetupDiDestroyDeviceInfoList(NewDeviceInfoSet);
            IntPtr KeyClass = SetupDiOpenClassRegKeyExA(
                ref ClassGuid, MAXIMUM_ALLOWED, DIOCR_INSTALLER, IntPtr.Zero, 0);
            if (KeyClass.ToInt32() == -1)
            {
                DevicePresent = false;
                return 0;
            }
            UInt32 sizeB = MAX_SIZE_DEVICE_DESCRIPTION;
            ClassDescription.Capacity = MAX_SIZE_DEVICE_DESCRIPTION;
            UInt32 res = RegQueryValueA(KeyClass, 0, ClassDescription, ref sizeB);
            if (res != 0) ClassDescription = new StringBuilder(""); //No device description
            DevicePresent = true;
            ClassesGuid = DeviceInfoData.ClassGuid;
            return 0;
        }

        /// <summary>
        /// 是否存在名称以deviceDescr开头的设备
        /// </summary>
        /// <param name="deviceDescr"></param>
        /// <returns></returns>
        public static Boolean ExistsDevice(string deviceDescr)
        {
            bool DevExist = false;
            UInt32 classIndex = 0;
            while (true)
            {
                StringBuilder classes = new StringBuilder("");
                StringBuilder classDescriptions = new StringBuilder("");
                int res = DeviceClasses.EnumerateClasses(classIndex, classes, classDescriptions, ref DevExist);
                if (res == DeviceClasses.CR_NO_SUCH_VALUE)
                {
                    break;
                }
                classIndex++;
                if ((res != DeviceClasses.CR_NO_SUCH_VALUE && res != DeviceClasses.CR_SUCCESS) || !DevExist)
                {
                    continue;
                }

                StringBuilder devices = new StringBuilder("");
                int result = 0;
                UInt32 deviceIndex = 0;
                while (true)
                {
                    result = DeviceInfo.EnumerateDevices(deviceIndex, classes.ToString(), devices);
                    deviceIndex++;
                    if (result == -2)
                    {
                        break;
                    }
                    if (result == -1)
                    {
                        break;
                    }
                    if (result == 0)
                    {
                        if (devices.ToString().IndexOf(deviceDescr) >= 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 寻找名称以deviceDescr开头的串口设备,
        /// </summary>
        /// <param name="deviceDescr"></param>
        /// <param name="deviceDescrMore">返回设备的完整名称</param>
        /// <returns></returns>
        public static  Boolean FindPortDevice(string deviceDescr, StringBuilder deviceDescrMore)
        {
            string classes = "Ports";
            StringBuilder devices = new StringBuilder("");
            int result = 0;
            UInt32 deviceIndex = 0;
            while (true)
            {
                result = DeviceInfo.EnumerateDevices(deviceIndex, classes, devices);
                deviceIndex++;
                if (result == -2)
                {
                    break;
                }
                if (result == -1)
                {
                    break;
                }
                if (result == 0)
                {
                    if (devices.ToString().IndexOf(deviceDescr) >= 0)
                    {
                        deviceDescrMore.Append(devices);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
