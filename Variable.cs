using System;
using System.Collections.Generic;
using System.ComponentModel;
using FoxProFileStructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Migration
{
    public class Variable
    {
        private bool include;
        private bool primaryKey;
        private string vfpFieldName;
        private FieldType vfpFieldType;
        private string dotNetType;
        private string variableName;
        private string headerText;
        private bool visible;
        private bool search;
        private bool read;
        private bool update;
        private bool insert;
        private bool shallowCopy;
        private bool deepCopy;
        private byte foxProLength;
        private byte decimalPlaces;

        [DisplayName("Include")]
        [Browsable(true)]
        public bool Include
        {
            get
            {
                return this.include;
            }
            set
            {
                this.include = value;
            }
        }

        [DisplayName("Primary Key")]
        [Browsable(true)]
        public bool PrimaryKey
        {
            get
            {
                return this.primaryKey;
            }
            set
            {
                this.primaryKey = value;
            }
        }

        [DisplayName("Field Length")]
        [Browsable(true)]
        public byte FoxProLength
        {
            get
            {
                return this.foxProLength;
            }
            set
            {
                this.foxProLength = value;
            }
        }

        [DisplayName("Decimal Places")]
        [Browsable(true)]
        public byte DecimalPlaces
        {
            get
            {
                return this.decimalPlaces;
            }
            set
            {
                this.decimalPlaces = value;
            }
        }

        [DisplayName("VFP Field")]
        [Browsable(true)]
        public string VfpFieldName
        {
            get
            {
                return this.vfpFieldName;
            }
            set
            {
                this.vfpFieldName = value;
            }
        }

        [DisplayName("VFP Type")]
        [Browsable(true)]
        public FieldType VfpFieldType
        {
            get
            {
                return this.vfpFieldType;
            }
            set
            {
                this.vfpFieldType = value;
            }
        }

        [DisplayName(".Net Type")]
        [Browsable(true)]
        public string DotNetType
        {
            get
            {
                return this.dotNetType;
            }
            set
            {
                this.dotNetType = value;
            }
        }

        [DisplayName("Variable")]
        [Browsable(true)]
        public string VariableName
        {
            get
            {
                return this.variableName;
            }
            set
            {
                this.variableName = value;
            }
        }

        [DisplayName("Property")]
        [Browsable(false)]
        public string PropertyName
        {
            get
            {
                return char.ToUpper(this.variableName[0]).ToString() + this.variableName.Substring(1);
            }
        }

        [DisplayName("Display")]
        [Browsable(true)]
        public string HeaderText
        {
            get
            {
                return this.headerText;
            }
            set
            {
                this.headerText = value;
            }
        }

        [DisplayName("ReadOnly")]
        [Browsable(true)]
        public bool ReadOnly
        {
            get
            {
                return this.visible;
            }
            set
            {
                this.visible = value;
            }
        }

        [DisplayName("Search")]
        [Browsable(true)]
        public bool Search
        {
            get
            {
                return this.search;
            }
            set
            {
                this.search = value;
            }
        }

        [DisplayName("Read")]
        [Browsable(true)]
        public bool Read
        {
            get
            {
                return this.read;
            }
            set
            {
                this.read = value;
            }
        }

        [DisplayName("Update")]
        [Browsable(true)]
        public bool Update
        {
            get
            {
                return this.update;
            }
            set
            {
                this.update = value;
            }
        }

        [DisplayName("Insert")]
        [Browsable(true)]
        public bool Insert
        {
            get
            {
                return this.insert;
            }
            set
            {
                this.insert = value;
            }
        }

        [DisplayName("Shallow Copy")]
        [Browsable(true)]
        public bool ShallowCopy
        {
            get
            {
                return this.shallowCopy;
            }
            set
            {
                this.shallowCopy = value;
            }
        }

        [DisplayName("Deep Copy")]
        [Browsable(true)]
        public bool DeepCopy
        {
            get
            {
                return this.deepCopy;
            }
            set
            {
                this.deepCopy = value;
            }
        }
    }
}
