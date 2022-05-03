using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColaLearningUnity
{
    internal class Tensor
    {
        private int x;
        private int y;
        private bool bias;
    }

    internal class TensorConnection
    {
        private Tensor previousTensor;
        private Tensor nextTensor;
    }
}
