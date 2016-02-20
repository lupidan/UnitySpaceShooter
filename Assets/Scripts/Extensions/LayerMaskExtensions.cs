///
/// The MIT License(MIT)
/// 
/// Copyright(c) 2016 Daniel Lupiañez Casares
/// 
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
/// 
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.
///

using UnityEngine;
using System.Collections;

public static class LayerMaskExtensions {

    /// <summary>
    /// Returns whether or not a LayerMask contains a specific layer index.
    /// </summary>
    /// <param name="layerMask">The receiver LayerMask.</param>
    /// <param name="layerIndex">The index of the layer we want to check.</param>
    /// <returns>true if the LayerMask contains the index (a.k.a. bit is active), false otherwise</returns>
    public static bool ContainsLayerWithIndex(this LayerMask layerMask, int layerIndex)
    {
        return layerMask == (layerMask | (1 << layerIndex));
    }

    /// <summary>
    /// Returns whether or not a LayerMask contains a specific layer.
    /// </summary>
    /// <param name="layerMask">The receiver LayerMask.</param>
    /// <param name="name">The name of the layer we want to check.</param>
    /// <returns>true if the LayerMask contains the layer (a.k.a. bit is active), false otherwise</returns>
    public static bool ContainsLayerNamed(this LayerMask layerMask, string name)
    {
        return layerMask.ContainsLayerWithIndex(LayerMask.NameToLayer(name));
    }

}
