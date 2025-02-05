using System.Collections.Generic;
using SimpleConfigReader.Core.Models;

namespace SimpleConfigReader.Benchmarks.Stabs;

internal static class ConfigurationStab
{
    private static List<Configuration> s_stabConfigurations;

    static ConfigurationStab()
    {
        s_stabConfigurations = new List<Configuration>
        {
            new Configuration { Name = "Configuration00", Description = "Description00" },
            new Configuration { Name = "Configuration01", Description = "Description01" },
            new Configuration { Name = "Configuration02", Description = "Description02" },
            new Configuration { Name = "Configuration03", Description = "Description03" },
            new Configuration { Name = "Configuration04", Description = "Description04" },
            new Configuration { Name = "Configuration05", Description = "Description05" },
            new Configuration { Name = "Configuration06", Description = "Description06" },
            new Configuration { Name = "Configuration07", Description = "Description07" },
            new Configuration { Name = "Configuration08", Description = "Description08" },
            new Configuration { Name = "Configuration09", Description = "Description09" },

            new Configuration { Name = "Configuration10", Description = "Description10" },
            new Configuration { Name = "Configuration11", Description = "Description11" },
            new Configuration { Name = "Configuration12", Description = "Description12" },
            new Configuration { Name = "Configuration13", Description = "Description13" },
            new Configuration { Name = "Configuration14", Description = "Description14" },
            new Configuration { Name = "Configuration15", Description = "Description15" },
            new Configuration { Name = "Configuration16", Description = "Description16" },
            new Configuration { Name = "Configuration17", Description = "Description17" },
            new Configuration { Name = "Configuration18", Description = "Description18" },
            new Configuration { Name = "Configuration19", Description = "Description19" },

            new Configuration { Name = "Configuration20", Description = "Description20" },
            new Configuration { Name = "Configuration21", Description = "Description21" },
            new Configuration { Name = "Configuration22", Description = "Description22" },
            new Configuration { Name = "Configuration23", Description = "Description23" },
            new Configuration { Name = "Configuration24", Description = "Description24" },
            new Configuration { Name = "Configuration25", Description = "Description25" },
            new Configuration { Name = "Configuration26", Description = "Description26" },
            new Configuration { Name = "Configuration27", Description = "Description27" },
            new Configuration { Name = "Configuration28", Description = "Description28" },
            new Configuration { Name = "Configuration29", Description = "Description29" },

            new Configuration { Name = "Configuration30", Description = "Description30" },
            new Configuration { Name = "Configuration31", Description = "Description31" },
            new Configuration { Name = "Configuration32", Description = "Description32" },
            new Configuration { Name = "Configuration33", Description = "Description33" },
            new Configuration { Name = "Configuration34", Description = "Description34" },
            new Configuration { Name = "Configuration35", Description = "Description35" },
            new Configuration { Name = "Configuration36", Description = "Description36" },
            new Configuration { Name = "Configuration37", Description = "Description37" },
            new Configuration { Name = "Configuration38", Description = "Description38" },
            new Configuration { Name = "Configuration39", Description = "Description39" },

            new Configuration { Name = "Configuration40", Description = "Description40" },
            new Configuration { Name = "Configuration41", Description = "Description41" },
            new Configuration { Name = "Configuration42", Description = "Description42" },
            new Configuration { Name = "Configuration43", Description = "Description43" },
            new Configuration { Name = "Configuration44", Description = "Description44" },
            new Configuration { Name = "Configuration45", Description = "Description45" },
            new Configuration { Name = "Configuration46", Description = "Description46" },
            new Configuration { Name = "Configuration47", Description = "Description47" },
            new Configuration { Name = "Configuration48", Description = "Description48" },
            new Configuration { Name = "Configuration49", Description = "Description49" },

            new Configuration { Name = "Configuration50", Description = "Description50" },
            new Configuration { Name = "Configuration51", Description = "Description51" },
            new Configuration { Name = "Configuration52", Description = "Description52" },
            new Configuration { Name = "Configuration53", Description = "Description53" },
            new Configuration { Name = "Configuration54", Description = "Description54" },
            new Configuration { Name = "Configuration55", Description = "Description55" },
            new Configuration { Name = "Configuration56", Description = "Description56" },
            new Configuration { Name = "Configuration57", Description = "Description57" },
            new Configuration { Name = "Configuration58", Description = "Description58" },
            new Configuration { Name = "Configuration59", Description = "Description59" },

            new Configuration { Name = "Configuration60", Description = "Description60" },
            new Configuration { Name = "Configuration61", Description = "Description61" },
            new Configuration { Name = "Configuration62", Description = "Description62" },
            new Configuration { Name = "Configuration63", Description = "Description63" },
            new Configuration { Name = "Configuration64", Description = "Description64" },
            new Configuration { Name = "Configuration65", Description = "Description65" },
            new Configuration { Name = "Configuration66", Description = "Description66" },
            new Configuration { Name = "Configuration67", Description = "Description67" },
            new Configuration { Name = "Configuration68", Description = "Description68" },
            new Configuration { Name = "Configuration69", Description = "Description69" },

            new Configuration { Name = "Configuration70", Description = "Description70" },
            new Configuration { Name = "Configuration71", Description = "Description71" },
            new Configuration { Name = "Configuration72", Description = "Description72" },
            new Configuration { Name = "Configuration73", Description = "Description73" },
            new Configuration { Name = "Configuration74", Description = "Description74" },
            new Configuration { Name = "Configuration75", Description = "Description75" },
            new Configuration { Name = "Configuration76", Description = "Description76" },
            new Configuration { Name = "Configuration77", Description = "Description77" },
            new Configuration { Name = "Configuration78", Description = "Description78" },
            new Configuration { Name = "Configuration79", Description = "Description79" },

            new Configuration { Name = "Configuration80", Description = "Description80" },
            new Configuration { Name = "Configuration81", Description = "Description81" },
            new Configuration { Name = "Configuration82", Description = "Description82" },
            new Configuration { Name = "Configuration83", Description = "Description83" },
            new Configuration { Name = "Configuration84", Description = "Description84" },
            new Configuration { Name = "Configuration85", Description = "Description85" },
            new Configuration { Name = "Configuration86", Description = "Description86" },
            new Configuration { Name = "Configuration87", Description = "Description87" },
            new Configuration { Name = "Configuration88", Description = "Description88" },
            new Configuration { Name = "Configuration89", Description = "Description89" },

            new Configuration { Name = "Configuration90", Description = "Description90" },
            new Configuration { Name = "Configuration91", Description = "Description91" },
            new Configuration { Name = "Configuration92", Description = "Description92" },
            new Configuration { Name = "Configuration93", Description = "Description93" },
            new Configuration { Name = "Configuration94", Description = "Description94" },
            new Configuration { Name = "Configuration95", Description = "Description95" },
            new Configuration { Name = "Configuration96", Description = "Description96" },
            new Configuration { Name = "Configuration97", Description = "Description97" },
            new Configuration { Name = "Configuration98", Description = "Description98" },
            new Configuration { Name = "Configuration99", Description = "Description99" }
        };
    }

    internal static List<Configuration> GetConfigurations()
    {
        return s_stabConfigurations;
    }
}