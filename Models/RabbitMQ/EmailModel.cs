using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamidas.Utils.Models.RabbitMQ;

record EmailModel(string Recipient, string Body, string Subject);
