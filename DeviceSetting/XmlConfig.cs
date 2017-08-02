﻿using System.Xml.Linq;

namespace DeviceSetting
{
    /// <summary>
    ///     用于操作 DeviceConfig.xml 和储存设备信息的类
    /// </summary>
    public class XmlConfig
    {
        public int Id;
        public string Name;
        public string LibarayName;
        public string ClassName;
        public string EntryPoint;

        // 创建默认的 DeviceConfig.xml 并保存于根目录
        public static XDocument Create()
        {
            #region CreateXml

            var xdc = new XDocument(
                new XDeclaration("1.0", "utf-8", "no"),
                new XElement("ROOT",
                    new XElement("DevicePath", "Device"),
                    new XElement("CurrentDevice",
                        new XAttribute("id", "2")),
                    new XElement("Devices",
                        new XElement("Device",
                            new XAttribute("id", "1"),
                            new XAttribute("Name", "ZLGCan"),
                            new XElement("LibarayName", "ZLGCan.dll"),
                            new XElement("Class", "ZLGCan.ZlgCanDevice"),
                            new XElement("EntryPoint", "GetInstance"),
                            new XElement("Param",
                                new XElement("DeviceType", "3"),
                                new XElement("DeviceIndex", "0"),
                                new XElement("CanIndex", "0"),
                                new XElement("Timer", "0x011C")
                            ),
                            new XElement("ParamConfigOption",
                                new XElement("DeviceType",
                                    new XElement("Type", "0"),
                                    new XElement("Type", "1"),
                                    new XElement("Type", "2"),
                                    new XElement("Type", "3"),
                                    new XElement("Type", "4")
                                ),
                                new XElement("DeviceIndex",
                                    new XElement("Index", "0"),
                                    new XElement("Index", "1")
                                ),
                                new XElement("CanIndex",
                                    new XElement("Index", "0"),
                                    new XElement("Index", "1")
                                ),
                                new XElement("Timer",
                                    new XElement("Value", "0xBFFF"),
                                    new XElement("Value", "0x311C"),
                                    new XElement("Value", "0x181C"),
                                    new XElement("Value", "0x87FF"),
                                    new XElement("Value", "0x091C"),
                                    new XElement("Value", "0x83FF"),
                                    new XElement("Value", "0x041C"),
                                    new XElement("Value", "0x031C"),
                                    new XElement("Value", "0x81FA"),
                                    new XElement("Value", "0x011C"),
                                    new XElement("Value", "0x80FA"),
                                    new XElement("Value", "0x001C"),
                                    new XElement("Value", "0x80B6"),
                                    new XElement("Value", "0x0016"),
                                    new XElement("Value", "0x0014")
                                )
                            )
                        ),
                        new XElement("Device",
                            new XAttribute("id", "2"),
                            new XAttribute("Name", "NetWork"),
                            new XElement("LibarayName", "NetWork.dll"),
                            new XElement("Class", "NetWork.UdpDevice"),
                            new XElement("EntryPoint", "GetInstance"),
                            new XElement("Param",
                                new XElement("IPAddress", "127.0.0.1"),
                                new XElement("Port", "8900")
                            ),
                            new XElement("ParamConfigOption",
                                new XElement("IPAddress",
                                    new XElement("Address", "127.0.0.1"),
                                    new XElement("Address", "192.168.1.1"),
                                    new XElement("Address", "10.0.0.1")
                                ),
                                new XElement("Port",
                                    new XElement("Number", "8900"),
                                    new XElement("Number", "80"),
                                    new XElement("Number", "8080"),
                                    new XElement("Number", "9080")
                                )
                            )
                        ),
                        new XElement("Device",
                            new XAttribute("id", "3"),
                            new XAttribute("Name", "SerialPort"),
                            new XElement("LibarayName", "SerialPort.dll"),
                            new XElement("Class", "SerialPort"),
                            new XElement("EntryPoint", "GetInstance"),
                            new XElement("Param",
                                new XElement("COM", "1"),
                                new XElement("BaudRate", "9600"),
                                new XElement("StopBit", "1"),
                                new XElement("DataBit", "8"),
                                new XElement("ParityCheck", "Odd")
                            ),
                            new XElement("ParamConfigOption",
                                new XElement("COM",
                                    new XElement("Index", "1"),
                                    new XElement("Index", "2"),
                                    new XElement("Index", "3")
                                ),
                                new XElement("BaudRate",
                                    new XElement("Rate", "4800"),
                                    new XElement("Rate", "9600"),
                                    new XElement("Rate", "19200")
                                ),
                                new XElement("StopBit",
                                    new XElement("Bit", "1"),
                                    new XElement("Bit", "1.5"),
                                    new XElement("Bit", "2")
                                ),
                                new XElement("DataBit",
                                    new XElement("Bit", "8"),
                                    new XElement("Bit", "7"),
                                    new XElement("Bit", "6"),
                                    new XElement("Bit", "5")
                                ),
                                new XElement("ParityCheck",
                                    new XElement("Check", "Odd"),
                                    new XElement("Check", "Even")
                                )
                            )
                        )
                    )
                )
            );
            
            #endregion

            xdc.Save("DeviceConfig.xml");
            return xdc;
        }

        // TODO:保存修改过后的配置文件
        public static void Save()
        {
        }
    }
}
