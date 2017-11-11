using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearProject
{
    class ModelNuclearReactor
    {
        float Ct, pT, Vt, C, P, V, a, FT, t0;

        public ModelNuclearReactor(
            float Ct,
            float pT,
            float Vt,
            float C,
            float P,
            float V,
            float a,
            float FT,
            float t0)
        {
            this.Ct = Ct;
            this.pT = pT;
            this.Vt = Vt;
            this.P = P;
            this.V = V;
            this.a = a;
            this.FT = FT;
            this.t0 = t0;
        }

        public float getCt() { return this.Ct; }
        public float getPt() { return this.pT; }
        public float getVt() { return this.Vt; }
        public float getP() { return this.P; }
        public float getV() { return this.V; }
        public float geta() { return this.a; }
        public float getFT() { return this.FT; }
        public float getT0() { return this.t0; }

    }
}
