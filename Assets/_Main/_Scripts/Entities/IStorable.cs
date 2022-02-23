using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
    public interface IStorable
    {
        public Sprite ShowImage { get;}

        public string ItemName { get;}

        public int ItemValue { get; }
    }
