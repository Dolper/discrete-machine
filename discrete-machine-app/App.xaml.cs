﻿using discrete_machine;
using discrete_machine_app.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace discrete_machine_app
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public MachineAdapter Machine { get; set; }
    }
}
