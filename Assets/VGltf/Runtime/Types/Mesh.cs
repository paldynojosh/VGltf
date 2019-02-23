//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using VJson;
using VJson.Schema;

// Reference: https://github.com/KhronosGroup/glTF/blob/master/specification/2.0/schema/*
namespace VGltf.Types
{
    [JsonSchema(Id = "mesh.schema.json")]
    public class Mesh : GltfChildOfRootProperty
    {
        [JsonField(Name = "primitives")]
        [JsonSchema(MinItems=1), JsonSchemaRequired]
        public PrimitiveType[] Primitives;

        [JsonField(Name = "weights")]
        [JsonSchema(MinItems=1)]
        public float[] weights;

        //

        [JsonSchema(Id = "mesh.primitive.schema.json")]
        public class PrimitiveType
        {
        }
    }
}
