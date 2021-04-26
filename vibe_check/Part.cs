using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vibe_check
{
    [Serializable]
    public class RequiredPart
    {
        public string name;
        public bool chosen;
        public bool alwaysChosen;

        public RequiredPart()
        {

        }

        public RequiredPart(string name, bool chosen, bool alwaysChosen)
        {
            this.name = name;
            this.chosen = chosen;
            this.alwaysChosen = alwaysChosen;
        }
    }

    [Serializable]
    public class Part
    {
        public string name;
        public List<string> subParts;

        public Part()
        {
        }

        public Part(string name, List<string> subParts)
        {
            this.name = name;
            this.subParts = subParts;
        }
    }

    [Serializable]
    public class Attachment
    {
        public string name;

        public Attachment()
        {

        }

        public Attachment(string name)
        {
            this.name = name;
        }
    }

    //[Serializable]
    //public class Save_
    //{
    //    public List<RequiredPart> requiredParts;
    //    public List<Attachment> Parts;
    //    public List<Attachment> attachments;

    //    public Save()
    //    {

    //    }

    //    public Save(List<RequiredPart> requiredParts, List<Attachment> parts, List<Attachment> attachments)
    //    {
    //        this.requiredParts = requiredParts;
    //        this.Parts = parts;
    //        this.attachments = attachments;
    //    }
    //}

    [Serializable]
    public class Save1
    {
        public List<RequiredPart> requiredParts;
        public List<Part> Parts;
        public List<Attachment> attachments;

        public Save1()
        {

        }

        public Save1(List<RequiredPart> requiredParts, List<Part> parts, List<Attachment> attachments)
        {
            this.requiredParts = requiredParts;
            this.Parts = parts;
            this.attachments = attachments;
        }
    }
}
