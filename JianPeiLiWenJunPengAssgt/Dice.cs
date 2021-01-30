using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JianPeiLiWenJunPengAssgt
{
    //dice class
    class Dice
    {
        //the initial value
        private int dValue;
        Random r;

        public Dice()
        {
            r = new Random();
        }

        #region "Properties"
        public int DValue 
        {
            get
            {
                return dValue;
            }

        }
        #endregion

        //roll function to roll from 1 to 6 
        public void Roll()
        {
            dValue = r.Next(1, 7);
        }

    }
}
