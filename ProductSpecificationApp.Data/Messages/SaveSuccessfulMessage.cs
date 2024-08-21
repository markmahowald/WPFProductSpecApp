using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ProductSpecificationApp.Data.Messages
{
    public class SaveSuccessfulMessage : ValueChangedMessage<(Type ObjectType, int ObjectId)>
    {
        public SaveSuccessfulMessage(Type objectType, int objectId, bool success) : base((objectType, objectId))
        {
        }
    }
}