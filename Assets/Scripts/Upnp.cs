using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Open.Nat;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Upnp : MonoBehaviour
{
    public void Start()
    {
        Test().Wait(1000);
    }

    private static Task Test()
    {
        var nat = new NatDiscoverer();
        var cts = new CancellationTokenSource();
        cts.CancelAfter(5000);

        NatDevice device = null;
        var sb = new StringBuilder();
        IPAddress ip = null;

        return nat.DiscoverDeviceAsync(PortMapper.Upnp, cts)
            .ContinueWith(task =>
            {
                device = task.Result;
                return device.GetExternalIPAsync();

            })
            .Unwrap()
            .ContinueWith(task =>
            {
                ip = task.Result;
                sb.AppendFormat("\nYour IP: {0}", ip);
                return device.CreatePortMapAsync(new Mapping(Protocol.Tcp, 7777, 7777, 0, "myGame Server (TCP)"));
            })
            .Unwrap()
            .ContinueWith(task =>
            {
                return device.CreatePortMapAsync(
                    new Mapping(Protocol.Udp, 7777, 7777, 0, "myGame Server (UDP)"));
            })
            .Unwrap()
            .ContinueWith(task =>
            {
                sb.AppendFormat("\nAdded mapping: {0}:7777 -> 127.0.0.1:7777\n", ip);
                sb.AppendFormat("\n+------+-------------------------------+--------------------------------+------------------------------------+-------------------------+");
                sb.AppendFormat("\n| PORT | PUBLIC (Reacheable)           | PRIVATE (Your computer)        | Description                        |                         |");
                sb.AppendFormat("\n+------+----------------------+--------+-----------------------+--------+------------------------------------+-------------------------+");
                sb.AppendFormat("\n|      | IP Address           | Port   | IP Address            | Port   |                                    | Expires                 |");
                sb.AppendFormat("\n+------+----------------------+--------+-----------------------+--------+------------------------------------+-------------------------+");
                return device.GetAllMappingsAsync();
            })
            .Unwrap()
            .ContinueWith(task =>
            {
                foreach (var mapping in task.Result)
                {
                    sb.AppendFormat("\n|  {5} | {0,-20} | {1,6} | {2,-21} | {3,6} | {4,-35}|{6,25}|",
                        ip, mapping.PublicPort, mapping.PrivateIP, mapping.PrivatePort, mapping.Description,
                        mapping.Protocol == Protocol.Tcp ? "TCP" : "UDP", mapping.Expiration.ToLocalTime());
                }
                sb.AppendFormat("\n+------+----------------------+--------+-----------------------+--------+------------------------------------+-------------------------+");
                sb.AppendFormat("\n[Removing TCP mapping] {0}:7777 -> 127.0.0.1:7777", ip);
                return device.DeletePortMapAsync(new Mapping(Protocol.Tcp, 1600, 1700));
            })
            .Unwrap()
            .ContinueWith(task =>
            {
                sb.AppendFormat("\n[Done]");
                Debug.Log(sb.ToString());
            });
    }
}

