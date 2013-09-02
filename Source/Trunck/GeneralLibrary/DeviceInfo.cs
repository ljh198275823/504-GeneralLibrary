using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LJH.GeneralLibrary
{
    /// <summary>
    /// 表示计算机硬件
    /// </summary>
    public class DeviceInfo
    {
        private const int DIGCF_PRESENT = (0x00000002);
        private const int MAX_DEV_LEN = 1000;
        private const int SPDRP_FRIENDLYNAME = (0x0000000C);
        private const int SPDRP_DEVICEDESC = (0x00000000);

        [StructLayout(LayoutKind.Sequential)]
        private class SP_DEVINFO_DATA
        {
            public int cbSize;
            public Guid ClassGuid;
            public int DevInst;
            public ulong Reserved;
        };
        [DllImport("setupapi.dll")]
        private static extern Boolean SetupDiClassGuidsFromNameA(string ClassN, ref Guid guids, UInt32 ClassNameSize, ref UInt32 ReqSize);

        [DllImport("setupapi.dll")]
        private static extern IntPtr SetupDiGetClassDevsA(ref Guid ClassGuid, UInt32 Enumerator, IntPtr hwndParent, UInt32 Flags);

        [DllImport("setupapi.dll")]
        private static extern Boolean SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, UInt32 MemberIndex, SP_DEVINFO_DATA DeviceInfoData);

        [DllImport("setupapi.dll")]
        private static extern Boolean SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        [DllImport("setupapi.dll")]
        private static extern Boolean SetupDiGetDeviceRegistryPropertyA(IntPtr DeviceInfoSet, SP_DEVINFO_DATA DeviceInfoData, UInt32 Property, UInt32 PropertyRegDataType, StringBuilder PropertyBuffer, UInt32 PropertyBufferSize, IntPtr RequiredSize);

        /// <summary>
        /// 通过设备类型枚举设备信息
        /// </summary>
        /// <param name="DeviceIndex"></param>
        /// <param name="ClassName"></param>
        /// <param name="DeviceName"></param>
        /// <returns></returns>
        public static int EnumerateDevices(UInt32 DeviceIndex, string ClassName, StringBuilder DeviceName)
        {
            UInt32 RequiredSize = 0;
            Guid guid = Guid.Empty;
            Guid[] guids = new Guid[1];
            IntPtr NewDeviceInfoSet;
            SP_DEVINFO_DATA DeviceInfoData = new SP_DEVINFO_DATA();


            bool res = SetupDiClassGuidsFromNameA(ClassName, ref guids[0], RequiredSize, ref RequiredSize);
            if (RequiredSize == 0)
            {
                //类型不正确
                DeviceName = new StringBuilder("");
                return -2;
            }

            if (!res)
            {
                guids = new Guid[RequiredSize];
                res = SetupDiClassGuidsFromNameA(ClassName, ref guids[0], RequiredSize, ref RequiredSize);

                if (!res || RequiredSize == 0)
                {
                    //类型不正确
                    DeviceName = new StringBuilder("");
                    return -2;
                }
            }

            //通过类型获取设备信息
            NewDeviceInfoSet = SetupDiGetClassDevsA(ref guids[0], 0, IntPtr.Zero, DIGCF_PRESENT);
            if (NewDeviceInfoSet.ToInt32() == -1)
            {
                //设备不可用
                DeviceName = new StringBuilder("");
                return -3;
            }

            DeviceInfoData.cbSize = 28;
            //正常状态
            DeviceInfoData.DevInst = 0;
            DeviceInfoData.ClassGuid = System.Guid.Empty;
            DeviceInfoData.Reserved = 0;

            res = SetupDiEnumDeviceInfo(NewDeviceInfoSet,
                   DeviceIndex, DeviceInfoData);
            if (!res)
            {
                //没有设备
                SetupDiDestroyDeviceInfoList(NewDeviceInfoSet);
                DeviceName = new StringBuilder("");
                return -1;
            }



            DeviceName.Capacity = MAX_DEV_LEN;
            if (!SetupDiGetDeviceRegistryPropertyA(NewDeviceInfoSet, DeviceInfoData,
            SPDRP_FRIENDLYNAME, 0, DeviceName, MAX_DEV_LEN, IntPtr.Zero))
            {
                res = SetupDiGetDeviceRegistryPropertyA(NewDeviceInfoSet,
                 DeviceInfoData, SPDRP_DEVICEDESC, 0, DeviceName, MAX_DEV_LEN, IntPtr.Zero);
                if (!res)
                {
                    //类型不正确
                    SetupDiDestroyDeviceInfoList(NewDeviceInfoSet);
                    DeviceName = new StringBuilder("");
                    return -4;
                }
            }
            return 0;
        }
    }
}
