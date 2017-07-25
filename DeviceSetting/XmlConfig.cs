using System.Xml.Linq;

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

        //public XmlConfig(int id, string name, string libarayName, string className, string entryPoint)
        //{
        //    Id = id;
        //    Name = name;
        //    LibarayName = libarayName;
        //    ClassName = className;
        //    EntryPoint = entryPoint;
        //}

        // 创建默认的 DeviceConfig.xml 并保存于根目录
        public static XDocument Create()
        {
            #region CreateXml

            var xdc = new XDocument(
                new XDeclaration("1.0", "utf-8", "no"),
                new XElement("ROOT",
                    new XElement("DevicePath", "Device"),
                    new XElement("CurrrentDevice",
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
                                new XElement("Timer", "1c")
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
                                    new XElement("Value", "1c"),
                                    new XElement("Value", "011c")
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
                                new XElement("BaudRate", "9600")
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
